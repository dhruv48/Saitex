<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OCYarnCustAdjustment.aspx.cs"
    Inherits="Module_OrderDevelopment_Pages_OCYarnCustAdjustment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Yarn Dyeing Customer Request Adjustment</title>
    <link href="../../../StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">

        function GetRowValue(val, txtQTY) {
            window.opener.document.getElementById(txtQTY).value = val;
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
                            <span class="titleheading">Customer Request Adjustment</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdCenter td SmallFont">
                            <br />
                            <asp:Label ID="lblAdjustArticleCode" runat="server"></asp:Label>
                            <asp:Label ID="lblShadeCode" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdCenter td SmallFont">
                            <asp:Label ID="lblIndentAdjustmentError" runat="server" CssClass="SmallFont Label"
                                Font-Bold="False" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdCenter td SmallFont">
                            <asp:Panel ID="pnlIndentAdjustment" runat="server" ScrollBars="Vertical" Height="200px"
                                BackColor="#afcae4">
                                <asp:GridView ID="grdCustAdjustment" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                    OnRowCommand="grdCustAdjustment_RowCommand1">
                                    <Columns>
                                        <asp:TemplateField HeaderText="CUST NUMBER">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCustNo" CssClass="SmallFont LabelNo" runat="server" Text='<%# Bind("custno") %>'></asp:Label>
                                                <asp:Label ID="lblAdjustOrderNuber" CssClass="SmallFont LabelNo" runat="server" Text='<%# Bind("ORDER_NO") %>'></asp:Label>
                                                <asp:Label ID="lblCR_COMP_CODE" CssClass="SmallFont LabelNo" Visible="False" runat="server"
                                                    Text='<%# Bind("COMP_CODE") %>'></asp:Label>
                                                <asp:Label ID="lblCR_BRANCH_CODE" CssClass="SmallFont LabelNo" Visible="False" runat="server"
                                                    Text='<%# Bind("BRANCH_CODE") %>'></asp:Label>
                                                <asp:Label ID="lblCR_BUSINESS_TYPE" CssClass="SmallFont LabelNo" Visible="False"
                                                    runat="server" Text='<%# Bind("BUSINESS_TYPE") %>'></asp:Label>
                                                <asp:Label ID="lblCR_ST_ORDER_NO" CssClass="SmallFont LabelNo" Visible="False" runat="server"
                                                    Text='<%# Bind("ORDER_NO") %>'></asp:Label>
                                                <asp:Label ID="lblCR_ST_SUBSTRATE" CssClass="SmallFont LabelNo" Visible="False" runat="server"
                                                    Text='<%# Bind("SUBSTRATE") %>'></asp:Label>
                                                <asp:Label ID="lblCR_ST_COUNT" CssClass="SmallFont LabelNo" Visible="False" runat="server"
                                                    Text='<%# Bind("COUNT") %>'></asp:Label>
                                                <asp:Label ID="lblCR_ST_SHADE_FAMILY_CODE" CssClass="SmallFont LabelNo" Visible="False"
                                                    runat="server" Text='<%# Bind("SHADE_FAMILY_CODE") %>'></asp:Label>
                                                <asp:Label ID="lblCR_ST_SHADE_CODE" CssClass="SmallFont LabelNo" Visible="False"
                                                    runat="server" Text='<%# Bind("SHADE_CODE") %>'></asp:Label>
                                                <asp:Label ID="lblAPPR_QTY" CssClass="SmallFont LabelNo" runat="server" Text='<%# Bind("QUANTITY") %>'
                                                    Visible="False"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Branch Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBRANCH_CODE" CssClass="SmallFont LabelNo" Visible="True" runat="server"
                                                    Text='<%# Bind("BRANCH_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="YEAR">
                                            <ItemTemplate>
                                                <asp:Label ID="lblYEAR" CssClass="SmallFont Label" runat="server" ToolTip='<%# Bind("YEAR") %>'
                                                    Text='<%# Bind("YEAR") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ARTICLE CODE">
                                            <ItemTemplate>
                                                <asp:Label ID="lblArticleCode" CssClass="SmallFont Label" runat="server" ToolTip='<%# Bind("ARTICLE_NO") %>'
                                                    Text='<%# Bind("ARTICLE_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SHADE CODE">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSHADE" CssClass="SmallFont Label" runat="server" ToolTip='<%# Bind("SHADE_CODE") %>'
                                                    Text='<%# Bind("SHADE_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ORDER TYPE">
                                            <ItemTemplate>
                                                <asp:Label ID="lblORDER_TYPE" CssClass="SmallFont Label" runat="server" ToolTip='<%# Bind("ORDER_TYPE") %>'
                                                    Text='<%# Bind("ORDER_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="REQ DATE">
                                        <ItemTemplate>
                                        <asp:Label ID="ibiREQ_DATE" CssClass="SmallFont Label" runat="server"
                                        Text='<%# Bind("REQ_DATE") %>'></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ORDER CAT">
                                            <ItemTemplate>
                                                <asp:Label ID="lblORDER_CAT" CssClass="SmallFont Label" runat="server" ToolTip='<%# Bind("ORDER_CAT") %>'
                                                    Text='<%# Bind("ORDER_CAT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PRODUCT TYPE">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPRODUCT_TYPE" CssClass="SmallFont Label" runat="server" ToolTip='<%# Bind("PRODUCT_TYPE") %>'
                                                    Text='<%# Bind("PRODUCT_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterText="Total" HeaderText="QUANTITY">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdjustQuantity" CssClass="SmallFont LabelNo" runat="server" Text='<%# Bind("QUANTITY") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ADJUST_QTY" HeaderText="AJUSTED QTY" />
                                        <asp:TemplateField HeaderText="Remaining">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdjustRemQty" CssClass="SmallFont LabelNo" runat="server" Text='<%# Bind("REMQTY") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Adjust QTY">
                                            <FooterTemplate>
                                                <asp:Label ID="txtTotalAdjustedIndentQTY" CssClass="SmallFont LabelNo" runat="server"
                                                    Width="50px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtAdjustedIndentQTY" runat="server" Text="0" MaxLength="5" Width="50px"
                                                    AutoPostBack="True" CssClass="SmallFont TextBoxNo" OnTextChanged="txtAdjustedIndentQTY_TextChanged1"></asp:TextBox>
                                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtAdjustedIndentQTY"
                                                    ErrorMessage="Only numeric value allowed" Display="Dynamic" SetFocusOnError="true"
                                                    MaximumValue="999999999" MinimumValue="0" Type="Double"></asp:RangeValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnAdjAll" runat="server" CommandArgument='<%# Bind("ORDER_NO") %>'
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
                            <asp:Button ID="btnAdjustIndentItem" runat="server" Text="Adjust Cust Request " OnClick="btnAdjustIndentItem_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
