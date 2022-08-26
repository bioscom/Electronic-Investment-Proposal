using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;

public partial class ApprovalSupportFunction_AddCommentGMREPlan : aspnetPage
{
    Proposal oProposal = new Proposal();
    ProposalMgt oProposalMgt = new ProposalMgt();
    appUsers oAppUser = new appUsers();
    appUserMgt oAppUserMgt = new appUserMgt();
    TimeDateCulture dateCulture = new TimeDateCulture();
    SupportApprovalStatus SupportApproval = new SupportApprovalStatus();
    ProposalApprovalDetailsMgt oProposalApprovalDetailsMgt = new ProposalApprovalDetailsMgt();
    SupportApproverCommentMgt oSupportApproverCommentMgt = new SupportApproverCommentMgt();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Proposalid"] != null)
        {
            oProposal = oProposalMgt.objGetProposalById(long.Parse(Request.QueryString["Proposalid"].ToString()));

            if (!IsPostBack)
            {
                oProposalMgt.FillSupportState(SupportStandDropDownList);
                IPDetailInfo1.LoadProposalDetails(oProposal);
                List<ProposalApprovalDetails> oProposalApprovalDetails = oProposalApprovalDetailsMgt.lstGetProposalSupportDetailsByProposalId(oProposal.m_lProposalId);

                //TODO: stream down the no of rows returned
                foreach (ProposalApprovalDetails oProposalApprovalDetail in oProposalApprovalDetails)
                {
                    if (oProposalApprovalDetail.m_iUserRoleId == (int)appUsersRoles.userRole.GM_Regional_Planning)
                    {
                        SupportStandDropDownList.SelectedValue = oProposalApprovalDetail.m_iStand.ToString();
                        CommentTextBox.Text = oProposalApprovalDetail.m_sComments;
                    }
                }

                LoadVPGrid(oProposal);
            }
        }
    }

    private void LoadVPGrid(Proposal proposal)
    {
        grdView.DataSource = oAppUserMgt.dtGetUsersByRole((int)appUsersRoles.userRole.VP);
        grdView.DataBind();

        //Vice presidents (i.e) the GMs must have been Inserted into the table before showing their comments and date stuff
        DataTable dt = oProposalMgt.dtGetProposalSupportApprovalDetailsByRole(proposal.m_lProposalId, (int)appUsersRoles.userRole.VP);
        foreach (GridViewRow grdRow in grdView.Rows)
        {
            CheckBox VP = (CheckBox)grdRow.FindControl("GeneralManager");
            CheckBox Reminder = (CheckBox)grdRow.FindControl("Reminder");
            Label Status = (Label)grdRow.FindControl("StatusLabel");
            Label Comments = (Label)grdRow.FindControl("CommentsLabel");
            Status.Font.Bold = true;

            string VPUserID = VP.Attributes["IDUSERMGT"].ToString();

            string stand = "";
            //Check if VPUserID exists in dt DataTable
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["IDUSERMGT"].ToString() == VPUserID)
                {
                    VP.Checked = true;
                    VP.Enabled = false;

                    stand = oProposalMgt.MyStand(Convert.ToInt32(dr["STAND"]));
                    oProposalMgt.SupportStatusCode(Convert.ToInt32(dr["STAND"]), Status);

                    Status.Text = stand;
                    Comments.Text = dr["COMMENTS"].ToString();
                }
            }
        }
    }

    protected void forwardButton_Click(object sender, EventArgs e)
    {
        appUsers oIPInitiator = oAppUserMgt.objGetUserByUserId(oProposal.m_iUserId);
        appUsers oBPO = oAppUserMgt.objGetUserByUserRoleCompany(oIPInitiator.m_iCompany, (int)appUsersRoles.userRole.Business_Process_Owner);

        bool success = false;
        bool aGM_Selected = false;

        foreach (GridViewRow grdRow in grdView.Rows)
        {
            CheckBox GeneralManager = (CheckBox)grdRow.FindControl("GeneralManager");
            if (GeneralManager.Checked)
            {
                aGM_Selected = true;
                break;
            }
        }

        if (aGM_Selected == false)
        {
            ajaxWebExtension.showJscriptAlert(Page, this, "No General Manager selected. At least, General Manager Finance must be selected before this page can be submitted.");
        }
        else if (aGM_Selected == true)
        {
            //Note: This is a very important requirement.
            //The GM Regional Planning Support stand should be set to SupportState.iSupportApproverStandDefault, which is 0, and and SUPPORT_BIT  = 0.
            //This will allow the GM RE plan to have access to the IP when it is still going through GMs support,
            //the GMRE Plan can also send the IP to any other GM that is supposed to see the IP, who was not included initially.

            //The GM RE Plan will be the one to forward the IP to the REVP, when all the VPs have supported the IP.
            //After this the GM RE Plan Support Stand will be set to SupportState.Supported and the IP will then leave his/her in-tray.

            //Please, for now this Procedure has to follow as below, we may still refer back to above later (19-May-2010)

            int stand = 0;
            if (SupportStandDropDownList.SelectedValue == SupportState.iSupported.ToString())
            {
                stand = SupportState.iStandDefault;
            }
            else if (SupportStandDropDownList.SelectedValue == SupportState.iNotSupported.ToString())
            {
                stand = SupportState.iNotSupported;
            }

            if ((CommentTextBox.Text == "") && SupportStandDropDownList.SelectedValue == SupportState.iNotSupported.ToString())
            {
                ajaxWebExtension.showJscriptAlert(Page, this, "Please, enter reason(s) why Not Supported in the comment box.");
            }
            else
            {
                //1. Enter Support Details for GM RE Plan
                success = oSupportApproverCommentMgt.AddComment(oSessnx.getOnlineUser, oProposal, CommentTextBox.Text, stand);
                if (success)
                {
                    //2. Send Mails to Selected GMs (Provided that GM RE Plan supports the IP)
                    GMREPlanAddCommentProcedure(oSessnx.getOnlineUser, oProposal, CommentTextBox.Text, int.Parse(SupportStandDropDownList.SelectedValue), oBPO, oIPInitiator);

                    Response.Redirect("~/ApprovalSupportFunction/PendingProposal.aspx");
                }
            }
        }
    }

    private bool GMREPlanAddCommentProcedure(appUsers OnlineUser, Proposal oProposal, string comment, int stand, appUsers oBPO, appUsers oInitiator)
    {
        bool success = false;

        if (stand == SupportState.iSupported)
        {
            GMREPlanForwardsIP(oProposal, oSessnx.getOnlineUser);
            success = oProposalMgt.ProposalSupportedApproved(oProposal, OnlineUser, oBPO.structUserIdx, oInitiator.structUserIdx);
            //Update Proposal Status
            oProposalMgt.UpdateProposalStatus(oProposal.m_lProposalId, (int)IPStatusReporter.ipStatusRpt.AwaitGMFin);
        }
        else if (stand == SupportState.iNotSupported)
        {
            success = oProposalMgt.ProposalNotSupported(oProposal, OnlineUser, comment);
        }

        oProposalMgt.MailCERP(oProposal, OnlineUser);
        return success;
    }

    private void GMREPlanForwardsIP(Proposal oProposal, appUsers OnlineUser)
    {
        List<structUserMailIdx> toGeneralManagers = new List<structUserMailIdx>();
        foreach (GridViewRow grdRow in grdView.Rows)
        {
            CheckBox GeneralManager = (CheckBox)grdRow.FindControl("GeneralManager");
            //Label RoleLabel = (Label)grdRow.FindControl("RoleLabel");
            //
            int iGMUserID = int.Parse(GeneralManager.Attributes["IDUSERMGT"].ToString());
            if ((GeneralManager.Checked == true) && (GeneralManager.Enabled == true))
            {
                // Check to find if the VP has been Inserted into the table previously, if found just send a reminder mail to the VP=General Manager
                SupportApproverComments oSupportApproverComments = oSupportApproverCommentMgt.objGetFunctionalSupportsApproverCommentByUserId(oProposal.m_lProposalId, iGMUserID);

                if (oSupportApproverComments.m_iUserId == iGMUserID) //This is to test if the user selected has previously received the IP 
                {
                    //Just send mail to the VPs
                    oAppUser = oAppUserMgt.objGetUserByUserId(iGMUserID);
                    toGeneralManagers.Add(oAppUser.structUserIdx);
                }
                else
                {
                    //Assign the IP to the General Manager, formmally the Vice president
                    oProposalMgt.AssignIPtoNextSupportApprover(iGMUserID, oProposal.m_lProposalId, (int)appUsersRoles.userRole.VP);

                    oAppUser = oAppUserMgt.objGetUserByUserId(iGMUserID);
                    toGeneralManagers.Add(oAppUser.structUserIdx);
                }
            }
        }
        sendMail oSendMail = new sendMail(oSessnx.getOnlineUser.structUserIdx);
        oSendMail.MailVicePresident(toGeneralManagers, oProposal.m_sProj_Title, oProposal.m_sProj_Num);
    }

    protected void closeButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ApprovalSupportFunction/PendingProposal.aspx");
    }

    protected void SupportStandDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (SupportStandDropDownList.SelectedValue == SupportState.iNotSupported.ToString()) grdView.Enabled = false;
        else grdView.Enabled = true;
    }

    protected void sendReminderButton_Click(object sender, EventArgs e)
    {
        bool success = false;
        sendMail oSendMail = new sendMail();
        List<structUserMailIdx> ToEmail = new List<structUserMailIdx>();
        appUserMgt oAppUserMgt = new appUserMgt();

        foreach (GridViewRow grdRow in grdView.Rows)
        {
            CheckBox Reminder = (CheckBox)grdRow.FindControl("Reminder");
            Label RoleLabel = (Label)grdRow.FindControl("RoleLabel");
           
            int VPUserID = int.Parse(Reminder.Attributes["IDUSERMGT"].ToString());
            if (Reminder.Checked == true)
            {
                //Just send mail to the Selected VPs
                ToEmail.Add(oAppUserMgt.objGetUserByUserId(VPUserID).structUserIdx);
                success = oSendMail.MailVicePresident(ToEmail, oProposal.m_sProj_Title, oProposal.m_sProj_Num);
                if (success == true)
                {
                    ajaxWebExtension.showJscriptAlert(Page, this, "Reminder Mail successfully sent.");
                }
            }
        }
    }

    protected void VPDetailedSupportLinkButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ApprovalSupportFunction/VPSupportDetails.aspx?Proposalid=" + oProposal.m_lProposalId + "");
    }
}