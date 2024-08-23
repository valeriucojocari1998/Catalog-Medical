namespace Catalog_Medical.Models.Events;

public class PatientCreatedEvent
{
    public int PatientId { get; set; }
    public required string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
}
