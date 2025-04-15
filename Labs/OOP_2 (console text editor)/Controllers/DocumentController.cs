using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Models;

namespace OOP_2__console_text_editor_.Controllers;

public class DocumentController
{ 
    private Document? document = null;
    private readonly IDocumentViewer _documentViewer;
    
    private int _scrollOffset = 0;

    public DocumentController(IDocumentViewer documentViewer)
    {
       
        _documentViewer = documentViewer;
    }

    public void SetDocument(Document document)
    {
        this.document = document;
        _scrollOffset = 0;
        
        UpdateView();
    }
    
    public void UpdateView()
    {
        if (document == null || document.Lines.Count == 0) return;
        
        _documentViewer.Render(document, _scrollOffset);
        
    }
    
}