using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer.Common;

namespace UBERREP.Controls
{
    public partial class SignUp : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private BusinessLayer.Users.User PrePareEntity()
        {
            BusinessLayer.Users.User retObj = new BusinessLayer.Users.User();

                retObj.Email = retObj.Username = this.TXTEmail.Text.Trim();


                retObj.Type = BusinessLayer.Users.UserTypes.Retailer;
            
            
            {
                retObj.Password = this.TXTPassword.Text.Trim();
            }
            
            retObj.Name = this.TXTName.Text.Trim();
            retObj.Email = this.TXTEmail.Text.Trim();

            retObj.Status = Status.Active;


            return retObj;
        }

        protected void BTNSave_Click(object sender, EventArgs e)
        {            
            BusinessLayer.Users.UserManager.Create(PrePareEntity());
            UBERREP.BusinessLayer.Common.EmailManager.SendGMail(TXTEmail.Text.Trim(), "Account Created", "Your account with UBERREP has been created<br/>Thanks", null);

        }

        protected void BTNCancel_Click(object sender, EventArgs e)
        {
            TXTConfirmPassword.Text = TXTEmail.Text = TXTName.Text = TXTPassword.Text = string.Empty;
        }
    }
}