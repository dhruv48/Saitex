.<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CR_Receipt_Adjustment.aspx.cs" Inherits="Module_Appaerals_Pages_CR_Receipt_Adjustment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Adjust Waste Receipt</title>
    <link href="../../../StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />

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

</head>
<body>
    <form id="form2" runat="server" style="background-color: #afcae4">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table class="tContentArial" style="background-color: #afcae4">
            <tr>
                <td class="TableHeader td tdCenter">
                    <strong>Receipt Adjustment</strong>
                </td>
            </tr>
            <tr>
                <td class="tdCenter td SmallFont">
                    Adjust Issue For Code :
                    <asp:Label ID="lblAdjustItemReceiptCode" runat="server"></asp:Label>
                    ( of Shade :
                    <asp:Label ID="lblAdjustItemReceiptShade" runat="server"></asp:Label>)
                    <asp:Label ID="lblPaHeding" runat="server"></asp:Label><asp:Label ID="lblPANO" runat="server"></asp:Label><asp:Label
                        ID="lblMaxQty" runat="server"></asp:Label>
                        <asp:GridView ID="grdReceiptAdjustment" runat="server" AutoGenerateColumns="False"
                            OnRowCommand="grdReceiptAdjustment_RowCommand" ShowFooter="True" CssClass="SmallFont"
                            RowStyle-VerticalAlign="Top" 
                        onselectedindexchanged="grdReceiptAdjustment_SelectedIndexChanged">
                            <Columns>
                                <asp:TemplateField HeaderText="Rcpt.Type-No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTRN_TYPE" runat="server" Text='<%# Bind("TRN_TYPE") %>'></asp:Label>-
                                        <asp:Label ID="lblAdjustReceiptNuber" runat="server" 
                                            Text='<%# Bind("TRN_NUMB") %>'></asp:Label>
                                        <asp:Label ID="lblAPPR_QTY" runat="server" Text='<%# Bind("QTY") %>' ToolTip='<%# Bind("TRN_TYPE") %>'
                                            Visible="True"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="SmallFont tdLeft" />
                                    <HeaderStyle CssClass="SmallFont tdLeft" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="item">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPACKING_ID" runat="server" Text='<%# Bind("ITEM_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="SmallFont tdLeft" />
                                    <HeaderStyle CssClass="SmallFont tdLeft" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lot No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLotNo" runat="server" Text='<%# Bind("LOT_NO") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="SmallFont tdLeft" />
                                    <HeaderStyle CssClass="SmallFont tdLeft" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="UOM_OF_UNIT" HeaderText="Uom Of Unit" ItemStyle-CssClass="SmallFont tdRight"
                                    HeaderStyle-CssClass="SmallFont tdRight" />
                               <%-- <asp:BoundField DataField="WEIGHT_OF_UNIT" HeaderText="Weight Of Unit" ItemStyle-CssClass="SmallFont tdRight"
                                    HeaderStyle-CssClass="SmallFont tdRight" />--%>
                                <asp:TemplateField FooterText="Total" HeaderText="Total Pack Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAdjustRemQty" runat="server" Text='<%# Bind("REMQTY") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="SmallFont tdRight" />
                                    <HeaderStyle CssClass="SmallFont tdRight" />
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="No Of Unit" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoOfUnit" runat="server" Text='<%# Bind("NO_OF_UNIT") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="SmallFont tdRight" />
                                    <HeaderStyle CssClass="SmallFont tdRight" />
                                </asp:TemplateField>--%>
                                <asp:BoundField DataField="IR_ADJUST_QTY" HeaderText="Pre Adj.Unit" ItemStyle-CssClass="SmallFont tdRight"
                                    HeaderStyle-CssClass="SmallFont tdRight" />
                                <asp:TemplateField HeaderText="Adj.Unit">
                                    <FooterTemplate>
                                        <asp:Label ID="txtTotalAdjustedReceiptQTYUnit" runat="server" Width="50px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAdjustedReceiptQTYUnit" runat="server" AutoPostBack="True" OnTextChanged="txtAdjustedReceiptQTYUnit_TextChanged1"
                                            Text="0" Width="50px" CssClass="TextBoxNo"></asp:TextBox>
                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtAdjustedReceiptQTYUnit"
                                            ErrorMessage="Only Numeric value allowed" Display="Dynamic" MaximumValue="10000"
                                            MinimumValue="0" Type="Double"></asp:RangeValidator>
                                        <asp:TextBox ID="txtAdjustedReceiptQTYUOM" runat="server" AutoPostBack="True" Text='<%# Bind("UOM_OF_UNIT") %>'
                                            Visible="false" Width="1px" ReadOnly="true" 
                                            CssClass="TextBoxDisplay SmallFont TextBox"></asp:TextBox>
                                        <asp:TextBox ID="txtAdjustedReceiptQTYWeight" runat="server" AutoPostBack="True"
                                            Visible="false" Text='<%# Bind("WEIGHT_OF_UNIT") %>' Width="1px" ReadOnly="true"
                                            CssClass="TextBoxDisplay SmallFont TextBoxNo"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="SmallFont tdRight" />
                                    <HeaderStyle CssClass="SmallFont tdRight" />
                                    <FooterStyle CssClass="SmallFont tdRight" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Adj QTY" >
                                    <FooterTemplate>
                                        <asp:Label ID="txtTotalAdjustedReceiptQTY" runat="server" Width="50px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAdjustedReceiptQTY" runat="server" AutoPostBack="True" MaxLength="5"
                                            Text="0" Width="50px" CssClass="TextBoxDisplay SmallFont TextBoxNo" ReadOnly="true"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="SmallFont tdRight" />
                                    <HeaderStyle CssClass="SmallFont tdRight" />
                                    <FooterStyle CssClass="SmallFont tdRight" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnAdjAll" runat="server" CommandArgument='<%# Bind("TRN_NUMB") %>'
                                            CommandName="AdjAll" Text="Adjust All" />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="SmallFont tdLeft" />
                                    <HeaderStyle CssClass="SmallFont tdLeft" />
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle CssClass="RowStyle SmallFont" />
                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                            <PagerStyle CssClass="PagerStyle" />
                            <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                        </asp:GridView>
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
                    <asp:Panel ID="pnlIndentAdjustment" runat="server" BackColor="#afcae4" Height="350px"
                        ScrollBars="Vertical" CssClass="SmallFont">
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td class="tdCenter td SmallFont">
                    <asp:Button ID="btnAdjustReceiptItem" runat="server" OnClick="btnAdjustReceiptItem_Click"
                        Text="Adjust Item Receipt" Width="150px" />
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
