using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace IP.MenuSysMgt
{
    public partial class EditApplicationForm : CustomBasePage
    {
        string xMenuID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["xMenuID"] != null)
            {
                xMenuID = Request.QueryString["xMenuID"].ToString();
            }

            if (!IsPostBack)
            {
                RetrieveFormDetails();
            }
        }

        private void RetrieveFormDetails()
        {
            try
            {
                string sql = "SELECT FORMTITLE, FORMURL, FORMDESC FROM EIP_MENUSYSTEM WHERE IDMENUSYS = '" + xMenuID + "'";
                DataTable dt = DataAccess.ExecuteQueryCommand(sql);
                if (dt.Rows.Count > 0)
                {
                    titleTextBox.Text = dt.Rows[0]["FORMTITLE"].ToString();
                    navigateURLTextBox.Text = dt.Rows[0]["FORMURL"].ToString();
                    descTextBox.Text = dt.Rows[0]["FORMDESC"].ToString();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString());
                System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
            }
        }

        protected void updateButton_Click(object sender, EventArgs e)
        {
            string title = titleTextBox.Text.Replace("'", "''");
            string navURL = navigateURLTextBox.Text.Replace("'", "''");
            string description = descTextBox.Text.Replace("'", "''");

            string sql = "UPDATE EIP_MENUSYSTEM SET FORMTITLE = '" + title + "', FORMURL = '" + navURL + "', ";
            sql += "FORMDESC = '" + description + "' WHERE IDMENUSYS = '" + xMenuID + "'";

            bool success = DataAccess.ExecuteNonQueryCommand(sql);
            Clear();
            if (success == true)
            {
                MessageBox.Show("Update was successful.");
            }
        }

        private void Clear()
        {
            titleTextBox.Text = "";
            descTextBox.Text = "";
            navigateURLTextBox.Text = "";
        }

        protected void closeButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/MenuSysMgt/ApplicationForms.aspx");
        }
    }
}
