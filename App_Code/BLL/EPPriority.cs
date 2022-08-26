using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;

/// <summary>
/// Summary description for EPPriority
/// </summary>
public class EPPriority
{
    public int m_iEPPriority { get; set; }
    public string m_sEPPriority { get; set; }

	public EPPriority()
	{
		
	}

    public EPPriority(DataRow dr)
    {
        m_iEPPriority = int.Parse(dr["EPPRIORITYID"].ToString());
        m_sEPPriority = dr["EPPRIORITY"].ToString();
    }

    public DataTable dtGetEPPriorities()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getEPPriorities();

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public EPPriority objGetEPPriority()
    {
        DataTable dt = dtGetEPPriorities();

        EPPriority xRows = new EPPriority();
        foreach (DataRow dr in dt.Rows)
        {
            xRows = new EPPriority(dr);
        }
        return xRows;
    }

    public List<EPPriority> lstGetEPPriority()
    {
        DataTable dt = dtGetEPPriorities();

        List<EPPriority> xRows = new List<EPPriority>(dt.Rows.Count);
        foreach (DataRow dr in dt.Rows)
        {
            xRows.Add(new EPPriority(dr));
        }
        return xRows;
    }
}