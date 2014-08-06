using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer.Common;
using UBERREP.BusinessLayer.Common;

namespace UBERREP.Admin.Users
{
    public partial class User : WebLogic.WebPage
    {
        public int UserID
        {
            get
            {
                string uid = Request.QueryString["id"];
                return !string.IsNullOrEmpty(uid) ? int.Parse(uid) : 0;
            }
        }

        private BusinessLayer.Users.User entityToEdit = null;
        public BusinessLayer.Users.User EntityToEdit
        {
            get
            {
                if (this.entityToEdit != null) return this.entityToEdit;
                if (this.UserID != 0)
                {
                    this.entityToEdit = BusinessLayer.Users.UserManager.Get(this.UserID);
                }
                return this.entityToEdit;
            }

        }
        private string _receivedQueryString = string.Empty;
        string ReceivedQueryString
        {
            get
            {
                if (this._receivedQueryString == string.Empty)
                    this._receivedQueryString = MakeQueryString();
                return this._receivedQueryString;
            }
        }

        private void FillClients()
        {
            this.LSTClients.Items.Clear();

            List<BusinessLayer.Client.Client> Clients = BusinessLayer.Client.ClientManager.GetClients(null);
            foreach (BusinessLayer.Client.Client s in Clients)
            {
                ListItem li = new ListItem(s.Name, s.ID.ToString());
                li.Selected = this.IsClientSelected(s);
                this.LSTClients.Items.Add(li);
            }
            this.LSTClients.Items.Insert(0, new ListItem("--Select--", "-1"));
        }

        private bool IsClientSelected(BusinessLayer.Client.Client Client)
        {
            if (this.EntityToEdit != null && this.EntityToEdit.Clients != null && this.EntityToEdit.Clients.Count > 0)
            {
                return this.EntityToEdit.Clients.Any(s => s.ID == Client.ID);
            }
            return false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // on postback we loosing this values - let's re-assign them

            string password = Request[TXTPassword.UniqueID];
            if (!string.IsNullOrEmpty(password)) this.TXTPassword.Attributes.Add("value", password);

            string confirmpassword = Request[this.TXTConfirmPassword.UniqueID];
            if (!string.IsNullOrEmpty(confirmpassword)) this.TXTConfirmPassword.Attributes.Add("value", confirmpassword);

            if (!IsPostBack)
            {
                this.FillEnumTypes(this.DRPStatus, null, typeof(Status), null);
                this.FillEnumTypes(this.DRPType, "--Select--", typeof(BusinessLayer.Users.UserTypes), "0");
                this.FillClients();
                this.FillUserGroups();                
            }

            if (this.EntityToEdit != null && !IsPostBack) this.DisplayEntity();
            if (BusinessLayer.Common.CurrentContext.CurrentUser != null && (this.UserID == BusinessLayer.Common.CurrentContext.CurrentUser.ID)) this.TRStatus.Visible = false;

        }

        private void FillUserGroups()
        {
            System.Collections.Specialized.NameValueCollection data = new System.Collections.Specialized.NameValueCollection();;
            if (BusinessLayer.Common.CurrentContext.CurrentUser != null && BusinessLayer.Common.CurrentContext.CurrentUser.Clients != null && BusinessLayer.Common.CurrentContext.CurrentUser.Clients.Count > 0 && BusinessLayer.Common.CurrentContext.CurrentUser.Type == BusinessLayer.Users.UserTypes.Client)
            {
                data.Add("ClientID", BusinessLayer.Common.CurrentContext.CurrentUser.Clients.FirstOrDefault().ID.ToString());
            }
            else if (!LSTClients.SelectedValue.Equals("0"))
            {                
                data.Add("ClientID", LSTClients.SelectedValue.ToString());
            }
            
                LSTGroups.DataTextField = "Name";
                LSTGroups.DataValueField = "ID";
                this.LSTGroups.DataSource = BusinessLayer.Users.UserGroupManager.GetUserGroups(data);
                this.LSTGroups.DataBind();
                this.LSTGroups.Items.Insert(0, new ListItem("--Select--", "0"));
                this.LSTGroups.SelectedValue = "0";                        
        }


