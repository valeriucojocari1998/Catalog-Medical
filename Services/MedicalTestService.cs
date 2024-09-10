using Catalog_Medical.Interfaces;
using Catalog_Medical.Models.Entities;

namespace Catalog_Medical.Services;

public class MedicalTestService : IMedicalTestService
{
    private readonly IMedicalTestRepository _medicalTestRepository;

    public MedicalTestService(IMedicalTestRepository medicalTestRepository)
    {
        _medicalTestRepository = medicalTestRepository;
    }

    public async Task AddMedicalTestAsync(string patientId, IFormFile pdfFile, string testName)
    {
        if (pdfFile == null || pdfFile.Length == 0 || Path.GetExtension(pdfFile.FileName) != ".pdf")
        {
            throw new Exception("Invalid PDF file.");
        }

        using (var memoryStream = new MemoryStream())
        {
            await pdfFile.CopyToAsync(memoryStream);
            var medicalTest = new MedicalTest
            {
                Id = Guid.NewGuid().ToString(),
                PatientId = patientId,
                TestName = testName,
                FileName = pdfFile.FileName,
                Date = DateTime.UtcNow,
                PdfData = memoryStream.ToArray() // Store the PDF data as BLOB
            };

            await _medicalTestRepository.AddMedicalTestAsync(medicalTest);
        }
    }

    public async Task<IEnumerable<MedicalTest>> GetMedicalTestsByPatientIdAsync(string patientId)
    {
        return await _medicalTestRepository.GetMedicalTestsByPatientIdAsync(patientId);
    }

    public async Task<MedicalTest> GetMedicalTestByIdAsync(string testId)
    {
        return await _medicalTestRepository.GetMedicalTestByIdAsync(testId);
    }

    public async Task DeleteMedicalTestAsync(string testId)
    {
        await _medicalTestRepository.DeleteMedicalTestAsync(testId);
    }
}
