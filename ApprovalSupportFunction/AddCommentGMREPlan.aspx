<%@ Page Language="C#" MasterPageFile="~/MasterPages/eIPMasterPage.master" AutoEventWireup="True" CodeFile="AddCommentGMREPlan.aspx.cs" Inherits="ApprovalSupportFunction_AddCommentGMREPlan" Title="Investment Proposal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="width: 40%">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <eip:IPDetailInfo ID="IPDetailInfo1" runat="server" />
                        </td>
                    </tr>
                </table>
                <table style="width: 98%" class="tMainBorder">
                    <tr class="cHeadTile">
                        <td colspan="2">General Manager Regional Planning</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="standLabel" runat="server" Font-Bold="True" Text="Stand:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="SupportStandDropDownList" runat="server" Width="200px"
                                AutoPostBack="True"
                                OnSelectedIndexChanged="SupportStandDropDownList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            <asp:Label ID="commentsLabel" runat="server" Font-Bold="True" Text="Comments:"></asp:Label>
                        </td>
                        <td style="vertical-align: top">
                            <asp:TextBox ID="CommentTextBox" runat="server" Height="100px" Text=""
                                TextMode="MultiLine" Width="330px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            &nbsp;</td>
                        <td style="vertical-align: top">
                                <asp:Button ID="forwardButton" runat="server" OnClick="forwardButton_Click" Text="Submit" Width="100px" />
                                &nbsp;<asp:Button ID="closeButton" runat="server" OnClick="closeButton_Click" Text="Close" Width="100px" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table style="width: 99%" class="tMainBorder">
                    <tr class="cHeadTile">
                        <td>Select General Manager(s) to support this proposal</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="grdView" runat="server" Width="100%" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="...">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="GeneralManager" runat="server" IDUSERMGT='<%# DataBinder.Eval(Container.DataItem, "IDUSERMGT") %>'
                                                EMAILADDY='<%# DataBinder.Eval(Container.DataItem, "USERMAIL") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="General Manager">
                                        <ItemTemplate>
                                            <asp:Label ID="GMLabel" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "FULLNAME") %>' Font-Bold="True"
                                                ForeColor="#000066"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="StatusLabel" runat="server" Text=''></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Comments">
                                        <ItemTemplate>
                                            <asp:Label ID="CommentsLabel" runat="server" Text=''></asp:Label>
                                        </ItemTemplate>
                                        <%--<ItemStyle HorizontalAlign="Left" Width="300px" />--%>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Date Received">
                                        <ItemTemplate>
                                            <asp:Label ID="DateReceivedLabel" runat="server" Text=''></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Date Reviewed">
                                        <ItemTemplate>
                                            <asp:Label ID="DateReviewedLabel" runat="server" Text=''></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="...">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="Reminder" runat="server" IDUSERMGT='<%# DataBinder.Eval(Container.DataItem, "IDUSERMGT") %>'
                                                EMAILADDY='<%# DataBinder.Eval(Container.DataItem, "USERMAIL") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>

                            </asp:GridView>
                        </td>
                    </tr>
                    </table>
                <%--<asp:LinkButton ID="VPDetailedSupportLinkButton" runat="server" OnClick="VPDetailedSupportLinkButton_Click">Click here to view support details from Vice Presidents</asp:LinkButton>--%>
                            <div style="float: right">
                                <asp:Button ID="sendReminderButton" runat="server" OnClick="sendReminderButton_Click" Text="Send Reminder" Width="130px" />
                            </div>
            </td>
        </tr>
    </table>
</asp:Content>
