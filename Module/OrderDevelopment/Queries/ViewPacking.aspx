<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewPacking.aspx.cs" Inherits="Module_OrderDevelopment_Queries_ViewPacking" %>

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
                            <strong class="titleheading">View Packing</strong>
                        </td>
                    </tr>
                    <tr>
                        <td class="td tdLeft" width="100%">
                            <asp:Panel ID="Panel2" runat="server" Width="750px" Height="200px" ScrollBars="Horizontal">
                                <asp:GridView ID="grdViewCR" runat="server" AutoGenerateColumns="False" CssClass="SmallFont"
                                    Width="100%">
                                    <Columns>
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
                                                            <asp:TextBox ID="txtBranchName" Width="70px" CssClass="SmallFont" 
                                                                runat="server"  ></asp:TextBox>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:ImageButton ID="BtnBranchName" CssClass="SmallFont" runat="server" Text="Go"
                                                                 ImageUrl="~/CommonImages/Icons/Search.png" 
                                                                onclick="BtnBranchName_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCOMP_CODE" Visible="false" runat="server" CssClass=" SmallFont"
                                                    Text='<%# Eval("COMP_CODE") %>'></asp:Label>
                                                <asp:Label ID="lblBRANCH_CODE" Visible="false" runat="server" CssClass=" SmallFont"
                                                    Text='<%# Eval("BRANCH_CODE") %>'></asp:Label>
                                                <asp:Label ID="lblSHADE_CODE" Visible="false" runat="server" CssClass=" SmallFont"
                                                    Text='<%# Eval("SHADE_CODE") %>'></asp:Label>
                                                <asp:Label ID="lblBranchName" runat="server" CssClass=" SmallFont" Text='<%# Eval("BRANCH_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle CssClass=" label SmallFont" HorizontalAlign="Left" VerticalAlign="Top"
                                                Wrap="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Business Type">
                                            <HeaderTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td colspan="2" width="100%">
                                                            Business Type
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="80%">
                                                            <asp:TextBox ID="txtBusinessType" Width="70px" CssClass="SmallFont" 
                                                                runat="server"  ></asp:TextBox>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:ImageButton ID="btnBusinessType" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/Search.png"
                                                                Text="Go" onclick="BtnBranchName_Click"   />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblBUSINESS_TYPE" runat="server" CssClass=" SmallFont" Text='<%# Eval("BUSINESS_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle CssClass=" label SmallFont" HorizontalAlign="Left" VerticalAlign="Top"
                                                Wrap="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Product Type">
                                            <HeaderTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td colspan="2" width="100%">
                                                            Product Type
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="80%">
                                                            <asp:TextBox ID="txtProductType" runat="server" CssClass="SmallFont" Width="70px"></asp:TextBox>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:ImageButton ID="btnProductType" runat="server" ImageUrl="~/CommonImages/Icons/Search.png"
                                                                CssClass="SmallFont"  onclick="BtnBranchName_Click" Text="Go" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPRODUCT_TYPE" runat="server" CssClass=" SmallFont" Text='<%# Eval("PRODUCT_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle CssClass=" label SmallFont" HorizontalAlign="Left" VerticalAlign="Top"
                                                Wrap="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Order Cat">
                                            <HeaderTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td colspan="2" width="100%">
                                                            Order Cat
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="80%">
                                                            <asp:TextBox ID="txtOrderCat" runat="server" CssClass="SmallFont" Width="70px"></asp:TextBox>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:ImageButton ID="btnOrderCat" runat="server" ImageUrl="~/CommonImages/Icons/Search.png"
                                                                CssClass="SmallFont" onclick="BtnBranchName_Click" Text="Go" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblORDER_CAT" runat="server" CssClass=" SmallFont" Text='<%# Eval("ORDER_CAT") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle CssClass=" label SmallFont" HorizontalAlign="Left" VerticalAlign="Top"
                                                Wrap="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Order Type">
                                            <HeaderTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td colspan="2" width="100%">
                                                            Order Type
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="80%">
                                                            <asp:TextBox ID="txtOrderType" runat="server" CssClass="SmallFont" Width="70px"></asp:TextBox>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:ImageButton ID="btnOrderType" runat="server" ImageUrl="~/CommonImages/Icons/Search.png"
                                                                CssClass="SmallFont"  onclick="BtnBranchName_Click" Text="Go" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblORDER_TYPE" runat="server" CssClass=" SmallFont" Text='<%# Eval("ORDER_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle CssClass=" label SmallFont" HorizontalAlign="Left" VerticalAlign="Top"
                                                Wrap="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Order No">
                                            <HeaderTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td colspan="2" width="100%">
                                                            Order No
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="80%">
                                                            <asp:TextBox ID="txtOrderNo" runat="server" CssClass="SmallFont" Width="70px"></asp:TextBox>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:ImageButton ID="btnOrderNo" ImageUrl="~/CommonImages/Icons/Search.png" runat="server"
                                                                 CssClass="SmallFont" Text="Go" onclick="BtnBranchName_Click"/>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblORDER_NO" runat="server" CssClass=" SmallFont" Text='<%# Eval("ORDER_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle CssClass=" label SmallFont" HorizontalAlign="Left" VerticalAlign="Top"
                                                Wrap="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Pi Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPI_TYPE" runat="server" CssClass=" SmallFont" Text='<%# Eval("PI_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td colspan="2" width="100%">
                                                            Pi Type
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="80%">
                                                            <asp:TextBox ID="txtPiType" runat="server" CssClass="SmallFont" Width="70px"></asp:TextBox>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:ImageButton ID="btnPiType" ImageUrl="~/CommonImages/Icons/Search.png" runat="server"
                                                                CssClass="SmallFont" onclick="BtnBranchName_Click" Text="Go" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle CssClass=" label SmallFont" HorizontalAlign="Left" VerticalAlign="Top"
                                                Wrap="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Pi No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPI_NO" runat="server" CssClass=" SmallFont" Text='<%# Eval("PI_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td colspan="2" width="100%">
                                                            Pi No
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="80%">
                                                            <asp:TextBox ID="txtPiNo" runat="server" CssClass="SmallFont" Width="70px"></asp:TextBox>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:ImageButton ID="btnPiNo" ImageUrl="~/CommonImages/Icons/Search.png" runat="server"
                                                                CssClass="SmallFont" Text="Go" onclick="BtnBranchName_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle CssClass=" label SmallFont" HorizontalAlign="Left" VerticalAlign="Top"
                                                Wrap="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Article Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblARTICAL_CODE" runat="server" CssClass=" SmallFont" Text='<%# Eval("ARTICAL_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td colspan="2" width="100%">
                                                            Article Code
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="80%">
                                                            <asp:TextBox ID="txtArticleCode" runat="server" CssClass="SmallFont" Width="70px"></asp:TextBox>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:ImageButton ID="btnArticleCode" runat="server" CssClass="SmallFont" 
                                                                Text="Go" onclick="BtnBranchName_Click" ImageUrl="~/CommonImages/Icons/Search.png" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle CssClass=" label SmallFont" HorizontalAlign="Left" VerticalAlign="Top"
                                                Wrap="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Artical desc">
                                            <ItemTemplate>
                                                <asp:Label ID="lblARTICAL_DESC" runat="server" CssClass=" SmallFont" Text='<%# Eval("ARTICAL_DESC") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td colspan="2" width="100%">
                                                            Artical desc
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="80%">
                                                            <asp:TextBox ID="txtArticaldesc" runat="server" CssClass="SmallFont" Width="70px"></asp:TextBox>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:ImageButton ID="btnArticaldesc" onclick="BtnBranchName_Click" ImageUrl="~/CommonImages/Icons/Search.png" runat="server"
                                                                CssClass="SmallFont" Text="Go" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle CssClass=" label SmallFont" HorizontalAlign="Left" VerticalAlign="Top"
                                                Wrap="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Shade Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblShadeCode" runat="server" CssClass=" SmallFont" Text='<%# Eval("SHADE_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td colspan="2" width="100%">
                                                            ShadeCode
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="80%">
                                                            <asp:TextBox ID="txtShadeCode" runat="server" CssClass="SmallFont" Width="70px"></asp:TextBox>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:ImageButton ID="btnShadeCode" onclick="BtnBranchName_Click" ImageUrl="~/CommonImages/Icons/Search.png" runat="server"
                                                                CssClass="SmallFont" Text="Go" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle CssClass=" label SmallFont" HorizontalAlign="Left" VerticalAlign="Top"
                                                Wrap="true" />
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
