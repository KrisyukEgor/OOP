using OOP_2__console_text_editor_.Interfaces;

namespace OOP_2__console_text_editor_.Utils.Decorators;

public class BoldDecorator : AbstractTextDecorator {
    public BoldDecorator(ITextComponent textComponent) : base(textComponent)
    {

    }

    public override string GetText()
    {
        return $"\x1B[1m{base.GetText()}\x1B[0m";
    }

}