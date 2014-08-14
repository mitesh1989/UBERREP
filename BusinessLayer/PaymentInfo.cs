using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace UBERREP.BusinessLayer.Payment
{
    public class PaymentInfo
    {
        public int ID;
        public CreditCard CreditCard;
        public bool AutoPayment, EmailMonthly;

        public static BusinessLayer.Payment.PaymentInfo Get(int PaymentID)
        {
            PaymentInfo PaymentInfo = null;           

            List<SqlParameter> spparams = new List<SqlParameter>();

            SqlDataReader dataReader = null;

            spparams.Add(new System.Data.SqlClient.SqlParameter("@ID", PaymentID));

            try
            {
                dataReader = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader("GetPaymentInfo", spparams.ToArray());
                if (dataReader != null && dataReader.HasRows)
                {
                    PaymentInfo = new Payment.PaymentInfo();
                    PaymentID = dataReader["PaymentID"] != DBNull.Value ? int.Parse(dataReader["PaymentID"].ToString()) : 0;
                    PaymentInfo.CreditCard = new Payment.CreditCard();
                    PaymentInfo.CreditCard.HolderName = dataReader["HolderName"] != DBNull.Value ? dataReader["HolderName"].ToString() : string.Empty;
                    PaymentInfo.CreditCard.BankName = dataReader["BankName"] != DBNull.Value ? dataReader["BankName"].ToString() : string.Empty;
                    PaymentInfo.CreditCard.CVV = dataReader["CVV"] != DBNull.Value ? dataReader["CVV"].ToString() : string.Empty;
                    PaymentInfo.CreditCard.ExpiryDate = dataReader["ExpireDate"] != DBNull.Value ? dataReader["ExpireDate"].ToString() : string.Empty;
                    PaymentInfo.CreditCard.Number = dataReader["CCNumber"] != DBNull.Value ? dataReader["CCNumber"].ToString() : string.Empty;
                    PaymentInfo.AutoPayment = dataReader["AutoPayment"] != DBNull.Value ? bool.Parse(dataReader["AutoPayment"].ToString()) : false;
                    PaymentInfo.EmailMonthly = dataReader["EmailMonthly"] != DBNull.Value ? bool.Parse(dataReader["EmailMonthly"].ToString()) : false;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (dataReader != null && !dataReader.IsClosed)
                    dataReader.Close();
            }
            
            return PaymentInfo;
        }

        public static void ManagePayment(Users.User User, DbOperationMode Mode)
        {
            int currentUserID = UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser != null ? UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.ID : BusinessLayer.Users.User.DefaultSystemUserID;

            

            List<System.Data.SqlClient.SqlParameter> sParameters = new List<System.Data.SqlClient.SqlParameter>();

            sParameters.Add(new System.Data.SqlClient.SqlParameter("@userID", currentUserID));
            sParameters.Add(new System.Data.SqlClient.SqlParameter("@paymentid",BusinessLayer.Common.CurrentContext.CurrentUser.PaymentID ));
            sParameters.Add(new System.Data.SqlClient.SqlParameter("@mode", Mode));
            sParameters.Add(new System.Data.SqlClient.SqlParameter("@BankName", User.PaymentInfo.CreditCard.BankName));
            sParameters.Add(new System.Data.SqlClient.SqlParameter("@CVV", User.PaymentInfo.CreditCard.CVV));
            sParameters.Add(new System.Data.SqlClient.SqlParameter("@ExpireDate", User.PaymentInfo.CreditCard.ExpiryDate));
            sParameters.Add(new System.Data.SqlClient.SqlParameter("@CCNumber", User.PaymentInfo.CreditCard.Number));
            sParameters.Add(new System.Data.SqlClient.SqlParameter("@autopayment", User.PaymentInfo.AutoPayment));
            sParameters.Add(new System.Data.SqlClient.SqlParameter("@emailmonthly", User.PaymentInfo.EmailMonthly));
            sParameters.Add(new System.Data.SqlClient.SqlParameter("@cardholdername", User.PaymentInfo.CreditCard.HolderName));

            if (Mode != BusinessLayer.DbOperationMode.Select)
            {
                if (Mode == BusinessLayer.DbOperationMode.Insert)
                {
                    object id = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar("ManagePaymentInfo", sParameters);
                    if (id != null) User.PaymentInfo.ID = int.Parse(id.ToString());
                }
                else Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery("ManagePaymentInfo", sParameters.ToArray());
            }
        }
    }
    public class CreditCard
    {
        public string Number, CVV, ExpiryDate, BankName, HolderName;
    }
    public enum PaymentPreference
    {
        Unknown = 0,
        AutoPay = 1,
        MonthlyEmail = 2
    }
}

