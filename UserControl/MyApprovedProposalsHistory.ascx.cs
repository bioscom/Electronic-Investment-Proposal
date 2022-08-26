using System;
using System.Web.UI.WebControls;
using System.Data;

public partial class UserControl_MyApprovedProposalsHistory : aspnetUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void pendingProposalGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        long lProposalId = 0;
        
        string ButtonClicked = e.CommandName; //Determines which button was clicked (stores the name of each button)
        int index = Convert.ToInt32(e.CommandArgument); //Command Argument stores the index of each row

        if (ButtonClicked == "ViewOriginalProposal")
        {
            LinkButton lbOriginalProposal = (LinkButton)pendingProposalGridView.Rows[index].FindControl("ViewOriginalProposalLinkButton");
            lProposalId = long.Parse(lbOriginalProposal.Attributes["PROPOSALID"].ToString());
            Response.Redirect("~/ViewProposal.aspx" + "?Proposalid=" + lProposalId, false);
        }

        if (ButtonClicked == "CheckComment")
        {
            LinkButton lbCheckComment = (LinkButton)pendingProposalGridView.Rows[index].FindControl("CheckCommentLinkButton");
            lProposalId = long.Parse(lbCheckComment.Attributes["PROPOSALID"].ToString());
            Response.Redirect("~/Common/ViewProposalStatus.aspx" + "?Proposalid=" + lProposalId, false);
        }

        if (ButtonClicked == "forwardProposal")
        {
            LinkButton lbForwardProposal = (LinkButton)pendingProposalGridView.Rows[index].FindControl("forwardProposalLinkButton");
            lProposalId = long.Parse(lbForwardProposal.Attributes["PROPOSALID"].ToString());

            ProposalMgt oProposalMgt = new ProposalMgt();
            Proposal oProposal = oProposalMgt.objGetProposalById(lProposalId);

            sendMail oSendMail = new sendMail(oSessnx.getOnlineUser.structUserIdx);
            oSendMail.ForwardIP(oProposal, oSessnx.getOnlineUser.structUserIdx);
        }
    }

    protected void pendingProposalGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        pendingProposalGridView.PageIndex = e.NewPageIndex;
        LoadMyProposalsHistory(oSessnx.getOnlineUser);
    }

    public void LoadMyProposalsHistory(appUsers OnlineUser)
    {
        SupportApproverCommentMgt oSupportApproverCommentMgt = new SupportApproverCommentMgt();
        DataTable dt = oSupportApproverCommentMgt.MyProposalHistory(OnlineUser.m_iUserId);

        pendingProposalGridView.DataSource = dt;
        pendingProposalGridView.DataBind();
    }
}


//if (CurrentUser.iUSERROLESID == eipUserRoles.iLineTeamLead)
//{
//    LineTeamLeadComments ltl = new LineTeamLeadComments();
//    ds = ltl.MyProposalHistory(CurrentUser.iIDUSERMGT.ToString());
//}
//else if (CurrentUser.iUSERROLESID == eipUserRoles.iBusinessProcessOwner)
//{
//    BPOComments BPO = new BPOComments();
//    ds = BPO.MyProposalHistory(CurrentUser.iIDUSERMGT.ToString());
//}
//else if (CurrentUser.iUSERROLESID == eipUserRoles.iGMRegionalPlanning)
//{
//    GMREPLANComments GMREPLAN = new GMREPLANComments();
//    ds = GMREPLAN.MyProposalHistory(CurrentUser.iIDUSERMGT.ToString());
//}
//else if (CurrentUser.iUSERROLESID == eipUserRoles.iFinanceSignature)
//{
//    FinanceSignatureComments FinSig = new FinanceSignatureComments();
//    ds = FinSig.MyProposalHistory(CurrentUser.iIDUSERMGT.ToString());
//}
//else if (CurrentUser.iUSERROLESID == eipUserRoles.iREVP)
//{
//    REVPComments REVP = new REVPComments();
//    ds = REVP.MyProposalHistory(CurrentUser.iIDUSERMGT.ToString());
//}
//else if ((CurrentUser.iUSERROLESID == eipUserRoles.iVP) && (CurrentUser.Function != cpdmsFunctionsNames.Finance))
//{
//    VPComments VP = new VPComments();
//    ds = VP.MyProposalHistory(CurrentUser.iIDUSERMGT.ToString());
//}
//else if ((CurrentUser.iUSERROLESID == eipUserRoles.iVP) && (CurrentUser.Function == cpdmsFunctionsNames.Finance))
//{
//    VPFinanceComments VPFin = new VPFinanceComments();
//    ds = VPFin.MyProposalHistory(CurrentUser.iIDUSERMGT.ToString());
//}
//else if (CurrentUser.iUSERROLESID == eipUserRoles.iMD)
//{
//    MDComments MD = new MDComments();
//    ds = MD.MyProposalHistory(CurrentUser.iIDUSERMGT.ToString());
//}
//else if ((CurrentUser.iUSERROLESID == eipUserRoles.iGM) && (CurrentUser.Function == cpdmsFunctionsNames.Finance))
//{
//    GMFinanceComments GMFiN = new GMFinanceComments();
//    ds = GMFiN.MyProposalHistory(CurrentUser.iIDUSERMGT.ToString());
//}
//else if ((CurrentUser.iUSERROLESID == eipUserRoles.iGM) && (CurrentUser.Function != cpdmsFunctionsNames.Finance))
//{
//    GMComments GM = new GMComments();
//    ds = GM.MyProposalHistory(CurrentUser.iIDUSERMGT.ToString());
//}
//else if (CurrentUser.iUSERROLESID == eipUserRoles.iCorporatePlanningManager)
//{
//    CorporatePlanningManager cpm = new CorporatePlanningManager();
//    ds = cpm.MyProposalHistory(CurrentUser.iIDUSERMGT.ToString());
//}


//if (ds.Rows.Count == 0)
//{
//    MessageBox.Show("No Pending Proposal Found!!!");
//}