using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UBERREP.Controls
{
    public partial class UsersList : System.Web.UI.UserControl
    {
        private List<BusinessLayer.Users.User> userList;
        public List<BusinessLayer.Users.User> UserList
        {
            get { return this.userList; }
            set { this.userList = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void BindData()
        {
            if (UserList != null && UserList.Count > 0)
            {
                UserListRPT.DataSource = UserList;
                UserListRPT.DataBind();
            }
            else { UserListRPT.Visible = false; }
        }
        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        protected override void OnPreRender(EventArgs e)
        {

            base.OnPreRender(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

            this.Load += new System.EventHandler(this.Page_Load);
            this.UserListRPT.ItemCommand += UserListRPT_ItemCommand;
        }

        private void UserListRPT_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            throw new NotImplementedException();    
        }
        #endregion
    }
}