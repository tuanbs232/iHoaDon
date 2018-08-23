using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace iHoaDon.Util
{
    /// <summary>
    /// 
    /// </summary>
    public static class ProvinceTaxAgency
    {

        public static List<TaxAgencyNew> TaxBureaus(string path)
        {
            try
            {
                string json = System.IO.File.ReadAllText(path);
                var playerList = JsonConvert.DeserializeObject<List<TaxAgencyNew>>(json);
                return playerList;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
public class TaxAgencyNew
{
    public string Value;// { get; set; }
    public string Text;//{get;set;}
    public List<ProvinceNew> Childs;

}
public class ProvinceNew
{
    public string Value;// { get; set; }
    public string Text;//{get;set;}

}
