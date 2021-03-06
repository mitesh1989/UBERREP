﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer.Common;
using UBERREP.BusinessLayer.Common;

namespace UBERREP.Admin
{
    public partial class Login : WebLogic.WebPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //added to login when return key is hit - as hidden image button is made default
            this.Page.Form.DefaultButton = BTNLogin.UniqueID;
            this.TXTUserName.Focus();
        }

        public override bool ValidateInput(out string errorMessage)
        {
            errorMessage = string.Empty;
            
            if (!this.TXTUserName.IsValidInput(InputType.Username)) errorMessage += "Invalid Input - UserName";
            if (!this.TXTPassword.IsValidInput(InputType.Password)) errorMessage += "Invalid Input - Password";

            return !string.IsNullOrEmpty(errorMessage);
        }

        protected void BTNLogin_Click(object sender, EventArgs e)
        {
            string errMessage = string.Empty;
            if (this.ValidateInput(out errMessage))
            {
                this.DisplayMessage = new UBERREP.Admin.WebLogic.ErrorMessage("Invalid Input", errMessage);
            }
            else
            {
                UBERREP.BusinessLayer.Users.User loggedInUser = UBERREP.BusinessLayer.Users.UserManager.ValidateUser(this.TXTUserName.Text.Trim(), this.TXTPassword.Text.Trim());
                

                if (loggedInUser != null)
                {// username and password validated
                    switch (loggedInUser.Status)
                    {
                        case Status.Active:
                            {
                                CurrentContext.CurrentUser = loggedInUser;
                                loggedInUser = UBERREP.BusinessLayer.Users.UserManager.GetUserAllowedGroupSections(loggedInUser);
                                Response.Redirect("~/DashBoard.aspx", true);
                                break;
                            }
                        case Status.Suspended:
                            {
                                this.DisplayMessage = new UBERREP.Admin.WebLogic.ErrorMessage("User account suspended", "Please contact site administrator");
                                break;
                            }
                        case Status.Deleted:
                            {
                                this.DisplayMessage = new UBERREP.Admin.WebLogic.ErrorMessage("User account deleted", "Please contact site administrator");
                                break;
                            }
                    }
                }
                else
                {
                    TXTPassword.Focus();
                    this.DisplayMessage = new UBERREP.Admin.WebLogic.ErrorMessage("Invalid Credentials", "Please enter correct username or password");
                }
            }
        }

        [System.Web.Services.WebMethod]
        public static string ForgotPassword(string email)
        {             
            string retMessage = string.Empty;
            //if (!string.IsNullOrEmpty(email))
            //{
            //    System.Collections.Specialized.NameValueCollection data = new System.Collections.Specialized.NameValueCollection();
            //    data.Add("username", email);
            //    BusinessLayer.Users.User resetUser = BusinessLayer.Users.UserManager.GetUsers(data).FirstOrDefault();
            //    if (resetUser != null && !string.IsNullOrEmpty(resetUser.Password))
            //    {
            //        CommonLayer.EmailAccountSettings emailAccountCollection = System.Configuration.ConfigurationManager.GetSection("EmailSettings/SystemEmails") as CommonLayer.EmailAccountSettings;
            //        // we don't have values in configuration
            //        if (emailAccountCollection == null) return "";
            //        // error from
            //        CommonLayer.EmailAccount sender = emailAccountCollection.EmailAccounts["support"];
            //        try
            //        {
            //            System.Net.Mail.MailAddress from = sender == null ? new System.Net.Mail.MailAddress("support@odysseussolutions.com") : sender;

            //            if (from != null)
            //            {
            //                CommonLayer.EmailManager.SendMail(from.Address, resetUser.Email, "Password", "Your Password for Admin Login is : " + resetUser.Password, null);
            //                retMessage = "Email has been sent to registered email (" + resetUser.Email + ") for username provided";
            //            }                        
            //        }
            //        catch (Exception ex) { }
            //    }
            //    else
            //    {
            //        retMessage = "Username not registered";
            //    }
            //}
            //else
            //    retMessage = "Provide Username";

            return retMessage;        
        }
 
    }
}