namespace CinemaHour.Web.Areas.Identity.Pages.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using CinemaHour.Common;
    using CinemaHour.Data.Models;
    using CinemaHour.Services.Messaging;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Logging;

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._logger = logger;
            this._emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            // [Display(Name = "Avatar")]
            // [DataType(DataType.Upload)]
            // public byte[] Avatar { get; set; }

            [StringLength(20, MinimumLength = 3, ErrorMessage = "Your first name must be between 3 and 20 characters.")]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [StringLength(20, MinimumLength = 3, ErrorMessage = "Your last name must be between 3 and 20 characters.")]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required]
            [StringLength(20, MinimumLength = 6, ErrorMessage = "Username must be between 6 and 20 characters.")]
            [Display(Name = "Username *")]
            public string UserName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email *")]
            public string Email { get; set; }

            [Required]
            [StringLength(15, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password *")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password *")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.LocalRedirect("/");
            }

            this.ReturnUrl = returnUrl;
            this.ExternalLogins = (await this._signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? this.Url.Content("~/");
            this.ExternalLogins = (await this._signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (this.ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = this.Input.UserName, Email = this.Input.Email, FirstName = this.Input.FirstName, LastName = this.Input.LastName };

                if (this._userManager.Users.Any(x => x.UserName == user.UserName))
                {
                    this.ModelState.AddModelError(string.Empty, "Username already exists.");
                    return this.Page();
                }
                else if (this._userManager.Users.Any(x => x.Email == user.Email))
                {
                    this.ModelState.AddModelError(string.Empty, "Email already exists.");
                    return this.Page();
                }

                var result = await this._userManager.CreateAsync(user, this.Input.Password);

                if (result.Succeeded)
                {
                    this._logger.LogInformation("User created a new account with password.");

                    var code = await this._userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = this.Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: this.Request.Scheme);

                    await this._emailSender.SendEmailAsync("rokudo3@gmail.com", "Cinema Hour", this.Input.Email, "Email Confirmation", callbackUrl);

                    if (this._userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return this.RedirectToPage("RegisterConfirmation", new { email = this.Input.Email });
                    }
                    else
                    {
                        //await this._signInManager.SignInAsync(user, isPersistent: false);
                        return this.LocalRedirect(returnUrl);
                    }
                }

                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return this.Page();
        }
    }
}
