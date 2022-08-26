using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections.Generic;

public partial class Common_IPRouter : aspnetPage
{
    appUserMgt oAppUserMgt = new appUserMgt();
    ProposalMgt oProposalMgt = new ProposalMgt();

    protected void Page_Load(object sender, EventArgs e)
    {
        bool bRet = false;
        try
        {
            string[] sPageAccess = { appUsersRoles.userRole.Administrator.ToString(), appUsersRoles.userRole.IP_Initiator.ToString(), appUsersRoles.userRole.Business_Process_Owner.ToString() };
            appUsersRoles oAccess = new appUsersRoles();
            bRet = oAccess.grantPageAccess(sPageAccess, this.oSessnx.getOnlineUser.m_eUserRole);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
        if (!bRet) Response.Redirect("~/Profiles/pageDenied.aspx");

        if (!IsPostBack)
        {
            pnlDetails.Visible = false;
            if (oSessnx.getOnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.IP_Initiator)
            {
                List<Proposal> oProposals = oProposalMgt.lstGetMyPendingProposals(oSessnx.getOnlineUser.m_iUserId);
                foreach (Proposal oProposal in oProposals)
                {
                    proposalList.Items.Add(new ListItem(oProposal.m_sProj_Title, oProposal.m_lProposalId.ToString()));
                }
            }
            else
            {
                List<Proposal> oProposals = oProposalMgt.lstGetPendingProposals();
                foreach (Proposal oProposal in oProposals)
                {
                    proposalList.Items.Add(new ListItem(oProposal.m_sProj_Title, oProposal.m_lProposalId.ToString()));
                }
            }
        }
    }

    protected void closeButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Common/IPRegister.aspx");
    }

