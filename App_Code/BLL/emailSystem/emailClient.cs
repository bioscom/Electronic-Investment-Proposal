using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

/// <summary>
/// Summary description for emailClient
/// </summary>

public class emailClient
{
    public const string c_sShellMailExt = "@shell.com";

    private static clxMailAddress m_eAdmin = new clxMailAddress(AppConfiguration.AdminName, AppConfiguration.AdminEmail);
    private static string m_sHost = AppConfiguration.SmtpHost;
    private static bool m_bBccAdmin = AppConfiguration.BccAdmin;

    //TODO: gradually replace ex & idx with clxMailAdress
    private smtpxClient oMail;

    public emailClient(bool bUseAdmin)
    {
        oMail = new smtpxClient(m_sHost, m_eAdmin);
    }

    public emailClient(structUserMailIdx _eSender)
    {
        oMail = new smtpxClient(m_sHost, new clxMailAddress(_eSender.m_sUserName, _eSender.m_sUserMail));
    }

    public emailClient(clxMailAddress _eSender)
    {
        oMail = new smtpxClient(m_sHost, _eSender);
    }

    public emailClient(structUserMailEx _eSender)
    {
        oMail = new smtpxClient(m_sHost, new clxMailAddress(_eSender.m_sUserName, _eSender.m_sUserMail));
    }

