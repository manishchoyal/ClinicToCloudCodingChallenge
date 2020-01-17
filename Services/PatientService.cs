using ClinicToCloudCodingChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicToCloudCodingChallenge.Services
{
    public class PatientService : IPatientService
    {
        public async Task<List<PatientResponse>> GetPatients()
        {
            return new List<PatientResponse>();
        }
        public async Task<PatientResponse> AddPatient(PatientRequest patientRequest)
        {
            return new PatientResponse();
        }
        public async Task<PatientResponse> UpdatePatient(Guid id, PatientRequest patientRequest)
        {
            return new PatientResponse();
        }
    }
}
