<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChequeCancel.ascx.cs"
    Inherits="Module_FA_Controls_ChequeCancel" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <table align="left" class="tContentArial">
            <tr>
                <td align="left" valign="top" class="td" width="100%">
                    <table align="left">
                        <tr>
                            <td id="tdUpdate" runat="server" align="left">
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                    Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click1"></asp:ImageButton>
                            </td>
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
                    <b class="titleheading">Cheque Cancellation</b>
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
                        ValidationGroup="M1" />
                    <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <table style="width: 880px">
                        <tr>
                            <td class="tdRight">
                                <asp:Label ID="lblStart" runat="server" CssClass="Label" Text="Starting Cheque Number :"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtStart" runat="server" TabIndex="1" CssClass="TextBoxNo" MaxLength="7"
                                    Width="90px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFStartChequeNo" runat="server" ValidationGroup="M1"
                                    Display="Dynamic" ErrorMessage="Pls.. Enter Starting Cheque Number.." ControlToValidate="txtStart"
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RVStartChequeNo" ControlToValidate="txtStart" runat="server"
                                    ErrorMessage="Only numeric value allowed" Display="Dynamic" MaximumValue="9999999"
                                    MinimumValue="0" Type="Integer" ValidationGroup="M1"></asp:RangeValidator>
                            </td>
                            <td class="tdRight">
                                <asp:Label ID="lblEnd" runat="server" CssClass="Label" Text="Ending Cheque Number :"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEnd" runat="server" TabIndex="2" Width="90px" CssClass="TextBoxNo"
                                    MaxLength="7"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFEndChequeNo" runat="server" ValidationGroup="M1"
                                    Display="Dynamic" ErrorMessage="Pls.. Enter Ending Cheque Number.." ControlToValidate="txtEnd"
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RVEndChequeNo" ControlToValidate="txtEnd" runat="server"
                                    ErrorMessage="Only numeric value allowed" Display="Dynamic" MaximumValue="9999999"
                                    MinimumValue="0" Type="Integer" ValidationGroup="M1"></asp:RangeValidator>
                            </td>
                            <td>
                                <asp:Button ID="btnOk" runat="server" Text="Search" OnClick="btnOk_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="trTotalRecord" runat="server">
                <td align="left" class="td" width="100%">
                    <b>Total Record&nbsp;:&nbsp;<asp:Label ID="lblTotalRecord" runat="server" CssClass="Label"></asp:Label></b>
                </td>
            </tr>
            <tr id="trgrid" runat="server" width="100%">
                <td align="left" class="td"  width="100%">
                    <asp:GridView ID="grdChequeCancellation" CssClass="SmallFont" runat="server" AllowSorting="True"
                        AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="grdChequeCancellation_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="Voucher No">
                                <ItemTemplate>
                                    <asp:Label ID="lblVoucherNo" runat="server" Text='<%# Bind("VCHR_NO") %>' CssClass="Label smallfont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="BANK_NAME" HeaderText="Bank Name">
                                <ItemStyle HorizontalAlign="Left" CssClass="label smallfont" VerticalAlign="Top" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="ChequeBook Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblChequeBookCode" runat="server" Text='<%# Bind("CHEQUEBOOK_CODE") %>'
                                        CssClass="Label smallfont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="CHEQUEBOOK_NO" HeaderText="ChequeBook No">
                                <ItemStyle HorizontalAlign="Left" CssClass="label smallfont" VerticalAlign="Top" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CHEQUE_NO" HeaderText="Cheque No">
                                <ItemStyle HorizontalAlign="Center" CssClass="label smallfont" VerticalAlign="Top" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Cheque Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblChequeDate" runat="server" HtmlEncode="false" Text='<%# Bind("CHEQUE_DATE", "{0:dd-MM-yyyy}") %>'
                                        CssClass="Label smallfont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="true" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Party Ledger Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblPartyLGRCode" runat="server" Text='<%# Bind("PARTY_LGR_CODE") %>'
                                        CssClass="Label smallfont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="PARTY_LGR_NAME" HeaderText="Party Name">
                                <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Amount"  HeaderStyle-HorizontalAlign ="Right" ItemStyle-HorizontalAlign = "Right"  FooterStyle-HorizontalAlign = "Right" FooterStyle-Font-Bold = "true" >
                                <ItemTemplate>
                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("AMOUNT") %>' CssClass="Label smallfont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle  VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cancellation" HeaderStyle-HorizontalAlign ="Center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkCancel" runat="server"  />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cancellation Date" HeaderStyle-HorizontalAlign ="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCancellationDate" runat="server" ReadOnly="true" Text='<%# Bind("CANCELLED_DATE", "{0:dd-MM-yyyy}") %>'
                                        CssClass=" TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top"  />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cancelled By" HeaderStyle-HorizontalAlign ="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtConfirmBy" runat="server" ReadOnly="true" Text='<%# Bind("CANCELLED_BY") %>'
                                        ToolTip='<%# Bind("CANCELLED_BY") %>' CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
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
