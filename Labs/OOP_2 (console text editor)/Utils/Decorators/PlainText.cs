using OOP_2__console_text_editor_.Interfaces;

namespace OOP_2__console_text_editor_.Utils.Decorators;

public class PlainText : ITextComponent
{
    private string _text;
    public PlainText(string text)
    {
        this._text = text;
    }

    public string GetText()
    {
        return _text;
    }
}