        private void Save(BusinessLayer.Users.User obj)
        {
            if (this.UserID == 0) BusinessLayer.Users.UserManager.Create(obj);
            else BusinessLayer.Users.UserManager.Update(obj);
        }

        /// <summary>
        /// To display data from object into controls
        /// </summary>
        private void DisplayEntity()
        {
            this.TXTPassword.Attributes.Add("value", this.entityToEdit.Password);
            this.TXTConfirmPassword.Attributes.Add("value", this.entityToEdit.Password);
            this.TXTEmail.Text = this.EntityToEdit.Email;
            this.TXTName.Text = this.entityToEdit.Name;
            this.TXTUserName.Text = this.entityToEdit.Username;
            if (this.entityToEdit.ContactData != null)
            {
                this.TXTCell.Text = this.entityToEdit.ContactData.CellNumber;
            }

            foreach (ListItem item in this.LSTGroups.Items)
            {
                item.Selected = this.EntityToEdit.Groups.Any(c => c.ID == int.Parse(item.Value));
            }
            if (this.EntityToEdit != null && this.EntityToEdit.Type == BusinessLayer.Users.UserTypes.Client && this.EntityToEdit.Clients != null & this.EntityToEdit.Clients.Count > 0)
                this.LSTClients.SelectedValue = this.EntityToEdit.Clients[0].ID.ToString();

            this.TXTUserName.Enabled = false;//make it non editable when displaying            
            this.DRPStatus.SelectedValue = ((int)this.EntityToEdit.Status).ToString();
            this.DRPType.SelectedValue = ((int)this.EntityToEdit.Type).ToString();
        }
        /// <summary>
        /// method to set Data from controls to object
        /// </summary>
        /// <returns></returns>
        private BusinessLayer.Users.User PrePareEntity()
        {
            BusinessLayer.Users.User retObj = this.EntityToEdit != null ? this.EntityToEdit : new BusinessLayer.Users.User();
            if (this.UserID == 0)
            {
                retObj.Username = this.TXTUserName.Text.Trim();
            }
            retObj.ContactData = new ContactInfo();
            retObj.ContactData.CellNumber = TXTCell.Text.Trim();

            // if we are editing a user account and password field is not blank - then only go for changing it
            if (this.EntityToEdit != null && !string.IsNullOrEmpty(this.TXTPassword.Text.Trim()))
            {
                retObj.Password = this.TXTPassword.Text.Trim();
            }
            else retObj.Password = this.TXTPassword.Text.Trim();
            retObj.Name = this.TXTName.Text.Trim();
            retObj.Email = this.TXTEmail.Text.Trim();

            if (this.IsSystemUser)
            {
                foreach (ListItem item in LSTClients.Items)
                {
                    if (item.Selected)
                        retObj.ClientsToSave += item.Value + ",";
                }
            }
            else
            {                
                retObj.ClientsToSave = BusinessLayer.Common.CurrentContext.CurrentUser.Clients[0].ToString();
            }

            if(!string.IsNullOrEmpty(retObj.ClientsToSave))
                retObj.ClientsToSave = retObj.ClientsToSave.Trim(',');

            if (this.IsSystemUser)
                retObj.Type = (BusinessLayer.Users.UserTypes)int.Parse(DRPType.SelectedValue);
            else if (this.EntityToEdit != null)
                retObj.Type = this.EntityToEdit.Type;
            else if(this.EntityToEdit==null)
            {
                retObj.Type = BusinessLayer.Users.UserTypes.Client;
            }
            
            

            if (this.IsSystemUser && retObj.Type == BusinessLayer.Users.UserTypes.Client && string.IsNullOrEmpty(retObj.ClientsToSave))
            {
                this.DisplayMessage = new WebLogic.ErrorMessage("Invalid Input", "Please select atleast one client");
                LSTClients.Visible = true;
                return null;
            }

            retObj.Status = (Status)int.Parse(DRPStatus.SelectedValue);
            

            return retObj;
        }

