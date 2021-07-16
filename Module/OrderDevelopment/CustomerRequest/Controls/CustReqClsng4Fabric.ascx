<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustReqClsng4Fabric.ascx.cs" Inherits="Module_OrderDevelopment_CustomerRequest_Controls_CustReqClsng4Fabric" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<script src="../../../../javascript/jquery-1.4.1.min.js" type="text/javascript"></script>

<script src="../../../../javascript/ScrollableGrid.js" type="text/javascript"></script>

<script type="text/javascript" language="javascript">
    $(document).ready(function() {
        $('#<%=gvCustomerApproval.ClientID %>').Scrollable();
    }
)
</script>--%>
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
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="left" class="tContentArial" width="100%">
            <tr>
                <td align="left" valign="top" class="td" width="100%">
                    <table align="left">
                        <tr>
                            <td id="tdUpdate" runat="server" align="left">
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                    Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click"></asp:ImageButton>
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
                    <b class="titleheading">Customer Request Closing For <asp:Label ID=lblHeader runat="server" Text=""></asp:Label></b>
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
                    <asp:Panel ID="pnlTree" runat="server" ScrollBars="Both" Height="400px" Width="100%">
                        <asp:GridView ID="gvCustomerApproval" runat="server" AutoGenerateColumns="False"
                            CssClass="SmallFont" Width="100%" OnRowDataBound="gvCustomerApproval_RowDataBound"
                            AllowPaging="True" PagerStyle-CssClass="pager" OnPageIndexChanging="gvCustomerApproval_PageIndexChanging"
                            PageSize="12">
                            <RowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Branch Name" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblREMUNIT" CssClass=" SmallFont" runat="server" Text='<%# Eval("REMUNIT") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Branch Name" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="INVOICE_NO_OF_UNIT" CssClass=" SmallFont" runat="server" Text='<%# Eval("INVOICE_NO_OF_UNIT") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Branch Name" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblConfFlag" CssClass=" SmallFont" runat="server" Text='<%# Eval("CONF_FLAG") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Branch Name">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Branch Name
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txthBranchName" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnhBranchName" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblBRANCH_NAME" CssClass=" SmallFont" runat="server" Text='<%# Eval("BRANCH_NAME") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Branch Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBRANCH_CODE" CssClass=" SmallFont" runat="server" Text='<%# Eval("BRANCH_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label Smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="CR Date">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td width="80%">
                                                    CR Date
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnhCRDate" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    <asp:TextBox ID="txthCRDate" Width="65px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblORDER_DATE" runat="server" CssClass="label smallfont" Text='<%# Bind("ORDER_DATE", "{0:dd-MM-yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Cust No">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    Cust No
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="btnhCustNo" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txthCustNo" Width="65px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCUSTNO" runat="server" Text='<%# Bind("CUSTNO") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Party Name...">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Party Code
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txthPartyName" Width="65px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnhPartyName" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblPRTY_Name" Font-Size="Smaller" runat="server" Text='<%# Bind("PRTY_Name") %>'></asp:LinkButton>
                                        <asp:Panel ID="pnlParty" runat="server" BackColor="Yellow">
                                            <asp:Label ID="lblParty_Code" Font-Size="Smaller" runat="server" Text='<%# Bind("PRTY_CODE") %>'></asp:Label>
                                        </asp:Panel>
                                        <cc1:HoverMenuExtender ID="hmeParty" runat="server" PopupPosition="Top" TargetControlID="lblPRTY_Name"
                                            PopupControlID="pnlParty" PopDelay="50">
                                        </cc1:HoverMenuExtender>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Req NoOfUnit" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCompCode" runat="server" Text='<%# Eval("COMP_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Req NoOfUnit" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblYEAR" runat="server" Text='<%# Eval("YEAR") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Req NoOfUnit" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblORDER_TYPE" runat="server" Text='<%# Eval("ORDER_TYPE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Req NoOfUnit" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblORDER_CAT" runat="server" Text='<%# Eval("ORDER_CAT") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Req NoOfUnit" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPRODUCT_TYPE" runat="server" Text='<%# Eval("PRODUCT_TYPE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Req NoOfUnit"
                                    Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBUSINESS_TYPE" runat="server" Text='<%# Eval("BUSINESS_TYPE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Req NoOfUnit"
                                    Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblORDER_NO" runat="server" Text='<%# Eval("custno") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Party Yarn Detail">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Party Yarn Detail
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txthPartyGrayYarnDtl" Width="90px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnhPartyGrayYarnDtl" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPARTY_ARTICLE_NO" runat="server" Text='<%# Eval("PARTY_ARTICLE_DESC") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Article Detail">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    Yarn Details
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="btnhGrayYarnDtl" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    <asp:TextBox ID="txthGrayYarnDtl" Width="80px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblYARN_DESC" Font-Size="Smaller" runat="server" Text='<%# Eval("YARN_DESC") %>' ></asp:LinkButton>
                                        <asp:Panel ID="pnlarticledetail" runat="server" BackColor="Yellow">
                                            <asp:Label ID="lblARTICLE_NO" runat="server" Text='<%# Eval("ARTICLE_NO") %>'></asp:Label>
                                        </asp:Panel>
                                        <cc1:HoverMenuExtender ID="hmeArticle" runat="server" PopupPosition="Top" TargetControlID="lblYARN_DESC"
                                            PopupControlID="pnlarticledetail" PopDelay="50">
                                        </cc1:HoverMenuExtender>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Req NoOfUnit" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSUBSTRATE" runat="server" Text='<%# Eval("SUBSTRATE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Req NoOfUnit" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCOUNT" runat="server" Text='<%# Eval("COUNT") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false" HeaderStyle-HorizontalAlign="Left" HeaderText="Shade Family Code">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Shade Family
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txthShadeFamily" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnhShadeFamily" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSHADE_FAMILY_CODE" runat="server" Text='<%# Eval("SHADE_FAMILY_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Shade Code">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Shade Code
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txthShadeCode" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnhShadeCode" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSHADE_CODE" runat="server" Text='<%# Eval("SHADE_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false" HeaderStyle-HorizontalAlign="Right" HeaderText="Trans Price">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Tr. Price
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txthTransPrice" Width="35px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnhTransPrice" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTRANS_PRICE" runat="server" Text='<%# Eval("TRANS_PRICE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false" HeaderStyle-HorizontalAlign="Right" HeaderText="Sale Price">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Sale Price
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txthSalePrice" Width="35px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnhSalePrice" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSALE_PRICE" runat="server" Text='<%# Eval("SALE_PRICE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
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
                                                    <asp:ImageButton ID="btnhUOM" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    <asp:TextBox ID="txthUOM" Width="45px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("CR_UNIT") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="CR Unit">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td align="right">
                                                    CR Unit
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="btnhCRQty" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txthCRQty" Width="40px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblRequestedNoOfUnit" runat="server" Text='<%# Eval("NO_OF_UNIT") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Adjusted Qty">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td align="right">
                                                    Adjusted Qty
                                                </td>
                                                <%--<td>
                                                    <asp:ImageButton ID="btnhAdjustedQty" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>--%>
                                            </tr>
                                            <%--<tr>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txthAdjustedQty" Width="40px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                            </tr>--%>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblADJUST_QTY" runat="server" Text='<%# Eval("ADJUST_QTY") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Invoice Qty" Visible="false">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td align="right">
                                                    Invoice Qty
                                                </td>
                                                <%--<td>
                                                    <asp:ImageButton ID="btnhInvoiceQty" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>--%>
                                            </tr>
                                            <%-- <tr>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txthInvoiceQty" Width="40px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                            </tr>--%>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblINVOICE_QTY" runat="server" Text='<%# Eval("INVOICE_QTY") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approve CR" Visible="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkApproved" runat="server" AutoPostBack="True" OnCheckedChanged="chkApproved_CheckedChanged" />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="label Smallfont" HorizontalAlign="Center" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Invoiced Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblINVOICE_NO_OF_UNIT" runat="server" Text='<%# Eval("INVOICE_NO_OF_UNIT") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Bal Invoice Unit">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lblNoOfUnit" runat="server" AutoPostBack="True" Width="60px" CssClass="SmallFont TextBoxNo"
                                            OnTextChanged="lblNoOfUnit_TextChanged" Text='<%# Eval("BAL_INVOICE_NO_OF_UNIT") %>'
                                            ValidationGroup="gg"></asp:TextBox>
                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="lblNoOfUnit"
                                            Display="Dynamic" ErrorMessage="Please Enter Numeric" MaximumValue="99999999999"
                                            MinimumValue="0" Type="Double" ValidationGroup="gg"></asp:RangeValidator>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Close CR">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChkClosed" runat="server" />
                                        <%--AutoPostBack="True" OnCheckedChanged="ChkClosed_CheckedChanged" />--%>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="label Smallfont" HorizontalAlign="Center" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Apr Unit" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAPP_NO_OF_UNIT" runat="server" Text='<%# Eval("APP_NO_OF_UNIT") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Req Qty" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRequestedQuantity" runat="server" Text='<%# Eval("QUANTITY") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Req WeighofUnit"
                                    Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReqWeighofUnit" runat="server" Text='<%# Eval("WEIGHT_OF_UNIT") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Apr.Weight" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWeighofUnit" runat="server" Text='<%# Eval("WEIGHT_OF_UNIT") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Apr.Weight" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("QTY_APPROVED") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Closed Date" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtConfirmDate" runat="server" Font-Size="Smaller" Width="60px"
                                            CssClass=" TextBox TextBoxDisplay SmallFont" ReadOnly="true" Text='<%# Bind("CONF_DATE") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Closed By" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtConfirmBy" runat="server" Font-Size="Smaller" Width="60px" CssClass="TextBox TextBoxDisplay SmallFont"
                                            ReadOnly="true" Text='<%# Bind("CONF_BY") %>' ToolTip='<%# Bind("CONF_BY") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle BackColor="#336799" ForeColor="White" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#336799" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
