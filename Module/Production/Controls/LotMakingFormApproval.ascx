<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LotMakingFormApproval.ascx.cs" Inherits="Module_Production_Controls_LotMakingFormApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<style type="text/css">
    .style1
    {
        height: 20px;
    }
    .style2
    {
        height: 19px;
    }
    .style3
    {
        color: #333333;
        
    }
</style>


<table align="left" border="0" cellpadding="0" cellspacing="0" class="tContentArial" width ="100%">
    <tr>
        <td align="left" valign="top" class="td">
            <table align="left" cellpadding="0" cellspacing="0" >
                <tr>
                    <td id="tdUpdate" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" Height="41" ImageUrl="~/CommonImages/edit1.jpg"
                            OnClick="imgbtnUpdate_Click1" ToolTip="Update" ValidationGroup="M1" Width="48"
                            OnClientClick="if (!confirm('Are you sure to Update the record ?')) { return false; }" />
                    </td>
                    <td id="tdDelete" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnDelete" runat="server" Height="41" ImageUrl="~/CommonImages/del6.png"
                            ToolTip="Delete" Width="48" 
                            OnClientClick="if (!confirm('Are you sure to Delete the record ?')) { return false; }" 
                            onclick="imgbtnDelete_Click" />
                    </td>
                    <td id="tdFind" runat="server" align="left" visible="false" width="48">
                        <asp:ImageButton ID="imgbtnFindTop" runat="server" Height="41" ImageUrl="~/CommonImages/link_find.png"
                            ToolTip="Find" Width="48" 
                            OnClientClick="if (!confirm('Are you sure to Find the record ?')) { return false; }" 
                            onclick="imgbtnFindTop_Click" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                            OnClick="imgbtnPrint_Click" ToolTip="Print" Width="48" OnClientClick="if (!confirm('Are you sure to print the record ?')) { return false; }" />
                        
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                            OnClick="imgbtnClear_Click" ToolTip="Clear" Width="48" OnClientClick="if (!confirm('Are you sure to Clear the record ?')) { return false; }" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                            OnClick="imgbtnExit_Click" ToolTip="Exit" Width="48" OnClientClick="if (!confirm('Are you sure to Exit ?')) { return false; }" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                            OnClick="imgbtnHelp_Click" ToolTip="Help" Width="48" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader td" colspan="3">
            <b class="titleheading">&nbsp;Lot Making Approval</b>
        </td>
    </tr>
    <tr>
        <td align="left" colspan="2" valign="top" >
            <span class="Mode">You are in&nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>
                &nbsp;Mode.</span> 
                
          
        </td>
        <td  align="right" valign="top">
              <span class="style3">Select Lot No to take Print:</span>&nbsp; 
                        <asp:DropDownList ID="ddlLotNo" runat="server" CssClass="SmallFont" TabIndex="1"
                            Width="150px" >
                        </asp:DropDownList>
            
        </td>
    </tr>
    <tr>
        <td align="center" colspan="3" class="td">
            <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="left" colspan="3" class="style1">
            <b>Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
            </b>
        </td>
    </tr>
    <tr>
        <td align="center" colspan="3" class="td" Width ="100%">
            <b>
            <asp:FormView ID="FormView1" runat="server" CellPadding="4" ForeColor="#333333" 
                Width="80%" AllowPaging="True" 
                onpageindexchanging="FormView1_PageIndexChanging" >
               <ItemTemplate>
