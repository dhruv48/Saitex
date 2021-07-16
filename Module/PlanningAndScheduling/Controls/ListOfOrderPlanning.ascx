<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ListOfOrderPlanning.ascx.cs" Inherits="Module_PlanningAndScheduling_Controls_ListOfOrderPlanning" %>
<%@ Register Assembly="AjaxControlToolkit"  Namespace="AjaxControlToolkit" TagPrefix="cc3"  %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<link href="../../../StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />

<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;}
    .header
    {
        margin-left: 2px;
    }
    .c1
    {
        width: 150px;
    }
    .c2
    {
        margin-left: 2px;
        width: 150px;
    }
    .c3
    { 
        margin-left : 2px;
        width: 200px;
    }
    .d1
    {
        width: 180px;
    }
    .d2
    {
        margin-left: 4px;
        width: 180px;
    }
    .d3
    {
        margin-left: 4px;
        width: 180px;
    }
    .d4
    {
        margin-left: 4px;
        width: 180px;
    }
</style>

<script type="text/javascript">
 function divexpandcollapse(divname) {
        var div = document.getElementById(divname);
        var img = document.getElementById('img' + divname);

        if (div.style.display == "none") {
            div.style.display = "inline";
            img.src = "../../../APP_IMAGES/minus.gif";
        } else {
            div.style.display = "none";
            img.src = "../../../APP_IMAGES/plus.gif";
            
        }
    }

</script>

<table  width="100%">
<tr id="trProcessRootDetails"  runat="server">
<td align="center" class="TableHeader" colspan="4">
<b class="titleheading" > List&nbsp;Of&nbsp;<asp:Label ID="lblType" runat="server"></asp:Label>&nbsp;Order</b>
</td>
</tr>
<tr>
<td>&nbsp;</td>
</tr>

<tr >
<td align="center" >
 
<asp:Panel ID="pnlPlanned" runat="server" Visible="false" >
<table>
<tr>
<td width="100%" align="left">
<asp:Button ID="btnUnplanned" Text="Unplan" runat="server" CssClass="AButton"  onclick="btnUnplanned_Click"     />
</td>
</tr>

