using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Domain.Identity;
using Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        private readonly IAppBLL _bll;

        public AccountController(SignInManager<AppUser> signInManager, IConfiguration configuration,
            UserManager<AppUser> userManager, IAppBLL bll, IEmailSender emailSender)
        {
            _signInManager = signInManager;
            _configuration = configuration;
            _userManager = userManager;
            _bll = bll;
            _emailSender = emailSender;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] LoginDTO model)
        {
            var appUser = await _userManager.FindByEmailAsync(model.Email);

            if (appUser == null)
            {
                // user is not found, return 403
                return StatusCode(403);
            }

            // do not log user in, just check that the password is ok
            var result = await _signInManager.CheckPasswordSignInAsync(appUser, model.Password, false);

            if (!result.Succeeded) return StatusCode(403);

            // create claims based on user 
            var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);

            // get the Json Web Token
            var jwt = JwtHelper.GenerateJwt(
                claims: claimsPrincipal.Claims,
                keyString: _configuration["JWT:Key"],
                issuer: _configuration["JWT:Issuer"],
                expiresInDays: int.Parse(_configuration["JWT:ExpireDays"]));
            return Ok(new {token = jwt});
        }

        [HttpPost]
        public async Task<ActionResult<string>> Register([FromBody] RegisterDTO registerDto)
        {
            if (await _bll.AppUserService. NicknameExistsAsync(registerDto.Nickname)) return BadRequest("Nickname already in user");
            if (await _bll.AppUserService.EmailExistsAsync(registerDto.Email)) return BadRequest("Email already in user");
            
            var user = new AppUser
            {
                UserName = registerDto.Email, 
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                UserNickname = registerDto.Nickname
            };
                
            IdentityResult result;
            //
                
            try
            {
                result = await _userManager.CreateAsync(user, registerDto.Password);
            }
            catch (Exception e)
            {
                return BadRequest("An unknown error occurred");
            }
            //------------------------------------------------------------------------------------------------------

            if (!result.Succeeded) return BadRequest("Registration failed");
            
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = "url";
            /*var callbackUrl = Url.Page( TODO: fix this
                "Areas/Identity/Pages/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = user.Id, code = code },
                protocol: Request.Scheme);*/

            await _emailSender.SendEmailAsync(
                email: registerDto.Email, 
                subject: "Confirm your email",
                htmlMessage: $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            return await Login(new LoginDTO() {Email = registerDto.Email, Password = registerDto.Password});

        }


        public class LoginDTO
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class RegisterDTO
        {
            [Required] 
            [EmailAddress] 
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
                MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [MaxLength(64)]
            [MinLength(1)]
            public string FirstName { get; set; }

            [Required]
            [MaxLength(64)]
            [MinLength(1)]
            public string LastName { get; set; }

            [Required]
            [MaxLength(16)]
            [MinLength(1)]
            public string Nickname { get; set; }
        }
    }
}