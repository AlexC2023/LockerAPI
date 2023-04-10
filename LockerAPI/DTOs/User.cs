using System.ComponentModel.DataAnnotations;

namespace LockerAPI.DTOs
{
    public class User
    {
        [Key]
        public Guid userid { get; set; }

        public string? name { get; set; }

        public string? surname { get; set; }

        public DateTime? birthdate { get; set; }
        public bool active { get; set; }
    }
}
