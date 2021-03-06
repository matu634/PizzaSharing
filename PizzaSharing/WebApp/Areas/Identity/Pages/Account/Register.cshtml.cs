using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Authorization;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IAppUnitOfWork _uow;

        public RegisterModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender, 
            IAppUnitOfWork uow)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _uow = uow;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessageResourceName = "MissingEmailError", ErrorMessageResourceType = typeof(Resources.Account.Register))]
            [EmailAddress]
            public string Email { get; set; }

            [Required(ErrorMessageResourceName = "MissingPasswordError", ErrorMessageResourceType = typeof(Resources.Account.Register))]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required(ErrorMessageResourceName = "MissingConfirmPasswordEmail", ErrorMessageResourceType = typeof(Resources.Account.Register))]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessageResourceName = "PasswordsNotEqualError", ErrorMessageResourceType = typeof(Resources.Account.Register))]
            public string ConfirmPassword { get; set; }
            
            [Required(ErrorMessageResourceName = "MissingFirstNameError", ErrorMessageResourceType = typeof(Resources.Account.Register))]
            [MaxLength(64, ErrorMessageResourceName = "TooLongFirstNameError", ErrorMessageResourceType = typeof(Resources.Account.Register))]
            [MinLength(1, ErrorMessageResourceName = "TooShortFirstNameError", ErrorMessageResourceType = typeof(Resources.Account.Register))]
            public string FirstName { get; set; }
            
            [Required(ErrorMessageResourceName = "MissingLastNameError", ErrorMessageResourceType = typeof(Resources.Account.Register))]
            [MaxLength(64, ErrorMessageResourceName = "TooLongLastNameError", ErrorMessageResourceType = typeof(Resources.Account.Register))]
            [MinLength(1, ErrorMessageResourceName = "TooShortLastNameError", ErrorMessageResourceType = typeof(Resources.Account.Register))]
            public string LastName { get; set; }
            
            [Required(ErrorMessageResourceName = "MissingNicknameError", ErrorMessageResourceType = typeof(Resources.Account.Register))]
            [MaxLength(16, ErrorMessageResourceName = "TooLongNicknameError", ErrorMessageResourceType = typeof(Resources.Account.Register))]
            [MinLength(1, ErrorMessageResourceName = "TooShortNicknameError", ErrorMessageResourceType = typeof(Resources.Account.Register))]
            public string Nickname { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                if (await _uow.AppUsers.NicknameExistsAsync(userName: Input.Nickname))
                {
                    ModelState.AddModelError("NicknameExists", Resources.Account.Register.NicknameAlreadyExists);
                    return Page();
                }

                if (await _uow.AppUsers.EmailExistsAsync(email: Input.Email))
                {
                    ModelState.AddModelError("EmailExists", Resources.Account.Register.EmailAlreadyExists);
                    return Page();
                }
                
                var user = new AppUser
                {
                    UserName = Input.Email, 
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    UserNickname = Input.Nickname
                };
                
                IdentityResult result;
                //
                
                try
                {
                    result = await _userManager.CreateAsync(user, Input.Password);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Error", "An unknown error occurred");
                    return Page();
                }
                //------------------------------------------------------------------------------------------------------
                
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
