using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Common_SLAAuditTrail : System.Web.UI.Page
{
    string CurrentSortExpression = "";
    //TimeDateCulture dateCulture = new TimeDateCulture();
    ProposalMgt oProposalMgt = new ProposalMgt();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CurrentSortExpression = "IDSLA";
            LoadGrid(CurrentSortExpression);
        }
    }

    protected void viewButton_Click(object sender, EventArgs e)
    {
        LoadGridByDateRange(CurrentSortExpression);
    }

    protected void grdView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdView.PageIndex = e.NewPageIndex;

        if (fromDateControl.DateSelectedDate.Year == 0)
        {
            LoadGrid(CurrentSortExpression);
        }
        else
        {
            LoadGridByDateRange(CurrentSortExpression);
        }
    }

    private void LoadGrid(string sortExpression)
    {
        DataView dv = oProposalMgt.dtGetAuditTrail().DefaultView;
        dv.Sort = sortExpression;

        if (dv.Count > 0)
        {
            grdView.DataSource = dv;
            grdView.DataBind();

            foreach (GridViewRow grdRow in grdView.Rows)
            {
                Label stand = (Label)grdView.Rows[grdRow.RowIndex].FindControl("labelStand");
                stand.Font.Bold = true;
                stand.ForeColor = System.Drawing.Color.Red;
                stand.Text = "Not actioned.";
                //if (Convert.ToInt32(stand.Text) == SupportState.iNotSupported)
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
                //}
            }
        }
    }

    private void LoadGridByDateRange(string sortExpression)
    {
        if ((fromDateControl.DateSelectedDate.Year != 0) && (toDateControl.DateSelectedDate.Year != 0))
        {
            DataView dv = oProposalMgt.dtGetAuditTrailByDate(fromDateControl.DateSelectedDate, toDateControl.DateSelectedDate).DefaultView;
            dv.Sort = sortExpression;

            if (dv.Count > 0)
            {
                grdView.DataSource = dv;
                grdView.DataBind();

                foreach (GridViewRow grdRow in grdView.Rows)
                {
                    Label stand = (Label)grdView.Rows[grdRow.RowIndex].FindControl("labelStand");
                    stand.Font.Bold = true;
                    if (Convert.ToInt32(stand.Text) == SupportState.iNotSupported)
                    {
                        stand.ForeColor = System.Drawing.Color.Red;
                        stand.Text = SupportState.NotSupported;
                    }
                    else if (Convert.ToInt32(stand.Text) == SupportState.iNotApproved)
                    {
                        stand.ForeColor = System.Drawing.Color.Red;
                        stand.Text = SupportState.NotApproved;
                    }
                    else
                    {
                        stand.Text = "No specific stand yet taken.";
                    }
                }
            }
            else
            {
                ajaxWebExtension.showJscriptAlert(Page, this, "No one violates SLA agreement within the selected dates.");
            }
        }
        else
        {
            ajaxWebExtension.showJscriptAlert(Page, this, "Select date range to see SLA violation.");
        }
    }

    protected void grdView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string ButtonClicked = e.CommandName; //Determines which button was clicked (stores the name of each button)

        if (ButtonClicked == "Sort")
        {
            CurrentSortExpression = e.CommandArgument.ToString();
        }
    }

    protected void grdView_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (fromDateControl.DateSelectedDate.Year == 0)
        {
            LoadGrid(CurrentSortExpression);
        }
        else
        {
            LoadGridByDateRange(CurrentSortExpression);
        }
    }
}