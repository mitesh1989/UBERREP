﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UBERREP.Controls
{
    public partial class MyAccount : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                DisplayData();
            }
        }
        private void SaveData()
        {
            BusinessLayer.Users.User obj = new BusinessLayer.Users.User();

            obj.Username = TXTUsername.Text;
            obj.Password = TXTPassword.Text;
            obj.Remarks = TXTRemarks.Text;

            obj.Name = TXTName.Text;


            obj.ContactData = new global::BusinessLayer.Common.ContactInfo();
            obj.ContactData.Address = new global::BusinessLayer.Common.Address();
            obj.ContactData.Address.LineOne = TXTAddress.Text;
            obj.ContactData.Address.City = new global::BusinessLayer.Common.Location();
            obj.ContactData.Address.City.Name = TXTCity.Text;
            obj.ContactData.Email = TXTEmail.Text;
            obj.ContactData.ClientPhone = TXTPhone.Text;
            obj.Remarks = TXTRemarks.Text;
            if (RDOMale.Checked)
                obj.Gender = BusinessLayer.Users.Gender.Male;
            else
                obj.Gender = BusinessLayer.Users.Gender.Female;

            BusinessLayer.Users.UserManager.Update(obj);

            obj.PaymentInfo = new BusinessLayer.Payment.PaymentInfo();
            obj.PaymentInfo.CreditCard = new BusinessLayer.Payment.CreditCard();
            obj.PaymentInfo.CreditCard.BankName = TXTBankName.Text;
            obj.PaymentInfo.CreditCard.CVV = TXTCVC.Text;
            obj.PaymentInfo.CreditCard.ExpiryDate = TXTMonth.Text + TXTYear.Text;
            obj.PaymentInfo.CreditCard.HolderName = TXTHolderName.Text;
            obj.PaymentInfo.CreditCard.Number = TXTCCNumber.Text;

            BusinessLayer.Payment.PaymentInfo.ManagePayment(obj, BusinessLayer.DbOperationMode.Update);

        }
        private void DisplayData()
        {



            BusinessLayer.Users.User obj = BusinessLayer.Users.UserManager.Get(BusinessLayer.Common.CurrentContext.CurrentUser != null ? BusinessLayer.Common.CurrentContext.CurrentUser.ID : BusinessLayer.Users.User.DefaultSystemUserID);
            if (obj.ContactData != null && obj.ContactData.Address != null)
            {
                TXTAddress.Text = obj.ContactData.Address.LineOne;
                TXTCity.Text = obj.ContactData.Address.City != null ? obj.ContactData.Address.City.Name : string.Empty;
                TXTEmail.Text = obj.ContactData.Email;
                TXTPhone.Text = obj.ContactData.ClientPhone;
            }

            TXTConfirmPassword.Text = TXTPassword.Text = obj.Password;

            TXTName.Text = obj.Name;

            TXTUsername.Text = obj.Username;
            TXTRemarks.Text = obj.Remarks;

            if (obj.PaymentInfo != null)
            {
                if (obj.PaymentInfo.CreditCard != null)
                {
                    TXTBankName.Text = obj.PaymentInfo.CreditCard.BankName;
                    TXTCCNumber.Text = obj.PaymentInfo.CreditCard.Number;
                    TXTCVC.Text = obj.PaymentInfo.CreditCard.CVV;
                    TXTHolderName.Text = obj.PaymentInfo.CreditCard.HolderName;
                    TXTMonth.Text = obj.PaymentInfo.CreditCard.ExpiryDate.Substring(0, 2);
                    TXTYear.Text = obj.PaymentInfo.CreditCard.ExpiryDate.Substring(2, 4);
                }

            }

        }

        protected void BTNSubmit_Click(object sender, EventArgs e)
        {
            SaveData();
        }

    }
}