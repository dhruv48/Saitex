<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PO_QUERY.ascx.cs" Inherits="Module_OrderDevelopment_Controls_PO_QUERY" %>
<%@ Register Src="../../../CommonControls/LOV/PartyCodeLOV.ascx" TagName="PartyCodeLOV"
    TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<link href="../../../StyleSheet/abhishek.css" rel="stylesheet" type="text/css" />
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
<table width="100%">
    <tr>
        <td>
            <table>
                <tr>
                    <td id="tdClear" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                            ToolTip="Clear" Width="48" OnClick="imgbtnClear_Click" />
                    </td>
                    <td id="tdPrint" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" Width="48" />
                    </td>
                    <td id="tdExit" runat="server">
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                            Width="48" Height="41" OnClick="imgbtnExit_Click"></asp:ImageButton>
                    </td>
                    <td id="tdHelp" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                            ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td align="center" valign="top" class="hd" style="color: White;">
                        PO QUERY
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td align="Right" valign="top" width="15%" style="font-size: 14px;">
                        PO Date From
                    </td>
                    <td align="left" valign="top" width="25%">
                        <asp:TextBox ID="TxtFdate" runat="server" Width="100px" Height="15px"></asp:TextBox>
                        To<asp:TextBox ID="TxtTdate" runat="server" Width="100px" Height="15px"></asp:TextBox>
                    </td>
                    <td align="Right" valign="top" width="5%" style="font-size: 14px;">
                        Party Code
                    </td>
                    <td align="left" valign="top" width="20%">
                        <uc1:PartyCodeLOV ID="ddlprtycode" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="Right" valign="top" width="15%" style="font-size: 14px;">
                        PO number From
                    </td>
                    <td align="left" valign="top" width="20%">
                        <asp:TextBox ID="TxtpoFrom" runat="server" Width="37px" CssClass="textboxno" Font-Size="11px"
                            ToolTip="Enter Po Number From"></asp:TextBox>
                        To<asp:TextBox ID="TxtpoTo" runat="server" Width="37px" CssClass="textboxno" Font-Size="11px"
                            ToolTip="Enter Po Number To"></asp:TextBox>
                    </td>
                    <td align="Right" valign="top" width="5%">
                    </td>
                    <td align="left" valign="top" width="25%">
                        <asp:Button ID="btnview" runat="server" Text="Filter" Width="135px" Height="21px"
                            OnClick="btnview_Click" />
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td align="left" valign="top" class="cl">
                        <asp:Panel ID="panel3" runat="server" ScrollBars="Auto" Width="950px" Height="250px">
                            <asp:GridView ID="Podata_Grid" runat="server" Font-Size="11px" CellPadding="4" ForeColor="#333333"
                                GridLines="None" AutoGenerateColumns="false">
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#EFF3FB" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Indent" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIndent" runat="server" Text=""></asp:Label>
                                            <asp:LinkButton ID="linkbom" runat="server" CommandName="viewindent" CssClass="Label SmallFont"
                                                Text="View Indent">
                                            </asp:LinkButton>
                                            <asp:Panel ID="pnlindent" runat="server" BorderStyle="Ridge" BorderWidth="5px" BackColor="#C5E7F1">
                                                <asp:GridView ID="grdvwindent" runat="server" AutoGenerateColumns="false" BorderColor="#C5E7F1">
                                                    <Columns>
                                                    
                                                     <%-- <asp:TemplateField HeaderText="W SIDE">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtwside" runat="server" CssClass="Label" Text='<%# Bind("W_SIDE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                                    <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                                </asp:TemplateField>--%>
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Party Code" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprtycode" runat="server" Text='<%# Eval("PRTY_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Party Name" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprtyname" runat="server" Text='<%# Eval("PRTY_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PO Number" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblponumber" runat="server" Text='<%# Eval("PO_NUMB") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PO Type" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpotype" runat="server" Text='<%# Eval("PO_TYPE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PO Date" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpodate" runat="server" Text='<%# Eval("PO_DATE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Po Nature" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblponature" runat="server" Text='<%# Eval("PO_NATURE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delevery Date" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldeldate" runat="server" Text='<%# Eval("DEL_DATE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delevery Branch" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldelbrnch" runat="server" Text='<%# Eval("DELV_BRANCH") %>'></asp:Label>
                                        </ItemTemplate>
                                        <%--  <HeaderStyle Width="60px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delevery Branch Code" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldelbrnch_cd" runat="server" Text='<%# Eval("DELV_BRANCH_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delevery Status" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldelstatus" runat="server" Text='<%# Eval("DEL_STATUS") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transporter Code" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltrspcode" runat="server" Text='<%# Eval("TRSP_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Currency Code" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcurrency_cd" runat="server" Text='<%# Eval("CURRENCY_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Conversion Rate" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblconversion_rt" runat="server" Text='<%# Eval("CONVERSION_RATE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Code" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblitemcode" runat="server" Text='<%# Eval("ITEM_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item_Desc" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblitemdesc" runat="server" Text='<%# Eval("ITEM_DESC") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order Qty" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblorderqty" runat="server" Text='<%# Eval("ORD_QTY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UOM" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluom" runat="server" Text='<%# Eval("UOM") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quotation No" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblquotation_no" runat="server" Text='<%# Eval("QUOTATION_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty Rcpt" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblqtyrcpt" runat="server" Text='<%# Eval("QTY_RCPT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Basic Rate" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbasic_rt" runat="server" Text='<%# Eval("BASIC_RATE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Final_Rate" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfinal_rt" runat="server" Text='<%# Eval("FINAL_RATE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                                    </asp:TemplateField>
                                    <%--    <asp:TemplateField HeaderText="Indent Po Adjust" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfinal_rt" runat="server" Text='<%# Eval("FINAL_RATE") %>'></asp:Label>
                                             <asp:LinkButton ID="linkbom" runat="server" CommandName="viewbom" CssClass="Label SmallFont"
                                                    Text="View adjust PO">
                                                </asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                                    </asp:TemplateField>--%>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <cc1:CalendarExtender ID="cetsub_FDT" Format="dd/MM/yyyy" TargetControlID="TxtFdate"
                            runat="server">
                        </cc1:CalendarExtender>
                        <cc1:CalendarExtender ID="cetsub_TODT" Format="dd/MM/yyyy" TargetControlID="TxtTdate"
                            runat="server">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<%--</ContentTemplate>
</asp:UpdatePanel>--%>