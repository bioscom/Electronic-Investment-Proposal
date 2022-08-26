<%@ Page Language="C#" MasterPageFile="~/MasterPages/eIPMasterPage.master" AutoEventWireup="True" CodeFile="CreateIP.aspx.cs" Inherits="CreateIP" Title="Investment Proposal" %>
<%@ Register Namespace="AjaxControlToolkit" TagPrefix="AjaxControlToolkit" %>
<%@ Register Src="UserControl/Others/oLocator.ascx" TagName="oLocator" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 600px;" class="tMainBorder">
        <tr>
            <td class="cHeadTile" colspan="3">Create Investment Proposal</td>
        </tr>
        <tr>
            <td style="width: 30%">
                <asp:Label ID="Label110" runat="server" Text="Organisational Unit :   "></asp:Label>
                <asp:CompareValidator ID="CompareValidator1" runat="server"
                    ControlToValidate="OUDropDownList" ErrorMessage="Please select Organisational Unit"
                    Operator="NotEqual" Type="Integer"
                    ValueToCompare="-1" ValidationGroup="OUValidation">*</asp:CompareValidator>
            </td>
            <td colspan="2">
                <asp:DropDownList ID="OUDropDownList" runat="server" Width="250px" CausesValidation="True" ValidationGroup="OUValidation">
                    <asp:ListItem Value="-1">[Please Select OU]</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="ipSourceLabel" runat="server" Text="IP Souce :   "></asp:Label>
                <asp:CompareValidator ID="CompareValidator3" runat="server"
                    ControlToValidate="IPSourceDropDownList" ErrorMessage="Please select IP Source"
                    Operator="NotEqual" Type="Integer"
                    ValueToCompare="-1" ValidationGroup="OUValidation">*</asp:CompareValidator>
            </td>
            <td colspan="2">
                <asp:DropDownList ID="IPSourceDropDownList" runat="server" Width="250px">
                    <asp:ListItem Value="-1">[Please Select IP Source]</asp:ListItem>
                    <asp:ListItem>SPDC</asp:ListItem>
                    <asp:ListItem Value="2">SNEPCO</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Project Name:   "></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                    ControlToValidate="projTitleTextBox"
                    ErrorMessage="Project Name is required">*</asp:RequiredFieldValidator>
            </td>
            <td colspan="2">
                <asp:TextBox ID="projTitleTextBox" runat="server" Width="80%"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="Label111" runat="server" Text="Project Description:   "></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                    ControlToValidate="projDescTextBox"
                    ErrorMessage="Project Description is required">*</asp:RequiredFieldValidator>
            </td>
            <td colspan="2">
                <asp:TextBox ID="projDescTextBox" runat="server" Width="80%" Height="100px"
                    TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label112" runat="server" Text="EP Priority Alignment:   "></asp:Label>
                <asp:CompareValidator ID="CompareValidator2" runat="server"
                    ControlToValidate="EPPriorityDropDownList" ErrorMessage="Please select EPPriority Alignment"
                    Operator="NotEqual" Type="Integer"
                    ValueToCompare="-1">*</asp:CompareValidator>
            </td>
            <td colspan="2">
                <asp:DropDownList ID="EPPriorityDropDownList" runat="server" Width="250px">
                    <asp:ListItem Value="-1">[Please Select EP EPriority Alignment]</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <%--<tr>
        <td>
            &nbsp;</td>
        <td>
        <div style="color:Red; width:72%">
            The Amount JV ($mln) and IP Value (Amount SS $mln) must be equal to the value on the document to be uploaded. This is required for IP to go through the expected approval.
        </div>
        </td>
    </tr>--%>
        <tr>
            <td>
                <asp:Label ID="Label113" runat="server" Text="Line Team Lead:   "></asp:Label>
                <asp:CompareValidator ID="CompareValidator4" runat="server"
                    ControlToValidate="LineTeamLeadDropDownList" ErrorMessage="Please select your Line Team Lead"
                    Operator="NotEqual" Type="Integer"
                    ValueToCompare="-1">*</asp:CompareValidator>
            </td>
            <td colspan="2">
                <asp:DropDownList ID="LineTeamLeadDropDownList" runat="server"
                    Width="250px">
                    <asp:ListItem Value="-1">Select Line Team Lead</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label106" runat="server" Text="Business Opportunity Mngr:"></asp:Label>
                &nbsp;</td>
            <td colspan="2">
                <%--<asp:DropDownList ID="drpBOM" runat="server" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="drpBOM_SelectedIndexChanged">
                </asp:DropDownList>--%>                    <%--<tr>
        <td>
            &nbsp;</td>
        <td>
        <div style="color:Red; width:72%">
            The Amount JV ($mln) and IP Value (Amount SS $mln) must be equal to the value on the document to be uploaded. This is required for IP to go through the expected approval.
        </div>
        </td>
    </tr>--%>
                <%--<asp:DropDownList ID="drpBOM" runat="server" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="drpBOM_SelectedIndexChanged">
                </asp:DropDownList>--%>
                <uc1:oLocator ID="oLocator1" runat="server" />
                <asp:Image ID="imgBOM" runat="server" ImageUrl="~/Images/globalheader_help.gif" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label109" runat="server" Text="Amount JV):"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                    ControlToValidate="AmountJVTextBox"
                    ErrorMessage="Amount JV ($mln) is required">*</asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:TextBox ID="AmountJVTextBox" runat="server" CssClass="number-only"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="AmountJVTextBox" ErrorMessage="Amount JV accept only number" SetFocusOnError="True" ValidationExpression="-?(0|([1-9]\d*))(\.\d+)?"></asp:RegularExpressionValidator>
            </td>
            <td>
                ($mln)</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label11" runat="server" Text="IP Value Amount SS:"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                    ControlToValidate="IPValueTextBox"
                    ErrorMessage="Amount SS ($mln) is required">*</asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:TextBox ID="IPValueTextBox" runat="server" CssClass="number-only"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="IPValueTextBox" Display="Dynamic" ErrorMessage="IP Value Amount SS accept only number" SetFocusOnError="True" ValidationExpression="-?(0|([1-9]\d*))(\.\d+)?"></asp:RegularExpressionValidator>
            </td>
            <td>
                ($mln)</td>
        </tr>
        <%--<tr>
        <td>
            &nbsp;</td>
        <td>
        <div style="color:Red; width:72%">
            The Amount JV ($mln) and IP Value (Amount SS $mln) must be equal to the value on the document to be uploaded. This is required for IP to go through the expected approval.
        </div>
        </td>
    </tr>--%>
        <tr>
            <td>
                <asp:Label ID="Label107" runat="server" Text="Upload Proposal:"></asp:Label>
            </td>
            <td colspan="2">
                <asp:FileUpload ID="UploadProposal" runat="server" Width="215px" Height="23px" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td colspan="2">
                <asp:Button ID="SaveBtn" runat="server" OnClick="SaveBtn_Click" Text="Save"
                    Width="100px" />
                &nbsp;&nbsp;
      <asp:Button ID="closeButton" runat="server" OnClick="closeButton_Click"
          Text="Close" ValidationGroup="xxxx" />

            </td>
        </tr>
    </table>

    <asp:ValidationSummary ID="ValidationSummary1" runat="server"
        ShowMessageBox="True" ShowSummary="False"
        ValidationGroup="AmountJVSS" />
    <asp:ValidationSummary ID="ValidationSummary3" runat="server"
        ShowMessageBox="True" ShowSummary="False" ValidationGroup="OUValidation" />
    <asp:ValidationSummary ID="ValidationSummary2" runat="server"
        ShowMessageBox="True" ShowSummary="False" />
</asp:Content>