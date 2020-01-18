using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicToCloudCodingChallenge.Models
{
    public class PatientResponseV2 : PatientResponse
    {
        [JsonProperty(PropertyName = "pagingheader")]
        public PagingHeader PagingHeader { get; set; }
    }

    public class PagingHeader
    {
        [JsonProperty(PropertyName = "totalrecords")]
        public int TotalRecords { get; set; }
        [JsonProperty(PropertyName = "pagesize")]
        public int PageSize { get; set; }
        [JsonProperty(PropertyName = "currentpagenumber")]
        public int PageNumber { get; set; }
        [JsonProperty(PropertyName = "totalpages")]
        public int TotalPages { get; set; }
    }
}
