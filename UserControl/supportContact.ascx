<%@ Control Language="C#" AutoEventWireup="true" CodeFile="supportContact.ascx.cs" Inherits="UserControl_supportContact" %>

<table style="width: 18.9em; margin-left:0em" class="tMainBorder">
    <tr>
        <td class="cHeadTile">Support Contacts</td>
    </tr>
    <tr>
        <td>
            <asp:BulletedList ID="supportBlst" runat="server" Font-Size="11px">
            </asp:BulletedList>
            <center>
                <asp:HyperLink ID="templateHyperLink" runat="server" NavigateUrl="http://sww-knowledge-epg.shell.com/knowtepg1/llisapi.dll?func=ll&objId=12093961&objAction=browse&sort=name" Target="_blank">HELP</asp:HyperLink>
            <center/>
            <br />
        </td>
    </tr>
</table>
