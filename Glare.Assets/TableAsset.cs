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
using System.Windows.Forms;

namespace Glare.Assets {
	public class TableAsset : FolderAsset {
		public TableAsset(AssetManager manager, AssetLoader loader) : base(manager, loader) { }
		public TableAsset(FolderAsset parent, AssetLoader loader) : base(parent, loader) { }
	}

	public abstract class TableAsset<TRow> : TableAsset where TRow : TableRowAsset {
		public List<PropertyInfo> BrowsableProperties {
			get {
				var enumerator = (from p in Children[0].GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy) where IsPropertyBrowsable(p) select p).ToList();
				PropertyTableRowOrderAttribute[] orders = GetType().GetCustomAttributes<PropertyTableRowOrderAttribute>(true);
				List<KeyValuePair<int, PropertyInfo>> sortList = new List<KeyValuePair<int, PropertyInfo>>();

				foreach (PropertyInfo property in enumerator) {
					var attribute = property.GetCustomAttribute<TableRowAttribute>(true);
					int order = -1;

					if (attribute != null) {
						order = attribute.SortOrder;
						foreach (PropertyTableRowOrderAttribute orderAttribute in orders)
							if (orderAttribute.Id == attribute.Id) {
								order = orderAttribute.SortOrder;
								break;
							}
					}

					sortList.Add(new KeyValuePair<int, PropertyInfo>(order, property));
				}

				sortList.Sort((a, b) => a.Key.CompareTo(b.Key));

				List<PropertyInfo> list = new List<PropertyInfo>(sortList.Count);
				foreach (var pair in sortList)
					list.Add(pair.Value);

				return list;
			}
		}

		public string CommaSeparatedValues {
			get {
				StringBuilder builder = new StringBuilder();
				List<PropertyInfo> properties = BrowsableProperties;
				bool first = true;

				foreach (PropertyInfo property in properties) {
					if (!first)
						builder.Append(",");
					first = false;
					builder.AppendFormat("\"{0}\"", property.Name);
				}
				builder.Append("\r\n");

				foreach (TRow child in Children) {
					first = true;
					foreach (PropertyInfo property in properties) {
						if (!first)
							builder.Append(",");
						first = false;
						var value = property.GetValue(child, null);
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
				DataSource = Children,
				AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells,
				AllowUserToResizeColumns = true,
				AllowUserToOrderColumns = true,
				AllowUserToResizeRows = true,
			};

			TRow row = (TRow)Children[0];
			List<PropertyInfo> properties = BrowsableProperties;
			foreach (PropertyInfo property in properties) {
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
				column.HeaderText = property.Name;
				column.HeaderCell = new DataGridViewColumnHeaderCell() {
					Value = column.HeaderText,
				};
				grid.Columns.Add(column);
			}

			grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
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
			readonly PropertyInfo Property;

			public MyDataGridViewCheckBoxCell(PropertyInfo property) {
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
			readonly PropertyInfo Property;

			public MyDataGridViewTextBoxCell(PropertyInfo property) {
				Property = property;
			}

			public override object Clone() {
				return new MyDataGridViewTextBoxCell(Property);
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
	}

	public class TableRowPropertyInfo {
		readonly DefaultValueAttribute DefaultValueAttribute;
		object DefaultValueValue;
		readonly DescriptionAttribute DescriptionAttribute;
		readonly DisplayNameAttribute DisplayNameAttribute;
		readonly TableRowAttribute TableRowAttribute;

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

		public string DisplayName { get { return DisplayNameAttribute != null ? DisplayNameAttribute.DisplayName : Property.Name; } }

		public bool HasDefaultValue { get { return DefaultValueAttribute != null; } }
		public bool HasDisplayName { get { return DisplayNameAttribute != null; } }
		public bool HasId { get { return TableRowAttribute != null; } }
		public bool HasMaximum { get { return TableRowAttribute != null; } }
		public bool HasMinimum { get { return TableRowAttribute != null; } }

		/// <summary>
		/// Get the <see cref="TableRowAttribute.Id"/> property provided by the <see cref="TableRowAttribute"/> if one is applied to the <see cref="Property"/>; otherwise return <c>null</c>. The <see cref="TableRowAttribute.Id"/> property is a unique identifier used to match this property with a <see cref="PropertyTableRowOrderAttribute"/>.
		/// </summary>
		public string Id { get { return TableRowAttribute != null ? TableRowAttribute.Id : null; } }

		public object Maximum { get { return TableRowAttribute != null ? TableRowAttribute.Maximum : null; } }

		public object Minimum { get { return TableRowAttribute != null ? TableRowAttribute.Minimum : null; } }

		public PropertyInfo Property { get; private set; }

		public Type PropertyType { get { return Property.PropertyType; } }

		public TableRowPropertyInfo(PropertyInfo property) {
			Property = property;

			object[] attributes = property.GetCustomAttributes(true);

			foreach (Attribute attribute in attributes) {
				DefaultValueAttribute = DefaultValueAttribute ?? (attribute as DefaultValueAttribute);
				DescriptionAttribute = DescriptionAttribute ?? (attribute as DescriptionAttribute);
				DisplayNameAttribute = DisplayNameAttribute ?? (attribute as DisplayNameAttribute);
				TableRowAttribute = TableRowAttribute ?? (attribute as TableRowAttribute);
			}
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
