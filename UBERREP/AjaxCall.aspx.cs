using BusinessLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UBERREP.BusinessLayer.Common;

namespace UBERREP
{
    public partial class mytest : System.Web.UI.Page
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