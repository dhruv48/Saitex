<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Adj_INDENT_BOM.aspx.cs" Inherits="Module_Fiber_Pages_Adj_INDENT_BOM" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Adjust Fiber BOM</title>
    <script language="javascript" type="text/javascript">

    function GetRowValue(val,TextBoxId)
    {           
        window.opener.document.getElementById(TextBoxId).value=val;   
        window.opener.document.forms[0].submit();      
        window.close();
    }

    </script>
    <link href="../../../StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
</head>
<body style="background-color:#afcae4;">
    <form id="form1" runat="server" style="background-color:#afcae4;">
    <div>
    <cc1:ToolkitScriptManager ID="ToolKitScriptManager1" runat="server">
   </cc1:ToolkitScriptManager>
   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
   <ContentTemplate>
   <table class="tContentArial" style="background-color:#afcae4;">
   <tr>
   <td class="TableHeader td tdCenter">
   <span class="titleheading">
   Adjust Fiber BOM
   </span>
   </td>   
   </tr>
   <tr>
   <td class="tdCenter td SmallFont">
    Adjust BOM For Fiber
     <asp:Label ID="lblAdjITEMBOMCode" CssClass="SmallFont Label" runat="server"></asp:Label>
   </td>
   </tr>
   <tr>
   <td class="tdCenter td SmallFont">
   <asp:Label ID="lblAdjBomError" runat="server" CssClass="SmallFont Label"  Font-Bold="false" ForeColor="Red"></asp:Label>
   </td>
   </tr>
   <tr>
   <td class="SmallFont td tdCenter">
   <asp:Panel ID="pnlAdjBom" runat="server" ScrollBars="Vertical" Height="200px" BackColor="#afcae4">
   <asp:GridView ID="grdAdjBom" runat="server" AutoGenerateColumns="false" 
           ShowFooter="true" onrowcommand="grdAdjBom_RowCommand" >
           <Columns>
                                        <asp:TemplateField HeaderText="Buss. Type" HeaderStyle-BackColor="#336799" HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdjBOM_BUSINESS_TYPE" CssClass="SmallFont LabelNo" runat="server"
                                                    Text='<%# Bind("BOM_BUSINESS_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Product Type" HeaderStyle-BackColor="#336799"  HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdjBOM_PRODUCT_TYPE" CssClass="SmallFont LabelNo" runat="server"
                                                    Text='<%# Bind("BOM_PRODUCT_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Category" HeaderStyle-BackColor="#336799" HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdjBOM_ORDER_CAT" CssClass="SmallFont LabelNo" runat="server" Text='<%# Bind("BOM_ORDER_CAT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Type" HeaderStyle-BackColor="#336799" HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdjBOM_ORDER_TYPE" CssClass="SmallFont LabelNo" runat="server"
                                                    Text='<%# Bind("BOM_ORDER_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order #" HeaderStyle-BackColor="#336799" HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdjBOM_ORDER_NO" CssClass="SmallFont LabelNo" runat="server" Text='<%# Bind("BOM_ORDER_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PI_TYPE" HeaderStyle-BackColor="#336799" HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdjBOM_PI_TYPE" CssClass="SmallFont LabelNo" runat="server" Text='<%# Bind("BOM_PI_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PI #" HeaderStyle-BackColor="#336799" HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdjBOM_PI_NO" CssClass="SmallFont LabelNo" runat="server" Text='<%# Bind("BOM_PI_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Artical Code" HeaderStyle-BackColor="#336799" HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdjBOM_ARTICAL_CODE" CssClass="SmallFont LabelNo" runat="server"
                                                    Text='<%# Bind("BOM_ARTICAL_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ARTICLE_DESC" HeaderStyle-BackColor="#336799" HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdjFIBER_DESC" CssClass="SmallFont LabelNo" runat="server" Text='<%# Bind("FIBER_DESC") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="W SIDE" HeaderStyle-BackColor="#336799" HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdjBOM_W_SIDE" CssClass="SmallFont LabelNo" runat="server" Text='<%# Bind("BOM_W_SIDE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Yarn Code" HeaderStyle-BackColor="#336799" HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdjBOM_BASE_ARTICAL_CODE" CssClass="SmallFont LabelNo" runat="server"
                                                    Text='<%# Bind("BOM_BASE_ARTICAL_CODE") %>'></asp:Label>
                                                <asp:Label ID="lblAdjBOM_BASE_ARTICAL_TYPE" CssClass="SmallFont LabelNo" runat="server"
                                                    Text='<%# Bind("BOM_BASE_ARTICAL_TYPE") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="YARN DESC" HeaderStyle-BackColor="#336799" HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdjYARN_DESC" CssClass="SmallFont LabelNo" runat="server" Text='<%# Bind("YARN_DESC") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterText="Total" HeaderText="Quantity" HeaderStyle-BackColor="#336799" HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdjRemQty" CssClass="SmallFont LabelNo" runat="server" Text='<%# Bind("REMQTY") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="IND_ADJ_QTY" HeaderText="Adjusted QTY" HeaderStyle-BackColor="#336799"  HeaderStyle-ForeColor="White"/>
                                        <asp:TemplateField HeaderText="Adjust QTY" HeaderStyle-BackColor="#336799" HeaderStyle-ForeColor="White">
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
                                        <asp:TemplateField HeaderStyle-BackColor="#336799" HeaderStyle-ForeColor="White">
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