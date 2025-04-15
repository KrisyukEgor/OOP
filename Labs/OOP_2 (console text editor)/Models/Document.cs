namespace OOP_2__console_text_editor_.Models;


public class Document
{

    private List<StyledString> _linesList;

    public Document()
    {
        _linesList = new() {new StyledString()};
    }

    public List<StyledString> Lines
    {
        get { return _linesList; }
        set { _linesList = value; }
    }


    public StyledString GetLine(int index)
    {
        return _linesList[index];
    }
    
}