using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UBERREP.BusinessLayer.Base
{
    public abstract class BaseManager
    {
        public static DbObject Get(DbObject obj)
        {
            try
            {
                obj.Fetch();
                return obj;
            }
            catch (System.Exception ex)
            {
                UBERREP.BusinessLayer.Common.CurrentContext.AddExecutionError(new UBERREP.BusinessLayer.Common.Diagnostics.Exception("101", "No records found for requested data"));
                ((UBERREP.BusinessLayer.Common.Diagnostics.Exception)ex).LogError();
                return null;
            }

        }
        public static DbObject Create(DbObject obj)
        {
            try
            {
                obj.Create();
                return obj;
            }
            catch (System.Data.SqlClient.SqlException sEx)
            {
                if (sEx.Number == 2627 || sEx.Number == 2601)
                {// index violation
                    UBERREP.BusinessLayer.Common.CurrentContext.AddExecutionError(new UBERREP.BusinessLayer.Common.Diagnostics.Exception("102", "Entity already exists.Please try with other value(s)."));
                }
                else
                {
                    UBERREP.BusinessLayer.Common.CurrentContext.AddExecutionError(new UBERREP.BusinessLayer.Common.Diagnostics.Exception("100", null)); // generic error message
                    ((UBERREP.BusinessLayer.Common.Diagnostics.Exception)sEx).LogError();
                }
            }
            return null;
        }

        public static bool Update(DbObject obj)
        {
            try
            {
                obj.Update();
                return true;
            }
            catch (System.Data.SqlClient.SqlException sEx)
            {
                if (sEx.Number == 2627 || sEx.Number == 2601)
                {// index violation
                    UBERREP.BusinessLayer.Common.CurrentContext.AddExecutionError(new UBERREP.BusinessLayer.Common.Diagnostics.Exception("103", "Entity already exists.Please try with other value(s)."));
                }
                else
                {
                    ((UBERREP.BusinessLayer.Common.Diagnostics.Exception)sEx).LogError();
                    UBERREP.BusinessLayer.Common.CurrentContext.AddExecutionError(new UBERREP.BusinessLayer.Common.Diagnostics.Exception("100", null)); // generic error message
                }
            }
            return false;
        }
        public static bool Delete(DbObject obj)
        {
            try
            {
                obj.Delete();
                return true;
            }
            catch (System.Data.SqlClient.SqlException sEx)
            {
                if (sEx.Message.Contains("-1000"))
                {// foreign key violation  - fixed by sp
                    UBERREP.BusinessLayer.Common.CurrentContext.AddExecutionWarning(new UBERREP.BusinessLayer.Common.Diagnostics.Warning("104", "Entity has been inactivated instead of delete due to other dependable entities.<li>" + sEx.Message.Split('|')[1] + "</li>"));
                }
                else if (sEx.Number == 547)
                {// foreign key violation 
                    UBERREP.BusinessLayer.Common.CurrentContext.AddExecutionError(new UBERREP.BusinessLayer.Common.Diagnostics.Exception("104", "Can not delete this as it's been used by other dependable entities.<li>" + sEx.Message + "</li>"));
                }
                else
                {
                    ((UBERREP.BusinessLayer.Common.Diagnostics.Exception)sEx).LogError();
                    UBERREP.BusinessLayer.Common.CurrentContext.AddExecutionError(new UBERREP.BusinessLayer.Common.Diagnostics.Exception("100", null)); // generic error message
                }
            }
            return false;
        }
    }
}
