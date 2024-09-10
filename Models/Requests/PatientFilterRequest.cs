namespace Catalog_Medical.Models.Requests;

public class PatientFilterRequest
{
    public string? Name { get; set; } = null;
    public string? Gender { get; set; } = null;
    public string? BloodType { get; set; } = null;
    public string? Email { get; set; } = null;
    public string? PhoneNumber { get; set; } = null;
}
