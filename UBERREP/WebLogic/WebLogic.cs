using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Specialized;

namespace UBERREP.Admin.WebLogic
{
    public abstract class WebPage : System.Web.UI.Page
    {
        public static String ConstructQueryString(NameValueCollection parameters)
        {
            List<string> items = new List<string>();
            foreach (String name in parameters) items.Add(String.Concat(name, "=", System.Web.HttpUtility.UrlEncode(parameters[name]))); return String.Join("&", items.ToArray());
        }
        public abstract bool ValidateInput(out string ErrorMessage);
        public new MasterPage Master
        {
            get { return (MasterPage)this.Page.Master; }
        }

        public Message DisplayMessage
        {
            get { return ((MasterPage)this.Page.Master).DisplayMessage; }
            set { ((MasterPage)this.Page.Master).DisplayMessage = value; }
        }


        #region Error Handling
        public void CheckErrors(string redirectURL, string displayMessage, bool doredirectOnError, bool resetEntity)
        {
            List<BusinessLayer.Common.Diagnostics.Exception> exceptions = BusinessLayer.Common.CurrentContext.GetExecutionErrors(true);
            List<BusinessLayer.Common.Diagnostics.Warning> warnings = BusinessLayer.Common.CurrentContext.GetExecutionWarnings(true);

            string errorMessage = string.Empty;
            if (exceptions.Count > 0 || warnings.Count > 0)
            {
                foreach (BusinessLayer.Common.Diagnostics.Exception ex in exceptions) errorMessage += "<li>" + ex.Message + "</li>";
                foreach (BusinessLayer.Common.Diagnostics.Warning ex in warnings) errorMessage += "<li>" + ex.Message + "</li>";

                if (exceptions.Count > 0) this.Master.DisplayMessage = new ErrorMessage("Error", errorMessage);
                else this.Master.DisplayMessage = new WarningMessage("Warning", errorMessage);

            }
            else this.Master.DisplayMessage = new InfoMessage("Success", displayMessage);
            if (exceptions.Count == 0 && warnings.Count == 0)
            {
                if (resetEntity) BusinessLayer.Common.CurrentContext.EntityToEdit = null;
                Response.Redirect(redirectURL, true); // no errors
            }
            else if (doredirectOnError) Response.Redirect(redirectURL, true); // errors still do redirect
        }
        #endregion

        #region Dropdown FillUP

        public void FillEnumTypes(DropDownList entityTypeList, string defaultText, Type enumType, string defaultValue)
        {
            string selected = IsPostBack ? Request[entityTypeList.UniqueID] : string.Empty;
            entityTypeList.Items.Clear();
            string _defaultValue = defaultValue != null ? defaultValue : string.Empty;
            if (defaultText != null) entityTypeList.Items.Add(new ListItem(defaultText, _defaultValue));
            Array entityTypes = Enum.GetValues(enumType);
            foreach (int value in entityTypes)
            {
                ListItem li = new ListItem(Enum.GetName(enumType, value).Replace("_", " "), value.ToString());
                if (value.ToString() == selected) li.Selected = true;
                entityTypeList.Items.Add(li);
            }
        }

        public void FillClients(ListControl ClientList, string defaultText, string defaultValue, string selectedValue)
        {
            ClientList.DataTextField = "Name";
            ClientList.DataValueField = "ID";
            if(BusinessLayer.Common.CurrentContext.CurrentUser!=null && BusinessLayer.Common.CurrentContext.CurrentUser.Type==BusinessLayer.Users.UserTypes.System)
                ClientList.DataSource = BusinessLayer.Client.ClientManager.GetClients(null);
            else
                ClientList.DataSource = BusinessLayer.Common.CurrentContext.CurrentUser.Clients;
            ClientList.DataBind();
            ClientList.Items.Insert(0, new ListItem(defaultText, defaultValue));
            ClientList.SelectedValue = ClientList.Items.FindByValue(selectedValue) != null ? selectedValue : defaultValue;
        }

        public void FillAPIs(DropDownList APIList, string defaultText, string defaultValue, string selectedValue)
        {
            APIList.DataTextField = "Name";
            APIList.DataValueField = "ID";            
            APIList.DataBind();
            APIList.Items.Insert(0, new ListItem(defaultText, defaultValue));
            APIList.SelectedValue = selectedValue;
        }

        public void FillUserGroups(ListControl DRPUserGroups, string defaultText, string defaultValue, string selectedValue)
        {
            DRPUserGroups.DataTextField = "Name";
            DRPUserGroups.DataValueField = "ID";
            DRPUserGroups.DataSource = BusinessLayer.Users.UserGroupManager.GetUserGroups(null);
            DRPUserGroups.DataBind();
            DRPUserGroups.Items.Insert(0, new ListItem(defaultText, defaultValue));
            DRPUserGroups.SelectedValue = selectedValue;
        }

