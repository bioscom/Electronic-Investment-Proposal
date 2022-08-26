using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class BPO_YearBusinessPlan : aspnetPage
{
    TimeDateCulture dateCulture = new TimeDateCulture();
    //TODO: re-code this page

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
            yearLabel.Text = DateTime.Today.Year.ToString();

            string sql = "SELECT TO_CHAR(TO_DATE(EIP_IPYEARBP.DATE_SUBMIT, 'DD-MON-YY'), 'YYYY') AS DYEAR, EIP_IPYEARBP.IDYEARBP, EIP_IPYEARBP.JAN, EIP_IPYEARBP.FEB, ";
            sql += "EIP_IPYEARBP.MAR, EIP_IPYEARBP.APR, EIP_IPYEARBP.MAY, EIP_IPYEARBP.JUN, EIP_IPYEARBP.JUL, EIP_IPYEARBP.AUG, EIP_IPYEARBP.SEP, ";
            sql += "EIP_IPYEARBP.OCT, EIP_IPYEARBP.NOV, EIP_IPYEARBP.DEC, EIP_USERMGT.FULLNAME ";
            sql += "FROM EIP_IPYEARBP INNER JOIN ";
            sql += "EIP_USERMGT ON EIP_IPYEARBP.IDUSERMGT = EIP_USERMGT.IDUSERMGT ";
            sql += "WHERE TO_CHAR(TO_DATE(EIP_IPYEARBP.DATE_SUBMIT, 'DD-MON-YY') ,'YYYY') = '" + DateTime.Today.Year + "'";
            
            DataTable dt = DataAccess.ExecuteQueryCommand(sql);
            if (dt.Rows.Count > 0)
            {
                IDYEARBPHF.Value = dt.Rows[0]["IDYEARBP"].ToString();
                submitButton.Visible = false;
                updateButton.Visible = true;

                janTextBox.Text = dt.Rows[0]["JAN"].ToString(); febTextBox.Text = dt.Rows[0]["FEB"].ToString();
                marTextBox.Text = dt.Rows[0]["MAR"].ToString(); aprTextBox.Text = dt.Rows[0]["APR"].ToString();
                mayTextBox.Text = dt.Rows[0]["MAY"].ToString(); junTextBox.Text = dt.Rows[0]["JUN"].ToString();
                julTextBox.Text = dt.Rows[0]["JUL"].ToString(); augTextBox.Text = dt.Rows[0]["AUG"].ToString();
                sepTextBox.Text = dt.Rows[0]["SEP"].ToString(); octTextBox.Text = dt.Rows[0]["OCT"].ToString();
                novTextBox.Text = dt.Rows[0]["NOV"].ToString(); decTextBox.Text = dt.Rows[0]["DEC"].ToString();
                fixedByLabel.Text = dt.Rows[0]["FULLNAME"].ToString();

                MessageBox.Show("You have BP Year Plan set for the year " + DateTime.Today.Year + "\n To update BP Plan enter your values and click submit. \n\nThanks.");
            }
            else
            {
                submitButton.Visible = true;
                updateButton.Visible = false;
                MessageBox.Show("You have not set BP Year Plan for the year " + DateTime.Today.Year);
            }
            totalLabel.Text = getTotalIPs();
        }

        janTextBox.Attributes.Add("onkeypress", "return isNumberKey(event)");
        febTextBox.Attributes.Add("onkeypress", "return isNumberKey(event)");
        marTextBox.Attributes.Add("onkeypress", "return isNumberKey(event)");
        aprTextBox.Attributes.Add("onkeypress", "return isNumberKey(event)");
        mayTextBox.Attributes.Add("onkeypress", "return isNumberKey(event)");
        junTextBox.Attributes.Add("onkeypress", "return isNumberKey(event)");
        julTextBox.Attributes.Add("onkeypress", "return isNumberKey(event)");
        augTextBox.Attributes.Add("onkeypress", "return isNumberKey(event)");
        sepTextBox.Attributes.Add("onkeypress", "return isNumberKey(event)");
        octTextBox.Attributes.Add("onkeypress", "return isNumberKey(event)");
        novTextBox.Attributes.Add("onkeypress", "return isNumberKey(event)");
        decTextBox.Attributes.Add("onkeypress", "return isNumberKey(event)");
    }
    protected void submitButton_Click(object sender, EventArgs e)
    {
        string todaysDate = dateCulture.GetTodaysDateInBritishFormat();
        string sql = "INSERT INTO EIP_IPYEARBP (DATE_SUBMIT, JAN, FEB, MAR, APR, MAY, JUN, JUL, AUG, SEP, OCT, NOV, DEC, IDUSERMGT) ";
        sql += "VALUES (TO_DATE('" + todaysDate + "', 'DD/MM/YYYY'), @JAN, @FEB, @MAR, @APR, @MAY, @JUN, @JUL, @AUG, @SEP, @OCT, @NOV, @DEC, @IDUSERMGT)";
        sql = sql.Replace("@JAN", "'" + janTextBox.Text + "'");
        sql = sql.Replace("@FEB", "'" + febTextBox.Text + "'");
        sql = sql.Replace("@MAR", "'" + marTextBox.Text + "'");
        sql = sql.Replace("@APR", "'" + aprTextBox.Text + "'");
        sql = sql.Replace("@MAY", "'" + mayTextBox.Text + "'");
        sql = sql.Replace("@JUN", "'" + junTextBox.Text + "'");
        sql = sql.Replace("@JUL", "'" + julTextBox.Text + "'");
        sql = sql.Replace("@AUG", "'" + augTextBox.Text + "'");
        sql = sql.Replace("@SEP", "'" + sepTextBox.Text + "'");
        sql = sql.Replace("@OCT", "'" + octTextBox.Text + "'");
        sql = sql.Replace("@NOV", "'" + novTextBox.Text + "'");
        sql = sql.Replace("@DEC", "'" + decTextBox.Text + "'");
        sql = sql.Replace("@IDUSERMGT", "'" + oSessnx.getOnlineUser.m_iUserId + "'");

        bool success = DataAccess.ExecuteNonQueryCommand(sql);
        if (success == true)
        {
            totalLabel.Text = getTotalIPs();
            MessageBox.Show("Submit was successful.");
        }
    }
    protected void updateButton_Click(object sender, EventArgs e)
    {
        string todaysDate = dateCulture.GetTodaysDateInBritishFormat();
        string sql = "UPDATE EIP_IPYEARBP SET DATE_SUBMIT = TO_DATE('" + todaysDate + "', 'DD/MM/YYYY'), JAN = @JAN, FEB = @FEB, MAR = @MAR, APR = @APR, ";
        sql += "MAY = @MAY, JUN = @JUN, JUL = @JUL, AUG = @AUG, SEP = @SEP, OCT = @OCT, NOV = @NOV, DEC = @DEC, IDUSERMGT = @IDUSERMGT WHERE IDYEARBP = @IDYEARBP";
        sql = sql.Replace("@IDYEARBP", "'" + IDYEARBPHF.Value + "'");
        sql = sql.Replace("@JAN", "'" + janTextBox.Text + "'");
        sql = sql.Replace("@FEB", "'" + febTextBox.Text + "'");
        sql = sql.Replace("@MAR", "'" + marTextBox.Text + "'");
        sql = sql.Replace("@APR", "'" + aprTextBox.Text + "'");
        sql = sql.Replace("@MAY", "'" + mayTextBox.Text + "'");
        sql = sql.Replace("@JUN", "'" + junTextBox.Text + "'");
        sql = sql.Replace("@JUL", "'" + julTextBox.Text + "'");
        sql = sql.Replace("@AUG", "'" + augTextBox.Text + "'");
        sql = sql.Replace("@SEP", "'" + sepTextBox.Text + "'");
        sql = sql.Replace("@OCT", "'" + octTextBox.Text + "'");
        sql = sql.Replace("@NOV", "'" + novTextBox.Text + "'");
        sql = sql.Replace("@DEC", "'" + decTextBox.Text + "'");
        sql = sql.Replace("@IDUSERMGT", "'" + oSessnx.getOnlineUser.m_iUserId + "'");

        bool success = DataAccess.ExecuteNonQueryCommand(sql);
        if (success == true)
        {
            totalLabel.Text = getTotalIPs();
            MessageBox.Show("Update was successfully submitted.");
        }
    }

    private string getTotalIPs()
    {
        int totalIPs = Convert.ToInt32(janTextBox.Text) + Convert.ToInt32(febTextBox.Text) + Convert.ToInt32(marTextBox.Text) 
                     + Convert.ToInt32(aprTextBox.Text) + Convert.ToInt32(mayTextBox.Text) + Convert.ToInt32(junTextBox.Text) 
                     + Convert.ToInt32(julTextBox.Text) + Convert.ToInt32(augTextBox.Text) + Convert.ToInt32(sepTextBox.Text) 
                     + Convert.ToInt32(octTextBox.Text) + Convert.ToInt32(novTextBox.Text) + Convert.ToInt32(decTextBox.Text);

        return totalIPs.ToString();
    }
}
