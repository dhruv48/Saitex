<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LedgerMasterQueryForm.ascx.cs"
    Inherits="Module_FA_Controls_LedgerMasterQueryForm" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <table>
            <tr>
                <td class="td">
                    <table cellpadding="0" cellspacing="0" border="1" align="left" class="tContentArial ">
                        <tr>
                            <td id="tdPrint" runat="server">
                                <asp:ImageButton ID="imgbtnPrint" Width="48" Height="41" runat="server" ToolTip="Print"
                                    ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click"></asp:ImageButton>
                            </td>
                            <td id="tdHelp" runat="server">
                                <asp:ImageButton ID="imgbtnHelp" Width="48" Height="41" runat="server" ToolTip="Help"
                                    ImageUrl="~/CommonImages/link_help.png" OnClick="imgbtnHelp_Click"></asp:ImageButton>
                            </td>
                            <td id="tdExit" runat="server">
                                <asp:ImageButton ID="imgbtnExit" Width="48" Height="41" runat="server" ToolTip="Exit"
                                    ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr class="TableHeader">
                <td align="center" valign="top" class="td">
                    <span class="titleheading">Ledger Master Query</span>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <cc2:Grid ID="Grid1" runat="server" AllowAddingRecords="False" AllowFiltering="True"
                        PageSize="5" AutoGenerateColumns="False" Serialize="false" AllowPaging="true"
                        AllowPageSizeSelection="true" AllowMultiRecordSelection="False" AutoPostBackOnSelect="True"
                        OnSelect="Grid1_Select">
                        <Columns>
                            <cc2:Column DataField="LDGR_CODE" Align="Right" HeaderText="Ledger Code" Width="110px">
                            </cc2:Column>
                            <cc2:Column DataField="LDGR_NAME" Align="Left" HeaderText="Ledger Name‎" Width="120px">
                            </cc2:Column>
                            <cc2:Column DataField="PRINT_NAME" Align="Left" HeaderText="Print Name" Width="120px">
                            </cc2:Column>
                            <cc2:Column DataField="GRP_NAME" Align="Left" HeaderText="Group Name" Width="140px">
                            </cc2:Column>
                            <cc2:Column DataField="OP_BAL" Align="Right" HeaderText=" OP_BAL" Width="80px">
                            </cc2:Column>
                            <cc2:Column DataField="LDGR_DESC" Align="Left" HeaderText="Ledger Desc" Width="130px">
                            </cc2:Column>
                        </Columns>
                        <PagingSettings PageSizeSelectorPosition="Bottom" ShowRecordsCount="true" Position="Bottom" />
                        <FilteringSettings InitialState="Hidden" FilterPosition="Top" FilterLinksPosition="Top" />
                    </cc2:Grid>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