<tr>
<td width="100%" align="center">
<asp:GridView ID="grdOrderDetails" runat="server" AllowPaging="True" 
        AllowSorting="True"  AutoGenerateColumns="false" CellPadding="4" 
        Font-Size="10px" ForeColor="#333333"  GridLines="None"    Width="100%" 
        onrowdatabound="grdOrderDetails_RowDataBound" >
                            <FooterStyle BackColor="#CCCCCC" />
                                <RowStyle BackColor="#EFF3FB" />
                             <Columns>     
                                           <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>                                                                                                                 
                                                           <a href="JavaScript:divexpandcollapse('div<%# Eval("ORDER_NO") %>');"><img id="imgdiv<%# Eval("ORDER_NO") %>" width="9px" border="0" src="../../../APP_IMAGES/plus.gif" /></a>
                                                    
                                                     <asp:Label ID="txtord" runat="server" CssClass="Label SmallFont"   Text='<%# Bind("ORDER_NO") %>' Visible="false"></asp:Label> 
                                                    </ItemTemplate>
                                             </asp:TemplateField>
                                                                            
                                       <asp:TemplateField HeaderText="ORDER NO" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblorderno" runat="server" Text='<%# Eval("ORDER_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="PARTY NAME" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblprtyname" runat="server" Text='<%# Eval("PRTY_NAME") %>' ToolTip='<%# Eval("PRTY_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="120px" />
                                    </asp:TemplateField>     
                                                       
                                     <asp:TemplateField HeaderText="ARTICAL DESCRIPTION" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblarticleDesc" runat="server" Text='<%# Eval("ARTICAL_DESC") %>' ToolTip='<%# Eval("ARTICAL_CODE") %>' ></asp:Label>
                                        </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>                           
                                                                       
                                   
                                     <asp:TemplateField HeaderText="SHADE NAME" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSHADE_NAME" runat="server" Text='<%# Eval("SHADE_NAME") %>' ToolTip='<%# Eval("SHADE_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="ORD QTY" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20px">
                                        <ItemTemplate>                                          
                                            <asp:Label ID="lblordqty" runat="server" Text='<%# Bind("ORDER_QTY") %>'  ></asp:Label>                                         
                                        </ItemTemplate>
                                        <HeaderStyle Width="20px" />
                                     <ItemStyle HorizontalAlign="Left" Width="20px"></ItemStyle>
                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderText="PLANNED QTY" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20px">
                                        <ItemTemplate>                                          
                                            <asp:Label ID="lblplannedqty" runat="server" Text='<%# Bind("TOTAL_PLANNED_QTY") %>'   ></asp:Label>                                         
                                        </ItemTemplate>
                                        <HeaderStyle Width="20px" />
                                     <ItemStyle HorizontalAlign="Left" Width="20px"></ItemStyle>
                                    </asp:TemplateField>    
                                                                          
                                     <%--<asp:TemplateField HeaderText="REMARKS" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblremarks" runat="server" Text='<%# Eval("REMARKS") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20px" />
                                       <ItemStyle HorizontalAlign="Left" Width="20px"></ItemStyle>
                                    </asp:TemplateField>                              
                                                                   
                                    <asp:TemplateField  HeaderText="ORDER DATE" ItemStyle-HorizontalAlign="Left" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblorderdt" runat="server" Text='<%# Eval("ORDER_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                      <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField  HeaderText="ORDER DELEVERY DATE" ItemStyle-HorizontalAlign="Left" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDELDT" runat="server" Text='<%# Eval("DEL_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                      <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>   --%>         
                                 
                                   <asp:TemplateField  HeaderText="ORDER PROCESS ROOT" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprderpro" runat="server" Text='<%# Eval("PROS_ROUTE_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                       <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>                                 
                                     
                                      <asp:TemplateField>
                                        <ItemTemplate>
                                           <asp:CheckBox ID="chkOrder" runat="server" />
                                        </ItemTemplate>                                       
                                      </asp:TemplateField>     
                                     
                                       <asp:TemplateField>
                                    <ItemTemplate>
                                    <tr>
                                    <td colspan="100%">
                                   <div id="div<%# Eval("ORDER_NO") %>" style="display: none; position: relative; left: 15px; overflow: auto" width="100%">
                                   
                                   <asp:GridView ID="gvChildGrid" runat="server" AutoGenerateColumns="false" 
                                            BorderStyle="Double"  BorderColor="#df5015" GridLines="None" Width="95%" 
                                            DataKeyNames="ORDER_NO">
                                   <HeaderStyle BackColor="#df5015" Font-Bold="true" ForeColor="White" />
                                   <RowStyle BackColor="#E1E1E1" />
                                   <AlternatingRowStyle BackColor="White" />
                                   <HeaderStyle BackColor="#df5015" Font-Bold="true" ForeColor="White" />
                                   <Columns>
                                 
                                  <asp:BoundField DataField="PROS_ROUTE_CODE" HeaderText="PROCESS ROUTE CODE" 
                                           HeaderStyle-HorizontalAlign="Left" >                                    
                                       </asp:BoundField>
                                       
                                       <asp:BoundField DataField="MACHINE_GROUP" HeaderText="MACHINE GROUP" 
                                           HeaderStyle-HorizontalAlign="Left" >                                    
                                       </asp:BoundField>
                                 
                                   <asp:BoundField DataField="MACHINE_CODE" HeaderText="MACHINE CODE" 
                                           HeaderStyle-HorizontalAlign="Left" >                                      
                                       </asp:BoundField>
                                       
                                   <%-- <asp:BoundField DataField="MACHINE_CAPACITY" HeaderText="MACHINE CAPACITY" 
                                           HeaderStyle-HorizontalAlign="Left" >
                                       </asp:BoundField>--%>
                                   
                                  <asp:BoundField DataField="P_QTY" HeaderText="PLANNED QTY" 
                                           HeaderStyle-HorizontalAlign="Left" >                                      
                                       </asp:BoundField>
                                       
                                   <asp:TemplateField HeaderText="START DATE" ItemStyle-HorizontalAlign="Left" Visible="true" ItemStyle-Width="30%">
                                        <ItemTemplate>
                                          <asp:Label ID="txtFrom" runat="server" Width="50%"  
                                                Text='<%# Bind("SCHEDULED_DATE_FROM") %>' ></asp:Label>                                                    
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                   
                                       <asp:TemplateField HeaderText="END DATE" ItemStyle-HorizontalAlign="Left" Visible="true" ItemStyle-Width="30%">
                                        <ItemTemplate>
                                          <asp:Label ID="txtTo" runat="server" Width="50%"  
                                                Text='<%# Bind("SCHEDULED_DATE_TO") %>'  ></asp:Label> 
                                        
                                        </ItemTemplate>
                                       
                                           <ItemStyle HorizontalAlign="Left" Width="30%" />
                                       
                                    </asp:TemplateField>
                                    
                                    
                                    
                                    
                                    
                                  </Columns>
                                  </asp:GridView>
                               
                                   </td>
                                  </tr>
                                    </ItemTemplate>
                                   </asp:TemplateField>
                                                               
                                </Columns>
                        
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#336799" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
</td>
</tr>
</table>
</asp:Panel>
 
