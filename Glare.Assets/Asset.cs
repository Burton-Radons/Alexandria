using Glare.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Glare.Internal;
using Glare.Assets.Controls;
using System.ComponentModel;
using System.Reflection;

namespace Glare.Assets {
	public abstract class Asset : INotifyPropertyChanged, INotifyPropertyChanging {
		AssetFormat assetFormat;
		readonly RichList<Asset> children = new RichList<Asset>();
		bool isModified;
		bool isReadOnly;
		readonly AssetManager manager;
		Asset parent;
		RichList<AssetLoadError> loadErrors;
		UnknownList unknowns;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		string name, description;

		static readonly PropertyInfo AssetFormatProperty = GetProperty<Asset>("AssetFormat");
		static readonly PropertyInfo DescriptionProperty = GetProperty<Asset>("Description");
		static readonly PropertyInfo IsModifiedProperty = GetProperty<Asset>("IsModified");
		static readonly PropertyInfo IsReadOnlyProperty = GetProperty<Asset>("IsReadOnly");
		static readonly PropertyInfo LoadErrorsProperty = GetProperty<Asset>("LoadErrors");
		static readonly PropertyInfo NameProperty = GetProperty<Asset>("Name");
		static readonly PropertyInfo ParentProperty = GetProperty<Asset>("Parent");

		/// <summary>Get the <see cref="AssetFormat"/> that was used to load this asset, or <c>null</c> if there was none.</summary>
		[Browsable(false)]
		public AssetFormat AssetFormat {
			get { return assetFormat; }
			protected set { SetProperty(ref assetFormat, ref value, AssetFormatProperty); }
		}

		[Browsable(false)]
		public ReadOnlyList<Asset> Children { get { return children; } }

		[Browsable(false)]
		public virtual string Description {
			get { return GetResourceString(description, "Description"); }
			protected set { SetProperty(ref description, ref value, DescriptionProperty); }
		}

		[Browsable(false)]
		public virtual string DisplayName { get { return Name ?? GetType().Name + " resource"; } }

		[Browsable(false)]
		public static Encoding EncodingShiftJis { get { return Encoding.GetEncoding("shift-jis"); } }

		/// <summary>Get this <see cref="Asset"/> as something that Glare's <see cref="ResourceManager"/> might want to save/load, or return <c>null</c> (the default).</summary>
		[Browsable(false)]
		public virtual object GlareObject { get { return null; } }

		[Browsable(false)]
		public bool HasUnknowns { get { return unknowns != null && unknowns.Count > 0; } }

		/// <summary>Get whether this <see cref="Asset"/> or any children have been locked from modification.</summary>
		[Browsable(false)]
		public bool IsAnyModified {
			get {
				if (IsModified)
					return true;
				foreach (Asset child in children)
					if (child.IsModified)
						return true;
				return false;
			}
		}

		/// <summary>Get whether the <see cref="Asset"/> is modified.</summary>
		[Browsable(false)]
		public bool IsModified {
			get { return isModified; }
			protected set { SetProperty(ref isModified, ref value, IsModifiedProperty); }
		}

		/// <summary>Get whether the <see cref="Asset"/> cannot be modified in any way. The default is <c>true</c>.</summary>
		[Browsable(false)]
		public bool IsReadOnly {
			get { return isReadOnly; }
			protected set { SetProperty(ref isReadOnly, ref value, IsReadOnlyProperty); }
		}

		/// <summary>Get a collection of errors that were recorded while trying to load this <see cref="Asset"/>, or <c>null</c> if this <see cref="Asset"/> hasn't been loaded.</summary>
		[Browsable(false)]
		public RichList<AssetLoadError> LoadErrors {
			get { return loadErrors; }
			internal set { SetProperty(ref loadErrors, ref value, LoadErrorsProperty); }
		}

		[Browsable(false)]
		public Asset LoadContext { get; private set; }

		[Browsable(false)]
		public string LoadName { get; private set; }