        public override bool ValidateInput(out string ErrorMessage)
        {
            ErrorMessage = string.Empty;
            if (BusinessLayer.Common.CurrentContext.CurrentUser != null && (this.UserID == BusinessLayer.Common.CurrentContext.CurrentUser.ID))
            {
                if (!this.DRPStatus.SelectedItem.ToString().Equals(Status.Active.ToString())) ErrorMessage += "Can not Deactive/Suspend Current User";
            }
            if (!this.TXTUserName.IsValidInput(InputType.Username)) ErrorMessage += "UserName - Alphanumeric 5 to 50 chars<br>";
            if (!this.TXTEmail.IsValidInput(InputType.EMailAddress)) ErrorMessage += "Email - Invalid email format<br>";
            if (!this.TXTPassword.IsValidInput(InputType.Password)) ErrorMessage += "Password - Invalid password chars<br>";
            if (!this.TXTConfirmPassword.IsValidInput(InputType.Password)) ErrorMessage += "Confirm Password - Invalid password chars<br>";
            if (!this.TXTName.IsValidInput(InputType.Name)) ErrorMessage += "Name - 2 to 50 chars<br>";
            if (!string.IsNullOrEmpty(this.TXTCell.Text.Trim()) && !this.TXTCell.IsValidInput(InputType.Mobile)) ErrorMessage += "Invalid Input - Cell Number<br>";
            if (this.IsSystemUser && DRPType.SelectedValue.ToString().Equals("0")) ErrorMessage += "Select User Type<br/>";
            if (this.IsSystemUser && BusinessLayer.Common.CurrentContext.CurrentUser != null && (this.EntityToEdit!=null && this.EntityToEdit.ID==BusinessLayer.Common.CurrentContext.CurrentUser.ID) && (BusinessLayer.Common.CurrentContext.CurrentUser.Type != (BusinessLayer.Users.UserTypes)int.Parse(DRPType.SelectedValue)))
            {
                ErrorMessage += "Current user can not change his user type<br/>";
            }

            bool Hasgroup = false;
            foreach (ListItem item in LSTGroups.Items)
            {
                if (item.Selected && !this.LSTGroups.SelectedItem.Value.Equals("0"))
                {
                    Hasgroup = true;
                    break;
                }
            }
            if ((!this.IsSystemUser &&!Hasgroup))
                ErrorMessage += "Assign at least one user group<br/>";
            if(this.IsSystemUser && ((BusinessLayer.Users.UserTypes)int.Parse(DRPType.SelectedValue))  == (BusinessLayer.Users.UserTypes.Client) && LSTClients.SelectedItem==null)
                ErrorMessage += "Please select one client for non system user<br/>";
            if (((BusinessLayer.Users.UserTypes)int.Parse(this.DRPType.SelectedValue))== BusinessLayer.Users.UserTypes.Client && !Hasgroup)
                ErrorMessage += "Assign at least one user group<br/>";
            //if(this.IsSystemUser && (BusinessLayer.Users.UserTypes)int.Parse(DRPType.SelectedValue)==BusinessLayer.Users.UserTypes.System && Hasgroup)
            //    ErrorMessage += "System User can not be assigned any group<br/>";
            if (!TXTPassword.Text.Trim().Equals(TXTConfirmPassword.Text)) ErrorMessage += "Password and Confirm Password do not match<br>";

            return !string.IsNullOrEmpty(ErrorMessage);
        }