<asp:Panel ID="pnlRemaining" runat="server" Visible="false" >

<asp:GridView ID="grdOrderDetails1" runat="server" AllowPaging="True" AllowSorting="True"  AutoGenerateColumns="false" CellPadding="4" Font-Size="10px" ForeColor="#333333"  GridLines="None"    Width="100%" >
                            <FooterStyle BackColor="#CCCCCC" />
                                <RowStyle BackColor="#EFF3FB" />
                               
                                  <Columns>                                 
                                 
                                     <asp:TemplateField HeaderText="ORDER NO" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblorderno" runat="server" Text='<%# Eval("ORDER_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="PRTY NAME" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblprtyname" runat="server" Text='<%# Eval("PRTY_NAME") %>' ToolTip='<%# Eval("PRTY_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="120px" />
                                    </asp:TemplateField>     
                                                                   
                                          
                                     <asp:TemplateField HeaderText="ARTICAL DESCRIPTION" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblarticleDesc" runat="server" Text='<%# Eval("ARTICAL_DESC") %>' ToolTip='<%# Eval("ARTICAL_CODE") %>' ></asp:Label>
                                        </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>                           
                                    
                                    
                                   
                                     <asp:TemplateField HeaderText="SHADE NAME" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSHADE_NAME" runat="server" Text='<%# Eval("SHADE_NAME") %>' ToolTip='<%# Eval("SHADE_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="ORD QTY" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20px">
                                        <ItemTemplate>                                          
                                            <asp:Label ID="lblordqty" runat="server" Text='<%# Bind("ORD_QTY") %>' readonly="true"  Width="75px"   ></asp:Label>                                         
                                        </ItemTemplate>
                                        <HeaderStyle Width="20px" />
                                     <ItemStyle HorizontalAlign="Left" Width="20px"></ItemStyle>
                                    </asp:TemplateField>    
                                      
                                    
                                     <%--<asp:TemplateField HeaderText="REMARKS" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblremarks" runat="server" Text='<%# Eval("REMARKS") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20px" />
                                       <ItemStyle HorizontalAlign="Left" Width="20px"></ItemStyle>
                                    </asp:TemplateField>        --%>                      

                                                                   
                                    <asp:TemplateField  HeaderText="ORDER DATE" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblorderdt" runat="server" Text='<%# Eval("ORDER_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                      <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField  HeaderText="ORDER DELEVERY DATE" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblDELDT" runat="server" Text='<%# Eval("DEL_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                      <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    
                                                                    
                                 
                                   <asp:TemplateField  HeaderText="ORDER PROCESS ROOT" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprderpro" runat="server" Text='<%# Eval("PROS_ROUTE_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                       <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>                                 
                                                               
                                </Columns>
                        
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#336799" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>

