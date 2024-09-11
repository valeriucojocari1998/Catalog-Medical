using Catalog_Medical.Interfaces;

namespace Catalog_Medical.Services;

public class MedicalTestService : IMedicalTestService
{
    private readonly IMedicalTestRepository _medicalTestRepository;

    public MedicalTestService(IMedicalTestRepository medicalTestRepository)
    {
        _medicalTestRepository = medicalTestRepository;
    }

    public async Task<MedicalTest> AddMedicalTestAsync(string patientId, string title, string description, string date, IFormFile file)
    {
        var medicalTest = new MedicalTest
        {
            PatientId = patientId,
            Title = title,
            Description = description,
            Date = DateTime.UtcNow
        };

        return await _medicalTestRepository.AddMedicalTestAsync(medicalTest, file);
    }

    public async Task<IEnumerable<MedicalTest>> GetMedicalTestsByPatientIdAsync(string patientId)
    {
        return await _medicalTestRepository.GetMedicalTestsByPatientIdAsync(patientId);
    }

    public async Task<MedicalTest> GetMedicalTestByIdAsync(string testId)
    {
        return await _medicalTestRepository.GetMedicalTestByIdAsync(testId);
    }

    public async Task DeleteMedicalTestAsync(string testId)
    {
        await _medicalTestRepository.DeleteMedicalTestAsync(testId);
    }
}
