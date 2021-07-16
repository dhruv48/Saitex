<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ITEM_MATER_QUERY.ascx.cs"
    Inherits="Module_Inventory_Controls_ITEM_MATER_QUERY" %>
    <style type="text/css">
    #gridDiv
    {
    	width:1100px;
    	height:350px;
    	overflow:scroll;
    }

</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
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
                            ToolTip="Print" OnClick="imgbtnPrint_Click1" />
                    </td>
                    <td>
                        <asp:ImageButton ID="ImageButton1" runat="server" Width="48" Height="41" ToolTip="Print"
                            ImageUrl="~/CommonImages/export.png" OnClick="imgbtnPrint_Click"></asp:ImageButton>&nbsp;
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
            
         </td>
         </tr>
 <tr>
                <td align="center" class="TableHeader td">
                    <span class="titleheading"><strong>Item Master List</strong> </span>
                </td>
 </tr>
            <tr>
                <td align="left">
                    <b>
                        <asp:Label ID="Label1" runat="server" Text="Total Record : " CssClass="Label"></asp:Label>
                        <asp:Label ID="lblTotalRecord" runat="server" CssClass="Label"></asp:Label></b>
                </td>
                <%--     <td align="left" valign="top" width="100%" cssclass="Label">
                                <b>
                                    <asp:UpdateProgress ID="UpdateProgress9" runat="server">
                                        <ProgressTemplate>
                                            Loading...</ProgressTemplate>
                                    </asp:UpdateProgress>
                                </b>
                            </td>--%>
            </tr>
            <tr >
                <td align="center">
                <div id="gridDiv"  >
                
                <asp:GridView ID="grdITEMMasterQuery" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                        Width="100%" AllowPaging="True" AllowSorting="True" CellPadding="3" BorderStyle="Ridge"
                        CssClass="smallfont" Font-Size="X-Small" ForeColor="#333333" PagerStyle-HorizontalAlign="Left"
                        OnPageIndexChanging="grUserMasterQuery_PageIndexChanging" 
                      
                        onselectedindexchanged="grdITEMMasterQuery_SelectedIndexChanged">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#EFF3FB" />
                        <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                        <Columns>
                            <asp:TemplateField HeaderText="Department">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                                Department
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtDepartment" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnDepartment" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDepartment" runat="server" Text='<%#Eval("DEPT_NAME") %>' ToolTip='<%#Eval("DEPT_NAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="	Cat.Code">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                                Item Cat
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtCatCode" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnCatCode" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCatCode" runat="server" Text='<%#Eval("CAT_CODE") %>' ToolTip='<%#Eval("CAT_CODE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="	Item Type">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                                Item Type
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtItemType" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnItemType" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblItemType" runat="server" Text='<%#Eval("ITEM_TYPE") %>' ToolTip='<%#Eval("ITEM_TYPE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Code">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                                Item Code
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtItemCode" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnItemCode" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblItemCode" runat="server" Text='<%#Eval("ITEM_CODE") %>' ToolTip='<%#Eval("ITEM_CODE") %>'></asp:Label>
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
                            
                            
                             <asp:TemplateField HeaderText="HSN CODE">
                                <HeaderTemplate>
                                    <table width="50%">
                                        <tr>
                                            <td colspan="2" width="50%">
                                                HSN Code
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtHSNCODE" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnHSNCODE" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblHSNCODE" runat="server" Text='<%#Eval("HSN_CODE") %>' ToolTip='<%#Eval("HSN_CODE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            
                            <asp:TemplateField HeaderText="	Colors">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                                Colors
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtItemMake" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnItemMake" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblItemMake" runat="server" Text='<%#Eval("ITEM_MAKE") %>' ToolTip='<%#Eval("ITEM_MAKE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rack">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                                Rack Code
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtRack" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnRack" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblRack" runat="server" Text='<%#Eval("RACK_CODE") %>' ToolTip='<%#Eval("RACK_CODE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                          
                          
                            
               
                            <asp:TemplateField HeaderText="MinSt.LeveL">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                                MinSt.LeveL
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtMinStLeveL" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnMinStLeveL" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblMinStLeveL" runat="server" Text='<%#Eval("MIN_STOCK_LVL") %>' ToolTip='<%#Eval("MIN_STOCK_LVL") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ReOrderl">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                                ReOrder Lvl
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtReOrderl" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnReOrderl" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblReOrderl" runat="server" Text='<%#Eval("REODR_LVL") %>' ToolTip='<%#Eval("REODR_LVL") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MaxSt.LeveL">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                                MaxSt.LeveL
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtMaxStLeveL" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnMaxStLeveL" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblMaxStLeveL" runat="server" Text='<%#Eval("MAX_STK_LVL") %>' ToolTip='<%#Eval("MAX_STK_LVL") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CONSUME">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                                Consumable
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtCONSUME" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnCONSUME" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCONSUME" runat="server" Text='<%#Eval("CONSUME") %>' ToolTip='<%#Eval("CONSUME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reorder Qty">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                                Reorder Qty
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtReorderQty" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnReorderQty" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblReorderQty" runat="server" Text='<%#Eval("REODR_QTY") %>' ToolTip='<%#Eval("REODR_QTY") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Expire Days">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                                Expire Days
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtExpireDays" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnExpireDays" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblExpireDays" runat="server" Text='<%#Eval("EXPIRY_DAYS") %>' ToolTip='<%#Eval("EXPIRY_DAYS") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Opening Bal. St.">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                                OpeningBal. St.
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtOpeningBal" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnOpeningBal" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblOpeningBal" runat="server" Text='<%#Eval("OP_BAL_STOCK") %>' ToolTip='<%#Eval("OP_BAL_STOCK") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Opening Rate">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                                Opening Rate
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtOpeningRate" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnOpeningRate" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblOpeningRate" runat="server" Text='<%#Eval("OP_RATE") %>' ToolTip='<%#Eval("OP_RATE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" MinProcure">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                                MinProcure Days
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtMinProcure" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnMinProcure" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblMinStLeveL" runat="server" Text='<%#Eval("MIN_PROCURE_DAYS") %>'
                                        ToolTip='<%#Eval("MIN_PROCURE_DAYS") %>'></asp:Label>
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
                            <asp:TemplateField HeaderText="QC">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                                QC
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtQC" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnQC" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblQC" runat="server" Text='<%#Eval("QC_REQUIRED") %>' ToolTip='<%#Eval("QC_REQUIRED") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="REMARKS">
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
                                    <asp:Label ID="lblREMARKS" runat="server" Text='<%#Eval("ITEM_REMARKS") %>' ToolTip='<%#Eval("ITEM_REMARKS") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="STATUS">
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
                            
                                        <asp:TemplateField HeaderText="ITEMSIZE">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                                Item Size
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtITEMSIZE" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnITEMSIZE" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Label ID="lblITEMSIZE" runat="server" Text='<%#Eval("ITEM_SIZE") %>' ToolTip='<%#Eval("ITEM_SIZE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="WEIGHT">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                                Item Weight
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtWEIGHT" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnWEIGHT" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                  <asp:Label ID="lblWEIGHT" runat="server" Text='<%#Eval("WEIGHT") %>' ToolTip='<%#Eval("WEIGHT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            
                              
                            <asp:TemplateField HeaderText="ISEXCISABLE" Visible="false">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                                IS Excisable
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtISEXCISABLE" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnISEXCISABLE" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Label ID="lblISEXCISABLE" runat="server" Text='<%#Eval("IS_EXCISABLE") %>' ToolTip='<%#Eval("IS_EXCISABLE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                             <%--<asp:TemplateField HeaderText="TARIFFHEADING">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                                Tariff Heading 
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtTARIFFHEADING" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnTARIFFHEADING" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                 <asp:Label ID="lblTARIFFHEADING" runat="server" Text='<%#Eval("TARIFF_HEADING") %>' ToolTip='<%#Eval("TARIFF_HEADING") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            
                              <%--<asp:TemplateField HeaderText="SALESITCHSCODE">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                               Sales ITCHS Code)
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtSALESITCHSCODE" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnSALESITCHSCODE" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                <asp:Label ID="lblSALESITCHSCODE" runat="server" Text='<%#Eval("SALES_ITCHS_CODE") %>' ToolTip='<%#Eval("SALES_ITCHS_CODE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                         <%--<asp:TemplateField HeaderText="CUSTOMITCHSCODE">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                               Custom ITCHS Code
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtCUSTOMITCHSCODE" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnCUSTOMITCHSCODE" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                <asp:Label ID="lblCUSTOMITCHSCODE" runat="server" Text='<%#Eval("CUSTOM_ITCHS_CODE") %>' ToolTip='<%#Eval("CUSTOM_ITCHS_CODE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            
                                      <asp:TemplateField HeaderText="ISMOVABLE" Visible="false">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                              IS Movable
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtISMOVABLE" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnISMOVABLE" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                <asp:Label ID="lblISMOVABLE" runat="server" Text='<%#Eval("IS_MOVABLE") %>' ToolTip='<%#Eval("IS_MOVABLE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
   
   
                        </Columns>
                        
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                            VerticalAlign="Middle" />
                    </asp:GridView>
                    </div>
                </td>
            </tr>
        
</table>
</ContentTemplate>
<Triggers><asp:PostBackTrigger ControlID="ImageButton1" /></Triggers>
</asp:UpdatePanel>