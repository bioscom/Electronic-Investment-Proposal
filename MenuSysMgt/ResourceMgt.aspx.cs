using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class MenuSysMgt_ResourceMgt : System.Web.UI.Page
{
    eIPMenus oEIPMenus = new eIPMenus();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<eMenus> lstEMenus = oEIPMenus.lstGetMainMenus();
            foreach (eMenus oMenus in lstEMenus)
            {
                ddlMainMenu.Items.Add(new ListItem(oMenus.m_sTitle, oMenus.m_iMenu.ToString()));
            }

            bindData();
        }
    }

    private void bindData()
    {
        try
        {
            DataTable dt = oEIPMenus.dtGetUserMenu();

            if (dt.Rows.Count > 0)
            {
                formsGridView.DataSource = dt;
                formsGridView.DataBind();

                foreach (GridViewRow grdView in formsGridView.Rows)
                {
                    LinkButton lbDelete = (LinkButton)grdView.FindControl("deleteLinkButton");
                    Label lbformTitle = (Label)grdView.FindControl("labelformTitle");
                    lbDelete.Attributes.Add("onClick", "return DeleteProject('" + lbformTitle.Text + "')");
                }
            }
            else
            {
                ajaxWebExtension.showJscriptAlert(Page, this, "No Record found!!!");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        bool success = oEIPMenus.InsertMainMenu(txtMainMenu.Text, txtMainMenuDesc.Text);
        if (success == true)
        {
            bindData();
            ajaxWebExtension.showJscriptAlert(Page, this, "Main Menu Successfully Inserted.");
        }
    }

    protected void addButton_Click(object sender, EventArgs e)
    {
        bool success = oEIPMenus.InsertSubMenu(titleTextBox.Text, descTextBox.Text, navigateURLTextBox.Text, Convert.ToInt32(ddlMainMenu.SelectedValue));
        if (success == true)
        {
            bindData();
            ajaxWebExtension.showJscriptAlert(Page, this, "Sub Menu Successfully Inserted.");
        }
    }

    private void Clear()
    {
        titleTextBox.Text = "";
        navigateURLTextBox.Text = "";
        descTextBox.Text = "";
    }
    protected void formsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        formsGridView.PageIndex = e.NewPageIndex;
        bindData();
    }

    protected void formsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string ButtonClicked = e.CommandName;

        int index = Convert.ToInt32(e.CommandArgument);
        if (ButtonClicked == "EditThis")
        {
            LinkButton lbEdit = (LinkButton)formsGridView.Rows[index].FindControl("editLinkButton");
            string SystemMenuID = lbEdit.Attributes["IDMENUSYS"].ToString();

            Response.Redirect("~/MenuSysMgt/EditApplicationForm.aspx" + "?xMenuID=" + SystemMenuID, false);
        }

        if (ButtonClicked == "DeleteThis")
        {
            LinkButton lbDelete = (LinkButton)formsGridView.Rows[index].FindControl("deleteLinkButton");
            string SystemMenuID = lbDelete.Attributes["IDMENUSYS"].ToString();
            string sql = "DELETE FROM EIP_MENUSYSTEM WHERE IDMENUSYS = '" + SystemMenuID + "'";
            bool itemDeleted = DataAccess.ExecuteNonQueryCommand(sql);

            if (itemDeleted == true)
            {
                MessageBox.Show("Item successfully deleted");
            }
        }
    }

    
}