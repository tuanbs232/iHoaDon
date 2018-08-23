using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace iHoaDon.Entities.Enum
{

    public enum PaymentsType
    {

        [Description("Tiền mặt")]
        Cash = 1,

        [Description("Chuyển khoản")]
        Transfer = 2,

        [Description("Tiền mặt/ Chuyển khoản")]
        CashAndTransfer = 3,

        [Description("Đối trừ công nợ")]
        Liabilities = 4

    }
}
