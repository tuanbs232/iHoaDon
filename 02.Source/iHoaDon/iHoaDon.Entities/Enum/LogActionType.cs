using System.ComponentModel;

namespace iHoaDon.Entities
{
    /// <summary>
    /// The action type of log (add, update, delete)
    /// </summary>
    public enum LogActionType
    {
        /// <summary>
        /// Tạo mới tài khoản quản trị
        /// </summary>
        [Description("Tạo mới tài khoản quản trị")]
        CreateAccountAdminLog = 1,
        /// <summary>
        /// Cập nhật tài khoản quản trị
        /// </summary>
        [Description("Cập nhật tài khoản quản trị")]
        UpdateAccountAdminLog = 2,
        /// <summary>
        /// Tạo mới tài khoản khách hàng
        /// </summary>
        [Description("Tạo mới tài khoản khách hàng")]
        CreateAccountProfileLog = 3,
        /// <summary>
        /// Cập nhật tài khoản khách hàng
        /// </summary>
        [Description("Cập nhật tài khoản khách hàng")]
        UpdateAccountProfileLog = 4,
        /// <summary>
        /// Xóa tài khoản khách hàng
        /// </summary>
        [Description("Xóa tài khoản khách hàng")]
        DeleteAccountProfileLog = 5,
        /// <summary>
        /// Reset mật khẩu
        /// </summary>
        [Description("Reset mật khẩu")]
        ResetPasswordLog = 6,
        /// <summary>
        /// Tạo mới gói dịch vụ
        /// </summary>
        [Description("Tạo mới gói dịch vụ")]
        CreatePlanLog = 7,
        /// <summary>
        /// Cập nhật gói dịch vụ
        /// </summary>
        [Description("Cập nhật gói dịch vụ")]
        UpdatePlanLog = 8,
        /// <summary>
        /// Xóa gói dịch vụ
        /// </summary>
        [Description("Xóa gói dịch vụ")]
        DeletePlanLog = 9,
        /// <summary>
        /// Tạo mới nhóm thuế
        /// </summary>
        [Description("Tạo mới nhóm thuế")]
        CreateGroupTaxLog = 10,
        /// <summary>
        /// Cập nhật nhóm thuế
        /// </summary>
        [Description("Cập nhật nhóm thuế")]
        UpdateGroupTaxLog = 11,
        /// <summary>
        /// Xóa tài khoản khách hàng
        /// </summary>
        [Description("Xóa nhóm thuế")]
        DeleteGroupTaxLog = 12,
        /// <summary>
        /// Tạo mới loại thuế
        /// </summary>
        [Description("Tạo mới loại thuế")]
        CreatePackageTaxLog = 13,
        /// <summary>
        /// Cập nhật loại thuế
        /// </summary>
        [Description("Cập nhật loại thuế")]
        UpdatePackageTaxLog = 14,
        /// <summary>
        /// Không sử dụng loại thuế
        /// </summary>
        [Description("Không sử dụng loại thuế")]
        MarkObsoleteLog = 15,
        /// <summary>
        /// Xóa loại thuế
        /// </summary>
        [Description("Xóa loại thuế")]
        DeletePackageTaxLog = 16,
        /// <summary>
        /// Tạo mới tờ khai
        /// </summary>
        [Description("Tạo mới tờ khai")]
        CreateFormLog = 17,
        /// <summary>
        /// Sửa tờ khai
        /// </summary>
        [Description("Cập nhật tờ khai")]
        UpdateFormLog = 18,
        /// <summary>
        /// Xóa tờ khai
        /// </summary>
        [Description("Xóa tờ khai")]
        DeleteFormLog = 19,
        /// <summary>
        /// Sửa schema tờ khai
        /// </summary>
        [Description("Cập nhật schema tờ khai")]
        UpdateSchemaLog = 20,
        /// <summary>
        /// Hủy dịch vụ
        /// </summary>
        [Description("Hủy dịch vụ")]
        IsDeactived = 21,

        /// <summary>
        /// ScheduleCode
        /// </summary>
        [Description("Đặt lịch tự động gửi tin nhắn nhắc nhở kê khai")]
        ScheduleCode = 22,

        /// <summary>
        /// ScheduleDel
        /// </summary>
        [Description("Xóa đặt lịch tự động gửi tin nhắn nhắc nhở kê khai")]
        ScheduleDel = 23,

        /// <summary>
        /// ScheduleEdit
        /// </summary>
        [Description("Sửa đặt lịch tự động gửi tin nhắn nhắc nhở kê khai")]
        ScheduleEdit = 24,
        /// <summary>
        /// 
        /// </summary>
        [Description("Gửi thông báo theo định kỳ")]
        ScheduleCalender = 25,
        /// <summary>
        /// 
        /// </summary>
        [Description("Gửi thông báo theo sự kiện")]
        Schedule = 26,
    }
}
