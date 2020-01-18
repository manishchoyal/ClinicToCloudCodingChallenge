using ClinicToCloudCodingChallenge.Database;
using DatabaseModels = ClinicToCloudCodingChallenge.Database.Models;
using ClinicToCloudCodingChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicToCloudCodingChallenge.Utilities;

namespace ClinicToCloudCodingChallenge.Services
{
    public class PatientService : IPatientService
    {
        private readonly IDatabase _database;
        public PatientService(IDatabase database)
        {
            _database = database;
        }
        public async Task<PatientResponse> GetPatients()
        {
            //var results = await _database.GetPatients();
            //var response = new PatientResponse();
            //List<Patient> patients = Helper.TransformToPatient(results);
            //response.Patients = patients;
            //return response;
            return await _database.GetPatients();
        }
        public async Task<PatientResponseV2> GetPatients(int page, int pagesize)
        {
            return await _database.GetPatients(page, pagesize);            
        }
        public async Task<Patient> AddPatient(PatientRequest patientRequest)
        {
            var  response = await _database.AddPatient(patientRequest);
            //Can do better transformation
            return new Patient
            {
                Id = response.Id,
                Email = response.Email,
                DateOfBirth = response.DateOfBirth,
                CreatedAt = response.CreatedAt.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:sszzz"),
                FirstName = response.FirstName,
                Gender = response.Gender,
                IsActive = response.IsActive,
                LastName = response.LastName,
                Phone = response.Phone,
                UpdatedAt = response.UpdatedAt.HasValue ?
                            response.UpdatedAt.Value.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:sszzz") : string.Empty
            };
        }
        public async Task<Patient> UpdatePatient(Guid id, PatientRequest patientRequest)
        {
            var response = await _database.UpdatePatient(patientRequest, id);
            return new Patient
            {
                Id = response.Id,
                Email = response.Email,
                DateOfBirth = response.DateOfBirth,
                CreatedAt = response.CreatedAt.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:sszzz"),
                FirstName = response.FirstName,
                Gender = response.Gender,
                IsActive = response.IsActive,
                LastName = response.LastName,
                Phone = response.Phone,
                UpdatedAt = response.UpdatedAt.HasValue ?
                            response.UpdatedAt.Value.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:sszzz") : string.Empty
            };
        }

        public async Task<Patient> CheckIfAlreadyPresent(PatientRequest patientRequest)
        {
            var response = await _database.CheckIfAlreadyPresent(patientRequest);
            
            if (response != null)
            {
                return new Patient
                {
                    Id = response.Id,
                    Email = response.Email,
                    DateOfBirth = response.DateOfBirth,
                    CreatedAt = response.CreatedAt.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:sszzz"),
                    FirstName = response.FirstName,
                    Gender = response.Gender,
                    IsActive = response.IsActive,
                    LastName = response.LastName,
                    Phone = response.Phone,
                    UpdatedAt = response.UpdatedAt.HasValue ?
                               response.UpdatedAt.Value.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:sszzz") : string.Empty
                };
            }
            return null;
        }
        public async Task<bool> CheckIfIdExists(Guid id)
        {
            var response = await _database.CheckIfIdExists(id);

            if (response == null)
            {
                return false;
            }
            return true;
        }
    }
}
