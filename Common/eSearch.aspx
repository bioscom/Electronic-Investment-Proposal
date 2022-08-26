<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/eIPMasterPage.master" AutoEventWireup="true" CodeFile="eSearch.aspx.cs" Inherits="Common_eSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:GridView id="IPTrackRegGridView" runat="server" 
                    AutoGenerateColumns="False" onrowcommand="IPTrackRegGridView_RowCommand" 
                    AllowPaging="True" PageSize="20" Width="100%" onsorted="IPTrackRegGridView_Sorted" 
                    onsorting="IPTrackRegGridView_Sorting" AllowSorting="True" 
                    onpageindexchanging="IPTrackRegGridView_PageIndexChanging">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="IP Number" SortExpression="PROJ_NUM">
            <ItemTemplate>
                <asp:Label id="labelProjectNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PROJ_NUM") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Project Title" SortExpression="PROJ_TITLE">
            <ItemTemplate>
                <asp:Label id="labelProjectTitle" runat="server"  
                                Text='<%# DataBinder.Eval(Container.DataItem, "PROJ_TITLE") %>' ForeColor="#003366" Font-Bold="True"></asp:Label>
            </ItemTemplate>
	    <ItemStyle width="350px" Wrap="True"></ItemStyle>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Project Initiator" SortExpression="PROJ_INIT">
            <ItemTemplate>
                <%--<asp:Label id="labelInitiator" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PROJ_INIT") %>'></asp:Label>--%>
                <a href="mailto:<%# DataBinder.Eval(Container.DataItem, "USERMAIL") %>">
                <%# DataBinder.Eval(Container.DataItem, "PROJ_INIT")%></a>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Amount JV($mln)" SortExpression="JV">
            <ItemTemplate>
                <div style="text-align:right">
                    <asp:Label id="labelAmountJV" runat="server" style="color:Green" Font-Bold="True"
                                    Text='<%# DataBinder.Eval(Container.DataItem, "JV") %>'></asp:Label>
                </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Amount SS($mln)" SortExpression="SS">
            <ItemTemplate>
                <div style="text-align:right">
                    <asp:Label id="labelAmountSS" runat="server" style="color:Green" Font-Bold="True"
                                    Text='<%# DataBinder.Eval(Container.DataItem, "SS") %>'></asp:Label>
                </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Status">
            <ItemTemplate>
                <asp:Label id="labelStatus" runat="server" Text =""></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Date Submitted" SortExpression="DATE_SUBMIT">
            <ItemTemplate>
                <%--<asp:Label id="labelDateForwarded" runat="server" 
                            Text='<%# DataBinder.Eval(Container.DataItem, "DATE_SUBMIT") %>'></asp:Label>--%>
                <asp:Label id="labelDateForwarded" runat="server" Text='<%# Bind("DATE_SUBMIT", "{0:dd/MM/yyyy}") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Date of Last Action" SortExpression="DATE_LAST_ACTIONED">
            <ItemTemplate>
                <%--<asp:Label id="labelDateInitiated" runat="server" 
                                Text='<%# DataBinder.Eval(Container.DataItem, "DATE_LAST_ACTIONED") %>'></asp:Label>--%>
                <asp:Label id="labelDateInitiated" runat="server" Text='<%# Bind("DATE_LAST_ACTIONED", "{0:dd/MM/yyyy}") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <%--<asp:TemplateField HeaderText="Date Supported/Approved by VP">
                            <ItemTemplate>
                                <asp:Label id="labelDateApprovedVP" runat="server" 
                                Text='<%# DataBinder.Eval(Container.DataItem, "DIR_DATE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Date Approved by REVP">
                            <ItemTemplate>
                                <asp:Label id="labelDateApprovedREVP" runat="server" 
                                Text='<%# DataBinder.Eval(Container.DataItem, "VP_DATE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
        <asp:TemplateField HeaderText="...">
            <ItemTemplate>
                <asp:LinkButton id="ViewStatusLinkButton" runat="server" 
                                PROPOSALid='<%# DataBinder.Eval(Container.DataItem, "IDPROPOSAL") %>'  
                                CommandName="ViewStatus" CommandArgument='<%# Container.DisplayIndex %>'>View Status</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <%--<asp:TemplateField HeaderText="...">
            <ItemTemplate>
                <asp:LinkButton id="OriginalProposalLinkButton" runat="server" 
                                PROPOSALid='<%# DataBinder.Eval(Container.DataItem, "IDPROPOSAL") %>' 
                                CommandName="ViewOriginalProposal" CommandArgument='<%# Container.DisplayIndex %>'>View Proposal</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>--%>
        <asp:TemplateField HeaderText="...">
            <ItemTemplate>
                <asp:LinkButton id="forwardProposalLinkButton" runat="server" 
                                PROPOSALid='<%# DataBinder.Eval(Container.DataItem, "IDPROPOSAL") %>' 
                                CommandName="forwardProposal" CommandArgument='<%# Container.DisplayIndex %>'>Forward IP to my email</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <%--<asp:TemplateField HeaderText="...">
            <ItemTemplate>
                <asp:LinkButton id="DeleteProposalLinkButton" runat="server" 
                                PROPOSALid='<%# DataBinder.Eval(Container.DataItem, "IDPROPOSAL") %>'
                                CommandName="DeleteProposal" CommandArgument='<%# Container.DisplayIndex %>'>Delete</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Flag">
            <ItemTemplate>
                <asp:TextBox id="flagImage" runat="server" Width="50" ReadOnly="True" BorderStyle="None"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>--%>
    </Columns>
</asp:GridView>
</asp:Content>

