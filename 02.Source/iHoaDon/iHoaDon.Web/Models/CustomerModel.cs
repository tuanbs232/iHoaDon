using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using iHoaDon.Resources.Client;
using iHoaDon.Entities;

namespace iHoaDon.Web
{
    public class CustomerModel
    {
        public int Id { get; set; }

        [StringLength(50, ErrorMessageResourceName = "CustomerNameMaxLength", ErrorMessageResourceType = typeof(CustomerModelResource))]
        [LocalizedDisplayName("CustomerName", NameResourceType = typeof(CustomerModelResource))]
        [Required(ErrorMessageResourceName = "CustomerNameRequied", ErrorMessageResourceType = typeof(CustomerModelResource))]
        public string CustomerName { get; set; }


        [StringLength(50, ErrorMessageResourceName = "CompanyNameMaxLength", ErrorMessageResourceType = typeof(CustomerModelResource))]
        [LocalizedDisplayName("CompanyName", NameResourceType = typeof(CustomerModelResource))]
        [Required(ErrorMessageResourceName = "CompanyNameRequied", ErrorMessageResourceType = typeof(CustomerModelResource))]
        public string CompanyName { get; set; }


        [StringLength(13, ErrorMessageResourceName = "CompanyCodeMaxLength", ErrorMessageResourceType = typeof(CustomerModelResource))]
        [LocalizedDisplayName("CompanyCode", NameResourceType = typeof(CustomerModelResource))]
        [Required(ErrorMessageResourceName = "CompanyCodeRequied", ErrorMessageResourceType = typeof(CustomerModelResource))]
        public string CompanyCode { get; set; }


        [StringLength(200, ErrorMessageResourceName = "AddressMaxLength", ErrorMessageResourceType = typeof(CustomerModelResource))]
        [LocalizedDisplayName("Address", NameResourceType = typeof(CustomerModelResource))]
        [Required(ErrorMessageResourceName = "AddressRequied", ErrorMessageResourceType = typeof(CustomerModelResource))]
        public string Address { get; set; }


        [StringLength(20, ErrorMessageResourceName = "BankAccountMaxLength", ErrorMessageResourceType = typeof(CustomerModelResource))]
        [LocalizedDisplayName("BankAccount", NameResourceType = typeof(CustomerModelResource))]
        [Required(ErrorMessageResourceName = "BankAccountRequied", ErrorMessageResourceType = typeof(CustomerModelResource))]
        public string BankAccount { get; set; }


        [StringLength(300, ErrorMessageResourceName = "BankNameMaxLength", ErrorMessageResourceType = typeof(CustomerModelResource))]
        [LocalizedDisplayName("BankName", NameResourceType = typeof(CustomerModelResource))]
        [Required(ErrorMessageResourceName = "BankNameRequied", ErrorMessageResourceType = typeof(CustomerModelResource))]
        public string BankName { get; set; }

        [StringLength(50, ErrorMessageResourceName = "PhoneMaxLength", ErrorMessageResourceType = typeof(CustomerModelResource))]
        [LocalizedDisplayName("Phone", NameResourceType = typeof(CustomerModelResource))]
        public string Phone { get; set; }

        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$", ErrorMessage = "Email không đúng định dạng!")]
        //[StringLength(100, ErrorMessageResourceName = "BuyerEmailMaxLength", ErrorMessageResourceType = typeof(CustomerModelResource))]
        [LocalizedDisplayName("BuyerEmail", NameResourceType = typeof(CustomerModelResource))]
        public string BuyerEmail { get; set; }

    }
}