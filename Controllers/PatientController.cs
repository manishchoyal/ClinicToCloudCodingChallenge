using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
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
            var allPatients = await getPatientsTask;
            if( allPatients == null || allPatients.Count == 0)
            {
                return NotFound("No records found.");
            }

            return Ok(allPatients);
        }
        [HttpPost("v1/patients")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddPatient(PatientRequest patientRequest)
        {
            var addPatientTask = _patientService.AddPatient(patientRequest);
            var patient = await addPatientTask;

            return Ok(patient);
        }
        [HttpPut("v1/patients/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdatePatient([Required(ErrorMessage = "Patient Id is Required")] Guid id, 
            PatientRequest patientRequest)
        {
            var updatePatientTask = _patientService.UpdatePatient(id, patientRequest);
            var patient = await updatePatientTask;

            return Ok(patient);
        }
    }
}