using System;
using System.Data;

/// <summary>
/// IN THIS PAGE, #FFCC00 REPRESENT AMBER COLOUR
/// </summary>

public partial class IPAdministrator_SLA : aspnetPage
{
    //TODO: you should re-write the entire code here to conform to new tech.
    //appUserMgt oAppUserMgt = new appUserMgt();

    //public string SLAID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //ValidateInput();

        if (!IsPostBack)
        {
            //retriveBPOFixedSLA();
        }

        //string sql = "SELECT IDSLA FROM EIP_SLA WHERE COMPANYID = '" + oSessnx.getOnlineUser.m_iCompany + "'";
        //DataTable dt = DataAccess.ExecuteQueryCommand(sql);

        //if (dt.Rows.Count > 0)
        //{
        //    SLAID = dt.Rows[0]["IDSLA"].ToString();
        //}
    }

    private void ValidateInput()
    {
        txtASLA.Attributes.Add("onkeypress", "return isNumberKey(event)");
        txtFSSLA.Attributes.Add("onkeypress", "return isNumberKey(event)");

        //txtASLA.Attributes.Add("onkeypress", "return textboxMultilineMaxNumber(this,2)");
        //txtFSSLA.Attributes.Add("onkeypress", "return textboxMultilineMaxNumber(this,2)");
    }

    protected void submitbtn_Click(object sender, EventArgs e)
    {
        //if (MSGLabel.Text == "")
        //{
        //    string sql = "INSERT INTO EIP_SLA (ASLA, FSSLA, COMPANYID, IDUSERMGT) VALUES ";
        //    sql += "('" + txtASLA.Text + "', '" + txtFSSLA.Text + "', '" + oSessnx.getOnlineUser.m_iCompany + "', '" + oSessnx.getOnlineUser.m_iUserId + "')";
        //    DataAccess.ExecuteNonQueryCommand(sql);
        //}
        //else
        //{
        //    string sql1 = "UPDATE EIP_SLA SET ASLA = '" + txtASLA.Text + "', FSSLA = '" + txtFSSLA.Text + "', ";
        //    sql1 += "COMPANYID = '" + oSessnx.getOnlineUser.m_iCompany + "', IDUSERMGT = '" + oSessnx.getOnlineUser.m_iUserId + "' WHERE IDSLA = '" + SLAID + "'";
        //    bool success = DataAccess.ExecuteNonQueryCommand(sql1);
        //    if (success == true)
        //    {
        //        MessageBox.Show("SLA succeessfully submitted.");
        //    }
        //}
    }


    private void retriveBPOFixedSLA()
    {
        //string sql = "SELECT * FROM EIP_SLA WHERE COMPANYID = '" + oSessnx.getOnlineUser.m_iCompany + "'";
        //DataTable dt = DataAccess.ExecuteQueryCommand(sql);

        //if (dt.Rows.Count > 0)
        //{
        //    SLAID = dt.Rows[0]["IDSLA"].ToString();
        //    txtASLA.Text = dt.Rows[0]["ASLA"].ToString();
        //    txtFSSLA.Text = dt.Rows[0]["FSSLA"].ToString();

        //    MSGLabel.Text = "SLA fixed by: " + oSessnx.getOnlineUser.m_sFullName;

        //    //for Under Construction
        //    if (!dt.Rows[0].IsNull("UNDERCONSTR"))
        //    {
        //        if (dt.Rows[0]["UNDERCONSTR"].ToString() == "RED") { opt1.Checked = true; }
        //        else if (dt.Rows[0]["UNDERCONSTR"].ToString() == "#FFCC00") { opt2.Checked = true; }
        //        else if (dt.Rows[0]["UNDERCONSTR"].ToString() == "GREEN") { opt3.Checked = true; }
        //    }

        //    //for Under Appsroval
        //    if (!dt.Rows[0].IsNull("UNDERAPPROVAL"))
        //    {
        //        if (dt.Rows[0]["UNDERAPPROVAL"].ToString() == "RED") { opt4.Checked = true; }
        //        else if (dt.Rows[0]["UNDERAPPROVAL"].ToString() == "#FFCC00") { opt5.Checked = true; }
        //        else if (dt.Rows[0]["UNDERAPPROVAL"].ToString() == "GREEN") { opt6.Checked = true; }
        //    }

        //    //for Within SLA
        //    if (!dt.Rows[0].IsNull("WITHINSLA"))
        //    {
        //        if (dt.Rows[0]["WITHINSLA"].ToString() == "RED") { opt7.Checked = true; }
        //        else if (dt.Rows[0]["WITHINSLA"].ToString() == "#FFCC00") { opt8.Checked = true; }
        //        else if (dt.Rows[0]["WITHINSLA"].ToString() == "GREEN") { opt9.Checked = true; }
        //    }

        //    //Outside SLA
        //    if (!dt.Rows[0].IsNull("OUTSIDESLA"))
        //    {
        //        if (dt.Rows[0]["OUTSIDESLA"].ToString() == "RED") { opt10.Checked = true; }
        //        else if (dt.Rows[0]["OUTSIDESLA"].ToString() == "#FFCC00") { opt11.Checked = true; }
        //        else if (dt.Rows[0]["OUTSIDESLA"].ToString() == "GREEN") { opt12.Checked = true; }
        //    }

        //    //Exceeds SLA
        //    if (!dt.Rows[0].IsNull("EXCEEDSLA"))
        //    {
        //        if (dt.Rows[0]["EXCEEDSLA"].ToString() == "RED") { opt13.Checked = true; }
        //        else if (dt.Rows[0]["EXCEEDSLA"].ToString() == "#FFCC00") { opt14.Checked = true; }
        //        else if (dt.Rows[0]["EXCEEDSLA"].ToString() == "GREEN") { opt15.Checked = true; }
        //    }
        //}
    }

    protected void savebtn_Click(object sender, EventArgs e)
    {
        //string UnderConstr = null; string UnderApproval = null;
        //string WithinSLA = null; string OutsideSLA = null; string ExceedSLA = null;

        ////for Under Construction
        //if (opt1.Checked == true) { UnderConstr = "RED"; }
        //else if (opt2.Checked == true) { UnderConstr = "#FFCC00"; }
        //else if (opt3.Checked == true) { UnderConstr = "GREEN"; }

        ////for Under Approval
        //if (opt4.Checked == true) { UnderApproval = "RED"; }
        //else if (opt5.Checked == true) { UnderApproval = "#FFCC00"; }
        //else if (opt6.Checked == true) { UnderApproval = "GREEN"; }

        ////Within SLA
        //if (opt7.Checked == true) { WithinSLA = "RED"; }
        //else if (opt8.Checked == true) { WithinSLA = "#FFCC00"; }
        //else if (opt9.Checked == true) { WithinSLA = "GREEN"; }

        ////Outside SLA
        //if (opt10.Checked == true) { OutsideSLA = "RED"; }
        //else if (opt11.Checked == true) { OutsideSLA = "#FFCC00"; }
        //else if (opt12.Checked == true) { OutsideSLA = "GREEN"; }

        ////Exeeds SLA
        //if (opt13.Checked == true) { ExceedSLA = "RED"; }
        //else if (opt14.Checked == true) { ExceedSLA = "#FFCC00"; }
        //else if (opt15.Checked == true) { ExceedSLA = "GREEN"; }

        //string sql = "UPDATE EIP_SLA SET UNDERCONSTR = '" + UnderConstr + "', UNDERAPPROVAL = '" + UnderApproval + "', ";
        //sql += "WITHINSLA = '" + WithinSLA + "', OUTSIDESLA = '" + OutsideSLA + "', EXCEEDSLA = '" + ExceedSLA + "' WHERE IDSLA = '" + SLAID + "'";
        //bool success = DataAccess.ExecuteNonQueryCommand(sql);
        //if (success == true)
        //{
        //    MessageBox.Show("SLA color flags succeessfully submitted.");
        //}
    }

    protected void closeButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Common/IPRegister.aspx");
    }
}