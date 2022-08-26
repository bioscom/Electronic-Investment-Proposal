using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class UserControl_MyPendingProposals : aspnetUserControl
{
    appUsers CurrentUser = new appUsers();

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

        if (ButtonClicked == "AddComment")
        {
            LinkButton lbAddComment = (LinkButton)pendingProposalGridView.Rows[index].FindControl("AddCommentLinkButton");
            lProposalId = long.Parse(lbAddComment.Attributes["PROPOSALID"].ToString());

            Response.Redirect("~/ApprovalSupportFunction/AddComment.aspx" + "?Proposalid=" + lProposalId, false);

            //if (oSessnx.getOnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.GM_Regional_Planning)
            //{
            //    Response.Redirect("~/ApprovalSupportFunction/AddCommentGMREPlan.aspx" + "?Proposalid=" + lProposalId, false);
            //}
            //else
            //{
            //    //Note: this else statement takes care of the following Approval roles (MD, Finance Signature, and other Support Functions)
            //    Response.Redirect("~/ApprovalSupportFunction/AddComment.aspx" + "?Proposalid=" + lProposalId, false);
            //}
            //else if (CurrentUser.iUSERROLESID == eipUserRoles.iREVP)
            //{
            //    Response.Redirect("~/ApprovalSupportFunction/AddCommentREVP.aspx" + "?Proposalid=" + ProposalID, false);
            //}
            //else if ((CurrentUser.iUSERROLESID == eipUserRoles.iVP) || (CurrentUser.iUSERROLESID == eipUserRoles.iMD))
            //{
            //    Response.Redirect("~/ApprovalSupportFunction/AddCommentVPS.aspx" + "?Proposalid=" + ProposalID, false);
            //}
            //else if (CurrentUser.iUSERROLESID == eipUserRoles.iCorporatePlanningManager)
            //{
            //    Response.Redirect("~/CPMIPMGT/Default.aspx" + "?Proposalid=" + ProposalID, false);
            //}
        }
        if (ButtonClicked == "CheckComment")
        {
            LinkButton lbCheckComment = (LinkButton)pendingProposalGridView.Rows[index].FindControl("CheckCommentLinkButton");
            lProposalId = long.Parse(lbCheckComment.Attributes["PROPOSALID"].ToString());
            Response.Redirect("~/Common/ViewProposalStatus.aspx" + "?Proposalid=" + lProposalId, false);
        }

        if (ButtonClicked == "EditThisProposal")
        {
            LinkButton lbEditProposal = (LinkButton)pendingProposalGridView.Rows[index].FindControl("EditProposalLinkButton");
            lProposalId = long.Parse(lbEditProposal.Attributes["PROPOSALID"].ToString());
            Response.Redirect("~/EditIP.aspx" + "?Proposalid=" + lProposalId, false);
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
        LoadMyPendingProposals(oSessnx.getOnlineUser);
    }

    public void LoadMyPendingProposals(appUsers OnlineUser)
    {
        SupportApproverCommentMgt oSupportApproverCommentMgt = new SupportApproverCommentMgt();
        DataTable dt = new DataTable();

        if ((OnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.Finance_Director) || (OnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.Finance_Signature))
        {
            dt = oSupportApproverCommentMgt.MyPendingProposal(OnlineUser.m_iUserId);
        }
        else
        {
            dt = oSupportApproverCommentMgt.MyPendingProposal2(OnlineUser.m_iUserId);
        }

        pendingProposalGridView.DataSource = dt;
        pendingProposalGridView.DataBind();

        ManageLinks();
    }

    private void ManageLinks()
    {
        appUserMgt oAppUserMgt = new appUserMgt();
        SupportApproverCommentMgt oSupportApproverCommentMgt = new SupportApproverCommentMgt();

        foreach (GridViewRow grdRow in pendingProposalGridView.Rows)
        {
            LinkButton editProposal = (LinkButton)grdRow.FindControl("EditProposalLinkButton");
            LinkButton actionProposal = (LinkButton)grdRow.FindControl("AddCommentLinkButton");
            if (oSessnx.getOnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.Line_Team_Lead)
            {
                editProposal.Enabled = true; // Can edit Proposal
            }
            else
            {
                //If proposal has exceeded SLA, the disable the Action button for the Non Mandatory Functional Support
                //This happens when either Finance Signature or Finance Director had actioned the IP.
                SupportApproverComments oComment = oSupportApproverCommentMgt.objGetFunctionalSupportsApproverComments(long.Parse(actionProposal.Attributes["PROPOSALid"].ToString()));

                if ((oComment.m_iUserRoleId == (int)appUsersRoles.userRole.Finance_Director) ||
                    (oComment.m_iUserRoleId == (int)appUsersRoles.userRole.Finance_Signature) ||
                    ((oComment.m_iUserRoleId == (int)appUsersRoles.userRole.VP) && (oAppUserMgt.objGetUserByUserId(oComment.m_iUserId).eFunction.m_sFunction == cpdmsFunctionsNames.Finance)))
                {
                    if (oComment.m_iStand == SupportState.iFinanceApproval)
                    {
                        actionProposal.Enabled = false;
                    }
                }
                editProposal.Enabled = false; // Can not edit Proposal
            }
        }
    }
}


