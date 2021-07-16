<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SW_TRN_SUB_MISC.aspx.cs" Inherits="Module_SewingThread_Page_SW_TRN_SUB_MISC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Generate Yarn Recieving Sub Tran</title>
    <link rel="stylesheet" type="text/css" href="../../../StyleSheet/CommonStyle.css" />

    <script language="javascript" type="text/javascript">
    
   function BindYRNQTY(totalQty,NoOfUNIT,UOM,WeightofUnit,txtQTY,txtNoOfUnit,txtWeightOfUnit,txtUOm)
    {     
        
     window.opener.document.getElementById(txtQTY).value=totalQty; 
     window.opener.document.getElementById(txtNoOfUnit).value=NoOfUNIT; 
     window.opener.document.forms[0].submit();      
     window.close();
    }
    
    
////    function BindYRNQTY(totalQty,NoOfUNIT,UOM,WeightofUnit,txtQTY,txtNoOfUnit,txtWeightOfUnit,txtUOm)
////    {     
////        
////     window.opener.document.getElementById(txtQTY).value=totalQty.toFixed(4); 
////     window.opener.document.getElementById(txtNoOfUnit).value=NoOfUNIT.toFixed(4); 
////     window.opener.document.getElementById(txtWeightOfUnit).value=WeightofUnit.toFixed(4); 
////     window.opener.document.getElementById(txtUOm).value=UOM;
////     window.opener.document.forms[0].submit();      
////     window.close();
////    }
     
    window.onload = function(){
        window.opener.document.body.disabled=true;
    }
    
    window.onunload = function(){
        window.opener.document.body.disabled=false;
    }
   
     
    </script>
<%--changes--%>
</head>
<body  >
    <form id="form1" style="background-color: #afcae4" runat="server">
    <div>
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
        </cc1:ToolkitScriptManager>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
            ShowMessageBox="True" ShowSummary="False" ValidationGroup="YM" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate> 
                <table >
                    <tr>
                        <td align="center" class="td TableHeader" valign="top">
                            <strong class="titleheading">Sub Transaction of Sewing Thread Receiving for
                            <asp:Label ID="lblArticleCode" runat="server"></asp:Label>
                             <asp:Label ID="lblShadeCode" runat="server"></asp:Label> </strong>
