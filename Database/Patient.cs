using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicToCloudCodingChallenge.Database.Models
{
    public class Patient
    {
        public int? Page { get; set; }

     
        public int? PageSize { get; set; }
        [System.ComponentModel.DataAnnotations.Key]
        public Guid Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Gender { get; set; }
        public string Date_Of_Birth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Is_Active { get; set; }
    }
}
