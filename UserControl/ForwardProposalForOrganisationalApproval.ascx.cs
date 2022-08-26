using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_ForwardProposalForOrganisationalApproval : aspnetUserControl
{
    ProposalMgt oProposalMgt = new ProposalMgt();
    appUserMgt oAppUserMgt = new appUserMgt();
    ProposalApprovalDetailsMgt oProposalApprovalDetailsMgt = new ProposalApprovalDetailsMgt();

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    public void Init_Page(long lProposalId)
    {
        Proposal oProposal = oProposalMgt.objGetProposalById(lProposalId);
        proposalIDDHiddenField.Value = lProposalId.ToString();

        //Check if the Business Process Owner has supported the IP. If Supported then enable the controls on this page.
        List<ProposalApprovalDetails> oProposalApprovalDetails = oProposalApprovalDetailsMgt.lstGetProposalSupportDetailsByProposalId(lProposalId);
        foreach (ProposalApprovalDetails oProposalApprovalDetail in oProposalApprovalDetails)
        {
            if ((oProposalApprovalDetail.m_iUserRoleId == (int)appUsersRoles.userRole.Finance_Signature) || (oProposalApprovalDetail.m_iUserRoleId == (int)appUsersRoles.userRole.Finance_Director))
            {
                if ((oProposalApprovalDetail.m_iStand == SupportState.iSupported) || (oProposalApprovalDetail.m_iStand == SupportState.iFinanceApproval))
                {
                    //Load all General Managers
                    List<appUsers> oAppUsers = oAppUserMgt.lstGetUsersByRole((int)appUsersRoles.userRole.VP); // Note: VP == General Managers
                    foreach (appUsers oAppUser in oAppUsers)
                    {
                        approvalDropDownList.Items.Add(new ListItem(oAppUser.m_sFullName, oAppUser.m_iUserId.ToString()));
                    }

                    foreach (ProposalApprovalDetails tProposalApprovalDetail in oProposalApprovalDetails)
                    {
                        if (oProposalApprovalDetail.m_iUserRoleId == (int)appUsersRoles.userRole.VP)
                        {
                            approvalDropDownList.SelectedValue = tProposalApprovalDetail.m_iUserId.ToString();
                        }
                    }
                }
                else
                {
                    approvalSupportButton.Enabled = false;
                }
            }
        }
    }
        
    protected void approvalSupportButton_Click(object sender, EventArgs e)
    {
        bool success = false;
        List<ProposalApprovalDetails> oProposalApprovalDetails = oProposalApprovalDetailsMgt.lstGetProposalSupportDetailsByProposalId(long.Parse(proposalIDDHiddenField.Value));
        foreach (ProposalApprovalDetails oProposalApprovalDetail in oProposalApprovalDetails)
        {
            if (oProposalApprovalDetail.m_iUserRoleId == int.Parse(approvalDropDownList.SelectedValue)) //Where VP == Genrel Manager
            {
                string oMessage = "This Proposal to has been previously forwarded to " + approvalDropDownList.SelectedItem.Text + ", on " + oProposalApprovalDetail.m_sDateReceived + ", please send a reminder or reroute IP to another General Manager for final approval.";

                ajaxWebExtension.showJscriptAlertCx(Page, this, oMessage);
                mssgLabel.Text = oMessage;
            }
            else
            {
                appUsers oAppUser = oAppUserMgt.objGetUserByUserId(int.Parse(approvalDropDownList.SelectedValue));
                if (oAppUser.m_iUserRoleId == (int)appUsersRoles.userRole.VP)
                {
                    success = AssignIPToFinalApprover(approvalDropDownList, approvalSupportLabel);
                    if (success == true)
                    {
                        string oMessage = "Proposal successfully sent to " + approvalDropDownList.SelectedItem.Text + " for General Manager's approval.";
                        ajaxWebExtension.showJscriptAlertCx(Page, this, oMessage);
                        mssgLabel.Text = oMessage;
                    }
                    break;
                }
            }
        }
    }

    private bool AssignIPToFinalApprover(DropDownList theDropDownList, Label theSupport)
    {
        bool success = false;

        Proposal oProposal = oProposalMgt.objGetProposalById(long.Parse(proposalIDDHiddenField.Value));
        appUsers oInitiator = oAppUserMgt.objGetUserByUserId(oProposal.m_iUserId);
        appUsers oGeneralManager = oAppUserMgt.objGetUserByUserId(int.Parse(theDropDownList.SelectedValue));

        sendMail oSendmail = new sendMail(oSessnx.getOnlineUser.structUserIdx);
        success = oProposalMgt.AssignIPtoNextSupportApprover(oGeneralManager.m_iUserId, oProposal.m_lProposalId, (int)appUsersRoles.userRole.VP); //Where VP == General Manager
        if (success)
        {
            oProposalMgt.UpdateProposalStatus(oProposal.m_lProposalId, (int)IPStatusReporter.ipStatusRpt.AwaitsVPApprover);
            string oMessage = "Proposal forwarded for organisational approval, awaiting GM's approval.";
            ajaxWebExtension.showJscriptAlertCx(Page, this, oMessage);
            mssgLabel.Text = oMessage;

            success = oSendmail.MailApproval(oGeneralManager.structUserIdx, oInitiator.structUserIdx, oProposal.m_sProj_Title, oProposal.m_sProj_Num);
        }
        return success;
    }
}