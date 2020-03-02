using System.Threading.Tasks;
using AutoMapper;
using Forum.Core.Model;
using Forum.Services.BusinessServices.Interfaces;
using Forum.Web.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Controllers
{
    /// <summary>
    /// Api to register and login user
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region Fields

        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor takes two parameters and is handled automatically by ASP.NET
        /// </summary>
        /// <param name="userService">Constructor injection done by Services Providers</param>
        /// <param name="mapper">Constructor injection done by Services Providers</param>
        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
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
            UserAccount userAccount = await _userService.FindByEmailAsync(loginDTO.Email);

            if (userAccount != null)
            {
                bool canSignIn = await _userService.CanSignInAsync(userAccount);

                if (canSignIn)
                {

                    var res = await _userService.CheckPasswordSignInAsync(userAccount, loginDTO.Password);
                    if (res.Succeeded)
                    {
                        string token = _userService.GetToken(userAccount);

                        return Created("", token);
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "The password is incorrect");
                    }
                }
                else
                {
                    ModelState.AddModelError("Error", "This user can't sign in");
                }

            }
            else
            {
                ModelState.AddModelError("Error", "We couldn't find the user with that e-mail");
            }

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
            if (await _userService.CheckIfEmailUnique(registerDTO.Email) == false)
            {
                ModelState.AddModelError("Error", "Email is not unique");
            }
            else
            {

                UserAccount userAccount = _mapper.Map<RegisterDTO, UserAccount>(registerDTO);
                var res = await _userService.CreateAsync(userAccount, registerDTO.Password);

                if (res.Succeeded)
                {
                    string token = _userService.GetToken(userAccount);

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
        /// <returns>Returns true if the mail doesn't exist otherwise false
        /// </returns>
        [AllowAnonymous]
        [HttpGet("EmailUnique")]
        public async Task<ActionResult<bool>> CheckIfEmailUnique(string mail)
        {
            return await _userService.CheckIfEmailUnique(mail);
        }

        #endregion
    }
}
