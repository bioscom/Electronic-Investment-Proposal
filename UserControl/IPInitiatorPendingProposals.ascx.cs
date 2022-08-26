using System;
using System.Web.UI.WebControls;

public partial class UserControl_IPInitiatorPendingProposals : aspnetUserControl
{
    string CurrentSortExpression = "";
    ProposalMgt oProposalMgt = new ProposalMgt();

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void grdView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string ButtonClicked = e.CommandName; //Determines which button was clicked (stores the name of each button)
        int index = Convert.ToInt32(e.CommandArgument); //Command Argument stores the index of each row

        if (ButtonClicked == "ViewProposalStatus")
        {
            LinkButton lbViewProposalStatus = (LinkButton)grdView.Rows[index].FindControl("ViewStatusLinkButton");
            long lProposalId = long.Parse(lbViewProposalStatus.Attributes["PROPOSALID"].ToString());
            Response.Redirect("~/Common/ViewProposalStatus.aspx" + "?Proposalid=" + lProposalId, false);
        }

        if (ButtonClicked == "EditThisProposal")
        {
            LinkButton lbEditProposal = (LinkButton)grdView.Rows[index].FindControl("EditProposalLinkButton");
            long lProposalId = long.Parse(lbEditProposal.Attributes["PROPOSALID"].ToString());
            Response.Redirect("~/EditIP.aspx" + "?ProposalID=" + lProposalId, false);
        }

        if (ButtonClicked == "ViewOriginalProposal")
        {
            LinkButton lbOriginalProposal = (LinkButton)grdView.Rows[index].FindControl("OriginalProposalLinkButton");
            long lProposalId = long.Parse(lbOriginalProposal.Attributes["PROPOSALID"].ToString());
            Response.Redirect("~/ViewProposal.aspx" + "?Proposalid=" + lProposalId, false);
        }

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

        //if (ButtonClicked == "DeleteProposal")
        //{
        //    LinkButton delete = (LinkButton)myProposalGridView.Rows[index].FindControl("DeleteProposalLinkButton");
        //    lProposalId = long.Parse(delete.Attributes["PROPOSALID"].ToString());

        //    ProposalMgt oProposalMgt = new ProposalMgt();
        //    oProposalMgt.DeactivateProposal(lProposalId);
            
        //    LoadMyPendingProposals(oSessnx.getOnlineUser);
        //}
    }

    protected void grdView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdView.PageIndex = e.NewPageIndex;
        LoadMyPendingProposals(oSessnx.getOnlineUser);
    }

    protected void grdView_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void grdView_Load(object sender, EventArgs e)
    {

    }

    public void LoadMyPendingProposals(appUsers oUser)
    {
        Proposal oProposal = new Proposal();
        ProposalMgt oProposalMgt = new ProposalMgt();

        IPStatusReporter.IPStatusReport(oUser, grdView, oProposalMgt.dtGetMyPendingProposals(oUser), CurrentSortExpression);

        foreach (GridViewRow grdRow in grdView.Rows)
        {
            //Once a Proposal is approved by the IP Initiator's Line Team Lead, the Delete command is disabled.
            LinkButton lbEditProposal = (LinkButton)grdRow.FindControl("EditProposalLinkButton");
            long lProposalId = long.Parse(lbEditProposal.Attributes["PROPOSALID"].ToString());
            oProposal = oProposalMgt.objGetProposalById(lProposalId);

            if (oProposal.m_iDoc_Stand == (int)IPStatusReporter.ipStatusRpt.Approved)
            {
                lbEditProposal.Enabled = false;
            }
        }
    }
}
