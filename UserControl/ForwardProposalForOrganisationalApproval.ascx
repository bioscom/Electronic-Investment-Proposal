<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ForwardProposalForOrganisationalApproval.ascx.cs" Inherits="UserControl_ForwardProposalForOrganisationalApproval" %>

<%--<table>
     <tr>
         <td style="width:200px">
                 <asp:Label ID="mdOULabel" runat="server" CssClass="Warning" 
                     Text="Is MD OU the final approver?" Font-Underline="True"></asp:Label> 
         </td>
         <td>
                 <asp:RadioButton ID="YesRadioButton" runat="server" AutoPostBack="True" 
                     CssClass="Warning" GroupName="mdou" 
                     oncheckedchanged="YesRadioButton_CheckedChanged" Text="Yes" />
         &nbsp;&nbsp;&nbsp;
                 <asp:RadioButton ID="NoRadioButton" runat="server" AutoPostBack="True" 
                     CssClass="Warning" GroupName="mdou" 
                     oncheckedchanged="NoRadioButton_CheckedChanged" Text="No" />
         </td>
         <td>
                 &nbsp;</td>
     </tr>
     <tr>
         <td>
                 &nbsp;</td>
         <td>
             <asp:DropDownList ID="MDOUDropDownList" runat="server" Width="266px">
                 <asp:ListItem Value="-1">&lt;&lt;Select MD OU&gt;&gt;</asp:ListItem>
             </asp:DropDownList>
         </td>
         <td>
             <asp:Label ID="MDOUSignatureLabel" runat="server" Font-Bold="True"></asp:Label>
         </td>
     </tr>
     <tr>
         <td>
             &nbsp;</td>
         <td colspan="2">
             <asp:Label ID="instructionLabel" runat="server" CssClass="Warning" 
		Text="Select Yes if MD is the final approver, the Select MD OU is locked. Then go to Forward proposal for Organisational approval to forward the IP to the MD. If NO, select MD OU and send to MD for support. When MD 		supports, then go to Forward proposal for Organisational approval, select a final approver from Organisational approval." 
		Width="400px"></asp:Label>
	</td>
     </tr>
     <tr>
         <td>
             &nbsp;</td>
         <td colspan="2">
             <asp:Button ID="MDOUButton" runat="server" Text="Forward" onclick="MDOUButton_Click" />
         </td>
     </tr>
     <tr>
         <td colspan="3">
            <div style="width:500px">
                 <asp:Label ID="mssgLabel" runat="server" CssClass="Warning" BorderStyle="Solid" 
                     BorderColor="Gold" BorderWidth="1px"></asp:Label>
            </div>
         </td>
     </tr>
 </table>--%>

<asp:Label ID="Label109" runat="server" Text="Forward Proposal for Organisational Approval" Font-Bold="true"></asp:Label>
<hr />
<br />
<table>
    <tr>
        <td style="width: 200px">&nbsp;      
        </td>
        <td>
            <asp:DropDownList ID="approvalDropDownList" runat="server" Width="290px">
                <asp:ListItem Value="-1">Select a General Manager for final approval</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="approvalDropDownList" ErrorMessage="Please select General Manager  for final approval" Operator="NotEqual" Type="Integer" ValidationGroup="GM" ValueToCompare="-1">*</asp:CompareValidator>
            <asp:Label ID="approvalSupportLabel" runat="server" Font-Bold="True"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td colspan="2">&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td colspan="2">
            <asp:Button ID="approvalSupportButton" runat="server" OnClick="approvalSupportButton_Click" Text="Forward" ValidationGroup="GM" />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="GM" />
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:Label ID="mssgLabel" runat="server" CssClass="Warning"></asp:Label>
        </td>
    </tr>
</table>


<asp:HiddenField ID="proposalIDDHiddenField" runat="server" />





