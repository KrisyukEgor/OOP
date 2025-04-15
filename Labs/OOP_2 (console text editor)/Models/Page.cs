namespace OOP_2__console_text_editor_.Models;

public class Page
{
    private List<Button> buttons;
    private string name;
    public Page(string name)
    {
        buttons = new List<Button>();
        this.name = name;
    }

    public void AddButton(int x, int y, string text)
    {
        Button button = new Button(text);

        button.X = x;
        button.Y = y;
        
        buttons.Add(button);
    }

    public void SetButtons(List<Button> buttons)
    {
        this.buttons = buttons;
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public List<Button> GetButtons()
    {
        return buttons;
    }
}