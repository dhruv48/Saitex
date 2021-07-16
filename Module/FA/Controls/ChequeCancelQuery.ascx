<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChequeCancelQuery.ascx.cs"
    Inherits="Module_FA_Controls_ChequeCancelQuery" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <table align="left" class="tContentArial">
            <tr>
                <td align="left" valign="top" class="td" width="100%">
                    <table align="left">
                        <tr>
                            <td>
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Width="48" Height="41" ToolTip="Print"
                                    ImageUrl="~/CommonImages/link_print.png"></asp:ImageButton>&nbsp;
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
                    <b class="titleheading">Cancelled Cheque Detail</b>
                </td>
            </tr>
            <tr>
                <td align="center" width="100%" class="td">
                    <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" class="td" width="100%">
                    <b>Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server"
                        CssClass="Label"></asp:Label></b>
                </td>
            </tr>
            <tr>
                <td align="left" class="td">
                    <cc2:Grid ID="grdCancelledCheque" runat="server" AllowAddingRecords="False" AllowFiltering="True"
                        PageSize="10" AutoGenerateColumns="False" Serialize="false" AllowPaging="true"
                        AllowPageSizeSelection="true" AllowMultiRecordSelection="False" TabIndex="5">
                        <Columns>
                            <cc2:Column DataField="VCHR_NO" Align="Left" HeaderText="Voucher No" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="BANK_NAME" Align="Left" HeaderText="Bank Name" Width="150px">
                            </cc2:Column>
                            <cc2:Column DataField="CHEQUEBOOK_CODE" Align="Left" HeaderText="CHQ Book Code" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="CHEQUEBOOK_NO" Align="Left" HeaderText="CHQ Book No" Width="150px">
                            </cc2:Column>
                            <cc2:Column DataField="CHEQUE_NO" Align="Left" HeaderText="CHQ No" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="CHEQUE_DATE" Align="Left" DataFormatString="{0:dd/MM/yyyy}"
                                HeaderText="CHQ Date" Width="85px">
                            </cc2:Column>
                            <cc2:Column DataField="PARTY_LGR_CODE" Align="Left" HeaderText="Party LGR Code" Width="85px">
                            </cc2:Column>
                            <cc2:Column DataField="PARTY_LGR_NAME" Align="Left" HeaderText="Party Name" Width="120px">
                            </cc2:Column>
                            <cc2:Column DataField="CANCELLED_AMT" Align="Left" HeaderText="Amount" Width="85px">
                            </cc2:Column>
                            <cc2:Column DataField="CANCELLED_DATE" Align="Left" DataFormatString="{0:dd/MM/yyyy}"
                                HeaderText="Cancel Date" Width="85px">
                            </cc2:Column>
                            <cc2:Column DataField="CANCELLED_BY" Align="Left" HeaderText="Cancel By" Width="100px">
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
