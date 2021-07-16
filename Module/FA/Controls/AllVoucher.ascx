<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AllVoucher.ascx.cs" Inherits="Module_FA_Controls_AllVoucher" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <table class="tContentArial">
            <tr>
                <td class="td">
                    <table>
                        <tr>
                            <td id="tdSave" runat="server">
                                <asp:ImageButton ID="imgbtnNew" runat="server" Height="41" ImageUrl="~/CommonImages/save.jpg"
                                    OnClick="imgbtnNew_Click" ToolTip="Save" ValidationGroup="M1" Width="48" />
                            </td>
                            <td id="tdUpdate" runat="server">
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" Height="41" ImageUrl="~/CommonImages/edit1.jpg"
                                    OnClick="imgbtnUpdate_Click" ToolTip="Update" ValidationGroup="M1" Width="48" />
                            </td>
                            <td id="tdDelete" runat="server">
                                <asp:ImageButton ID="imgbtnDelete" runat="server" Height="41" ImageUrl="~/CommonImages/del6.png"
                                    OnClick="imgbtnDelete_Click" ToolTip="Delete" Width="48" />
                            </td>
                            <td id="tdFind" runat="server">
                                <asp:ImageButton ID="imgbtnFind" runat="server" Height="41" ImageUrl="~/CommonImages/link_find.png"
                                    OnClick="imgbtnFind_Click" ToolTip="Find" Width="48" />
                            </td>
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
                    <span class="titleheading">All Vouchers Entry</span>
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
                            <td class="TableHeader SmallFont tdLeft" colspan="6">
                                <span class="titleheading"><i>Main Details.... </i></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight">
                                Voucher Type :
                            </td>
                            <td class="tdLeft">
                                <asp:DropDownList ID="ddlVoucherType" runat="server" AppendDataBoundItems="true"
                                    AutoPostBack="true" Width="150px" DataTextField="VCHR_NAME" DataValueField="VCHR_CODE"
                                    TabIndex="1" CssClass="SmallFont" OnSelectedIndexChanged="ddlVoucherType_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFVoucherType" runat="server" ControlToValidate="ddlVoucherType"
                                    Display="None" ErrorMessage="Please Select Ledger Type" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="M1"></asp:RequiredFieldValidator>
                            </td>
                            <td class="tdRight">
                                Voucher No.:
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtVoucherNo" runat="server" CssClass="TextBox TextBoxDisplay" Width="100px"
                                    TabIndex="2" ReadOnly="True" OnTextChanged="txtVoucherNo_TextChanged"></asp:TextBox>
                                <asp:DropDownList ID="ddlVoucherNo" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                    Width="100px" DataTextField="VCHR_NO" DataValueField="VCHR_NO" TabIndex="3" CssClass="SmallFont"
                                    OnSelectedIndexChanged="ddlVoucherNo_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="tdRight">
                                Date :
                            </td>
                            <td class="tdRight">
                                <asp:TextBox ID="txtJournalDate" runat="server" CssClass="TextBox" Width="100px"
                                    TabIndex="4"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <table>
                                    <tr>
                                        <td class="tdRight">
                                            Description :
                                        </td>
                                        <td class="tdLeft" colspan="3">
                                            <asp:TextBox ID="txtDescription" runat="server" CssClass="TextBox" Width="600px"
                                                MaxLength="200" TabIndex="5"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="TableHeader SmallFont tdLeft" colspan="6">
                                <span class="titleheading"><i>transaction Details.... </i></span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="td" valign="top">
                    <table>
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0">
                                    <tr bgcolor="#336699" class="titleheading">
                                        <td colspan="2">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            Particulars
                                        </td>
                                        <td style="text-align: right">
                                            Debit
                                        </td>
                                        <td style="text-align: right">
                                            Credit
                                        </td>
                                        <td style="text-align: center">
                                            Doc No
                                        </td>
                                        <td style="text-align: center">
                                            Doc Date
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlEntry_Type" runat="server" Width="50px" TabIndex="6" CssClass="SmallFont"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlEntry_Type_SelectedIndexChanged">
                                                <asp:ListItem Value="Dr">Dr</asp:ListItem>
                                                <asp:ListItem Value="Cr">Cr</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlLedgerCode" runat="server" AppendDataBoundItems="true" Width="190px"
                                                DataTextField="LDGR_NAME" DataValueField="LDGR_CODE" TabIndex="7" CssClass="SmallFont"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlLedgerCode_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDebitAmount" runat="server" Width="80px" TabIndex="8" CssClass="TextBoxNo"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCreditAmount" runat="server" Width="80px" TabIndex="9" CssClass="TextBoxNo"
                                                Style="text-align: right"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDocNo" runat="server" Width="80px" TabIndex="10" CssClass="gCtrTxt UpperCase"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDocDT" runat="server" Width="90px" TabIndex="11"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSaveDetail" runat="server" Style="top: 0px; left: -1px" Text="Save"
                                                Width="50px" OnClick="btnSaveDetail_Click" TabIndex="12" ValidationGroup="M1">
                                            </asp:Button>
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="60px" OnClick="btnCancel_Click"
                                                TabIndex="13"></asp:Button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdRight">
                                            Narration :
                                        </td>
                                        <td class="tdLeft" colspan="5">
                                            <asp:TextBox ID="txtTranDescription" runat="server" CssClass="TextBox" Width="550px"
                                                MaxLength="200" TabIndex="14"></asp:TextBox>
                                        </td>
                                        <td class="tdCentre">
                                            <asp:TextBox ID="txtLedgerPopUp" runat="server" Width="1px" Visible="false" AutoPostBack="true"
                                                BackColor="#C1D3FB" BorderStyle="None" OnTextChanged="txtLedgerPopUp_TextChanged"></asp:TextBox>
                                            <asp:Button ID="btnLedger" runat="server" Text="Add Ledger" OnClick="btnLedger_Click">
                                            </asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="grdJourenaldetails" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" OnRowCommand="grdJourenaldetails_RowCommand" CellSpacing="0"
                                    Width="730px" TabIndex="15">
                                    <Columns>
                                        <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Particulars">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" CssClass="LabelNo" runat="server" Text='<%# Bind("ENTRY_TYPE") %>'></asp:Label>
                                                <asp:Label ID="Label2" CssClass="LabelNo" runat="server" Text='<%# Bind("LEDGER_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="200px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Debit Amount" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                            <FooterTemplate>
                                                <asp:Label ID="lblDr_Amount_ftr" runat="server" CssClass="LabelNo"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("DR_AMOUNT") %>' CssClass="LabelNo"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Credit Amount" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                            <FooterTemplate>
                                                <asp:Label ID="lblCr_Amount_ftr" runat="server" CssClass="LabelNo" ItemStyle-HorizontalAlign="Right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("CR_AMOUNT") %>' CssClass="LabelNo"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Doc No" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("DOC_NO") %>' CssClass="LabelNo"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Doc Date" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("DOC_DT") %>' CssClass="LabelNo"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Narration" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("DESC") %>' CssClass="LabelNo"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnEdit" runat="server" CommandArgument='<%# bind("UNIQUE_ID") %>'
                                                    CommandName="EditTRN" Text="Edit" />
                                                <asp:Button ID="btnDelete" runat="server" CommandArgument='<%# bind("UNIQUE_ID") %>'
                                                    CommandName="DeleteTRN" Text="Delete" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle VerticalAlign="Top" />
                                    <HeaderStyle CssClass="HeaderStyle SmallFont titleheading" BackColor="#336699" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <cc4:CalendarExtender ID="ce1" runat="server" TargetControlID="txtJournalDate" PopupPosition="TopRight"
            Format="dd/MM/yyyy">
        </cc4:CalendarExtender>
        <cc4:CalendarExtender ID="ce2" runat="server" TargetControlID="txtDocDT" PopupPosition="TopRight"
            Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate">
        </cc4:CalendarExtender>
        <cc4:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtJournalDate" PromptCharacter="_">
        </cc4:MaskedEditExtender>
        <cc4:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtDocDT" PromptCharacter="_">
        </cc4:MaskedEditExtender>
    </ContentTemplate>
</asp:UpdatePanel>
