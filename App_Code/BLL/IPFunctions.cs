using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;

/// <summary>
/// Summary description for IPFunctions
/// </summary>
public class IPFunctions
{
    public int m_iFunctionId {get; set;}
    public string m_sFunction {get;set;}

	public IPFunctions()
	{
		
	}

    public IPFunctions(DataRow dr)
	{
		m_iFunctionId = int.Parse(dr["FUNCTIONID"].ToString());
        m_sFunction = dr["FUNCTION"].ToString();
	}

    public DataTable dtGetIPFunctions()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getIPFunctions();

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public DataTable dtGetIPFunctions(int iFunctionId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getIPFunctionByFunction();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":FUNCTIONID";
        param.Value = iFunctionId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public IPFunctions objGetIPFunctionByFunctionId(int iFunctionId)
    {
        DataTable dt = dtGetIPFunctions(iFunctionId);

        IPFunctions xRows = new IPFunctions();
        foreach (DataRow dr in dt.Rows)
        {
            xRows = new IPFunctions(dr);
        }
        return xRows;
    }

    public List<IPFunctions> lstGetIPFunctions()
    {
        DataTable dt = dtGetIPFunctions();

        List<IPFunctions> xRows = new List<IPFunctions>(dt.Rows.Count);
        foreach (DataRow dr in dt.Rows)
        {
            xRows.Add(new IPFunctions(dr));
        }
        return xRows;
    }
}