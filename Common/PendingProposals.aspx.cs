using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class Common_PendingProposals : aspnetPage
{
    SupportApprovalStatus SupportApproval = new SupportApprovalStatus();
    ProposalMgt oProposalMgt = new ProposalMgt();
    appUserMgt oAppUserMgt = new appUserMgt();

    string CurrentSortExpression = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            gridSorting(CurrentSortExpression);
        }
    }

    private void gridSorting(string sortExpression)
    {
        try
        {
            CurrentSortExpression = sortExpression;
            currentPageLabel.Text = "Current Page: " + (pendingProposalGridView.PageIndex + 1).ToString();
            LoadProposals(CurrentSortExpression);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    protected void pendingProposalGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
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

                if (ButtonClicked == "ViewOriginalProposal")
                {
                    LinkButton lbOriginalProposal = (LinkButton)pendingProposalGridView.Rows[index].FindControl("ViewOriginalProposalLinkButton");
                    long lProposalId = long.Parse(lbOriginalProposal.Attributes["PROPOSALID"].ToString());
                    Response.Redirect("~/ViewProposal.aspx" + "?Proposalid=" + lProposalId, false);
                }

                if (ButtonClicked == "ViewStatus")
                {
                    LinkButton lbCheckComment = (LinkButton)pendingProposalGridView.Rows[index].FindControl("ViewStatusLinkButton");
                    long lProposalId = long.Parse(lbCheckComment.Attributes["PROPOSALID"].ToString());
                    Response.Redirect("~/Common/ViewProposalStatus.aspx" + "?Proposalid=" + lProposalId, false);
                }

                if (ButtonClicked == "Remark")
                {
                    LinkButton lbRemark = (LinkButton)pendingProposalGridView.Rows[index].FindControl("RemarkLinkButton");
                    long lProposalId = long.Parse(lbRemark.Attributes["PROPOSALID"].ToString());
                    Response.Redirect("~/BPO/Remark.aspx" + "?Proposalid=" + lProposalId, false);
                }

                if (ButtonClicked == "Reroute")
                {
                    LinkButton lbReroute = (LinkButton)pendingProposalGridView.Rows[index].FindControl("RerouteLinkButton");
                    long lProposalId = long.Parse(lbReroute.Attributes["PROPOSALID"].ToString());
                    Response.Redirect("~/IPAdministrator/RerouteIP.aspx" + "?Proposalid=" + lProposalId, false);
                }

                if (ButtonClicked == "Discontinue")
                {
                    LinkButton lbDiscontinue = (LinkButton)pendingProposalGridView.Rows[index].FindControl("DiscontinueLinkButton");
                    long lProposalId = long.Parse(lbDiscontinue.Attributes["PROPOSALID"].ToString());
                    //Call the class that discontinues proposal and pass the ProposalID as the parameter
                    DiscontinueProposal thisProposal = new DiscontinueProposal();
                    bool success = thisProposal.DiscontinueProposals(lProposalId, oSessnx.getOnlineUser);
                    if (success == true)
                    {
                        string oMessage = "Proposal successfully discontinued. All functional support and approver, for the discontinued proposal, have been notified.";
                        ajaxWebExtension.showJscriptAlert(Page, this, oMessage);
                    }
                    gridSorting(CurrentSortExpression);
                }

                if (ButtonClicked == "DeleteProposal")
                {
                    LinkButton delete = (LinkButton)pendingProposalGridView.Rows[index].FindControl("DeleteProposalLinkButton");
                    long lProposalId = long.Parse(delete.Attributes["PROPOSALID"].ToString());
                    oProposalMgt.DeleteProposal(lProposalId);

                    gridSorting(CurrentSortExpression);
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n\n" + ex.StackTrace + "\n\n" + ex.Message.ToString());
        }
    }

    protected void pendingProposalGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        pendingProposalGridView.PageIndex = e.NewPageIndex;

        LoadProposals(CurrentSortExpression);
    }

    private void LoadProposals(string sortExpression)
    {
       // if ((oSessnx.getOnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.Business_Process_Owner) ||(oSessnx.getOnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.Administrator))
       // {
            appUsers oBusinessProcessOwner = oAppUserMgt.objGetDefaultUsersByRole((int)appUsersRoles.userRole.Business_Process_Owner, DefaultRoleHolder.iDefault);
            DataTable dt = oProposalMgt.BPOPendingProposal(oBusinessProcessOwner.m_iUserId);
            IPStatusReporter.IPStatusReport(oSessnx.getOnlineUser, pendingProposalGridView, dt, sortExpression);
        //}
       // else
       // {
          //  ajaxWebExtension.showJscriptAlert(Page, this, "Pending Proposals in this case are only for Default Business Process Owner.");
        //}
    }

    protected void pendingProposalGridView_Sorting(object sender, GridViewSortEventArgs e)
    {
        gridSorting(CurrentSortExpression);
    }
}