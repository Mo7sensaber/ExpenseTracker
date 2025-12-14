using Domain.Model.IdentityMedule;
using Domain.RepoInterface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbestraction;
using Shared.AuthenticationDTOS;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service
{
    public class AuthenticationService : IAuthenticationsService
    {
        private readonly IUnitOfWork unit;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public AuthenticationService(
            IUnitOfWork unit,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration)
        {
            this.unit = unit;
            this.userManager = userManager;
            this.configuration = configuration;
        }

        public async Task<bool> CheckEmailAsync(string email)
        {
            var check = await userManager.FindByEmailAsync(email);
            return check != null;
        }

        public async Task<ReturnUserDto> GetCurrentUserAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
                throw new Exception("User Not Found");

            return new ReturnUserDto
            {
                DisplayName = user.FullName,
                Email = user.Email,
                token = await GenerateToken(user)
            };
        }

        public async Task<ReturnUserDto> Login(LoginDto login)
        {
            var user = await userManager.FindByEmailAsync(login.Email);

            if (user == null)
                throw new Exception("Email Not Found");

            var validPassword = await userManager.CheckPasswordAsync(user, login.Password);

            if (!validPassword)
                throw new Exception("Invalid Email or Password");

            return new ReturnUserDto
            {
                DisplayName = user.FullName,
                Email = user.Email,
                token = await GenerateToken(user)
            };
        }

        public async Task<ReturnUserDto> Register(RegisterDto register)
        {
            var existingUser = await userManager.FindByEmailAsync(register.Email);

            if (existingUser != null)
                throw new Exception("Email is already in use");

            var user = new ApplicationUser
            {
                UserName = register.UserName,
                FullName = register.UserName,   // ✅ مهم جداً عشان العمود مش يقبل null
                Email = register.Email,
                PhoneNumber = register.phoneNumber
            };

            var result = await userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new Exception(string.Join(" | ", errors));
            }

            return new ReturnUserDto
            {
                DisplayName = user.FullName,
                Email = user.Email,
                token = await GenerateToken(user)
            };
        }

        private async Task<string> GenerateToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FullName)
            };

            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var secretKey = configuration["JWToptions:SecretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["JWToptions:ValidIssuer"],
                audience: configuration["JWToptions:ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
