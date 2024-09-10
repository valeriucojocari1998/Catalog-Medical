namespace Catalog_Medical.Interfaces;

public interface IMedicalTestService
{
    Task AddMedicalTestAsync(string patientId, IFormFile pdfFile, string testName);
    Task<IEnumerable<MedicalTest>> GetMedicalTestsByPatientIdAsync(string patientId);
    Task<MedicalTest> GetMedicalTestByIdAsync(string testId);
    Task DeleteMedicalTestAsync(string testId);
}
