using System;
using System.Web;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Microsoft.Security.Application;

public partial class EditIP : aspnetPage
{
    SupportApprovalStatus supportStatus = new SupportApprovalStatus();
    ViewProposals ViewProp = new ViewProposals();    
    ProposalMgt oProposalMgt = new ProposalMgt();

    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
        //Response.Expires = -1500;
        //Response.CacheControl = "no-cache";

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();

        if (!IsPostBack)
        {
            long lProposalId = long.Parse(Request.QueryString["ProposalID"].ToString());
            Proposal oProposal = oProposalMgt.objGetProposalById(lProposalId);

            //Load EP Priority
            EPPriority oPriority = new EPPriority();
            List<EPPriority> Priorities = oPriority.lstGetEPPriority();
            foreach (EPPriority Priority in Priorities)
            {
                EPPriorityDropDownList.Items.Add(new ListItem(Priority.m_sEPPriority, Priority.m_iEPPriority.ToString()));
            }

            //Load Line Team Leads
            //appUserMgt oAppUserMgt = new appUserMgt();
            //List<appUsers> MyLTLs = oAppUserMgt.lstGetUsersByRole((int)appUsersRoles.userRole.Line_Team_Lead);
            //foreach (appUsers MyLTL in MyLTLs)
            //{
            //    LineTeamLeadDropDownList.Items.Add(new ListItem(MyLTL.m_sFullName, MyLTL.m_iUserId.ToString()));
            //}

            validateNumeric();
            ShowData(lProposalId);
            LoadProposal(oProposal.m_sProposalFileName);

           // Response.Redirect(Request.RawUrl);

            //Response.Redirect("~/EditIP.aspx?ProposalID=" + lProposalId);
        }
    }

    private void validateNumeric()
    {
        IPValueTextBox.Attributes.Add("onkeypress", "return isNumberKey(event)");
        AmountJVTextBox.Attributes.Add("onkeypress", "return isNumberKey(event)");
    }

    protected void UploadFileBtn_Click(object sender, EventArgs e)
    {
        try
        {
            string oMessage = "";
            SaveIP2FileSystem UpLoadMe = new SaveIP2FileSystem();

            long lProposalId = long.Parse(Request.QueryString["ProposalID"].ToString());
            Proposal oProposal = oProposalMgt.objGetProposalById(lProposalId);

            oProposal.m_iFunctionId = int.Parse(Encoder.HtmlEncode(oSessnx.getOnlineUser.m_iFunction.ToString()));
            oProposal.m_sProj_Title = projTitleTextBox.Text;
            oProposal.m_lJV = decimal.Parse(AmountJVTextBox.Text);
            oProposal.m_lSS = decimal.Parse(IPValueTextBox.Text);
            oProposal.m_sProj_Num = projNumTextBox.Text;
            oProposal.m_sProj_Desc = projDescTextBox.Text;
            oProposal.m_iEppriorityId = int.Parse(EPPriorityDropDownList.SelectedValue);

            bool bRet = false;
            if (UploadProposal.HasFile) bRet = oProposalMgt.EditProposalWtFile(oProposal, UploadProposal, oSessnx.getOnlineUser.m_iUserId, ref oMessage);
            else bRet = oProposalMgt.EditProposalWithoutPDFFile(oProposal, oSessnx.getOnlineUser.m_iUserId, UploadProposal);

            if (bRet)
            {
                bool IPNotSupportedApproved = oProposalMgt.IPNotSupportedApproved(lProposalId);
                if (IPNotSupportedApproved == true)
                {
                    oProposalMgt.IPInitiatorReloadsUpdate(oProposal, oSessnx.getOnlineUser, projTitleTextBox.Text, ApplicationURL.MyAppURL());
                    string mssg = "Proposal successfully updated. Your Line Team Lead, the Business Process Owner and all Support Functions have been notified.";
                    ajaxWebExtension.showJscriptAlert(Page, this, mssg);
                }
                else
                {
                    //Send a mail notifying the Users in the IPLine that IP Initiator has updated IP.
                    sendMail oSendMail = new sendMail(oSessnx.getOnlineUser.structUserIdx);
                    oSendMail.IPInitiatorUpdatedIP(oProposalMgt.getEmailAddressForIPLine(oProposal.m_lProposalId), oSessnx.getOnlineUser.m_sEmail, oProposal.m_sProj_Title, ApplicationURL.MyAppURL(), oProposal.m_sProj_Num);
                    ajaxWebExtension.showJscriptAlert(Page, this, "Proposal successfully updated.");
                }
                string message = "Proposal successfully updated.";
                ajaxWebExtension.showJscriptAlert(Page, this, message);
                LoadProposal(oProposal.m_sProposalFileName);
            }
            else
            {
                ajaxWebExtension.showJscriptAlert(Page, this, oMessage);
            }
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    private void LoadProposal(string sProposalFileName)
    {
        ViewProp.ViewProposal(sProposalFileName);
    }

    protected void closeButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Common/MyProposal.aspx");
    }

    private void ShowData(long lProposalId)
    {
        Proposal oProposal = oProposalMgt.objGetProposalById(lProposalId);

        projNumTextBox.Text = Encoder.HtmlEncode(oProposal.m_sProj_Num);
        projTitleTextBox.Text = Encoder.HtmlEncode(oProposal.m_sProj_Title);
        dateInitTextBox.Text = Encoder.HtmlEncode(oProposal.m_sDate_Init);
        IPValueTextBox.Text = Encoder.HtmlEncode(oProposal.m_lSS.ToString());
        AmountJVTextBox.Text = Encoder.HtmlEncode(oProposal.m_lJV.ToString());
        //
        //BOMTextBox.Text = oProposal.BOM;
        projDescTextBox.Text = Encoder.HtmlEncode(oProposal.m_sProj_Desc);
        EPPriorityDropDownList.SelectedValue = Encoder.HtmlEncode(oProposal.m_iEppriorityId.ToString());

        ////Get the Line Team Lead
        //SupportApproverCommentMgt oSupportApproverCommentMgt = new SupportApproverCommentMgt();
        //List<SupportApproverComments> oSupportApproverComments = oSupportApproverCommentMgt.lstGetFunctionalSupportsApproverComments(lProposalId);
        //foreach (SupportApproverComments oSupportApproverComment in oSupportApproverComments)
        //{
        //    if (oSupportApproverComment.m_iUserRoleId == (int)appUsersRoles.userRole.Line_Team_Lead)
        //    {
        //        LineTeamLeadDropDownList.SelectedValue = oSupportApproverComment.m_iUserId.ToString();
        //    }
        //} 
    }

    //protected void UploadFileBtn_Click(object sender, EventArgs e)
    //{
    //    long lProposalId = long.Parse(Request.QueryString["ProposalID"].ToString());
    //    Proposal oProposal = oProposalMgt.objGetProposalById(lProposalId);

    //    oProposal.m_iFunctionId = int.Parse(Encoder.HtmlEncode(oSessnx.getOnlineUser.m_iFunction.ToString()));
    //    oProposal.m_sProj_Title = projTitleTextBox.Text;
    //    oProposal.m_lJV = decimal.Parse(AmountJVTextBox.Text);
    //    oProposal.m_lSS = decimal.Parse(IPValueTextBox.Text);
    //    oProposal.m_sProj_Num = projNumTextBox.Text;
    //    oProposal.m_sProj_Desc = projDescTextBox.Text;
    //    oProposal.m_iEppriorityId = int.Parse(EPPriorityDropDownList.SelectedValue);

    //    SaveIP2FileSystem UpLoadMe = new SaveIP2FileSystem();
    //    string sFileName = UpLoadMe.UploadInvestmentProposal(UploadProposal, projNumTextBox.Text);
    //    if (sFileName != "")
    //    {
    //        oProposalMgt.EditProposalWtFile(oProposal, oSessnx.getOnlineUser.m_iUserId, sFileName);

    //        bool IPNotSupportedApproved = oProposalMgt.IPNotSupportedApproved(oProposal.m_lProposalId);
    //        if (IPNotSupportedApproved == true)
    //        {
    //            oProposalMgt.IPInitiatorReloadsUpdate(oProposal, oSessnx.getOnlineUser, projTitleTextBox.Text, ApplicationURL.MyAppURL());
    //            string mssg = "Proposal successfully updated. Your Line Team Lead, the Business Process Owner and all Support Functions have been notified.";
    //            ajaxWebExtension.showJscriptAlert(Page, this, mssg);
    //        }
    //        else
    //        {
    //            //Send a mail notifying the Users in the IPLine that IP Initiator has updated IP.
    //            sendMail oSendMail = new sendMail(oSessnx.getOnlineUser.structUserIdx);
    //            oSendMail.IPInitiatorUpdatedIP(oProposalMgt.getEmailAddressForIPLine(oProposal.m_lProposalId), oSessnx.getOnlineUser.m_sEmail, 
    //                oProposal.m_sProj_Title, ApplicationURL.MyAppURL(), oProposal.m_sProj_Num);
    //            ajaxWebExtension.showJscriptAlert(Page, this, "Proposal successfully updated.");
    //        }
    //    }
    //    else
    //    {
    //        oProposalMgt.EditProposalWithoutPDFFile(oProposal, oSessnx.getOnlineUser.m_iUserId, oSessnx.getOnlineUser, UploadProposal);

    //        bool IPNotSupportedApproved = oProposalMgt.IPNotSupportedApproved(oProposal.m_lProposalId);
    //        if (IPNotSupportedApproved == true)
    //        {
    //            oProposalMgt.IPInitiatorReloadsUpdate(oProposal, oSessnx.getOnlineUser, projTitleTextBox.Text, ApplicationURL.MyAppURL());
    //            string mssg = "Proposal successfully updated. Your Line Team Lead, the Business Process Owner and all Support Functions have been notified.";
    //            ajaxWebExtension.showJscriptAlert(Page, this, mssg);
    //        }
    //        else
    //        {
    //            //Send a mail notifying the Users in the IPLine that IP Initiator has updated IP.
    //            sendMail oSendMail = new sendMail();
    //            oSendMail.IPInitiatorUpdatedIP(oProposalMgt.getEmailAddressForIPLine(oProposal.m_lProposalId), oSessnx.getOnlineUser.m_sEmail, oProposal.m_sProj_Title, ApplicationURL.MyAppURL(), oProposal.m_sProj_Num);
    //            MessageBox.Show("Proposal successfully updated. \n Now Select your Line Team Lead and click Forward button.");
    //        }
    //    }

    //    //if (oSessnx.getOnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.IP_Initiator)
    //    //{
    //    //    oProposalMgt.forwardProposalToLineTeamLead(lProposalId, projNumTextBox.Text, int.Parse(LineTeamLeadDropDownList.SelectedValue), 
    //    //        projTitleTextBox.Text, oSessnx.getOnlineUser.m_iCompany, oSessnx.getOnlineUser.m_iFunction, oSessnx.getOnlineUser);
    //    //}

    //    //if (!oProposalMgt.IPNotSupportedApproved(oProposal.m_lProposalId))
    //    //{
    //    //    LineTeamLeadDropDownList.Enabled = false;
    //    //    oProposalMgt.forwardProposalToLineTeamLead(lProposalId, projNumTextBox.Text, int.Parse(LineTeamLeadDropDownList.SelectedValue),
    //    //        projTitleTextBox.Text, oSessnx.getOnlineUser.m_iCompany, oSessnx.getOnlineUser.m_iFunction, oSessnx.getOnlineUser);
    //    //}

    //    LoadProposal(oProposal.m_sProposalFileName);
    //}
}