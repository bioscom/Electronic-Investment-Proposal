<%@ Page Language="C#" MasterPageFile="~/MasterPages/FrontPage.master" AutoEventWireup="True" CodeFile="Index.aspx.cs" Inherits="Index" Title="Investment Proposal"%>

<asp:Content id="Content1" ContentPlaceHolderid="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript" src="JavaScripts/eip.js"></script>
    <div>
        <table style="width: 99%" class="tMainBorder">
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <center>
                        <asp:Label ID="AccessLabel" runat="server" Font-Bold="True" Font-Size="Small" 
                            ForeColor="Red" Text=""></asp:Label>
                    </center>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <center>
                        <asp:Label ID="regMemLabel" runat="server" Font-Bold="True" Font-Size="Small" 
                            ForeColor="Red" Text=""></asp:Label>
                    </center>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="text-align:center">
                        &nbsp;<asp:LinkButton ID="hereLinkButton" runat="server" Font-Bold="True" 
                        Font-Size="Small" PostBackUrl="~/Index.aspx">Click here to continue...</asp:LinkButton> 
                    </div>
                </td>
            </tr>
        </table>
    
    </div>
</asp:Content>