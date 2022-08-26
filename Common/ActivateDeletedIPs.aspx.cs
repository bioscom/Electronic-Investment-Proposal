using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Common_ActivateDeletedIPs : aspnetPage
{
    ProposalMgt oProposalCore = new ProposalMgt();

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
            LoadDeletedProposals();
        }
    }

    protected void DeletedIPsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int iProposalId = 0;
        //string strHTML = "";
        string ButtonClicked = e.CommandName; //Determines which button was clicked (stores the name of each button)
        int index = Convert.ToInt32(e.CommandArgument); //Command Argument stores the index of each row

        if (ButtonClicked == "ViewProposal")
        {
            LinkButton lbOriginalProposal = (LinkButton)DeletedIPsGridView.Rows[index].FindControl("OriginalProposalLinkButton");
            iProposalId = int.Parse(lbOriginalProposal.Attributes["PROPOSALID"].ToString());
            Response.Redirect("~/ViewProposal.aspx" + "?Proposalid=" + iProposalId, false);
        }

        if (ButtonClicked == "UndeleteProposal")
        {
            LinkButton lbUndeleteProposal = (LinkButton)DeletedIPsGridView.Rows[index].FindControl("UndeleteProposalLinkButton");
            iProposalId = int.Parse(lbUndeleteProposal.Attributes["PROPOSALID"].ToString());
            
            bool success = oProposalCore.UndeleteProposal(iProposalId);
            if (success)
            {
                ajaxWebExtension.showJscriptAlert(Page, this, "Successfully undeleted.");
                LoadDeletedProposals();
            }
        }

        if (ButtonClicked == "DeleteProposal")
        {
            LinkButton lbDeleteProposal = (LinkButton)DeletedIPsGridView.Rows[index].FindControl("DeleteProposalLinkButton");
            iProposalId = int.Parse(lbDeleteProposal.Attributes["PROPOSALID"].ToString());

            bool success = oProposalCore.DeleteProposal(iProposalId);
            if (success)
            {
                ajaxWebExtension.showJscriptAlert(Page, this, "Successfully Purged.");
                LoadDeletedProposals();
            }
        }
    }

    private void LoadDeletedProposals()
    {
        ProposalMgt oProposalCore = new ProposalMgt();
        DataTable dt = oProposalCore.dtGetDeletedProposal();
        DeletedIPsGridView.DataSource = dt;
        DeletedIPsGridView.DataBind();

        foreach (GridViewRow grdRow in DeletedIPsGridView.Rows)
        {
            LinkButton lbDelete = (LinkButton)grdRow.FindControl("DeleteProposalLinkButton");
            Label lbProjectTitle = (Label)grdRow.FindControl("labelProjectTitle");
            Label lbProjectNumber = (Label)grdRow.FindControl("labelProjectNumber");
            lbDelete.Attributes.Add("onClick", "return DeleteProject('" + lbProjectNumber.Text + "<-->" + lbProjectTitle.Text + "')");
        }
    }
}