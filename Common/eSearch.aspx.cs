using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Common_eSearch : aspnetPage
{
    appUserMgt oAppUserMgt = new appUserMgt();
    ProposalMgt oProposalMgt = new ProposalMgt();
    string CurrentSortExpression = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        bool bRet = false;
        try
        {
            string[] sPageAccess = { appUsersRoles.userRole.Administrator.ToString(), appUsersRoles.userRole.Auditor.ToString() };
            appUsersRoles oAccess = new appUsersRoles();
            bRet = oAccess.grantPageAccess(sPageAccess, this.oSessnx.getOnlineUser.m_eUserRole);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
        if (!bRet) Response.Redirect("~/Profiles/pageDenied.aspx");

        if (Request.QueryString["ProposalName"] != null)
        {
            string ProposalName = Request.QueryString["ProposalName"].ToString();
            eSearch(ProposalName, CurrentSortExpression);
        }
    }

    private void eSearch(string projectNumber, string sortExpression)
    {
        try
        {
            if (oSessnx.getOnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.CERP)
            {
                //TODO: Complete job here
                //LoadProposals(oProposalMgt.IPTrackingLoadEPGProposalByProjName(projectNumber), CurrentSortExpression);
                IPStatusReporter.IPStatusReport(oSessnx.getOnlineUser, IPTrackRegGridView, oProposalMgt.dtGetProposalByProjTitleOrNumber(projectNumber), sortExpression);
            }
            else
            {
                IPStatusReporter.IPStatusReport(oSessnx.getOnlineUser, IPTrackRegGridView, oProposalMgt.dtGetProposalByProjTitleOrNumber(projectNumber), sortExpression);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }
    protected void IPTrackRegGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void IPTrackRegGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        sendMail oSendMail = new sendMail(oSessnx.getOnlineUser.structUserIdx);
        
        string ButtonClicked = e.CommandName; //Determines which button was clicked (stores the name of each button)
        DataSorter SortMe = new DataSorter();

        try
        {
            if ((ButtonClicked == "Sort") || (ButtonClicked == "Page"))
            {
                CurrentSortExpression = SortMe.MySortExpression(e);
            }
            else
            {
                int index = Convert.ToInt32(e.CommandArgument); //Command Argument stores the index of each row
                if (ButtonClicked == "ViewStatus")
                {
                    LinkButton lbViewStatus = (LinkButton)IPTrackRegGridView.Rows[index].FindControl("ViewStatusLinkButton");
                    long lProposalId = long.Parse(lbViewStatus.Attributes["PROPOSALID"].ToString());
                    Response.Redirect("~/Common/ViewProposalStatus.aspx" + "?Proposalid=" + lProposalId, false);
                }

                if (ButtonClicked == "ApprovalDetails")
                {
                    //TODO: solve the problem here
                    //string sql = db.ProposalSupportApprovalDetails(ProposalID);
                }

                if (ButtonClicked == "ViewOriginalProposal")
                {
                    LinkButton lbOriginalProposal = (LinkButton)IPTrackRegGridView.Rows[index].FindControl("OriginalProposalLinkButton");
                    long lProposalId = long.Parse(lbOriginalProposal.Attributes["PROPOSALID"].ToString());
                    Response.Redirect("~/ViewProposal.aspx" + "?Proposalid=" + lProposalId, false);
                }

                // Forward Proposal to My Email

                if (ButtonClicked == "forwardProposal")
                {
                    LinkButton lbForwardProposal = (LinkButton)IPTrackRegGridView.Rows[index].FindControl("forwardProposalLinkButton");
                    long lProposalId = long.Parse(lbForwardProposal.Attributes["PROPOSALID"].ToString());

                    Proposal oProposal = oProposalMgt.objGetProposalById(lProposalId);

                    bool success = oSendMail.ForwardIP(oProposal, oSessnx.getOnlineUser.structUserIdx);
                    if (success == true)
                    {
                        ajaxWebExtension.showJscriptAlert(Page, this, "Proposal successfully forwarded.");
                    }
                }

                //if (ButtonClicked == "DeleteProposal")
                //{
                //    LinkButton delete = (LinkButton)IPTrackRegGridView.Rows[index].FindControl("DeleteProposalLinkButton");
                //    long lProposalId = long.Parse(delete.Attributes["PROPOSALID"].ToString());
                //    oProposalMgt.DeleteProposal(lProposalId);

                //    //TODO: findout what you need to do here
                //    ///oProposalMgt.dtGetProposalByProposalId(lProposalId);
                //    //LoadProposals(proposal.IPTrackingLoadProposal(), CurrentSortExpression);
                //}
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }
    protected void IPTrackRegGridView_Sorted(object sender, EventArgs e)
    {

    }
    protected void IPTrackRegGridView_Sorting(object sender, GridViewSortEventArgs e)
    {
        
    }
}
