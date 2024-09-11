using Microsoft.AspNetCore.Mvc;

namespace Catalog_Medical.Interfaces;

public interface IMedicalTestService
{
    Task<MedicalTest> AddMedicalTestAsync(string patientId, string title, string description, string date, IFormFile file);
    Task<IEnumerable<MedicalTest>> GetMedicalTestsByPatientIdAsync(string patientId);
    Task<MedicalTest> GetMedicalTestByIdAsync(string testId);
    Task DeleteMedicalTestAsync(string testId);
}
