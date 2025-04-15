namespace OOP_2__console_text_editor_.Models;

public class Button
{
    private string text;
    
    public event Action OnClick;

    public Button(string text)
    {
        this.text = text;
    }

    public int X { get; set; }
    public int Y { get; set; }
    
    public int Width { get; set; }
    public int Height { get; set; }

    public string Text
    {
        get { return text; }
        set { text = value; }
    }
    
    public bool IsSelected { get; set; }
    
    public void Click()
    {
        OnClick?.Invoke();
    }
}