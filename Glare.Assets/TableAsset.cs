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
	/// <summary>
	/// A basic table asset.
	/// </summary>
	public class TableAsset : FolderAsset {
		/// <summary>Initialise the table asset.</summary>
		/// <param name="loader"></param>
		public TableAsset(AssetLoader loader) : base(loader) { }

		/// <summary>Initialise the table asset.</summary>
		/// <param name="parent"></param>
		/// <param name="loader"></param>
		public TableAsset(FolderAsset parent, AssetLoader loader) : base(parent, loader) { }
	}

	/// <summary>
	/// A basic table asset.
	/// </summary>
	/// <typeparam name="TRow">The type of a row in the asset.</typeparam>
	public abstract class TableAsset<TRow> : TableAsset where TRow : TableRowAsset {
		/// <summary>Get the browsable properties.</summary>
		public List<TableRowPropertyInfo> BrowsableProperties {
			get {
				var type = Children[0].GetType();
				var enumerator = (from p in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy) where IsPropertyBrowsable(p) select p).ToList();
				IEnumerable<PropertyTableRowOrderAttribute> orders = type.GetCustomAttributes<PropertyTableRowOrderAttribute>(true);
				List<KeyValuePair<int, TableRowPropertyInfo>> sortList = new List<KeyValuePair<int, TableRowPropertyInfo>>();

				foreach (PropertyInfo property in enumerator) {
					var info = new TableRowPropertyInfo(property);
					int order = -1;

					string matchName = null;

					if(info.AttributeTableRow != null) {
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

		/// <summary>Get the comma-separated values for the rows.</summary>
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

		/// <summary>Fill the context menu.</summary>
		/// <param name="strip"></param>
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

		/// <summary>Initialise the table asset.</summary>
		/// <param name="loader"></param>
		public TableAsset(AssetLoader loader) : base(loader) { }

		/// <summary>Initialise the table asset.</summary>
		/// <param name="parent"></param>
		/// <param name="loader"></param>
		public TableAsset(FolderAsset parent, AssetLoader loader) : base(parent, loader) { }

		/// <summary>Create a control to view the table asset.</summary>
		/// <param name="progressUpdateCallback"></param>
		/// <returns></returns>
		public override Control Browse(Action<double> progressUpdateCallback = null) {
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

		/// <summary>Browse the contents of the table.</summary>
		/// <param name="progressUpdateCallback"></param>
		/// <returns></returns>
		public override Control BrowseContents(Action<double> progressUpdateCallback = null) {
			return CreateBarPanel(Browse(progressUpdateCallback));
		}

		/// <summary>Get whether this property can be browsed, meaning it does not have a <see cref="BrowsableAttribute"/> with a <see cref="BrowsableAttribute.Browsable"/> value of <c>false</c>, and the first table row's, <see cref="TableRowAsset"/>'s <see cref="TableRowAsset.IsBrowsable"/> method returns <c>true</c>.</summary>
		/// <param name="property"></param>
		/// <returns></returns>
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

	/// <summary>
	/// Information about a property.
	/// </summary>
	public class TableRowPropertyInfo {
		object DefaultValueValue;

		#region Attributes

		/// <summary>The default value attribute, if any.</summary>
		public DefaultValueAttribute AttributeDefaultValue { get; private set; }

		/// <summary>The description attribute, if any.</summary>
		public DescriptionAttribute AttributeDescription { get; private set; }

		/// <summary>The designer attribute, if any.</summary>
		public DesignerAttribute AttributeDesigner { get; private set; }

		/// <summary>The display name attribute, if any.</summary>
		public DisplayNameAttribute AttributeDisplayName { get; private set; }

		/// <summary>The table row attribute, if any.</summary>
		public TableRowAttribute AttributeTableRow { get; private set; }

		#endregion Attributes

		/// <summary>Get the default value of the property from the <see cref="AttributeDefaultValue"/>; if there is none, then the default constructor for structs is used or <c>null</c> for classes.</summary>
		public object DefaultValue {
			get {
				if (AttributeDefaultValue != null)
					return AttributeDefaultValue.Value;
				if (Property.PropertyType.IsClass)
					return null;

				if (DefaultValueValue != null)
					return DefaultValueValue;
				var constructor = Property.PropertyType.GetConstructor(Type.EmptyTypes);
				return DefaultValueValue = constructor.Invoke(null);
			}
		}

		/// <summary>Get the description of the property from the <see cref="AttributeDescription"/>; if there is none, then <c>null</c> is returned.</summary>
		public string Description { get { return AttributeDescription != null ? AttributeDescription.Description : null; } }

		/// <summary>Get the <see cref="DisplayNameAttribute.DisplayName"/> from the <see cref="AttributeDisplayName"/> if there is one; otherwise return <see cref="PropertyInfo"/>'s Name.</summary>
		public string DisplayName { get { return AttributeDisplayName != null ? AttributeDisplayName.DisplayName : Property.Name; } }

		/// <summary>Whether <see cref="AttributeDefaultValue"/> is non-<c>null</c>, giving <see cref="DefaultValue"/> a specific value.</summary>
		public bool HasDefaultValue { get { return AttributeDefaultValue != null; } }

		/// <summary>Whether <see cref="AttributeDescription"/> is non-<c>null</c>, giving <see cref="Description"/> a non-<c>null</c> value.</summary>
		public bool HasDescription { get { return AttributeDescription != null; } }

		/// <summary>Whether <see cref="AttributeDisplayName"/> is non-<c>null</c>, giving <see cref="DisplayName"/> a specific value.</summary>
		public bool HasDisplayName { get { return AttributeDisplayName != null; } }

		/// <summary>Whether <see cref="AttributeTableRow"/> is non-<c>null</c>, giving <see cref="Id"/> a non-<c>null</c> value.</summary>
		public bool HasId { get { return AttributeTableRow != null; } }

		/// <summary>Whether <see cref="AttributeTableRow"/> is non-<c>null</c>, giving <see cref="Maximum"/> a specific value.</summary>
		public bool HasMaximum { get { return AttributeTableRow != null; } }

		/// <summary>Whether <see cref="AttributeTableRow"/> is non-<c>null</c>, giving <see cref="Minimum"/> a specific value.</summary>
		public bool HasMinimum { get { return AttributeTableRow != null; } }

		/// <summary>Whether <see cref="AttributeTableRow"/> is non-<c>null</c>, giving <see cref="SortOrder"/> a specific value.</summary>
		public bool HasSortOrder { get { return AttributeTableRow != null; } }

		/// <summary>
		/// Get the <see cref="TableRowAttribute.Id"/> property provided by the <see cref="AttributeTableRow"/> if one is applied to the <see cref="Property"/>; otherwise return <c>null</c>. The <see cref="TableRowAttribute.Id"/> property is a unique identifier used to match this property with a <see cref="PropertyTableRowOrderAttribute"/>.
		/// </summary>
		public string Id { get { return AttributeTableRow != null ? AttributeTableRow.Id : null; } }

		/// <summary>Get te <see cref="TableRowAttribute.Maximum"/> property provided by the <see cref="AttributeTableRow"/> if one is applied to the <see cref="Property"/>; otherwise return <c>null</c>. The presence of a valid value for this property can be determined with <see cref="HasMaximum"/>. The <see cref="TableRowAttribute.Maximum"/> property is the maximum value for a scalar property.</summary>
		public object Maximum { get { return AttributeTableRow != null ? AttributeTableRow.Maximum : null; } }

		/// <summary>Get te <see cref="TableRowAttribute.Minimum"/> property provided by the <see cref="AttributeTableRow"/> if one is applied to the <see cref="Property"/>; otherwise return <c>null</c>. The presence of a valid value for this property can be determined with <see cref="HasMinimum"/>. The <see cref="TableRowAttribute.Minimum"/> property is the minimum value for a scalar property.</summary>
		public object Minimum { get { return AttributeTableRow != null ? AttributeTableRow.Minimum : null; } }

		/// <summary>Get the property's name.</summary>
		public string Name { get { return Property.Name; } }

		/// <summary>Get the base property info.</summary>
		public PropertyInfo Property { get; private set; }

		/// <summary>Get the property type.</summary>
		public Type PropertyType { get { return Property.PropertyType; } }

		/// <summary>Get the sort order of this property in the columns if the <see cref="AttributeTableRow"/> provides one; otherwise this is -1.</summary>
		public int SortOrder { get { return AttributeTableRow != null ? AttributeTableRow.SortOrder : -1; } }

		/// <summary>
		/// Initialise the table row property info.
		/// </summary>
		/// <param name="property"></param>
		public TableRowPropertyInfo(PropertyInfo property) {
			Property = property;

			object[] attributes = property.GetCustomAttributes(true);

			foreach (Attribute attribute in attributes) {
				AttributeDefaultValue = AttributeDefaultValue ?? (attribute as DefaultValueAttribute);
				AttributeDescription = AttributeDescription ?? (attribute as DescriptionAttribute);
				AttributeDesigner = AttributeDesigner ?? (attribute as DesignerAttribute);
				AttributeDisplayName = AttributeDisplayName ?? (attribute as DisplayNameAttribute);
				AttributeTableRow = AttributeTableRow ?? (attribute as TableRowAttribute);
			}
		}

		/// <summary>Get the value of the property.</summary>
		/// <param name="source"></param>
		/// <param name="index"></param>
		/// <returns></returns>
		public object GetValue(object source, object[] index = null) { return Property.GetValue(source, index); }

		/// <summary>Get the value of the property.</summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <param name="index"></param>
		/// <returns></returns>
		public T GetValue<T>(object source, object[] index = null) { return (T)Property.GetValue(source, index); }

		/// <summary>Set the value of the property.</summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <param name="value"></param>
		/// <param name="index"></param>
		/// <returns></returns>
		public T SetValue<T>(object source, T value, object[] index = null) { Property.SetValue(source, value, index); return value; }

		/// <summary>Convert to a string representation of the object.</summary>
		/// <returns></returns>
		public override string ToString() {
			return string.Format("{0}({1}.{2})", GetType().Name, Property.DeclaringType.Name, Property.Name);
		}
	}

	/// <summary>A row in a <see cref="TableAsset{T}"/>.</summary>
	public abstract class TableRowAsset : Asset {
		/// <summary>Get the containing <see cref="TableAsset"/>.</summary>
		[Browsable(false)]
		public new TableAsset Parent { get { return (TableAsset)base.Parent; } }

		/// <summary>Initialise the row.</summary>
		/// <param name="table"></param>
		/// <param name="index"></param>
		public TableRowAsset(TableAsset table, int index)
			: base(table, table.Name + " row " + index) {
		}

		/// <summary>Get whether a property can be browsed.</summary>
		/// <param name="property"></param>
		/// <returns></returns>
		internal protected virtual bool IsBrowsable(PropertyInfo property) {
			return true;
		}
	}

	/// <summary>An attribute applied to properties that provides information on the property.</summary>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class TableRowAttribute : Attribute {
		/// <summary>Used to identify the property.</summary>
		public string Id { get; private set; }

		/// <summary>The maximum value or <c>null</c> for none.</summary>
		public object Maximum { get; private set; }

		/// <summary>The minimum value or <c>null</c> for none.</summary>
		public object Minimum { get; private set; }

		/// <summary>The sort order or -1 for none.</summary>
		public int SortOrder { get; private set; }

		/// <summary>Initialise the attribute.</summary>
		/// <param name="id"></param>
		/// <param name="minimum"></param>
		/// <param name="maximum"></param>
		/// <param name="sortOrder"></param>
		public TableRowAttribute(string id, object minimum = null, object maximum = null, int sortOrder = -1) {
			Minimum = minimum;
			Maximum = maximum;
			SortOrder = sortOrder;
		}
	}

	/// <summary>This can be applied to a <see cref="TableRowAsset"/> to override the order of a property that has a <see cref="TableRowAttribute"/>.</summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	public class PropertyTableRowOrderAttribute : Attribute {
		/// <summary>Used to identify the property.</summary>
		public string Id { get; private set; }

		/// <summary>The sort order or -1 for none.</summary>
		public int SortOrder { get; private set; }

		/// <summary>Initialise the attribute.</summary>
		/// <param name="id"></param>
		/// <param name="sortOrder"></param>
		public PropertyTableRowOrderAttribute(string id, int sortOrder) {
			Id = id;
			SortOrder = sortOrder;
		}
	}
}
