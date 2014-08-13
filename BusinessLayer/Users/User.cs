using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using BusinessLayer.Common;

namespace UBERREP.BusinessLayer.Users
{
    public class User : DbObject
    {
        [System.Xml.Serialization.XmlIgnore]
        public const int DefaultSystemUserID = -9999;
        [System.Xml.Serialization.XmlAttribute]
        public string Username, Password, Email;
        [System.Xml.Serialization.XmlAttribute]
        public Status Status;
        public ContactInfo ContactData;
        [System.Xml.Serialization.XmlAttribute]
        public DateTime LastLogin;
        [System.Xml.Serialization.XmlAttribute]
        public UserTypes Type;

        public decimal Points;


        public Gender Gender;

        public string Remarks;

        public BusinessLayer.Payment.PaymentInfo PaymentInfo;

        //public static object lockObj = new object();//to prevent dictionary from being modified by more than one object at the same time - by obtaining lock on that object

        //Allowed Roles to this user on each section
        //key-sectioncode, value-role        
        public Dictionary<Section.SectionCodes, Role> UserAllowedSections = new Dictionary<Section.SectionCodes, Role>();


        private string _groups = null;
        private List<UserGroup> groups = null;
        public List<UserGroup> Groups
        {
            get
            {
                if (this.groups != null) return this.groups;
                List<UserGroup> retObj = new List<UserGroup>();
                if (!string.IsNullOrEmpty(this._groups))
                {
                    string[] usersites = this._groups.Split(',');
                    foreach (string group in usersites)
                    {
                        UserGroup item = UserGroupManager.Get(int.Parse(group));
                        if (item != null) retObj.Add(item);
                    }
                }
                return retObj;
            }
            set
            {
                this.groups = value;
                if (value != null)
                {
                    this._groups = string.Join<UserGroup>(",", this.groups);
                }
            }
        }

        public string ClientsToSave;//used to send ',' separated ClientIDs in ClientUsers table
        private string _Clients = null;
        private List<Client.Client> clients = null;
        public List<Client.Client> Clients
        {
            get
            {
                if (this.clients != null) return this.clients;
                List<Client.Client> retObj = new List<Client.Client>();
                if (!string.IsNullOrEmpty(this._Clients))
                {
                    string[] usersites = this._Clients.Split(',');
                    foreach (string currClient in usersites)
                    {
                        Client.Client item = Client.ClientManager.Get(int.Parse(currClient));
                        if (item != null) retObj.Add(item);
                    }
                }
                return retObj;
            }
            set
            {
                this.Clients = value;
                if (value != null)
                {
                    this._Clients = string.Join<BusinessLayer.Client.Client>(",", this.Clients);
                }
            }
        }

