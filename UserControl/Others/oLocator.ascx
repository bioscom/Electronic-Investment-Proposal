<%@ Control Language="C#" AutoEventWireup="true" CodeFile="oLocator.ascx.cs" Inherits="UserControl_Others_oLocator" %>

        <asp:DropDownList ID="drpUserx" runat="server" Width="200px">
            <asp:ListItem Selected="True" Value="0">[Select GID User Name]</asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="txtUserx" runat="server" MaxLength="25" TextMode="SingleLine" Width="200px" ToolTip="Enter First Name or Last Name of the User to search"></asp:TextBox>
        <asp:ImageButton runat="Server" ID="imbEdit" 
            ImageUrl="~/Images/btnEdit.png" Width="20"
            Height="20" CausesValidation="False" onclick="imbEdit_Click" ToolTip="Reset" />
        <asp:ImageButton runat="Server" ID="imbFind" 
            ImageUrl="~/Images/btnFind.png" Width="20"
            Height="20" CausesValidation="False" onclick="imbFind_Click" ToolTip="Click to search for user" />
