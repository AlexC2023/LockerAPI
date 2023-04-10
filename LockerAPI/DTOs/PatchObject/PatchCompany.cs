using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LockerAPI.DTOs.PatchObject
{
    public class PatchCompany
    {
        [Key]
        [JsonIgnore]
        public Guid? companyid { get; set; }

        public string? companyname { get; set; }

        public string? companyemail { get; set; }

        public string? companycui { get; set; }

        public DateTime? dateadded { get; set; }
        public bool active { get; set; }
    }
}
