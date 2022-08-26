<%@ Control Language="C#" AutoEventWireup="true" CodeFile="oApprovedProposals.ascx.cs" Inherits="UserControl_oApprovedProposals" %>

<%@ Register Src="Others/dateControl.ascx" TagName="dateControl" TagPrefix="uc1" %>

<table style="width: 100%" class="tMainBorder">
    <tr class="cHeadTile">
        <td>Approved Proposals</td>
    </tr>
    <tr>
        <td style="vertical-align: top">
            <div>
                <div style="float: left">
                    <asp:Label ID="Label2" runat="server" Text="Proposals by Year of approval:" SkinID="ar" Font-Bold="False"></asp:Label>
                </div>
                <div style="float: left">
                    <uc1:dateControl ID="dtDate" runat="server" />
                </div>
                <div style="float: left">
                    <asp:Button ID="viewButton" runat="server" Text="View" Width="100px" OnClick="viewButton_Click" />
                </div>
            </div>
        </td>
    </tr>
    <tr>
        <td>

            <asp:GridView ID="IPTrackRegGridView" runat="server"
                AutoGenerateColumns="False" OnRowCommand="IPTrackRegGridView_RowCommand"
                AllowPaging="True" PageSize="40" Width="100%"
                OnSorted="IPTrackRegGridView_Sorted"
                OnSorting="IPTrackRegGridView_Sorting" AllowSorting="True"
                OnPageIndexChanging="IPTrackRegGridView_PageIndexChanging">
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
                            <asp:Label ID="labelProjectTitle" runat="server"
                                Text='<%# DataBinder.Eval(Container.DataItem, "PROJ_TITLE") %>' ForeColor="#003366"></asp:Label>
                        </ItemTemplate>

                        <ItemStyle Width="350px" Wrap="True"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Project Initiator" SortExpression="PROJ_INIT">
                        <ItemTemplate>
                            <%--<asp:Label id="labelInitiator" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PROJ_INIT") %>'></asp:Label>--%>
                            <a href="mailto:<%# DataBinder.Eval(Container.DataItem, "USERMAIL") %>"><%# DataBinder.Eval(Container.DataItem, "PROJ_INIT")%></a>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="JV($mln)" SortExpression="JV">
                        <ItemTemplate>
                            <div style="text-align: right">
                                <asp:Label ID="labelAmountJV" runat="server" Style="color: Green" Font-Bold="true"
                                    Text='<%# DataBinder.Eval(Container.DataItem, "JV") %>'></asp:Label>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="SS($mln)" SortExpression="SS">
                        <ItemTemplate>
                            <div style="text-align: right">
                                <asp:Label ID="labelAmountSS" runat="server" Style="color: Green" Font-Bold="true"
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
                            <%--<asp:Label ID="labelDateApproved" runat="server" Text='<%# Bind("DATE_COMMENT", "{0:dd/MM/yyyy}") %>'></asp:Label>--%>
                            <asp:Label ID="labelDateApproved" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Approved by" SortExpression="APPROVEDBY">
                        <ItemTemplate>
                            <asp:Label ID="labelApprovedby" runat="server"
                                Text='<%# DataBinder.Eval(Container.DataItem, "APPROVEDBY") %>'
                                IDUSERMGT='<%# DataBinder.Eval(Container.DataItem, "IDUSERMGT") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="...">
                        <ItemTemplate>
                            <asp:LinkButton ID="ViewStatusLinkButton" runat="server"
                                PROPOSALid='<%# DataBinder.Eval(Container.DataItem, "IDPROPOSAL") %>'
                                CommandName="ViewStatus" CommandArgument='<%# Container.DisplayIndex %>'>View Status</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="...">
                        <ItemTemplate>
                            <asp:LinkButton ID="OriginalProposalLinkButton" runat="server"
                                PROPOSALid='<%# DataBinder.Eval(Container.DataItem, "IDPROPOSAL") %>'
                                CommandName="ViewOriginalProposal" CommandArgument='<%# Container.DisplayIndex %>'>View Proposal</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="...">
                        <ItemTemplate>
                            <asp:LinkButton ID="forwardProposalLinkButton" runat="server"
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
            </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>
