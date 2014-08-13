using System;
using System.Web.UI;
using BusinessLayer.Common;
using System.Collections.Specialized;




namespace UBERREP
{
    public partial class Retailers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UsersList1.UserType = BusinessLayer.Users.UserTypes.Retailer;

            if (!Page.IsPostBack)
            {
                NameValueCollection spData = new NameValueCollection();
                spData.Add("type", Convert.ToString((int)UsersList1.UserType));
                UsersList1.UserList = BusinessLayer.Users.UserManager.GetUsers(spData);
                UsersList1.BindData();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
    }
}