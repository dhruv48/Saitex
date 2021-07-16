<%@ Control Language="C#" AutoEventWireup="true" CodeFile="YearlyLeaveQuery.ascx.cs" Inherits="Module_HRMS_Controls_YearlyLeaveQuery" %>
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
                    <td >
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                            Width="48" Height="41" ></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                            Width="48" Height="41" onclick="imgbtnExit_Click"  ></asp:ImageButton>
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
            <span class="titleheading">Yearly Leave Query</span>
        </td>
    </tr>
      <tr>
        <td class="td">
            <table>
                <td align="left">
             <cc2:Grid ID="Grid1" runat="server" AllowAddingRecords="False" AllowFiltering="True" AllowPaging="true"
                PageSize="5" AutoGenerateColumns="False" AutoPostBackOnSelect="True" >
                <Columns>
                    <cc2:Column DataField="LV_ID" HeaderText="LeaveId" Visible="false" width="100px">
                    </cc2:Column>
                    <cc2:Column DataField="LV_NAME" HeaderText="Leave Name" width="100px">
                    </cc2:Column>
                    <cc2:Column DataField="LV_DAY" HeaderText="No of Days" width="100px">
                    </cc2:Column>
                    <cc2:Column DataField="YEAR" HeaderText="Year" width="100px">
                    </cc2:Column>
                    <cc2:Column DataField="REMARKS" HeaderText="Remarks" width="150px">
                    </cc2:Column>
                     <cc2:Column DataField="LV_FRWD" HeaderText="Carring Forward" width="70px">
                    </cc2:Column>
                    <cc2:Column DataField="LV_PRD_TYPE" HeaderText="Period Type" Visible="false" width="100px">
                    </cc2:Column>
                    </Columns>
                   <PagingSettings Position="Bottom"/>
                            <FilteringSettings InitialState="Visible" FilterPosition="Top" FilterLinksPosition="Top" />
                    </cc2:Grid>    
                    </td>
            </table>
        </td>
    </tr>
</table>