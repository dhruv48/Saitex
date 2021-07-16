<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LOT_MERGE_WISE_DEPT_MACHINE_PRODUCTION_SUMMARY.ascx.cs" Inherits="Module_Production_Controls_LOT_MERGE_WISE_DEPT_MACHINE_PRODUCTION_SUMMARY" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc1" %>
<%--<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
--%><%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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
        width: 150px;
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

<%--<asp:UpdatePanel ID="UpdatePanel8971" runat="server"> <ContentTemplate>--%>
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
                                    OnClick="imgbtnPrint_Click" ToolTip="Print" Width="48"  Visible="false"/>
                            </td>
                              <td>
                        <asp:ImageButton ID="imgbtnExport" runat="server" Height="41" ImageUrl="~/CommonImages/export.png"
                            ToolTip="Print" Width="48" OnClick="imgbtnExport_Click" />
                    </td>
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
                                <span class="titleheading">MERGEWISE DEPT,MACHINE AND PRODUCTION SUMMARY WITH LOT NO</span>
                            </td>
                        </tr>
                    </table>
                    <table width="90%" >
                        <tr>
                            <td align="right" >
                                Select&nbsp;Branch:
                            </td>
                            <td >
                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="gCtrTxt " Font-Size="9"
                                    Width="160px" >
                                </asp:DropDownList>
                            </td>
                            <td align="right" >
                                Department: </td>
                            <td >
                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="gCtrTxt " Font-Size="9"
                                    Width="160px">
                                </asp:DropDownList>
                               <%-- <td align="right" >  Lot&nbsp;No:</td>--%>
                            <td runat="server" visible="false" id="tdidlot" >
             <cc1:ComboBox ID="txtLot" runat="server" CssClass="smallfont" Width="151px" EnableLoadOnDemand="True"
                DataTextField="LOT_NO" DataValueField="LOT_TYPE" MenuWidth="150px" EnableVirtualScrolling="true"
                OpenOnFocus="true" Visible="true" Height="200px" EmptyText="------------All----------"
                OnLoadingItems="txtLot_LoadingItems" AutoPostBack="True">
                
                <HeaderTemplate>
                    <div class="header c2">LOT NO</div>
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="item c2"><%# Eval("LOT_NO")%></div>
                </ItemTemplate>
                <FooterTemplate>
                    Displaying items
                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                    out of
                    <%# Container.ItemsCount %>.
                    
                </FooterTemplate>
                
            </cc1:ComboBox>
                            </td>
                            <td align="right" class="style4">
                                &nbsp;</td>
                            <td >
                                &nbsp;</td>
                            <td align="right" >
                                Merge No: </td>
                            <td class="style4">
                        
                        <cc1:ComboBox ID="txtYCODE" runat="server" CssClass="smallfont" Width="161px" MenuWidth="350px" EnableLoadOnDemand="true" EnableVirtualScrolling="true"
                         Visible="true" Height="200px" OnLoadingItems="txtYCODE_LoadingItems" AutoPostBack="True" EmptyText="------------All----------">
                       <HeaderTemplate>
                            <div class="header c2">
                                MERGE CODE</div>
                           <div class="header c3">
                                FIBER DESCRIPTION</div> 
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="item c2">
                                <%# Eval("FIBER_CODE")%></div>
                            <div class="item c3">
                                <%# Eval("FIBER_DESC") %></div>
                            
                        </ItemTemplate>
                      
                         <FooterTemplate>
                            Displaying items
                            <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                            out of
                            <%# Container.ItemsCount %>.
                        </FooterTemplate>
                    </cc1:ComboBox>
                        </td>
                            <td align="right" >
                            <asp:CheckBox ID="ChkStock" runat=server Text="Bal WIP" TextAlign="Right" Checked="true" Visible="false"/>
                            </td>
                            
                        
                            <br />
                            <tr>
                                <td align="right" >
                                    From&nbsp;Date: </td>
                                <td >
                        <asp:TextBox ID="TxtFromDate" runat="server" TabIndex="6" Width="145px" CssClass="SmallFont"   ></asp:TextBox><%--OnTextChanged="TxtFromDate_TextChanged"--%>
              <cc1:CalendarExtender ID="ce1" runat="server" TargetControlID="TxtFromDate" PopupPosition="TopLeft"
                            Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>                
                                </td>
                                <td  align ="right">
                                    To&nbsp;Date: </td>
                                <td >
                        <asp:TextBox ID="TxtToDate" runat="server" TabIndex="7" Width="145px" CssClass="SmallFont"
                           ></asp:TextBox> <%--AutoPostBack="true"  OnTextChanged="TxtToDate_TextChanged" --%>
                        <cc1:CalendarExtender ID="ce2" runat="server" TargetControlID="TxtToDate" Format="dd/MM/yyyy"
                            PopupPosition="TopLeft">
                        </cc1:CalendarExtender>                
                                
                                </td>
                               <%-- <td >
                                    Party: </td>--%>
                                <td visible="false" id="tdidparty" runat="server">
                                    <cc1:ComboBox ID="txtPartyCODE" runat="server" CssClass="smallfont" Width="161px"
                                        MenuWidth="350px" EnableLoadOnDemand="true" EnableVirtualScrolling="true" Visible="true"
                                        Height="200px" OnLoadingItems="txtPartyCODE_LoadingItems" AutoPostBack="True"
                                        EmptyText="------------All----------">
                                        <HeaderTemplate>
                                            <div class="header c2">
                                                PARTY CODE</div>
                                            <div class="header c4">
                                                PARTY NAME</div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="item c2">
                                                <%# Eval("PRTY_CODE")%></div>
                                            <div class="item c4">
                                                <%# Eval("PRTY_NAME") %></div>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Displaying items
                                            <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                            out of
                                            <%# Container.ItemsCount %>.
                                        </FooterTemplate>
                                    </cc1:ComboBox>
                                </td>
                                <td >
                                </td>
                                <td >
                                    </td>
                                <td align="right">Product&nbsp;Type:</td>
                                <td>
                             
                                 <asp:DropDownList Width="160px" TabIndex="2" CssClass="SmallFont TextBox UpperCase"
                ID="ddlProductType" runat="server" AutoPostBack="True" AppendDataBoundItems="True">
               <asp:ListItem id="Select" Text="-----------Select-----------  "></asp:ListItem>
                <asp:ListItem id="TEXTURISING" Text="TEXTURISING"></asp:ListItem>
                <asp:ListItem id="TWISTING" Text="TWISTING"></asp:ListItem>
                <asp:ListItem id="DYENING" Text="DYEING"></asp:ListItem>
            </asp:DropDownList>
                                </td>
                                <td >
                                    <asp:Button ID="btnsave" runat="server" CssClass="AButton" Height="22px" 
                                        OnClick="btnsave_Click" Text="Get Record" Width="85px" />
                                </td>
                            </tr>
                            <caption>
                       
               
                        
                        </caption>
                        </tr>
                            <caption>
                                <br />
                                <tr>
                                    <td colspan="4" width="50%">
                                        <b>Total&nbsp;Records: : </b>
                                        <asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
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
                       
                        
                    </table>
                 
                                       
                                         
                                         
                 
                    
                    <table  width="100%" >
                        <tr>
                            <td align="left">
                                <asp:Panel ID="pnlShowHover" runat="server" ScrollBars="Auto" Width="100%">
                                    <asp:GridView ID="Grid1" runat="server" AutoGenerateColumns="False" 
                                        HeaderStyle-Font-Bold="true" Width="90%"
                                        CellPadding="3" ForeColor="#333333" GridLines="Both"
                                        BorderStyle="Ridge" Font-Names="Arial" Font-Size="X-Small" 
                                        onpageindexchanging="Grid1_PageIndexChanging"    OnRowDataBound="RowDataBound" > <%--AllowPaging="true" PageSize="20"--%>
                                       
                                       <emptydatarowstyle backcolor="LightBlue" forecolor="Red"/>
                                         <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />

                                        <Columns>                   
                                                                                   
                                          
                         <asp:TemplateField HeaderText="Merge No" >
                          <ItemTemplate>
                            <asp:Label  ID="lblMerge_No" runat="server" ReadOnly="true" Text='<%# Bind("MERGE_NO") %>'     ></asp:Label>
                          </ItemTemplate>                      
                        </asp:TemplateField>
                                          <%--<asp:BoundField DataField="MERGE_NO" NullDisplayText="0" HeaderText="Merge No"/>--%>
                                          <asp:BoundField DataField="FIBER_CODE" NullDisplayText="0" HeaderText="POY Code"/>
                                          <asp:BoundField DataField="FIBER_DESC" NullDisplayText="0" HeaderText="POY Description"/>
                                          <asp:BoundField DataField="DEPT_ISSUE_QTY" NullDisplayText="0" HeaderText="Dept.&nbsp;Issue&nbsp; QTY"/>
                                            <asp:BoundField DataField="DEPT_ISSUE_COPS" NullDisplayText="0" HeaderText="Dept.&nbsp;Issue&nbsp;Cops" />  
                                   
                                          
                        
                                          <asp:BoundField DataField="MACHINE_ISSUE_QTY" NullDisplayText="0" HeaderText="Machine&nbsp;Issue&nbsp; QTY"/>
                                           <asp:BoundField DataField="MACHINE_ISSUE_COPS" NullDisplayText="0" HeaderText="Machine&nbsp;Issue&nbsp;Cops"/>
                                           <%-- <asp:BoundField DataField="DOFF_QTY" NullDisplayText="0" HeaderText="DOFF&nbsp;Production" />--%>
                                            
                                            <asp:TemplateField HeaderText="Doff&nbsp;Production" ItemStyle-HorizontalAlign="Right">
                                           <ItemTemplate>
                                            
                                          <asp:GridView ID="grdPRODUCTION" runat="server" AutoGenerateColumns="False"  Width="98%">
                                        <Columns>
                                      
                                                       <asp:TemplateField HeaderText="LOT&nbsp;NO">                                                   
                                                    <ItemTemplate>
                                                       <asp:Label ID="lblLotNo" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("LOT_NO") %>'></asp:Label>
                                                    </ItemTemplate>   
                                                                                                 
                                                </asp:TemplateField>  
                                                  <asp:TemplateField HeaderText="PROD&nbsp;QTY" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLotQty" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("PROD_QTY") %>'></asp:Label>
                                                    </ItemTemplate>                                                                                                    
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="COPS" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProdCops" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("PROD_COPS") %>'></asp:Label>
                                                    </ItemTemplate>                                                                                                    
                                                </asp:TemplateField>
                                        </Columns>  
                                        <HeaderStyle  Font-Bold="True"  HorizontalAlign="Center"     VerticalAlign="Top" />                                   
                                    </asp:GridView>
                                     <asp:Label  ID="lblDoff_Qty" runat="server" ReadOnly="true" Text='<%# Bind("DOFF_QTY") %>'   Font-Bold="true"   ></asp:Label>/
                                      <asp:Label  ID="lblDoff_Cops" runat="server" ReadOnly="true" Text='<%# Bind("PRODUCTION_COPS") %>'   Font-Bold="true"   ></asp:Label>
                                           </ItemTemplate>
                                       
                                          </asp:TemplateField>
                                          
                                         <%-- <asp:BoundField DataField="ACTUAL_PACKING" NullDisplayText="0" HeaderText="Packing"/>--%>
                                             <asp:TemplateField HeaderText="Packing" ItemStyle-HorizontalAlign="Right">
                                           <ItemTemplate>
                                            
                                          <asp:GridView ID="grdPACKING" runat="server" AutoGenerateColumns="False" Width="98%">
                                        <Columns>
                                      
                                                       <asp:TemplateField HeaderText="LOT&nbsp;NO">                                                   
                                                    <ItemTemplate>
                                                       <asp:Label ID="lblLotNo1" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("LOT_NO") %>'></asp:Label>
                                                    </ItemTemplate>   
                                                                                                 
                                                </asp:TemplateField>  
                                                  <asp:TemplateField HeaderText="PACK&nbsp;QTY" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPackQty" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("PACK_QTY") %>'></asp:Label>
                                                    </ItemTemplate>                                                                                                    
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="COPS" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPackCops" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("PACK_COPS") %>'></asp:Label>
                                                    </ItemTemplate>                                                                                                    
                                                </asp:TemplateField>
                                                
                                        </Columns>
                                         <HeaderStyle  Font-Bold="True" HorizontalAlign="Center"     VerticalAlign="Top" />
                                    </asp:GridView>
                                     <asp:Label  ID="lblPacking_Qty" runat="server" ReadOnly="true" Text='<%# Bind("ACTUAL_PACKING") %>'  Font-Bold="true"    ></asp:Label>/
                                      <asp:Label  ID="lblPacking_Cops" runat="server" ReadOnly="true" Text='<%# Bind("PACKED_COPS") %>'  Font-Bold="true"    ></asp:Label>
                                           </ItemTemplate>
                                       
                                          </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center"/>
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"     VerticalAlign="Top" />
                                        <RowStyle    HorizontalAlign="Center"          VerticalAlign="Top" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>  
       </td>
            </tr>
        </table>            
  <%-- </ContentTemplate>--%>