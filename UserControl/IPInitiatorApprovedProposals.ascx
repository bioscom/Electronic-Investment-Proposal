<%@ Control Language="C#" AutoEventWireup="true" CodeFile="IPInitiatorApprovedProposals.ascx.cs" Inherits="UserControl_IPInitiatorApprovedProposals" %>
<%@ Register src="Others/dateControl.ascx" tagname="dateControl" tagprefix="uc1" %>
<table style="width:100%" class="tMainBorder">
    <tr>
        <td>
            <asp:Label ID="Label2" runat="server" Text="Proposals by Year of approval:" Font-Bold="False"></asp:Label>
        </td>
        <td>
            <uc1:dateControl ID="dtDate" runat="server" />
        </td>
        <td>
            <asp:Button ID="viewButton" runat="server" Text="View" Width="100px" OnClick="viewButton_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="3">

<asp:GridView ID="grdView" runat="server"
    AutoGenerateColumns="False" OnRowCommand="grdView_RowCommand"
    AllowPaging="True" PageSize="20" Width="100%"
    OnSorted="grdView_Sorted"
    OnSorting="grdView_Sorting" AllowSorting="True"
    OnPageIndexChanging="grdView_PageIndexChanging">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="IP Number" SortExpression="PROJ_NUM">
            <ItemTemplate>
                <asp:Label ID="labelProjectNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PROJ_NUM") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Project Title" SortExpression="PROJ_TITLE">
            <ItemTemplate>
                <asp:Label ID="labelProjectTitle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PROJ_TITLE") %>' ForeColor="#003366"></asp:Label>
            </ItemTemplate>

            <%--<ItemStyle width="350px" Wrap="True"></ItemStyle>--%>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="JV($mln)" SortExpression="JV">
            <ItemTemplate>
                <div style="text-align: right">
                    <asp:Label ID="labelAmountJV" runat="server" Style="color: Green" Font-Bold="True"
                        Text='<%# DataBinder.Eval(Container.DataItem, "JV") %>'></asp:Label>
                </div>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="SS($mln)" SortExpression="SS">
            <ItemTemplate>
                <div style="text-align: right">
                    <asp:Label ID="labelAmountSS" runat="server" Style="color: Green" Font-Bold="True"
                        Text='<%# DataBinder.Eval(Container.DataItem, "SS") %>'></asp:Label>
                </div>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Status">
            <ItemTemplate>
                <asp:Label ID="labelStatus" runat="server" Text=""></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Date Initiated" SortExpression="DATE_INIT">
            <ItemTemplate>
                <%--<asp:Label id="labelDateInitiated" runat="server" 
                                Text='<%# DataBinder.Eval(Container.DataItem, "DATE_INIT") %>'></asp:Label>--%>
                <asp:Label ID="labelDateInitiated" runat="server" Text='<%# Bind("DATE_INIT", "{0:dd/MM/yyyy}") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Date Approved" SortExpression="DATE_COMMENT">
            <ItemTemplate>
                <%--<asp:Label id="labelDateApproved" runat="server" 
                                Text='<%# DataBinder.Eval(Container.DataItem, "DATE_COMMENT") %>'></asp:Label>--%>
                <asp:Label ID="labelDateApproved" runat="server" Text='<%# Bind("DATE_COMMENT", "{0:dd/MM/yyyy}") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Approved by" SortExpression="APPROVEDBY">
            <ItemTemplate>
                <asp:Label ID="labelApprovedby" runat="server"
                    Text='<%# DataBinder.Eval(Container.DataItem, "APPROVEDBY") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="...">
            <ItemTemplate>
                <asp:LinkButton ID="ViewStatusLinkButton" runat="server"
                    PROPOSALID='<%# DataBinder.Eval(Container.DataItem, "IDPROPOSAL") %>'
                    CommandName="ViewStatus" CommandArgument='<%# Container.DisplayIndex %>'>View Status</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="...">
            <ItemTemplate>
                <asp:LinkButton ID="OriginalProposalLinkButton" runat="server"
                    PROPOSALID='<%# DataBinder.Eval(Container.DataItem, "IDPROPOSAL") %>'
                    CommandName="ViewOriginalProposal" CommandArgument='<%# Container.DisplayIndex %>'>View Proposal</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="...">
            <ItemTemplate>
                <asp:LinkButton ID="forwardProposalLinkButton" runat="server"
                    PROPOSALID='<%# DataBinder.Eval(Container.DataItem, "IDPROPOSAL") %>'
                    CommandName="forwardProposal" CommandArgument='<%# Container.DisplayIndex %>'>Forward IP to my email</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>

    </Columns>
</asp:GridView>
        </td>
    </tr>
</table>

