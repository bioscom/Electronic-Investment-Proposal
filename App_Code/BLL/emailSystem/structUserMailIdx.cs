using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for structUserMailIdx
/// </summary>

public class structUserMailIdx : structUserMail
{
    private string m_sUserId = "";

    public structUserMailIdx()
    {

    }

    public structUserMailIdx(structUserMailIdx eUser)
        : base(eUser.m_sUserName, eUser.m_sUserMail)
    {
        try
        {
            m_sUserId = eUser.userId;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    public structUserMailIdx(structUserMail eUser, string sUserId)
        : base(eUser.m_sUserName, eUser.m_sUserMail)
    {
        try
        {
            m_sUserId = sUserId;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    public structUserMailIdx(string sUserName, string sUserMail, string sUserId)
        : base(sUserName, sUserMail)
    {
        try
        {
            m_sUserId = sUserId;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    public string userId
    {
        get { return m_sUserId; }
    }
}