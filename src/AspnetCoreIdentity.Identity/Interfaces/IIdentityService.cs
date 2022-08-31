using AspnetCoreIdentity.Identity.DTOs;

namespace AspnetCoreIdentity.Identity.Interfaces
{
    public interface IIdentityService
    {
        Task<CreateUserResponseDTO> CreateUserAsync(CreateUserRequestDTO createUserRequestDTO);
        Task<UserLoginReponseDTO> Login(UserLoginRequestDTO userLoginRequest);
        Task<UserLoginReponseDTO> LoginWithoutPwd(string userId);
    }
}
