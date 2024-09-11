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

    public async Task<MedicalTest> GetMedicalTestByIdAsync(string testId)
    {
        return await _context.MedicalTests.FindAsync(testId);
    }

    public async Task<MedicalTest> AddMedicalTestAsync(MedicalTest medicalTest, IFormFile pdfFile)
    {
        var filePath = await SaveFileAsync(pdfFile);
        medicalTest.FileUrl = filePath;
        _context.MedicalTests.Add(medicalTest);
        await _context.SaveChangesAsync();
        return medicalTest;
    }

    public async Task<List<MedicalTest>> GetMedicalTestsByPatientIdAsync(string patientId)
    {
        return await _context.MedicalTests
            .Where(mt => mt.PatientId == patientId)
            .ToListAsync();
    }

    private async Task<string> SaveFileAsync(IFormFile file)
    {
        var folderPath = Path.Combine("MedicalTestFiles");
        Directory.CreateDirectory(folderPath);

        var filePath = Path.Combine(folderPath, $"{Guid.NewGuid()}_{file.FileName}");
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        return filePath;
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
