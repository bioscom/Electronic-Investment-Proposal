using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;

public partial class Common_ViewProposalStatus : aspnetPage
{
    //SupportApprovalStatus SupportApproval = new SupportApprovalStatus();
    //Proposal proposal = new Proposal();
    readonly ProposalMgt oProposalMgt = new ProposalMgt();

    string mSubject = "";

    protected void Page_Load(object sender, EventArgs e)
    {        
        if (Request.QueryString["Proposalid"] != null)
        {
            Proposal oProposal = oProposalMgt.objGetProposalById(int.Parse(Request.QueryString["Proposalid"].ToString()));

            IPDetailInfo1.LoadProposalDetails(oProposal);

            if (oProposal.m_iDiscontinue == (int)IPStatus.iDiscontinued)
            {
                string mssg = "Note: This proposal is currently discontinued. It will be automatically deleted, after 60 days, if not reactivated.";
                ajaxWebExtension.showJscriptAlert(Page, this, mssg);
            }

            DisableAllCheckBoxes();
            OnOffCheckBoxes();
            
            //NotRequired();
            //GetFunctionalSupportApproverComments(oProposal.m_lProposalId);
            GetApproverComments(oProposal.m_lProposalId);

            //NotRequired();
            //If the value of the IP is Greater than the Local IP Limit, the functional Support will not be able to support and the BPO should override functional support.
            IPLimit oIPLimits = new IPLimit();
            IPLimit.IPLevels oIPLevels = oIPLimits.Limits();

            //decimal TotalIPValueDec = Convert.ToDecimal(oProposal.m_lSS);
            //int TotalIPValue = Convert.ToInt32(TotalIPValueDec*100);

            GetFunctionalSupportComments(oProposal.m_lProposalId);
            GetApproverComments(oProposal.m_lProposalId);

            //if (TotalIPValue > (oIPLevels.iValue4*100))
            //{
            //    string mssg = "The value of the IP, $" + oProposal.m_lSS + "mln, is greater than Local Functional IP Support Limit. Therefore, Functional support overriden.";
            //    controllerCommentLabel.Text = mssg;
            //    securityCommentLabel.Text = mssg;
            //    econsCommentLabel.Text = mssg;
            //    hseCommentLabel.Text = mssg;
            //    legalCommentLabel.Text = mssg;
            //    scdCommentLabel.Text = mssg;
            //    taxCommentLabel.Text = mssg;
            //    treasuryCommentLabel.Text = mssg;
            //    ITCommentLabel.Text = mssg;
            //    scdCommentLabel.Text = mssg;
            //    SCMCommentLabel.Text = mssg;
            //}
            //else
            //{
            //    GetFunctionalSupportComments(oProposal.m_lProposalId);
            //    GetApproverComments(oProposal.m_lProposalId);
            //}
        }
    }

