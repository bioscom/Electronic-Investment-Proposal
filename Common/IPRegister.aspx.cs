using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class Common_IPRegister : aspnetPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool bRet = false;
        try
        {
            if (oSessnx.getOnlineUser.m_iUserId == 0)
            {
                //User Not found in the database
                bRet = false;
            }
            else
            {
                bRet = true;
            }
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
        if (!bRet) Response.Redirect("~/Profiles/pageDenied.aspx");
    }
}