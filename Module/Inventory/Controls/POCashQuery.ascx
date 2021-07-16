<%@ Control Language="C#" AutoEventWireup="true" CodeFile="POCashQuery.ascx.cs" Inherits="Module_Inventory_Controls_POCashQuery" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc1" %>
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;}
    .header
    {
        margin-left: 4px;
    }
    .c1
    {
        width: 50px;
    }
    .c2
    {
        margin-left: 4px;
        width: 90px;
    }
    .c3
    {
        margin-left: 4px;
        width: 80px;
    }
    .tdText
    {
        font: 11px Verdana;
        color: #333333;
    }
    .option2
    {
        font: 11px Verdana;
        color: #0033cc;
        padding-left: 4px;
        padding-right: 4px;
    }
    a
    {
        font: 11px Verdana;
        color: #315686;
        text-decoration: underline;
    }
    a:hover
    {
        color: Teal;
    }
    .ob_gMCont_DT
    {
        overflow: hidden;
    }
    .ob_gMCont
    {
        position: relative;
    }
</style>
<table class="tContentArial">
    <tr>
        <td class="td">
            <table>
                <td>
                    <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                        Width="48" Height="41"></asp:ImageButton>
                </td>
                <td>
                    <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                        Width="48" Height="41" OnClick="imgbtnExit_Click"></asp:ImageButton>
                </td>
                <td>
                    <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                        Width="48" Height="41"></asp:ImageButton>
                </td>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" valign="top" class="tRowColorAdmin td">
            <span class="titleheading">Purchase Order Query</span>
        </td>
    </tr>
    <tr>
        <td class="td">
            <table>
                <td align="left">
                <cc2:Grid ID="Grid1" runat="server" AllowPaging="False"   AllowFiltering="True"
                AllowAddingRecords="False" AllowColumnResizing="False" OnSelect="Grid1_Select"
                AllowMultiRecordSelection="False" AllowPageSizeSelection="False" 
                AllowSorting="False" AutoGenerateColumns="False" GenerateRecordIds="True" 
                AllowRecordSelection="False" PageSize="50"  AutoPostBackOnSelect="True"  Height = " 350px">
                  
                    <PagingSettings PageSizeSelectorPosition="TopAndBottom" />
                    <MasterDetailSettings ShowEmptyDetails="True" />
                  
                    <ScrollingSettings ScrollWidth="930px" ScrollHeight="300px" />

                
                   <%-- <cc2:Grid ID="Grid1" runat="server" AllowAddingRecords="False" AllowFiltering="True"
                        AllowPaging="true" PageSize="5" AutoGenerateColumns="False" OnSelect="Grid1_Select"
                        AutoPostBackOnSelect="True">--%>
                        <Columns>
                            <cc2:Column DataField="YEAR" HeaderText="Year" Visible="false" >
                            </cc2:Column>
                            <cc2:Column DataField="COMP_CODE" HeaderText="Company" Visible="false" >
                            </cc2:Column>
                            <cc2:Column DataField="BRANCH_CODE" HeaderText="Branch" Visible="false" >
                            </cc2:Column>
                            <cc2:Column DataField="COMP_NAME" HeaderText="Company" Visible="false" >
                            </cc2:Column>
                            <cc2:Column DataField="BRANCH_NAME" HeaderText="Branch" Visible="false" >
                            </cc2:Column>
                            <cc2:Column DataField="PO_NUMB" HeaderText="PO No" >
                            </cc2:Column>
                            <cc2:Column DataField="PO_TYPE" HeaderText="PO Type" >
                            </cc2:Column>
                            <cc2:Column DataField="PO_DATE" HeaderText="PO Date" >
                            </cc2:Column>
                            <cc2:Column DataField="PRTY_NAME" HeaderText="Party Name" Visible="false" >
                            </cc2:Column>
                            <cc2:Column DataField="DEL_DATE" HeaderText="Delivery Date" >
                            </cc2:Column>
                            <cc2:Column DataField="PAY_TERM" HeaderText="Pay Term" >
                            </cc2:Column>
                            <cc2:Column DataField="CONF_FLAG" HeaderText="Confirmation Flag" >
                            </cc2:Column>
                            <cc2:Column DataField="COMMENTS" HeaderText="Comments" Visible="false" >
                            </cc2:Column>
                            <cc2:Column DataField="DELV_BRANCH" HeaderText="Delivery Branch" >
                            </cc2:Column>
                            <cc2:Column DataField="TRSP" HeaderText="Transporter" >
                            </cc2:Column>
                            <cc2:Column DataField="DESP_MODE" HeaderText="despatch mode" >
                            </cc2:Column>
                            <cc2:Column DataField="INSU_FLAG" HeaderText="Item Status" Visible="false" >
                            </cc2:Column>
                            <cc2:Column DataField="AUTH" HeaderText="Authorised" >
                            </cc2:Column>
                            <cc2:Column DataField="AUTH_BY" HeaderText="Authorised By" >
                            </cc2:Column>
                            <cc2:Column DataField="AUTH_DATE" HeaderText="Asociate Date" >
                            </cc2:Column>
                            <cc2:Column DataField="ADV_PRCNT" HeaderText="Advance Percent" >
                            </cc2:Column>
                            <cc2:Column DataField="FINAL_AMT" HeaderText="Final Amount" >
                            </cc2:Column>
                            <cc2:Column DataField="CH_CASH" HeaderText="Cash" >
                            </cc2:Column>
                        </Columns>
                        <PagingSettings Position="Bottom" />
                        <FilteringSettings InitialState="Visible" FilterPosition="Top" FilterLinksPosition="Top" />
                    </cc2:Grid>
                    <cc2:Grid ID="Grid2" runat="server" AllowAddingRecords="False" AllowFiltering="True"
                        AllowPaging="true" PageSize="5" AutoGenerateColumns="False" AutoPostBackOnSelect="True"
                        OnSelect="Grid2_Select">
                        <Columns>
                            <cc2:Column DataField="YEAR" HeaderText="Year" Visible="false" >
                            </cc2:Column>
                            <cc2:Column DataField="COMP_CODE" HeaderText="Company" Visible="false" >
                            </cc2:Column>
                            <cc2:Column DataField="BRANCH_CODE" HeaderText="Branch" Visible="false" >
                            </cc2:Column>
                            <cc2:Column DataField="PO_NUMB" HeaderText="PO No" Visible="false" >
                            </cc2:Column>
                            <cc2:Column DataField="PO_TYPE" HeaderText="PO Type" Visible="false" >
                            </cc2:Column>
                            <cc2:Column DataField="ITEM_CODE" HeaderText="Item Code" Visible="false" >
                            </cc2:Column>
                            <cc2:Column DataField="ITEM_DESC" HeaderText="Item" >
                            </cc2:Column>
                            <cc2:Column DataField="UOM" HeaderText="UOM" >
                            </cc2:Column>
                            <cc2:Column DataField="ORD_QTY" HeaderText="Qty.Ordered" >
                            </cc2:Column>
                            <cc2:Column DataField="QTY_RCPT" HeaderText="Qty.Rec" >
                            </cc2:Column>
                            <cc2:Column DataField="QTY_RTN" HeaderText="Qty.Rtn" >
                            </cc2:Column>
                            <cc2:Column DataField="BASIC_RATE" HeaderText="B.Rate" >
                            </cc2:Column>
                            <cc2:Column DataField="FINAL_RATE" HeaderText="F.Rate" >
                            </cc2:Column>
                            <cc2:Column DataField="CURR_CODE" HeaderText="Currency" >
                            </cc2:Column>
                            <cc2:Column DataField="DEL_DATE" HeaderText="Delivery Date" >
                            </cc2:Column>
                            <cc2:Column DataField="COMMENTS" HeaderText="Comments" >
                            </cc2:Column>
                            <cc2:Column DataField="STAT_FLAG" HeaderText="Catgory" >
                            </cc2:Column>
                        </Columns>
                        <PagingSettings Position="Bottom" />
                        <FilteringSettings InitialState="Visible" FilterPosition="Top" FilterLinksPosition="Top" />
                    </cc2:Grid>
                </td>
            </table>
        </td>
    </tr>
</table>
