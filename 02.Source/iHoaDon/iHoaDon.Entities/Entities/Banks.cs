using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iHoaDon.Entities
{
    public class Banks
    {
        public int Id { get; set; }

        public string BankCode { get; set; }

        public string BankName { get; set; }

        public int AccountId { get; set; }

       // public virtual Account Account { get; set; }
    }
}
