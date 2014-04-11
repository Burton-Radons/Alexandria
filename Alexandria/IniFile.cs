using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Glare.Internal;

namespace Alexandria {
	/// <summary>
	/// .ini configuration file loading.
	/// </summary>
	public class IniFile {
		/// <summary>
		/// Create a new in-memory initialization file.
		/// </summary>
		public IniFile()
			: this(IniFileBehavior.Default) {
		}

		/// <summary>
		/// Create a new in-memory initialization file with the defined behavior.
		/// </summary>
		/// <param name="behavior">The behavior to follow.</param>
		public IniFile(IniFileBehavior behavior) {
			Sections = new List<IniFileSection>();
			SectionsByName = new Dictionary<string, IniFileSection>();
			Behavior = behavior;
		}

		/// <summary>
		/// Create an initialization file, loading from the given path. This path must exist.
		/// </summary>
		/// <param name="path"></param>
		public IniFile(string path) : this(path, IniFileBehavior.Default) { }

		public IniFile(string path, IniFileBehavior behavior)
			: this(behavior) {
			var lines = File.ReadAllLines(path, Encoding.ASCII);
			IniFileSection section = null;

			for (var index = 0; index < lines.Length; index++) {
				var line = lines[index].Trim();

				if (line.Length == 0 || line[0] == Behavior.CommentDelimiter)
					continue;
				if (line[0] == '[') {
					int end = line.IndexOf(']');

					if (end < 0)
						throw new Exception("Expected ']' to terminate a section name.");
					section = FindOrCreateSection(line.Substring(1, end - 1));
				} else {
					var comment = -1;
					var split = line.IndexOf(behavior.SettingValueDelimiter);
					string name, value;

					if (Behavior.CommentInValue) {
						comment = line.IndexOf(behavior.CommentDelimiter);
						if (behavior.CommentDelimiter2 != null)
							comment = Extensions.GetSmallerIndex(comment, line.IndexOf(behavior.CommentDelimiter2));
					}

					if (split < 0 && comment >= 0) {
						for (int chIndex = 0; chIndex < comment; chIndex++)
							if (!char.IsWhiteSpace(line[chIndex]))
								throw new Exception("This line has non-whitespace before a comment, but is not a setting.");
						continue;
					}

					if (split < 0 || (comment >= 0 && comment < split))
						throw new Exception("This line cannot be parsed as a setting, but it's the only thing it could be.");

					name = line.Substring(0, split).TrimEnd();
					if (comment >= 0)
						value = line.Substring(split + 1, comment - split - 1).Trim();
					else
						value = line.Substring(split + 1).Trim();

					new IniFileSetting(section, name, value);
				}
			}
		}

		/// <summary>
		/// List of sections in the file.
		/// </summary>
		public List<IniFileSection> Sections { get; protected set; }

		/// <summary>
		/// Dictionary of sections indexed by their name.
		/// </summary>
		public Dictionary<string, IniFileSection> SectionsByName { get; protected set; }

		/// <summary>
		/// Get the behavior to use for this file.
		/// </summary>
		public IniFileBehavior Behavior { get; protected set; }

		/// <summary>
		/// Return the section with the given name, creating it if necessary.
		/// </summary>
		/// <param name="name">The name of the section.</param>
		/// <returns></returns>
		public IniFileSection FindOrCreateSection(string name) {
			IniFileSection section;

			if (!SectionsByName.TryGetValue(name, out section))
				section = new IniFileSection(this, name);
			return section;
		}

		/// <summary>
		/// Get a section with the specified name.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public IniFileSection this[string sectionName] {
			get { return SectionsByName[sectionName]; }
		}

		public string this[string sectionName, string settingName] {
			get { return this[sectionName][settingName]; }
		}

