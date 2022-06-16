using JwtAuthenticationApi.Model;
using JwtAuthenticationApi.Model.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthenticationApi.Services
{
    public interface IUserService
    {
        public UserDetail AddUserDetails(UserVM request);
        public string CheckLogin(LoginVM request);


    }
}
