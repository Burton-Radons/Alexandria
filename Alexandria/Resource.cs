using Alexandria.Resources;
using Glare;
using Glare.Graphics;
using Glare.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alexandria {
	public abstract class Resource {
		readonly ArrayBackedList<Resource> children = new ArrayBackedList<Resource>();
		readonly Manager manager;
		Resource parent;
		UnknownList unknowns;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		string name, description;

		public ReadOnlyList<Resource> Children { get { return children; } }

		public virtual string Description {
			get { return GetResourceString(description, "Description"); }
			protected set { description = value; }
		}

		public virtual string DisplayName { get { return Name ?? GetType().Name + " resource"; } }

		public static Encoding EncodingShiftJis { get { return Encoding.GetEncoding("shift-jis"); } }

		/// <summary>Get this <see cref="Resource"/> as something that Glare's <see cref="ResourceManager"/> might want to save/load, or return <c>null</c> (the default).</summary>
		public virtual object GlareObject { get { return null; } }

		public bool HasUnknowns { get { return unknowns != null && unknowns.Count > 0; } }

		/// <summary>Get whether this <see cref="Resource"/> or any children have been locked from modification.</summary>
		public bool IsAnyModified {
			get {
				if (IsModified)
					return true;
				foreach (Resource child in children)
					if (child.IsModified)
						return true;
				return false;
			}
		}

		/// <summary>Get whether the <see cref="Resource"/> is modified.</summary>
		public bool IsModified { get; protected set; }

		/// <summary>Get whether the <see cref="Resource"/> cannot be modified in any way. The default is <c>true</c>.</summary>
		public bool IsReadOnly { get; protected set; }

		public Manager Manager { get { return manager; } }

		public virtual string Name {
			get { return GetResourceString(name, "Name"); }
			protected set { name = value; }
		}

		public Resource Parent {
			get { return parent; }

			protected set {
				if (parent != null)
					parent.children.Remove(this);
				if (value != null)
					value.children.Add(this);
				parent = value;
			}
		}

		public virtual string PathName { get { return Name; } }

		public ResourceManager ResourceManager { get; private set; }

		protected virtual string ResourcePrefix { get { return GetType().Name; } }

		public UnknownList Unknowns { get { return unknowns ?? (unknowns = new UnknownList()); } }

		Resource(Manager manager) {
			if (manager == null)
				throw new ArgumentNullException("manager");
			this.manager = manager;
			IsReadOnly = true;
		}

		public Resource(Manager manager, ResourceManager resourceManager)
			: this(manager) {
			if (resourceManager == null)
				throw new ArgumentNullException("resourceManager");
			this.ResourceManager = resourceManager;
		}

		public Resource(Manager manager, string name, string description = null)
			: this(manager) {
			if (name == null)
				throw new ArgumentNullException("name");
			Name = name;
			Description = description;
		}

		public Resource(Manager manager, ResourceManager resourceManager, string name, string description = null) {
			if (resourceManager == null) {
				if (name == null)
					throw new ArgumentNullException("name");
			}

			ResourceManager = resourceManager;
			Name = name;
			Description = description;
		}

		public Resource(Folder parent, string name, string description = null)
			: this(parent.Manager, name, description) {
			Parent = parent;
		}

		protected void AddChild(Resource child) {
			if (child == null)
				throw new ArgumentNullException("child");
			child.Parent = this;
		}

		public virtual Control Browse() {
			return null;
		}

		public virtual Control BrowseContents() {
			return Browse();
		}

		public virtual void FillContextMenu(ContextMenuStrip strip) {
		}

		public ContextMenuStrip GetContextMenu() {
			ContextMenuStrip strip = new ContextMenuStrip();

			strip.Items.Add(new ToolStripButton("Copy \"" + Name + "\" to clipboard", null, (sender, args) => {
				Clipboard.SetText(Name);
			}));

			if (Name != PathName) {
				strip.Items.Add(new ToolStripButton("Copy \"" + PathName + "\" to clipboard", null, (sender, args) => {
					Clipboard.SetText(PathName);
				}));
			}

			FillContextMenu(strip);
			return strip;
		}

		string GetResourceString(string value, string suffix) {
			if (value != null)
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

			Resource target = Parent;
			for (; index < path.Length - 1; index++) {
				string name = path[index];
				bool found = false;

				foreach (Resource child in target.Children)
					if (child is Folder && child.Name == name) {
						target = child;
						found = true;
					}

				if (!found)
					target = new Folder((Folder)target, name);
			}

			Parent = target;
			Name = path[index];
		}

		protected void SortChildrenRecursively() {
			children.Sort((a, b) => a.Name.CompareNumeric(b.Name));
			foreach (Resource child in children)
				child.SortChildrenRecursively();
		}
	}
}