        protected void BTNSave_Click(object sender, EventArgs e)
        {
            string ErrorMessage = string.Empty;

            if (!ValidateInput(out ErrorMessage))
            {
                BusinessLayer.Users.User obj = PrePareEntity();
                if (obj == null) return;
                this.Save(obj);
                //save Client users
                if (this.EntityToEdit == null && obj.Type == BusinessLayer.Users.UserTypes.Client)//new then use newly inserted user's ID
                    BusinessLayer.Users.UserManager.ManageClientUsers(BusinessLayer.DbOperationMode.Insert, obj.ID, obj.ClientsToSave);
                else//use entitytoedit's id
                {
                    if(!string.IsNullOrEmpty(obj.ClientsToSave) && obj.Type== BusinessLayer.Users.UserTypes.Client)//offie user then only insert to Clientusers table
                        BusinessLayer.Users.UserManager.ManageClientUsers(BusinessLayer.DbOperationMode.Update, this.EntityToEdit.ID, obj.ClientsToSave);
                }

                //save assigned groups to user         
                if((this.EntityToEdit!=null && obj.Type != BusinessLayer.Users.UserTypes.System) || this.EntityToEdit == null)//edit mode and user type is being switched from System to Client - then do not save user allowed groups - "ManageUsers"-SP[Save method] has already assigned him system group OR new user then go for insert
                    BusinessLayer.Users.UserGroupManager.SaveAllowedUserGroups(this.EntityToEdit != null ? this.EntityToEdit.ID : obj.ID, PrepareUserGroups());

                if(this.EntityToEdit !=null && BusinessLayer.Common.CurrentContext.CurrentUser!=null && this.EntityToEdit.ID == BusinessLayer.Common.CurrentContext.CurrentUser.ID)
                    BusinessLayer.Common.CurrentContext.CurrentUser = BusinessLayer.Users.UserManager.GetUserAllowedGroupSections(obj);


                if (this.IsSystemUser || BusinessLayer.Common.CurrentContext.CurrentUser != null && BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections != null && BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections.Count > 0 && BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections[BusinessLayer.Users.Section.SectionCodes.OFC_MGNT].ShowClientAdminData)
                {
                    CheckErrors("~/Users/default.aspx" + MakeQueryString(), "Data has been saved successfully", false, false);
                }
                else
                {
                    CheckErrors("~/DashBoard.aspx" + MakeQueryString(), "Data has been saved successfully", false, false);
                }
            }
            else
            {
                this.DisplayMessage = new WebLogic.ErrorMessage("Invalid Input", ErrorMessage);
            }
        }

        private List<BusinessLayer.Users.UserGroup> PrepareUserGroups()
        {
            //if (this.IsSystemUser)
            {
                if (this.IsSystemUser || (BusinessLayer.Common.CurrentContext.CurrentUser != null && this.EntityToEdit!=null && BusinessLayer.Common.CurrentContext.CurrentUser.ID != this.EntityToEdit.ID))
                {
//                    this.LSTGroups.Attributes.Add("disabled", "disabled");
                    List<BusinessLayer.Users.UserGroup> retObj = new List<BusinessLayer.Users.UserGroup>();
                    foreach (ListItem item in this.LSTGroups.Items)
                    {
                        if (item.Selected && !item.Value.Equals("0"))
                            retObj.Add(new BusinessLayer.Users.UserGroup { ID = int.Parse(item.Value) });
                    }
                    return retObj;
                }
                if (this.EntityToEdit != null)
                {
                    return this.EntityToEdit.Groups;
                }
                return new List<BusinessLayer.Users.UserGroup>();
                
            }
            //else
                //return this.EntityToEdit.Groups;            
        }

        private string MakeQueryString()
        {
            string retString = string.Empty;
            retString += "?name=" + (Request.QueryString["name"] != null ? Request.QueryString["name"].ToString() : string.Empty) + "&";
            retString += "email=" + (Request.QueryString["email"] != null ? Request.QueryString["email"].ToString() : string.Empty) + "&";
            retString += "status=" + (Request.QueryString["status"] != null ? Request.QueryString["status"].ToString() : string.Empty) + "&";
            retString += "type=" + (Request.QueryString["type"] != null ? Request.QueryString["type"].ToString() : string.Empty);
            return retString;
        }

        protected void BTNCancel_Click(object sender, EventArgs e)
        {
            if (this.IsSystemUser || BusinessLayer.Common.CurrentContext.CurrentUser != null && BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections != null && BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections.Count > 0 && BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections[BusinessLayer.Users.Section.SectionCodes.OFC_MGNT].ShowClientAdminData)
            {
                Response.Redirect("~/Users/default.aspx" + MakeQueryString());
            }
            else
            {
                Response.Redirect("~/DashBoard.aspx" + MakeQueryString());
            }
        }

        protected void LSTClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillUserGroups();
        }        
    }
}