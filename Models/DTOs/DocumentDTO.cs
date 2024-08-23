namespace Catalog_Medical.Models.DTOs;

public class DocumentDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DateUploaded { get; set; }
}
