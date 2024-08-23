namespace Catalog_Medical.Models.Entities;

public class Patient
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string MedicalHistory { get; set; }
    public ICollection<MedicalTest> MedicalTests { get; set; }
}
