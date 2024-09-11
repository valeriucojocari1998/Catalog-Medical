using Catalog_Medical.Interfaces;
using Catalog_Medical.Models.Entities;
using Catalog_Medical.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog_Medical.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpPost("{UserId}/list")]
    public async Task<ActionResult<IEnumerable<Patient>>> GetPatients([FromRoute] string UserId, [FromBody] PatientFilterRequest? filter = null)
    {
        var patients = await _patientService.GetPatients(UserId, filter);
        return Ok(patients);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Patient>> GetPatient([FromRoute] string id)
    {
        var patient = await _patientService.GetPatientById(id);
        if (patient == null)
        {
            return NotFound();
        }
        return Ok(patient);
    }

    [HttpPost("{UserId}/create")]
    public async Task<ActionResult> AddPatient([FromRoute] string UserId, [FromBody] CreatePatientRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var res = await _patientService.AddPatient(UserId, request);
        return Ok(res);
    }

    [HttpPost("{patientId}/edit")]
    public async Task<IActionResult> EditPatient([FromRoute]string patientId, [FromBody] CreatePatientRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _patientService.EditPatient(patientId, request);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePatient([FromRoute] string id)
    {
        var patient = await _patientService.GetPatientById(id);
        if (patient == null)
        {
            return NotFound();
        }

        await _patientService.DeletePatient(id);
        return NoContent();
    }
}
