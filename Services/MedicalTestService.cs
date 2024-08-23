using Catalog_Medical.Interfaces;
using Catalog_Medical.Models.Entities;

namespace Catalog_Medical.Services;

public class MedicalTestService
{
    private readonly IMedicalTestRepository _medicalTestRepository;

    public MedicalTestService(IMedicalTestRepository medicalTestRepository)
    {
        _medicalTestRepository = medicalTestRepository;
    }

    public async Task<IEnumerable<MedicalTest>> GetAllMedicalTestsAsync()
    {
        return await _medicalTestRepository.GetAllMedicalTestsAsync();
    }

    public async Task<MedicalTest> GetMedicalTestByIdAsync(int id)
    {
        return await _medicalTestRepository.GetMedicalTestByIdAsync(id);
    }

    public async Task<MedicalTest> AddMedicalTestAsync(MedicalTest medicalTest)
    {
        return await _medicalTestRepository.AddMedicalTestAsync(medicalTest);
    }

    public async Task<MedicalTest> UpdateMedicalTestAsync(MedicalTest medicalTest)
    {
        return await _medicalTestRepository.UpdateMedicalTestAsync(medicalTest);
    }

    public async Task DeleteMedicalTestAsync(int id)
    {
        await _medicalTestRepository.DeleteMedicalTestAsync(id);
    }
}
