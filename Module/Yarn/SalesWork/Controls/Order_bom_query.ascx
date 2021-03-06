<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Order_bom_query.ascx.cs"
    Inherits="Module_Yarn_SalesWork_Controls_Order_bom_query" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="left" class=" tContentArial" width="100%">
            <tr>
                <td>
                    <table>
                        <tr>
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
                            <td align="center" class="TableHeader td">
                                <span class="titleheading"><strong>Order Bom Query</strong> </span>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" style="border-style: ridge; border-color: White;">
                        <tr>
                            <td align="right" valign="top" width="15%">
                                Order Type
                            </td>
                            <td align="left" valign="top" width="15%">
                                <asp:DropDownList ID="ddlOrderType" runat="server" Width="140px" CssClass="SmallFont BoldFont UPPERCASE">
                                </asp:DropDownList>
                            </td>
                            <td align="right" valign="top" width="15%">
                                Product Type
                            </td>
                            <td align="left" valign="top" width="15%">
                                <asp:DropDownList ID="ddlProductType" runat="server" Width="140px" CssClass="SmallFont BoldFont">
                                </asp:DropDownList>
                            </td>
                            <td align="right" valign="top" width="15%">
                                Shade code
                            </td>
                            <td align="left" valign="top" width="15%" class="style8">
                                <asp:TextBox ID="txtshadecode" runat="server" Height="13px"></asp:TextBox>
                            </td>
                            <td align="right" valign="top">
                                Article Code:
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtarticlecode" Width="140px" Height="13px" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top" width="10%">
                                PA No:
                            </td>
                            <td align="left" valign="top" width="10%">
                                <asp:TextBox ID="txtpano" Width="140px" Height="13px" runat="server"></asp:TextBox>
                            </td>
                            <td align="right" valign="top" width="10%">
                                Base Shade Code:
                            </td>
                            <td align="left" valign="top" width="10%">
                                <asp:TextBox ID="txtbaseshadecode" Width="140px" Height="13px" runat="server"></asp:TextBox>
                            </td>
                            <td align="right" valign="top" width="10%">
                                Base Article Code:
                            </td>
                            <td align="left" valign="top" width="10%">
                                <asp:TextBox ID="txtbasearticlecode" Width="140px" Height="13px" runat="server"></asp:TextBox>
                            </td>
                            <td align="right" valign="top">
                            </td>
                            <td align="right" valign="top" width="10%">
                                <asp:Button ID="btngetrecord" runat="server" Text="Get Record" Width="85px" Height="22px"
                                    OnClick="btngetrecord_Click1" CssClass="AButton"/>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td align="left" width="50%">
                                <b>
                                    <asp:Label ID="Label1" runat="server" Text="Total Record : " CssClass="Label"></asp:Label>
                                    <asp:Label ID="lblTotalRecord" runat="server" CssClass="Label"></asp:Label></b>
                            </td>
                            <td align="left" valign="top" width="50%" cssclass="Label">
                                <b>
                                    <asp:UpdateProgress ID="UpdateProgress9" runat="server">
                                        <ProgressTemplate>
                                            Loading...</ProgressTemplate>
                                    </asp:UpdateProgress>
                                </b>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <asp:GridView ID="grdOrderBomQuery" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            PageSize="20" AllowSorting="True" CellPadding="3" BorderStyle="Ridge" CssClass="smallfont"
                            EmptyDataText="No Record Found" Font-Size="X-Small" ForeColor="#333333" PagerStyle-HorizontalAlign="Left"
                            Width="100%" OnPageIndexChanging="grdOrderBomQuery_PageIndexChanging">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                            <Columns>
                                <asp:BoundField DataField="product_type" HeaderText="Product Type" />
                                <asp:BoundField DataField="order_type" HeaderText="Order Type" />
                                <asp:BoundField DataField="order_no" HeaderText="Order No" />
                                <asp:BoundField DataField="pi_no" HeaderText="PI No" />
                                <asp:BoundField DataField="artical_code" HeaderText="Artical Code" />
                               <%-- <asp:BoundField DataField="SHADE_FAMILY" HeaderText="Shade Family" />--%>
                                <asp:BoundField DataField="shade_code" HeaderText="Shade Code" />
                                <asp:BoundField DataField="base_artical_code" HeaderText="Base Artical Code" />
                                <asp:BoundField DataField="BASE_SHADE_CODE" HeaderText="Base Shade Code" />
                                <asp:BoundField DataField="req_qty" HeaderText="Req qty" />
                                <asp:BoundField DataField="iss_sty" HeaderText="Iss sty" />
                                <asp:BoundField DataField="bal_to_issue" HeaderText="Bal to issue" />
                            </Columns>
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </table>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
