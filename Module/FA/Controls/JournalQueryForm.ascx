<%@ Control Language="C#" AutoEventWireup="true" CodeFile="JournalQueryForm.ascx.cs"
    Inherits="Module_FA_Controls_JournalQueryForm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <table>
            <tr>
                <td class="td">
                    <table>
                        <tr>
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
                    <span class="titleheading">Journal Voucher Query</span>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <table style="width: 885px">
                        <tr>
                            <td class="tdRight">
                                Date From :
                            </td>
                            <td>
                                <asp:TextBox ID="txtStartingDate" runat="server" CssClass="TextBox" Width="100px"
                                    TabIndex="1"></asp:TextBox>
                            </td>
                            <td class="tdRight">
                                Date To :
                            </td>
                            <td>
                                <asp:TextBox ID="txtEndingDate" runat="server" CssClass="TextBox" Width="100px" TabIndex="2"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trSelectVoucherType" runat="server">
                            <td class="TableHeader SmallFont tdLeft" colspan="4">
                                <span class="titleheading"><i>select voucher type.... </i></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:CheckBox ID="chkAllVouchers" runat="server" Text="Select All" Checked="true"
                                    AutoPostBack="true" OnCheckedChanged="chkAllVouchers_CheckedChanged" ForeColor="Bisque" />
                            </td>
                        </tr>
                        <tr id="trCheckBoxList" runat="server">
                            <td colspan="4">
                                <asp:CheckBoxList ID="chkVoucherType" runat="server" TabIndex="3" RepeatColumns="10"
                                    CssClass="Label" RepeatDirection="Horizontal" DataTextField="VCHR_NAME" DataValueField="VCHR_CODE">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr id="trClickVoucher" runat="server">
                            <td class="tdCenter" colspan="4">
                                <asp:Button ID="btnVoucherType" runat="server" Text="Click for Vouchers" TabIndex="4"
                                    OnClick="btnVoucherType_Click"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="trJournalMst" runat="server">
                <td class="td">
                    <cc2:Grid ID="grdJournalMst" runat="server" AllowAddingRecords="False" AllowFiltering="True"
                        PageSize="5" AutoGenerateColumns="False" Serialize="false" AllowPaging="true"
                        AllowPageSizeSelection="true" AllowMultiRecordSelection="False" AutoPostBackOnSelect="True"
                        TabIndex="5" OnSelect="grdJournalMst_Select">
                        <Columns>
                            <cc2:Column DataField="VCHR_CODE" Align="Left" HeaderText="Code" Width="110px">
                            </cc2:Column>
                            <cc2:Column DataField="VCHR_NAME" Align="Left" HeaderText="Voucher Name" Width="180px">
                            </cc2:Column>
                            <cc2:Column DataField="VCHR_NO" Align="Left" HeaderText="Voucher No" Width="130px">
                            </cc2:Column>
                            <cc2:Column DataField="JOURNAL_DATE" Align="Left" DataFormatString="{0:dd/MM/yyyy}"
                                HeaderText="Voucher Date" Width="125px">
                            </cc2:Column>
                            <cc2:Column DataField="DESCRIPTION" Align="Left" HeaderText="Description" Width="325px">
                            </cc2:Column>
                        </Columns>
                        <PagingSettings PageSizeSelectorPosition="Bottom" ShowRecordsCount="true" Position="Bottom" />
                        <FilteringSettings InitialState="Hidden" FilterPosition="Top" FilterLinksPosition="Top" />
                    </cc2:Grid>
                </td>
            </tr>
            <tr id="trJournalTrn" runat="server">
                <td class="td">
                    <cc2:Grid ID="grdJournalTrn" runat="server" AllowAddingRecords="False" AllowFiltering="True"
                        PageSize="5" AutoGenerateColumns="False" Serialize="false" AllowPaging="true"
                        AllowPageSizeSelection="true" AllowMultiRecordSelection="False" TabIndex="6">
                        <Columns>
                            <cc2:Column DataField="VCHR_NO" Align="Left" HeaderText="Code" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="ENTRY_TYPE" Align="Left" HeaderText="Type" Width="60px">
                            </cc2:Column>
                            <cc2:Column DataField="LDGR_NAME" Align="Left" HeaderText="Ledger Name" Width="160px">
                            </cc2:Column>
                            <cc2:Column DataField="DR_AMOUNT" Align="Left" HeaderText="Dr Amt" Width="80px">
                            </cc2:Column>
                            <cc2:Column DataField="CR_AMOUNT" Align="Left" HeaderText="Cr Amt" Width="80px">
                            </cc2:Column>
                            <cc2:Column DataField="DOC_NO" Align="Left" HeaderText="Doc No" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="DOC_DT" Align="Left" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Doc Date"
                                Width="120px">
                            </cc2:Column>
                            <cc2:Column DataField="DESCRIPTION" Align="Left" HeaderText="Narration" Width="180px">
                            </cc2:Column>
                        </Columns>
                        <PagingSettings PageSizeSelectorPosition="Bottom" ShowRecordsCount="true" Position="Bottom" />
                        <FilteringSettings InitialState="Hidden" FilterPosition="Top" FilterLinksPosition="Top" />
                    </cc2:Grid>
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
