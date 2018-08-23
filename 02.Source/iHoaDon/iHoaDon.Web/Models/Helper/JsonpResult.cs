using System;
using System.Web.Mvc;

namespace iHoaDon.Web
{
    public class JsonpResult : JsonResult
    {
        public string CallBackName { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            var request = context.HttpContext.Request;
            var response = context.HttpContext.Response;
            var callBackName = CallBackName ?? "callback";
            var jsoncallback = (context.RouteData.Values[callBackName] as string) ?? request[callBackName];
            if (!string.IsNullOrEmpty(jsoncallback))
            {
                if (string.IsNullOrEmpty(ContentType))
                {
                    ContentType = "application/x-javascript";
                }
                response.Write(string.Format("{0}(", jsoncallback));
            }
            base.ExecuteResult(context);
            if (!string.IsNullOrEmpty(jsoncallback))
            {
                response.Write(")");
            }
        }
    }
}