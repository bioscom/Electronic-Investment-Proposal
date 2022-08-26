using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for appMonitor
/// </summary>

public class appMonitor
{
    public enum appDeployMode
    {
        localMachine = 1,
        testServer = 2,
        liveServer = 3
    };

    public static appDeployMode appRunningServer()
    {
        appDeployMode eRet = appDeployMode.localMachine;
        try
        {
            //eRet = (appDeployMode)  CType(webConfig.sGetAppSettingValue("cpdms.deployMode"), appDeployMode)
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return eRet;
    }

    public static bool logAppExceptions(Exception eErr)
    {
        try
        {
            System.Diagnostics.Debug.Fail(eErr.TargetSite.Name + "\n \n" + eErr.Message.ToString() + "\n \n" + eErr.StackTrace);
            string sErr = "";
            if (eErr.InnerException != null)
            {
                System.Diagnostics.Debug.Fail(eErr.InnerException.TargetSite.Name + "\n \n" + eErr.InnerException.Message + "\n \n" + eErr.InnerException.StackTrace);
                sErr = "Inner Exception Information=================" + "\n \n" + eErr.InnerException.TargetSite.Name + "\n \n" + "\n \n" + eErr.InnerException.Message + "\n \n" + "\n \n" + eErr.InnerException.StackTrace + "\n \n";
            }
            //sErr = sErr + "\n \n" + "This Exception Information================= \n\n Method: " + eErr.TargetSite.Name + "\n \n" + "\n\n Message: " + eErr.Message + "\n \n" + "\n\n Stack Trace: \n\n" + eErr.StackTrace + "\n \n Source: " + eErr.Source;

            HttpContext context = HttpContext.Current;
            sErr += "\n\n Page location: " + context.Request.RawUrl;
            // build the error message
            sErr += "\n\n Message: " + eErr.Message;
            sErr += "\n\n Source: " + eErr.Source;
            sErr += "\n\n Method: " + eErr.TargetSite;
            sErr += "\n\n Stack Trace: \n\n" + eErr.StackTrace;
            
            
            
            emailClient oMail = new emailClient(true);
            oMail.sendAdminAppMail(AppConfiguration.AppNameId + " Application Error Log", sErr);
            //'LUXOR uncomment
            //'Call windowsEventLog.writeWindowsEventLog(c_sEventLogSource, sErr, EventLogEntryType.Error, eCategory)
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return true;
    }

    public static bool logAppExceptions(string sSubject, string sErr)
    {
        try
        {
            emailClient oMail = new emailClient(true);
            oMail.sendAdminAppMail(AppConfiguration.AppNameId + " " + sSubject, sErr);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return true;
    }
}