<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Item_QC_MasterList.ascx.cs"
    Inherits="Module_Inventory_Controls_Item_QC_MasterList" %>
<table align="left" class=" td tContentArial" width="100%">
    <tr>
        <td>
            <table class=" td tContentArial">
                <tr>
                    <td id="tdUpdate" runat="server" align="left">
                        <asp:ImageButton ID="imgbtnAddNew" runat="server" Width="48" Height="41" ToolTip="Add New"
                            ImageUrl="~/CommonImages/addnew.png" OnClick="imgbtnAddNew_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" OnClick="imgbtnPrint_Click" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExport" runat="server" Width="48" Height="41" ToolTip="Print"
                            ImageUrl="~/CommonImages/export.png" onclick="imgbtnExport_Click" ></asp:ImageButton>&nbsp;
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
                            ToolTip="Help" Width="48" OnClick="imgbtnHelp_Click" />
                    </td>
                </tr>
            </table>
            <tr>
                <td align="center" class="TableHeader td">
                    <span class="titleheading"><strong>Item QC Standard Master List</strong> </span>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <b>
                        <asp:Label ID="Label1" runat="server" Text="Total Record : " CssClass="Label"></asp:Label>
                        <asp:Label ID="lblTotalRecord" runat="server" CssClass="Label"></asp:Label></b>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="width: 100%;">
                        <asp:GridView ID="grdITEMMasterQuery" runat="server" AutoGenerateColumns="False"
                            Width="100%" AllowPaging="True" AllowSorting="True" CellPadding="3" BorderStyle="Ridge"
                            CssClass="smallfont" Font-Size="X-Small" ForeColor="#333333" PagerStyle-HorizontalAlign="Left"
                            OnPageIndexChanging="grUserMasterQuery_PageIndexChanging" >
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                            <Columns>
                                <asp:TemplateField HeaderText="Item Category">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Item Category
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtITEM_CATEGORY" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnITEM_CATEGORY" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblITEM_CATEGORY" runat="server" Text='<%#Eval("ITEM_CATEGORY") %>'
                                            ToolTip='<%#Eval("ITEM_CATEGORY") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="	Item Code">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Item Code
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtITEM_CODE" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnITEM_CODE" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblITEM_CODE" runat="server" Text='<%#Eval("ITEM_CODE") %>' ToolTip='<%#Eval("ITEM_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Desc">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Item Desc
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtItemDesc" Width="120px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnItemDesc" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemDesc" runat="server" Text='<%#Eval("ITEM_DESC") %>' ToolTip='<%#Eval("ITEM_DESC") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Std Value">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Std Value
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtSTD_VALUE" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnSTD_VALUE" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSTD_VALUE" runat="server" Text='<%#Eval("STD_VALUE") %>' ToolTip='<%#Eval("STD_VALUE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Std Type">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Std Type
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtSTD_TYPE" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnSTD_TYPE" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSTD_TYPE" runat="server" Text='<%#Eval("STD_TYPE") %>' ToolTip='<%#Eval("STD_TYPE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tolerance">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Tolerance
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtTOLERANCE" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnTOLERANCE" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTOLERANCE" runat="server" Text='<%#Eval("TOLERANCE") %>' ToolTip='<%#Eval("TOLERANCE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tolerance Type">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Tolerance Type
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtTOLERANCE_TYPE" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnTOLERANCE_TYPE" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTOLERANCE_TYPE" runat="server" Text='<%#Eval("TOLERANCE_TYPE") %>'
                                            ToolTip='<%#Eval("TOLERANCE_TYPE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tolerance Range">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Tolerance Range
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtTOLERANCE_RANGE" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnTOLERANCE_RANGE" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTOLERANCE_RANGE" runat="server" Text='<%#Eval("TOLERANCE_RANGE") %>'
                                            ToolTip='<%#Eval("TOLERANCE_RANGE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                 <asp:TemplateField HeaderText="Max Value">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Max Value
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtMAX_VALUE" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnMAX_VALUE" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMAX_VALUE" runat="server" Text='<%#Eval("MAX_VALUE") %>'
                                            ToolTip='<%#Eval("MAX_VALUE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                 <asp:TemplateField HeaderText="Min Value">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Min Value
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtMIN_VALUE" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnMIN_VALUE" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMIN_VALUE" runat="server" Text='<%#Eval("MIN_VALUE") %>'
                                            ToolTip='<%#Eval("MIN_VALUE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="UOM">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    UOM
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtUOM" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnUOM" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblUOM" runat="server" Text='<%#Eval("UOM") %>' ToolTip='<%#Eval("UOM") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Remarks
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtREMARKS" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnREMARKS" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblREMARKS" runat="server" Text='<%#Eval("REMARKS") %>' ToolTip='<%#Eval("REMARKS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Status
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtSTATUS" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnSTATUS" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSTATUS" runat="server" Text='<%#Eval("STATUS") %>' ToolTip='<%#Eval("STATUS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </div>
                </td>
            </tr>
        </td>
    </tr>
</table>
