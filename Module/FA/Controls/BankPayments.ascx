<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BankPayments.ascx.cs"
    Inherits="Module_FA_Controls_BankPayments" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>
<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>--%>
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
                    <span class="titleheading">Bank Payment Voucher</span>
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
                <td class="td">
                    <table>
                        <tr>
                            <td class="TableHeader SmallFont tdLeft" colspan="6">
                                <span class="titleheading"><i>main details....</i></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight">
                                *Voucher Type :
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtPaymentVoucher" runat="server" CssClass="TextBox TextBoxDisplay"
                                    Width="150px" TabIndex="1" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td class="tdRight">
                                *Voucher No:
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtVoucherNo" runat="server" CssClass="TextBox TextBoxDisplay" Width="150px"
                                    TabIndex="2" ReadOnly="True" OnTextChanged="txtVoucherNo_TextChanged"></asp:TextBox>
                                <asp:DropDownList ID="ddlVoucherNo" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                    Width="150px" DataTextField="VCHR_DTL" DataValueField="VCHR_NO" TabIndex="3"
                                    CssClass="SmallFont" OnSelectedIndexChanged="ddlVoucherNo_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="tdRight">
                                *Voucher Date :
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtJournalDate" runat="server" CssClass="TextBox TextBoxDisplay"
                                    Width="100px" TabIndex="4" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight">
                                *Type Of Payment :
                            </td>
                            <td class="tdLeft">
                                <asp:DropDownList ID="ddlTypeOfPayment" runat="server" Width="153px" TabIndex="5"
                                    CssClass="SmallFont" AutoPostBack="true" OnSelectedIndexChanged="ddlTypeOfPayment_SelectedIndexChanged">
                                    <asp:ListItem Value="ON ACCOUNT">ON ACCOUNT</asp:ListItem>
                                    <asp:ListItem Value="BILL WISE">BILL WISE</asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="txtTypeOfPayment" runat="server" CssClass="TextBox TextBoxDisplay"
                                    Width="150px" ReadOnly="True" Visible="false"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFTypeOfPayment" runat="server" ControlToValidate="ddlTypeOfPayment"
                                    Display="None" ErrorMessage="Please Select Type Of Payment" InitialValue="0"
                                    SetFocusOnError="True" ValidationGroup="M1"></asp:RequiredFieldValidator>
                            </td>
                            <td class="tdRight">
                                *Mode Of Payment :
                            </td>
                            <td class="tdLeft">
                                <asp:DropDownList ID="ddlModeOfPayment" runat="server" Width="153px" TabIndex="6"
                                    CssClass="SmallFont" AutoPostBack="true" OnSelectedIndexChanged="ddlModeOfPayment_SelectedIndexChanged">
                                    <asp:ListItem Value="CHEQUE">CHEQUE</asp:ListItem>
                                    <asp:ListItem Value="DEMAND DRAFT">DEMAND DRAFT</asp:ListItem>
                                    <asp:ListItem Value="PAY ORDER">PAY ORDER</asp:ListItem>
                                    <asp:ListItem Value="ECS">ECS</asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="txtModeOfPayment" runat="server" CssClass="TextBox TextBoxDisplay"
                                    Width="150px" ReadOnly="True" Visible="false"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTypeOfPayment"
                                    Display="None" ErrorMessage="Please Select Type Of Payment" InitialValue="0"
                                    SetFocusOnError="True" ValidationGroup="M1"></asp:RequiredFieldValidator>
                            </td>
                            <td class="tdRight">
                                *Date :
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtChequeDate" runat="server" CssClass="TextBox" Width="100px" TabIndex="7"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight">
                                Description :
                            </td>
                            <td colspan="5">
                                <asp:TextBox ID="txtDescription" runat="server" CssClass="TextBox" Width="698px"
                                    MaxLength="200" TabIndex="8"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="td" valign="top">
                    <table>
                        <tr>
                            <td class="TableHeader SmallFont tdLeft">
                                <span class="titleheading"><i>transaction details.... </i></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr bgcolor="#336699" class="titleheading">
                                        <td>
                                        </td>
                                        <td>
                                            *Particulars
                                        </td>
                                        <td style="text-align: center">
                                            *Amount
                                        </td>
                                        <td colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdRight">
                                            <asp:DropDownList ID="ddlEntry_Type" runat="server" Width="50px" TabIndex="14" CssClass="SmallFont"
                                                Enabled="false">
                                                <asp:ListItem Value="Dr" Selected="True">Dr</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlLedgerCode" runat="server" AppendDataBoundItems="true" Width="200px"
                                                DataTextField="LDGR_NAME" DataValueField="LDGR_CODE" TabIndex="15" CssClass="SmallFont"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlLedgerCode_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAmount" runat="server" Width="80px" TabIndex="16" AutoPostBack="True"
                                                OnTextChanged="txtAmount_TextChanged" CssClass="TextBoxNo TextBoxDisplay"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RFVAmount" runat="server" ValidationGroup="M1" Display="dynamic"
                                                ErrorMessage="Enter Amount" ControlToValidate="txtAmount" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="RVAmount" runat="server" ValidationGroup="M1" Display="Dynamic"
                                                ControlToValidate="txtAmount" MinimumValue="0" MaximumValue="9999999" ErrorMessage="Enter only number"
                                                Type="Double" SetFocusOnError="true"></asp:RangeValidator>
                                        </td>
                                        <td>
                                            <asp:Button ID="blnAdjustment" runat="server" Style="top: 0px; left: -1px" Text="Adjust"
                                                Width="50px" TabIndex="17" ValidationGroup="M1" OnClick="blnAdjustment_Click">
                                            </asp:Button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdRight">
                                            Narration :
                                        </td>
                                        <td colspan="5">
                                            <asp:TextBox ID="txtTranDescription" runat="server" CssClass="TextBox" Width="695px"
                                                MaxLength="200" TabIndex="18"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdRight">
                                            <asp:DropDownList ID="ddlCredit" runat="server" Width="50px" TabIndex="9" CssClass="SmallFont"
                                                Enabled="false">
                                                <asp:ListItem Value="Cr" Selected="True">Cr</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlPaymentLedger" runat="server" AppendDataBoundItems="true"
                                                Width="200px" DataTextField="LDGR_NAME" DataValueField="LDGR_CODE" TabIndex="10"
                                                CssClass="SmallFont" AutoPostBack="true" OnSelectedIndexChanged="ddlPaymentLedger_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPaymentNo" runat="server" Text="*Payment No"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtPaymentNo" runat="server" CssClass="gCtrTxt UpperCase" Width="150px"
                                                TabIndex="11"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trChequeRow" runat="server">
                                        <td>
                                            <asp:Label ID="lblChequeBookNo" runat="server" Text="*Cheque Book No"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlChequeBookNo" runat="server" AppendDataBoundItems="true"
                                                Width="200px" DataTextField="CHEQUEBOOK_NO" DataValueField="CHEQUEBOOK_CODE"
                                                TabIndex="12" CssClass="SmallFont" AutoPostBack="true" OnSelectedIndexChanged="ddlChequeBookNo_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblChequeNo" runat="server" Text="*Cheque No"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <asp:DropDownList ID="ddlChequeNo" runat="server" AppendDataBoundItems="true" Width="150px"
                                                DataTextField="CHEQUE_NO" DataValueField="CHEQUE_NO" TabIndex="13" CssClass="SmallFont">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TableHeader SmallFont tdLeft" colspan="6">
                                            <span class="titleheading"><i>bank transaction details.... </i></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <table>
                                                <tr>
                                                    <td>
                                                        Bank Charges :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtBankCharges" runat="server" Width="100px" TabIndex="19" AutoPostBack="true"
                                                            OnTextChanged="txtBankCharges_TextChanged" MaxLength="10" CssClass="TextBoxNo"></asp:TextBox>
                                                        <asp:RangeValidator ID="RVBankCharges" runat="server" ValidationGroup="M1" Display="Dynamic"
                                                            ControlToValidate="txtBankCharges" MinimumValue="0" MaximumValue="9999999" ErrorMessage="Enter only number"
                                                            Type="Double" SetFocusOnError="true"></asp:RangeValidator>
                                                    </td>
                                                    <td>
                                                        *Bank Charges Code :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtChargesCode" runat="server" Width="100px" TabIndex="20" CssClass="TextBoxNo TextBoxDisplay"
                                                            ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        *Amount Payable :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAmountPayable" runat="server" Width="100px" TabIndex="21" ReadOnly="true"
                                                            CssClass="TextBoxNo TextBoxDisplay"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Payable To :
                                                    </td>
                                                    <td colspan="7">
                                                        <asp:TextBox ID="txtPayableTo" runat="server" CssClass="gCtrTxt UpperCase" Width="695px"
                                                            MaxLength="200" TabIndex="22"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Import No :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtImportNo" runat="server" Width="100px" TabIndex="23" MaxLength="12"
                                                            CssClass="gCtrTxt UpperCase"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        Bill No :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtBillNo" runat="server" Width="100px" TabIndex="24" CssClass="gCtrTxt UpperCase"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        *Amt For Dr.(JV) :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAmtJV" runat="server" Width="100px" TabIndex="25" CssClass="TextBoxNo TextBoxDisplay"
                                                            ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Payable At :
                                                    </td>
                                                    <td colspan="7">
                                                        <asp:TextBox ID="txtPayableAt" runat="server" CssClass="gCtrTxt UpperCase" Width="695px"
                                                            MaxLength="200" TabIndex="26"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Remarks :
                                                    </td>
                                                    <td colspan="7">
                                                        <asp:TextBox ID="txtChequeRemarks" runat="server" CssClass="TextBox" Width="695px"
                                                            MaxLength="200" TabIndex="27"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <cc4:CalendarExtender ID="ce1" runat="server" TargetControlID="txtJournalDate" PopupPosition="TopLeft"
            Format="dd/MM/yyyy">
        </cc4:CalendarExtender>
        <cc4:CalendarExtender ID="ce2" runat="server" TargetControlID="txtChequeDate" PopupPosition="TopLeft"
            Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate">
        </cc4:CalendarExtender>
        <cc4:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtJournalDate" PromptCharacter="_">
        </cc4:MaskedEditExtender>
        <cc4:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtChequeDate" PromptCharacter="_">
        </cc4:MaskedEditExtender>
   <%-- </ContentTemplate>
</asp:UpdatePanel>
--%>