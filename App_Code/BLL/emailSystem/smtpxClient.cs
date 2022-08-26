using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.IO;
//using System.Net.Mime;

/// <summary>
/// Summary description for smtpxClient
/// </summary>


public class smtpxClient : smtpxEngine
{
    public smtpxClient(string _sSmtpHost, clxMailAddress _eSender)
        : base(_sSmtpHost, _eSender, new NetworkCredential("", ""), 0)
    {

    }

    public smtpxClient(string _sSmtpHost, clxMailAddress _eSender, int _iSmtpPort)
        : base(_sSmtpHost, _eSender, new NetworkCredential("", ""), _iSmtpPort)
    {

    }

    public smtpxClient(string _sSmtpHost, clxMailAddress _eSender, NetworkCredential _eCredential)
        : base(_sSmtpHost, _eSender, _eCredential, 0)
    {

    }

    public smtpxClient(string _sSmtpHost, clxMailAddress _eSender, NetworkCredential _eCredential, int _iSmtpPort)
        : base(_sSmtpHost, _eSender, _eCredential, _iSmtpPort)
    {

    }

    public bool sendSmtpMail(clxMailAddress eTo, List<clxMailAddress> eCcList, List<clxMailAddress> eBccList, string sSubjet, string sBody)
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
        return dispatchSmtpMail(eToList, eCcList, eBccList, sSubjet, sBody, oAttach);
    }

    public bool sendSmtpMail(clxMailAddress eTo, clxMailAddress eCc, List<clxMailAddress> eBccList, string sSubjet, string sBody)
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
        return dispatchSmtpMail(eToList, eCcList, eBccList, sSubjet, sBody, oAttach);
    }

    public bool sendSmtpMail(List<clxMailAddress> eToList, List<clxMailAddress> eCcList, List<clxMailAddress> eBccList, string sSubjet, string sBody)
    {
        List<string> oAttach = new List<string>();
        return dispatchSmtpMail(eToList, eCcList, eBccList, sSubjet, sBody, oAttach);
    }

    public bool sendSmtpMail(List<clxMailAddress> eToList, List<clxMailAddress> eCcList, List<clxMailAddress> eBccList, string sSubjet, string sBody, string sAttach)
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
        return dispatchSmtpMail(eToList, eCcList, eBccList, sSubjet, sBody, oAttach);
    }

    public bool sendSmtpMail(List<clxMailAddress> eToList, List<clxMailAddress> eCcList, string sSubjet, string sBody)
    {
        List<clxMailAddress> eBccList = new List<clxMailAddress>();
        return sendSmtpMail(eToList, eCcList, eBccList, sSubjet, sBody);
    }

    public bool sendSmtpMail(List<clxMailAddress> eToList, clxMailAddress eCc, clxMailAddress eBcc, string sSubjet, string sBody)
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
        return sendSmtpMail(eToList, eCcList, eBccList, sSubjet, sBody);
    }

    public bool sendSmtpMail(List<clxMailAddress> eToList, clxMailAddress eCc, string sSubjet, string sBody)
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
        return sendSmtpMail(eToList, eCcList, eBccList, sSubjet, sBody);
    }

    public bool sendSmtpMail(List<clxMailAddress> eToList, string sSubjet, string sBody)
    {
        List<clxMailAddress> eBccList = new List<clxMailAddress>();
        List<clxMailAddress> eCcList = new List<clxMailAddress>();

        return sendSmtpMail(eToList, eCcList, eBccList, sSubjet, sBody);
    }

    public bool sendSmtpMail(clxMailAddress eTo, clxMailAddress eCc, string sSubjet, string sBody)
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
        return sendSmtpMail(eToList, eCcList, eBccList, sSubjet, sBody);
    }

    public bool sendSmtpMail(clxMailAddress eTo, string sSubjet, string sBody)
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
        return sendSmtpMail(eToList, eCcList, eBccList, sSubjet, sBody);
    }

    public bool sendSmtpMail(clxMailAddress eTo, List<clxMailAddress> eCcList, string sSubjet, string sBody)
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
        return sendSmtpMail(eToList, eCcList, eBccList, sSubjet, sBody);
    }

    public bool sendSmtpMail(clxMailAddress eTo, string sSubjet, string sBody, string sAttach)
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
        return dispatchSmtpMail(eToList, eCcList, eBccList, sSubjet, sBody, oAttach);
    }

    public bool sendSmtpMailFS(clxMailAddress eTo, string sSubjet, string sBody, List<FileStream> fsAttach, Proposal oProposal)
    {
        List<clxMailAddress> eToList = new List<clxMailAddress>();
        List<clxMailAddress> eBccList = new List<clxMailAddress>();
        List<clxMailAddress> eCcList = new List<clxMailAddress>();
        List<FileStream> oAttach = new List<FileStream>();

        try
        {
            eToList.Add(eTo);
            //oAttach.Add(fsAttach);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return dispatchSmtpMailFS(eToList, eCcList, eBccList, sSubjet, sBody, fsAttach, oProposal);
    }
}