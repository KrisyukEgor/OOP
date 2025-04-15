using OOP_2__console_text_editor_.Interfaces;

namespace OOP_2__console_text_editor_.Models;

public class DocumentBuffer : IBuffer
{

    private StyledString buffer = new();
    public DocumentBuffer()
    {
        
    }

    public void SetBuffer(StyledString str)
    {
        var newString = new StyledString(str);
        buffer = newString;
    }

    public StyledString GetBuffer()
    {
        return buffer;
    }
    
}