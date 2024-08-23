namespace Catalog_Medical.Models.Requests;

public class UpdateMedicalTestRequest
{
    public int Id { get; set; }
    public string TestName { get; set; }
    public DateTime TestDate { get; set; }
    public string Results { get; set; }
    public int PatientId { get; set; }
}