    protected void forwardReminderButton_Click(object sender, EventArgs e)
    {
        appUserMgt oAppUserMgt = new appUserMgt();
        sendMail oSendMail = new sendMail(oSessnx.getOnlineUser.structUserIdx);
        List<structUserMailIdx> eToEmail = new List<structUserMailIdx>();
        //This is used by the BPO to forward a reminder mail to those who have not attended to the IP since it was forwarded to them by the BPO
        try
        {
            Proposal oProposal = oProposalMgt.objGetProposalById(int.Parse(Request.QueryString["Proposalid"].ToString()));
            mSubject = oProposal.m_sProj_Title;

            if (bfmCheckBox.Checked)
                eToEmail.Add(oAppUserMgt.objGetUserByUserId(int.Parse(bfmHF.Value)).structUserIdx);

            if (cplanCheckBox.Checked)
                eToEmail.Add(oAppUserMgt.objGetUserByUserId(int.Parse(cplanHF.Value)).structUserIdx);

            if (ecoCheckBox.Checked)
                eToEmail.Add(oAppUserMgt.objGetUserByUserId(int.Parse(ecoHF.Value)).structUserIdx);

            if (hseCheckBox.Checked)
                eToEmail.Add(oAppUserMgt.objGetUserByUserId(int.Parse(hseHF.Value)).structUserIdx);

            if (legalCheckBox.Checked)
                eToEmail.Add(oAppUserMgt.objGetUserByUserId(int.Parse(legalHF.Value)).structUserIdx);

            if (secCheckBox.Checked)
                eToEmail.Add(oAppUserMgt.objGetUserByUserId(int.Parse(secHF.Value)).structUserIdx);

            if (spcaCheckBox.Checked)
                eToEmail.Add(oAppUserMgt.objGetUserByUserId(int.Parse(spcaHF.Value)).structUserIdx);

            if (taxCheckBox.Checked)
                eToEmail.Add(oAppUserMgt.objGetUserByUserId(int.Parse(taxHF.Value)).structUserIdx);

            if (treasuryCheckBox.Checked)
                eToEmail.Add(oAppUserMgt.objGetUserByUserId(int.Parse(treasuryHF.Value)).structUserIdx);

            if (ltleadCheckBox.Checked)
                eToEmail.Add(oAppUserMgt.objGetUserByUserId(int.Parse(ltleadHF.Value)).structUserIdx);

            if (finCheckBox.Checked)
                eToEmail.Add(oAppUserMgt.objGetUserByUserId(int.Parse(finHF.Value)).structUserIdx);

            if (GMfinCheckBox.Checked)
                eToEmail.Add(oAppUserMgt.objGetUserByUserId(int.Parse(GMfinHF.Value)).structUserIdx);

            //if (mdCheckBox.Checked)
            //    eToEmail.Add(oAppUserMgt.objGetUserByUserId(int.Parse(mdHF.Value)).structUserIdx);

            if (gmreplanCheckBox.Checked)
                eToEmail.Add(oAppUserMgt.objGetUserByUserId(int.Parse(gmREHF.Value)).structUserIdx);

            if (cerpCheckBox.Checked)
                eToEmail.Add(oAppUserMgt.objGetUserByUserId(int.Parse(CerpHF.Value)).structUserIdx);

            if (vpCheckBox.Checked)
                eToEmail.Add(oAppUserMgt.objGetUserByUserId(int.Parse(vpHF.Value)).structUserIdx);

            if (revpCheckBox.Checked)
                eToEmail.Add(oAppUserMgt.objGetUserByUserId(int.Parse(revpHF.Value)).structUserIdx);

            if (CPMCheckBox.Checked)
                eToEmail.Add(oAppUserMgt.objGetUserByUserId(int.Parse(cpmHF.Value)).structUserIdx);

            if (VPfinCheckBox.Checked)
                eToEmail.Add(oAppUserMgt.objGetUserByUserId(int.Parse(VPfinHF.Value)).structUserIdx);

            if (eToEmail.Count > 0)
            {
                if (oSendMail.PendingProposalReminder(eToEmail, oProposal.m_sProj_Title, oProposal.m_sProj_Num))
                {
                    ajaxWebExtension.showJscriptAlert(Page, this, "Reminder mail Successfully sent.");
                }

                DisableAllCheckBoxes();
                OnOffCheckBoxes();
            }
            else
            {
                ajaxWebExtension.showJscriptAlert(Page, this, "Please, select reminder mail receiver!!!");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    //private void NotRequired()
    //{
    //    if (SupportApproval.IPGT2ndQuartile(proposal) == false)
    //    {
    //        CPMCommentLabel.ForeColor = System.Drawing.Color.Red;
    //        CPMCommentLabel.Text = "Corporate Planning Manager support not required.";
    //        CPMCheckBox.Enabled = false;

    //        directorCommentLabel.ForeColor = System.Drawing.Color.Red;
    //        directorCommentLabel.Text = "MD Support not required.";
    //        mdCheckBox.Enabled = false;

    //        //****************************************************************************************************************
    //        //This is a new requirement for the March 2011, that Tax Support is required for all IP Values.
    //        //****************************************************************************************************************
    //        //taxCommentLabel.ForeColor = System.Drawing.Color.Red;
    //        //taxCommentLabel.Text = "Tax Functional Support not required.";
    //        //taxCheckBox.Enabled = false;

    //        GMREPlanCommentLabel.ForeColor = System.Drawing.Color.Red;
    //        GMREPlanCommentLabel.Text = "GM Regional Planning Support not required.";
    //        gmreplanCheckBox.Enabled = false;

    //        revpCommentLabel.ForeColor = System.Drawing.Color.Red;
    //        revpCommentLabel.Text = "Regional Vice President's Approval not required.";
    //        revpCheckBox.Enabled = false;
    //    }
    //}

    private void GetFunctionalSupportComments(long lProposalId)
    {
        appUsers oAppUsers = new appUsers();
        appUserMgt oAppUserMgt = new appUserMgt();
        SupportApproverCommentMgt functionalComments = new SupportApproverCommentMgt();

        try
        {
            List<SupportApproverComments> oComments = functionalComments.lstGetFunctionalSupportsApproverComments(lProposalId);
            
            //Get all functional Support who have received the IP
            foreach (SupportApproverComments oComment in oComments)
            {
                //Check the Individual comments, by ProposalID and UserID
                SupportApproverComments MyComment = functionalComments.objGetFunctionalSupportsApproverCommentByUserId(lProposalId, oComment.m_iUserId);
                oAppUsers = oAppUserMgt.objGetUserByUserId(MyComment.m_iUserId);

                if (MyComment.m_iUserRoleId == (int)appUsersRoles.userRole.Business_Process_Owner)
                {
                    if (MyComment.m_iStand == SupportState.iBPOStandDefault)
                    {
                        planStandLabel.Text = "BPO's action awaited.";
                    }
                    else
                    {
                        planStandLabel.Text = oProposalMgt.MyStand(MyComment.m_iStand);
                    }
                    cplanCheckBox.Enabled = false;
                    planCommentLabel.Text = MyComment.m_sComments;
                    planDateReceived.Text = MyComment.m_sDateReceived;
                    planDateReviewedLabel.Text = MyComment.m_sDateComment;
                    cplanHF.Value = oAppUsers.m_iUserId.ToString();
                    responsibleCPLANLabel.Text = oAppUsers.m_sFullName;
                    oProposalMgt.SupportStatusCode(MyComment.m_iStand, planStandLabel, cplanCheckBox);
                }
                else if (MyComment.m_iUserRoleId == (int)appUsersRoles.userRole.Line_Team_Lead)
                {
                    managerStandLabel.Text = oProposalMgt.MyStand(MyComment.m_iStand);
                    managerCommentLabel.Text = MyComment.m_sComments;
                    managerDateReceived.Text = MyComment.m_sDateReceived;
                    managerDateReviewedLabel.Text = MyComment.m_sDateComment;
                    ltleadHF.Value = oAppUsers.m_iUserId.ToString();
                    responsibleTeamLeadLabel.Text = oAppUsers.m_sFullName;
                    oProposalMgt.SupportStatusCode(MyComment.m_iStand, managerStandLabel, ltleadCheckBox);
                }
                else if (MyComment.m_iUserRoleId == (int)appUsersRoles.userRole.Controllers)
                {
                    controllerStandLabel.Text = oProposalMgt.MyStand(MyComment.m_iStand);
                    controllerCommentLabel.Text = MyComment.m_sComments;
                    controllerDateReceived.Text = MyComment.m_sDateReceived;
                    controllerDateReviewedLabel.Text = MyComment.m_sDateComment;
                    bfmHF.Value = oAppUsers.m_iUserId.ToString();
                    responsibleBFMLabel.Text = oAppUsers.m_sFullName;
                    oProposalMgt.SupportStatusCode(MyComment.m_iStand, controllerStandLabel, bfmCheckBox);
                }
                else if (MyComment.m_iUserRoleId == (int)appUsersRoles.userRole.Economics_Support)
                {
                    econsStandLabel.Text = oProposalMgt.MyStand(MyComment.m_iStand);
                    econsCommentLabel.Text = MyComment.m_sComments;
                    econsDateReceived.Text = MyComment.m_sDateReceived;
                    econsDateReviewedLabel.Text = MyComment.m_sDateComment;
                    ecoHF.Value = oAppUsers.m_iUserId.ToString();
                    responsibleEcoLabel.Text = oAppUsers.m_sFullName;
                    oProposalMgt.SupportStatusCode(MyComment.m_iStand, econsStandLabel, ecoCheckBox);
                }
                else if (MyComment.m_iUserRoleId == (int)appUsersRoles.userRole.HSE_Support)
                {
                    hseStandLabel.Text = oProposalMgt.MyStand(MyComment.m_iStand);
                    hseCommentLabel.Text = MyComment.m_sComments;
                    hseDateReceived.Text = MyComment.m_sDateReceived;
                    hseDateReviewedLabel.Text = MyComment.m_sDateComment;
                    hseHF.Value = oAppUsers.m_iUserId.ToString();
                    responsibleHSELabel.Text = oAppUsers.m_sFullName;
                    oProposalMgt.SupportStatusCode(MyComment.m_iStand, hseStandLabel, hseCheckBox);
                }
                else if (MyComment.m_iUserRoleId == (int)appUsersRoles.userRole.LEGAL_Support)
                {
                    legalStandLabel.Text = oProposalMgt.MyStand(MyComment.m_iStand);
                    legalCommentLabel.Text = MyComment.m_sComments;
                    legalDateReceived.Text = MyComment.m_sDateReceived;
                    legalDateReviewedLabel.Text = MyComment.m_sDateComment;
                    legalHF.Value = oAppUsers.m_iUserId.ToString();
                    responsibleLegalLabel.Text = oAppUsers.m_sFullName;
                    oProposalMgt.SupportStatusCode(MyComment.m_iStand, legalStandLabel, legalCheckBox);
                }
                else if (MyComment.m_iUserRoleId == (int)appUsersRoles.userRole.Security_Support)
                {
                    securityStandLabel.Text = oProposalMgt.MyStand(MyComment.m_iStand);
                    securityCommentLabel.Text = MyComment.m_sComments;
                    securityDateReceived.Text = MyComment.m_sDateReceived;
                    securityDateReviewedLabel.Text = MyComment.m_sDateComment;
                    secHF.Value = oAppUsers.m_iUserId.ToString();
                    responsibleSecLabel.Text = oAppUsers.m_sFullName;
                    oProposalMgt.SupportStatusCode(MyComment.m_iStand, securityStandLabel, secCheckBox);
                }
                else if (MyComment.m_iUserRoleId == (int)appUsersRoles.userRole.SPCA_Support)
                {
                    scdStandLabel.Text = oProposalMgt.MyStand(MyComment.m_iStand);
                    scdCommentLabel.Text = MyComment.m_sComments;
                    scdDateReceived.Text = MyComment.m_sDateReceived;
                    scdDateReviewedLabel.Text = MyComment.m_sDateComment;
                    spcaHF.Value = oAppUsers.m_iUserId.ToString();
                    responsibleSPCALabel.Text = oAppUsers.m_sFullName;
                    oProposalMgt.SupportStatusCode(MyComment.m_iStand, scdStandLabel, spcaCheckBox);
                }
                else if (MyComment.m_iUserRoleId == (int)appUsersRoles.userRole.TAX_Support)
                {
                    taxStandLabel.Text = oProposalMgt.MyStand(MyComment.m_iStand);
                    taxCommentLabel.Text = MyComment.m_sComments;
                    taxDateReceived.Text = MyComment.m_sDateReceived;
                    taxDateReviewedLabel.Text = MyComment.m_sDateComment;
                    taxHF.Value = oAppUsers.m_iUserId.ToString();
                    responsibleTAXLabel.Text = oAppUsers.m_sFullName;
                    oProposalMgt.SupportStatusCode(MyComment.m_iStand, taxStandLabel, taxCheckBox);
                }
                else if (MyComment.m_iUserRoleId == (int)appUsersRoles.userRole.Treasury_Support)
                {
                    treasuryStandLabel.Text = oProposalMgt.MyStand(MyComment.m_iStand);
                    treasuryCommentLabel.Text = MyComment.m_sComments;
                    treasuryDateReceived.Text = MyComment.m_sDateReceived;
                    treasuryDateReviewedLabel.Text = MyComment.m_sDateComment;
                    treasuryHF.Value = oAppUsers.m_iUserId.ToString();
                    responsibleTreasuryLabel.Text = oAppUsers.m_sFullName;
                    oProposalMgt.SupportStatusCode(MyComment.m_iStand, treasuryStandLabel, treasuryCheckBox);
                }
                else if (MyComment.m_iUserRoleId == (int)appUsersRoles.userRole.IT)
                {
                    ITStandLabel.Text = oProposalMgt.MyStand(MyComment.m_iStand);
                    ITCommentLabel.Text = MyComment.m_sComments;
                    ITDateReceived.Text = MyComment.m_sDateReceived;
                    ITDateReviewedLabel.Text = MyComment.m_sDateComment;
                    ITHF.Value = oAppUsers.m_iUserId.ToString();
                    responsibleITLabel.Text = oAppUsers.m_sFullName;
                    oProposalMgt.SupportStatusCode(MyComment.m_iStand, ITStandLabel, ITCheckBox);
                }
                else if (MyComment.m_iUserRoleId == (int)appUsersRoles.userRole.SCM)
                {
                    SCMStandLabel.Text = oProposalMgt.MyStand(MyComment.m_iStand);
                    SCMCommentLabel.Text = MyComment.m_sComments;
                    SCMDateReceived.Text = MyComment.m_sDateReceived;
                    scdDateReviewedLabel.Text = MyComment.m_sDateComment;
                    scmHF.Value = oAppUsers.m_iUserId.ToString();
                    responsibleSCMLabel.Text = oAppUsers.m_sFullName;
                    oProposalMgt.SupportStatusCode(MyComment.m_iStand, SCMStandLabel, scmCheckBox);
                }
                else if (MyComment.m_iUserRoleId == (int)appUsersRoles.userRole.Finance_Signature) //TODO: (CRITICAL)
                {
                    financeStandLabel.Text = oProposalMgt.MyStand(MyComment.m_iStand);
                    financeCommentLabel.Text = MyComment.m_sComments;
                    financeDateReceived.Text = MyComment.m_sDateReceived;
                    financeDateReviewedLabel.Text = MyComment.m_sDateComment;
                    finHF.Value = oAppUsers.m_iUserId.ToString();
                    responsibleFinLabel.Text = oAppUsers.m_sFullName;
                    oProposalMgt.SupportStatusCode(MyComment.m_iStand, financeStandLabel, finCheckBox);
                }
                else if ((MyComment.m_iUserRoleId == (int)appUsersRoles.userRole.Finance_Director) && (oAppUsers.eFunction.m_sFunction == cpdmsFunctionsNames.Finance)) //TODO: GM Finance == Finance Director (Now) (CRITICAL)
                {
                    GMfinanceStandLabel.Text = oProposalMgt.MyStand(MyComment.m_iStand);
                    GMfinanceCommentLabel.Text = MyComment.m_sComments;
                    GMfinanceDateReceived.Text = MyComment.m_sDateReceived;
                    GMfinanceDateReviewedLabel.Text = MyComment.m_sDateComment;
                    GMfinHF.Value = oAppUsers.m_iUserId.ToString();
                    responsibleGMFinLabel.Text = oAppUsers.m_sFullName;
                    oProposalMgt.SupportStatusCode(MyComment.m_iStand, GMfinanceStandLabel, GMfinCheckBox);
                }
                else if (MyComment.m_iUserRoleId == (int)appUsersRoles.userRole.Technical_Planning_Manager) //TODO: Please update the database for this role. (CRITICAL)
                {
                    CPMStandLabel.Text = oProposalMgt.MyStand(MyComment.m_iStand);
                    CPMCommentLabel.Text = MyComment.m_sComments;
                    CPMDateReceived.Text = MyComment.m_sDateReceived;
                    CPMDateReviewedLabel.Text = MyComment.m_sDateComment;
                    cpmHF.Value = oAppUsers.m_iUserId.ToString();
                    responsibleCPMLabel.Text = oAppUsers.m_sFullName;
                    oProposalMgt.SupportStatusCode(MyComment.m_iStand, CPMStandLabel, CPMCheckBox);
                }
                else if ((MyComment.m_iUserRoleId == (int)appUsersRoles.userRole.VP) && (oAppUsers.eFunction.m_sFunction == cpdmsFunctionsNames.Finance)) //TODO: VP Finance == GM Finance (Now) (CRITICAL)
                {
                    VPfinanceStandLabel.Text = oProposalMgt.MyStand(MyComment.m_iStand);
                    VPfinanceCommentLabel.Text = MyComment.m_sComments;
                    VPfinanceDateReceived.Text = MyComment.m_sDateReceived;
                    VPfinanceDateReviewedLabel.Text = MyComment.m_sDateComment;
                    VPfinHF.Value = oAppUsers.m_iUserId.ToString();
                    responsibleVPFinLabel.Text = oAppUsers.m_sFullName;
                    oProposalMgt.SupportStatusCode(MyComment.m_iStand, VPfinanceStandLabel, VPfinCheckBox);
                }
                //else if (MyComment.m_iUserRoleId == (int)appUsersRoles.userRole.MD) //TODO: MD (CRITICAL) == GM Now
                //{
                //    directorStandLabel.Text = oProposalMgt.MyStand(MyComment.m_iStand);
                //    directorCommentLabel.Text = MyComment.m_sComments;
                //    directorDateReceived.Text = MyComment.m_sDateReceived;
                //    directorDateReviewedLabel.Text = MyComment.m_sDateComment;
                //    mdHF.Value = oAppUsers.m_iUserId.ToString();
                //    responsibleMDLabel.Text = oAppUsers.m_sFullName;
                //    oProposalMgt.SupportStatusCode(MyComment.m_iStand, directorStandLabel, mdCheckBox);
                //}
                else if (MyComment.m_iUserRoleId == (int)appUsersRoles.userRole.GM_Regional_Planning)
                {
                    GMREPlanStandLabel.Text = oProposalMgt.MyStand(MyComment.m_iStand);
                    GMREPlanCommentLabel.Text = MyComment.m_sComments;
                    GMREPlanDateReceived.Text = MyComment.m_sDateReceived;
                    GMREPlanDateReviewedLabel.Text = MyComment.m_sDateComment;
                    gmREHF.Value = oAppUsers.m_iUserId.ToString();
                    responsibleGMRELabel.Text = oAppUsers.m_sFullName;
                    oProposalMgt.SupportStatusCode(MyComment.m_iStand, GMREPlanStandLabel, gmreplanCheckBox);
                }
                else if (MyComment.m_iUserRoleId == (int)appUsersRoles.userRole.VP)
                {
                    approvalStandLabel.Text = oProposalMgt.MyStand(MyComment.m_iStand);
                    approvalCommentLabel.Text = MyComment.m_sComments;
                    approvalDateReceived.Text = MyComment.m_sDateReceived;
                    approvalDateReviewedLabel.Text = MyComment.m_sDateComment;
                    vpHF.Value = oAppUsers.m_iUserId.ToString();
                    responsibleVPLabel.Text = oAppUsers.m_sFullName;
                    oProposalMgt.SupportStatusCode(MyComment.m_iStand, approvalStandLabel, vpCheckBox);
                }
                else if (MyComment.m_iUserRoleId == (int)appUsersRoles.userRole.REVP)
                {
                    revpStandLabel.Text = oProposalMgt.MyStand(MyComment.m_iStand);
                    revpCommentLabel.Text = MyComment.m_sComments;
                    revpDateReceived.Text = MyComment.m_sDateReceived;
                    revpDateReviewedLabel.Text = MyComment.m_sDateComment;
                    revpHF.Value = oAppUsers.m_iUserId.ToString();
                    responsibleREVPLabel.Text = oAppUsers.m_sFullName;
                    oProposalMgt.SupportStatusCode(MyComment.m_iStand, revpStandLabel, revpCheckBox);
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    private void GetApproverComments(long lProposalId)
    {
        appUsers oAppUsers = new appUsers();
        appUserMgt oAppUserMgt = new appUserMgt();
        SupportApproverCommentMgt functionalComments = new SupportApproverCommentMgt();

        try
        {
            List<SupportApproverComments> oComments = functionalComments.lstGetFunctionalSupportsApproverComments(lProposalId);

            //Get all functional Support who have received the IP
            foreach (SupportApproverComments oComment in oComments)
            {
                //Check the Individual comments, by ProposalID and UserID
                SupportApproverComments MyComment = functionalComments.objGetFunctionalSupportsApproverCommentByUserId(lProposalId, oComment.m_iUserId);
                oAppUsers = oAppUserMgt.objGetUserByUserId(MyComment.m_iUserId);

                if (MyComment.m_iUserRoleId == (int)appUsersRoles.userRole.Business_Process_Owner)
                {
                    if (MyComment.m_iStand == SupportState.iBPOStandDefault)
                    {
                        planStandLabel.Text = "BPO's action awaited.";
                    }
                    else
                    {
                        planStandLabel.Text = oProposalMgt.MyStand(MyComment.m_iStand);
                    }
                    cplanCheckBox.Enabled = false;
                    planCommentLabel.Text = MyComment.m_sComments;
                    planDateReceived.Text = MyComment.m_sDateReceived;
                    planDateReviewedLabel.Text = MyComment.m_sDateComment;
                    cplanHF.Value = oAppUsers.m_iUserId.ToString();
                    responsibleCPLANLabel.Text = oAppUsers.m_sFullName;
                    oProposalMgt.SupportStatusCode(MyComment.m_iStand, planStandLabel, cplanCheckBox);
                }
                else if (MyComment.m_iUserRoleId == (int)appUsersRoles.userRole.Line_Team_Lead)
                {
                    managerStandLabel.Text = oProposalMgt.MyStand(MyComment.m_iStand);
                    managerCommentLabel.Text = MyComment.m_sComments;
                    managerDateReceived.Text = MyComment.m_sDateReceived;
                    managerDateReviewedLabel.Text = MyComment.m_sDateComment;
                    ltleadHF.Value = oAppUsers.m_iUserId.ToString();
                    responsibleTeamLeadLabel.Text = oAppUsers.m_sFullName;
                    oProposalMgt.SupportStatusCode(MyComment.m_iStand, managerStandLabel, ltleadCheckBox);
                }
                else if (MyComment.m_iUserRoleId == (int)appUsersRoles.userRole.Finance_Signature) //TODO: (CRITICAL)
                {
                    financeStandLabel.Text = oProposalMgt.MyStand(MyComment.m_iStand);
                    financeCommentLabel.Text = MyComment.m_sComments;
                    financeDateReceived.Text = MyComment.m_sDateReceived;
                    financeDateReviewedLabel.Text = MyComment.m_sDateComment;
                    finHF.Value = oAppUsers.m_iUserId.ToString();
                    responsibleFinLabel.Text = oAppUsers.m_sFullName;
                    oProposalMgt.SupportStatusCode(MyComment.m_iStand, financeStandLabel, finCheckBox);
                }
                else if ((MyComment.m_iUserRoleId == (int)appUsersRoles.userRole.Finance_Director) && (oAppUsers.eFunction.m_sFunction == cpdmsFunctionsNames.Finance)) //TODO: GM Finance == Finance Director (Now) (CRITICAL)
                {
                    GMfinanceStandLabel.Text = oProposalMgt.MyStand(MyComment.m_iStand);
                    GMfinanceCommentLabel.Text = MyComment.m_sComments;
                    GMfinanceDateReceived.Text = MyComment.m_sDateReceived;
                    GMfinanceDateReviewedLabel.Text = MyComment.m_sDateComment;
                    GMfinHF.Value = oAppUsers.m_iUserId.ToString();
                    responsibleGMFinLabel.Text = oAppUsers.m_sFullName;
                    oProposalMgt.SupportStatusCode(MyComment.m_iStand, GMfinanceStandLabel, GMfinCheckBox);
                }
                else if (MyComment.m_iUserRoleId == (int)appUsersRoles.userRole.Technical_Planning_Manager) //TODO: Please update the database for this role. (CRITICAL)
                {
                    CPMStandLabel.Text = oProposalMgt.MyStand(MyComment.m_iStand);
                    CPMCommentLabel.Text = MyComment.m_sComments;
                    CPMDateReceived.Text = MyComment.m_sDateReceived;
                    CPMDateReviewedLabel.Text = MyComment.m_sDateComment;
                    cpmHF.Value = oAppUsers.m_iUserId.ToString();
                    responsibleCPMLabel.Text = oAppUsers.m_sFullName;
                    oProposalMgt.SupportStatusCode(MyComment.m_iStand, CPMStandLabel, CPMCheckBox);
                }
                else if ((MyComment.m_iUserRoleId == (int)appUsersRoles.userRole.VP) && (oAppUsers.eFunction.m_sFunction == cpdmsFunctionsNames.Finance)) //TODO: VP Finance == GM Finance (Now) (CRITICAL)
                {
                    VPfinanceStandLabel.Text = oProposalMgt.MyStand(MyComment.m_iStand);
                    VPfinanceCommentLabel.Text = MyComment.m_sComments;
                    VPfinanceDateReceived.Text = MyComment.m_sDateReceived;
                    VPfinanceDateReviewedLabel.Text = MyComment.m_sDateComment;
                    VPfinHF.Value = oAppUsers.m_iUserId.ToString();
                    responsibleVPFinLabel.Text = oAppUsers.m_sFullName;
                    oProposalMgt.SupportStatusCode(MyComment.m_iStand, VPfinanceStandLabel, VPfinCheckBox);
                }
                //else if (MyComment.m_iUserRoleId == (int)appUsersRoles.userRole.MD) //TODO: MD (CRITICAL) == GM Now
                //{
                //    directorStandLabel.Text = oProposalMgt.MyStand(MyComment.m_iStand);
                //    directorCommentLabel.Text = MyComment.m_sComments;
                //    directorDateReceived.Text = MyComment.m_sDateReceived;
                //    directorDateReviewedLabel.Text = MyComment.m_sDateComment;
                //    mdHF.Value = oAppUsers.m_iUserId.ToString();
                //    responsibleMDLabel.Text = oAppUsers.m_sFullName;
                //    oProposalMgt.SupportStatusCode(MyComment.m_iStand, directorStandLabel, mdCheckBox);
                //}
                else if (MyComment.m_iUserRoleId == (int)appUsersRoles.userRole.GM_Regional_Planning)
                {
                    GMREPlanStandLabel.Text = oProposalMgt.MyStand(MyComment.m_iStand);
                    GMREPlanCommentLabel.Text = MyComment.m_sComments;
                    GMREPlanDateReceived.Text = MyComment.m_sDateReceived;
                    GMREPlanDateReviewedLabel.Text = MyComment.m_sDateComment;
                    gmREHF.Value = oAppUsers.m_iUserId.ToString();
                    responsibleGMRELabel.Text = oAppUsers.m_sFullName;
                    oProposalMgt.SupportStatusCode(MyComment.m_iStand, GMREPlanStandLabel, gmreplanCheckBox);
                }
                else if (MyComment.m_iUserRoleId == (int)appUsersRoles.userRole.CERP)
                {
                    CERPStandLabel.Text = oProposalMgt.MyStand(MyComment.m_iStand);
                    CERPCommentLabel.Text = MyComment.m_sComments;
                    CERPDateReceived.Text = MyComment.m_sDateReceived;
                    CERPDateReviewedLabel.Text = MyComment.m_sDateComment;
                    CerpHF.Value = oAppUsers.m_iUserId.ToString();
                    responsibleCERPLabel.Text = oAppUsers.m_sFullName;
                    oProposalMgt.SupportStatusCode(MyComment.m_iStand, CERPStandLabel, cerpCheckBox);
                }
                else if (MyComment.m_iUserRoleId == (int)appUsersRoles.userRole.VP)
                {
                    approvalStandLabel.Text = oProposalMgt.MyStand(MyComment.m_iStand);
                    approvalCommentLabel.Text = MyComment.m_sComments;
                    approvalDateReceived.Text = MyComment.m_sDateReceived;
                    approvalDateReviewedLabel.Text = MyComment.m_sDateComment;
                    vpHF.Value = oAppUsers.m_iUserId.ToString();
                    responsibleVPLabel.Text = oAppUsers.m_sFullName;
                    oProposalMgt.SupportStatusCode(MyComment.m_iStand, approvalStandLabel, vpCheckBox);
                }
                else if (MyComment.m_iUserRoleId == (int)appUsersRoles.userRole.REVP)
                {
                    revpStandLabel.Text = oProposalMgt.MyStand(MyComment.m_iStand);
                    revpCommentLabel.Text = MyComment.m_sComments;
                    revpDateReceived.Text = MyComment.m_sDateReceived;
                    revpDateReviewedLabel.Text = MyComment.m_sDateComment;
                    revpHF.Value = oAppUsers.m_iUserId.ToString();
                    responsibleREVPLabel.Text = oAppUsers.m_sFullName;
                    oProposalMgt.SupportStatusCode(MyComment.m_iStand, revpStandLabel, revpCheckBox);
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }


    private void OnOffCheckBoxes()
    {
        if ((oSessnx.getOnlineUser.m_iUserRoleId == (int) appUsersRoles.userRole.Business_Process_Owner)
            || (oSessnx.getOnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.GM_Regional_Planning)
            || (oSessnx.getOnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.Administrator))
        {
            bfmCheckBox.Visible = true; cplanCheckBox.Visible = true; ecoCheckBox.Visible = true;
            hseCheckBox.Visible = true; legalCheckBox.Visible = true; secCheckBox.Visible = true;
            spcaCheckBox.Visible = true; taxCheckBox.Visible = true; treasuryCheckBox.Visible = true;
            ltleadCheckBox.Visible = true; finCheckBox.Visible = true; cerpCheckBox.Visible = true; //mdCheckBox.Visible = true;
            gmreplanCheckBox.Visible = true; vpCheckBox.Visible = true; revpCheckBox.Visible = true;
            forwardReminderButton.Visible = true; GMfinCheckBox.Visible = true; CPMCheckBox.Visible = true;
            VPfinCheckBox.Visible = true; ITCheckBox.Visible = true; scmCheckBox.Visible = true;

        }
        else
        {
            bfmCheckBox.Visible = false; cplanCheckBox.Visible = false; ecoCheckBox.Visible = false;
            hseCheckBox.Visible = false; legalCheckBox.Visible = false; secCheckBox.Visible = false;
            spcaCheckBox.Visible = false; taxCheckBox.Visible = false; treasuryCheckBox.Visible = false;
            ltleadCheckBox.Visible = false; finCheckBox.Visible = false; cerpCheckBox.Visible = false;//mdCheckBox.Visible = false;
            gmreplanCheckBox.Visible = false; vpCheckBox.Visible = false; revpCheckBox.Visible = false;
            forwardReminderButton.Visible = false; GMfinCheckBox.Visible = false; CPMCheckBox.Visible = false;
            VPfinCheckBox.Visible = false; ITCheckBox.Visible = false; scmCheckBox.Visible = false;
        }
    }

    private void DisableAllCheckBoxes()
    {
        bfmCheckBox.Enabled = false; cplanCheckBox.Enabled = false; ecoCheckBox.Enabled = false;
        hseCheckBox.Enabled = false; legalCheckBox.Enabled = false; secCheckBox.Enabled = false;
        spcaCheckBox.Enabled = false; taxCheckBox.Enabled = false; treasuryCheckBox.Enabled = false;
        ltleadCheckBox.Enabled = false; finCheckBox.Enabled = false; cerpCheckBox.Enabled = false; //mdCheckBox.Enabled = false;
        gmreplanCheckBox.Enabled = false; vpCheckBox.Enabled = false; revpCheckBox.Enabled = false;
        GMfinCheckBox.Enabled = false; CPMCheckBox.Enabled = false; VPfinCheckBox.Enabled = false;
        ITCheckBox.Enabled = false; scmCheckBox.Enabled = false;
    }

    //protected void VPDetailedSupportLinkButton_Click(object sender, EventArgs e)
    //{
    //    Proposal MyProposal = oProposalMgt.objGetProposalById(int.Parse(Request.QueryString["Proposalid"].ToString()));
    //    Response.Redirect("~/ApprovalSupportFunction/VPSupportDetails.aspx?Proposalid=" + MyProposal.m_lProposalId + "");
    //}

    //protected void GMDetailedSupportLinkButton_Click(object sender, EventArgs e)
    //{
    //    Proposal MyProposal = oProposalMgt.objGetProposalById(int.Parse(Request.QueryString["Proposalid"].ToString()));
    //    Response.Redirect("~/ApprovalSupportFunction/GMSupportDetails.aspx?Proposalid=" + MyProposal.m_lProposalId + "");
    //}
}