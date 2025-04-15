using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Utils;

namespace OOP_2__console_text_editor_.Commands.Page;

public class ClickCommand : ICommand
{
    private PageController pageController;
    public ClickCommand(PageController pageController)
    {
        this.pageController = pageController;
    }

    public void Execute()
    {
        // this.pageController.HandleClick();
    }

    public void UnExecute()
    {
        
    }
}