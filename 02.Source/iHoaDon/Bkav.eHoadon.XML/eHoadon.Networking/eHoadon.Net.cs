using System;
using System.Net;
using RestSharp;

namespace Bkav.eHoadon.XML.eHoadon.Networking
{
    /// <summary>
    /// Gọi Restful WebService
    /// </summary>
    public class eHoadonNet
    {
        private static string _contentType = "application/xml;charset=UTF-8";
        public static string contentType
        {
            get { return _contentType; }
            set { _contentType = value; }
        }

        private static string _authorization = null;
        public static string authorization
        {
            get { return _authorization; }
            set { _authorization = value; }
        }
        /// <summary>
        /// Thực thi xác thực qua giao thức Restful WebService
        /// </summary>
        /// <param name="inXml">Dữ liệu XML gửi đi xác thực</param>
        /// <param name="url">Đường dẫn dịch vụ xác thực</param>
        /// <returns></returns>
        public static String Execute(string inXml, string url)
        {
            var client = new RestClient(url);

            var request = new RestRequest(Method.POST);
            request.AddHeader("Accept", contentType);
            request.AddHeader("Accept-Encoding", "gzip,deflate");

            if (!String.IsNullOrEmpty(authorization))
                request.AddHeader("Authorization", "Basic " + authorization);

            request.AddParameter(contentType, inXml, ParameterType.RequestBody);

            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("sending to: " + url);
            Console.WriteLine("---------------------------------------------------");
            IRestResponse response = client.Execute(request);

            if (response.ErrorException != null)
                throw response.ErrorException;

            var content = response.Content;
            return content;
        }
    }
}