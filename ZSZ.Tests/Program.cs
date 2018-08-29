using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.Common;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Autofac;
using System.Reflection;
using System.Net;
using qcloudsms_csharp;
using qcloudsms_csharp.json;
using qcloudsms_csharp.httpclient;
using ZSZ.Service;
using System.Data.SqlClient;
using System.Data;

namespace ZSZ.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            //string s = CommonHelper.CreateVerifyCode(4);
            //Console.WriteLine(s);
            //string body = "尊敬的崔英群，您好：\r\n 感谢您使用 iCloud 来安全地储存 iOS 设备中的重要信息。您的 5 GB 免费 iCloud 储存空间剩余容量已不足 25 %。\r\n若每个月支付 ¥6.00，您便可获得 50 GB 储存空间。获取更多储存空间。您正在使用 iCloud 云盘将文稿安全地储存在 iCloud 中。iCloud 云备份还会在每天夜间自动备份您的设备上的所有 App。如果 iCloud 储存空间不足，您的新文稿将不会再上传至 iCloud，您的设备也会停止备份。此致iCloud 团队";

            //CommonHelper.SendMail("1134519857@qq.com",body, "您的 iCloud 储存空间容量仅剩 25%");
            //log4net.Config.XmlConfigurator.Configure();//从Config文件中加载配置
            //ILog log = LogManager.GetLogger(typeof(Program));
            //log.Debug("飞行高度1000米");
            //log.Warn("油压不足");
            //log.Error("引擎失灵");
            //Console.WriteLine(nameof(Program));
            //IScheduler sched = new StdSchedulerFactory().GetScheduler();
            //JobDetailImpl jdBossReport = new JobDetailImpl("jdTest", typeof(TestJob));
            //IMutableTrigger triggerBossReport =  CalendarIntervalScheduleBuilder.Create().WithInterval(3, IntervalUnit.Second).Build();
            ////IMutableTrigger triggerBossReport = CronScheduleBuilder.DailyAtHourAndMinute(14, 32).Build();
            //triggerBossReport.Key = new TriggerKey("triggerTest");
            //sched.ScheduleJob(jdBossReport, triggerBossReport);
            //sched.Start();       
            //ContainerBuilder bulider = new ContainerBuilder();
            //bulider.RegisterType<UserBll>().As<IUserBll>();
            //Assembly asm = Assembly.Load(nameof(BLLImpl));
            //bulider.RegisterAssemblyTypes(asm).AsImplementedInterfaces().PropertiesAutowired();//属性注入
            ////IUserBll userbll = bulider.Build().Resolve<IUserBll>();
            ////userbll.Add("test");
            //ISchool school = bulider.Build().Resolve<ISchool>();
            //school.FangXue();
            //发送验证码
            //WebClient client = new WebClient();
            //string userName = "supreme";
            //string appKey = "f3411e802ff10cb70f7a32";
            //string templateId = "1180";
            //string code = "123456";
            //string phoneNum = "18918918918";
            //string url = string.Format("http://sms.rupeng.cn/SendSms.ashx?userName={0}&appKey={1}&templateId={2}&code={3}&phoneNum={4}", Uri.EscapeDataString(userName), Uri.EscapeDataString(appKey), Uri.EscapeDataString(templateId), Uri.EscapeDataString(code), Uri.EscapeDataString(phoneNum));
            //client.Encoding = Encoding.UTF8;
            //string result = client.DownloadString(url);
            //Console.WriteLine(result);
            //使用腾讯短信接口发送短信
            //SendSMSUseTencentAPI();
            //using(ZSZDbContext dbcontext = new ZSZDbContext())
            //{
            //    dbcontext.Database.Delete();
            //    dbcontext.Database.Create();
            //}
            //using (SqlConnection conn = new SqlConnection ( "Server=.;DataBase=test1; uid=sa;pwd=123456;"))
            //{
            //    SqlCommand cmd = new SqlCommand();
            //    cmd.Connection = conn;
            //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //    cmd.CommandText = "GetAllMinZu";
            //    //conn.Open();
            //    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            //    DataSet ds = new DataSet();
            //    dataAdapter.Fill(ds);
            //    if(ds.Tables.Count != 0)
            //    {
            //        DataTable dt = ds.Tables[0];
            //        if(dt.Rows.Count != 0)
            //        {
            //            for (int i = 0; i < dt.Rows.Count; i++)
            //            {
            //                Console.WriteLine(dt.Rows[i]["Name"]); 
            //            }
            //        }
            //    } 
            //}
            Console.WriteLine("ok");
            Console.ReadKey();
        }

        public static void SendSMSUseTencentAPI()
        {
            // 短信应用SDK AppID
            int appid = 1400130590;

            // 短信应用SDK AppKey
            string appkey = "f9683967be57f6617d7096e2cc5ce78c";

            // 需要发送短信的手机号码
            string[] phoneNumbers = { "18397776538" };

            // 短信模板ID，需要在短信应用中申请
            int templateId = 182188; // NOTE: 这里的模板ID`7839`只是一个示例，真实的模板ID需要在短信控制台中申请
                                   //templateId 7839 对应的内容是"您的验证码是: {1}"
                                   // 签名
            string smsSign = ""; // NOTE: 这里的签名只是示例，请使用真实的已申请的签名, 签名参数使用的是`签名内容`，而不是`签名ID`

            try
            {
                SmsSingleSender ssender = new SmsSingleSender(appid, appkey);
                var result = ssender.sendWithParam("86", phoneNumbers[0],
                    templateId, new[] { "老婆" }, smsSign, "", "");  // 签名参数未提供或者为空时，会使用默认签名发送短信
                Console.WriteLine(result);
            }
            catch (JSONException e)
            {
                Console.WriteLine(e);
            }
            catch (HTTPException e)
            {
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
