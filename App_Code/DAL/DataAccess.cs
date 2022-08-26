using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;

/// <summary>
/// Summary description for DataAccess
/// </summary>
public static class DataAccess
{
	static DataAccess()
	{
		
	}

    public static bool ExecuteNonQueryCommand(string sql)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();

        comm.CommandText = sql;
        int result = -1;
        try
        {
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            // any errors are logged in GenericDataAccess, we ignore them here
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return (result != -1);
    }

    public static DataTable ExecuteQueryCommand(string sql)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;
        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);

        return dt;
    }

}
