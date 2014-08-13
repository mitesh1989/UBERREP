using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UBERREP.Controls;

namespace UBERREP
{
    public partial class TestPage : System.Web.UI.Page
    {

        protected UBERREP.Controls.UsersList UserListUC;
        protected void Page_Load(object sender, EventArgs e)
        {
          

           UserListUC.UserList = BusinessLayer.Users.UserManager.GetUsers(null);
           UserListUC.BindData();
        }
       
    }
}