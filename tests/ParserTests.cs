using System;
using Xunit;
using Minsk.CodeAnalysis;
using FluentAssertions;
using FluentAssertions.Execution;

namespace Tests
{
    public class ParserTests
    {
        [Fact]
        public void Parser_Should_Detect_EndOfFile()
        {
            var expression = "1 + 1";
            var parser = new Parser(expression);
            var syntaxTree = parser.Parse();

            using (new AssertionScope())
            {
                syntaxTree.EndOfFileToken.Should().NotBeNull();
                syntaxTree.EndOfFileToken.Position.Should().Be(5);
            }
        }

        [Fact]
        public void Parser_Should_Parse_Parenthesis_Expression()
        {
            var expression = "(1 + 2)";
            var openParenthesisPosition = expression.IndexOf('(');
            var closeParenthesisPosition = expression.IndexOf(')');
            var parser = new Parser(expression);
            var syntaxTree = parser.Parse();

            using (new AssertionScope())
            {
                syntaxTree.Root.Should().NotBeNull();
                syntaxTree.Root.Kind.Should().Be(SyntaxKind.ParenthesizedExpression);
                syntaxTree.Root.Should().BeOfType<ParenthesizedExpressionSyntax>();

                var parenthesizedExpressionSyntax = (ParenthesizedExpressionSyntax)syntaxTree.Root;
                parenthesizedExpressionSyntax.OpenParenthesisToken.Position.Should().Be(openParenthesisPosition);
                parenthesizedExpressionSyntax.CloseParenthesisToken.Position.Should().Be(closeParenthesisPosition);
            }
        }

        [Fact]
        public void Parser_Should_Detected_Incorrect_Number_Expression()
        {
            var parser = new Parser("+");
            var syntaxTree = parser.Parse();

            using (new AssertionScope())
            {
                syntaxTree.Diagnostics.Should().HaveCount(2);
                syntaxTree.Diagnostics.Should().Contain($"ERROR: Unexpected token <{SyntaxKind.PlusToken}>, expected <{SyntaxKind.NumberToken}>");
                syntaxTree.Diagnostics.Should().Contain($"ERROR: Unexpected token <{SyntaxKind.EndOfFileToken}>, expected <{SyntaxKind.NumberToken}>");
            }
        }

        [Theory]
        [ClassData(typeof(ParserTestDataOperators))]
        internal void Parser_Should_Parse_BinaryExpression_Expression(char binaryOperator, SyntaxKind expectedSyntaxKind)
        {
            var expression = $"4 {binaryOperator} 2";
            var operatorPosition = expression.IndexOf(binaryOperator);
            var parser = new Parser(expression);
            var syntaxTree = parser.Parse();

            using (new AssertionScope())
            {                
                syntaxTree.Root.Should().NotBeNull();
                syntaxTree.Root.Kind.Should().Be(SyntaxKind.BinaryExpression);
                syntaxTree.Root.Should().BeOfType<BinaryExpressionSyntax>();

                var binaryExpressionSyntax = (BinaryExpressionSyntax)syntaxTree.Root;
                binaryExpressionSyntax.OperatorToken.Position.Should().Be(operatorPosition);
                binaryExpressionSyntax.OperatorToken.Kind.Should().Be(expectedSyntaxKind);
            }
        }
    }
}