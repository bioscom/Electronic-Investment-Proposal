<%@ Page Language="C#" MasterPageFile="~/MasterPages/eIPMasterPage.master" AutoEventWireup="True" CodeFile="EditUser.aspx.cs" Inherits="Common_EditUser" Title="Investment Proposal" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center>
        <table style="width: 700px; margin-bottom: 0px; margin-top: 5em; border-bottom: 0px" class="tMainBorder">
            <tr>
                <td class="cHeadTile" colspan="2">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="#003366" Text="Edit User"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width: 700px; margin-top: 0px" class="tMainBorder" cellpadding="10px" cellspacing="15px">
            <tr>
                <td>&nbsp;</td>
                <td colspan="2">
                    <asp:Label ID="fullNameLabel" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>

            <tr>
                <td style="width: 150px">
                    <asp:Label ID="Label10" runat="server" Text="User Name:"></asp:Label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                        ControlToValidate="userNameTextBox" ErrorMessage="User Name is required"
                        ValidationGroup="User">*</asp:RequiredFieldValidator>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="userNameTextBox" runat="server" Width="200px" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label14" runat="server" Text="User's Role:"></asp:Label>
                    <asp:CompareValidator ID="CompareValidator2" runat="server"
                        ControlToValidate="userRoleDropDownList" ErrorMessage="Select user's role."
                        Type="Integer" ValueToCompare="-1" Operator="NotEqual">*</asp:CompareValidator>
                </td>
                <td style="width: 200px">
                    <asp:DropDownList ID="userRoleDropDownList" runat="server" Width="200px"
                        AutoPostBack="True"
                        OnSelectedIndexChanged="userRoleDropDownList_SelectedIndexChanged">
                        <asp:ListItem Value="-1">--Select Role--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label15" runat="server" Text="User's Company:"></asp:Label>
                    <asp:CompareValidator ID="CompareValidator3" runat="server"
                        ControlToValidate="ddlOUs"
                        ErrorMessage="Select user's company, select N/A where company not applicable"
                        Type="Integer" ValueToCompare="-1" Operator="NotEqual">*</asp:CompareValidator>
                </td>
                <td>
                    <%--<asp:DropDownList ID="companyDropDownList" runat="server" Width="200px"
                        AutoPostBack="True"
                        OnSelectedIndexChanged="companyDropDownList_SelectedIndexChanged">
                        <asp:ListItem Value="-1">--Select Company--</asp:ListItem>
                    </asp:DropDownList>--%>
                    <asp:DropDownList ID="ddlOUs" runat="server" Width="200px">
                        <asp:ListItem Value="-1">[Select User OU]</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:Label ID="Label3" runat="server" ForeColor="Red"
                        Text="Note: Ensure that a specific OU is assigned to User Role Business Process Owner(BPO). Do not select N/A for a BPO" Font-Bold="True"
                        Font-Italic="True"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label11" runat="server" Text="User's Function:"></asp:Label>
                    <asp:CompareValidator ID="CompareValidator1" runat="server"
                        ControlToValidate="functionDropDownList"
                        ErrorMessage="Select user's function, select N/A where function not applicable"
                        Type="Integer" ValueToCompare="-1" Operator="NotEqual">*</asp:CompareValidator>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="functionDropDownList" runat="server" Width="200px">
                        <asp:ListItem Value="-1">--Select User&#39;s Function--</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td colspan="2">
                    <asp:Label ID="Label16" runat="server" ForeColor="Red"
                        Text="Select N/A where function is not applicable." Font-Bold="True"
                        Font-Italic="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ApprovalLimitLabel" runat="server" Text="Approval Limit ($ mln):"></asp:Label>
                </td>
                <td colspan="2">
                    <table style="border: 0; width: 100%">
                        <tr>
                            <td>
                                <asp:RadioButton ID="L1" runat="server" GroupName="ApprovalLimit" />
                            </td>
                            <td>
                                <asp:RadioButton ID="L2" runat="server" GroupName="ApprovalLimit" />
                            </td>
                            <td>
                                <asp:RadioButton ID="L3" runat="server" GroupName="ApprovalLimit" />
                            </td>
                            <td>
                                <asp:RadioButton ID="L4" runat="server" GroupName="ApprovalLimit" />
                            </td>
                            <td>
                                <asp:RadioButton ID="L5" runat="server" GroupName="ApprovalLimit" />
                            </td>
                            <td>
                                <asp:RadioButton ID="NArdb" runat="server" GroupName="ApprovalLimit" Text="N/A" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr>
                <td>&nbsp;             
                </td>
                <td colspan="2">
                    <asp:Button ID="saveButton" runat="server"
                        OnClick="saveButton_Click" Text="Submit" ToolTip="Click to submit this form"
                        Width="100px" />
                    &nbsp;
                    <asp:Button ID="closeButton" runat="server" OnClick="closeButton_Click"
                        Text="Close" ValidationGroup="xxxx" />
                </td>
            </tr>
        </table>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server"
            ShowMessageBox="True" ShowSummary="False" />
    </center>
</asp:Content>
