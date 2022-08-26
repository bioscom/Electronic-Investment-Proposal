using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_IPWorkFlowOverRide : aspnetUserControl
{
    ProposalMgt oProposalMgt = new ProposalMgt();
    appUserMgt oAppUserMgt = new appUserMgt();
    SupportApprovalStatus SupportApproval = new SupportApprovalStatus();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void Init_Page(long lProposalId)
    {
        proposalIDHiddenField.Value = lProposalId.ToString();
        //Load Finance Signature
        List<appUsers> finSignatures = oProposalMgt.lstGetSupportApproverByRole((int)appUsersRoles.userRole.Finance_Signature);
        ddlFinSig.Items.Add(new ListItem("None", "-1"));
        foreach (appUsers finSignature in finSignatures)
        {
            ddlFinSig.Items.Add(new ListItem(finSignature.m_sFullName, finSignature.m_iUserId.ToString()));
        }

        List<appUsers> FinanceDirectors = oProposalMgt.lstGetSupportApproverByRole((int)appUsersRoles.userRole.Finance_Director);
        foreach (appUsers FinanceDirector in FinanceDirectors)
        {
            ddlFinSig.Items.Add(new ListItem(FinanceDirector.m_sFullName, FinanceDirector.m_iUserId.ToString()));
        }

        //TODO: Not Sure if this guy will be needed
        List<appUsers> GMFinance = oProposalMgt.lstGetSupportApproverByRole((int)appUsersRoles.userRole.VP);
        foreach (appUsers oGMFinance in GMFinance)
        {
            //Only GM Finance is allowed here
            if (oGMFinance.eFunction.m_sFunction == cpdmsFunctionsNames.Finance)
            {
                ddlFinSig.Items.Add(new ListItem(oGMFinance.m_sFullName, oGMFinance.m_iUserId.ToString()));
            }
        }

        //Load Finance Director
        List<appUsers> OUFinanceDirectors = oProposalMgt.lstGetSupportApproverByRole((int)appUsersRoles.userRole.Finance_Director);
        foreach (appUsers OUFinanceDirector in OUFinanceDirectors)
        {
            ddlOUFinDir.Items.Add(new ListItem(OUFinanceDirector.m_sFullName, OUFinanceDirector.m_iUserId.ToString()));
        }

        //Load Corporate Planning OU for the IP
        List<appUsers> CPMs = oProposalMgt.lstGetSupportApproverByRole((int)appUsersRoles.userRole.Technical_Planning_Manager);
        foreach (appUsers CPM in CPMs)
        {
            ddlCPM.Items.Add(new ListItem(CPM.m_sFullName, CPM.m_iUserId.ToString()));
        }

        //Load GM Regional Planning
        List<appUsers> GMREs = oProposalMgt.lstGetSupportApproverByRole((int)appUsersRoles.userRole.GM_Regional_Planning);
        foreach (appUsers GMRE in GMREs)
        {
            ddlGMREPlan.Items.Add(new ListItem(GMRE.m_sFullName, GMRE.m_iUserId.ToString()));
        }

        //Load GMs
        List<appUsers> GMs = oProposalMgt.lstGetSupportApproverByRole((int)appUsersRoles.userRole.VP); //Where VP == General Managers
        foreach (appUsers GM in GMs)
        {
            ddlGM.Items.Add(new ListItem(GM.m_sFullName, GM.m_iUserId.ToString()));
        }

        //Load REVP
        List<appUsers> REVPs = oProposalMgt.lstGetSupportApproverByRole((int)appUsersRoles.userRole.REVP); //Where VP == General Managers
        foreach (appUsers REVP in REVPs)
        {
            ddlREVP.Items.Add(new ListItem(REVP.m_sFullName, REVP.m_iUserId.ToString()));
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

    protected void btnFinSig_Click(object sender, EventArgs e)
    {
        OverrideForwardProposal(ddlFinSig, (int)appUsersRoles.userRole.Finance_Signature);
    }
    protected void btnGMFin_Click(object sender, EventArgs e)
    {
        OverrideForwardProposal(ddlFinSig, (int)appUsersRoles.userRole.Finance_Signature);
    }
    protected void btnCPM_Click(object sender, EventArgs e)
    {
        OverrideForwardProposal(ddlFinSig, (int)appUsersRoles.userRole.Finance_Signature);
    }
    protected void btnGMREPlan_Click(object sender, EventArgs e)
    {
        OverrideForwardProposal(ddlFinSig, (int)appUsersRoles.userRole.Finance_Signature);
    }
    protected void btnVP_Click(object sender, EventArgs e)
    {
        OverrideForwardProposal(ddlFinSig, (int)appUsersRoles.userRole.Finance_Signature);
    }
    protected void btnREVP_Click(object sender, EventArgs e)
    {
        OverrideForwardProposal(ddlFinSig, (int)appUsersRoles.userRole.Finance_Signature);
    }

    private void SupportStatusCode(int iStand, Label Stand, Button forward)
    {
        if (iStand == SupportState.iSupported)
        {
            Stand.ForeColor = System.Drawing.Color.Green;
            forward.Enabled = false;
        }
        else if (iStand == SupportState.iApproved)
        {
            Stand.ForeColor = System.Drawing.Color.Green;
            forward.Enabled = false;
        }
        else if (iStand == SupportState.iFinanceApproval)
        {
            Stand.ForeColor = System.Drawing.Color.Green;
            forward.Enabled = false;
        }
        else
        {
            Stand.ForeColor = System.Drawing.Color.Red;
            forward.Enabled = true;
        }
    }

    private void OverrideForwardProposal(DropDownList ddlReceiver, int iRoleId)
    {
        long lProposalId = long.Parse(proposalIDHiddenField.Value);
        Proposal oProposal = oProposalMgt.objGetProposalById(lProposalId);
        sendMail oSendMail = new sendMail(oSessnx.getOnlineUser.structUserIdx);
        appUsers oInitiator = oAppUserMgt.objGetUserByUserId(oProposal.m_iUserId);
        appUsers oReceiver = oAppUserMgt.objGetUserByUserId(int.Parse(ddlReceiver.SelectedValue));

        bool success = oProposalMgt.AssignIPtoNextSupportApprover(int.Parse(ddlReceiver.SelectedValue), lProposalId, iRoleId);
        if (success)
        {
            success = oSendMail.mailFinanceSignature(oReceiver.structUserIdx, oInitiator.structUserIdx, oProposal.m_sProj_Title, oInitiator.m_sFullName, oProposal.m_sProj_Num);
            if (success)
            {
                string oMessage = "Proposal successfully sent to  " + ddlReceiver.SelectedItem.Text + " Finance Support";
                ajaxWebExtension.showJscriptAlertCx(Page, this, oMessage);
            }
        }
    }
    protected void GMDetailedSupportLinkButton_Click(object sender, EventArgs e)
    {

    }
    protected void VPDetailedSupportLinkButton_Click(object sender, EventArgs e)
    {

    }
}