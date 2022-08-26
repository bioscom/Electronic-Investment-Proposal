using System;
using System.Web.UI.WebControls;
using System.Data;

public partial class ApprovalSupportFunction_GMSupportDetails : System.Web.UI.Page
{
    ProposalMgt oProposalMgt = new ProposalMgt();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Proposalid"].ToString() != null)
        {
            long lProposal = long.Parse(Request.QueryString["Proposalid"].ToString());
            Proposal oProposal = oProposalMgt.objGetProposalById(lProposal);
            IPDetailInfo1.LoadProposalDetails(oProposal);
            LoadGMSResponse(lProposal);
        }
    }

    private void LoadGMSResponse(long lProposal)
    {
        DataTable dt = oProposalMgt.dtGetProposalSupportApprovalDetailsByRole(lProposal, (int)appUsersRoles.userRole.VP);

        GMDetailsGridView.DataSource = dt;
        GMDetailsGridView.DataBind();

        foreach (GridViewRow grdRow in GMDetailsGridView.Rows)
        {
            Label Stand = (Label)GMDetailsGridView.Rows[grdRow.RowIndex].FindControl("labelStand");
            Stand.Font.Bold = true;
            string stand = "";

            stand = oProposalMgt.MyStand(Convert.ToInt32(Stand.Text));
            //oProposalMgt.SupportStatusCode(Convert.ToInt32(dr["STAND"]), Status);

            Stand.Text = stand;
            //Comments.Text = dr["COMMENTS"].ToString();

            //if (Convert.ToInt32(stand.Text) == SupportState.iSupported)
            //{
            //    stand.ForeColor = System.Drawing.Color.Green;
            //    stand.Text = SupportState.Supported;
            //}
            //else if (Convert.ToInt32(stand.Text) == SupportState.iApproved)
            //{
            //    stand.ForeColor = System.Drawing.Color.Green;
            //    stand.Text = SupportState.Approved;
            //}
            //else if (Convert.ToInt32(stand.Text) == SupportState.iFinanceApproval)
            //{
            //    stand.ForeColor = System.Drawing.Color.Green;
            //    stand.Text = SupportState.Approved;
            //}
            //else if (Convert.ToInt32(stand.Text) == SupportState.iNotSupported)
            //{
            //    stand.ForeColor = System.Drawing.Color.Red;
            //    stand.Text = SupportState.NotSupported;
            //} 
            //else if (Convert.ToInt32(stand.Text) == SupportState.iNotApproved)
            //{
            //    stand.ForeColor = System.Drawing.Color.Red;
            //    stand.Text = SupportState.NotApproved;
            //}
            //else
            //{
            //    stand.Text = "";
            //}
        }
    }
}