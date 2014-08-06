using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace UBERREP.BusinessLayer.Common
{
    public static class CustomValidator
    {
        public static Dictionary<InputType, ValidationItem> RegExCache
        {
            get
            {
                return System.Web.HttpRuntime.Cache["regex"] as Dictionary<InputType, ValidationItem>;
            }
            set
            {
                System.Web.HttpRuntime.Cache.Insert("regex", value, new System.Web.Caching.CacheDependency(AppDomain.CurrentDomain.BaseDirectory + "Data\\Patterns.xml"));
            }
        }

        private static void LoadPatterns()
        {
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(List<ValidationItem>));
            List<ValidationItem> patterns = new List<ValidationItem>();

            try
            {
                using (System.Xml.XmlTextReader xReader = new System.Xml.XmlTextReader(AppDomain.CurrentDomain.BaseDirectory + "Data\\Patterns.xml"))
                {
                    xReader.Normalization = true;
                    xReader.WhitespaceHandling = System.Xml.WhitespaceHandling.None;
                    patterns = (System.Collections.Generic.List<ValidationItem>)xs.Deserialize(xReader);
                    xReader.Close();
                }
            }
            catch (Exception ex) { ((UBERREP.BusinessLayer.Common.Diagnostics.Exception)ex).LogError(); }
            finally
            {
                xs = null;
            }
            RegExCache = new Dictionary<InputType, ValidationItem>();
            foreach (ValidationItem item in patterns)
            {
                RegExCache.Add(item.Type, item);
            }
        }

        public static bool IsValidInput(this object control, InputType type)
        {
            string value = string.Empty;
            if (control.GetType() == typeof(System.Web.UI.WebControls.TextBox))
            {
                value = ((System.Web.UI.WebControls.TextBox)control).Text.Trim();
            }
            else if (control.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
            {
                value = ((System.Web.UI.WebControls.DropDownList)control).SelectedValue;
            }
            else value = control.ToString();

            if (RegExCache == null) LoadPatterns();
            if (RegExCache != null && RegExCache.ContainsKey(type)) return Regex.IsMatch(value, @RegExCache[type].Pattern);
            else
            {
                new UBERREP.BusinessLayer.Common.Diagnostics.Exception("", "Validation cache not loaded or key not found from data file").LogError();
                return false;
            }
        }

        public static bool IsValidDocument(this System.Web.UI.WebControls.FileUpload uploadedFile, string[] allowedExtentions, int maxFileSize)
        {
            if (uploadedFile.PostedFile.ContentLength > maxFileSize) return false;
            string fileExtension = System.IO.Path.GetExtension(uploadedFile.FileName).ToLower();
            int index = Array.IndexOf(allowedExtentions, fileExtension);
            return index != -1;
        }
    }

    public sealed class ValidationItem
    {
        public string Pattern;
        public InputType Type;
    }

    public enum InputType
    {

        Decimal,
        Int16,

        Address,
        City,
        ZipCode,
        Phone,
        Mobile,
        Fax,
        EMailAddress,

        //
        Date,
        Month,
        Year,
        CategoryCode,

        //Guest Inputs
        Name,
        Username,
        Password,

        ClientID
    }
}
