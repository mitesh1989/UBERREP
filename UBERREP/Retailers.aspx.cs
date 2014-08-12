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
            NameValueCollection spData = new NameValueCollection();
            spData.Add("usertype", Convert.ToString(UsersList1.UserType));
            UsersList1.UserList = BusinessLayer.Users.UserManager.GetUsers(null);
            if (!Page.IsPostBack)
                UsersList1.BindData();
        }
    }
}