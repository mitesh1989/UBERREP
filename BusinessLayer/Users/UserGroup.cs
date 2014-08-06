using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using BusinessLayer.Common;

namespace UBERREP.BusinessLayer.Users
{
    //To assign User to a Group
    [Serializable]
    public class UserGroup: DbObject
    {
        [System.Xml.Serialization.XmlIgnore]
        public string ClientID;
        private Client.Client _Client;
        [System.Xml.Serialization.XmlIgnore]
        public Status Status;

        [System.Xml.Serialization.XmlAttribute]
        public bool IsSystemGroup;

        public static object lockObj = new object();//to prevent dictionary from being modified by more than one object at the same time - by obtaining lock on that object

        public Client.Client ClientGroup
        {
            get
            {
                if (!string.IsNullOrEmpty(this.ClientID))
                {
                    NameValueCollection ClientData = new NameValueCollection();
                    ClientData.Add("ClientID", ClientID);
                    this._Client = (Client.Client)Client.ClientManager.GetClients(ClientData).FirstOrDefault();
                }

                return this._Client;
            }
        }

        public UserGroup()
        {
            this.Properties.StoredProcedure = "ManageClientGroups";
        }
        internal override void Fill(System.Data.SqlClient.SqlDataReader dataReader)
        {
            this.ID = int.Parse(dataReader["ID"].ToString());
            this.Name = dataReader["Name"].ToString();
            this.ClientID = dataReader["ClientID"].ToString();
            this.Status = (Status)(int.Parse(dataReader["status"].ToString()));
            this.Properties.InsertedBy = !string.IsNullOrEmpty(dataReader["insertedby"].ToString()) ? Convert.ToInt32(dataReader["insertedby"].ToString()) : 0;
            this.Properties.ModifiedBy = !string.IsNullOrEmpty(dataReader["updatedby"].ToString()) ? Convert.ToInt32(dataReader["updatedby"].ToString()) : 0;
            this.Properties.InsertedOn = !string.IsNullOrEmpty(dataReader["insertedon"].ToString()) ? Convert.ToDateTime(dataReader["insertedon"].ToString()) : DateTime.MinValue;
            this.Properties.ModifiedOn = !string.IsNullOrEmpty(dataReader["updatedon"].ToString()) ? Convert.ToDateTime(dataReader["updatedon"].ToString()) : DateTime.MinValue;

            this.IsSystemGroup = dataReader["IsSystem"]!=DBNull.Value ? bool.Parse(dataReader["IsSystem"].ToString()) : false;
        }
        internal override void Execute()
        {
            int currentUserID = UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser != null ? UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.ID : BusinessLayer.Users.User.DefaultSystemUserID;
            this.Properties.sParameters = new System.Data.SqlClient.SqlParameter[7];

            this.Properties.sParameters[0] = new System.Data.SqlClient.SqlParameter("@mode", this.Mode);
            this.Properties.sParameters[1] = new System.Data.SqlClient.SqlParameter("@id", this.ID);
            this.Properties.sParameters[2] = new System.Data.SqlClient.SqlParameter("@name", this.Name);
            this.Properties.sParameters[3] = new System.Data.SqlClient.SqlParameter("@ClientID", this.ClientID);
            this.Properties.sParameters[4] = new System.Data.SqlClient.SqlParameter("@status", this.Status);
            this.Properties.sParameters[5] = new System.Data.SqlClient.SqlParameter("@userID",currentUserID);
            this.Properties.sParameters[6] = new System.Data.SqlClient.SqlParameter("@IsSystem", this.IsSystemGroup);

            if (this.Mode != UBERREP.BusinessLayer.DbOperationMode.Select)
            {
                if (this.Mode == UBERREP.BusinessLayer.DbOperationMode.Insert)
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
                else throw new Exception("No user group found to match selected parameters");
            }
        }

