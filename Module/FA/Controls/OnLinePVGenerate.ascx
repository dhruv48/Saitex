<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OnLinePVGenerate.ascx.cs"
    Inherits="Module_FA_Controls_OnLinePVGenerate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <table align="left" class="tContentArial" width="100%">
            <tr>
                <td align="left" valign="top" class="td" width="100%">
                    <table align="left">
                        <tr>
                            <td>
                                <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" ToolTip="Clear"
                                    ImageUrl="~/CommonImages/clear.jpg" OnClick="imgbtnClear_Click"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnExit" runat="server" Width="48" Height="41" ToolTip="Exit"
                                    ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnHelp" runat="server" Width="48" Height="41" ToolTip="Help"
                                    ImageUrl="~/CommonImages/link_help.png" OnClick="imgbtnHelp_Click"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="TableHeader td" width="100%">
                    <b class="titleheading">On Line Bill Vouching (Purchase)</b>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" width="100%" class="td">
                    <span class="Mode">You are in
                        <asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode </span>
                </td>
            </tr>
            <tr>
                <td align="center" width="100%" class="td">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ShowSummary="false" ValidationGroup="M1" />
                    <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" class="td" width="100%">
                    <b>Total Record&nbsp;:&nbsp;<asp:Label ID="lblTotalRecord" runat="server" CssClass="Label"></asp:Label></b>
                </td>
            </tr>
            <tr>
                <td align="left" class="td" width="100%">
                    <asp:GridView ID="grdBillGenerate" CssClass="SmallFont" runat="server" AllowSorting="True"
                        AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="grdBillGenerate_PageIndexChanging"
                        OnRowCommand="grdBillGenerate_RowCommand" width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Branch Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblBranchCode" runat="server" Text='<%# Bind("BRANCH_CODE") %>' CssClass="Label smallfont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bill Entry Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblBillEntryDT" runat="server" Text='<%# Bind("BILL_ENTRY_DATE", "{0:dd-MM-yyyy}") %>'
                                        CssClass="Label smallfont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bill Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblBillType" runat="server" Text='<%# Bind("BILL_TYPE") %>' CssClass="Label smallfont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Bill Desc">
                                <ItemTemplate>
                                    <asp:Label ID="lblBillTypeDesc" runat="server" Text='<%# Bind("BILL_TYPE_DESC") %>' ToolTip='<%# Bind("BILL_TYPE") %>' CssClass="Label smallfont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bill Number">
                                <ItemTemplate>
                                    <asp:Label ID="lblBillNumb" runat="server" Text='<%# Bind("BILL_NUMB") %>' CssClass="Label smallfont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Party Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblPartyCode" runat="server" Text='<%# Bind("PRTY_CODE") %>' CssClass="Label smallfont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Party Ledger Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblPartyLedgerCode" runat="server" Text='<%# Bind("LEDGER_PARTY_CODE") %>'
                                        CssClass="Label smallfont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Party Ledger Name" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblPartyLedgerName" runat="server" Text='<%# Bind("LEDGER_PARTY_NAME") %>'
                                        CssClass="Label smallfont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bill Year" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblBillYear" runat="server" Text='<%# Bind("BILL_YEAR") %>' CssClass="Label smallfont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Party Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblPartyName" runat="server" Text='<%# Bind("PRTY_NAME") %>' CssClass="Label smallfont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bill Date" ControlStyle-Width="80px">
                                <ItemTemplate>
                                    <asp:Label ID="lblBillDate" runat="server" Text='<%# Bind("BILL_DATE", "{0:dd-MM-yyyy}") %>'
                                        CssClass="Label smallfont"></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="80px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bill Amount" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblBillAmt" runat="server" Text='<%# Bind("BILL_AMNT") %>' CssClass="Label smallfont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bill Clearance Date" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblBillClearDT" runat="server" Text='<%# Bind("BILL_CLR_PUR_DATE", "{0:dd-MM-yyyy}") %>'
                                        CssClass="Label smallfont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Generate PV" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Button ID="btnGenerate" runat="server" Text="Vouching" CommandName="VOUCHING" CssClass="SmallFont" Width="60px" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
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
