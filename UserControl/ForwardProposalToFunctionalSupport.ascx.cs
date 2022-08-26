using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;

public partial class UserControl_ForwardProposalToFunctionalSupport : aspnetUserControl
{
    ProposalMgt oProposalMgt = new ProposalMgt();
    appUserMgt oAppUserMgt = new appUserMgt();
    ProposalApprovalDetailsMgt oProposalApprovalDetailsMgt = new ProposalApprovalDetailsMgt();
    SupportApprovalStatus SupportApproval = new SupportApprovalStatus();
    //FinanceSignatureApproval MyFinanceSignature = new FinanceSignatureApproval();
    SupportApproverCommentMgt oSupportApproverCommentMgt = new SupportApproverCommentMgt();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void Init_Page(long lProposalId)
    {
        //Check if the Business Process Owner has supported the IP. If Supported then enable the controls on this page.
        List<ProposalApprovalDetails> oProposalApprovalDetails = oProposalApprovalDetailsMgt.lstGetProposalSupportDetailsByProposalId(lProposalId);
        foreach (ProposalApprovalDetails oProposalApprovalDetail in oProposalApprovalDetails)
        {
            if (oProposalApprovalDetail.m_iUserRoleId == (int)appUsersRoles.userRole.Business_Process_Owner)
            {
                if (oProposalApprovalDetail.m_iStand == SupportState.iSupported)
                {
                    Proposal oProposal = oProposalMgt.objGetProposalById(lProposalId);
                    proposalIDHiddenField.Value = lProposalId.ToString();

                    LoadFunctionalSupports(lProposalId);
                    DisplaySupportStatus(lProposalId);

                    bool HaveAllMandatoryRequiredSupportSupported = oSupportApproverCommentMgt.MandatoryFuntionalSupportStand(oProposal);
                    if (HaveAllMandatoryRequiredSupportSupported == true)
                    {
                        forwardButton.Enabled = false; //disable this button to stop sending mails to Support Functions
                        reviewAgainButton.Enabled = false;
                    }
                }
                else
                {
                    forwardButton.Enabled = false;
                    reviewAgainButton.Enabled = false;
                }
            } 
        }       
    }

