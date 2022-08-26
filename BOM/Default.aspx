<%@ Page Language="C#" MasterPageFile="~/MasterPages/eIPMasterPage.master" AutoEventWireup="True" CodeFile="Default.aspx.cs" Inherits="BOM_Default" Title="Investment Proposal" %>

<asp:Content id="Content1" ContentPlaceHolderid="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%">
        <tr>
            <td class="cHeadTile">
                <asp:Label id="Label1" runat="server" Font-Bold="True" 
                    Text="My Proposals" ForeColor="White"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
    <div style="font-size:105%">
    <asp:GridView id="myProposalGridView" runat="server" AutoGenerateColumns="False" 
            onrowcommand="myProposalGridView_RowCommand" 
            onselectedindexchanged="myProposalGridView_SelectedIndexChanged" 
            AllowPaging="True" onload="myProposalGridView_Load" 
            onpageindexchanging="myProposalGridView_PageIndexChanging" PageSize="20" 
            Width="100%">
    <Columns>
   
        <asp:TemplateField>
            <ItemTemplate>
               <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="Date Initiated">
            <ItemTemplate>
                <asp:Label id="labelDateInitiated" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DATE_INIT") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="Date Forwarded">
            <ItemTemplate>
                <asp:Label id="labelDateForwarded" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DATE_SUBMIT") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="IP Number">
            <ItemTemplate>
                <asp:Label id="labelProjectNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PROJ_NUM") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="Project Title">
            <ItemTemplate>
                <asp:Label id="labelProjectTitle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PROJ_TITLE") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="Project Initiator">
                <ItemTemplate>
                    <asp:Label id="labelInitiator" runat="server" 
                    Text='<%# DataBinder.Eval(Container.DataItem, "PROJ_INIT") %>'></asp:Label>
                </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="...">
            <ItemTemplate>
                <asp:LinkButton id="ViewProposalStatusLinkButton" runat="server" 
                    PROPOSALid='<%# DataBinder.Eval(Container.DataItem, "IDPROPOSAL") %>' 
                    CommandName="ViewProposalStatus" CommandArgument='<%# Container.DisplayIndex %>'>View Proposal Status</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        
        <%--<asp:TemplateField HeaderText="...">
            <ItemTemplate>
                <asp:LinkButton id="OriginalProposalLinkButton" runat="server" 
                    PROPOSALid='<%# DataBinder.Eval(Container.DataItem, "IDPROPOSAL") %>' 
                    CommandName="ViewOriginalProposal" CommandArgument='<%# Container.DisplayIndex %>'>View 
                Proposal</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>--%>
        
        <asp:TemplateField HeaderText="...">
            <ItemTemplate>
                <asp:LinkButton id="forwardProposalLinkButton" runat="server" 
                    PROPOSALid='<%# DataBinder.Eval(Container.DataItem, "IDPROPOSAL") %>' 
                    CommandName="forwardProposal" CommandArgument='<%# Container.DisplayIndex %>'>Forward IP to my email</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        
    </Columns>
    </asp:GridView>
    </div>
            </td>
        </tr>
        <tr>
            <td class="cHeadTile">
                &nbsp;</td>
        </tr>
        </table>
</asp:Content>

