<%@ Control Language="C#" AutoEventWireup="true" CodeFile="IPInitiatorPendingProposals.ascx.cs" Inherits="UserControl_IPInitiatorPendingProposals" %>

<table style="width: 99%;" class="tMainBorder">
    <tr>
        <td style="background-color: White">
            <asp:GridView ID="grdView" runat="server" AutoGenerateColumns="False"
                OnRowCommand="grdView_RowCommand"
                OnSelectedIndexChanged="grdView_SelectedIndexChanged"
                AllowPaging="True" OnLoad="grdView_Load"
                OnPageIndexChanging="grdView_PageIndexChanging" PageSize="20"
                Width="100%">
                <Columns>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="IP Number">
                        <ItemTemplate>
                            <asp:Label ID="labelProjectNumber" runat="server" ForeColor="Navy" Text='<%# DataBinder.Eval(Container.DataItem, "PROJ_NUM") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Project Title">
                        <ItemTemplate>
                            <asp:Label ID="labelProjectTitle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PROJ_TITLE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Date Initiated">
                        <ItemTemplate>
                            <asp:Label ID="labelDateInit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DATE_INIT") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Date Submitted" SortExpression="DATE_SUBMIT">
                        <ItemTemplate>
                            <%--<asp:Label id="labelDateForwarded" runat="server" 
                            Text='<%# DataBinder.Eval(Container.DataItem, "DATE_SUBMIT") %>'></asp:Label>--%>
                            <asp:Label ID="labelDateForwarded" runat="server" Text='<%# Bind("DATE_SUBMIT", "{0:dd/MM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--<asp:TemplateField HeaderText="Date Forwarded">
                        <ItemTemplate>
                            <asp:Label id="labelDateForwarded" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DATE_SUBMIT") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

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

                    <asp:TemplateField HeaderText="Date of Last Action" SortExpression="DATE_LAST_ACTIONED">
                        <ItemTemplate>
                            <%--<asp:Label id="labelDateInitiated" runat="server" 
                                Text='<%# DataBinder.Eval(Container.DataItem, "DATE_LAST_ACTIONED") %>'></asp:Label>--%>
                            <asp:Label ID="labelDateInitiated" runat="server" Text='<%# Bind("DATE_LAST_ACTIONED", "{0:dd/MM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="...">
                        <ItemTemplate>
                            <asp:LinkButton ID="ViewStatusLinkButton" runat="server"
                                PROPOSALID='<%# DataBinder.Eval(Container.DataItem, "IDPROPOSAL") %>'
                                CommandName="ViewProposalStatus" CommandArgument='<%# Container.DisplayIndex %>'>View Status</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="...">
                        <ItemTemplate>
                            <asp:LinkButton ID="EditProposalLinkButton" runat="server"
                                PROPOSALID='<%# DataBinder.Eval(Container.DataItem, "IDPROPOSAL") %>'
                                CommandName="EditThisProposal" CommandArgument='<%# Container.DisplayIndex %>'>Edit Proposal</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="...">
                        <ItemTemplate>
                            <asp:LinkButton ID="forwardProposalLinkButton" runat="server"
                                PROPOSALID='<%# DataBinder.Eval(Container.DataItem, "IDPROPOSAL") %>'
                                CommandName="forwardProposal" CommandArgument='<%# Container.DisplayIndex %>'>Forward IP to my email</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="...">
                        <ItemTemplate>
                            <asp:LinkButton id="OriginalProposalLinkButton" runat="server" 
                                PROPOSALID='<%# DataBinder.Eval(Container.DataItem, "IDPROPOSAL") %>' 
                                CommandName="ViewOriginalProposal" CommandArgument='<%# Container.DisplayIndex %>'>View 
                            Proposal</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <%--<asp:TemplateField HeaderText="...">
                        <ItemTemplate>
                            <asp:LinkButton id="DeleteProposalLinkButton" runat="server" 
                                PROPOSALID='<%# DataBinder.Eval(Container.DataItem, "IDPROPOSAL") %>'
                                CommandName="DeleteProposal" CommandArgument='<%# Container.DisplayIndex %>'
                                onclientclick="return confirm('Are you sure you want to delete this proposal?')">Delete</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Flag">
                        <ItemTemplate>
                            <asp:TextBox id="flagImage" runat="server" Width="50" ReadOnly="True" BorderStyle="None"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>


