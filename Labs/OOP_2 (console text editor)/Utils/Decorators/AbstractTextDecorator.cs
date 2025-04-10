using OOP_2__console_text_editor_.Interfaces;

namespace OOP_2__console_text_editor_.Utils.Decorators;

public abstract class AbstractTextDecorator : ITextComponent
{
    protected readonly ITextComponent _component;

    public AbstractTextDecorator(ITextComponent component)
    {
        _component = component;
    }

    public virtual string GetText()
    {
        return _component.GetText();
    }
}