<%@ Control Language="C#" AutoEventWireup="true" CodeFile="fiber_lotstock_agrbal.ascx.cs" Inherits="Module_Fiber_Controls_fiber_lotstock_agrbal" %>
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
                            &nbsp;
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
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader td" width="100%">
            <b class="titleheading">Lot Wise Poy Aggregate Query </b>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" width="100%">
            <asp:Button ID="printGrid" runat="server" Text="Print Grid" Width="85px" Height="22px"
                CssClass="AButton" />
            <b>Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server"></asp:Label></b>
            <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
            <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Height="400px" Width="100%">
                <div id="divPrint">
                    <asp:GridView ID="gvStock" runat="server" AutoGenerateColumns="false" CssClass="SmallFont"
                        Width="100%" OnPageIndexChanging="gvStock_PageIndexChanging1" OnPreRender="gvStock_PreRender1" OnRowDataBound ="row_boundgrd">
                        <RowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Branch Name" Visible="true">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                                Branch Name
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:DropDownList ID="ddlBranchName" runat="server"
                                                    AutoPostBack="true" AppendDataBoundItems="true"  Width="75px" Height="17px" Font-Size="X-Small" >
                                                    
                                                    
                                                    
                                                </asp:DropDownList>
                                                
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnhBranchName" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblBranchName" CssClass="SmallFont" runat="server" Text='<%# Eval("BRANCH_NAME") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
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
                            
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Fiber Desc" Visible="false">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                                Poy Desc
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
                                    <asp:Label ID="lblFiberDesc" runat="server" ToolTip='<%# Eval("FIBER_CODE") %>' Text='<%# Eval("FIBER_CODE") %>'></asp:Label>
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
                                                L/M No
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
                            
                         <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Bal Value">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td width="80%">
                                                Pt.&nbsp;Code
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnpalletcode" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" width="100%">
                                                <asp:TextBox ID="txtpalletcode" Width="45px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblpalletcode" runat="server" Text='<%# Eval("PALLET_CODE") %>'></asp:Label>
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
                                                Recived&nbsp;Tube
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
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Issue Bale" Visible="false">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                                Issued&nbsp;Tube
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
                                    <asp:Label ID="lblIssueBale" runat="server" Text='<%# Eval("ISS_NO_OF_BALE") %>'></asp:Label>
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
                            
                            
                            
                            
                            
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Bal Qty">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td width="80%">
                                                Bal&nbsp;Qty
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
                                                Bal&nbsp;Value
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
            &nbsp;
        </td>
    </tr>
</table>