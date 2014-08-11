using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Glare.Graphics.Rendering {
	public class ShaderBuilder {
		readonly string path;
		readonly Dictionary<string, string> sections = new Dictionary<string, string>();
		readonly List<string> sources = new List<string>();
		readonly string source;

		public string Path { get { return path; } }

		public Dictionary<string, string> Sections { get { return sections; } }

		public string Source { get { return source; } }

		public string VersionCode { get; set; }

		ShaderBuilder(string path) {
			if (path == null)
				throw new ArgumentNullException("path");
			this.path = path;
		}

		public ShaderBuilder(string path, ShaderBuilderManager manager = null)
			: this(path) {
			source = File.ReadAllText(path);
			Construct(manager ?? new FileManager());
		}

		public ShaderBuilder(string path, Stream source, ShaderBuilderManager manager = null, bool closeSource = false)
			: this(path) {
			if (source == null)
				throw new ArgumentNullException("source");
			this.source = new StreamReader(source).ReadToEnd();
			if (closeSource)
				source.Close();
			Construct(manager);
		}

		public ShaderBuilder(string path, string source, ShaderBuilderManager manager = null)
			: this(path) {
			if (source == null)
				throw new ArgumentNullException("source");
			this.source = source;
			Construct(manager);
		}

		public ShaderBuilder(string path, Assembly assembly, ShaderBuilderManager manager = null)
			: this(path) {
			if (assembly == null)
				throw new ArgumentNullException("assembly");
			using (var stream = assembly.GetManifestResourceStream(path)) {
				if (stream == null) {
					var names = assembly.GetManifestResourceNames();
					throw new ArgumentException(string.Format("Path '{0}' does not appear as a manifest resource for assembly {1}, or is not accessible; the valid names are: {2}.", path, assembly.FullName, string.Join(", ", names)));
				}
				this.source = new StreamReader(stream).ReadToEnd();
			}
			Construct(manager);
		}

		void Construct(ShaderBuilderManager manager) {
			Split(path, source, manager, "Common", 1);
		}

		public void AddVersionCode(StringBuilder builder) {
			if (VersionCode != null)
				builder.AppendFormat("#version {0}\n", VersionCode);
		}

		public void AddSections(StringBuilder builder, string sectionName) {
			if (sectionName.StartsWith("*"))
				builder.Append(sectionName, 1, sectionName.Length - 1);
			else
				builder.Append(sections[sectionName]);
		}

		public void AddSections(StringBuilder builder, string sectionName1, string sectionName2) { AddSections(builder, sectionName1); AddSections(builder, sectionName2); }
		public void AddSections(StringBuilder builder, string sectionName1, string sectionName2, string sectionName3) { AddSections(builder, sectionName1); AddSections(builder, sectionName2); AddSections(builder, sectionName3); }

		public void AddSections(StringBuilder builder, params string[] sectionNames) { foreach (string name in sectionNames) AddSections(builder, name); }

		public void AddSections(StringBuilder builder, IEnumerable<string> sectionNames) { foreach (string name in sectionNames) AddSections(builder, name); }

		public void AddSections(StringBuilder builder, params IEnumerable<string>[] sectionNames) { foreach (IEnumerable<string> list in sectionNames) foreach (string name in list) AddSections(builder, name); }

		public void AddSections(StringBuilder builder, IEnumerable<IEnumerable<string>> sectionNames) {

			foreach (IEnumerable<string> list in sectionNames)
				foreach (string name in list)
					AddSections(builder, name);
		}

		static int CountNewlines(string text) {
			int total = 0;
			for (int index = 0, count = text.Length; index < count; index++) {
				char ch = text[index];

				if (ch == '\r') {
					bool more = index < count - 1;

					if (more && text[index + 1] == '\n')
						index++;
					total++;
				} else if (ch == '\n')
					total++;
			}
			return total;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public static ShaderBuilder CreateFromAssemblyResource(string path, ShaderBuilderManager manager = null) {
			return new ShaderBuilder(path, Assembly.GetCallingAssembly(), manager);
		}

		public FragmentShader FragmentShader(params string[] sectionNames) { return new FragmentShader(JoinSections(sectionNames)); }
		public FragmentShader FragmentShader(params IEnumerable<string>[] sectionNames) { return new FragmentShader(JoinSections(sectionNames)); }
		public FragmentShader FragmentShader(IEnumerable<IEnumerable<string>> sectionNames) { return new FragmentShader(JoinSections(sectionNames)); }

		public string JoinSections(IEnumerable<IEnumerable<string>> sectionNames) { var builder = new StringBuilder(); AddVersionCode(builder); AddSections(builder, sectionNames); return builder.ToString(); }

		public string JoinSections(params IEnumerable<string>[] sectionNames) { var builder = new StringBuilder(); AddVersionCode(builder); AddSections(builder, sectionNames); return builder.ToString(); }

		public string JoinSections(params string[] sectionNames) { var builder = new StringBuilder(); AddVersionCode(builder); AddSections(builder, sectionNames); return builder.ToString(); }

		string ErrorString(string path, int row, string text, params object[] args) { return path + "(" + row + "): " + string.Format(text, args); }

		string LineDirective(int sourceIndex, int line) { return "\n#line " + line + " " + sourceIndex + "\n"; }

		void AddToSection(string sectionName, int sourceIndex, int line, string content) {
			content = LineDirective(sourceIndex, line) + content;
			if (sections.ContainsKey(sectionName))
				sections[sectionName] += content;
			else
				sections[sectionName] = content;
		}

		void Split(string path, string source, ShaderBuilderManager manager, string lastSection, int lastSectionLine) {
			sources.Add(path);

			int sourceIndex = sources.Count - 1;
			string[] split = source.Split(new string[] { "::" }, StringSplitOptions.None);
			int line = CountNewlines(split[0]) + 1;

			AddToSection(lastSection, sourceIndex, 1, split[0]);

			for (int index = 1; index < split.Length; index += 2) {
				string fullName = split[index];
				string name = fullName.Trim();

				if (name.StartsWith("#")) {
					name = name.Substring(1).Trim();

					string command;
					int commandSplit;
					for (commandSplit = 0; commandSplit < name.Length; commandSplit++)
						if (char.IsWhiteSpace(name[commandSplit]))
							break;
					command = name.Substring(0, commandSplit);
					name = name.Substring(commandSplit).Trim();

					switch (command) {
						case "include":
							bool local = false;
							string end;

							if (name.StartsWith("\"")) {
								local = true;
								end = "\"";
							} else if (name.StartsWith("<"))
								end = ">";
							else
								throw new InvalidOperationException(ErrorString(path, line, "Expected '\"' or '<' and '>' to surround the filename for the include."));

							if (!name.EndsWith(end))
								throw new InvalidOperationException(ErrorString(path, line, "Expected '{0}' after the file name.", end));
							name = name.Substring(1, name.Length - 2);

							if (manager == null)
								throw new ArgumentNullException("manager", ErrorString(path, line, "Include cannot be used while manager is null."));

							string includePath = manager.IncludePath(sources[0], path, name, local);
							string includeSource = manager.Include(sources[0], path, name, local);
							Split(includePath, includeSource, manager, lastSection, lastSectionLine);
							break;

						case "version":
							VersionCode = name;
							break;

						default:
							throw new InvalidOperationException(ErrorString(path, line, "There is no command named '" + command + "'."));
					}
				} else
					lastSection = name;

				if (index < split.Length - 1) {
					string content = split[index + 1];
					line += CountNewlines(fullName);
					AddToSection(lastSection, sourceIndex, line, content);
					line += CountNewlines(content);

					lastSection = name;
					lastSectionLine = line;
				}
			}
		}

		public VertexShader VertexShader(params string[] sectionNames) { return new VertexShader(JoinSections(sectionNames)); }
		public VertexShader VertexShader(params IEnumerable<string>[] sectionNames) { return new VertexShader(JoinSections(sectionNames)); }
		public VertexShader VertexShader(IEnumerable<IEnumerable<string>> sectionNames) { return new VertexShader(JoinSections(sectionNames)); }

		class AssemblyManager : ShaderBuilderManager {
			readonly Assembly assembly;

			public AssemblyManager(Assembly assembly) {
				this.assembly = assembly;
			}

			public string Include(string originalPath, string filePath, string includePath, bool local) {
				string path = IncludePath(originalPath, filePath, includePath, local);

				using (Stream stream = assembly.GetManifestResourceStream(path))
					return new StreamReader(stream).ReadToEnd();
			}


			public string IncludePath(string originalPath, string filePath, string includePath, bool local) {
				if (!local)
					return includePath;
				int end = filePath.LastIndexOf('.');
				if (end < 0)
					throw new Exception();
				end = filePath.LastIndexOf('.', end - 1);
				if (end < 0)
					throw new Exception();
				string path = filePath.Substring(0, end + 1) + includePath;

				return path;
			}
		}

		class FileManager : ShaderBuilderManager {
			public string Include(string originalPath, string filePath, string include, bool local) {
				throw new NotImplementedException();
			}

			string ShaderBuilderManager.Include(string originalPath, string filePath, string include, bool local) {
				throw new NotImplementedException();
			}

			string ShaderBuilderManager.IncludePath(string originalPath, string filePath, string include, bool local) {
				throw new NotImplementedException();
			}
		}

	}

	public interface ShaderBuilderManager {
		string Include(string originalPath, string filePath, string include, bool local);
		string IncludePath(string originalPath, string filePath, string include, bool local);
	}
}
