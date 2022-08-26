using System;
using System.Web;
using System.Web.UI;
using System.Collections.Generic;
using System.Text;

public class aspnetPage : Page
{
    public httpSessionx oSessnx;

    override protected void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
    }

    override protected void OnInit(EventArgs e)
    {
        oSessnx = new httpSessionx(this.Session); //instantiate new object
        base.OnInit(e);
    }

    protected string reqQueryIdx()
    {
        return reqQuerys("Idx");
    }

    protected string reqQueryMsg()
    {
        return reqQuerys("Msg");
    }

    protected string reqQuerys(string sQueryId)
    {
        string sRet = "";
        try
        {
            if (Request.QueryString[sQueryId] != null)
            {
                sRet = Request.QueryString[sQueryId];
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return sRet;
    }
}
