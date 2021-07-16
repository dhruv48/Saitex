<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HolidayQuery.ascx.cs" Inherits="Module_HRMS_Controls_HolidayQuery" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>

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
            <span class="titleheading">Holiday Query</span>
        </td>
    </tr>
      <tr>
        <td class="td">
            <table>
                <td align="left">
             <cc2:Grid ID="Grid1" runat="server" AllowAddingRecords="False" AllowFiltering="True" AllowPaging="true"
                PageSize="5" AutoGenerateColumns="False" AutoPostBackOnSelect="True" >
                <Columns>
                    <cc2:Column DataField="HLD_ID" HeaderText="HolidayId" Visible="false" width="100px">
                    </cc2:Column>
                    <cc2:Column DataField="HLD_NAME" HeaderText="Holiday Name" width="150px">
                    </cc2:Column>
                    <cc2:Column DataField="YEAR" HeaderText="Year" width="100px">
                    </cc2:Column>
                    <cc2:Column DataField="HLD_DATE" HeaderText="Holiday Date" width="150px">
                    </cc2:Column>
                     <cc2:Column DataField="OPT_LV" HeaderText="Optional Leave" width="100px">
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