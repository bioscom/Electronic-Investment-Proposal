<%@ Page Language="C#" MasterPageFile="~/MasterPages/eIPMasterPage.master" AutoEventWireup="True" CodeFile="CreateNewUser.aspx.cs" Inherits="Common_CreateNewUser" Title="Investment Proposal" %>

<%@ Register Src="../UserControl/Search4User.ascx" TagName="Search4User" TagPrefix="uc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnablePartialRendering="true" ID="smtAjaxManager" CombineScripts="false" />
    <center>
        <table style="width: 700px; margin-bottom:0px; margin-top:5em; border-bottom:0px" class="tMainBorder">
            <tr>
                <td class="cHeadTile" colspan="2">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="#003366" Text="Add New User"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width: 700px; margin-top:0px" class="tMainBorder" cellpadding="10px" cellspacing="15px">
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="First Name or Last Name:"></asp:Label>
                </td>
                <td>
                    <uc1:Search4User ID="Search4User1" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label14" runat="server" Text="User's Role:"></asp:Label>
                    <asp:CompareValidator ID="CompareValidator2" runat="server"
                        ControlToValidate="userRoleDropDownList" ErrorMessage="Select user's role."
                        Type="Integer" ValueToCompare="-1" Operator="NotEqual">*</asp:CompareValidator>
                </td>
                <td>
                    <asp:DropDownList ID="userRoleDropDownList" runat="server" Width="200px">
                        <asp:ListItem Value="-1">--Select Role--</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label15" runat="server" Text="User's Company:"></asp:Label>
                    <%--<asp:CompareValidator ID="CompareValidator3" runat="server" 
                    ControlToValidate="companyDropDownList" 
                    ErrorMessage="Select user's company, select N/A where company not applicable" 
                    Type="Integer" ValueToCompare="-1" Operator="NotEqual">*</asp:CompareValidator>--%>
                    <asp:CompareValidator ID="CompareValidator3" runat="server"
                        ControlToValidate="ddlOus" ErrorMessage="Select user's role."
                        Type="Integer" ValueToCompare="-1" Operator="NotEqual">*</asp:CompareValidator>
                </td>
                <td>
                    <%--<asp:DropDownList ID="companyDropDownList" runat="server" 
                    AutoPostBack="True" 
                    onselectedindexchanged="companyDropDownList_SelectedIndexChanged">
                    <asp:ListItem Value="-1">--Select Company--</asp:ListItem>
                </asp:DropDownList>--%>
                    <asp:DropDownList ID="ddlOus" runat="server" Width="200px">
                        <asp:ListItem Value="-1">--Select User OU--</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:Label ID="Label3" runat="server" ForeColor="Red"
                        Text="Note: Ensure that a specific OU is assigned to User Role Business Process Owner(BPO). Do not select N/A for a BPO" Font-Bold="True"
                        Font-Italic="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="User's Function:"></asp:Label>
                    <asp:CompareValidator ID="CompareValidator1" runat="server"
                        ControlToValidate="functionDropDownList"
                        ErrorMessage="Select user's function, select N/A where function not applicable"
                        Type="Integer" ValueToCompare="-1" Operator="NotEqual">*</asp:CompareValidator>
                </td>
                <td>
                    <asp:DropDownList ID="functionDropDownList" runat="server" Width="200px">
                        <asp:ListItem Value="-1">--Select User&#39;s Function--</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Label ID="Label11" runat="server" ForeColor="Red"
                        Text="Select N/A where function is not applicable." Font-Bold="True"
                        Font-Italic="True"></asp:Label>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Approval Limit ($ mln):"></asp:Label>
                </td>
                <td>
                    <table style="width:100%">
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
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="saveButton" runat="server" OnClick="saveButton_Click" Text="Submit" ToolTip="Click to submit this form" Width="100px" />
                    &nbsp;
                <asp:Button ID="closeButton" runat="server" OnClick="closeButton_Click" Text="Close" ValidationGroup="xxxx" />
                </td>
            </tr>
        </table>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server"
            ShowMessageBox="True" ShowSummary="False" />
    </center>
</asp:Content>

