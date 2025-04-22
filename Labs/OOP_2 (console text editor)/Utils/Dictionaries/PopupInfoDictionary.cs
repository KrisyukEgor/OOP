using OOP_2__console_text_editor_.Commands.Window;
using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Services.Window;

namespace OOP_2__console_text_editor_.Utils.Dictionaries;

public class PopupInfoDictionary : IDictionary
{
    private readonly Dictionary<(ConsoleKey Key, ConsoleModifiers Mods), Func<ICommand>> _commands = new();
    private PageService _pageService;

    public PopupInfoDictionary(PageService pageService)
    {
        this._pageService = pageService;
        
        InizializeCommands();
    }
    public ICommand? GetCommand(ConsoleKeyInfo key)
    {
        var turpleKey = (key.Key, key.Modifiers);
        
        if (_commands.TryGetValue(turpleKey, out var commandFunc))
        {
            return commandFunc();
        }

        return null;
    }

    private void InizializeCommands()
    {
        _commands.Add((ConsoleKey.LeftArrow, 0), () => new SelectDownButtonCommand(_pageService));
        _commands.Add((ConsoleKey.RightArrow, 0), () => new SelectUpButtonCommand(_pageService));
        _commands.Add((ConsoleKey.Enter, 0), () => new ClickCommand(_pageService));
    }
}