<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReadMail.ascx.cs" Inherits="Module_Mail_Controls_ReadMail" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc1" %>
<%@ Register Src="Compose.ascx" TagName="Compose" TagPrefix="uc1" %>
<table width="100%" align="left" class="tContentArial">
    <tr>
        <td>
            <table width="100%" style="background-color: #BFDCFF">
                <tr>
                    <td class="td tdLeft" width="100%">
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lbtnInbox" runat="server" OnClick="lbtnInbox_Click"></asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lbtnDelete" runat="server" OnClick="lbtnDelete_Click">Delete</asp:LinkButton>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="td tdLeft" width="100%">
                        <asp:Label ID="lblSubject" runat="server"></asp:Label>
                        <asp:HiddenField ID="hfReceiveMailId" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="td tdLeft" width="100%">
                        <table width="100%">
                            <tr>
                                <td width="40%" class="tdLeft">
                                    <asp:Label ID="lblFrom" runat="server"></asp:Label>
                                    To
                                    <asp:Label ID="lblEmail_Add" runat="server"></asp:Label>
                                </td>
                                <td width="30%" class="tdRight">
                                    <asp:LinkButton ID="lbtnShowDetail" runat="server" OnClick="lbtnShowDetail_Click">Show Details</asp:LinkButton>
                                    <asp:Label ID="lblDateTime" runat="server"></asp:Label>
                                </td>
                                <td width="30%" class="tdRight">
                                    <cc1:OboutDropDownList ID="ddlOtherOpt" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOtherOpt_SelectedIndexChanged">
                                        <asp:ListItem>Select...</asp:ListItem>
                                        <asp:ListItem Value="Reply"></asp:ListItem>
                                        <asp:ListItem>Forward</asp:ListItem>
                                        <asp:ListItem>Delete</asp:ListItem>
                                    </cc1:OboutDropDownList>
                                </td>
                            </tr>
                            <tr id="trDetail" runat="server">
                                <td colspan="3" width="100%" class="tdLeft">
                                    <asp:Label ID="lblTo" runat="server"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblCc" runat="server"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblBcc" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="td tdLeft" width="100%">
                        <div id="dvBody" runat="server">
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="trReplyForward" runat="server">
        <td class="td" width="100%">
            <asp:LinkButton ID="lbtnDiscardReply" runat="server" OnClick="lbtnDiscardReply_Click">Discard</asp:LinkButton>
            <uc1:Compose ID="Compose1" runat="server" />
        </td>
    </tr>
</table>
