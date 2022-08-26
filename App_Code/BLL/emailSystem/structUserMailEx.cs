using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for structUserMailEx
/// </summary>

public class structUserMailEx : structUserMailIdx
{
    private byte m_tBizRole = 0;
    public structUserMailEx()
    {
        try
        {

        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    public structUserMailEx(string sUserName, string sUserMail, string sUserId, byte tBizRole)
        : base(sUserName, sUserMail, sUserId)
    {
        try
        {
            m_tBizRole = tBizRole;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }
    public byte bizRole
    {
        get { return m_tBizRole; }
    }
}