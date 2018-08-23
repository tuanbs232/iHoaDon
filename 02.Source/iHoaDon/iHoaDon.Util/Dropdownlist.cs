using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iHoaDon.Util
{
    /// <summary>
    /// 
    /// </summary>
    public class Dropdownlist
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<DataListType> ListType()
        {
            var list = new List<DataListType>();
            list.Add(new DataListType(0, "Khác"));
            list.Add(new DataListType(1, "Nguồn tin"));
            list.Add(new DataListType(2, "Loại lỗ hổng"));
            list.Add(new DataListType(3, "Khả năng khai thác"));
            list.Add(new DataListType(4, "Cách khắc phục"));
            list.Add(new DataListType(5, "Loại mã khai thác"));
            list.Add(new DataListType(6, "Mức độ ảnh hưởng"));
            list.Add(new DataListType(7, "Link lỗi"));
            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<DataListType> ListResult()
        {
            List<DataListType> list = new List<DataListType>();
            list.Add(new DataListType(0, "Chưa kiểm tra"));
            list.Add(new DataListType(1, "Đã duyệt"));
            list.Add(new DataListType(2, "Đã phát hành"));
            return list;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class DataListType
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="intValue"></param>
        /// <param name="strValue"></param>
        public DataListType(int intValue, string strValue)
        {
            IntegerData = intValue;
            StringData = strValue;
        }
        /// <summary>
        /// 
        /// </summary>
        public int IntegerData { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string StringData { get; private set; }
    }
}
