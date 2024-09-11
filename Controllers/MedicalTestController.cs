using Catalog_Medical.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace Catalog_Medical.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class MedicalTestsController : ControllerBase
{
    private readonly IMedicalTestService _medicalTestService;
    private readonly IPatientRepository _patientRepo;


    public MedicalTestsController(IMedicalTestService medicalTestService, IPatientRepository patientRepo)
    {
        _medicalTestService = medicalTestService;
        _patientRepo = patientRepo;
    }

    [HttpPost("{patientId}/add-test")]
    public async Task<IActionResult> AddMedicalTest([FromRoute] string patientId, [FromForm] string title, [FromForm] string description, [FromForm] string date, [FromForm] IFormFile file)
    {
        try
        {
            var mt = await _medicalTestService.AddMedicalTestAsync(patientId, title, description, date, file);
            return Ok(mt);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpGet("{patientId}/tests")]
    public async Task<IActionResult> GetMedicalTests([FromRoute] string patientId)
    {
        var tests = await _medicalTestService.GetMedicalTestsByPatientIdAsync(patientId);
        return Ok(tests);
    }

    [HttpGet("download/{id}")]
    public async Task<IActionResult> DownloadMedicalTestFile([FromRoute] string id)
    {
        var medicalTest = await _medicalTestService.GetMedicalTestByIdAsync(id);
        if (medicalTest == null)
        {
            return NotFound();
        }

        var fileBytes = await System.IO.File.ReadAllBytesAsync(medicalTest.FileUrl);
        return File(fileBytes, "application/pdf", Path.GetFileName(medicalTest.FileUrl));
    }

    [HttpGet("send/{id}")]
    public async Task<IActionResult> SendTestByEmail([FromRoute] string id)
    {
        // Get the medical test details
        var medicalTest = await _medicalTestService.GetMedicalTestByIdAsync(id);

        if (medicalTest == null)
        {
            return NotFound(new { Message = "Medical test not found." });
        }

        var patient = await _patientRepo.GetPatientById(medicalTest.PatientId);

        if (patient == null)
        {
            return NotFound(new { Message = "Patient not found." });

        }

        // Read the file as byte array
        var fileBytes = await System.IO.File.ReadAllBytesAsync(medicalTest.FileUrl);

        if (fileBytes == null || fileBytes.Length == 0)
        {
            return BadRequest(new { Message = "Failed to read medical test file." });
        }

        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587, 
            Credentials = new NetworkCredential("valeriucojocari1998@gmail.com", "valerash123"),
            EnableSsl = true,
        };

        // Create a mail message
        var mailMessage = new MailMessage
        {
            From = new MailAddress("valeriucojocari1998@gmail.com"),
            Subject = $"Medical Test: {medicalTest.Title}",
            Body = $"Please find attached the medical test: {medicalTest.Description}",
            IsBodyHtml = true,
        };

        // Add recipient email
        mailMessage.To.Add(patient.Email);

        // Add the PDF as an attachment
        using (var memoryStream = new MemoryStream(fileBytes))
        {
            var attachment = new Attachment(memoryStream, Path.GetFileName(medicalTest.FileUrl), "application/pdf");
            mailMessage.Attachments.Add(attachment);

            // Send the email
            await smtpClient.SendMailAsync(mailMessage);
        }

        // Return a success message
        return Ok(new { Message = "Medical test sent successfully." });
    }

    [HttpDelete("test/{testId}")]
    public async Task<IActionResult> DeleteMedicalTest([FromRoute] string testId)
    {
        try
        {
            await _medicalTestService.DeleteMedicalTestAsync(testId);
            return Ok(new { Message = "Medical test deleted successfully." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}
