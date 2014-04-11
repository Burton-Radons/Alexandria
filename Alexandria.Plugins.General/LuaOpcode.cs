using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexandria.General {
	public enum LuaOpcode : byte {
		Move,
		LoadConstant,
		LoadBoolean,
		LoadNil,
		GetUpValue,

		GetGlobal,
		GetTable,

		SetGlobal,
		SetUpValue,
		SetTable,

		NewTable,

		/// <summary>(R(A) R(B) RK(C))   R(A+1) := R(B); R(A) := R(B).RK(C)</summary>
		Self,

		Add,
		Subtract,
		Multiply,
		Divide,
		Power,
		Negate,
		Not,

		Concatenate,

		Jump,

		Equal,
		LessThan,
		LessThanOrEqualTo,

		Test,

		/// <summary>(R(A) R(B) R(C)) R(A to A+C-2) := R(A)(R(A+1 to A+B-1))</summary>
		Call,
		TailCall,
		Return,

		ForLoop,

		/// <summary>(R(A) R(C)) R(A+2 to A+2+C) := R(A)(R(A+1), R(A+2)); if R(A+2) != <c>null</c> then PC++</summary>
		TForLoop,

		TForPreparation,

		SetList,
		SetList0,

		Close,

		/// <summary>(A Bx) R(A) := closure(KPROTO[Bx], R(A to A+n))</summary>
		Closure,
	}
}