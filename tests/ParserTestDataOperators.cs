using Xunit;
using Minsk.CodeAnalysis;

namespace Tests
{
    internal class ParserTestDataOperators : TheoryData<char, SyntaxKind>
    {
        public ParserTestDataOperators()
        {
            Add('*', SyntaxKind.StarToken);
            Add('/', SyntaxKind.SlashToken);
            Add('+', SyntaxKind.PlusToken);
            Add('-', SyntaxKind.MinusToken);
        }
    }
}