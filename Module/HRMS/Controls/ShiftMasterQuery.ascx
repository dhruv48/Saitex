<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShiftMasterQuery.ascx.cs" Inherits="Module_HRMS_Controls_ShiftMasterQuery" %>
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
                        Width="48" Height="41" onclick="imgbtnExit_Click"></asp:ImageButton>
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
            <span class="titleheading">Shift Master Query</span>
        </td>
    </tr>
        <tr>
        <td class="td">
            <table>
                <td align="left">
                    <cc2:Grid ID="Grid1" runat="server" AllowAddingRecords="False" AllowFiltering="True"
                        PageSize="5" AutoGenerateColumns="False"  AutoPostBackOnSelect="True">
                        <Columns>
                            <cc2:Column DataField="SFT_ID" HeaderText="SFT_ID" Visible="false" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="SFT_NAME" HeaderText="Shift Name" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="SFT_IN_TIME" HeaderText="In Time" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="SFT_OUT_TIME" HeaderText="Out Time" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="SFT_RLX_TIME" HeaderText="Relax Time" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="SFT_LNCH_TIME" HeaderText="Lunch Time" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="SFT_OVR_TIME" HeaderText="Over Time" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="SFT_MIN_WRK_HOUR" HeaderText="Min Working Hour" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="SFT_MIN_HLD_HOUR" HeaderText="Min Holiday Hour" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="SFT_MIN_SHORT_DAY_HOUR" HeaderText="MinShortDayHour" Width="100px">
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