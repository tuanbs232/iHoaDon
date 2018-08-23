using System.Web.Mvc;
using iHoaDon.Entities;
using iHoaDon.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iHoaDon.Web.Models
{
    public class InvoiceModel
    {

        public int Id { get; set; }

        public int CustomerId { get; set; }

        //thông tin người mua
        public string CustomerName { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
        public string BuyerEmail { get; set; }
        public string InvoiceNumber { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public PaymentsType PaymentsType { get; set; }

        //tài khoản người bán
        public int BankIdSeller { get; set; }
        public IEnumerable<Banks> BanksSeller { get; set; }
        public string CompanyNameAcc { get; set; }
        public string CompanyCodeAcc { get; set; }
        public string AddressAcc { get; set; }
        public string PhoneAcc { get; set; }

        //tài khoản người mua
        public string BankCodeBuy { get; set; }

        public string BankNameBuy { get; set; }

        public int ResleaseIdNo { get; set; }

        public IEnumerable<SelectListItem> RelesaseNos { get; set; }

        public int Serial { get; set; }

        public IEnumerable<SelectListItem> Serials { get; set; }


        public string Comment { get; set; }

        public int CurrencyId { get; set; }

        public IEnumerable<SelectListItem> Currencies { get; set; }

        public string CurrencyRate { get; set; }

        public int ProductId { get; set; }

        public IEnumerable<Product> Products { get; set; }


        public int UnitId { get; set; }

        public IEnumerable<Unit> Units { get; set; }

    }
}