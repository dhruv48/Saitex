<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BillQueryForm.ascx.cs"
    Inherits="Module_Inventory_Controls_BillQueryForm" %>
<table class="td tContentArial" width="950px">
    <tr>
        <td align="Right" class="td" colspan="10">
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
        <td class="TableHeader td" align="center" colspan="10">
            <span class="titleheading">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                Bill Query Form &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
    <tr>
        <td align="right">
            Bill&nbsp;NO.&nbsp;:
        </td>
        <td>
            <asp:DropDownList ID="ddlbillNO" runat="server" DataTextField="BILL_NUMB" DataValueField="BILL_NUMB"
                Width="160px" AutoPostBack="True" CssClass="tContentArial" OnSelectedIndexChanged="ddlbillNO_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td align="right">
            Party:
        </td>
        <td>
            <asp:DropDownList ID="ddlPartycode" runat="server" DataTextField="PRTY_CODE" DataValueField="PRTY_CODE"
                Width="160px" AutoPostBack="True" CssClass="tContentArial" OnSelectedIndexChanged="ddlPartycode_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td align="right">
            Trn&nbsp;Type:
        </td>
        <td>
            <asp:DropDownList ID="ddlTrnType" runat="server" DataTextField="TRN_TYPE" DataValueField="TRN_TYPE"
                Width="160px" AutoPostBack="True" CssClass="tContentArial" OnSelectedIndexChanged="ddlTrnType_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td align="right">
            Fin&nbsp;Vouch&nbsp;No.:
        </td>
        <td>
            <asp:DropDownList ID="ddlFinVouchNo" runat="server" DataTextField="FIN_VOUCH_NO"
                DataValueField="FIN_VOUCH_NO" Width="160px" AutoPostBack="True" CssClass="tContentArial"
                OnSelectedIndexChanged="ddlFinVouchNo_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        
        <td class="right">
                    Year:
                </td>
                <td>
                    <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" CssClass="gCtrTxt"
                        Font-Size="8" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" Width="80px">
                    </asp:DropDownList>
                </td>
        
    </tr>
    <tr>
        <td colspan="8" width="100%" class="TdBackVir">
            <b>Total&nbsp;Records&nbsp;: &nbsp;&nbsp;</b><asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
    <td width="100%" colspan="11" >
    <table>
    <tr>
        <td colspan="5" class="td tContentArial">
            <asp:Panel ID="pnlShowHover" runat="server" Width="950px" ScrollBars="Auto">
                <asp:GridView ID="GridProductEntry" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    Width="400%" CellPadding="4" ForeColor="#333333" HeaderStyle-Wrap="true"
                    Font-Size="X-Small" OnPageIndexChanging="GridProductEntry_PageIndexChanging"
                    OnSelectedIndexChanged="GridProductEntry_SelectedIndexChanged" 
                    PageSize="15">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                    <Columns>
                        <asp:BoundField DataField="YEAR" HeaderText="YEAR "></asp:BoundField>
                        <asp:BoundField DataField="BILL_YEAR" HeaderText="BILL YEAR" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                              <asp:BoundField DataField="BILL_DATE" HeaderText="BILL DATE" DataFormatString="{0:dd-MM-yyyy}"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                       <asp:BoundField DataField="BILL_ENTRY_DATE" HeaderText="BILL ENTRY DATE" DataFormatString="{0:dd-MM-yyyy}"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BILL_TYPE" HeaderText="BILL TYPE" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" >
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BILL_NUMB" HeaderText="BILL NUMB" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" >
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                       <%-- <asp:BoundField DataField="BILL_ENTR_BY" HeaderText="BILL ENTRY BY" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" >      
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>--%>
                        
                        <asp:TemplateField HeaderText="BILL ENTRY BY" HeaderStyle-HorizontalAlign="Left"   ItemStyle-HorizontalAlign="Left" >
                        <ItemTemplate   >
                        <asp:Label ID="lblbillenteruserName" runat="server" ToolTip='<%# Eval("BILL_ENTR_BY") %>' Text='<%# Eval("BILL_ENTER_BY_NAME") %>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="BILL_AMNT" HeaderText="BILL AMOUNT" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:0.00}" >     
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BILL_REMARK" HeaderText="BILL REMARK" HeaderStyle-HorizontalAlign="Left" Visible="false"
                            ItemStyle-HorizontalAlign="Left" >
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BILL_RCV_DATE" HeaderText="BILL RCV DATE" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" >
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                       <%-- <asp:BoundField  DataField="BILL_CLR_USER" HeaderText="BILL CLR USER" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left"  >
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>--%>
                        <asp:TemplateField HeaderText="BILL CLR USER" HeaderStyle-HorizontalAlign="Left"   ItemStyle-HorizontalAlign="Left" >
                        <ItemTemplate   >
                        <asp:Label ID="lblbillclruserName" runat="server" ToolTip='<%# Eval("BILL_CLR_USER") %>' Text='<%# Eval("BILL_CLR_USER_NAME") %>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="BILL_CLR_DATE" HeaderText="BILL CLR DATE" DataFormatString="{0:dd-MM-yyyy}"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BILL_ACC_RCV_DATE" HeaderText="BILL ACC RCV DATE" DataFormatString="{0:dd-MM-yyyy}"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BILL_ACC_RCV_USER" HeaderText="BILL ACC RCV USER" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" >
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BILL_CLR_ACC_USER" HeaderText="BILL CLR ACC USER" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" >
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BILL_CLR_ACC_DATE" HeaderText="BILL CLR ACC DATE" DataFormatString="{0:dd-MM-yyyy}"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BILL_CLR_PUR_USER" HeaderText="BILL CLR PUR USER" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" >
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BILL_CLR_PUR_REMARK" HeaderText="BILL CLR PUR REMARK"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BILL_CLR_PUR_DATE" HeaderText="BILL CLR PUR DATE" DataFormatString="{0:dd-MM-yyyy}"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
         
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
         
                        <asp:BoundField DataField="PRTY_CODE" HeaderText="PARTY CODE" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" >
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PARTYNAME" HeaderText="PARTY NAME" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" >
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        
                       <%-- PRTY_NAME--%>
                        <asp:BoundField DataField="COMP_NAME" HeaderText="COMPANY " HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" >
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BRANCH_NAME" HeaderText="BRANCH" Visible="false" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" >
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CONV_RATE" HeaderText="CONV RATE" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:0.00}" >
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FIN_COMP_CODE" HeaderText="FIN COMP CODE" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" >
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FIN_YEAR" HeaderText="FIN YEAR" />
                        <asp:BoundField DataField="FIN_VOUCH_TYPE" HeaderText="FIN VOUCH TYPE" />
                        <asp:BoundField DataField="FIN_VOUCH_NO" HeaderText="FIN VOUCH NO" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" >
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FIN_DEB_LGR_CODE" HeaderText="FIN DEB LGR CODE" />
                        <asp:BoundField DataField="VAT_TYPE" HeaderText="VAT TYPE" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" >
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="VAT_SUPP_TYPE" HeaderText="VAT SUPP TYPE" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" >
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="VAT_TRN_AMT" HeaderText="VAT TRN AMT" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" >
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="VAT_AMT" HeaderText="VAT AMT" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" >
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TRN_TYPE" HeaderText="TRN TYPE" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" >
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TRN_NUMB" HeaderText="TRN NUMB" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" >
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PRN_FLAG" HeaderText="PRN FLAG" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" >
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="QUALITY_POINT" HeaderText="QUALITY POINT" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" >
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DEL_POINT" HeaderText="DEL POINT" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" >
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PRICE_POINT" HeaderText="PRICE POINT" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" >
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SUPPORT_POINT" HeaderText="SUPPORT POINT" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" >
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TRN_AMT" HeaderText="TRN AMT" DataFormatString="{0:0.00}"
                            HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" >
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TUSER" HeaderText="TUSER" Visible="false" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" >
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TDATE" HeaderText="TDATE" DataFormatString="{0:dd-MM-yyyy}"
                            Visible="false" HeaderStyle-HorizontalAlign="Left" 
                            ItemStyle-HorizontalAlign="Left" >
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
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
    </td>
    </tr>
</table>
