

using LockerAPI.Authentication;

namespace LockerAPI.Services.UserService
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
    }
}
