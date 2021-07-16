<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PurchaseRegisterDetail.ascx.cs"
    Inherits="Module_Inventory_Controls_PurchaseRegisterDetail" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
    }
    .header
    {
        margin-left: 4px;
    }
    .c1
    {
        width: 80px;
    }
    .c4
    {
        margin-left: 4px;
        width: 300px;
    }
    .d1
    {
        width: 150px; 
    }
    .d2
    {
        margin-left: 4px;
        width: 200px;
    }
    .d3
    {
        width: 100px;
    }
</style>
<table align="left" class="td tContentArial" width="945px">
    <tr>
        <td class="td" colspan="8">
            <table>
                <tr>
                    <td>
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                            OnClick="imgbtnPrint_Click"></asp:ImageButton>
                    </td>
                    <td>  
                              <asp:ImageButton ID="imgBtnExportExcel" runat="server" Width="48" Height="41" ToolTip="Print"
                            ImageUrl="~/CommonImages/export.png" onclick="imgBtnExportExcel_Click" ></asp:ImageButton>&nbsp;</td> 
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                            OnClick="imgbtnClear_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                            ToolTip="Exit" Width="48" OnClick="imgbtnExit_Click" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                            ToolTip="Help" Width="48" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader td" colspan="8">
            <span class="titleheading"><strong>Purchase Register Detail</strong> </span>
        </td>
    </tr>
    <tr>
        <td align="right">
            Branch:
        </td>
        <td>
            <asp:DropDownList ID="ddlBranch" runat="server" CssClass="gCtrTxt " Font-Size="9"
                Width="160px"  OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td class="tdRight">
            Year:
        </td>
        <td>
            <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" CssClass="gCtrTxt"
                OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" Width="160px">
            </asp:DropDownList>
        </td>
        <td class="tdRight">
            From date:
        </td>
        <td class="tdLeft">
            <asp:TextBox ID="TxtFromDate" Width="150px" runat="server" CssClass="SmallFont TextBox UpperCase"
                OnTextChanged="TxtFromDate_TextChanged" AutoPostBack="True"></asp:TextBox>
        </td>
        <td class="tdRight">
            To Date:
        </td>
        <td class="tdLeft">
            <asp:TextBox ID="TxtToDate" CssClass="SmallFont TextBox UpperCase" Width="150px"
                runat="server" OnTextChanged="TxtToDate_TextChanged1" AutoPostBack="True"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="tdRight">
            Item:
        </td>
        <td class="tdLeft">
            <cc2:ComboBox ID="txtICODE" runat="server" CssClass="smallfont" Width="151px" EnableLoadOnDemand="True"
                DataTextField="ITEM_CODE" DataValueField="ITEM_CODE" MenuWidth="650px" EnableVirtualScrolling="true"
                OpenOnFocus="true" Visible="true" Height="200px" EmptyText="------------All----------"
                OnLoadingItems="txtICODE_LoadingItems">
                <HeaderTemplate>
                    <div class="header d1">
                        ITEM CODE</div>
                    <div class="header d2">
                        ITEM DESCRIPTION</div>
                    <div class="header d3">
                        TYPE</div>
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="item d1">
                        <%# Eval("ITEM_CODE")%></div>
                    <div class="item d2">
                        <%# Eval("ITEM_DESC") %></div>
                    <div class="item d3">
                        <%# Eval("ITEM_TYPE")%></div>
                </ItemTemplate>
                <FooterTemplate>
                    Displaying items
                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                    out of
                    <%# Container.ItemsCount %>.
                </FooterTemplate>
            </cc2:ComboBox>
        </td>
        <td align="right">
            Item Category:
        </td>
        <td>
            <asp:DropDownList ID="ddlItemCate" runat="server" CssClass="gCtrTxt " Font-Size="9"
                Width="160px">
            </asp:DropDownList>
        </td>
        <td align="right">
            Item Type:
        </td>
        <td>
            <asp:DropDownList ID="ddlItemType" runat="server" CssClass="gCtrTxt " Font-Size="9"
                Width="160px">
            </asp:DropDownList>
        </td>
        <td align="right">
            Party:
        </td>
        <td>
            <asp:DropDownList ID="ddlpartycode" runat="server" DataTextField="PRTY_NAME" DataValueField="PRTY_CODE"
                Width="160px" AutoPostBack="True" CssClass="tContentArial">
            </asp:DropDownList>
        </td>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td align="right">
                <%-- Department:--%>
            </td>
            <td>
                <asp:Button ID="btnGetReport" runat="server" Text="Get Report" OnClick="btnGetReport_Click"  CssClass="AButton" />
                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="gCtrTxt " Font-Size="9"
                    Width="160px" Visible="False">
                </asp:DropDownList>
            </td>
        </tr>
    </tr>
    <tr>
        <td class="TdBackVir" colspan="8">
            <b>Total Records : &nbsp;&nbsp;</b><asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="td tContentArial" colspan="8">
            <asp:Panel ID="pnlShowHover" runat="server" Width="1100px" ScrollBars="Auto" Height="350px">
                <asp:GridView ID="GridLedger" runat="server" AutoGenerateColumns="False" Width="200%"
                    CellPadding="4" ForeColor="#333333" HeaderStyle-Wrap="true"
                    Font-Size="X-Small" AllowPaging="True" OnPageIndexChanging="GridLedger_PageIndexChanging"
                    PageSize="13" AllowSorting="True" 
                    OnSelectedIndexChanged="GridLedger_SelectedIndexChanged">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" 
                        Width="100%" />
                    <RowStyle BackColor="#EFF3FB" />
                    <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                    <Columns>
                        <asp:BoundField DataField="YEAR" HeaderText="YEAR" />
                        <asp:BoundField DataField="TRN_TYPE" HeaderText="TRN TYPE" />
                        <asp:BoundField DataField="TRN_DESC" HeaderText="TRN DESC" />
                        <asp:BoundField DataField="MRN" HeaderText="MRN" />
                        <asp:BoundField DataField="MRN_DATE" HeaderText="MRN DATE" />
                        <asp:BoundField DataField="ITEM_CODE" HeaderText="ITEM CODE" />
                        <asp:BoundField DataField="ITEM_DESC" HeaderText="ITEM DESC" />
                        <asp:BoundField DataField="UOM" HeaderText="UOM" />
                        <asp:BoundField DataField="MRN_QTY" HeaderText="MRN QTY" ItemStyle-HorizontalAlign="Right"
                            HeaderStyle-HorizontalAlign="Right" >
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FINAL_RATE" HeaderText="FINAL RATE" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:0.00}">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PO_NUMB" HeaderText="PO NUMB" />
                        <asp:BoundField DataField="AMOUNT" HeaderText="AMOUNT" ItemStyle-HorizontalAlign="Right"
                            HeaderStyle-HorizontalAlign="Right" >
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="COST_CENTER_CODE" HeaderText="COST CENTER CODE" />
                        <asp:BoundField DataField="GATE_DATE" HeaderText="GATE DATE" 
                            DataFormatString="{0:dd-MM-yyyy}" />
                        <asp:BoundField DataField="PRTY_CODE" HeaderText="PARTY CODE" />
                        <asp:BoundField DataField="PRTY_NAME" HeaderText="PARTY NAME" />
                        <asp:BoundField DataField="TRNSPRT_CODE" HeaderText="TRNSPRT CODE" />
                        <asp:BoundField DataField="LORY_NUMB" HeaderText="LORY NUMB" />
                        <asp:BoundField DataField="PRTY_CH_NUMB" HeaderText="PRTY CH NUMB" />
                    </Columns>
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" 
                        ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                </asp:GridView>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="8">
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TxtFromDate"
                PopupPosition="TopLeft" Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TxtToDate"
                PopupPosition="TopLeft" Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
        </td>
    </tr>
</table>
