using OOP_2__console_text_editor_.Commands.Window;
using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Services.Popup;

namespace OOP_2__console_text_editor_.Utils.Dictionaries;

public class PopupInputDictionary : IDictionary
{
    private readonly Dictionary<(ConsoleKey Key, ConsoleModifiers Mods), Func<ICommand>> _commands = new();
    private PopupInputService popupInputService;

    public PopupInputDictionary(PopupInputService popupInputService)
    {
        this.popupInputService = popupInputService;
        
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
        _commands.Add((ConsoleKey.LeftArrow, 0), () => new SelectLeftButtonCommand(popupInputService));
        _commands.Add((ConsoleKey.RightArrow, 0), () => new SelectRightButtonCommand(popupInputService));
        _commands.Add((ConsoleKey.Enter, 0), () => new PopupClickCommand(popupInputService));
    }
}