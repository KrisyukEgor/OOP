using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Models;

namespace OOP_2__console_text_editor_.Services.Page;

public class PageService
{
    private PageController _pageController;
    
    private Models.Page currentPage;
    private List<Models.Page> pagesList = new();
    
    private Models.Page _currentPage;
    
    public PageService(PageController pageController)
    {
        _pageController = pageController;
    }
    
    public void SelectDownButton()
    {
        SelectButton(1);
    }

    public void SelectUpButton()
    {
        SelectButton(-1);
    }

    private void SelectButton(int move)
    {
        var buttons = currentPage.GetButtons();

        int selectedIndex = buttons.FindIndex(b => b.IsSelected);

        if (selectedIndex == -1)
        {
            buttons[0].IsSelected = true;
        }
        else
        {
            buttons[selectedIndex].IsSelected = false;
            
            int nexIndex = (selectedIndex + move + buttons.Count) % buttons.Count;
            buttons[nexIndex].IsSelected = true;
        }
        
        
        UpdateView();
    }
    
    public void RenderDocumentStatePage()
    {
        string pageName = "DocumentState";
        var page = GetPage(pageName);
        
        if (page == null)
        {
            page = new Models.Page(pageName);
            
            Button openDocButton = new Button("Open document");
            Button createDocButton = new Button("Create document");

            List<Button> buttons = new List<Button>() { openDocButton, createDocButton };

            _pageController.CalculateButtonsParameters(buttons);
            
            page.SetButtons(buttons);
            
            pagesList.Add(page);
            
        }
        
        currentPage = page;
        UpdateView();
        
    }

    private void UpdateView()
    {
        _pageController.RenderPage(currentPage);
    }
    
    private Models.Page? GetPage(string name)
    {
        foreach (var page in pagesList)
        {
            if (page.Name == name)
            {
                return page;
            }
        }

        return null;
    }
}