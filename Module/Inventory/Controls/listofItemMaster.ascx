<%@ Control Language="C#" AutoEventWireup="true" CodeFile="listofItemMaster.ascx.cs"
    Inherits="Module_List_Search_Controls_listofItemMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript" language="javascript">
    function FillCrUnit(val) {
//        alert(val);
        var aapunit = document.getElementById(val).value;
        // alert(aapunit);
        if (aapunit=="")
       { 
        var mySplitResult = val.substr(0,64);
        //alert(mySplitResult);
        var merger = mySplitResult + 'lblRequestedNoOfUnit';
       // alert(merger);
        var a = document.getElementById(merger).innerHTML;
        //alert(a);
        document.getElementById(val).value = a;     
            }
</script>

<style type="text/css">
    .HideControl
    {
        visibility: hidden;
    }
    .pager span
    {
        color: #009900;
        font-weight: bold;
        font-size: 16pt;
    }
</style>
<table align="left" class="tContentArial" width="100%">
    <tr>
        <td align="left" valign="top" class="td" width="100%">
            <table align="left">
                <tr>
                    <td id="tdUpdate" runat="server" align="left">
                        <asp:ImageButton ID="imgbtnAddNew" runat="server" Width="48" Height="41" ToolTip="Add New"
                            ImageUrl="~/CommonImages/add.jpg" OnClick="imgbtnAddNew_Click"></asp:ImageButton>
                    </td>
                    <td id="tdDelete" runat="server" align="left" visible="false">
                        &nbsp;
                    </td>
                    <td id="tdFind" runat="server" visible="false" align="left">
                        &nbsp;
                    </td>
                    <td visible="false">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Width="48" Height="41" ToolTip="Print"
                            ImageUrl="~/CommonImages/export.png" OnClick="imgbtnPrint_Click"></asp:ImageButton>&nbsp;
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
                <asp:Label ID="lblHeader" runat="server" Text="List Of Item Master"></asp:Label>
            </b>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" width="100%">
            <b>Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server"></asp:Label></b>
            <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" width="100%">
            <asp:GridView ID="gvLogistic" runat="server" AutoGenerateColumns="False" CssClass="SmallFont"
                Width="100%" OnRowDataBound="gvLogistic_RowDataBound" AllowPaging="True" PagerStyle-CssClass="pager"
                PageSize="12" OnPageIndexChanging="gvLogistic_PageIndexChanging">
                <RowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="ItemCode">
                        <HeaderTemplate>
                            <table width="100%">
                                <tr>
                                    <td colspan="2" width="100%">
                                        ItemCode
                                    </td>
                                </tr>
                                <tr>
                                    <td width="80%">
                                        <asp:TextBox ID="txtItemCode" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                    </td>
                                    <td width="20%">
                                        <asp:ImageButton ID="btnGetLogistic" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                            Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblItemCode" CssClass=" SmallFont" runat="server" Text='<%# Eval("ITEM_CODE") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Item Description">
                        <HeaderTemplate>
                            <table width="100%">
                                <tr>
                                    <td>
                                        Item Description
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnPartyName" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                            Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtItemDesc" Width="65px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblItemDesc" Font-Size="Smaller" runat="server" Text='<%# Bind("ITEM_DESC") %>'></asp:Label>
                            </asp:Panel>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Item Desc" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblItemDesc" runat="server" Text='<%# Bind("ITEM_DESC") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Cat Code">
                        <HeaderTemplate>
                            <table width="100%">
                                <tr>
                                    <td>
                                        Cat Code
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnJobUnit" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                            Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtCatCode" Width="65px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCatCode" runat="server" Text='<%# Bind("CAT_CODE") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                    <asp:TemplateField Visible="true" HeaderStyle-HorizontalAlign="Left" HeaderText="Item Type">
                        <HeaderTemplate>
                            <table width="100%">
                                <tr>
                                    <td>
                                        Item Type
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnChallanNo" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                            Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtItemtype" Width="65px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblItemtype" runat="server" Text='<%# Bind("ITEM_TYPE") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Item Make">
                        <HeaderTemplate>
                            <table width="100%">
                                <tr>
                                    <td width="80%">
                                        Item Make
                                    </td>
                                    <td width="20%">
                                        <asp:ImageButton ID="btnGrey" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                            Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" width="100%">
                                        <asp:TextBox ID="txtItemMake" Width="98%" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblItemMake" runat="server" CssClass="label smallfont" Text='<%# Bind("ITEM_MAKE") %>'
                                ToolTip='<%# Bind("ITEM_MAKE") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="HSN Code">
                        <HeaderTemplate>
                            <table width="100%">
                                <tr>
                                    <td colspan="2" width="100%">
                                        HSN Code
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td width="80%">
                                        <asp:TextBox ID="txtCurrentStock" runat="server" Width="65px" Height="10px" Font-Size="X-Small"></asp:TextBox>
                                    </td>
                                    <td width="20%">
                                        <asp:ImageButton ID="btnHSN_CODE" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                            Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                    </td>
                                </tr>--%>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblhsn_code" runat="server" Text='<%# Bind("HSN_CODE") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Unit">
                        <HeaderTemplate>
                            <table width="100%">
                                <tr>
                                    <td colspan="2" width="100%">
                                        Unit
                                    </td>
                                </tr>
                                <tr>
                                    <td width="80%">
                                        <asp:TextBox ID="txtUnit" runat="server" Width="65px" Height="10px" Font-Size="X-Small"></asp:TextBox>
                                    </td>
                                    <td width="20%">
                                        <asp:ImageButton ID="btnCarryType" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                            Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblUnit" runat="server" Text='<%# Bind("UOM") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Rack_Code">
                        <HeaderTemplate>
                            <table width="100%">
                                <tr>
                                    <td colspan="2" width="100%">
                                        Rack Code
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td width="80%">
                                        <asp:TextBox ID="txtlastporate" runat="server" Width="65px" Height="10px" Font-Size="X-Small"></asp:TextBox>
                                    </td>
                                    <td width="20%">
                                        <asp:ImageButton ID="btnQuality" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                            Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                    </td>
                                </tr>--%>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblRACK_CODE" runat="server" Text='<%# Bind("RACK_CODE") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                   <%-- <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="LAst PO Rate">
                       <ItemTemplate>
                          <asp:Label ID="lbllastporate" runat="server" Text='<%# Bind("L_PORATE") %>' ></asp:Label>
                            </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Remarks">
                        <HeaderTemplate>
                            <table width="100%">
                                <tr>
                                    <td colspan="2" width="100%">
                                        Remarks
                                    </td>
                                </tr>
                                <tr>
                                    <td width="80%">
                                        <asp:TextBox ID="txtRemarks" runat="server" Width="65px" Height="10px" Font-Size="X-Small"></asp:TextBox>
                                    </td>
                                    <td width="20%">
                                        <asp:ImageButton ID="btnRemarks" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                            Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("ITEM_REMARKS") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Comp Code" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblDeptCode" runat="server" Text='<%# Eval("DEPT_CODE") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Branch Code" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblBranchCode" CssClass=" SmallFont" runat="server" Text='<%# Eval("BRANCH_CODE") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass="label Smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                </Columns>
                <PagerStyle BackColor="#336799" ForeColor="White" HorizontalAlign="Left" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#336799" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#2461BF" />
            </asp:GridView>
        </td>
    </tr>
</table>
