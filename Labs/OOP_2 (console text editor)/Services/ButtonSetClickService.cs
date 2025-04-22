using OOP_2__console_text_editor_.Models;
using OOP_2__console_text_editor_.Services.Document;

namespace OOP_2__console_text_editor_.Services;

public class ButtonSetClickService
{
    private FileService _fileService = new();
    private DocumentService documentService;
    
    public ButtonSetClickService(DocumentService documentService)
    {
        this.documentService = documentService;
    }

    public void ButtonSetCreateDocument(Button button)
    {
        button.OnClick += OnCreateDocumentButtonClicked;
    }

    public void ButtonSetOpenDocument(Button button)
    {
        button.OnClick += OnOpenDocumentButtonClicked;
    }
    
    private void OnCreateDocumentButtonClicked()
    {
        Models.Document document = _fileService.CreateDocument();
        documentService.Focus(document);
    }

    private void OnOpenDocumentButtonClicked()
    {
        Models.Document document = _fileService.OpenDocument();
        documentService.Focus(document);
    }
    
}