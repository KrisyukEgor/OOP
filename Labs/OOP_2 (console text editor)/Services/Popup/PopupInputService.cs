using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Services.Window;
using OOP_2__console_text_editor_.Utils.Dictionaries;

namespace OOP_2__console_text_editor_.Services.Popup;

public class PopupInputService
{
    private WindowSizeService _windowSizeService;
    private InputController _inputController;


    public PopupInputService(WindowSizeService windowSizeService, InputController inputController)
    {
        _windowSizeService = windowSizeService;
        _inputController = inputController;
    }

    public void CalculateContentParameters()
    {

    }
    
    public void SelectRightButton()
    {
        
    }

    public void SelectLeftButton()
    {
        
    }

    private void SelectButton(int move)
    {
    }

    public void HandleClick()
    {
        
    }


}