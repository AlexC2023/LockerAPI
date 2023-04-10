using AutoMapper.Execution;
using LockerAPI.Authentication;
using LockerAPI.DataContext;
using LockerAPI.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Text;


namespace LockerAPI.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly LockerDataContext _context;
        private readonly IConfiguration _configuration;
        public UserService(LockerDataContext context)
        {
            _context = context;
        }

        public UserService(LockerDataContext context, IConfiguration configuration)
        {
            _context = context; _configuration = configuration;
        }
        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _context.Users.SingleOrDefault(x => x.userid == model.username && x.name == model.Password);
            //return null if user not found
            if (user == null) return null;
            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }
        //helper methods
        private string generateJwtToken(MemberAccessException user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AUTHSECRET_AUTHSECRET"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken("https://localhost:7002", "https://localhost:7002", null, expires: DateTime.Now.AddDays(3), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        private string generateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Authentication:Secret")));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration.GetValue<string>("Authentication:Domain"),
                _configuration.GetValue<string>("Authentication:Audience"), null, expires: DateTime.Now.AddDays(3), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
