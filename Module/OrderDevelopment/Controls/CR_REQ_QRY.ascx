<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CR_REQ_QRY.ascx.cs" Inherits="Module_OrderDevelopment_Controls_CR_REQ_QRY" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
    .c1
    {
        width: 80px;
    }
    .c2
    {
        margin-left: 4px;
        width: 150px;
    }
    .c3
    {
        margin-left: 4px;
        width: 350px;
    }
    .c4
    {
        margin-left: 4px;
        width: 150px;
    }
    .c5
    {
        margin-left: 4px;
        width: 100px;
    }
</style>
<table width="100%" class="tContentArial">
    <tr>
        <td align="left" valign="top" class="td" width="100%">
            <table>
                <tr>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" ToolTip="Clear"
                            ImageUrl="~/CommonImages/clear.jpg" OnClick="imgbtnClear_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" Width="48" Height="41" ToolTip="Exit"
                            ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click"></asp:ImageButton>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td align="center" class="TableHeader td" valign="top">
                        <b class="titleheading">Order Capture Query For Production Entry</b>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td>
                        PI No:
                    </td>
                    <td>
                        <asp:TextBox ID="txtpiNo" runat="server" CssClass="SmallFont" Width="150px"></asp:TextBox>
                    </td>
                    <td>
                        Order No:
                    </td>
                    <td>
                        <asp:TextBox ID="txtorder_no" runat="server" CssClass="SmallFont" Width="150px"></asp:TextBox>
                    </td>
                    <td>
                        Party Code:
                    </td>
                    <td>
                        <cc2:ComboBox ID="txtPartyCodecmb" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="txtPartyCodecmb_LoadingItems" DataTextField="PRTY_CODE" DataValueField="Address"
                            EmptyText="Select Vendor" EnableVirtualScrolling="true" Width="150px" MenuWidth="800px"
                            Height="200px">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Code</div>
                                <div class="header c3">
                                    NAME</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                <div class="item c3">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                    </td>
                    <td colspan="6" align="center" valign="top">
                        <asp:Button ID="Button1" runat="server" Text="Get Data" Width="120px" OnClick="Button1_Click" />
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td>
                        <asp:GridView ID="grd_cr_req_qry" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            PageSize="10" AllowSorting="True" CellPadding="3" BorderStyle="Ridge" CssClass="smallfont"
                            EmptyDataText="No Record Found" Font-Size="X-Small" ForeColor="#333333" PagerStyle-HorizontalAlign="Left"
                            Width="100%" OnPageIndexChanging="grd_cr_req_qry_PageIndexChanging" OnRowCommand="grd_cr_req_qry_RowCommand"
                            OnRowDataBound="grd_cr_req_qry_RowDataBound">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Branch Name" Visible="false">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Branch Name
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtBranchName" Width="70px" CssClass="SmallFont" runat="server"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="BtnBranchName" CssClass="SmallFont" runat="server" Text="Go"
                                                        ImageUrl="~/CommonImages/Icons/Search.png" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCOMP_CODE" Visible="false" runat="server" CssClass=" SmallFont"
                                            Text='<%# Eval("COMP_CODE") %>'></asp:Label>
                                        <asp:Label ID="lblBRANCH_CODE" Visible="false" runat="server" CssClass=" SmallFont"
                                            Text='<%# Eval("BRANCH_CODE") %>'></asp:Label>
                                        <asp:Label ID="lblSHADE_CODE" Visible="false" runat="server" CssClass=" SmallFont"
                                            Text='<%# Eval("SHADE_CODE") %>'></asp:Label>
                                        <asp:Label ID="lblBranchName" runat="server" CssClass=" SmallFont" Text='<%# Eval("BRANCH_NAME") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass=" label SmallFont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Buisiness type" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBUSINESS_TYPE" Text='<%#Eval("BUSINESS_TYPE") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="4%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="product type" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPRODUCT_TYPE" Text='<%#Eval("PRODUCT_TYPE") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="4%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ORDER_CAT" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblORDER_CAT" Text='<%#Eval("ORDER_CAT") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="4%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ORDER_TYPE" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblORDER_TYPE" Text='<%#Eval("ORDER_TYPE") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="4%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order No" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblORDER_NO" Text='<%#Eval("ORDER_NO") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="4%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Party Code" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPRTY_CODE" Text='<%#Eval("PRTY_CODE") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="6%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PI No" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPI_NO" Text='<%#Eval("PI_NO") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="4%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PI_TYPE" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPI_TYPE" Text='<%#Eval("PI_TYPE") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="4%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Art. Code" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblarticle_code" Text='<%#Eval("ARTICAL_CODE") %>' ToolTip='<%#Eval("ARTICAL_DESC") %>'
                                            runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="4%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UOM" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbluom" Text='<%#Eval("UOM") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="8%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order Date" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblorder_dt" Text='<%#Eval("ORDER_DATE") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="5%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Del. Date" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldel_dt" Text='<%#Eval("DEL_DATE") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="5%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQty" Text='<%#Eval("ORD_QTY") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="5%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Paking Qty" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPACKING_QTY" Text='<%#Eval("PACKING_QTY") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="5%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Req. Qty" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblREQ_QTY" Text='<%#Eval("REQ_QTY") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="5%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Issu Qty" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblISS_QTY" Text='<%#Eval("ISS_QTY") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="5%" HorizontalAlign="Left" />
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
            <table width="100%">
                <tr>
                    <td>
                        <asp:GridView ID="grd_cr_req_qry2" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            PageSize="3" AllowSorting="True" CellPadding="3" BorderStyle="Ridge" CssClass="smallfont"
                            EmptyDataText="No Record Found" Font-Size="X-Small" ForeColor="#333333" PagerStyle-HorizontalAlign="Left"
                            Width="100%" OnRowCommand="grd_cr_req_qry2_RowCommand" OnRowDataBound="grd_cr_req_qry2_RowDataBound">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Branch Name" Visible="false">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Branch Name
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtBranchName" Width="70px" CssClass="SmallFont" runat="server"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="BtnBranchName" CssClass="SmallFont" runat="server" Text="Go"
                                                        ImageUrl="~/CommonImages/Icons/Search.png" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblBRANCH_CODE" Visible="false" runat="server" CssClass=" SmallFont"
                                            Text='<%# Eval("BRANCH_CODE") %>'></asp:Label>
                                        
                                    <%-- <asp:Label ID="lblBranchName" runat="server" CssClass=" SmallFont" Text='<%# Eval("BRANCH_NAME") %>'>
                                        </asp:Label>--%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass=" label SmallFont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="company" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCOMP_CODE" Text='<%#Eval("COMP_CODE") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="4%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="order_no" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblORDER_NO" Text='<%#Eval("ORDER_NO") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="4%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="order_no" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSHADE_CODE" Text='<%#Eval("SHADE_CODE") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="4%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PI_TYPE" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPI_TYPE" Text='<%#Eval("PI_TYPE") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="4%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                              
                                <asp:TemplateField HeaderText="ARTICAL_CODE" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblARTICAL_CODE" Text='<%#Eval("ARTICAL_CODE") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="4%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PI_NO" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPI_NO" Text='<%#Eval("PI_NO") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="4%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branch Code" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblbranchcode" Text='<%#Eval("BRANCH_CODE") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="4%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Business Type" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBUSINESS_TYPE" Text='<%#Eval("BUSINESS_TYPE") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="4%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="product type" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPRODUCT_TYPE" Text='<%#Eval("PRODUCT_TYPE") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="4%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order Cat" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblORDER_CAT" Text='<%#Eval("ORDER_CAT") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="4%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order Type" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblORDER_TYPE" Text='<%#Eval("ORDER_TYPE") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="4%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                               <%-- <asp:TemplateField HeaderText="PI Type" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPI_TYPE" Text='<%#Eval("PI_TYPE") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="4%" HorizontalAlign="Left" />
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="CR" HeaderStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtn_cr" runat="server">
                                       CR
                                        </asp:LinkButton>
                                        <asp:Panel ID="pnlShowHover2" runat="server" Width="400px" BackColor="Beige" BorderWidth="2px"
                                            Height="60px" ScrollBars="Auto">
                                            <asp:GridView ID="grd_cr" runat="server" Width="400px" CssClass="SmallFont" AutoGenerateColumns="False"
                                                Height="60px">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Year" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCR_YEAR" Text='<%#Eval("CR_YEAR") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="4%" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Branch" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblbrnach_Name" Text='<%#Eval("CR_BRANCH_NAME") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="4%" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CR No" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCR_ST_ORDER_NO" Text='<%#Eval("CR_ST_ORDER_NO") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="4%" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Order Type" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtCR_ORDER_TYPE" Text='<%#Eval("CR_ORDER_TYPE") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="4%" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Business type " HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCR_BUSINESS_TYPE" Text='<%#Eval("CR_BUSINESS_TYPE") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="6%" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Shade Code" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblshadecode" Text='<%#Eval("CR_ST_SHADE_CODE") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="4%" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Shade Fm Code" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblshadefmcode" Text='<%#Eval("CR_ST_SHADE_FAMILY_CODE") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Adj Qty" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbladj_qty" Text='<%#Eval("ADJ_QTY") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="4%" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Count" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCR_ST_COUNT" Text='<%#Eval("CR_ST_COUNT") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="4%" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <RowStyle CssClass="SmallFont" Width="98%" />
                                                <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="BlueViolet" />
                                            </asp:GridView>
                                        </asp:Panel>
                                        <cc1:HoverMenuExtender ID="HoverMenuExtender12" runat="server" TargetControlID="lnkbtn_cr"
                                            PopupControlID="pnlShowHover2" PopupPosition="Left" PopDelay="10">
                                        </cc1:HoverMenuExtender>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle Width="3%" HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Packing" HeaderStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtn_packing" runat="server">
                                       Packing
                                        </asp:LinkButton>
                                        <asp:Panel ID="pnlShowHover1" runat="server" Width="400px" BackColor="Beige" BorderWidth="2px"
                                            Height="60px" ScrollBars="Auto">
                                            <asp:GridView ID="grd_pack" runat="server" Width="400px" CssClass="SmallFont" AutoGenerateColumns="False"
                                                Height="60px">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Branch" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblbrnach_Name" Text='<%#Eval("BRANCH_NAME") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="4%" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Business Type" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Business_Type" Text='<%#Eval("BUSINESS_TYPE") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="8%" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Product Type" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPRODUCT_TYPE" Text='<%#Eval("PRODUCT_TYPE") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="8%" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Order No" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblORDER_NO" Text='<%#Eval("ORDER_NO") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="4%" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Order Cat." HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblORDER_CAT" Text='<%#Eval("ORDER_CAT") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="4%" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Order type" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblORDER_TYPE" Text='<%#Eval("ORDER_TYPE") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="4%" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PI No" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPI_NO" runat="server" Text='<%#Eval("PI_NO") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="4%" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="PI Type" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPI_TYPE" runat="server" Text='<%#Eval("PI_TYPE") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="4%" HorizontalAlign="Left" />
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Article Code" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblARTICAL_CODE" Text='<%#Eval("ARTICAL_CODE") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="8%" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Article desc" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblARTICAL_DESC" Text='<%#Eval("ARTICAL_DESC") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="8%" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Shace Code" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblShadeCode" Text='<%#Eval("SHADE_CODE") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="8%" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <RowStyle CssClass="SmallFont" Width="98%" />
                                                <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="BlueViolet" />
                                            </asp:GridView>
                                        </asp:Panel>
                                        <cc1:HoverMenuExtender ID="HoverMenuExtender123" runat="server" TargetControlID="lnkbtn_packing"
                                            PopupControlID="pnlShowHover1" PopupPosition="Left" PopDelay="10">
                                        </cc1:HoverMenuExtender>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle Width="3%" HorizontalAlign="Right" />
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
        </td>
    </tr>
</table>
