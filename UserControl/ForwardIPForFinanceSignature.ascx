<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ForwardIPForFinanceSignature.ascx.cs" Inherits="UserControl_ForwardIPForFinanceSignature" %>
<p>
 <asp:Label ID="Label1" runat="server" Text="Forward Proposal to Finance Signature/OU Finance Director" Font-Bold="True"></asp:Label>
 </p>
 <hr />
 <table>
 <tr>
     <td>
         <asp:Label ID="Label107" runat="server" 
             Text="Finance Signature:"></asp:Label>
     </td>
     <td>
         <asp:DropDownList ID="finDropDownList" runat="server" Width="260px">
         </asp:DropDownList>
     </td>
     <td>
         <asp:Label ID="finSupportLabel" runat="server" Font-Bold="True"></asp:Label>
     </td>
 </tr>
 <tr>
     <td>
         &nbsp;</td>
     <td>
         &nbsp;</td>
     <td>
         &nbsp;</td>
 </tr>
 <tr>
     <td>
         &nbsp;</td>
     <td>
         <asp:Button ID="financeSignatureButton" runat="server" 
             onclick="financeSignatureButton_Click" Text="Forward IP for Finance Signature" 
             Width="260px" />
     </td>
     <td>
         <asp:CheckBox ID="ckbOverride" runat="server" AutoPostBack="True" OnCheckedChanged="ckbOverride_CheckedChanged" Text="Override Functional Support" />&nbsp;&nbsp;
         <asp:Image ID="img4" runat="server" ImageUrl="~/Images/globalheader_help.gif" ToolTip="Check this box to override sending IP to Functional support" />
     </td>
 </tr>
 <tr>
     <td colspan="3">
                 <asp:Label ID="mssgLabel" runat="server" CssClass="Warning" BorderStyle="Solid" 
                     BorderColor="Gold" BorderWidth="1px"></asp:Label>
            </td>
 </tr>
 </table>

<asp:HiddenField ID="proposalIDHiddenField" runat="server" />                     
                     
                                          
                     

<asp:HiddenField ID="finSigIDHF" runat="server" />  
                     
                     
                                          
                     

