using BusinessLayer.Common;
using System;
using System.Collections.Specialized;
using System.Web.UI;

namespace UBERREP
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        [System.Web.Services.WebMethod]
        public static string ValidateLogin(string userName, string passWord)
        {
            return Common.ValidateLogin(userName, passWord);
        }
    }
}