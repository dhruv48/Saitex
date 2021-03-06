<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YRN_TRN_SUB.aspx.cs" Inherits="Module_Yarn_SalesWork_Pages_YRN_TRN_SUB" %>

<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Generate Yarn Cartoon Sub Tran</title>
    <link rel="stylesheet" type="text/css" href="../../../../StyleSheet/CommonStyle.css" />

    <script language="javascript" type="text/javascript">

        function BindYRNQTY(totalQty, NoOfUNIT, WeightofUnit, gross_wt, tare_wt, cartons, no_of_pallet, txtQTY, txtNoOfUnit, txtWeightOfUnit, txtGross_WT, txtTareWT, txtCartons, txt_No_of_Pallet) {

            window.opener.document.getElementById(txtQTY).value = totalQty;
            window.opener.document.getElementById(txtNoOfUnit).value = NoOfUNIT;
            window.opener.document.getElementById(txtWeightOfUnit).value = WeightofUnit;
            window.opener.document.getElementById(txtGross_WT).value = gross_wt;
            window.opener.document.getElementById(txtTareWT).value = tare_wt;
            window.opener.document.getElementById(txtCartons).value = cartons;
            window.opener.document.getElementById(txt_No_of_Pallet).value = no_of_pallet;
            //            window.opener.document.getElementById(txtUOm).value = UOM;
            window.opener.document.forms[0].submit();
            window.close();
        }
        ////    function BindYRNQTY(totalQty,NoOfUNIT,UOM,WeightofUnit,txtQTY,txtNoOfUnit,txtWeightOfUnit,txtUOm)
        ////    {     
        ////        
        ////     window.opener.document.getElementById(txtQTY).value=totalQty.toFixed(4); 
        ////     window.opener.document.getElementById(txtNoOfUnit).value=NoOfUNIT.toFixed(4); 
        ////     window.opener.document.getElementById(txtUOm).value=UOM;
        ////     window.opener.document.forms[0].submit();      
        ////     window.close();
        ////    }
     

     
    </script>

    <%--changes--%>
