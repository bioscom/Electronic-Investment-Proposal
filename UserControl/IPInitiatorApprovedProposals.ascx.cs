using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class UserControl_IPInitiatorApprovedProposals : aspnetUserControl
{
    string CurrentSortExpression = "";
    ProposalMgt oProposalMgt = new ProposalMgt();

    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    protected void grdView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string ButtonClicked = e.CommandName; //Determines which button was clicked (stores the name of each button)
        DataSorter sortMe = new DataSorter();

        try
        {
            if ((ButtonClicked == "Sort") || (ButtonClicked == "Page"))
            {
                CurrentSortExpression = sortMe.MySortExpression(e);

                //int result;
                //if (Int32.TryParse(e.CommandArgument.ToString(), out result) == false)
                //{
                //    Session["SortExpression"] = e.CommandArgument.ToString();
                //}
                //CurrentSortExpression = Session["SortExpression"].ToString();
            }
            else
            {
                int index = Convert.ToInt32(e.CommandArgument); //Command Argument stores the index of each row
                if (ButtonClicked == "ViewStatus")
                {
                    LinkButton lbViewStatus = (LinkButton)grdView.Rows[index].FindControl("ViewStatusLinkButton");
                    long lProposalId = long.Parse(lbViewStatus.Attributes["PROPOSALID"].ToString());
                    Response.Redirect("~/Common/ViewProposalStatus.aspx" + "?Proposalid=" + lProposalId, false);
                }

                if (ButtonClicked == "ApprovalDetails")
                {
                    //TODO: ???
                    //string sql = Proposal.ProposalSupportApprovalDetails(ProposalID);
                }

                if (ButtonClicked == "ViewOriginalProposal")
                {
                    LinkButton lbOriginalProposal = (LinkButton)grdView.Rows[index].FindControl("OriginalProposalLinkButton");
                    long lProposalId = long.Parse(lbOriginalProposal.Attributes["PROPOSALID"].ToString());
                    Response.Redirect("~/ViewProposal.aspx" + "?Proposalid=" + lProposalId, false);
                }

                // Forward Proposal to My Email

                if (ButtonClicked == "forwardProposal")
                {
                    LinkButton lbForwardProposal = (LinkButton)grdView.Rows[index].FindControl("forwardProposalLinkButton");
                    long ProposalID = long.Parse(lbForwardProposal.Attributes["PROPOSALID"].ToString());

                    Proposal MyProposal = oProposalMgt.objGetProposalById(ProposalID);

                    sendMail oSendMail = new sendMail();
                    bool success = oSendMail.ForwardIP(MyProposal, oSessnx.getOnlineUser.structUserIdx);
                    if (success == true)
                    {
                        ajaxWebExtension.showJscriptAlert(Page, this, "Proposal successfully forwarded.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    protected void grdView_Sorting(object sender, GridViewSortEventArgs e)
    {
        MyApprovedProposals(oSessnx.getOnlineUser);
    }

    protected void grdView_Sorted(object sender, EventArgs e)
    {

    }

    public void MyApprovedProposals(appUsers OnlineUser)
    {
        try
        {
            Proposal oProposal = new Proposal();
            DataTable dt = oProposalMgt.dtMyApprovedProposals(OnlineUser);
            IPStatusReporter.IPStatusReport(OnlineUser, grdView, dt, CurrentSortExpression);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    protected void grdView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            Proposal proposal = new Proposal();
            grdView.PageIndex = e.NewPageIndex;
            //TODO: the code here needs some adjustment
            //int iYear = dtDate.DateSelectedDate.Year;

            DateTime Me = new DateTime();
            if (!DateTime.TryParse(dtDate.DateSelectedDate.ToString(), out Me))
            {
                MyApprovedProposals(oSessnx.getOnlineUser);
            }
            else
            {
                MyApprovedProposalByYear(oSessnx.getOnlineUser);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    protected void viewButton_Click(object sender, EventArgs e)
    {
        MyApprovedProposalByYear(oSessnx.getOnlineUser);
    }

    private void MyApprovedProposalByYear(appUsers OnlineUser)
    {
        try
        {
            Proposal oProposal = new Proposal();
            int iYear = dtDate.DateSelectedDate.Year;
            IPStatusReporter.IPStatusReport(OnlineUser, grdView, oProposalMgt.MyApprovedProposalByYearOfApproval(iYear, OnlineUser), CurrentSortExpression);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }
}