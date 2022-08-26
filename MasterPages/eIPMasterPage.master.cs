using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Collections.Generic;

public partial class MasterPages_eIPMasterPage : aspnetMaster
{
    eIPMenus oEIPMenus = new eIPMenus();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            PageLoad();

            //The SLA procedure can only be called by Business Process Owner or System Administrator
            //Also, Session["SLA"] must be 0 before the SLA procedure can run
            //This reduces the number of times the procedure is called when more people are logged into the appplication
            if (Session["SLA"].ToString() == "0")
            {
                SLA MySLA = new SLA();
                SLAMgt oSLAMgt = new SLAMgt();
                //TODO: Please work on this SLA stuff
                //oSLAMgt.SendReminderMail(oSessnx.getOnlineUser);
                Session["SLA"] = 1;
            }
        }
    }
    protected void searchButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        Response.Redirect("~/Common/eSearch.aspx?ProposalName=" + IPNameTextBox.Text);
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
            oEIPMenus.getUserMenu(UserXmlDataSource, oSessnx.getOnlineUser);

            //Added on 26-11-2013 12:51PM
            //MenuItem oMenuItems = UserMenu.Items;
            //foreach (MenuItem item in UserMenu.Items)
            //{
            //    if (item.Text == "Help")
            //    {
            //        item.Target = "_blank";
            //    }
            //}

            logoutHyperLink.Attributes.Add("onclick", "return LogoutMessage()");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }
}