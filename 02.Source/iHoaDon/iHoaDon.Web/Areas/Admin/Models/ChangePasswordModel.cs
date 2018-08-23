using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using iHoaDon.Entities;
using iHoaDon.Resources;
using iHoaDon.Resources.Admin;

namespace iHoaDon.Web.Models
{
    public class ChangePasswordModel
    {
        [DataType(DataType.Password)]
        [LocalizedDisplayName("OldPassword", NameResourceType = typeof(ChangePasswordModelResource))]
        [Required(ErrorMessageResourceName = "OldPasswordRequied", ErrorMessageResourceType = typeof(ChangePasswordModelResource))]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [LocalizedDisplayName("NewPassword", NameResourceType = typeof(ChangePasswordModelResource))]
        [StringLength(100, ErrorMessageResourceName = "NewPasswordMinLength", ErrorMessageResourceType = typeof(ChangePasswordModelResource), MinimumLength = 8)]
        [Required(ErrorMessageResourceName = "NewPasswordRequied", ErrorMessageResourceType = typeof(ChangePasswordModelResource))]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [LocalizedDisplayName("ConfirmPassword", NameResourceType = typeof(ChangePasswordModelResource))]
        [Compare("NewPassword", ErrorMessageResourceName = "ConfirmPasswordMustMatch", ErrorMessageResourceType = typeof(ChangePasswordModelResource))]
        public string ConfirmPassword { get; set; }
    }
}