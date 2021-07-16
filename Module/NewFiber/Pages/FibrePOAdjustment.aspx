<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FibrePOAdjustment.aspx.cs" Inherits="Module_Fiber_Pages_FibrePOAdjustment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Adjust Fibre Indent</title>
    <link href="../../../StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">

    function GetRowValue(val,TextBoxId)
    {           
        window.opener.document.getElementById(TextBoxId).value=val;   
        window.opener.document.forms[0].submit();      
        window.close();
    }

    </script>
    <style type="text/css">
        .style1
        {
            
        }
    </style>
</head>
<body bgcolor="#afcae4">
    <form id="form1" style="background-color: #afcae4" runat="server">
    <div>
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
                            
    <asp:UpdatePanel ID="updatePanel1" runat="server">
    <ContentTemplate>
    <table class="tContentArial" style="background-color: #afcae4">
    <tr>
    <td class="TableHeader td tdCenter">
       <span class="titleheading">Fiber Indent Adjustment</span>
    </td>
    <tr>
     <td class="style1">
      Adjust Indent for Fiber: 
     <asp:Label ID="lblindentadjustment" runat="server" CssClass="SmallFont Label" ></asp:Label>
     </td>
    </tr>
    <tr>
    <td class="tdCenter td SmallFont">
    <asp:Label ID="lblFibreIndentAdjustmentError" runat="server" CssClass="SmallFont Label" Font-Bold="False" ForeColor="Red"></asp:Label>
    </td>
    
    <tr>
                        <td class="tdCenter td SmallFont" >
                            <asp:Panel ID="pnlFibreIndentAdjustment" runat="server" ScrollBars="Vertical" 
                                BackColor="#afcae4">
                                    <asp:GridView ID="grdFibreIndentAdjustment" 
            runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" 
            OnRowCommand="grdFibreIndentAdjustment_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Indent Number">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdjustIndentNumber" CssClass="SmallFont LabelNo" runat="server"
                                                    Text='<%# Bind("IND_NUMB") %>'></asp:Label>
                                                <asp:Label ID="lblAPPR_QTY" CssClass="SmallFont LabelNo" runat="server" ToolTip='<%# Bind("Ind_Type") %>'
                                                    Text='<%# Bind("APPR_QTY") %>' Visible="False"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Indent Branch">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBranch" CssClass="SmallFont Label" runat="server" ToolTip='<%# Bind("BRANCH_CODE") %>'
                                                    Text='<%# Bind("BRANCH_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         
                                        <asp:TemplateField FooterText="Total" HeaderText="Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdjustRemQty" CssClass="SmallFont LabelNo" runat="server" 
                                                    Text='<%# Bind("REMQTY") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="POADJUST_QTY" HeaderText="Adjusted QTY" />
                                        <asp:TemplateField HeaderText="Adjust QTY">
                                            <FooterTemplate>
                                                <asp:Label ID="txtTotalAdjustedIndentQTY" CssClass="SmallFont LabelNo" runat="server"
                                                    Width="50px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtAdjustedIndentQTY" runat="server" Text="0" MaxLength="9" Width="50px"
                                                    AutoPostBack="True" CssClass="SmallFont TextBoxNo" 
                                                    OnTextChanged="txtAdjustedIndentQTY_TextChanged1"></asp:TextBox>
                                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtAdjustedIndentQTY"
                                                    ErrorMessage="Only numeric value allowed" Display="Dynamic" SetFocusOnError="true"
                                                    MaximumValue="999999999" MinimumValue="0" Type="Double"></asp:RangeValidator>
                                           
                                                   <cc1:FilteredTextBoxExtender ID="FiltertxtAdjustedIndentQTY" runat="server"
                                                       TargetControlID="txtAdjustedIndentQTY"         
                                                       FilterType="Custom, Numbers" ValidChars="."
                                                            />
                                           
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnAdjAll" runat="server" CommandArgument='<%# Bind("IND_NUMB") %>'
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
                            <asp:Button ID="btnAdjustIndentItem" runat="server" Text="Adjust Item Indent" OnClick="btnAdjustIndentItem_Click" />
                        </td>
                    </tr>
                    </tr>
                    </table>
    </ContentTemplate></asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