//TODO: To be deleted when program works fine
//if (CurrentUser.iUSERROLESID == eipUserRoles.iLineTeamLead)
//{
//    LineTeamLeadComments ltl = new LineTeamLeadComments();
//    ds = ltl.MyPendingProposal(CurrentUser.iIDUSERMGT.ToString());
//}
//else if (CurrentUser.iUSERROLESID == eipUserRoles.iBusinessProcessOwner)
//{
//    BPOComments BPO = new BPOComments();
//    ds = BPO.MyPendingProposal(CurrentUser.iIDUSERMGT.ToString());
//}
//else if (CurrentUser.iUSERROLESID == eipUserRoles.iGMRegionalPlanning)
//{
//    GMREPLANComments GMREPLAN = new GMREPLANComments();
//    ds = GMREPLAN.MyPendingProposal(CurrentUser.iIDUSERMGT.ToString());
//}
//else if (CurrentUser.iUSERROLESID == eipUserRoles.iFinanceSignature)
//{
//    FinanceSignatureComments FinSig = new FinanceSignatureComments();
//    ds = FinSig.MyPendingProposal(CurrentUser.iIDUSERMGT.ToString());
//}
//else if (CurrentUser.iUSERROLESID == eipUserRoles.iREVP)
//{
//    REVPComments REVP = new REVPComments();
//    ds = REVP.MyPendingProposal(CurrentUser.iIDUSERMGT.ToString());
//}
//else if ((CurrentUser.iUSERROLESID == eipUserRoles.iVP) && (CurrentUser.Function != cpdmsFunctionsNames.Finance))
//{
//    VPComments VP = new VPComments();
//    ds = VP.MyPendingProposal(CurrentUser.iIDUSERMGT.ToString());
//}
//else if ((CurrentUser.iUSERROLESID == eipUserRoles.iVP) && (CurrentUser.Function == cpdmsFunctionsNames.Finance))
//{
//    VPFinanceComments VPFin = new VPFinanceComments();
//    ds = VPFin.MyPendingProposal(CurrentUser.iIDUSERMGT.ToString());
//}
//else if (CurrentUser.iUSERROLESID == eipUserRoles.iMD)
//{
//    MDComments MD = new MDComments();
//    ds = MD.MyPendingProposal(CurrentUser.iIDUSERMGT.ToString());
//}
//else if ((CurrentUser.iUSERROLESID == eipUserRoles.iGM) && (CurrentUser.Function == cpdmsFunctionsNames.Finance))
//{
//    GMFinanceComments GMFiN = new GMFinanceComments();
//    ds = GMFiN.MyPendingProposal(CurrentUser.iIDUSERMGT.ToString());
//}
//else if ((CurrentUser.iUSERROLESID == eipUserRoles.iGM) && (CurrentUser.Function != cpdmsFunctionsNames.Finance))
//{
//    GMComments GM = new GMComments();
//    ds = GM.MyPendingProposal(CurrentUser.iIDUSERMGT.ToString());
//}
//else if (CurrentUser.iUSERROLESID == eipUserRoles.iCorporatePlanningManager)
//{
//    CorporatePlanningManager cpm = new CorporatePlanningManager();
//    ds = cpm.MyPendingProposal(CurrentUser.iIDUSERMGT.ToString());
//}
//else
//{
//    //For Support Functions and others
//    FunctionalSupportComments FuncSupport = new FunctionalSupportComments();
//    ds = FuncSupport.MyPendingProposal(CurrentUser.iIDUSERMGT.ToString());
//}

//if (ds.Rows.Count == 0)
//{
//    MessageBox.Show("No Pending Proposal Found!!!");
//}
//else if (ds.Rows.Count > 0) //Display LinkButtons to be visible according to the Proposal Logic
//{

//}
