using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Utils;
using OOP_2__console_text_editor_.Utils.Decorators;

namespace OOP_2__console_text_editor_.Services;

public class TextDecoratorService
{
    public string GetBoldText(string text)
    {
        ITextComponent textComponent = new PlainText(text);
        textComponent = new BoldDecorator(textComponent);
        
        return textComponent.GetText();
    }

    public string GetItalicText(string text)
    {
        ITextComponent textComponent = new PlainText(text);
        textComponent = new ItalicDecorator(textComponent);
        
        return textComponent.GetText();
    }

    public string GetUnderlineText(string text)
    {
        ITextComponent textComponent = new PlainText(text);
        textComponent = new UnderlineDecorator(textComponent);
        
        return textComponent.GetText();
    }
}