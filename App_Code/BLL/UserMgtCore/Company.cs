using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

public class Company
{
    private string m_sCOMPANYID;
    private string m_sCOMPANYNAME;
    private string m_sCOUNTRY;
    private string m_sDESCRIPTION;

    public Company()
    {

    }

    public Company(int CompanyID)
    {
        try
        {
            string sql = "SELECT COMPANYID, COMPANYNAME, COUNTRY, DESCRIPTION FROM CPDMS_SHELLCOMPANIES WHERE COMPANYID = '" + CompanyID + "'";

            DataTable dt = DataAccess.ExecuteQueryCommand(sql);
            if (dt.Rows.Count > 0)
            {
                m_sCOMPANYID = dt.Rows[0]["COMPANYID"].ToString();
                m_sCOMPANYNAME = dt.Rows[0]["COMPANYNAME"].ToString();
                m_sCOUNTRY = dt.Rows[0]["COUNTRY"].ToString();
                m_sDESCRIPTION = dt.Rows[0]["DESCRIPTION"].ToString();
            }
        }
        catch (Exception ex)
        {
            //MessageBox.Show(ex.ToString());
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    public string COMPANYID
    {
        get
        {
            return m_sCOMPANYID;
        }
    }

    public string COMPANYNAME
    {
        get
        {
            return m_sCOMPANYNAME;
        }
    }

    public string COUNTRY
    {
        get
        {
            return m_sCOUNTRY;
        }
    }

    public string DESCRIPTION
    {
        get
        {
            return m_sDESCRIPTION;
        }
    }
}