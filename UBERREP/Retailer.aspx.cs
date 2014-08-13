using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UBERREP
{
    public partial class Retailer : System.Web.UI.Page
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
    }
}