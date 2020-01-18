using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ClinicToCloudCodingChallenge.Models;
using ClinicToCloudCodingChallenge.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicToCloudCodingChallenge.Controllers
{
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }
        [HttpGet("v1/patients")]
        [ProducesResponseType(typeof(PatientResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetPatients()
        {
            var getPatientsTask = _patientService.GetPatients();
            var response = await getPatientsTask;
            if (response == null || response.Patients.Count == 0)
            {
                return NotFound("No records found.");
            }

            return Ok(response);
        }
        [HttpGet("v1/patientsbypage")]
        [ProducesResponseType(typeof(PatientResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetPatients(int page, int pagesize)
        {
            var getPatientsTask = _patientService.GetPatients(page, pagesize);
            var response = await getPatientsTask;
            if (response == null || response.Patients.Count == 0)
            {
                return NotFound("No records found.");
            }

            return Ok(response);
        }
        [HttpPost("v1/patients")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Patient), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> AddPatient(PatientRequest patientRequest)
        {
            var patientExists = await _patientService.CheckIfAlreadyPresent(patientRequest);
            var patientUri = new Uri($"v1/patients", UriKind.Relative);
            if (patientExists != null)
            {
                return Conflict(patientExists);
            }
            var addPatientTask = _patientService.AddPatient(patientRequest);
            var patient = await addPatientTask;            
            return Created(patientUri, patient);
        }
        [HttpPut("v1/patients/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdatePatient([Required(ErrorMessage = "Patient Id is Required")] Guid id,
            [Required] PatientRequest patientRequest)
        {
            var patientExists = await _patientService.CheckIfIdExists(id);
            var patientUri = new Uri($"v1/patients/{id}", UriKind.Relative);
            if (patientExists == false)
            {
                return StatusCode(304); 
            }
            _patientService.UpdatePatient(id, patientRequest);

            return Ok();
        }
    }
}