		public string this[string sectionName, string settingName, string defaultValue] {
			get {
				IniFileSection section;

				if (!SectionsByName.TryGetValue(sectionName, out section))
					return defaultValue;
				return section[settingName, defaultValue];
			}
		}
	}

	/// <summary>
	/// Behavior for a <see cref="IniFile"/>.
	/// </summary>
	public struct IniFileBehavior {
		public static readonly IniFileBehavior Default = new IniFileBehavior() {
			Duplicates = IniFileDuplicateBehavior.AllowAll,
			SettingValueDelimiter = '=',
			CommentDelimiter = ';',
			CommentDelimiter2 = "//",
			CommentInValue = true,
			LineContinuationEscape = false,
		};

		/// <summary>
		/// Get behavior to follow when a duplicate setting with the same name is encountered in a file.
		/// The <see cref="Default"/> value is <see cref="IniFileDuplicateBehavior.AllowAll"/>.
		/// </summary>
		public IniFileDuplicateBehavior Duplicates { get; set; }

		/// <summary>
		/// Get or set the delimiter between a setting name and its value. The <see cref="Default"/> value is '='; some implementations use ':'.
		/// </summary>
		public char SettingValueDelimiter { get; set; }

		/// <summary>
		/// Get or set the delimiter before a comment. The <see cref="Default"/> value is ';'; some implementations use '#'. Setting this to '\0' disables comments.
		/// </summary>
		public char CommentDelimiter { get; set; }

		/// <summary>
		/// Sometimes ini files contain more stupid delimiters like "//". If this is <c>null</c>, it is not used.
		/// </summary>
		public string CommentDelimiter2 { get; set; }

		/// <summary>
		/// Get or set whether a comment delimiter in a setting value is considered to be a comment or is part of the value. The <see cref="Default"/> value is true.
		/// </summary>
		public bool CommentInValue { get; set; }

		/// <summary>
		/// Get or set whether to support line continuation escapes, which is a backslash followed by newline and causes the line break to be ignored.
		/// The <see cref="Default"/> value is false.
		/// </summary>
		public bool LineContinuationEscape { get; set; }
	}

	/// <summary>
	/// Behaviors that the <see cref="IniFile"/> can use when there are duplicate settings.
	/// </summary>
	public enum IniFileDuplicateBehavior {
		/// <summary>
		/// Allow and retain the duplicates. Attempting to retrieve it as an individual (like with <c>section[setting]</c>) will raise an exception.
		/// </summary>
		AllowAll,

		/// <summary>
		/// Allow and retain the duplicates. If a setting is retrieved as an individual (like with <c>section[setting]</c>), then return the last defined setting.
		/// </summary>
		AllowChooseLast,

		/// <summary>
		/// Allow and retain the duplicates. If a setting is retrieved as an individual (like with <c>section[setting]</c>), then return the first defined setting.
		/// </summary>
		AllowChooseFirst,

		/// <summary>
		/// Ignore duplicates, retaining only a single value. The last setting with this name in the file is used.
		/// Note that the duplicate is not retained at all, so if there are multiple settings with this name in the file
		/// and the setting is removed, then there will be no setting, and it won't return the next last setting value.
		/// If the latter behavior is desired, use <see cref="AllowChooseLast"/>.
		/// </summary>
		IgnoreChooseLast,

		/// <summary>
		/// Ignore duplicates, retaining only a single value. The first setting with this name in the file is used.
		/// Note that the duplicate is not retained at all, so if there are multiple settings with this name in the file
		/// and the setting is removed, then there will be no setting, and it won't return the next setting value.
		/// If the latter behavior is desired, use <see cref="AllowChooseFirst"/>.
		/// </summary>
		IgnoreChooseFirst,

		/// <summary>
		/// Raise an exception if a second setting with the same name is in the file.
		/// </summary>
		Abort,
	}

	/// <summary>
	/// A section within an <see cref="IniFile"/>.
	/// </summary>
	public class IniFileSection {
		public IniFileSection(IniFile file, string name) {
			if (file == null)
				throw new ArgumentNullException("file");
			if (name == null)
				throw new ArgumentNullException("name");
			File = file;
			Name = name;

			if (File.SectionsByName.ContainsKey(name))
				throw new InvalidOperationException("Ini file section already exists.");

			Settings = new List<IniFileSetting>();
			SettingsByName = new Dictionary<string, IniFileSetting>();
			File.Sections.Add(this);
			File.SectionsByName[Name] = this;
		}

		/// <summary>
		/// File this section is in.
		/// </summary>
		public IniFile File { get; protected set; }

		/// <summary>
		/// Name of this section.
		/// </summary>
		public string Name { get; protected set; }

		/// <summary>
		/// List of settings in this section.
		/// </summary>
		public List<IniFileSetting> Settings { get; protected set; }

		/// <summary>
		/// The first setting with a given name in the section.
		/// If the <see cref="File"/>'s <see cref="IniFile.DuplicateBehavior"/> is <see cref="IniFileDuplicateBehavior.AllowChooseLast"/>, then
		/// this will be in reverse order from how it was encountered in the file.
		/// </summary>
		public Dictionary<string, IniFileSetting> SettingsByName { get; protected set; }

		string GetBase(IniFileSetting setting) {
			switch (File.Behavior.Duplicates) {
				case IniFileDuplicateBehavior.Abort:
				case IniFileDuplicateBehavior.AllowChooseFirst:
				case IniFileDuplicateBehavior.AllowChooseLast:
				case IniFileDuplicateBehavior.IgnoreChooseFirst:
				case IniFileDuplicateBehavior.IgnoreChooseLast:
					return setting.Value;

				case IniFileDuplicateBehavior.AllowAll:
					if (setting.NextSettingWithSameName != null)
						throw new Exception("There are multiple settings in this section with the same name. As such, this setting cannot be accessed individually.");
					return setting.Value;

				default:
					throw new Exception();
			}
		}

		/// <summary>
		/// Get or set a setting.
		/// Assignment removes all settings but the new value, if there are more than one.
		/// </summary>
		/// <param name="settingName"></param>
		/// <returns></returns>
		public string this[string settingName] {
			get { return GetBase(SettingsByName[settingName]); }
		}

		/// <summary>
		/// Get a value of a setting. If the setting doesn't exist, this returns <paramref name="defaultValue"/>.
		/// </summary>
		/// <param name="settingName"></param>
		/// <param name="defaultValue"></param>
		/// <returns></returns>
		public string this[string settingName, string defaultValue] {
			get {
				IniFileSetting setting;

				if (!SettingsByName.TryGetValue(settingName, out setting))
					return defaultValue;
				return GetBase(setting);
			}
		}

		/// <summary>
		/// Return all settings with the specified name. If the setting does not exist, an empty list is returned.
		/// </summary>
		/// <param name="settingName"></param>
		/// <returns></returns>
		public List<string> GetMultiple(string settingName) {
			List<string> list = new List<string>(1);
			IniFileSetting setting;

			if (!SettingsByName.TryGetValue(settingName, out setting))
				return list;

			for (; setting != null; setting = setting.NextSettingWithSameName)
				list.Add(setting.Value);

			return list;
		}
	}

	/// <summary>
	/// A setting within an <see cref="IniFileSection"/>.
	/// </summary>
	public class IniFileSetting {
		public IniFileSetting(IniFileSection section, string name, string value) {
			if (section == null)
				throw new ArgumentNullException("section");
			if (name == null)
				throw new ArgumentNullException("name");
			if (value == null)
				throw new ArgumentNullException("value");
			Section = section;
			Name = name;
			Value = value;

			IniFileSetting first;

			// Insert this into section.SettingsByName using the duplicate behavior setting.
			if (!section.SettingsByName.TryGetValue(name, out first))
				section.SettingsByName[name] = this;
			else {
				switch (File.Behavior.Duplicates) {
					case IniFileDuplicateBehavior.AllowAll:
					case IniFileDuplicateBehavior.AllowChooseFirst:
						for (var last = first; ; last = last.NextSettingWithSameName)
							if (last.NextSettingWithSameName == null) {
								last.NextSettingWithSameName = this;
								break;
							}
						break;

					case IniFileDuplicateBehavior.Abort:
						throw new InvalidOperationException("This IniFile may not have duplicate keys within a section.");

					case IniFileDuplicateBehavior.AllowChooseLast:
						NextSettingWithSameName = first;
						section.SettingsByName[name] = this;
						break;

					case IniFileDuplicateBehavior.IgnoreChooseFirst:
						return;

					case IniFileDuplicateBehavior.IgnoreChooseLast:
						first.Remove();
						section.SettingsByName[name] = this;
						break;

					default:
						throw new Exception();
				}
			}

			Section.Settings.Add(this);
		}

		/// <summary>
		/// Get the file this setting is in.
		/// </summary>
		public IniFile File { get { return Section.File; } }

		/// <summary>
		/// Get the section this setting is in.
		/// </summary>
		public IniFileSection Section { get; protected set; }

		/// <summary>
		/// A pointer to the next setting in the section with the same name as this one, or null if there is none.
		/// </summary>
		public IniFileSetting NextSettingWithSameName { get; protected set; }

		/// <summary>
		/// Name of the setting.
		/// </summary>
		public string Name { get; protected set; }

		/// <summary>
		/// Value of the setting.
		/// </summary>
		public string Value { get; protected set; }

		/// <summary>
		/// Remove this setting from the section.
		/// </summary>
		public void Remove() {
			if (!Section.Settings.Remove(this))
				return;

			IniFileSetting first = Section.SettingsByName[Name];
			var next = NextSettingWithSameName;

			NextSettingWithSameName = null;

			if (first == this) {
				if (next != null)
					Section.SettingsByName[Name] = next;
				else
					Section.SettingsByName.Remove(Name);
			} else
				for (; ; first = first.NextSettingWithSameName)
					if (first.NextSettingWithSameName == this) {
						first.NextSettingWithSameName = next;
						break;
					}
		}

		public override string ToString() {
			return string.Format("[{0}].{1}={2}", Section.Name, Name, Value);
		}
	}
}
