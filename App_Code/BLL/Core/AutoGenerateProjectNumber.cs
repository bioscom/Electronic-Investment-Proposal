using System;
using System.Data;

public class AutoGenerateProjectNumber
{
    public AutoGenerateProjectNumber()
    {

    }

    public string GenerateProjectNumber(string OU)
    {
        string YYYY = ""; // YYYY = Project Origin
        string xx = DateTime.Now.Year.ToString().Remove(0, 2);// xx = year of origin
        string xxxx; //project sequencial numbering
        if (OU.Length > 4)
        {
            int i = OU.Length - 4;
            YYYY = OU.Remove(4, i);
        }
        else
        {
            YYYY = OU;
        }

        string sql = "SELECT EIP_AUTONUMBER_SEQ.NEXTVAL FROM DUAL";

        DataTable dt = DataAccess.ExecuteQueryCommand(sql);
        string xy = dt.Rows[0]["NEXTVAL"].ToString();

        if (xy.Length == 1) { xxxx = "000" + xy; }
        else if (xy.Length == 2) { xxxx = "00" + xy; }
        else if (xy.Length == 3) { xxxx = "0" + xy; }
        else { xxxx = xy; }

        return YYYY + xx + xxxx;
    }
}