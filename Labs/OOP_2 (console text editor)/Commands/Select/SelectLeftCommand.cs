using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;

namespace OOP_2__console_text_editor_.Commands.Select;

public class SelectLeftCommand : ICommand
{
    DocumentController controller;
     
    public SelectLeftCommand(DocumentController controller)
    {
        this.controller = controller; 
    }
     
    public void Execute()
    {
        controller.SelectLeft();
    }
 
    public void UnExecute()
    {
        // controller.SelectRight();
    }
}