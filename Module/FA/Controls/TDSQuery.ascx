<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TDSQuery.ascx.cs" Inherits="Module_FA_Controls_TDSQuery" %>
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
                            <td id="tdPrint" runat="server">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                                    OnClick="imgbtnPrint_Click" ToolTip="Print" Width="48" />
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
            <tr class="TableHeader">
                <td align="center" valign="top" class="td">
                    <span class="titleheading">TDS Query Form</span>
                    <cc2:Grid ID="grdTDS" runat="server" AllowAddingRecords="False" AllowFiltering="True"
                        PageSize="5" AutoGenerateColumns="False" Serialize="false" AllowPaging="true"
                        AllowPageSizeSelection="true" AllowMultiRecordSelection="False" AutoPostBackOnSelect="false">
                        <Columns>
                            <cc2:Column DataField="VCHR_NAME" Align="Left" HeaderText="Voucher Name" Width="130px">
                            </cc2:Column>
                            <cc2:Column DataField="VCHR_NO" Align="Left" HeaderText="Voucher No" Width="130px">
                            </cc2:Column>
                            <cc2:Column DataField="TDS_DATE" Align="Left" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}"
                                Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="REF_VCHR_NO" Align="Left" HeaderText="Ref Voucher No" Width="135px">
                            </cc2:Column>
                            <cc2:Column DataField="PARTY_LDGR_NAME" Align="Left" HeaderText="Party Name" Width="150px">
                            </cc2:Column>
                            <cc2:Column DataField="DOC_NO" Align="Left" HeaderText="Doc No" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="DOC_DT" Align="Left" HeaderText="Doc Date" DataFormatString="{0:dd/MM/yyyy}"
                                Width="110px">
                            </cc2:Column>
                            <cc2:Column DataField="CONTRACT_CODE" Align="Center" HeaderText="Contract Code" Width="130px">
                            </cc2:Column>
                            <cc2:Column DataField="TAX_RATE" Align="Center" HeaderText="Rate(In %)" Width="110px">
                            </cc2:Column>
                            <cc2:Column DataField="TAX_LDGR_NAME" Align="Left" HeaderText="Tax" Width="120px">
                            </cc2:Column>
                            <cc2:Column DataField="AMOUNT" Align="Left" HeaderText="Amount" Width="90px">
                            </cc2:Column>
                        </Columns>
                        <PagingSettings PageSizeSelectorPosition="Bottom" ShowRecordsCount="true" Position="Bottom" />
                        <FilteringSettings InitialState="Hidden" FilterPosition="Top" FilterLinksPosition="Top" />
                    </cc2:Grid>
                </td>
            </tr>
            <tr>
                <td class="td">
                    &nbsp;
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
