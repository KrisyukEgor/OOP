using OOP_2__console_text_editor_.Models;
using OOP_2__console_text_editor_.Services.Document;

namespace OOP_2__console_text_editor_.Services;

public class ButtonSetClickService
{
    private DocumentCreator _documentCreator;

    public ButtonSetClickService(DocumentCreator documentCreator)
    {
        _documentCreator = documentCreator;
    }

    public void ButtonSetCreateDocument(Button button)
    {
        button.OnClick += OnCreateDocumentButtonClicked;

    }

    private void OnCreateDocumentButtonClicked()
    {
        Models.Document document = _documentCreator.CreateDocument();
        
    }
}