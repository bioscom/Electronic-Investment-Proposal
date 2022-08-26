using System;
using System.Web.Security;

public partial class Index : aspnetPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string sRedirect = "";
        try
        {
            if (!IsPostBack) //fresh loading
            {
                string sMsg = this.reqQueryMsg();
                string sRet = "";
                switch (sMsg)
                {
                    case "sLO":
                        sRedirect = "~/Support/logout.aspx?Msg=sLO";
                        break;

                    case "sTl":
                        sRet = useWindowsAuth();
                        if (sRet != "")
                        {
                            sRedirect = "~/Support/pageDenied.aspx?Msg=" + sRet;
                        }
                        break;

                    default:
                        sRet = useWindowsAuth();
                        if (sRet != "")
                        {
                            sRedirect = "~/Support/pageDenied.aspx?Msg=" + sRet;
                        }
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }

        if (sRedirect != "")
        {
            Response.Redirect(sRedirect);
        }
    }

    private string useWindowsAuth()
    {
        string sRet = "Err";
        try
        {
            loginUser oLogin = new loginUser();
            loginUser.loginRet me = oLogin.verifyAppUser();

            switch (me.eStatus)
            {
                case loginUser.statusx.loginSucceed:
                    sRet = "";
                    FormsAuthentication.RedirectFromLoginPage(me.eUserInfo.m_sUserName, true);
                    break;
                case loginUser.statusx.idIsNotFound:
                    sRet = "eId";
                    break;
                case loginUser.statusx.loginFailed:
                    sRet = "Err";
                    break;
                case loginUser.statusx.statusDisabld:
                    sRet = "nId";
                    break;
                case loginUser.statusx.statusUnKnown:
                    sRet = "nId";
                    break;
                case loginUser.statusx.statusLocked:
                    sRet = "lId";
                    break;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return sRet;
    }
}