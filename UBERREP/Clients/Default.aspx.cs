using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer.Common;

namespace UBERREP.Admin.ClientManagement
{
    public partial class Default : WebLogic.WebPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
            FillEnumTypes(DRPStatus, "All", typeof(Status), "0");
            if (Request["Status"] != null && !Page.IsPostBack) DRPStatus.SelectedValue = Request["Status"].ToString();
        }

        private void BindData()
        {
            string sitename = Request["name"];
            string status = Request["status"];
            string api = Request["apiid"];

            //fill APIs
            if (!Page.IsPostBack)
            {
                
            }

            System.Collections.Specialized.NameValueCollection requestData = new System.Collections.Specialized.NameValueCollection();
            if (!string.IsNullOrEmpty(sitename)) requestData.Add("name", sitename);
            if (!string.IsNullOrEmpty(status)) requestData.Add("status", status);
            if (!string.IsNullOrEmpty(api)) requestData.Add("apiid", api);
            if (BusinessLayer.Common.CurrentContext.CurrentUser != null && BusinessLayer.Common.CurrentContext.CurrentUser.Type == BusinessLayer.Users.UserTypes.Client && BusinessLayer.Common.CurrentContext.CurrentUser.ID != null)
            {
                requestData.Add("userID",BusinessLayer.Common.CurrentContext.CurrentUser.ID.ToString());
            }


            // set up page controls based on querystring values
            if (!IsPostBack) this.SetControls(requestData);

            List<BusinessLayer.Client.Client> lstClients = BusinessLayer.Client.ClientManager.GetClients(requestData);
            if (lstClients != null && lstClients.Count > 0)
            {
                this.RPTRClientList.DataSource = lstClients;                
            }
            else
            {
                this.RPTRClientList.DataSource = null;                
            }
            this.RPTRClientList.DataBind();            
        }

        private void SetControls(System.Collections.Specialized.NameValueCollection requestData)
        {
            this.TXTClientName.Text = requestData["name"];

            if (!string.IsNullOrEmpty(requestData["status"]))
                this.DRPStatus.SelectedValue = requestData["status"];
            if (!string.IsNullOrEmpty(requestData["apiid"]))
                this.DRPAPIID.SelectedValue = requestData["apiid"].ToString();
        }
        protected void RPTRClientList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            BusinessLayer.Client.Client obj = ((List<BusinessLayer.Client.Client>)((Repeater)source).DataSource)[e.Item.ItemIndex];
            if (obj == null) return;
            switch (e.CommandName)
            {
                case "delete":
                    {
                        if (obj != null && obj.ID == -9999)
                        {
                            this.DisplayMessage = new WebLogic.ErrorMessage("Error", "System Client cannot be deleted");
                        }
                        else
                        {
                            BusinessLayer.Client.ClientManager.Delete(obj);
                            this.BindData();
                            CheckErrors("~/ClientManagement/Default.aspx", "Client has been deleted successfully", false, false);
                        }                        
                        break;
                    }
                case "edit":
                    {
                        Response.Redirect("Client.aspx?sid=" + obj.ID.ToString() + MakeQueryString());
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
            string retString = "&name=" + TXTClientName.Text.Trim();
            retString += "&status=" + DRPStatus.SelectedValue.ToString();
            retString += "&apiid=" + DRPAPIID.SelectedValue.ToString();

            return retString;
        }
        public override bool ValidateInput(out string ErrorMessage)
        {
            throw new NotImplementedException();
        }

        protected void BTNSearch_Click(object sender, EventArgs e)
        {
            string siteName = TXTClientName.Text.Trim().Equals("") ? null : TXTClientName.Text.Trim();
            string status = DRPStatus.SelectedValue.ToString();
            string API = DRPAPIID.SelectedValue.ToString();

            System.Collections.Specialized.NameValueCollection requestData = new System.Collections.Specialized.NameValueCollection();
            requestData.Add("name", siteName);
            requestData.Add("status", status);
            requestData.Add("apiid", API);
            Response.Redirect("default.aspx?" + ConstructQueryString(requestData), true);
        }        
    }
}