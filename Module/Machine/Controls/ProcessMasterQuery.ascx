<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProcessMasterQuery.ascx.cs" Inherits="Module_Machine_Controls_ProcessMasterQuery" %>
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
            <span class="titleheading">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Process Master Query Form&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
<tr>
        <td align="left" valign="top" class="td" colspan="6">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
                                   
        </td>
    </tr>
 <tr>
 <td align = "right"  class = "tContentArial">Machin Group Code:</td>
 <td>
            <asp:DropDownList ID="ddlmaccode" runat="server" DataTextField="MAC_CODE" 
                DataValueField="MAC_CODE" 
                Width="128px" AutoPostBack="True" CssClass="tContentArial" onselectedindexchanged="ddlmaccode_SelectedIndexChanged" 
                > 
            </asp:DropDownList>
            </td><td>&nbsp;</td><td align = "right" class = "tContentArial">Department :</td><td>
        <asp:DropDownList ID="ddldepartment" runat="server" DataTextField="DEPT_CODE" 
            DataValueField="DEPT_NAME" 
             Width="128px" 
                AutoPostBack="True" CssClass="tContentArial" 
            onselectedindexchanged="ddldepartment_SelectedIndexChanged">
        </asp:DropDownList>
        </td><td></td>
  </tr>
<tr>
 <td align = "right" class = "tContentArial">Process Code.:</td><td>
          <asp:DropDownList ID="ddlprocessno" runat="server" DataTextField="PROS_DESC" 
                DataValueField="PROS_CODE" 
                Width="128px" 
                AutoPostBack="True" CssClass="tContentArial" 
                onselectedindexchanged="ddlprocessno_SelectedIndexChanged">
            </asp:DropDownList>
            </td><td></td ><td align = "right" class = "tContentArial">Main Process :</td>  <td>
        <asp:DropDownList ID="ddlmainprocess" runat="server" DataTextField="MAIN_PROCESS" 
            DataValueField="MAIN_PROCESS" 
             Width="128px" 
                AutoPostBack="True" CssClass="tContentArial" 
                onselectedindexchanged="ddlmainprocess_SelectedIndexChanged"   >
              
         
        </asp:DropDownList>
            </td><td></td>
 </tr>
 <tr>
 <td colspan="5"  class = "td tContentArial">
 
  <table>
  <tr>
   <td colspan="5"  class = "td tContentArial">
   <asp:Panel ID="pnlShowHover" runat="server" Width="950px" BackColor="Beige" BorderWidth="2px"
                                Height="420px" ScrollBars="Auto">
       <asp:GridView ID="GridProcessMaster" runat="server" 
     AutoGenerateColumns="False" AllowPaging= "True"  
                 Width="100%" CellPadding="4" ForeColor="#333333" 
                GridLines="None"  HeaderStyle-Wrap = "true" Font-Size="X-Small" 
           onpageindexchanging="GridProcessMaster_PageIndexChanging" PageSize="5"  >
           <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
           <RowStyle BackColor="#EFF3FB" />
           <Columns>
               <asp:BoundField DataField="PROS_CODE" HeaderText="PROS CODE " />
               <asp:BoundField DataField="PROS_DESC" HeaderText="PROS DESC" />
               <asp:BoundField DataField="MAC_CODE" HeaderText="MAC CODE" />
               <asp:BoundField DataField="MAIN_PROCESS" HeaderText="MAIN PROCESS"  />
               <asp:BoundField DataField="FABR_TYPE" HeaderText="FABR TYPE" />
               <asp:BoundField DataField="DEPT_NAME" HeaderText="DEPARTMENT" />
               <asp:BoundField DataField="SPEED" HeaderText="SPEED"  />
               <asp:BoundField DataField="TEMP" HeaderText="TEMP" />
               <asp:BoundField DataField="REMARKS" HeaderText="REMARKS "   />
               <asp:BoundField DataField="SETTIME" HeaderText="SETTIME"  />
               <asp:BoundField DataField="LONGATION" HeaderText="LONGATION" />
               <asp:BoundField DataField="TARR_QTY" HeaderText="TARR QTY" />
               <asp:BoundField DataField="EXPR" HeaderText="EXPR" />
               <asp:BoundField DataField="CLRFLG" HeaderText="CLRFLG" />
               <asp:BoundField DataField="QCFLG" HeaderText="QCFLG" />
               <asp:BoundField DataField="MAC_GRUP_CODE" HeaderText="MAC GRUP CODE" />
               <asp:BoundField DataField="BUSSINESS_TYPE" HeaderText="BUSSINESS TYPE "   />
               <asp:BoundField DataField="STANDARD_COST" HeaderText= "STANDARD COST"  />
               <asp:BoundField DataField="TIME" HeaderText="TIME" />
               <asp:BoundField DataField="YEAR" HeaderText="YEAR" />
               <asp:BoundField DataField="TUSER" HeaderText="TUSER" />
               <asp:BoundField DataField="STATUS" HeaderText="STATUS" />
               <asp:BoundField DataField="QCFLG" HeaderText="QCFLG" />
               <asp:BoundField DataField="PRODUCT_TYPE" HeaderText="PRODUCT TYPE" />
           </Columns>
           <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
           <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
           <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
           <EditRowStyle BackColor="#2461BF" />
           <AlternatingRowStyle BackColor="White" />
       </asp:GridView>
                                   
   </asp:Panel>                     
  
        </td></tr>
 </table>
 </td>
 </tr> 

 </table>