    private void LoadFunctionalSupports(long lProposalId)
    {
        //Clear the contents of each dropdown before refilling
        hseDropDownList.Items.Clear(); controllersDropDownList.Items.Clear(); taxDropDownList.Items.Clear();
        legalDropDownList.Items.Clear(); tressuryDropDownList.Items.Clear(); ecoDropDownList.Items.Clear();
        secDropDownList.Items.Clear(); spcaDropDownList.Items.Clear(); ITDropDownList.Items.Clear();
        scmDropDownList.Items.Clear();

        Proposal oProposal = oProposalMgt.objGetProposalById(lProposalId);

        //Mandatory Functional Supports
        List<appUsers> LegalSupports = oProposalMgt.GetRequiredSupportApprovers(oProposal, (int)appUsersRoles.userRole.LEGAL_Support);
        legalDropDownList.Items.Add(new ListItem("None", "-1"));
        foreach (appUsers LegalSupport in LegalSupports)
        {
            legalDropDownList.Items.Add(new ListItem(LegalSupport.m_sFullName, LegalSupport.m_iUserId.ToString()));
        }

        List<appUsers> TaxSupports = oProposalMgt.GetRequiredSupportApprovers(oProposal, (int)appUsersRoles.userRole.TAX_Support);
        taxDropDownList.Items.Add(new ListItem("None", "-1"));
        foreach (appUsers TaxSupport in TaxSupports)
        {
            taxDropDownList.Items.Add(new ListItem(TaxSupport.m_sFullName, TaxSupport.m_iUserId.ToString()));
        }

        List<appUsers> ControllerSupports = oProposalMgt.GetRequiredSupportApprovers(oProposal, (int)appUsersRoles.userRole.Controllers);
        controllersDropDownList.Items.Add(new ListItem("None", "-1"));
        foreach (appUsers ControllerSupport in ControllerSupports)
        {
            controllersDropDownList.Items.Add(new ListItem(ControllerSupport.m_sFullName, ControllerSupport.m_iUserId.ToString()));
        }

        List<appUsers> TreasuryFunctionalSupports = oProposalMgt.GetRequiredSupportApprovers(oProposal, (int)appUsersRoles.userRole.Treasury_Support);
        tressuryDropDownList.Items.Add(new ListItem("None", "-1"));
        foreach (appUsers TreasuryFunctionalSupport in TreasuryFunctionalSupports)
        {
            tressuryDropDownList.Items.Add(new ListItem(TreasuryFunctionalSupport.m_sFullName, TreasuryFunctionalSupport.m_iUserId.ToString()));
        }

        List<appUsers> EconomicsFunctionalSupports = oProposalMgt.GetRequiredSupportApprovers(oProposal, (int)appUsersRoles.userRole.Economics_Support);
        ecoDropDownList.Items.Add(new ListItem("None", "-1"));
        foreach (appUsers EconomicsFunctionalSupport in EconomicsFunctionalSupports)
        {
            ecoDropDownList.Items.Add(new ListItem(EconomicsFunctionalSupport.m_sFullName, EconomicsFunctionalSupport.m_iUserId.ToString()));
        }


        //Non Mandatory Functional Supports
        List<appUsers> HSEFunctionalSupports = oProposalMgt.GetRequiredSupportApprovers(oProposal, (int)appUsersRoles.userRole.HSE_Support);
        //List<appUsers> HSEFunctionalSupports = oProposalMgt.lstGetSupportApproverByRole((int)appUsersRoles.userRole.HSE_Support);
        hseDropDownList.Items.Add(new ListItem("None", "-1"));
        foreach (appUsers HSEFunctionalSupport in HSEFunctionalSupports)
        {
            hseDropDownList.Items.Add(new ListItem(HSEFunctionalSupport.m_sFullName, HSEFunctionalSupport.m_iUserId.ToString()));
        }

        List<appUsers> SecurityFunctionalSupports = oProposalMgt.GetRequiredSupportApprovers(oProposal, (int)appUsersRoles.userRole.Security_Support);
        secDropDownList.Items.Add(new ListItem("None", "-1"));
        foreach (appUsers SecurityFunctionalSupport in SecurityFunctionalSupports)
        {
            secDropDownList.Items.Add(new ListItem(SecurityFunctionalSupport.m_sFullName, SecurityFunctionalSupport.m_iUserId.ToString()));
        }

        List<appUsers> SPCAFunctionalSupports = oProposalMgt.GetRequiredSupportApprovers(oProposal, (int)appUsersRoles.userRole.SPCA_Support);
        //List<appUsers> SPCAFunctionalSupports = oProposalMgt.lstGetSupportApproverByRole((int)appUsersRoles.userRole.SPCA_Support);
        spcaDropDownList.Items.Add(new ListItem("None", "-1"));
        foreach (appUsers SPCAFunctionalSupport in SPCAFunctionalSupports)
        {
            spcaDropDownList.Items.Add(new ListItem(SPCAFunctionalSupport.m_sFullName, SPCAFunctionalSupport.m_iUserId.ToString()));
        }

        List<appUsers> ITFunctionalSupports = oProposalMgt.GetRequiredSupportApprovers(oProposal, (int)appUsersRoles.userRole.IT);
        //List<appUsers> ITFunctionalSupports = oProposalMgt.lstGetSupportApproverByRole((int)appUsersRoles.userRole.IT);
        ITDropDownList.Items.Add(new ListItem("None", "-1"));
        foreach (appUsers ITFunctionalSupport in ITFunctionalSupports)
        {
            ITDropDownList.Items.Add(new ListItem(ITFunctionalSupport.m_sFullName, ITFunctionalSupport.m_iUserId.ToString()));
        }

        List<appUsers> SCMFunctionalSupports = oProposalMgt.GetRequiredSupportApprovers(oProposal, (int)appUsersRoles.userRole.SCM);
        //List<appUsers> SCMFunctionalSupports = oProposalMgt.lstGetSupportApproverByRole((int)appUsersRoles.userRole.SCM);
        scmDropDownList.Items.Add(new ListItem("None", "-1"));
        foreach (appUsers SCMFunctionalSupport in SCMFunctionalSupports)
        {
            scmDropDownList.Items.Add(new ListItem(SCMFunctionalSupport.m_sFullName, SCMFunctionalSupport.m_iUserId.ToString()));
        }
    }

