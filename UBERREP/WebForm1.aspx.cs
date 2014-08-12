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
            UserListUC.UserType = BusinessLayer.Users.UserTypes.Retailer;
            NameValueCollection spData = new NameValueCollection();
            spData.Add("usertype",Convert.ToString(UserListUC.UserType));
            UserListUC.UserList = BusinessLayer.Users.UserManager.GetUsers(null);
            if(!Page.IsPostBack)
            UserListUC.BindData();
        }
        [System.Web.Services.WebMethod]
        public static string ValidateLogin(string userName, string passWord)
        {
            return Common.ValidateLogin(userName, passWord);
        }
    }
}