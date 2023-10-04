using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Web.UI.WebControls;
using TMS.be_together.Code;

namespace TMS.be_together.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Strings), ErrorMessageResourceName = "ErrorMassage_LoginnameRequired")]
        //[Display(ResourceType = typeof(Resources.Strings))]
        //[RegularExpression(@"([\+]?[0-9 \-\/\\]+)|([\w][\w\-\._]*@[\w][\w\-\._]*\.[\w]+)", ErrorMessageResourceType = typeof(Resources.Strings), ErrorMessageResourceName = "ErrorMassage_LoginnameNotValid")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.Strings), ErrorMessageResourceName = "ErrorMassage_LoginnameNotValid", MinimumLength = 8)]
        public string Loginname { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Strings), ErrorMessageResourceName = "ErrorMassage_PasswordRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.Strings), ErrorMessageResourceName = "ErrorMassage_PasswordNotValid", MinimumLength = 6)]
        [DataType(DataType.Password, ErrorMessageResourceType = typeof(Resources.Strings), ErrorMessageResourceName = "ErrorMassage_PasswordNotValid")]
        //[Display(Name = "Password")]
        public string Password { get; set; }

        //[Display(Name = "MaskLink_Contact")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
