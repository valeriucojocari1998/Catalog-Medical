
namespace Catalog_Medical.Interfaces;

public interface IMedicalTestRepository
{
    Task AddMedicalTestAsync(MedicalTest medicalTest);
    Task<MedicalTest> GetMedicalTestByIdAsync(string testId);
    Task<IEnumerable<MedicalTest>> GetMedicalTestsByPatientIdAsync(string patientId);
    Task DeleteMedicalTestAsync(string testId);
}