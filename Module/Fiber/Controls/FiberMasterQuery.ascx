<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FiberMasterQuery.ascx.cs" Inherits="Module_Fiber_Controls_FiberMasterQuery" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="left" class=" td tContentArial"  width = "100%">
            <tr>
                <td>
                    <table class=" td tContentArial"   >
                        <tr>
                        <td id="tdUpdate" runat="server" align="left">
                       <asp:ImageButton ID="imgbtnAddNew" runat="server" Width="48" Height="41" ToolTip="Add New"
                            ImageUrl="~/CommonImages/addnew.png" onclick="imgbtnAddNew_Click" ></asp:ImageButton></td>
                        <td>
                        
                            <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                                ToolTip="Print" OnClick="imgbtnPrint_Click1" />
                        </td>
                        
                                   <td >
                        <asp:ImageButton ID="ImageButton1" runat="server" Width="48" Height="41" ToolTip="Print"
                            ImageUrl="~/CommonImages/export.png" OnClick="imgbtnPrint_Click"></asp:ImageButton>&nbsp;
                    </td>  
                            <td>
                                <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                    ToolTip="Clear" Width="48" OnClick="imgbtnClear_Click"></asp:ImageButton>
                            </td>
                            
                            
               
                            
                            <td>
                                <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                                    ToolTip="Exit" Width="48" OnClick="imgbtnExit_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                                    ToolTip="Help" Width="48" onclick="imgbtnHelp_Click" />
                            </td>
                        </tr>
                    </table>
                    
                        <tr>
                            <td align="center" class="TableHeader td" >
                                <span class="titleheading"><strong>Poy Master List</strong> </span>
                            </td>
                        </tr>
                   
                  
                       
                  
                   
                        <tr>
                            <td align="left" >
                                <b>
                                    <asp:Label ID="Label1" runat="server" Text="Total Record : " CssClass="Label"></asp:Label>
                                    <asp:Label ID="lblTotalRecord" runat="server" CssClass="Label"></asp:Label></b>
                            </td>
                       <%--     <td align="left" valign="top" width="100%" cssclass="Label">
                                <b>
                                    <asp:UpdateProgress ID="UpdateProgress9" runat="server">
                                        <ProgressTemplate>
                                            Loading...</ProgressTemplate>
                                    </asp:UpdateProgress>
                                </b>
                            </td>--%>
                        </tr>
                    
                   
                    <tr>
                    <td >
                    <div id="GridScroll">
                      <asp:GridView ID="grdFiberMasterQuery" runat="server" 
                            AutoGenerateColumns="False"  Width = "100%"
                            AllowPaging="True" AllowSorting="True" CellPadding="3" BorderStyle="Ridge" CssClass="smallfont"
                            Font-Size="X-Small" ForeColor="#333333" PagerStyle-HorizontalAlign="Left" onpageindexchanging="grUserMasterQuery_PageIndexChanging" 
                            > 
                           <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"  />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                            <Columns> 
                            
                           
                                                             
                                <asp:TemplateField HeaderText="Poy Desc">
                                <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Poydesc
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtpoydesc" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnpoydesc" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go"  OnClick="FilterGrid_Click"  Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                <ItemTemplate>
                                <asp:Label ID="lblFabCode"  runat="server" Text='<%#Eval("FIBER_DESC") %>' ToolTip='<%#Eval("FIBER_DESC") %>' ></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>
                               <%-- <asp:BoundField DataField="FIBER_DESC" HeaderText="Fiber Description" />--%>
                                <asp:TemplateField HeaderText="Poy Cat">
                                <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                   Poy Cat
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtpoycat" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnpoycat" CssClass="SmallFont" runat="server"  ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click"   Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                <ItemTemplate>
                                <asp:Label ID="lblFabCode"  runat="server" Text='<%#Eval("FIBER_CAT") %>' ToolTip='<%#Eval("FIBER_CAT") %>' ></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Poy Sub Cat">
                                <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                               Poy Sub Cat
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtpoysubcat" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnpoysubcat" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go"  OnClick="FilterGrid_Click"  Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                <ItemTemplate>
                                <asp:Label ID="lblFabCode"  runat="server" Text='<%#Eval("SUB_FIBER_CAT") %>' ToolTip='<%#Eval("SUB_FIBER_CAT") %>' ></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>
                                                              
                                <asp:BoundField DataField="LENGTH_TYPE" HeaderText="Length Type" Visible="false" />
                                <asp:TemplateField HeaderText="Filament">
                                <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                   Filament
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtfilament" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnFilament" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go"  OnClick="FilterGrid_Click"  Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                <ItemTemplate>
                                <asp:Label ID="lblFabCode"  runat="server" Text='<%#Eval("LENGTH_VALUE") %>' ToolTip='<%#Eval("LENGTH_VALUE") %>' ></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lusture">
                                <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                   Lusture
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtlusture" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnlusture" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go"  OnClick="FilterGrid_Click"  Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                <ItemTemplate>
                                <asp:Label ID="lblFabCode"  runat="server" Text='<%#Eval("LUSTURE") %>' ToolTip='<%#Eval("LUSTURE") %>' ></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>
                              
                                <asp:TemplateField HeaderText="Denier">
                                <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                  Denier
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtdenier" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btndenier" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go"   OnClick="FilterGrid_Click"  Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                <ItemTemplate>
                                <asp:Label ID="lblFabCode"  runat="server" Text='<%#Eval("DENIER") %>' ToolTip='<%#Eval("DENIER") %>' ></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:BoundField DataField="FANCY_EFFECT" HeaderText="Fancy Effect" Visible="false" />
                                <asp:BoundField DataField="TENACITY" HeaderText="Tenacity" Visible="false"/>
                                 <asp:BoundField DataField="UOM_BAIL" HeaderText="KG/BALE" Visible="false" />
                                <asp:TemplateField HeaderText="Open Rate">
                                <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                Open Rate
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtopenrate" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnopenrate" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go"  OnClick="FilterGrid_Click"  Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                <ItemTemplate>
                                <asp:Label ID="lblFabCode"  runat="server" Text='<%#Eval("OPEN_RATE") %>' ToolTip='<%#Eval("OPEN_RATE") %>' ></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Maximum Stock">
                                <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                              Maximum Stock
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtmaximumstock" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnmaximumstock" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go"  OnClick="FilterGrid_Click"  Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                <ItemTemplate>
                                <asp:Label ID="lblFabCode"  runat="server" Text='<%#Eval("MAXIMUM_STOCK") %>' ToolTip='<%#Eval("MAXIMUM_STOCK") %>' ></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>
                                
                               <asp:TemplateField HeaderText="Remarks">
                                <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                             Remarks
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtremarks" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnremarks" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go"  OnClick="FilterGrid_Click"  Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                <ItemTemplate>
                                <asp:Label ID="lblFabCode"  runat="server" Text='<%#Eval("REMARK") %>' ToolTip='<%#Eval("REMARK") %>' ></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="Party Name">
                                <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                            Party Name
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtpartyname" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnpartyname" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go"  OnClick="FilterGrid_Click"  Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                <ItemTemplate>
                                <asp:Label ID="lblFabCode"  runat="server" Text='<%#Eval("PRTY_NAME") %>' ToolTip='<%#Eval("PRTY_NAME") %>' ></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="Branch">
                                <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                           Branch
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtbranch" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnbranch" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go"   OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                <ItemTemplate>
                                <asp:Label ID="lblFabCode"  runat="server" Text='<%#Eval("BRANCH_NAME") %>' ToolTip='<%#Eval("BRANCH_NAME") %>' ></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>
                                
                                 <%--Start ITCHS--%>
                                
                                <asp:TemplateField HeaderText="SALESITCHSCODE">
                                <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                   Sales ITCHS Code
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtSALESITCHSCODE" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnSALESITCHSCODE" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go"  OnClick="FilterGrid_Click"  Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                <ItemTemplate>
                               <asp:Label ID="lblSALESITCHSCODE"  runat="server" Text='<%#Eval("SALES_ITCHS_CODE") %>' ToolTip='<%#Eval("SALES_ITCHS_CODE") %>' ></asp:Label> 
                                </ItemTemplate>
                                </asp:TemplateField>
                                
                                 <asp:TemplateField HeaderText="CUSTOMITCHSCODE">
                                <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                  Custom ITCHS Code
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtCUSTOMITCHSCODE" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnCUSTOMITCHSCODE" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go"  OnClick="FilterGrid_Click"  Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                <ItemTemplate>
                              <asp:Label ID="lblCUSTOMITCHSCODE"  runat="server" Text='<%#Eval("CUSTOM_ITCHS_CODE") %>' ToolTip='<%#Eval("CUSTOM_ITCHS_CODE") %>' ></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>
                                
                                
                                  <asp:TemplateField HeaderText="ISEXCISABLE">
                                <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                   IS Excisable
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtISEXCISABLE" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnISEXCISABLE" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go"  OnClick="FilterGrid_Click"  Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                <ItemTemplate>
                                <asp:Label ID="lblISEXCISABLE"  runat="server" Text='<%#Eval("IS_EXCISABLE") %>' ToolTip='<%#Eval("IS_EXCISABLE") %>' ></asp:Label> 
                                </ItemTemplate>
                                </asp:TemplateField>

                                
                                <%-- End ITCHS --%>
                                
                                <asp:TemplateField HeaderText="TARIFFHEADING">
                                <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                  Tariff Heading
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtTARIFFHEADING" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnTARIFFHEADING" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go"  OnClick="FilterGrid_Click"  Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                <ItemTemplate>
                               <asp:Label ID="lblITARIFFHEADING"  runat="server" Text='<%#Eval("TARIFF_HEADING") %>' ToolTip='<%#Eval("TARIFF_HEADING") %>' ></asp:Label> 
                                </ItemTemplate>
                                </asp:TemplateField>
                                
                                
                             
                            </Columns>
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                        
                        </div>
                       </td>
                    </tr>
                
                </td>
            </tr>
        </table>
    </ContentTemplate>
<Triggers >
<asp:PostBackTrigger  ControlID="ImageButton1"/>
 </Triggers>
</asp:UpdatePanel>

<style type="text/css">
.gridScroll
{
	
	width:800px;
	overflow:scroll;
}

</style>