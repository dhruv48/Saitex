<%@ Control Language="C#" AutoEventWireup="true" CodeFile="yarnbillquery.ascx.cs"
    Inherits="Module_Yarn_SalesWork_Controls_yarnbillquery" %>
<table class="td tContentArial" width="100%">
    <tr>
        <td align="left" class="td" colspan="6">
            <table align="left">
                <tr>
                    <td id="tdPrint" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" Width="48" OnClick="imgbtnPrint_Click" />
                    </td>
                    <td id="tdExit" runat="server">
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                            Width="48" Height="41" OnClick="imgbtnExit_Click"></asp:ImageButton>
                    </td>
                    <td id="tdHelp" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                            ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="TableHeader td" align="center" colspan="6">
            <span class="titleheading">Bill Query Form </span>
        </td>
    </tr>
    <tr>
        <td align="left" valign="top" class="td" colspan="6">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>
    <tr>
        <td align="right">
            Bill NO. :
        </td>
        <td>
            <asp:DropDownList ID="ddlbillNO" runat="server" DataTextField="BILL_NUMB" DataValueField="BILL_NUMB"
                Width="128px" AutoPostBack="True" CssClass="tContentArial" OnSelectedIndexChanged="ddlbillNO_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td align="right">
            Party Name :
        </td>
        <td>
            <asp:DropDownList ID="ddlPartycode" runat="server" DataTextField="PRTY_NAME" DataValueField="PRTY_CODE"
                Width="128px" AutoPostBack="True" CssClass="tContentArial" OnSelectedIndexChanged="ddlPartycode_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td align="right">
            Fin Vouch No.:
        </td>
        <td>
            <asp:DropDownList ID="ddlFinVouchNo" runat="server" DataTextField="FIN_VOUCH_NO"
                DataValueField="FIN_VOUCH_NO" Width="128px" AutoPostBack="True" CssClass="tContentArial"
                OnSelectedIndexChanged="ddlFinVouchNo_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:DropDownList ID="ddlTrnType" runat="server" DataTextField="TRN_TYPE" DataValueField="TRN_TYPE"
                Width="128px" AutoPostBack="True" CssClass="tContentArial" OnSelectedIndexChanged="ddlTrnType_SelectedIndexChanged"
                Visible="False">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td colspan="6" width="100%" class="TdBackVir">
            <b>Total Records : &nbsp;&nbsp;</b><asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="td tContentArial" colspan="6">
            <asp:Panel ID="Panel1" runat="server" Height="440px" ScrollBars="Auto" Width="950px">
                <asp:GridView ID="GridProductEntry" runat="server" AutoGenerateColumns="False" Font-Size="X-Small"
                    HeaderStyle-Wrap="true" PageSize="8" AllowPaging="True" 
                    AllowSorting="True" CellPadding="3" Width="400%" CssClass="smallfont" 
                    PagerStyle-HorizontalAlign="Left" BackColor="White" BorderColor="#999999" 
                    BorderStyle="None" BorderWidth="1px">
                     <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                    <Columns>
                        <asp:BoundField DataField="YEAR" HeaderText="YEAR "></asp:BoundField>
                        <asp:BoundField DataField="BILL_YEAR" HeaderText="BILL YEAR"></asp:BoundField>
                        <asp:BoundField DataField="BILL_TYPE" HeaderText="BILL TYPE" />
                        <asp:BoundField DataField="BILL_NUMB" HeaderText="BILL NUMB" />
                        <asp:BoundField DataField="BILL_ENTR_BY" HeaderText="BILL ENTR BY" />
                        <asp:BoundField DataField="BILL_REMARK" HeaderText="BILL REMARK" />
                        <asp:BoundField DataField="BILL_RCV_DATE" HeaderText="BILL RCV DATE" />
                        <asp:BoundField DataField="BILL_CLR_USER" HeaderText="BILL CLR USER" DataFormatString="{0:dd-MM-yyyy}" />
                        <asp:BoundField DataField="BILL_CLR_DATE" HeaderText="BILL CLR DATE" DataFormatString="{0:dd-MM-yyyy}" />
                        <asp:BoundField DataField="BILL_ACC_RCV_DATE" HeaderText="BILL ACC RCV DATE" DataFormatString="{0:dd-MM-yyyy}" />
                        <asp:BoundField DataField="BILL_ACC_RCV_USER" HeaderText="BILL ACC RCV USER" />
                        <asp:BoundField DataField="BILL_CLR_ACC_USER" HeaderText="BILL CLR ACC USER" />
                        <asp:BoundField DataField="BILL_CLR_ACC_DATE" HeaderText="BILL CLR ACC DATE" DataFormatString="{0:dd-MM-yyyy}" />
                        <asp:BoundField DataField="BILL_CLR_PUR_USER" HeaderText="BILL CLR PUR USER" />
                        <asp:BoundField DataField="BILL_CLR_PUR_REMARK" HeaderText="BILL CLR PUR REMARK" />
                        <asp:BoundField DataField="BILL_CLR_PUR_DATE" HeaderText="BILL CLR PUR DATE" DataFormatString="{0:dd-MM-yyyy}" />
                        <asp:BoundField DataField="BILL_ENTRY_DATE" HeaderText="BILL ENTRY DATE" DataFormatString="{0:dd-MM-yyyy}" />
                        <asp:BoundField DataField="BILL_DATE" HeaderText="BILL DATE" DataFormatString="{0:dd-MM-yyyy}" />
                        <asp:BoundField DataField="BILL_AMNT" HeaderText="BILL AMNT" DataFormatString="{0:0.00}"
                            ItemStyle-HorizontalAlign="Right" >
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BILL_DATE" HeaderText="BILL DATE" DataFormatString="{0:dd-MM-yyyy}" />
                        <asp:BoundField DataField="PRTY_CODE" HeaderText="PRTY CODE" />
                        <asp:BoundField DataField="PRTY_NAME" HeaderText="PRTY NAME" />
                        <asp:BoundField DataField="COMP_NAME" HeaderText="COMPANY " Visible="false" />
                        <asp:BoundField DataField="BRANCH_NAME" HeaderText="BRANCH" Visible="false" />
                        <asp:BoundField DataField="CONV_RATE" HeaderText="CONV RATE" />
                        <asp:BoundField DataField="FIN_COMP_CODE" HeaderText="FIN COMP CODE" />
                        <asp:BoundField DataField="FIN_YEAR" HeaderText="FIN YEAR" />
                        <asp:BoundField DataField="FIN_VOUCH_TYPE" HeaderText="FIN VOUCH TYPE" />
                        <asp:BoundField DataField="FIN_VOUCH_NO" HeaderText="FIN VOUCH_NO" />
                        <asp:BoundField DataField="FIN_DEB_LGR_CODE" HeaderText="FIN DEB LGR CODE" />
                        <asp:BoundField DataField="VAT_TYPE" HeaderText="VAT TYPE" />
                        <asp:BoundField DataField="VAT_SUPP_TYPE" HeaderText="VAT SUPP TYPE" />
                        <asp:BoundField DataField="VAT_TRN_AMT" HeaderText="VAT TRN AMT" />
                        <asp:BoundField DataField="VAT_AMT" HeaderText="VAT AMT" />
                        <asp:BoundField DataField="TRN_TYPE" HeaderText="TRN TYPE" />
                        <asp:BoundField DataField="TRN_NUMB" HeaderText="TRN NUMB" />
                        <asp:BoundField DataField="PRN_FLAG" HeaderText="PRN FLAG" />
                        <asp:BoundField DataField="QUALITY_POINT" HeaderText="QUALITY POINT" />
                        <asp:BoundField DataField="DEL_POINT" HeaderText="DEL POINT" />
                        <asp:BoundField DataField="PRICE_POINT" HeaderText="PRICE POINT" />
                        <asp:BoundField DataField="SUPPORT_POINT" HeaderText="SUPPORT POINT" />
                        <asp:BoundField DataField="TRN_AMT" HeaderText="TRN AMT" DataFormatString="{0:0.00}"
                            ItemStyle-HorizontalAlign="Right" >
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TUSER" HeaderText="TUSER" Visible="false" />
                        <asp:BoundField DataField="TDATE" HeaderText="TDATE" DataFormatString="{0:dd-MM-yyyy}"
                            Visible="false" />
                    </Columns>
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                </asp:GridView>
            </asp:Panel>
        </td>
    </tr>
</table>