    public bool sendShellMail(List<structUserMailIdx> eToList, List<structUserMailIdx> eCcList, string sSubjet, string sBody)
    {
        List<clxMailAddress> eBccList = new List<clxMailAddress>();
        try
        {
            if (m_bBccAdmin)
            {
                eBccList.Add(m_eAdmin);
            }
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
        return oMail.sendSmtpMail(formatEmail(eToList), formatEmail(eCcList), eBccList, sSubjet, sBody);
    }

    public bool sendShellMail(List<structUserMailIdx> eToList, structUserMailIdx eCc, structUserMailIdx eBcc, string sSubjet, string sBody)
    {
        bool bRet = false;
        try
        {
            List<clxMailAddress> eCcList = new List<clxMailAddress>();
            List<clxMailAddress> eBccList = new List<clxMailAddress>();
            eCcList.Add(formatEmail(eCc));
            eBccList.Add(formatEmail(eBcc));
            if (m_bBccAdmin) eBccList.Add(m_eAdmin);
            bRet = oMail.sendSmtpMail(formatEmail(eToList), eCcList, eBccList, sSubjet, sBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
        return bRet;
    }

    public bool sendShellMail(List<structUserMailIdx> eToList, structUserMailIdx eCc, string sSubjet, string sBody)
    {
        bool bRet = false;
        try
        {
            List<clxMailAddress> eCcList = new List<clxMailAddress>();
            List<clxMailAddress> eBccList = new List<clxMailAddress>();

            eCcList.Add(formatEmail(eCc));
            if (m_bBccAdmin) eBccList.Add(m_eAdmin);
            bRet = oMail.sendSmtpMail(formatEmail(eToList), eCcList, eBccList, sSubjet, sBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
        return bRet;
    }

    public bool sendShellMail(List<structUserMailIdx> eToList, string sSubjet, string sBody)
    {
        bool bRet = false;
        try
        {
            List<clxMailAddress> eCcList = new List<clxMailAddress>();
            List<clxMailAddress> eBccList = new List<clxMailAddress>();
            if (m_bBccAdmin) eBccList.Add(m_eAdmin);
            bRet = oMail.sendSmtpMail(formatEmail(eToList), eCcList, eBccList, sSubjet, sBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
        return bRet;
    }

    public bool sendShellMail(structUserMailIdx eTo, structUserMailIdx eCc, string sSubjet, string sBody)
    {
        bool bRet = false;
        try
        {
            List<clxMailAddress> eCcList = new List<clxMailAddress>();
            List<clxMailAddress> eBccList = new List<clxMailAddress>();
            eCcList.Add(formatEmail(eCc));
            if (m_bBccAdmin) eBccList.Add(m_eAdmin);
            bRet = oMail.sendSmtpMail(formatEmail(eTo), eCcList, eBccList, sSubjet, sBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
        return bRet;
    }

    public bool sendShellMail(structUserMailIdx eTo, string sSubjet, string sBody)
    {
        bool bRet = false;
        try
        {
            List<clxMailAddress> eCcList = new List<clxMailAddress>();
            List<clxMailAddress> eBccList = new List<clxMailAddress>();
            if (m_bBccAdmin) eBccList.Add(m_eAdmin);
            bRet = oMail.sendSmtpMail(formatEmail(eTo), eCcList, eBccList, sSubjet, sBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
        return bRet;
    }

    public bool sendShellMail(structUserMailIdx eTo, List<structUserMailIdx> eCcList, string sSubjet, string sBody)
    {
        bool bRet = false;
        try
        {
            List<clxMailAddress> eBccList = new List<clxMailAddress>();
            if (m_bBccAdmin) eBccList.Add(m_eAdmin);
            bRet = oMail.sendSmtpMail(formatEmail(eTo), formatEmail(eCcList), eBccList, sSubjet, sBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
        return bRet;
    }

    public bool sendShellMail(structUserMailIdx eTo, string sSubjet, string sBody, string sAttach)
    {
        bool bRet = false;
        try
        {
            bRet = oMail.sendSmtpMail(formatEmail(eTo), sSubjet, sBody, sAttach);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
        return bRet;
    }

    public bool sendShellMail(structUserMailIdx eTo, string sSubjet, string sBody, List<FileStream> lstAttach, Proposal oProposal)
    {
        bool bRet = false;
        try
        {
            bRet = oMail.sendSmtpMailFS(formatEmail(eTo), sSubjet, sBody, lstAttach, oProposal);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
        return bRet;
    }

    public bool sendShellMail(List<structUserMailEx> eToList, List<structUserMailEx> eCcList, string sSubjet, string sBody)
    {
        bool bRet = false;
        try
        {
            List<clxMailAddress> eBccList = new List<clxMailAddress>();
            if (m_bBccAdmin) eBccList.Add(m_eAdmin);
            bRet = oMail.sendSmtpMail(formatEmail(eToList), formatEmail(eCcList), eBccList, sSubjet, sBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
        return bRet;
    }

    public bool sendShellMail(List<structUserMailEx> eToList, structUserMailEx eCc, structUserMailEx eBcc, string sSubjet, string sBody)
    {
        bool bRet = false;
        try
        {
            List<clxMailAddress> eCcList = new List<clxMailAddress>();
            List<clxMailAddress> eBccList = new List<clxMailAddress>();
            eCcList.Add(formatEmail(eCc));
            eBccList.Add(formatEmail(eBcc));
            if (m_bBccAdmin) eBccList.Add(m_eAdmin);
            bRet = oMail.sendSmtpMail(formatEmail(eToList), eCcList, eBccList, sSubjet, sBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
        return bRet;
    }

    public bool sendShellMail(List<structUserMailEx> eToList, structUserMailEx eCc, string sSubjet, string sBody)
    {
        bool bRet = false;
        try
        {
            List<clxMailAddress> eCcList = new List<clxMailAddress>();
            List<clxMailAddress> eBccList = new List<clxMailAddress>();
            eCcList.Add(formatEmail(eCc));
            if (m_bBccAdmin) eBccList.Add(m_eAdmin);
            bRet = oMail.sendSmtpMail(formatEmail(eToList), eCcList, eBccList, sSubjet, sBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
        return bRet;
    }

    public bool sendShellMail(List<structUserMailEx> eToList, string sSubjet, string sBody)
    {
        bool bRet = false;
        try
        {
            List<clxMailAddress> eCcList = new List<clxMailAddress>();
            List<clxMailAddress> eBccList = new List<clxMailAddress>();
            if (m_bBccAdmin) eBccList.Add(m_eAdmin);
            bRet = oMail.sendSmtpMail(formatEmail(eToList), eCcList, eBccList, sSubjet, sBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
        return bRet;
    }

    //public bool sendShellMail(List<structUserMailEx> eTo, structUserMailEx eCc, string sSubjet, string sBody)
    //{
    //    bool bRet = false;
    //    try
    //    {
    //        List<clxMailAddress> eCcList = new List<clxMailAddress>();
    //        List<clxMailAddress> eBccList = new List<clxMailAddress>();
    //        eCcList.Add(formatEmail(eCc));
    //        if (m_bBccAdmin) eBccList.Add(m_eAdmin);
    //        bRet = oMail.sendSmtpMail(formatEmail(eTo), eCcList, eBccList, sSubjet, sBody);
    //    }
    //    catch (Exception ex)
    //    {
    //        appMonitor.logAppExceptions(ex);
    //    }
    //    return bRet;
    //}

    public bool sendShellMail(structUserMailEx eTo, string sSubjet, string sBody)
    {
        bool bRet = false;
        try
        {
            List<clxMailAddress> eCcList = new List<clxMailAddress>();
            List<clxMailAddress> eBccList = new List<clxMailAddress>();
            if (m_bBccAdmin) eBccList.Add(m_eAdmin);
            bRet = oMail.sendSmtpMail(formatEmail(eTo), eCcList, eBccList, sSubjet, sBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
        return bRet;
    }

    public bool sendShellMail(structUserMailEx eTo, List<structUserMailEx> eCcList, string sSubjet, string sBody)
    {
        bool bRet = false;
        try
        {
            List<clxMailAddress> eBccList = new List<clxMailAddress>();
            if (m_bBccAdmin) eBccList.Add(m_eAdmin);
            bRet = oMail.sendSmtpMail(formatEmail(eTo), formatEmail(eCcList), eBccList, sSubjet, sBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }

        return bRet;
    }

    public static structUserMailIdx adminEmail()
    {
        structUserMailIdx eRet = new structUserMailIdx();
        try
        {
            eRet = new structUserMailIdx(m_eAdmin.m_sUserName, m_eAdmin.m_sUserMail, "XX_XX");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return eRet;
    }

    public bool sendAdminAppMail(string sSubjet, string sBody)
    {
        return oMail.sendSmtpMail(m_eAdmin, sSubjet, sBody);
    }

    private static clxMailAddress formatEmail(structUserMailEx rUserx)
    {
        return new clxMailAddress(rUserx.m_sUserName, rUserx.m_sUserMail);
    }

    private static clxMailAddress formatEmail(structUserMailIdx rUserx)
    {
        return new clxMailAddress(rUserx.m_sUserName, rUserx.m_sUserMail);
    }

    private static List<clxMailAddress> formatEmail(List<structUserMailEx> oUserx)
    {
        List<clxMailAddress> oRet = new List<clxMailAddress>();
        foreach (structUserMailEx rUserx in oUserx)
        {
            oRet.Add(new clxMailAddress(rUserx.m_sUserName, rUserx.m_sUserMail));
        }
        return oRet;
    }

    private static List<clxMailAddress> formatEmail(List<structUserMailIdx> oUserx)
    {
        List<clxMailAddress> oRet = new List<clxMailAddress>();
        foreach (structUserMailIdx rUserx in oUserx)
        {
            oRet.Add(new clxMailAddress(rUserx.m_sUserName, rUserx.m_sUserMail));
        }
        return oRet;
    }

    //clxMailAdress
    public bool sendShellMail(List<clxMailAddress> eToList, List<clxMailAddress> eCcList, string sSubject, string sBody)
    {
        bool bRet = false;
        try
        {
            List<clxMailAddress> eBccList = new List<clxMailAddress>();
            if (m_bBccAdmin) eBccList.Add(m_eAdmin);
            bRet = oMail.sendSmtpMail(eToList, eCcList, eBccList, sSubject, sBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
        return bRet;
    }

    public bool sendShellMail(List<clxMailAddress> eToList, clxMailAddress eCc, clxMailAddress eBcc, string sSubjet, string sBody)
    {
        bool bRet = false;
        try
        {
            List<clxMailAddress> eBccList = new List<clxMailAddress>();
            List<clxMailAddress> eCcList = new List<clxMailAddress>();
            eCcList.Add(eCc);
            eBccList.Add(eBcc);
            if (m_bBccAdmin) eBccList.Add(m_eAdmin);
            bRet = oMail.sendSmtpMail(eToList, eCcList, eBccList, sSubjet, sBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
        return bRet;
    }

    public bool sendShellMail(List<clxMailAddress> eToList, clxMailAddress eCc, string sSubjet, string sBody)
    {
        List<clxMailAddress> eCcList = new List<clxMailAddress>();
        List<clxMailAddress> eBccList = new List<clxMailAddress>();
        bool bRet = false;
        try
        {
            eCcList.Add(eCc);
            if (m_bBccAdmin) eBccList.Add(m_eAdmin);
            oMail.sendSmtpMail(eToList, eCcList, eBccList, sSubjet, sBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
        return bRet;
    }

    public bool sendShellMail(List<clxMailAddress> eToList, string sSubjet, string sBody)
    {
        bool bRet = false;
        try
        {
            List<clxMailAddress> eCcList = new List<clxMailAddress>();
            List<clxMailAddress> eBccList = new List<clxMailAddress>();
            if (m_bBccAdmin) eBccList.Add(m_eAdmin);
            bRet = oMail.sendSmtpMail(eToList, eCcList, eBccList, sSubjet, sBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
        return bRet;
    }

    public bool sendShellMail(clxMailAddress eTo, clxMailAddress eCc, string sSubjet, string sBody)
    {
        bool bRet = false;
        try
        {
            List<clxMailAddress> eCcList = new List<clxMailAddress>();
            List<clxMailAddress> eBccList = new List<clxMailAddress>();
            eCcList.Add(eCc);
            if (m_bBccAdmin) eBccList.Add(m_eAdmin);
            bRet = oMail.sendSmtpMail(eTo, eCcList, eBccList, sSubjet, sBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
        return bRet;
    }

    public bool sendShellMail(clxMailAddress eTo, string sSubjet, string sBody)
    {
        bool bRet = false;
        try
        {
            List<clxMailAddress> eCcList = new List<clxMailAddress>();
            List<clxMailAddress> eBccList = new List<clxMailAddress>();
            if (m_bBccAdmin) eBccList.Add(m_eAdmin);
            bRet = oMail.sendSmtpMail(eTo, eCcList, eBccList, sSubjet, sBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
        return bRet;
    }

    public bool sendShellMail(clxMailAddress eTo, List<clxMailAddress> eCcList, string sSubjet, string sBody)
    {
        bool bRet = false;
        try
        {
            List<clxMailAddress> eBccList = new List<clxMailAddress>();
            if (m_bBccAdmin) eBccList.Add(m_eAdmin);
            bRet = oMail.sendSmtpMail(eTo, eCcList, eBccList, sSubjet, sBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
        return bRet;
    }

    public bool sendShellMail(clxMailAddress eTo, string sSubjet, string sBody, string sAttach)
    {
        bool bRet = false;
        try
        {
            bRet = oMail.sendSmtpMail(eTo, sSubjet, sBody, sAttach);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
        return bRet;
    }

    //Public Overloads Function XXXsendShellMail(ByVal eTo As clxMailAdress, ByVal sSubjet As String, ByVal sBody As String) As Boolean
    //    Try
    //        Dim eCcList As New List(Of clxMailAdress)
    //        Dim eBccList As New List(Of clxMailAdress)
    //        If m_bBccAdmin Then eBccList.Add(m_eAdmin)
    //        Return oMail.sendSmtpMail(eTo, eCcList, eBccList, sSubjet, sBody)
    //    Catch ex As Exception
    //        appMonitor.logAppExceptions(ex)
    //    End Try
    //End Function

    //Public Overloads Function XXXsendShellMail(ByVal eTo As clxMailAdress, ByVal eCc As clxMailAdress, ByVal sSubjet As String, ByVal sBody As String) As Boolean
    //    Try
    //        Dim eCcList As New List(Of clxMailAdress)
    //        Dim eBccList As New List(Of clxMailAdress)
    //        eCcList.Add(eCc)
    //        If m_bBccAdmin Then eBccList.Add(m_eAdmin)
    //        Return oMail.sendSmtpMail(eTo, eCcList, eBccList, sSubjet, sBody)
    //    Catch ex As Exception
    //        appMonitor.logAppExceptions(ex)
    //    End Try
    //End Function

    //Public Overloads Function XXXsendShellMail(ByVal eToList As List(Of clxMailAdress), ByVal eCc As clxMailAdress, ByVal sSubjet As String, ByVal sBody As String) As Boolean
    //    Try
    //        Dim eCcList As New List(Of clxMailAdress)
    //        Dim eBccList As New List(Of clxMailAdress)
    //        eCcList.Add(eCc)
    //        If m_bBccAdmin Then eBccList.Add(m_eAdmin)
    //        Return oMail.sendSmtpMail(eToList, eCcList, eBccList, sSubjet, sBody)
    //    Catch ex As Exception
    //        appMonitor.logAppExceptions(ex)
    //    End Try
    //End Function

    //Public Overloads Function XXXsendShellMail(ByVal eTo As clxMailAdress, ByVal eCcList As List(Of clxMailAdress), ByVal sSubjet As String, ByVal sBody As String) As Boolean
    //    Try
    //        Dim eBccList As New List(Of clxMailAdress)
    //        If m_bBccAdmin Then eBccList.Add(m_eAdmin)
    //        Return oMail.sendSmtpMail(eTo, eCcList, eBccList, sSubjet, sBody)
    //    Catch ex As Exception
    //        appMonitor.logAppExceptions(ex)
    //    End Try
    //End Function

    //'idx
}