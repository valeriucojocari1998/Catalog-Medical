public class MedicalTest
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string PatientId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public string FileUrl { get; set; }
}