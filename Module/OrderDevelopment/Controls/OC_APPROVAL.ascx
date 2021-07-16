<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OC_APPROVAL.ascx.cs" Inherits="Module_OrderDevelopment_Controls_OC_APPROVAL" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<table width="100%" align="left" class="tContentArial">
    <tr>
        <td align="left" valign="top" class="td" width="100%">
            <table align="left">
                <tr>
                    <td id="tdUpdate" runat="server" align="left">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                            Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click1"></asp:ImageButton>
                    </td>
                    <td id="tdFind" runat="server" visible="false" align="left">
                        <asp:ImageButton ID="imgbtnFindTop" Width="48" Height="41" runat="server" ToolTip="Find"
                            ImageUrl="~/CommonImages/link_find.png"></asp:ImageButton>
                    </td>
                    <td>cmbArticleNo_LoadingItems
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Width="48" Height="41" ToolTip="Print"
                            ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click"></asp:ImageButton>&nbsp;
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" ToolTip="Clear"
                            ImageUrl="~/CommonImages/clear.jpg" OnClick="imgbtnClear_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" Width="48" Height="41" ToolTip="Exit"
                            ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" runat="server" Width="48" Height="41" ToolTip="Help"
                            ImageUrl="~/CommonImages/link_help.png" OnClick="imgbtnHelp_Click"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader td" width="100%">
            <b class="titleheading">
                <asp:Label ID="lblFormHeading" runat="server" CssClass="SmallFont"></asp:Label></b>
        </td>
    </tr>
    <tr>
        <td align="left" valign="top" width="100%" class="td">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>
    <tr>
        <td align="center" width="100%" class="td">
            <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" width="100%">
            <b>Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server"></asp:Label></b>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" width="100%">
            <asp:GridView ID="grdORDER_APPROVAL" CssClass="SmallFont" runat="server" AllowSorting="True"
                AutoGenerateColumns="False" OnRowDataBound="grdORDER_APPROVAL_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Business Type">
                        <ItemTemplate>
                            <asp:Label ID="lblBUSINESS_TYPE" runat="server" Text='<%# Bind("BUSINESS_TYPE") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="SmallFont tdLeft" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Product Type">
                        <ItemTemplate>
                            <asp:Label ID="lblPRODUCT_TYPE" runat="server" Text='<%# Bind("PRODUCT_TYPE") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="SmallFont tdLeft" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Order Category">
                        <ItemTemplate>
                            <asp:Label ID="lblORDER_CAT" runat="server" Text='<%# Bind("ORDER_CAT") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="SmallFont tdLeft" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Order Type">
                        <ItemTemplate>
                            <asp:Label ID="lblORDER_TYPE" runat="server" Text='<%# Bind("ORDER_TYPE") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="SmallFont tdLeft" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Order #">
                        <ItemTemplate>
                            <asp:Label ID="lblORDER_NO" runat="server" Text='<%# Bind("ORDER_NO") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="SmallFont tdRight" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Order Date">
                        <ItemTemplate>
                            <asp:Label ID="lblORDER_DATE" runat="server" Text='<%# Bind("ORDER_DATE", "{0:dd-MM-yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="SmallFont tdLeft" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Party Ref #">
                        <ItemTemplate>
                            <asp:Label ID="lblPARTY_REF_NO" runat="server" Text='<%# Bind("PARTY_REF_NO") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="SmallFont tdLeft" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Party Ref Date">
                        <ItemTemplate>
                            <asp:Label ID="lblPARTY_REF_DATE" runat="server" Text='<%# Bind("PARTY_REF_DATE", "{0:dd-MM-yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="SmallFont tdLeft" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Order Process">
                        <ItemTemplate>
                            <asp:Label ID="lblORDER_PROCESS" runat="server" Text='<%# Bind("ORDER_PROCESS") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="SmallFont tdLeft" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Shipment">
                        <ItemTemplate>
                            <asp:Label ID="lblSHIPMENT" runat="server" Text='<%# Bind("SHIPMENT") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="SmallFont tdLeft" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Consignee">
                        <ItemTemplate>
                            <asp:Label ID="lblCONSIGNEE_DATA" runat="server" Text='<%# Bind("CONSIGNEE_NAME") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="SmallFont tdLeft" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Party">
                        <ItemTemplate>
                            <asp:Label ID="lblPRTY_DATA" runat="server" Text='<%# Bind("PRTY_CODE") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="SmallFont tdLeft" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Currency">
                        <ItemTemplate>
                            <asp:Label ID="lblCURRENCY_CODE" runat="server" Text='<%# Bind("CURRENCY_CODE") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="SmallFont tdLeft" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Conversion Rate">
                        <ItemTemplate>
                            <asp:Label ID="lblCONV_RATE" runat="server" Text='<%# Bind("CONV_RATE") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="SmallFont tdRight" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Confirm">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkApproved" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Confirm Date">
                        <ItemTemplate>
                            <asp:TextBox ID="txtConfirmDate" runat="server" ReadOnly="true" Text='<%# Bind("CONF_DATE", "{0:dd-MM-yyyy}") %>'
                                CssClass=" TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Confirm By">
                        <ItemTemplate>
                            <asp:TextBox ID="txtConfirmBy" runat="server" ReadOnly="true" Text='<%# Bind("CONF_BY") %>'
                                ToolTip='<%# Bind("CONF_BY") %>' CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnViewOCTRN" runat="server" Text="View Details"></asp:LinkButton>
                            <asp:Panel ID="pnlOCTRN" runat="server" Width="90%" BackColor="Beige" BorderWidth="2px"
                                Height="140px" ScrollBars="Auto">
                                <asp:GridView ID="grdTRNDetail" runat="server" AutoGenerateColumns="False" CssClass="SmallFont"
                                    Width="99%" OnRowDataBound="grdTRNDetail_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTRNBT" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("BUSINESS_TYPE") %>'></asp:Label>
                                                <asp:Label ID="lblTRNPT" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("PRODUCT_TYPE") %>'></asp:Label>
                                                <asp:Label ID="lblTRNOC" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("ORDER_CAT") %>'></asp:Label>
                                                <asp:Label ID="lblTRNOT" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("ORDER_TYPE") %>'></asp:Label>
                                                <asp:Label ID="lblTRNON" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("ORDER_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PI Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTRNPI_Type" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("PI_TYPE") %>'></asp:Label></ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PI Indent #">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTRNPI_NO" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("PI_NO") %>'></asp:Label></ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Artical Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTRNArticalCode" runat="server" CssClass="Label SmallFont" Text='<%# Bind("ARTICAL_CODE") %>'></asp:Label></ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UOM">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTRNUOM" runat="server" CssClass="Label SmallFont" Text='<%# Bind("UOM") %>'></asp:Label></ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTRNOrderQuantity" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("ORD_QTY") %>'></asp:Label></ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delivery Date">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="txtTRNDelDate" runat="server" CssClass="Label SmallFont" Text='<%# Bind("DEL_DATE", "{0:dd-MM-yyyy}") %>'>
                                                </asp:LinkButton>
                                                <asp:Panel ID="pnlDelSchedule" runat="server" BackColor="#C5E7F1" BorderColor="BlueViolet"
                                                    BorderStyle="Solid" BorderWidth="5px">
                                                    <asp:GridView ID="grdDelSchedule" runat="server" BorderColor="#C5E7F1" AutoGenerateColumns="False">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Delivery Address">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtDelAdd" runat="server" CssClass="Label" Text='<%# Bind("DEL_ADDRESS") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delivery Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtDelDate" runat="server" CssClass="Label" Text='<%# Bind("DEL_DATE", "{0:dd-MM-yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delivery Quantity">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtDelQty" runat="server" CssClass="Label" Text='<%# Bind("DEL_QTY") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delivery Remarks">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtDelRemarks" runat="server" CssClass="Label" Text='<%# Bind("DEL_REMARKS") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <RowStyle CssClass="SmallFont" />
                                                        <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                                <cc1:HoverMenuExtender ID="hmeDelSchedule" runat="server" PopupControlID="pnlDelSchedule"
                                                    PopupPosition="Bottom" TargetControlID="txtTRNDelDate">
                                                </cc1:HoverMenuExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cost Price">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="txtTRNCost" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TOTAL_COST") %>'>
                                                </asp:LinkButton>
                                                <asp:Panel ID="pnlCostDetail" runat="server" BackColor="#C5E7F1" BorderColor="BlueViolet"
                                                    BorderStyle="Solid" BorderWidth="5px">
                                                    <asp:DataList ID="dlTRNYRNSPIN_Cost" runat="server">
                                                        <ItemTemplate>
                                                            <table style="color: Black;">
                                                                <tr>
                                                                    <td class="tdRight">
                                                                        Sale Rate :
                                                                    </td>
                                                                    <td class="tdLeft">
                                                                        <asp:Label ID="txtYRNSPIN_COST_Sale" runat="server" Text='<%# Bind("SALE") %>' CssClass="LabelNo"
                                                                            Width="80px"></asp:Label>
                                                                    </td>
                                                                    <td class="tdRight">
                                                                        Freight :
                                                                    </td>
                                                                    <td class="tdLeft">
                                                                        <asp:Label ID="txtYRNSPIN_COST_Freight" runat="server" Text='<%# Bind("FREIGHT") %>'
                                                                            CssClass="LabelNo" Width="80px">0</asp:Label>
                                                                    </td>
                                                                    <td class="tdRight">
                                                                        Commission :
                                                                    </td>
                                                                    <td class="tdLeft">
                                                                        <asp:Label ID="txtYRNSPIN_COST_Commission" runat="server" Text='<%# Bind("COMMISSION") %>'
                                                                            CssClass="LabelNo" Width="80px">0</asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="tdRight">
                                                                        Incentives :
                                                                    </td>
                                                                    <td class="tdLeft">
                                                                        <asp:Label ID="txtYRNSPIN_COST_Incentives" runat="server" Text='<%# Bind("INCENTIVES") %>'
                                                                            CssClass="LabelNo" Width="80px">0</asp:Label>
                                                                    </td>
                                                                    <td class="tdRight">
                                                                        Brokerage :
                                                                    </td>
                                                                    <td class="tdLeft">
                                                                        <asp:Label ID="txtYRNSPIN_COST_Brokerage" runat="server" Text='<%# Bind("BROKERAGE") %>'
                                                                            CssClass="LabelNo" Width="80px">0</asp:Label>
                                                                    </td>
                                                                    <td class="tdRight">
                                                                        Ex-Mill Rate :
                                                                    </td>
                                                                    <td class="tdLeft">
                                                                        <asp:Label ID="txtYRNSPIN_COST_ExMill" runat="server" Text='<%# Bind("EX_MILL_RATE") %>'
                                                                            CssClass="LabelNo" Width="80px">0</asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                    </asp:DataList>
                                                </asp:Panel>
                                                <cc1:HoverMenuExtender ID="hmeCost" runat="server" PopupControlID="pnlCostDetail"
                                                    PopupPosition="Bottom" TargetControlID="txtTRNCost">
                                                </cc1:HoverMenuExtender>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Shade">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTRNShade" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("SHADE") %>'></asp:Label></ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="BOM">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="txtTRNBOM" runat="server" CssClass="Label SmallFont" Text="View BOM">
                                                </asp:LinkButton>
                                                <asp:Panel ID="pnlBOM" runat="server" BackColor="#C5E7F1" BorderColor="BlueViolet"
                                                    BorderStyle="Solid" BorderWidth="5px">
                                                    <asp:GridView ID="grdBOM" runat="server" AutoGenerateColumns="False">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Warp/Weft">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgrdW_SIDE" runat="server" CssClass="SmallFont Label" Text='<%# Bind("W_SIDE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Product Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgrdPRODUCT_TYPE" runat="server" CssClass="SmallFont Label" Text='<%# Bind("BASE_ARTICAL_TYPE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Article Code">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgrdARTICLE_CODE" runat="server" CssClass="SmallFont Label" Text='<%# Bind("BASE_ARTICAL_CODE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="UOM">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgrdUOM" runat="server" CssClass="SmallFont Label" Text='<%# Bind("UOM") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Basis">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgrdBASIS" runat="server" CssClass="SmallFont Label" Text='<%# Bind("BASIS") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Value Qty">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgrdVALUE_QTY" runat="server" CssClass="SmallFont LabelNo" Text='<%# Bind("VALUE_QTY") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Requested Qty">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgrdREQ_QTY" runat="server" CssClass="SmallFont LabelNo" Text='<%# Bind("REQ_QTY") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <RowStyle CssClass="SmallFont" />
                                                        <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                                <cc1:HoverMenuExtender ID="hmeBOM" runat="server" PopupControlID="pnlBOM" PopupPosition="Bottom"
                                                    TargetControlID="txtTRNBOM">
                                                </cc1:HoverMenuExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Design">
                                            <ItemTemplate>
                                                <asp:Label ID="txtTRNDesign" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("DESIGN") %>'></asp:Label></ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Lot No">
                                            <ItemTemplate>
                                                <asp:Label ID="txtTRNLotNo" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("LOT_NO") %>'></asp:Label></ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Srinkage/ Process Loss">
                                            <ItemTemplate>
                                                <asp:Label ID="txtTRNSrinkage" runat="server" CssClass="Label SmallFont" Text='<%# Bind("SRINKAGE") %>'></asp:Label></ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#336699" CssClass="SmallFont titleheadingGrid" />
                                    <RowStyle CssClass="SmallFont" />
                                </asp:GridView>
                                <asp:Button ID="btnTRNDetailOk" runat="server" Text="Close This" />
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="OCTRN" BackgroundCssClass="modalBackground" TargetControlID="lbtnViewOCTRN"
                                PopupControlID="pnlOCTRN" CancelControlID="btnTRNDetailOk" DropShadow="true"
                                runat="server">
                            </cc1:ModalPopupExtender>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                    </asp:TemplateField>
                </Columns>
                <RowStyle CssClass="SmallFont" Width="98%" />
                <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
            </asp:GridView>
        </td>
    </tr>
</table>
