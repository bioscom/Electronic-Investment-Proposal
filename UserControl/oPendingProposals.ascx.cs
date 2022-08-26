using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class UserControl_oPendingProposals : aspnetUserControl
{
    string CurrentSortExpression = "";
    ProposalMgt oProposalMgt = new ProposalMgt();

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    public void Page_Init()
    {
        if (!IsPostBack)
        {
            gridSorting(CurrentSortExpression);
        }

        //if (oSessnx.getOnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.EPG_IP_Tracker)
        //{
        //    ddlApprovedProposals.Visible = true;
        //}
        //else
        //{
        //    ddlApprovedProposals.Visible = false;
        //}
    }

    private void gridSorting(string sortExpression)
    {
        DataTable dt = new DataTable();
        try
        {
            Proposal proposal = new Proposal();
            CurrentSortExpression = sortExpression;

            dt = oProposalMgt.dtGetPendingProposals();
            IPStatusReporter.IPStatusReport(oSessnx.getOnlineUser, IPTrackRegGridView, dt, sortExpression);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    protected void IPTrackRegGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        sendMail oSendMail = new sendMail(oSessnx.getOnlineUser.structUserIdx);
        //Proposal proposal = new Proposal();

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
                    //TODO: complete work here
                    //string sql = oProposalMgt.ProposalSupportApprovalDetails(ProposalID);
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
                    Proposal oProposal = oProposalMgt.objGetProposalById(lProposalId); //oProposalMgt.dtGetProposalByProposalId(lProposalId);
                    //this is where the Logic lies

                    bool bRet = oSendMail.ForwardIP(oProposal, oSessnx.getOnlineUser.structUserIdx);
                    if (bRet) ajaxWebExtension.showJscriptAlert(Page, this, "Proposal successfully forwarded.");
                    else ajaxWebExtension.showJscriptAlert(Page, this, "Proposal not forwarded, please try again");
                }

                if (ButtonClicked == "Remark")
                {
                    LinkButton lbRemark = (LinkButton)IPTrackRegGridView.Rows[index].FindControl("RemarkLinkButton");
                    long lProposalId = long.Parse(lbRemark.Attributes["PROPOSALID"].ToString());
                    Response.Redirect("~/BPO/Remark.aspx" + "?Proposalid=" + lProposalId, false);
                }

                if (ButtonClicked == "Reroute")
                {
                    LinkButton lbReroute = (LinkButton)IPTrackRegGridView.Rows[index].FindControl("RerouteLinkButton");
                    long lProposalId = long.Parse(lbReroute.Attributes["PROPOSALID"].ToString());
                    Response.Redirect("~/IPAdministrator/RerouteIP.aspx" + "?Proposalid=" + lProposalId, false);
                }

                if (ButtonClicked == "Discontinue")
                {
                    LinkButton lbDiscontinue = (LinkButton)IPTrackRegGridView.Rows[index].FindControl("DiscontinueLinkButton");
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
                    LinkButton delete = (LinkButton)IPTrackRegGridView.Rows[index].FindControl("DeleteProposalLinkButton");
                    long lProposalId = long.Parse(delete.Attributes["PROPOSALID"].ToString());
                    oProposalMgt.DeleteProposal(lProposalId);

                    //TODO: also complete work here
                    //LoadProposals(oProposalMgt.IPTrackingLoadProposal(), CurrentSortExpression);
                }
            }
        }
        catch (Exception ex)
        {
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
    protected void ddlApprovedProposals_SelectedIndexChanged(object sender, EventArgs e)
    {
        gridSorting(CurrentSortExpression);
    }
}