using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_oDiscontinuedProposals : aspnetUserControl
{
    appUserMgt oAppUserMgt = new appUserMgt();
    ProposalMgt oProposalMgt = new ProposalMgt();

    string CurrentSortExpression = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    public void Page_Init()
    {
        gridSorting(CurrentSortExpression);
    }

    private void gridSorting(string sortExpression)
    {
        try
        {
            Proposal proposal = new Proposal();
            CurrentSortExpression = sortExpression;
            currentPageLabel.Text = "Current Page: " + (pendingProposalGridView.PageIndex + 1).ToString();
            //TODO: see to this
            //LoadDiscontinuedProposals(oProposalMgt.DiscontinuedProposal(), CurrentSortExpression);
            IPStatusReporter.IPStatusReport(oSessnx.getOnlineUser, pendingProposalGridView, oProposalMgt.dtGetDiscontinuedProposal(), sortExpression);
        }
        catch (Exception ex)
        {
            //MessageBox.Show(ex.Message.ToString());
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    protected void pendingProposalGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string ProposalID = "";
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

                if (ButtonClicked == "ViewOriginalProposal")
                {
                    LinkButton lbOriginalProposal = (LinkButton)pendingProposalGridView.Rows[index].FindControl("ViewOriginalProposalLinkButton");
                    ProposalID = lbOriginalProposal.Attributes["PROPOSALID"].ToString();
                    Response.Redirect("~/ViewProposal.aspx" + "?Proposalid=" + ProposalID, false);
                }

                if (ButtonClicked == "ViewStatus")
                {
                    LinkButton lbCheckComment = (LinkButton)pendingProposalGridView.Rows[index].FindControl("ViewStatusLinkButton");
                    ProposalID = lbCheckComment.Attributes["PROPOSALID"].ToString();
                    Response.Redirect("~/Common/ViewProposalStatus.aspx" + "?Proposalid=" + ProposalID, false);
                }

                if (ButtonClicked == "Reactivate")
                {

                    LinkButton lbReactivate = (LinkButton)pendingProposalGridView.Rows[index].FindControl("ReactivateLinkButton");
                    long iProposalId = long.Parse(lbReactivate.Attributes["PROPOSALID"].ToString());
                    //Call the class that discontinues proposal and pass the ProposalID as the parameter
                    DiscontinueProposal thisProposal = new DiscontinueProposal();
                    bool success = thisProposal.ReactivateProposal(iProposalId, oSessnx.getOnlineUser);
                    if (success == true)
                    {
                        string oMessage = "Proposal successfully reactivated. All functional support and approver, for the discontinued proposal, have been notified of the reactivation.";
                        ajaxWebExtension.showJscriptAlert(Page, this, oMessage);

                    }
                    //Response.Redirect("~/IPAdministrator/RerouteIP.aspx" + "?Proposalid=" + ProposalID, false);
                }

                if (ButtonClicked == "DeleteProposal")
                {
                    LinkButton delete = (LinkButton)pendingProposalGridView.Rows[index].FindControl("DeleteProposalLinkButton");
                    long iProposalId = long.Parse(delete.Attributes["PROPOSALID"].ToString());
                    oProposalMgt.DeleteProposal(iProposalId);

                    gridSorting(CurrentSortExpression);
                }
            }
        }
        catch (Exception ex)
        {
            //MessageBox.Show(ex.Message.ToString());
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n\n" + ex.StackTrace + "\n\n" + ex.Message.ToString());
        }
    }

    protected void pendingProposalGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        pendingProposalGridView.PageIndex = e.NewPageIndex;
        gridSorting(CurrentSortExpression);
    }

    protected void pendingProposalGridView_Sorting(object sender, GridViewSortEventArgs e)
    {
        gridSorting(CurrentSortExpression);
    }
}