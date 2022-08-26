using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class BOM_Default : aspnetPage
{
    DataSet ds = new DataSet();
    appUsers oAppUsers = new appUsers();
    ProposalMgt oProposalMgt = new ProposalMgt();

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

        bindData();

        if (ds.Tables[0].Rows.Count == 0)
        {
            ajaxWebExtension.showJscriptAlert(Page, this, "No Record Found!!!");
        }
        else if (ds.Tables[0].Rows.Count > 0) //Display LinkButtons to be visible according to the Proposal Logic
        {
            foreach (GridViewRow grd in myProposalGridView.Rows)
            {

            }
        }
    }

    protected void myProposalGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string ButtonClicked = e.CommandName; //Determines which button was clicked (stores the name of each button)
        int index = Convert.ToInt32(e.CommandArgument); //Command Argument stores the index of each row

        //4. View Proposal Status

        if (ButtonClicked == "ViewProposalStatus")
        {
            LinkButton lbViewProposalStatus = (LinkButton)myProposalGridView.Rows[index].FindControl("ViewProposalStatusLinkButton");
            long lProposalId = long.Parse(lbViewProposalStatus.Attributes["PROPOSALID"].ToString());
            Response.Redirect("~/Common/ViewProposalStatus.aspx" + "?Proposalid=" + lProposalId, false);
        }

        // Forward Proposal to My Email

        if (ButtonClicked == "forwardProposal")
        {
            LinkButton lbForwardProposal = (LinkButton)myProposalGridView.Rows[index].FindControl("forwardProposalLinkButton");
            long lProposalId = long.Parse(lbForwardProposal.Attributes["PROPOSALID"].ToString());
            //this is where the Logic lies

            Proposal oProposal = oProposalMgt.objGetProposalById(lProposalId);

            //Label lbDateInit = (Label)myProposalGridView.Rows[index].FindControl("labelDateInitiated");
            //Label lbDateForwarded = (Label)myProposalGridView.Rows[index].FindControl("labelDateForwarded");
            //Label lbProjectNumber = (Label)myProposalGridView.Rows[index].FindControl("labelProjectNumber");
            //Label lbProjectTitle = (Label)myProposalGridView.Rows[index].FindControl("labelProjectTitle");
            //Label lbInitiator = (Label)myProposalGridView.Rows[index].FindControl("labelInitiator");

            sendMail oSendMail = new sendMail(oSessnx.getOnlineUser.structUserIdx);
            oSendMail.ForwardIP(oProposal, oSessnx.getOnlineUser.structUserIdx);
        }

        //6. View Original Proposal

        if (ButtonClicked == "ViewOriginalProposal")
        {
            LinkButton lbOriginalProposal = (LinkButton)myProposalGridView.Rows[index].FindControl("OriginalProposalLinkButton");
            long lProposalId = long.Parse(lbOriginalProposal.Attributes["PROPOSALID"].ToString());
            Response.Redirect("~/ViewProposal.aspx" + "?Proposalid=" + lProposalId, false);
        }
    }

    protected void myProposalGridView_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void myProposalGridView_Load(object sender, EventArgs e)
    {

    }

    protected void myProposalGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        myProposalGridView.PageIndex = e.NewPageIndex;
        bindData();
        //myProposalGridView.DataBind();
    }


    private void bindData()
    {
        DataTable dt = db.GetBOMDetails(oSessnx.getOnlineUser.m_sFullName);

        myProposalGridView.DataSource = dt;
        myProposalGridView.DataBind();
    }
}