    private void DisplaySupportStatus(long lProposalId)
    {
        secSupportLabel.Text = ""; hseSupportLabel.Text = ""; spcaSupportLabel.Text = ""; taxSupportLabel.Text = "";
        treasurySupportLabel.Text = ""; legalSupportLabel.Text = ""; ControllerSupportLabel.Text = ""; ecoSupportLabel.Text = "";

        ProposalApprovalDetailsMgt oProposalApprovalDetailsMgt = new ProposalApprovalDetailsMgt();
        List<ProposalApprovalDetails> oProposalApprovalDetails = oProposalApprovalDetailsMgt.lstGetProposalSupportDetailsByProposalId(lProposalId);

        foreach (ProposalApprovalDetails oProposalApprovalDetail in oProposalApprovalDetails)
        {
            //Check the Individual comments, by ProposalID and UserID
            if (oProposalApprovalDetail.m_iUserRoleId == (int)appUsersRoles.userRole.HSE_Support)
            {
                hseSupportLabel.Text = MyStand(oProposalApprovalDetail.m_iStand);
                SupportStatusColor(hseSupportLabel);
                SupportStatus(hseSupportLabel, hseCheckBox, oProposalApprovalDetail.m_iStand, lProposalId);
                hseDropDownList.SelectedValue = oProposalApprovalDetail.m_iUserId.ToString();
                hseIDHF.Value = oProposalApprovalDetail.m_iUserId.ToString();
            }
            else if (oProposalApprovalDetail.m_iUserRoleId == (int)appUsersRoles.userRole.SPCA_Support)
            {
                spcaSupportLabel.Text = MyStand(oProposalApprovalDetail.m_iStand);
                SupportStatusColor(spcaSupportLabel);
                SupportStatus(spcaSupportLabel, spcaCheckBox, oProposalApprovalDetail.m_iStand, lProposalId);
                spcaDropDownList.SelectedValue = oProposalApprovalDetail.m_iUserId.ToString();
                spcaIDHF.Value = oProposalApprovalDetail.m_iUserId.ToString();
            }
            else if (oProposalApprovalDetail.m_iUserRoleId == (int)appUsersRoles.userRole.Controllers)
            {
                ControllerSupportLabel.Text = MyStand(oProposalApprovalDetail.m_iStand);
                SupportStatusColor(ControllerSupportLabel);
                SupportStatus(ControllerSupportLabel, controllerCheckBox, oProposalApprovalDetail.m_iStand, lProposalId);
                controllersDropDownList.SelectedValue = oProposalApprovalDetail.m_iUserId.ToString();
                bfmIDHF.Value = oProposalApprovalDetail.m_iUserId.ToString();
            }
            else if (oProposalApprovalDetail.m_iUserRoleId == (int)appUsersRoles.userRole.TAX_Support)
            {
                taxSupportLabel.Text = MyStand(oProposalApprovalDetail.m_iStand);
                SupportStatusColor(taxSupportLabel);
                SupportStatus(taxSupportLabel, taxCheckBox, oProposalApprovalDetail.m_iStand, lProposalId);
                taxDropDownList.SelectedValue = oProposalApprovalDetail.m_iUserId.ToString();
                taxIDHF.Value = oProposalApprovalDetail.m_iUserId.ToString();
            }
            else if (oProposalApprovalDetail.m_iUserRoleId == (int)appUsersRoles.userRole.Economics_Support)
            {
                ecoSupportLabel.Text = MyStand(oProposalApprovalDetail.m_iStand);
                SupportStatusColor(ecoSupportLabel);
                SupportStatus(ecoSupportLabel, econsCheckBox, oProposalApprovalDetail.m_iStand, lProposalId);
                ecoDropDownList.SelectedValue = oProposalApprovalDetail.m_iUserId.ToString();
                ecoIDHF.Value = oProposalApprovalDetail.m_iUserId.ToString();
            }
            else if (oProposalApprovalDetail.m_iUserRoleId == (int)appUsersRoles.userRole.LEGAL_Support)
            {
                legalSupportLabel.Text = MyStand(oProposalApprovalDetail.m_iStand);
                SupportStatusColor(legalSupportLabel);
                SupportStatus(legalSupportLabel, legalCheckBox, oProposalApprovalDetail.m_iStand, lProposalId);
                legalDropDownList.SelectedValue = oProposalApprovalDetail.m_iUserId.ToString();
                legalIDHF.Value = oProposalApprovalDetail.m_iUserId.ToString();
            }
            else if (oProposalApprovalDetail.m_iUserRoleId == (int)appUsersRoles.userRole.Treasury_Support)
            {
                treasurySupportLabel.Text = MyStand(oProposalApprovalDetail.m_iStand);
                SupportStatusColor(treasurySupportLabel);
                SupportStatus(treasurySupportLabel, treasuryCheckBox, oProposalApprovalDetail.m_iStand, lProposalId);
                tressuryDropDownList.SelectedValue = oProposalApprovalDetail.m_iUserId.ToString();
                treasuryIDHF.Value = oProposalApprovalDetail.m_iUserId.ToString();
            }
            else if (oProposalApprovalDetail.m_iUserRoleId == (int)appUsersRoles.userRole.Security_Support)
            {
                secSupportLabel.Text = MyStand(oProposalApprovalDetail.m_iStand);
                SupportStatusColor(secSupportLabel);
                SupportStatus(secSupportLabel, securityCheckBox, oProposalApprovalDetail.m_iStand, lProposalId);
                secDropDownList.SelectedValue = oProposalApprovalDetail.m_iUserId.ToString();
                secIDHF.Value = oProposalApprovalDetail.m_iUserId.ToString();
            }
            else if (oProposalApprovalDetail.m_iUserRoleId == (int)appUsersRoles.userRole.IT)
            {
                ITSupportLabel.Text = MyStand(oProposalApprovalDetail.m_iStand);
                SupportStatusColor(ITSupportLabel);
                SupportStatus(ITSupportLabel, ITCheckBox, oProposalApprovalDetail.m_iStand, lProposalId);
                ITDropDownList.SelectedValue = oProposalApprovalDetail.m_iUserId.ToString();
                ITIDHF.Value = oProposalApprovalDetail.m_iUserId.ToString();
            }
            else if (oProposalApprovalDetail.m_iUserRoleId == (int)appUsersRoles.userRole.SCM)
            {
                scmSupportLabel.Text = MyStand(oProposalApprovalDetail.m_iStand);
                SupportStatusColor(scmSupportLabel);
                SupportStatus(scmSupportLabel, scmCheckBox, oProposalApprovalDetail.m_iStand, lProposalId);
                scmDropDownList.SelectedValue = oProposalApprovalDetail.m_iUserId.ToString();
                scmIDHF.Value = oProposalApprovalDetail.m_iUserId.ToString();
            }
        }
    }

