using System.Data;
using System.Data.Common;
using System.Collections.Generic;

/// <summary>
/// Summary description for shellCompanies
/// </summary>
/// 

public class shellOus
{
    //Fields
    public int m_iCompanyId { get; set; }
    public string m_sCompanyname { get; set; }
    public string m_sCountry { get; set; }
    public string m_sDescription { get; set; }

    //Constructor
    public shellOus()
    {

    }

    public shellOus(DataRow dr)
    {
        m_iCompanyId = int.Parse(dr["COMPANYID"].ToString());
        m_sCompanyname = dr["COMPANYNAME"].ToString();
        m_sCountry = dr["COUNTRY"].ToString();
        m_sDescription = dr["DESCRIPTION"].ToString();
    }

}

public class shellCompanies
{
	public shellCompanies()
	{
		
	}

    public DataTable dtGetShellCompanies()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getShellCompanies();

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public List<shellOus> lstGetShellCompanies()
    {
        DataTable dt = dtGetShellCompanies();

        List<shellOus> xRows = new List<shellOus>(dt.Rows.Count);
        foreach (DataRow dr in dt.Rows)
        {
            xRows.Add(new shellOus(dr));
        }
        return xRows;
    }
    public shellOus objGetShellCompanyById(int iCompanyId)
    {
        DataTable dt = dtGetShellCompaniesByCompanyId(iCompanyId);

        shellOus oRow = new shellOus();
        foreach (DataRow dr in dt.Rows)
        {
            oRow = new shellOus(dr);
        }
        return oRow;
    }

    public DataTable dtGetShellCompaniesByCompanyId(int iCompanyId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getCompanyByCompanyID();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":COMPANYID";
        param.Value = iCompanyId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public DataTable dtGetShellCompanyByName(string sName)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getCompanyByCompanyName();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":COMPANYNAME";
        param.Value = sName;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
}