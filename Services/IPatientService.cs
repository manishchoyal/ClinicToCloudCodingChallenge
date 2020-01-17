using ClinicToCloudCodingChallenge.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicToCloudCodingChallenge.Services
{
    public interface IPatientService
    {
        Task<List<PatientResponse>> GetPatients();
        Task<PatientResponse> AddPatient(PatientRequest patientRequest);
        Task<PatientResponse> UpdatePatient(Guid id, PatientRequest patientRequest);
    }
}