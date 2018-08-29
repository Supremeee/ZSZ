using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ZSZ.CommonMVC
{
    public class RuPengSMSSender
    {
        public string UserName { get; set; }

        public string AppKey { get; set; }

        public RuPengSMSResult SendSMS(string templateId,string code, string phoneNum)
        {
            WebClient client = new WebClient();
           
            
            string url = string.Format("http://sms.rupeng.cn/SendSms.ashx?userName={0}&appKey={1}&templateId={2}&code={3}&phoneNum={4}", Uri.EscapeDataString(UserName), Uri.EscapeDataString(AppKey), Uri.EscapeDataString(templateId), Uri.EscapeDataString(code), Uri.EscapeDataString(phoneNum));
            client.Encoding = Encoding.UTF8;
            string res = client.DownloadString(url);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            RuPengSMSResult result= jss.Deserialize<RuPengSMSResult>(res);
            return result;
        }
    }
}
