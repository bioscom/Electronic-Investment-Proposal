using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for structUserMail
/// </summary>

public class structUserMail
{
    public string m_sUserName { get; set; }
    public string m_sUserMail { get; set; }

    public structUserMail()
    {

    }

    public structUserMail(string sUserName, string sUserMail)
    {
        try
        {
            m_sUserName = sUserName;
            m_sUserMail = sUserMail;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }
}