</head>
<body bgcolor="#afcae4">
    <form id="form1" style="background-color: #afcae4" runat="server">
    <div>
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
        </cc1:ToolkitScriptManager>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
            ShowSummary="False" ValidationGroup="YM" />
      <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
                <table width="100%">
                    <tr>
                        <td align="center" class="td TableHeader" valign="top">
                            <strong class="titleheading">Sub Transaction of Yarn Packing :<asp:Label
                                ID="lblYarnCode" runat="server"></asp:Label>
                                of Shade Family :<asp:Label ID="txtShadeFamily" runat="server"></asp:Label> Shade :<asp:Label ID="txtShadeCode" runat="server"></asp:Label>
                            </strong>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="td TableHeader" valign="top">
                            <strong class="titleheading">Remaining Quantity =
                                <asp:Label ID="lblRemaining" runat="server"></asp:Label></strong>
                                  <asp:CheckBox ID="chkCartonWt" runat="server" Text="Add Carton Wt." Checked="true"  AutoPostBack="true"
                                TextAlign="Left" oncheckedchanged="chkCartonWt_CheckedChanged" ForeColor="Red" />
                                 
                                <asp:CheckBox ID="chkPaperTubeWt" runat="server" Text="Add PLT Wt." Checked="true"  AutoPostBack="true"
                                TextAlign="Left" oncheckedchanged="chkPaperTubeWt_CheckedChanged" ForeColor="Red" />
                              &nbsp;
                               <span style="color:Red; ">P.Wt.: </span>  
                                <asp:TextBox ID="txtPWT" runat="server" Width="50px" class="SmallFont" 
                               ></asp:TextBox>   
                        </td>
                        </td>
                    </tr>
                    <tr>
                        <td class="td tdLeft">
                            <table width="100%">
                                <tr bgcolor="#006699">
                                    <td align="left" class="tdLeft SmallFont" valign="top">
                                        <span class="titleheading"><b>Carton&nbsp;No</b></span>
                                    </td>
                                    <td align="left" class="tdLeft SmallFont" valign="top">
                                        <span class="titleheading"><b>Grade</b></span>
                                    </td>
                                    <td align="left" class="tdLeft SmallFont" valign="top">
                                        <span class="titleheading"><b>C.Wt</b></span>
                                    </td>
                                    <td align="left" class="tdLeft SmallFont" valign="top">
                                        <span class="titleheading"><b>Cops</b></span>
                                    </td>
                                    <td align="left" class="tdLeft SmallFont" valign="top">
                                        <span class="titleheading"><b>Gross.Wt.</b></span>
                                    </td>
                                    <td align="left" class="tdLeft SmallFont" valign="top">
                                        <span class="titleheading"><b>Tare.Wt.</b></span>
                                    </td>
                                    <td align="left" class="tdLeft SmallFont" valign="top">
                                        <span class="titleheading"><b>Net.Wt.</b></span>
                                    </td>
                                    <td align="left" class="tdLeft SmallFont" valign="top">
                                        <span class="titleheading"><b>UOM</b></span>
                                    </td>
                                    <td align="left" class="tdLeft SmallFont" valign="top" >
                                        <span class="titleheading"><b>Weight. of Unit</b></span>
                                    </td>
                                     <td align="left" class="tdLeft SmallFont" valign="top" >
                                        <span class="titleheading"><b>Bar&nbsp;Code</b></span>
                                    </td>
                                    <td align="left" class="tdLeft SmallFont" valign="top" >
                                        <span class="titleheading"><b>Is&nbsp;Pallet</b></span>
                                    </td>
                                    <td align="left" class="tdLeft SmallFont" valign="top" id="tddatemdh" runat="server" visible="false">
                                        <span class="titleheading"><b>Date of Manufacturing</b></span>
                                    </td>
                                    <td align="left" class="tdLeft SmallFont" valign="top" id="tdstatush" runat="server" visible="false">
                                        <span class="titleheading"><b>Material Status</b></span>
                                    </td>
                                    <td align="left" class="tdLeft SmallFont" valign="top">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="top">
                                        <asp:TextBox ID="txtCartornNo" Width="70px" class="SmallFont uppercase" 
                                            runat="server" TabIndex="1" ontextchanged="txtCartornNo_TextChanged" AutoPostBack="true" ></asp:TextBox>
                                        <br />
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:TextBox ID="txtGrade" class="SmallFont uppercase" Width="70px" runat="server"  TabIndex="2"></asp:TextBox>
                                        <br />
                                    </td>
                                     <td align="left" valign="top">
                                    <asp:TextBox ID="txtCWT" runat="server" Width="50px" class="SmallFont"   
                                            TabIndex="3" ontextchanged="txtCWT_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:TextBox ID="txtNoofUnit" runat="server" class="SmallFont" Width="70px" AutoPostBack="True"
                                            OnTextChanged="txtNoofUnit_TextChanged"  TabIndex="4"></asp:TextBox>
                                        <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="txtNoofUnit"
                                            Display="Dynamic" ErrorMessage="Please Enter No of Cops in Numeric &amp; Precision Should be 9 and Scale 2   "
                                            MaximumValue="999999999.9999" MinimumValue="0" Type="Double" ValidationGroup="YM"></asp:RangeValidator>
                                              <cc1:FilteredTextBoxExtender ID="FilteredTexttxtNoofUnit" runat="server"  TargetControlID="txtNoofUnit"   FilterType="Custom, Numbers" ValidChars="."/>
                                    </td>
                                       <td align="left" valign="top">
                                        <asp:TextBox ID="txtGrossWt" Width="70px" class="SmallFont" runat="server" AutoPostBack="True"
                                             MaxLength="15"  TabIndex="5" ontextchanged="txtGrossWt_TextChanged"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtQty"
                                            Display="Dynamic" ErrorMessage="Please Enter Gross WT" SetFocusOnError="True" ValidationGroup="YM"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtGrossWt"
                                            Display="Dynamic" ErrorMessage="Please Enter Gross Wt in Numeric &amp; Precision Should be 9 and Scale 2"
                                            MaximumValue="999999999.9999" MinimumValue="0" Type="Double" ValidationGroup="YM"></asp:RangeValidator>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"  TargetControlID="txtGrossWt"   FilterType="Custom, Numbers" ValidChars="." />
                                                                                </td>
                                                                                   <td align="left" valign="top">
                                        <asp:TextBox ID="txtTareWt" Width="70px" class="SmallFont" runat="server" AutoPostBack="True"  TabIndex="6"
                                            MaxLength="15" ontextchanged="txtTareWt_TextChanged"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtQty"
                                            Display="Dynamic" ErrorMessage="Please Enter Tare Wt" SetFocusOnError="True" ValidationGroup="YM"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RangeValidator5" runat="server" ControlToValidate="txtTareWt"
                                            Display="Dynamic" ErrorMessage="Please Enter Tare.Wt. in Numeric &amp; Precision Should be 9 and Scale 2   "
                                            MaximumValue="999999999.9999" MinimumValue="0" Type="Double" ValidationGroup="YM"></asp:RangeValidator>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"  TargetControlID="txtTareWt"   FilterType="Custom, Numbers" ValidChars="."/>
                                                                                </td>
                                    <td align="left" valign="top">
                                        <asp:TextBox ID="txtQty" Width="70px" class="SmallFont" runat="server" AutoPostBack="True"
                                            OnTextChanged="txtQty_TextChanged" MaxLength="15"  TabIndex="7"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtQty"
                                            Display="Dynamic" ErrorMessage="Please Enter QTY" SetFocusOnError="True" ValidationGroup="YM"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtQty"
                                            Display="Dynamic" ErrorMessage="Please Enter QTY in Numeric &amp; Precision Should be 9 and Scale 2   "
                                            MaximumValue="999999999.9999" MinimumValue="0" Type="Double" ValidationGroup="YM"></asp:RangeValidator>
                                            <cc1:FilteredTextBoxExtender ID="FiltertxtQty" runat="server"  TargetControlID="txtQty"   FilterType="Custom, Numbers" ValidChars="." />
                                                                                </td>
                                    <td align="left" valign="top">
                                        <asp:DropDownList ID="ddlUOM" class="SmallFont" runat="server"  TabIndex="7">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:TextBox ID="txtWeightofUnit" class="SmallFont" runat="server" Width="70px" Enabled="False"  TabIndex="8"></asp:TextBox>
                                        <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="txtWeightofUnit"
                                            Display="None" ErrorMessage="Please Enter Weight of Unit in Numeric &amp; Precision Should be 9 and Scale 2   "
                                            MaximumValue="999999999.9999" MinimumValue="0" Type="Double" ValidationGroup="YM"></asp:RangeValidator>
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:TextBox ID="txtBarCode" class="SmallFont" runat="server" Width="70px"  TabIndex="9"></asp:TextBox>
                                        
                                    </td>
                                    <td align="left" valign="top">
                                       <asp:CheckBox ID="isPallet" runat="server" Checked="false"  Text="" />
                                        
                                    </td>
                                    <td align="left" valign="top" id="tddatemdt" runat="server" visible="false">
                                        <asp:TextBox ID="txtDofMfd" class="SmallFont" Width="70px" runat="server"></asp:TextBox>
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
                                    <td align="left" valign="top" id="tdstatust" runat="server" visible="false">
                                        <asp:DropDownList ID="ddlMaterialStatus" class="SmallFont" runat="server">
                                            <%--<asp:ListItem Value="0">------Select---------</asp:ListItem>--%>
                                            <%--<asp:ListItem>UnCheck</asp:ListItem>
                                            <asp:ListItem>Extracted</asp:ListItem>
                                            <asp:ListItem>Rejected</asp:ListItem>--%>
                                            <asp:ListItem>Approved</asp:ListItem>
                                            <asp:ListItem>Rejected</asp:ListItem>
                                            <asp:ListItem>Hold</asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                    </td>
                                    <td align="left" valign="top">
                                  
                                        <asp:Button ID="BtnBOMSave" class="SmallFont" runat="server" OnClick="BtnBOMSave_Click"
                                            Text="Add" ValidationGroup="YM" Width="50px"  TabIndex="10" />
                                        <asp:Button ID="BtnBOMCancel" runat="server" class="SmallFont" OnClick="BtnBOMCancel_Click" Width="50px" 
                                            Text="Cancel"   TabIndex="11"/>
                                           
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="13" valign="top">
                                        <asp:GridView ID="grdsub_trn" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                            Font-Bold="False" OnRowCommand="grdSub_trnArticleDetail_RowCommand" ShowFooter="True"
                                            Width="98%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl&nbsp;No." ItemStyle-VerticalAlign="top" ItemStyle-Width="25px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtSubTrnUNIQUE_ID" runat="server" Text='<%# Bind("UNIQUE_ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="PI No" Visible="false">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbtpino" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("PI_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Lot&nbsp;No" Visible="false">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                     
                                                    <ItemTemplate>
                                                       <asp:Label ID="lblLotNO" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("LOT_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Grade">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBOMUOM" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("GRADE") %>'></asp:Label>
                                                    </ItemTemplate>
                                              <FooterTemplate>
                                                    <asp:Label ID="flblBOMUOM" runat="server" CssClass="LabelNo SmallFont"  >Total:</asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Carton&nbsp;No">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                       <asp:Label ID="lbtcartonno" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("CARTON_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                    <asp:Label ID="flbtcartonno" runat="server" CssClass="LabelNo SmallFont" ></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                   
                                                <asp:TemplateField HeaderText="Cops">
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNoUnit" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("NO_OF_UNIT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                    <asp:Label ID="flblNoUnit" runat="server" CssClass="LabelNo SmallFont" ></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Gross&nbsp;Wt">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGrossWt" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("GROSS_WT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                    <FooterTemplate>
                                                    <asp:Label ID="flblGrossWt" runat="server" CssClass="LabelNo SmallFont" ></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tare&nbsp;Wt">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTareWt" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TARE_WT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                    <FooterTemplate >
                                                    <asp:Label ID="flblTareWt" runat="server" CssClass="LabelNo SmallFont" ></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Net&nbsp;Wt">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQTY" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TRN_QTY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                      <FooterTemplate>
                                                    <asp:Label ID="flblQTY" runat="server" CssClass="LabelNo SmallFont" ></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="UOM">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUom" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("UOM_OF_UNIT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bar&nbsp;Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBarcodeNo" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("BARCODE_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Is&nbsp;Pallet">
                                                 <ItemTemplate>
                                                        <asp:Label ID="lblPallet" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("IS_PALLET") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                    <FooterTemplate>
                                                    <asp:Label ID="flblPallet" runat="server" CssClass="LabelNo SmallFont" ></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                               
                                                <asp:TemplateField HeaderText="Date of Mfd" Visible="false">
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBOMValueQty" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("DATE_OF_MFG") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                               
                                                <asp:TemplateField HeaderText="WeightofUnit">
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWeightofUnit" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("WEIGHT_OF_UNIT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Material Status" Visible="false">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtBOMArticleCode" runat="server" CssClass="Label SmallFont" Text='<%# Bind("MATERIAL_STATUS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Button ID="lnkBOMEdit" class="SmallFont" runat="server" CommandArgument='<%# Eval("UNIQUE_ID") %>'
                                                            CommandName="BOMEdit"  Text="Edit"  Width="50px"/>
                                                        <asp:Button ID="lnkBOMDelete" class="SmallFont" runat="server" CommandArgument='<%# Eval("UNIQUE_ID") %>'
                                                            CommandName="BOMDelete" OnClientClick="return confirm('Are you Sure want to delete this BOM Detail?');"
                                                             Text="Delete" Width="50px"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle CssClass="RowStyle " />
                                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                                            <AlternatingRowStyle CssClass="AltRowStyle" />
                                            <PagerStyle CssClass="PagerStyle" />
                                            <HeaderStyle BackColor="#336699" CssClass="SmallFont" ForeColor="White" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="td" valign="top">
                            <asp:Button ID="btnSubmit" class="SmallFont" runat="server" OnClick="btnSubmit_Click"
                                Text="Submit" ValidationGroup="M1" Width="60px"  TabIndex="11" />
                        </td>
                    </tr>
                </table>
           <%-- </ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
    </form>
</body>
</html>
