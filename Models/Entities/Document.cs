namespace Catalog_Medical.Models.Entities;

public class Document
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DateUploaded { get; set; }
    public int PatientId { get; set; }
    public Patient Patient { get; set; }
}
