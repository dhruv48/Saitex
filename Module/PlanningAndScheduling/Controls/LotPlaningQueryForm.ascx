<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LotPlaningQueryForm.ascx.cs"
    Inherits="Module_PlanningAndScheduling_Controls_LotPlaningQueryForm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<link href="../../../StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
<asp:UpdatePanel ID="Update1" runat="server">
<ContentTemplate>
<table class="td tContentArial" width="95%">
    <tr>
        <td class="td" width="95%">
            <table>
                <tr>
                    <td>
                        <asp:ImageButton ID="imgPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" OnClick="imgPrint_Click" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                            ToolTip="Clear" OnClick="imgClear_Click" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgExit" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                            ToolTip="Exit" OnClick="imgExit_Click" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader" colspan="3">
            <table width="100%">
                <tr>
                    <td align="center" style="background-color: #336799; color: white;">
                        <b class="titleheading">Lot Planning </b>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:Label ID="LblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
            <asp:Label ID="LblError" runat="server" CssClass="UserError"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="td">
            <table width="100%" style="font-weight: bold">
                <tr>
                    <td align="right" valign="top" class="tdRight" width="12%">
                        <asp:Label ID="lblProducttype" runat="server" Text="PRODUCT:" CssClass="SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="12%">
                        <asp:DropDownList ID="ddlProductType" CssClass="SmallFont TextBox UpperCase BoldFont"
                            runat="server" Width="98%">
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight" width="12%" valign="top">
                        <asp:Label ID="lblOrderCategory" runat="server" Text="Order Category:" CssClass="SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="12%">
                        <asp:DropDownList ID="ddlOrderCategory" runat="server" CssClass="SmallFont BoldFont"
                            Width="98%">
                            <asp:ListItem>DIRECT SALE</asp:ListItem>
                            <asp:ListItem>INHOUSE</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight" width="12%" valign="top">
                        <asp:Label ID="lblBranch" runat="server" Text="Branch:" CssClass="SmallFont">
                        </asp:Label>
                    </td>
                    <td class="tdLeft" width="12%">
                        <asp:DropDownList ID="ddlBranch" CssClass="SmallFont TextBox UpperCase BoldFont"
                            runat="server" Width="98%">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="12%">
                        <asp:Label ID="lblOrderType" runat="server" Text="Order Type:" CssClass="SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="12%">
                        <asp:DropDownList ID="ddlordertype" runat="server" CssClass="SmallFont BoldFont UPPERCASE"
                            Width="99%">
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight" width="12%">
                        <asp:Label ID="lblStatus" runat="server" Text="STATUS:" CssClass="SmallFont"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="SmallFont TextBox UpperCase"
                            TabIndex="3" Width="120px">
                            <asp:ListItem>ALL</asp:ListItem>
                            <asp:ListItem>Un-Approved</asp:ListItem>
                            <asp:ListItem>Approved</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight">
                        <asp:Label ID="lblBusinessType" runat="server" Text="BUSINESS TYPE:" CssClass="SmallFont">

                        </asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBusiness" runat="server" CssClass="SmallFont TextBox UpperCase">
                            <asp:ListItem Value="SW">Sales Work</asp:ListItem>
                            <asp:ListItem Value="JW">Job Work</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnSubmit" runat="server" Text="Search" OnClick="btnSubmit_Click" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="6">
        </td>
    </tr>
    <tr>
        <td class="td" width="100%">
            <b>Total Records: &nbsp; &nbsp; &nbsp;<asp:Label ID="lblTotalRecords" runat="server"></asp:Label></b>
        </td>
    </tr>
    <tr>
        <td align="left" width="100%" class="td">
            <asp:Panel ID="Panel1" runat="server" Width="100%">
                <span class="titleheading"><b>
                    <asp:GridView ID="grdLotPlanning" runat="server" AllowSorting="true" AllowPaging="true"
                        AutoGenerateColumns="false" PageSize="10" CellPadding="4" Font-Size="10px" ForeColor="#333333"
                        GridLines="Both" Width="885px" OnPageIndexChanging="grdLotPlanning_PageIndexChanging"
                        OnRowDataBound="grdLotPlanning_RowDataBound" OnRowCommand="grdLotPlanning_RowCommand">
                        <FooterStyle BackColor="#CCCCCC" />
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:TemplateField HeaderText="Comp Code" ItemStyle-HorizontalAlign="Left" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblComp_Code" runat="server" Text='<%# Eval("COMP_CODE") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branch Code" ItemStyle-HorizontalAlign="Left" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Branch_Code" runat="server" Text='<%# Eval("BRANCH_CODE") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Business Type" ItemStyle-HorizontalAlign="Left" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblBusiness_Type" runat="server" Text='<%# Eval("BUSINESS_TYPE") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product Type" ItemStyle-HorizontalAlign="Left" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblProduct_type" runat="server" Text='<%# Eval("PRODUCT_TYPE") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Order Cat" ItemStyle-HorizontalAlign="Left" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblOrder_Cat" runat="server" Text='<%# Eval("ORDER_CAT") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Order No" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblOrder_No" runat="server" Text='<%# Eval("ORDER_NO") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Order Type" ItemStyle-HorizontalAlign="Left" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblOrder_Type" runat="server" Text='<%# Eval("ORDER_TYPE") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Party Code" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblParty_Code" runat="server" Text='<%# Eval("PRTY_CODE") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Party Name" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblParty_Name" runat="server" Text='<%# Eval("PRTY_NAME") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PI Type" ItemStyle-HorizontalAlign="Left" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblPi_Type" runat="server" Text='<%# Eval("PI_TYPE") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PA No" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblPA_NO" runat="server" Text='<%# Eval("PI_NO") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Artical Code" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblArtical_Code" runat="server" Text='<%# Eval("ARTICAL_CODE") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Artical Desc" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblArtical_Desc" runat="server" Text='<%# Eval("ARTICAL_DESC") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UOM" ItemStyle-HorizontalAlign="Left" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UOM") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Shade Code" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblShade_Code" runat="server" Text='<%# Eval("SHADE_CODE") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ORD QTY" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblOrder_QTY" runat="server" Text='<%# Eval("ORD_QTY") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bom Flag" ItemStyle-HorizontalAlign="Left" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblBom_Flag" runat="server" Text='<%# Eval("BOM_FLAG") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cost Price Flag" ItemStyle-HorizontalAlign="Left"
                                Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblCost_Price_Flag" runat="server" Text='<%# Eval("COST_PRICE_FLAG") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Process Route Flag" ItemStyle-HorizontalAlign="Left"
                                Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblProcess_Route_Flag" runat="server" Text='<%# Eval("PROCESS_ROUTE_FLAG") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Lot Flag" ItemStyle-HorizontalAlign="Left" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblLot_Flag" runat="server" Text='<%# Eval("LOT_FLAG") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Final Lot Flag" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Final_Lot_Flag" runat="server" Text='<%# Eval("FINAL_LOT_CNF_FLAG") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ViewBOM" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkViewBOM" runat="server" CommandName="ViewBOM" CssClass="Label SmallFont"
                                        Text="ViewBOM"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ViewLotDetail" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkLotDetail" runat="server"  CssClass="Label SmallFont"
                                        Text="LotDetail">
                                    </asp:LinkButton>
                                    <asp:Panel ID="PnlLotDetail" runat="server" BackColor="#C5E7F1" BorderColor="Desktop" BorderStyle="Solid">
                                        <asp:GridView ID="grdLotDetail" runat="server">
                                            <Columns>
                                                <%--<asp:TemplateField HeaderText="LOT ID" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLotId" runat="server" Text='<% #Bind("LOT_ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                          
                                                <asp:TemplateField HeaderText="LOT QTY" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLotQty" runat="server" Text='<%#Bind("LOT_QTY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ORD QTY" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrdQty" runat="server" Text='<%#Bind("ORD_QTY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="REM QTY" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRemQty" runat="server" Text='<%#Bind("REM_QTY" )%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="LOT NO" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLotno" runat="server" Text='<%#Bind("LOT_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                            <RowStyle CssClass="SmallFont" />
                                        <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                                   </asp:GridView>
                                    </asp:Panel>
                                    <cc1:HoverMenuExtender ID="hmebom" runat="server" PopupControlID="PnlLotDetail" TargetControlID="linkLotDetail" PopupPosition="Left"></cc1:HoverMenuExtender>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="LotID" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <%--<asp:TextBox ID="txtLotID" runat="server" CssClass="Label SmallFont" Width="80px" ReadOnly="true"></asp:TextBox>
--%>
                                    <asp:Label ID="lblLot_id" runat="server" Text='<%# Eval("LOT_ID") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Lot Date" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="txtLotDate" runat="server" CssClass="Label SmallFont" Width="65px"
                                        Text='<%# Eval("LOT_CNF_DATE") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Confirm By" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="txtCofBy" runat="server" CssClass="TextBox SmallFont TextBoxDisplay"
                                        Width="70px" Text='<%# Eval("LOT_CNF_BY") %>' ReadOnly="true"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Left">
 <ItemTemplate>
 <asp:TextBox ID="txtRemarks" runat="server" CssClass="Label SmallFont"
   Width="100px"></asp:TextBox>
 </ItemTemplate>
 </asp:TemplateField>--%>
                        </Columns>
                        <PagerStyle BackColor="#336799" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#336799" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </b></span>
            </asp:Panel>
        </td>
    </tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>