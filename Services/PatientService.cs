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

    public async Task<IEnumerable<Patient>> GetPatients(string UserId, PatientFilterRequest filter = null)
    {
        return await _patientRepository.GetPatients(UserId, filter);
    }

    public async Task<Patient> GetPatientById(string id)
    {
        return await _patientRepository.GetPatientById(id);
    }

    public async Task<Patient> AddPatient(string UserId, CreatePatientRequest request)
    {
        var patient = request.ToPatient(UserId);
        await _patientRepository.AddPatient(patient);
        return patient;
    }

    public async Task<Patient> EditPatient(string patientId, CreatePatientRequest request)
    {
        var patient = await _patientRepository.GetPatientById(patientId);

        patient.Name = request.Name;
        patient.DateOfBirth = request.DateOfBirth;
        patient.Gender = request.Gender;
        patient.PhoneNumber = request.PhoneNumber;
        patient.Email = request.Email;

        await _patientRepository.UpdatePatient(patient);
        return patient;
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