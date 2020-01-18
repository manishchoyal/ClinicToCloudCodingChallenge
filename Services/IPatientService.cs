using ClinicToCloudCodingChallenge.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicToCloudCodingChallenge.Services
{
    public interface IPatientService
    {
        Task<PatientResponse> GetPatients();
        Task<Patient> AddPatient(PatientRequest patientRequest);
        Task<Patient> UpdatePatient(Guid id, PatientRequest patientRequest);
        Task<Patient> CheckIfAlreadyPresent(PatientRequest patientRequest);
        Task<bool> CheckIfIdExists(Guid id);
        Task<PatientResponseV2> GetPatients(int page, int pagesize);
    }
}