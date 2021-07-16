<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProcessRouteQuery.ascx.cs" Inherits="Module_Machine_Controls_ProcessRouteQuery" %>
<table class="td tContentArial" width = "950px">
<tr>
        <td align = "left"  class="td" colspan = "6">
            <table align="left">
                <tr>               
                    <td id="tdPrint" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" Width="48" onclick="imgbtnPrint_Click1"  />
                    </td>
                    <td id="tdExit" runat="server">
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                            Width="48" Height="41" onclick="imgbtnExit_Click"  ></asp:ImageButton>
                    </td>
                    <td id="tdHelp" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                            ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                    </td>
                </tr>
            </table>
                        
        </td>
    </tr>
<tr>
        <td class="TableHeader td" align="center" colspan = "6">
            <span class="titleheading">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Process Route Query Form &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
<tr>
        <td align="left" valign="top" class="td" colspan="6">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>
 <tr>
 <td align = "right" >Process:</td>
 <td>
            <asp:DropDownList ID="ddlProcess" runat="server" DataTextField="PROCESS" 
                DataValueField="PROCESS" 
                Width="128px" AutoPostBack="True" CssClass="tContentArial" 
                onselectedindexchanged="ddlProcess_SelectedIndexChanged" > 
            </asp:DropDownList>
            </td>
            <td style="text-align: right">
                                Process Code.:</td><td>
          <asp:DropDownList ID="ddlprocessno" runat="server" DataTextField="PROS_DESC" 
                DataValueField="PROS_CODE" 
                Width="128px" 
                AutoPostBack="True" CssClass="tContentArial" 
                onselectedindexchanged="ddlprocessno_SelectedIndexChanged">
            </asp:DropDownList>
            </td>
            
            <td style="text-align: right">Process Route Code :</td><td align = "right" 
                style="text-align: left">
        <asp:DropDownList ID="ddlProsRoute" runat="server" DataTextField="PROS_ROUTE_CODE" 
            DataValueField="PROS_ROUTE_CODE" 
             Width="128px" 
                AutoPostBack="True" CssClass="tContentArial" 
                onselectedindexchanged="ddlProsRoute_SelectedIndexChanged">
             
        </asp:DropDownList>
            </td>
  </tr>
  <tr>
             <td colspan="8" width="100%" class="TdBackVir">
                            <b>Total Records : &nbsp;&nbsp;</b><asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
                        </td></tr>
   <tr>
   <td colspan="5"  class = "td tContentArial">
   
   
  <table>
   <td colspan="5"  class = "td tContentArial">
   
  <asp:Panel ID="pnlShowHover" runat="server" Width="950px" Height="350px"
                               ScrollBars="Auto">
      <asp:GridView ID="GridProcess" runat="server" 
     AutoGenerateColumns="False" 
                 Width="150%"   CellPadding="4" ForeColor="#333333" 
                GridLines="None" PageSize = "15"  HeaderStyle-Wrap = "true" Font-Size="X-Small" 
          AllowPaging="True" onpageindexchanging="GridProcess_PageIndexChanging"  >
          <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
          <RowStyle BackColor="#EFF3FB" />
          <Columns>
             <asp:BoundField DataField="S_NO" HeaderText="S NO" />
              <asp:BoundField DataField="YEAR" HeaderText="YEAR " />
              <asp:BoundField DataField="PROS_ROUTE_CODE" HeaderText="PROS ROUTE_CODE" />
              <asp:BoundField DataField="MAIN_PROCESS" HeaderText="PROCESS" />
              <asp:BoundField DataField="MAC_CODE" HeaderText="MAC CODE"  />
              <asp:BoundField DataField="PROS_CODE" HeaderText="PROS CODE "  />
              <asp:BoundField DataField="PROS_DESC" HeaderText="PROS DESC" />
              <asp:BoundField DataField="GROUND" HeaderText="GROUND" />
              <asp:BoundField DataField="COMP_NAME" HeaderText="COMPANY "  Visible = "false" />
              <asp:BoundField DataField="BRANCH_NAME" HeaderText="BRANCH" Visible = "false" />
              <asp:BoundField DataField="PRINT_STYLE" HeaderText="PRINT STYLE" />
              <asp:BoundField DataField="TUSER" HeaderText="TUSER" />
              <asp:BoundField DataField="TDATE" HeaderText="TDATE" DataFormatString = {0:dd-MM-yyyy} />                  
              <asp:BoundField DataField="TEST_CODE" HeaderText="TEST CODE" />
              <asp:BoundField DataField="REMARKS" HeaderText="REMARKS" />
          </Columns>
          <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
          <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
          <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
          <EditRowStyle BackColor="#2461BF" />
          <AlternatingRowStyle BackColor="White" />
      </asp:GridView>
                                   
   </asp:Panel>
        </td>
 </table>
 </td>
 </tr>

 </table>