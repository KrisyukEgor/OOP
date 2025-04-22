using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Services.Window;

namespace OOP_2__console_text_editor_.Services.Popup;

public class PopupService
{
    private int popupWidth = 0;
    private int popupHeight = 0;
    

    private WindowSizeService windowSizeService;
    public PopupService(WindowSizeService windowSizeService, InputController inputController)
    {
        this.windowSizeService = windowSizeService;
    }
    public void RenderInputPopup()
    {
        
    }

    public void RenderInformationPopup(string content)
    {
        
    }

    public void Focus()
    {
        
    }

    private void CalculatePopupParameters(string content)
    {
        
    }
}