using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_oApprovedProposals : aspnetUserControl
{
    ProposalMgt oProposalMgt = new ProposalMgt();
    appUserMgt oAppUserMgt = new appUserMgt();

    string CurrentSortExpression = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    public void Page_Init()
    {
        if (!IsPostBack)
        {
            //string sql = StoredProcedure.getDistinctYear();
            //sql = sql.Replace(":DOC_STAND", ((int)IPStatusReporter.ipStatusRpt.Approved).ToString());

            //oProposalMgt.DropDownYearFiller(yearddl, sql);
            gridSorting(CurrentSortExpression);
        }
    }

    private void MyApprovedProposalByYear(appUsers OnlineUser)
    {
        try
        {
            LoadProposalsByYearOfApproval(oProposalMgt.IPTrackingLoadApprovedProposalByYearOfApproval(dtDate.DateSelectedDate.Year), CurrentSortExpression);
            //Proposal oProposal = new Proposal();
            //IPStatusReporter.IPStatusReport(OnlineUser, IPTrackRegGridView, oProposalMgt.MyApprovedProposalByYearOfApproval(iYear, OnlineUser), CurrentSortExpression);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    private void gridSorting(string sortExpression)
    {
        try
        {
            CurrentSortExpression = sortExpression;
            DataTable dt = oProposalMgt.dtGetApprovedProposal();
            LoadProposals(dt, CurrentSortExpression);

            DateApproved();

            //if (oSessnx.getOnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.EPG_IP_Tracker)
            //{
            //    //TODO: sort out this line and remove the hard code
            //    //LoadProposals(oProposalMgt.IPTrackingLoadEPGProposal(2), CurrentSortExpression);
            //    LoadProposals(oProposalMgt.dtGetApprovedProposal(), CurrentSortExpression);
            //}
            //else
            //{
            //    LoadProposals(oProposalMgt.dtGetApprovedProposal(), CurrentSortExpression);
            //}
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    private void DateApproved()
    {
        ProposalApprovalDetailsMgt oProposalApprovalDetailsMgt = new ProposalApprovalDetailsMgt();

        foreach (GridViewRow grdRow in IPTrackRegGridView.Rows)
        {
            LinkButton lnkProposal = (LinkButton)grdRow.FindControl("ViewStatusLinkButton");
            long lProposalId = long.Parse(lnkProposal.Attributes["PROPOSALID"].ToString());
            Proposal oProposal = oProposalMgt.objGetProposalById(lProposalId);

            Label lbApprover = (Label)grdRow.FindControl("labelApprovedby");
            Label lbDateApproved = (Label)grdRow.FindControl("labelDateApproved");

            int iUserId = int.Parse(lbApprover.Attributes["IDUSERMGT"].ToString());

            ProposalApprovalDetails oDetails = oProposalApprovalDetailsMgt.objGetProposalSupportDetailsByProposalUserId(iUserId, lProposalId);
            lbDateApproved.Text = oDetails.m_sDateComment;
        }
    }

    protected void IPTrackRegGridView_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    LinkButton lbViewStatus = (LinkButton)IPTrackRegGridView.Rows[index].FindControl("ViewStatusLinkButton");
                    long lProposalId = long.Parse(lbViewStatus.Attributes["PROPOSALID"].ToString());
                    Response.Redirect("~/Common/ViewProposalStatus.aspx" + "?Proposalid=" + lProposalId, false);
                }

                if (ButtonClicked == "ApprovalDetails")
                {
                    //TODO: find solution here
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
                    //this is where the Logic lies

                    sendMail oSendMail = new sendMail(oSessnx.getOnlineUser.structUserIdx);
                    Proposal oProposal = oProposalMgt.objGetProposalById(lProposalId);

                    bool bRet = oSendMail.ForwardIP(oProposal, oSessnx.getOnlineUser.structUserIdx);
                    if (bRet)
                    {
                        ajaxWebExtension.showJscriptAlertCx(Page, this, "Proposal successfully forwarded.");
                    }
                    else
                    {

                    }
                }

                if (ButtonClicked == "DeleteProposal")
                {
                    LinkButton delete = (LinkButton)IPTrackRegGridView.Rows[index].FindControl("DeleteProposalLinkButton");
                    long lProposalId = long.Parse(delete.Attributes["PROPOSALID"].ToString());
                    oProposalMgt.DeleteProposal(lProposalId);

                    //TODO: Complete the line below
                    //LoadProposals(oProposalMgt.IPTrackingLoadProposal(), CurrentSortExpression);
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    private void LoadProposals(DataTable dtProposal, string sortExpression)
    {
        IPStatusReporter.IPStatusReport(oSessnx.getOnlineUser, IPTrackRegGridView, dtProposal, sortExpression);
    }

    private void LoadProposalsByYearOfApproval(DataTable dtProposal, string sortExpression)
    {
        IPStatusReporter.IPStatusReport(oSessnx.getOnlineUser, IPTrackRegGridView, dtProposal, sortExpression);
        DateApproved();
    }

    protected void IPTrackRegGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        IPTrackRegGridView.PageIndex = e.NewPageIndex;
        if (dtDate.DateSelectedDate.Year == DateTime.Today.Year)
        {
            gridSorting(CurrentSortExpression);
        }
        else
        {
            LoadProposalsByYearOfApproval(oProposalMgt.IPTrackingLoadApprovedProposalByYearOfApproval(dtDate.DateSelectedDate.Year), CurrentSortExpression);
        }
    }

    //protected void searchButton_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (CurrentUser.iUSERROLESID == eipUserRoles.iEPGIPTracker)
    //        {
    //            LoadProposals(proposal.IPTrackingLoadEPGProposalByProjName(txtProjectNumber.Text.ToUpper().Trim()), CurrentSortExpression);
    //        }
    //        else
    //        {
    //            LoadProposals(proposal.IPTrackingLoadProposalByProjName(txtProjectNumber.Text.ToUpper().Trim()), CurrentSortExpression);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
    //    }
    //}

    protected void IPTrackRegGridView_Sorting(object sender, GridViewSortEventArgs e)
    {
        gridSorting(CurrentSortExpression);
    }

    protected void IPTrackRegGridView_Sorted(object sender, EventArgs e)
    {

    }

    protected void yearddl_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProposalsByYearOfApproval(oProposalMgt.IPTrackingLoadApprovedProposalByYearOfApproval(dtDate.DateSelectedDate.Year), CurrentSortExpression);
    }
    protected void viewButton_Click(object sender, EventArgs e)
    {
        //MyApprovedProposalByYear(oSessnx.getOnlineUser);
        LoadProposalsByYearOfApproval(oProposalMgt.IPTrackingLoadApprovedProposalByYearOfApproval(dtDate.DateSelectedDate.Year), CurrentSortExpression);
    }
}