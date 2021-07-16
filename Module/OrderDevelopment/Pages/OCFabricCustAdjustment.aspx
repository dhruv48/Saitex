<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OCFabricCustAdjustment.aspx.cs" Inherits="Module_OrderDevelopment_Pages_OCFabricCustAdjustment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Fabric Customer Request Adjustment</title>
    <link href="../../../StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">

    function GetRowValue(val,txtQTY)
    {           
        window.opener.document.getElementById(txtQTY).value=val;   
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
                            <asp:GridView ID="grdCustAdjustment" runat="server" AutoGenerateColumns="False" 
                                OnRowCommand="grdCustAdjustment_RowCommand1" ShowFooter="True">
                                <Columns>
                                    <asp:TemplateField HeaderText="CUST NUMBER">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustNo" runat="server" CssClass="SmallFont LabelNo" 
                                                Text='<%# Bind("custno") %>'></asp:Label>
                                            <asp:Label ID="lblAdjustOrderNuber" runat="server" CssClass="SmallFont LabelNo" 
                                                Text='<%# Bind("ORDER_NO") %>'></asp:Label>
                                            <asp:Label ID="lblCR_COMP_CODE" runat="server" CssClass="SmallFont LabelNo" 
                                                Text='<%# Bind("COMP_CODE") %>' Visible="False"></asp:Label>
                                            <asp:Label ID="lblCR_BRANCH_CODE" runat="server" CssClass="SmallFont LabelNo" 
                                                Text='<%# Bind("BRANCH_CODE") %>' Visible="False"></asp:Label>
                                            <asp:Label ID="lblCR_BUSINESS_TYPE" runat="server" CssClass="SmallFont LabelNo" 
                                                Text='<%# Bind("BUSINESS_TYPE") %>' Visible="False"></asp:Label>
                                            <asp:Label ID="lblCR_ST_ORDER_NO" runat="server" CssClass="SmallFont LabelNo" 
                                                Text='<%# Bind("ORDER_NO") %>' Visible="False"></asp:Label>
                                            <asp:Label ID="lblCR_ST_SUBSTRATE" runat="server" CssClass="SmallFont LabelNo" 
                                                Text='<%# Bind("SUBSTRATE") %>' Visible="False"></asp:Label>
                                            <asp:Label ID="lblCR_ST_COUNT" runat="server" CssClass="SmallFont LabelNo" 
                                                Text='<%# Bind("COUNT") %>' Visible="False"></asp:Label>
                                            <asp:Label ID="lblCR_ST_SHADE_FAMILY_CODE" runat="server" 
                                                CssClass="SmallFont LabelNo" Text='<%# Bind("SHADE_FAMILY_CODE") %>' 
                                                Visible="False"></asp:Label>
                                            <asp:Label ID="lblCR_ST_SHADE_CODE" runat="server" CssClass="SmallFont LabelNo" 
                                                Text='<%# Bind("SHADE_CODE") %>' Visible="False"></asp:Label>
                                            <asp:Label ID="lblAPPR_QTY" runat="server" CssClass="SmallFont LabelNo" 
                                                Text='<%# Bind("QUANTITY") %>' Visible="False"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Branch Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBRANCH_CODE" runat="server" CssClass="SmallFont LabelNo" 
                                                Text='<%# Bind("BRANCH_NAME") %>' Visible="True"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="YEAR">
                                        <ItemTemplate>
                                            <asp:Label ID="lblYEAR" runat="server" CssClass="SmallFont Label" 
                                                Text='<%# Bind("YEAR") %>' ToolTip='<%# Bind("YEAR") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ARTICLE CODE">
                                        <ItemTemplate>
                                            <asp:Label ID="lblArticleCode" runat="server" CssClass="SmallFont Label" 
                                                Text='<%# Bind("ARTICLE_NO") %>' ToolTip='<%# Bind("ARTICLE_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SHADE CODE">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSHADE" runat="server" CssClass="SmallFont Label" 
                                                Text='<%# Bind("SHADE_CODE") %>' ToolTip='<%# Bind("SHADE_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ORDER TYPE">
                                        <ItemTemplate>
                                            <asp:Label ID="lblORDER_TYPE" runat="server" CssClass="SmallFont Label" 
                                                Text='<%# Bind("ORDER_TYPE") %>' ToolTip='<%# Bind("ORDER_TYPE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="REQ DATE">
                                        <ItemTemplate>
                                            <asp:Label ID="ibiREQ_DATE" runat="server" CssClass="SmallFont Label" 
                                                Text='<%# Bind("REQ_DATE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ORDER CAT">
                                        <ItemTemplate>
                                            <asp:Label ID="lblORDER_CAT" runat="server" CssClass="SmallFont Label" 
                                                Text='<%# Bind("ORDER_CAT") %>' ToolTip='<%# Bind("ORDER_CAT") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PRODUCT TYPE">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPRODUCT_TYPE" runat="server" CssClass="SmallFont Label" 
                                                Text='<%# Bind("PRODUCT_TYPE") %>' ToolTip='<%# Bind("PRODUCT_TYPE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField FooterText="Total" HeaderText="QUANTITY">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAdjustQuantity" runat="server" CssClass="SmallFont LabelNo" 
                                                Text='<%# Bind("QUANTITY") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ADJUST_QTY" HeaderText="AJUSTED QTY" />
                                    <asp:TemplateField HeaderText="Remaining">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAdjustRemQty" runat="server" CssClass="SmallFont LabelNo" 
                                                Text='<%# Bind("REMQTY") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Adjust QTY">
                                        <FooterTemplate>
                                            <asp:Label ID="txtTotalAdjustedIndentQTY" runat="server" 
                                                CssClass="SmallFont LabelNo" Width="50px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAdjustedIndentQTY" runat="server" AutoPostBack="True" 
                                                CssClass="SmallFont TextBoxNo" MaxLength="5" 
                                                OnTextChanged="txtAdjustedIndentQTY_TextChanged1" Text="0" Width="50px"></asp:TextBox>
                                            <asp:RangeValidator ID="RangeValidator1" runat="server" 
                                                ControlToValidate="txtAdjustedIndentQTY" Display="Dynamic" 
                                                ErrorMessage="Only numeric value allowed" MaximumValue="999999999" 
                                                MinimumValue="0" SetFocusOnError="true" Type="Double"></asp:RangeValidator>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnAdjAll" runat="server" 
                                                CommandArgument='<%# Bind("ORDER_NO") %>' CommandName="AdjAll" 
                                                Text="Adjust All" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
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
