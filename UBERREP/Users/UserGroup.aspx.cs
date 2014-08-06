using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer.Common;

namespace UBERREP.Admin.Users
{    
    public partial class UserGroup : WebLogic.WebPage
    {
        public int GroupID
        {
            get
            {
                string gid = Request.QueryString["gid"];
                return !string.IsNullOrEmpty(gid) ? int.Parse(gid) : 0;
            }
        }

        private BusinessLayer.Users.UserGroup entityToEdit = null;
        public BusinessLayer.Users.UserGroup EntityToEdit
        {
            get
            {
                if (this.entityToEdit != null) return this.entityToEdit;
                if (this.GroupID != 0)
                {
                    this.entityToEdit = BusinessLayer.Users.UserGroupManager.Get(this.GroupID);
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

        //to maintain Section information
        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
            this.EntityToBind = this.ViewState["EntityToBind"] != null ? this.ViewState["EntityToBind"] as List<BusinessLayer.Users.GroupSectionRole> : null;
            this.RPTRSectionRights.DataSource = this.ViewState["DataSource"] != null ? this.ViewState["DataSource"] as List<BusinessLayer.Users.GroupSectionRole> : null;
        }

        protected override object SaveViewState()
        {
            this.ViewState.Add("EntityToBind", this.EntityToBind);
            this.ViewState.Add("DataSource",this.RPTRSectionRights.DataSource);
            return base.SaveViewState();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            this.FillEnumTypes(this.DRPStatus, null, typeof(Status), null);          

            if(!Page.IsPostBack)
                BindSectionRights();           
            if (!Page.IsPostBack)
                FillClients(DRPClient, "--Select--", "0", "0");
            if (this.EntityToEdit != null && !IsPostBack)
            {
                this.DisplayEntity();
            }
        }

        List<BusinessLayer.Users.GroupSectionRole> EntityToBind=new List<BusinessLayer.Users.GroupSectionRole>();
        private void BindSectionRights()
        {
             this.EntityToBind = new List<BusinessLayer.Users.GroupSectionRole>();
            //get all system sections - null
            List<BusinessLayer.Users.Section> systemsections = BusinessLayer.Users.SectionManager.GetSections(null);                    
            //get group Specific sections
            System.Collections.Specialized.NameValueCollection data = new System.Collections.Specialized.NameValueCollection();
            data.Add("GroupID", this.GroupID.ToString());
            List<BusinessLayer.Users.GroupSectionRole> GroupSections = BusinessLayer.Users.GroupSectionRoleManager.GetGroupSectionRoles(data);

            foreach (BusinessLayer.Users.Section sysSection in systemsections)
            {
                BusinessLayer.Users.GroupSectionRole GroupSection = GroupSections.FirstOrDefault(c => c.Section.ID == sysSection.ID);
                if (GroupSection != null) this.EntityToBind.Add(GroupSection);
                else this.EntityToBind.Add(new BusinessLayer.Users.GroupSectionRole() { SectionID = sysSection.ID, Role = new BusinessLayer.Users.Role() });
            }
            if (this.EntityToBind != null && this.EntityToBind.Count != 0)//no system section and no group section then don't bind data
            {
                this.RPTRSectionRights.DataSource = this.EntityToBind;
                this.RPTRSectionRights.DataBind();
            }
            else
            {
                this.RPTRSectionRights.DataSource = null;
                this.RPTRSectionRights.DataBind();
            }
        }

        private void DisplayEntity()
        {
            // Groups information
            this.TXTGroupName.Text = this.EntityToEdit.Name;
            this.DRPStatus.SelectedValue = ((int)this.EntityToEdit.Status).ToString();
            DRPClient.SelectedValue = this.EntityToEdit.ClientID.ToString();
            CHKIsSystemGroup.Checked = this.EntityToEdit.IsSystemGroup;
        }

        private void Save(BusinessLayer.Users.UserGroup obj)
        {
            if (this.GroupID == 0) BusinessLayer.Users.UserGroupManager.Create(obj);
            else BusinessLayer.Users.UserGroupManager.Update(obj);
        }

        protected void BTNSave_Click(object sender, EventArgs e)
        {
            string ErrorMessage = string.Empty;
            if (!ValidateInput(out ErrorMessage))
            {
                BusinessLayer.Users.UserGroup entityToSave = this.PrePareEntity();
                this.Save(entityToSave);                
                BusinessLayer.Users.GroupSectionRoleManager.SaveGroupSectionRoles(entityToSave.ID, PrepareGroupSectionRole());
                BusinessLayer.Common.CurrentContext.CurrentUser = BusinessLayer.Users.UserManager.GetUserAllowedGroupSections(BusinessLayer.Common.CurrentContext.CurrentUser);
                CheckErrors("~/Users/UserGroups.aspx", "User Group has been saved successfully", false, false);
            }
            else
            {
                this.DisplayMessage = new WebLogic.ErrorMessage("Invalid Input", ErrorMessage);
            }
        }

        private List<BusinessLayer.Users.GroupSectionRole> PrepareGroupSectionRole()
        {
            List<BusinessLayer.Users.GroupSectionRole> entityToSave = new List<BusinessLayer.Users.GroupSectionRole>();

            if (this.RPTRSectionRights.DataSource != null && this.RPTRSectionRights.Items.Count > 0)
            {
                foreach (RepeaterItem item in this.RPTRSectionRights.Items)
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        CheckBox CHKHasInsert = item.FindControl("CHKHasInsert") as CheckBox;
                        CheckBox CHKHasUpdate = item.FindControl("CHKHasUpdate") as CheckBox;
                        CheckBox CHKDelete = item.FindControl("CHKDelete") as CheckBox;
                        CheckBox CHKView = item.FindControl("CHKView") as CheckBox;
                        //CheckBox CHKShowAdminData = item.FindControl("CHKShowAdminData") as CheckBox;
                        CheckBox CHKShowEncryptedData = item.FindControl("CHKShowEncryptedData") as CheckBox;                        

                        if (CHKHasInsert != null && CHKDelete!=null && CHKHasUpdate!=null && CHKView!=null &&  CHKShowEncryptedData!=null) //CHKShowAdminData!=null)
                        {
                            BusinessLayer.Users.GroupSectionRole c = this.EntityToBind[item.ItemIndex];
                            if (c != null)
                            {
                                entityToSave.Add(new BusinessLayer.Users.GroupSectionRole { SectionID = c.SectionID, Role = new BusinessLayer.Users.Role() { HasDelete = CHKDelete.Checked, HasInsert = CHKHasInsert.Checked, HasUpdate = CHKHasUpdate.Checked, HasView = CHKView.Checked, ShowEncryptedData = CHKShowEncryptedData.Checked } });
                            }
                        }
                    }
                }
            }

            return entityToSave;
        }


