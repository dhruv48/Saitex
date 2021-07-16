<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OutDoorDutyQuery.ascx.cs" Inherits="Module_HRMS_Controls_OutDoorDutyQuery" %>
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
               <%-- <td>
                    <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                        Width="48" Height="41" ></asp:ImageButton>
                </td>--%>
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
            <span class="titleheading">OutDoor Duty Query</span>
        </td>
    </tr>
     <tr>
        <td class="td">
            <table>
                <td align="left">
                    <cc2:Grid ID="Grid1" runat="server" AllowAddingRecords="False" AllowFiltering="True" AllowPaging="true"
                        PageSize="5" AutoGenerateColumns="False" >
                        <Columns>
                            <cc2:Column DataField="OD_ID" HeaderText="OD ID" Visible="false" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="DEPARTMENT" HeaderText="Department" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="SFT_NAME" HeaderText="Shift Name" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="APPLIEDDATE" HeaderText="Applid Date" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="EMPNAME" HeaderText="Employee Name" Width="150px">
                            </cc2:Column>
                            <cc2:Column DataField="FROM_DATE" HeaderText="Duty From" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="TO_DATE" HeaderText="Duty To" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="ON_FROM" HeaderText="From" Width="100px">
                            </cc2:Column>
                             <cc2:Column DataField="ON_TO" HeaderText="To" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="DAYS" HeaderText="Days" Width="80px">
                            </cc2:Column>
                            <cc2:Column DataField="PLACE" HeaderText="Place" Width="150px">
                            </cc2:Column>
                           <%-- <cc2:Column DataField="LV_ID" HeaderText="LeaveId" Visible="false" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="FROM_DATE" HeaderText="Leave Name" Width="100px">
                            </cc2:Column>--%>
                            <cc2:Column DataField="OD_STATUS" HeaderText="Status" Width="100px">
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