        #region Cache Management
        internal static Dictionary<int, UserGroup> UserGroupsCached = new Dictionary<int, UserGroup>();
        internal override void ManageCache()
        {
            switch (this.Mode)
            {
                case UBERREP.BusinessLayer.DbOperationMode.Delete:
                    {
                        if (UserGroupsCached.ContainsKey(this.ID)) UserGroupsCached.Remove(this.ID);
                        break;
                    }
                default:
                    {
                        lock (lockObj)
                        {
                            if (UserGroupsCached.ContainsKey(this.ID)) UserGroupsCached[this.ID] = this;
                            else UserGroupsCached.Add(this.ID, this);
                        }
                        break;
                    }
            }
        }
        #endregion
    }
    public class UserGroupManager : UBERREP.BusinessLayer.Base.BaseManager
    {
        public static UserGroup Get(int GroupID)
        {
            UserGroup retObj = null;
            if (UserGroup.UserGroupsCached.ContainsKey(GroupID)) return UserGroup.UserGroupsCached[GroupID];
            else
            {
                retObj = new UserGroup { ID = GroupID };
                retObj.Fetch();
            }
            return retObj;
        }
        /// <summary>
        /// Used to Get UserGroups based on Parameters
        /// </summary>
        /// <param name="spData">name,status,,ClientID</param>
        /// <returns></returns>
        public static List<UserGroup> GetUserGroups(System.Collections.Specialized.NameValueCollection spData)
        {
            System.Data.SqlClient.SqlDataReader dataReader = null;
            List<UserGroup> retObj = new List<UserGroup>();
            try
            {
                if (spData == null) spData = new System.Collections.Specialized.NameValueCollection();
                List<SqlParameter> spParams = new List<SqlParameter>();
                if (spData["name"] != null) spParams.Add(new SqlParameter("@name", spData["name"]));
                if (spData["status"] != null) spParams.Add(new SqlParameter("@status", spData["status"]));
                if (spData["Clientid"] != null) spParams.Add(new SqlParameter("@Clientid", spData["Clientid"]));

                dataReader = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(System.Data.CommandType.StoredProcedure, "GetClientGroups", spParams.ToArray());
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        UserGroup item = new UserGroup();
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
        public static bool SaveAllowedUserGroups(int UserID,List<UserGroup> Groups)
        {
            int currUserID = UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser!=null ? UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.ID : BusinessLayer.Users.User.DefaultSystemUserID;
            try
            {
                System.Data.SqlClient.SqlParameter[] paras = new System.Data.SqlClient.SqlParameter[]
                {                       
                    new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                    new System.Data.SqlClient.SqlParameter("@insertedby", currUserID),
                    new System.Data.SqlClient.SqlParameter("@AllowedGroupsData", UBERREP.CommonLayer.Functions.Serialize(Groups)),
                };
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery("ManageUserAllowedGroups", paras);
            }
            catch (Exception ex)
            {
                UBERREP.BusinessLayer.Common.CurrentContext.AddExecutionError((UBERREP.BusinessLayer.Common.Diagnostics.Exception)ex);
                ((UBERREP.BusinessLayer.Common.Diagnostics.Exception)ex).LogError();
                return false;
            }
            return true;
        }
    }
        
    [Serializable]
    public class Role
    {        
        public bool HasInsert;
        public bool HasUpdate;
        public bool HasDelete;
        public bool HasView;
        public bool ShowClientAdminData;
        public bool ShowEncryptedData;

        public Role() { }
    }
    [Serializable]
    public class Section : DbObject
    {
        public SectionCodes Code;
        public UserTypes    Type;
        public Role         AllowedSectionRoles;//these are the possible applicable roles on the section like Insert/Update etc do not apply to reports section

        public Section()
        {
            this.Properties.StoredProcedure = "ManageSections";
        }

        internal override void Execute()
        {            
            this.Properties.sParameters = new System.Data.SqlClient.SqlParameter[2];
         
            this.Properties.sParameters[0] = new System.Data.SqlClient.SqlParameter("@mode", DbOperationMode.Select);
            this.Properties.sParameters[1] = new System.Data.SqlClient.SqlParameter("@id", this.ID);
            
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

        internal override void Fill(SqlDataReader dataReader)//possible allowed roles on each section
        {
            this.ID =  dataReader["id"]!=DBNull.Value ? int.Parse(dataReader["id"].ToString()) : 0;
            this.Type = dataReader["SectionType"] != null ? (UserTypes)int.Parse(dataReader["SectionType"].ToString()) : UserTypes.Client;
            this.Name = dataReader["name"]!=DBNull.Value ? dataReader["name"].ToString() : string.Empty;
            this.Code = (SectionCodes)Enum.Parse(typeof(SectionCodes),dataReader["Code"].ToString());
            this.AllowedSectionRoles = dataReader["Roles"] != DBNull.Value ? (Role)UBERREP.CommonLayer.Functions.DeSerialize(new StringReader(dataReader["Roles"].ToString()), typeof(Role)) : new Role();
        }

        #region Cache Management
        internal static Dictionary<int, Section> Sections = new Dictionary<int,Section>();
        internal override void ManageCache()
        {
            switch (this.Mode)
            {
                case BusinessLayer.DbOperationMode.Delete:
                    {
                        if (Sections.ContainsKey(this.ID)) Sections.Remove(this.ID);
                        break;
                    }
                default:
                    {
                        if (Sections.ContainsKey(this.ID)) Sections[this.ID] = this;
                        else Sections.Add(this.ID, this);
                        break;
                    }
            }
        }
        #endregion
        /// <summary>
        /// enum for SectionCode to check on every page and to be used as key in Dictionary with Role as Value, inside User object
        /// </summary>
        public enum SectionCodes
        {
            OFC_MGNT,//Client Management            
            USR_MGNT,//User Management
            USR_GRP_MGNT//Group Management            
        }
    }
    public class SectionManager : BusinessLayer.Base.BaseManager
    {
        /// <summary>
        /// Returns Section based on search paramters passed
        /// </summary>
        /// <param name="spData">name,code</param>
        /// <returns></returns>
        public static List<Section> GetSections(System.Collections.Specialized.NameValueCollection spData)
        {
            List<Section> retObj = new List<Section>();
            System.Data.SqlClient.SqlDataReader dataReader = null;
            if (spData == null) spData = new System.Collections.Specialized.NameValueCollection();
            List<SqlParameter> spParams = new List<SqlParameter>();
            try
            {
                if (spData["name"] != null) spParams.Add(new SqlParameter("@name", spData["name"]));
                if (spData["code"] != null) spParams.Add(new SqlParameter("@code", spData["code"]));
                if (spData["groupid"] != null) spParams.Add(new SqlParameter("@groupid", spData["groupid"]));

                dataReader = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(System.Data.CommandType.StoredProcedure, "GetSections", spParams.ToArray());
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Section item = new Section();
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
        /// Get specific Section
        /// </summary>
        /// <param name="UserID">ID of the Section</param>
        /// <returns></returns>
        public static Section Get(int SectionID)
        {
            Section retObj = null;
            if (Section.Sections.ContainsKey(SectionID)) return Section.Sections[SectionID];
            else
            {
                retObj = new Section { ID = SectionID };
                retObj.Fetch();
            }
            return retObj;
        }
    }

    [Serializable]
    public class GroupSectionRole : DbObject
    {        
        public int GroupID;
        private UserGroup _usergroup;
        public UserGroup UserGroup
        {            
            get
            {
                if (this.GroupID != 0)
                {
                    this._usergroup = UserGroupManager.Get(this.GroupID);                    
                }
                return _usergroup;
            }
        }

        [System.Xml.Serialization.XmlAttribute]
        public int SectionID;
        private Section _section;
        public Section Section
        {
            get
            {
                if (this.SectionID != 0)
                {
                    this._section = SectionManager.Get(this.SectionID);
                }
                return this._section;
            }
        }
        [System.Xml.Serialization.XmlElement]
        public Role Role;

        internal override void Execute()
        {
            throw new NotImplementedException();
        }
        internal override void Fill(SqlDataReader dataReader)
        {
            this.ID = int.Parse(dataReader["ID"].ToString());
            this.GroupID = int.Parse(dataReader["GroupID"].ToString());
            this.SectionID = int.Parse(dataReader["SectionID"].ToString());
            this.Role = dataReader["Roles"] != DBNull.Value ? (Role)UBERREP.CommonLayer.Functions.DeSerialize(new StringReader(dataReader["Roles"].ToString()), typeof(Role)) : new Role();
            this.Properties.InsertedBy = !string.IsNullOrEmpty(dataReader["insertedby"].ToString()) ? Convert.ToInt32(dataReader["insertedby"].ToString()) : 0;
            this.Properties.ModifiedBy = !string.IsNullOrEmpty(dataReader["updatedby"].ToString()) ? Convert.ToInt32(dataReader["updatedby"].ToString()) : 0;
            this.Properties.InsertedOn = !string.IsNullOrEmpty(dataReader["insertedon"].ToString()) ? Convert.ToDateTime(dataReader["insertedon"].ToString()) : DateTime.MinValue;
            this.Properties.ModifiedOn = !string.IsNullOrEmpty(dataReader["updatedon"].ToString()) ? Convert.ToDateTime(dataReader["updatedon"].ToString()) : DateTime.MinValue;
        }
        internal override void ManageCache()
        {
            throw new NotImplementedException();
        }        
    }
    public class GroupSectionRoleManager : BusinessLayer.Base.BaseManager
    {
        /// <summary>
        /// returns which group is allowed what roles on each section - based on parameters
        /// </summary>
        /// <param name="spData">groupid,sectionid</param>
        /// <returns></returns>
        public static List<GroupSectionRole> GetGroupSectionRoles(System.Collections.Specialized.NameValueCollection spData)
        {
            List<GroupSectionRole> retObj = new List<GroupSectionRole>();

            System.Data.SqlClient.SqlDataReader dataReader = null;
            if (spData == null) spData = new System.Collections.Specialized.NameValueCollection();
            List<SqlParameter> spParams = new List<SqlParameter>();
            try
            {
                if (spData["groupid"] != null) spParams.Add(new SqlParameter("@groupid", spData["groupid"]));
                if (spData["sectionid"] != null) spParams.Add(new SqlParameter("@sectionid", spData["sectionid"]));

                dataReader = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(System.Data.CommandType.StoredProcedure, "GetGroupSectionRoles", spParams.ToArray());
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        GroupSectionRole item = new GroupSectionRole();
                        item.Fill(dataReader);                        
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

        public static bool SaveGroupSectionRoles(int GroupID,List<GroupSectionRole> data)
        {
            int currUserID = UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser != null ? UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.ID : BusinessLayer.Users.User.DefaultSystemUserID;
            try
            {
                System.Data.SqlClient.SqlParameter[] paras = new System.Data.SqlClient.SqlParameter[] 
                {                       
                    new System.Data.SqlClient.SqlParameter("@groupID", GroupID),                
                    new System.Data.SqlClient.SqlParameter("@GroupSectionData", UBERREP.CommonLayer.Functions.Serialize(data)),
                    new System.Data.SqlClient.SqlParameter("@userID", currUserID)                    
                };
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery("ManageGroupSectionRoles", paras);
            }
            catch (Exception ex)
            {
                UBERREP.BusinessLayer.Common.CurrentContext.AddExecutionError((UBERREP.BusinessLayer.Common.Diagnostics.Exception)ex);
                ((UBERREP.BusinessLayer.Common.Diagnostics.Exception)ex).LogError();
                return false;
            }

            return true;
        }
    }
}
