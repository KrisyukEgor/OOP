using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Services.Window;

namespace OOP_2__console_text_editor_.Commands.Window;

public class SelectUpButtonCommand : ICommand
{
    private PageService pageService;
    public SelectUpButtonCommand(PageService pageService)
    {
        this.pageService = pageService;
    }

    public void Execute()
    {
        this.pageService.SelectUpButton();
    }

    public void UnExecute()
    {
        
    }
}