<%@ Page Language="C#" MasterPageFile="~/MasterPages/eIPMasterPage.master" AutoEventWireup="True" CodeFile="LeaveMgt.aspx.cs" Inherits="IPAdministrator_LeaveMgt" Title="Investment Proposal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="tMainBorder" style="width: 50%">
        <tr>
            <td class="cHeadTile">Authority Delegation (Leave Management)</td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="ddlRoles" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRoles_SelectedIndexChanged" Width="250px">
                    <asp:ListItem Value="-1">Select Role...</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <div style="background-color: White; width: 99%">
                    <asp:GridView ID="grdView" runat="server" Width="100%" AutoGenerateColumns="False">

                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="" SortExpression="FULLNAME">
                                <ItemTemplate>
                                    <asp:Label ID="bpoLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FULLNAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Set as Default">
                                <ItemTemplate>
                                    <asp:CheckBox ID="BPOCheckBox" runat="server" IDUSERMGT='<%# DataBinder.Eval(Container.DataItem, "IDUSERMGT") %>'
                                        FLAG_COLOR='<%# DataBinder.Eval(Container.DataItem, "FLAG_COLOR") %>' GroupName="outOffice" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="statusLabel" runat="server" Text='' ForeColor="RED" Font-Bold="True"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <center>
                    <asp:Button ID="updateButton" runat="server" Text="Update" OnClick="updateButton_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="closeButton" runat="server" OnClick="closeButton_Click" Text="Close" ValidationGroup="xxxx" />
                </center>
            </td>
        </tr>
    </table>
    <table style="width: 50%;" class="tMainBorder">
        <tr>
            <td colspan="5">
                <%--<div style="background-color:#abc; font-size:100%">--%>Note:<br />
                <br />
                The <asp:Label ID="lblRole" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label> &nbsp;is the default 
                <asp:Label ID="lblRoleDesc" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label> &nbsp;to whom all transactions default to automatically.<br />
                When going on leave do 
                the followings:
                <ul>
                    <li>Select &quot;Set as <asp:Label ID="lblRoleDesc2" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label> &quot; option button against the <asp:Label ID="lblRoleDesc3" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label> &nbsp;you want to set as Default,</li>
                    <li>then click Update button.</li>
                </ul>
                This changes the status to Out of Office and makes the selected <asp:Label ID="lblRoleDesc4" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label> &nbsp;to 
                be the Current <asp:Label ID="lblRoleDesc5" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label> .<br />
                <br />
                When the <asp:Label ID="lblRoleDesc6" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label> &nbsp;resumes, repeat the same process.
                The case is reversed.<br />
                <br />
                In case of any further assistance, contact  [<asp:Label
                    ID="SysAdminEmailAddress" runat="server"></asp:Label>
                ],&nbsp; [x<asp:Label ID="SysAdminExtension" runat="server"></asp:Label>
                ]<br />
                <%--</div>--%>
            </td>
        </tr>
    </table>
</asp:Content>

