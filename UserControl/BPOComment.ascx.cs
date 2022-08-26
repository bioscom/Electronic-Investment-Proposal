using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_BPOComment : aspnetUserControl
{
    appUserMgt oAppUserMgt = new appUserMgt();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void initBPoComment(long lProposalId)
    {
        ProposalMgt oProposalMgt = new ProposalMgt();
        oProposalMgt.FillSupportState(SupportStandDropDownList);

        Proposal proposal = oProposalMgt.objGetProposalById(lProposalId);
        xproposalIDHiddenField.Value = lProposalId.ToString();

        SupportApproverCommentMgt oSupportApproverCommentMgt = new SupportApproverCommentMgt();
        List<SupportApproverComments> oComments = oSupportApproverCommentMgt.lstGetFunctionalSupportsApproverComments(lProposalId);
        foreach (SupportApproverComments oComment in oComments)
        {
            if (oComment.m_iUserRoleId == (int)appUsersRoles.userRole.Business_Process_Owner)
            {
                SupportStandDropDownList.SelectedValue = oComment.m_iStand.ToString();
                commentTextBox.Text = oComment.m_sComments;
                if (oComment.m_iStand == SupportState.iSupported)
                {
                    SaveButton.Enabled = false;
                }
            }
        }
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        try
        {
            ProposalMgt oProposalMgt = new ProposalMgt();
            Proposal oProposal = oProposalMgt.objGetProposalById(long.Parse(xproposalIDHiddenField.Value));

            SupportApproverCommentMgt oSupportApproverCommentMgt = new SupportApproverCommentMgt();

            if ((string.IsNullOrEmpty(commentTextBox.Text)) && (SupportStandDropDownList.SelectedValue == SupportState.iNotSupported.ToString()))
            {
                string mssg = "Please, enter reason(s) why Not Supported in the comment box.";
                ajaxWebExtension.showJscriptAlert(Page, this, mssg);
            }
            else
            {
                sendMail oSendMail = new sendMail(oSessnx.getOnlineUser.structUserIdx);
                int iStand = Convert.ToInt32(SupportStandDropDownList.SelectedValue);
                string sComment = commentTextBox.Text;

                bool bRet = oSupportApproverCommentMgt.AddComment(oSessnx.getOnlineUser, oProposal, sComment, iStand);
                if (int.Parse(SupportStandDropDownList.SelectedValue) == SupportState.iSupported)
                {
                    if (bRet)
                    {
                        oProposalMgt.AuditTrail(oSessnx.getOnlineUser, iStand, sComment, oProposal.m_lProposalId);
                        oProposalMgt.DateLastActioned(oProposal.m_lProposalId, oSessnx.getOnlineUser);
                        oProposalMgt.UpdateProposalStatus(oProposal.m_lProposalId, (int)IPStatusReporter.ipStatusRpt.BPOYetToFunctionalSupport);

                        //Send a mail to Capital Expenditure Review Panel User, that BPO has supported an IP >= $10mln
                        bRet = oProposalMgt.MailCERP(oProposal, oSessnx.getOnlineUser);
                        if (bRet)
                        {

                            string oMessage = "Support successful! Forward IP to functional supports.";
                            ajaxWebExtension.showJscriptAlertCx(Page, this, oMessage);
                            mssgLabel.Text = oMessage;

                            Response.Redirect("~/BPO/Remark.aspx?Proposalid=" + long.Parse(xproposalIDHiddenField.Value));
                        }
                        else
                        {
                            string oMessage = "CERP not found, contact the Administrator to setup CERP.";
                            ajaxWebExtension.showJscriptAlertCx(Page, this, oMessage);
                        }
                    }
                    else
                    {
                        string oMessage = "Support not successful, please try again later.";
                        ajaxWebExtension.showJscriptAlertCx(Page, this, oMessage);
                        mssgLabel.Text = oMessage;
                    }
                }
                else if (int.Parse(SupportStandDropDownList.SelectedValue) == SupportState.iNotSupported)
                {
                    if (bRet)
                    {
                        appUsers oIPInitiator = oAppUserMgt.objGetUserByUserId(oProposal.m_iUserId);
                        oProposalMgt.UpdateProposalStatus(oProposal.m_lProposalId, (int)IPStatusReporter.ipStatusRpt.BPONotSupported);
                        oSendMail.IPNotSupported(oIPInitiator.structUserIdx, oSessnx.getOnlineUser.structUserIdx, oProposal.m_sProj_Title, commentTextBox.Text,
                            oSessnx.getOnlineUser.m_sFullName, appUsersRoles.userRoleDesc(oSessnx.getOnlineUser.m_eUserRole), oProposal.m_sProj_Num);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n\n" + ex.StackTrace + "\n\n" + ex.Message.ToString());
        }
    }
}
