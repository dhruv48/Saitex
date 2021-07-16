<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Item_QC_Master_Query.ascx.cs"
    Inherits="Module_Inventory_Controls_Item_QC_Master_Query" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
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
    .d1
    {
        width: 150px;
    }
    .d2
    {
        margin-left: 4px;
        width: 350px;
    }
    .d3
    {
        width: 80px;
    }
</style>
<table width="1000px" class="td tContentArial">
    <tr>
        <td>
            <table class="tContentArial">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td id="tdUpdate" runat="server" align="left">
                                    <asp:ImageButton ID="imgbtnAddNew" runat="server" Width="48" Height="41" ToolTip="Add New"
                                        ImageUrl="~/CommonImages/addnew.png" OnClick="imgbtnAddNew_Click"></asp:ImageButton>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                                        Width="48" Height="41" OnClick="imgbtnPrint_Click"></asp:ImageButton>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgbtnExport" runat="server" Width="48" Height="41" ToolTip="Print"
                                        ImageUrl="~/CommonImages/export.png" OnClick="imgbtnExport_Click"></asp:ImageButton>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                        ToolTip="Clear" Width="48" OnClick="imgbtnClear_Click"></asp:ImageButton>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                                        Width="48" Height="41" OnClick="imgbtnExit_Click"></asp:ImageButton>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                                        Width="48" Height="41"></asp:ImageButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center" valign="top" class="tRowColorAdmin td" colspan="12">
                        <span class="titleheading">Item QC Standard Master Query</span>
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top">
                        <table class="tContentArial">
                            <tr>
                                <td align="right" width="10%">
                                    Item Category:
                                </td>
                                <td class="tdLeft">
                                    <cc2:ComboBox runat="server" ID="ddlItemCategory" CssClass="SmallFont" MenuWidth="400px"
                                        EnableLoadOnDemand="True" Width="100px" Height="180px" EmptyText="Select Item Category..."
                                        OnLoadingItems="ddlItemCategory_LoadingItems" AutoPostBack="True" OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged1"
                                        TabIndex="1">
                                        <HeaderTemplate>
                                            <div class="header d1">
                                                Code</div>
                                            <div class="header d1">
                                                Desc</div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="item d1">
                                                <%# Eval("MST_CODE")%></div>
                                            <div class="item d1">
                                                <%# Eval("MST_DESC") %></div>
                                        </ItemTemplate>
                                    </cc2:ComboBox>
                                </td>
                                <td align="right" width="10%">
                                    Item:
                                </td>
                                <td class="tdLeft">
                                    <cc2:ComboBox ID="ddlItemCode" runat="server" CssClass="SmallFont" AutoPostBack="true"
                                        TabIndex="2" Width="100px" EnableLoadOnDemand="True" DataTextField="ITEM_DESC"
                                        DataValueField="ITEM_CODE" MenuWidth="650px" EnableVirtualScrolling="true" OpenOnFocus="true"
                                        Visible="true" Height="200px" EmptyTextSelect="Select Item" OnSelectedIndexChanged="ddlItemCode_SelectedIndexChanged"
                                        OnLoadingItems="ddlItemCode_LoadingItems">
                                        <HeaderTemplate>
                                            <div class="header d1">
                                                ITEM CODE</div>
                                            <div class="header d2">
                                                ITEM DESCRIPTION</div>
                                            <div class="header d3">
                                                TYPE</div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="item d1">
                                                <%# Eval("ITEM_CODE")%></div>
                                            <div class="item d2">
                                                <%# Eval("ITEM_DESC") %></div>
                                            <div class="item d3">
                                                <%# Eval("ITEM_TYPE")%></div>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Displaying items
                                            <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                            out of
                                            <%# Container.ItemsCount %>.
                                        </FooterTemplate>
                                    </cc2:ComboBox>
                                </td>
                                <td align="right" width="10%">
                                    Std Type:
                                </td>
                                <td class="tdLeft">
                                    <asp:DropDownList runat="server" ID="ddlStdType" Width="100px" CssClass="tContentArial"
                                        TabIndex="4" AutoPostBack="True" OnSelectedIndexChanged="ddlStdType_SelectedIndexChanged" />
                                </td>
                                <td align="right" width="10%">
                                    Tolerance Type:
                                </td>
                                <td class="tdLeft">
                                    <asp:DropDownList runat="server" ID="ddltoleranceType" Width="100px" CssClass="tContentArial"
                                        TabIndex="6" AutoPostBack="True" OnSelectedIndexChanged="ddltoleranceType_SelectedIndexChanged" />
                                </td>
                                <td align="right" width="10%">
                                    Tolerance Range
                                </td>
                                <td class="tdLeft">
                                    <asp:DropDownList runat="server" ID="ddltolerancerange" Width="100px" CssClass="tContentArial"
                                        TabIndex="7" AutoPostBack="True" OnSelectedIndexChanged="ddltolerancerange_SelectedIndexChanged" />
                                </td>
                            </tr>
                            
                            <tr>
                                
                                <td align="right" width="10%">
                                    Status:
                                </td>
                                <td class="tdLeft">
                                    <asp:DropDownList ID="ddlstatus" runat="server" CssClass="tContentArial" Width="100px"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="">All</asp:ListItem>
                                        <asp:ListItem Value="0">Pending</asp:ListItem>
                                        <asp:ListItem Value="1">Approved</asp:ListItem>
                                        <asp:ListItem Value="3">REJECTED</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                              
                            </tr>
                        </table>
                    </td>
                </tr>
               
                <tr>
                    <td align="left" valign="top" width="50%" class="Label">
                        <b>Total Records :</b>
                        <asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
                                        <asp:GridView ID="Grid12" runat="server" 
                            AutoGenerateColumns="False" HeaderStyle-Font-Bold="true"
                                            AllowPaging="true" PageSize="20" CellPadding="3" 
                            ForeColor="#333333" GridLines="Both"
                                            BorderStyle="Ridge" Font-Names="Arial" Font-Size="X-Small" OnPageIndexChanging="Grid12_PageIndexChanging"
                                            Width="200%" Style="margin-right: 627px">
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                                            <Columns>
                                                <asp:BoundField DataField="TRN_NUMB" HeaderText="Sr. No. " ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                                <asp:BoundField DataField="ITEM_CATEGORY" HeaderText="Item Category" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                                <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                                <asp:BoundField DataField="ITEM_DESC" HeaderText="Item Description" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                                <asp:BoundField DataField="STD_VALUE" HeaderText="Std Value" ItemStyle-HorizontalAlign="Right"
                                                    HeaderStyle-HorizontalAlign="Right"></asp:BoundField>
                                                <asp:BoundField DataField="STD_TYPE" HeaderText="Std Type" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                                <asp:BoundField DataField="TOLERANCE" HeaderText="Tolerance" ItemStyle-HorizontalAlign="Right"
                                                    HeaderStyle-HorizontalAlign="Right"></asp:BoundField>
                                                <asp:BoundField DataField="TOLERANCE_TYPE" HeaderText="Tolerance Type" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                                <asp:BoundField DataField="TOLERANCE_RANGE" HeaderText="Tolerance Range" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                                <asp:BoundField DataField="MAX_VALUE" HeaderText="Max Value" ItemStyle-HorizontalAlign="Right"
                                                    HeaderStyle-HorizontalAlign="Right"></asp:BoundField>
                                                <asp:BoundField DataField="MIN_VALUE" HeaderText="Min Value" ItemStyle-HorizontalAlign="Right"
                                                    HeaderStyle-HorizontalAlign="Right"></asp:BoundField>
                                                <asp:BoundField DataField="UOM" HeaderText="UOM" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                                <asp:BoundField DataField="REMARKS" HeaderText="Remarks" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                                <asp:BoundField DataField="STATUS" HeaderText="Status" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                            </Columns>
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                                        </asp:GridView>
                    </td>
                    <td align="left" width="50%" class="Label" valign="top">
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <h3>
                                    Loading...</h3>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td align="left">
                                    <asp:Panel ID="pnlShowHover" runat="server" Width="1000px" ScrollBars="Auto">
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
