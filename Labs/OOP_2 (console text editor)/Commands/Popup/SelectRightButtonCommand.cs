using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Services.Popup;
using OOP_2__console_text_editor_.Services.Window;

namespace OOP_2__console_text_editor_.Commands.Window;

public class SelectRightButtonCommand : ICommand
{
    private PopupInputService _popupInputService;
    public SelectRightButtonCommand(PopupInputService popupInputService)
    {
        this._popupInputService = popupInputService;
    }

    public void Execute()
    {
        this._popupInputService.SelectRightButton();
    }

    public void UnExecute()
    {
        
    }
}