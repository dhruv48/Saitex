<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GetBankBook.ascx.cs" Inherits="Module_FA_Controls_GetBankBook" %>
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
        margin-left: 2px;
    }
    .c1
    {
        width: 80px;
    }
    .c2
    {
        margin-left: 4px;
        width: 100px;
    }
    .c3
    {
        margin-left: 4px;
        width: 200px;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <table class="tContentArial">
            <tr>
                <td class="td">
                    <table>
                        <td id="tdPrint" runat="server">
                            <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                                ToolTip="Print" Width="48" />
                        </td>
                        <td id="tdHelp" runat="server">
                            <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                                ToolTip="Help" Width="48" />
                        </td>
                        <td id="tdExit" runat="server">
                            <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                                OnClick="imgbtnExit_Click" ToolTip="Exit" Width="48" />
                        </td>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" valign="top" class="tRowColorAdmin td">
                    <span class="titleheading">Bank Book</span>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <table style="width: 532px">
                        <tr>
                            <td class="tdRight">
                                <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Date From :"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtStartingDate" runat="server" CssClass="TextBox" Width="100px"
                                    TabIndex="1"></asp:TextBox>
                            </td>
                            <td class="tdRight">
                                <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Date To :"></asp:Label>
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
            <tr>
                <td align="center" valign="top" colspan="4" class="td">
                    <cc2:ComboBox ID="ddlLedgerCode" EmptyText="select Bank" runat="server" Width="170px"
                        Height="250px" DataTextField="LDGR_NAME" DataValueField="LDGR_CODE" EnableLoadOnDemand="True"
                        MenuWidth="300px" OnLoadingItems="ddlLedgerCode_LoadingItems" TabIndex="5">
                        <HeaderTemplate>
                            <div class="header c1">
                                Code
                            </div>
                            <div class="header c3">
                                Ledger Name</div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="item c1">
                                <%# Eval("LDGR_CODE")%></div>
                            <div class="item c3">
                                <%# Eval("LDGR_NAME")%></div>
                        </ItemTemplate>
                        <FooterTemplate>
                            Displaying items
                            <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                            out of
                            <%# Container.ItemsCount %>.
                        </FooterTemplate>
                    </cc2:ComboBox>
                    <asp:Button ID="btnGetLedger" runat="server" OnClick="btnGetLedger_Click" Text="Get Ledger Book">
                    </asp:Button>
                </td>
            </tr>
            <tr id="ShowLedger" runat="server">
                <td class="td">
                    <table>
                        <tr>
                            <td align="center" valign="top">
                                <asp:Label ID="lblAccountName" runat="server" Text="Bank Book"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="grdLgr_Book" runat="server" AutoGenerateColumns="False" OnRowCommand="grdLedgerBook_Dr_RowCommand"
                                    ShowFooter="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Voucher Date">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnVoucher_Date" runat="server" CommandArgument='<%# Bind("VOUCHER_NO") %>'
                                                    Text='<%# Bind("JOURNAL_DATE","{0:dd-MM-yyyy}") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Particulars">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnr_Ledger_Name" runat="server" CommandArgument='<%# Bind("VOUCHER_NO") %>'
                                                    Text='<%# Bind("R_LEDGER_NAME") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Voucher">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnVoucher_No" runat="server" CommandArgument='<%# Bind("VOUCHER_NO") %>'
                                                    Text='<%# Bind("VOUCHER_NAME") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Debit Amount">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnDrAmount" runat="server" CommandArgument='<%# Bind("VOUCHER_NO") %>'
                                                    Text='<%# Bind("DR_AMOUNT") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblFDR_Amount" CssClass="LabelNo" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Credit Amount">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnCrAmount" runat="server" CommandArgument='<%# Bind("VOUCHER_NO") %>'
                                                    Text='<%# Bind("CR_AMOUNT") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblFCR_Amount" CssClass="LabelNo" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Is Cleared">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkIsCleared" runat="server" CommandArgument='<%# Bind("CLEARED") %>'
                                                    Text='<%# Bind("CLEARED") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="RowStyle SmallFont" VerticalAlign="Top" Height="18px" />
                                    <HeaderStyle CssClass="HeaderStyle" BackColor="#336699" />
                                </asp:GridView>
                            </td>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                    </table>
                </td>
            </tr>
        </table>
        <cc4:CalendarExtender ID="ce1" runat="server" TargetControlID="txtStartingDate" PopupPosition="TopLeft"
            OnClientDateSelectionChanged="checkDate" Format="dd/MM/yyyy">
        </cc4:CalendarExtender>
        <cc4:CalendarExtender ID="ce2" runat="server" TargetControlID="txtEndingDate" PopupPosition="TopLeft"
            OnClientDateSelectionChanged="checkDate" Format="dd/MM/yyyy">
        </cc4:CalendarExtender>
        <asp:RangeValidator ID="rvStartDate" runat="server" ControlToValidate="txtStartingDate"
            Display="Dynamic" ErrorMessage="Hi Dear, Pls.. enter valid date of this Financial Year"
            Type="Date" ValidationGroup="M1" SetFocusOnError="True"></asp:RangeValidator>
        <asp:RangeValidator ID="rvEndDate" runat="server" ControlToValidate="txtEndingDate"
            Display="Dynamic" ErrorMessage="Hi Dear, Pls.. enter valid date of this Financial Year"
            Type="Date" ValidationGroup="M1" SetFocusOnError="True"></asp:RangeValidator>
        <cc4:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtStartingDate" PromptCharacter="_">
        </cc4:MaskedEditExtender>
        <cc4:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtEndingDate" PromptCharacter="_">
        </cc4:MaskedEditExtender>
    </ContentTemplate>
</asp:UpdatePanel>