    private string MyStand(int iStand)
    {
        string stand = "";
        if (iStand == SupportState.iSupported)
        {
            stand = SupportState.Supported;
        }
        else if (iStand == SupportState.iApproved)
        {
            stand = SupportState.Approved;
        }
        else if (iStand == SupportState.iFinanceApproval)
        {
            stand = SupportState.Approved;
        }
        else if (iStand == SupportState.iNotSupported)
        {
            stand = SupportState.NotSupported;
        }
        else if (iStand == SupportState.iNotApproved)
        {
            stand = SupportState.NotApproved;
        }

        return stand;
    }

    private void SupportStatusColor(Label theSupportLabel)
    {
        if (theSupportLabel.Text == SupportState.Supported)
        {
            theSupportLabel.ForeColor = System.Drawing.Color.Green;
        }
        else
        {
            theSupportLabel.ForeColor = System.Drawing.Color.Red;
        }
    }

    private void SupportStatus(Label theSupportLabel, CheckBox reReviewIP, int SupportStand, long lProposalId)
    {
        //check if the IP was not supported at any point
        bool IPNotSupportedApproved = oProposalMgt.IPNotSupportedApproved(lProposalId);
        if (IPNotSupportedApproved == true)
        {
            if ((theSupportLabel.Text == SupportState.Supported) && (SupportStand == SupportState.iSupported))
            {
                reReviewIP.Enabled = true;
            }
        }
    }

