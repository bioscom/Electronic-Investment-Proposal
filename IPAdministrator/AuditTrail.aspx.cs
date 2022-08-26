using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class IPAdministrator_AuditTrail : CustomBasePage
{
    eIPAuditTrail audit = new eIPAuditTrail();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            db.DropDownYearFiller(yearDropDownList);
        }
    }

    protected void yearDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        proposalsDropDownList.Items.Clear();
        db.ProposalInfoFiller(proposalsDropDownList, yearDropDownList.SelectedValue);
    }

    protected void proposalsDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData(proposalsDropDownList.SelectedValue);
    }

    protected void auditTrailGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        auditTrailGridView.PageIndex = e.NewPageIndex;
        BindData(proposalsDropDownList.SelectedValue);
    }

    private void BindData(string ProposalID)
    {
        DataTable dt = audit.AuditTrail(ProposalID);
        foreach (DataRow dr in dt.Rows)
        {
            projNumLabel.Text = dr["PROJ_NUM"].ToString();
            //BOMLabel.Text = dr["BOM"].ToString();
            projInitLabel.Text = dr["FULLNAME"].ToString();
            dateForwardedLabel.Text = dr["DATE_SUBMIT"].ToString();
            dateInitLabel.Text = dr["DATE_INIT"].ToString();
        }

        auditTrailGridView.DataSource = dt;
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

    protected void closeButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Common/IPRegister.aspx");
    }
}