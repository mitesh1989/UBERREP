using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UBERREP.BusinessLayer;
using System.Data.SqlClient;
using UBERREP.CommonLayer;
using BusinessLayer.Common;

namespace UBERREP.BusinessLayer.Client
{
    [Serializable]
    public class Client : UBERREP.BusinessLayer.DbObject
    {
        public CommonLayer.GenericProperties ClientAPIProperities = new CommonLayer.GenericProperties();
        public CommonLayer.GenericProperties EmailAPIProperities = new CommonLayer.GenericProperties();
        public CommonLayer.GenericProperties SMSAPIProperities = new CommonLayer.GenericProperties();
              
        [System.Xml.Serialization.XmlAttribute]
        public Status Status;

        public ContactInfo ContactInfo;

        //store Miscellaneous Client info
        public CommonLayer.GenericProperties GeneralClientInfo;

        public Client()
        {
            this.Properties.StoredProcedure = "ManageClients";
        }

        public override string ToString()
        {
            if (this.ID == 0) return string.Empty;
            return this.ID.ToString();
        }
        internal override void Fill(System.Data.SqlClient.SqlDataReader dataReader)
        {
            this.ID = int.Parse(dataReader["id"].ToString());
            this.Name = dataReader["name"].ToString();
            this.Status = (Status)Convert.ToInt16(dataReader["status"].ToString());
            

            this.Properties.InsertedBy = !string.IsNullOrEmpty(dataReader["insertedby"].ToString()) ? Convert.ToInt32(dataReader["insertedby"].ToString()) : 0;
            this.Properties.ModifiedBy = !string.IsNullOrEmpty(dataReader["updatedby"].ToString()) ? Convert.ToInt32(dataReader["updatedby"].ToString()) : 0;
            this.Properties.InsertedOn = !string.IsNullOrEmpty(dataReader["insertedon"].ToString()) ? Convert.ToDateTime(dataReader["insertedon"].ToString()) : DateTime.MinValue;
            this.Properties.ModifiedOn = !string.IsNullOrEmpty(dataReader["updatedon"].ToString()) ? Convert.ToDateTime(dataReader["updatedon"].ToString()) : DateTime.MinValue;
            

            if (ExtentionMethods.GetValue(dataReader, "contactinfo") != null)
                this.ContactInfo = (ContactInfo)CommonLayer.Functions.DeSerialize(new System.IO.StringReader(dataReader["contactinfo"].ToString()), typeof(ContactInfo));

            if (ExtentionMethods.GetValue(dataReader, "generalclientinfo") != null)
                this.GeneralClientInfo = (GenericProperties)CommonLayer.Functions.DeSerialize(new System.IO.StringReader(dataReader["generalclientinfo"].ToString()), typeof(GenericProperties));

        }

