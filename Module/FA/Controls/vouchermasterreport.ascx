<%@ Control Language="C#" AutoEventWireup="true" CodeFile="vouchermasterreport.ascx.cs"
    Inherits="Module_FA_Controls_vouchermasterreport" %>
<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>--%>
<table width="400px">
    <tr>
        <td class="td" colspan="4">
            <table cellpadding="0" cellspacing="0" border="1" align="left" class="tContentArial">
                <tr>
                    <td id="tdClear" runat="server">
                        <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                            OnClick="imgbtnClear_Click" ToolTip="Clear" Width="48" />
                    </td>
                    <td id="tdHelp" runat="server">
                        <asp:ImageButton ID="imgbtnHelp" Width="48" Height="41" runat="server" ToolTip="Help"
                            ImageUrl="~/CommonImages/link_help.png" OnClick="imgbtnHelp_Click"></asp:ImageButton>
                    </td>
                    <td id="tdExit" runat="server">
                        <asp:ImageButton ID="imgbtnExit" Width="48" Height="41" runat="server" ToolTip="Exit"
                            ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="TableHeader td" align="center" colspan="4">
            <b class="titleheading">Voucher Master Report</b>
        </td>
    </tr>
    <tr>
        <td class="tdLeft td" align="center" colspan="4">
            <span class="Mode">
                <asp:Label ID="lblMode" runat="server"></asp:Label>
            </span>
        </td>
    </tr>
    <tr>
        <td valign="top" align="center" colspan="4">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                ShowSummary="false" ValidationGroup="M1" />
            <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label><strong>
            </strong>
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label><strong>
            </strong>
        </td>
    </tr>
    <tr>
        <td colspan="2" align="right">
            Select Voucher :
        </td>
        <td colspan="2" align="left">
            <asp:DropDownList ID="ddlVoucherType" runat="server" AppendDataBoundItems="true"
                AutoPostBack="true" CssClass="SmallFont" DataTextField="VCHR_NAME" DataValueField="VCHR_CODE"
                TabIndex="1" Width="150px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td colspan="4" align="center">
            <asp:Button ID="btnprint" runat="server" Text="Print" OnClick="btnprint_Click" Width="100px" />
        </td>
    </tr>
</table>
<%-- </ContentTemplate>
</asp:UpdatePanel>--%>
