<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/FrontPage.master" AutoEventWireup="True" CodeFile="pageNotFound.aspx.cs" Inherits="Profiles_pageNotFound" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="tSubFree">
        <tr>
            <td align="center" valign="top" width="50%">
                <table class="tSubWhite" cellpadding ="2" cellspacing ="0">
                    <tr>
                        <td class="cHeadTileCenta">
                            Resource Not Available</td>
                    </tr>
                    <tr>
                        <td class="cTextCenta">
                            <br />
                            <br />
                            <img src ="../Images/i_ExclaimLogo.gif" width="108"/>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="cTextCenta">
                            <strong>The Requested Resource does not exist in this Application. Please, review
                                the URL and make sure that it is spelt correctly or
                                contact Your Administrator for Help.</strong></td>
                    </tr>
                    <tr>
                        <td class="cTextCenta">
                            <input id="Submit1" onclick="javascript:history.back()" type="submit" 
                                value="Previous Page" /></td>
                    </tr>
                </table>
            </td>
            <td align="center" valign="top" width="50%">
            </td>
        </tr>
        <tr>
            <td align="center" valign="top" width="50%">
            </td>
            <td align="center" valign="top" width="50%">
            </td>
        </tr>
    </table>

</asp:Content>

