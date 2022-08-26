<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/FrontPage.master" AutoEventWireup="true" CodeFile="logout.aspx.cs" Inherits="Support_logout" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<table class="tSubFree">
        <tr>
            <td align="center" valign="top" width="40%">
                <table cellpadding="2" cellspacing="0" class="tSubGray">
                    <tr>
                        <td class="cHeadLeft">
                            <asp:Label ID="lblHeader" runat="server" CssClass="fTileNormal12" Font-Bold="True"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="cTextCenta">
                            <br />
                            <br />
                            <img src="../Images/i_passwordKey.gif" width="108" />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="cTextCenta">
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="True"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="cTextCenta">
                            <asp:HyperLink ID="hpkLogin" runat="server" NavigateUrl="~/Default.aspx">My Account Session Login</asp:HyperLink></td>
                    </tr>
                </table>
            </td>
            <td align="center" valign="top" width="60%">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right" valign="top" width="40%">
            </td>
            <td align="center" valign="top" width="60%">
            </td>
        </tr>
        <tr>
            <td align="center" valign="top" width="40%">
            </td>
            <td align="center" valign="top" width="60%">
            </td>
        </tr>
    </table>
</asp:Content>

