using System;
using System.Collections;
using System.Collections.Generic;

public partial class AppEvent : aspnetPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        sendMail oSendMail = new sendMail(oSessnx.getOnlineUser.structUserIdx);
        try
        {
            //Code that runs when an unhandled error occurs
            Exception objErr = Server.GetLastError().GetBaseException();
            string err = "<b>Error Caught in Application_Error event<b/> <br/> <br/>";
            err += "Error in: " + Request.Url.ToString() + "<br/>";
            err += "<br/>Error Message: " + objErr.Message.ToString() + "";
            err += "<br/> <br/>Stack Trace: " + objErr.StackTrace.ToString();
            //EventLog.WriteEntry("EIP_WebApp_Errors", err, EventLogEntryType.Error);
            Server.ClearError();

            appUserMgt oUserMgt = new appUserMgt();

            List<structUserMailIdx> Admins = new List<structUserMailIdx>();
            List<appUsers> Administrators = oUserMgt.lstGetUsersByRole((int)appUsersRoles.userRole.Administrator);
            foreach (appUsers Administrator in Administrators)
            {
                if (Administrator.m_iDefaultRoleHolder == DefaultRoleHolder.iDefault)
                {
                    Admins.Add(Administrator.structUserIdx);
                }
            }

            oSendMail.ApplicationErrorMessage(Admins, err);
            Server.Transfer("~/Index.aspx");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }
}



//public partial class AppEvent : System.Web.UI.Page
//{
//    structUserMailIdx systemAdmin_email = new structUserMailIdx();
//    //ArrayList al = new ArrayList();
//    protected void Page_Load(object sender, EventArgs e)
//    {
//        try
//        {
//            //Code that runs when an unhandled error occurs
//            Exception objErr = Server.GetLastError().GetBaseException();
//            string err = "<b>Error Caught in Application_Error event<b/> <br/> <br/>";
//            err += "Error in: " + Request.Url.ToString() + "<br/>";
//            err += "<br/>Error Message: " + objErr.Message.ToString() + "";
//            err += "<br/> <br/>Stack Trace: " + objErr.StackTrace.ToString();
//            //EventLog.WriteEntry("EIP_WebApp_Errors", err, EventLogEntryType.Error);
//            Server.ClearError();

//            appUserMgt oAppUserMgt = new appUserMgt();
//            List<appUsers> oAppUsers = oAppUserMgt.lstGetUsersByRole((int)appUsersRoles.userRole.Administrator);
//            foreach (appUsers oAppUser in oAppUsers)
//            {
//                if (oAppUser.m_iFLAG_COLOR == DefaultBPO.iDefault)
//                {
//                    systemAdmin_email = oAppUser.structUserIdx;
//                }
//            }

//            sendMail oSendMail = new sendMail(); //AppConfiguration.AdminEmail

//            oSendMail.ApplicationErrorMessage(systemAdmin_email, systemAdmin_email, err);
//            Server.Transfer("~/Index.aspx");
//        }
//        catch (Exception ex)
//        {
//            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
//        }
//    }
//}