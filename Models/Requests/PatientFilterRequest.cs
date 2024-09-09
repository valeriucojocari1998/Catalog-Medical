namespace Catalog_Medical.Models.Requests;

public class PatientFilterRequest
{
    public string Name { get; set; }
    public string Gender { get; set; }
    public string BloodType { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime? DateOfBirthFrom { get; set; }
    public DateTime? DateOfBirthTo { get; set; }
}
