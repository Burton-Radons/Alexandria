using Moki.Compiler;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Moki.Syntax.TypeDeclarations {
	public class LiteralType : TypeDeclaration {
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		Type type;

		static readonly Type ThisType = typeof(LiteralType);
		public static readonly PropertyInfo TypeProperty = FindProperty(ThisType, "Type");

		Type Type {
			get { return GetProperty(TypeProperty, ref type); }

			set {
				if (value == null)
					throw new ArgumentNullException("value");
				SetProperty(TypeProperty, ref type, ref value);
			}
		}

		public LiteralType(Marker marker, Type value)
			: base(marker) {
			Type = value;
		}

		public LiteralType(Token literalTypeToken)
			: this(literalTypeToken.Marker, (Type)literalTypeToken.Value) {
		}

		public override string ToString() {
			return type.FullName;
		}
	}
}
