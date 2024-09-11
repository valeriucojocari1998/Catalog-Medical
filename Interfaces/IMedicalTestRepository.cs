
namespace Catalog_Medical.Interfaces;

public interface IMedicalTestRepository
{
    Task<MedicalTest> AddMedicalTestAsync(MedicalTest medicalTest, IFormFile pdfFile);
    Task<MedicalTest> GetMedicalTestByIdAsync(string testId);
    Task<List<MedicalTest>> GetMedicalTestsByPatientIdAsync(string patientId);
    Task DeleteMedicalTestAsync(string testId);
}