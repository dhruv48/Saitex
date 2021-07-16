<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdjBOM_INDENT.aspx.cs" Inherits="Module_Inventory_Pages_AdjBOM_INDENT" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Adjust BOM</title>
    <link href="../../../StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">

    function GetRowValue(val,TextBoxId)
    {           
        window.opener.document.getElementById(TextBoxId).value=val;   
        window.opener.document.forms[0].submit();      
        window.close();
    }

    </script>

</head>
<body bgcolor="#afcae4">
    <form id="form1" style="background-color: #afcae4" runat="server">
    <div>
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </cc1:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table class="tContentArial" style="background-color: #afcae4">
                    <tr>
                        <td class="TableHeader td tdCenter">
                            <span class="titleheading">Adjust BOM</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdCenter td SmallFont">
                            Adjust BOM For Item :
                            <asp:Label ID="lblAdjITEMBOMCode" CssClass="SmallFont Label" runat="server"></asp:Label>
                        </td>
                        <asp:Label ID="lblAdjBOMItemCode" runat="server"></asp:Label></tr>
                    <tr>
                        <td class="tdCenter td SmallFont">
                            <asp:Label ID="lblAdjBOMError" runat="server" CssClass="SmallFont Label" Font-Bold="False"
                                ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdCenter td SmallFont">
                            <asp:Panel ID="pnlAdjBOM" runat="server" ScrollBars="Vertical" Height="200px" BackColor="#afcae4">
                                <asp:GridView ID="grdAdjBOM" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                    OnRowCommand="grdAdjBOM_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Buss. Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdjBOM_BUSINESS_TYPE" CssClass="SmallFont LabelNo" runat="server"
                                                    Text='<%# Bind("BOM_BUSINESS_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Product Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdjBOM_PRODUCT_TYPE" CssClass="SmallFont LabelNo" runat="server"
                                                    Text='<%# Bind("BOM_PRODUCT_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Category">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdjBOM_ORDER_CAT" CssClass="SmallFont LabelNo" runat="server" Text='<%# Bind("BOM_ORDER_CAT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdjBOM_ORDER_TYPE" CssClass="SmallFont LabelNo" runat="server"
                                                    Text='<%# Bind("BOM_ORDER_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order #">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdjBOM_ORDER_NO" CssClass="SmallFont LabelNo" runat="server" Text='<%# Bind("BOM_ORDER_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PI Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdjBOM_PI_TYPE" CssClass="SmallFont LabelNo" runat="server" Text='<%# Bind("BOM_PI_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PI #">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdjBOM_PI_NO" CssClass="SmallFont LabelNo" runat="server" Text='<%# Bind("BOM_PI_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Artical Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdjBOM_ARTICAL_CODE" CssClass="SmallFont LabelNo" runat="server"
                                                    Text='<%# Bind("BOM_ARTICAL_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="W Side">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdjBOM_W_SIDE" CssClass="SmallFont LabelNo" runat="server" Text='<%# Bind("BOM_W_SIDE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdjBOM_BASE_ARTICAL_CODE" CssClass="SmallFont LabelNo" runat="server"
                                                    Text='<%# Bind("BOM_BASE_ARTICAL_CODE") %>'></asp:Label>
                                                <asp:Label ID="lblAdjBOM_BASE_ARTICAL_TYPE" CssClass="SmallFont LabelNo" runat="server"
                                                    Text='<%# Bind("BOM_BASE_ARTICAL_TYPE") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterText="Total" HeaderText="Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdjRemQty" CssClass="SmallFont LabelNo" runat="server" Text='<%# Bind("REMQTY") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="IND_ADJ_QTY" HeaderText="Adjusted QTY" />
                                        <asp:TemplateField HeaderText="Adjust QTY">
                                            <FooterTemplate>
                                                <asp:Label ID="txtTotalAdjBOMQTY" CssClass="SmallFont LabelNo" runat="server" Width="50px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtAdjBOMQTY" runat="server" Text="0" Width="50px" AutoPostBack="True"
                                                    CssClass="SmallFont TextBoxNo" OnTextChanged="txtAdjustedBOMQTY_TextChanged1"></asp:TextBox>
                                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtAdjBOMQTY"
                                                    ErrorMessage="Only numeric value allowed" Display="Dynamic" SetFocusOnError="true"
                                                    MaximumValue="999999999" MinimumValue="0" Type="Double"></asp:RangeValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnAdjAll" runat="server" CommandArgument='<%# Bind("BOM_ORDER_NO") %>'
                                                    CommandName="AdjAll" Text="Adjust All" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdCenter td SmallFont">
                            <asp:Button ID="btnAdjBOMItem" runat="server" Text="Adjust Item BOM" OnClick="btnAdjBOMItem_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
