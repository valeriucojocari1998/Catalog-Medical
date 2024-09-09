#nullable enable
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace Catalog_Medical.Models.Entities;

public class User : IdentityUser
{
    public string Name { get; set; } = "";
    public string Surname { get; set; }  = "";
    public string Phone { get; set; } = "";

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DoctorType DoctorType { get; set; }
    public string OtherType { get; set; } = "";
}

public enum DoctorType
{
    Cardiologist,
    Dermatologist,
    Neurologist,
    Oncologist,
    Pediatrician,
    Surgeon,
    Urologist,
    Radiologist,
    Pathologist,
    Psychiatrist,
    Gynecologist,
    Endocrinologist,
    Gastroenterologist,
    Pulmonologist,
    Nephrologist,
    Anesthesiologist,
    Orthopedic,
    Ophthalmologist,
    Otolaryngologist,
    Other
}
