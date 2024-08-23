using Catalog_Medical.Models.Entities;

namespace Catalog_Medical.Interfaces;

public interface IMedicalTestRepository
{
    Task<IEnumerable<MedicalTest>> GetAllMedicalTestsAsync();
    Task<MedicalTest> GetMedicalTestByIdAsync(int id);
    Task<MedicalTest> AddMedicalTestAsync(MedicalTest medicalTest);
    Task<MedicalTest> UpdateMedicalTestAsync(MedicalTest medicalTest);
    Task DeleteMedicalTestAsync(int id);
}