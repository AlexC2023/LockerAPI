using LockerAPI.DTOs;
using AutoMapper.Execution;

namespace LockerAPI.Authentication
{
    public class AuthenticateResponse
    {
        public Guid? userid { get; set; }
        public string? name { get; set; }
        public string? surname { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(User user, string token)
        {
            userid = user.userid;
            name = user.name;
            surname = user.surname;
            Token = token;

        }
    }
}
