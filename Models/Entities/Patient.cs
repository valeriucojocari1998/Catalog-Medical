namespace Catalog_Medical.Models.Entities;

public class Patient
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string MedicalHistory { get; set; }
    public string BloodType { get; set; }
    public DateTime DateOfBirth { get; set; }
}