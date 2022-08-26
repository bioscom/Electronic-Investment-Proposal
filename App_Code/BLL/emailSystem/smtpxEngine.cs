using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

/// <summary>
/// Summary description for smtpxEngine
/// </summary>


public class smtpxEngine //: ISmtpxEngine
{
    private clxMailAddress m_eSender;
    private NetworkCredential m_eCredential;
    private string m_sSmtpHost;
    private int m_iSmtpPort;

    public smtpxEngine(string _sSmtpHost, clxMailAddress _eSender, NetworkCredential eCredential, int _iSmtpPort)
    {
        m_sSmtpHost = _sSmtpHost;
        m_eSender = _eSender;
        m_iSmtpPort = _iSmtpPort;
        m_eCredential = eCredential;
    }

    //'Friend Function dispatchSmtpMail(ByVal eToList As List(Of clxMailAdress), ByVal eCcList As List(Of clxMailAdress), ByVal eBccList As List(Of clxMailAdress), ByVal sSubjet As String, ByVal sBody As String, ByVal oAttach As List(Of String)) As Boolean Implements ISmtpxEngine.dispatchSmtpMail
    //'    Dim bRet As Boolean
    //'    Try
    //'        Dim NetMail As New Net.Mail.MailMessage
    //'        NetMail.From = New Net.Mail.MailAddress(m_eSender.userMail, m_eSender.userName)
    //'        For Each eItem As clxMailAdress In eToList
    //'            NetMail.To.Add(New Net.Mail.MailAddress(eItem.userMail, eItem.userName))
    //'        Next
    //'        For Each eItem As clxMailAdress In eCcList
    //'            NetMail.CC.Add(New Net.Mail.MailAddress(eItem.userMail, eItem.userName))
    //'        Next
    //'        For Each eItem As clxMailAdress In eBccList
    //'            NetMail.Bcc.Add(New Net.Mail.MailAddress(eItem.userMail, eItem.userName))
    //'        Next

    //'        NetMail.BodyEncoding = System.Text.Encoding.UTF8
    //'        NetMail.Subject = sSubjet
    //'        NetMail.IsBodyHtml = True
    //'        NetMail.Body = sBody

    //'        For Each sItem As String In oAttach
    //'            Dim sAttach As New Attachment(sItem, MediaTypeNames.Application.Octet)
    //'            NetMail.Attachments.Add(sAttach)
    //'        Next

    //'        Dim MailClient As New Net.Mail.SmtpClient(m_sSmtpHost)
    //'        If (m_eCredential.UserName <> "" And m_eCredential.Password <> "") Then
    //'            MailClient.Credentials = m_eCredential
    //'            MailClient.UseDefaultCredentials = False
    //'        End If
    //'        If m_iSmtpPort > 0 Then MailClient.Port = m_iSmtpPort

    //'        'the userstate can be any object. The object can be accessed in the callback method
    //'        'in this example, we will just use the MailMessage object.
    //'        Dim userState As Object = NetMail
    //'        'wire up the event for when the Async send is completed
    //'        AddHandler MailClient.SendCompleted, AddressOf SmtpClient_OnCompleted

    //'        MailClient.SendAsync(NetMail, userState)


    //'        bRet = True
    //'    Catch smtpEx As SmtpException
    //'        Debug.Fail(smtpEx.TargetSite.Name & vbNewLine & smtpEx.StackTrace & vbNewLine & smtpEx.Message)
    //'        My.Computer.FileSystem.WriteAllText("C:\Users\PhoxWare\Desktop\qwe.txt", smtpEx.Message, True)
    //'    Catch ex As Exception
    //'        Debug.Fail(ex.TargetSite.Name & vbNewLine & ex.Message & vbNewLine & ex.StackTrace)
    //'    End Try
    //'    Debug.Print(bRet.ToString)
    //'    Return bRet
    //'End Function
    //'Public Sub SmtpClient_OnCompleted(ByVal sender As Object, ByVal e As AsyncCompletedEventArgs)
    //'    'Get the Original MailMessage object
    //'    Dim mail As MailMessage = CType(e.UserState, MailMessage)

    //'    'write out the subject
    //'    Dim subject As String = mail.Subject

    //'    If e.Cancelled Then
    //'        Console.WriteLine("Send canceled for mail with subject [{0}].", subject)
    //'    End If
    //'    If Not (e.Error Is Nothing) Then
    //'        Console.WriteLine("Error {1} occurred when sending mail [{0}] ", subject, e.Error.ToString())
    //'    Else
    //'        Console.WriteLine("Message [{0}] sent.", subject)
    //'    End If
    //'End Sub 'SmtpClient_OnCompleted

