using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Models;

namespace OOP_2__console_text_editor_.Controllers;

public class DocumentController
{ 
    private Document? document = null;
    private readonly IDocumentViewer _documentViewer;
    
    public DocumentController(IDocumentViewer documentViewer)
    {
       
        _documentViewer = documentViewer;
    }

    public void SetDocument(Document document)
    {
        this.document = document;
        
        UpdateView();
    }
    
    public void UpdateView()
    {
        if (document == null || document.Lines.Count == 0) return;
        
        _documentViewer.Render(document);
        
    }
    
}