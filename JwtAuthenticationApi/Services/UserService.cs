using JwtAuthenticationApi.Model;
using JwtAuthenticationApi.Model.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace JwtAuthenticationApi.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;
        public UserService(ApplicationDbContext db,IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public UserDetail AddUserDetails(UserVM request) {
            UserDetail user = new UserDetail();
            CreatePasswordHash(request.Password, out Byte[] passwordHash, out Byte[] passwordSalt);
            user.UserName = request.UserName;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.UserAge = request.UserAge;
            user.Email = request.Email;
            user.Address = request.Address;
            user.PhoneNumber = request.PhoneNumber;
            user.RoleName = request.RoleName;
            _db.UserDetails.Add(user);
            _db.SaveChanges();
            return user;
        }

        public string CheckLogin(LoginVM request)
        {
            var result = _db.UserDetails.Where(n => n.UserName == request.UserName).FirstOrDefault();
            
            if(result == null)
            {
                return "No1";
            }
            bool response = VerifyPasswordHash(request.Password, result.PasswordHash, result.PasswordSalt);
            if (!response)
            {
                return "No2";
            }
            string token = CreateToken(result);
            return token;
            
        }

        private void CreatePasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password,byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(UserDetail user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Role, user.RoleName)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

    }
}
