using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_ForwardIPForFinanceSignature : aspnetUserControl
{
    bool success = false;
    appUserMgt oAppUserMgt = new appUserMgt();
    ProposalMgt oProposalMgt = new ProposalMgt();
    SupportApprovalStatus SupportApproval = new SupportApprovalStatus();
    //FinanceSignatureApproval MyFinanceSignature = new FinanceSignatureApproval();
    SupportApproverCommentMgt oSupportApproverCommentMgt = new SupportApproverCommentMgt();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void Init_Page(long lProposalId)
    {
        Proposal oProposal = oProposalMgt.objGetProposalById(lProposalId);
        proposalIDHiddenField.Value = lProposalId.ToString();
        //bpoSupportStatus();
        FinanceSignatureSupportFlow(oProposal);
    }

    private void FinanceSignatureSupportFlow(Proposal oProposal)
    {
        //Check if all Mandatory Support Functions have supported the IP, then enable the flow to the Finance Approval
        bool HaveAllMandatoryRequiredSupportSupported = oSupportApproverCommentMgt.MandatoryFuntionalSupportStand(oProposal);
        if (HaveAllMandatoryRequiredSupportSupported == false)
        {
            financeSignatureButton.Enabled = false; //disable this button until all Mandatory Functional Supports have supported
        }
        else
        {
            //Goto Override Functional Support Method
            OverrideFunctionalSupport(oProposal);
            ckbOverride.Enabled = false;
        }
    }

    private void OverrideFunctionalSupport(Proposal oProposal)
    {
        try
        {
            financeSignatureButton.Enabled = true;
                //enable this button to forward IP to Finance Signature/GM Finance/OU Finance Director
            //if the Proposal is from Finance Function i.e IPInitiator.FunctionID == cpdms.function.Finance
            //jump this process

            //IP goes for Finance Signature, call FinanceSignature() Class to determine the route of the IP by filling the Finance Signature/GM Finance DropDownList
            //string sqlFinanceSignature = MyFinanceSignature.FinanceSignature(proposal);

            //Fill Finance Signature dropdownlist with Finance Signatures, Finance Directors
            IPLimit oIPLimits = new IPLimit();
            IPLimit.IPLevels oIPLevels = oIPLimits.Limits();

            List<appUsers> finSignatures = oProposalMgt.lstGetSupportApproverByRole((int) appUsersRoles.userRole.Finance_Signature);
            finDropDownList.Items.Add(new ListItem("None", "-1"));
            foreach (appUsers finSignature in finSignatures)
            {
                //if ((oProposal.m_lSS <= oIPLevels.iValue2) && (finSignature.m_iIPLimitId == oIPLevels.iLimitId2))
                //{
                    finDropDownList.Items.Add(new ListItem(finSignature.m_sFullName, finSignature.m_iUserId.ToString()));
                //}
            }

            List<appUsers> FinanceDirectors = oProposalMgt.lstGetSupportApproverByRole((int) appUsersRoles.userRole.Finance_Director);
            foreach (appUsers FinanceDirector in FinanceDirectors)
            {
                finDropDownList.Items.Add(new ListItem(FinanceDirector.m_sFullName, FinanceDirector.m_iUserId.ToString()));
            }

            //List<appUsers> GMFinance = oProposalMgt.lstGetSupportApproverByRole((int)appUsersRoles.userRole.GM);
            //foreach (appUsers oGMFinance in GMFinance)
            //{
            //    //Only GM Finance is allowed here
            //    if (oGMFinance.m_sFunction == cpdmsFunctionsNames.Finance)
            //    {
            //        finDropDownList.Items.Add(new ListItem(oGMFinance.m_sFullName, oGMFinance.m_iUserId.ToString()));
            //    }
            //}

            //Load Report
            //Test if BPO had previously forwarded the IP to Finance Signature or GM Finance
            List<SupportApproverComments> oSupportApproverComments = oSupportApproverCommentMgt.lstGetFunctionalSupportsApproverComments(oProposal.m_lProposalId);
            foreach (SupportApproverComments oSupportApproverComment in oSupportApproverComments)
            {
                if ((oSupportApproverComment.m_iUserRoleId == (int) appUsersRoles.userRole.Finance_Signature) ||
                    (oSupportApproverComment.m_iUserRoleId == (int) appUsersRoles.userRole.Finance_Director))
                {
                    finDropDownList.SelectedValue = oSupportApproverComment.m_iUserId.ToString();
                    finSigIDHF.Value = oSupportApproverComment.m_iUserId.ToString();
                    if (oSupportApproverComment.m_iStand == SupportState.iFinanceApproval)
                    {
                        FinSupportApproved(); //Presents the status if the IP
                    }
                    else if (oSupportApproverComment.m_iStand == SupportState.iNotApproved)
                    {
                        finSupportLabel.Text = SupportState.NotApproved;
                        finSupportLabel.ForeColor = System.Drawing.Color.Red;
                    }
                    else if (oSupportApproverComment.m_iStand == SupportState.iSupportApproverStandDefault)
                    {
                        finSupportLabel.Text = "Not Yet Actioned, send a reminder.";
                        finSupportLabel.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            if (oSupportApproverComments.Count == 0)
            {
                finSupportLabel.Text = "IP Not Yet Forwarded to Finance Signature";
                finSupportLabel.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    private void FinSupportApproved()
    {
        finSupportLabel.Text = SupportState.Approved;
        finSupportLabel.ForeColor = System.Drawing.Color.Green;
        financeSignatureButton.Enabled = false;
    }

    protected void financeSignatureButton_Click(object sender, EventArgs e)
    {
        //Note: the Finance Approver here could be Finance Signature Finance Director or GM Finance, 
        //depending on who was selected by the BPO at the time the IP was forwarded for approval.

        Proposal oProposal = oProposalMgt.objGetProposalById(long.Parse(proposalIDHiddenField.Value));
        if (finDropDownList.SelectedItem.Text != "None")
        {
            finSigIDHF.Value = finDropDownList.SelectedValue;
            appUsers oAppUsers = oAppUserMgt.objGetUserByUserId(int.Parse(finDropDownList.SelectedValue));

            if (oAppUsers.m_iUserRoleId == (int)appUsersRoles.userRole.Finance_Signature)
            {
                success = oProposalMgt.forwardIPtoNextSupportApprover(int.Parse(finDropDownList.SelectedValue), oProposal, oSessnx.getOnlineUser, int.Parse(finSigIDHF.Value), (int)appUsersRoles.userRole.Finance_Signature);
                if (success == true)
                {
                    oProposalMgt.UpdateProposalStatus(oProposal.m_lProposalId, (int)IPStatusReporter.ipStatusRpt.AwaitFinSig);
                    string oMessage = "Proposal forwarded to Finance Signature, awaiting approval.";
                    ajaxWebExtension.showJscriptAlertCx(Page, this, oMessage);
                    mssgLabel.Text = oMessage;
                }
            }
            else if (oAppUsers.m_iUserRoleId == (int)appUsersRoles.userRole.Finance_Director)
            {
                success = oProposalMgt.forwardIPtoNextSupportApprover(int.Parse(finDropDownList.SelectedValue), oProposal, oSessnx.getOnlineUser, int.Parse(finSigIDHF.Value), (int)appUsersRoles.userRole.Finance_Director);
                if (success == true)
                {
                    oProposalMgt.UpdateProposalStatus(oProposal.m_lProposalId, (int)IPStatusReporter.ipStatusRpt.AwaitGMFin);// AwaitGMFin = OU Finance Director
                    string oMessage = "Proposal forwarded to Finance Director, awaiting approval.";
                    ajaxWebExtension.showJscriptAlertCx(Page, this, oMessage);
                    mssgLabel.Text = oMessage;
                }
            }

            //TODO: BPO sends warning mail to Non Mandatory Functional Supports who have not supported the IP after the IP has been sent for Finance Signature
            NonMandatorySupportWarnings Warning = new NonMandatorySupportWarnings();
            Warning.BPOWarningMessage(oProposal, oSessnx.getOnlineUser);
        }
        else
        {
            ajaxWebExtension.showJscriptAlertCx(Page, this, "Please select Finance Approver.");
        }
    }
    protected void ckbOverride_CheckedChanged(object sender, EventArgs e)
    {
        if (ckbOverride.Checked)
        {
            Proposal oProposal = oProposalMgt.objGetProposalById(long.Parse(proposalIDHiddenField.Value));
            OverrideFunctionalSupport(oProposal);
        }
        else Reset();        
    }

    private void Reset()
    {
        financeSignatureButton.Enabled = false;
        finSupportLabel.Text = "";
        finDropDownList.Items.Clear();
    }
}
