<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UnadjustedAdvice.ascx.cs"
    Inherits="Module_FA_Controls_UnadjustedAdvice" %>
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
                                    ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click"></asp:ImageButton>&nbsp;
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
                    <b class="titleheading">Pending Advanced Advice</b>
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
                    <cc2:Grid ID="grdPendingAdvice" runat="server" AllowAddingRecords="False" AllowFiltering="True"
                        PageSize="10" AutoGenerateColumns="False" Serialize="false" AllowPaging="true"
                        AllowPageSizeSelection="true" AllowMultiRecordSelection="False" TabIndex="5">
                        <Columns>
                            <cc2:Column DataField="ADV_NO" Align="Left" HeaderText="Advice No" Width="110px">
                            </cc2:Column>
                            <cc2:Column DataField="ADV_DATE" Align="Left" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Advice Date"
                                Width="130px">
                            </cc2:Column>
                            <cc2:Column DataField="LEDGER_CODE" Align="Left" HeaderText="Code" Width="130px">
                            </cc2:Column>
                            <cc2:Column DataField="LDGR_NAME" Align="Left" HeaderText="Party Name" Width="180px">
                            </cc2:Column>
                            <cc2:Column DataField="ADV_AMT" Align="Left" HeaderText="Advice Amount" Width="120px">
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
