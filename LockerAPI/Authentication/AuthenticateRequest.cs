using System.ComponentModel.DataAnnotations;

namespace LockerAPI.Authentication
{
    public class AuthenticateRequest
    {
        [Required]
        public Guid username { get; set; } //userid - tabela User

        [Required]
        public string Password { get; set; } //name -tabela User
    }
}
