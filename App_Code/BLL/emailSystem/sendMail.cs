using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mime;
using System.Net.Mail;
using System.IO;
using System.Text;

/// <summary>
/// Summary description for sendMail
/// </summary>

public class sendMail
{
    private const string c_sMailSubjet = "Electronic Investment Proposal: ";
    private string c_sLogoPath = HttpContext.Current.Server.MapPath(@"~/Images/p_ShellLogo.gif");

    private bool success = true;

    public sendMail()
    {

    }

    private structUserMailIdx m_eSender;

    public sendMail(structUserMailIdx _eSender)
    {
        m_eSender = _eSender;
    }

    public static structUserMailIdx eManager()
    {
        return new structUserMailIdx(AppConfiguration.AdminName, AppConfiguration.AdminEmail, "");
    }

    public bool ApplicationErrorMessage(List<structUserMailIdx> eTo, string errorMessage)
    {
        bool bRet = false;
        try
        {
            string sSubject, sBody;

            sSubject = "Electronic Investment Proposal Error:";
            sBody = "Dear System Admnistrator, <br/> <br/>";
            sBody += errorMessage;
            sBody += "<br/>";

            emailClient oMail = new emailClient(m_eSender);
            bRet = oMail.sendShellMail(eTo, sSubject, sBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
        return bRet;
    }

    private bool Mailer(string fromEmailAddress, string[] toEmailAddress, string mSubject, string mBody, string[] mCC)
    {
        try
        {
            SmtpClient smtp = new SmtpClient(AppConfiguration.MailServer);
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(fromEmailAddress);

            //This will take care of sending mail to multiple people
            foreach (String sendTo in toEmailAddress)
            {
                msg.To.Add(sendTo);
            }

            //This will take care of copying mail to multiple people
            if (mCC.Length != 0)
            {
                foreach (String ccopy in mCC)
                {
                    msg.CC.Add(ccopy);
                }
            }

            msg.Subject = mSubject;
            msg.Body = mBody;
            msg.IsBodyHtml = true;
            smtp.Send(msg);
        }
        catch (Exception ex)
        {
            success = false;
            AppStatusMessages.MailServerStatus(ex);
        }
        return success;
    }

    private bool Mailer(string fromEmailAddress, string[] toEmailAddress, string mSubject, string mBody, string mCC)
    {
        try
        {
            SmtpClient smtp = new SmtpClient(AppConfiguration.MailServer);
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(fromEmailAddress);

            //This will take care of sending mail to multiple people
            foreach (String sendTo in toEmailAddress)
            {
                msg.To.Add(sendTo);
            }

            //This will take care of copying mail to multiple people
            if (mCC.Length != 0)
            {
                msg.CC.Add(mCC);
            }

            msg.Subject = mSubject;
            msg.Body = mBody;
            msg.IsBodyHtml = true;
            smtp.Send(msg);
        }
        catch (Exception ex)
        {
            success = false;
            AppStatusMessages.MailServerStatus(ex);
        }
        return success;
    }

    //public bool ApplicationErrorMessage(string[] toEmailAddress, string fromEmailAddress, string errorMessage)
    //{
    //    string mSubject, mBody;

    //    mSubject = "Electronic Investment Proposal";
    //    mBody = "Dear System Admnistrator, <br/> <br/>";
    //    mBody += errorMessage;
    //    mBody += "<br/>";

    //    return Mailer(fromEmailAddress, toEmailAddress, mSubject, mBody, "");
    //}

    public bool MailEIPUser(structUserMailIdx toEmailAddress, string UserName, string UserRole)
    {
        bool bRet = false;
        try
        {
            string sSubject = "e-IP Role Definition";
            string sBody = Resources.eIPResource.MailEIPUsers;
            sBody = sBody.Replace("@@USERROLE", UserRole);
            sBody = sBody.Replace("@@URL", ApplicationURL.MyAppURL() + "/Common/Confirmation.aspx");
            sBody = sBody.Replace("@@USERNAME", UserName.ToUpper());
            sBody = SystemAdmin(sBody);

            emailClient oMail = new emailClient(m_eSender);
            bRet = oMail.sendShellMail(toEmailAddress, sSubject, sBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
        return bRet;
    }

    public bool MailIPInitiatorUser(structUserMailIdx toEmailAddress, string UserName)
    {
        bool bRet = false;
        try
        {
            string sSubject = "e-IP System Definition";
            string sBody = Resources.eIPResource.WhenIPInitiatorIsCreated;
            sBody = sBody.Replace("@@URL", ApplicationURL.MyAppURL() + "/Common/Confirmation.aspx");
            sBody = sBody.Replace("@@USERNAME", UserName.ToUpper());
            sBody = SystemAdmin(sBody);

            emailClient oMail = new emailClient(m_eSender);
            bRet = oMail.sendShellMail(toEmailAddress, sSubject, sBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
        return bRet;
    }

    public bool MailFunctionalPlannerUser(structUserMailIdx toEmailAddress)
    {
        bool bRet = false;
        try
        {
            string sSubject, sBody;
            sBody = Resources.eIPResource.FunctionalPlannerCreated;
            sSubject = "e-IP System Definition";

            emailClient oMail = new emailClient(m_eSender);
            bRet = oMail.sendShellMail(toEmailAddress, sSubject, sBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
        return bRet;
    }

    public bool AcceptMailBPOSystemAdmin(structUserMailIdx toBPOEmail, string UserRole)
    {
        bool bRet = false;
        try
        {
            string mBody;

            string mSubject = "Electronic Investment Proposal user role acceptance";
            mBody = Resources.eIPResource.UserRoleAcceptance;
            mBody = mBody.Replace("@@USERROLE", UserRole);

            emailClient oMail = new emailClient(m_eSender);
            bRet = oMail.sendShellMail(toBPOEmail, mSubject, mBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
        return bRet;
    }

    public bool DeclineMailBPOSystemAdmin(structUserMailIdx toBPOEmail, string UserRole)
    {
        bool bRet = false;
        try
        {
            string mSubject, mBody;

            mSubject = "Electronic Investment Proposal user role acceptance";
            mBody = Resources.eIPResource.UserRoleDecline;
            mBody = mBody.Replace("@@USERROLE", UserRole);

            emailClient oMail = new emailClient(m_eSender);
            bRet = oMail.sendShellMail(toBPOEmail, mSubject, mBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
        return bRet;
    }

    //IP Initiator has finished loading an IP
    public bool IPInitiatorLoadedIP(List<structUserMailIdx> toEmail, List<structUserMailIdx> cCopyMail, string mMSubject, string IPInitiator, string ProjectNumber)
    {
        bool bRet = false;
        try
        {
            string sSubject = ProjectNumber + " - Investment Proposal Submitted";
            string sBody = Resources.eIPResource.IPInitiatorLoadsIP;

            sBody = sBody.Replace("@@SUBJECT", mMSubject);
            sBody = sBody.Replace("@@URL", ApplicationURL.MyAppURL());
            sBody = sBody.Replace("@@IPINITIATOR", IPInitiator);
            sBody = SystemAdmin(sBody);

            emailClient oMail = new emailClient(m_eSender);
            bRet = oMail.sendShellMail(toEmail, cCopyMail, sSubject, sBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }

        return bRet;
    }

    public bool IPInitiatorLoadedIP2(structUserMailIdx toEmail, string mMSubject, string IPInitiator, string ProjectNumber)
    {
        bool bRet = false;
        try
        {
            string sSubject = ProjectNumber + " - Investment Proposal Initiated";
            string sBody = Resources.eIPResource.IPInitiatorLoadsIP;

            sBody = sBody.Replace("@@SUBJECT ", mMSubject);
            sBody = sBody.Replace("@@URL", ApplicationURL.MyAppURL());
            sBody = sBody.Replace("@@IPINITIATOR", IPInitiator);
            sBody = SystemAdmin(sBody);

            emailClient oMail = new emailClient(m_eSender);
            bRet = oMail.sendShellMail(toEmail, sSubject, sBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }

        return bRet;
    }

    public bool MailBPO(string[] toEmailAddress, string fromEmailAddress, string mMSubject, string ProjectNumber)
    {
        string mSubject = ProjectNumber + " - Investment Proposal";
        string mBody = Resources.eIPResource.MailBPO;

        mBody = mBody.Replace("@@SUBJECT ", mMSubject);
        mBody = mBody.Replace("@@URL", ApplicationURL.MyAppURL());
        mBody = SystemAdmin(mBody);

        return Mailer(fromEmailAddress, toEmailAddress, mSubject, mBody, "");
    }

    public bool mailEPGIPTracker(string[] toEmailAddress, string fromEmail, string fullName, string mMSubject, string URL, int ProposalID, string ProjectNumber)
    {
        string mSubject = ProjectNumber + " - Investment Proposal";

        string mBody = Resources.eIPResource.MailEPGIPTrackerWhenIPIsCreated;
        mBody = mBody.Replace("@@FULLNAME", fullName);

        mBody = mBody.Replace("@@SUBJECT ", mMSubject);
        mBody = mBody.Replace("@@URL", ApplicationURL.MyAppURL());
        mBody = mBody.Replace("@@PROPOSALID", ProposalID.ToString());
        mBody = SystemAdmin(mBody);

        return Mailer(fromEmail, toEmailAddress, mSubject, mBody, fromEmail);
    }

    public bool FunctionalPlannerCreated(structUserMailIdx toEmailAddress, string UserName)
    {
        bool bRet = false;
        try
        {
            string sSubject = "e-IP Role Definition";
            string sBody = Resources.eIPResource.FunctionalPlannerCreated;
            sBody = sBody.Replace("@@URL", ApplicationURL.MyAppURL() + "/Common/Confirmation.aspx");
            sBody = sBody.Replace("@@USERNAME", UserName.ToUpper());
            sBody = SystemAdmin(sBody);

            emailClient oMail = new emailClient(m_eSender);
            bRet = oMail.sendShellMail(toEmailAddress, sSubject, sBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }

        return bRet;

        //return Mailer(fromEmailAddress, toEmailAddress, mSubject, mBody, fromEmailAddress);
    }

    public bool MailFunctionalPlanner(string[] toEmailAddress, string fromEmailAddress, string mMSubject, string URL, string IPInitiator, string LineTeamLead, string ProjectNumber)
    {
        string mSubject = ProjectNumber + " - e-IP Processing FYI/A";

        string mBody = Resources.eIPResource.FunctionalPlannerMail;

        mBody = mBody.Replace("@@SUBJECT", mMSubject);
        mBody = mBody.Replace("@@IPINITIATOR", IPInitiator);
        mBody = mBody.Replace("@@LINETEAMLEAD", LineTeamLead);
        mBody = SystemAdmin(mBody);

        return Mailer(fromEmailAddress, toEmailAddress, mSubject, mBody, fromEmailAddress);
    }

    public bool IPUpdateReloadNotification(List<structUserMailIdx> toEmailAddress, string mMSubject, string URL, string ProjectNumber)
    {
        bool bRet = false;
        try
        {
            string mSubject = ProjectNumber + " - Previous Investment Proposal Updated - Awaiting Review";
            string mBody = Resources.eIPResource.IPUpdateReloadNotification;
            mBody = mBody.Replace("@@SUBJECT", mMSubject);
            mBody = mBody.Replace("@@LLURL", ApplicationURL.LiveLinkURLs());
            mBody = mBody.Replace("@@URL", ApplicationURL.MyAppURL());
            mBody = SystemAdmin(mBody);

            emailClient oMail = new emailClient(m_eSender);
            bRet = oMail.sendShellMail(toEmailAddress, mSubject, mBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }

        return bRet;
    }

    public bool PendingProposalReminder(List<structUserMailIdx> toEmailAddress, string mMSubject, string ProjectNumber)
    {
        bool bRet = false;
        try
        {
            string mSubject = ProjectNumber + " - Pending Proposal Reminder";
            string mBody = Resources.eIPResource.PendingProposalReminder;

            mBody = mBody.Replace("@@SUBJECT", mMSubject);
            mBody = mBody.Replace("@@URL", ApplicationURL.MyAppURL());
            mBody = SystemAdmin(mBody);

            emailClient oMail = new emailClient(m_eSender);
            bRet = oMail.sendShellMail(toEmailAddress, mSubject, mBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }

        return bRet;
    }

    public bool IPInitiatorUpdatedIP(List<structUserMailIdx> toEmailAddress, string fromEmailAddress, string mMSubject, string URL, string ProjectNumber)
    {
        bool bRet = false;
        try
        {
            string mSubject = ProjectNumber + " - Investment Proposal Updated";
            string mBody = Resources.eIPResource.IPUpdateReloadNotification;
            mBody = mBody.Replace("@@SUBJECT", mMSubject);
            mBody = mBody.Replace("@@URL", ApplicationURL.MyAppURL());
            mBody = SystemAdmin(mBody);

            emailClient oMail = new emailClient(m_eSender);
            bRet = oMail.sendShellMail(toEmailAddress, mSubject, mBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }

        return bRet;
    }

    public bool LineTeamLeadSupportsIP(structUserMailIdx toBPO, string mMSubject, string ProjectNumber, structUserMailIdx copyInitiator)
    {
        bool bRet = false;
        try
        {
            string mSubject = ProjectNumber + " - Investment Proposal for processing";
            string mBody = Resources.eIPResource.LineTeamLeadSupportsIP;

            mBody = mBody.Replace("@@SUBJECT", mMSubject);
            mBody = mBody.Replace("@@URL", ApplicationURL.MyAppURL());
            mBody = SystemAdmin(mBody);

            emailClient oMail = new emailClient(m_eSender);
            bRet = oMail.sendShellMail(toBPO, copyInitiator, mSubject, mBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }

        return bRet;
    }

    public bool IPForwardForSupportApproval(structUserMailIdx toEmail, structUserMailIdx copyEmail, string IPInitiator, Proposal oProposal)
    {
        bool bRet = false;
        try
        {
            string mBody = Resources.eIPResource.BPOSendsIPForFunctionalSupport;
            string mSubject = oProposal.m_sProj_Num + " - Investment Proposal for your Support/approval";

            mBody = mBody.Replace("@@SUBJECT", oProposal.m_sProj_Title);
            mBody = mBody.Replace("@@URL", ApplicationURL.MyAppURL());
            mBody = mBody.Replace("@@IPINITIATOR", IPInitiator);
            mBody = mBody.Replace("@@3DAYS", (DateTime.Today.Date.AddDays(3)).ToLongDateString());


            appUserMgt oappUserMgt = new appUserMgt();
            appUsers IpInit = oappUserMgt.objGetUserByUserId(oProposal.m_iUserId);

            //mBody += Resources.eIPResource.ForwardIPtoMyEmail;
            //mBody = mBody.Replace("@@ProjectTitle", oProposal.m_sProj_Title);
            //mBody = mBody.Replace("@@ProjectNumber", oProposal.m_sProj_Num);
            //mBody = mBody.Replace("@@Initiator", IpInit.m_sFullName);
            //mBody = mBody.Replace("@@DateInit", oProposal.m_sDate_Submit);
            //mBody = mBody.Replace("@@DateForwarded", oProposal.m_sDate_Submit);

            //string MyPath = AppConfiguration.InvestmentProposalsFileLocation + oProposal.m_sProposalFileName;
            //List<FileStream> fsReads = new List<FileStream>();

            //FileStream fsRead = new FileStream(MyPath, FileMode.Open, FileAccess.Read);
            //fsReads.Add(fsRead);

            emailClient oMail = new emailClient(m_eSender);
            mBody = SystemAdmin(mBody);

            //bRet = oMail.sendShellMail(m_eSender, mSubject, mBody);
            bRet = oMail.sendShellMail(toEmail, copyEmail, mSubject, mBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }

        return bRet;
    }

    //Any Functional Support supports the IP
    public bool IPSupported(structUserMailIdx toEmail, structUserMailIdx copyEmail, string supportFunction, string mMSubject, string ProjectNumber, int xNo)
    {
        bool bRet = false;
        try
        {
            //Send Mail to the IP owner when there is a review on the eIP
            string mSubject = ProjectNumber + " - Investment Proposal Supported";
            string mBody = Resources.eIPResource.ProposalSupported;

            mBody = mBody.Replace("@@SUBJECT", mMSubject);
            mBody = mBody.Replace("@@URL", ApplicationURL.MyAppURL());
            mBody = mBody.Replace("@XXX", xNo.ToString());
            mBody = mBody.Replace("@@FUNCTIONALSUPPORT", supportFunction);
            mBody = SystemAdmin(mBody);

            emailClient oMail = new emailClient(m_eSender);
            bRet = oMail.sendShellMail(toEmail, copyEmail, mSubject, mBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }

        return bRet;
    }

    //When an IP is not Supported by Any of the Functional Support and Approval. (BPO should be copied)
    public bool IPNotSupported(List<structUserMailIdx> toEmailAddress, structUserMailIdx ccEmailAddress, string mMSubject, string reasonNotSupported, string SupportFunctionFullName, string MyFunction, string ProjectNumber)
    {
        bool bRet = false;
        try
        {
            string mSubject = ProjectNumber + " - Investment Proposal not Supported";
            string mBody = Resources.eIPResource.IPNotSupportedByFunctionalSupportOrApprover;

            mBody = mBody.Replace("@@SUPPORTFUNCTIONFULLNAME", SupportFunctionFullName);
            mBody = mBody.Replace("@@MYFUNCTION", MyFunction);
            mBody = mBody.Replace("@@SUBJECT", mMSubject);
            mBody = mBody.Replace("@@REASONNOTSUPPORTED", reasonNotSupported);
            mBody = mBody.Replace("@@URL", ApplicationURL.MyAppURL());
            mBody = SystemAdmin(mBody);

            emailClient oMail = new emailClient(m_eSender);
            bRet = oMail.sendShellMail(toEmailAddress, ccEmailAddress, mSubject, mBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }

        return bRet;
    }

    public bool IPNotSupported(structUserMailIdx toEmailAddress, structUserMailIdx ccEmailAddress, string mMSubject, string reasonNotSupported, string SupportFunctionFullName, string MyFunction, string ProjectNumber)
    {
        bool bRet = false;
        try
        {
            string mSubject = ProjectNumber + " - Investment Proposal not Supported";
            string mBody = Resources.eIPResource.IPNotSupportedByFunctionalSupportOrApprover;

            mBody = mBody.Replace("@@SUPPORTFUNCTIONFULLNAME", SupportFunctionFullName);
            mBody = mBody.Replace("@@MYFUNCTION", MyFunction);
            mBody = mBody.Replace("@@SUBJECT", mMSubject);
            mBody = mBody.Replace("@@REASONNOTSUPPORTED", reasonNotSupported);
            mBody = mBody.Replace("@@URL", ApplicationURL.MyAppURL());
            mBody = SystemAdmin(mBody);

            emailClient oMail = new emailClient(m_eSender);
            bRet = oMail.sendShellMail(toEmailAddress, ccEmailAddress, mSubject, mBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }

        return bRet;
    }


    public bool IPNotSupportedByLineTeamLead(structUserMailIdx toIPInitiator, string mMSubject, string reasonNotSupported, string LineTeamLeadFullName, string MyFunction, structUserMailIdx copyBPO, string ProjectNumber)
    {
        bool bRet = false;
        try
        {
            string mSubject = ProjectNumber + " - Investment Proposal Not Supported";
            string mBody = Resources.eIPResource.IPNotSupportedByLineTeamLead;
            mBody = mBody.Replace("@@SUBJECT", mMSubject);
            mBody = mBody.Replace("@@URL", ApplicationURL.MyAppURL());
            mBody = mBody.Replace("@@LINETEAMLEADFULLNAME", LineTeamLeadFullName);
            mBody = mBody.Replace("@@REASONNOTSUPPORTED", reasonNotSupported);
            mBody = SystemAdmin(mBody);

            emailClient oMail = new emailClient(m_eSender);
            bRet = oMail.sendShellMail(toIPInitiator, copyBPO, mSubject, mBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }

        return bRet;
    }

    public bool mailEPGIPTrackerOnIPUpdate(structUserMailIdx toEmailAddress, string ProjectNumber, string mMSubject, long lProposalId)
    {
        bool bRet = false;
        try
        {
            string mSubject = ProjectNumber + " - Investment Proposal Updated";
            string mBody = Resources.eIPResource.MailEPGIPTrackerWhenIPIsUpdated;

            mBody = mBody.Replace("@@URL", ApplicationURL.MyAppURL());
            //mBody = mBody.Replace("@@PROPOSALID", ProposalID);
            mBody = mBody.Replace("@@SUBJECT", mMSubject);
            mBody = mBody.Replace("@@IPNUMBER", ProjectNumber);
            mBody = SystemAdmin(mBody);

            emailClient oMail = new emailClient(m_eSender);
            bRet = oMail.sendShellMail(toEmailAddress, mSubject, mBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }

        return bRet;
    }

    //BPO Forwards IP to Finance Signature/or GM Finance
    public bool mailFinanceSignature(structUserMailIdx toEmailAddress, structUserMailIdx ccEmailAddress, string mMSubject, string IPInitiator, string ProjectNumber)
    {
        bool bRet = false;
        try
        {
            string mSubject = ProjectNumber + " - Investment Proposal for Review";
            string mBody = Resources.eIPResource.BPOSendsIPtoFinanceSignature;

            mBody = mBody.Replace("@@SUBJECT", mMSubject);
            mBody = mBody.Replace("@@URL", ApplicationURL.MyAppURL());
            mBody = mBody.Replace("@@IPINITIATOR", IPInitiator);
            mBody = SystemAdmin(mBody);

            emailClient oMail = new emailClient(m_eSender);
            bRet = oMail.sendShellMail(toEmailAddress, ccEmailAddress, mSubject, mBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }

        return bRet;
    }

    //BPO forwards IP to approver (Vice President)
    public bool MailApproval(structUserMailIdx toEmailAddress, structUserMailIdx copyEmailAddress, string mMSubject, string ProjectNumber)
    {
        bool bRet = false;
        try
        {
            string mSubject = ProjectNumber + " - Investment Proposal for Approval";
            string mBody = Resources.eIPResource.BPOSendsMailToVicePresidentForApproval;

            mBody = mBody.Replace("@@SUBJECT", mMSubject);
            mBody = mBody.Replace("@@URL", ApplicationURL.MyAppURL());
            mBody = SystemAdmin(mBody);

            emailClient oMail = new emailClient(m_eSender);
            bRet = oMail.sendShellMail(toEmailAddress, copyEmailAddress, mSubject, mBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }

        return bRet;
        //return Mailer(fromEmailAddress, toEmailAddress, mSubject, mBody, fromEmailAddress);
    }

    //When Proposal is Approved
    public bool ProposalApproved(structUserMailIdx toIPInitiator, List<structUserMailIdx> ccopy, string mMSubject, string ProjectNumber)
    {
        bool bRet = false;
        try
        {
        string mSubject = ProjectNumber + " Investment Proposal Has Received All Approvals";
        string mBody = Resources.eIPResource.WhenProposalIsApproved;
        mBody = mBody.Replace("@@SUBJECT", mMSubject);
        mBody = mBody.Replace("@@URL", ApplicationURL.MyAppURL());
        mBody = SystemAdmin(mBody);

        emailClient oMail = new emailClient(m_eSender);
        bRet = oMail.sendShellMail(toIPInitiator, ccopy, mSubject, mBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }

        return bRet;

        //return Mailer(fromEmailAddress, toEmailAddress, mSubject, mBody, BPOEmailAddress);
    }

    public bool MailRegionalVicePresident(structUserMailIdx toEmailAddress, structUserMailIdx copyInitiator, string mMSubject, string ProjectNumber)
    {
        bool bRet = false;
        try
        {
            string mSubject = ProjectNumber + " - Investment Proposal awaiting your approval";
            string mBody = Resources.eIPResource.MailRegionalVicePresident;
            mBody = mBody.Replace("@@SUBJECT", mMSubject);
            mBody = mBody.Replace("@@URL", ApplicationURL.MyAppURL());
            mBody = SystemAdmin(mBody);

            emailClient oMail = new emailClient(m_eSender);
            bRet = oMail.sendShellMail(toEmailAddress, copyInitiator, mSubject, mBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }

        return bRet;
    }

    //When IP >=$10mln, OU Finance Director Sends this Mail to Corporate Planning Manager(CPM) and copy Initiator
    public bool sendMailToCPM(structUserMailIdx toEmailAddress, structUserMailIdx copyInitiator, string supportFunction, string mMSubject, string ProjectNumber, long lProposalId)
    {
        //Send Mail to the IP owner when there is a review on the eIP
        bool bRet = false;
        try
        {
            string mSubject = ProjectNumber + " - Investment Proposal Awaiting your Support";

            string mBody = Resources.eIPResource.CorporatePlanningManagerGetsIP;
            mBody = mBody.Replace("@@SUBJECT", mMSubject);
            mBody = mBody.Replace("@@URL", ApplicationURL.MyAppURL());
            mBody = mBody.Replace("@@SUPPORTFUNCTION", supportFunction);
            mBody = mBody.Replace("@@PROPOSALID", lProposalId.ToString());
            mBody = SystemAdmin(mBody);

            emailClient oMail = new emailClient(m_eSender);
            bRet = oMail.sendShellMail(toEmailAddress, copyInitiator, mSubject, mBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }

        return bRet;
    }

    //Corporate Planning Manager Mails MD
    public bool MailManagingDirector(string[] toEmailAddress, string fromEmailAddress, string mMSubject, string IPInitiatorMail, string URL, string ProjectNumber)
    {
        string mSubject = ProjectNumber + " - Investment Proposal Awaiting your Support";
        string mBody = Resources.eIPResource.CorporatePlanningManagerMailsMD;

        mBody = mBody.Replace("@@SUBJECT", mMSubject);
        mBody = mBody.Replace("@@URL", ApplicationURL.MyAppURL());
        mBody = SystemAdmin(mBody);

        return Mailer(fromEmailAddress, toEmailAddress, mSubject, mBody, IPInitiatorMail);
    }

    public bool MailGMREPlanning(structUserMailIdx toEmail, structUserMailIdx copyIPInitiatorMail, string mMSubject, string ProjectNumber)
    {
        bool bRet = false;
        try
        {
            string mSubject = ProjectNumber + " - Investment Proposal Awaiting your Support";
            string mBody = Resources.eIPResource.MailGMREPlanning;

            mBody = mBody.Replace("@@SUBJECT", mMSubject);
            mBody = mBody.Replace("@@URL", ApplicationURL.MyAppURL());
            mBody = SystemAdmin(mBody);

            emailClient oMail = new emailClient(m_eSender);
            bRet = oMail.sendShellMail(toEmail, copyIPInitiatorMail, mSubject, mBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }

        return bRet;
    }

    public bool MailCERP(structUserMailIdx toEmailAddress, Proposal oProposal)
    {
        //Vice Presidents are  now the General Managers
        bool bRet = false;
        try
        {
            string mSubject = oProposal.m_sProj_Num + " - Investment Proposal Awaiting your Support";
            string mBody = Resources.eIPResource.GMREPlanningForwardsIPToVPs;

            mBody = mBody.Replace("@@SUBJECT", oProposal.m_sProj_Title);
            mBody = mBody.Replace("@@URL", ApplicationURL.MyAppURL());
            mBody = SystemAdmin(mBody);

            emailClient oMail = new emailClient(m_eSender);
            bRet = oMail.sendShellMail(toEmailAddress, mSubject, mBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }

        return bRet;
    }

    public bool MailVicePresident(List<structUserMailIdx> toEmailAddress, string mMSubject, string ProjectNumber)
    {
        //Vice Presidents are  now the General Managers
        bool bRet = false;
        try
        {
            string mSubject = ProjectNumber + " - Investment Proposal Awaiting your Support";
            string mBody = Resources.eIPResource.GMREPlanningForwardsIPToVPs;

            mBody = mBody.Replace("@@SUBJECT", mMSubject);
            mBody = mBody.Replace("@@URL", ApplicationURL.MyAppURL());
            mBody = SystemAdmin(mBody);

            emailClient oMail = new emailClient(m_eSender);
            bRet = oMail.sendShellMail(toEmailAddress, mSubject, mBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }

        return bRet;
    }

    public bool MailVicePresident2(structUserMailIdx toEmailAddress, string mMSubject, string ProjectNumber)
    {
        //Vice Presidents are  now the General Managers
        bool bRet = false;
        try
        {
            string mSubject = ProjectNumber + " - Investment Proposal Awaiting your Support";
            string mBody = Resources.eIPResource.GMREPlanningForwardsIPToVPs;

            mBody = mBody.Replace("@@SUBJECT", mMSubject);
            mBody = mBody.Replace("@@URL", ApplicationURL.MyAppURL());
            mBody = SystemAdmin(mBody);

            emailClient oMail = new emailClient(m_eSender);
            bRet = oMail.sendShellMail(toEmailAddress, mSubject, mBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }

        return bRet;
    }

    public bool MailGeneralManager(string[] toEmailAddress, string fromEmailAddress, string mMSubject, string URL, string ProjectNumber)
    {
        string mSubject = ProjectNumber + " - Investment Proposal Awaiting your Support";
        string mBody = Resources.eIPResource.GMREPlanningForwardsIPToVPs; //Note: in this case, it is BPO that forwards this mail to General Manager. The mails are the same, thus the reuse

        mBody = mBody.Replace("@@SUBJECT", mMSubject);
        mBody = mBody.Replace("@@URL", ApplicationURL.MyAppURL());
        mBody = SystemAdmin(mBody);

        return Mailer(fromEmailAddress, toEmailAddress, mSubject, mBody, fromEmailAddress);
    }

    public bool CentralSupportStaff(string[] toEmailAddress, string fromEmail, Proposal oProposal, int iStand, string sComment)
    {
        string mSubject = oProposal.m_sProj_Num + " - Group Investment Proposal for your review";
        string mBody = Resources.eIPResource.ForwardIPtoMyEmail2;

        appUserMgt oappUserMgt = new appUserMgt();
        appUsers ipInit = oappUserMgt.objGetUserByUserId(oProposal.m_iUserId);

        //string mBody = Resources.eIPResource.ForwardIPtoMyEmail;
        mBody = mBody.Replace("@@ProjectTitle", oProposal.m_sProj_Title);
        mBody = mBody.Replace("@@ProjectNumber", oProposal.m_sProj_Num);
        mBody = mBody.Replace("@@Initiator", ipInit.m_sFullName);

        mBody = mBody.Replace("@@Stand", iStand == SupportState.iSupported ? SupportState.Supported : SupportState.NotSupported);
        mBody = mBody.Replace("@@Comments", sComment);

        mBody = mBody.Replace("@@DateInit", oProposal.m_sDate_Submit);
        mBody = mBody.Replace("@@DateForwarded", oProposal.m_sDate_Submit);

        //Call the file writting class to do the job
        //Proposal proposal = new Proposal(ProposalID);
        //string MyPath = wrtFile.WriteData2(ProposalID);
        string myPath = AppConfiguration.InvestmentProposalsFileLocation + oProposal.m_sProposalFileName;

        FileStream fsRead = new FileStream(myPath, FileMode.Open, FileAccess.Read);

        SmtpClient smtp = new SmtpClient(AppConfiguration.MailServer);
        MailMessage msg = new MailMessage();
        msg.Attachments.Add(new Attachment(fsRead, "DownloadedIP.pdf"));

        msg.From = new MailAddress(fromEmail);
        foreach (var toEmail in toEmailAddress)
        {
            msg.To.Add(toEmail);
        }
        
        msg.Subject = mSubject;
        msg.Body = mBody;
        msg.IsBodyHtml = true;

        try
        {
            smtp.Send(msg);
        }
        catch (Exception ex)
        {
            success = false;
            AppStatusMessages.MailServerStatus(ex);
        }
        finally
        {
            fsRead.Close();
            fsRead = null;
        }
        return success;





        //mBody = mBody.Replace("@@SUBJECT", oProposal.m_sProj_Title);
        //mBody = mBody.Replace("@@URL", ApplicationURL.MyAppURL());
        //mBody = SystemAdmin(mBody);

        //return Mailer(fromEmailAddress, toEmailAddress, mSubject, mBody, fromEmailAddress);
    }

    public bool SupportFunctionSLA(structUserMailIdx toEmailAddress, string mMSubject, string ProjectNumber)
    {
        bool bRet = false;
        try
        {
            string mSubject = ProjectNumber + " - EIP - Service Level Agreement";
            string mBody = Resources.eIPResource.FunctionalSupportSLAMail;
            mBody = mBody.Replace("@@SUBJECT", mMSubject);
            mBody = mBody.Replace("@@URL", ApplicationURL.MyAppURL());
            mBody = SystemAdmin(mBody);

            emailClient oMail = new emailClient(m_eSender);
            bRet = oMail.sendShellMail(toEmailAddress, mSubject, mBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }

        return bRet;
    }

    public bool DeligateIPInitiatorFunction(structUserMailIdx toEmailAddress, StringBuilder sbSubject, string IPInitiatorName)
    {
        bool bRet = false;
        try
        {
            string mBody = Resources.eIPResource.DelegatedIPInitiatorFunction;
            string mSubject = "Delegate IP Initiator Role";

            mBody = mBody.Replace("@@PROPOSALS", sbSubject.ToString());
            mBody = mBody.Replace("@@URL", ApplicationURL.MyAppURL());
            mBody = mBody.Replace("@@IPINITIATOR", IPInitiatorName);
            mBody = SystemAdmin(mBody);

            emailClient oMail = new emailClient(m_eSender);
            bRet = oMail.sendShellMail(toEmailAddress, mSubject, mBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }

        return bRet;
    }

    public bool ProposalDiscontinued(List<structUserMailIdx> toEmailAddress, string mMSubject, string IPInitiatorName, string ProjectNumber)
    {
        bool bRet = false;
        try
        {
            string mBody = Resources.eIPResource.ProposalDiscontinued;
            string mSubject = ProjectNumber + " - Proposal Discontinued";

            mBody = mBody.Replace("@@SUBJECT", mMSubject);
            mBody = mBody.Replace("@@URL", ApplicationURL.MyAppURL());
            mBody = mBody.Replace("@@IPINITIATOR", IPInitiatorName);
            mBody = SystemAdmin(mBody);

            emailClient oMail = new emailClient(m_eSender);
            bRet = oMail.sendShellMail(toEmailAddress, mSubject, mBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }

        return bRet;
    }

    public bool ProposalReactivated(List<structUserMailIdx> toEmailAddress, string mMSubject, string IPInitiatorName, string ProjectNumber)
    {
        bool bRet = false;
        try
        {
            string mBody = Resources.eIPResource.ProposalReactivated;
            string mSubject = ProjectNumber + " - Discontinued Proposal reactivated";

            mBody = mBody.Replace("@@SUBJECT", mMSubject);
            mBody = mBody.Replace("@@URL", ApplicationURL.MyAppURL());
            mBody = mBody.Replace("@@IPINITIATOR", IPInitiatorName);
            mBody = SystemAdmin(mBody);

            emailClient oMail = new emailClient(m_eSender);
            bRet = oMail.sendShellMail(toEmailAddress, mSubject, mBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }

        return bRet;
    }

    public bool ForwardIP(Proposal oProposal, structUserMailIdx fromEmail)
    {
        bool bRet = false;
        try
        {
            string mSubject = "Electronic Investment Proposal";

            appUserMgt oappUserMgt = new appUserMgt();
            appUsers IpInit = oappUserMgt.objGetUserByUserId(oProposal.m_iUserId);

            string mBody = Resources.eIPResource.ForwardIPtoMyEmail;
            mBody = mBody.Replace("@@ProjectTitle", oProposal.m_sProj_Title);
            mBody = mBody.Replace("@@ProjectNumber", oProposal.m_sProj_Num);
            mBody = mBody.Replace("@@Initiator", IpInit.m_sFullName);
            mBody = mBody.Replace("@@DateInit", oProposal.m_sDate_Submit);
            mBody = mBody.Replace("@@DateForwarded", oProposal.m_sDate_Submit);

            string MyPath = AppConfiguration.InvestmentProposalsFileLocation + oProposal.m_sProposalFileName;
            List<FileStream> fsReads = new List<FileStream>();

            FileStream fsRead = new FileStream(MyPath, FileMode.Open, FileAccess.Read);
            fsReads.Add(fsRead);

            var oMail = new emailClient(m_eSender);
            bRet = oMail.sendShellMail(fromEmail, mSubject, mBody, fsReads, oProposal);

        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return bRet;
    }

    //    FileStream fsRead = new FileStream(MyPath, FileMode.Open, FileAccess.Read);

    //    SmtpClient smtp = new SmtpClient(AppConfiguration.MailServer);
    //    MailMessage msg = new MailMessage();
    //    msg.Attachments.Add(new Attachment(fsRead, "DownloadedIP.pdf"));

    //    msg.From = new MailAddress(fromEmail);
    //    msg.To.Add(toEmail);
    //    msg.Subject = mSubject;
    //    msg.Body = mBody;
    //    msg.IsBodyHtml = true;

    //    try
    //    {
    //        smtp.Send(msg);
    //    }
    //    catch (Exception ex)
    //    {
    //        success = false;
    //        appMonitor.logAppExceptions(ex);
    //        System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
    //        //AppStatusMessages.MailServerStatus(ex);
    //    }
    //    finally
    //    {
    //        fsRead.Close();
    //        fsRead = null;
    //    }
    //    return success;
    //}

    public bool NonMandatorySupportWarning(List<structUserMailIdx> toEmailAddress, structUserMailIdx ccBPO, string mMSubject, string ProjectNumber)
    {
        bool bRet = false;
        try
        {
            string mBody = Resources.eIPResource.NonMandatorySupportWarning;
            string mSubject = ProjectNumber + " - IP Functional Support Final Reminder";

            mBody = mBody.Replace("@@SUBJECT", mMSubject);
            mBody = mBody.Replace("@@URL", ApplicationURL.MyAppURL());
            mBody = SystemAdmin(mBody);

            emailClient oMail = new emailClient(m_eSender);
            bRet = oMail.sendShellMail(toEmailAddress, ccBPO, mSubject, mBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }

        return bRet;
    }

    public bool AllMandatorySupportCompleted(List<structUserMailIdx> toBPOCERP, structUserMailIdx ccInitiator, string mMSubject, string ProjectNumber)
    {
        bool bRet = false;
        try
        {
            string mBody = Resources.eIPResource.AllMandatorySupportReceived;
            string mSubject = ProjectNumber + " - All Mandatory Functional Support Completed";

            mBody = mBody.Replace("@@SUBJECT", mMSubject);
            mBody = mBody.Replace("@@URL", ApplicationURL.MyAppURL());
            mBody = SystemAdmin(mBody);

            emailClient oMail = new emailClient(m_eSender);
            bRet = oMail.sendShellMail(toBPOCERP, ccInitiator, mSubject, mBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }

        return bRet;
    }

    private string SystemAdmin(string mBody)
    {
        mBody = mBody.Replace("@SSSADMIN", AppConfiguration.AdminName);
        mBody = mBody.Replace("@@EXT", AppConfiguration.AdminExt);
        mBody = mBody.Replace("@@EMAIL", AppConfiguration.AdminEmail);

        //mBody = mBody.Replace("@@SSADMN", Administrator.sFULLNAME2);
        //mBody = mBody.Replace("@@EXT2", Administrator.sSYSADMINEXT2);
        //mBody = mBody.Replace("@@EMAIL2", Administrator.sUSERMAIL2);

        return mBody;
    }

    public bool cleanUpReport(structUserMailIdx eSender, StringBuilder sb)
    {
        bool bRet = false;        
        try
        {
            string mBody = Resources.eIPResource.UserTableCleaUp;
            string mSubject = "EIP User database cleanup Report.";

            mBody = mBody.Replace("@@URL", ApplicationURL.MyAppURL());
            mBody = mBody.Replace("@@=BODY", mBody);
            mBody = SystemAdmin(mBody);

            emailClient oMail = new emailClient(m_eSender);
            bRet = oMail.sendShellMail(eSender, mSubject, mBody);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
        return bRet;
    }
}