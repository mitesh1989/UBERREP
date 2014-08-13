using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UBERREP
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UsersListRetailer.UserType = BusinessLayer.Users.UserTypes.Retailer;
            UsersListSales.UserType = BusinessLayer.Users.UserTypes.Sales;

            if (!Page.IsPostBack)
            {
                NameValueCollection spData = new NameValueCollection();
                spData.Add("type", Convert.ToString((int)UsersListSales.UserType));
                UsersListSales.UserList = BusinessLayer.Users.UserManager.GetUsers(spData);
                UsersListSales.BindData();

                NameValueCollection spData2 = new NameValueCollection();
                spData.Add("type", Convert.ToString((int)UsersListRetailer.UserType));
                UsersListRetailer.UserList = BusinessLayer.Users.UserManager.GetUsers(spData2);
                UsersListRetailer.BindData();
            }

            
        }

      

         
    }
}