        internal override void Execute()
        {
            int currentUserID = UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser != null ? UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.ID : BusinessLayer.Users.User.DefaultSystemUserID;
            string SerizaliedContactData = string.Empty;
            List<System.Data.SqlClient.SqlParameter> SPparams = new List<SqlParameter>();
            SPparams.Add(new SqlParameter("@mode", this.Mode));
            SPparams.Add(new SqlParameter("@id", this.ID));
            SPparams.Add(new SqlParameter("@name", this.Name));
            SPparams.Add(new SqlParameter("@status", this.Status));
           
            if (this.ContactInfo != null && this.Mode != UBERREP.BusinessLayer.DbOperationMode.Delete)//if delete, then no need to deserialize
                SerizaliedContactData = CommonLayer.Functions.Serialize(this.ContactInfo);
            if (!string.IsNullOrEmpty(SerizaliedContactData))
                 SPparams.Add(new SqlParameter("@contactinfo", SerizaliedContactData));
            else
                SPparams.Add(new SqlParameter("@contactinfo", null));

            if (this.GeneralClientInfo != null && this.Mode != UBERREP.BusinessLayer.DbOperationMode.Delete)//if delete, then no need to deserialize)
            {
                string strgeneralClientinfo;
                strgeneralClientinfo = CommonLayer.Functions.Serialize(this.GeneralClientInfo);
                if (!string.IsNullOrEmpty(strgeneralClientinfo))
                     SPparams.Add(new SqlParameter("@generalclientinfo", strgeneralClientinfo));
                else
                     SPparams.Add(new SqlParameter("@generalclientinfo", null));
            }
            else
                SPparams.Add(new SqlParameter("@generalclientinfo", null));

            SPparams.Add(new SqlParameter("@userID", currentUserID));

            this.Properties.sParameters = SPparams.ToArray(); 
            if (this.Mode != UBERREP.BusinessLayer.DbOperationMode.Select)
            {
                if (this.Mode == UBERREP.BusinessLayer.DbOperationMode.Insert)
                {
                    object id = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(this.Properties.StoredProcedure, this.Properties.sParameters);//get ID of last row inserted, used when creating site with user
                    if (id != null) this.ID = int.Parse(id.ToString());
                }
                else Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(this.Properties.StoredProcedure, this.Properties.sParameters);
            }
            else
            {
                System.Data.SqlClient.SqlDataReader dataReader =
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(this.Properties.StoredProcedure, this.Properties.sParameters);
                if (dataReader != null && dataReader.Read())
                {
                    this.Fill(dataReader);
                    if (!dataReader.IsClosed)
                        dataReader.Close();
                }
                else throw new Exception("No Client found to match selected parameters");
            }
        }

        #region Cache Management
        internal static System.Collections.Concurrent.ConcurrentDictionary<int, Client> ClientsCached = new System.Collections.Concurrent.ConcurrentDictionary<int, Client>();
        internal override void ManageCache()
        {
            switch (this.Mode)
            {
                case UBERREP.BusinessLayer.DbOperationMode.Delete:
                    {
                        //if (ClientsCached.ContainsKey(this.ID)) 
                        Client _clientRemoved;
                        ClientsCached.TryRemove(this.ID, out _clientRemoved);//removes client with given ID from dictionary and puts that removed object in out param                            
                        break;
                    }
                default:
                    {
                        //if (ClientsCached.ContainsKey(this.ID)) ClientsCached.TryUpdate(this.ID,this,this);
                        //else ClientsCached.TryAdd(this.ID, this);                            
                        //ClientsCached.AddOrUpdate(this.ID,this,)
                        if (ClientsCached.ContainsKey(this.ID)) ClientsCached[this.ID] = this;//ClientsCached.TryUpdate(this.ID,this,this);
                        else ClientsCached.TryAdd(this.ID, this);
                        break;
                    }
            }
        }
        #endregion

        public enum GeneralClientInformation
        {
            IATANumber,
            ARCNumber,
            CallBackURL,//to post score update to client of request processing
            EmailSubject,
            PaymentEmailSubject,
            EmailCC,
            EmailBCC,
            ClientLogo,
            PrivacyPolicyURL ,
            CustomerSupportEmail,
            CustomerSupportPhone,
            ClientWebSiteURL
        }
        /// <summary>
        /// Enum for Keywords used to replace by actual content between <<KeyWord>>
        /// </summary>
        public enum EmailTemplateKeyWords
        {
            Client,
            CustomerName,
            VerificationURL,
            RawVerificationURL,            
            ClientPrivacyPolicyURL,
            CCLast4Digits,
            MerchantName
        }

