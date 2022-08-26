using System;
using System.Web.UI;

/// <summary>
/// Summary description for ajaxWebExtension
/// </summary>
public class ajaxWebExtension
{
    public ajaxWebExtension()
    {
        
    }

    public static bool showJscriptAlert(Page oPage, object oThis, string sMessage)
    {
        try
        {
            if (sMessage.Length > 0)
            {
                sMessage = sMessage.Replace("'", "''");
                string sAlert = "alert('" + sMessage + "')";
                ScriptManager.RegisterClientScriptBlock(oPage, oThis.GetType(), scriptKeyName(), sAlert, true);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return true;
    }

    public static bool showJscriptAlertCx(System.Web.UI.Control oControl, Object oThis, String sMessage)
    {
        try
        {
            if (sMessage.Length > 0)
            {
                sMessage = sMessage.Replace("'", "''");
                string sAlert = "alert('" + sMessage + "')";
                ScriptManager.RegisterClientScriptBlock(oControl, oThis.GetType(), scriptKeyName(), sAlert, true);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }

        return true;
    }

    private static string scriptKeyName()
    {
        string sNewRndId = "";
        try
        {
            System.Security.Cryptography.RandomNumberGenerator rng = System.Security.Cryptography.RandomNumberGenerator.Create();
            byte[] randomData = new byte[10];

            rng.GetBytes(randomData);
            sNewRndId = BitConverter.ToString(randomData);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return sNewRndId;
    }
}