using Moki.Compiler;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Moki.Syntax.Declarations {
	/// <summary>
	/// A <see cref="Declaration"/> that declares new variables.
	/// </summary>
	[Serializable]
	public class VariableDeclaration : Declaration {
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		TypeDeclaration variableType;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		ObservableCollection<VariableDeclarationVariable> variables;

		static readonly Type ThisType = typeof(VariableDeclaration);
		public static readonly PropertyInfo VariableTypeProperty = FindProperty(ThisType, "VariableType");
		public static readonly PropertyInfo VariablesProperty = FindProperty(ThisType, "Variables");

		/// <summary>Get the type of all of the variables, or <c>null</c> to get the type from the initializers.</summary>
		public TypeDeclaration VariableType {
			get { return GetProperty(VariableTypeProperty, ref variableType); }
			set { SetProperty(VariableTypeProperty, ref variableType, ref value); }
		}

		public ObservableCollection<VariableDeclarationVariable> Variables {
			get { return GetProperty(VariablesProperty, ref variables); }
			set { SetProperty(VariablesProperty, ref variables, ref value); }
		}

		public VariableDeclaration(Marker marker, TypeDeclaration variableType)
			: base(marker) {
			VariableType = variableType;
			Variables = new ObservableCollection<VariableDeclarationVariable>();
		}

		public VariableDeclaration(Marker marker, TypeDeclaration variableType, params VariableDeclarationVariable[] variables)
			: this(marker, variableType, (IEnumerable<VariableDeclarationVariable>)variables) {
		}

		public VariableDeclaration(Marker marker, TypeDeclaration variableType, IEnumerable<VariableDeclarationVariable> variables)
			: this(marker, variableType) {
			foreach (VariableDeclarationVariable variable in variables)
				Variables.Add(variable);
		}

		public override string ToString() {
			return (VariableType != null ? VariableType.ToString() : "var") + " " + string.Join(", ", variables) + ";";
		}
	}

	/// <summary>
	/// A variable defined in a <see cref="VariableDeclaration"/>.
	/// </summary>
	[Serializable]
	public class VariableDeclarationVariable : Node {
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		Expression initialiser;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		string name;

		static readonly Type ThisType = typeof(VariableDeclarationVariable);
		public static readonly PropertyInfo InitialiserProperty = FindProperty(ThisType, "Initialiser");
		public static readonly PropertyInfo NameProperty = FindProperty(ThisType, "Name");

		/// <summary>Get or set the initial value of the variable.</summary>
		public Expression Initialiser {
			get { return GetProperty(InitialiserProperty, ref initialiser); }
			set { SetProperty(InitialiserProperty, ref initialiser, ref value); }
		}

		/// <summary>Get or set the name of the variable.</summary>
		/// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
		public string Name {
			get { return GetProperty(NameProperty, ref name); }

			set {
				if (value == null)
					throw new ArgumentNullException("value");
				SetProperty(NameProperty, ref name, ref value);
			}
		}

		public VariableDeclarationVariable(Marker marker, string name, Expression initialiser)
			: base(marker) {
			Name = name;
			Initialiser = initialiser;
		}

		public override string ToString() {
			return name + (initialiser != null ? " = " + initialiser : "");
		}
	}
}
