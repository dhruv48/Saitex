<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bom_adj.aspx.cs" Inherits="Module_OrderDevelopment_Pages_bom_adj" %>

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
                            <span class="titleheading">BOM Adjustment</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdCenter td SmallFont">
                            Adjust BOM For Artical Code :
                            <asp:Label ID="lblAdjustItemBOMCode" CssClass="SmallFont Label" runat="server"></asp:Label>
                        </td>
                        <asp:Label ID="lbladjBOMItemCode" runat="server"></asp:Label></tr>
                    <tr>
                        <td class="tdCenter td SmallFont">
                            <asp:Label ID="lblBOMAdjustmentError" runat="server" CssClass="SmallFont Label" Font-Bold="False"
                                ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdCenter td SmallFont">
                            <asp:Panel ID="pnlBOMAdjustment" runat="server" ScrollBars="Vertical" Height="200px"
                                BackColor="#afcae4">
                                <asp:GridView ID="grdBOMAdjustment" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                    OnRowCommand="grdBOMAdjustment_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Business Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lbladjBOM_BUSINESS_TYPE" CssClass="SmallFont LabelNo" runat="server"
                                                    Text='<%# Bind("BOM_BUSINESS_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Product Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lbladjBOM_PRODUCT_TYPE" CssClass="SmallFont LabelNo" runat="server"
                                                    Text='<%# Bind("BOM_PRODUCT_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Category">
                                            <ItemTemplate>
                                                <asp:Label ID="lbladjBOM_ORDER_CAT" CssClass="SmallFont LabelNo" runat="server" Text='<%# Bind("BOM_ORDER_CAT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lbladjBOM_ORDER_TYPE" CssClass="SmallFont LabelNo" runat="server"
                                                    Text='<%# Bind("BOM_ORDER_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbladjBOM_ORDER_NO" CssClass="SmallFont LabelNo" runat="server" Text='<%# Bind("BOM_ORDER_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PI Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lbladjBOM_PI_TYPE" CssClass="SmallFont LabelNo" runat="server" Text='<%# Bind("BOM_PI_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PI No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbladjBOM_PI_NO" CssClass="SmallFont LabelNo" runat="server" Text='<%# Bind("BOM_PI_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Artical Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lbladjBOM_ARTICAL_CODE" CssClass="SmallFont LabelNo" runat="server"
                                                    Text='<%# Bind("BOM_ARTICAL_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Warp/ Weft">
                                            <ItemTemplate>
                                                <asp:Label ID="lbladjBOM_W_SIDE" CssClass="SmallFont LabelNo" runat="server" Text='<%# Bind("BOM_W_SIDE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Base Artical Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lbladjBOM_BASE_ARTICAL_TYPE" CssClass="SmallFont LabelNo" runat="server"
                                                    Text='<%# Bind("BOM_BASE_ARTICAL_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Base Artical Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lbladjBOM_BASE_ARTICAL_CODE" CssClass="SmallFont LabelNo" runat="server"
                                                    Text='<%# Bind("BOM_BASE_ARTICAL_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterText="Total" HeaderText="Required Qty.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdjustREQ_QTY" CssClass="SmallFont LabelNo" runat="server" Text='<%# Bind("REQ_QTY") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ADJ_QTY" HeaderText="Adjusted Qty." />
                                        <asp:TemplateField HeaderText="Remaining Qty.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdjustREM_QTY" CssClass="SmallFont LabelNo" runat="server" Text='<%# Bind("REM_QTY") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Adjusted Qty.">
                                            <FooterTemplate>
                                                <asp:Label ID="txtTotalAdjBOMQTY" CssClass="SmallFont LabelNo" runat="server" Width="50px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtAdjBOMQTY" runat="server" Text="0" Width="50px" AutoPostBack="True"
                                                    CssClass="SmallFont TextBoxNo" ontextchanged="txtAdjBOMQTY_TextChanged"></asp:TextBox>
                                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtAdjBOMQTY"
                                                    ErrorMessage="Only numeric value allowed" Display="Dynamic" SetFocusOnError="true"
                                                    MaximumValue="999999999" MinimumValue="0" Type="Double"></asp:RangeValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnAdjAll" runat="server" CommandName="AdjAll" Text="Adjust All" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdCenter td SmallFont">
                            <asp:Button ID="btnAdjBOMItem" runat="server" Text="Adjust Item BOM" 
                                onclick="btnAdjBOMItem_Click1" />
                            <asp:HiddenField ID="hfBUSINESS_TYPE" runat="server" />
                            <asp:HiddenField ID="hfPRODUCT_TYPE" runat="server" />
                            <asp:HiddenField ID="hfORDER_CAT" runat="server" />
                            <asp:HiddenField ID="hfORDER_TYPE" runat="server" />
                            <asp:HiddenField ID="hfORDER_NO" runat="server" />
                            <asp:HiddenField ID="hfPI_TYPE" runat="server" />
                            <asp:HiddenField ID="hfPI_NO" runat="server" />
                            <asp:HiddenField ID="hfARTICAL_CODE" runat="server" />
                            <asp:HiddenField ID="hfTextBoxId" runat="server" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
