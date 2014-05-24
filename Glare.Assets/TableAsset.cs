using Glare.Assets;
using Glare.Assets.Controls;
using Glare.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Glare.Assets {
	public class TableAsset : FolderAsset {
		public TableAsset(AssetManager manager, AssetLoader loader) : base(manager, loader) { }
		public TableAsset(FolderAsset parent, AssetLoader loader) : base(parent, loader) { }
	}

	public abstract class TableAsset<TRow> : TableAsset where TRow : TableRowAsset {
		public List<TableRowPropertyInfo> BrowsableProperties {
			get {
				var type = Children[0].GetType();
				var enumerator = (from p in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy) where IsPropertyBrowsable(p) select p).ToList();
				PropertyTableRowOrderAttribute[] orders = type.GetCustomAttributes<PropertyTableRowOrderAttribute>(true);
				List<KeyValuePair<int, TableRowPropertyInfo>> sortList = new List<KeyValuePair<int, TableRowPropertyInfo>>();

				foreach (PropertyInfo property in enumerator) {
					var info = new TableRowPropertyInfo(property);
					int order = -1;

					string matchName = null;

					if(info.TableRowAttribute != null) {
						matchName = info.Name; 
						order = info.SortOrder;
					}

					if(matchName == null)
						matchName = "@" + property.Name;

					foreach (PropertyTableRowOrderAttribute orderAttribute in orders)
						if (orderAttribute.Id == matchName) {
							order = orderAttribute.SortOrder;
							break;
						}

					sortList.Add(new KeyValuePair<int, TableRowPropertyInfo>(order, info));
				}

				sortList.Sort((a, b) => a.Key.CompareTo(b.Key));

				List<TableRowPropertyInfo> list = new List<TableRowPropertyInfo>(sortList.Count);
				foreach (var pair in sortList)
					list.Add(pair.Value);

				return list;
			}
		}

		public string CommaSeparatedValues {
			get {
				StringBuilder builder = new StringBuilder();
				List<TableRowPropertyInfo> properties = BrowsableProperties;
				bool first = true;

				foreach (TableRowPropertyInfo property in properties) {
					if (!first)
						builder.Append(",");
					first = false;
					builder.AppendFormat("\"{0}\"", property.DisplayName);
				}
				builder.Append("\r\n");

				foreach (TRow child in Children) {
					first = true;
					foreach (TableRowPropertyInfo property in properties) {
						if (!first)
							builder.Append(",");
						first = false;
						var value = property.Property.GetValue(child, null);
						builder.AppendFormat("\"{0}\"", (value ?? "").ToString().Replace("\"", "\"\"").Replace("\r\n", "\\n").Replace("\n", "\\n"));
					}
					builder.Append("\r\n");
				}

				return builder.ToString();
			}
		}

		public override void FillContextMenu(ContextMenuStrip strip) {
			base.FillContextMenu(strip);
			strip.Items.Add(new ToolStripButton("Save .CSV to File...", null, (sender, args) => {
				var dialog = new SaveFileDialog() {
					AddExtension = false,
					DefaultExt = "csv",
					Filter = "Comma-separated values files (*.csv)|*.csv|All files (*.*)|*.*",
					AutoUpgradeEnabled = true,
					Title = "Select file to save to.",
				};

				var text = CommaSeparatedValues;
				DialogResult result = dialog.ShowDialog();
				if (result == DialogResult.OK) {
					File.WriteAllText(dialog.FileName, text, Encoding.UTF8);
				}
			}));
		}

		public TableAsset(AssetManager manager, AssetLoader loader) : base(manager, loader) { }
		public TableAsset(FolderAsset parent, AssetLoader loader) : base(parent, loader) { }

		public override Control Browse() {
			var grid = new DoubleBufferedDataGridView() {
				AutoGenerateColumns = false,
				AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
				AllowUserToResizeColumns = true,
				AllowUserToOrderColumns = true,
				AllowUserToResizeRows = true,
				DataSource = Children,
				ReadOnly = false,
			};

			TRow row = (TRow)Children[0];
			List<TableRowPropertyInfo> properties = BrowsableProperties;
			foreach (TableRowPropertyInfo property in properties) {
				Type propertyType = property.PropertyType;

				DataGridViewColumn column;
				if (propertyType == typeof(bool))
					column = new DataGridViewCheckBoxColumn() { CellTemplate = new MyDataGridViewCheckBoxCell(property) };
				/*else if (propertyType.IsEnum)
					column = new DataGridViewComboBoxColumn() { CellTemplate = new MyDataGridViewComboBoxCell(property) };*/
				else
					column = new DataGridViewTextBoxColumn() { CellTemplate = new MyDataGridViewTextBoxCell(property) };

				column.Resizable = DataGridViewTriState.True;
				column.DataPropertyName = property.Name;
				column.HeaderText = property.DisplayName;
				column.HeaderCell = new DataGridViewColumnHeaderCell() {
					Style = new DataGridViewCellStyle() {
						WrapMode = DataGridViewTriState.True,
					},

					ToolTipText = property.Name + "\n" + property.Description,
					Value = column.HeaderText,
				};
				grid.Columns.Add(column);
			}


//			grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
			
			new Thread(() => {
				while (!grid.IsHandleCreated)
					Thread.Yield();

				foreach (DataGridViewAutoSizeColumnMode mode in new DataGridViewAutoSizeColumnMode[] { DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader, DataGridViewAutoSizeColumnMode.AllCellsExceptHeader }) {
					foreach (DataGridViewColumn outsideColumn in grid.Columns) {
						grid.Invoke((Action<DataGridViewColumn>)((column) => {
							int totalSize = column.GetPreferredWidth(mode, false);
							int headerSize = column.GetPreferredWidth(DataGridViewAutoSizeColumnMode.ColumnHeader, false);

							if (headerSize > totalSize)
								totalSize = (totalSize + headerSize) / 2;
							column.Width = totalSize;
						}), outsideColumn);
						Thread.Sleep(20);
					}
					grid.Invoke((Action)(() => {
						grid.AutoResizeColumnHeadersHeight();
					}));
				}
			}).Start();
			return grid;
		}

		public override Control BrowseContents() {
			return CreateBarPanel(Browse());
		}

		public bool IsPropertyBrowsable(PropertyInfo property) {
			BrowsableAttribute browsable = property.GetCustomAttribute<BrowsableAttribute>(true);
			if (browsable != null && !browsable.Browsable)
				return false;
			if (Children.Count > 0 && !((TRow)Children[0]).IsBrowsable(property))
				return false;
			return true;
		}

		class MyDataGridViewCheckBoxCell : DataGridViewCheckBoxCell {
			readonly TableRowPropertyInfo Property;

			public MyDataGridViewCheckBoxCell(TableRowPropertyInfo property) {
				Property = property;
			}

			public override object Clone() {
				return new MyDataGridViewCheckBoxCell(Property);
			}

			protected override object GetValue(int rowIndex) {
				var target = ((IList)DataGridView.DataSource)[rowIndex];
				var result = Property.GetValue(target, null);
				return result;
			}

			protected override bool SetValue(int rowIndex, object value) {
				var target = ((IList)DataGridView.DataSource)[rowIndex];
				Property.SetValue(target, value, null);
				return true;
			}
		}

		class MyDataGridViewComboBoxCell : DataGridViewComboBoxCell {
			readonly PropertyInfo Property;

			public MyDataGridViewComboBoxCell(PropertyInfo property) {
				Property = property;
			}

			public override object Clone() {
				return new MyDataGridViewComboBoxCell(Property);
			}

			protected override object GetValue(int rowIndex) {
				var target = ((IList)DataGridView.DataSource)[rowIndex];
				var result = Property.GetValue(target, null);
				return result;
			}

			protected override bool SetValue(int rowIndex, object value) {
				var target = ((IList)DataGridView.DataSource)[rowIndex];
				Property.SetValue(target, value, null);
				return true;
			}
		}

		class MyDataGridViewTextBoxCell : DataGridViewTextBoxCell {
			readonly TableRowPropertyInfo Property;

			public MyDataGridViewTextBoxCell(TableRowPropertyInfo property) {
				Property = property;
			}

			public override object Clone() {
				return new MyDataGridViewTextBoxCell(Property);
			}

			protected override object GetValue(int rowIndex) {
				var target = ((IList)DataGridView.DataSource)[rowIndex];
				var result = Property.GetValue(target);
				return result;
			}

			protected override bool SetValue(int rowIndex, object value) {
				var target = ((IList)DataGridView.DataSource)[rowIndex];
				Property.SetValue(target, value);
				return true;
			}
		}
	}

	public class TableRowPropertyInfo {
		object DefaultValueValue;

		#region Attributes

		public DefaultValueAttribute DefaultValueAttribute { get; private set; }
		public DescriptionAttribute DescriptionAttribute { get; private set; }
		public DesignerAttribute DesignerAttribute { get; private set; }
		public DisplayNameAttribute DisplayNameAttribute { get; private set; }
		public TableRowAttribute TableRowAttribute { get; private set; }

		#endregion Attributes

		public object DefaultValue {
			get {
				if (DefaultValueAttribute != null)
					return DefaultValueAttribute.Value;
				if (Property.PropertyType.IsClass)
					return null;

				if (DefaultValueValue != null)
					return DefaultValueValue;
				var constructor = Property.PropertyType.GetConstructor(Type.EmptyTypes);
				return DefaultValueValue = constructor.Invoke(null);
			}
		}

		public string Description { get { return DescriptionAttribute != null ? DescriptionAttribute.Description : null; } }

		/// <summary>Get the <see cref="DisplayNameAttribute.DisplayName"/> from the <see cref="DisplayNameAttribute"/> if there is one; otherwise return <see cref="PropertyInfo.Name"/>.</summary>
		public string DisplayName { get { return DisplayNameAttribute != null ? DisplayNameAttribute.DisplayName : Property.Name; } }

		public bool HasDefaultValue { get { return DefaultValueAttribute != null; } }
		public bool HasDescription { get { return DescriptionAttribute != null; } }
		public bool HasDisplayName { get { return DisplayNameAttribute != null; } }
		public bool HasId { get { return TableRowAttribute != null; } }
		public bool HasMaximum { get { return TableRowAttribute != null; } }
		public bool HasMinimum { get { return TableRowAttribute != null; } }
		public bool HasSortOrder { get { return TableRowAttribute != null; } }

		/// <summary>
		/// Get the <see cref="TableRowAttribute.Id"/> property provided by the <see cref="TableRowAttribute"/> if one is applied to the <see cref="Property"/>; otherwise return <c>null</c>. The <see cref="TableRowAttribute.Id"/> property is a unique identifier used to match this property with a <see cref="PropertyTableRowOrderAttribute"/>.
		/// </summary>
		public string Id { get { return TableRowAttribute != null ? TableRowAttribute.Id : null; } }

		/// <summary>Get te <see cref="TableRowAttribute.Maximum"/> property provided by the <see cref="TableRowAttribute"/> if one is applied to the <see cref="Property"/>; otherwise return <see cref="null"/>. The presence of a valid value for this property can be determined with <see cref="HasMaximum"/>. The <see cref="TableRowAttribute.Maximum"/> property is the maximum value for a scalar property.</summary>
		public object Maximum { get { return TableRowAttribute != null ? TableRowAttribute.Maximum : null; } }

		/// <summary>Get te <see cref="TableRowAttribute.Minimum"/> property provided by the <see cref="TableRowAttribute"/> if one is applied to the <see cref="Property"/>; otherwise return <see cref="null"/>. The presence of a valid value for this property can be determined with <see cref="HasMinimum"/>. The <see cref="TableRowAttribute.Minimum"/> property is the minimum value for a scalar property.</summary>
		public object Minimum { get { return TableRowAttribute != null ? TableRowAttribute.Minimum : null; } }

		public string Name { get { return Property.Name; } }

		public PropertyInfo Property { get; private set; }

		public Type PropertyType { get { return Property.PropertyType; } }

		public int SortOrder { get { return TableRowAttribute != null ? TableRowAttribute.SortOrder : -1; } }

		public TableRowPropertyInfo(PropertyInfo property) {
			Property = property;

			object[] attributes = property.GetCustomAttributes(true);

			foreach (Attribute attribute in attributes) {
				DefaultValueAttribute = DefaultValueAttribute ?? (attribute as DefaultValueAttribute);
				DescriptionAttribute = DescriptionAttribute ?? (attribute as DescriptionAttribute);
				DesignerAttribute = DesignerAttribute ?? (attribute as DesignerAttribute);
				DisplayNameAttribute = DisplayNameAttribute ?? (attribute as DisplayNameAttribute);
				TableRowAttribute = TableRowAttribute ?? (attribute as TableRowAttribute);
			}
		}

		public object GetValue(object source, object[] index = null) { return Property.GetValue(source, index); }
		public T GetValue<T>(object source, object[] index = null) { return (T)Property.GetValue(source, index); }
		public T SetValue<T>(object source, T value, object[] index = null) { Property.SetValue(source, value, index); return value; }

		public override string ToString() {
			return string.Format("{0}({1}.{2})", GetType().Name, Property.DeclaringType.Name, Property.Name);
		}
	}

	public abstract class TableRowAsset : Asset {
		[Browsable(false)]
		public new TableAsset Parent { get { return (TableAsset)base.Parent; } }

		public TableRowAsset(TableAsset table, int index)
			: base(table, table.Name + " row " + index) {
		}

		internal protected virtual bool IsBrowsable(PropertyInfo property) {
			return true;
		}
	}

	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class TableRowAttribute : Attribute {
		public string Id { get; private set; }
		public object Maximum { get; private set; }
		public object Minimum { get; private set; }
		public int SortOrder { get; private set; }

		public TableRowAttribute(string id, object minimum = null, object maximum = null, int sortOrder = -1) {
			Minimum = minimum;
			Maximum = maximum;
			SortOrder = sortOrder;
		}
	}

	/// <summary>This can be applied to a <see cref="TableRow"/> to override the order of a property that has a <see cref="TableRowAttribute"/>.</summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	public class PropertyTableRowOrderAttribute : Attribute {
		public string Id { get; private set; }
		public int SortOrder { get; private set; }

		public PropertyTableRowOrderAttribute(string id, int sortOrder) {
			Id = id;
			SortOrder = sortOrder;
		}
	}
}
