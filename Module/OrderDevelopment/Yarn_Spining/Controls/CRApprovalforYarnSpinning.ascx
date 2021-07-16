<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CRApprovalforYarnSpinning.ascx.cs" Inherits="Module_OrderDevelopment_Controls_CRApprovalforYarnSpinning" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


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
 <td id="td1" runat="server" align="left">
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                    Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click1"></asp:ImageButton>
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
Sales Contract Approval for Yarn Spinning
</b>
</td>
</tr>
<tr>
<td align="left" class="td" width="100%">
                    <b>Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server"></asp:Label></b>
                    <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
<asp:GridView ID="gvCustomerApproval" runat="server" AutoGenerateColumns="false"  AllowPaging="true"
        CssClass="SmallFont" Width="100%" 
                        onpageindexchanging="gvCustomerApproval_PageIndexChanging1" 
                        onprerender="gvCustomerApproval_PreRender1" >
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
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Party Name......">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Party Name.......
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
                                        <asp:Label ID="lblPRTY_Name" runat="server" ToolTip='<%# Bind("PRTY_Name") %>' Text='<%# Bind("PRTY_CODE") %>'></asp:Label>
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
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Gray Yarn Detail...">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                     Yarn Detail...
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txthGrayYarnDtl" Width="90px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnhGrayYarnDtl" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblARTICLE_NO" runat="server" ToolTip='<%# Eval("YARN_DESC") %>' Text='<%# Eval("ARTICLE_NO") %>'></asp:Label>
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
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Shade Family">
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
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Trans Price" Visible="false">
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
                                        <asp:TextBox ID="lblTRANS_PRICE" runat="server" Text='<%# Eval("TRANS_PRICE") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Sale Price">
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
                                        <asp:TextBox ID="lblSALE_PRICE" runat="server" Text='<%# Eval("SALE_PRICE") %>' MaxLength="6"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilterlblSALE_PRICE" runat="server"  TargetControlID="lblSALE_PRICE"   FilterType="Custom, Numbers" ValidChars="."/>
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
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Cr Qty">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td align="right">
                                                    Qty.
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
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Apr.Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lblNoOfUnit" runat="server"  Width="60px" CssClass="SmallFont TextBoxNo"
                                            OnTextChanged="lblNoOfUnit_TextChanged" Text='<%# Eval("BAL_INVOICE_NO_OF_UNIT") %>'
                                            ValidationGroup="gg" MaxLength="10" ></asp:TextBox>
                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="lblNoOfUnit"
                                            Display="Dynamic" ErrorMessage="Please Enter Numeric" MaximumValue="99999999999"
                                            MinimumValue="0" Type="Double" ValidationGroup="gg"></asp:RangeValidator>
                                             <cc1:FilteredTextBoxExtender ID="FilterlblNoOfUnit" runat="server"  TargetControlID="lblNoOfUnit"   FilterType="Custom, Numbers" ValidChars="."/>
                                    
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Confirm CR">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkApproved" runat="server" AutoPostBack="True" OnCheckedChanged="chkApproved_CheckedChanged" />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="label Smallfont" HorizontalAlign="Center" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Apr Qty" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAPP_NO_OF_UNIT" runat="server" Text='<%# Eval("APP_NO_OF_UNIT") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Invoiced Qty" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblINVOICE_NO_OF_UNIT" runat="server" Text='<%# Eval("INVOICE_NO_OF_UNIT") %>'></asp:Label>
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
                               <asp:TemplateField HeaderText="Closed CR" Visible="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChkClosed" Enabled="false" runat="server" AutoPostBack="True" OnCheckedChanged="ChkClosed_CheckedChanged" />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="label Smallfont" HorizontalAlign="Center" VerticalAlign="Top"
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
                                <asp:TemplateField HeaderText="Confirm Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtConfirmDate" runat="server" Font-Size="Smaller" Width="60px"
                                            CssClass=" TextBox TextBoxDisplay SmallFont" ReadOnly="true" Text='<%# Bind("CONF_DATE") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Confirm By">
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
                </td>
</tr>
<tr>
<td align="left" class="td" width="100%">
    &nbsp;</td>
</tr>
</table>

