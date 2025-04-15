using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Services.Page;
using OOP_2__console_text_editor_.Utils;

namespace OOP_2__console_text_editor_.Commands.Page;

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