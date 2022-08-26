using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class IPAdministrator_UsersApprovalHistory : System.Web.UI.Page
{
    appUserMgt oAppUserMgt = new appUserMgt();
    ProposalMgt oProposalMgt = new ProposalMgt();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<appUsers> oAppUsers = oAppUserMgt.lstGetUsers();
            foreach (appUsers oAppUser in oAppUsers)
            {
                ddlUser.Items.Add(new ListItem(oAppUser.m_sFullName, oAppUser.m_iUserId.ToString()));
            }
        }
    }
    
    protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData(int.Parse(ddlUser.SelectedValue));
    }


    private void BindData(int iUserId)
    {
        auditTrailGridView.DataSource = oProposalMgt.dtMyProposalActivityHistory(iUserId);
        auditTrailGridView.DataBind();

        foreach (GridViewRow grvRow in auditTrailGridView.Rows)
        {
            Label SupportStand = (Label)auditTrailGridView.Rows[grvRow.RowIndex].FindControl("labelSupportStand");

            if (SupportStand.Text == SupportState.iSupported.ToString())
            {
                SupportStand.Text = SupportState.Supported;
            }
            else if (SupportStand.Text == SupportState.iApproved.ToString())
            {
                SupportStand.Text = SupportState.Approved;
            }
            else if (SupportStand.Text == SupportState.iNotSupported.ToString())
            {
                SupportStand.Text = SupportState.NotSupported;
            }
            else if (SupportStand.Text == SupportState.iNotApproved.ToString())
            {
                SupportStand.Text = SupportState.NotApproved;
            }
            else if (SupportStand.Text == "")
            {
                SupportStand.Text = "";
            }
            else
            {
                SupportStand.Text = "";
            }
        }
    }

    protected void auditTrailGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        auditTrailGridView.PageIndex = e.NewPageIndex;
        BindData(int.Parse(ddlUser.SelectedValue));
    }
}
