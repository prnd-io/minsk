using System;
using Xunit;
using Minsk.CodeAnalysis;
using FluentAssertions;
using FluentAssertions.Execution;

namespace Tests
{
    public class LexerTests
    {
        [Fact]
        public void Lexer_Should_Tokenize_Number()
        {
            var lexer = new Lexer("123");
            var token = lexer.NextToken();

            using (new AssertionScope())
            {
                token.Kind.Should().Be(SyntaxKind.NumberToken);
                token.Position.Should().Be(0);
                token.Text.Should().Be("123");
                token.Value.Should().BeOfType(typeof(System.Int32));
            }
        }

        [Fact]
        public void Lexer_Should_Add_Diagnostics_For_Bad_Numbers()
        {
            var numberToTest = (int.MaxValue + 1L).ToString();
            var lexer = new Lexer(numberToTest);
            var token = lexer.NextToken();

            using (new AssertionScope())
            {
                token.Kind.Should().Be(SyntaxKind.NumberToken);
                token.Position.Should().Be(0);
                token.Text.Should().Be(numberToTest);
                lexer.Diagnostics.Should().Contain($"The number {numberToTest} isn't valid Int32.");
            }
        }

        [Fact]
        public void Lexer_Should_Tokenize_Whitespace()
        {
            var lexer = new Lexer(" ");
            var token = lexer.NextToken();

            using (new AssertionScope())
            {
                token.Kind.Should().Be(SyntaxKind.WhitespaceToken);
                token.Position.Should().Be(0);
                token.Text.Should().Be(" ");
                token.Value.Should().BeNull();
            }
        }

        [Fact]
        public void Lexer_Should_Tokenize_Plus()
        {
            var lexer = new Lexer("+");
            var token = lexer.NextToken();

            using (new AssertionScope())
            {
                token.Kind.Should().Be(SyntaxKind.PlusToken);
                token.Position.Should().Be(0);
                token.Text.Should().Be("+");
                token.Value.Should().BeNull();
            }
        }

        [Fact]
        public void Lexer_Should_Tokenize_Minus()
        {
            var lexer = new Lexer("-");
            var token = lexer.NextToken();

            using (new AssertionScope())
            {
                token.Kind.Should().Be(SyntaxKind.MinusToken);
                token.Position.Should().Be(0);
                token.Text.Should().Be("-");
                token.Value.Should().BeNull();
            }
        }

        [Fact]
        public void Lexer_Should_Tokenize_Star()
        {
            var lexer = new Lexer("*");
            var token = lexer.NextToken();

            using (new AssertionScope())
            {
                token.Kind.Should().Be(SyntaxKind.StarToken);
                token.Position.Should().Be(0);
                token.Text.Should().Be("*");
                token.Value.Should().BeNull();
            }
        }

        [Fact]
        public void Lexer_Should_Tokenize_Slash()
        {
            var lexer = new Lexer("/");
            var token = lexer.NextToken();

            using (new AssertionScope())
            {
                token.Kind.Should().Be(SyntaxKind.SlashToken);
                token.Position.Should().Be(0);
                token.Text.Should().Be("/");
                token.Value.Should().BeNull();
            }
        }

        [Fact]
        public void Lexer_Should_Tokenize_OpenParenthesis()
        {
            var lexer = new Lexer("(");
            var token = lexer.NextToken();

            using (new AssertionScope())
            {
                token.Kind.Should().Be(SyntaxKind.OpenParenthesisToken);
                token.Position.Should().Be(0);
                token.Text.Should().Be("(");
                token.Value.Should().BeNull();
            }
        }

        [Fact]
        public void Lexer_Should_Tokenize_CloseParenthesis()
        {
            var lexer = new Lexer(")");
            var token = lexer.NextToken();

            using (new AssertionScope())
            {
                token.Kind.Should().Be(SyntaxKind.CloseParenthesisToken);
                token.Position.Should().Be(0);
                token.Text.Should().Be(")");
                token.Value.Should().BeNull();
            }
        }

        [Fact]
        public void Lexer_Should_Tokenize_EndOfFile()
        {
            var lexer = new Lexer("");
            var token = lexer.NextToken();

            using (new AssertionScope())
            {
                token.Kind.Should().Be(SyntaxKind.EndOfFileToken);
                token.Position.Should().Be(0);
                token.Text.Should().Be("\0");
                token.Value.Should().BeNull();
            }
        }

        [Fact]
        public void Lexer_Should_Tokenize_BadToken()
        {
            var badTokenText = "@";
            var lexer = new Lexer(badTokenText);
            var token = lexer.NextToken();

            using (new AssertionScope())
            {
                token.Kind.Should().Be(SyntaxKind.BadToken);
                token.Position.Should().Be(0);
                token.Text.Should().Be(badTokenText);
                token.Value.Should().BeNull();
                lexer.Diagnostics.Should().Contain($"ERROR: bad character input: '{badTokenText}'");
            }
        }
    }
}
