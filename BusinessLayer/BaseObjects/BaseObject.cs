using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UBERREP.BusinessLayer
{   
    [Serializable]
    public abstract class DbObject
    {
        [System.Xml.Serialization.XmlAttribute]
        public string Name { get; set; }
        [System.Xml.Serialization.XmlAttribute]
        public int LanguageID, DisplayOrder;
        [System.Xml.Serialization.XmlAttribute]
        public int ID { get; set; }
        [System.Xml.Serialization.XmlAttribute]
        public string ExternalID;
        [System.Xml.Serialization.XmlAttribute]
        public bool Active { get;set;}
        [System.Xml.Serialization.XmlAttribute]
        public bool Visible { get;set;}

        /// <summary>
        /// used to show record number with paging ability
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public int RecordNumber { get; set; }

        internal DbProperties Properties;
        internal DbOperationMode Mode;

        internal virtual void Create()
        {
            this.Mode = DbOperationMode.Insert;
            this.Execute();
            this.ManageCache();
        }

        internal virtual void Delete()
        {
            this.Mode = DbOperationMode.Delete;
            this.Execute();
            this.ManageCache();
        }

        internal virtual void Fetch()
        {
            this.Mode = DbOperationMode.Select;
            this.Execute();
            this.ManageCache();
        }

        internal virtual void Update()
        {
            this.Mode = DbOperationMode.Update;
            this.Execute();
            this.ManageCache();
        }
        internal abstract   void Fill(System.Data.SqlClient.SqlDataReader dataReader);
        internal abstract   void Execute();
        internal abstract   void ManageCache();

        public static System.Data.DataTable Parse(IEnumerable<DbObject> list)
        {
            System.Data.DataTable retObj = new System.Data.DataTable();
            retObj.Columns.Add("ID", typeof(int));
            retObj.Columns.Add("ExternalID", typeof(string));
            foreach (BusinessLayer.DbObject item in list) retObj.Rows.Add(new object[] { item.ID, item.ExternalID });
            return retObj;
        }
    }

    public enum DbOperationMode
    {
        Select = 0,
        Insert = 1,
        Update = 2,
        Delete = 3
    }
    [Serializable]
    internal struct DbProperties
    {
        [System.Xml.Serialization.XmlAttribute]
        internal string StoredProcedure;
        [System.Xml.Serialization.XmlIgnore]
        internal System.Data.SqlClient.SqlParameter[] sParameters;
        [System.Xml.Serialization.XmlAttribute]
        internal int InsertedBy;
        [System.Xml.Serialization.XmlAttribute]
        internal DateTime InsertedOn;
        [System.Xml.Serialization.XmlAttribute]
        internal int ModifiedBy;
        [System.Xml.Serialization.XmlAttribute]
        internal DateTime ModifiedOn;
    }   
}
