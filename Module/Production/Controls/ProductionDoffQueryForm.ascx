<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductionDoffQueryForm.ascx.cs" Inherits="Module_Production_Controls_ProductionDoffQueryForm" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; 
        *display:inline;
        overflow:hidden;
        white-space:nowrap;
        }
    .header
    {
        margin-left: 2px;
    }
    .c1
    {
        width: 60px;
    }
    .c2
    {
        margin-left: 4px;
        width: 80px;
    }
    .c3
    {
        margin-left: 4px;
        width: 120px;
    }
    .c4
    {
        margin-left: 4px;
        width: 200px;
    }
    .c5
    {
        margin-left: 4px;
        width: 150px;
    }
</style>
<%--<asp:UpdatePanel ID="UpdatePanel8971" runat="server">
    <ContentTemplate>--%>
        <table width="100%" class ="td tContentArial">
            <tr>
                <td width="100%">
                    <table >
                        <tr>
                        <td >
                                &nbsp;</td>
                            <td id="tdClear" runat="server" align="left" width="48">
                                <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                                    ToolTip="Clear" Width="48" OnClick="imgbtnClear_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                                    OnClick="imgbtnPrint_Click" ToolTip="Print" Width="48" />
                            </td>
                            <td>  
<asp:ImageButton ID="imgBtnExportExcel" runat="server" Width="48" Height="41" ToolTip="Print"
                            ImageUrl="~/CommonImages/export.png" onclick="imgBtnExportExcel_Click" ></asp:ImageButton>&nbsp;</td> 
                            <td>
                                <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                                    OnClick="imgbtnExit_Click" ToolTip="Exit" Width="48" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                                    ToolTip="Help" Width="48" onclick="imgbtnHelp_Click" />
                            </td>
                        </tr>
                    </table>
                    <table width="100%" class ="td">
                        <tr>
                            <td align="center" valign="top" class="tRowColorAdmin ">
                                <span class="titleheading">Production Doff Query Form</span>
                            </td>
                        </tr>
                    </table>
                    <table width="75%" >
                        <tr>
                            <td align="right" class="style1">
                                Select&nbsp;Branch:
                            </td>
                            <td class="style1">
                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="gCtrTxt " Font-Size="9"
                                    Width="160px" >
                                </asp:DropDownList>
                            </td>
                            <td align="right" class="style2">
                                Dept.: </td>
                            <td class="style1">
                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="gCtrTxt " Font-Size="9"
                                    Width="160px">
                                </asp:DropDownList>
                                <td align="right" class="style2">
                                Doff&nbsp;Id:</td>
                            <td class="style1">
                                <asp:DropDownList ID="ddlBatchNo" runat="server" CssClass="gCtrTxt " Font-Size="9"
                                    Width="160px">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                Lot&nbsp;No:</td>
                            <td >
                                <asp:DropDownList ID="ddlLotNo" runat="server" CssClass="gCtrTxt " Font-Size="9"
                                    Width="160px">
                                </asp:DropDownList>
                            </td>
                              <td>
                                   Machine</td>
                                <td>
                                    <cc1:ComboBox ID="ddlMacCode" runat="server" CssClass="SmallFont"
                            EmptyText="select Machine" EnableLoadOnDemand="true" EnableVirtualScrolling="true"
                            Height="200px" MenuWidth="180px" OnLoadingItems="ddlMacCode_LoadingItems" 
                            Width="160px">
                            <HeaderTemplate>
                                <div class="header c3">
                                    Mac Code</div>
                                <%--<div class="header c2">
                                    Mac Group</div>
                                <div class="header c3 ">
                                    Mac Segement</div>
                                <div class="header c3">
                                    Mac Type</div>
                                <div class="header c3 ">
                                    Mac Section</div>--%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c3">
                                    <asp:Literal ID="Container7" runat="server" Text='<%# Eval("MACHINE_CODE") %>' />&nbsp;
                                    <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("OLD_MACHINE_NAME") %>' />
                                </div>
                               <%-- <div class="item c2">
                                    <asp:Literal ID="Container3" runat="server" Text='<%# Eval("MACHINE_GROUP") %>' />
                                </div>
                                <div class="item c3 ">
                                    <asp:Literal ID="Literal6" runat="server" Text='<%# Eval("MACHINE_SEGMENT") %>' />
                                </div>
                                <div class="item c3">
                                    <asp:Literal ID="Literal7" runat="server" Text='<%# Eval("MACHINE_TYPE") %>' />
                                </div>
                                <div class="item c3 ">
                                    <asp:Literal ID="Literal8" runat="server" Text='<%# Eval("MACHINE_SEC") %>' />
                                </div>--%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc1:ComboBox></td>
                            <td align="right" class="style1">
                                </td>
                            <td >
                            </td>    
                           
                        </tr>
                        <caption>
                            <br />
                            <tr>
                                <td align="right">
                                    From&nbsp;Date: </td>
                                <td>
                        <asp:TextBox ID="TxtFromDate" runat="server" TabIndex="6" Width="145px" CssClass="SmallFont"   ></asp:TextBox><%--OnTextChanged="TxtFromDate_TextChanged"--%>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TxtFromDate" Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>                    
                                </td>
                                <td>
                                    To&nbsp;Date:</td>
                                <td>
                        <asp:TextBox ID="TxtToDate" runat="server" TabIndex="7" Width="145px" CssClass="SmallFont"
                           ></asp:TextBox>  <%--AutoPostBack="true"  OnTextChanged="TxtToDate_TextChanged"--%>
               <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TxtToDate" Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>                  
                                
                                </td>
                                <td>
                                    Lot&nbsp;Type:</td>
                                <td>
            <asp:DropDownList Width="160px" TabIndex="2"
                ID="ddlLotType" runat="server"  AppendDataBoundItems="True">
                <asp:ListItem id="select0" Value="" Text="------------Select------------"></asp:ListItem>
                <asp:ListItem id="Domastic" Text="Domastic"></asp:ListItem>
                <asp:ListItem id="Export" Text="Export"></asp:ListItem>
            </asp:DropDownList>
                                </td>
                                <td>
                                    Merge&nbsp;No:</td>
                                <td>
                                <asp:DropDownList ID="ddlMergeNo" runat="server" CssClass="gCtrTxt " Font-Size="9"
                                    Width="160px">
                                </asp:DropDownList>
                                </td>
                                <td>Prod.&nbsp;Id:</td>
                                <td
                                </td>
                                <asp:DropDownList ID="ddlProdPocsIdNo" runat="server" CssClass="gCtrTxt " Font-Size="9"
                                    Width="160px">
                                </asp:DropDownList>
                                <td>
                                    &nbsp;</td>
                                <td></td>
                            </tr>
                            <caption>
                                <br />
                                <tr>
                                <td align="right" id="tdOrderNo1" runat="server" visible="false">
                                    Order No:</td>
                                <td id="tdOrderNo2" runat="server" visible="false">
                                <asp:DropDownList ID="ddlOrderNo" runat="server" CssClass="gCtrTxt " Font-Size="9"
                                    Width="160px">
                                </asp:DropDownList>
                                
                                    </td>
                                    <td align="right" class="style2">
                                Finish&nbsp;Type:</td>
                            <td >
            <asp:DropDownList Width="150px" TabIndex="2" 
                ID="ddlFinishType" runat="server" AppendDataBoundItems="True">
                <asp:ListItem id="select1" Value="" Text="----------Select-----------"></asp:ListItem>
                <asp:ListItem id="Soft" Text="Soft"></asp:ListItem>
                <asp:ListItem id="Hard" Text="Hard"></asp:ListItem>
            </asp:DropDownList>
                            </td>
                              
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <asp:Button ID="btnsave" runat="server" CssClass="AButton" 
                                        OnClick="btnsave_Click" Text="Get Record" Width="85px" /></td>
                                <td>&nbsp;</td>
                                <td
                                </td>
                                    &nbsp;<td>
                                        &nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                                <tr>
                                    <td colspan="4" width="50%">
                                        <b>Total&nbsp;Records: </b>
                                        <asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
                                        <asp:Label ID="lblMode" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="4" width="50%">
                                        <asp:UpdateProgress ID="UpdateProgress9587" runat="server">
                                            <ProgressTemplate>
                                                <h3>
                                                    Loading...</h3>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                            </caption>
                        </caption>
                    </table>
                    <table  width="100%" >
                        <tr>
                            <td align="left">
                                <asp:Panel ID="pnlShowHover" runat="server" ScrollBars="Auto" Width="100%">
                                
                                
                                <asp:GridView ID="Grid1" runat="server" AutoGenerateColumns="False" 
                                        HeaderStyle-Font-Bold="true" Width="80%"
                                          CellPadding="3" ForeColor="#333333" GridLines="Both"
                                        BorderStyle="Ridge" Font-Names="Arial" Font-Size="X-Small"  
                                        ShowFooter="true" >
                                       <%-- OnPageIndexChanging="Grid1_PageIndexChanging"--%>                                       
                                         <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                                        <Columns>                                          
                                            <asp:BoundField DataField="YEAR" HeaderText="Year" Visible="false" />
                                            <asp:TemplateField HeaderText="Date">
                                             <ItemTemplate>
                                            <asp:Label ID="lblDoffDate" runat="server" CssClass="Label SmallFont" Text='<%# Bind("DOFF_DATE","{0:dd/MM/yyyy}") %>' Font-Bold="true"></asp:Label>
                                             </ItemTemplate>               
                                            </asp:TemplateField>   
                                           
                                             <asp:BoundField DataField="BRANCH_NAME" HeaderStyle-HorizontalAlign="Left" HeaderText="Branch" ItemStyle-HorizontalAlign="Left"/>
                                             <asp:BoundField DataField="DEPT_NAME" HeaderStyle-HorizontalAlign="Left" HeaderText="Dept." />
                                              <asp:BoundField DataField="PROD_ID" HeaderStyle-HorizontalAlign="Left" HeaderText="Prod.&nbsp;No"  ItemStyle-HorizontalAlign="Left"/> 
                                              <asp:BoundField DataField="ORDER_NO" HeaderText="Production&nbsp;Order&nbsp;No" />
                                              <asp:BoundField DataField="LOT_NUMBER" HeaderText="Lot&nbsp;No" />
                                              <asp:BoundField DataField="MERGE_NO" HeaderText="Merge&nbsp;No" />                                         
                                                                                
                                             <asp:BoundField DataField="YARN_CODE" HeaderText="Yarn&nbsp;Code" />
                                             <asp:BoundField DataField="YARN_DESC" HeaderText="Yarn&nbsp;Description"  />  
                                            <asp:TemplateField HeaderText="Doff&nbsp;ID">
                                             <ItemTemplate>
                                            <asp:Label ID="lblDoff" runat="server" CssClass="Label SmallFont" Text='<%# Bind("DOFF_ID") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                             <asp:Label ID="lblTotalDoff" runat="server" CssClass="Label SmallFont" ForeColor="White" ></asp:Label>                                           
                                            </FooterTemplate>                                     
                                            </asp:TemplateField>   
                                                <asp:TemplateField HeaderText="Total&nbsp;Doffs">
                                             <ItemTemplate>
                                            <asp:Label ID="lblDoffNo" runat="server" CssClass="Label SmallFont" Text='<%# Bind("TOTAL_DOFF_NO") %>' Font-Bold="true"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                             <asp:Label ID="lblTotalDoffNo" runat="server" CssClass="Label SmallFont" Font-Bold="true" ForeColor="White" ></asp:Label>                                           
                                            </FooterTemplate>                                     
                                            </asp:TemplateField>   
                                            
                                              <asp:BoundField DataField="DOFF_START_DATE" HeaderText="Doff&nbsp;Start&nbsp;Time" />
                                              <asp:BoundField DataField="DOFF_END_DATE" HeaderText="Doff&nbsp;End&nbsp;Time" />   
                                            
                                            <asp:TemplateField HeaderText="Doff&nbsp;Prd.&nbsp;Qty">
                                             <ItemTemplate>
                                            <asp:Label ID="lblProductionQty" runat="server" CssClass="Label SmallFont" Text='<%# Bind("DOFF_PRODUCTION_QTY") %>' ForeColor="Green" Font-Bold="true"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                             <asp:Label ID="lblTotalProductionQty" runat="server" CssClass="Label SmallFont" ForeColor="White" Font-Bold="true"></asp:Label>                                           
                                            </FooterTemplate>                                     
                                            </asp:TemplateField>                                                 
                                             <asp:TemplateField HeaderText="Cheeses">
                                             <ItemTemplate>
                                            <asp:Label ID="lblProductionCops" runat="server" CssClass="Label SmallFont" Text='<%# Bind("PRODUCTION_NO_OF_COPS") %>' ForeColor="Blue" Font-Bold="true"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                             <asp:Label ID="lblTotalProductionCops" runat="server" CssClass="Label SmallFont"  ForeColor="White" Font-Bold="true"></asp:Label>                                           
                                            </FooterTemplate>                                     
                                            </asp:TemplateField>                                             
                                            <asp:BoundField DataField="PRODUCTION_COPS_AVG_WT" HeaderText="Cheeses&nbsp;Avg&nbsp;Wt" /> 
                                            
                                               <asp:TemplateField HeaderText="Pack&nbsp;Qty">
                                             <ItemTemplate>
                                            <asp:Label ID="lblPackQty" runat="server" CssClass="Label SmallFont" Text='<%# Bind("PACKING_QTY") %>' ForeColor="Green" Font-Bold="true"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                             <asp:Label ID="lblTotalPackQty" runat="server" CssClass="Label SmallFont" ForeColor="White" Font-Bold="true"></asp:Label>                                           
                                            </FooterTemplate>                                     
                                            </asp:TemplateField>                                                 
                                             <asp:TemplateField HeaderText="Pack&nbsp;Cops">
                                             <ItemTemplate>
                                            <asp:Label ID="lblPackCops" runat="server" CssClass="Label SmallFont" Text='<%# Bind("PACKED_NO_OF_COPS") %>' ForeColor="Blue" Font-Bold="true"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                             <asp:Label ID="lblTotalPackCops" runat="server" CssClass="Label SmallFont"  ForeColor="White" Font-Bold="true"></asp:Label>                                           
                                            </FooterTemplate>                                     
                                            </asp:TemplateField> 
                                                                                                                   
                                            <asp:BoundField DataField="UOM" HeaderText="UOM" />   
                                            <asp:BoundField DataField="MACHINE_CODE" HeaderText="Machine" />                                                 
                                              <asp:BoundField DataField="LOT_TYPE" HeaderText="Lot&nbsp;Type" /> 
                                             <asp:BoundField DataField="QUALITY" HeaderText="Quality" />        
                                             <asp:BoundField DataField="FINISH_TYPE" HeaderText="Finish&nbsp;Type" />   
                                             <asp:BoundField DataField="STOP_TIME" HeaderText="Stop&nbsp;Time" />        
                                             <asp:BoundField DataField="REMARKS" HeaderText="Remarks" />                                                
                                           <asp:BoundField DataField="AIR_PRESSURE" HeaderText="Air Pressure"  Visible="false"/>
                                            <asp:BoundField DataField="ROTO_JET_NO" HeaderText="Roto Jet No" Visible="false" />     
                                         <asp:BoundField DataField="OPERATOR" HeaderText="Operator"  Visible="false"/>
                                             <asp:BoundField DataField="SUPERVISOR" HeaderText="Supervisor" Visible="false" />
                                              <asp:BoundField DataField="SFT_ID" HeaderText="SFT Id"  Visible="false"/>     
                                         <asp:BoundField DataField="USER_NAME" HeaderText="User Name" Visible="false" /> 
                                                                         
                                         </Columns>
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                                    </asp:GridView>
                                 </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    <%--</ContentTemplate>--%>