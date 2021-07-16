<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Machine_Issue_Details_DateWise.ascx.cs" Inherits="Module_Production_Controls_Machine_Issue_Details_DateWise" %>
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
<table width="100%" class="td tContentArial">
    <tr>
        <td width="100%">
            <table>
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
                            ToolTip="Help" Width="48" OnClick="imgbtnHelp_Click" />
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            <table width="100%" class="td">
                <tr>
                    <td align="center" valign="top" class="tRowColorAdmin ">
                        <span class="titleheading">Machine Issue Details Date Wise (Nominal V/S Actual)</span>
                    </td>
                </tr>
            </table>
            <table width="75%">
                <tr>
                    <td align="right" class="style1">
                        Issue&nbsp;From&nbsp;Date:
                    </td>
                    <td class="style4">
                        <asp:TextBox ID="TxtFromDate" runat="server" TabIndex="6" Width="125px" CssClass="SmallFont"></asp:TextBox><%--OnTextChanged="TxtFromDate_TextChanged" --%>
                        <cc1:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="TxtFromDate"
                            Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                    </td>
                    <td align="right" class="style3">
                        Issue&nbsp;To&nbsp;Date:
                    </td>
                    <td class="style3">
                        <asp:TextBox ID="TxtToDate" runat="server" TabIndex="7" Width="125px" CssClass="SmallFont"></asp:TextBox>
                        <%--AutoPostBack="true"  OnTextChanged="TxtToDate_TextChanged" --%>
                        <cc1:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="TxtToDate"
                            Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                    </td>
                    <td align="right" class="style1">
                        Issue&nbsp;No:
                    </td>
                    <td class="style3">
                        <%-- <asp:DropDownList ID="DropDownList2" runat="server" CssClass="gCtrTxt " Font-Size="9"
                                    Width="160px" >
                                </asp:DropDownList>--%>
                        <asp:TextBox ID="txtMachIssNo" runat="server" TabIndex="7" Width="125px" CssClass="SmallFont"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;Merge&nbsp;No:
                    </td>
                    <td>
                        <cc1:ComboBox ID="txtYCODE" runat="server" CssClass="smallfont" Width="131px" EnableLoadOnDemand="True"
                            DataTextField="FIBER_CODE" DataValueField="FIBER_CODE" MenuWidth="350px" EnableVirtualScrolling="true"
                            Height="200px" OnLoadingItems="txtYCODE_LoadingItems" EmptyText="------------All----------">
                            <%--onselectedindexchanged="txtYCODE_SelectedIndexChanged" OpenOnFocus="true"Visible="true" --%>
                            <HeaderTemplate>
                                <div class="header c2">
                                    MERGE CODE</div>
                                <div class="header c3">
                                    FIBER DESCRIPTION</div>
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
                    <td align="right">
                        <asp:Button ID="btnsave" runat="server" CssClass="AButton" OnClick="btnsave_Click"
                            Text="Get Record" Width="85px" />
                    </td>
                </tr>
                <%--<td align="right">
                <asp:Button ID="btnsave" runat="server" CssClass="AButton" OnClick="btnsave_Click"
                    Text="Get Record" Width="85px" />
        </td>--%>
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
<table width="100%">
    <tr>
        <td align="left">
            <asp:Panel ID="pnlShowHover" runat="server" ScrollBars="Auto" Width="100%">
                <asp:GridView ID="Grid1" runat="server" AutoGenerateColumns="False" HeaderStyle-Font-Bold="true"
                    Width="90%" CellPadding="3" ForeColor="#333333" GridLines="Both" BorderStyle="Ridge"
                    Font-Names="Arial" Font-Size="X-Small" ShowFooter="true"  OnPageIndexChanging="Grid1_PageIndexChanging">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                    <RowStyle BackColor="#EFF3FB" />
                    <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                    <Columns>
                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="2%">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Issue&nbsp;Date" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblDeptIssDate" NullDisplayText="0" Text='<%#Eval("MACHINE_ISSUE_DATE","{0:dd/mm/yyyy}")%>'
                                    runat="server" Visible="true"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="3%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Merge&nbsp;No" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblMergeNo" Text='<%#Eval("MERGE_NO")%>' runat="server" Visible="true"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="3%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="MACHINE_ISSUE_NO" HeaderText="Machine&nbsp;Issue&nbsp;No" />
                       
                        <asp:TemplateField HeaderText="Issue&nbsp;Qty" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblIssQty" Text='<%#Eval("ISSUE_QTY")%>' runat="server" Visible="true"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="3%" HorizontalAlign="Left" />
                            <FooterTemplate>
                                <asp:Label ID="lblTotalIssQty" runat="server" CssClass="Label SmallFont" ForeColor="White"
                                    Font-Bold="true"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        
                         <asp:TemplateField HeaderText="Issue&nbsp;Cops" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblIssCops" Text='<%#Eval("ISSUE_COPS")%>' runat="server" Visible="true"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="3%" HorizontalAlign="Left" />
                            <FooterTemplate>
                                <asp:Label ID="lblTotalIssCops" runat="server" CssClass="Label SmallFont" ForeColor="White"
                                    Font-Bold="true"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                    
                      
                        <asp:TemplateField HeaderText="Actual&nbsp;Issue&nbsp;Qty" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblActuaload" runat="server" Text='<%# Eval("ACTUAL_ISSUE_QTY")== DBNull.Value ? "0" : Eval("ACTUAL_ISSUE_QTY") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="3%" HorizontalAlign="Left" />
                             <FooterTemplate>
                                <asp:Label ID="lblTotalActuaload" runat="server" CssClass="Label SmallFont" ForeColor="White"
                                    Font-Bold="true"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Actual&nbsp;Issue&nbsp;Cops" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblActualcops" runat="server" Text='<%# Eval("ACTUAL_ISSUE_COPS")== DBNull.Value ? "0" : Eval("ACTUAL_ISSUE_COPS") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="3%" HorizontalAlign="Left" />
                             <FooterTemplate>
                                <asp:Label ID="lblTotalActualcops" runat="server" CssClass="Label SmallFont" ForeColor="White"
                                    Font-Bold="true"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="Doff&nbsp;Adjust&nbsp;Qty" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblDOFF" Text='<%#Eval("ISSUE_ADJ_QTY")%>' runat="server" Visible="true"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="3%" HorizontalAlign="Left" />
                            <FooterTemplate>
                                <asp:Label ID="lblTotalAdjQty" runat="server" CssClass="Label SmallFont" ForeColor="White"
                                    Font-Bold="true"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                      
                   <asp:TemplateField HeaderText="Doff&nbsp;Adjust&nbsp;Cops" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblIssueAdjustCops" Text='<%#Eval("ISSUE_ADJ_COPS")%>' runat="server" Visible="true"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="3%" HorizontalAlign="Left" />
                            <FooterTemplate>
                                <asp:Label ID="lblTotalIssueAdjustCops" runat="server" CssClass="Label SmallFont" ForeColor="White"
                                    Font-Bold="true"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Machine" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblMachine" runat="server" Text='<%# Eval("MACHINE_CODE") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="3%" HorizontalAlign="Left" />
                        </asp:TemplateField>
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
                    <Triggers>
        
                </td>
            </tr>
        </table>
        
    <%--</ContentTemplate>--%>