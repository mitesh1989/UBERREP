using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer.Common;

namespace UBERREP.Admin.Users
{
    public partial class UserGroups : WebLogic.WebPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
            FillEnumTypes(DRPStatus, "All", typeof(Status), "0");
            if (Request["Status"] != null && !Page.IsPostBack) DRPStatus.SelectedValue = Request["Status"].ToString();
            if (Request["name"] != null && !Page.IsPostBack) TXTGroupName.Text = Request["name"].ToString();
        }

        private void BindData()
        {
            string ClientID = null;//Request["Clientid"];
            string status = Request["status"];
            string groupname = Request["name"];

            if (ClientID == null)
                ClientID = BusinessLayer.Common.CurrentContext.CurrentUser != null && BusinessLayer.Common.CurrentContext.CurrentUser.Clients != null && BusinessLayer.Common.CurrentContext.CurrentUser.Clients.Count > 0 ? BusinessLayer.Common.CurrentContext.CurrentUser.Clients.FirstOrDefault().ID.ToString() : null;
            System.Collections.Specialized.NameValueCollection requestData = new System.Collections.Specialized.NameValueCollection();
            if (!string.IsNullOrEmpty(ClientID)) requestData.Add("ClientID", !this.IsSystemUser ? ClientID : null);
            if (!string.IsNullOrEmpty(status)) requestData.Add("status", status);
            if (!string.IsNullOrEmpty(groupname)) requestData.Add("name", groupname);

            // set up page controls based on querystring values
            if (!IsPostBack) this.SetControls(requestData);

            List<BusinessLayer.Users.UserGroup> lstClients = BusinessLayer.Users.UserGroupManager.GetUserGroups(requestData);
            if (lstClients != null && lstClients.Count > 0)
            {
                this.RPTRUserGroups.DataSource = lstClients;                
            }
            else
            {
                this.RPTRUserGroups.DataSource = null;                
            }
            this.RPTRUserGroups.DataBind();            
        }

        private void SetControls(System.Collections.Specialized.NameValueCollection requestData)
        {
            //this.TXTClientName.Text = requestData["ClientID"];

            if (!string.IsNullOrEmpty(requestData["status"]))
                this.DRPStatus.SelectedValue = requestData["status"];
            if (!string.IsNullOrEmpty(requestData["name"]))
                this.TXTGroupName.Text = requestData["name"];
        }
        protected void RPTRUserGroups_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            BusinessLayer.Users.UserGroup obj = ((List<BusinessLayer.Users.UserGroup>)((Repeater)source).DataSource)[e.Item.ItemIndex];
            if (obj == null) return;
            switch (e.CommandName)
            {
                case "delete":
                    {
                        if (obj != null && obj.ID == -9999)
                        {
                            this.DisplayMessage = new WebLogic.ErrorMessage("Error", "System User Group cannot be deleted");
                        }
                        else
                        {
                            BusinessLayer.Users.UserGroupManager.Delete(obj);
                            this.BindData();
                            CheckErrors("~/Users/UserGroups.aspx", "User Group has been deleted successfully", false, false);
                        }
                        break;
                    }
                case "edit":
                    {
                        Response.Redirect("UserGroup.aspx?gid=" + obj.ID.ToString() + MakeQueryString());
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
            //string retString = "&ClientID=" + TXTClientName.Text.Trim();
            string retString = string.Empty;
            retString += "&status=" + DRPStatus.SelectedValue.ToString();
            retString += "&name=" + TXTGroupName.Text;
            return retString;
        }
        public override bool ValidateInput(out string ErrorMessage)
        {
            throw new NotImplementedException();
        }

        protected void BTNSearch_Click(object sender, EventArgs e)
        {
            //string ClientName = TXTClientName.Text.Trim().Equals("") ? null : TXTClientName.Text.Trim();
            string status = DRPStatus.SelectedValue.ToString();            

            System.Collections.Specialized.NameValueCollection requestData = new System.Collections.Specialized.NameValueCollection();
            //requestData.Add("ClientID", ClientName);
            requestData.Add("status", status);
            requestData.Add("name", TXTGroupName.Text.Trim());
            Response.Redirect("UserGroups.aspx?" + ConstructQueryString(requestData), true);
        }

        protected void RPTRUserGroups_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (((BusinessLayer.Users.UserGroup)e.Item.DataItem).ClientID.Equals("-9999") && (BusinessLayer.Common.CurrentContext.CurrentUser.Type == BusinessLayer.Users.UserTypes.Client))
                {
                    ((LinkButton)e.Item.FindControl("BTNEdit")).Visible = ((LinkButton)e.Item.FindControl("BTNDelete")).Visible = false;//
                }
            }
        }        
    }    
}