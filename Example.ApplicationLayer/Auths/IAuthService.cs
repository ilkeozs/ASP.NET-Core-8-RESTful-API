using Example.ViewModel;

namespace Example.ApplicationLayer.Auths;

public interface IAuthService
{
    Task<string> AuthenticateAsync(LoginRequest loginRequest);
}