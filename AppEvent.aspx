<%@ Page Language="C#" MasterPageFile="~/MasterPages/FrontPage.master" AutoEventWireup="True" CodeFile="AppEvent.aspx.cs" Inherits="AppEvent" Title="Investment Proposal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="tMainBorder" style="width: 100%">
        <tr>
            <td class="cHeadTile">
                Error Message Instruction</td>
        </tr>
        <tr>
            <td>
                Please if you are seeing this error message, kindly copy and email to
                <a href="mailto:isaac.bejide@shell.com">isaac.bejide@shell.com</a>.<br />
                <br />
                Thank you.</td>
        </tr>
    </table>
    <table class="tMainBorder" style="width: 100%; height:50%">
        <tr>
            <td class="cHeadTile">
                Error Message</td>
        </tr>
        <tr>
            <td><asp:Label ID="mssgLabel" runat="server" CssClass="Warning" Font-Bold="False"></asp:Label></td>
        </tr>
    </table>
    
</asp:Content>

