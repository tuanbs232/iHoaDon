using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iHoaDon.Entities
{
   public class Currency
    {
       public int Id { get; set; }

       public string CurrencyName { get; set; }

       public double CurrencyRate { get; set; }

       public DateTime DateRating { get; set; }

       public string BankName { get; set; }

       public int? AccountId { get; set; }

       public virtual Account Account { get; set; }

       public string CurrencyCode { get; set; }
           
    }
}
