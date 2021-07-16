<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TrialBalanceWithFilters.ascx.cs"
    Inherits="Module_FA_Controls_TrialBalanceWithFilters" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;}
    .header
    {
        margin-left: 4px;
    }
    .c1
    {
        width: 150px;
    }
    .c2
    {
        margin-left: 4px;
        width: 150px;
    }
    .c3
    {
        margin-left: 4px;
        width: 150px;
    }
    .c4
    {
        width: 70px;
    }
    .c5
    {
        margin-left: 4px;
        width: 300px;
    }
    .c6
    {
        margin-left: 4px;
        width: 200px;
    }
</style>
<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>--%>
<table class="tContentArial">
    <tr>
        <td class="td">
            <table>
                <tr>
                    <td id="tdPrint" runat="server">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                            OnClick="imgbtnPrint_Click" ToolTip="Print" Width="48" />
                    </td>
                    <td id="tdClear" runat="server">
                        <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                            OnClick="imgbtnClear_Click" ToolTip="Clear" Width="48" />
                    </td>
                    <td id="tdHelp" runat="server">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                            OnClick="imgbtnHelp_Click" ToolTip="Help" Width="48" />
                    </td>
                    <td id="tdExit" runat="server">
                        <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                            OnClick="imgbtnExit_Click" ToolTip="Exit" Width="48" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr class="TableHeader">
        <td align="center" valign="top" class="td">
            <span class="titleheading">Trial Balance Report</span>
        </td>
    </tr>
    <tr>
        <td class="tdLeft td">
            <span class="Mode">
                <asp:Label ID="lblMode" runat="server"></asp:Label>
            </span>
        </td>
    </tr>
    <tr>
        <td valign="top" align="center">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                ShowSummary="false" ValidationGroup="M1" />
            <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label><strong>
            </strong>
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label><strong>
            </strong>
        </td>
    </tr>
    <tr>
        <td class="td">
            <table>
                <tr>
                    <td align="center" colspan="4">
                        <asp:RadioButtonList ID="rdoLstDate" runat="server" RepeatDirection="Horizontal"
                            TabIndex="1">
                            <asp:ListItem Selected="True" Text="Voucher Date Wise" Value="Voucher Date Wise"></asp:ListItem>
                            <asp:ListItem Text="Bill Date Wise" Value="Bill Date Wise"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight">
                        From Date :
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="txtFromDT" runat="server" CssClass="TextBox" Width="115px" TabIndex="2"></asp:TextBox>
                    </td>
                    <td class="tdRight">
                        To Date :
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="txtToDT" runat="server" CssClass="TextBox" Width="115px" TabIndex="3"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight">
                        Start Ledger Code :
                    </td>
                    <td class="tdLeft">
                        <cc2:ComboBox ID="cmbLedgerCodeStart" runat="server" AutoPostBack="True" CssClass="smallfont"
                            DataTextField="LDGR_CODE" DataValueField="LDGR_CODE" EnableLoadOnDemand="True"
                            Width="118px" MenuWidth="660" EnableVirtualScrolling="true" OpenOnFocus="true"
                            TabIndex="4" Visible="true" Height="200px" OnLoadingItems="cmbLedgerCodeStart_LoadingItems"
                            OnSelectedIndexChanged="cmbLedgerCodeStart_SelectedIndexChanged" EmptyText="Select Ledger">
                            <HeaderTemplate>
                                <div class="header c4">
                                    Code</div>
                                <div class="header c5">
                                    Ledger Name</div>
                                <div class="header c6">
                                    Group Name</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c4">
                                    <%# Eval("LDGR_CODE")%></div>
                                <div class="item c5">
                                    <%# Eval("LDGR_NAME")%></div>
                                <div class="item c6">
                                    <%# Eval("GRP_NAME")%></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                    </td>
                    <td class="tdRight">
                        End Ledger Code :
                    </td>
                    <td class="tdLeft">
                        <cc2:ComboBox ID="cmbLedgerCodeEnd" runat="server" AutoPostBack="True" CssClass="smallfont"
                            DataTextField="LDGR_CODE" DataValueField="LDGR_CODE" EnableLoadOnDemand="True"
                            MenuWidth="660" EnableVirtualScrolling="true" OpenOnFocus="true" TabIndex="5"
                            Width="118px" Visible="true" Height="200px" EmptyText="Select Ledger" OnLoadingItems="cmbLedgerCodeEnd_LoadingItems"
                            OnSelectedIndexChanged="cmbLedgerCodeEnd_SelectedIndexChanged">
                            <HeaderTemplate>
                                <div class="header c4">
                                    Code</div>
                                <div class="header c5">
                                    Ledger Name</div>
                                <div class="header c6">
                                    Group Name</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c4">
                                    <%# Eval("LDGR_CODE")%></div>
                                <div class="item c5">
                                    <%# Eval("LDGR_NAME")%></div>
                                <div class="item c6">
                                    <%# Eval("GRP_NAME")%></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Panel ID="pnlOpening" runat="server" BorderColor="AliceBlue" BorderWidth="1px">
                            <table>
                                <tr>
                                    <td>
                                        <asp:RadioButtonList ID="rdbLstOpening" runat="server" RepeatDirection="Vertical"
                                            TabIndex="6">
                                            <asp:ListItem Selected="True" Text="With Opening" Value="With Opening"></asp:ListItem>
                                            <asp:ListItem Text="Without Opening" Value="Without Opening"></asp:ListItem>
                                            <asp:ListItem Text="Only Opening" Value="Only Opening"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <br />
                        <asp:Panel ID="pnlOrderBy" runat="server" BorderColor="AliceBlue" BorderWidth="1px">
                            <table>
                                <tr>
                                    <td>
                                        <asp:RadioButtonList ID="rdoLstOrder" runat="server" RepeatDirection="Vertical" TabIndex="8">
                                            <asp:ListItem Selected="True" Text="Code Wise" Value="Code Wise"></asp:ListItem>
                                            <asp:ListItem Text="Name Wise" Value="Name Wise"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td colspan="2">
                        <asp:Panel ID="pnlVouchers" runat="server" BorderColor="AliceBlue" BorderWidth="1px">
                            <table>
                                <tr>
                                    <td>
                                        <asp:RadioButtonList ID="rdoLstVoucherType" runat="server" RepeatDirection="Vertical"
                                            TabIndex="7">
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td id="trType" colspan="4" align="center" runat="server">
                        <asp:Panel ID="pnlType" runat="server" BorderColor="AliceBlue" BorderWidth="1px">
                            <asp:CheckBoxList ID="chkLstExport" runat="server" RepeatDirection="Horizontal" TabIndex="9">
                                <asp:ListItem Selected="True" Text="Crystal File" Value="Crystal File" Enabled="false"></asp:ListItem>
                                <asp:ListItem Text="PDF File" Value="PDF File"></asp:ListItem>
                                <asp:ListItem Text="Excel File" Value="Excel File"></asp:ListItem>
                            </asp:CheckBoxList>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<cc4:CalendarExtender ID="ce1" runat="server" TargetControlID="txtFromDT" PopupPosition="TopRight"
    OnClientDateSelectionChanged="checkDate" Format="dd/MM/yyyy">
</cc4:CalendarExtender>
<cc4:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
    MaskType="Date" TargetControlID="txtFromDT" PromptCharacter="_">
</cc4:MaskedEditExtender>
<cc4:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDT"
    PopupPosition="TopRight" OnClientDateSelectionChanged="checkDate" Format="dd/MM/yyyy">
</cc4:CalendarExtender>
<cc4:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
    MaskType="Date" TargetControlID="txtToDT" PromptCharacter="_">
</cc4:MaskedEditExtender>
<%-- </ContentTemplate>
</asp:UpdatePanel>
--%>