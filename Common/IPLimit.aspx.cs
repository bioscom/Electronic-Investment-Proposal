using System;

public partial class Common_IPLimit : aspnetPage
{
    IPLimit oIPLimits = new IPLimit();
    
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
            retrieveIPLimits();
        }
    }

    private void retrieveIPLimits()
    {
        IPLimit.IPLevels oIPLevels = oIPLimits.Limits();
        //xIPValueRange = oIPValueRange.objGetIPValueRange();
        //txtOne.Text = xIPValueRange.m_iRangeOne.ToString();
        //txtTwo.Text = xIPValueRange.m_iRangeTwo.ToString();
        //txtThree.Text = xIPValueRange.m_iRangeThree.ToString();
        //txtFour.Text = xIPValueRange.m_iRangeFour.ToString();
        //txtFive.Text = xIPValueRange.m_iRangeFive.ToString();
    }
    
    protected void saveButton_Click(object sender, EventArgs e)
    {
        IPLimit.IPLevels oIPLevels = oIPLimits.Limits();
        //string sStatus = "";

        //oIPValueRange.m_iRangeOne = Convert.ToInt32(txtOne.Text);
        //oIPValueRange.m_iRangeTwo = Convert.ToInt32(txtTwo.Text);
        //oIPValueRange.m_iRangeThree = Convert.ToInt32(txtThree.Text);
        //oIPValueRange.m_iRangeFour = Convert.ToInt32(txtFour.Text);
        //oIPValueRange.m_iRangeFive = Convert.ToInt32(txtFive.Text);

        //bool success = oIPValueRange.UpdateIPLimit(oIPValueRange);
        //if (success == true)
        //{
        //    retrieveIPLimits();
        //    ajaxWebExtension.showJscriptAlert(Page, this, "IP Limits successfully updated.");
        //}
        //else
        //{
        //    ajaxWebExtension.showJscriptAlert(Page, this, sStatus);
        //}
    }
    protected void resetButton_Click(object sender, EventArgs e)
    {
        retrieveIPLimits();
    }
}