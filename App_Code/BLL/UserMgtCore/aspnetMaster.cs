using System;
using System.Collections.Generic;

using System.Text;
using System.Web.UI;

public class aspnetMaster : MasterPage
{
    public httpSessionx oSessnx;

    override protected void OnLoad(EventArgs e) //'automatically invoke when page is displayed
    {
        base.OnLoad(e);
    }

    override protected void OnInit(EventArgs e)
    {
        oSessnx = new httpSessionx(this.Session); //'instantiate new object
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
                sRet = Request.QueryString[sQueryId].ToString();
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return sRet;
    }
}