		[Browsable(false)]
		public AssetManager Manager { get { return manager; } }

		public virtual string Name {
			get { return GetResourceString(name, "Name"); }
			set { SetProperty(ref name, ref value, NameProperty); }
		}

		[Browsable(false)]
		public Asset Parent {
			get { return parent; }

			protected set {
				if (parent != null)
					parent.children.Remove(this);
				if (value != null)
					value.children.Add(this);
				SetProperty(ref parent, ref value, ParentProperty);
			}
		}

		[Browsable(false)]
		public virtual string PathName { get { return Name; } }

		[Browsable(false)]
		public ResourceManager ResourceManager { get; private set; }

		[Browsable(false)]
		protected virtual string ResourcePrefix { get { return GetType().Name; } }

		[Browsable(false)]
		public UnknownList Unknowns { get { return unknowns ?? (unknowns = new UnknownList()); } }

		public event PropertyChangedEventHandler PropertyChanged;

		public event PropertyChangingEventHandler PropertyChanging;

		Asset(AssetManager manager) {
			if (manager == null)
				throw new ArgumentNullException("manager");
			this.manager = manager;
			IsReadOnly = true;
		}

		public Asset(AssetManager manager, ResourceManager resourceManager)
			: this(manager) {
			if (resourceManager == null)
				throw new ArgumentNullException("resourceManager");
			this.ResourceManager = resourceManager;
		}

		public Asset(AssetManager manager, string name, string description = null)
			: this(manager) {
			if (name == null)
				throw new ArgumentNullException("name");
			Name = name;
			Description = description;
		}

		public Asset(AssetManager manager, ResourceManager resourceManager, string name, string description = null) {
			if (resourceManager == null) {
				if (name == null)
					throw new ArgumentNullException("name");
			}

			ResourceManager = resourceManager;
			Name = name;
			Description = description;
		}

		public Asset(FolderAsset parent, string name, string description = null)
			: this(parent.Manager, name, description) {
			Parent = parent;
		}

		public Asset(FolderAsset parent, AssetLoader loader)
			: this(parent.Manager, loader) {
			Parent = parent;
		}

		public Asset(AssetManager manager, AssetLoader loader)
			: this(manager, loader.Name) {
			LoadContext = loader.Context;
			LoadName = loader.Name;
			LoadErrors = loader.Errors;
		}

		protected void AddChild(Asset child) {
			if (child == null)
				throw new ArgumentNullException("child");
			child.Parent = this;
		}

		public virtual Control Browse() {
			return null;
		}

		public virtual Control BrowseContents() {
			return CreateBarPanel(Browse());
		}

		protected TableLayoutPanel CreateBarPanel(Control contents) {
			var table = new TableLayoutPanel() {
				ColumnCount = 1,
				RowCount = 2,
				Dock = DockStyle.Fill,
			};

			var bar = new AssetBar() {
				Asset = this,
				Dock = DockStyle.Fill,
			};

			table.Controls.Add(bar, 0, 0);
			if (contents != null) {
				contents.Dock = DockStyle.Fill;
				table.Controls.Add(contents, 0, 1);
			}

			table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
			table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

			table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

			return table;
		}

		public virtual void FillContextMenu(ContextMenuStrip strip) {
		}

		public ContextMenuStrip GetContextMenu() {
			ContextMenuStrip strip = new ContextMenuStrip() {
				AutoSize = true,
			};

			strip.Items.Add(new ToolStripButton("Copy \"" + Name + "\" to clipboard", null, (sender, args) => {
				Clipboard.SetText(Name);
			}));

			if (Name != PathName) {
				strip.Items.Add(new ToolStripButton("Copy \"" + PathName + "\" to clipboard", null, (sender, args) => {
					Clipboard.SetText(PathName);
				}));
			}

			if (LoadErrors != null && LoadErrors.Count > 0) {
				var errors = LoadErrors;
				strip.Items.Add(new ToolStripButton("Show load errors", null, (sender, args) => { ShowLoadErrors(errors); }));
			}

			FillContextMenu(strip);

			strip.AutoSize = false;
			strip.AutoSize = true;

			return strip;
		}

