using OOP_2__console_text_editor_.Models;

namespace OOP_2__console_text_editor_.Controllers;

public class CursorController
{
    private Cursor cursor;
    private Document? document;

    public CursorController()
    {
        cursor = new Cursor();
        cursor.X = 0;
        cursor.Y = 0;
    }

    public int GetY()
    {
        return cursor.Y;
    }

    public int GetX()
    {
        return cursor.X;
    }

    public (int, int) GetPosition()
    {
        return (cursor.X, cursor.Y);
    }

    public void SetDocument(Document document)
    {
        this.document = document;
        ResetPosition();
    }

    public void SetPosition(int x, int y)
    {
        cursor.X = x;
        cursor.Y = y;
    }


    public void ResetPosition()
    {
        cursor.X = 0;
        cursor.Y = 0;
    }
    public void ChangePosition(int x, int y)
    {
        if (document != null)
        {
            cursor.Y = Math.Clamp(y, 0, document.Lines.Count - 1);
            cursor.X = Math.Clamp(x, 0, document.Lines[cursor.Y].Length);
        }
    }

    public void MoveUp()
    {
        if (cursor.Y > 0)
        {
            cursor.Y--;
            UpdatePosition();
        }
    }

    public void MoveDown()
    {
        if (document == null) return;

        if (cursor.Y < document.Lines.Count - 1)
        {
            cursor.Y++;
            UpdatePosition();
        }
    }

    public void MoveLeft()
    {
        if (cursor.X > 0)
        {
            cursor.X--;
        }
        else if (cursor.Y > 0 && document != null)
        {
            cursor.Y--;
            cursor.X = document.Lines[cursor.Y].Length;
        }

        UpdatePosition();
    }

    public void MoveRight()
    {
        if (document == null) return;

        int lineLength = document.Lines[cursor.Y].Length;
        
        if (cursor.X < lineLength)
        {
            cursor.X++;
        }
        else if (cursor.Y < document.Lines.Count - 1)
        {
            cursor.Y++;
            cursor.X = 0;
        }

        UpdatePosition();
    }

    public void MoveToLineStart()
    {
        cursor.X = 0;
    }

    public void MoveToLineEnd()
    {
        if (document != null)
        {
            cursor.X = document.Lines[cursor.Y].Length;
        }
    }
    

    private void UpdatePosition()
    {
        if (document == null)
            return;

        if (document.Lines.Count == 0)
        {
            cursor.Y = 0;
            cursor.X = 0;
            return;
        }

        cursor.Y = Math.Clamp(cursor.Y, 0, document.Lines.Count - 1);

        string currentLine = document.Lines[cursor.Y];
    
        cursor.X = Math.Clamp(cursor.X, 0, currentLine.Length);
    }
    

}