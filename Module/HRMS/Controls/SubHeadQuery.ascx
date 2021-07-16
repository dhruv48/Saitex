<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SubHeadQuery.ascx.cs" Inherits="Module_HRMS_Controls_SubHeadQuery" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc3" %>
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
                <td id="tdPrint" runat="server"  align="left">
                    <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                        Width="48" Height="41" TabIndex="9" /></asp:ImageButton>
                </td>
                <td id="tdExit" runat="server"  align="left">
                    <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                        Width="48" Height="41" TabIndex="10" onclick="imgbtnExit_Click" ></asp:ImageButton>
                </td>
                <td id="tdHelp" runat="server"  align="left">
                    <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                        Width="48" Height="41" TabIndex="11"></asp:ImageButton>
                </td>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" valign="top" class="tRowColorAdmin td">
            <span class="titleheading">Sub Head Master Query</span>
        </td>
    </tr>
       <tr>
        <td class="td">
            <table>
                <td align="left">
                    <cc2:Grid ID="Grid1" runat="server" AllowAddingRecords="False" AllowFiltering="True" AllowPaging="true"
                        PageSize="5" AutoGenerateColumns="False"  AutoPostBackOnSelect="false">
                        <Columns>
                            <cc2:Column DataField="SUBH_ID" HeaderText="SubHeadId" Visible="false" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="HEAD_ID" HeaderText="HeadName" Visible="false" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="HEAD_NAME" HeaderText="HeadName" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="SUBH_NAME" HeaderText="Sub Head Name" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="Headcat" HeaderText="Sub Head Catagory" Width="150px">
                            </cc2:Column>
                            <cc2:Column DataField="subHeadCat" HeaderText="Salary Type" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="RupeesPercent" HeaderText="Sub Head Type" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="SUBH_SLIP_FLD_NAME" HeaderText="Slip Field Name" Width="100px">
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