		protected static PropertyInfo GetProperty<T>(string name) {
			var info = typeof(T).GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (info == null)
				throw new InvalidOperationException("Could not find property '" + name + "' in type " + typeof(T));
			return info;
		}

		string GetResourceString(string value, string suffix) {
			if (value != null)
				return value;
			if (ResourceManager == null)
				return value;

			try {
				return ResourceManager.GetString(ResourcePrefix + suffix);
			} catch (MissingManifestResourceException) {
			}

			return "?" + ResourcePrefix + suffix + "?";
		}

		protected void MoveIntoPath(string path) {
			MoveIntoPath(path.Split('/', '\\'));
		}

		protected void MoveIntoPath(params string[] path) {
			int index = 0;
			if (path[0].Length == 0)
				index = 1;

			Asset target = Parent;
			for (; index < path.Length - 1; index++) {
				string name = path[index];
				bool found = false;

				foreach (Asset child in target.Children)
					if (child is FolderAsset && child.Name == name) {
						target = child;
						found = true;
					}

				if (!found)
					target = new FolderAsset((FolderAsset)target, name);
			}

			Parent = target;
			Name = path[index];
		}

		static readonly Dictionary<PropertyInfo, PropertyChangedEventArgs> ChangedEventArgs = new Dictionary<PropertyInfo, PropertyChangedEventArgs>();

		static readonly Dictionary<PropertyInfo, PropertyChangingEventArgs> ChangingEventArgs = new Dictionary<PropertyInfo, PropertyChangingEventArgs>();

		protected void SetProperty<T>(ref T slot, ref T value, PropertyInfo property) {
			OnPropertyChanging(ref slot, ref value, property);
			slot = value;
			OnPropertyChanged(ref slot, ref value, property);
		}

		protected virtual void OnPropertyChanged<T>(ref T slot, ref T value, PropertyInfo property) {
			if (PropertyChanged != null) {
				PropertyChangedEventArgs args;

				lock (ChangedEventArgs) {
					if (!ChangedEventArgs.TryGetValue(property, out args))
						args = ChangedEventArgs[property] = new PropertyChangedEventArgs(property.Name);
				}

				PropertyChanged(this, args);
			}
		}

		protected virtual void OnPropertyChanging<T>(ref T slot, ref T value, PropertyInfo property) {
			if (PropertyChanging != null) {
				PropertyChangingEventArgs args;

				lock (ChangingEventArgs) {
					if (!ChangingEventArgs.TryGetValue(property, out args))
						args = ChangingEventArgs[property] = new PropertyChangingEventArgs(property.Name);
				}

				PropertyChanging(this, args);
			}
		}

		protected void SortChildrenRecursively() {
			children.Sort((a, b) => a.Name.CompareNumeric(b.Name));
			foreach (Asset child in children)
				child.SortChildrenRecursively();
		}

		void ShowLoadErrors(IList<AssetLoadError> list) {
			if (list == null || list.Count == 0)
				return;

			var window = new Form() {
				Text = DisplayName,
			};

			var view = new DataGridView() {
				AutoGenerateColumns = false,
				DataSource = list,
				Dock = DockStyle.Fill,
				ReadOnly = true,
			};

			view.Columns.Add(new DataGridViewTextBoxColumn() {
				DataPropertyName = "Offset",
				HeaderText = "Offset",
				SortMode = DataGridViewColumnSortMode.Automatic,
				AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
			});

			view.Columns.Add(new DataGridViewTextBoxColumn() {
				DataPropertyName = "Message",
				HeaderText = "Message",
				SortMode = DataGridViewColumnSortMode.Automatic,
				AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
			});

			window.Controls.Add(view);
			window.ClientSize = new System.Drawing.Size(view.PreferredSize.Width + 4, view.PreferredSize.Height + 4);
			window.Show();
		}
	}
}
