using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer.Common;

namespace UBERREP.Admin.Users
{
    public partial class Default : WebLogic.WebPage
    {
        protected override void OnInit(EventArgs e)
        {
            this.FillEnumTypes(this.DRPStatus, "All", typeof(Status), "0");
            this.BindData();
            base.OnInit(e);
        }

        private void BindData()
        {
            // try get all search parameters from query string
            string username = Request["username"];
            string email = Request["email"];
            string status = Request["status"];
            string name = Request["name"];

            // based on values being get on search params - setup request object

            System.Collections.Specialized.NameValueCollection requestData = new System.Collections.Specialized.NameValueCollection();
            if (!string.IsNullOrEmpty(username)) requestData.Add("username", username);
            if (!string.IsNullOrEmpty(email)) requestData.Add("email", email);
            if (!string.IsNullOrEmpty(status)) requestData.Add("status", status);
            if (!string.IsNullOrEmpty(name)) requestData.Add("name", name);

            // set up page controls based on querystring values
            if (!IsPostBack) this.SetControls(requestData);
            // get and bind users data
            List<BusinessLayer.Users.User> lstUser = BusinessLayer.Users.UserManager.GetUsers(requestData);
            if (lstUser != null && lstUser.Count != 0)
            {
                this.UserList.DataSource = lstUser;
                this.UserList.DataBind();
            }
        }
        /// <summary>
        /// We would be loosing our control values on search redirect - let's re-assign those
        /// </summary>
        /// <param name="data"></param>
        private void SetControls(System.Collections.Specialized.NameValueCollection data)
        {
            this.TXTName.Text = data["name"];
            this.TXTEmail.Text = data["email"];

            if (!string.IsNullOrEmpty(data["status"])) this.DRPStatus.SelectedValue = data["status"];
        }

        protected void UserList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            BusinessLayer.Users.User obj = ((List<BusinessLayer.Users.User>)((Repeater)source).DataSource)[e.Item.ItemIndex];
            if (obj == null) return;
            if (BusinessLayer.Common.CurrentContext.CurrentUser != null && (obj.ID == BusinessLayer.Common.CurrentContext.CurrentUser.ID && e.CommandName.Equals("delete")))
            {
                this.DisplayMessage = new WebLogic.InfoMessage("User Deletion Failed", "Current user can not be deleted");
                Response.Redirect("default.aspx", true);
            }

            switch (e.CommandName)
            {
                case "delete":
                    {
                        BusinessLayer.Users.UserManager.Delete(obj);
                        this.BindData();
                        CheckErrors("~/Users/Default.aspx", "User has been deleted successfully", false, false);
                        break;
                    }
                case "edit":
                    {
                        Response.Redirect("User.aspx?id=" + obj.ID.ToString() + MakeQueryString());
                        break;
                    }
            }
        }
        /// <summary>
        /// used to make query string, to set controls back to the value what user was searching, when user comes back after editing, in SetControls
        /// </summary>
        /// <returns></returns>
        private string MakeQueryString()
        {
            string retString = "&name=" + TXTName.Text.Trim();
            retString += "&email=" + TXTEmail.Text.Trim();
            retString += "&status=" + DRPStatus.SelectedValue.ToString();

            return retString;
        }
        protected void UserList_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (BusinessLayer.Common.CurrentContext.CurrentUser != null && ((BusinessLayer.Users.User)e.Item.DataItem).ID == BusinessLayer.Common.CurrentContext.CurrentUser.ID)
                    ((LinkButton)e.Item.FindControl("BTNDelete")).Visible = false;//current user can not delete himself
            }
        }
        protected void BTNSearch_Click(object sender, EventArgs e)
        {
            string username = null;
            string email = this.TXTEmail.Text.Trim();
            string status = this.DRPStatus.SelectedValue;
            string name = this.TXTName.Text.Trim();

            System.Collections.Specialized.NameValueCollection requestData = new System.Collections.Specialized.NameValueCollection();
            if (!string.IsNullOrEmpty(username)) requestData.Add("username", username);
            if (!string.IsNullOrEmpty(email)) requestData.Add("email", email);
            if (!string.IsNullOrEmpty(status)) requestData.Add("status", status);
            if (!string.IsNullOrEmpty(name)) requestData.Add("name", name);
            Response.Redirect("default.aspx?" + ConstructQueryString(requestData), true);
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public override bool ValidateInput(out string ErrorMessage)
        {
            throw new NotImplementedException();
        }
    }
}