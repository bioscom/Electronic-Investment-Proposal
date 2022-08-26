using System;
using System.Web.UI;
using System.Collections.Generic;
using System.Linq;

public partial class ApprovalSupportFunction_AddComment : aspnetPage
{
    ProposalMgt oProposalMgt = new ProposalMgt();
    appUserMgt oAppUserMgt = new appUserMgt();
    Proposal oProposal = new Proposal();
    NonMandatorySupportWarnings warnings = new NonMandatorySupportWarnings();
    ProposalApprovalDetailsMgt MyProposalApprovalDetails = new ProposalApprovalDetailsMgt();
    SupportApproverCommentMgt oSupportApproverCommentMgt = new SupportApproverCommentMgt();
    shellCompanies Companies = new shellCompanies();
    string oMessage = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Proposalid"] != null)
        {
            long lProposalID = long.Parse(Request.QueryString["Proposalid"].ToString());
            oProposal = oProposalMgt.objGetProposalById(lProposalID);

            IPDetailInfo1.LoadProposalDetails(oProposal);

            if (!IsPostBack)
            {
                pnlForwardComment.Visible = false;
                if (((oSessnx.getOnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.VP) && (oSessnx.getOnlineUser.eFunction.m_sFunction == cpdmsFunctionsNames.Finance))
                    || (oSessnx.getOnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.Finance_Signature)
                    || (oSessnx.getOnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.Finance_Director))
                    oProposalMgt.FillFinanceApprovalState(ddlSupportStand);
                else if (oSessnx.getOnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.CERP)
                {
                    oProposalMgt.FillApproveState(ddlSupportStand);
                    forwardButton.Text = "Submit";
                }
                else oProposalMgt.FillSupportState(ddlSupportStand);

                ProposalApprovalDetails oProposalApprovalDetails = MyProposalApprovalDetails.objGetProposalSupportDetailsByProposalUserId(oSessnx.getOnlineUser.m_iUserId, lProposalID);

                //SupportLabel.Text = oSessnx.getOnlineUser.m_sRole;
                SupportLabel.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oSessnx.getOnlineUser.m_iUserRoleId);
                CommentTextBox.Text = oProposalApprovalDetails.m_sComments;

                if ((oSessnx.getOnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.Finance_Signature) || (oSessnx.getOnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.CERP))
                {
                    digiSignButton.Visible = true;
                    warningLabel.Visible = true;
                }
            }
        }
    }

    //digiSignButton.Attributes.Add("onclick", "return CenteredPopup('DigitalSignature.aspx?ProposalID=" + proposal.IDPROPOSAL + "','MyWindowForm','550','230','yes');return false");

    protected void forwardButton_Click(object sender, EventArgs e)
    {
        //sendMail oSendMail = new sendMail(oSessnx.getOnlineUser.structUserIdx);
        //List<structUserMailIdx> toEmail = new List<structUserMailIdx>();

        try
        {
            long lProposalId = long.Parse(Request.QueryString["Proposalid"].ToString());
            oProposal = oProposalMgt.objGetProposalById(lProposalId);

            if ((CommentTextBox.Text == "") && (ddlSupportStand.SelectedValue == SupportState.iNotSupported.ToString()))
            {
                string mssg = "Please, enter reason(s) why Not Supported or Not Approved in the comment box.";
                ajaxWebExtension.showJscriptAlert(Page, this, mssg);
            }
            else
            {
                //Note: The Forward Button saves the Support Comment and sends an Email to the IP Initiator and Copy the Business Process Owner of the IP OU           
                appUsers oIPInitiator = oAppUserMgt.objGetUserByUserId(oProposal.m_iUserId);
                appUsers oBPO = oAppUserMgt.objGetUserByUserRoleCompany(oIPInitiator.m_iCompany, (int) appUsersRoles.userRole.Business_Process_Owner);
                if(oBPO.m_iUserId == 0)
                {
                    string sCompany = Companies.objGetShellCompanyById(oIPInitiator.m_iCompany).m_sCompanyname;
                    ajaxWebExtension.showJscriptAlert(Page, this, sCompany + ", Business Process Owner(BPO) not found!!! Please contact System Administrator to set the profile of the BPO, to include " + sCompany + " as OU.");
                }
                else
                {
                    if (oSessnx.getOnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.Line_Team_Lead) LineTeamLead(oBPO, oIPInitiator, oProposal);
                    else if (oSessnx.getOnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.Finance_Signature) FinanceSignature(oBPO, oIPInitiator, oProposal);
                    else if (oSessnx.getOnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.Finance_Director) FinanceDirector(lProposalId, oIPInitiator, oProposal);
                    else if (oSessnx.getOnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.Technical_Planning_Manager) TechnicalPlanningManager(lProposalId, oIPInitiator, oProposal);
                    else if (oSessnx.getOnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.GM_Regional_Planning) GMREPlanning(oProposal, oIPInitiator, oBPO);
                    else if (oSessnx.getOnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.CERP) ExecutiveVicePresident(lProposalId, oIPInitiator, oProposal);
                    else
                    {
                        FunctionalSupport(lProposalId, oIPInitiator, oBPO, oProposal);
                        string mssg = "To Forward the comment or IP to the central support staff, select the checkbox below, enter multiple email addresses separated with semi-colon ;, then click send button";
                        ajaxWebExtension.showJscriptAlert(Page, this, mssg);
                    }
                }

                //These roles no longer needed in the IP approval process. Thanks. Verion No 12.0
                //else if ((oSessnx.getOnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.VP) && (oSessnx.getOnlineUser.eFunction.m_sFunction == cpdmsFunctionsNames.Finance)) GMFinance(lProposalId, oIPInitiator);
                //else if (oSessnx.getOnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.REVP) ExecutiveVicePresident(lProposalId, oIPInitiator);

                //This is no longer required since the role no longer exist in SHELL
                //oProposalMgt.MailEPGIPTracker(oProposal, oSessnx.getOnlineUser);
            }

            //Response.Redirect("~/ApprovalSupportFunction/PendingProposal.aspx?=rtMsg" + oMessage);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }
    private void GMREPlanning(Proposal oProposal, appUsers oIPInitiator, appUsers oBPO)
    {
        sendMail oSendMail = new sendMail(oSessnx.getOnlineUser.structUserIdx);
        try
        {
            appUsers oCerp = oAppUserMgt.objGetDefaultUsersByRole((int)appUsersRoles.userRole.CERP, DefaultRoleHolder.iDefault);
            if (oCerp.m_iUserId != 0)
            {
                //1. Enter Support Details for GM RE Plan
                bool success = oSupportApproverCommentMgt.AddComment(oSessnx.getOnlineUser, oProposal,
                    CommentTextBox.Text, int.Parse(ddlSupportStand.SelectedValue));
                if (success)
                {
                    //2. Send Mails to CERP.
                    bool bRet = false;
                    int iStand = int.Parse(ddlSupportStand.SelectedValue);

                    if (iStand == SupportState.iSupported)
                    {
                        bRet = oProposalMgt.ProposalSupportedApproved(oProposal, oSessnx.getOnlineUser,
                            oBPO.structUserIdx, oIPInitiator.structUserIdx);
                        if (bRet)
                        {
                            //Assign IP to CERP Representative
                            oProposalMgt.AssignIPtoNextSupportApprover(oCerp.m_iUserId, oProposal.m_lProposalId, oCerp.m_iUserRoleId);

                            //Update Proposal Status
                            oProposalMgt.UpdateProposalStatus(oProposal.m_lProposalId,
                                (int) IPStatusReporter.ipStatusRpt.AwaitsCERPSupport);

                            oSendMail.MailCERP(oCerp.structUserIdx, oProposal);
                            ajaxWebExtension.showJscriptAlert(Page, this, "Proposal Successfully supported. Capital Expenditure Review Panel and IP Initiator have been notified.");
                        }
                    }
                    else if (iStand == SupportState.iNotSupported)
                    {
                        bRet = oProposalMgt.ProposalNotSupported(oProposal, oSessnx.getOnlineUser, CommentTextBox.Text);
                        ajaxWebExtension.showJscriptAlert(Page, this, "IP Initiator has been notified of this IP Not Supported");
                    }
                    //Response.Redirect("~/ApprovalSupportFunction/PendingProposal.aspx");
                }
            }
            else
            {
                ajaxWebExtension.showJscriptAlert(Page, this, "Capital Expenditure Review Panel representative not found. Please contact the Administrator to add CERP Rep.");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    protected void closeButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ApprovalSupportFunction/PendingProposal.aspx");
    }

    //Digital Signatures Code
    protected void digiSignImgBtn_Click(object sender, ImageClickEventArgs e)
    {
        WriteFile MyFile = new WriteFile();

        try
        {
            long lProposalID = long.Parse(Request.QueryString["Proposalid"].ToString());
            Proposal MyProposal = new Proposal();
            oProposalMgt.objGetProposalById(lProposalID);

            byte[] MyData = MyFile.DownLoadProposal(MyProposal.m_sProposalFileName);

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Proposal.pdf");
            Response.AddHeader("Content-Length", Convert.ToString(MyData.Length));
            Response.BinaryWrite(MyData);
            Response.End();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    protected void uploadButton_Click(object sender, EventArgs e)
    {
        long lProposalID = long.Parse(Request.QueryString["Proposalid"].ToString());
        Proposal oProposal = oProposalMgt.objGetProposalById(lProposalID);

        SaveIP2FileSystem UpLoadMe = new SaveIP2FileSystem();
        string oMessage = "";
        fileProperty MyFileProperties = UpLoadMe.UploadInvestmentProposal(UploadProposal, oProposal.m_sProj_Num, ref oMessage); //return the name with which proposal pdf doc was saved
        if (MyFileProperties.sFileName != "")
        {
            bool bRet = oProposalMgt.UpdateFileName(lProposalID, MyFileProperties.sFileName);
            if (bRet) ajaxWebExtension.showJscriptAlert(Page, this, "Proposal successfully attached.");
        }
    }

    private void LockApproval(long lProposalId)
    {
        List<appUsers> NonMandatoryFunctionalSupport = warnings.GetNonMandatoryFunctionalSuppport(lProposalId);
        foreach (appUsers NoSupport in NonMandatoryFunctionalSupport)
        {
            warnings.AddComment(NoSupport.m_iUserId, lProposalId, "No comments received during SLA period.");
        }
    }

    private void LineTeamLead(appUsers oBPO, appUsers oIPInitiator, Proposal oProposal)
    {
        try
        {
            //1. Line Team Lead receives the IP from Initiator, if he Support the IP and forward to the BPO / Not Support the IP and send back to the Initiator
            //Proposal oProposal = new Proposal();

            sendMail oSendMail = new sendMail(oSessnx.getOnlineUser.structUserIdx);
            bool bRet = oSupportApproverCommentMgt.AddComment(oSessnx.getOnlineUser, oProposal, CommentTextBox.Text, int.Parse(ddlSupportStand.SelectedValue));
            if (bRet)
            {
                if (int.Parse(ddlSupportStand.SelectedValue) == SupportState.iSupported)
                {
                    //Note: Line Team Lead sends the IP to Business Process Owner
                    oProposalMgt.AssignIPtoNextSupportApprover(oBPO.m_iUserId, oProposal.m_lProposalId, oBPO.m_iUserRoleId);
                    bRet = oProposalMgt.UpdateProposalStatus(oProposal.m_lProposalId, (int)IPStatusReporter.ipStatusRpt.BPOSupport);
                    oSendMail.LineTeamLeadSupportsIP(oBPO.structUserIdx, oProposal.m_sProj_Title, oProposal.m_sProj_Num, oIPInitiator.structUserIdx);

                    ajaxWebExtension.showJscriptAlert(Page, this, "Proposal Successfully supported. BPO and IP Initiator have been notified.");
                }
                else if (int.Parse(ddlSupportStand.SelectedValue) == SupportState.iNotSupported)
                {
                    bRet = oProposalMgt.UpdateProposalStatus(oProposal.m_lProposalId, (int)IPStatusReporter.ipStatusRpt.LTLNotSupported);
                    oSendMail.IPNotSupported(oIPInitiator.structUserIdx, oBPO.structUserIdx, oProposal.m_sProj_Title, CommentTextBox.Text,
                        oSessnx.getOnlineUser.m_sFullName, oSessnx.getOnlineUser.eFunction.m_sFunction, oProposal.m_sProj_Num);

                    ajaxWebExtension.showJscriptAlert(Page, this, "IP Initiator has been notified of this IP Not Supported");
                }

                oProposalMgt.AuditTrail(oSessnx.getOnlineUser, int.Parse(ddlSupportStand.SelectedValue), CommentTextBox.Text, oProposal.m_lProposalId);
                oProposalMgt.DateLastActioned(oProposal.m_lProposalId, oSessnx.getOnlineUser);
            }
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
    }

    private void FinanceSignature(appUsers oBPO, appUsers oIPInitiator, Proposal oProposal)
    {
        try
        {
            sendMail oSendMail = new sendMail(oSessnx.getOnlineUser.structUserIdx);
            //Note: When Finance Signature approves an IP, it goes to BPO to forward the IP for Organisation Approval
            bool bRet = oSupportApproverCommentMgt.AddComment(oSessnx.getOnlineUser, oProposal, CommentTextBox.Text, int.Parse(ddlSupportStand.SelectedValue));
            if (bRet)
            {
                if (int.Parse(ddlSupportStand.SelectedValue) == SupportState.iFinanceApproval)
                {
                    //IP approved by Finance Signature, mail BPO and copy IP Initiator
                    bRet = oProposalMgt.UpdateProposalStatus(oProposal.m_lProposalId, (int)IPStatusReporter.ipStatusRpt.BPOToOrgApprover);
                    int xNo = oProposalMgt.NoAwaitingSupport(oProposal.m_lProposalId);
                    oSendMail.IPSupported(oBPO.structUserIdx, oIPInitiator.structUserIdx, oSessnx.getOnlineUser.eFunction.m_sFunction, oProposal.m_sProj_Title, oProposal.m_sProj_Num, xNo);

                    //Lock the IP against Non Mandatory Functional Support that are yet to support the IP
                    //warnings.FinanceSignatureApprovedIPAccessLocked(oProposal.m_lProposalId);
                    LockApproval(oProposal.m_lProposalId);

                    ajaxWebExtension.showJscriptAlert(Page, this, "Proposal approved, Business Process Owner and the IP Initiator have been notified. Thank you for your Support.");
                }
            }
            else if (int.Parse(ddlSupportStand.SelectedValue) == SupportState.iNotApproved)
            {
                oProposalMgt.ProposalNotSupported(oProposal, oSessnx.getOnlineUser, CommentTextBox.Text);
                ajaxWebExtension.showJscriptAlert(Page, this, "IP Initiator has been notified.");
            }

            oProposalMgt.AuditTrail(oSessnx.getOnlineUser, int.Parse(ddlSupportStand.SelectedValue), CommentTextBox.Text, oProposal.m_lProposalId);
            oProposalMgt.DateLastActioned(oProposal.m_lProposalId, oSessnx.getOnlineUser);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
    }

    private void FinanceDirector(long lProposalId, appUsers oIPInitiator, Proposal oProposal)
    {
        try
        {
            //Approval by Finance Director
            sendMail oSendMail = new sendMail(oSessnx.getOnlineUser.structUserIdx);
            //appUsers cpm = oAppUserMgt.objGetUsersByRole((int)appUsersRoles.userRole.Technical_Planning_Manager);
            appUsers corporatePlanningManager = new appUsers();
            List<appUsers> CPMS = oAppUserMgt.lstGetUsersByRole((int) appUsersRoles.userRole.Technical_Planning_Manager);
            foreach (var cpm in CPMS)
            {
                if (cpm.m_iDefaultRoleHolder == 1)
                {
                    corporatePlanningManager = cpm;
                }
            }

            if (corporatePlanningManager.m_iUserId != 0)
            {
                bool bRet = oSupportApproverCommentMgt.AddComment(oSessnx.getOnlineUser, oProposal, CommentTextBox.Text, int.Parse(ddlSupportStand.SelectedValue));
                if (bRet)
                {
                    if (int.Parse(ddlSupportStand.SelectedValue) == SupportState.iFinanceApproval)
                    {
                        //oProposalMgt.ForwardProposalToNextSuppportApprover(lProposalId, cpm.m_iUserId);

                        //Lock the IP against Non Mandatory Functional Support that are yet to support the IP
                        //warnings.FinanceSignatureApprovedIPAccessLocked(oProposal.m_lProposalId);
                        LockApproval(oProposal.m_lProposalId);

                        //Finance Director assigns IP To Technical Planning Manager
                        oProposalMgt.AssignIPtoNextSupportApprover(corporatePlanningManager.m_iUserId, lProposalId, corporatePlanningManager.m_iUserRoleId);
                        //Update proposal status
                        oProposalMgt.UpdateProposalStatus(oProposal.m_lProposalId, (int)IPStatusReporter.ipStatusRpt.AwaitCorpPlanMgr);
                        //Send mail to Technical Planning manager and copy the necessary people
                        oSendMail.sendMailToCPM(corporatePlanningManager.structUserIdx, oIPInitiator.structUserIdx, oSessnx.getOnlineUser.eFunction.m_sFunction, oProposal.m_sProj_Title, oProposal.m_sProj_Num, oProposal.m_lProposalId);
                        ajaxWebExtension.showJscriptAlert(Page, this, "Proposal Successfully supported. Corporate Planning Manager and IP Initiator have been notified.");
                    }
                    else if (int.Parse(ddlSupportStand.SelectedValue) == SupportState.iNotApproved)
                    {
                        oProposalMgt.ProposalNotSupported(oProposal, oSessnx.getOnlineUser, CommentTextBox.Text);
                        ajaxWebExtension.showJscriptAlert(Page, this, "IP Initiator has been notified.");
                    }

                    oProposalMgt.AuditTrail(oSessnx.getOnlineUser, int.Parse(ddlSupportStand.SelectedValue), CommentTextBox.Text, oProposal.m_lProposalId);
                    oProposalMgt.DateLastActioned(oProposal.m_lProposalId, oSessnx.getOnlineUser);
                }
                else
                {
                    ajaxWebExtension.showJscriptAlert(Page, this, "Please try again later.");
                }
            }
            else
            {
                ajaxWebExtension.showJscriptAlert(Page, this, "The Technical Planning Manager was not found for the IP OU, Administrator has been notified.");

                //Send an email to alert the System Administrator, that the 
                List<structUserMailIdx> toAdmins = new List<structUserMailIdx>();
                List<appUsers> SystemAdministrators = oAppUserMgt.lstGetUsersByRole((int)appUsersRoles.userRole.Administrator);
                foreach (appUsers SystemAdministrator in SystemAdministrators)
                {
                    toAdmins.Add(SystemAdministrator.structUserIdx);
                }

                oSendMail.ApplicationErrorMessage(toAdmins, "The Technical Planning Manager was not found for Proposals from " + oProposal.m_sIPOriginatingUnit + " OU. Please, add the Current Technical Planning Manager for the OU.");
            }
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
    }

    private void TechnicalPlanningManager(long lProposalId, appUsers oIPInitiator, Proposal oProposal)
    {
        try
        {
            sendMail oSendMail = new sendMail(oSessnx.getOnlineUser.structUserIdx);
            //OU Technical Planning Manager forwards IP to GM Regional Planning
            int EPNigeriaLogic = oProposalMgt.OriginatingUnit(oProposal);
            bool bRet = oSupportApproverCommentMgt.AddComment(oSessnx.getOnlineUser, oProposal, CommentTextBox.Text, int.Parse(ddlSupportStand.SelectedValue));
            if (bRet)
            {
                if (int.Parse(ddlSupportStand.SelectedValue) == SupportState.iSupported)
                {
                    appUsers GMREPlan = oAppUserMgt.objGetUsersByRole((int)appUsersRoles.userRole.GM_Regional_Planning);
                    //oProposalMgt.ForwardProposalToNextSupportApprover(lProposalId, GMREPlan.m_iUserId);
                    oProposalMgt.AssignIPtoNextSupportApprover(GMREPlan.m_iUserId, lProposalId, GMREPlan.m_iUserRoleId);
                    //Update Proposal Status
                    bRet = oProposalMgt.UpdateProposalStatus(oProposal.m_lProposalId, (int)IPStatusReporter.ipStatusRpt.AwaitsGMREPlanSupport);
                    //Send Email
                    oSendMail.MailGMREPlanning(GMREPlan.structUserIdx, oIPInitiator.structUserIdx, oProposal.m_sProj_Title, oProposal.m_sProj_Num);
                    ajaxWebExtension.showJscriptAlert(Page, this, "Proposal Successfully supported. GM Regional Planning and IP Initiator have been notified.");
                }
                else if (int.Parse(ddlSupportStand.SelectedValue) == SupportState.iNotSupported)
                {
                    oProposalMgt.ProposalNotSupported(oProposal, oSessnx.getOnlineUser, CommentTextBox.Text);
                    ajaxWebExtension.showJscriptAlert(Page, this, "IP Initiator has been notified.");
                }

                oProposalMgt.AuditTrail(oSessnx.getOnlineUser, int.Parse(ddlSupportStand.SelectedValue), CommentTextBox.Text, oProposal.m_lProposalId);
                oProposalMgt.DateLastActioned(oProposal.m_lProposalId, oSessnx.getOnlineUser);
            }
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
    }

    private void GMFinance(long lProposalId, appUsers oIPInitiator, Proposal oProposal)
    {
        try
        {
            sendMail oSendMail = new sendMail(oSessnx.getOnlineUser.structUserIdx);
            //GM Finance Approves IP and forwards to VP Nigeria
            int EPNigeriaLogic = oProposalMgt.OriginatingUnit(oProposal);
            bool bRet = oSupportApproverCommentMgt.AddComment(oSessnx.getOnlineUser, oProposal, CommentTextBox.Text, int.Parse(ddlSupportStand.SelectedValue));
            if (bRet)
            {
                if (int.Parse(ddlSupportStand.SelectedValue) == SupportState.iFinanceApproval)
                {
                    appUsers VPNigeriaGabon = oAppUserMgt.objGetUsersByRole((int)appUsersRoles.userRole.REVP);
                    oProposalMgt.AssignIPtoNextSupportApprover(VPNigeriaGabon.m_iUserId, lProposalId, VPNigeriaGabon.m_iUserRoleId);

                    //Remove the proposal from GM RE Planining'd Intray
                    ProposalApprovalDetails oProposalApprovalDetails = oProposalMgt.objGetProposalSupportApprovalDetailsByRole(lProposalId, (int)appUsersRoles.userRole.GM_Regional_Planning);
                    if (oProposalApprovalDetails.m_iUserId != 0)
                    {
                        oProposalMgt.CloseGMREPlanIntray(lProposalId, oProposalApprovalDetails.m_iUserId);
                    }

                    //Update the IP status.
                    bRet = oProposalMgt.UpdateProposalStatus(oProposal.m_lProposalId, (int)IPStatusReporter.ipStatusRpt.AwaitsREVPSupport);

                    //Send Mail to Regional VP
                    oSendMail.MailRegionalVicePresident(VPNigeriaGabon.structUserIdx, oIPInitiator.structUserIdx, oProposal.m_sProj_Title, oProposal.m_sProj_Num);
                }
                else if (int.Parse(ddlSupportStand.SelectedValue) == SupportState.iNotApproved)
                {
                    oProposalMgt.ProposalNotSupported(oProposal, oSessnx.getOnlineUser, CommentTextBox.Text);
                }

                oProposalMgt.AuditTrail(oSessnx.getOnlineUser, int.Parse(ddlSupportStand.SelectedValue), CommentTextBox.Text, oProposal.m_lProposalId);
                oProposalMgt.DateLastActioned(oProposal.m_lProposalId, oSessnx.getOnlineUser);
            }
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    private void ExecutiveVicePresident(long lProposalId, appUsers oIPInitiator, Proposal oProposal)
    {
        try
        {
            sendMail oSendMail = new sendMail(oSessnx.getOnlineUser.structUserIdx);
            //Executive Vice president approves the IP Finance
            int EPNigeriaLogic = oProposalMgt.OriginatingUnit(oProposal);
            bool bRet = oSupportApproverCommentMgt.AddComment(oSessnx.getOnlineUser, oProposal, CommentTextBox.Text, int.Parse(ddlSupportStand.SelectedValue));
            if (bRet)
            {
                if (int.Parse(ddlSupportStand.SelectedValue) == SupportState.iApproved)
                {
                    //Update the IP status.
                    bRet = oProposalMgt.UpdateProposalStatus(oProposal.m_lProposalId, (int)IPStatusReporter.ipStatusRpt.Approved);

                    //Send Mail to IP Initiator and copy all that is involved in the support and approval process of the IP.
                    List<structUserMailIdx> toAllEmailAddy = new List<structUserMailIdx>();
                    ProposalApprovalDetailsMgt oProposalApprovalDetailsMgt = new ProposalApprovalDetailsMgt();
                    List<ProposalApprovalDetails> oProposalApprovalDetails = oProposalApprovalDetailsMgt.lstGetProposalSupportDetailsByProposalId(lProposalId);
                    foreach(ProposalApprovalDetails oProposalApprovalDetail in oProposalApprovalDetails)
                    {
                        toAllEmailAddy.Add(oAppUserMgt.objGetUserByUserId(oProposalApprovalDetail.m_iUserId).structUserIdx);
                    }

                    oSendMail.ProposalApproved(oIPInitiator.structUserIdx, toAllEmailAddy, oProposal.m_sProj_Title, oProposal.m_sProj_Num);
                }
                else if (int.Parse(ddlSupportStand.SelectedValue) == SupportState.iNotApproved)
                {
                    //Update the IP status.
                    bRet = oProposalMgt.UpdateProposalStatus(oProposal.m_lProposalId, (int)IPStatusReporter.ipStatusRpt.Rejected);

                    oProposalMgt.ProposalNotSupported(oProposal, oSessnx.getOnlineUser, CommentTextBox.Text);
                }

                oProposalMgt.AuditTrail(oSessnx.getOnlineUser, int.Parse(ddlSupportStand.SelectedValue), CommentTextBox.Text, oProposal.m_lProposalId);
                oProposalMgt.DateLastActioned(oProposal.m_lProposalId, oSessnx.getOnlineUser);
            }
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    private void FunctionalSupport(long lProposalId, appUsers oIPInitiator, appUsers oBPO, Proposal oProposal)
    {
        try
        {
            sendMail oSendMail = new sendMail(oSessnx.getOnlineUser.structUserIdx);
            bool bRet = false;
            int iStand = int.Parse(ddlSupportStand.SelectedValue);
            bRet = oSupportApproverCommentMgt.AddComment(oSessnx.getOnlineUser, oProposal, CommentTextBox.Text, iStand);
            if (bRet)
            {
                if (iStand == SupportState.iNotSupported)
                {
                    bRet = oProposalMgt.ProposalNotSupported(oProposal, oSessnx.getOnlineUser, CommentTextBox.Text);
                    oMessage = "Reason(s) Proposal Not Supported or Not Approved have been sent to Business Process Owner and the IP Initiator has been notified.";
                }
                else if (iStand == SupportState.iSupported)
                {
                    bRet = oProposalMgt.ProposalSupportedApproved(oProposal, oSessnx.getOnlineUser, oBPO.structUserIdx, oIPInitiator.structUserIdx);
                    if (bRet)
                    {
                        //Check to see if all Mandatory functional support that have supported the IP.
                        //If all functional support have supported, send mail to BPO and copy all the Non Mandatory Functional Support to support before the 
                        //OU Finance Director's approval

                        bool HaveAllMandatoryRequiredSupportSupported = oSupportApproverCommentMgt.MandatoryFuntionalSupportStand(oProposal);
                        if (HaveAllMandatoryRequiredSupportSupported)
                        {
                            appUsers oCERP = oAppUserMgt.objGetDefaultUsersByRole((int)appUsersRoles.userRole.CERP, DefaultRoleHolder.iDefault);
                            //Change the Status to Business Process Owner to forward the IP to OU Finance Director/Finance Signature depending on the value of the IP.
                            IPLimit oIPLimits = new IPLimit();
                            IPLimit.IPLevels oIPLevels = oIPLimits.Limits();
                            if (oProposal.m_lSS < oIPLevels.iValue2)
                            {
                                bRet = oProposalMgt.UpdateProposalStatus(oProposal.m_lProposalId, (int)IPStatusReporter.ipStatusRpt.BPOToFinSig);
                            }
                            else if (oProposal.m_lSS >= oIPLevels.iValue2)
                            {
                                //Then assign the IP to CERP personnel.
                                oProposalMgt.AssignIPtoNextSupportApprover(oCERP.m_iUserId, oProposal.m_lProposalId, oCERP.m_iUserRoleId);

                                //Note: This is a new development from oga Bolaji Adewale and Remi Olomodosi. If the IP is >= $10Mln it should go for CERP.
                                bRet = oProposalMgt.UpdateProposalStatus(oProposal.m_lProposalId, (int)IPStatusReporter.ipStatusRpt.AwaitsCERPSupport);
                            }

                            //Add the email of CERP to this list.
                            List<structUserMailIdx> toBPOCERP = new List<structUserMailIdx>();
                            toBPOCERP.Add(oCERP.structUserIdx);
                            toBPOCERP.Add(oBPO.structUserIdx);


                            //Get the list of Non Mandatory Functional Suppport who have not supported when all the Mandatory have supported.
                            List<structUserMailIdx> toNonMandatories = new List<structUserMailIdx>();
                            List<appUsers> NonMandatoryFunctionalSupport = warnings.GetNonMandatoryFunctionalSuppport(lProposalId);
                            if (NonMandatoryFunctionalSupport.Count > 0)
                            {
                                foreach (appUsers NoSupport in NonMandatoryFunctionalSupport)
                                    toNonMandatories.Add(NoSupport.structUserIdx);

                                //oSendMail.AllMandatorySupportCompleted(toBPOCERP, oIPInitiator.structUserIdx, oProposal.m_sProj_Title, oProposal.m_sProj_Num);
                                oSendMail.NonMandatorySupportWarning(toNonMandatories, oBPO.structUserIdx, oProposal.m_sProj_Title, oProposal.m_sProj_Num);
                            }
                            //Send mail to BPO that all the Mandatory functional Support have been received and he should forward the IP to OU Finance Director/Finance Signature
                            oSendMail.AllMandatorySupportCompleted(toBPOCERP, oIPInitiator.structUserIdx, oProposal.m_sProj_Title, oProposal.m_sProj_Num);

                            //else
                            //{
                            //    //Send mail to BPO that all the Mandatory functional Support have been received and he should forward the IP to OU Finance Director/Finance Signature
                            //    oSendMail.AllMandatorySupportCompleted(toBPOCERP, oIPInitiator.structUserIdx, oProposal.m_sProj_Title, oProposal.m_sProj_Num);
                            //}
                        }
                        else
                        {
                            oMessage = "Proposal Supported, Business Process Owner, CERP and the IP Initiator have been notified. Thank you for your Support.";
                        }
                    }
                }

                oProposalMgt.AuditTrail(oSessnx.getOnlineUser, int.Parse(ddlSupportStand.SelectedValue), CommentTextBox.Text, oProposal.m_lProposalId);
                oProposalMgt.DateLastActioned(oProposal.m_lProposalId, oSessnx.getOnlineUser);

                ajaxWebExtension.showJscriptAlert(Page, this, oMessage);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
            appMonitor.logAppExceptions(ex);
        }
    }
    protected void ckbForward_CheckedChanged(object sender, EventArgs e)
    {
        if (ckbForward.Checked)
        {
            pnlForwardComment.Visible = true;
        }
        else
        {
             pnlForwardComment.Visible = false;
        }
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        long lProposalId = long.Parse(Request.QueryString["Proposalid"].ToString());
            oProposal = oProposalMgt.objGetProposalById(lProposalId);

        sendMail oSendMail = new sendMail(oSessnx.getOnlineUser.structUserIdx);
        string[] toPple = txtEmail.Text.Split(';');
        oSendMail.CentralSupportStaff(toPple, oSessnx.getOnlineUser.m_sEmail, oProposal, int.Parse(ddlSupportStand.SelectedValue), CommentTextBox.Text);

        ckbForward.Checked = false;
        pnlForwardComment.Visible = false;
        oMessage = "Sent!";
        ajaxWebExtension.showJscriptAlert(Page, this, oMessage);
    }
}