using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicToCloudCodingChallenge.Models
{
    public class PatientResponse
    {
        public Guid? Id { get; set; }
        public string First_Name{ get; set; }
        public string Last_Name { get; set; }
        public string Gender { get; set; }
        public string Date_Of_Birth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Is_Active { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
    }
}
