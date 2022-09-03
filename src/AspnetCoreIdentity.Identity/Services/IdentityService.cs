using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using AspnetCoreIdentity.Identity.Configurations;
using AspnetCoreIdentity.Identity.Interfaces;
using AspnetCoreIdentity.Identity.DTOs.Request;
using AspnetCoreIdentity.Identity.DTOs.Response;

namespace AspnetCoreIdentity.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtOptions _jwtOptions;

        public IdentityService(SignInManager<IdentityUser> signInManager,
                               UserManager<IdentityUser> userManager,
                               IOptions<JwtOptions> jwtOptions)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<CreateUserResponseDTO> CreateUserAsync(CreateUserRequestDTO createUserDTO)
        {
            var identityUser = new IdentityUser
            {
                UserName = createUserDTO.Email,
                Email = createUserDTO.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(identityUser, createUserDTO.Pwd);
            if (result.Succeeded)
                await _userManager.SetLockoutEnabledAsync(identityUser, false);

            var createUserResponse = new CreateUserResponseDTO(result.Succeeded);
            if (result.Errors.Any())
                createUserResponse.AddErrors(result.Errors.Select(r => r.Description));

            return createUserResponse;
        }

        public async Task<UserLoginReponseDTO> Login(UserLoginRequestDTO userLoginDTO)
        {
            var result = await _signInManager.PasswordSignInAsync(userLoginDTO.Email, userLoginDTO.Pwd, false, true);
            if (result.Succeeded)
                return await GenerateCredetials(userLoginDTO.Email);

            var userLoginResponse = new UserLoginReponseDTO();
            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                    userLoginResponse.AddError("This account is blocked");
                else if (result.IsNotAllowed)
                    userLoginResponse.AddError("This account does not have permission to login");
                else if (result.RequiresTwoFactor)
                    userLoginResponse.AddError("You need to confirm login in your second factor of authentication");
                else
                    userLoginResponse.AddError("Username or password are incorrect");
            }

            return userLoginResponse;
        }

        public async Task<UserLoginReponseDTO> LoginWithoutPwd(string userId)
        {
            var userLoginResponse = new UserLoginReponseDTO();
            var usuario = await _userManager.FindByIdAsync(userId);

            if (await _userManager.IsLockedOutAsync(usuario))
                userLoginResponse.AddError("This account is blocked");
            else if (!await _userManager.IsEmailConfirmedAsync(usuario))
                userLoginResponse.AddError("This account needs to confirm your email before logging in");

            if (userLoginResponse.Success)
                return await GenerateCredetials(usuario.Email);

            return userLoginResponse;
        }

        private async Task<UserLoginReponseDTO> GenerateCredetials(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var accessTokenClaims = await GetClaimsAndRoles(user, addUserClaims: true);
            var refreshTokenClaims = await GetClaimsAndRoles(user, addUserClaims: false);

            var expirationDateAccessToken = DateTime.Now.AddSeconds(_jwtOptions.AccessTokenExpiration);
            var expirationDateRefreshToken = DateTime.Now.AddSeconds(_jwtOptions.RefreshTokenExpiration);

            var accessToken = GenerateToken(accessTokenClaims, expirationDateAccessToken);
            var refreshToken = GenerateToken(refreshTokenClaims, expirationDateRefreshToken);

            return new UserLoginReponseDTO
            (
                success: true,
                accessToken: accessToken,
                refreshToken: refreshToken
            );
        }

        private string GenerateToken(IEnumerable<Claim> claims, DateTime dataExpiracao)
        {
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: dataExpiracao,
                signingCredentials: _jwtOptions.SigningCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private async Task<IList<Claim>> GetClaimsAndRoles(IdentityUser user, bool addUserClaims)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()));

            if (addUserClaims)
            {
                var userClaims = await _userManager.GetClaimsAsync(user);
                var roles = await _userManager.GetRolesAsync(user);
                
                claims.AddRange(userClaims);

                foreach (var role in roles)
                    claims.Add(new Claim("role", role));
            }

            return claims;
        }
    }
}
