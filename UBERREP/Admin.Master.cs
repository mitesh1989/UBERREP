using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UBERREP
{
    public partial class AdminMaster : UBERREP.Admin.WebLogic.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void BTNExit_Click(object sender, EventArgs e)
        {
            BusinessLayer.Common.CurrentContext.CurrentUser = null;
            this.Session.Abandon();
            Response.Redirect("~/Index.aspx");
        }
    }
}