using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moki.Compiler {
	public enum TokenCode {
		EndOfSource,

		Identifier,
		TypeLiteral,
		/// <summary>The <see cref="Token"/> contains a literal value in its <see cref="Token.Value"/> property.</summary>
		Literal,

		#region Punctuation

		/// <summary>'->'</summary>
		RightArrow,

		/// <summary>'='</summary>
		Assign,

		/// <summary>'+='</summary>
		AssignAdd,

		/// <summary>'&amp;='</summary>
		AssignBitwiseAnd,

		/// <summary>'^='</summary>
		AssignBitwiseExclusiveOr,

		/// <summary>'|='</summary>
		AssignBitwiseOr,

		/// <summary>'/='</summary>
		AssignDivide,

		/// <summary>'%='</summary>
		AssignModulus,

		/// <summary>'*='</summary>
		AssignMultiply,

		/// <summary>'&gt;&gt;='</summary>
		AssignShiftRight,

		/// <summary>'&lt;&lt;='</summary>
		AssignShiftLeft,

		/// <summary>'-='</summary>
		AssignSubtract,

		/// <summary>'&amp;'</summary>
		BitwiseAnd,

		/// <summary>'^'</summary>
		BitwiseExclusiveOr,

		/// <summary>'~'</summary>
		BitwiseNot,

		/// <summary>'|'</summary>
		BitwiseOr,

		/// <summary>'['</summary>
		BracketOpen,

		/// <summary>']'</summary>
		BracketClose,

		/// <summary>'=&gt;'</summary>
		Closure,

		/// <summary>':'</summary>
		Colon,

		/// <summary>','</summary>
		Comma,

		/// <summary>'=='</summary>
		CompareEqual,

		/// <summary>'>'</summary>
		CompareGreater,

		/// <summary>'>='</summary>
		CompareGreaterOrEqual,

		/// <summary>'&lt;'</summary>
		CompareLess,

		/// <summary>'&lt;='</summary>
		CompareLessOrEqual,

		/// <summary>'+'</summary>
		CompareNotEqual,

		/// <summary>'{'</summary>
		CurlyOpen,

		/// <summary>'}'</summary>
		CurlyClose,

		/// <summary>'--'</summary>
		Decrement,

		/// <summary>'/'</summary>
		Divide,

		/// <summary>'::'</summary>
		DoubleColon,

		/// <summary>'.'</summary>
		Dot,

		/// <summary>'??'</summary>
		IfNull,

		/// <summary>'++'</summary>
		Increment,

		/// <summary>'&amp;&amp;'</summary>
		LogicalAnd,

		/// <summary>'!'</summary>
		LogicalNot,

		/// <summary>'||'</summary>
		LogicalOr,

		/// <summary>'-'</summary>
		Minus,

		/// <summary>'%'</summary>
		Modulus,

		/// <summary>'*'</summary>
		Multiply,

		/// <summary>'('</summary>
		ParenthesisOpen,

		/// <summary>')'</summary>
		ParenthesisClose,

		/// <summary>'+'</summary>
		Plus,

		/// <summary>';'</summary>
		Semicolon,

		/// <summary>'&lt;&lt;'</summary>
		ShiftLeft,

		/// <summary>'&gt;&gt;'</summary>
		ShiftRight,

		/// <summary>'?'</summary>
		Trinary,

		#endregion Punctuation

		#region Keywords

		Abstract,
		As,
		Base,
		Break,
		Case,
		Catch,
		Checked,
		Class,
		Const,
		Continue,
		Default,
		Delegate,
		Do,
		Else,
		Enum,
		Event,
		Explicit,
		Extern,
		Finally,
		Fixed,
		For,
		Foreach,
		Goto,
		If,
		Implicit,
		In,
		Interface,
		Internal,
		Is,
		Lock,
		Namespace,
		New,
		Operator,
		Out,
		Override,
		Params,
		Private,
		Protected,
		Public,
		Readonly,
		Ref,
		Return,
		Sealed,
		Sizeof,
		Stackalloc,
		Static,
		Struct,
		Switch,
		This,
		Throw,
		Try,
		Typeof,
		Unchecked,
		Unsafe,
		Using,
		Virtual,
		Volatile,
		While,

		#endregion Keywords
	}
}
