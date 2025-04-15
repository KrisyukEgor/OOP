using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Utils;

namespace OOP_2__console_text_editor_;

public class App
{
    private AppController _appController;

    public App()
    {
        _appController = new AppController();
    }
    public void Start()
    {
        _appController.Start();
    }
    
}

