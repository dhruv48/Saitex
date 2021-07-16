<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Production_Confirmation.ascx.cs" Inherits="Module_Production_Controls_Production_Confirmation" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
 
 <asp:UpdatePanel ID="updPanel" runat="server">
 <ContentTemplate>


<script type = "text/javascript">

 function SetTarget() {

     document.forms[0].target = "_blank";

 }

</script>

 <table width="100%"  class="tContentArial">
   <tr>
        <td align="left" valign="top" class="td" width="100%">
            <table align="left">
                <tr>
                   
                    
                   <td >
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Width="48" Height="41" ToolTip="Print"
                            ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click"></asp:ImageButton>&nbsp;
                    </td>
                    <td >
                        <asp:ImageButton ID="imgbtnExport" runat="server" Width="48" Height="41" ToolTip="Print"
                            ImageUrl="~/CommonImages/export.png" OnClick="imgbtnExport_Click"></asp:ImageButton>&nbsp;
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" ToolTip="Clear"
                            ImageUrl="~/CommonImages/clear.jpg" OnClick="imgbtnClear_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" Width="48" Height="41" ToolTip="Exit"
                            ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" runat="server" Width="48" Height="41" ToolTip="Help"
                            ImageUrl="~/CommonImages/link_help.png" OnClick="imgbtnHelp_Click"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader td" width="100%">
            <b class="titleheading">Production Planning Form <asp:Literal ID="ltrHeading" runat="server" ></asp:Literal>
        </td></b>
    </tr>
    
    <tr>
        <td align="left" class="td" width="100%">
                  <asp:GridView ID="grdProductionDetails" runat="server"  CallbackMode="false" 
                Serialize="true" AllowAddingRecords="false"
                AutoGenerateColumns="false" AllowRecordSelection="false" AllowFiltering="True" 
                AllowMultiRecordSelection="false"  width="100%"  
                onrowdatabound="grdProductionDetails_RowDataBound" 
                      onpageindexchanging="grdProductionDetails_PageIndexChanging">
             
                <Columns>                      
                    <asp:TemplateField HeaderText="Comp Code" Visible="false" ItemStyle-Width="5%" HeaderStyle-Width="5%" >
                        <ItemTemplate>
                            <asp:Label ID="lblCompCode" runat="server" CssClass="SmallFont" Text='<%# Bind("COMP_CODE") %>' 
                                ></asp:Label>
                        </ItemTemplate>                       
                        <HeaderStyle Width="5%" />
                        <ItemStyle Width="5%" />
                    </asp:TemplateField>
                   
                    <asp:TemplateField HeaderText="Branch Code" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblBranchCode" runat="server" CssClass="SmallFont"   Text='<%# Bind("BRANCH_CODE") %>'
                               ></asp:Label>
                        </ItemTemplate>                       
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Branch Name" Visible="false" >
                        <ItemTemplate>
                            <asp:Label ID="lblBranchName" runat="server" CssClass="SmallFont"  Text='<%# Bind("BRANCH_NAME") %>'
                               ></asp:Label>
                        </ItemTemplate>                       
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Year"  >
                    <HeaderTemplate>
                       <table width="50px">
                                <tr>
                                    <td align="center" >
                                      Year <asp:ImageButton ID="btnYear" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                            Text="Go" OnClick="FilterGrid_Click" Height="10px" Width="10px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtYear" Width="40px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                    </td>                                  
                                </tr>
                            </table>
                    </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblYear" runat="server" CssClass="SmallFont"  Text='<%# Bind("YEAR") %>'
                               ></asp:Label>
                        </ItemTemplate>                       
                    </asp:TemplateField>
                    
                    
                     <asp:TemplateField HeaderText="Artical Type" Visible="false" ItemStyle-Width="5%" HeaderStyle-Width="5%" >
                        <ItemTemplate>
                            <asp:Label ID="lblArticalType" runat="server" CssClass="SmallFont"  Text='<%# Bind("ARTICAL_TYPE") %>' 
                                ></asp:Label>
                        </ItemTemplate>                       
                        <HeaderStyle Width="5%" />
                        <ItemStyle Width="5%" />
                    </asp:TemplateField>
                    
                    
                    <asp:TemplateField HeaderText="B.Type">
                     <HeaderTemplate>
                       <table width="50px">
                                <tr>
                                    <td align="center" >
                                      Type <asp:ImageButton ID="btnBType" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                            Text="Go" OnClick="FilterGrid_Click" Height="10px" Width="10px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtBtype" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                    </td>                                  
                                </tr>
                            </table>
                            </HeaderTemplate> 
                        <ItemTemplate>
                            <asp:Label ID="lblBusinessType" runat="server" CssClass="SmallFont"  Text='<%# Bind("BUSINESS_TYPE") %>' ToolTip='<%# Bind("PRODUCTION_TYPE") %>'
                                ></asp:Label>
                        </ItemTemplate>                       
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderText="Order Type" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblOrderType" runat="server" CssClass="SmallFont"  Text='<%# Bind("ORDER_TYPE") %>' ToolTip='<%# Bind("ORDER_CAT") %>'
                                ></asp:Label>
                        </ItemTemplate>                       
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Party Name" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblPartyName" runat="server" CssClass="SmallFont"  Text='<%# Bind("PRTY_NAME") %>' ToolTip='<%# Bind("PRTY_CODE") %>'
                                ></asp:Label>
                        </ItemTemplate>                       
                    </asp:TemplateField>
                    
                      <asp:TemplateField HeaderText="Order No">
                        <HeaderTemplate>
                       <table width="50px">
                                <tr>
                                    <td align="center" >
                                      Order NO <asp:ImageButton ID="btnOrderNo" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                            Text="Go" OnClick="FilterGrid_Click" Height="10px" Width="10px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtOrderNo" Width="100px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                    </td>                                  
                                </tr>
                            </table>
                            </HeaderTemplate> 
                        <ItemTemplate>
                            <asp:Label ID="lblOrderNo" runat="server" CssClass="SmallFont"  Text='<%# Bind("PI_NO") %>' ToolTip='<%# Bind("ORDER_NO") %>'
                                ></asp:Label>
                        </ItemTemplate>                       
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Product&nbsp;Desc">
                      <HeaderTemplate>
                       <table width="50px">
                                <tr>
                                    <td align="center" >
                                     Product <asp:ImageButton ID="btnProductDesc" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                            Text="Go" OnClick="FilterGrid_Click" Height="10px" Width="10px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtProductDesc" Width="140px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                    </td>                                  
                                </tr>
                            </table>
                            </HeaderTemplate> 
                        <ItemTemplate>
                            <asp:Label ID="lblProductionType" runat="server" CssClass="SmallFont" Text='<%# Bind("PRODUCT_DESC") %>'   ToolTip='<%# Bind("PRODUCT_TYPE") %>' 
                                ></asp:Label>
                        </ItemTemplate>                       
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SO&nbsp;Qty">
                      <HeaderTemplate>
                       <table width="50px">
                                <tr>
                                    <td align="center" >
                                     SO Qty <asp:ImageButton ID="btnSOQty" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                            Text="Go" OnClick="FilterGrid_Click" Height="10px" Width="10px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtSOQty" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                    </td>                                  
                                </tr>
                            </table>
                            </HeaderTemplate> 
                        <ItemTemplate>
                            <asp:Label ID="lblProductionQty" runat="server" CssClass="SmallFont"  Text='<%# Bind("ORD_QTY") %>'  
                                ></asp:Label>
                        </ItemTemplate>                       
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Main Artical&nbsp;Desc" Visible="false">
                      <HeaderTemplate>
                       <table width="50px">
                                <tr>
                                    <td align="center" >
                                    Main  Artical Desc <asp:ImageButton ID="btnArticleDesc" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                            Text="Go" OnClick="FilterGrid_Click" Height="10px" Width="10px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtArticalDesc" Width="140px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                    </td>                                  
                                </tr>
                            </table>
                            </HeaderTemplate> 
                        <ItemTemplate>
                            <asp:Label ID="lblArticalDesc" runat="server" CssClass="SmallFont"  Text='<%# Bind("ARTICAL_DESC") %>'   ToolTip='<%# Bind("ARTICAL_CODE") %>' 
                                ></asp:Label>
                        </ItemTemplate>                       
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Artical&nbsp;Desc">
                      <HeaderTemplate>
                       <table width="50px">
                                <tr>
                                    <td align="center" >
                                     Artical Desc <asp:ImageButton ID="btnAssArticleDesc" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                            Text="Go" OnClick="FilterGrid_Click" Height="10px" Width="10px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtAssArticalDesc" Width="140px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                    </td>                                  
                                </tr>
                            </table>
                            </HeaderTemplate> 
                        <ItemTemplate>
                            <asp:Label ID="lblAssArticalDesc" runat="server" CssClass="SmallFont"  Text='<%# Bind("ASS_ARTICAL_DESC") %>'   ToolTip='<%# Bind("ASS_ARTICAL_CODE") %>' 
                                ></asp:Label>
                        </ItemTemplate>                       
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Artical&nbsp;Shade">
                       <HeaderTemplate>
                       <table width="50px">
                                <tr>
                                    <td align="center" >
                                     Shade <asp:ImageButton ID="btnArticleShade" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                            Text="Go" OnClick="FilterGrid_Click" Height="10px" Width="10px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtArticalShade" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                    </td>                                  
                                </tr>
                            </table>
                            </HeaderTemplate> 
                        <ItemTemplate>
                            <asp:Label ID="lblShade" runat="server" CssClass="SmallFont"  Text='<%# Bind("SHADE_CODE") %>'  
                                ></asp:Label>
                        </ItemTemplate>                       
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Artical&nbsp;Qty">
                    <HeaderTemplate>
                       <table width="50px">
                                <tr>
                                    <td align="center" >
                                      Qty <asp:ImageButton ID="btnArticleQty" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                            Text="Go" OnClick="FilterGrid_Click" Height="10px" Width="10px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtArticalQty" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                    </td>                                  
                                </tr>
                            </table>
                            </HeaderTemplate> 
                        <ItemTemplate>
                            <asp:Label ID="lblArticalQty" runat="server" CssClass="SmallFont"  Text='<%# Bind("ARTICAL_QTY") %>'  
                                ></asp:Label>
                        </ItemTemplate>                       
                    </asp:TemplateField>  
                     <asp:TemplateField HeaderText="Prd. Qty" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>                         
                                <asp:Label ID="lblProdQty" runat="server"  Text='<%# Eval("PRODUCTION_QTY") %>' ></asp:Label>
                        </ItemTemplate>                          
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>  
                        </asp:TemplateField>            
                   <asp:TemplateField HeaderText="Pck. Qty" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>                         
                                <asp:Label ID="lblPackingQty" runat="server"  Text='<%# Eval("PACKING_QTY") %>' ></asp:Label>
                        </ItemTemplate>                          
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>  
                        </asp:TemplateField> 
                   <asp:TemplateField HeaderText="Proces Root" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                           
                            <asp:Button ID="btnProcessRoot" runat="server" Width="100px" CssClass="SmallFont" 
                                Text='<%# Eval("PROS_ROUTE_CODE") %>'  onclick="btnProcessRoot_Click"   />
                                <asp:Label ID="lblProcessRootConfig" runat="server" Visible="false" Text='<%# Eval("PROCESS_ROUTE_FLAG") %>' ></asp:Label>
                        </ItemTemplate>  
                        
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>  
                        </asp:TemplateField> 

                    <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                           <asp:CheckBox ID="chkStatus" runat="server"   />
                            <asp:Button ID="btnStatus" runat="server" Width="100px" CssClass="SmallFont"  Text='<%# Eval("SCHEDULED_QTY") %>' onclick="btnStatus_Click"   OnClientClick = "SetTarget();" />
                        </ItemTemplate>                       

                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </asp:TemplateField>
                       <asp:TemplateField HeaderText="View">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnViewTRN" runat="server" Text="Details"></asp:LinkButton>
                            <asp:Panel ID="pnlTRN" runat="server" BackColor="Beige" BorderWidth="1"  >
                                <asp:GridView ID="grdTRN" runat="server" AutoGenerateColumns="False"   CssClass="SmallFont" Width="100%" >
                                    <Columns>
                                        <asp:BoundField DataField="BASE_ARTICAL_CODE" HeaderText="Article Code">
                                            <ItemStyle HorizontalAlign="Center" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="BASE_ARTICAL_DESC" HeaderText="Article Description">
                                            <ItemStyle HorizontalAlign="Center" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>                                       
                                       
                                 <asp:TemplateField HeaderText="Req. Qty" HeaderStyle-HorizontalAlign="Center" runat="server"   >
                                 <ItemTemplate>                             
                                    <asp:Label ID="lblREQ_QTY" runat="server" Text='<%# Bind("REQ_QTY") %>'  ></asp:Label>
                               </ItemTemplate>
                               <ItemStyle CssClass="labelNo smallfont" Wrap="true" HorizontalAlign="Center" VerticalAlign="Top" />
                             </asp:TemplateField>  
                                                                 
                                        <asp:BoundField DataField="UOM" HeaderText="UOM" HeaderStyle-HorizontalAlign="Center"      >
                                            <ItemStyle HorizontalAlign="Center" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                              <asp:TemplateField HeaderText="Stock Qty" HeaderStyle-HorizontalAlign="Center" runat="server"  >
                                 <ItemTemplate>                             
                                    <asp:Label ID="lblSTOCK_QTY" runat="server" Text='<%# Bind("STOCK_QTY") %>' ></asp:Label>
                               </ItemTemplate>
                               <ItemStyle CssClass="labelNo smallfont" Wrap="true" HorizontalAlign="Center" VerticalAlign="Top" />
                             </asp:TemplateField>
                              <asp:TemplateField HeaderText="Issue Qty" HeaderStyle-HorizontalAlign="Center" runat="server"  >
                                 <ItemTemplate>                             
                                    <asp:Label ID="lblISS_QTY" runat="server" Text='<%# Bind("ISS_QTY") %>' ></asp:Label>
                               </ItemTemplate>
                               <ItemStyle CssClass="labelNo smallfont" Wrap="true" HorizontalAlign="Center" VerticalAlign="Top" />
                             </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="SmallFont" Width="98%" />
                                    <HeaderStyle CssClass="SmallFont" BackColor="#336799" />
                                </asp:GridView>
                            </asp:Panel>
                            <cc1:HoverMenuExtender ID="hmeTRN" runat="server" PopupPosition="Left" OffsetX="20"  OffsetY="20" PopupControlID="pnlTRN"
                                TargetControlID="lbtnViewTRN">
                            </cc1:HoverMenuExtender>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                    </asp:TemplateField>

                   </Columns>
                  
             </asp:GridView>
        </td>
    </tr>
</table>
  </ContentTemplate>
 <Triggers>
 <asp:PostBackTrigger ControlID="imgbtnExport" />
 </Triggers>
 </asp:UpdatePanel>   