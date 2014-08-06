using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Configuration;
using System.Net;


namespace UBERREP.BusinessLayer.Common.Diagnostics
{
    [Serializable]
    public class Exception
    {
        [System.Xml.Serialization.XmlAttribute]
        public string Code;
        [System.Xml.Serialization.XmlAttribute]
        public string Message;
        [System.Xml.Serialization.XmlAttribute]
        public string Source;

        public string Text;

        public string StackTrace;

        [System.Xml.Serialization.XmlIgnore]
        private string ScreenShot;
        
        public static explicit operator Exception(System.Exception ex)
        {
            Exception error = new Exception();
            if (ex != null)
            {
                error.Message = ex.Message;
                error.Source = ex.Source;
                error.StackTrace = ex.StackTrace;
                HttpException httpWrapper = ex as HttpException;
                if (httpWrapper != null) error.ScreenShot = httpWrapper.GetHtmlErrorMessage();
            }
            return error;
        }
        public Exception() { }
        public Exception(string code)
        {
            this.Code = code;
        }
        public Exception(string code, string message)
        {
            this.Code = code;
            this.Message = message;
        }

        public void LogError()
        {
            System.Threading.ThreadPool.QueueUserWorkItem(DoWork, HttpContext.Current);
        }

        public void LogError(Object currentContext)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(DoWork, currentContext);
        }

        public void LogError(string errorCode)
        {// pass current context to this thread
            this.Code = errorCode;
            if (ConfigurationManager.AppSettings["LogErrors"] == "true") { }
            System.Threading.ThreadPool.QueueUserWorkItem(DoWork, HttpContext.Current);
        }
        public void LogError(string errorCode, Object currentContext)
        {// pass current context to this thread
            this.Code = errorCode;
            if (ConfigurationManager.AppSettings["LogErrors"] == "true")
                System.Threading.ThreadPool.QueueUserWorkItem(this.DoWork, currentContext);
        }

        protected string Pad(int length,string data, string characters)
        {
            if (data.Length < length) length = data.Length - 1;
            return data.Replace(data.Substring(0, length), characters);
        }
        private void DoWork(Object statInfo)
        {
            // get email accounts loaded from configuration file
            //EmailAccountSettings emailAccountCollection = ConfigurationManager.GetSection("EmailSettings/SystemEmails") as EmailAccountSettings;
            //// we don't have values in configuration
            //if (emailAccountCollection == null) return;

            //// error from
            //EmailAccount sender     = emailAccountCollection.EmailAccounts["customErrorFrom"];
            ////error to
            //EmailAccount receiver   = emailAccountCollection.EmailAccounts["customErrorTo"];

            //string WSTrace = string.Empty;

            //try
            //{

            //    System.Net.Mail.MailAddress to      = receiver == null ? new System.Net.Mail.MailAddress("support@odysseussolutions.com") : receiver;
            //    System.Net.Mail.MailAddress from    = sender == null ? new System.Net.Mail.MailAddress("errors@odysseussolutions.com") : sender;
            //    //// maximum length allowed in subject line should not exceed 255
            //    string subject                      = System.Environment.MachineName + " : UBERREP - ";
            //    string XMLTrace                     = string.Empty;

            //    string title                        = this.Message.Length > 150 ? this.Message.Substring(0, 150) : this.Message;
            //    subject += title;

            //    System.Text.StringBuilder message = new System.Text.StringBuilder();
            //    message.AppendFormat("<strong> Code : </strong> {0} <br/>", this.Code);
            //    message.AppendFormat("<strong> Source : </strong> {0} <br/>", this.Source);
            //    message.AppendFormat("<strong> Message : </strong> {0} <br/>", this.Message);
            //    message.AppendFormat("<strong> Stack Trace : </strong> {0} <br/>", this.StackTrace);

            //    HttpContext c = statInfo as HttpContext;
            //    System.Net.Mail.Attachment errorRequest = null;

            //    if (c != null)
            //    {
            //        c.Request.InputStream.Position = 0;
            //        byte[] bytes    = new byte[c.Request.InputStream.Length];
            //        c.Request.InputStream.Read(bytes, 0, (int)c.Request.InputStream.Length);
            //        string data     = System.Text.Encoding.UTF8.GetString(bytes);
            //        errorRequest    = System.Net.Mail.Attachment.CreateAttachmentFromString(data, "Request.txt");
            //    }


            //    System.Net.Mail.MailMessage ErrorEmail = new System.Net.Mail.MailMessage(from, to);
            //    ErrorEmail.IsBodyHtml = true;
            //    ErrorEmail.Subject = subject;
            //    ErrorEmail.Priority = System.Net.Mail.MailPriority.High;
            //    ErrorEmail.Body = message.ToString();
            //    if (errorRequest != null) ErrorEmail.Attachments.Add(errorRequest);
            //    new SmtpClient().GetInstance().Send(ErrorEmail);                
            //}
            //catch (System.Net.Mail.SmtpException) { }
            //catch (System.Exception) { }
        }
    }       

    public class Warning
    {
        [System.Xml.Serialization.XmlAttribute]
        public string Code;
        [System.Xml.Serialization.XmlAttribute]
        public string Message;

        public Warning() { }
        public Warning(string code, string message)
        {
            this.Code = code;
            this.Message = message;
        }
    }
}
