using DatabaseModels = ClinicToCloudCodingChallenge.Database.Models;
using ClinicToCloudCodingChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicToCloudCodingChallenge.Database
{
    public class Database : IDatabase
    {
        readonly ApiContext _apiContext;
        public Database(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        public async Task<List<DatabaseModels.Patient>> GetPatients()
        {
            return await Task<List<Patient>>.Run(() => _apiContext.Patients.ToList());
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

            var response =  await _apiContext.SaveChangesAsync();
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
            try
            {
                //_apiContext.Entry(request).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                //_apiContext.Entry(request).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                //_apiContext.Patients.

                var entry = _apiContext.Patients.First(e => e.Id == request.Id);
                _apiContext.Entry(entry).CurrentValues.SetValues(request);
                
                //_apiContext.SaveChanges();
                var response = await _apiContext.SaveChangesAsync();
                return request;

            }
            catch(Exception ex)
            {
                throw ex;
            }

            
        }
        public async Task<DatabaseModels.Patient> CheckIfAlreadyPresent(PatientRequest patientRequest)
        {            
            var response = await Task< DatabaseModels.Patient>.Run(() => 
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
