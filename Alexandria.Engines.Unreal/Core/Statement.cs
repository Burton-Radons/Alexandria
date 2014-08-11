using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Engines.Unreal.Core {

	/// <summary>
	/// Root class of the statements.
	/// </summary>
	public abstract class Statement : RootObject {
		/// <summary>
		/// Offset from the start of the code to this statement.
		/// </summary>
		public int UnpackedCodeOffset { get; protected set; }

		/// <summary></summary>
		protected abstract int PartialUnpackedCodeSize { get; }

		int PrepopCounter { get; set; }

		/// <summary>
		/// Size in bytes of this statement unpacked. This is not necessarily the size when encoded.
		/// </summary>
		public int UnpackedCodeSize { get { return PartialUnpackedCodeSize + PrepopCounter; } }

		/// <summary></summary>
		public static Statement Load(Package package, System.IO.BinaryReader reader, ref int unpackedCodeOffset, long end) {
			var opcode = (Opcode)reader.ReadByte();
			int prepopCounter = 0;

			while (opcode == Opcode.Pop) {
				prepopCounter++;
				unpackedCodeOffset++;
				opcode = (Opcode)reader.ReadByte();
			}

			var statement = CreateStatement(opcode);

			statement.PrepopCounter = prepopCounter;
			statement.UnpackedCodeOffset = unpackedCodeOffset;
			statement.Package = package;
			statement.Load(reader, end);
			unpackedCodeOffset += statement.PartialUnpackedCodeSize;
			return statement;
		}

		/// <summary></summary>
		public static Statement CreateStatement(Opcode opcode) {
			switch (opcode) {
				case Opcode.Add: return new Add();
				case Opcode.Add_IntInt: return new Add_IntInt();
				case Opcode.Assign: return new Assign();
				case Opcode.Branch: return new Branch();
				case Opcode.BranchIfFalse: return new BranchIfFalse();
				case Opcode.ConCat_StrStr: return new ConCat_StrStr();
				case Opcode.DeclareParameter: return new DeclareParameter();
				case Opcode.Equals_StringString: return new Equals_StringString();
				case Opcode.Equals_IntInt: return new Equals_IntInt();
				case Opcode.Field: return new Field();
				case Opcode.FRand: return new FRand();
				case Opcode.InStr: return new InStr();
				case Opcode.Index: return new Index();
				case Opcode.IntToString: return new IntToString();
				case Opcode.Left: return new Left();
				case Opcode.Len: return new Len();
				case Opcode.LessThan: return new LessThan();
				case Opcode.LiteralInt: return new LiteralInt();
				case Opcode.LiteralIntByte: return new LiteralIntByte();
				case Opcode.Local: return new Local();
				case Opcode.Log: return new Log();
				case Opcode.LogicalAnd: return new LogicalAnd();
				case Opcode.LogicalOr: return new LogicalOr();
				case Opcode.Mid: return new Mid();
				case Opcode.Multiply: return new Multiply();
				case Opcode.NotEqual_IntInt: return new NotEqual_IntInt();
				case Opcode.NotEqual_StringString: return new NotEqual_StringString();
				case Opcode.Null: return new Null();
				case Opcode.One: return new One();
				case Opcode.PostIncrement: return new PostIncrement();
				case Opcode.Return: return new Return();
				//case Opcode.ReturnNothing: return new ReturnNothing();
				case Opcode.Right: return new Right();
				case Opcode.StringLiteralAsciiz: return new LiteralStringAsciiz();
				case Opcode.Subtract: return new Subtract();
				case Opcode.Subtract_IntInt: return new Subtract_IntInt();
				case Opcode.Zero: return new Zero();
				default: throw new Exception(String.Format("Unsupported, unimplemented, or unexisting opcode " + opcode + " ({0:X}h) encountered.", (int)opcode));
			}
		}

		#region Abstract base statement types

		public abstract class Binary : Statement {
			[PackageProperty(0)]
			public Statement LeftValue { get; protected set; }

			[PackageProperty(1)]
			public Statement RightValue { get; protected set; }

			protected override int PartialUnpackedCodeSize { get { return 1 + LeftValue.UnpackedCodeSize + RightValue.UnpackedCodeSize; } }

			public override string ToString() {
				return "(" + GetType().Name + " " + LeftValue.ToString() + " " + RightValue.ToString() + ")";
			}
		}

		public abstract class Literal : Statement {
			public abstract object Value { get; }
			public virtual string ValueAsString { get { return Value != null ? Value.ToString() : "null"; } }

			public override string ToString() {
				return ValueAsString;
			}
		}

		public abstract class LiteralString : Literal {
			public override string ValueAsString { get { return "\"" + Value + "\""; } }
		}

		public abstract class NoArguments : Statement {
			protected override int PartialUnpackedCodeSize { get { return 1; } }

			public override string ToString() {
				return GetType().Name;
			}
		}

		public abstract class Trinary : Statement {
			[PackageProperty(0)]
			public Statement A { get; protected set; }

			[PackageProperty(1)]
			public Statement B { get; protected set; }

			[PackageProperty(2)]
			public Statement C { get; protected set; }

			protected override int PartialUnpackedCodeSize { get { return 1 + A.UnpackedCodeSize + B.UnpackedCodeSize + C.UnpackedCodeSize; } }

			public override string ToString() {
				return "(" + GetType().Name + " " + A.ToString() + " " + B.ToString() + " " + C.ToString() + ")";
			}
		}

		public abstract class Unary : Statement {
			[PackageProperty(0)]
			public Statement Value { get; protected set; }

			protected override int PartialUnpackedCodeSize { get { return 1 + Value.UnpackedCodeSize; } }

			public override string ToString() {
				return GetType().Name + ":" + Value.ToString();
			}
		}

		public abstract class UnaryReference : Statement {
			[PackageProperty(0)]
			public Reference Value { get; protected set; }

			protected override int PartialUnpackedCodeSize { get { return 5; } }

			public override string ToString() {
				return GetType().Name + ":" + (Value == null ? "NULL" : Value.Name);
			}
		}

		#endregion

		/// <summary></summary>
		public class Add : Binary { }

		/// <summary></summary>
		public class Add_IntInt : Binary { }

		/// <summary></summary>
		public class Assign : Binary { }

		/// <summary></summary>
		public class Branch : Statement {
			/// <summary></summary>
			[PackageProperty(0)]
			public ushort TargetOffset { get; protected set; }

			/// <summary></summary>
			protected override int PartialUnpackedCodeSize { get { return 3; } }

			/// <summary></summary>
			public override string ToString() {
				return GetType().Name + " to " + TargetOffset;
			}
		}

		/// <summary></summary>
		public class BranchIfFalse : Branch {
			/// <summary></summary>
			[PackageProperty(0)]
			public Statement Test { get; protected set; }

			/// <summary></summary>
			protected override int PartialUnpackedCodeSize { get { return 3 + Test.UnpackedCodeSize; } }

			public override string ToString() {
				return base.ToString() + " ifnot " + Test.ToString();
			}
		}

		/// <summary></summary>
		public class ConCat_StrStr : Binary { }

		/// <summary></summary>
		public class DeclareParameter : UnaryReference { }

		/// <summary></summary>
		public class Equals_IntInt : Binary { }

		/// <summary></summary>
		public class Equals_StringString : Binary { }

		/// <summary></summary>
		public class Field : UnaryReference { }

		/// <summary></summary>
		public class FRand : NoArguments { }

		/// <summary></summary>
		public class InStr : Binary { }

		/// <summary></summary>
		public class Index : Binary { }

		/// <summary></summary>
		public class IntToString : Unary { }

		/// <summary></summary>
		public class Left : Binary { }

		/// <summary></summary>
		public class Len : Unary { }

		/// <summary></summary>
		public class LessThan : Binary { }

		/// <summary></summary>
		public class LiteralInt : Literal {
			/// <summary></summary>
			[PackageProperty(0)]
			public int IntValue { get; protected set; }

			/// <summary></summary>
			public override object Value { get { return IntValue; } }

			/// <summary></summary>
			protected override int PartialUnpackedCodeSize { get { return 5; } }
		}

		/// <summary></summary>
		public class LiteralIntByte : Literal {
			/// <summary></summary>
			[PackageProperty(0)]
			public byte ByteValue { get; protected set; }

			/// <summary></summary>
			public override object Value { get { return ByteValue; } }
			
			/// <summary></summary>
			protected override int PartialUnpackedCodeSize { get { return 2; } }
		}

		/// <summary></summary>
		public class LiteralStringAsciiz : LiteralString {
			/// <summary></summary>
			[PackageProperty(0, typeof(DataProcessors.NulTerminatedAscii))]
			public string StringValue { get; protected set; }

			/// <summary></summary>
			public override object Value { get { return StringValue; } }
			
			/// <summary></summary>
			protected override int PartialUnpackedCodeSize { get { return 1 + StringValue.Length + 1; } }
		}

		/// <summary></summary>
		public class Local : UnaryReference { }

		/// <summary></summary>
		public class Log : Unary { }

		/// <summary></summary>
		public class LogicalAnd : Statement {
			/// <summary></summary>
			[PackageProperty(0)]
			public Statement A { get; protected set; }

			/// <summary></summary>
			public int ElseCount { get; protected set; }

			/// <summary></summary>
			[PackageProperty(1, typeof(ElseBranchReader))]
			public ushort ElseBranch { get; protected set; }

			/// <summary></summary>
			[PackageProperty(2)]
			public Statement B { get; protected set; }

			/// <summary></summary>
			protected override int PartialUnpackedCodeSize { get { return 1 + A.UnpackedCodeSize + ElseCount + 1 + 2 + B.UnpackedCodeSize; } }

			/// <summary></summary>
			public override string ToString() {
				return "(" + GetType().Name + " " + A.ToString() + " " + B.ToString() + ")";
			}

			class ElseBranchReader : DataProcessor {
				public override object Read(object target, Package package, System.IO.BinaryReader reader, long end) {
					LogicalAnd targetAnd = (LogicalAnd)target;
					byte code;

					targetAnd.ElseCount = 0;
					while ((code = reader.ReadByte()) == 0x16)
						targetAnd.ElseCount++;
					if (code != 0x18)
						throw new Exception();
					return reader.ReadUInt16();
				}
			}

		}

		/// <summary></summary>
		public class LogicalOr : LogicalAnd { }

		/// <summary></summary>
		public class Mid : Trinary { }

		/// <summary></summary>
		public class Multiply : Binary { }

		/// <summary></summary>
		public class NotEqual_IntInt : Binary { }

		/// <summary></summary>
		public class NotEqual_StringString : Binary { }

		/// <summary></summary>
		public class Null : NoArguments { }

		/// <summary></summary>
		public class One : NoArguments { }

		/// <summary></summary>
		public class PostIncrement : Unary { }

		/// <summary></summary>
		public class Return : Unary { }

		//public class ReturnNothing : NoArguments { }

		/// <summary></summary>
		public class Right : Binary { }

		/// <summary></summary>
		public class Subtract : Binary { }

		/// <summary></summary>
		public class Subtract_IntInt : Binary { }

		/// <summary></summary>
		public class Zero : NoArguments { }
	}

	/// <summary></summary>
	public enum Opcode : byte {
		/// <summary></summary>
		Local = 0x00,

		/// <summary></summary>
		Field = 0x01,

		/// <summary></summary>
		Return = 0x04,

		/// <summary></summary>
		Branch = 0x06,

		/// <summary></summary>
		BranchIfFalse = 0x07,

		/// <summary></summary>
		Null = 0x0B,

		/// <summary></summary>
		Assign = 0x0F,

		/// <summary></summary>
		Pop = 0x16,
		//Equals_IntInt = 0x17,

		/// <summary></summary>
		Index = 0x1A,

		/// <summary></summary>
		LiteralInt = 0x1D,

		/// <summary></summary>
		StringLiteralAsciiz = 0x1F,

		/// <summary></summary>
		Zero = 0x25,

		/// <summary></summary>
		One = 0x26,

		/// <summary></summary>
		DeclareParameter = 0x29,

		/// <summary></summary>
		LiteralIntByte = 0x2C,

		/// <summary></summary>
		ReturnNothing = 0x3A,

		/// <summary></summary>
		IntToString = 0x53,

		/// <summary></summary>
		ConCat_StrStr = 0x70,

		/// <summary></summary>
		Equals_StringString = 0x7A,

		/// <summary></summary>
		NotEqual_StringString = 0x7B,

		/// <summary></summary>
		Len = 0x7D,

		/// <summary></summary>
		InStr = 0x7E,

		/// <summary></summary>
		Mid = 0x7F,

		/// <summary></summary>
		Left = 0x80,

		/// <summary></summary>
		LogicalAnd = 0x82,

		/// <summary></summary>
		LogicalOr = 0x84,

		/// <summary></summary>
		Add_IntInt = 0x92,

		/// <summary></summary>
		Subtract_IntInt = 0x93,

		/// <summary></summary>
		LessThan = 0x96,

		/// <summary></summary>
		Equals_IntInt = 0x9A,

		/// <summary></summary>
		NotEqual_IntInt = 0x9B,

		/// <summary></summary>
		PostIncrement = 0xA5,

		/// <summary></summary>
		Multiply = 0xAB,

		/// <summary></summary>
		Add = 0xAE,

		/// <summary></summary>
		Subtract = 0xAF,

		/// <summary></summary>
		FRand = 0xC3,

		/// <summary></summary>
		Log = 0xE7,

		/// <summary></summary>
		Right = 0xEA,
	}
}
