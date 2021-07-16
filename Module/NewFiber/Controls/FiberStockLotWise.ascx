<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FiberStockLotWise.ascx.cs" Inherits="Module_Fiber_Controls_FiberStockLotWise" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script type="text/javascript" language="javascript">    
    function CallPrint(strid) {
        var prtContent = document.getElementById(strid);
        if (prtContent != null) {
            var WinPrint = window.open('', '', 'center=1,width=800,height=600,toolbar=0,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            //WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();           
        }
    }    
</script>



<table align="left" class="tContentArial" width="100%">
<tr>
<td align="left" valign="top" class="td" width="100%">
<table align="left">
<tr>
<td id="tdUpdate" runat="server" align="left">
    &nbsp;<td id="tdFind" runat="server" visible="false" align="left">
        &nbsp;</td>
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

</tr>
</table>

</td>
</tr>
<tr>
<td align="center" class="TableHeader td" width="100%">
<b class ="titleheading">
Lot Wise Fiber Stock Query
</b>
</td>
</tr>
<tr>
<td align="left" class="td" width="100%">
<asp:Button ID=printGrid runat="server" Text="Print Grid"  Width="85px" Height="22px"  CssClass="AButton" />
                    <b>Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server"></asp:Label></b>
                    <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
                    

 <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Height="400px" Width="100%">
                        <div id="divPrint">


<asp:GridView ID="gvStock" runat="server" AutoGenerateColumns="false"  
        CssClass="SmallFont" Width="100%" 
                        onpageindexchanging="gvStock_PageIndexChanging1" 
                        onprerender="gvStock_PreRender1" >
<RowStyle BackColor="White" />
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
                                                    <asp:TextBox ID="txtBranchName" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnBranchName" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblBranchName" CssClass=" SmallFont" runat="server" Text='<%# Eval("BRANCH_NAME") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                              <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Trn Date" Visible="false">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td width="80%">
                                                    Trn Date
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnhTrnDate" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    <asp:TextBox ID="txtTrnDate" Width="65px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTrnDate" runat="server" CssClass="label smallfont" Text='<%# Bind("TDATE", "{0:dd-MM-yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                              <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Trn No" Visible="false">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    Trn No
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="btnTrnNo" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txtTrnNo" Width="65px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTrnNo" runat="server" Text='<%# Bind("TRN_NUMB") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                              <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Trn Desc" Visible="false">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                   Trn Desc
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtTrnDesc" Width="65px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnTrnDesc" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTrnDesc" runat="server" ToolTip='<%# Bind("TRN_TYPE") %>' Text='<%# Bind("TRN_DESC") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                               <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Fiber Cat" Visible="false">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                     Fiber Cat
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtFiberCat" Width="90px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnFiberCat" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblFiberCat" runat="server" ToolTip='<%# Eval("FIBER_CAT") %>' Text='<%# Eval("FIBER_CAT") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                              <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Fiber Code" Visible="false">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                     Fiber Code
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtFiberCode" Width="90px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnFiberCode" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblFiberCode" runat="server" ToolTip='<%# Eval("FIBER_DESC") %>' Text='<%# Eval("FIBER_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                              <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Fiber Desc">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                     Fiber Desc
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtFiberDesc" Width="90px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnFiberDesc" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblFiberDesc" runat="server" ToolTip='<%# Eval("FIBER_CODE") %>' Text='<%# Eval("FIBER_DESC") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Lot No">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                   Lot No   
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtLotNo" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnLotNo" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblLotNo" runat="server" Text='<%# Eval("LOT_NO") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Total Bale">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Recived Bale  
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtTotalBale" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnTotalBale" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalBale" runat="server" Text='<%# Eval("TOTAL_NO_OF_BALE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                              <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Issue Bale">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Issued Bale
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtIssueBale" Width="35px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnIssueBale" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblIssueBale" runat="server" Text='<%# Eval("ISS_NO_OF_BALE") %>' ></asp:Label>
                                       </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                 
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Bal Bale">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td width="80%">
                                                    Balance Bale
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnBalBale" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    <asp:TextBox ID="txtBalBale" Width="45px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblBalBale" runat="server" Text='<%# Eval("TOTAL_BAL_BALE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>                                
                               <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Weight of Unit">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Weight of Unit 
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtWeightofUnit" Width="35px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnWeightofUnit" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblWeightofUnit" runat="server" Text='<%# Eval("WEIGHT_OF_UNIT") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Quantity">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td width="80%">
                                                   Quantity
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnQuantity" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    <asp:TextBox ID="txtQuantity" Width="45px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("TRN_QTY") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="UOM">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td width="80%">
                                                   UOM
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnUom" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    <asp:TextBox ID="txtUom" Width="45px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblUom" runat="server" Text='<%# Eval("UOM_OF_UNIT") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Final Rate">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td width="80%">
                                                   Final Rate
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnFinalRate" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    <asp:TextBox ID="txtFinalRate" Width="45px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblFinalRate" runat="server" Text='<%# Eval("FINAL_RATE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Total Value">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td width="80%">
                                                   Total Value
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnTotalValue" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    <asp:TextBox ID="txtTotalValue" Width="45px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalValue" runat="server" Text='<%# Eval("TRN_VALUE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Issue Qty">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td width="80%">
                                                  Issue Qty
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnIssueQty" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    <asp:TextBox ID="txtIssueQty" Width="45px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblIssueQty" runat="server" Text='<%# Eval("ISS_QTY") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Issue Value">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td width="80%">
                                                  Issue Value
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnIssueValue" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    <asp:TextBox ID="txtIssueValue" Width="45px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblIssueValue" runat="server" Text='<%# Eval("ISS_VALUE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Bal Qty">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td width="80%">
                                                  Bal Qty
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnBalQty" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    <asp:TextBox ID="txtBalQty" Width="45px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblBalQty" runat="server" Text='<%# Eval("BAL_QTY") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Bal Value">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td width="80%">
                                                  Bal Value
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnBalValue" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    <asp:TextBox ID="txtBalValue" Width="45px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblBalValue" runat="server" Text='<%# Eval("BAL_VALUE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                               
                            </Columns>
 <PagerStyle BackColor="#336799" ForeColor="White" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#336799" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
</asp:GridView>
              
              </div>
                    </asp:Panel>
              
                </td>
</tr>
<tr>
<td align="left" class="td" width="100%">
    &nbsp;</td>
</tr>
</table>

