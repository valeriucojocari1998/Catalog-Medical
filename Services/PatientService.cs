using Catalog_Medical.Interfaces;
using Catalog_Medical.Models.Entities;
using Catalog_Medical.Models.Requests;

namespace Catalog_Medical.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;

    public PatientService(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<IEnumerable<Patient>> GetPatients(PatientFilterRequest filter = null)
    {
        return await _patientRepository.GetPatients(filter);
    }

    public async Task<Patient> GetPatientById(string id)
    {
        return await _patientRepository.GetPatientById(id);
    }

    public async Task AddPatient(CreatePatientRequest request)
    {
        var patient = request.ToPatient();
        await _patientRepository.AddPatient(patient);
    }

    public async Task UpdatePatient(UpdatePatientRequest request)
    {
        var currentPatient = await GetPatientById(request.Id);
        var patient = request.UpdatePatient(currentPatient);
        await _patientRepository.UpdatePatient(patient);
    }

    public async Task DeletePatient(string id)
    {
        await _patientRepository.DeletePatient(id);
    }
}