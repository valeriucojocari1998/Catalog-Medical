using Catalog_Medical.Models.Entities;
using Catalog_Medical.Models.Requests;

namespace Catalog_Medical.Interfaces;

public interface IPatientService
{
    Task<IEnumerable<Patient>> GetPatients(string UserId, PatientFilterRequest? filter = null);
    Task<Patient> GetPatientById(string id);
    Task<Patient> AddPatient(string UserId, CreatePatientRequest patient);
    Task UpdatePatient(UpdatePatientRequest patient);
    Task DeletePatient(string id);
}