    public bool dispatchSmtpMail(List<clxMailAddress> eToList, List<clxMailAddress> eCcList, List<clxMailAddress> eBccList, string sSubjet, string sBody, List<string> oAttach) //: ISmtpxEngine.dispatchSmtpMail
    {
        bool bRet = false;
        try
        {
            MailMessage NetMail = new MailMessage();
            NetMail.From = new MailAddress(m_eSender.m_sUserMail, m_eSender.m_sUserName);

            foreach (clxMailAddress eItem in eToList)
            {
                NetMail.To.Add(new MailAddress(eItem.m_sUserMail, eItem.m_sUserName));
            }

            foreach (clxMailAddress eItem in eCcList)
            {
                NetMail.CC.Add(new MailAddress(eItem.m_sUserMail, eItem.m_sUserName));
            }

            foreach (clxMailAddress eItem in eBccList)
            {
                NetMail.Bcc.Add(new MailAddress(eItem.m_sUserMail, eItem.m_sUserName));
            }

            NetMail.BodyEncoding = System.Text.Encoding.UTF8;
            NetMail.Subject = sSubjet;
            NetMail.IsBodyHtml = true;
            NetMail.Body = sBody;

            foreach (string sItem in oAttach)
            {
                Attachment sAttach = new Attachment(sItem, MediaTypeNames.Application.Octet);
                NetMail.Attachments.Add(sAttach);
            }

            SmtpClient MailClient = new SmtpClient(m_sSmtpHost);
            if ((m_eCredential.UserName != "") && (m_eCredential.Password != ""))
            {
                MailClient.Credentials = m_eCredential;
                MailClient.UseDefaultCredentials = false;
            }
            if (m_iSmtpPort > 0) MailClient.Port = m_iSmtpPort;
            MailClient.Send(NetMail);
            //'System.Threading.Thread.Sleep(500)
            bRet = true;
        }
        catch (SmtpException smtpEx)
        {
            appMonitor.logAppExceptions(smtpEx);
            //System.Diagnostics.Debug.Fail(smtpEx.TargetSite.Name + "\n \n" + smtpEx.StackTrace + "\n \n" + smtpEx.Message.ToString());
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }

        return bRet;
    }

    //Send mail with pictures
    public bool dispatchSmtpMail(List<clxMailAddress> eToList, List<clxMailAddress> eCcList, List<clxMailAddress> eBccList, string sSubjet, string sBody, List<string> oAttach, AlternateView altView) //: ISmtpxEngine.dispatchSmtpMail
    {
        bool bRet = false;
        try
        {
            MailMessage NetMail = new MailMessage();
            NetMail.From = new MailAddress(m_eSender.m_sUserMail, m_eSender.m_sUserName);

            foreach (clxMailAddress eItem in eToList)
            {
                NetMail.To.Add(new MailAddress(eItem.m_sUserMail, eItem.m_sUserName));
            }

            foreach (clxMailAddress eItem in eCcList)
            {
                NetMail.CC.Add(new MailAddress(eItem.m_sUserMail, eItem.m_sUserName));
            }

            foreach (clxMailAddress eItem in eBccList)
            {
                NetMail.Bcc.Add(new MailAddress(eItem.m_sUserMail, eItem.m_sUserName));
            }

            NetMail.AlternateViews.Add(altView);
            NetMail.BodyEncoding = System.Text.Encoding.UTF8;
            NetMail.Subject = sSubjet;
            NetMail.IsBodyHtml = true;
            NetMail.Body = sBody;

            foreach (string sItem in oAttach)
            {
                Attachment sAttach = new Attachment(sItem, MediaTypeNames.Application.Octet);
                NetMail.Attachments.Add(sAttach);
            }

            SmtpClient MailClient = new SmtpClient(m_sSmtpHost);
            if ((m_eCredential.UserName != "") && (m_eCredential.Password != ""))
            {
                MailClient.Credentials = m_eCredential;
                MailClient.UseDefaultCredentials = false;
            }
            if (m_iSmtpPort > 0) MailClient.Port = m_iSmtpPort;
            MailClient.Send(NetMail);
            //'System.Threading.Thread.Sleep(500)
            bRet = true;
        }
        catch (SmtpException smtpEx)
        {
            appMonitor.logAppExceptions(smtpEx);
            //System.Diagnostics.Debug.Fail(smtpEx.TargetSite.Name + "\n \n" + smtpEx.StackTrace + "\n \n" + smtpEx.Message.ToString());
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }

        return bRet;
    }

    public bool dispatchSmtpMailFS(List<clxMailAddress> eToList, List<clxMailAddress> eCcList, List<clxMailAddress> eBccList, string sSubjet, string sBody, List<FileStream> fsAttaches, Proposal oProposal) //: ISmtpxEngine.dispatchSmtpMail
    {
        bool bRet = false;
        try
        {
            MailMessage NetMail = new MailMessage();
            NetMail.From = new MailAddress(m_eSender.m_sUserMail, m_eSender.m_sUserName);

            foreach (clxMailAddress eItem in eToList)
            {
                NetMail.To.Add(new MailAddress(eItem.m_sUserMail, eItem.m_sUserName));
            }

            foreach (clxMailAddress eItem in eCcList)
            {
                NetMail.CC.Add(new MailAddress(eItem.m_sUserMail, eItem.m_sUserName));
            }

            foreach (clxMailAddress eItem in eBccList)
            {
                NetMail.Bcc.Add(new MailAddress(eItem.m_sUserMail, eItem.m_sUserName));
            }

            NetMail.BodyEncoding = System.Text.Encoding.UTF8;
            NetMail.Subject = sSubjet;
            NetMail.IsBodyHtml = true;
            NetMail.Body = sBody;

            foreach (FileStream fsAttach in fsAttaches)
            {
                Attachment sAttach = new Attachment(fsAttach, oProposal.m_sProj_Num + ".pdf", MediaTypeNames.Application.Pdf);
                NetMail.Attachments.Add(sAttach);
            }

            SmtpClient MailClient = new SmtpClient(m_sSmtpHost);
            if ((m_eCredential.UserName != "") && (m_eCredential.Password != ""))
            {
                MailClient.Credentials = m_eCredential;
                MailClient.UseDefaultCredentials = false;
            }
            if (m_iSmtpPort > 0) MailClient.Port = m_iSmtpPort;
            MailClient.Send(NetMail);
            //'System.Threading.Thread.Sleep(500)
            bRet = true;
        }
        catch (SmtpException smtpEx)
        {
            appMonitor.logAppExceptions(smtpEx);
            //System.Diagnostics.Debug.Fail(smtpEx.TargetSite.Name + "\n \n" + smtpEx.StackTrace + "\n \n" + smtpEx.Message.ToString());
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }

        return bRet;
    }
}