</td>
                    </tr>
                   <%-- <tr>
                        <td align="center" class="td TableHeader" valign="top">
                            <strong class="titleheading">Remaining Quantity =
                                <asp:Label ID="lblRemaining" runat="server"></asp:Label></strong>
                        </td>
                        </td>
                    </tr>--%>
                    <tr>
                        <td class="td tdLeft">
                            <table >
                                <tr bgcolor="#006699">
                                    <td align="left" class="  SmallFont" valign="top">
                                        <span class="titleheading"><b>Lot No</b></span>
                                    </td>
                                    <td align="left" class=" SmallFont" valign="top">
                                        <span class="titleheading"><b>Grade</b></span>
                                    </td>
                                    <td align="right" class=" SmallFont" valign="top">
                                        <span class="titleheading"><b>Base UOM</b></span>
                                    </td>
                                    <td align="right" class=" SmallFont" valign="top">
                                        <span class="titleheading"><b>Unit Weight </b></span>
                                    </td>
                                    <td align="left" class=" SmallFont" valign="top">
                                        <span class="titleheading"><b>No Of Unit</b></span>
                                    </td>
                                    <td align="right" class=" SmallFont" valign="top">
                                        <span class="titleheading"><b>Quantity</b></span>
                                    </td>
                                    <td align="left" class=" SmallFont" valign="top">
                                        <span class="titleheading"><b>Date of Manufacturing</b></span>
                                    </td>
                                    <td align="left" class=" SmallFont" valign="top">
                                        <span class="titleheading"><b>Material Status</b></span>
                                    </td>
                                    <td align="left" class=" SmallFont" valign="top">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="top">
                                        <asp:TextBox CssClass="SmallFont TextBox " ID="txtLotNo" Width="70px" runat="server"></asp:TextBox>
                                        <br />
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:TextBox ID="txtGrade" CssClass="SmallFont TextBox" Width="70px" runat="server"></asp:TextBox>
                                        <br />
                                    </td>
                                    <td align="right" valign="top">
                                        <asp:TextBox ID="txtBaseUOM" runat="server" CssClass="SmallFont TextBoxNo" 
                                            Enabled="False" ReadOnly="True" Width="70px"></asp:TextBox>
                                    </td>
                                    <td align="right" valign="top">
                                        <asp:TextBox ID="txtWeightofUnit" runat="server" CssClass="SmallFont TextBoxNo" 
                                            Enabled="False" ReadOnly="True" Width="70px"></asp:TextBox>
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:TextBox ID="txtNoofUnit" runat="server" AutoPostBack="True" 
                                            CssClass="SmallFont TextBoxNo" OnTextChanged="txtNoofUnit_TextChanged" 
                                            Width="70px"></asp:TextBox>
                                    </td>
                                    <td align="right" valign="top">
                                        <asp:TextBox ID="txtQty" runat="server" CssClass="SmallFont TextBoxNo" 
                                            ReadOnly="True" Width="70px"></asp:TextBox>
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:TextBox ID="txtDofMfd" CssClass="SmallFont TextBox" Width="70px" runat="server"></asp:TextBox>
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
                                        <asp:DropDownList ID="ddlMaterialStatus" CssClass="SmallFont " runat="server">
                                            <%--<asp:ListItem Value="0">------Select---------</asp:ListItem>--%>
                                            <asp:ListItem>UnCheck</asp:ListItem>
                                            <asp:ListItem>Extracted</asp:ListItem>
                                            <asp:ListItem>Rejected</asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:Button ID="BtnBOMSave" runat="server" OnClick="BtnBOMSave_Click" Text="Save"
                                            ValidationGroup="YM" CssClass="SmallFont" />
                                        <asp:Button ID="BtnBOMCancel" runat="server" OnClick="BtnBOMCancel_Click" 
                                            Text="Cancel" CssClass="SmallFont" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="9" valign="top">
                                        <asp:GridView ID="grdsub_trn" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                          Font-Bold="False" OnRowCommand="grdSub_trnArticleDetail_RowCommand"
                                            ShowFooter="True" Width="98%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl No." ItemStyle-VerticalAlign="top" ItemStyle-Width="25px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtSubTrnUNIQUE_ID" runat="server" Text='<%# Bind("UNIQUE_ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Top"  />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Lot No">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbtlotno" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("LOT_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Grade">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBOMUOM" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("GRADE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="QTY">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtQTY" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TRN_QTY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date of Mfd">
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBOMValueQty" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("DATE_OF_MFG") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NoUnit">
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"  />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNoUnit" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("NO_OF_UNIT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UOM">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"  />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUom" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("UOM_OF_UNIT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="WeightofUnit">
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"  />
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
                                                        <asp:Button ID="lnkBOMEdit" CssClass="SmallFont"  runat="server" CommandArgument='<%# Eval("UNIQUE_ID") %>'
                                                            CommandName="BOMEdit" TabIndex="12" Text="Edit" />
                                                        <asp:Button ID="lnkBOMDelete" runat="server" CommandArgument='<%# Eval("UNIQUE_ID") %>'
                                                            CommandName="BOMDelete"  CssClass="SmallFont" OnClientClick="return confirm('Are you Sure want to delete this BOM Detail?');"
                                                            TabIndex="12" Text="Delete" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle CssClass="RowStyle " />
                                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                                            <AlternatingRowStyle CssClass="AltRowStyle" />
                                            <PagerStyle CssClass="PagerStyle" />
                                            <HeaderStyle BackColor="#336699" CssClass="SmallFont"   ForeColor="White" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="td" valign="top">
                            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit"
                                ValidationGroup="M1" CssClass="SmallFont" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
