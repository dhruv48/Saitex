<%@ Control Language="C#" AutoEventWireup="true" CodeFile="POApproval.ascx.cs" Inherits="Module_Inventory_Controls_POApproval" %>
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
                    <td id="tdDelete" runat="server" align="left">
                        <asp:ImageButton ID="imgbtnDelete" runat="server" Width="48" Height="41" ToolTip="Delete"
                            ImageUrl="~/CommonImages/del6.png"></asp:ImageButton>
                    </td>
                    <td id="tdFind" runat="server" visible="false" align="left">
                        <asp:ImageButton ID="imgbtnFindTop" Width="48" Height="41" runat="server" ToolTip="Find"
                            ImageUrl="~/CommonImages/link_find.png"></asp:ImageButton>
                    </td>
                    <td>
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
            <b class="titleheading">Material Purchase Order Approval</b>
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
            <asp:GridView ID="gvMaterialpoApproval"   CssClass="SmallFont" runat="server" AllowSorting="True"
                AutoGenerateColumns="False" OnRowDataBound="gvMaterialpoApproval_RowDataBound" Width="95%">
                <Columns>
                    <asp:BoundField DataField="PO_TYPE" HeaderText="PO Type">
                        <ItemStyle HorizontalAlign="Left" CssClass="label smallfont" Wrap="true" VerticalAlign="Top"
                            Width="50px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="PO No" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblPO_NUMB" runat="server" ToolTip='<%# Bind("PO_NUMB") %>' Text='<%# Bind("PO_NUMB") %>'
                                CssClass="SmallFont LabelNo"></asp:Label>
                            <asp:Label ID="lblPO_type" runat="server" Text='<%# Bind("PO_TYPE") %>' Visible="false"></asp:Label>
                        </ItemTemplate>

<HeaderStyle HorizontalAlign="Right"></HeaderStyle>

                        <ItemStyle CssClass="labelNo SmallFont" Wrap="true" HorizontalAlign="Right" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="PO_NATURE" HeaderText="PO Nature" HtmlEncode="False">
                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PARTY_DATA" HeaderText="Party">
                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DEL_DATE" HeaderText="Delivery Date" DataFormatString="{0:dd/MM/yyyy}">
                        <ItemStyle HorizontalAlign="Right" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Confirm">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkApproved" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Confirm Date">
                        <ItemTemplate>
                            <asp:TextBox ID="txtConfirmDate" runat="server" ReadOnly="true" Text='<%# Bind("CONF_DATE") %>'
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
                            <asp:LinkButton ID="lbtnViewPOTRN" runat="server" Text="View Details" 
                                onclick="lbtnViewPOTRN_Click"></asp:LinkButton>
                            <asp:Panel ID="pnlPOTRN" runat="server" Width="520px" BackColor="Beige" BorderWidth="2px"
                                Height="140px" ScrollBars="Auto">
                                <asp:GridView ID="grdPOTRN" runat="server" AutoGenerateColumns="False" Width="500px"
                                    CssClass="SmallFont" Height="140px">
                                    <Columns>
                                        <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ITEM_DESC" HeaderText="Item Description">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="HSN_CODE" HeaderText="HSN Code">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UOM" HeaderText="UOM">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ORD_QTY" HeaderText="Ordered Qty" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="LabelNo SmallFont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="BASIC_RATE" HeaderText="Basic Rate" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="LabelNo SmallFont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FINAL_RATE" HeaderText="Final Rate" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="LabelNo SmallFont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="VALUE" HeaderText="Value">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="QUOTATION_NO" HeaderText="Quotation No">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DEL_DATE" HeaderText="Delivery Date">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="COMMENTS" HeaderText="Comments">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                    </Columns>
                                    <RowStyle CssClass="SmallFont" Width="98%" />
                                    <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="BlueViolet" />
                                </asp:GridView>
                            </asp:Panel>
                            <cc1:HoverMenuExtender ID="hmePOTRN" runat="server" PopupPosition="Left" PopupControlID="pnlPOTRN"
                                TargetControlID="lbtnViewPOTRN">
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
