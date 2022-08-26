<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/eIPMasterPage.master" AutoEventWireup="true" CodeFile="GMSupportDetails.aspx.cs" Inherits="ApprovalSupportFunction_GMSupportDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 98%">
        <div style="float: left; width: 35%">
            <eip:IPDetailInfo ID="IPDetailInfo1" runat="server" />
        </div>
        <div style="float: left; width: 63%">
            <table style="width: 100%" class="tMainBorder">
                <tr class="cHeadTile">
                    <td>General Managers support details</td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="GMDetailsGridView" runat="server" AutoGenerateColumns="False" Width="100%">
                            <Columns>
                                <%--<asp:TemplateField HeaderText="Support">
                            <ItemTemplate>
                                <asp:Label id="labelSupport" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ROLES") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="General Manager">
                                    <ItemTemplate>
                                        <asp:Label ID="labelSupport" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FULLNAME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Stand">
                                    <ItemTemplate>
                                        <asp:Label ID="labelStand" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "STAND") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Comment">
                                    <ItemTemplate>
                                        <asp:Label ID="labelComment" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "COMMENTS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Date Received">
                                    <ItemTemplate>
                                        <asp:Label ID="labelDateReceived" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DATE_RECEIVED") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Date Reviewed">
                                    <ItemTemplate>
                                        <asp:Label ID="labelDateReviewed" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DATE_COMMENT") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </div>


</asp:Content>
