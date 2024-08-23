using Catalog_Medical.Models.Entities;
using Catalog_Medical.Models.Requests;
using Catalog_Medical.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog_Medical.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class MedicalTestController : ControllerBase
{
    private readonly MedicalTestService _medicalTestService;
    private readonly NotificationService _notificationService;

    public MedicalTestController(MedicalTestService medicalTestService, NotificationService notificationService)
    {
        _medicalTestService = medicalTestService;
        _notificationService = notificationService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MedicalTest>>> GetAllMedicalTests()
    {
        return Ok(await _medicalTestService.GetAllMedicalTestsAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MedicalTest>> GetMedicalTestById(int id)
    {
        var medicalTest = await _medicalTestService.GetMedicalTestByIdAsync(id);
        if (medicalTest == null)
        {
            return NotFound();
        }
        return Ok(medicalTest);
    }

    [HttpPost]
    public async Task<ActionResult<MedicalTest>> AddMedicalTest(CreateMedicalTestRequest request)
    {
        var medicalTest = new MedicalTest
        {
            TestName = request.TestName,
            TestDate = request.TestDate,
            Results = request.Results,
            PatientId = request.PatientId
        };

        var createdMedicalTest = await _medicalTestService.AddMedicalTestAsync(medicalTest);

        // Notify all clients about the new medical test
        await _notificationService.SendNotificationAsync($"New medical test '{createdMedicalTest.TestName}' added for patient ID {createdMedicalTest.PatientId}");

        return CreatedAtAction(nameof(GetMedicalTestById), new { id = createdMedicalTest.Id }, createdMedicalTest);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMedicalTest(int id, UpdateMedicalTestRequest request)
    {
        if (id != request.Id)
        {
            return BadRequest();
        }

        var medicalTest = new MedicalTest
        {
            Id = request.Id,
            TestName = request.TestName,
            TestDate = request.TestDate,
            Results = request.Results,
            PatientId = request.PatientId
        };

        await _medicalTestService.UpdateMedicalTestAsync(medicalTest);

        // Notify all clients about the updated medical test
        await _notificationService.SendNotificationAsync($"Medical test '{medicalTest.TestName}' updated for patient ID {medicalTest.PatientId}");

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMedicalTest(int id)
    {
        await _medicalTestService.DeleteMedicalTestAsync(id);

        // Notify all clients about the deleted medical test
        await _notificationService.SendNotificationAsync($"Medical test with ID {id} was deleted.");

        return NoContent();
    }
}
