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


public partial class IPAdministrator_ShellCompanies : CustomBasePage
{
   

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (db.sessionStateStatusExpired() == true)
        //{
        //    Response.Redirect("~/SessionExpires.aspx");
        //}

        string sql = "SELECT COMPANYNAME, COUNTRY FROM CPDMS_SHELLCOMPANIES";
        DataTable dt = DataAccess.ExecuteQueryCommand(sql);

        shellCompaniesGridView.DataSource = dt;
        shellCompaniesGridView.DataBind();

    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        string sql = "INSERT INTO CPDMS_SHELLCOMPANIES (COMPANYNAME, COUNTRY) VALUES ('" + companyNameTextBox.Text + "', '" + countriesDropDownList.SelectedItem.Text + "')";
        DataAccess.ExecuteNonQueryCommand(sql);
    }
}
