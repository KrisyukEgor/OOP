using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Services.Popup;
using OOP_2__console_text_editor_.Services.Window;

namespace OOP_2__console_text_editor_.Commands.Window;

public class PopupClickCommand : ICommand
{
    private PopupInputService popupInputService;
    public PopupClickCommand(PopupInputService popupInputService)
    {
        this.popupInputService = popupInputService;
    }

    public void Execute()
    {
        this.popupInputService.HandleClick();
    }

    public void UnExecute()
    {
        
    }
}