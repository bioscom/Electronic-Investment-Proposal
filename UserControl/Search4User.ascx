<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search4User.ascx.cs" Inherits="UserControl_userFinder_Search4User" %>

<asp:UpdatePanel ID="uppAjaxUi" runat="server">
    <ContentTemplate>
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
    </ContentTemplate>
</asp:UpdatePanel>


<%--<div>
    <div style="float:left">
        <asp:Panel ID="textBoxPanel" runat="server" Width="250px">
            <asp:TextBox ID="resultTextBox" runat="server" Width="220px"></asp:TextBox>
            <asp:ImageButton ID="fndBtn" runat="server" ImageUrl="~/Images/btnEdit.png" 
                onclick="fndBtn_Click" ValidationGroup="searchControl" />
        </asp:Panel>
    </div>
    <div style="float:left">
        <asp:Panel ID="dropDownPanel" runat="server" Width="250px">
            <asp:DropDownList ID="resultDropDownList" runat="server" Width="220px" 
                AutoPostBack="True" 
                onselectedindexchanged="resultDropDownList_SelectedIndexChanged"></asp:DropDownList>
        </asp:Panel>
    </div>
</div>--%>