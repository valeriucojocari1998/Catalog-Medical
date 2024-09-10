using Catalog_Medical.Data;
using Catalog_Medical.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog_Medical.Repositories;

public class MedicalTestRepository : IMedicalTestRepository
{
    private readonly ApplicationDbContext _context;

    public MedicalTestRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddMedicalTestAsync(MedicalTest medicalTest)
    {
        await _context.MedicalTests.AddAsync(medicalTest);
        await _context.SaveChangesAsync();
    }

    public async Task<MedicalTest> GetMedicalTestByIdAsync(string testId)
    {
        return await _context.MedicalTests.FindAsync(testId);
    }

    public async Task<IEnumerable<MedicalTest>> GetMedicalTestsByPatientIdAsync(string patientId)
    {
        return await _context.MedicalTests
            .Where(m => m.PatientId == patientId)
            .ToListAsync();
    }

    public async Task DeleteMedicalTestAsync(string testId)
    {
        var test = await _context.MedicalTests.FindAsync(testId);
        if (test != null)
        {
            _context.MedicalTests.Remove(test);
            await _context.SaveChangesAsync();
        }
    }
}
