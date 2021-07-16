<%@ Control Language="C#" AutoEventWireup="true" CodeFile="POList.ascx.cs" Inherits="Module_Inventory_Controls_POList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc2" %>
<table width="100%" align="left" class="tContentArial">
    <tr>
        <td align="left" valign="top" class="td" width="100%">
            <table align="left">
                <tr>
                    <%--<td id="tdUpdate" runat="server" align="left">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                            Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click1"></asp:ImageButton>
                    </td>
                    <td id="tdDelete" runat="server" align="left">
                        <asp:ImageButton ID="imgbtnDelete" runat="server" Width="48" Height="41" ToolTip="Delete"
                            ImageUrl="~/CommonImages/del6.png"></asp:ImageButton>
                    </td>--%>
                    <td id="tdFind" runat="server" align="left">
                        <asp:ImageButton ID="imgbtnFindTop" Width="48" Height="41" runat="server" ToolTip="Find"
                            ImageUrl="~/CommonImages/link_find.png" OnClick="imgbtnFindTop_Click"></asp:ImageButton>
                    </td>
                    <%-- <td>
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Width="48" Height="41" ToolTip="Print"
                            ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click"></asp:ImageButton>&nbsp;
                    </td>--%>
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
            <b class="titleheading">Material Purchase Order List</b>
        </td>
    </tr>
    <tr>
        <td align="left" valign="top" width="100%" class="td">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>
    <tr>
        <td align="left" width="100%" class="td">
            Select Filter Option :
            <cc2:OboutDropDownList ID="ddlFilterOption" runat="server">
                <asp:ListItem>ALL</asp:ListItem>
                <asp:ListItem>Pending</asp:ListItem>
                <asp:ListItem>Approved</asp:ListItem>
                <asp:ListItem>Canceled</asp:ListItem>
                <asp:ListItem>Closed</asp:ListItem>
            </cc2:OboutDropDownList>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" width="100%">
            <b>Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server"></asp:Label></b>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" width="100%">
            <asp:GridView ID="gvMaterialpo" CssClass="SmallFont" runat="server" AllowSorting="True"
                AutoGenerateColumns="False" OnRowDataBound="gvMaterialpo_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="PO_TYPE" HeaderText="PO Type">
                        <ItemStyle HorizontalAlign="Left" CssClass="label smallfont" Wrap="true" VerticalAlign="Top"
                            Width="50px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="po No">
                        <ItemTemplate>
                            <asp:Label ID="lblPO_NUMB" runat="server" ToolTip='<%# Bind("PO_NUMB") %>' Text='<%# Bind("PO_NUMB") %>'></asp:Label>
                            <asp:Label ID="lblPO_type" runat="server" Text='<%# Bind("PO_TYPE") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="labelNo smallfont" Wrap="true" HorizontalAlign="Right" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="PO_DATE" HeaderText="PO Date" HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}">
                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PO_NATURE" HeaderText="PO Nature" HtmlEncode="False">
                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PRTY_NAME" HeaderText="Party">
                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TRSP" HeaderText="Transporter">
                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CURRENCY_CODE" HeaderText="Currency Code">
                        <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CONVERSION_RATE" HeaderText="Conversion Rate">
                        <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DEL_DATE" HeaderText="Delivery Date" DataFormatString="{0:dd/MM/yyyy}">
                        <ItemStyle HorizontalAlign="Right" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PO_STATUS" HeaderText="PO Status">
                        <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CONF_DATE" HeaderText="Status Date" DataFormatString="{0:dd/MM/yyyy}">
                        <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CONF_BY" HeaderText="Status Updated By">
                        <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Show Details">
                        <ItemTemplate>
                            <asp:Panel ID="pnlView" runat="server">
                                <asp:LinkButton ID="lbtnView" runat="server" Text="View Detail" CssClass="Label"></asp:LinkButton>
                            </asp:Panel>
                            <asp:Panel ID="pnlShowHover" runat="server" Width="650px" BackColor="Beige" BorderWidth="2px"
                                Height="130px" ScrollBars="Auto">
                                <asp:GridView ID="grdPODetail" runat="server" Width="650px" CssClass="SmallFont"
                                    AutoGenerateColumns="False" Height="130px">
                                    <Columns>
                                        <asp:BoundField DataField="PO_NUMB" HeaderText="PO #">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PO_TYPE" HeaderText="PO Type">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ITEM_DESC" HeaderText="Item Description">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UOM" HeaderText="Unit">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ORD_QTY" HeaderText="Ordered Qty.">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="QUOTATION_NO" HeaderText="Quotation No">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DEL_DATE" HeaderText="Delivery Date" HtmlEncode="false"
                                            DataFormatString="{0:M-dd-yyyy}">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="QTY_RCPT" HeaderText="Qty. Receipt">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="QTY_RTN" HeaderText="Qty. Return">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="BASIC_RATE" HeaderText="Basic Rate">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FINAL_RATE" HeaderText="Final Rate">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                    </Columns>
                                    <RowStyle CssClass="SmallFont" Width="98%" />
                                    <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="BlueViolet" />
                                </asp:GridView>
                            </asp:Panel>
                            <cc1:HoverMenuExtender ID="HoverMenuExtender1" runat="server" TargetControlID="pnlView"
                                PopupControlID="pnlShowHover" PopupPosition="Left" PopDelay="10">
                            </cc1:HoverMenuExtender>
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
