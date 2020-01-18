using Newtonsoft.Json;
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
        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is Required")]
        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Gender is Required")]
        [JsonProperty(PropertyName = "gender")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Date of Birth is Required")]
        [JsonProperty(PropertyName = "date_of_birth")]
        public string DateOfBirth { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone Number is Required")]
        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }
        [Display(Name = "Is Active")]
        [JsonProperty(PropertyName = "is_active")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "The field must be set to Active.")]
        public bool IsActive { get; set; }
    }
}