    protected void forwardButton_Click(object sender, EventArgs e)
    {
        try
        {
            //Note: all selected Support Functions by BPO should be Inserted into the EIP_SUPPORTAPPROVERCOMMENTS table with the followings
            //1. UserID of the selected user, 2. ProposalID of the said proposal
            Proposal oProposal = oProposalMgt.objGetProposalById(long.Parse(proposalIDHiddenField.Value));

            ProposalApprovalDetailsMgt oProposalApprovalDetailsMgt = new ProposalApprovalDetailsMgt();
            List<ProposalApprovalDetails> oProposalApprovalDetails = oProposalApprovalDetailsMgt.lstGetProposalSupportDetailsByProposalId(oProposal.m_lProposalId);
            foreach (ProposalApprovalDetails oProposalApprovalDetail in oProposalApprovalDetails)
            {
                if (oProposalApprovalDetail.m_iUserRoleId == (int)appUsersRoles.userRole.Business_Process_Owner)
                {
                    //Check if the Business Process Owner have supported the IP.
                    if (oProposalApprovalDetail.m_iStand == SupportState.iSupported)
                    {
                        hseIDHF.Value = hseDropDownList.SelectedValue; bfmIDHF.Value = controllersDropDownList.SelectedValue; taxIDHF.Value = taxDropDownList.SelectedValue;
                        ecoIDHF.Value = ecoDropDownList.SelectedValue; secIDHF.Value = secDropDownList.SelectedValue; spcaIDHF.Value = spcaDropDownList.SelectedValue;
                        legalIDHF.Value = legalDropDownList.SelectedValue; treasuryIDHF.Value = tressuryDropDownList.SelectedValue; ITIDHF.Value = ITDropDownList.SelectedValue;
                        scmIDHF.Value = scmDropDownList.SelectedValue;

                        if ((hseDropDownList.SelectedItem.Text != "None") && (hseSupportLabel.Text.Length == 0))
                            oProposalMgt.forwardIPtoNextSupportApprover(int.Parse(hseDropDownList.SelectedValue), oProposal, oSessnx.getOnlineUser, int.Parse(hseIDHF.Value), (int)appUsersRoles.userRole.HSE_Support);

                        if ((controllersDropDownList.SelectedItem.Text != "None") && (ControllerSupportLabel.Text.Length == 0))
                            oProposalMgt.forwardIPtoNextSupportApprover(int.Parse(controllersDropDownList.SelectedValue), oProposal, oSessnx.getOnlineUser, int.Parse(bfmIDHF.Value), (int)appUsersRoles.userRole.Controllers);

                        if ((taxDropDownList.SelectedItem.Text != "None") && (taxSupportLabel.Text.Length == 0))
                            oProposalMgt.forwardIPtoNextSupportApprover(int.Parse(taxDropDownList.SelectedValue), oProposal, oSessnx.getOnlineUser, int.Parse(taxIDHF.Value), (int)appUsersRoles.userRole.TAX_Support);

                        if ((ecoDropDownList.SelectedItem.Text != "None") && (ecoSupportLabel.Text.Length == 0))
                            oProposalMgt.forwardIPtoNextSupportApprover(int.Parse(ecoDropDownList.SelectedValue), oProposal, oSessnx.getOnlineUser, int.Parse(ecoIDHF.Value), (int)appUsersRoles.userRole.Economics_Support);

                        if ((secDropDownList.SelectedItem.Text != "None") && (secSupportLabel.Text.Length == 0))
                            oProposalMgt.forwardIPtoNextSupportApprover(int.Parse(secDropDownList.SelectedValue), oProposal, oSessnx.getOnlineUser, int.Parse(secIDHF.Value), (int)appUsersRoles.userRole.Security_Support);

                        if ((spcaDropDownList.SelectedItem.Text != "None") && (spcaSupportLabel.Text.Length == 0))
                            oProposalMgt.forwardIPtoNextSupportApprover(int.Parse(spcaDropDownList.SelectedValue), oProposal, oSessnx.getOnlineUser, int.Parse(spcaIDHF.Value), (int)appUsersRoles.userRole.SPCA_Support);

                        if ((legalDropDownList.SelectedItem.Text != "None") && (legalSupportLabel.Text.Length == 0))
                            oProposalMgt.forwardIPtoNextSupportApprover(int.Parse(legalDropDownList.SelectedValue), oProposal, oSessnx.getOnlineUser, int.Parse(legalIDHF.Value), (int)appUsersRoles.userRole.LEGAL_Support);

                        if ((tressuryDropDownList.SelectedItem.Text != "None") && (treasurySupportLabel.Text.Length == 0))
                            oProposalMgt.forwardIPtoNextSupportApprover(int.Parse(tressuryDropDownList.SelectedValue), oProposal, oSessnx.getOnlineUser, int.Parse(treasuryIDHF.Value), (int)appUsersRoles.userRole.Treasury_Support);

                        if ((ITDropDownList.SelectedItem.Text != "None") && (ITSupportLabel.Text.Length == 0))
                            oProposalMgt.forwardIPtoNextSupportApprover(int.Parse(ITDropDownList.SelectedValue), oProposal, oSessnx.getOnlineUser, int.Parse(ITIDHF.Value), (int)appUsersRoles.userRole.IT);

                        if ((scmDropDownList.SelectedItem.Text != "None") && (scmSupportLabel.Text.Length == 0))
                            oProposalMgt.forwardIPtoNextSupportApprover(int.Parse(scmDropDownList.SelectedValue), oProposal, oSessnx.getOnlineUser, int.Parse(scmIDHF.Value), (int)appUsersRoles.userRole.SCM);

                        string oMessage = "Proposal successfully sent for Functional Support.";

                        //Change the Proposal status to Functional Support in progress.
                        oProposalMgt.UpdateProposalStatus(oProposal.m_lProposalId, (int)IPStatusReporter.ipStatusRpt.FunctionalSupport);

                        ajaxWebExtension.showJscriptAlertCx(Page, this, oMessage);
                        mssgLabel1.Text = oMessage;
                    }
                    else
                    {
                        string oMessage = "Sorry, the Proposal was not forwarded. Your support is required before the proposal can be sent for Functional Support.";
                        ajaxWebExtension.showJscriptAlertCx(Page, this, oMessage);
                        mssgLabel1.Text = oMessage;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //appMonitor.logAppExceptions(ex);
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    protected void reviewAgainButton_Click(object sender, EventArgs e)
    {
        Proposal oProposal = oProposalMgt.objGetProposalById(long.Parse(proposalIDHiddenField.Value));
        appUsers oAppUser = new appUsers();
        structUserMailIdx toEmail = new structUserMailIdx();
        string oMessage = "";

        if (controllerCheckBox.Checked || hseCheckBox.Checked || treasuryCheckBox.Checked || taxCheckBox.Checked || econsCheckBox.Checked || legalCheckBox.Checked || securityCheckBox.Checked || spcaCheckBox.Checked)
        {
            if (controllerCheckBox.Checked)
            {
                oAppUser = oAppUserMgt.objGetUserByUserId(int.Parse(controllersDropDownList.SelectedValue));
                toEmail = oAppUser.structUserIdx;
                SupportApproval.ReviewIPAgain(toEmail, oAppUser.m_iUserId, oProposal);
            }
            if (hseCheckBox.Checked)
            {
                oAppUser = oAppUserMgt.objGetUserByUserId(int.Parse(hseDropDownList.SelectedValue));
                toEmail = oAppUser.structUserIdx;
                SupportApproval.ReviewIPAgain(toEmail, oAppUser.m_iUserId, oProposal);
            }
            if (treasuryCheckBox.Checked)
            {
                oAppUser = oAppUserMgt.objGetUserByUserId(int.Parse(tressuryDropDownList.SelectedValue));
                toEmail = oAppUser.structUserIdx;
                SupportApproval.ReviewIPAgain(toEmail, oAppUser.m_iUserId, oProposal);
            }
            if (taxCheckBox.Checked)
            {
                oAppUser = oAppUserMgt.objGetUserByUserId(int.Parse(taxDropDownList.SelectedValue));
                toEmail = oAppUser.structUserIdx;
                SupportApproval.ReviewIPAgain(toEmail, oAppUser.m_iUserId, oProposal);
            }
            if (econsCheckBox.Checked)
            {
                oAppUser = oAppUserMgt.objGetUserByUserId(int.Parse(ecoDropDownList.SelectedValue));
                toEmail = oAppUser.structUserIdx;
                SupportApproval.ReviewIPAgain(toEmail, oAppUser.m_iUserId, oProposal);
            }
            if (legalCheckBox.Checked)
            {
                oAppUser = oAppUserMgt.objGetUserByUserId(int.Parse(legalDropDownList.SelectedValue));
                toEmail = oAppUser.structUserIdx;
                SupportApproval.ReviewIPAgain(toEmail, oAppUser.m_iUserId, oProposal);
            }
            if (securityCheckBox.Checked)
            {
                oAppUser = oAppUserMgt.objGetUserByUserId(int.Parse(secDropDownList.SelectedValue));
                toEmail = oAppUser.structUserIdx;
                SupportApproval.ReviewIPAgain(toEmail, oAppUser.m_iUserId, oProposal);
            }
            if (spcaCheckBox.Checked)
            {
                oAppUser = oAppUserMgt.objGetUserByUserId(int.Parse(spcaDropDownList.SelectedValue));
                toEmail = oAppUser.structUserIdx;
                SupportApproval.ReviewIPAgain(toEmail, oAppUser.m_iUserId, oProposal);
            }

            oMessage = "Reloaded Not Supported review on " + oProposal.m_sProj_Title + " Investment Proposal successfully sent to selected Functional Support(s) for another review.";
            ajaxWebExtension.showJscriptAlertCx(Page, this, oMessage);
        }
        else
        {
            oMessage = "No Support Function was selected to Review the IP after recent update by IP Initiator. To send the IP for another review, mark checkbox corresponding to a Support Function, then click Review Again button.";
            ajaxWebExtension.showJscriptAlertCx(Page, this, oMessage);
        }
    }
}