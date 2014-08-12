using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Uberrep
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [System.Web.Services.WebMethod]
        public static string ValidateLogin(string userName, string passWord)
        {
            return BusinessLayer.Common.Common.ValidateLogin(userName, passWord);
        }
    }
}