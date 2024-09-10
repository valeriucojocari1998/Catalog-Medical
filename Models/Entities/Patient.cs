namespace Catalog_Medical.Models.Entities;

public class Patient
{
    public string Id { get; set; }
    public string DoctorId { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
}