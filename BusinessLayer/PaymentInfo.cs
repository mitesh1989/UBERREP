using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UBERREP.BusinessLayer.Payment
{
    public class PaymentInfo
    {
        public int ID;
        public CreditCard CreditCard;
        public bool AutoPayment, EmailMonthly;

        public static void ManagePayment(Users.User User,DbOperationMode Mode)
        {
            int currentUserID = UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser != null ? UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.ID : BusinessLayer.Users.User.DefaultSystemUserID;
            List<System.Data.SqlClient.SqlParameter> sParameters = new List<System.Data.SqlClient.SqlParameter>();            

            sParameters.Add(new System.Data.SqlClient.SqlParameter("@mode", Mode));
            sParameters.Add(new System.Data.SqlClient.SqlParameter("@paymentid", User.PaymentInfo.ID));
            sParameters.Add(new System.Data.SqlClient.SqlParameter("@BankName", User.PaymentInfo.CreditCard.BankName));
            sParameters.Add(new System.Data.SqlClient.SqlParameter("@CVV",User.PaymentInfo.CreditCard.CVV));
            sParameters.Add(new System.Data.SqlClient.SqlParameter("@ExpireDate", User.PaymentInfo.CreditCard.ExpiryDate));
            sParameters.Add(new System.Data.SqlClient.SqlParameter("@CCNUmber", User.PaymentInfo.CreditCard.Number));
            sParameters.Add(new System.Data.SqlClient.SqlParameter("@autopayment", User.PaymentInfo.AutoPayment));
            sParameters.Add(new System.Data.SqlClient.SqlParameter("@emailmonthly", User.PaymentInfo.EmailMonthly));
            sParameters.Add(new System.Data.SqlClient.SqlParameter("@userID", User.ID));
            sParameters.Add(new System.Data.SqlClient.SqlParameter("@cardholdername", User.PaymentInfo.CreditCard.HolderName));

            if (Mode != BusinessLayer.DbOperationMode.Select)
            {
                if (Mode == BusinessLayer.DbOperationMode.Insert)
                {
                    object id = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar("ManagePaymentInfo", sParameters);
                    if (id != null) User.PaymentInfo.ID= int.Parse(id.ToString());
                }
                else Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery("ManagePaymentInfo", sParameters);
            }            
        }
    }
    public class CreditCard
    {
        public string Number, CVV, ExpiryDate, BankName,HolderName;
    }
    public enum PaymentPreference
    {
        Unknown = 0,
        AutoPay = 1,
        MonthlyEmail = 2
    }
}

    