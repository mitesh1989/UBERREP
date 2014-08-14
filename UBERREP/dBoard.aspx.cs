using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UBERREP
{
    public partial class dBoard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UsersListRetailer.UserType = BusinessLayer.Users.UserTypes.Retailer;
            UsersListSales.UserType = BusinessLayer.Users.UserTypes.Sales;
        }
    }
}