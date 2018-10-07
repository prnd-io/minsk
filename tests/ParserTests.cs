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
            var parser = new Parser("1 + 1");
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
            var parser = new Parser("(1 + 2)");
            var syntaxTree = parser.Parse();

            using (new AssertionScope())
            {
            }
        }

        [Fact]
        public void Parser_Should_Detected_Error()
        {
            var parser = new Parser("+");
            var syntaxTree = parser.Parse();

            using (new AssertionScope())
            {
            }
        }

        [Fact]
        public void Parser_Should_Parse_Multiplication()
        {
            var parser = new Parser("1 * 2");
            var syntaxTree = parser.Parse();

            using (new AssertionScope())
            {
            }
        }

        [Fact]
        public void Parser_Should_Parse_Unsupported_Number()
        {
            var longNumberString = (int.MaxValue + 1L).ToString();
            var parser = new Parser("1 + " + longNumberString);
            var syntaxTree = parser.Parse();

            using (new AssertionScope())
            {
            }
        }
    }
}