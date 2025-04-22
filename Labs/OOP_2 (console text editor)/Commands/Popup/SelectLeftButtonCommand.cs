using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Services.Popup;
using OOP_2__console_text_editor_.Services.Window;

namespace OOP_2__console_text_editor_.Commands.Window;

public class SelectLeftButtonCommand : ICommand
{
    private PopupInputService _popupInputService;
    public SelectLeftButtonCommand(PopupInputService popupInputService)
    {
        this._popupInputService = popupInputService;
    }

    public void Execute()
    {
        this._popupInputService.SelectLeftButton();
    }

    public void UnExecute()
    {
        
    }
}