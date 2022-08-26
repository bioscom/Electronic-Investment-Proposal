using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;

/// <summary>
/// Summary description for cFunctions
/// </summary>
public class cFunctions
{
    public int m_iFunctionId { get; set; }
    public string m_sFunction { get; set; } 

	public cFunctions()
	{
		
	}

    public cFunctions(DataRow dr)
    {
        m_iFunctionId = int.Parse(dr["FUNCTIONID"].ToString());
        m_sFunction = dr["FUNCTION"].ToString();
    }

    public static DataTable dtGetFunctions()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getIPFunctions();

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static List<cFunctions> lstGetFunctions()
    {
        DataTable dt = dtGetFunctions();

        List<cFunctions> xRows = new List<cFunctions>(dt.Rows.Count);
        foreach (DataRow dr in dt.Rows)
        {
            xRows.Add(new cFunctions(dr));
        }
        return xRows;
    }
}