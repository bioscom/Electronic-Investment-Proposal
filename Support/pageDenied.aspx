<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/FrontPage.master"
    AutoEventWireup="true" CodeFile="pageDenied.aspx.cs" Inherits="Support_pageDenied" %>

<%--<asp:Content ID="Content3" ContentPlaceHolderID="rightColumn" runat="Server">--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="tSubFree">
        <tr>
            <td align="center" valign="top" width="40%">
                <table cellpadding="2" cellspacing="0" class="tSubGray">
                    <tr>
                        <td class="cHeadLeft">
                            <asp:Label ID="lblHeader" runat="server" CssClass="fTileNormal12" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="cTextCenta">
                            <br />
                            <br />
                            <img src="../Images/i_keyLock.gif" width="108" />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="cTextCenta">
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td align="center" valign="top" width="60%">
                <table cellpadding="4" cellspacing="4" class="tSubFree">
                    <tr>
                        <td class="cTextCenta" width="25%">
                        </td>
                        <td class="cTextCenta" width="25%">
                        </td>
                        <td class="cTextCenta" width="25%">
                        </td>
                        <td class="cTextCenta" width="25%">
                            <br />
                            <br />
                            <asp:Image ID="imgLogout" runat="server" CssClass="iIconSize64" Height="64px" ImageUrl="~/Images/mySupport64.gif"
                                Width="64px" /><br />
                            <asp:HyperLink ID="hpkLogin" runat="server" NavigateUrl="~/index.aspx?Msg=sTl">My Account Session Login</asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </td>
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
