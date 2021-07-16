<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HeadMasterQuery.ascx.cs"
    Inherits="Module_HRMS_Controls_HeadMasterQuery" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<style type="text/css">
    .ob_gMCont_DT
    {
        overflow: hidden;
    }
    .ob_gMCont
    {
        position: relative;
    }
    .ob_gPSTT
    {
        display: none !important;
    }
</style>



<table class="tContentArial">
    <tr>
        <td class="td">
            <table>
                <tr>
                    <td>
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                        Width="48" Height="41" >
                    </asp:ImageButton>
                       
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                            Width="48" Height="41" onclick="imgbtnExit_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                            Width="48" Height="41"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" valign="top" class="tRowColorAdmin td">
            <span class="titleheading">Head Master Query</span>
        </td>
    </tr>
    <tr>
        <td class="td">
           <%-- <div id="divPrint">--%>
                <cc2:Grid ID="Grid1" runat="server" AllowAddingRecords="False" AllowFiltering="True"
                    AllowPaging="true" PageSize="5" AutoGenerateColumns="False" FolderStyle="~/StyleSheet/black_glass">
                    <Columns>
                        <cc2:Column DataField="HEAD_ID" HeaderText="HeadId" Width="100px" Align="center">
                        </cc2:Column>
                        <cc2:Column DataField="HEAD_NAME" HeaderText="Head Name" Width="200px">
                        </cc2:Column>
                    </Columns>
                    <PagingSettings Position="Bottom" />
                    <FilteringSettings InitialState="Hidden" FilterPosition="Top" FilterLinksPosition="Top" />
                </cc2:Grid>
            <%--</div>--%>
        </td>
    </tr>
</table>
