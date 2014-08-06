using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer.Common;
using UBERREP.BusinessLayer.Common;

namespace UBERREP.Admin.ClientManagement
{
    public partial class Client : WebLogic.WebPage
    {
        public int ClientID
        {
            get
            {
                string sid = Request.QueryString["sid"];
                return !string.IsNullOrEmpty(sid) ? int.Parse(sid) : 0;
            }
        }

        private BusinessLayer.Client.Client entityToEdit = null;
        public BusinessLayer.Client.Client EntityToEdit
        {
            get
            {
                if (this.entityToEdit != null) return this.entityToEdit;
                if (this.ClientID != 0)
                {
                    this.entityToEdit = BusinessLayer.Client.ClientManager.Get(this.ClientID);
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

        protected void Page_Load(object sender, EventArgs e)
        {
            // on postback we loose password textbox values            
            string password = Request[TXTPassword.UniqueID];
            if (!string.IsNullOrEmpty(password)) this.TXTPassword.Attributes.Add("value", password);

            string confirmpassword = Request[this.TXTConfirmPassword.UniqueID];
            if (!string.IsNullOrEmpty(confirmpassword)) this.TXTConfirmPassword.Attributes.Add("value", confirmpassword);

            this.FillEnumTypes(this.DRPStatus, null, typeof(Status), null);
            if (!Page.IsPostBack)
            {
            
            }
            if (this.EntityToEdit != null && !IsPostBack)
            {
                this.DisplayEntity();
            }
        }
        

        private void DisplayEntity()
        {
            // Client information
            this.TXTClientName.Text = this.EntityToEdit.Name;


            
            
            this.DRPStatus.SelectedValue = ((int)this.EntityToEdit.Status).ToString();
            
            //extract contact info
            if (this.EntityToEdit != null && this.EntityToEdit.ContactInfo != null)
            {
                this.TXTCellNo.Text = this.EntityToEdit.ContactInfo.CellNumber;
                this.TXTEmail.Text = this.EntityToEdit.ContactInfo.Email;
            }

            string clientLogo = string.Empty;
            //extract general Client information - if any            
            if (this.EntityToEdit != null && this.EntityToEdit.GeneralClientInfo != null)
            {
                if (this.EntityToEdit.GeneralClientInfo.ContainsKey(BusinessLayer.Client.Client.GeneralClientInformation.IATANumber.ToString()))
                    this.TXTIATA.Text = this.EntityToEdit.GeneralClientInfo[BusinessLayer.Client.Client.GeneralClientInformation.IATANumber.ToString()].ToString();
                if (this.EntityToEdit.GeneralClientInfo.ContainsKey(BusinessLayer.Client.Client.GeneralClientInformation.IATANumber.ToString()))
                    this.TXTARC.Text = this.EntityToEdit.GeneralClientInfo[BusinessLayer.Client.Client.GeneralClientInformation.ARCNumber.ToString()].ToString();
                if (this.EntityToEdit.GeneralClientInfo.ContainsKey(BusinessLayer.Client.Client.GeneralClientInformation.CallBackURL.ToString()))
                    this.TXTCallBackURL.Text = this.EntityToEdit.GeneralClientInfo[BusinessLayer.Client.Client.GeneralClientInformation.CallBackURL.ToString()].ToString();

                if (this.EntityToEdit.GeneralClientInfo.ContainsKey(BusinessLayer.Client.Client.GeneralClientInformation.EmailSubject.ToString()))
                    this.TXTEmailSubject.Text = this.EntityToEdit.GeneralClientInfo[BusinessLayer.Client.Client.GeneralClientInformation.EmailSubject.ToString()].ToString();
                if (this.EntityToEdit.GeneralClientInfo.ContainsKey(BusinessLayer.Client.Client.GeneralClientInformation.PaymentEmailSubject.ToString()))
                    this.TXTPaymentSubject.Text = this.EntityToEdit.GeneralClientInfo[BusinessLayer.Client.Client.GeneralClientInformation.PaymentEmailSubject.ToString()].ToString();
                if (this.EntityToEdit.GeneralClientInfo.ContainsKey(BusinessLayer.Client.Client.GeneralClientInformation.EmailBCC.ToString()))
                    this.TXTBCCReceipent.Text = this.EntityToEdit.GeneralClientInfo[BusinessLayer.Client.Client.GeneralClientInformation.EmailBCC.ToString()].ToString();
                if (this.EntityToEdit.GeneralClientInfo.ContainsKey(BusinessLayer.Client.Client.GeneralClientInformation.EmailCC.ToString()))
                    this.TXTCCReceipent.Text = this.EntityToEdit.GeneralClientInfo[BusinessLayer.Client.Client.GeneralClientInformation.EmailCC.ToString()].ToString();

                if (this.EntityToEdit.GeneralClientInfo.ContainsKey(BusinessLayer.Client.Client.GeneralClientInformation.PrivacyPolicyURL.ToString()))
                    this.TXTPrivacyPolicy.Text = this.EntityToEdit.GeneralClientInfo[BusinessLayer.Client.Client.GeneralClientInformation.PrivacyPolicyURL.ToString()].ToString();

                if (this.EntityToEdit.GeneralClientInfo.ContainsKey(BusinessLayer.Client.Client.GeneralClientInformation.CustomerSupportEmail.ToString()))
                    this.TXTSupportEmail.Text = this.EntityToEdit.GeneralClientInfo[BusinessLayer.Client.Client.GeneralClientInformation.CustomerSupportEmail.ToString()].ToString();

                if (this.EntityToEdit.GeneralClientInfo.ContainsKey(BusinessLayer.Client.Client.GeneralClientInformation.CustomerSupportPhone.ToString()))
                    this.TXTSupportPhone.Text = this.EntityToEdit.GeneralClientInfo[BusinessLayer.Client.Client.GeneralClientInformation.CustomerSupportPhone.ToString()].ToString();

                if (this.EntityToEdit.GeneralClientInfo.ContainsKey(BusinessLayer.Client.Client.GeneralClientInformation.ClientWebSiteURL.ToString()))
                    this.TXTWebsiteURL.Text = this.EntityToEdit.GeneralClientInfo[BusinessLayer.Client.Client.GeneralClientInformation.ClientWebSiteURL.ToString()].ToString();


                clientLogo = this.EntityToEdit.GeneralClientInfo.ContainsKey(BusinessLayer.Client.Client.GeneralClientInformation.ClientLogo.ToString()) ? this.EntityToEdit.GeneralClientInfo[BusinessLayer.Client.Client.GeneralClientInformation.ClientLogo.ToString()].ToString() : string.Empty;
                
                
            }
        }

        private void Save(BusinessLayer.Client.Client obj)
        {
            if (this.ClientID == 0) BusinessLayer.Client.ClientManager.Create(obj);
            else BusinessLayer.Client.ClientManager.Update(obj);
        }

        protected void BTNSave_Click(object sender, EventArgs e)
        {
            string ErrorMessage = string.Empty;
            if (!ValidateInput(out ErrorMessage))
            {
                BusinessLayer.Client.Client entityToSave = this.PrePareEntity();
                this.Save(entityToSave);
                CheckErrors("~/ClientManagement/Default.aspx", "Client has been saved successfully", false, false);
            }
            else
            {
                this.DisplayMessage = new WebLogic.ErrorMessage("Invalid Input", ErrorMessage);
            }
        }

        private string MakeQueryString()
        {
            string retString = string.Empty;
            retString += "?name=" + (Request.QueryString["name"] != null ? Request.QueryString["name"].ToString() : string.Empty) + "&";
            retString += "status=" + (Request.QueryString["status"] != null ? Request.QueryString["status"].ToString() : string.Empty) + "&";
            retString += "apiid=" + (Request.QueryString["apiid"] != null ? Request.QueryString["apiid"].ToString() : string.Empty);
            return retString;
        }

        private BusinessLayer.Client.Client PrePareEntity()
        {

            string clientLogo = string.Empty;
            if (this.EntityToEdit != null && this.EntityToEdit.GeneralClientInfo.ContainsKey(BusinessLayer.Client.Client.GeneralClientInformation.ClientLogo.ToString()) && !string.IsNullOrWhiteSpace(this.EntityToEdit.GeneralClientInfo[BusinessLayer.Client.Client.GeneralClientInformation.ClientLogo.ToString()].ToString()))
                clientLogo = this.EntityToEdit.GeneralClientInfo[BusinessLayer.Client.Client.GeneralClientInformation.ClientLogo.ToString()].ToString();

            BusinessLayer.Client.Client retObj = this.EntityToEdit != null ? this.EntityToEdit : new BusinessLayer.Client.Client();

            retObj.Name         = this.TXTClientName.Text.Trim();

            //populate contact Information
            retObj.ContactInfo  = new ContactInfo();

            retObj.Status       = (Status)int.Parse(DRPStatus.SelectedValue);
            
            //retObj.SMSAPIID     = int.Parse(DRPSMSAPI.SelectedValue);

            //add general information for site avaiable in BusinessLayer.Sites.Site.GeneralSiteInformation - enum                                    
            retObj.GeneralClientInfo = new CommonLayer.GenericProperties();

            if (!string.IsNullOrEmpty(TXTIATA.Text.Trim())) retObj.GeneralClientInfo.Add(BusinessLayer.Client.Client.GeneralClientInformation.IATANumber.ToString(), TXTIATA.Text.Trim());
            if (!string.IsNullOrEmpty(TXTARC.Text.Trim())) retObj.GeneralClientInfo.Add(BusinessLayer.Client.Client.GeneralClientInformation.ARCNumber.ToString(), TXTARC.Text.Trim());
            if (!string.IsNullOrEmpty(TXTCallBackURL.Text.Trim())) retObj.GeneralClientInfo.Add(BusinessLayer.Client.Client.GeneralClientInformation.CallBackURL.ToString(), TXTCallBackURL.Text.Trim());
            
            if (!string.IsNullOrEmpty(TXTBCCReceipent.Text.Trim())) retObj.GeneralClientInfo.Add(BusinessLayer.Client.Client.GeneralClientInformation.EmailBCC.ToString(), TXTBCCReceipent.Text.Trim());
            if (!string.IsNullOrEmpty(TXTCCReceipent.Text.Trim())) retObj.GeneralClientInfo.Add(BusinessLayer.Client.Client.GeneralClientInformation.EmailCC.ToString(), TXTCCReceipent.Text.Trim());
            if (!string.IsNullOrEmpty(TXTEmailSubject.Text.Trim())) retObj.GeneralClientInfo.Add(BusinessLayer.Client.Client.GeneralClientInformation.EmailSubject.ToString(), TXTEmailSubject.Text.Trim());
            if (!string.IsNullOrEmpty(TXTPaymentSubject.Text.Trim())) retObj.GeneralClientInfo.Add(BusinessLayer.Client.Client.GeneralClientInformation.PaymentEmailSubject.ToString(), TXTPaymentSubject.Text.Trim());
            if (!string.IsNullOrEmpty(TXTPrivacyPolicy.Text.Trim())) retObj.GeneralClientInfo.Add(BusinessLayer.Client.Client.GeneralClientInformation.PrivacyPolicyURL.ToString(), TXTPrivacyPolicy.Text.Trim());
            if (!string.IsNullOrEmpty(TXTSupportEmail.Text.Trim())) retObj.GeneralClientInfo.Add(BusinessLayer.Client.Client.GeneralClientInformation.CustomerSupportEmail.ToString(), TXTSupportEmail.Text.Trim());
            if (!string.IsNullOrEmpty(TXTSupportPhone.Text.Trim())) retObj.GeneralClientInfo.Add(BusinessLayer.Client.Client.GeneralClientInformation.CustomerSupportPhone.ToString(), TXTSupportPhone.Text.Trim());
            if (!string.IsNullOrEmpty(TXTWebsiteURL.Text.Trim())) retObj.GeneralClientInfo.Add(BusinessLayer.Client.Client.GeneralClientInformation.ClientWebSiteURL.ToString(), TXTWebsiteURL.Text.Trim());

            //add general information for site avaiable in BusinessLayer.Sites.Site.GeneralSiteInformation - enum                        
            if (this.EntityToEdit == null || this.EntityToEdit.GeneralClientInfo == null)//new Client or Client with no contact info
                retObj.ContactInfo = new ContactInfo();


            

            retObj.ContactInfo.CellNumber   = TXTCellNo.Text;
            retObj.ContactInfo.Email        = TXTEmail.Text;
            

            return retObj;
        }

        protected bool IsLogoExists()
        {
            if (this.EntityToEdit == null) return false;
            try
            {
                string filePath = this.EntityToEdit.GeneralClientInfo[BusinessLayer.Client.Client.GeneralClientInformation.ClientLogo.ToString()].ToString();
                if (!filePath.Contains("http") && !string.IsNullOrEmpty(filePath)) return System.IO.File.Exists(Server.MapPath("~/Theme/Logos/ClientLogos/" + filePath));
                else return false;
            }
            catch (Exception ex) { return false; }
        }


        protected void LNKRemoveImage_Click(object sender, EventArgs e)
        {
            if (this.IsLogoExists())
            {
                System.IO.File.Delete(Server.MapPath("~/Theme/Logos/ClientLogos/" + this.EntityToEdit.GeneralClientInfo[BusinessLayer.Client.Client.GeneralClientInformation.ClientLogo.ToString()]));
                this.EntityToEdit.GeneralClientInfo[BusinessLayer.Client.Client.GeneralClientInformation.ClientLogo.ToString()] = string.Empty;
                
            }
        }



        protected void BTNCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ClientManagement/Default.aspx" + ReceivedQueryString);            
        }

        public override bool ValidateInput(out string ErrorMessage)
        {
            ErrorMessage = string.Empty;
            if (this.TXTClientName.Text.Trim().Equals(string.Empty)) ErrorMessage += "Required Field - Name<br/>";
            if (this.DRPAPI.SelectedValue.ToString().Equals("0")) ErrorMessage += "Required Field - Payment API<br/>";
            if (!this.TXTCellNo.Text.Trim().Equals(string.Empty))
            {
                if (!this.TXTCellNo.IsValidInput(InputType.Mobile)) ErrorMessage += "Invalid Input - Cell Number<br/>";
            }
            if (!this.TXTEmail.Text.Trim().Equals(string.Empty))
            {
                if (!this.TXTEmail.IsValidInput(InputType.EMailAddress)) ErrorMessage += "Invalid Input - Email Address<br/>";
            }
            else
                ErrorMessage += "Required Field - Email<br/>";

            

            if (this.DRPEmailAPI.SelectedValue.Equals("0")) ErrorMessage += "Required Field - Email API<br/>";

            if (TXTClientID.Text.Trim().Equals("")) ErrorMessage += "Required Field - ClientID<br/>";
            if (TXTPassword.Text.Trim().Equals("")) ErrorMessage += "Required Field - Password<br/>";
            else if(!TXTPassword.Text.Trim().Equals(TXTConfirmPassword.Text)) ErrorMessage += "Password and Confirm Password do not match<br/>";

            
            
            return !string.IsNullOrEmpty(ErrorMessage);
        }
    }
}