using System;
using System.Net.Mail;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;

/// <summary>
    /// Class contains miscellaneous functionality
    /// </summary>
public static class Utilities
{
    static Utilities()
    {
        
    }
    // Generic method for sending emails
    private static void SendMail (string fromEmailAddress, string toEmailAddress, string mSubject, string mBody, string mCC)
    {
        try
        {
            SmtpClient smtp = new SmtpClient(AppConfiguration.MailServer);
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(fromEmailAddress);
            msg.To.Add(toEmailAddress);

            if (mCC.Length != 0)
            {
                msg.CC.Add(mCC);
            }

            msg.Subject = mSubject;
            msg.Body = mBody;
            msg.IsBodyHtml = true;
            smtp.Send(msg);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    //public static void LogError(Exception ex)
    //{
    //    // get the current date and time
    //    string dateTime = DateTime.Now.ToLongDateString() + ", at " + DateTime.Now.ToShortTimeString();
    //    // stores the error message
    //    string errorMessage = "Exception generated on " + dateTime;
    //    // obtain the page that generated the error
    //    HttpContext context = HttpContext.Current;
    //    errorMessage += "\n\n Page location: " + context.Request.RawUrl;
    //    // build the error message
    //    errorMessage += "\n\n Message: " + ex.Message;
    //    errorMessage += "\n\n Source: " + ex.Source;
    //    errorMessage += "\n\n Method: " + ex.TargetSite;
    //    errorMessage += "\n\n Stack Trace: \n\n" + ex.StackTrace;
    //    // send error email in case the option is activated in web.config
    //    if (AppConfiguration.EnableErrorLogEmail)
    //    {

    //        appUsers Me = new appUsers(Apps.LoginIDNoDomain(HttpContext.Current.User.Identity.Name));
    //        //string from = AppConfiguration.MailFrom;
    //        string from = Me.sUSERMAIL;
    //        string to = AppConfiguration.ErrorLogEmail;
    //        string subject = "EIP Error Report";
    //        string body = errorMessage;
    //        SendMail(from, to, subject, body, "");
    //    }
    //}

    public enum status : int { Active = 1, Inactive };


    public static void FillDBL(DropDownList theDropDownList, DataTable dt)
    {
        foreach (DataRow dr in dt.Rows)
        {
            string listItemText = dr[0].ToString();
            string listItemValue = dr[1].ToString();
            theDropDownList.Items.Add(new ListItem(listItemText, listItemValue));
        }
    }

    public static void FillDBL2(DropDownList theDropDownList, DataTable dt, string ItemValue, string ItemText)
    {
        theDropDownList.Items.Clear();
        theDropDownList.Items.Add(new ListItem(ItemValue, ItemText));

        foreach (DataRow dr in dt.Rows)
        {
            string listItemText = dr[0].ToString();
            string listItemValue = dr[1].ToString();
            theDropDownList.Items.Add(new ListItem(listItemText, listItemValue));
        }
    }

    public static void FillListBox(ListBox theListBox, DataTable dt, string ItemValue, string ItemText)
    {
        theListBox.Items.Clear();
        theListBox.Items.Add(new ListItem(ItemValue, ItemText));

        foreach (DataRow dr in dt.Rows)
        {
            string listItemText = dr[0].ToString();
            string listItemValue = dr[1].ToString();
            theListBox.Items.Add(new ListItem(listItemText, listItemValue));
        }
    }

    public static string AppURL()
    {
        string ServerURL = "";

        string httpHost = HttpContext.Current.Request.ServerVariables["http_host"].ToString();

        if (httpHost == AppConfiguration.SiteHostName) //Life server
        {
            ServerURL = "http://" + httpHost + "/EIP";
        }
        else if (httpHost == AppConfiguration.SiteDevelopmentEnvironment)         //Test Server
        {
            ServerURL = "http://" + httpHost + "/EIP";
        }
        else
        {
            ServerURL = "http://" + httpHost + "/EIP"; //Development PC
        }

        return ServerURL;
    }

    public static string LiveLinkURLs()
    {
        string LiveLink = "http://sww-knowledge-epg.shell.com/knowtepg1/llisapi.dll?func=ll&objId=12093961&objAction=browse&sort=name";
        return LiveLink;
    }

    public static string eIPBehaviouralGuideLines()
    {
        string behaviouralGuid2 = "http://sww-knowledge-epg.shell.com/knowtepg1/livelink.exe?func=ll&objId=12096514&objAction=Open&viewType=1&nexturl=%2Fknowtepg1%2Flivelink%2Eexe%3Ffunc%3Dll%26objId%3D12093961%26objAction%3Dbrowse%26sort%3Dname";
        //string behaviouralGuide = "http://sww-knowledge-epg.shell.com/knowtepg1/llisapi.dll?func=ll&objId=12102069&objAction=Open&viewType=1&nexturl=%2Fknowtepg1%2Fllisapi%2Edll%3Ffunc%3Dll%26objId%3D12093961%26objAction%3Dbrowse%26sort%3Dname%26viewType%3D1";
        return behaviouralGuid2;
    }

    public static void LoadMyGridView(GridView grd, string sql, string SortExpression)
    {
        DataTable dt = DataAccess.ExecuteQueryCommand(sql);

        if (dt.Rows.Count > 0)
        {
            DataView dv = dt.DefaultView;
            dv.Sort = SortExpression;
            grd.DataSource = dv;
            grd.DataBind();
        }
        else
        {
            MessageBox.Show("No record found!");
        }
    }

    public static void PageIndexChanging(GridView grdView, GridViewPageEventArgs e, string CurrentSortExpression)
    {
        grdView.PageIndex = e.NewPageIndex;
        DataSorter SortMe = new DataSorter();
    }

    //Fill the User Roles
    public static void getAllRoles(DropDownList ddl)
    {
        Utilities.addRoleToDropDown(appUsersRoles.userRole.Administrator, ddl);
        Utilities.addRoleToDropDown(appUsersRoles.userRole.Auditor, ddl);
        Utilities.addRoleToDropDown(appUsersRoles.userRole.BOM, ddl);
        Utilities.addRoleToDropDown(appUsersRoles.userRole.Business_Process_Owner, ddl);
        Utilities.addRoleToDropDown(appUsersRoles.userRole.CERP, ddl);
        Utilities.addRoleToDropDown(appUsersRoles.userRole.Controllers, ddl);
        Utilities.addRoleToDropDown(appUsersRoles.userRole.Economics_Support, ddl);
        //Utilities.addRoleToDropDown(appUsersRoles.userRole.EPG_IP_Tracker, ddl);
        Utilities.addRoleToDropDown(appUsersRoles.userRole.Finance_Signature, ddl);
	Utilities.addRoleToDropDown(appUsersRoles.userRole.Finance_Director, ddl);
        Utilities.addRoleToDropDown(appUsersRoles.userRole.Functional_Planner, ddl);
        Utilities.addRoleToDropDown(appUsersRoles.userRole.GM_Regional_Planning, ddl);
        Utilities.addRoleToDropDown(appUsersRoles.userRole.HSE_Support, ddl);
        Utilities.addRoleToDropDown(appUsersRoles.userRole.IP_Initiator, ddl);
        Utilities.addRoleToDropDown(appUsersRoles.userRole.IT, ddl);
        Utilities.addRoleToDropDown(appUsersRoles.userRole.LEGAL_Support, ddl);
        Utilities.addRoleToDropDown(appUsersRoles.userRole.Line_Team_Lead, ddl);
        Utilities.addRoleToDropDown(appUsersRoles.userRole.MD, ddl);
        Utilities.addRoleToDropDown(appUsersRoles.userRole.REVP, ddl);
        Utilities.addRoleToDropDown(appUsersRoles.userRole.SCM, ddl);
        Utilities.addRoleToDropDown(appUsersRoles.userRole.Security_Support, ddl);
        Utilities.addRoleToDropDown(appUsersRoles.userRole.SPCA_Support, ddl);
        Utilities.addRoleToDropDown(appUsersRoles.userRole.TAX_Support, ddl);
        Utilities.addRoleToDropDown(appUsersRoles.userRole.Technical_Planning_Manager, ddl);
        Utilities.addRoleToDropDown(appUsersRoles.userRole.Treasury_Support, ddl);
        Utilities.addRoleToDropDown(appUsersRoles.userRole.VP, ddl);
    }

    public static void getDefaultUserRoles(DropDownList ddl)
    {
        addRoleToDropDown(appUsersRoles.userRole.Administrator, ddl);
        addRoleToDropDown(appUsersRoles.userRole.Business_Process_Owner, ddl);
        addRoleToDropDown(appUsersRoles.userRole.CERP, ddl);
        //addRoleToDropDown(appUsersRoles.userRole.EPG_IP_Tracker, ddl);
        addRoleToDropDown(appUsersRoles.userRole.Finance_Director, ddl);
        addRoleToDropDown(appUsersRoles.userRole.Finance_Signature, ddl);
        addRoleToDropDown(appUsersRoles.userRole.GM_Regional_Planning, ddl);
        addRoleToDropDown(appUsersRoles.userRole.Technical_Planning_Manager, ddl);
        addRoleToDropDown(appUsersRoles.userRole.REVP, ddl);
    }

    private static void addRoleToDropDown(appUsersRoles.userRole eRole, DropDownList ddl)
    {
        try
        {
            ListItem oItem = new ListItem();
            oItem.Text = appUsersRoles.userRoleDesc(eRole);
            oItem.Value = ((int)eRole).ToString();
            ddl.Items.Add(oItem);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }
}