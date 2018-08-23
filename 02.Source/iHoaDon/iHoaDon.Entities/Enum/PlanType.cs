using System;

namespace iHoaDon.Entities
{
    ///<summary>
    /// Loại gói dịch vụ
    ///</summary>
    [Flags]
    public enum PlanType
    {
        ///<summary>
        /// Cho doanh nghiệp
        ///</summary>
        Business = 1,
        ///<summary>
        /// Cho đại lý
        ///</summary>
        Service = 2,
        ///<summary>
        /// Cho cả doanh nghiệp và đại lý
        ///</summary>
        BusinessAndService = 3
    }
}