</asp:Panel>

<asp:Panel ID="pnlUnplanned" runat="server" Visible="false" >


<asp:GridView ID="grdOrderDetails2" runat="server" AllowPaging="True" 
        AllowSorting="True"  AutoGenerateColumns="false" CellPadding="4" 
        Font-Size="10px" ForeColor="#333333"  GridLines="None"    Width="100%" 
        onrowdatabound="grdOrderDetails_RowDataBound" >
                            <FooterStyle BackColor="#CCCCCC" />
                                <RowStyle BackColor="#EFF3FB" />
                             <Columns>     
                                           <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>                                                                                                                 
                                                           <a href="JavaScript:divexpandcollapse('div<%# Eval("ORDER_NO") %>');"><img id="imgdiv<%# Eval("ORDER_NO") %>" width="9px" border="0" src="../../../APP_IMAGES/plus.gif" /></a>
                                                    
                                                     <asp:Label ID="txtord" runat="server" CssClass="Label SmallFont"   Text='<%# Bind("ORDER_NO") %>' Visible="false"></asp:Label> 
                                                    </ItemTemplate>
                                             </asp:TemplateField>
                                                                            
                                       <asp:TemplateField HeaderText="ORDER NO" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblorderno" runat="server" Text='<%# Eval("ORDER_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="PARTY NAME" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblprtyname" runat="server" Text='<%# Eval("PRTY_NAME") %>' ToolTip='<%# Eval("PRTY_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="120px" />
                                    </asp:TemplateField>     
                                                       
                                     <asp:TemplateField HeaderText="ARTICAL DESCRIPTION" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblarticleDesc" runat="server" Text='<%# Eval("ARTICAL_DESC") %>' ToolTip='<%# Eval("ARTICAL_CODE") %>' ></asp:Label>
                                        </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>                           
                                                                       
                                   
                                     <asp:TemplateField HeaderText="SHADE NAME" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSHADE_NAME" runat="server" Text='<%# Eval("SHADE_NAME") %>' ToolTip='<%# Eval("SHADE_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="ORD QTY" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20px">
                                        <ItemTemplate>                                          
                                            <asp:Label ID="lblordqty" runat="server" Text='<%# Bind("ORDER_QTY") %>'  ></asp:Label>                                         
                                        </ItemTemplate>
                                        <HeaderStyle Width="20px" />
                                     <ItemStyle HorizontalAlign="Left" Width="20px"></ItemStyle>
                                    </asp:TemplateField>
                                    
                                    <%-- <asp:TemplateField HeaderText="PLANNED QTY" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20px">
                                        <ItemTemplate>                                          
                                            <asp:Label ID="lblplannedqty" runat="server" Text='<%# Bind("TOTAL_PLANNED_QTY") %>'   ></asp:Label>                                         
                                        </ItemTemplate>
                                        <HeaderStyle Width="20px" />
                                     <ItemStyle HorizontalAlign="Left" Width="20px"></ItemStyle>
                                    </asp:TemplateField>    --%>
                                                                          
                                     <%--<asp:TemplateField HeaderText="REMARKS" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblremarks" runat="server" Text='<%# Eval("REMARKS") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20px" />
                                       <ItemStyle HorizontalAlign="Left" Width="20px"></ItemStyle>
                                    </asp:TemplateField>                              
                                                                   
                                    <asp:TemplateField  HeaderText="ORDER DATE" ItemStyle-HorizontalAlign="Left" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblorderdt" runat="server" Text='<%# Eval("ORDER_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                      <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField  HeaderText="ORDER DELEVERY DATE" ItemStyle-HorizontalAlign="Left" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDELDT" runat="server" Text='<%# Eval("DEL_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                      <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>   --%>         
                                 
                                   <asp:TemplateField  HeaderText="ORDER PROCESS ROOT" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprderpro" runat="server" Text='<%# Eval("PROS_ROUTE_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                       <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>                                 
                                     
                                      <asp:TemplateField>
                                        <ItemTemplate>
                                           <asp:CheckBox ID="chkOrder" runat="server" />
                                        </ItemTemplate>                                       
                                      </asp:TemplateField>     
                                     
                                       <asp:TemplateField>
                                    <ItemTemplate>
                                    <tr>
                                    <td colspan="100%">
                                   <div id="div<%# Eval("ORDER_NO") %>" style="display: none; position: relative; left: 15px; overflow: auto" width="100%">
                                   
                                   <asp:GridView ID="gvChildGrid" runat="server" AutoGenerateColumns="false" 
                                            BorderStyle="Double"  BorderColor="#df5015" GridLines="None" Width="95%" 
                                            DataKeyNames="ORDER_NO">
                                   <HeaderStyle BackColor="#df5015" Font-Bold="true" ForeColor="White" />
                                   <RowStyle BackColor="#E1E1E1" />
                                   <AlternatingRowStyle BackColor="White" />
                                   <HeaderStyle BackColor="#df5015" Font-Bold="true" ForeColor="White" />
                                   <Columns>
                                 
                                  <asp:BoundField DataField="PROS_ROUTE_CODE" HeaderText="PROCESS ROUTE CODE" 
                                           HeaderStyle-HorizontalAlign="Left" >                                    
                                       </asp:BoundField>
                                       
                                       <asp:BoundField DataField="MACHINE_GROUP" HeaderText="MACHINE GROUP" 
                                           HeaderStyle-HorizontalAlign="Left" >                                    
                                       </asp:BoundField>
                                 
                                   <asp:BoundField DataField="MACHINE_CODE" HeaderText="MACHINE CODE" 
                                           HeaderStyle-HorizontalAlign="Left" >                                      
                                       </asp:BoundField>
                                       
                                   <%-- <asp:BoundField DataField="MACHINE_CAPACITY" HeaderText="MACHINE CAPACITY" 
                                           HeaderStyle-HorizontalAlign="Left" >
                                       </asp:BoundField>--%>
                                   
                                  <asp:BoundField DataField="P_QTY" HeaderText="PLANNED QTY" 
                                           HeaderStyle-HorizontalAlign="Left" >                                      
                                       </asp:BoundField>
                                       
                                   <asp:TemplateField HeaderText="START DATE" ItemStyle-HorizontalAlign="Left" Visible="true" ItemStyle-Width="30%">
                                        <ItemTemplate>
                                          <asp:Label ID="txtFrom" runat="server" Width="50%"  
                                                Text='<%# Bind("SCHEDULED_DATE_FROM") %>' ></asp:Label>                                                    
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                   
                                       <asp:TemplateField HeaderText="END DATE" ItemStyle-HorizontalAlign="Left" Visible="true" ItemStyle-Width="30%">
                                        <ItemTemplate>
                                          <asp:Label ID="txtTo" runat="server" Width="50%"  
                                                Text='<%# Bind("SCHEDULED_DATE_TO") %>'  ></asp:Label> 
                                        
                                        </ItemTemplate>
                                       
                                           <ItemStyle HorizontalAlign="Left" Width="30%" />
                                       
                                    </asp:TemplateField>
                                    
                                    
                                    
                                    
                                    
                                  </Columns>
                                  </asp:GridView>
                               
                                   </td>
                                  </tr>
                                    </ItemTemplate>
                                   </asp:TemplateField>
                                                               
                                </Columns>
                        
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#336799" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>


</asp:Panel>
</td>
</tr>
</table>