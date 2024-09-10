using Catalog_Medical.Data;
using Catalog_Medical.Interfaces;
using Catalog_Medical.Models.Entities;
using Catalog_Medical.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace Catalog_Medical.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly ApplicationDbContext _context;

    public PatientRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Patient>> GetPatients(string UserId, PatientFilterRequest filter = null)
    {
        var query = _context.Patients.AsQueryable();

        query = query.Where(x => x.DoctorId == UserId || x.DoctorId == "6efb1a29-ac71-4f97-be3a-4532e61aaec1");

        if (!string.IsNullOrEmpty(filter?.Name))
        {
            query = query.Where(p => p.Name.Contains(filter.Name));
        }

        if (!string.IsNullOrEmpty(filter?.Gender))
        {
            query = query.Where(p => p.Gender == filter.Gender);
        }

        if (!string.IsNullOrEmpty(filter?.Email))
        {
            query = query.Where(p => p.Email.Contains(filter.Email));
        }

        if (!string.IsNullOrEmpty(filter?.PhoneNumber))
        {
            query = query.Where(p => p.PhoneNumber.Contains(filter.PhoneNumber));
        }

        return await query.ToListAsync();
    }

    public async Task<Patient> GetPatientById(string id)
    {
        return await _context.Patients.FindAsync(id);
    }

    public async Task AddPatient(Patient patient)
    {
        await _context.Patients.AddAsync(patient);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePatient(Patient patient)
    {
        _context.Patients.Update(patient);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePatient(string id)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient != null)
        {
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
        }
    }
}