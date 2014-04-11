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

        protected abstract int PartialUnpackedCodeSize { get; }

		int PrepopCounter { get; set; }

        /// <summary>
        /// Size in bytes of this statement unpacked. This is not necessarily the size when encoded.
        /// </summary>
		public int UnpackedCodeSize { get { return PartialUnpackedCodeSize + PrepopCounter; } }

        public static Statement Load(Package package, System.IO.BinaryReader reader, ref int unpackedCodeOffset, long end) {
            var opcode = (Opcode)reader.ReadByte();
			int prepopCounter = 0;

            while(opcode == Opcode.Pop) {
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

        public static Statement CreateStatement(Opcode opcode) {
            switch(opcode) {
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
		
        public class Add : Binary { }

        public class Add_IntInt : Binary { }

        public class Assign : Binary { }

        public class Branch : Statement {
            [PackageProperty(0)]
            public ushort TargetOffset { get; protected set; }

            protected override int PartialUnpackedCodeSize { get { return 3; } }

            public override string ToString() {
                return GetType().Name + " to " + TargetOffset;
            }
        }

        public class BranchIfFalse : Branch {
            [PackageProperty(0)]
            public Statement Test { get; protected set; }

            protected override int PartialUnpackedCodeSize { get { return 3 + Test.UnpackedCodeSize; } }

            public override string ToString() {
                return base.ToString() + " ifnot " + Test.ToString();
            }
        }

        public class ConCat_StrStr : Binary { }

        public class DeclareParameter : UnaryReference { }

        public class Equals_IntInt : Binary { }

        public class Equals_StringString : Binary { }

        public class Field : UnaryReference { }

		public class FRand : NoArguments { }

        public class InStr : Binary { }

        public class Index : Binary { }

        public class IntToString : Unary { }

        public class Left : Binary { }

        public class Len : Unary { }

        public class LessThan : Binary { }

        public class LiteralInt : Literal {
            [PackageProperty(0)]
            public int IntValue { get; protected set; }

            public override object Value { get { return IntValue; } }
            protected override int PartialUnpackedCodeSize { get { return 5; } }
        }

        public class LiteralIntByte : Literal {
            [PackageProperty(0)]
            public byte ByteValue { get; protected set; }

            public override object Value { get { return ByteValue; } }
            protected override int PartialUnpackedCodeSize { get { return 2; } }
        }

        public class LiteralStringAsciiz : LiteralString {
            [PackageProperty(0, typeof(DataProcessors.NulTerminatedAscii))]
            public string StringValue { get; protected set; }

            public override object Value { get { return StringValue; } }
            protected override int PartialUnpackedCodeSize { get { return 1 + StringValue.Length + 1; } }
        }

        public class Local : UnaryReference { }

        public class Log : Unary { }

        public class LogicalAnd : Statement {
            [PackageProperty(0)]
            public Statement A { get; protected set; }

            public int ElseCount { get; protected set; }

            [PackageProperty(1, typeof(ElseBranchReader))]
            public ushort ElseBranch { get; protected set; }

            [PackageProperty(2)]
            public Statement B { get; protected set; }

            protected override int PartialUnpackedCodeSize { get { return 1 + A.UnpackedCodeSize + ElseCount + 1 + 2 + B.UnpackedCodeSize; } }

            public override string ToString() {
                return "(" + GetType().Name + " " + A.ToString() + " " + B.ToString() + ")";
            }

            class ElseBranchReader : DataProcessor {
                public override object Read(object target, Package package, System.IO.BinaryReader reader, long end) {
                    LogicalAnd targetAnd = (LogicalAnd)target;
                    byte code;

                    targetAnd.ElseCount = 0;
                    while((code = reader.ReadByte()) == 0x16)
                        targetAnd.ElseCount++;
                    if(code != 0x18)
                        throw new Exception();
                    return reader.ReadUInt16();
                }
            }

        }

        public class LogicalOr : LogicalAnd { }

        public class Mid : Trinary { }

		public class Multiply : Binary { }

        public class NotEqual_IntInt : Binary { }

        public class NotEqual_StringString : Binary { }

        public class Null : NoArguments { }

        public class One : NoArguments { }

        public class PostIncrement : Unary { }

        public class Return : Unary { }

		//public class ReturnNothing : NoArguments { }

        public class Right : Binary { }

		public class Subtract : Binary { }

        public class Subtract_IntInt : Binary { }

        public class Zero : NoArguments { }
    }

    public enum Opcode : byte {
        Local = 0x00,
        Field = 0x01,
        Return = 0x04,
        Branch = 0x06,
        BranchIfFalse = 0x07,
        Null = 0x0B,
        Assign = 0x0F,
        Pop = 0x16,
        //Equals_IntInt = 0x17,
        Index = 0x1A,
        LiteralInt = 0x1D,
        StringLiteralAsciiz = 0x1F,
        Zero = 0x25,
        One = 0x26,
        DeclareParameter = 0x29,
        LiteralIntByte = 0x2C,
        ReturnNothing = 0x3A,
        IntToString = 0x53,
        ConCat_StrStr = 0x70,
        Equals_StringString = 0x7A,
        NotEqual_StringString = 0x7B,
        Len = 0x7D,
        InStr = 0x7E,
        Mid = 0x7F,
        Left = 0x80,
        LogicalAnd = 0x82,
        LogicalOr = 0x84,
        Add_IntInt = 0x92,
        Subtract_IntInt = 0x93,
        LessThan = 0x96,
        Equals_IntInt = 0x9A,
        NotEqual_IntInt = 0x9B,
        PostIncrement = 0xA5,
        Multiply = 0xAB,
        Add = 0xAE,
        Subtract = 0xAF,
        FRand = 0xC3,
        Log = 0xE7,
        Right = 0xEA,
    }
}
