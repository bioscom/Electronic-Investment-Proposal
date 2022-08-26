<%@ Page Language="C#" AutoEventWireup="True" CodeFile="DigitalSignature.aspx.cs" Inherits="ApprovalSupportFunction_DigitalSignature" Title="Electronic Investment Proposal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <title>Investment Proposal</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../JavaScript/eip.js"></script>
    <%--<base target="_self"/>--%>
     
     <%--<script language="javascript" type="text/javascript">
         window.onblur = function() {
         var upload = document.getElementById("UploadProposal");
             upload.focus();
             //window.focus();
         }
     </script>--%>
     
</head>
<body>
    <form id="form1" runat="server">
    <table class="tMainBorder" style="width: 99%; font-size:70%">
        <tr class="cHeadTile">
            <td style="width:98%" valign="top">
                <asp:ImageButton ID="ImageButton1" runat="server" Height="21px" ImageUrl="~/Images/acrobatLogo.jpg" Width="21px" />
                &nbsp;&nbsp;&nbsp; Adobe Acrobat Digital Signature
            </td>
            <td valign="top">
                <asp:ImageButton ID="closeButton" runat="server" ImageUrl="~/Images/Exit.gif" 
                    ToolTip="Close" Height="19px" Width="22px" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div style="text-align:center; background-color:#abc">
                    <asp:ImageButton ID="digiSignImgBtn" runat="server" 
                        ImageUrl="~/Images/digitalSign.gif" onclick="digiSignImgBtn_Click" 
                        ToolTip="Click here to append Digital Signature to this IP." 
                        Width="82px"/>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
            <center>
                <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" 
                    NavigateUrl="http://sww-knowledge-epg.shell.com/knowtepg1/llisapi.dll?func=ll&amp;objId=12093961&amp;objAction=browse&amp;sort=name">Clich here to view instructions on how 
                to append digital signature
                </asp:HyperLink>
                </center>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <center>
                    Click browse to select the document you have<br/> 
                    digital signed and then click Upload button.
                </center>
            </td>
        </tr>
        <tr>
            <td valign="top" colspan="2">
            <center>
                <asp:FileUpload ID="UploadProposal" runat="server" Width="215px" 
                    Height="23px" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="uploadButton" runat="server" Text="Upload" onclick="uploadButton_Click" />
            </center>
            </td>
        </tr>
        <tr class="cHeadTile">
            <td colspan="2">
                &nbsp;</td>
        </tr>
        </table>
</form>
</body>
</html>