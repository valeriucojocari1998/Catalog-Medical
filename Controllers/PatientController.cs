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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Patient>>> GetPatients([FromQuery] PatientFilterRequest filter)
    {
        var patients = await _patientService.GetPatients(filter);
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

    [HttpPost]
    public async Task<ActionResult> AddPatient([FromBody] CreatePatientRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _patientService.AddPatient(request);
        return NoContent();
    }

    [HttpPut]
    public async Task<ActionResult> UpdatePatient([FromBody] UpdatePatientRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _patientService.UpdatePatient(request);
        return NoContent();
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
