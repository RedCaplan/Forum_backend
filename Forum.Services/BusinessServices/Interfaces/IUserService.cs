using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Forum.Core.Model;
using Microsoft.AspNetCore.Identity;

namespace Forum.Services.BusinessServices.Interfaces
{
    public interface IUserService
    {
        Task<UserAccount> FindByEmailAsync(string email);
        Task<IdentityResult> CreateAsync(UserAccount userAccount, string password);
        Task<SignInResult> CheckPasswordSignInAsync(UserAccount userAccount, string password);
        Task<bool> CheckIfEmailUnique(string email);
        Task<bool> CanSignInAsync(UserAccount userAccount);
        string GetToken(UserAccount userAccount);
    }
}
