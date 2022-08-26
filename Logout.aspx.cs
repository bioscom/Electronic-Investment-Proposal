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

public partial class Logout : CustomBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Abandon();
        FormsAuthentication.SignOut();
        //FormsAuthentication.RedirectToLoginPage("Msg=" & sRet);
        Session.Clear();
        Response.Redirect("http://sww.scin.shell.com/ep/epg/sepcin");
    }
}
