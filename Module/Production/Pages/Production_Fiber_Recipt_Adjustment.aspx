<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Production_Fiber_Recipt_Adjustment.aspx.cs" Inherits="Module_Production_Pages_Production_Fiber_Recipt_Adjustment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Adjust Poy Department Issue</title>
    
</head>
<body>
 <link href="../../../StyleSheet/CommonStyle.css"rel="stylesheet" type="text/css" />
     <script language="javascript" type="text/javascript">

         function GetRowValue(val, TextBoxId,     txtQtyUnit, QtyUnit,   txtQtyWeight, QtyWeight) {

             textObj = window.opener.document.getElementById(TextBoxId);
             textObj.value = val;           
             window.opener.document.getElementById(txtQtyUnit).value = QtyUnit;           
             window.opener.document.getElementById(txtQtyWeight).value = QtyWeight;
             window.opener.document.forms[0].submit();
             window.close();
         }

    </script>
     
  <form id="form2" runat="server" style="background-color: #afcae4">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table class="tContentArial" style="background-color: #afcae4" width="99%">
            <tr>
                <td class="TableHeader td tdCenter">
                    <strong>Receipt Adjustment</strong>
                </td>
            </tr>
            <tr>
                <td class="tdCenter td SmallFont">
                    Adjust : Issue For Poy Code :
                    <asp:Label ID="lblAdjustItemReceiptCode" runat="server"></asp:Label>
                   <%-- ( Of Shade :<asp:Label ID="lblAdjustItemReceiptShade" runat="server"></asp:Label>--%>
                   <%-- )--%>&nbsp;<asp:Label ID="lblPaHeding" runat="server"></asp:Label><asp:Label ID="lblPANO"
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
                    <asp:Panel ID="pnlIndentAdjustment" runat="server" BackColor="#afcae4" Height="100%"
                        ScrollBars="Vertical" Width="100%">
                        <asp:GridView ID="grdReceiptAdjustment" runat="server" AutoGenerateColumns="False"
                            OnRowCommand="grdReceiptAdjustment_RowCommand" ShowFooter="True">
                            <Columns>
                               <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                       
                                       <asp:Label ID="lblTrnDate" runat="server" Text='<%# Bind("TRN_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>/
                                      
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                              <asp:TemplateField HeaderText="Issue&nbsp;No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIssueNo" runat="server" Text='<%# Bind("ISS_TRN_NUMB") %>' ToolTip='<%# Bind("ISS_TRN_TYPE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="GRN&nbsp;Details">
                                    <ItemTemplate>
                                       
                                       <asp:Label ID="lblAdjustReceiptNumber" runat="server" Text='<%# Bind("TRN_NUMB") %>'></asp:Label>/
                                        <asp:Label ID="lblTRN_TYPE" runat="server" Text='<%# Bind("TRN_TYPE") %>'></asp:Label>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="PO&nbsp;Details">
                                    <ItemTemplate> 
                                       <asp:Label ID="lblPO_Number" runat="server" Text='<%# Bind("PO_NUMB") %>' ToolTip='<%# Bind("PO_TYPE") %>'></asp:Label>
                                         </ItemTemplate>
                                </asp:TemplateField>
                              
                                <asp:TemplateField HeaderText="Merge&nbsp;No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLotNo" runat="server" Text='<%# Bind("LOT_NO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pallet&nbsp;Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPalletCode" runat="server" Text='<%# Bind("PALLET_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pallet&nbsp;No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPalletNo" runat="server" Text='<%# Bind("PALLET_NO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                              
                              
                                <asp:BoundField DataField="WEIGHT_OF_UNIT" HeaderText="Weight&nbsp;Of&nbsp;Tube" />
                                 <asp:TemplateField HeaderText="No&nbsp;Of&nbsp;Tube">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoOfUnit" runat="server" Text='<%# Bind("NO_OF_UNIT") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField FooterText="Total" HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAdjustRemQty" runat="server" Text='<%# Bind("REMQTY") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="IRNO_OF_UNIT" HeaderText="Adjusted&nbsp;Tube" />
                                <asp:BoundField DataField="IRADJUST_QTY" HeaderText="Adjusted&nbsp;QTY" />
                                
                                <asp:TemplateField HeaderText="Adjust&nbsp;Tubes" >
                                  
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAdjustedReceiptQTYUnit" runat="server" AutoPostBack="True"  Text='<%# Bind("IRNO_OF_UNIT") %>'
                                            Width="50px" CssClass="Label SmallFont" 
                                            ontextchanged="txtAdjustedReceiptQTYUnit_TextChanged" ></asp:TextBox>
                                             <asp:RangeValidator ID="RangeValidatortxtAdjustedReceiptQTYUnit" runat="server" ControlToValidate="txtAdjustedReceiptQTYUnit"
                                            ErrorMessage="Only Numeric value allowed" MaximumValue="9999999999" MinimumValue="0"
                                            Type="Double"></asp:RangeValidator>
                                    </ItemTemplate>
                                    
                                      <FooterTemplate>
                                        <asp:Label ID="txtTotalAdjustedReceiptQTYUnit" runat="server" Width="50px"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Adjust&nbsp;Uom&nbsp;Of&nbsp;Unit" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAdjustedReceiptQTYUOM" runat="server" Text='<%# Bind("UOM_OF_UNIT") %>'
                                            Width="70px" ReadOnly="true" CssClass="Label SmallFont TextBox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Adjust&nbsp;Weight&nbsp;Of&nbsp;Unit" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAdjustedReceiptQTYWeight" runat="server" AutoPostBack="True"
                                            Text='<%# Bind("WEIGHT_OF_UNIT") %>' Width="70px" ReadOnly="true" 
                                            CssClass="Label SmallFont TextBoxNo" 
                                           ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Adjust&nbsp;QTY">
                                    
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAdjustedReceiptQTY" runat="server" AutoPostBack="True" OnTextChanged="txtAdjustedReceiptQTY_TextChanged1"
                                            Text='<%# Bind("IRADJUST_QTY") %>'  Width="50px" CssClass="Label SmallFont"></asp:TextBox>
                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtAdjustedReceiptQTY"
                                            ErrorMessage="Only Numeric value allowed" MaximumValue="9999999999" MinimumValue="0"
                                            Type="Double"></asp:RangeValidator>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="txtTotalAdjustedReceiptQTY" runat="server" Width="50px"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnAdjAll" runat="server" CommandArgument='<%# Bind("ISS_TRN_NUMB") %>'
                                            CommandName="AdjAll" Text="Adjust All"  />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>                       
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td class="tdCenter td SmallFont" align="center" width="100%">
                     <asp:Button ID="btnAdjustReceiptItem" runat="server" OnClick="btnAdjustReceiptItem_Click"
                        Text="Adjust Poy Receipt" />
                </td>
            </tr>
        </table>
        
    </div>
    </form>
</body>
</html>