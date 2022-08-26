using System;
using System.Collections.Generic;
using System.Text;
using System.Web.SessionState;

public class httpSessionx
{
    private const string c_sLoginInfo = "loginInfo";
    private HttpSessionState m_oSessnx;

    //other session state management
    public const string c_sLineStats = "lineStats";

    public httpSessionx(HttpSessionState oSession)
    {
        try
        {
            m_oSessnx = oSession;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    public httpSessionx(HttpSessionState oSession, appUsers oLogin)
    {
        try
        {
            m_oSessnx = oSession;
            if (m_oSessnx[c_sLoginInfo] != null)//Is Nothing Then
            {
                m_oSessnx.Remove(c_sLoginInfo);
            }
            m_oSessnx[c_sLoginInfo] = oLogin;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    public appUsers getOnlineUser
    {
        get
        {
            appUsers oRet = new appUsers();
            if (m_oSessnx[c_sLoginInfo] != null) //Is Nothing Then
            {
                oRet = (appUsers)m_oSessnx[c_sLoginInfo];
            }
            return oRet;
        }

        set
        {
            try
            {
                if (m_oSessnx[c_sLoginInfo] != null) //Is Nothing Then
                {
                    m_oSessnx.Remove(c_sLoginInfo);
                }
                m_oSessnx[c_sLoginInfo] = value;
                //(onlineUser value)
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
            }
        }
    }
}
