namespace Catalog_Medical.Models.Requests;

public class CreatePatientRequest
{
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string MedicalHistory { get; set; }
}
