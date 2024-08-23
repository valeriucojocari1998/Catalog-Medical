namespace Catalog_Medical.Models.DTOs;

public class MedicalTestDTO
{
    public int Id { get; set; }
    public string TestName { get; set; }
    public DateTime TestDate { get; set; }
    public string Results { get; set; }
}
