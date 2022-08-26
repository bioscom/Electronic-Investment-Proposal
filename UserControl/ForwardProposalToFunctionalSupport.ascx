<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ForwardProposalToFunctionalSupport.ascx.cs" Inherits="UserControl_ForwardProposalToFunctionalSupport" %>

<asp:Label ID="Label114" runat="server" Text="Forward Proposal for Functional Support" Font-Bold="True"></asp:Label>
<hr />
 <asp:Label ID="mssgLabel1" runat="server" CssClass="Warning" BorderStyle="Solid" BorderColor="Gold" BorderWidth="1px"></asp:Label>
<br />
<table class="tMainBorder" style="width:100%">
 <tr>
     <td colspan="4" class="cHeadTile">
        Mandatory Functional Support
     </td>
 </tr>
 <tr>
     <%--<td style="width:150px">--%>
     <td>
         <asp:Label ID="Label93" runat="server" Text="Controller"></asp:Label>
     </td>
     <%--<td style="width:270px">--%>
     <td>
         <asp:DropDownList ID="controllersDropDownList" runat="server" Width="260px">
         </asp:DropDownList>
<asp:HiddenField ID="bfmIDHF" runat="server" />
     </td>
     <%--<td style="width:270px">--%>
     <td>
         <asp:Label ID="ControllerSupportLabel" runat="server" Font-Bold="True"></asp:Label>
     </td>
     <td style="background-color:ButtonFace">
         <asp:CheckBox ID="controllerCheckBox" runat="server" Text="Should Review IP after recent update" />
     </td>
 </tr>
 <tr>
     <td>
         <asp:Label ID="Label94" runat="server" Text="Legal"></asp:Label>
     </td>
     <td>
         <asp:DropDownList ID="legalDropDownList" runat="server" Width="260px">
         </asp:DropDownList>
<asp:HiddenField ID="legalIDHF" runat="server" />
     </td>
     <td>
         <asp:Label ID="legalSupportLabel" runat="server" Font-Bold="True"></asp:Label>
     </td>
     <td style="background-color:ButtonFace">
         <asp:CheckBox ID="legalCheckBox" runat="server" Text="Should Review IP after recent update" />
     </td>
 </tr>
 <tr>
    <td>
        <asp:Label ID="Label95" runat="server" Text="Tax"></asp:Label>
    </td>
    <td>
        <asp:DropDownList ID="taxDropDownList" runat="server" Width="260px">
        </asp:DropDownList>
<asp:HiddenField ID="taxIDHF" runat="server" />
    </td>
    <td>
        <asp:Label ID="taxSupportLabel" runat="server" Font-Bold="True"></asp:Label>
    </td>
    <td style = "background-color:ButtonFace">
        <asp:CheckBox ID="taxCheckBox" runat="server" Text="Should Review IP after recent update" />
    </td>
</tr>
 <tr>
     <td>
         <asp:Label ID="Label104" runat="server" Text="Treasury (BSM)"></asp:Label>
     </td>
     <td>
         <asp:DropDownList ID="tressuryDropDownList" runat="server" Width="260px">
         </asp:DropDownList>
<asp:HiddenField ID="treasuryIDHF" runat="server" />  
     </td>
     <td>
         <asp:Label ID="treasurySupportLabel" runat="server" Font-Bold="True"></asp:Label>
     </td>
     <td style="background-color:ButtonFace">
         <asp:CheckBox ID="treasuryCheckBox" runat="server" Text="Should Review IP after recent update" />
     </td>
 </tr>
 <tr>
     <td>
         <asp:Label ID="Label92" runat="server" Text="Economics"></asp:Label>
     </td>
     <td>
         <asp:DropDownList ID="ecoDropDownList" runat="server" Width="260px">
         </asp:DropDownList>
