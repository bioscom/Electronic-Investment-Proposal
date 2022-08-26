<%@ Control Language="C#" AutoEventWireup="true" CodeFile="dateControl.ascx.cs" Inherits="Others_UserControls_dateControl" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<table style="border-collapse:collapse; text-space-collapse:collapse">
    <tr>
        <td style="vertical-align:top; text-align:left">
            <asp:TextBox ID="txtDate" runat="server" Width="150px"></asp:TextBox>
            <%--<asp:ImageButton ID="imgBtnStartDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" ValidationGroup="yyyy" />--%>
            <ajaxToolkit:CalendarExtender ID="txtDateExt"  
                runat="server" Enabled="True" EnableViewState="true"
                PopupButtonID="imgBtnStartDate" TargetControlID="txtDate" Format="dd-MM-yyyy">
            </ajaxToolkit:CalendarExtender>
        </td>
        <td style="vertical-align:top; text-align:left"'>
            <asp:ImageButton ID="imgBtnStartDate" runat="server" Height="18px" ImageUrl="~/Images/Calendar_scheduleHS.png" ValidationGroup="yyyy" />
        </td>
    </tr>
</table>