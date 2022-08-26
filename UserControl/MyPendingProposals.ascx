<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MyPendingProposals.ascx.cs" Inherits="UserControl_MyPendingProposals" %>

<table class="tMainBorder" style="width: 100%">
    <tr>
        <td class="cHeadTile">
            <asp:Label ID="Label1" runat="server" Font-Bold="True"
                Text="Pending Proposals" ForeColor="#003366"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="pendingProposalGridView" runat="server" AutoGenerateColumns="False"
                OnRowCommand="pendingProposalGridView_RowCommand" AllowPaging="True"
                OnPageIndexChanging="pendingProposalGridView_PageIndexChanging"
                Width="100%" PageSize="20">
                <Columns>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="...">
                        <ItemTemplate>
                            <asp:LinkButton ID="AddCommentLinkButton" runat="server"
                                PROPOSALid='<%# DataBinder.Eval(Container.DataItem, "IDPROPOSAL") %>'
                                CommandName="AddComment" CommandArgument='<%# Container.DisplayIndex %>'>Action...</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="IP Number">
                        <ItemTemplate>
                            <asp:Label ID="labelProjectNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PROJ_NUM") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Project Title">
                        <ItemTemplate>
                            <asp:Label ID="labelProjectTitle" runat="server" ForeColor="#003366" Text='<%# DataBinder.Eval(Container.DataItem, "PROJ_TITLE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Initiator">
                        <ItemTemplate>
                            <asp:Label ID="labelInitiator" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PROJ_INIT") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Date Submit">
                        <ItemTemplate>
                            <asp:Label ID="labelDateForwarded" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DATE_SUBMIT") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Date Last Actioned">
                        <ItemTemplate>
                            <asp:Label ID="labelDateLastActioned" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DATE_LAST_ACTIONED") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="...">
                        <ItemTemplate>
                            <asp:LinkButton ID="ViewOriginalProposalLinkButton" runat="server"
                                PROPOSALID='<%# DataBinder.Eval(Container.DataItem, "IDPROPOSAL") %>'
                                CommandName="ViewOriginalProposal" CommandArgument='<%# Container.DisplayIndex %>'>View Proposal</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="...">
                        <ItemTemplate>
                            <asp:LinkButton ID="EditProposalLinkButton" runat="server"
                                PROPOSALID='<%# DataBinder.Eval(Container.DataItem, "IDPROPOSAL") %>'
                                CommandName="EditThisProposal"
                                CommandArgument='<%# Container.DisplayIndex %>'>Edit this proposal</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="...">
                        <ItemTemplate>
                            <asp:LinkButton ID="CheckCommentLinkButton" runat="server"
                                PROPOSALID='<%# DataBinder.Eval(Container.DataItem, "IDPROPOSAL") %>'
                                CommandName="CheckComment" CommandArgument='<%# Container.DisplayIndex %>'>Check Comment</asp:LinkButton>
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
<%--    <tr>
        <td class="cHeadTile">&nbsp;</td>
    </tr>--%>
</table>
