<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Profit_Loss.ascx.cs" Inherits="Module_FA_Controls_Profit_Loss" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>
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
                    <span class="titleheading">Profit & Loss Account</span>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <table style="width: 532px">
                        <tr>
                            <td class="tdRight">
                                <asp:Label ID="lblStartMonth" runat="server" CssClass="Label" Text="Date From :"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtStartingDate" runat="server" CssClass="TextBox" Width="100px"
                                    TabIndex="1"></asp:TextBox>
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
                <td class="td">
                    <table>
                        <tr>
                            <td align="center" valign="top" colspan="4">
                                <asp:Label ID="lblAccountName" runat="server" Text="Profit & Loss Account" CssClass="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="top" colspan="4">
                                <cc3:Grid ID="grdPLAccount" runat="server" AutoPostBackOnSelect="True" AutoGenerateColumns="False"
                                    OnSelect="grdPLAccount_Select" ShowFooter="false" AllowAddingRecords="false"
                                    ShowColumnsFooter="true" ShowTotalNumberOfPages="false" PageSize="100" AllowColumnReordering="true"
                                    AllowSorting="false" AllowPaging="false" OnRebind="grdPLAccount_Rebind" OnRowDataBound="grdPLAccount_RowDataBound">
                                    <Columns>
                                        <cc3:Column DataField="ACCOUNT_ID" ShowHeader="true" Visible="false" HeaderText="Particulars"
                                            Width="10px">
                                        </cc3:Column>
                                        <cc3:Column DataField="ACCOUNT_NAME" ShowHeader="true" HeaderText="Particulars" Width="240px">
                                        </cc3:Column>
                                        <cc3:Column DataField="CR_AMOUNT" ItemStyle-HorizontalAlign="Right" ShowHeader="true"
                                            HeaderText="Dr" Width="125px">
                                        </cc3:Column>
                                        <cc3:Column DataField="DR_AMOUNT" ShowHeader="true" HeaderText="Cr" Width="125px">
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
