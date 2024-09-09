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

    public async Task<IEnumerable<Patient>> GetPatients(PatientFilterRequest filter = null)
    {
        var query = _context.Patients.AsQueryable();

        if (!string.IsNullOrEmpty(filter?.Name))
        {
            query = query.Where(p => p.Name.Contains(filter.Name));
        }

        if (!string.IsNullOrEmpty(filter?.Gender))
        {
            query = query.Where(p => p.Gender == filter.Gender);
        }

        if (!string.IsNullOrEmpty(filter?.BloodType))
        {
            query = query.Where(p => p.BloodType == filter.BloodType);
        }

        if (!string.IsNullOrEmpty(filter?.Email))
        {
            query = query.Where(p => p.Email.Contains(filter.Email));
        }

        if (!string.IsNullOrEmpty(filter?.PhoneNumber))
        {
            query = query.Where(p => p.PhoneNumber.Contains(filter.PhoneNumber));
        }

        if (filter?.DateOfBirthFrom.HasValue == true)
        {
            query = query.Where(p => p.DateOfBirth >= filter.DateOfBirthFrom.Value);
        }

        if (filter?.DateOfBirthTo.HasValue == true)
        {
            query = query.Where(p => p.DateOfBirth <= filter.DateOfBirthTo.Value);
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