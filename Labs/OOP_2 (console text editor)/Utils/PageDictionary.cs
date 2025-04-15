using OOP_2__console_text_editor_.Commands.Page;
using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Services.Page;

namespace OOP_2__console_text_editor_.Utils;

public class PageDictionary : IDictionary
{
    private readonly Dictionary<(ConsoleKey Key, ConsoleModifiers Mods), Func<ICommand>> _commands = new();
    private PageService pageService;

    public PageDictionary(PageService pageService)
    {
        this.pageService = pageService;
        
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
        _commands.Add((ConsoleKey.DownArrow, 0), () => new SelectDownButtonCommand(pageService));
        _commands.Add((ConsoleKey.UpArrow, 0), () => new SelectUpButtonCommand(pageService));
    }
}