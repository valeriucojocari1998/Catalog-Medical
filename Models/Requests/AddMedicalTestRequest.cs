namespace Catalog_Medical.Models.Requests;

public class AddMedicalTestRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public IFormFile PdfFile { get; set; }
}