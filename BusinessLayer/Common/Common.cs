using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UBERREP.BusinessLayer.Common;

namespace BusinessLayer.Common
{
    public class Common
    {
       
        public static string ValidateLogin(string userName, string passWord)
        {
            string result = string.Empty;
            if (String.IsNullOrEmpty(userName) && string.IsNullOrEmpty(passWord))
            {

                return "Username and Password Required";
            }
            if (String.IsNullOrEmpty(userName))
            {

                return "Username Required";
            }
            if (String.IsNullOrEmpty(passWord))
            {

                return "Password Required";
            }
            UBERREP.BusinessLayer.Users.User loggedInUser = UBERREP.BusinessLayer.Users.UserManager.ValidateUser(userName, passWord);


            if (loggedInUser != null)
            {// username and password validated
                switch (loggedInUser.Status)
                {
                    case Status.Active:
                        {
                            CurrentContext.CurrentUser = loggedInUser;
                         //   loggedInUser = UBERREP.BusinessLayer.Users.UserManager.GetUserAllowedGroupSections(loggedInUser);
                            result = "Success";
                            break;
                        }
                    case Status.Suspended:
                        {
                            result = "User account suspended";
                            //this.LBLErrorMessage.Text = "User account suspended";
                            break;
                        }
                    case Status.Deleted:
                        {
                            result = "User account deleted";
                            //this.LBLErrorMessage.Text = "User account deleted";
                            break;
                        }
                }
            }
            else
            {
                result = "Invalid Credential";
                //this.LBLErrorMessage.Text = "Invalid Credentials, Please enter correct username or password";
            }
            return result;
        }
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