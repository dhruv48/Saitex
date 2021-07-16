<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LeaveMappingQuery.ascx.cs" Inherits="Module_HRMS_Controls_LeaveMappingQuery" %>
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
                            Width="48" Height="41" onclick="imgbtnExit_Click" ></asp:ImageButton>
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
            <span class="titleheading">Leave Mapping Query</span>
        </td>
    </tr>
   <tr>
        <td class="td">
            <table>
                <td align="left">
            <cc2:Grid ID="Grid1" runat="server" AllowAddingRecords="False" AllowFiltering="True" AllowPaging="true"
                PageSize="5" AutoGenerateColumns="False" AutoPostBackOnSelect="True">
                <Columns>
                    <cc2:Column DataField="LV_MPP_ID" HeaderText="LeaveMappId" Visible="false" width="100px">
                    </cc2:Column>
                      <cc2:Column DataField="LV_ID_1" HeaderText="LeaveId" Visible="false" width="160px">
                    </cc2:Column>
                    <cc2:Column DataField="LV_ID_2" HeaderText="Mapped LeaveId" Visible="false" width="160px">
                    </cc2:Column>
                    <cc2:Column DataField="LV_NAME1" HeaderText="Leave" width="160px">
                    </cc2:Column>
                    <cc2:Column DataField="LV_NAME2" HeaderText="Mapped Leave" width="160px">
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