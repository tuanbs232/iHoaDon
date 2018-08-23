using System.ComponentModel;

namespace iHoaDon.Entities
{
    /// <summary>
    /// The operation type code (which is fed to GDT's web service along with the tax file)
    /// </summary>
    public enum OperationType
    {
        /// <summary>
        /// 01/ĐK-Tvan: đăng ký sử dụng một dịch vụ Tvan
        /// </summary>
        [Description("Đăng ký MST")]
        Registration    = 0001,
        /// <summary>
        /// 01/ĐK-Tvan: đăng ký gửi tờ khai nào qua Tvan, bắt đầu từ khi nào
        /// </summary>
        [Description("Đăng ký tờ khai")]
        Subscription    = 0002,
        /// <summary>
        /// 01/ĐK-Tvan: Thay đổi chứng thư số
        /// </summary>
        [Description("Thay đổi chữ ký số")]
        ChangeCA        = 0003,
        /// <summary>
        /// 01/ĐK-Tvan: đăng ký ngừng gửi tờ khai nào qua Tvan, ngừng từ khi nào
        /// </summary>
        [Description("Hủy đăng ký tờ khai")]
        UnSubscription  = 0004,
        /// <summary>
        /// 05/ĐK-Tvan: ngừng sử dụng dịch vụ Tvan
        /// </summary>
        [Description("Hủy đăng ký MST")]
        UnRegistrattion = 0005,
        /// <summary>
        /// Gửi tờ khai (pdf)
        /// </summary>
        [Description("Gửi tờ khai")]
        SendDocument    = 0006,
        /// <summary>
        /// Gửi phụ lục (excel)
        /// </summary>
        [Description("Gửi phụ lục")]
        SendAppendix    = 0007,
        /// <summary>
        /// Gửi ấn chỉ
        /// </summary>
        [Description("Gửi ấn chỉ")]
        SendAnChi = 0008,
        /// <summary>
        /// Gửi quyết toán thuế TNCN
        /// </summary>
        [Description("Gửi quyết toán thuế TNCN")]
        SendQtTncn = 0009,
    }
}