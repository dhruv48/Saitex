<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TrailBalance.ascx.cs"
    Inherits="Module_FA_Controls_TrailBalance" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc3" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <table width="98%">
            <tr>
                <td class="td" width="100%">
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
                <td class="td" align="center" valign="top" width="100%">
                    <span class="titleheading">Trial Balance</span>
                </td>
            </tr>
            <tr>
                <td class="td" width="100%">
                    <table width="100%">
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
                        <tr>
                            <td colspan="4" align="center">
                                <asp:Button ID="btnOK" runat="server" Text="OK" Width="10%" OnClick="btnOK_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="td" width="100%">
                    <table width="100%">
                        <tr>
                            <td align="center" valign="top" width="100%">
                                <asp:Label ID="lblAccountName" runat="server" CssClass="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" width="100%">
                                <cc3:Grid ID="grdTrial" runat="server" AutoPostBackOnSelect="True" AutoGenerateColumns="False"
                                    OnSelect="grdTrial_Select" ShowFooter="false" AllowAddingRecords="false" ShowColumnsFooter="true"
                                    ShowTotalNumberOfPages="false" PageSize="100" AllowColumnReordering="true" AllowSorting="false"
                                    AllowPaging="false" OnRebind="grdTrial_Rebind" OnRowDataBound="grdTrial_RowDataBound"
                                    Width="98%">
                                    <Columns>
                                        <cc3:Column DataField="ACCOUNT_ID" ShowHeader="true" Visible="false" HeaderText="Account ID"
                                            Width="2%" Wrap="true">
                                        </cc3:Column>
                                        <cc3:Column Wrap="true" DataField="ACCOUNT_NAME" ShowHeader="true" HeaderText="Particulars"
                                            Width="20%">
                                        </cc3:Column>
                                        <cc3:Column Wrap="true" DataField="DR_OP_AMOUNT" ItemStyle-HorizontalAlign="Right"
                                            ShowHeader="true" Width="14%" HeaderText="Opening Bal. Dr" />
                                        <cc3:Column Wrap="true" DataField="CR_OP_AMOUNT" ItemStyle-HorizontalAlign="Right"
                                            ShowHeader="true" Width="14%" HeaderText="Opening Bal. Cr" />
                                        <cc3:Column Wrap="true" DataField="DR_TOTAL" ItemStyle-HorizontalAlign="Right" ShowHeader="true"
                                            Width="11%" HeaderText="Dr">
                                        </cc3:Column>
                                        <cc3:Column Wrap="true" DataField="CR_TOTAL" ShowHeader="true" HeaderText="Cr" Width="11%">
                                        </cc3:Column>
                                        <cc3:Column Wrap="true" DataField="CR_AMOUNT" ItemStyle-HorizontalAlign="Right" ShowHeader="true"
                                            Width="14%" HeaderText="Closing Bal. Dr" />
                                        <cc3:Column Wrap="true" DataField="DR_AMOUNT" ItemStyle-HorizontalAlign="Right" ShowHeader="true"
                                            Width="14%" HeaderText="Closing Bal. Cr" />
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
        <cc4:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtStartingDate" PromptCharacter="_">
        </cc4:MaskedEditExtender>
        <cc4:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtEndingDate" PromptCharacter="_">
        </cc4:MaskedEditExtender>
    </ContentTemplate>
</asp:UpdatePanel>
