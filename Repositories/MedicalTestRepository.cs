using Catalog_Medical.Data;
using Catalog_Medical.Interfaces;
using Catalog_Medical.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog_Medical.Repositories;

public class MedicalTestRepository : IMedicalTestRepository
{
    private readonly ApplicationDbContext _context;

    public MedicalTestRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MedicalTest>> GetAllMedicalTestsAsync()
    {
        return await _context.MedicalTests.ToListAsync();
    }

    public async Task<MedicalTest> GetMedicalTestByIdAsync(int id)
    {
        return await _context.MedicalTests.FindAsync(id);
    }

    public async Task<MedicalTest> AddMedicalTestAsync(MedicalTest medicalTest)
    {
        _context.MedicalTests.Add(medicalTest);
        await _context.SaveChangesAsync();
        return medicalTest;
    }

    public async Task<MedicalTest> UpdateMedicalTestAsync(MedicalTest medicalTest)
    {
        _context.Entry(medicalTest).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return medicalTest;
    }

    public async Task DeleteMedicalTestAsync(int id)
    {
        var medicalTest = await _context.MedicalTests.FindAsync(id);
        if (medicalTest != null)
        {
            _context.MedicalTests.Remove(medicalTest);
            await _context.SaveChangesAsync();
        }
    }
}
