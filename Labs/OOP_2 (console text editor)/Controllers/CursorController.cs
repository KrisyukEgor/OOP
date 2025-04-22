using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Models;

namespace OOP_2__console_text_editor_.Controllers;

public class CursorController
{
    private Cursor cursor;
    private Document? document;
    private IDocumentViewer documentViewer;

    public CursorController(IDocumentViewer documentViewer)
    {
        this.documentViewer = documentViewer;
        
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

    public void MoveCursorUp()
    {
        if (cursor.Y > 0)
        {
            cursor.Y--;
            UpdatePosition();
        }
    }

    public void MoveCursorDown()
    {
        if (document == null) return;

        if (cursor.Y < document.Lines.Count - 1)
        {
            cursor.Y++;
            UpdatePosition();
        }
    }
    
    public void MoveCursorLeft()
    {
        if (cursor.X > 0)
        {
            cursor.X--;
        }
        else if (cursor.Y > 0 && document != null)
        {
            cursor.Y--;
            MoveToLineEnd();
        }

        UpdatePosition();
    }


    public void MoveCursorRight()
    {
        if (document == null) 
            return;  

        var currentLine = document.Lines[cursor.Y];
        bool atEndOfLine = cursor.X >= currentLine.Length;
        bool atBreakSymbol = !atEndOfLine && currentLine.GetStyledSymbol(cursor.X).Symbol == '\0';
        
        if (atEndOfLine || atBreakSymbol)
        {
            if (cursor.Y < document.Lines.Count - 1)
            {
                cursor.Y++;
                MoveToLineStart();
            }
            return;
        }
        
        cursor.X++;
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
            var line = document.Lines[cursor.Y];
            cursor.X = line.Length;
            
            if (line.Length > 0 && line.GetStyledSymbol(line.Length - 1).Symbol == '\0')
            {
                cursor.X = line.Length - 1;
            }
        }
    }
    

    public void UpdatePosition()
    {
        if (document == null) return;

        if (document.Lines.Count == 0)
        {
            cursor.X = 0;
            cursor.Y = 0;
            return;
        }
        
        cursor.Y = Math.Clamp(cursor.Y, 0, document.Lines.Count - 1);
        
        var currentLine = document.Lines[cursor.Y];
        var maxX = currentLine.Length;
        
        if (currentLine.Length > 0 && currentLine.GetStyledSymbol(currentLine.Length - 1).Symbol == '\0')
        {
            maxX = currentLine.Length - 1;
        }
        
        cursor.X = Math.Clamp(cursor.X, 0, maxX);
        
        documentViewer.SetCursorPosition(cursor.X, cursor.Y);
    }
    

}