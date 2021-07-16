<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OD_CAP_Query4YS.ascx.cs"
    Inherits="Module_OrderDevelopment_Controls_OD_CAP_Query4YS" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<link href="../../../StyleSheet/style.css" rel="stylesheet" type="text/css" />
<link href="../../../StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    .style1
    {
        width: 100%;
    }
</style>
<asp:UpdatePanel ID="UpdateLabel1" runat="server">
<ContentTemplate>
<table align="left" class="tContentArial" width="970px">
    <tr>
        <td align="left" valign="top" class="td" width="100%">
            <table align="left">
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
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader td" width="100%">
            <b class="titleheading">Order Capture Query For Yarn Spinning</b>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" width="100%">
            <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
        </td>
    </tr>
    <tr>
    <td align="left" width="100%">
            <b>Total Record<asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
            </b>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" width="100%">
            <asp:GridView ID="gvOrder" runat="server" AllowPaging="true" AllowSorting="true"
                AutoGenerateColumns="false" Font-Size="Small" Width="100%" 
                onpageindexchanging="gvOrder_PageIndexChanging" 
                onrowcommand="gvOrder_RowCommand" onrowdatabound="gvOrder_RowDataBound">
                <RowStyle BackColor="White" Font-Size="Small" />
                <Columns>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Branch Name" >
                        <HeaderTemplate>
                            <table width="100%">
                                <tr>
                                    <td colspan="2" width="100%">
                                        Branch Name:
                                    </td>
                                </tr>
                                <tr>
                                    <td width="80%">
                                        <asp:TextBox ID="txtBranchName" Width="70px" CssClass="SmallFont" runat="server"></asp:TextBox>
                                    </td>
                                    <td width="20%">
                                        <asp:ImageButton ID="BtnBranchName" CssClass="SmallFont" runat="server" Text="Go"
                                            OnClick="BtnBranchName_Click" ImageUrl="~/CommonImages/Icons/Search.png" />
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
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" Visible="false" HeaderText="Business Type">
                        <HeaderTemplate>
                            <table width="100%">
                                <tr>
                                    <td colspan="2" width="100%">
                                        Business Type
                                    </td>
                                </tr>
                                <tr>
                                    <td width="80%">
                                        <asp:TextBox ID="txtBusinessType" Width="70px" CssClass="SmallFont" runat="server"></asp:TextBox>
                                    </td>
                                    <td width="20%">
                                        <asp:ImageButton ID="btnBusinessType" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/Search.png"
                                            Text="Go" OnClick="btnBusinessType_Click" />
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblBUSINESS_TYPE" runat="server" CssClass=" SmallFont" Text='<%# Eval("BUSINESS_TYPE") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass=" label SmallFont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" Visible="false" HeaderText="Product Type">
                        <HeaderTemplate>
                            <table width="100%">
                                <tr>
                                    <td colspan="2" width="100%">
                                        Product Type
                                    </td>
                                </tr>
                                <tr>
                                    <td width="80%">
                                        <asp:TextBox ID="txtProductType" runat="server" CssClass="SmallFont" Width="70px"></asp:TextBox>
                                    </td>
                                    <td width="20%">
                                        <asp:ImageButton ID="btnProductType" runat="server" ImageUrl="~/CommonImages/Icons/Search.png"
                                            CssClass="SmallFont" OnClick="btnProductType_Click" Text="Go" />
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblPRODUCT_TYPE" runat="server" CssClass=" SmallFont" Text='<%# Eval("PRODUCT_TYPE") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass=" label SmallFont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" Visible="false" HeaderText="Order Cat">
                        <HeaderTemplate>
                            <table width="100%">
                                <tr>
                                    <td colspan="2" width="100%">
                                        Order Cat
                                    </td>
                                </tr>
                                <tr>
                                    <td width="80%">
                                        <asp:TextBox ID="txtOrderCat" runat="server" CssClass="SmallFont" Width="70px"></asp:TextBox>
                                    </td>
                                    <td width="20%">
                                        <asp:ImageButton ID="btnOrderCat" runat="server" ImageUrl="~/CommonImages/Icons/Search.png"
                                            CssClass="SmallFont" OnClick="btnOrderCat_Click" Text="Go" />
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblORDER_CAT" runat="server" CssClass=" SmallFont" Text='<%# Eval("ORDER_CAT") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass=" label SmallFont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" Visible="false" HeaderText="Order Type">
                        <HeaderTemplate>
                            <table width="100%">
                                <tr>
                                    <td colspan="2" width="100%">
                                        Order Type
                                    </td>
                                </tr>
                                <tr>
                                    <td width="80%">
                                        <asp:TextBox ID="txtOrderType" runat="server" CssClass="SmallFont" Width="70px"></asp:TextBox>
                                    </td>
                                    <td width="20%">
                                        <asp:ImageButton ID="btnOrderType" runat="server" ImageUrl="~/CommonImages/Icons/Search.png"
                                            CssClass="SmallFont" OnClick="btnOrderType_Click" Text="Go" />
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblORDER_TYPE" runat="server" CssClass=" SmallFont" Text='<%# Eval("ORDER_TYPE") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass=" label SmallFont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Order No">
                        <HeaderTemplate>
                            <table width="100%">
                                <tr>
                                    <td colspan="2" width="100%">
                                        Order No
                                    </td>
                                </tr>
                                <tr>
                                    <td width="80%">
                                        <asp:TextBox ID="txtOrderNo" runat="server" CssClass="SmallFont" Width="70px"></asp:TextBox>
                                    </td>
                                    <td width="20%">
                                        <asp:ImageButton ID="btnOrderNo" ImageUrl="~/CommonImages/Icons/Search.png" runat="server"
                                            CssClass="SmallFont" Text="Go" OnClick="btnOrderNo_Click" />
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <FooterTemplate>
                            <asp:Button ID="bntNext" Visible="false"  CssClass="SmallFont" runat="server" Text="Next 15 Records" />
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblORDER_NO" runat="server" CssClass=" SmallFont" Text='<%# Eval("ORDER_NO") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass=" label SmallFont" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Party Code">
                        <HeaderTemplate>
                            <table width="100%">
                                <tr>
                                    <td colspan="2" width="100%">
                                        Party Code
                                    </td>
                                </tr>
                                <tr>
                                    <td width="80%">
                                        <asp:TextBox ID="txtPrtyCode" runat="server" CssClass="SmallFont" Width="70px"></asp:TextBox>
                                    </td>
                                    <td width="20%">
                                        <asp:ImageButton ID="btnPartyCode" OnClick="btnOrderNo_Click" ImageUrl="~/CommonImages/Icons/Search.png"
                                            runat="server" CssClass="SmallFont" Text="Go" />
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <FooterTemplate>
                            <asp:Button ID="bntNext" Visible ="false"  CssClass="SmallFont" runat="server" Text="Next 15 Records" />
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblPartyCode" runat="server" CssClass=" SmallFont" Text='<%# Eval("PRTY_CODE") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass=" label SmallFont" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Prty Name" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblPRTY_NAME" runat="server" CssClass=" SmallFont" Text='<%# Eval("PRTY_NAME") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <table width="100%">
                                <tr>
                                    <td colspan="2" width="100%">
                                        Prty Name
                                    </td>
                                </tr>
                                <tr>
                                    <td width="80%">
                                        <asp:TextBox ID="txtPrtyName" runat="server" CssClass="SmallFont" Width="70px"></asp:TextBox>
                                    </td>
                                    <td width="20%">
                                        <asp:ImageButton ID="btnPrtyName" ImageUrl="~/CommonImages/Icons/Search.png" runat="server"
                                            CssClass="SmallFont" OnClick="btnOrderNo_Click" Text="Go" />
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass=" label SmallFont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Pi No">
                        <ItemTemplate>
                            <asp:Label ID="lblPI_NO" runat="server" CssClass=" SmallFont" Text='<%# Eval("PI_NO") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <table width="100%">
                                <tr>
                                    <td colspan="2" width="100%">
                                        Pi No
                                    </td>
                                </tr>
                                <tr>
                                    <td width="80%">
                                        <asp:TextBox ID="txtPiNo" runat="server" CssClass="SmallFont" Width="70px"></asp:TextBox>
                                    </td>
                                    <td width="20%">
                                        <asp:ImageButton ID="btnPiNo" ImageUrl="~/CommonImages/Icons/Search.png" runat="server"
                                            OnClick="btnOrderNo_Click" CssClass="SmallFont" Text="Go" />
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass=" label SmallFont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" Visible="false" HeaderText="Article Code">
                        <ItemTemplate>
                            <asp:Label ID="lblARTICAL_CODE" runat="server" CssClass=" SmallFont" Text='<%# Eval("ARTICAL_CODE") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <table width="100%">
                                <tr>
                                    <td colspan="2" width="100%">
                                        Article Code
                                    </td>
                                </tr>
                                <tr>
                                    <td width="80%">
                                        <asp:TextBox ID="txtArticleCode" runat="server" CssClass="SmallFont" Width="70px"></asp:TextBox>
                                    </td>
                                    <td width="20%">
                                        <asp:ImageButton ID="btnArticleCode" runat="server" CssClass="SmallFont" OnClick="btnOrderNo_Click"
                                            Text="Go" ImageUrl="~/CommonImages/Icons/Search.png" />
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass=" label SmallFont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Artical desc">
                        <ItemTemplate>
                            <asp:Label ID="lblARTICAL_DESC" runat="server" CssClass=" SmallFont" Text='<%# Eval("ARTICAL_DESC") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <table width="100%">
                                <tr>
                                    <td colspan="2" width="100%">
                                        Artical desc
                                    </td>
                                </tr>
                                <tr>
                                    <td width="30%">
                                        <asp:TextBox ID="txtArticaldesc" runat="server" CssClass="SmallFont" Width="70px"></asp:TextBox>
                                    </td>
                                    <td width="70%">
                                        <asp:ImageButton ID="btnArticaldesc" ImageUrl="~/CommonImages/Icons/Search.png" runat="server"
                                            OnClick="btnOrderNo_Click" CssClass="SmallFont" Text="Go" />
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass=" label SmallFont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Uom">
                        <ItemTemplate>
                            <asp:Label ID="lblUOM" runat="server" CssClass=" SmallFont" Text='<%# Eval("UOM") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <table width="100%">
                                <tr>
                                    <td colspan="2" width="100%">
                                        Uom
                                    </td>
                                </tr>
                                <tr>
                                    <td width="80%">
                                        <asp:TextBox ID="txtUom" runat="server" CssClass="SmallFont" Width="70px"></asp:TextBox>
                                    </td>
                                    <td width="20%">
                                        <asp:ImageButton ID="btnUom" ImageUrl="~/CommonImages/Icons/Search.png" runat="server"
                                            OnClick="btnOrderNo_Click" CssClass="SmallFont" Text="Go" />
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass=" label SmallFont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" Visible="false" HeaderText="Pi Type">
                        <ItemTemplate>
                            <asp:Label ID="lblPI_TYPE" runat="server" CssClass=" SmallFont" Text='<%# Eval("PI_TYPE") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <table width="100%">
                                <tr>
                                    <td colspan="2" width="100%">
                                        Pi Type
                                    </td>
                                </tr>
                                <tr>
                                    <td width="80%">
                                        <asp:TextBox ID="txtPiType" runat="server" CssClass="SmallFont" Width="70px"></asp:TextBox>
                                    </td>
                                    <td width="20%">
                                        <asp:ImageButton ID="btnPiType" ImageUrl="~/CommonImages/Icons/Search.png" runat="server"
                                            CssClass="SmallFont" OnClick="btnPiType_Click" Text="Go" />
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass=" label SmallFont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                     <asp:BoundField HeaderText="Order Date" DataField="ORDER_DATE" />
                    <asp:BoundField HeaderText="Qty" DataField="ORD_QTY" />
                    <asp:BoundField HeaderText="Del Date" DataField="DEL_DATE" />
                    <asp:BoundField HeaderText="Packed Qty" DataField="PACKING_QTY" />
                    <asp:BoundField HeaderText="Qty Req" DataField="REQ_QTY" />
                    <asp:BoundField HeaderText="Qty Issued" DataField="ISS_QTY" />
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="View">
                    <ItemTemplate>
                    <table>
                    <tr>
                    <td class="SmallFont">
                    <asp:LinkButton ID="lnkviewCR" runat="server" CommandName="ViewCR" Text="ViewCR"></asp:LinkButton>
                    <asp:LinkButton ID="lnkViewBom" CommandName="ViewBom" runat="server">View Bom</asp:LinkButton>
                                      
                    </td>
                    <td class="SmallFont">
                    <asp:LinkButton ID="lnkViewProcessRoute" CommandName="ViewProcessRoute" runat="server">View ProcessRoute</asp:LinkButton>
                     <asp:LinkButton ID="lnkViewCost" CommandName="ViewCost" runat="server">View Cost</asp:LinkButton>
                                       
                                </td>
                    </tr>
                    </table>
                    </ItemTemplate>
                    
                    </asp:TemplateField>
                </Columns>
                <PagerStyle BackColor="#336799" ForeColor="White" HorizontalAlign="Left" />
                <AlternatingRowStyle BackColor="AliceBlue" />
                <SelectedRowStyle BackColor="#66CCFF" Font-Bold="True" ForeColor="Blue" />
                <HeaderStyle BackColor="#336799" ForeColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#336799" ForeColor="White" />
            </asp:GridView>
        </td>
    </tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>