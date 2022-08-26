using System;
using System.Collections.Generic;
using System.Web;

public class CustomBasePage : System.Web.UI.Page
{
    public CustomBasePage()
    {

    }

    public void SessionExpires()
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("~/Index.aspx");
        }

        //
        //CurrentUser = new eipUsers(Apps.LoginIDNoDomain(User.Identity.Name));

        //CurrentUser = new eipUsers(Session["UserID"].ToString());

    }
}
