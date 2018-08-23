using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iHoaDon.Entities;
using iHoaDon.Resources.Client;

namespace iHoaDon.Web
{
    public class RegisterModel
    {
        public int Id { get; set; }

        [StringLength(30, ErrorMessageResourceName = "CompanyCodeMaxLength", ErrorMessageResourceType = typeof(RegisterModelResource))]
        [LocalizedDisplayName("CompanyCode", NameResourceType = typeof(RegisterModelResource))]
        [Required(ErrorMessageResourceName = "CompanyCodeRequied", ErrorMessageResourceType = typeof(RegisterModelResource))]
        public string CompanyCode { get; set; }


        [StringLength(500, ErrorMessageResourceName = "CompanyNameMaxLength", ErrorMessageResourceType = typeof(RegisterModelResource))]
        [LocalizedDisplayName("CompanyName", NameResourceType = typeof(RegisterModelResource))]
        [Required(ErrorMessageResourceName = "CompanyNameRequied", ErrorMessageResourceType = typeof(RegisterModelResource))]
        public string CompanyName { get; set; }

        [DataType(DataType.Password)]
        [LocalizedDisplayName("NewPassword", NameResourceType = typeof(RegisterModelResource))]
        [StringLength(100, ErrorMessageResourceName = "NewPasswordMinLength", ErrorMessageResourceType = typeof(RegisterModelResource), MinimumLength = 8)]
        [Required(ErrorMessageResourceName = "NewPasswordRequied", ErrorMessageResourceType = typeof(RegisterModelResource))]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [LocalizedDisplayName("ConfirmPassword", NameResourceType = typeof(RegisterModelResource))]
        [Compare("NewPassword", ErrorMessageResourceName = "ConfirmPasswordMustMatch", ErrorMessageResourceType = typeof(RegisterModelResource))]
        public string ConfirmPassword { get; set; }


        [StringLength(500, ErrorMessageResourceName = "AddressMaxLength", ErrorMessageResourceType = typeof(RegisterModelResource))]
        [LocalizedDisplayName("Address", NameResourceType = typeof(RegisterModelResource))]
        [Required(ErrorMessageResourceName = "AddressRequied", ErrorMessageResourceType = typeof(RegisterModelResource))]
        public string Address { get; set; }


        [StringLength(128, ErrorMessageResourceName = "ProvinceMaxLength", ErrorMessageResourceType = typeof(RegisterModelResource))]
        [LocalizedDisplayName("Province", NameResourceType = typeof(RegisterModelResource))]
        [Required(ErrorMessageResourceName = "ProvinceRequied", ErrorMessageResourceType = typeof(RegisterModelResource))]
        public string ProvinceId { get; set; }

        public string Province { get; set; }

        [LocalizedDisplayName("TaxAgencyCode", NameResourceType = typeof(RegisterModelResource))]
        [Required(ErrorMessageResourceName = "TaxAgencyCodeRequied", ErrorMessageResourceType = typeof(RegisterModelResource))]
        public string TaxAgencyCode { get; set; }

        public string TaxAgencyName { get; set; }

        [StringLength(50, ErrorMessageResourceName = "BankAccountMaxLength", ErrorMessageResourceType = typeof(RegisterModelResource))]
        [LocalizedDisplayName("BankAccount", NameResourceType = typeof(RegisterModelResource))]
        [Required(ErrorMessageResourceName = "BankAccountRequied", ErrorMessageResourceType = typeof(RegisterModelResource))]
        public string BankAccount { get; set; }

        [StringLength(50, ErrorMessageResourceName = "RepresentativeMaxLength", ErrorMessageResourceType = typeof(RegisterModelResource))]
        [LocalizedDisplayName("Representative", NameResourceType = typeof(RegisterModelResource))]
        [Required(ErrorMessageResourceName = "RepresentativeRequied", ErrorMessageResourceType = typeof(RegisterModelResource))]
        public string Representative { get; set; }


        //số điện thoại cố đinh
        [StringLength(128, ErrorMessageResourceName = "PhoneMaxLength", ErrorMessageResourceType = typeof(RegisterModelResource))]
        [RegularExpression(@"^(04[0-9]{1}[0-9]{7})|^(024[0-9]{1}[0-9]{7})$", ErrorMessage = "Số điện thoại không đúng định dạng!")]
        [Required(ErrorMessageResourceName = "PhoneRequied", ErrorMessageResourceType = typeof(RegisterModelResource))]
        [LocalizedDisplayName("Phone", NameResourceType = typeof(RegisterModelResource))]
        public string Phone { get; set; }

        //email 
        [StringLength(256, ErrorMessageResourceName = "EmailMaxLength", ErrorMessageResourceType = typeof(RegisterModelResource))]
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$", ErrorMessage = "Email không đúng định dạng!")]
        [LocalizedDisplayName("Email", NameResourceType = typeof(RegisterModelResource))]
        [Required(ErrorMessageResourceName = "EmailRequied", ErrorMessageResourceType = typeof(RegisterModelResource))]
        public string Email { get; set; }

        //thông tin chữ ký sô
        //đơn vị cung cấp chữa ký số
        [LocalizedDisplayName("Issuer", NameResourceType = typeof(RegisterModelResource))]
        public string Issuer { get; set; }

        //tên chữ ký số  
        [LocalizedDisplayName("Subject", NameResourceType = typeof(RegisterModelResource))]
        public string Subject { get; set; }

        //số seria chữ ký số
        [LocalizedDisplayName("Serial", NameResourceType = typeof(RegisterModelResource))]
        public string Serial { get; set; }

        //chữ ký số có hiệu lực từ ngày 
        [LocalizedDisplayName("ValidFrom", NameResourceType = typeof(RegisterModelResource))]
        public string ValidFrom { get; set; }

        //chữ ký số có hiệu lực đến ngày
        [LocalizedDisplayName("ValidTo", NameResourceType = typeof(RegisterModelResource))]
        public string ValidTo { get; set; }

        //[Required(ErrorMessageResourceName = "CaRequied", ErrorMessageResourceType = typeof(RegisterModelResource))]
        public string ca { get; set; }


        public IEnumerable<SelectListItem> ListProvince { get; set; }

        public IEnumerable<SelectListItem> ListTaxAgencyName { get; set; }

    }
}