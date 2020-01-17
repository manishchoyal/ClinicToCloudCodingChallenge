using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicToCloudCodingChallenge.Models
{
    public class PatientRequest
    {
        public PatientRequest()
        {
            Page = 1;
            PageSize = 10;            
        }

        [Range(minimum: 1, maximum: int.MaxValue)]
        public int? Page { get; set; }

        [Range(minimum: 10, maximum: 100)]
        
        public int? PageSize { get; set; }

        [Required(ErrorMessage = "First Name is Required")]
        public string First_Name { get; set; }
        [Required(ErrorMessage = "Last Name is Required")]
        public string Last_Name { get; set; }
        [Required(ErrorMessage = "Gender is Required")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Date of Birth is Required")]
        public string Date_Of_Birth { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone Number is Required")]
        public string Phone { get; set; }
        [Display(Name = "Is Active")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "The field must be set to Active.")]
        public bool Is_Active { get; set; }
    }
}
