using Catalog_Medical.Models.Entities;
using Catalog_Medical.Models.Requests;

namespace Catalog_Medical.Interfaces;

public interface IPatientRepository
{
    Task<IEnumerable<Patient>> GetPatients(string UserId, PatientFilterRequest filter = null);
    Task<Patient> GetPatientById(string id);
    Task AddPatient(Patient patient);
    Task UpdatePatient(Patient patient);
    Task DeletePatient(string id);
}