<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BankMasterQuery1.ascx.cs" Inherits="Module_HRMS_Controls_BankMasterQuery1" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
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
<table class="tContentArial td">
    <tr>
        <td id="Td1" align="left"  runat="server" class="td">
            <table >
                <tr>
                    <td id="tdPrint" runat="server">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                            Width="48" Height="41" TabIndex="9" ></asp:ImageButton>
                    </td>
                    <td id="tdExit" runat="server">
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                            Width="48" Height="41" TabIndex="10" onclick="imgbtnExit_Click" ></asp:ImageButton>
                    </td>
                    <td id="tdHelp" runat="server">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                            Width="48" Height="41" TabIndex="11"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center"  valign="top" class="tRowColorAdmin td">
            <span class="titleheading">Bank Master Query</span>
        </td>
    </tr>
      <tr>
        <td align="left" valign="top" class="td">
            <cc2:Grid ID="Grid1" runat="server" AllowAddingRecords="False" AllowFiltering="True" AllowPaging="true"
                PageSize="5" AutoGenerateColumns="False" >
                <Columns>
                    <cc2:Column DataField="BANK_CODE" HeaderText="BANK CODE" Width="100px">
                    </cc2:Column>
                    <cc2:Column DataField="BANK_NAME" HeaderText="BANK NAME" Width="100px">
                    </cc2:Column>
                    <cc2:Column DataField="BANK_REMARKS" HeaderText="Remarks" Width="180px">
                    </cc2:Column>
                </Columns>
                 <PagingSettings Position="Bottom"/>
                            <FilteringSettings InitialState="Visible" FilterPosition="Top" FilterLinksPosition="Top" />
                    </cc2:Grid>
             
        </td>
    </tr>
</table>