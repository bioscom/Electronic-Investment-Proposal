using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class MasterPages_FrontPage : aspnetMaster
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageLoad();
    }

    public void PageLoad()
    {
        try
        {
            Company company = new Company(oSessnx.getOnlineUser.m_iCompany);
            string MyCompany = company.COMPANYNAME;
            if (company.COMPANYNAME == cpdmsFunctionsNames.NA)
            {
                MyCompany = "Africa-Me";
            }
            CompanyNameLabel.Text = MyCompany;

            loggedinUserLabel.Text = oSessnx.getOnlineUser.m_sFullName;
            dateLabel.Text = DateTime.Today.Date.ToLongDateString();

            UserIDLabel.Text = oSessnx.getOnlineUser.m_sUserName;
            string MyFunction = oSessnx.getOnlineUser.eFunction.m_sFunction;
            if (oSessnx.getOnlineUser.eFunction.m_sFunction == cpdmsFunctionsNames.NA)
            {
                MyFunction = "Africa-Me";
            }

            UserRoleLabel.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oSessnx.getOnlineUser.m_iUserRoleId) + " (" + MyFunction + ")";

            logoutHyperLink.Attributes.Add("onclick", "return LogoutMessage()");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

}
