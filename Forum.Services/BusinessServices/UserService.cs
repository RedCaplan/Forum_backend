using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Forum.Core.Model;
using Forum.Services.BusinessServices.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Forum.Services.BusinessServices
{
    public class UserService : IUserService
    {
        #region Fields

        private readonly UserManager<UserAccount> _userManager;
        private readonly SignInManager<UserAccount> _sim;
        private readonly IConfiguration _config;

        #endregion

        #region Constructor

        public UserService(UserManager<UserAccount> um, IConfiguration configuration, SignInManager<UserAccount> sim)
        {
            _userManager = um;
            _config = configuration;
            _sim = sim;
        }

        #endregion

        public async Task<UserAccount> FindByEmailAsync(string email)
        {
            Guard.Against.NullOrEmpty(email, nameof(email));

            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IdentityResult> CreateAsync(UserAccount userAccount, string password)
        {
            Guard.Against.Null(userAccount, nameof(userAccount));
            Guard.Against.NullOrEmpty(password, nameof(password));

            return await _userManager.CreateAsync(userAccount, password);
        }

        public async Task<SignInResult> CheckPasswordSignInAsync(UserAccount userAccount, string password)
        {
            Guard.Against.Null(userAccount, nameof(userAccount));
            Guard.Against.NullOrEmpty(password, nameof(password));

            return await _sim.CheckPasswordSignInAsync(userAccount, password, false);
        }

        public async Task<bool> CheckIfEmailUnique(string email)
        {
            Guard.Against.NullOrEmpty(email, nameof(email));

            return await _userManager.FindByEmailAsync(email) == null;
        }

        public async Task<bool> CanSignInAsync(UserAccount userAccount)
        {
            Guard.Against.Null(userAccount, nameof(userAccount));

            return await _sim.CanSignInAsync(userAccount);
        }

        public string GetToken(UserAccount userAccount)
        {
            Guard.Against.Null(userAccount, nameof(userAccount));

            // Create the token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userAccount.Email),
                new Claim(JwtRegisteredClaimNames.UniqueName, userAccount.UserName),
                new Claim("IsModerator", userAccount.IsModerator.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                null, null,
                claims,
                expires: DateTime.Now.AddHours(4),
                signingCredentials: creds);
            JwtSecurityTokenHandler.DefaultMapInboundClaims = true;

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
