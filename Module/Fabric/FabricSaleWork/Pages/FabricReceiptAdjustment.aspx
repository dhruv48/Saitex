<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FabricReceiptAdjustment.aspx.cs" Inherits="Module_Inventory_Pages_FabricReceiptAdjustment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Sainath Texport Ltd.</title>
</head>
<body>
    <title>Adjust Fabric Receipt</title>
    <link href="../../../../StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">

        function GetRowValue(val, TextBoxId, txtBasicRate, Rate, AmountId, Amount, txtQtyUnit, QtyUnit, txtQtyUom, QtyUom, txtQtyWeight, QtyWeight) {

            textObj = window.opener.document.getElementById(TextBoxId);

            textObj.value = val;

            window.opener.document.getElementById(txtBasicRate).value = Rate;

            window.opener.document.getElementById(txtQtyUnit).value = QtyUnit;
            window.opener.document.getElementById(txtQtyUom).value = QtyUom;
            window.opener.document.getElementById(txtQtyWeight).value = QtyWeight;

            window.opener.document.forms[0].submit();
            window.close();
        }

    </script>

    <form id="form2" runat="server" style="background-color: #afcae4">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <%--  <toolkitscriptmanager id="ToolkitScriptManager1" runat="server">
        </toolkitscriptmanager>--%>
        <%--    <asp:Panel ID="pnlReceiptAdjustment" runat="server" BackColor="#afcae4">--%>
        <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
        <table class="tContentArial" style="background-color: #afcae4">
            <tr>
                <td class="TableHeader td tdCenter">
                    <strong>Receipt Adjustment</strong>
                </td>
            </tr>
            <tr>
                <td class="tdCenter td SmallFont">
                    Adjust : Issue For Fabric Code :
                    <asp:Label ID="lblAdjustItemReceiptCode" runat="server"></asp:Label>
                    ( Of Shade :<asp:Label ID="lblAdjustItemReceiptShade" runat="server"></asp:Label>
                    )&nbsp;<asp:Label ID="lblPaHeding" runat="server"></asp:Label><asp:Label ID="lblPANO"
                        runat="server"></asp:Label>
                    <asp:Label ID="lblMaxQtyDisp" runat="server">Max Qty :</asp:Label>
                    <asp:Label ID="lblMaxQty" runat="server">0</asp:Label>
                </td>
                <asp:Label ID="lblAdjustReceiptItemCode" runat="server"></asp:Label>
            </tr>
            <tr>
                <td class="tdCenter td SmallFont">
                    <asp:Label ID="lblReceiptAdjustmentError" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdCenter td SmallFont">
                    <asp:Panel ID="pnlIndentAdjustment" runat="server" BackColor="#afcae4" Height="400px"
                        ScrollBars="Vertical">
                        <asp:GridView ID="grdReceiptAdjustment" runat="server" AutoGenerateColumns="False"
                            OnRowCommand="grdReceiptAdjustment_RowCommand" ShowFooter="True">
                            <Columns>
                                <asp:TemplateField HeaderText="Receipt Number">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTRN_TYPE" runat="server" Text='<%# Bind("TRN_TYPE") %>'></asp:Label>
                                        /<asp:Label ID="lblAdjustReceiptNuber" runat="server" Text='<%# Bind("TRN_NUMB") %>'></asp:Label>
                                        <asp:Label ID="lblAPPR_QTY" runat="server" Text='<%# Bind("QTY") %>' ToolTip='<%# Bind("TRN_TYPE") %>'
                                            Visible="False"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Party/Packing Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPACKING_ID" runat="server" Text='<%# Bind("PACKING_ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lot No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLotNo" runat="server" Text='<%# Bind("LOT_NO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No Of Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoOfUnit" runat="server" Text='<%# Bind("NO_OF_UNIT") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="UOM_OF_UNIT" HeaderText="Uom Of Unit" />
                                <asp:BoundField DataField="WEIGHT_OF_UNIT" HeaderText="Weight Of Unit" />
                                <asp:TemplateField FooterText="Total" HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAdjustRemQty" runat="server" Text='<%# Bind("REMQTY") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="IRADJUST_QTY" HeaderText="Adjusted QTY" />
                                <asp:TemplateField HeaderText="Adjust QTY Unit" Visible="false">
                                    <FooterTemplate>
                                        <asp:Label ID="txtTotalAdjustedReceiptQTYUnit" runat="server" Width="50px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAdjustedReceiptQTYUnit" runat="server" AutoPostBack="True" Text='<%# Bind("NO_OF_UNIT") %>'
                                            Width="50px" CssClass="TextBoxNo TextBoxDisplay SmallFont"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Adjust Uom Of Unit" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAdjustedReceiptQTYUOM" runat="server" Text='<%# Bind("UOM_OF_UNIT") %>'
                                            Width="70px" ReadOnly="true" CssClass="TextBoxDisplay SmallFont TextBox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Adjust Weight Of Unit" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAdjustedReceiptQTYWeight" runat="server" AutoPostBack="True"
                                            Text='<%# Bind("WEIGHT_OF_UNIT") %>' Width="70px" ReadOnly="true" CssClass="TextBoxDisplay SmallFont TextBoxNo"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Adjust QTY">
                                    <FooterTemplate>
                                        <asp:Label ID="txtTotalAdjustedReceiptQTY" runat="server" Width="50px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAdjustedReceiptQTY" runat="server" AutoPostBack="True" OnTextChanged="txtAdjustedReceiptQTY_TextChanged1"
                                            Text="0" Width="50px" CssClass="SmallFont TextBoxNo"></asp:TextBox>
                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtAdjustedReceiptQTY"
                                            ErrorMessage="Only Numeric value allowed" MaximumValue="9999999999" MinimumValue="0"
                                            Type="Double"></asp:RangeValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnAdjAll" runat="server" CommandArgument='<%# Bind("TRN_NUMB") %>'
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
                    <asp:Button ID="btnAdjustReceiptItem" runat="server" OnClick="btnAdjustReceiptItem_Click"
                        Text="Adjust Item Receipt" />
                </td>
            </tr>
        </table>
        <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
        <%-- </asp:Panel>--%>
    </div>
    </form>
</body>
</html>