        internal static bool IsPasswordEncrypted
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["IsPasswordEncrypted"] != "false"; }
        }


        public User() { this.Properties.StoredProcedure = "ManageUsers"; }

        internal override void Fill(System.Data.SqlClient.SqlDataReader dataReader)
        {
            this.ID = int.Parse(dataReader["id"].ToString());
            this.Name = dataReader["name"].ToString();
            this.Username = dataReader["username"].ToString();

            this.Password = IsPasswordEncrypted ? UBERREP.CommonLayer.Functions.Decrypt(dataReader["password"].ToString(), CommonLayer.Functions.EncryptionDataType.PasswordDataKey) : dataReader["password"].ToString();
            this.Email = dataReader["email"].ToString();
            if (!string.IsNullOrEmpty(dataReader["contactinfo"].ToString()))
            {
                System.IO.StringReader StrReader = new System.IO.StringReader(dataReader["contactinfo"].ToString());
                this.ContactData = new ContactInfo();
                this.ContactData = (ContactInfo)CommonLayer.Functions.DeSerialize(StrReader, typeof(ContactInfo));
            }

            this.Status = (Status)Convert.ToInt32(dataReader["status"]);
            this.Properties.InsertedBy = !string.IsNullOrEmpty(dataReader["insertedby"].ToString()) ? Convert.ToInt32(dataReader["insertedby"].ToString()) : 0;
            this.Properties.ModifiedBy = !string.IsNullOrEmpty(dataReader["updatedby"].ToString()) ? Convert.ToInt32(dataReader["updatedby"].ToString()) : 0;
            this.Properties.InsertedOn = !string.IsNullOrEmpty(dataReader["insertedon"].ToString()) ? Convert.ToDateTime(dataReader["insertedon"].ToString()) : DateTime.MinValue;
            this.Properties.ModifiedOn = !string.IsNullOrEmpty(dataReader["updatedon"].ToString()) ? Convert.ToDateTime(dataReader["updatedon"].ToString()) : DateTime.MinValue;
            if (dataReader.GetSchemaTable().Rows[0].Table.Select("ColumnName='LanguageID'").Length != 0 && dataReader["LanguageID"] != DBNull.Value)
                this.LanguageID = Convert.ToInt32(dataReader["LanguageID"].ToString());
            else
                this.LanguageID = 1;
            if (dataReader.GetSchemaTable().Rows[0].Table.Select("ColumnName='LastLogin'").Length != 0 && dataReader["LastLogin"] != DBNull.Value)
                this.LastLogin = Convert.ToDateTime(dataReader["LastLogin"].ToString());
            else
                this.LastLogin = DateTime.MinValue;

            if (dataReader.GetSchemaTable().Rows[0].Table.Select("ColumnName='GroupIDs'").Length != 0 && dataReader["GroupIDs"] != DBNull.Value)
            {
                this._groups = dataReader["GroupIDs"].ToString();
            }

            if (dataReader.GetSchemaTable().Rows[0].Table.Select("ColumnName='ClientIDs'").Length != 0 && dataReader["ClientIDs"] != DBNull.Value)
                this._Clients = dataReader["ClientIDs"].ToString();

            if (dataReader.GetSchemaTable().Rows[0].Table.Select("ColumnName='Type'").Length != 0 && dataReader["Type"] != DBNull.Value)
                this.Type = (UserTypes)int.Parse(dataReader["Type"].ToString());

            if (dataReader.GetSchemaTable().Rows[0].Table.Select("ColumnName='Gender'").Length != 0 && dataReader["Gender"] != DBNull.Value)
                this.Gender = dataReader["Gender"] != DBNull.Value ? (BusinessLayer.Users.Gender)(Convert.ToInt32(dataReader["Gender"].ToString())) : BusinessLayer.Users.Gender.Unknown;

            if (dataReader.GetSchemaTable().Rows[0].Table.Select("ColumnName='Remarks'").Length != 0 && dataReader["Remarks"] != DBNull.Value)
                this.Remarks = dataReader["Remarks"].ToString();

            if (dataReader.GetSchemaTable().Rows[0].Table.Select("ColumnName='PaymentID'").Length != 0 && dataReader["PaymentID"] != DBNull.Value)
            {
                this.PaymentInfo = new Payment.PaymentInfo();
                this.PaymentInfo.CreditCard = new Payment.CreditCard();
                this.PaymentInfo.CreditCard.HolderName = dataReader["HolderName"] != DBNull.Value ? dataReader["HolderName"].ToString() : string.Empty;
                this.PaymentInfo.CreditCard.BankName = dataReader["BankName"] != DBNull.Value ? dataReader["BankName"].ToString() : string.Empty;
                this.PaymentInfo.CreditCard.CVV = dataReader["CVV"] != DBNull.Value ? dataReader["CVV"].ToString() : string.Empty;
                this.PaymentInfo.CreditCard.ExpiryDate = dataReader["ExpireDate"] != DBNull.Value ? dataReader["ExpireDate"].ToString() : string.Empty;
                this.PaymentInfo.CreditCard.Number = dataReader["CCNumber"] != DBNull.Value ? dataReader["CCNumber"].ToString() : string.Empty;
                this.PaymentInfo.AutoPayment = dataReader["AutoPayment"] != DBNull.Value ? bool.Parse(dataReader["AutoPayment"].ToString()) : false;
                this.PaymentInfo.EmailMonthly = dataReader["EmailMonthly"] != DBNull.Value ? bool.Parse(dataReader["EmailMonthly"].ToString()) : false;
            }
        }
        internal override void Execute()
        {
            int currentUserID = UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser != null ? UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.ID : BusinessLayer.Users.User.DefaultSystemUserID;
            this.Properties.sParameters = new System.Data.SqlClient.SqlParameter[13];

            string password = IsPasswordEncrypted && !string.IsNullOrEmpty(this.Password) ? UBERREP.CommonLayer.Functions.Encrypt(this.Password, CommonLayer.Functions.EncryptionDataType.PasswordDataKey) : this.Password;

            this.Properties.sParameters[0] = new System.Data.SqlClient.SqlParameter("@mode", this.Mode);
            this.Properties.sParameters[1] = new System.Data.SqlClient.SqlParameter("@id", this.ID);
            this.Properties.sParameters[2] = new System.Data.SqlClient.SqlParameter("@name", this.Name);
            this.Properties.sParameters[3] = new System.Data.SqlClient.SqlParameter("@username", this.Username);
            this.Properties.sParameters[4] = new System.Data.SqlClient.SqlParameter("@password", password);
            this.Properties.sParameters[5] = new System.Data.SqlClient.SqlParameter("@email", this.Email);
            this.Properties.sParameters[6] = new System.Data.SqlClient.SqlParameter("@status", this.Status);
            string SerizaliedContactData = string.Empty;
            if (this.ContactData != null && this.Mode != BusinessLayer.DbOperationMode.Delete)//if delete then do not deserialize)
                SerizaliedContactData = CommonLayer.Functions.Serialize(this.ContactData);
            if (!string.IsNullOrEmpty(SerizaliedContactData))
                this.Properties.sParameters[7] = new System.Data.SqlClient.SqlParameter("@contactinfo", SerizaliedContactData);
            else
                this.Properties.sParameters[7] = new System.Data.SqlClient.SqlParameter("@contactinfo", null);
            this.Properties.sParameters[8] = new System.Data.SqlClient.SqlParameter("@updatedby", currentUserID);
            this.Properties.sParameters[9] = new System.Data.SqlClient.SqlParameter("@type", this.Type);

            this.Properties.sParameters[10] = new System.Data.SqlClient.SqlParameter("@remarks", this.Remarks);
            this.Properties.sParameters[11] = new System.Data.SqlClient.SqlParameter("@gender", this.Gender);

            this.Properties.sParameters[12] = new System.Data.SqlClient.SqlParameter("@points", this.Points);

            if (this.Mode != BusinessLayer.DbOperationMode.Select)
            {
                if (this.Mode == BusinessLayer.DbOperationMode.Insert)
                {
                    object id = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(this.Properties.StoredProcedure, this.Properties.sParameters);
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
                else throw new Exception("No user found to match selected parameters");
            }
        }

        #region Cache Management
        internal static System.Collections.Concurrent.ConcurrentDictionary<int, Users.User> Users = new System.Collections.Concurrent.ConcurrentDictionary<int, Users.User>();
        internal override void ManageCache()
        {
            switch (this.Mode)
            {
                case BusinessLayer.DbOperationMode.Delete:
                    {
                        User _userRemoved;
                        if (Users.ContainsKey(this.ID)) Users.TryRemove(this.ID, out _userRemoved);
                        break;
                    }
                default:
                    {
                        //lock (lockObj)
                        //{
                        if (Users.ContainsKey(this.ID)) Users[this.ID] = this;
                        else Users.TryAdd(this.ID, this);
                        //}
                        break;
                    }
            }
        }
        #endregion

        public static implicit operator System.Net.Mail.MailAddress(User c)
        {
            System.Net.Mail.MailAddress add = new System.Net.Mail.MailAddress(c.Email, c.Name);
            return add;
        }
    }

    public enum Gender
    {
        Unknown = 0,
        Male = 1,
        Female = 2
    }
    public enum UserTypes
    {
        //System = 1,
        //Client = 2
        WholeSale_Admin = 1,  //Wholesale Admin
        Sales_Rep_Admin = 2,  //Sales Rep - Admin
        Sales = 3,   //Sales Rep - Individual
        Retailer = 4  // Retailer
    }
    public class UserManager : Base.BaseManager
    {
        public static BusinessLayer.Users.User ValidateUser(string username, string password)
        {
            string EncPassword = User.IsPasswordEncrypted ? UBERREP.CommonLayer.Functions.Encrypt(password, CommonLayer.Functions.EncryptionDataType.PasswordDataKey) : password;
            System.Data.SqlClient.SqlParameter[] sParameters = new System.Data.SqlClient.SqlParameter[2];
            sParameters[0] = new System.Data.SqlClient.SqlParameter("@username", username);
            sParameters[1] = new System.Data.SqlClient.SqlParameter("@password", EncPassword);

            object o = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(System.Data.CommandType.StoredProcedure, "ValidateUser", sParameters);
            if (o == null) return null;
            return Base.BaseManager.Get(new Users.User { ID = Convert.ToInt32(o) }) as BusinessLayer.Users.User;
        }


        /// <summary>
        /// Get User Allowed Groups
        /// </summary>
        /// <param name="username"></param>
        /// <param name="siteid"></param>
        /// <returns></returns>
        public static BusinessLayer.Users.User GetUserAllowedGroupSections(User loggedInUser)
        {
            if (loggedInUser != null)
            {
                if (loggedInUser.Groups != null && loggedInUser.Groups.Count > 0)//assigned any groups
                {
                    //fetch it's allowed section-roles
                    System.Collections.Specialized.NameValueCollection data = new System.Collections.Specialized.NameValueCollection();
                    data.Add("groupid", loggedInUser.Groups.First().ID.ToString());
                    //this.UserAllowedSections = 
                    List<BusinessLayer.Users.GroupSectionRole> tmpList = BusinessLayer.Users.GroupSectionRoleManager.GetGroupSectionRoles(data);
                    foreach (BusinessLayer.Users.GroupSectionRole item in tmpList)
                    {
                        if (loggedInUser.UserAllowedSections.ContainsKey(item.Section.Code))
                            loggedInUser.UserAllowedSections[item.Section.Code] = item.Role;
                        else
                            loggedInUser.UserAllowedSections.Add(item.Section.Code, item.Role);
                    }
                }
                else if (loggedInUser.Type == UserTypes.WholeSale_Admin)//assigned any groups
                {
                    //fetch it's allowed section-roles
                    System.Collections.Specialized.NameValueCollection data = new System.Collections.Specialized.NameValueCollection();
                    data.Add("groupid", "-9999");
                    //this.UserAllowedSections = 
                    List<BusinessLayer.Users.GroupSectionRole> tmpList = BusinessLayer.Users.GroupSectionRoleManager.GetGroupSectionRoles(data);
                    foreach (BusinessLayer.Users.GroupSectionRole item in tmpList)
                    {
                        if (loggedInUser.UserAllowedSections.ContainsKey(item.Section.Code))
                            loggedInUser.UserAllowedSections[item.Section.Code] = item.Role;
                        else
                            loggedInUser.UserAllowedSections.Add(item.Section.Code, item.Role);
                    }
                }
            }
            return loggedInUser;
        }

        public static BusinessLayer.Users.User GetUserInfo(string username, string siteid = null)
        {
            Users.User retObj = new Users.User();
            System.Data.SqlClient.SqlParameter[] sParameters = new System.Data.SqlClient.SqlParameter[2];
            sParameters[0] = new System.Data.SqlClient.SqlParameter("@username", username);
            sParameters[1] = new System.Data.SqlClient.SqlParameter("@siteid", siteid);
            System.Data.SqlClient.SqlDataReader dataReader = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(System.Data.CommandType.StoredProcedure, "ValidateUser", sParameters);
            if (dataReader != null && dataReader.Read())
            {
                retObj.Fill(dataReader);
                if (!dataReader.IsClosed)
                    dataReader.Close();
            }
            return retObj;
        }
        /// <summary>
        /// Get specific user
        /// </summary>
        /// <param name="UserID">ID of the user</param>
        /// <returns></returns>
        public static BusinessLayer.Users.User Get(int UserID)
        {
            User retObj = null;
            if (User.Users.ContainsKey(UserID)) return Users.User.Users[UserID];
            else
            {
                retObj = new User { ID = UserID };
                retObj.Fetch();
            }
            return retObj;
        }
        /// <summary>
        /// Returns user based on search paramters passed
        /// </summary>
        /// <param name="spData">username,name,status,email</param>
        /// <returns></returns>
        public static List<User> GetUsers(System.Collections.Specialized.NameValueCollection spData)
        {
            List<User> retObj = new List<User>();
            System.Data.SqlClient.SqlDataReader dataReader = null;
            if (spData == null) spData = new System.Collections.Specialized.NameValueCollection();
            List<SqlParameter> spParams = new List<SqlParameter>();
            try
            {
                if (spData["username"] != null) spParams.Add(new SqlParameter("@username", spData["username"]));
                if (spData["name"] != null) spParams.Add(new SqlParameter("@name", spData["name"]));
                if (spData["status"] != null) spParams.Add(new SqlParameter("@status", spData["status"]));
                if (spData["email"] != null) spParams.Add(new SqlParameter("@email", spData["email"]));
                if (spData["type"] != null) spParams.Add(new SqlParameter("@type", spData["type"]));
                if (BusinessLayer.Common.CurrentContext.CurrentUser != null && BusinessLayer.Common.CurrentContext.CurrentUser.Type == UserTypes.Retailer && BusinessLayer.Common.CurrentContext.CurrentUser.Clients != null && BusinessLayer.Common.CurrentContext.CurrentUser.Clients.Count > 0)
                {
                    spParams.Add(new SqlParameter("@ClientID", BusinessLayer.Common.CurrentContext.CurrentUser.Clients.FirstOrDefault().ID.ToString()));
                }

                dataReader = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(System.Data.CommandType.StoredProcedure, "GetUsers", spParams.ToArray());
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        User item = new User();
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
                if (dataReader != null && !dataReader.IsClosed)
                    dataReader.Close();
            }
            return retObj;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Mode">Insert/Update</param>
        /// <param name="userID">UserID</param>
        /// <param name="ClientIDs">Comma Separted ClientIDs</param>
        /// <returns></returns>
        public static bool ManageClientUsers(DbOperationMode Mode, int userID, string ClientIDs)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] sParameters = new System.Data.SqlClient.SqlParameter[4];
                sParameters[0] = new System.Data.SqlClient.SqlParameter("@mode", Mode);
                sParameters[1] = new System.Data.SqlClient.SqlParameter("@userid", userID);
                sParameters[2] = new System.Data.SqlClient.SqlParameter("@userClients", ClientIDs);
                sParameters[3] = new System.Data.SqlClient.SqlParameter("@insertedby", Common.CurrentContext.CurrentUser != null ? Common.CurrentContext.CurrentUser.ID : User.DefaultSystemUserID);
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "ManageClientUsers", sParameters);
                return true;
            }
            catch (Exception ex)
            {
                UBERREP.BusinessLayer.Common.CurrentContext.AddExecutionError((UBERREP.BusinessLayer.Common.Diagnostics.Exception)ex);
                ((UBERREP.BusinessLayer.Common.Diagnostics.Exception)ex).LogError();
            }
            return false;
        }
    }
}