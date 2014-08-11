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
	/// <summary>
	/// Abstract base of the asset objects, which contain information loaded from and saved to files.
	/// </summary>
	public abstract class Asset : NotifyingObject {
		AssetFormat assetFormat;
		readonly Codex<Asset> children = new Codex<Asset>();
		bool isModified;
		bool isReadOnly;
		readonly AssetManager manager;
		Asset parent;
		Codex<AssetLoadError> loadErrors;
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
			protected set { SetProperty(AssetFormatProperty, ref assetFormat, ref value); }
		}

		/// <summary>Get the children of the asset.</summary>
		[Browsable(false)]
		public virtual ReadOnlyCodex<Asset> Children { get { return children; } }

		/// <summary>
		/// Get the description of the asset.
		/// </summary>
		[Browsable(false)]
		public virtual string Description {
			get { return GetResourceString(description, "Description"); }
			protected set { SetProperty(DescriptionProperty, ref description, ref value); }
		}

		/// <summary>
		/// Get the display name of the asset.
		/// </summary>
		[Browsable(false)]
		public virtual string DisplayName { get { return Name ?? GetType().Name + " resource"; } }

		/// <summary>
		/// Get the "Shift-Jis" encoding.
		/// </summary>
		[Browsable(false)]
		public static Encoding EncodingShiftJis { get { return Encoding.GetEncoding("shift-jis"); } }

		/// <summary>Get this <see cref="Asset"/> as something that Glare's <see cref="ResourceManager"/> might want to save/load, or return <c>null</c> (the default).</summary>
		[Browsable(false)]
		public virtual object GlareObject { get { return null; } }

		/// <summary>Get whether this <see cref="Asset"/> has any <see cref="Children"/>. This is a separate property because some <see cref="Asset"/>s might not load their <see cref="Children"/> until that property is accessed, but this property doesn't force loading. So if all that is needed is whether the <see cref="Asset"/> has <see cref="Children"/>, then this could be executed faster.</summary>
		[Browsable(false)]
		public virtual bool HasChildren { get { return Children.Count > 0; } }

		/// <summary>
		/// Get whether <see cref="Unknowns"/> has any value.
		/// </summary>
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
			protected set { SetProperty(IsModifiedProperty, ref isModified, ref value); }
		}

		/// <summary>Get whether the <see cref="Asset"/> cannot be modified in any way. The default is <c>true</c>.</summary>
		[Browsable(false)]
		public bool IsReadOnly {
			get { return isReadOnly; }
			protected set { SetProperty(IsReadOnlyProperty, ref isReadOnly, ref value); }
		}

		/// <summary>Get a collection of errors that were recorded while trying to load this <see cref="Asset"/>, or <c>null</c> if this <see cref="Asset"/> hasn't been loaded.</summary>
		[Browsable(false)]
		public Codex<AssetLoadError> LoadErrors {
			get { return loadErrors; }
			internal set { SetProperty(LoadErrorsProperty, ref loadErrors, ref value); }
		}

		/// <summary>
		/// Get the context the <see cref="AssetLoader"/> had when this asset was loaded, or <c>null</c> for none.
		/// </summary>
		[Browsable(false)]
		public Asset LoadContext { get; private set; }

		/// <summary>
		/// Get the name of the <see cref="AssetLoader"/> had when this asset was loaded, or <c>null</c> for none.
		/// </summary>
		[Browsable(false)]
		public string LoadName { get; private set; }

		/// <summary>Get the <see cref="FileManager"/> that was used when this asset was loaded, or <c>null</c> for none.</summary>
		[Browsable(false)]
		public FileManager LoadFileManager { get; private set; }

		/// <summary>
		/// Get the asset manager.
		/// </summary>
		[Browsable(false)]
		public AssetManager Manager { get { return manager; } }

		/// <summary>
		/// Get the name of this asset.
		/// </summary>
		public virtual string Name {
			get { return GetResourceString(name, "Name"); }
			set { SetProperty(NameProperty, ref name, ref value); }
		}

		/// <summary>
		/// Get the parent asset or <c>null</c> if it has none.
		/// </summary>
		[Browsable(false)]
		public Asset Parent {
			get { return parent; }

			protected set {
				if (parent != null)
					parent.children.Remove(this);
				if (value != null)
					value.children.Add(this);
				SetProperty(ParentProperty, ref parent, ref value);
			}
		}

		/// <summary>
		/// Get the path to this asset.
		/// </summary>
		[Browsable(false)]
		public virtual string PathName { get { return Name; } }

		/// <summary>
		/// Get the resource manager for localization, or <c>null</c> for none.
		/// </summary>
		[Browsable(false)]
		public ResourceManager ResourceManager { get; private set; }

		/// <summary>
		/// Get the resource name prefix for localization; the default returns this type's name.
		/// </summary>
		[Browsable(false)]
		protected virtual string ResourcePrefix { get { return GetType().Name; } }

		/// <summary>
		/// Get unknown information found when loading the asset.
		/// </summary>
		[Browsable(false)]
		public UnknownList Unknowns { get { return unknowns ?? (unknowns = new UnknownList()); } }

		/// <summary>
		/// An event that's invoked when a property's changed in the asset.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// An event that's invoked when a property's changed in the asset.
		/// </summary>
		public event PropertyChangingEventHandler PropertyChanging;

		Asset(AssetManager manager) {
			if (manager == null)
				throw new ArgumentNullException("manager");
			this.manager = manager;
			IsReadOnly = true;
		}

		/// <summary>
		/// Initialise the asset.
		/// </summary>
		/// <param name="manager"></param>
		/// <param name="resourceManager"></param>
		public Asset(AssetManager manager, ResourceManager resourceManager)
			: this(manager) {
			if (resourceManager == null)
				throw new ArgumentNullException("resourceManager");
			this.ResourceManager = resourceManager;
		}

		/// <summary>
		/// Initialise the asset.
		/// </summary>
		/// <param name="manager"></param>
		/// <param name="name"></param>
		/// <param name="description"></param>
		public Asset(AssetManager manager, string name, string description = null)
			: this(manager) {
			if (name == null)
				throw new ArgumentNullException("name");
			Name = name;
			Description = description;
		}

		/// <summary>
		/// Initialise the asset.
		/// </summary>
		/// <param name="manager"></param>
		/// <param name="resourceManager"></param>
		/// <param name="name"></param>
		/// <param name="description"></param>
		public Asset(AssetManager manager, ResourceManager resourceManager, string name, string description = null) {
			if (resourceManager == null) {
				if (name == null)
					throw new ArgumentNullException("name");
			}

			ResourceManager = resourceManager;
			Name = name;
			Description = description;
		}

		/// <summary>
		/// Initialise the asset.
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="name"></param>
		/// <param name="description"></param>
		public Asset(FolderAsset parent, string name, string description = null)
			: this(parent.Manager, name, description) {
			Parent = parent;
		}

		/// <summary>
		/// Initialise the asset.
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="loader"></param>
		public Asset(FolderAsset parent, AssetLoader loader)
			: this(loader) {
			Parent = parent;
		}

		/// <summary>
		/// Initialise the asset.
		/// </summary>
		/// <param name="loader"></param>
		public Asset(AssetLoader loader)
			: this(loader.AssetManager, loader.Name) {
			LoadContext = loader.Context;
			LoadName = loader.Name;
			LoadErrors = loader.Errors;
			LoadFileManager = loader.FileManager;
		}

		/// <summary>
		/// Add an asset as a child of this asset.
		/// </summary>
		/// <param name="child"></param>
		protected void AddChild(Asset child) {
			if (child == null)
				throw new ArgumentNullException("child");
			child.Parent = this;
		}

		/// <summary>Return a control for browsing the asset, or return <c>null</c> if there is none.</summary>
		/// <param name="progressUpdateCallback"></param>
		/// <returns></returns>
		public virtual Control Browse(Action<double> progressUpdateCallback = null) {
			return null;
		}

		/// <summary>Place the result of <see cref="Browse"/> in a bar panel.</summary>
		/// <param name="progressUpdateCallback"></param>
		/// <returns></returns>
		public virtual Control BrowseContents(Action<double> progressUpdateCallback = null) {
			return CreateBarPanel(Browse());
		}

		/// <summary>Place the result of <see cref="Browse"/> in a bar panel.</summary>
		/// <param name="progressUpdateCallback"></param>
		/// <returns></returns>
		protected Control BrowseUnderBarPanel(Action<double> progressUpdateCallback = null) {
			return CreateBarPanel(Browse(progressUpdateCallback));
		}

		/// <summary>
		/// Create a bar panel, which has a panel for interacting with the asset at the top and some contents.
		/// </summary>
		/// <param name="contents"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Fill the elements of a context menu.
		/// </summary>
		/// <param name="strip"></param>
		public virtual void FillContextMenu(ContextMenuStrip strip) {
		}

		/// <summary>Search for an ancestor of this asset (or this asset itself) that has the given type.</summary>
		/// <typeparam name="TAsset"></typeparam>
		/// <returns></returns>
		public TAsset FindAncestor<TAsset>() where TAsset : Asset {
			for(Asset parent = this; ; parent = parent.Parent) {
				TAsset parentAsset = parent as TAsset;
				if (!ReferenceEquals(parentAsset, null) || ReferenceEquals(parent, null))
					return parentAsset;
			}
		}

		/// <summary>
		/// Get a context menu for this asset.
		/// </summary>
		/// <returns></returns>
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

		/// <summary>
		/// Find a property for this type.
		/// </summary>
		/// <typeparam name="T">The type to search.</typeparam>
		/// <param name="name"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Create <see cref="FolderAsset"/>s as necessary to store this asset within a path.
		/// </summary>
		/// <param name="path"></param>
		protected void MoveIntoPath(string path) {
			MoveIntoPath(path.Split('/', '\\'));
		}

		/// <summary>
		/// Create <see cref="FolderAsset"/>s as necessary to store this asset within a path and set the name of this asset to the last name in the path.
		/// </summary>
		/// <param name="path"></param>
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

		/// <summary>
		/// Sort the children of this asset recursively.
		/// </summary>
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

			var view = new DoubleBufferedDataGridView() {
				AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells,
				AutoGenerateColumns = false,
				DataSource = list,
				Dock = DockStyle.Fill,
				ReadOnly = true,
			};

			view.Columns.Add(new DataGridViewTextBoxColumn() {
				DataPropertyName = "OffsetHex",
				HeaderText = "Offset",
				SortMode = DataGridViewColumnSortMode.Automatic,
				AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
			});

			view.Columns.Add(new DataGridViewTextBoxColumn() {
				DataPropertyName = "Message",
				HeaderText = "Message",
				SortMode = DataGridViewColumnSortMode.Automatic,
				AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,

				DefaultCellStyle = new DataGridViewCellStyle() {
					WrapMode = DataGridViewTriState.True,
				},
			});

			window.Controls.Add(view);
			window.ClientSize = new System.Drawing.Size(view.PreferredSize.Width + 4, view.PreferredSize.Height + 4);
			window.Show();
		}
	}
}
