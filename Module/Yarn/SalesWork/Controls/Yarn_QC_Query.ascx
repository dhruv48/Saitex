<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Yarn_QC_Query.ascx.cs"
    Inherits="Module_Yarn_SalesWork_Controls_Yarn_QC_Query" %>
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
     .Smallfont
    {
        font-size: 8pt;
    }
</style>
<table width="100%" class="td tContentArial">
    <tr>
        <td>
            <table class="tContentArial">
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
                            ImageUrl="~/CommonImages/export.png" OnClick="imgbtnExport_Click"></asp:ImageButton>&nbsp;
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
            <table width="100%">
                <tr>
                    <td align="center" valign="top" class="tRowColorAdmin td">
                        <span class="titleheading">Yarn QC Standard Master Query</span>
                    </td>
                </tr>
            </table>
            <table class="tContentArial">
                <tr>
                    <td align="right" width="10%">
                        Inward Type:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlInwardType" runat="server" AutoPostBack="True" Width="160px"
                            CssClass="SmallFont" OnSelectedIndexChanged="ddlInwardType_SelectedIndexChanged"
                            TabIndex="2">
                        </asp:DropDownList>
                    </td>
                    <td align="right" width="10%">
                        Yarn Category:
                    </td>
                    <td class="tdLeft">
                        <asp:DropDownList ID="ddlYarnCat" runat="server" AppendDataBoundItems="True" CssClass="SmallFont"
                            TabIndex="3" Width="160px" OnSelectedIndexChanged="ddlYarnCat_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                    <td align="right" width="10%">
                        Yarn :
                    </td>
                    <td class="tdLeft">
                        <cc2:ComboBox ID="ddlyarncode" runat="server" AutoPostBack="True" CssClass="SmallFont"
                            DataTextField="YARN_CODE" DataValueField="Y_COUNT" EmptyText="Find Yarn Code"
                            EnableLoadOnDemand="true" EnableVirtualScrolling="true" Height="200px" MenuWidth="800"
                            OnLoadingItems="ddlyarncode_LoadingItems" OnSelectedIndexChanged="ddlyarncode_SelectedIndexChanged1"
                            TabIndex="4" Width="160px">
                            <HeaderTemplate>
                                <div class="header c1">
                                    YARN CODE</div>
                                <div class="header c2">
                                    YARN Description</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal ID="Container1" runat="server" Text='<%# Eval("YARN_CODE") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal ID="Container2" runat="server" Text='<%# Eval("YARN_DESC") %>' /></div>
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
                        <asp:DropDownList runat="server" ID="ddlStdType" Width="160px" CssClass="SmallFont"
                            TabIndex="4" AutoPostBack="True" OnSelectedIndexChanged="ddlStdType_SelectedIndexChanged" />
                    </td>
                </tr>
                <tr>
                    <td align="right" width="10%">
                        Tolerance Type:
                    </td>
                    <td class="tdLeft">
                        <asp:DropDownList runat="server" ID="ddltoleranceType" Width="160px" CssClass="SmallFont"
                            TabIndex="6" AutoPostBack="True" OnSelectedIndexChanged="ddltoleranceType_SelectedIndexChanged" />
                    </td>
                    <td align="right" width="10%">
                        Tolerance Range
                    </td>
                    <td class="tdLeft">
                        <asp:DropDownList runat="server" ID="ddltolerancerange" Width="160px" CssClass="SmallFont"
                            TabIndex="7" AutoPostBack="True" OnSelectedIndexChanged="ddltolerancerange_SelectedIndexChanged" />
                    </td>
                    <td align="right" width="10%">
                        Status:
                    </td>
                    <td class="tdLeft">
                        <asp:DropDownList ID="ddlstatus" runat="server" CssClass="SmallFont" Width="160px"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Value="">All</asp:ListItem>
                            <asp:ListItem Value="0">Pending</asp:ListItem>
                            <asp:ListItem Value="1">Approved</asp:ListItem>
                            <asp:ListItem Value="3">REJECTED</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td align="left" valign="top" width="50%" class="Label">
                        <b>Total Records :</b>
                        <asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
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
            </table>
            <table>
                <tr>
                    <td align="left">
                        <asp:Panel ID="pnlShowHover" runat="server" Width="100%" ScrollBars="Auto">
                            <asp:GridView ID="gvMaterialReceiptApproval" CssClass="SmallFont" runat="server"
                                AllowSorting="True" AutoGenerateColumns="False" Width="99%" OnPageIndexChanging="gvMaterialReceiptApproval_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField DataField="TRN_NUMB" HeaderText="QC&nbsp;No." ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                    <asp:BoundField DataField="INWARD_TYPE" HeaderText="Inward&nbsp;Type" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                    <asp:BoundField DataField="YARN_CATEGORY" HeaderText="Yarn&nbsp;Category" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                    <asp:BoundField DataField="YARN_CODE" HeaderText="Yarn&nbsp;Code" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                    <asp:BoundField DataField="YARN_DESC" HeaderText="Yarn&nbsp;Description" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                    <asp:BoundField DataField="STD_TYPE" HeaderText="Std&nbsp;Type" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                    <asp:BoundField DataField="TOLERANCE" HeaderText="Tolerance" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"></asp:BoundField>
                                    <asp:BoundField DataField="TOLERANCE_TYPE" HeaderText="Tolerance&nbsp;Type" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                    <asp:BoundField DataField="TOLERANCE_RANGE" HeaderText="Tolerance&nbsp;Range" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                    <asp:BoundField DataField="MAX_VALUE" HeaderText="Max&nbsp;Value" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"></asp:BoundField>
                                    <asp:BoundField DataField="MIN_VALUE" HeaderText="Min&nbsp;Value" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right"></asp:BoundField>
                                    <asp:BoundField DataField="UOM" HeaderText="UOM" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                    <asp:BoundField DataField="REMARKS" HeaderText="Remarks" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                    <asp:BoundField DataField="STATUS" HeaderText="Status" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                </Columns>
                                <RowStyle CssClass="SmallFont" Width="100%" />
                                <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
