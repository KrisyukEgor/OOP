namespace OOP_2__console_text_editor_.Models;

public class Document
{
    
    private List<string> _linesList;

    public Document()
    {
        _linesList = new List<string> { "" };
    }

    public List<string> Lines
    {
        get { return _linesList; }
        set { _linesList = value; }
    }


    public string GetLine(int index)
    {
        return _linesList[index];
    }
    
}
