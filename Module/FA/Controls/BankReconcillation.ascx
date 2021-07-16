<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BankReconcillation.ascx.cs"
    Inherits="Module_FA_Controls_WebUserControl" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <table>
            <tr>
                <td class="td ">
                    <table>
                        <tr>
                            <td id="tdUpdate" runat="server">
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" Height="41" ImageUrl="~/CommonImages/edit1.jpg"
                                    ToolTip="Update" ValidationGroup="M1" Width="48" OnClick="imgbtnUpdate_Click" />
                            </td>
                            <td id="tdClear" runat="server">
                                <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                                    ToolTip="Clear" Width="48" OnClick="imgbtnClear_Click" />
                            </td>
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
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top" align="center" colspan="5">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ShowSummary="false" ValidationGroup="M1" />
                    <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label><strong>
                    </strong>
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label><strong>
                    </strong>
                </td>
            </tr>
            <tr class="TableHeader">
                <td align="center" valign="top" class="td">
                    <span class="titleheading">Bank Reconciliation </span>
                </td>
            </tr>
            <tr Width="100%">
                <td class="td" Width="100%">
                    <asp:GridView ID="gvBankReconc" runat="server" CssClass="SmallFont" AutoGenerateColumns="False"
                        AllowSorting="True" AllowPaging="True" OnPageIndexChanging="gvBankReconc_PageIndexChanging" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Comp_code" Visible="false">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="LblCompcode" runat="server" Text='<%# Bind("COMP_CODE") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BRANCH_CODE" Visible="false">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="LblBranchCode" runat="server" Text='<%# Bind("BRANCH_CODE") %>' CssClass="Label SmallFont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="YEAR" Visible="false">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="LblYear" runat="server" Text='<%# Bind("YEAR") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="LblDate" runat="server" Text='<%# Bind("CHEQUE_DATE") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Voucher No">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="LblVoucherNo" runat="server" Text='<%# Bind("VCHR_NO") %>' CssClass="Label SmallFont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Party Code">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="LblLedgerCode" runat="server" Text='<%# Bind("PARTY_LGR_CODE") %>'
                                        CssClass="Label SmallFont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Party Name">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="LblLedgerName" runat="server" Text='<%# Bind("PARTY_LGR_NAME") %>'
                                        CssClass="Label SmallFont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Check No">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="LblCheckNo" runat="server" Text='<%# Bind("CHEQUE_NO") %>' CssClass="Label SmallFont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bank Name">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="LblBankName" runat="server" Text='<%# Bind("BANK_NAME") %>' CssClass="Label SmallFont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bank Ledger Name" Visible="false">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="LblBankName" runat="server" Text='<%# Bind("BANK_LGR_CODE") %>' CssClass="Label SmallFont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Issued Amount" HeaderStyle-HorizontalAlign="Right"
                                ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                <ItemStyle VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="LblIssued" runat="server" Text='<%# Bind("AMT_ISSUED") %>' CssClass="Label SmallFont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Recieved Amount" HeaderStyle-HorizontalAlign="Right"
                                ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                <ItemStyle VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="LblRecieved" runat="server" Text='<%# Bind("AMT_RECIEVED") %>' CssClass="Label SmallFont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cleared">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkCleared" runat="server" Text="" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle CssClass="SmallFont" Width="98%" />
                        <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
