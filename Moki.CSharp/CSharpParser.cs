using Moki.Compiler;
using Moki.Syntax;
using Moki.Syntax.Declarations;
using Moki.Syntax.TypeDeclarations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moki.Compilers.CSharp {
	public class CSharpParser : Parser {
		/// <summary>The string for the contextual keyword "var".</summary>
		const string VarKeyword = "var";

		public CSharpParser(Lexer lexer) : base(lexer) { }

		public override Declaration ParseDeclaration() {
			Token token = null;
			Type declarationType;

			if (TestDeclaration(token, ref token, out declarationType))
				return ParseDeclaration(declarationType);

			return base.ParseDeclaration();
		}

		public Declaration ParseDeclaration(Type declarationType) {
			if (declarationType == typeof(VariableDeclaration))
				return ParseDeclarationVariable();

			ErrorExpectedNode(declarationType);
			return null;
		}

		public VariableDeclaration ParseDeclarationVariable() {
			Marker declarationMarker = Lexer.Marker;
			TypeDeclaration type = ParseTypeDeclaration();
			Token token;

			if (type == null)
				return null;

			VariableDeclaration declaration = new VariableDeclaration(declarationMarker, type);

			do {
				if(!Lexer.Expect(out token, TokenCode.Identifier))
					break;
				Marker marker = token.Marker;
				string name = token.ValueString;
				Expression initialiser = null;

				if (Lexer.Match(TokenCode.Assign))
					initialiser = ParseExpression();
				declaration.Variables.Add(new VariableDeclarationVariable(marker, name, initialiser));
			} while(Lexer.Match(TokenCode.Comma));

			Lexer.Expect(TokenCode.Semicolon);
			return declaration;
		}

		public override Expression ParseExpression() {
			return base.ParseExpression();
		}

		public override Statement ParseStatement() {
			Declaration declaration = ParseDeclaration();
			if (declaration != null)
				return declaration;

			return base.ParseStatement();
		}

		public override TypeDeclaration ParseTypeDeclaration() {
			Token token;

			if (Lexer.Match(out token, TokenCode.TypeLiteral))
				return new LiteralType(token);

			return base.ParseTypeDeclaration();
		}

		public bool TestDeclaration(Token token, ref Token after, out Type declarationType) {
			if (TestVariableDeclaration(token, ref after))
				declarationType = typeof(VariableDeclaration);
			else {
				declarationType = null;
				return false;
			}

			return true;
		}

		/// <summary>Used by <see cref="TestVariableDeclaration"/>.</summary>
		static readonly TokenCode[] VariableDeclarationConfirmation = new TokenCode[] { TokenCode.Comma, TokenCode.Semicolon, TokenCode.Assign };

		public bool TestVariableDeclaration(Token token, ref Token after) {
			// Confirm the pattern "<type> <identifier> [';' | ',' | '=']", which is enough to say it's a variable declaration.
			if ((!Lexer.PeekTest(ref token, TokenCode.Identifier, VarKeyword) && !TestType(token, ref token)) ||
				!Lexer.PeekTest(ref token, TokenCode.Identifier) ||
				!Lexer.PeekTest(ref token, VariableDeclarationConfirmation))
				return false;
			after = token;
			return true;
		}

		/// <summary>Test whether the peeked <see cref="Token"/> is the start of what may be a <see cref="TypeDeclaration"/>. <paramref name="after"/> is set to the next peeked <see cref="Token"/> after the type, if any.</summary>
		/// <param name="token"></param>
		/// <param name="after">If this seems to be a <see cref="TypeDeclaration"/>, this receives the peeked <see cref="Token"/> after the type; otherwise it's unmodified.</param>
		/// <returns>Whether there appears to be a <see cref="TypeDeclaration"/> here.</returns>
		public bool TestType(Token token, ref Token after) {
			if (Lexer.PeekTest(ref token, TokenCode.TypeLiteral)) {
				after = token;
				return true;
			}

			return false;
		}
	}
}
