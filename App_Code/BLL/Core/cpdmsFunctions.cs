using System.Data;

public class cpdmsFunctions
{
    private string m_sFunction;
    private string m_sFunctionID;

    public cpdmsFunctions(string FunctionID)
    {
        
        string sql = "SELECT FUNCTION, FUNCTIONID FROM CPDMS_FUNCTIONS WHERE FUNCTIONID = '" + FunctionID + "'";
        DataTable dt = DataAccess.ExecuteQueryCommand(sql);

        m_sFunction = dt.Rows[0]["FUNCTION"].ToString();
        m_sFunctionID = dt.Rows[0]["FUNCTIONID"].ToString();
    }

    public string Function
    {
        get
        {
            return m_sFunction;
        }
    }

    public string FunctionID
    {
        get
        {
            return m_sFunctionID;
        }
    }
}


public static class cpdmsFunctionsNames
{
    private static string m_sProduction;
    private static string m_sCommercial;
    private static string m_sHR;
    private static string m_sFinance;
    private static string m_sLegal;
    private static string m_sExploration;
    private static string m_sInfrastructure;
    private static string m_sHSE;
    private static string m_sTechnical;
    private static string m_sSecurity;
    private static string m_sNA;

    static cpdmsFunctionsNames()
    {
        m_sProduction = "Production";
        m_sCommercial = "Commercial";
        m_sHR = "HR";
        m_sFinance = "Finance";
        m_sLegal = "Legal Counsel";
        m_sExploration = "Exploration";
        m_sInfrastructure = "Infrastructure and Logistics";
        m_sHSE = "HSE and Business Value";
        m_sTechnical = "Technical";
        m_sSecurity = "Security";
        m_sNA = "N/A";
    }

    public static string Production
    {
        get
        {
            return m_sProduction;
        }
    }

    public static string Commercial
    {
        get
        {
            return m_sCommercial;
        }
    }

    public static string HR
    {
        get
        {
            return m_sHR;
        }
    }

    public static string Finance
    {
        get
        {
            return m_sFinance;
        }
    }

    public static string Legal
    {
        get
        {
            return m_sLegal;
        }
    }

    public static string Exploration
    {
        get
        {
            return m_sExploration;
        }
    }

    public static string Infrastructure
    {
        get
        {
            return m_sInfrastructure;
        }
    }

    public static string HSE
    {
        get
        {
            return m_sHSE;
        }
    }

    public static string Technical
    {
        get
        {
            return m_sTechnical;
        }
    }

    public static string Security
    {
        get
        {
            return m_sSecurity;
        }
    }

    public static string NA
    {
        get
        {
            return m_sNA;
        }
    }
}