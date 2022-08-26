using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public partial class MenuSysMgt_MenuOptionAssignment : aspnetPage
{
    string SortExpression = "";
    eIPMenus oEIPMenus = new eIPMenus();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utilities.getAllRoles(ddlUserRole);
            LoadGrid();
        }
    }

    protected void grdView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void grdView_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void grdView_Sorted(object sender, EventArgs e)
    {

    }
    protected void grdView_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
    protected void ddlUserRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGrid();
        CheckMyOptions();
    }

    private void LoadGrid()
    {
        Utilities.LoadMyGridView(grdView, oEIPMenus.LoadApplicationMenus(), SortExpression);
    }

    private void CheckMyOptions()
    {
        DataTable dt = oEIPMenus.LoadMyMenu(ddlUserRole.SelectedValue);
        int MenuID = 0;

        foreach (GridViewRow grdRow in grdView.Rows)
        {
            foreach (DataRow dr in dt.Rows)
            {
                CheckBox ckbMenu = (CheckBox)grdRow.FindControl("menuCheckBox");
                MenuID = Convert.ToInt32(ckbMenu.Attributes["MENUID"].ToString());
                if (MenuID == Convert.ToInt32(dr["MENUID"]))
                {
                    ckbMenu.Checked = true;
                    grdView.Rows[grdRow.RowIndex].BackColor = System.Drawing.Color.Green;
                    grdView.Rows[grdRow.RowIndex].ForeColor = System.Drawing.Color.White;
                    grdView.Rows[grdRow.RowIndex].Font.Bold = true;
                }
            }
        }
        //Utilities.LoadMyGridView(grdView, eIPMenus.LoadMyMenu(ddlUserRole.SelectedValue), SortExpression);
    }
    protected void submitButton_Click(object sender, EventArgs e)
    {
        int MenuID = 0;
        int ParentID = 0;

        bool success = false;

        foreach (GridViewRow grdRow in grdView.Rows)
        {
            CheckBox ckbMenu = (CheckBox)grdRow.FindControl("menuCheckBox");
            MenuID = Convert.ToInt32(ckbMenu.Attributes["MENUID"].ToString());
            ParentID = Convert.ToInt32(ckbMenu.Attributes["PARENTID"].ToString());
            if (ckbMenu.Checked == true)
            {
                if (oEIPMenus.ConfirmMenuForRole(MenuID, Convert.ToInt32(ddlUserRole.SelectedValue)) == false)
                {
                    //Check if menu's ParentID exists for the selected role. If it does not, Insert the Menu's ParentID for the User Role
                    if (oEIPMenus.MyParentFound(ParentID, Convert.ToInt32(ddlUserRole.SelectedValue)) == false)
                    {
                        //i.e menu items Parent does exists for the selected userrole
                        //then Insert the ParentID for the selected UserRole
                        oEIPMenus.AssignMenuToRole(ParentID, Convert.ToInt32(ddlUserRole.SelectedValue));
                    }
                    else
                    {
                        //Insert the selected Menu directly
                        success = oEIPMenus.AssignMenuToRole(MenuID, Convert.ToInt32(ddlUserRole.SelectedValue));
                        if (success == false)
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                //If an option was unselected for a role, delete the option for the User Role.
                if (oEIPMenus.ConfirmMenuForRole(MenuID, Convert.ToInt32(ddlUserRole.SelectedValue)) == true)
                {
                    oEIPMenus.DeleteMenuForRole(MenuID, Convert.ToInt32(ddlUserRole.SelectedValue));
                }
            }
        }

        if (success == true)
        {
            ajaxWebExtension.showJscriptAlert(Page, this, "Menu(s) assignment to user role " + ddlUserRole.SelectedItem.Text + " successful");
        }
        else
        {
            ajaxWebExtension.showJscriptAlert(Page, this, "Menu(s) assignment to user role " + ddlUserRole.SelectedItem.Text + " failed, please try again later!!! ");
        }
    }
}