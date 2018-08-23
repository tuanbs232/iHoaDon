using System.ComponentModel.DataAnnotations;
using iHoaDon.Entities;
using iHoaDon.Resources.Client;


namespace iHoaDon.Web
{
    public class LogOnUseModel
    {
        [Required(ErrorMessageResourceName = "CompanyCodeRequied", ErrorMessageResourceType = typeof(LogOnModelResource))]
        [LocalizedDisplayName("CompanyCode", NameResourceType = typeof(LogOnModelResource))]
        public string CompanyCode { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceName = "PasswordRequied", ErrorMessageResourceType = typeof(LogOnModelResource))]
        [LocalizedDisplayName("Password", NameResourceType = typeof(LogOnModelResource))]
        [StringLength(100, ErrorMessageResourceName = "PasswordMinimumLength", ErrorMessageResourceType = typeof(LogOnModelResource), MinimumLength = 8)]
        public string Password { get; set; }

        [LocalizedDisplayName("RememberMe", NameResourceType = typeof(LogOnModelResource))]
        public bool RememberMe { get; set; }
    }
}