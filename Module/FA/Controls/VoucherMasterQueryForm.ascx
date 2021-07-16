<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VoucherMasterQueryForm.ascx.cs"
    Inherits="Module_FA_Controls_VoucherMasterQueryForm" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <table>
            <tr>
                <td class="td" colspan="3">
                    <table cellpadding="0" cellspacing="0" border="1" align="left" class="tContentArial">
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
            <tr>
                <td class="TableHeader td" align="center" colspan="3">
                    <b class="titleheading">Voucher Master Query</b>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <cc2:Grid ID="grdVoucher" runat="server" AllowAddingRecords="False" AllowFiltering="True"
                        PageSize="5" AutoGenerateColumns="False" Serialize="false" AllowPaging="true"
                        AllowPageSizeSelection="true" AllowMultiRecordSelection="False" AutoPostBackOnSelect="True">
                        <Columns>
                            <cc2:Column DataField="VCHR_CODE" Align="Right" HeaderText="Code" Width="65px">
                            </cc2:Column>
                            <cc2:Column DataField="VCHR_NAME" Align="Left" HeaderText="Voucher Name" Width="125px">
                            </cc2:Column>
                            <cc2:Column DataField="VCHR_TYPE" Align="Left" HeaderText="Type" Width="90px">
                            </cc2:Column>
                            <cc2:Column DataField="VCHR_PREFIX" Align="Left" HeaderText="Pre" Width="60px">
                            </cc2:Column>
                            <cc2:Column DataField="VCHR_SUFFIX" Align="Left" HeaderText="Suff" Width="60px">
                            </cc2:Column>
                            <cc2:Column DataField="VCHR_DESC" Align="Left" HeaderText="Description" Width="130px">
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