<table width="100%">
 <tr ><td align="center" width="100%" colspan="4"><b><%# Eval("PRODUCT_CATEGORY")%>&nbsp;YARN&nbsp;LOT&nbsp;DETAILS&nbsp;FOR&nbsp;LOT&nbsp;NO.-&nbsp;<%# Eval("LOT_NO")%></b></td>      
     
      </tr>
  <tr ><td align="right" width="25%"><b>Poy Denier/FIL:</b></td>      
      <td align="left" width="25%"><%# Eval("POY_DESC")%></td>
      <td align="right" width="25%"><b>Merge No : </b><%# Eval("MERGE_NO")%></td>      
      <td align="left" width="25%"><b>&nbsp;&nbsp;Party : </b><%--(<%# Eval("PRTY_CODE")%>)--%><%# Eval("PRTY_NAME")%></td>
      </tr>
  <tr><td align="right"><b>Machine Name:</b></td>    
      <td><%# Eval("MACHINE_NAME")%></td>
      <td align="right"><b>Finished Denier:</b></td>    
      <td><%# Eval("FINISHED_DENIER_DESC")%></td>
      </tr>
      <tr><td align="right"><b>Lot No:</b></td>     
      <td>
      <asp:Label ID="lblLotNo" runat="server" Text='<%# Eval("LOT_NO")%>' ></asp:Label>
      </td>
      <td align="center"><b>T1</b></td>     
      <td align="left"><b>T2</b></td>
      <td align="left"></td>     
      <td></td>
      </tr>    
        <tr><td align="right"><b>Machine Speed:</b></td>     
      <td><%# Eval("MACHINE_SPEED")%></td>
      <td align="center"><b><%# Eval("T1")%>/<%# Eval("T1_H")%></b></td>     
      <td><%# Eval("T2")%>/<%# Eval("T1_L")%></td>
      </tr>  
        <tr><td align="right"><b>DR:</b></td>     
      <td><%# Eval("DR") %></td>
            <td align="center"><b>Ratio T1</b></td>     
      <td align="left"><b>Ratio T2</b></td>
      <td align="left"></td>     
      <td></td>
      </tr>    
        <tr><td align="right"><b>SOF:</b></td>     
      <td><%# Eval("SOF") %></td>
     <td align="center"><b><%# Eval("RATIO_T1_H")%></b></td>     
      <td><%# Eval("RATIO_T2_L")%></td>
      </tr> 
        <tr><td align="right"><b>TKP:</b></td>      
      <td><%# Eval("TKP") %></td>
      <td align="right"><b>Dry Den:</b></td>      
      <td><%# Eval("DRY_DEN")%></td>
      </tr>
      <tr><td align="right"><b>DY:</b></td>    
      <td><%# Eval("DY") %></td>
       <td align="right"><b>ELG:</b></td>     
      <td><%# Eval("ELG")%></td>
      </tr>
      <tr><td align="right"><b>CPM:</b></td>     
      <td><%# Eval("CPM") %></td>
       <td align="right"><b>GPD :</b></td>     
      <td><%# Eval("GPD")%></td>
      </tr>
       <tr><td align="right"><b>PH:</b></td>     
      <td><%# Eval("PH") %></td>
       <td align="right"><b>OPU:</b></td>     
      <td><%# Eval("OPU")%></td>
      </tr>
       <tr><td align="right"><b>SH:</b></td>     
      <td><%# Eval("SH") %></td>
           <td align="right"><b>Oil Den:</b></td>     
      <td><%# Eval("OIL_DEN")%></td>
      </tr>
      <tr><td align="right"><b>Oil Rpm:</b></td>     
      <td><%# Eval("OIL_RPM") %></td>   
      <td align="right"><b>Purpose:</b></td>     
      <td><%# Eval("PURPOSE")%></td> 
      </tr>    
      <tr><td align="right"><b>Roto Pressure:</b></td>     
      <td><%# Eval("ROTO_PRESSURE")%></td>
      <td align="right"><b>Heat Setting:</b></td>     
      <td><%# Eval("HEAT_SETTING")%></td>
      </tr>  
       <tr><td align="right"><b>Jet No:</b></td>     
      <td><%# Eval("JET_NO")%></td></tr>
       <tr><td align="right"><b>Doff Time:</b></td>     
      <td><%# Eval("DOFF_TIME")%></td></tr>
       <tr><td align="right"><b>Doff Weight:</b></td>     
      <td><%# Eval("DOFF_WEIGHT")%></td></tr>
      <tr><td align="right"><b>PLT:</b></td>     
      <td><%# Eval("PLT")%></td></tr>    
         <tr><td align="right"><b>TINT:</b></td>     
      <td><%# Eval("TINT")%></td>
       <td align="right">
     Check to Approve
      </td>
      <td align="left">
      <asp:CheckBox runat="server" ID="chkApproved" />
      </td>
      </tr>
      
</table>                     
</ItemTemplate> 
            </asp:FormView>
          
        </td>
    </tr>
</table>
 
 