using DatabaseModels = ClinicToCloudCodingChallenge.Database.Models;
using ClinicToCloudCodingChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicToCloudCodingChallenge.Utilities;

namespace ClinicToCloudCodingChallenge.Database
{
    public class Database : IDatabase
    {
        readonly ApiContext _apiContext;
        public Database(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        public async Task<PatientResponse> GetPatients()
        {
            var results = await Task<List<Patient>>.Run(() => _apiContext.Patients.ToList());
            var response = new PatientResponse();
            List<Patient> patients = Helper.TransformToPatient(results);
            response.Patients = patients;
            return response;
        }
        //This can also be done with SQL server Stored Procedure
        public async Task<PatientResponseV2> GetPatients(int page, int pagesize)
        {
            var totalRecords = _apiContext.Patients.Count();

            var pageCount = (double)totalRecords / pagesize;
            var skip = (page - 1) * pagesize;

            var results = await Task<List<DatabaseModels.Patient>>.Run(() => _apiContext.Patients.Skip(skip).Take(pagesize).ToList());
            var response = new PatientResponseV2();
            List<Patient> patients = Helper.TransformToPatient(results);
            response.Patients = patients;
            response.PagingHeader = new PagingHeader
            {
                TotalRecords = totalRecords,
                TotalPages = (int)pageCount,
                PageNumber = page,
                PageSize = pagesize

            };
            return response;
        }

        public async Task<DatabaseModels.Patient> AddPatient(PatientRequest patientRequest)
        {
            var request = new DatabaseModels.Patient
            {
                Id = new Guid(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Email = patientRequest.Email,
                DateOfBirth = patientRequest.DateOfBirth,
                FirstName = patientRequest.FirstName,
                Gender = patientRequest.Gender,
                IsActive = patientRequest.IsActive,
                LastName = patientRequest.LastName,
                Phone = patientRequest.Phone
            };

            _apiContext.Patients.Add(request);

            var response = await _apiContext.SaveChangesAsync();
            return request;
        }
        public async Task<DatabaseModels.Patient> UpdatePatient(PatientRequest patientRequest, Guid id)
        {
            var request = new DatabaseModels.Patient
            {
                Id = id,
                UpdatedAt = DateTime.Now,
                Email = patientRequest.Email,
                DateOfBirth = patientRequest.DateOfBirth,
                FirstName = patientRequest.FirstName,
                Gender = patientRequest.Gender,
                IsActive = patientRequest.IsActive,
                LastName = patientRequest.LastName,
                Phone = patientRequest.Phone
            };
            var entry = _apiContext.Patients.First(e => e.Id == request.Id);
            _apiContext.Entry(entry).CurrentValues.SetValues(request);

            var response = await _apiContext.SaveChangesAsync();
            return request;
        }
        public async Task<DatabaseModels.Patient> CheckIfAlreadyPresent(PatientRequest patientRequest)
        {
            var response = await Task<DatabaseModels.Patient>.Run(() =>
                                           _apiContext.Patients.Where(a => a.FirstName == patientRequest.FirstName
                                               && a.LastName == patientRequest.LastName
                                               && a.DateOfBirth == patientRequest.DateOfBirth).FirstOrDefault());
            return response;
        }
        public async Task<DatabaseModels.Patient> CheckIfIdExists(Guid id)
        {
            var response = await Task<DatabaseModels.Patient>.Run(() =>
                                           _apiContext.Patients.Where(a => a.Id == id).FirstOrDefault());
            return response;
        }
    }
}