        public void FillDateFormats(DropDownList DRPDateFormat, string defaultText, string defaultValue, string selectedValue)
        {
            DRPDateFormat.Items.Add(new ListItem(defaultText, defaultValue));
            DRPDateFormat.Items.Add(new ListItem("M/d/yyyy", "M/d/yyyy"));
            DRPDateFormat.Items.Add(new ListItem("M/d/yy", "M/d/yy"));
            DRPDateFormat.Items.Add(new ListItem("MM/dd/yy", "MM/dd/yy"));
            DRPDateFormat.Items.Add(new ListItem("MM/dd/yyyy", "MM/dd/yyyy"));
            DRPDateFormat.Items.Add(new ListItem("yy/MM/dd", "yy/MM/dd"));
            DRPDateFormat.Items.Add(new ListItem("yyyy-MM-dd", "yyyy-MM-dd"));
            DRPDateFormat.Items.Add(new ListItem("dd-MMM-yy", "dd-MMM-yy"));
            DRPDateFormat.Items.Add(new ListItem("MMM,yyyy", "MMM,yyyy"));//Jan,2013
            DRPDateFormat.Items.Add(new ListItem("MM,yyyy", "MM,yyyy"));//01,2013
            DRPDateFormat.Items.Add(new ListItem("MMM-yyyy", "MMM-yyyy"));//Jan-2013
            DRPDateFormat.Items.Add(new ListItem("MM-yyyy", "MM-yyyy"));//01-2013
            DRPDateFormat.Items.Add(new ListItem("MMMyyyy", "MMMyyyy"));//Jul2013
            DRPDateFormat.Items.Add(new ListItem("MMM+ddy+yyy", "MMM+dd+yyyy"));//Dec+1+2014 
            DRPDateFormat.Items.Add(new ListItem("Other", "-1"));//Dec+1+2014 
        }

        public void FillPageSizes(DropDownList DRPPageSize, string defaultText, string defaultValue, string selectedValue)
        {
            for (int i = 5; i <= 100; i += 5)
            {
                ListItem item = new ListItem(i.ToString(), i.ToString());
                DRPPageSize.Items.Add(item);
            }
            ListItem defaultItem = new ListItem(defaultText, defaultValue);
            DRPPageSize.Items.Insert(0, defaultItem);
            DRPPageSize.SelectedValue = selectedValue;
        }

        #endregion        



        /// <summary>
        /// This method will mask email address
        /// </summary>
        /// <param name="email">Valid email address</param>
        /// <returns>Masked email address</returns>
        public static string MaskEmail(string email)
        {
            string maskedEmail = string.Empty;
            if (!string.IsNullOrEmpty(email))
            {
                int Index = email.IndexOf("@");
                //let's split email address to get string before @ and after @. Ideally it should return array of length two.
                string[] emailPortions = email.Split('@');

                if (emailPortions.Length >= 2)
                {
                    if (emailPortions[0].Length == 1)
                        return email;//no masking is required if 1 character is ahead of @
                    else
                    {
                        char[] emailStrBefore = emailPortions[0].ToCharArray();
                        if (emailPortions[0].Length == 2)
                        {//if length is 2, then mask second character only leaving first one
                            maskedEmail = emailStrBefore[0] + "x";
                        }
                        else if (emailPortions[0].Length == 3)
                        {//if length is 3, then mask second and third character leaving first one
                            maskedEmail = emailStrBefore[0] + "xx";
                        }
                        else
                        {//if length is greater than 3, then mask all characters except first and last one
                            for (int i = 0; i < emailStrBefore.Length; i++)
                            {
                                if (i == 0 || i == emailStrBefore.Length - 1)
                                    maskedEmail += emailStrBefore[i];
                                else
                                    maskedEmail += "x";
                            }
                        }
                        return maskedEmail + "@" + emailPortions[1];
                    }
                }
            }
            return email;
        }

        public bool IsSystemUser
        {
            get { return this.Master.IsSystemUser; }
        }

        public bool IsUserLoggedIn
        {
            get { return this.Master.IsUserLoggedIn; }
        }


    }

    public class MasterPage : System.Web.UI.MasterPage
    {
        protected global::System.Web.UI.WebControls.PlaceHolder PageMessage;

        #region Properties
        /// <summary>
        /// This will check and return if logged in user is System User
        /// </summary>
        public bool IsSystemUser
        {
            get { return this.IsUserLoggedIn && BusinessLayer.Common.CurrentContext.CurrentUser.Type == BusinessLayer.Users.UserTypes.System; }
        }

        /// <summary>
        /// This will validate if user has been logged in or not
        /// </summary>
        public bool IsUserLoggedIn
        {
            get { return BusinessLayer.Common.CurrentContext.CurrentUser != null; }
        }
        #endregion
        protected override void OnInit(EventArgs e)
        {
            if (BusinessLayer.Common.CurrentContext.CurrentUser == null && System.Configuration.ConfigurationManager.AppSettings["AuthenticateAdmin"] != "false")
            {
                //we need to set index.aspx as default page-darshan
                //if (!Request.RawUrl.Contains("Login.aspx"))
                //{
                //    Response.Redirect("~/Login.aspx", true);
                //}
            }
            base.OnInit(e);
        }
        protected override void OnPreRender(EventArgs e)
        {
            bool pmDisplayed = false;
            if (this.DisplayMessage != null && this.PageMessage != null)
            {
                this.PageMessage.Controls.Add(this.DisplayMessage);
                this.PageMessage.Visible = true;
                pmDisplayed = true;
            }
            base.OnPreRender(e);
            //remove page message from cache
            if (pmDisplayed) this.DisplayMessage = null;
        }

        public WebPage ContainerPage
        {
            get
            {
                return (WebPage)this.Page;
            }

        }
        private Message displayMessage = null;
        public Message DisplayMessage
        {
            get
            {
                if (displayMessage == null && Session["DisplayMessage"] != null)
                {
                    this.displayMessage = (Message)Session["DisplayMessage"];
                }
                return this.displayMessage;
            }
            set
            {
                if (value == null) Session.Remove("DisplayMessage");
                else Session["DisplayMessage"] = value;
                this.displayMessage = value;
            }
        }


    }

    public class PageControl : System.Web.UI.UserControl
    {
        public WebPage ContainerPage
        {
            get
            {
                return (WebPage)this.Page;
            }

        }

        public MasterPage Master
        {
            get { return (MasterPage)this.ContainerPage.Master; }
        }
    }

}


