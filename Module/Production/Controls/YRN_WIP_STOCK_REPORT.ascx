<%@ Control Language="C#" AutoEventWireup="true" CodeFile="YRN_WIP_STOCK_REPORT.ascx.cs" Inherits="Module_Production_Controls_YRN_WIP_STOCK_REPORT" %>
<table class="td tContentArial" width = "950px">
<tr>
        <td align="Right" class="td" colspan = "6">
            <table align="left">
                <tr>               
                    <td id="tdPrint" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" Width="48" onclick="imgbtnPrint_Click"  />
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
            <span class="titleheading">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; WORK IN PROGRESS  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
    <tr>
        <td align="left" valign="top" class="td" colspan="6">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>
 
 <tr>
 <td align = "right" >Order No. :</td><td>
            <asp:DropDownList ID="ddlOrderNo" runat="server" DataTextField="PI_NO" 
                DataValueField="PI_NO" 
                Width="128px" AutoPostBack="True" CssClass="tContentArial" 
                onselectedindexchanged="ddlOrderNo_SelectedIndexChanged">
            </asp:DropDownList>
            </td><td>&nbsp;</td><td align = "right">DepartMent :</td><td>
        <asp:DropDownList ID="ddlDepartMent" runat="server" DataTextField="DEPT_NAME" 
            DataValueField="DEPT_CODE" 
            onselectedindexchanged="ddlDepartMent_SelectedIndexChanged" Width="128px" 
                AutoPostBack="True" CssClass="tContentArial">
        </asp:DropDownList>
        </td><td></td>
  </tr>
  <tr>
 <td align = "right">Branch. :</td><td>
            <asp:DropDownList ID="ddlbranch" runat="server" 
                onselectedindexchanged="ddlbranch_SelectedIndexChanged" 
                DataTextField="BRANCH_NAME" DataValueField="BRANCH_CODE" Width="128px" 
                AutoPostBack="True" CssClass="tContentArial">
            </asp:DropDownList>
            </td><td></td ><td align = "right">Process No.:</td><td>
            <asp:DropDownList ID="ddlprocessno" runat="server" DataTextField="PROS_DESC" 
                DataValueField="PROS_CODE" 
                onselectedindexchanged="ddlprocessno_SelectedIndexChanged" Width="128px" 
                AutoPostBack="True" CssClass="tContentArial">
            </asp:DropDownList>
            </td><td></td>
 </tr>
 <tr>
             <td colspan="8" width="100%" class="TdBackVir">
                            <b>Total Records : &nbsp;&nbsp;</b><asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
                        </td></tr>
  <table>
   <td colspan="5"  class = "td tContentArial">
   
    <asp:Panel ID="pnlShowHover" runat="server" Width="950px" 
                                 ScrollBars="Auto">
        <asp:GridView ID="GridYRN_STOCK" runat="server" 
    AutoGenerateColumns="False" AllowPaging="True"
                 Width="300%" CellPadding="4" ForeColor="#333333" 
                GridLines="None"  HeaderStyle-Wrap = "true"  Font-Size ="10px" 
            onpageindexchanging="GridYRN_STOCK_PageIndexChanging" PageSize="15" >
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:BoundField DataField="ARTICLE_CODE" HeaderText="ARTICLE CODE" />
                <asp:BoundField DataField="DYED_LOT_NO" HeaderText="DYED LOT NO" />
                <asp:BoundField DataField="ORDER_NO" HeaderText="ORDER NO" />
                <asp:BoundField DataField="LOT_NUMBER" HeaderText="LOT NUMBER" />
                <asp:BoundField DataField="PROS_CODE" HeaderText="PROS CODE" />
                <asp:BoundField DataField="TRN_DATE" HeaderText="TRN DATE" 
                     DataFormatString = {0:MM-dd-yyyy} />
                <asp:BoundField DataField="BATCH_NO" HeaderText="BATCH NO" />
                <asp:BoundField DataField="BIN_LOCT" HeaderText="BIN LOCT" />
                <asp:BoundField DataField="LOT_QTY" HeaderText="LOT QTY"  ItemStyle-HorizontalAlign = "Right" DataFormatString="{0:0.00}" />
                <asp:BoundField DataField="STOCK_QTY" HeaderText="STOCK QTY" ItemStyle-HorizontalAlign = "Right" DataFormatString="{0:0.00}" />
                <asp:BoundField DataField="LENGTH" HeaderText="LENGTH" />
                <asp:BoundField DataField="UN_CONF_QTY" HeaderText="UN CONF QTY" ItemStyle-HorizontalAlign = "Right" DataFormatString="{0:0.00}" />
                <asp:BoundField DataField="FR_DEPT_CODE" HeaderText="FR DEPT CODE" />
                <asp:BoundField DataField="TO_DEPT_CODE" HeaderText="TO DEPT CODE" />
                <asp:BoundField DataField="PROS_DATE" HeaderText="PROS DATE" DataFormatString = {0:MM-dd-yyyy} />
                <asp:BoundField DataField="PROS_DESC" HeaderText="PROS DESC" />
                <asp:BoundField DataField="PROS_ROOT" HeaderText="PROS ROOT" />
                <asp:BoundField DataField="QTY_PACK" HeaderText="QTY PACK" />
                <asp:BoundField DataField="STATUS" HeaderText="STATUS" />
                <asp:BoundField DataField="STATUS_DATE" HeaderText="STATUS DATE" DataFormatString = {0:MM-dd-yyyy}/>
                <asp:BoundField DataField="STATUS_USER" HeaderText="STATUS USER" />
                <asp:BoundField DataField="BATCHCARD_NO" HeaderText="BATCHCARD NO" />
                <asp:BoundField DataField="REMARKS" HeaderText="REMARKS" />
                <asp:BoundField DataField="TDATE" HeaderText="TDATE" />
                <asp:BoundField DataField="TUSER" HeaderText="TUSER" />
                <asp:BoundField DataField="DIFF_STOCK" HeaderText="DIFF STOCK" />
                <asp:BoundField DataField="LOT_STD_GLM" HeaderText="LOT STD GLM" />
                <asp:BoundField DataField="WIP_SEQ_NO" HeaderText="WIP SEQ NO" />
                <asp:BoundField DataField="NO_OF_UNIT" HeaderText="NO OF UNIT" ItemStyle-HorizontalAlign = "Right" DataFormatString="{0:0.00}"  />
                <asp:BoundField DataField="UOM_OF_UNIT" HeaderText="UOM OF UNIT" ItemStyle-HorizontalAlign = "Right" DataFormatString="{0:0.00}"  />
                <asp:BoundField DataField="WEIGHT_OF_UNIT" HeaderText="WEIGHT OF UNIT" ItemStyle-HorizontalAlign = "Right" DataFormatString="{0:0.00}"  />
                <asp:BoundField DataField="COMP_NAME" HeaderText="COMPANY " />
                <asp:BoundField DataField="BRANCH_NAME" HeaderText="BRANCH" />
                <asp:BoundField DataField="DEPT_NAME" HeaderText="DEPARTMENT" />
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
 
</tr>
 </table>
 