using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Common_UserRoles : System.Web.UI.Page
{
   

    string CurrentSortExpression = "ROLES";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData(CurrentSortExpression);
        }
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow grdRow in grdView.Rows)
        {
            
            CheckBox ckb = (CheckBox)grdView.Rows[grdRow.RowIndex].FindControl("mandatoryCheckBox");
            string UserRoleID = ckb.Attributes["USERROLESID"].ToString();
            if (ckb.Checked == true)
            {
                string sql = "UPDATE EIP_USERROLES SET MANDATORY = '"+ MandatorySupport.iMandatory +"' WHERE USERROLESID = '" + UserRoleID + "'";
                DataAccess.ExecuteNonQueryCommand(sql);
            }
            else
            {
                string sql = "UPDATE EIP_USERROLES SET MANDATORY = '"+ MandatorySupport.iNotMandatory +"' WHERE USERROLESID = '" + UserRoleID + "'";
                DataAccess.ExecuteNonQueryCommand(sql);
            }
        }
        MessageBox.Show("Mandatory user roles updated.");
    }

    protected void grdView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdView.PageIndex = e.NewPageIndex;
        BindData(CurrentSortExpression);
    }

    protected void grdView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string ButtonClicked = e.CommandName; //Determines which button was clicked (stores the name of each button)

        try
        {
            if (ButtonClicked == "Sort")
            {
                CurrentSortExpression = e.CommandArgument.ToString();
            }
        }
        catch (Exception ex)
        {
            //MessageBox.Show(ex.Message.ToString());
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());

        }
    }

    protected void grdView_Sorted(object sender, EventArgs e)
    {

    }

    protected void grdView_Sorting(object sender, GridViewSortEventArgs e)
    {

    }

    private void BindData(string SortExpression)
    {
        string sql = "SELECT USERROLESID, ROLES, MANDATORY FROM EIP_USERROLES";
        DataTable dt = DataAccess.ExecuteQueryCommand(sql); 

        DataView dv = dt.DefaultView;
        dv.Sort = SortExpression;

        grdView.DataSource = dv;
        grdView.DataBind();

        foreach (GridViewRow grdRow in grdView.Rows)
        {
            Label Mandatory = (Label)grdView.Rows[grdRow.RowIndex].FindControl("labelMandatory");
            Label UserRole = (Label)grdView.Rows[grdRow.RowIndex].FindControl("labelUserRoles");
            CheckBox ckb = (CheckBox)grdView.Rows[grdRow.RowIndex].FindControl("mandatoryCheckBox");
            if (Convert.ToInt32(ckb.Attributes["MANDATORY"]) == MandatorySupport.iMandatory)
            {
                ckb.Checked = true;
                Mandatory.Text = "Mandatory Functional Support";
                Mandatory.Font.Bold = true;
                Mandatory.ForeColor = System.Drawing.Color.Red;

                UserRole.Font.Bold = true;
                UserRole.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
