<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LRGenrationYarnDyeing.ascx.cs" Inherits="Module_OrderDevelopment_LabDip_Controls_LRGenrationYarnDyeing" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style type="text/css">
    .header
    {
        font-weight: bold;
        position: absolute;
        background-color: #507CD1;
        font-weight: bold;
        color: White;
        text-align: left;
        vertical-align: top;
    }
</style>
<table width="100%">
    <tr>
        <td width="100%">
            <table>
                <tr>
                    <td id="tdUpdate" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" Height="41" ImageUrl="~/CommonImages/edit1.jpg"
                            ToolTip="Update" Width="48" OnClick="imgbtnUpdate_Click" />
                    </td>
                    <td id="tdClear" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                            ToolTip="Clear" Width="48" OnClick="imgbtnClear_Click" />
                    </td>
                    <td id="tdPrint" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" Width="48" OnClick="imgbtnPrint_Click" />
                    </td>
                    <td id="tdExit" runat="server">
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                            Width="48" Height="41" OnClick="imgbtnExit_Click"></asp:ImageButton>
                    </td>
                    <td id="tdHelp" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                            ToolTip="Help" Height="41" Width="48" OnClick="imgbtnHelp_Click"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader td" width="100%">
            <b class="titleheading">LR Generation For Yarn Dyeing</b>
        </td>
    </tr>
    <tr>
        <td align="left" valign="top" class="td" width="100%">
            <asp:Label ID="lblErrmsg" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="100%">
            <asp:Panel ID="pnlLRGeneration" runat="server" Height="400px" Width="99%" ScrollBars="Auto">
                <div style="width: 99%; height: 400px; overflow: auto;">
                    <asp:GridView ID="grdLrGeneration" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                        RowStyle-VerticalAlign="Bottom" Font-Size="10px" ForeColor="#333333" CellPadding="4"
                        GridLines="None" OnRowDataBound="grdLrGeneration_RowDataBound">
                        <Columns>
                            <asp:TemplateField Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblBRANCH_CODE" runat="server" Text='<%# Bind("BRANCH_CODE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CR No">
                                <ItemTemplate>
                                    <asp:Label ID="lblORDER_NO" runat="server" Text='<%# Bind("ORDER_NO") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="50px" />
                                <ItemStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CR Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblORDER_DATE" runat="server" Text='<%# Eval("ORDER_DATE","{0:dd/MM/yyyy}" ) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="50px" />
                                <ItemStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblPRTY_CODE" runat="server" Text='<%# Bind("PRTY_CODE") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="50px" />
                                <ItemStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer Details">
                                <ItemTemplate>
                                    <asp:Label ID="lblPRTY_ADD" runat="server" Text='<%# Bind("PRTY_ADD") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="170px" />
                                <ItemStyle Width="250px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Substrate">
                                <ItemTemplate>
                                    <asp:Label ID="lblSUBSTRATE" runat="server" Text='<%# Bind("SUBSTRATE") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="50px" />
                                <ItemStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Count">
                                <ItemTemplate>
                                    <asp:Label ID="lblCOUNT" runat="server" Text='<%# Bind("COUNT") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="40px" />
                                <ItemStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Shade Family Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblShadeFamilyCode" runat="server" Text='<%# Bind("SHADE_FAMILY_CODE") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="50px" />
                                <ItemStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Shade Family">
                                <ItemTemplate>
                                    <asp:Label ID="lblShadeFamilyName" runat="server" Text='<%# Bind("SHADE_FAMILY_NAME") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="50px" />
                                <ItemStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Shade Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblShadeCode" runat="server" Text='<%# Bind("SHADE_CODE") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="50px" />
                                <ItemStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Shade Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblShadeName" runat="server" Text='<%# Bind("SHADE_NAME") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="50px" />
                                <ItemStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Light Source">
                                <ItemTemplate>
                                    <asp:Label ID="lblLIGHT_SOURCE" runat="server" Text='<%# Bind("LIGHT_SOURCE") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="40px" />
                                <ItemStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UOM">
                                <ItemTemplate>
                                    <asp:Label ID="lblUOM" runat="server" Text='<%# Bind("UOM") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="50px" />
                                <ItemStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Generate LR" ItemStyle-Width="50px" HeaderStyle-Width="40px">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkLRGenerate" runat="server" Checked="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Series" ItemStyle-Width="50px" HeaderStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlLRSeries" AppendDataBoundItems="true" runat="server" Width="40px"
                                        CssClass="SmallFont">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Width="50px" />
                                <ItemStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Req Delivery Date" ItemStyle-Width="50px" HeaderStyle-Width="130px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtReqDelDate" runat="server" Width="120px"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtReqDelDate" runat="server"
                                        Format="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
                                </ItemTemplate>
                                <HeaderStyle Width="130px" />
                                <ItemStyle Width="50px" />
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle BackColor="#EFF3FB" />
                      
                        <HeaderStyle CssClass="header SmallFont" Height="30px" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </div>
            </asp:Panel>
        </td>
    </tr>
</table>
