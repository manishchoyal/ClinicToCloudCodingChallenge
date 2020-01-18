using DatabaseModels = ClinicToCloudCodingChallenge.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicToCloudCodingChallenge.Models;
using System;

namespace ClinicToCloudCodingChallenge.Database
{
    public interface IDatabase
    {
        Task<PatientResponse> GetPatients();
        Task<PatientResponseV2> GetPatients(int page, int pagesize);
        Task<DatabaseModels.Patient> AddPatient(PatientRequest patientRequest);
        Task<DatabaseModels.Patient> CheckIfAlreadyPresent(PatientRequest patientRequest);
        Task<DatabaseModels.Patient> CheckIfIdExists(Guid id);
        Task<DatabaseModels.Patient> UpdatePatient(PatientRequest patientRequest, Guid id);
    }
}