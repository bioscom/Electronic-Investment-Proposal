using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Net;
using System.Net.Mail;
//using System.Net.Mime;

/// <summary>
/// Summary description for smtpxClientExPic
/// for sending mails with pictures
/// </summary>


public class smtpxClientExPic : smtpxEngine
{
    public smtpxClientExPic(string _sSmtpHost, clxMailAddress _eSender)
        : base(_sSmtpHost, _eSender, new NetworkCredential("", ""), 0)
    {

    }

    public smtpxClientExPic(string _sSmtpHost, clxMailAddress _eSender, int _iSmtpPort)
        : base(_sSmtpHost, _eSender, new NetworkCredential("", ""), _iSmtpPort)
    {

    }

    public smtpxClientExPic(string _sSmtpHost, clxMailAddress _eSender, NetworkCredential _eCredential)
        : base(_sSmtpHost, _eSender, _eCredential, 0)
    {

    }

    public smtpxClientExPic(string _sSmtpHost, clxMailAddress _eSender, NetworkCredential _eCredential, int _iSmtpPort)
        : base(_sSmtpHost, _eSender, _eCredential, _iSmtpPort)
    {

    }

    public bool sendSmtpMail(clxMailAddress eTo, List<clxMailAddress> eCcList, List<clxMailAddress> eBccList, string sSubjet, string sBody, AlternateView altView)
    {
        List<clxMailAddress> eToList = new List<clxMailAddress>();
        List<string> oAttach = new List<string>();
        try
        {
            eToList.Add(eTo);
        }
        catch (SmtpException smtpEx)
        {
            System.Diagnostics.Debug.Fail(smtpEx.TargetSite.Name + "\n \n" + smtpEx.StackTrace + "\n \n" + smtpEx.Message.ToString());
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return dispatchSmtpMail(eToList, eCcList, eBccList, sSubjet, sBody, oAttach, altView);
    }

    public bool sendSmtpMail(clxMailAddress eTo, clxMailAddress eCc, List<clxMailAddress> eBccList, string sSubjet, string sBody, AlternateView altView)
    {
        List<clxMailAddress> eToList = new List<clxMailAddress>();
        List<clxMailAddress> eCcList = new List<clxMailAddress>();
        List<string> oAttach = new List<string>();

        try
        {
            eToList.Add(eTo);
            eCcList.Add(eCc);
        }
        catch (SmtpException smtpEx)
        {
            System.Diagnostics.Debug.Fail(smtpEx.TargetSite.Name + "\n \n" + smtpEx.StackTrace + "\n \n" + smtpEx.Message.ToString());
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return dispatchSmtpMail(eToList, eCcList, eBccList, sSubjet, sBody, oAttach, altView);
    }

    public bool sendSmtpMail(List<clxMailAddress> eToList, List<clxMailAddress> eCcList, List<clxMailAddress> eBccList, string sSubjet, string sBody, AlternateView altView)
    {
        List<string> oAttach = new List<string>();
        return dispatchSmtpMail(eToList, eCcList, eBccList, sSubjet, sBody, oAttach, altView);
    }

    public bool sendSmtpMail(List<clxMailAddress> eToList, List<clxMailAddress> eCcList, List<clxMailAddress> eBccList, string sSubjet, string sBody, string sAttach, AlternateView altView)
    {
        List<string> oAttach = new List<string>();
        try
        {
            oAttach.Add(sAttach);
        }
        catch (SmtpException smtpEx)
        {
            System.Diagnostics.Debug.Fail(smtpEx.TargetSite.Name + "\n \n" + smtpEx.StackTrace + "\n \n" + smtpEx.Message.ToString());
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return dispatchSmtpMail(eToList, eCcList, eBccList, sSubjet, sBody, oAttach, altView);
    }

    public bool sendSmtpMail(List<clxMailAddress> eToList, List<clxMailAddress> eCcList, string sSubjet, string sBody, AlternateView altView)
    {
        List<clxMailAddress> eBccList = new List<clxMailAddress>();
        return sendSmtpMail(eToList, eCcList, eBccList, sSubjet, sBody, altView);
    }

    public bool sendSmtpMail(List<clxMailAddress> eToList, clxMailAddress eCc, clxMailAddress eBcc, string sSubjet, string sBody, AlternateView altView)
    {
        List<clxMailAddress> eBccList = new List<clxMailAddress>();
        List<clxMailAddress> eCcList = new List<clxMailAddress>();
        try
        {
            eBccList.Add(eBcc);
            eCcList.Add(eCc);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return sendSmtpMail(eToList, eCcList, eBccList, sSubjet, sBody, altView);
    }

    public bool sendSmtpMail(List<clxMailAddress> eToList, clxMailAddress eCc, string sSubjet, string sBody, AlternateView altView)
    {
        List<clxMailAddress> eBccList = new List<clxMailAddress>();
        List<clxMailAddress> eCcList = new List<clxMailAddress>();
        try
        {
            eCcList.Add(eCc);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return sendSmtpMail(eToList, eCcList, eBccList, sSubjet, sBody, altView);
    }

    public bool sendSmtpMail(List<clxMailAddress> eToList, string sSubjet, string sBody, AlternateView altView)
    {
        List<clxMailAddress> eBccList = new List<clxMailAddress>();
        List<clxMailAddress> eCcList = new List<clxMailAddress>();

        return sendSmtpMail(eToList, eCcList, eBccList, sSubjet, sBody, altView);
    }

    public bool sendSmtpMail(clxMailAddress eTo, clxMailAddress eCc, string sSubjet, string sBody, AlternateView altView)
    {
        List<clxMailAddress> eToList = new List<clxMailAddress>();
        List<clxMailAddress> eBccList = new List<clxMailAddress>();
        List<clxMailAddress> eCcList = new List<clxMailAddress>();
        try
        {
            eToList.Add(eTo);
            eCcList.Add(eCc);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return sendSmtpMail(eToList, eCcList, eBccList, sSubjet, sBody, altView);
    }

    public bool sendSmtpMail(clxMailAddress eTo, string sSubjet, string sBody, AlternateView altView)
    {
        List<clxMailAddress> eToList = new List<clxMailAddress>();
        List<clxMailAddress> eBccList = new List<clxMailAddress>();
        List<clxMailAddress> eCcList = new List<clxMailAddress>();

        try
        {
            eToList.Add(eTo);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return sendSmtpMail(eToList, eCcList, eBccList, sSubjet, sBody, altView);
    }

    public bool sendSmtpMail(clxMailAddress eTo, List<clxMailAddress> eCcList, string sSubjet, string sBody, AlternateView altView)
    {
        List<clxMailAddress> eToList = new List<clxMailAddress>();
        List<clxMailAddress> eBccList = new List<clxMailAddress>();
        try
        {
            eToList.Add(eTo);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return sendSmtpMail(eToList, eCcList, eBccList, sSubjet, sBody, altView);
    }

    public bool sendSmtpMail(clxMailAddress eTo, string sSubjet, string sBody, string sAttach, AlternateView altView)
    {
        List<clxMailAddress> eToList = new List<clxMailAddress>();
        List<clxMailAddress> eBccList = new List<clxMailAddress>();
        List<clxMailAddress> eCcList = new List<clxMailAddress>();
        List<string> oAttach = new List<string>();

        try
        {
            eToList.Add(eTo);
            oAttach.Add(sAttach);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return dispatchSmtpMail(eToList, eCcList, eBccList, sSubjet, sBody, oAttach, altView);
    }
}