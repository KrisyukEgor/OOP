namespace OOP_2__console_text_editor_.Models.Builders
{
    public class StyledSymbolBuilder
    {
        private char _symbol = '\0';
        private bool _isBold = false;
        private bool _isItalic = false;
        private bool _isUnderline = false;
        private ConsoleColor _textColor = ConsoleColor.Gray;
        private ConsoleColor _backgroundColor = ConsoleColor.Black;

        public StyledSymbolBuilder WithSymbol(char symbol)
        {
            _symbol = symbol;
            return this;
        }

        public StyledSymbolBuilder WithBold(bool isBold = true)
        {
            _isBold = isBold;
            return this;
        }

        public StyledSymbolBuilder WithItalic(bool isItalic = true)
        {
            _isItalic = isItalic;
            return this;
        }

        public StyledSymbolBuilder WithUnderline(bool isUnderline = true)
        {
            _isUnderline = isUnderline;
            return this;
        }

        public StyledSymbolBuilder WithTextColor(ConsoleColor color)
        {
            _textColor = color;
            return this;
        }

        public StyledSymbolBuilder WithBackgroundColor(ConsoleColor color)
        {
            _backgroundColor = color;
            return this;
        }

        public StyledSymbol Build()
        {
            return new StyledSymbol
            {
                Symbol = _symbol,
                IsBold = _isBold,
                IsItalic = _isItalic,
                IsUnderline = _isUnderline,
                TextColor = _textColor,
                BackgroundColor = _backgroundColor
            };
        }
    }
}