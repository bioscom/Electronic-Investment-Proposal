using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Common_FunctionalIPS : aspnetPage
{
    string CurrentSortExpression = "";
    
    appUserMgt oAppUserMgt = new appUserMgt();
    ProposalMgt oProposalMgt = new ProposalMgt();
    ProposalApprovalDetailsMgt oProposalApprovalDetailsMgt = new ProposalApprovalDetailsMgt();

    protected void Page_Load(object sender, EventArgs e)
    {
        bool bRet = false;
        try
        {
            string[] sPageAccess = { appUsersRoles.userRole.Administrator.ToString() };
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
            CurrentSortExpression = "IDPROPOSAL";
            gridSorting(CurrentSortExpression);
        }
    }

    private void gridSorting(string sortExpression)
    {
        try
        {
            CurrentSortExpression = sortExpression;
            currentPageLabel.Text = "Current Page: " + (IPTrackRegGridView.PageIndex + 1).ToString();
            //TODO: please come here and sort out the line below
            //LoadProposals(oProposalMgt.IPTrackingLoadProposalFunctionalPlanner(CurrentUser), CurrentSortExpression);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    private void LoadProposals(DataTable Proposal, string sortExpression)
    {
        IPStatusReporter.IPStatusReport(oSessnx.getOnlineUser, IPTrackRegGridView, Proposal, sortExpression);
    }

    protected void IPTrackRegGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string ButtonClicked = e.CommandName; //Determines which button was clicked (stores the name of each button)

        try
        {
            if (ButtonClicked == "Sort")
            {
                CurrentSortExpression = e.CommandArgument.ToString();
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
                    //string sql = oProposalMgt.ProposalSupportApprovalDetails(ProposalID);
                    //oProposalApprovalDetailsMgt
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

                    sendMail oSendMail = new sendMail(oSessnx.getOnlineUser.structUserIdx);

                    bool success = oSendMail.ForwardIP(oProposal, oSessnx.getOnlineUser.structUserIdx);
                    if (success == true)
                    {
                        ajaxWebExtension.showJscriptAlert(Page, this, "Proposal successfully forwarded.");
                    }
                }

                if (ButtonClicked == "DeleteProposal")
                {
                    LinkButton delete = (LinkButton)IPTrackRegGridView.Rows[index].FindControl("DeleteProposalLinkButton");
                    long lProposalId = long.Parse(delete.Attributes["PROPOSALID"].ToString());
                    oProposalMgt.DeleteProposal(lProposalId);

                    //TODO: Line below to be sorted out
                    //LoadProposals(proposal.IPTrackingLoadProposal(), "IDPROPOSAL");
                }
            }
        }
        catch (Exception ex)
        {
            //MessageBox.Show(ex.Message.ToString());
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    protected void IPTrackRegGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        IPTrackRegGridView.PageIndex = e.NewPageIndex;

        gridSorting(CurrentSortExpression);

    }

    protected void IPTrackRegGridView_Sorting(object sender, GridViewSortEventArgs e)
    {
        gridSorting(CurrentSortExpression);
    }

    protected void IPTrackRegGridView_Sorted(object sender, EventArgs e)
    {

    }
}