using System.ComponentModel.DataAnnotations;
namespace LockerAPI.DTOs
{
    public class Company
    {
        [Key]
        public Guid? companyid { get; set; }

        public string? companyname { get; set; }

        public string? companyemail { get; set; }

        public string? companycui { get; set; }

        public DateTime? dateadded { get; set; }
        public bool active { get; set; }
    }
}
