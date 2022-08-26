<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/eIPMasterPage.master" AutoEventWireup="True" CodeFile="IPRouter.aspx.cs" Inherits="Common_IPRouter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="tMainBorder" style="width: 99%">
        <tr>
            <td class="cHeadTile">IP Not yet supported/Approved. (Note: these IPs can be re routed to another 
                user, if the original support or approver role in the organisation has changed.)</td>
        </tr>

        <tr>
            <td>
                <div>
                    <div style="float: left">
                    </div>
                    <div style="float: right">
                        <asp:Button ID="closeButton" runat="server" OnClick="closeButton_Click"
                            Text="Close" ValidationGroup="xxxx" />
                    </div>

                    <asp:DropDownList ID="proposalList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="proposalList_SelectedIndexChanged">
                        <asp:ListItem Value="-1">Select Proposal to be re routed...</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlDetails" runat="server">
        <eip:IPDetailInfo ID="IPDetailInfo1" runat="server" />
    </asp:Panel>

    <table class="tMainBorder" style="width: 99%; font-size:150%">
        <tr>
            <td>
                <asp:GridView ID="IPRouterGridView" runat="server" AutoGenerateColumns="False" OnRowCommand="IPRouterGridView_RowCommand" Width="100%">
                    <Columns>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Current IP Holder">
                            <ItemTemplate>
                                <asp:Label ID="labelSupportApprover" runat="server"
                                    Text='<%# DataBinder.Eval(Container.DataItem, "FULLNAME") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Role">
                            <ItemTemplate>
                                <asp:Label ID="labelUserRole" runat="server"
                                    Text='<%# DataBinder.Eval(Container.DataItem, "USERROLESID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="labelStatus" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Re-route IP to...">
                            <ItemTemplate>
                                <asp:DropDownList ID="userDropDownList" runat="server" Width="250px">
                                    <asp:ListItem Value="-1">Select User to Re-Route Proposal...</asp:ListItem>
                                </asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="userDropDownList"
                                    ErrorMessage="Select User to Re-Route Proposal." Type="Integer" ValueToCompare="-1" Operator="NotEqual" ValidationGroup="userrole">*</asp:CompareValidator>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="...">
                            <ItemTemplate>
                                <asp:LinkButton ID="ForwardProposalButton" runat="server"
                                    CommandArgument="<%# Container.DisplayIndex %>" CommandName="ForwardProposal"
                                    PROPOSALID='<%# DataBinder.Eval(Container.DataItem, "IDPROPOSAL") %>'
                                    USERROLESID='<%# DataBinder.Eval(Container.DataItem, "USERROLESID") %>'
                                    IDUSERMGT='<%# DataBinder.Eval(Container.DataItem, "IDUSERMGT") %>'
                                    Text="Forward IP to Selected User"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>


            </td>
        </tr>

        <%--  <tr>
            <td style="width: 30%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>--%>
    </table>

    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="userrole" />
</asp:Content>
