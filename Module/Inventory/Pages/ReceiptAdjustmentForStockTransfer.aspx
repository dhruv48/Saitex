<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReceiptAdjustmentForStockTransfer.aspx.cs"
    Inherits="Inventory_ReceiptAdjustmentForStockTransfer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Adjust Meterial Receipt</title>
    <link href="../../../StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">

        function GetRowValue(val, TextBoxId, txtBasicRate, Rate, AmountId, Amount) {

            textObj = window.opener.document.getElementById(TextBoxId);
            textObj.value = val;
            window.opener.document.getElementById(txtBasicRate).value = Rate;
            window.opener.document.getElementById(AmountId).value = Amount;
            window.opener.document.forms[0].submit();
            window.close();
        }
        function PopUpClose() {
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
        <%--    <asp:Panel ID="pnlReceiptAdjustment" runat="server" BackColor="#afcae4">--%>
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
                            Adjust Indent For Item :
                            <asp:Label ID="lblAdjustItemReceiptCode" runat="server"></asp:Label>
                            <asp:Label ID="lblPaHeding" runat="server"></asp:Label>
                            <asp:Label ID="lblPANO" runat="server"></asp:Label>
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
                            <%--<asp:Panel ID="pnlIndentAdjustment" runat="server" ScrollBars="Vertical" Height="200px"  BackColor="#afcae4">--%>
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
                                           <asp:TemplateField HeaderText="Prty Ch No" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblprtychno" runat="server" CssClass="SmallFont Label" Text='<%# Eval("PRTY_CH_NUMB") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Bill Numb" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblbillnumb" runat="server" CssClass="SmallFont Label" Text='<%# Eval("BILL_NUMB") %>'></asp:Label>
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
                                          <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Location" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLocation" runat="server" CssClass="SmallFont LabelNo" Text='<%# Bind("LOCATION") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Store" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStore" runat="server" CssClass="SmallFont LabelNo" Text='<%# Bind("STORE") %>'></asp:Label>
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
                                        <%-- <asp:BoundField DataField="NO_OF_UNIT" HeaderText="No Of Unit" />
                                        <asp:BoundField DataField="UOM_OF_UNIT" HeaderText="Uom Of Unit" />
                                        <asp:BoundField DataField="WEIGHT_OF_UNIT" HeaderText="Weight Of Unit" />--%>
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
                                      <asp:TemplateField FooterStyle-HorizontalAlign="Right" FooterText="Total" HeaderStyle-HorizontalAlign="Right" Visible="false"
                                            HeaderText="Quantity" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdjustedQty" runat="server" CssClass="LabelNo" Text='<%# Bind("IRADJUST_QTY") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                         
                                        <%-- <asp:TemplateField HeaderText="Adjust QTY">
                                            <FooterTemplate>
                                                <asp:Label ID="txtTotalAdjustedReceiptQTYUnit" runat="server" Width="50px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtAdjustedReceiptQTYUnit" runat="server" AutoPostBack="True" OnTextChanged="txtAdjustedReceiptQTYUnit_TextChanged1"
                                                    Text="0" Width="50px" CssClass="TextBoxNo tdRight"></asp:TextBox>
                                              
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Adjust Uom Of Unit">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtAdjustedReceiptQTYUOM" runat="server" AutoPostBack="True" Text='<%# Bind("UOM_OF_UNIT") %>'
                                                    Width="70px" ReadOnly="true" CssClass="TextBoxDisplay SmallFont TextBox"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Adjust Weight Of Unit">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtAdjustedReceiptQTYWeight" runat="server" AutoPostBack="True"
                                                    Text='<%# Bind("WEIGHT_OF_UNIT") %>' Width="70px" ReadOnly="true" CssClass="tdRight TextBoxDisplay SmallFont TextBoxNo"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right"
                                            HeaderText="Adjust QTY" ItemStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                <asp:Label ID="txtTotalAdjustedReceiptQTY" runat="server" CssClass="tdRight LabelNo"
                                                    Width="50px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtAdjustedReceiptQTY" runat="server" AutoPostBack="True" CssClass="SmallFont TextBoxNo"
                                                    MaxLength="11" OnTextChanged="txtAdjustedReceiptQTY_TextChanged1" Text='<%# Bind("IRADJUST_QTY") %>' Width="50px" ></asp:TextBox>
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
                          <%--  </asp:Panel>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdCenter td SmallFont">
                            <asp:Button ID="btnAdjustReceiptItem" runat="server" Text="Adjust Item Receipt" OnClick="btnAdjustReceiptItem_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <%-- </asp:Panel>--%>
    </div>
    </form>
</body>
</html>
