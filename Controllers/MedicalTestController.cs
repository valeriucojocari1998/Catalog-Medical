using Catalog_Medical.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog_Medical.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class MedicalTestsController : ControllerBase
{
    private readonly IMedicalTestService _medicalTestService;

    public MedicalTestsController(IMedicalTestService medicalTestService)
    {
        _medicalTestService = medicalTestService;
    }

    // Endpoint to upload a PDF for a specific patient
    [HttpPost("{patientId}/add-test")]
    public async Task<IActionResult> AddMedicalTest([FromRoute] string patientId, [FromForm] IFormFile pdfFile, [FromForm] string testName)
    {
        try
        {
            await _medicalTestService.AddMedicalTestAsync(patientId, pdfFile, testName);
            return Ok(new { Message = "Medical test uploaded successfully." });
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

    [HttpGet("test/{testId}")]
    public async Task<IActionResult> GetMedicalTest([FromRoute] string testId)
    {
        var test = await _medicalTestService.GetMedicalTestByIdAsync(testId);
        if (test == null)
        {
            return NotFound();
        }

        return File(test.PdfData, "application/pdf", test.FileName);
    }

    [HttpGet("test/{testId}/display")]
    public async Task<IActionResult> DisplayMedicalTestInIframe([FromRoute] string testId)
    {
        var test = await _medicalTestService.GetMedicalTestByIdAsync(testId);
        if (test == null)
        {
            return NotFound();
        }

        return new FileContentResult(test.PdfData, "application/pdf");
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
