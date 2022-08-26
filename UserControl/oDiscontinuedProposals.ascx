<%@ Control Language="C#" AutoEventWireup="true" CodeFile="oDiscontinuedProposals.ascx.cs" Inherits="UserControl_oDiscontinuedProposals" %>

<table style="width: 99%" class="tMainBorder">
    <tr>
        <td class="cHeadTile">
            <asp:Label ID="Label1" runat="server" Font-Bold="True"
                Text="Discontinued Proposals"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <center>
                    <asp:Label ID="currentPageLabel" runat="server" ForeColor="#006600"></asp:Label>
                </center>
        </td>
    </tr>
    <tr>
        <td>
            <div style="background-color: White">
                <asp:GridView ID="pendingProposalGridView" runat="server" AutoGenerateColumns="False"
                    OnRowCommand="pendingProposalGridView_RowCommand" AllowPaging="True"
                    OnPageIndexChanging="pendingProposalGridView_PageIndexChanging"
                    Width="100%" AllowSorting="True"
                    OnSorting="pendingProposalGridView_Sorting" PageSize="20">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="IP Number" SortExpression="PROJ_NUM">
                            <ItemTemplate>
                                <asp:Label ID="labelProjectNumber" runat="server" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem, "PROJ_NUM") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Project Title" SortExpression="PROJ_TITLE">
                            <ItemTemplate>
                                <asp:Label ID="labelProjectTitle" runat="server" Font-Bold="true" ForeColor="#003366" Text='<%# DataBinder.Eval(Container.DataItem, "PROJ_TITLE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount JV($mln)" SortExpression="JV">
                            <ItemTemplate>
                                <div style="text-align: right">
                                    <asp:Label ID="labelAmountJV" runat="server" Style="color: Green" Font-Bold="true" 
                                        Text='<%# DataBinder.Eval(Container.DataItem, "JV") %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount SS($mln)" SortExpression="SS">
                            <ItemTemplate>
                                <div style="text-align: right">
                                    <asp:Label ID="labelAmountSS" runat="server" Style="color: Green" Font-Bold="true" 
                                        Text='<%# DataBinder.Eval(Container.DataItem, "SS") %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Initiator" SortExpression="PROJ_INIT">
                            <ItemTemplate>
                                <%--<asp:Label id="labelInitiator" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PROJ_INIT") %>'></asp:Label>--%>
                                <a href="mailto:<%# DataBinder.Eval(Container.DataItem, "USERMAIL") %>">
                                    <%# DataBinder.Eval(Container.DataItem, "PROJ_INIT")%></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date Initiated" SortExpression="DATE_INIT">
                            <ItemTemplate>
                                <asp:Label ID="labelDateInitiated" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DATE_INIT") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date Forwarded" SortExpression="DATE_SUBMIT">
                            <ItemTemplate>
                                <asp:Label ID="labelDateForwarded" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DATE_SUBMIT") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="labelStatus" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="...">
                            <ItemTemplate>
                                <asp:LinkButton ID="ViewOriginalProposalLinkButton" runat="server"
                                    PROPOSALid='<%# DataBinder.Eval(Container.DataItem, "IDPROPOSAL") %>'
                                    CommandName="ViewOriginalProposal" CommandArgument='<%# Container.DisplayIndex %>'>View Proposal</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="...">
                            <ItemTemplate>
                                <asp:LinkButton ID="ViewStatusLinkButton" runat="server"
                                    PROPOSALid='<%# DataBinder.Eval(Container.DataItem, "IDPROPOSAL") %>'
                                    CommandName="ViewStatus" CommandArgument='<%# Container.DisplayIndex %>'>View Status</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%-- <asp:TemplateField HeaderText="...">
                                <ItemTemplate>
                                    <asp:LinkButton id="RemarkLinkButton" runat="server" 
                PROPOSALid='<%# DataBinder.Eval(Container.DataItem, "IDPROPOSAL") %>'
                CommandName="Remark" CommandArgument='<%# Container.DisplayIndex %>'>Support/Approval Process</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>--%>

                        <asp:TemplateField HeaderText="...">
                            <ItemTemplate>
                                <asp:LinkButton ID="ReactivateLinkButton" runat="server"
                                    PROPOSALid='<%# DataBinder.Eval(Container.DataItem, "IDPROPOSAL") %>'
                                    CommandName="Reactivate" CommandArgument='<%# Container.DisplayIndex %>'>Reactivate</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="...">
                            <ItemTemplate>
                                <asp:LinkButton ID="DeleteProposalLinkButton" runat="server"
                                    PROPOSALid='<%# DataBinder.Eval(Container.DataItem, "IDPROPOSAL") %>'
                                    CommandName="DeleteProposal" CommandArgument='<%# Container.DisplayIndex %>'>Delete</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%-- <asp:TemplateField HeaderText="Flag">
                                <ItemTemplate>
                                    <asp:TextBox id="flagImage" runat="server" Width="50" ReadOnly="True" BorderStyle="None"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                    </Columns>
                </asp:GridView>
            </div>
        </td>
    </tr>
</table>
