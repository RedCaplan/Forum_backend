using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using news_forum.DTO;
using news_forum.Model;

namespace news_forum.Controllers
{
    /// <summary>
    /// Api to register and login user
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region Fields
        public UserManager<UserAccount> UM { get; set; }
        private readonly SignInManager<UserAccount> _sim;
        private readonly IConfiguration _config;
        #endregion

        #region Constructor
        public UsersController(UserManager<UserAccount> um
            , IConfiguration configuration, SignInManager<UserAccount> sim)
        {
            UM = um;
            _config = configuration;
            _sim = sim;
        }
        #endregion

        #region Api methods

        /// <summary>
        /// Login using a dto and returns a token that's passed to the front end for authentication
        /// </summary>
        /// <param name="loginDTO">The dto containing the username and the password used for logging in</param>
        /// <returns>A cookie used to identify the user.</returns>
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(LoginDTO loginDTO)
        {
            var user = await UM.FindByEmailAsync(loginDTO.Email);

            if (user != null)
            {
                bool canSignIn = await _sim.CanSignInAsync(user);

                if (canSignIn)
                {

                    var res = await _sim.CheckPasswordSignInAsync(user, loginDTO.Password, false);
                    if (res.Succeeded)
                    {
                        string token = GetToken(user);
                        return Created("", token);
                    }
                    else
                        ModelState.AddModelError("Error", "The password is incorrect");
                }
                else
                    ModelState.AddModelError("Error", "This user can't sign in");

            }
            else
                ModelState.AddModelError("Error", "We couldn't find the user with that e-mail");

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Register the user using a dto containing all the details.Also returns a token that's used to identify the user
        /// </summary>
        /// <param name="registerDTO">A dto containing all the information needed to identify a user.</param>
        /// <returns>A cookie that's passed to the frontend to identify the user.</returns>
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult<string>> Register(RegisterDTO registerDTO)
        {
            if (UM.FindByEmailAsync(registerDTO.Email).Result != null)
                ModelState.AddModelError("Error", "Email is not unique");
            else
            {
                UserAccount ua = new UserAccount() { UserName = registerDTO.Username, Email = registerDTO.Email };
                var res = await UM.CreateAsync(ua, registerDTO.Password);

                if (res.Succeeded)
                {
                    string token = GetToken(ua);
                    return Created("", token);
                }
                else
                {
                    ModelState.AddModelError("Error", "Something went wrong in the registration process");
                }
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Check if the email is unique used for validation in the frontend
        /// </summary>
        /// <param name="mail">The email we wish to check</param>
        /// <returns>Returns true if the mail doesn't exist otherwise fals
        /// </returns>
        [AllowAnonymous]
        [HttpGet("EmailUnique")]
        public async Task<ActionResult<bool>> CheckIfEmailUnique(string mail)
        {
            return await UM.FindByEmailAsync(mail) == null;
        }

        #endregion

        #region Private methods
        private UserAccount GetUser()
        {
            return UM.FindByNameAsync(User.Identity.Name).Result;
        }
        #endregion

        #region Method to generate the cookie
        private String GetToken(UserAccount user)
        {
            // Create the token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
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
        #endregion
    }
}
