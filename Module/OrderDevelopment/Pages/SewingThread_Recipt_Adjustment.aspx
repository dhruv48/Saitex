<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SewingThread_Recipt_Adjustment.aspx.cs"
    Inherits="Module_OrderDevelopment_Pages_SewingThread_Recipt_Adjustment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <title>Adjust Meterial Receipt</title>
    <link href="../../../StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">

    function GetRowValue(val,TextBoxId,txtBasicRate,Rate,AmountId,Amount,NoofUnit,no_ofUnit_ID)
    {               
        window.opener.document.getElementById(TextBoxId).value=val;  
//        window.opener.document.getElementById(txtBasicRate).value=Rate; 
//        window.opener.document.getElementById(AmountId).value=Amount;  
        window.opener.document.getElementById(no_ofUnit_ID).value=NoofUnit;  
               window.opener.document.forms[0].submit();   
        window.close();
    }

    </script>

    <form id="form2" runat="server" style="background-color: #afcae4">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table class="tContentArial" style="background-color: #afcae4">
                    <tr>
                        <td class="TableHeader td">
                            <b class="titleheading">Receipt Adjustment</b>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdCenter td SmallFont">
                            Adjust : Receiving For Article :
                            <asp:Label ID="lblAdjustItemReceiptCode" runat="server"></asp:Label>
                            <asp:Label ID="lblShadeCode" runat="server"></asp:Label>
                        </td>
                        <asp:Label ID="lblAdjustReceiptItemCode" runat="server"></asp:Label>
                    </tr>
                    <tr>
                        <td class="tdCenter td SmallFont">
                            So Remaining Quantity :
                            <asp:Label ID="lblRemainingMaxQty" Font-Bold="true" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdCenter td SmallFont">
                            <asp:Label ID="lblReceiptAdjustmentError" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdCenter td SmallFont">
                            <asp:Panel ID="pnlIndentAdjustment" runat="server" BackColor="#afcae4" Height="200px"
                                ScrollBars="Vertical">
                                <asp:GridView ID="grdReceiptAdjustment" runat="server" AutoGenerateColumns="False"
                                    OnRowCommand="grdReceiptAdjustment_RowCommand" ShowFooter="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Receipt Number">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdjustReceiptNuber" runat="server" Text='<%# Bind("TRN_NUMB") %>'></asp:Label>
                                                <asp:Label ID="lblAPPR_QTY" runat="server" Text='<%# Bind("QTY") %>' ToolTip='<%# Bind("TRN_TYPE") %>'
                                                    Visible="False"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Lot No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLotNo" runat="server" Text='<%# Bind("LOT_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Base Uom">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbaseuom" runat="server" Text='<%# Bind("UOM_OF_UNIT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Weight Of Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblunitweight" runat="server" Text='<%# Bind("WEIGHT_OF_UNIT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No Of Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnoofunit" runat="server" Text='<%# Bind("NO_OF_UNIT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterText="Total" HeaderText="Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdjustRemQty" runat="server" Text='<%# Bind("REMQTY") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Adjusted QTY">
                                            <ItemTemplate>
                                                <asp:Label ID="lbliradjustqty" runat="server" Text='<%# Bind("IRADJUST_QTY") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Adjust No Of Unit">
                                            <FooterTemplate>
                                                <asp:Label ID="txtTotalAdjustedReceiptNoofUnit" runat="server" Width="50px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtAdjustedReceiptNoofUnit" runat="server" AutoPostBack="True" MaxLength="5"
                                                    OnTextChanged="txtAdjustedReceiptNoofUnit_TextChanged1" Text="0" Width="50px"
                                                    CssClass="TextBoxNo"></asp:TextBox>
                                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtAdjustedReceiptQTY"
                                                    ErrorMessage="Only numeric value allowed" MaximumValue="99999999" MinimumValue="0"
                                                    Type="Double"></asp:RangeValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Adjust Unit Weight">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtAdjustedReceiptUnitWeight" runat="server" MaxLength="5" ReadOnly="true"
                                                    Text="0" Width="50px" CssClass="TextBoxNo TextBoxDisplay"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Adjust QTY">
                                            <FooterTemplate>
                                                <asp:Label ID="txtTotalAdjustedReceiptQTY" runat="server" Width="50px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtAdjustedReceiptQTY" runat="server" MaxLength="5" ReadOnly="true"
                                                    Text="0" Width="50px" CssClass="TextBoxNo TextBoxDisplay"></asp:TextBox>
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
