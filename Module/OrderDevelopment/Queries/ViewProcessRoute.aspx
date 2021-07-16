<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewProcessRoute.aspx.cs" Inherits="Module_OrderDevelopment_Queries_ViewProcessRoute" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>View Process Route</title>
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
                            <strong class="titleheading">View Process Route</strong>
                        </td>
                    </tr>
                    <tr>
                        <td class="td tdLeft" width="100%">
                            <asp:Panel ID="Panel2" runat="server" Width="1225px" Height="200px" 
                                ScrollBars="Horizontal">
                                <asp:GridView ID="grdViewCR" runat="server" AutoGenerateColumns="False" CssClass="SmallFont"
                                    Width="100%">
                                    <Columns>
                                        
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
                                                            <asp:TextBox ID="txtBusinessType" Width="70px" CssClass="SmallFont" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:ImageButton ID="btnBusinessType" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/Search.png"
                                                                Text="Go" OnClick="BtnBranchName_Click" />
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
                                                                CssClass="SmallFont" OnClick="BtnBranchName_Click" Text="Go" />
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
                                                                CssClass="SmallFont" OnClick="BtnBranchName_Click" Text="Go" />
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
                                                                CssClass="SmallFont" OnClick="BtnBranchName_Click" Text="Go" />
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
                                                                CssClass="SmallFont" Text="Go" OnClick="BtnBranchName_Click" />
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
                                                                CssClass="SmallFont" OnClick="BtnBranchName_Click" Text="Go" />
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
                                                                CssClass="SmallFont" Text="Go"  OnClick="BtnBranchName_Click"/>
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
                                                            <asp:ImageButton ID="btnArticleCode" runat="server" CssClass="SmallFont" OnClick="BtnBranchName_Click"
                                                                Text="Go" ImageUrl="~/CommonImages/Icons/Search.png"  />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle CssClass=" label SmallFont" HorizontalAlign="Left" VerticalAlign="Top"
                                                Wrap="true" />
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
                                                            <asp:ImageButton ID="btnCRShadeCode" CssClass="SmallFont" 
                                                              OnClick="BtnBranchName_Click"  runat="server" Text="Go" ImageUrl="~/CommonImages/Icons/Search.png" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCR_ST_SHADE_CODE" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("SHADE_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                      <asp:BoundField DataField="PROS_ROUTE_CODE" HeaderText="PROS ROUTE CODE" />
                                      <asp:BoundField DataField="ORD_QTY" HeaderText="ORD QTY" />
                                      <asp:BoundField DataField="DEPT_NAME" HeaderText="DEPT NAME" />
                                      <asp:BoundField DataField="MAIN_PROCESS" HeaderText="MAIN PROCESS" />
                                      <asp:BoundField DataField="PROS_CODE" HeaderText="PROS CODE" />
                                      <asp:BoundField DataField="PROS_DESC"  HeaderText="PROS DESC" />
                                      <asp:BoundField DataField="MAC_GROUP_CODE" HeaderText="MAC GROUPCODE" />
                                      <asp:BoundField DataField="MACHINE_CODE" HeaderText="MACHINE_CODE" />  
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