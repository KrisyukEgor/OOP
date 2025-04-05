using OOP_2__console_text_editor_.Models;

namespace OOP_2__console_text_editor_.Controllers;

public class CursorController
{
    private Cursor cursor;
    private Document document;

    public CursorController()
    {
    }

    public void ChangePosition(int x, int y)
    {
        cursor.X = x;
        cursor.Y = y;
    }

    public void MoveUp()
    {
        
    }

    public void MoveDown()
    {
        
    }

    public void MoveLeft()
    {
        
    }
    public void MoveRight() {}
    
}