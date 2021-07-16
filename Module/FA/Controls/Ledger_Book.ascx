<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Ledger_Book.ascx.cs" Inherits="Module_FA_Controls_Ledger_Book" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc3" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>
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
    <ContentTemplate>
 --%>       <table>
            <tr>
                <td class="td">
                    <table>
                        <tr>
                            <td id="tdClear" runat="server">
                                <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                                    OnClick="imgbtnClear_Click" ToolTip="Clear" Width="48" />
                            </td>
                            <td id="tdPrint" runat="server">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                                    OnClick="imgbtnPrint_Click" ToolTip="Print" Width="48" />
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
                    <span class="titleheading">Ledger Book</span>
                </td>
            </tr>
            <tr>
                <td class="td" align="center">
                    <table>
                        <tr>
                            <td class="tdRight">
                                <asp:Label ID="lblStartMonth" runat="server" CssClass="Label" Text="Date From :"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtStartingDate" runat="server" CssClass="TextBox" Width="100px"
                                    TabIndex="1" ></asp:TextBox>
                            </td>
                            <td class="tdRight">
                                <asp:Label ID="lblEndMonth" runat="server" CssClass="Label" Text="Date To :"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEndingDate" runat="server" CssClass="TextBox" Width="100px" TabIndex="2"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="TableHeader SmallFont tdLeft" colspan="4">
                    <span class="titleheading"><i>select ledger.... </i></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr id="trComboLedger" runat="server">
                <td class="td" align="center">
                    <table>
                        <tr>
                            <td align="left">
                                <asp:CheckBox ID="chkShowHint" runat="server" ForeColor="BlueViolet" Text="View Ledger Tree"
                                    CssClass="Label" AutoPostBack="true" OnCheckedChanged="chkShowHint_CheckedChanged" />
                            </td>
                            <td align="left">
                                <cc2:ComboBox ID="ddlLedgerCode" runat="server" AutoPostBack="True" CssClass="smallfont"
                                    DataTextField="LDGR_NAME" DataValueField="LDGR_CODE" EnableLoadOnDemand="True"
                                    Width="150px" MenuWidth="660" EnableVirtualScrolling="true" OpenOnFocus="true"
                                    TabIndex="4" Visible="true" Height="200px" EmptyText="Select Ledger Code" OnLoadingItems="ddlLedgerCode_LoadingItems">
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
                            <td align="left" colspan="2">
                                <asp:Button ID="btnGetLedger" runat="server" OnClick="btnGetLedger_Click" Text="Get Ledger Book">
                                </asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="trTextLedger" runat="server">
                <td class="td" align="center">
                    <table>
                        <tr>
                            <td align="left">
                                <asp:CheckBox ID="chkHint" runat="server" ForeColor="BlueViolet" Text="Hide Ledger Tree"
                                    CssClass="Label" AutoPostBack="true" OnCheckedChanged="chkHint_CheckedChanged" />
                            </td>
                            <td>
                            </td>
                            <td align="right">
                                <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Ledger Code :"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtLedgerCode" runat="server" TabIndex="5" CssClass="TextBox TextBoxDisplay"
                                    ValidationGroup="M1" Width="37px" ReadOnly="true" AutoPostBack="True" OnTextChanged="txtLedgerCode_TextChanged"></asp:TextBox>
                                <asp:TextBox ID="txtLedgerName" runat="server" TabIndex="5" CssClass="TextBox TextBoxDisplay"
                                    ValidationGroup="M1" Width="175px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td align="left">
                                <asp:LinkButton ID="lnkbtnLedgerCode" runat="server" OnClick="lnkbtnLedgerCode_Click"
                                    ForeColor="BlueViolet" Font-Bold="true" Font-Italic="true">Click</asp:LinkButton>
                            </td>
                            <td align="left">
                                <asp:Button ID="btnGetLedgerHint" runat="server" Text="Get Ledger Book" OnClick="btnGetLedgerHint_Click">
                                </asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="ShowLedger" runat="server">
                <td class="td">
                    <table>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td align="center" valign="top">
                                            <asp:Label ID="lblAccountName" runat="server" CssClass="Label"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top">
                                            <cc3:Grid ID="grdLgr_Book" runat="server" AutoPostBackOnSelect="True" AutoGenerateColumns="False"
                                                OnSelect="grdLgr_Book_Select" ShowFooter="false" AllowAddingRecords="false" ShowColumnsFooter="true"
                                                ShowTotalNumberOfPages="false" PageSize="100" AllowColumnReordering="true" AllowSorting="false"
                                                AllowPaging="false" OnRebind="grdLgr_Book_Rebind" OnRowDataBound="grdLgr_Book_RowDataBound">
                                                <Columns>
                                                    <cc3:Column DataField="JOURNAL_DATE" DataFormatString="{0:dd/MM/yyyy}" ShowHeader="true"
                                                        HeaderText="Date" Width="90px">
                                                    </cc3:Column>
                                                    <cc3:Column DataField="VOUCHER_NO" ShowHeader="true" HeaderText="VoucherNo" FooterText="test"
                                                        FooterStyle-BackColor="White" Width="100px">
                                                    </cc3:Column>
                                                    <cc3:Column DataField="LEDGER_CODE" ShowHeader="true" HeaderText="Code" FooterText="test"
                                                        FooterStyle-BackColor="White" Width="80px">
                                                    </cc3:Column>
                                                    <cc3:Column DataField="R_LEDGER_NAME" ShowHeader="true" HeaderText="Ledger Name"
                                                        Width="170px">
                                                    </cc3:Column>
                                                    <cc3:Column DataField="DOC_NO" ShowHeader="true" HeaderText="Bill No" Width="90px">
                                                    </cc3:Column>
                                                    <cc3:Column DataField="DOC_DT" DataFormatString="{0:dd/MM/yyyy}" ShowHeader="true"
                                                        HeaderText="Bill Date" Width="100px">
                                                    </cc3:Column>
                                                    <cc3:Column DataField="DESCRIPTION" ShowHeader="true" HeaderText="Description" Width="200px">
                                                    </cc3:Column>
                                                    <cc3:Column DataField="DR_AMOUNT" ItemStyle-HorizontalAlign="Right" ShowHeader="true"
                                                        HeaderText="Dr" Width="120px">
                                                    </cc3:Column>
                                                    <cc3:Column DataField="CR_AMOUNT" ShowHeader="true" HeaderText="Cr" Width="120px">
                                                    </cc3:Column>
                                                </Columns>
                                                <ScrollingSettings ScrollHeight="350" />
                                            </cc3:Grid>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        <tr>
        <td>
        <cc4:CalendarExtender ID="ce1" runat="server" TargetControlID="txtStartingDate" PopupPosition="TopLeft"
            OnClientDateSelectionChanged="checkDate" Format="dd/MM/yyyy">
        </cc4:CalendarExtender>
        <cc4:CalendarExtender ID="ce2" runat="server" TargetControlID="txtEndingDate" PopupPosition="TopLeft"
            OnClientDateSelectionChanged="checkDate" Format="dd/MM/yyyy">
        </cc4:CalendarExtender>
        <cc4:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtStartingDate" PromptCharacter="_">
        </cc4:MaskedEditExtender>
        <cc4:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtEndingDate" PromptCharacter="_">
        </cc4:MaskedEditExtender>
        </td>
        </tr>
        </table>
  <%--  </ContentTemplate>
</asp:UpdatePanel>
--%>