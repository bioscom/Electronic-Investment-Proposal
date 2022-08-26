<%@ Page Language="C#" MasterPageFile="~/MasterPages/FrontPage.master" AutoEventWireup="True" CodeFile="SessionExpires.aspx.cs" Inherits="SessionExpires" Title="Investment Proposal"%>

<asp:Content id="Content1" ContentPlaceHolderid="ContentPlaceHolder1" Runat="Server">
    <div style="color:Black">
    <table style="width:100%" class="tMainBorder">
    <tr>
        <td class="cHeadTile">
        </td>
    </tr>
    
    <tr>
    <td>
        <center>
            <div style="font-size:150%; color:Red">
                Your session for this application has expired.
                <br /> <br />
                Please, always save your work at every point in this application.<br />
                Any unsaved work may be lost. 
            </div>
            <br />
            <div>
                Click 
                <asp:LinkButton ID="hereLinkButton" runat="server" 
                    PostBackUrl="~/Index.aspx">here</asp:LinkButton>
                    &nbsp;to continue.
            </div>
        </center>
    </td>
    </tr>
    
    <tr>
    <td>
        &nbsp;</td>
    </tr>
    
    <tr>
    <td>
        <center>
            <div>
                In case of any problem, please contact, <a href="mailto:isaac.bejide@shell.com">
                isaac.bejide@shell.com</a>&nbsp; ext.: 24772. Thank you.
            </div>
        </center>
    </td>
    </tr>
    
    </table>
    </div>
</asp:Content>