<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SW_TRN_SUB.aspx.cs" Inherits="Module_SewingThread_Page_SW_TRN_SUB" %>

<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Generate sewing Thread Recieving Sub Tran</title>
    <link rel="stylesheet" type="text/css" href="../../../../StyleSheet/CommonStyle.css" />

    <script language="javascript" type="text/javascript">
    
   
    function BindYRNQTY(totalQty,NoOfUNIT,UOM,WeightofUnit,txtQTY,txtNoOfUnit,txtWeightOfUnit,txtUOm)
    {     
        
     window.opener.document.getElementById(txtQTY).value=totalQty; 
     window.opener.document.getElementById(txtNoOfUnit).value=NoOfUNIT; 
     window.opener.document.getElementById(txtWeightOfUnit).value=WeightofUnit; 
        window.opener.document.getElementById(txtUOm).value=UOM;
     window.opener.document.forms[0].submit();      
     window.close();
    }
     

     
    </script>

</head>
<body bgcolor="#afcae4">
    <form id="form1" style="background-color: #afcae4" runat="server">
    <div>
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
        </cc1:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td align="center" class="td TableHeader" valign="top">
                            <strong class="titleheading">Sub Transaction of Sewing Thread Receiving</strong>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="td TableHeader" valign="top">
                            <strong class="titleheading">Remaining Quantity =
                                <asp:Label ID="lblRemaining" runat="server"></asp:Label></strong>
                        </td>
                        </td>
                    </tr>
                    <tr>
                        <td class="td tdLeft">
                            <table width="98%">
                                <tr bgcolor="#006699">
                                    <td align="left" class="tdLeft SmallFont" valign="top">
                                        <span class="titleheading"><b>Lot No</b></span>
                                    </td>
                                    <td align="left" class="tdLeft SmallFont" valign="top">
                                        <span class="titleheading"><b>Grade</b></span>
                                    </td>
                                    <td align="left" class="tdLeft SmallFont" valign="top">
                                        <span class="titleheading"><b>Qty</b></span>
                                    </td>
                                    <td align="left" class="tdLeft SmallFont" valign="top">
                                        <span class="titleheading"><b>No unit</b></span>
                                    </td>
                                    <td align="left" class="tdLeft SmallFont" valign="top">
                                        <span class="titleheading"><b>UOM</b></span>
                                    </td>
                                    <td align="left" class="tdLeft SmallFont" valign="top">
                                        <span class="titleheading"><b>Weight. of Unit</b></span>
                                    </td>
                                    <td align="left" class="tdLeft SmallFont" valign="top">
                                        <span class="titleheading"><b>Date of Manufacturing</b></span>
                                    </td>
                                    <td align="left" class="tdLeft SmallFont" valign="top">
                                        <span class="titleheading"><b>Material Status</b></span>
                                    </td>
                                    <td align="left" class="tdLeft SmallFont" valign="top">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="top">
                                        <asp:TextBox ID="txtLotNo" Width="70px" runat="server"></asp:TextBox>
                                        <br />
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:TextBox ID="txtGrade" Width="70px" runat="server"></asp:TextBox>
                                        <br />
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:TextBox ID="txtQty" Width="70px" runat="server" AutoPostBack="True" OnTextChanged="txtQty_TextChanged"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtQty"
                                            Display="Dynamic" ErrorMessage="Please Enter QTY" SetFocusOnError="True" ValidationGroup="YM"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtQty"
                                            Display="Dynamic" ErrorMessage="Please Enter QTY in Numeric &amp; Precision Should be 9 and Scale 2   "
                                            MaximumValue="999999999.99" MinimumValue="0" Type="Double" ValidationGroup="YM"></asp:RangeValidator>
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:TextBox ID="txtNoofUnit" runat="server" Width="70px" AutoPostBack="True" OnTextChanged="txtNoofUnit_TextChanged"></asp:TextBox>
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:DropDownList ID="ddlUOM" runat="server">
                                            <%--<asp:ListItem Value="0">------Select---------</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:TextBox ID="txtWeightofUnit" runat="server" Width="70px" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:TextBox ID="txtDofMfd" Width="70px" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDofMfd"
                                            Display="Dynamic" ErrorMessage="Please Enter Date Of Manufacutring" SetFocusOnError="True"
                                            ValidationGroup="YM"></asp:RequiredFieldValidator>
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupPosition="TopLeft"
                                            TargetControlID="txtDofMfd">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                            MaskType="Date" PromptCharacter="_" TargetControlID="txtDofMfd">
                                        </cc1:MaskedEditExtender>
                                        <br />
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:DropDownList ID="ddlMaterialStatus" runat="server">
                                            <%--<asp:ListItem Value="0">------Select---------</asp:ListItem>--%>
                                            <asp:ListItem>UnCheck</asp:ListItem>
                                            <asp:ListItem>Extracted</asp:ListItem>
                                            <asp:ListItem>Rejected</asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:Button ID="BtnBOMSave" runat="server" OnClick="BtnBOMSave_Click" Text="Save"
                                            ValidationGroup="YM" />
                                        <asp:Button ID="BtnBOMCancel" runat="server" OnClick="BtnBOMCancel_Click" Text="Cancel" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="9" valign="top">
                                        <asp:GridView ID="grdsub_trn" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                            BorderWidth="1px" CssClass="SmallFont" Font-Bold="False" OnRowCommand="grdSub_trnArticleDetail_RowCommand"
                                            ShowFooter="True" Width="98%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl No." ItemStyle-VerticalAlign="top" ItemStyle-Width="25px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtSubTrnUNIQUE_ID" runat="server" Text='<%# Bind("UNIQUE_ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Top" Width="25px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Lot No">
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbtlotno" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("LOT_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Grade">
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBOMUOM" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("GRADE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="QTY">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtQTY" runat="server" Text='<%# Bind("TRN_QTY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date of Mfd">
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBOMValueQty" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("DATE_OF_MFG") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NoUnit">
                                                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNoUnit" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("NO_OF_UNIT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UOM">
                                                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUom" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("UOM_OF_UNIT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="WeightofUnit">
                                                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWeightofUnit" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("WEIGHT_OF_UNIT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Material Status">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtBOMArticleCode" runat="server" CssClass="Label SmallFont" Text='<%# Bind("MATERIAL_STATUS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Button ID="lnkBOMEdit" runat="server" CommandArgument='<%# Eval("UNIQUE_ID") %>'
                                                            CommandName="BOMEdit" TabIndex="12" Text="Edit" />
                                                        <asp:Button ID="lnkBOMDelete" runat="server" CommandArgument='<%# Eval("UNIQUE_ID") %>'
                                                            CommandName="BOMDelete" OnClientClick="return confirm('Are you Sure want to delete this BOM Detail?');"
                                                            TabIndex="12" Text="Delete" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle CssClass="RowStyle SmallFont" />
                                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                                            <AlternatingRowStyle CssClass="AltRowStyle" />
                                            <PagerStyle CssClass="PagerStyle" />
                                            <HeaderStyle BackColor="#336699" CssClass="HeaderStyle SmallFont" ForeColor="White" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="td" valign="top">
                            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit"
                                ValidationGroup="M1" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
