using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for clsMailAddress
/// </summary>

public class clxMailAddress
{
    public string m_sUserName { get; set; }
    public string m_sUserMail { get; set; }

    public clxMailAddress()
    {

    }

    public clxMailAddress(string sUserName, string sUserMail)
    {
        try
        {
            m_sUserName = sUserName;
            m_sUserMail = sUserMail;
            if (sUserName.Length == 0) { sUserName = sUserMail; }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    public clxMailAddress(clxMailAddress eUserInfo)
    {
        try
        {
            m_sUserName = eUserInfo.m_sUserName;
            m_sUserMail = eUserInfo.m_sUserMail;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }
}