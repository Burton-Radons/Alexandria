using Moki.Messages;
using Moki.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moki.Compiler {
	/// <summary>
	/// Converts a set of tokens read from a <see cref="Lexer"/> into a syntax tree.
	/// </summary>
	public abstract class Parser : MessageProvider {
		public bool IsEndOfSource { get { return Lexer.Peek().Code == TokenCode.EndOfSource; } }

		public Lexer Lexer { get; private set; }

		public override MessageHandler MessageHandler { get { return Lexer.MessageHandler; } }

		public Parser(Lexer lexer) {
			if (lexer == null)
				throw new ArgumentNullException("lexer");
			Lexer = lexer;
		}

		/// <summary>Parse a <see cref="Declaration"/>. Return the new <see cref="Declaration"/>, or <c>null</c> if none could be read.</summary>
		/// <returns>The new <see cref="Declaration"/>, or <c>null</c> if there was none.</returns>
		public virtual Declaration ParseDeclaration() {
			ErrorExpectedNode<Declaration>();
			return null;
		}

		/// <summary>Parse a <see cref="Expression"/>. Return the new <see cref="Expression"/>, or <c>null</c> if none could be read.</summary>
		/// <returns>The new <see cref="Expression"/>, or <c>null</c> if there was none.</returns>
		public virtual Expression ParseExpression() {
			ErrorExpectedNode<Expression>();
			return null;
		}

		/// <summary>Parse a <see cref="Statement"/>. Return the new <see cref="Statement"/>, or <c>null</c> if none could be read.</summary>
		/// <returns>The new <see cref="Statement"/>, or <c>null</c> if there was none.</returns>
		public virtual Statement ParseStatement() {
			ErrorExpectedNode<Statement>();
			return null;
		}

		/// <summary>Parse a <see cref="TypeDeclaration"/>. Return the new <see cref="TypeDeclaration"/>, or <c>null</c> if none could be read.</summary>
		/// <returns>The new <see cref="TypeDeclaration"/>, or <c>null</c> if there was none.</returns>
		public virtual TypeDeclaration ParseTypeDeclaration() {
			ErrorExpectedNode<TypeDeclaration>();
			return null;
		}

		#region Warnings

		#endregion Warnings

		#region Errors

		/// <summary>A type of <see cref="Node"/> was expected, but instead something else was encountered. The next token will be used (and consumed) to report the error.</summary>
		/// <param name="expectedNodeType">The type of the <see cref="Node"/> that was expected.</param>
		protected void ErrorExpectedNode(Type expectedNodeType) {
			SendMessage(new ExpectedNodeError(expectedNodeType, Lexer.Next()));
		}

		/// <summary>A type of <see cref="Node"/> was expected, but instead something else was encountered. The next token will be used (and consumed) to report the error.</summary>
		/// <typeparam name="TExpectedNodeType">The type of the <see cref="Node"/> that was expected.</typeparam>
		protected void ErrorExpectedNode<TExpectedNodeType>() where TExpectedNodeType : Node {
			ErrorExpectedNode(typeof(TExpectedNodeType));
		}

		#endregion Errors
	}
}
