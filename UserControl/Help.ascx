<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Help.ascx.cs" Inherits="UserControl_Help" %>

<div>
    <b><span style="font-size: x-large; text-decoration: underline">Instruction:</span></b><br />
    <br />Note: IP must conform to BP and ORP requirements.<br />
    <br />1. If IP does not conform, select Not Supported, enter Remarks/Comments and 
    click submit. An email automatically goes 
    to the IP Initiator and IP&#39;s BOM to update the IP.<br />
    <br />2. If IP conforms, select Supported, enter Remarks/Comments and click submit,
    <span style="color: #FF0000">Forward IP to Support Functions is then enabled, 
    submit button is disabled.<br />
    <br />
    </span>
    <span style="color: #339933; font-weight: bold;">Note: IP cannot be forwarded for Functional Support&nbsp;until BPO supports the IP.
    </span><br />
    <br />3. Select the Support Functions relevant 
    to the IP and Click Forward. Automatic email is sent to all required Support Functions.        
    <br />
    <br />
    4. <span style="text-decoration: underline; font-weight: bold">When Support Function does not support IP</span><br />
    After IP Initiator has Updated
        <span style="color: #FF0000; font-weight: bold;">Not Supported</span> 
    comments, check "Should Review IP" box against Support Function to review IP.
    <br /><br />
    <span style="color: #FF0000"><b>Note:</b></span> The Checkboxes will only be available when an IP is currently Not Supported.
    <br />
    <br />
    <span style="color: #FF0000; font-size: large"><b>Mail Not Sent:</b></span><br />
    Note: if when the IP was forwarded to functional support(s), the mail server&#39;s 
    response was &quot;Mail Server not available&quot;.
    <br />
    <br />
    Go to IP Register, click View Status against the IP, click the check box againt 
    the support function then click Forward button below the page, when the mail 
    server is available.
    <br />
</div>                        