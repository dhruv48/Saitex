<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReceiptAdjustmentFabric.aspx.cs"
    Inherits="Module_Fabric_FabricSaleWork_Pages_ReceiptAdjustmentFabric" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Adjust Fabric Receipt</title>
    <link href="../../../../StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">

        function GetRowValue(val, TextBoxId, txtBasicRate, Rate, AmountId, Amount, txtQtyUnit, QtyUnit, txtQtyUom, QtyUom, txtQtyWeight, QtyWeight) {

            textObj = window.opener.document.getElementById(TextBoxId);
            textObj.value = val;

            window.opener.document.getElementById(txtBasicRate).value = Rate;
            window.opener.document.getElementById(AmountId).value = Amount;
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
                        <asp:Label ID="lblAdjustReceiptItemCode" runat="server"></asp:Label></tr>
                    <tr>
                        <td class="tdCenter td SmallFont">
                            <asp:Label ID="lblReceiptAdjustmentError" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdCenter td SmallFont">
                            <asp:Panel ID="pnlIndentAdjustment" runat="server" ScrollBars="Vertical" Height="200px"
                                BackColor="#afcae4">
                                <asp:GridView ID="grdReceiptAdjustment" runat="server" AutoGenerateColumns="False"
                                    OnRowCommand="grdReceiptAdjustment_RowCommand" ShowFooter="True" Width="99%">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Receipt Type" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTRN_TYPE" runat="server" Text='<%# Bind("TRN_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Receipt Number"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdjustReceiptNuber" runat="server" CssClass="SmallFont LabelNo"
                                                    Text='<%# Bind("TRN_NUMB") %>'></asp:Label>
                                                <asp:Label ID="lblAPPR_QTY" runat="server" CssClass="SmallFont LabelNo" Text='<%# Bind("QTY") %>'
                                                    ToolTip='<%# Bind("TRN_TYPE") %>' Visible="False"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PO comp" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpocomp" runat="server" CssClass="SmallFont Label" Text='<%# Eval("PO_COMP_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PO Branch">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpoBranch" runat="server" CssClass="SmallFont Label" Text='<%# Eval("PO_BRANCH") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PO Numb" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpotype" runat="server" CssClass="SmallFont Label" Text='<%# Eval("PO_TYPE")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PO Numb">
                                            <ItemTemplate>
                                                <asp:Label ID="lblponumb" runat="server" CssClass="SmallFont Label" Text='<%# Eval("PO_NUMB")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Receipt Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltrnDate" runat="server" CssClass="SmallFont Label" Text='<%# Eval("TRN_DATE","{0:dd-MM-yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Party Name" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPartyName" runat="server" CssClass="SmallFont Label" Text='<%# Eval("PRTY_NAME")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Lot No" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLotNo" runat="server" CssClass="SmallFont LabelNo" Text='<%# Bind("LOT_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="F.Rate" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFinalRate" runat="server" CssClass="SmallFont LabelNo" Text='<%# Bind("FINAL_RATE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-HorizontalAlign="Right" FooterText="Total" HeaderStyle-HorizontalAlign="Right"
                                            HeaderText="Quantity" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdjustRemQty" runat="server" CssClass="LabelNo" Text='<%# Bind("REMQTY") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="IRADJUST_QTY" HeaderStyle-HorizontalAlign="Right" HeaderText="Adjusted QTY"
                                            ItemStyle-HorizontalAlign="Right" />
                                        <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right"
                                            HeaderText="Adjust QTY" ItemStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                <asp:Label ID="txtTotalAdjustedReceiptQTY" runat="server" CssClass="tdRight LabelNo"
                                                    Width="50px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtAdjustedReceiptQTY" runat="server" AutoPostBack="True" CssClass="SmallFont TextBoxNo"
                                                    MaxLength="5" OnTextChanged="txtAdjustedReceiptQTY_TextChanged1" Text="0" Width="50px"></asp:TextBox>
                                                <br />
                                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtAdjustedReceiptQTY"
                                                    ErrorMessage="Only Numeric value allowed" MaximumValue="999999999" MinimumValue="0"
                                                    Type="Double"></asp:RangeValidator>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnAdjAll" Font-Underline="true" ForeColor="Desktop" runat="server"
                                                    CommandArgument='<%# Bind("TRN_NUMB") %>' CommandName="AdjAll" Text="Adjust All" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="RowStyle SmallFont" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                    <PagerStyle CssClass="PagerStyle" />
                                    <HeaderStyle CssClass="HeaderStyle GrdHeader" />
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdCenter td SmallFont">
                            <asp:Button ID="btnAdjustReceiptFabric" runat="server" Text="Adjust Fabric Receipt" OnClick="btnAdjustReceiptFabric_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
