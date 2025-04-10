using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;

namespace OOP_2__console_text_editor_.Commands.Select;

public class SelectRightCommand : ICommand
 {
     DocumentController controller;
     
     public SelectRightCommand(DocumentController controller)
     {
         this.controller = controller; 
     }
     
     public void Execute()
     {
         controller.SelectRight();
     }
 
     public void UnExecute()
     {
     }
 }