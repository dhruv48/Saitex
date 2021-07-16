<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewCROrderAdj.aspx.cs" Inherits="Module_OrderDevelopment_Queries_ViewCROrderAdj" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>View CR</title>
    <link rel="stylesheet" type="text/css" href="../../../StyleSheet/CommonStyle.css" />

    <script language="javascript" type="text/javascript">

        function BindYRNSPIN_BOM(COST, TextBoxCOST) {
            window.opener.document.forms[0].submit();
            window.close();
        } 
          
    </script>

</head>
<body>
    <form id="form1" runat="server" width="100%">
    <div>
        <asp:ScriptManager ID="Scriptmanager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="100%" class="tContentArial">
                    <tr>
                        <td align="center" class="td TableHeader" valign="top" width="100%">
                            <strong class="titleheading">View CR Against Order PA</strong>
                        </td>
                    </tr>
                    <tr>
                        <td class="td tdLeft" width="100%">
                            <asp:Panel ID="Panel2" runat="server" Width="750px" Height="200px" ScrollBars="Horizontal">
                                <asp:GridView ID="grdViewCR" runat="server" AutoGenerateColumns="False" CssClass="SmallFont"
                                    Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="CR Company">
                                            <HeaderTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td colspan="2" width="100%">
                                                            Comp Code
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="80%">
                                                            <asp:TextBox ID="txtCompCode" Width="60" CssClass="SmallFont" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:ImageButton ID="btnCompCode" CssClass="SmallFont" runat="server" Text="Go" ImageUrl="~/CommonImages/Icons/Search.png"
                                                                OnClick="btnCompCode_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="txtCR_COMP_CODE" CssClass="Label SmallFont" runat="server" Text='<%# Bind("CR_COMP_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CR Branch">
                                            <HeaderTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td colspan="2" width="100%">
                                                            Branch Name
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="80%">
                                                            <asp:TextBox ID="txtBranchCode" Width="70px" CssClass="SmallFont" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:ImageButton ID="btnBranchCode" CssClass="SmallFont" runat="server" Text="Go"
                                                                ImageUrl="~/CommonImages/Icons/Search.png" OnClick="btnCompCode_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="txtCR_BRANCH_CODE" CssClass="Label SmallFont" runat="server" Text='<%# Bind("CR_BRANCH_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CR Year" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td colspan="2" width="100%">
                                                            CR Year
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="80%">
                                                            <asp:TextBox ID="txtCrYear" Width="70px" CssClass="SmallFont" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:ImageButton ID="btnCRYear" CssClass="SmallFont" OnClick="btnCompCode_Click"
                                                                runat="server" Text="Go" ImageUrl="~/CommonImages/Icons/Search.png" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                            <ItemTemplate>
                                                <asp:Label ID="txtCR_YEAR" runat="server" CssClass="Label SmallFont" Text='<%# Bind("CR_YEAR") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CR Order Type">
                                            <HeaderTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td colspan="2" width="100%">
                                                            CR Order Type
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="80%">
                                                            <asp:TextBox ID="txtCROrderType" Width="70px" CssClass="SmallFont" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:ImageButton ID="btnCROrderType" CssClass="SmallFont" OnClick="btnCompCode_Click"
                                                                runat="server" Text="Go" ImageUrl="~/CommonImages/Icons/Search.png" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemTemplate>
                                                <asp:Label ID="txtCR_ORDER_TYPE" runat="server" CssClass="Label SmallFont" Text='<%# Bind("CR_ORDER_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CR Order Cat">
                                            <HeaderTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td colspan="2" width="100%">
                                                            CR Order Cat
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="80%">
                                                            <asp:TextBox ID="txtCROrderCat" Width="70px" CssClass="SmallFont" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:ImageButton ID="btnCROrderCat" CssClass="SmallFont" OnClick="btnCompCode_Click"
                                                                runat="server" Text="Go" ImageUrl="~/CommonImages/Icons/Search.png" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCR_ORDER_CAT" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("CR_ORDER_CAT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CR Product Type" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td colspan="2" width="100%">
                                                            CR Product Type
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="80%">
                                                            <asp:TextBox ID="txtCRProductType" Width="70px" CssClass="SmallFont" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:ImageButton ID="btnCRProductType" CssClass="SmallFont" OnClick="btnCompCode_Click"
                                                                runat="server" Text="Go" ImageUrl="~/CommonImages/Icons/Search.png" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCR_PRODUCT_TYPE" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("CR_PRODUCT_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CR Business Type" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td colspan="2" width="100%">
                                                            CR Business Type
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="80%">
                                                            <asp:TextBox ID="txtCRBusinessType" Width="70px" CssClass="SmallFont" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:ImageButton ID="btnCRBusinessType" CssClass="SmallFont" OnClick="btnCompCode_Click"
                                                                runat="server" Text="Go" ImageUrl="~/CommonImages/Icons/Search.png" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCR_BUSINESS_TYPE" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("CR_BUSINESS_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="CR Party" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td colspan="2" width="100%">
                                                            CR Party
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="80%">
                                                            <asp:TextBox ID="txtCRParty" Width="70px" CssClass="SmallFont" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:ImageButton ID="btnCRParty" CssClass="SmallFont" OnClick="btnCompCode_Click"
                                                                runat="server" Text="Go" ImageUrl="~/CommonImages/Icons/Search.png" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCR_ST_ORDER_NO" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("CR_PRTY_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Cr No" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td colspan="2" width="100%">
                                                            Cr No
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="80%">
                                                            <asp:TextBox ID="txtCrNo" Width="70px" CssClass="SmallFont" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:ImageButton ID="btnCrNo" CssClass="SmallFont" OnClick="btnCompCode_Click" runat="server"
                                                                Text="Go" ImageUrl="~/CommonImages/Icons/Search.png" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCR_ST_ORDER_NO" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("CR_ST_ORDER_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CR Article" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td colspan="2" width="100%">
                                                            CR Article
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="80%">
                                                            <asp:TextBox ID="txtCRArticle" Width="70px" CssClass="SmallFont" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:ImageButton ID="btnCRArticleo" CssClass="SmallFont" OnClick="btnCompCode_Click"
                                                                runat="server" Text="Go" ImageUrl="~/CommonImages/Icons/Search.png" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCR_ST_ARTICLE_NO" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("CR_ST_ARTICLE_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CR Substrate" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td colspan="2" width="100%">
                                                            CR Substrate
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="80%">
                                                            <asp:TextBox ID="txtCRSubstrate" Width="70px" CssClass="SmallFont" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:ImageButton ID="btnCRSubstrate" CssClass="SmallFont" OnClick="btnCompCode_Click"
                                                                runat="server" Text="Go" ImageUrl="~/CommonImages/Icons/Search.png" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCR_ST_SUBSTRATE" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("CR_ST_SUBSTRATE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CR Count" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td colspan="2" width="100%">
                                                            CR Count
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="80%">
                                                            <asp:TextBox ID="txtCRCount" Width="70px" CssClass="SmallFont" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:ImageButton ID="btnCRCount" CssClass="SmallFont" OnClick="btnCompCode_Click"
                                                                runat="server" Text="Go" ImageUrl="~/CommonImages/Icons/Search.png" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCR_ST_COUNT" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("CR_ST_COUNT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CR Shade Family Code">
                                            <HeaderTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td colspan="2" width="100%">
                                                            CR Shade Family Code
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="80%">
                                                            <asp:TextBox ID="txtCRShadeFamilyCode" Width="70px" CssClass="SmallFont" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:ImageButton ID="btnCRShadeFamilyCode" OnClick="btnCompCode_Click" CssClass="SmallFont"
                                                                runat="server" Text="Go" ImageUrl="~/CommonImages/Icons/Search.png" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCR_ST_SHADE_FAMILY_CODE" runat="server" CssClass="LabelNo SmallFont"
                                                    Text='<%# Bind("CR_ST_SHADE_FAMILY_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CR Shade Code" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td colspan="2" width="100%">
                                                            CR Shade Code
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="80%">
                                                            <asp:TextBox ID="txtCRShadeCode" Width="70px" CssClass="SmallFont" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:ImageButton ID="btnCRShadeCode" CssClass="SmallFont" OnClick="btnCompCode_Click"
                                                                runat="server" Text="Go" ImageUrl="~/CommonImages/Icons/Search.png" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCR_ST_SHADE_CODE" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("CR_ST_SHADE_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CR Adj Qty" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td colspan="2" width="100%">
                                                            CR Adj Qty
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="80%">
                                                            <asp:TextBox ID="txtCRAdjQty" Width="70px" CssClass="SmallFont" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:ImageButton ID="btnCRAdjQty" CssClass="SmallFont" OnClick="btnCompCode_Click"
                                                                runat="server" Text="Go" ImageUrl="~/CommonImages/Icons/Search.png" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblADJ_QTY" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("ADJ_QTY") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                    </Columns>
                                    <HeaderStyle BackColor="#336699" CssClass="SmallFont titleheadingGrid" />
                                    <RowStyle CssClass="SmallFont" />
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="td" valign="top" width="100%">
                            <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click" Text="Close" ValidationGroup="M1"
                                CssClass="SmallFont" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
