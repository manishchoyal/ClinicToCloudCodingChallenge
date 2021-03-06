﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicToCloudCodingChallenge.Database.Models
{
    public class Patient
    {
        [System.ComponentModel.DataAnnotations.Key]
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }
        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }
        [JsonProperty(PropertyName = "gender")]
        public string Gender { get; set; }
        [JsonProperty(PropertyName = "date_of_birth")]
        public string DateOfBirth { get; set; }
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }
        [JsonProperty(PropertyName = "is_active")]
        public bool IsActive { get; set; }
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty(PropertyName = "updated_at")] 
        public DateTime? UpdatedAt { get; set; }
    }
}
