using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;


/// <summary>
/// Summary description for OU
/// </summary>
public class OU
{
    public int m_iCompanyId {get; set;}
    public string s_CompanyName {get; set;}

	public OU()
	{
		
	}

    public OU(DataRow dr)
    {
        m_iCompanyId = int.Parse(dr["COMPANYID"].ToString());
        s_CompanyName = dr["COMPANYNAME"].ToString();
    }

    public DataTable dtGetOUs()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getOUs();

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public OU objGetOU()
    {
        DataTable dt = dtGetOUs();

        OU xRows = new OU();
        foreach (DataRow dr in dt.Rows)
        {
            xRows = new OU(dr);
        }
        return xRows;
    }

    public List<OU> lstGetOU()
    {
        DataTable dt = dtGetOUs();

        List<OU> xRows = new List<OU>(dt.Rows.Count);
        foreach (DataRow dr in dt.Rows)
        {
            xRows.Add(new OU(dr));
        }
        return xRows;
    }
}