        public enum EmailVerificationStatus
        {
            Unknown,//verification not yet attempted
            Verified,//done
            VerificationAttempted,//verification attempted but failed
            AlreadyVerified,//to give message already verified
            ReachedMaxCount//5 wrong attempts have been made - link is no more valid for further attempts with this payment.ID/Amount
        }

    }
    public class ClientManager : UBERREP.BusinessLayer.Base.BaseManager
    {
        public static BusinessLayer.Client.Client Get(int ClientID)
        {
            Client retObj = null;
            //if (Client.ClientsCached.ContainsKey(ClientID)) return Client.ClientsCached[ClientID];
            //else
            {
                retObj = new Client { ID = ClientID };
                retObj.Fetch();
            }
            return retObj;
        }
        /// <summary>
        /// Used to Get Clients based on Parameters - used for Admin - Search on ManageClients page
        /// </summary>
        /// <param name="spData">name,status,apiid,ClientId,Clientcode,userid</param>
        /// <returns></returns>
        public static List<Client> GetClients(System.Collections.Specialized.NameValueCollection spData)
        {
            System.Data.SqlClient.SqlDataReader dataReader = null;
            List<Client> retObj = new List<Client>();
            try
            {
                //if (BusinessLayer.Common.CurrentContext.CurrentUser != null && BusinessLayer.Common.CurrentContext.CurrentUser.Type == BusinessLayer.Users.UserTypes.Client && BusinessLayer.Common.CurrentContext.CurrentUser.ID != null && spData != null && spData["userID"] == null)
                //{
                //    spData["userID"] = BusinessLayer.Common.CurrentContext.CurrentUser.ID.ToString();
                //}
                if (spData == null) spData = new System.Collections.Specialized.NameValueCollection();
                List<SqlParameter> spParams = new List<SqlParameter>();
                if (spData["name"] != null) spParams.Add(new SqlParameter("@name", spData["name"]));
                if (spData["status"] != null) spParams.Add(new SqlParameter("@status", spData["status"]));               
                if (spData["Clientid"] != null) spParams.Add(new SqlParameter("@clientid", spData["Clientid"]));
                if (spData["Clientcode"] != null) spParams.Add(new SqlParameter("@clientcode", spData["Clientcode"]));
                if (spData["userID"] != null) spParams.Add(new SqlParameter("@userID", spData["userID"]));

                dataReader = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(System.Data.CommandType.StoredProcedure, "GetClients", spParams.ToArray());
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Client item = new Client();
                        item.Fill(dataReader);
                        item.ManageCache();
                        retObj.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                UBERREP.BusinessLayer.Common.CurrentContext.AddExecutionError((UBERREP.BusinessLayer.Common.Diagnostics.Exception)ex);
                ((UBERREP.BusinessLayer.Common.Diagnostics.Exception)ex).LogError();
            }
            finally
            {
                if (dataReader != null && !dataReader.IsClosed) dataReader.Close();
            }
            return retObj;
        }

        /// <summary>
        /// Authorize Client - Use Header Credentials from client request
        /// </summary>
        /// <param name="IpAddress"></param>
        /// <param name="ClientCode"></param>
        /// <param name="AuthKey"></param>
        /// <returns></returns>
        public static Client AuthorizeClient(string IpAddress, string ClientCode, string AuthKey)
        {
            System.Data.SqlClient.SqlDataReader dataReader = null;
            Client retObj = null;

            bool RequiresClientIP = System.Configuration.ConfigurationManager.AppSettings["RequiresClientIP"] == "true" ? true : false;//ClientIP check not mandatory

            try
            {
                SqlParameter[] spParams = new SqlParameter[]{
                    new SqlParameter("@Clientcode", ClientCode),
                    new SqlParameter("@authkey",AuthKey),
                    new SqlParameter("@ipaddress", RequiresClientIP==true && !string.IsNullOrEmpty(IpAddress) ? IpAddress : null)
                };

                dataReader = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader("ClientAccessCheck", spParams);
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        retObj = new Client();
                        retObj.Fill(dataReader);
                        retObj.ManageCache();
                    }
                }
            }
            catch (Exception ex)
            {
                UBERREP.BusinessLayer.Common.CurrentContext.AddExecutionError((UBERREP.BusinessLayer.Common.Diagnostics.Exception)ex);
                ((UBERREP.BusinessLayer.Common.Diagnostics.Exception)ex).LogError();
            }
            finally
            {
                if (dataReader != null && !dataReader.IsClosed) dataReader.Close();
            }
            return retObj;
            //return new Client() {ClientID="FGClient",Password="abc123" };
        }
    }
}