<asp:HiddenField ID="ecoIDHF" runat="server" />
     </td>
     <td>
             <asp:Label ID="ecoSupportLabel" runat="server" Font-Bold="True"></asp:Label>
         </td>
     <td style="background-color:ButtonFace">
         <asp:CheckBox ID="econsCheckBox" runat="server" Text="Should Review IP after recent update" />
     </td>
 </tr>
 <tr>
     <td colspan="4" class="cHeadTile">
         Non Mandatory Functional Support
     </td>
 </tr>
 <tr>
     <td>
         <asp:Label ID="Label90" runat="server" Text="HSE"></asp:Label>
     </td>
     <td>
         <asp:DropDownList ID="hseDropDownList" runat="server" Width="260px">
         </asp:DropDownList>
<asp:HiddenField ID="hseIDHF" runat="server" />
     </td>
     <td>
         <div style="float:left">
             <asp:Label ID="hseSupportLabel" runat="server" Font-Bold="True"></asp:Label>
         </div>
     </td>
     <td style = "background-color:ButtonFace">
         <asp:CheckBox ID="hseCheckBox" runat="server" Text="Should Review IP after recent update" />
     </td>
 </tr>
 <tr>
     <td>
         <asp:Label ID="Label102" runat="server" Text="Security"></asp:Label>
     </td>
     <td>
         <asp:DropDownList ID="secDropDownList" runat="server" Width="260px">
         </asp:DropDownList>
<asp:HiddenField ID="secIDHF" runat="server" />
     </td>
     <td>
         <div style="float:left">
             <asp:Label ID="secSupportLabel" runat="server" Font-Bold="True"></asp:Label>
         </div>
     </td>
     <td style = "background-color:ButtonFace">
         <asp:CheckBox ID="securityCheckBox" runat="server" Text="Should Review IP after recent update" />
     </td>
 </tr>
 <tr>
     <td>
         <asp:Label ID="Label91" runat="server" Text="SPCA"></asp:Label>
     </td>
     <td>
         <asp:DropDownList ID="spcaDropDownList" runat="server" Width="260px">
         </asp:DropDownList>
<asp:HiddenField ID="spcaIDHF" runat="server" />
     </td>
     <td>
         <div style="float:left">
             <asp:Label ID="spcaSupportLabel" runat="server" Font-Bold="True"></asp:Label>
         </div>
     </td>
     <td style = "background-color:ButtonFace">
         <asp:CheckBox ID="spcaCheckBox" runat="server" Text="Should Review IP after recent update" />
     </td>
 </tr>
 <tr>
     <td>
         <asp:Label ID="Label112" runat="server" Text="IT"></asp:Label>
     </td>
     <td>
         <asp:DropDownList ID="ITDropDownList" runat="server" Width="260px">
         </asp:DropDownList>
<asp:HiddenField ID="ITIDHF" runat="server" />  
     </td>
     <td>
         <div style="float:left">
         </div>
         <asp:Label ID="ITSupportLabel" runat="server" Font-Bold="True"></asp:Label>
     </td>
     <td style = "background-color:ButtonFace">
         <asp:CheckBox ID="ITCheckBox" runat="server" Text="Should Review IP after recent update" />
     </td>
 </tr>
 <tr>
     <td>
         <asp:Label ID="Label113" runat="server" Text="SCM"></asp:Label>
     </td>
     <td>
         <asp:DropDownList ID="scmDropDownList" runat="server" Width="260px">
         </asp:DropDownList>
<asp:HiddenField ID="scmIDHF" runat="server" />  
     </td>
     <td>
         <asp:Label ID="scmSupportLabel" runat="server" Font-Bold="True"></asp:Label>
     </td>
     <td style="background-color:ButtonFace">
         <asp:CheckBox ID="scmCheckBox" runat="server" Text="Should Review IP after recent update" /> 
             
     </td>
 </tr>
 <tr>
     <td colspan="4">
         &nbsp;</td>
 </tr>
 <tr>
     <td>
         &nbsp;
     </td>
     <td colspan="2">
         <asp:Button ID="forwardButton" runat="server" onclick="forwardButton_Click" 
             Text="Forward IP to Support Functions" Width="260px" />
     </td>
     <td>
         <asp:Button ID="reviewAgainButton" runat="server" 
             onclick="reviewAgainButton_Click" Text="Forward for Review" Width="150px" />
     </td>
 </tr>
 
 </table>
<asp:HiddenField ID="proposalIDHiddenField" runat="server" />                     
                     
                                          
                     