using ClinicToCloudCodingChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicToCloudCodingChallenge.Utilities
{
    public static class Helper
    {
        public static List<Patient> TransformToPatient(List<Database.Models.Patient> patients)
        {
            List<Patient> patientResponses = patients.Select(x => new Patient
            {
                Id = x.Id,
                Email = x.Email,
                DateOfBirth = x.DateOfBirth,
                CreatedAt = x.CreatedAt.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:sszzz"),
                FirstName = x.FirstName,
                Gender = x.Gender,
                IsActive = x.IsActive,
                LastName = x.LastName,
                Phone = x.Phone,
                UpdatedAt = x.UpdatedAt.HasValue ? x.UpdatedAt.Value.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:sszzz") : string.Empty

            }).ToList();
            return patientResponses;
        }
    }
}
