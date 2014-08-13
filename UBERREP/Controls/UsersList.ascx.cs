using BusinessLayer.Common;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace UBERREP.Controls
{
    public partial class UsersList : System.Web.UI.UserControl
    {
        private List<BusinessLayer.Users.User> userList;
        private BusinessLayer.Users.UserTypes userType;
        public List<BusinessLayer.Users.User> UserList
        {
            get { return this.userList; }
            set { this.userList = value; }
        }
        public BusinessLayer.Users.UserTypes UserType
        {
            get { return this.userType; }
            set { this.userType = value; }
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

        protected void BTNSaveToPDF_Click(object sender, EventArgs e)
        {
            Response.AddHeader("content-disposition", "attachment;filename=Export.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            HtmlForm frm = new HtmlForm();
            UserListRPT.Parent.Controls.Add(frm);
            frm.Attributes["runat"] = "server";
            frm.Controls.Add(UserListRPT);
            frm.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
        }


        private void UserListRPT_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
           
            switch (e.CommandName)
            {
                case "Delete":
                    {
                        BusinessLayer.Users.User obj = new BusinessLayer.Users.User();
                        obj.ID = Convert.ToInt16(e.CommandArgument);
                        BusinessLayer.Users.UserManager.Delete(obj);
                        this.BindData();
                        ShowMessage("Record Deleted Successfully");
                        break;
                    }
                case "Insert":
                    {
                        e.Item.FindControl("insertPnl").Visible = true;
                        break;
                    }
                case "InsertCancel":
                    {
                        e.Item.FindControl("insertPnl").Visible = false;
                        break;
                    }
                case "Edit":
                    {
                        e.Item.FindControl("lnk_Update").Visible = true;
                        e.Item.FindControl("lnk_delete").Visible = false;
                        e.Item.FindControl("lnk_Cancel").Visible = true;
                        e.Item.FindControl("lnk_Edit").Visible = false;
                        e.Item.FindControl("Lit_usename").Visible = false;
                        e.Item.FindControl("TXT_username").Visible = true;
                        e.Item.FindControl("lit_Point").Visible = false;
                        e.Item.FindControl("txt_Point").Visible = true;
                        e.Item.FindControl("lit_Notes").Visible = false;
                        e.Item.FindControl("txt_Notes").Visible = true;
                        e.Item.FindControl("lit_FullName").Visible = false;
                        e.Item.FindControl("txt_FullName").Visible = true;
                        break;
                    }
                case "Cancel":
                    {
                        e.Item.FindControl("lnk_Update").Visible = false;
                        e.Item.FindControl("lnk_delete").Visible = true;
                        e.Item.FindControl("lnk_Cancel").Visible = false;
                        e.Item.FindControl("lnk_Edit").Visible = true;
                        e.Item.FindControl("Lit_usename").Visible = true;
                        e.Item.FindControl("TXT_username").Visible = false;
                        e.Item.FindControl("lit_Point").Visible = true;
                        e.Item.FindControl("txt_Point").Visible = false;
                        e.Item.FindControl("lit_Notes").Visible = true;
                        e.Item.FindControl("txt_Notes").Visible = false;
                        e.Item.FindControl("lit_FullName").Visible = true;
                        e.Item.FindControl("txt_FullName").Visible = false;
                        break;
                    }
                case "Update":
                    {
                        BusinessLayer.Users.User obj = new BusinessLayer.Users.User();
                        obj.ID = Convert.ToInt16(e.CommandArgument);
                        obj.Type = BusinessLayer.Users.UserTypes.Retailer;
                        obj.Username = ((System.Web.UI.WebControls.TextBox)(e.Item.FindControl("TXT_username"))).Text;
                        obj.Name = ((System.Web.UI.WebControls.TextBox)(e.Item.FindControl("txt_FullName"))).Text;
                        //obj.RecordNumber = ((System.Web.UI.WebControls.TextBox)(e.Item.FindControl("txt_Point"))).Text;
                        //obj.Type = ((System.Web.UI.WebControls.TextBox)(e.Item.FindControl("txt_Notes"))).Text;
                        BusinessLayer.Users.UserManager.Update(obj);
                        this.BindData();
                        ShowMessage("Record Updated Successfully");
                        break;
                    }
                case "Save":
                    {
                        BusinessLayer.Users.User retObj = new BusinessLayer.Users.User();
                    //  retObj.Email = retObj.Username = ((System.Web.UI.WebControls.TextBox)(e.Item.FindControl("TXT_username"))).Text;
                        retObj.Name = ((System.Web.UI.WebControls.TextBox)(e.Item.FindControl("txt_ins_Username"))).Text; 
                        retObj.Password="123456";
                        retObj.Username = ((System.Web.UI.WebControls.TextBox)(e.Item.FindControl("txt_ins_Fullname"))).Text;
                        retObj.Type = userType;
                        retObj.Status = Status.Active;
                        this.BindData();
                        ShowMessage("Record Inserted Successfully");
                        break;
                    }
            }
 
        }
        #endregion


        #region AlertMessage
        public void ShowMessage(string message) { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('"+message+"')", true); }
        #endregion

    }
}