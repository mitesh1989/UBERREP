using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace UBERREP.BusinessLayer.Common
{
    [System.Xml.Serialization.XmlType("MailClient")]
    public sealed class SmtpClient
    {
        [System.Xml.Serialization.XmlAttribute]
        public string Sender;
        public System.Net.NetworkCredential Credentials { get; set; }
        [System.Xml.Serialization.XmlAttribute]
        public string Host { get; set; }
        [System.Xml.Serialization.XmlAttribute]
        public string Port { get; set; }
        [System.Xml.Serialization.XmlAttribute]
        public string PickUpDirectory { get; set; }
        [System.Xml.Serialization.XmlAttribute]
        public System.Net.Mail.SmtpDeliveryMethod DeliveryMethod { get; set; }

        private System.Net.Mail.SmtpClient _smtpClient;

        private static object forLock = new object();
        public SmtpClient() { }
        public System.Net.Mail.SmtpClient GetInstance()
        {
            if (_smtpClient == null)
            {
                lock (forLock)
                {
                    _smtpClient = new System.Net.Mail.SmtpClient();
                    if (System.Configuration.ConfigurationManager.AppSettings["Mail.UseConfig"] == "true") return _smtpClient;
                    if (this.Credentials != null)
                        _smtpClient.Credentials = new System.Net.NetworkCredential(this.Credentials.UserName, this.Credentials.Password);
                    if (!string.IsNullOrEmpty(this.Port)) _smtpClient.Port = int.Parse(this.Port);
                    if (!string.IsNullOrEmpty(this.Host)) _smtpClient.Host = this.Host;
                    if (!string.IsNullOrEmpty(this.PickUpDirectory)) _smtpClient.PickupDirectoryLocation = this.PickUpDirectory;
                    _smtpClient.DeliveryMethod = this.DeliveryMethod;
                }
            }
            return _smtpClient;
        }
    }

    public class EmailManager
    {
        public static bool SendMail(string toList, string subject, string body, System.Net.Mail.AttachmentCollection attachements, string from = "support@softinspect.com")
        {
            SmtpClient mailClient = new SmtpClient();
            try
            {
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(from, toList, subject, body);
                message.IsBodyHtml = true;

                if (attachements != null)
                {
                    foreach (System.Net.Mail.Attachment attchement in attachements) message.Attachments.Add(attchement);
                }
                //mailClient.GetInstance().Send(message);                
                System.Net.Mail.SmtpClient myClient = new System.Net.Mail.SmtpClient("smtp.softinspect.com");
              //  myClient.Host = "smtp.softinspect.com";
               // myClient.Credentials = new System.Net.NetworkCredential("support@softinspect.com", "Softinspect@1234", "smtp.softinspect.com");
               // myClient.UseDefaultCredentials = false;
             //   myClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                myClient.Send(message);

                return true;
            }
            catch { }
            return false;
        }

        public static bool SendGMail(string toList, string subject, string body, System.Net.Mail.AttachmentCollection attachements, string from = "support@softinspect.com")
        {

            try
            {

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                {
                    Host = "smtp.gmail.com", // smtp server address here…
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new System.Net.NetworkCredential("odeskteam301@gmail.com", "odeskteam@301"),
                    Timeout = 30000,
                };
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage("odeskteam301@gmail.com", toList, subject, body);
                smtp.Send(message);

            }
            catch (Exception ex)
            {
                return false;
            }
            return true;

        }
    }
}