<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DEPT_MACHINE_ISSUE_PALLET_WISE_QUERY.ascx.cs" Inherits="Module_Production_Controls_DEPT_MACHINE_ISSUE_PALLET_WISE_QUERY" %>
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
                        
                            <td id="tdClear" runat="server" align="left" width="48">
                                <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                                    ToolTip="Clear" Width="48" OnClick="imgbtnClear_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                                    OnClick="imgbtnPrint_Click" ToolTip="Print" Width="48" />
                            </td>
                            <td>  
                              <%-- <asp:ImageButton ID="imgBtnExportExcel" runat="server" Width="48" Height="41" ToolTip="Print"
                            ImageUrl="~/CommonImages/export.png" onclick="imgBtnExportExcel_Click" ></asp:ImageButton>&nbsp;--%>
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
                            <td >
                            </td>
                        </tr>
                    </table>
                    <table width="100%" class ="td">
                        <tr>
                            <td align="center" valign="top" class="tRowColorAdmin ">
                                <span class="titleheading">Machine Issue Details Pallet Wise Against Department Issue</span>
                            </td>
                        </tr>
                    </table>
                    <table width="75%" >
                       <%-- <tr>
                            <td align="right" class="style1">
                                Select&nbsp;Branch:
                            </td>
                            <td class="style3">
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
                                Process&nbsp;Id&nbsp;No:</td>
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
                            
                                <td align="right"> Machine:
                                   </td>
                                <td class="style4">
                                     <cc1:ComboBox ID="ddlMacCode" runat="server" CssClass="SmallFont"
                            EmptyText="select Machine" EnableLoadOnDemand="true" EnableVirtualScrolling="true"
                            Height="200px" MenuWidth="650px" OnLoadingItems="ddlMacCode_LoadingItems" 
                            Width="160px">
                            <HeaderTemplate>
                                <div class="header c3">
                                    Mac Code</div>
                                <div class="header c2">
                                    Mac Group</div>
                                <div class="header c3 ">
                                    Mac Segement</div>
                                <div class="header c3">
                                    Mac Type</div>
                                <div class="header c3 ">
                                    Mac Section</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c3">
                                    <asp:Literal ID="Container7" runat="server" Text='<%# Eval("MACHINE_CODE") %>' />
                                </div>
                                <div class="item c2">
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
                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc1:ComboBox> 
                                </td>
                           
                        </tr>
                      --%>
                        
                        <tr>
                        <td align="right" class="style1">
                                Pallet&nbsp;No:
                            </td>
                            <td class="style3">
                                <%--<asp:DropDownList ID="ddlBranch" runat="server" CssClass="gCtrTxt " Font-Size="9"
                                    Width="160px" >
                                </asp:DropDownList>--%>
                                <asp:TextBox ID="txtPalletNo" runat="server" TabIndex="7" Width="145px" CssClass="SmallFont"></asp:TextBox>
                            </td>
                            <td align="right" class="style1">
                                Dept.&nbsp;Issue&nbsp;No:
                            </td>
                            <td class="style3">
                                <%--<asp:DropDownList ID="DropDownList1" runat="server" CssClass="gCtrTxt " Font-Size="9"
                                    Width="160px" >
                                </asp:DropDownList>--%>
                                <asp:TextBox ID="txtDeptIssNo" runat="server" TabIndex="7" Width="145px" CssClass="SmallFont"></asp:TextBox>
                            </td>
                            <td align="right" class="style1">
                                Machine&nbsp;Issue&nbsp;No:
                            </td>
                            <td class="style3">
                               <%-- <asp:DropDownList ID="DropDownList2" runat="server" CssClass="gCtrTxt " Font-Size="9"
                                    Width="160px" >
                                </asp:DropDownList>--%>
                                <asp:TextBox ID="txtMachineNo" runat="server" TabIndex="7" Width="145px" CssClass="SmallFont"></asp:TextBox>
                            </td>
                            
                        <td>&nbsp;Merge&nbsp;No:</td>
                        <td>
                        <cc1:ComboBox ID="txtYCODE" runat="server" CssClass="smallfont" Width="161px" EnableLoadOnDemand="True"
                        DataTextField="FIBER_CODE" DataValueField="FIBER_CODE" MenuWidth="350px" EnableVirtualScrolling="true"
                        Height="200px" OnLoadingItems="txtYCODE_LoadingItems"
                        EmptyText="------------All----------" >  <%--onselectedindexchanged="txtYCODE_SelectedIndexChanged" OpenOnFocus="true"Visible="true" --%>
                        <HeaderTemplate>
                            <div class="header c2">
                                MERGE&nbsp;CODE</div>
                            <div class="header c3">
                                FIBER&nbsp;DESCRIPTION</div>  
                            <%--<div class="header d3">
                                TYPE</div>--%>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="item c2">
                                <%# Eval("FIBER_CODE")%></div>
                            <div class="item c3">
                                <%# Eval("FIBER_DESC") %></div>
                            <%--<div class="item d3">
                                <%# Eval("YARN_TYPE")%></div>--%>
                        </ItemTemplate>
                       
                         <FooterTemplate>
                            Displaying items
                            <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                            out of
                            <%# Container.ItemsCount %>.
                        </FooterTemplate>
                    </cc1:ComboBox>
                        </td>
                       
                        </tr>
                            <tr>
                                <td align="right">
                                    Dept.&nbsp;Iss.&nbsp;From&nbsp;Date: </td>
                                <td class="style4">
                        <asp:TextBox ID="TxtFromDate" runat="server" TabIndex="6" Width="145px" CssClass="SmallFont"  ></asp:TextBox><%--OnTextChanged="TxtFromDate_TextChanged" --%>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TxtFromDate" Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>                    
                                </td>
                                <td>
                                    Dept.&nbsp;Iss.&nbsp;To&nbsp;Date:</td>
                                <td>
                        <asp:TextBox ID="TxtToDate" runat="server" TabIndex="7" Width="145px" CssClass="SmallFont"
                            ></asp:TextBox> <%--AutoPostBack="true"  OnTextChanged="TxtToDate_TextChanged" --%>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TxtToDate" Format="dd/MM/yyyy">
                        </cc1:CalendarExtender> 
                                </td>
                                <td align="right">
                                   <%-- Machine.&nbsp;Iss.&nbsp;From&nbsp;Date: </td>--%>
                                <td class="style4">
                                 <asp:Button ID="btnsave" runat="server" CssClass="AButton"
                                        OnClick="btnsave_Click" Text="Get Record" Width="85px" />
                        <asp:TextBox ID="txtMachIssFrom" runat="server" TabIndex="8" Width="145px" CssClass="SmallFont" Visible="false" ></asp:TextBox><%--OnTextChanged="TxtFromDate_TextChanged" --%>
                          <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtMachIssFrom" Format="dd/MM/yyyy">
                          </cc1:CalendarExtender>                    
                                </td>
                                <td>
                                  <%--  Machine.&nbsp;Iss.&nbsp;To&nbsp;Date:</td>--%>
                                 
                                <td>
                        <asp:TextBox ID="txtMachIssTo" runat="server" TabIndex="7" Width="145px" CssClass="SmallFont" Visible="false"
                            ></asp:TextBox> <%--AutoPostBack="true"  OnTextChanged="TxtToDate_TextChanged" --%>
                        <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtMachIssTo" Format="dd/MM/yyyy">
                        </cc1:CalendarExtender> 
                                </td>
                            </tr>
                                <td align="right">
                                   </td>
                                
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
                         
                    </table>
                    <table  width="100%" >
                        <tr>
                            <td align="left">
                                <%--<asp:Panel ID="pnlShowHover" runat="server" ScrollBars="Auto" Width="100%">--%>
                                    <asp:GridView ID="Grid1" runat="server" AutoGenerateColumns="False" HeaderStyle-Font-Bold="true"
                                        Width="80%" CellPadding="3" ForeColor="#333333" GridLines="Both" BorderStyle="Ridge"
                                        Font-Names="Arial" Font-Size="X-Small" ShowFooter="true" onpageindexchanging="Grid1_PageIndexChanging"
                                        >
                                        <%-- OnPageIndexChanging="Grid1_PageIndexChanging"--%>
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                                        <Columns>
                                           
                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="2%">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="Dept&nbsp;Iss&nbsp;Date" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDeptIssDate" Text='<%#Eval("DEPT_ISSUE_DATE","{0:dd/mm/yyyy}")%>' runat="server" Visible="true"></asp:Label>
                                                
                                                </ItemTemplate>
                                                <ItemStyle Width="3%" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                         
                                            <asp:BoundField DataField="DEPT_ISSUE_SLIP_NO" HeaderText="Dept&nbsp;Iss&nbsp;Slip&nbsp;No" />
                                           
                                            <asp:BoundField DataField="MERGE_NO" HeaderText="Merge&nbsp;No" />
                                            <asp:BoundField DataField="PALLET_CODE" HeaderText="Pallet&nbsp;Code" />
                                            <asp:BoundField DataField="PALLET_NO" HeaderText="Pallet&nbsp;No" />
                                            <%--<asp:BoundField DataField="ISSUE_QTY" HeaderText="Issue&nbsp;Qty." />--%>
                                            <asp:TemplateField HeaderText="Issue&nbsp;Qty." Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIssQty" Text='<%#Eval("ISSUE_QTY")%>' runat="server" Visible="true"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="3%" HorizontalAlign="Left" />
                                                <FooterTemplate>
                                             <asp:Label ID="lblTotalIssQty" runat="server" CssClass="Label SmallFont" ForeColor="White" Font-Bold="true"></asp:Label>                                           
                                            </FooterTemplate> 
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="NO_OF_UNIT" HeaderText="No&nbsp;Of&nbsp;Unit" />
                                            <asp:BoundField DataField="M_ISSUE_QTY" HeaderText="Machine&nbsp;Iss&nbsp;Qty" />
                                            <asp:BoundField DataField="M_ISSUE_COPS" HeaderText="Machine&nbsp;Iss&nbsp;Cops" />
                                            <asp:TemplateField HeaderText="Machine&nbsp;Iss&nbsp;Date" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMachIssDate" Text='<%#Eval("MACHINE_ISSUE_DATE","{0:dd/mm/yyyy}")%>' runat="server" Visible="true"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="3%" HorizontalAlign="Left" />
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Machine&nbsp;Iss&nbsp;No" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMachIssNo" Text='<%#Eval("MACHINE_ISSUE_NO")%>' runat="server" Visible="true"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="3%" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Machine&nbsp;Iss&nbsp;QTY" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMachIssQty" Text='<%#Eval("MACHINE_ISSUE_QTY")%>' runat="server" Visible="true"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="3%" HorizontalAlign="Left" />
                                                <FooterTemplate>
                                             <asp:Label ID="lblTotalMachIssQty" runat="server" CssClass="Label SmallFont" ForeColor="White" Font-Bold="true"></asp:Label>                                           
                                            </FooterTemplate> 
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Machine&nbsp;Iss&nbsp;COPS" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMachIsscops" Text='<%#Eval("MACHINE_ISSUE_COPS")%>' runat="server" Visible="true"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="3%" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <%-- <asp:BoundField DataField="FR_BATCH_NO" HeaderText="Fr Lot No" />                                          
                                              <asp:BoundField DataField="TO_BATCH_NO" HeaderText="To Lot No" />--%>
                                            
                                        </Columns>
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                            VerticalAlign="Middle" />
                                    </asp:GridView>
                                <%--</asp:Panel>--%>
                            </td>
                        </tr>
                    </table>
                    
                </td>
            </tr>
        </table>
        
    <%--</ContentTemplate>--%>