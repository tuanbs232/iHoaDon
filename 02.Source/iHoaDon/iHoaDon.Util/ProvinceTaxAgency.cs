using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace iHoaDon.Web.Helper
{
    /// <summary>
    /// 
    /// </summary>
    public static class ProvinceTaxAgency
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<ProvinceJson> TaxBureaus(string path)
        {
            try
            {
                string json = System.IO.File.ReadAllText(path);
                var playerList = JsonConvert.DeserializeObject<List<ProvinceJson>>(json);
                return playerList;
            }
            catch (Exception)
            {
                return null;
            }
        }
     
    }
}
/// <summary>
/// 
/// </summary>
   public class ProvinceJson
        {
            /// <summary>
            /// 
            /// </summary>
            public string Value; 
            /// <summary>
            /// 
            /// </summary>
            public string Text;
            /// <summary>
            /// 
            /// </summary>
            public List<TaxAgencyJson> TaxAgencys;

        }
        /// <summary>
        /// 
        /// </summary>
        public class TaxAgencyJson
        {
            /// <summary>
            /// 
            /// </summary>
            public string Value;
            /// <summary>
            /// 
            /// </summary>
            public string Text;

        }