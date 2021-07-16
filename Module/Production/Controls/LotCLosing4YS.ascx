<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LotCLosing4YS.ascx.cs" Inherits="Module_Production_Controls_LotCLosing4YS" %>
<table>
<tr>
        <td id="tdUpdate" runat="server" align="left">
            <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click"></asp:ImageButton>
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
                ToolTip="Help" Width="48" />
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center" valign="top" width="100%">
            <strong>Lot Closing</strong>
        </td>
    </tr>
    <tr>
        <td align="center" valign="top" cssclass="Label">
            <b>
                <asp:UpdateProgress ID="UpdateProgress9" runat="server">
                    <ProgressTemplate>
                        Loading... Please Wait...</ProgressTemplate>
                </asp:UpdateProgress>
            </b>
        </td>
    </tr>
</table>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table width="100%">
            <tr>
                <td>
                    <asp:GridView ID="Grd_Lot_Closing" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                        PageSize="10" AllowSorting="True" CellPadding="3" BorderStyle="Ridge" CssClass="smallfont"
                        EmptyDataText="No Record Found" Font-Size="X-Small" ForeColor="#333333" PagerStyle-HorizontalAlign="Left"
                        Width="100%" OnPageIndexChanging="Grd_Lot_Closing_PageIndexChanging">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                        <RowStyle BackColor="#EFF3FB" />
                        <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                        <Columns>
                            <asp:TemplateField HeaderText="DEPARTMENT" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblDeptCode" runat="server" CssClass=" SmallFont" Text='<%# Eval("DEPT_CODE") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="4%" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ORDER NO" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblOrderNo" runat="server" CssClass=" SmallFont" Text='<%# Eval("ORDER_NO") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="4%" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="LOT NUMBER" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblLotNo" runat="server" CssClass=" SmallFont" Text='<%# Eval("LOT_NUMBER") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="4%" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="STOCK QTY" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbllotqty" runat="server" CssClass=" SmallFont" Text='<%# Eval("STOCK_QTY") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="4%" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PRODUCT TYPE" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblproductType" runat="server" CssClass=" SmallFont" Text='<%# Eval("PRODUCT_TYPE") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="4%" HorizontalAlign="Left" />
                            </asp:TemplateField>
                        
                            <asp:TemplateField HeaderText="CLOSE">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chklotclosing" runat="server" OnCheckedChanged="chklotclosing_CheckedChanged"
                                        AutoPostBack="True" />
                                </ItemTemplate>
                                <ItemStyle Width="1%" HorizontalAlign="Left" />
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                            VerticalAlign="Middle" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>