<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MaterialIndentQueryForm.ascx.cs"
    Inherits="Module_Inventory_Controls_MaterialIndentQueryForm" %>
<style type="text/css">
    .style2
    {
        height: 24px;
    }
</style>

<script type="text/javascript">
    var isAppliedFilter = false;
    var printGridOnCallback = false;
    var currentPageSize = 10;
    function applyFilter() {

        document.getElementById('apply').style.display = '';
        document.getElementById('remove').style.display = '';
        document.getElementById('hide').style.display = '';
        document.getElementById('show').style.display = 'none';

        grid1.filter()
        isAppliedFilter = true;
        return false;
    }
    function hideFilter() {

        if (isAppliedFilter == true) {
            document.getElementById('remove').style.display = '';
        } else {
            document.getElementById('remove').style.display = 'none';
        }
        document.getElementById('show').style.display = '';
        document.getElementById('hide').style.display = 'none';
        document.getElementById('apply').style.display = 'none';

        grid1.hideFilter()

        return false;
    }
    function showFilter() {

        if (isAppliedFilter == true) {
            document.getElementById('remove').style.display = '';
        } else {
            document.getElementById('remove').style.display = 'none';
        }
        document.getElementById('apply').style.display = '';
        document.getElementById('hide').style.display = '';
        document.getElementById('show').style.display = 'none';
        grid1.showFilter();

        return false;
    }

    function removeFilter() {

        document.getElementById('show').style.display = '';
        document.getElementById('apply').style.display = 'none';
        document.getElementById('hide').style.display = 'none';
        document.getElementById('remove').style.display = 'none';

        grid1.removeFilter();
        grid1.hideFilter();
        isAppliedFilter = false;
        return false;
    }

    function printGrid(printAll) {
        if (printAll) {
            printGridOnCallback = true;
            currentPageSize = grid1.getPageSize();
            ob_grid1PageSizeSelector.value(-1);
        } else {
            grid1.print();
        }

        return false;
    }

    function grid1_Callback() {
        if (printGridOnCallback) {
            grid1.print();
            printGridOnCallback = false;
            ob_grid1PageSizeSelector.value(currentPageSize);
        }
    }
</script>

<%--<asp:UpdatePanel ID="UpdatePanel2341" runat="server">
    <ContentTemplate>--%>
        <table class="tContentArial" width="100%">
            <tr>
                <td class="td" colspan="10">
                    <table>
                        <tr>
                            <td>
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                                    OnClick="imgbtnPrint_Click" ToolTip="Print" Width="48" />
                            </td>
                             <td>  
                              <asp:ImageButton ID="imgBtnExportExcel" runat="server" Width="48" Height="41" ToolTip="Print"
                            ImageUrl="~/CommonImages/export.png" onclick="imgBtnExportExcel_Click" ></asp:ImageButton>&nbsp;</td> 
                    <td>
                            <td>
                                <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                                    OnClick="imgbtnExit_Click"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                                    OnClick="imgbtnHelp_Click"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" valign="top" class="tRowColorAdmin td" colspan="10">
                    <span class="titleheading">Indent Query Form</span>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Branch:
                </td>
                <td>
                    <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="true" CssClass="gCtrTxt "
                        Font-Size="9" Width="160px">
                    </asp:DropDownList>
                </td>
                <td align="right">
                    Department:
                </td>
                <td>
                    <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" CssClass="gCtrTxt "
                        Font-Size="9" Width="160px">
                    </asp:DropDownList>
                </td>
                <td align="right">
                    Item&nbsp;Category:
                </td>
                <td>
                    <asp:DropDownList ID="ddlItemCate" runat="server" AutoPostBack="true" CssClass="gCtrTxt "
                        Font-Size="9" Width="160px">
                    </asp:DropDownList>
                </td>
                <td align="right">
                    Item&nbsp;Type:
                </td>
                <td>
                    <asp:DropDownList ID="ddlItemType" runat="server" AutoPostBack="true" CssClass="gCtrTxt "
                        Font-Size="9" Width="160px">
                    </asp:DropDownList>
                </td>
               
                
            </tr>
            
            <tr>
            <td align="right">
                    Location:
                </td>
                <td>
                    <asp:DropDownList ID="ddllocation" runat="server" AutoPostBack="true" CssClass="gCtrTxt "
                        Font-Size="9" Width="160px">
                    </asp:DropDownList>
                </td>
                <td align="right">
                    Store:
                </td>
                <td>
                    <asp:DropDownList ID="ddlstore" runat="server" AutoPostBack="true" CssClass="gCtrTxt "
                        Font-Size="9" Width="160px">
                    </asp:DropDownList>
                </td>
           
           <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td align="center"><asp:Button ID="btnGetData" runat="server" Text="Get Record" Width="85px" 
                        OnClick="btnGetData_Click" CssClass="AButton" /></td>
           
            </tr>
            <tr>
                <td width="50%" class="style2" colspan="4">
                    <b>Total Records :</b>
                    <asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
                </td>
                <td align="left" width="50%" colspan="4" class="style2">
                    <b>
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <h3>
                                    Loading...</h3>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </b>
                </td>
            </tr>
            <tr>
                <td class="td SmallFont" colspan="10" align="center"  >
                    <asp:Panel ID="pnlShowHover" runat="server" Width="100%" ScrollBars="Auto">
                        <asp:GridView ID="Grid1" runat="server" AutoGenerateColumns="False" HeaderStyle-Font-Bold="true"
                            AllowPaging="true" CellPadding="3" ForeColor="#333333" GridLines="Both" BorderStyle="Ridge"
                            Font-Names="Arial" Font-Size="X-Small" OnPageIndexChanging="Grid1_PageIndexChanging">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                            <Columns>
                                <asp:BoundField DataField="BRANCH_NAME" HeaderText="Branch"></asp:BoundField>
                                <asp:BoundField DataField="DEPT_NAME" HeaderText="Department"></asp:BoundField>
                                <asp:BoundField DataField="IND_NUMB" HeaderText="Indent No"></asp:BoundField>
                                <asp:BoundField DataField="IND_TYPE" HeaderText="Indent Type"></asp:BoundField>
                                <asp:BoundField DataField="IND_DATE" HeaderText="Date"></asp:BoundField>
                                <asp:BoundField DataField="USER_NAME" HeaderText="User"></asp:BoundField>
                                <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code"></asp:BoundField>
                                <asp:BoundField DataField="CAT_CODE" ItemStyle-Font-Size="9px" HeaderText="Item Cat">
                                </asp:BoundField>
                                <asp:BoundField DataField="ITEM_DESC" ItemStyle-Font-Size="9px" HeaderText="Item Desc">
                                </asp:BoundField>
                                <asp:BoundField DataField="ITEM_TYPE" HeaderText="Item Type"></asp:BoundField>
                                <asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
                                <asp:BoundField DataField="RQST_QTY" HeaderText="Req. Qty."></asp:BoundField>
                                <asp:BoundField DataField="APPR_QTY" HeaderText="App. Qty."></asp:BoundField>
                                <asp:BoundField DataField="CONF_FLAG" HeaderText="Confirmed"></asp:BoundField>
                                <asp:BoundField DataField="LOCATION" HeaderText="Location"></asp:BoundField>
                                <asp:BoundField DataField="STORE" HeaderText="Store"></asp:BoundField>
                            </Columns>
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
        </table>
 <%--   </ContentTemplate>
</asp:UpdatePanel>--%>