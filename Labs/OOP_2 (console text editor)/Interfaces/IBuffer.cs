using OOP_2__console_text_editor_.Models;

namespace OOP_2__console_text_editor_.Interfaces;

public interface IBuffer
{
    void SetBuffer(StyledString str);
    StyledString GetBuffer();
    
}