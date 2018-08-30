using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ZSZ.Common
{
    public static class CommonHelper
    {
        public static string CaclMD5(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            return CaclMD5(bytes);
        }

        public static string CaclMD5(byte[] bytes)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] computeBytes = md5.ComputeHash(bytes);
                string result = "";
                for (int i = 0; i < computeBytes.Length; i++)
                {
                    result += computeBytes[i].ToString("X").Length == 1 ? "0" + computeBytes[i].ToString("X") : computeBytes[i].ToString("X");
                }
                return result;
            }
        }
        public static string CalcMD5(Stream stream)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] computeBytes = md5.ComputeHash(stream);
                string result = "";
                for (int i = 0; i < computeBytes.Length; i++)
                {
                    result += computeBytes[i].ToString("X").Length == 1;
                }
                return result;
            }
        }
        /// <summary>
        /// 创建验证码
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string CreateVerifyCode(int len)
        {
            char[] data = { 'a', 'c', 'd', 'e', 'f', 'h', 'k', 'm', 'n', 'r', 's', 't', 'w', 'x', 'y', '3', '4', '5', '7', '8' };
            StringBuilder sb = new StringBuilder();
            Random rand = new Random();
            for (int i = 0; i < len; i++)
            {
                int index = rand.Next(data.Length);
                char ch = data[index];
                sb.Append(ch);
            }
            return sb.ToString();

        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="body"></param>
        /// <param name="subject"></param>
        /// <param name="receiverAddress"></param>
        public static void SendMail(string receiverAddress, string body, string subject)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                using (SmtpClient smtpClient = new SmtpClient("smtp.163.com"))
                {
                    //TODO: 可以做成从配置文件读取相关信息
                    mailMessage.To.Add(receiverAddress);
                    mailMessage.Body = body;
                    mailMessage.Subject = subject;
                    mailMessage.From = new MailAddress("18397776538@163.com");
                    smtpClient.Credentials = new NetworkCredential("18397776538@163.com", "cui060830");
                    smtpClient.Send(mailMessage);
                }
            }
        }
    }
 }