        private string MakeQueryString()
        {
            string retString = string.Empty;
            retString += "?ClientID=" + (Request.QueryString["ClientID"] != null ? Request.QueryString["ClientID"].ToString() : string.Empty) + "&";
            retString += "status=" + (Request.QueryString["status"] != null ? Request.QueryString["status"].ToString() : string.Empty) + "&";            
            return retString;
        }

        private BusinessLayer.Users.UserGroup PrePareEntity()
        {
            BusinessLayer.Users.UserGroup retObj = this.EntityToEdit != null ? this.EntityToEdit : new BusinessLayer.Users.UserGroup();

            retObj.Name = this.TXTGroupName.Text.Trim();

            retObj.Status = (Status)int.Parse(DRPStatus.SelectedValue);
            retObj.ClientID = DRPClient.SelectedValue.ToString();
            retObj.IsSystemGroup = CHKIsSystemGroup.Checked;
            
            return retObj;
        }

        protected void BTNCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Users/UserGroups.aspx" + ReceivedQueryString);
        }

        public override bool ValidateInput(out string ErrorMessage)
        {
            ErrorMessage = string.Empty;
            if (this.TXTGroupName.Text.Trim().Equals(string.Empty))  ErrorMessage += "Required Field - Name<br/>";
            if (this.DRPClient.SelectedValue.ToString().Equals("0")) ErrorMessage += "Required Field - Client<br/>";
            
            return !string.IsNullOrEmpty(ErrorMessage);
        }

        protected void RPTRSectionRights_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                BusinessLayer.Users.GroupSectionRole obj = e.Item.DataItem as BusinessLayer.Users.GroupSectionRole;
                if (BusinessLayer.Common.CurrentContext.CurrentUser.Type == BusinessLayer.Users.UserTypes.Client && obj.Section.Type == BusinessLayer.Users.UserTypes.System)
                    e.Item.Visible = false;
            }
        }               
    }
}