    protected void IPRouterGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument); //Command Argument stores the index of each row

        LinkButton lbForwardProposal = (LinkButton)IPRouterGridView.Rows[index].FindControl("ForwardProposalButton");
        long lProposalId = long.Parse(lbForwardProposal.Attributes["PROPOSALID"].ToString());
        int iRoleId = Convert.ToInt32(lbForwardProposal.Attributes["USERROLESID"]);
        int iOldUserId = Convert.ToInt32(lbForwardProposal.Attributes["IDUSERMGT"]);
        
        DropDownList ddlUsers = (DropDownList)IPRouterGridView.Rows[index].FindControl("userDropDownList");
        Label UserRole = (Label)IPRouterGridView.Rows[index].FindControl("labelUserRole");

        Proposal oProposal = oProposalMgt.objGetProposalById(lProposalId);
        appUsers oInitiator = oAppUserMgt.objGetUserByUserId(oProposal.m_iUserId);
        appUsers oReceiver = oAppUserMgt.objGetUserByUserId(int.Parse(ddlUsers.SelectedValue));

        sendMail oSendMail = new sendMail(oSessnx.getOnlineUser.structUserIdx);
        //TODO: this line below should be reviewed for proper re-routing of IP to new person
        bool bRet = ProposalRouter.reAssignIPtoFunctionalSupport(int.Parse(ddlUsers.SelectedValue), lProposalId, iOldUserId);
        if (bRet)
        {
            if (iRoleId == (int)appUsersRoles.userRole.Finance_Signature)
            {
                oSendMail.mailFinanceSignature(oReceiver.structUserIdx, oInitiator.structUserIdx, oProposal.m_sProj_Title, oInitiator.m_sFullName, oProposal.m_sProj_Num);
            }
            else if (iRoleId == (int)appUsersRoles.userRole.Finance_Director)
            {
                oSendMail.mailFinanceSignature(oReceiver.structUserIdx, oInitiator.structUserIdx, oProposal.m_sProj_Title, oInitiator.m_sFullName, oProposal.m_sProj_Num);
            }
            else if (iRoleId == (int)appUsersRoles.userRole.GM_Regional_Planning)
            {
                oSendMail.MailGMREPlanning(oReceiver.structUserIdx, oInitiator.structUserIdx, oProposal.m_sProj_Title, oProposal.m_sProj_Num);
            }
            //else if (iRoleId == (int)appUsersRoles.userRole.IP_Initiator)
            //{
            //    oSendMail.DeligateIPInitiatorFunction(oReceiver.structUserIdx, oProposal.m_sProj_Title, oInitiator.m_sFullName, oProposal.m_sProj_Num);
            //}
            else if (iRoleId == (int)appUsersRoles.userRole.Line_Team_Lead)
            {
                oSendMail.IPInitiatorLoadedIP2(oReceiver.structUserIdx, oProposal.m_sProj_Title, oInitiator.m_sFullName, oProposal.m_sProj_Num);
            }
            else if (iRoleId == (int)appUsersRoles.userRole.REVP)
            {
                oSendMail.MailRegionalVicePresident(oReceiver.structUserIdx, oInitiator.structUserIdx, oProposal.m_sProj_Title, oProposal.m_sProj_Num);
            }
            else if (iRoleId == (int)appUsersRoles.userRole.Technical_Planning_Manager)
            {
                oSendMail.sendMailToCPM(oReceiver.structUserIdx, oInitiator.structUserIdx, oSessnx.getOnlineUser.eFunction.m_sFunction, oProposal.m_sProj_Title, oProposal.m_sProj_Num, oProposal.m_lProposalId);
            }
            else if (iRoleId == (int)appUsersRoles.userRole.VP)
            {
                oSendMail.MailVicePresident2(oReceiver.structUserIdx, oProposal.m_sProj_Title, oProposal.m_sProj_Num);
            }
            else
            {
                oSendMail.IPForwardForSupportApproval(oReceiver.structUserIdx, oInitiator.structUserIdx, oInitiator.m_sFullName, oProposal);
            }

            proposalList_SelectedIndexChanged(this, e);

            ajaxWebExtension.showJscriptAlert(Page, this, "Proposal successfully routed to " + oReceiver.m_sFullName);
        }
        else
        {
            ajaxWebExtension.showJscriptAlert(Page, this, "Not successful!!!");
        }
    }
    
    protected void proposalList_SelectedIndexChanged(object sender, EventArgs e)
    {
        ProposalApprovalDetailsMgt oProposalApprovalDetailsMgt = new ProposalApprovalDetailsMgt();
        
        Proposal oProposal = oProposalMgt.objGetProposalById(long.Parse(proposalList.SelectedValue));
        pnlDetails.Visible = true;
        IPDetailInfo1.LoadProposalDetails(oProposal);

        //oProposalRouter.LoadProposalSupportApprovers(oProposal, oSessnx.getOnlineUser, IPRouterGridView);
        IPRouterGridView.DataSource = ProposalRouter.dtAllSupportApproverForThisProposal(oProposal.m_lProposalId);
        IPRouterGridView.DataBind();

        foreach (GridViewRow grdRow in IPRouterGridView.Rows)
        {
            DropDownList ddlUsers = (DropDownList)grdRow.FindControl("userDropDownList");
            Label labelUserRole = (Label)grdRow.FindControl("labelUserRole");
            Label labelStatus = (Label)grdRow.FindControl("labelStatus"); //labelStatus
            LinkButton lbForwardProposal = (LinkButton)grdRow.FindControl("ForwardProposalButton");
            int iUserId = Convert.ToInt32(lbForwardProposal.Attributes["IDUSERMGT"]);

            ProposalApprovalDetails oProposalApprovalDetails = oProposalApprovalDetailsMgt.objGetProposalSupportDetailsByProposalUserId(iUserId, oProposal.m_lProposalId);
            if (labelUserRole != null)
            {
                int iRoleId = int.Parse(labelUserRole.Text);
                labelUserRole.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)iRoleId);
                if ((oProposalApprovalDetails.m_iStand == SupportState.iSupported) || (oProposalApprovalDetails.m_iStand == SupportState.iFinanceApproval) || (oProposalApprovalDetails.m_iStand == SupportState.iApproved))
                {
                    ddlUsers.Enabled = false;
                    lbForwardProposal.Enabled = false;
                }
                else
                {
                    labelStatus.Text = oProposalMgt.MyStand(oProposalApprovalDetails.m_iStand);
                    oProposalMgt.SupportStatusCode(oProposalApprovalDetails.m_iStand, labelStatus);
                    List<appUsers> oAppUsers = oAppUserMgt.lstGetUsersByRole(iRoleId);
                    foreach (appUsers oAppUser in oAppUsers)
                    {
                        ddlUsers.Items.Add(new ListItem(oAppUser.m_sFullName, oAppUser.m_iUserId.ToString()));
                    }
                }
            }
        }
    }
}