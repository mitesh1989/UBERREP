using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;


namespace UBERREP.BusinessLayer.Common
{
    public class CurrentContext
    {
        internal static ContextStorage CStorage = new ContextStorage();


        public static BusinessLayer.Users.User CurrentUser
        {
            get
            {
                return CStorage.GetData("CContext_User") as BusinessLayer.Users.User;
            }
            set
            {
                CStorage.SetData("CContext_User", value);
            }
        }   
       
        public static Object EntityToEdit
        {
            get
            {
                object retValue = CStorage.GetData("CContext_EntityToEdit");
                return retValue;
            }
            set
            {
                CStorage.SetData("CContext_EntityToEdit", value);
            }
        }


        public static List<UBERREP.BusinessLayer.Common.Diagnostics.Exception> GetExecutionErrors(bool reset)
        {
            object storedData = CStorage.GetData("CContext_ExecErrors");
            List<UBERREP.BusinessLayer.Common.Diagnostics.Exception> retObj = storedData != null ? (List<UBERREP.BusinessLayer.Common.Diagnostics.Exception>)storedData : new List<UBERREP.BusinessLayer.Common.Diagnostics.Exception>();

            //reset cache if required
            if (reset) CStorage.SetData("CContext_ExecErrors", null);
            //return
            return retObj;
        }
        public static List<UBERREP.BusinessLayer.Common.Diagnostics.Warning> GetExecutionWarnings(bool reset)
        {
            object storedData = CStorage.GetData("CContext_ExecWarnings");
            List<UBERREP.BusinessLayer.Common.Diagnostics.Warning> retObj = storedData != null ? (List<UBERREP.BusinessLayer.Common.Diagnostics.Warning>)storedData : new List<UBERREP.BusinessLayer.Common.Diagnostics.Warning>();

            //reset cache if required
            if (reset) CStorage.SetData("CContext_ExecWarnings", null);
            //return
            return retObj;
        }

        public static bool HasErrors
        {
            get
            {
                return GetExecutionErrors(false).Count > 0;
            }
        }

        internal static void AddExecutionError(UBERREP.BusinessLayer.Common.Diagnostics.Exception ex)
        {
            object storedData = CStorage.GetData("CContext_ExecErrors");
            List<UBERREP.BusinessLayer.Common.Diagnostics.Exception> storedDataList = storedData != null ? (List<UBERREP.BusinessLayer.Common.Diagnostics.Exception>)storedData : new List<UBERREP.BusinessLayer.Common.Diagnostics.Exception>();
            storedDataList.Add(ex);
            CStorage.SetData("CContext_ExecErrors", storedDataList);
        }

        internal static void AddExecutionWarning(UBERREP.BusinessLayer.Common.Diagnostics.Warning ex)
        {
            object storedData = CStorage.GetData("CContext_ExecWarnings");
            List<UBERREP.BusinessLayer.Common.Diagnostics.Warning> storedDataList = storedData != null ? (List<UBERREP.BusinessLayer.Common.Diagnostics.Warning>)storedData : new List<UBERREP.BusinessLayer.Common.Diagnostics.Warning>();
            storedDataList.Add(ex);
            CStorage.SetData("CContext_ExecWarnings", storedDataList);
        }



        internal class ContextStorage
        {
            internal object GetData(string name)
            {
                HttpContext c = HttpContext.Current;
                if (c != null)
                {
                    System.Web.SessionState.HttpSessionState s = c.Session;

                    if (s != null) return s[name];
                }
                return null;
            }
            internal void SetData(string name, object data)
            {
                HttpContext c = HttpContext.Current;
                System.Web.SessionState.HttpSessionState s=null;
                if(c !=null)
                    s = c.Session;
                if (s != null) s[name] = data;
            }
        }
    }
}
