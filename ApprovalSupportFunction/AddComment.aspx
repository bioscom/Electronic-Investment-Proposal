<%@ Page Language="C#" MasterPageFile="~/MasterPages/eIPMasterPage.master" AutoEventWireup="True" CodeFile="AddComment.aspx.cs" Inherits="ApprovalSupportFunction_AddComment" Title="Investment Proposal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />
    <eip:IPDetailInfo ID="IPDetailInfo1" runat="server" />
    <table class="tMainBorder" style="width: 600px">
        <tr>
            <td colspan="2" class="cHeadTile">
                <asp:Label ID="SupportLabel" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Stand" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlSupportStand" runat="server" Width="200px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Comments" Font-Bold="True"></asp:Label></td>
            <td>
                <asp:TextBox ID="CommentTextBox" runat="server" Text="" Height="100px" TextMode="MultiLine"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:Button ID="digiSignButton" runat="server" Text="Click here to append Digital Signature to this IP"
                    Width="400px" Font-Bold="True" ForeColor="#000066" Height="25px" />

                <ajaxToolkit:ModalPopupExtender ID="MPE" runat="server"
                    TargetControlID="digiSignButton"
                    PopupControlID="Panel1"
                    BackgroundCssClass="modalBackground"
                    DropShadow="true"
                    CancelControlID="CancelButton"
                    PopupDragHandleControlID="Panel3" />

                <br />
                <br />
                <asp:Label ID="warningLabel" runat="server" CssClass="Warning"
                    Text="Warning: if you do not digital sign now, you will no longer be able to digital sign but can manually sign the document at the end of the workflow."
                    Width="400px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:Button ID="forwardButton" runat="server"
                    OnClick="forwardButton_Click" Text="Forward" />
                &nbsp;&nbsp;
                    <asp:Button ID="closeButton" runat="server" Text="Close"
                        OnClick="closeButton_Click" />

            </td>
        </tr>
    </table>

    <table class="tMainBorder" style="width: 600px">
        <tr>
            <td colspan="2" class="cHeadTile">Forward the comment/IP to the central support staff</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;<asp:CheckBox ID="ckbForward" runat="server" AutoPostBack="True" OnCheckedChanged="ckbForward_CheckedChanged" />
            &nbsp;(Click to enter Email address)</td>
        </tr>
        <tr>
            <td>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail" ErrorMessage="Invalid email address" ValidationGroup="SendEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
            </td>
            <td>
                <asp:Panel ID="pnlForwardComment" runat="server">
                    <asp:TextBox ID="txtEmail" runat="server" ToolTip="Enter multiple email addresses separated by semi column ;" TextMode="MultiLine" Height="50px" Width="400px" ValidationGroup="SendEmail"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Button ID="btnSend" runat="server" OnClick="btnSend_Click" Text="Send" ValidationGroup="SendEmail" Width="100px" />
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="SendEmail" />
            </td>
        </tr>
        <tr>
            <td colspan="2" class="cHeadTile">&nbsp;</td>
        </tr>
    </table>
    <%--<table>
        <tr>
            <td style="width:400px">
                
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                
            </td>
        </tr>
    </table>--%>



    <%--The Modal Popup codes begins here--%>
    <asp:Panel ID="Panel1" runat="server" Style="display: none" CssClass="modalPopup" Width="427px">
        <asp:Panel ID="Panel3" runat="server" Style="cursor: move; background-color: #DDDDDD; border: solid 1px Gray; color: Black">
            <div>
                <asp:ImageButton ID="ImageButton1" runat="server" Height="21px" ImageUrl="~/Images/acrobatLogo.jpg" Width="21px" />
                <b>Adobe Acrobat Digital Signature</b>
            </div>
        </asp:Panel>
        <br />
        <div>
            <div style="text-align: center; background-color: #abc">
                <asp:ImageButton ID="digiSignImgBtn" runat="server"
                    ImageUrl="~/Images/digitalSign.gif" OnClick="digiSignImgBtn_Click"
                    ToolTip="Click here to append Digital Signature to this IP."
                    Width="82px" ValidationGroup="xyz" />
            </div>
            <br />
            <center>
                <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank"
                    NavigateUrl="http://sww-knowledge-epg.shell.com/knowtepg1/llisapi.dll?func=ll&amp;objId=12093961&amp;objAction=browse&amp;sort=name">Click here to view instructions on how 
                to append digital signature
                </asp:HyperLink>
                <br />
                <br />
                <span class="style2"><b>Click browse to select the document you have digital 
                signed and then click Upload button. </b></span>
                <br />
                <br />
                <asp:FileUpload ID="UploadProposal" runat="server" Height="23px"
                    Width="215px" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                
                <asp:Button ID="uploadButton" runat="server" Text="Upload"
                    OnClick="uploadButton_Click" ValidationGroup="xyz" />
                <br />
                <br />
            </center>
            <p style="text-align: center;">
                <asp:Button ID="CancelButton" runat="server" Text="Cancel" />
            </p>
        </div>
    </asp:Panel>

    <%--The Modal Popup codes ends here--%>
</asp:Content>

