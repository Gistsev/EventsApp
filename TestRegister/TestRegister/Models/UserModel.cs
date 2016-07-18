using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TestRegister.App_LocalResources;

namespace TestRegister.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "ERR_Required")]
        [Display(Name = "Email", ResourceType = typeof(GlobalRes))]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "ERR_InvalidEmail", ErrorMessage = null/*bug in mvc4. this is solve it.*/)]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "ERR_Required")]
        [StringLength(100, ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "ERR_PasswordLength", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(GlobalRes))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(GlobalRes))]
        [Compare("Password", ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "ERR_PasswordConfirm")]
        public string ConfirmPassword { get; set; }

        public string PhoneNumber { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string LastName { get; set; }
    }

    public class LoginModel
    {
        [Required(ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "ERR_Required")]
        [Display(Name = "Email", ResourceType = typeof(GlobalRes))]
        [DataType(DataType.EmailAddress)]
        // email validation not required
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "ERR_Required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(GlobalRes))]
        public string Password { get; set; }

        [Display(Name = "RememberMe", ResourceType = typeof(GlobalRes))]
        public bool RememberMe { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required(ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "ERR_Required")]
        [DataType(DataType.Password)]
        [Display(Name = "CurrentPassword", ResourceType = typeof(GlobalRes))]
        public string OldPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "ERR_Required")]
        [StringLength(100, ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "ERR_PasswordLength", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword", ResourceType = typeof(GlobalRes))]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmNewPassword", ResourceType = typeof(GlobalRes))]
        [Compare("NewPassword", ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "ERR_NewPasswordConfirm")]
        public string ConfirmPassword { get; set; }
    }

}