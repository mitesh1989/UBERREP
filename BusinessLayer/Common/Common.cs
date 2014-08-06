using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Common
{
    public class Common
    {

    }
    public enum Status
    {
        Active = 1,
        Suspended = 2,
        Deleted = 3
    }
    [Serializable]
    public class ContactInfo
    {
        [System.Xml.Serialization.XmlAttribute]
        public string CellNumber, ClientPhone, HomePhone, Email, Fax;
        public BusinessLayer.Common.Address Address;
    }

    public class Location
    {
        [System.Xml.Serialization.XmlAttribute]
        public string Code;
        [System.Xml.Serialization.XmlText]
        public string Name;
    }

    [Serializable]
    public static class ExtentionMethods
    {
        public static object GetValue(this System.Data.SqlClient.SqlDataReader dataReader, string column)
        {
            if (dataReader == null) return null;
            if (dataReader.GetSchemaTable().Rows[0].Table.Select("ColumnName='" + column + "'").Length != 0 && dataReader[column] != DBNull.Value)
                return dataReader[column];
            return null;
        }
    }

    public class Address
    {
        public string LineOne;
        public string LineTwo;
        //public string Country;
        //public string State;
        //public string City;
        public Location Country = new Location();
        public Location State = new Location();
        public Location City = new Location();
        public string District;
        public string ZipCode;

        public override string ToString()
        {
            string retStr = string.Empty;
            if (!string.IsNullOrEmpty(this.LineOne)) retStr += this.LineOne;
            if (!string.IsNullOrEmpty(this.LineTwo)) retStr += (retStr != string.Empty ? ", " + this.LineTwo : this.LineTwo);
            if (this.City != null && !string.IsNullOrEmpty(this.City.Name)) retStr += (retStr != string.Empty ? ", " + this.City.Name : this.City.Name);
            if (!string.IsNullOrEmpty(this.ZipCode)) retStr += (retStr != string.Empty ? ", " + this.ZipCode : this.ZipCode);
            if (this.State != null && !string.IsNullOrEmpty(this.State.Name)) retStr += (retStr != string.Empty ? ", " + this.State.Name : this.State.Name);
            if (this.Country != null && !string.IsNullOrEmpty(this.Country.Name)) retStr += (retStr != string.Empty ? ", " + this.Country.Name : this.Country.Name);

            return retStr; // == string.Empty ? null : retStr;
        }
    }   
}