<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OCYarnCustAdjustment.aspx.cs" Inherits="Module_Prod_plan_Pages_OCYarnCustAdjustment" %>

<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Yarn Dyeing Customer Request Adjustment</title>
    <link href="../../../StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">

        function GetRowValue(val, txtQTY) {
            window.opener.document.getElementById(txtQTY).value = val;
            window.opener.document.forms[0].submit();
            window.close();
        }

    </script>
    <style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;}
    .header
    {
        margin-left: 2px;
    }
    .c1
    {
        width: 60px;
    }
    .c2
    {
        margin-left: 2px;
        width: 80px;
    }
    .c3
    {
        margin-left: 2px;
        width: 150px;
    }
    .c4
    {
        margin-left: 2px;
        width: 400px;
    }
    .c5
    {
        margin-left: 4px;
        width: 250px;
    }
</style>

</head>
<body bgcolor="#afcae4">
    <form id="form1" style="background-color: #afcae4" runat="server">
    <div>
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </cc1:ToolkitScriptManager>
         <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
            ShowSummary="False" ValidationGroup="YM" />
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                            <table class="tContentArial" style="background-color: #afcae4">
                    <tr>
                        <td class="TableHeader td tdCenter">
                            <span class="titleheading">Machine Planning </span>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdCenter td SmallFont">
                            <br />
                            
                            <asp:Label ID="lblAdjustArticleCode" runat="server"></asp:Label>  
                            Quality Code  :<asp:Label ID="lblQualityCode" runat="server"></asp:Label>                       
                             Shade Family :<asp:Label ID="txtShadeFamily" runat="server"></asp:Label> 
                             Shade :<asp:Label ID="txtShadeCode" runat="server"></asp:Label>            
                              Remaining Quantity :<asp:Label ID="lblAssQty" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdCenter td SmallFont">
                            <asp:Label ID="lblIndentAdjustmentError" runat="server" CssClass="SmallFont Label"
                                Font-Bold="False" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    
                    
                    <tr>
                        <td class="td tdLeft">
                            <table width="100%">
                                <tr bgcolor="#006699">
                                 <td align="left" class="tdLeft SmallFont" valign="top">
                                        <span class="titleheading"><b>TRN&nbsp;No</b></span>
                                    </td>
                                    <td align="left" class="tdLeft SmallFont" valign="top">
                                        <span class="titleheading"><b>Machine&nbsp;No</b></span>
                                    </td>
                                    <td align="left" class="tdLeft SmallFont" valign="top">
                                        <span class="titleheading"><b>Cons</b></span>
                                    </td>                                                                   
                                   
                                    <td align="left" class="tdLeft SmallFont" valign="top">
                                        <span class="titleheading"><b>Plan Qty</b></span>
                                    </td>
                                    <td align="left" class="tdLeft SmallFont" valign="top">
                                        <span class="titleheading"><b>UOM</b></span>
                                    </td>
                                      <td align="left" class="tdLeft SmallFont" valign="top">
                                        <span class="titleheading"><b>Lot No</b></span>
                                    </td>
                                   
                                    <td align="left" class="tdLeft SmallFont"  valign="top" id="tddatemdh" runat="server" >
                                        <span  class="titleheading"><b>Plan Date</b></span>
                                    </td>
                                     <td align="left" class="tdLeft SmallFont" valign="top">
                                        <span class="titleheading"><b>Supplier Name</b></span>
                                    </td>
                                       <td align="left" class="tdLeft SmallFont" valign="top">
                                        <span class="titleheading"><b>Remarks</b></span>
                                    </td>
                                  
                                    <td align="left" class="tdLeft SmallFont" valign="top">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                
                                <tr>
                                
                                 <td align="center" valign="top">
                                 <asp:TextBox ID="txtTRNNo" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                            ReadOnly="true" onkeyup="javascript:this.value = this.value.toUpperCase();" 
                            runat="server"></asp:TextBox>
                             </td>
                                
                                   
                                   <td align="center" valign="top">
                                        <cc2:ComboBox ID="ddlMachine" runat="server"  CssClass="SmallFont" Width="120px"
                                    EmptyText="select..." EnableLoadOnDemand="true" EnableVirtualScrolling="true" TabIndex="11"
                                    Height="200px" MenuWidth="200px" OnLoadingItems="ddlMacCode_LoadingItems" 
                                    >
                                    <HeaderTemplate>
                                        <div class="header c2">
                                            Mac Code</div>
                                        <div class="header c2">
                                            Mac Capacity</div>
                                       
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c2">
                                            <asp:Literal ID="Container7" runat="server" Text='<%# Eval("MACHINE_CODE") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal ID="Container3" runat="server" Text='<%# Eval("MACHINE_CAPACITY") %>' />
                                        </div>
                                    </ItemTemplate>
                                </cc2:ComboBox>
                                         </td>
                                   
                                   
                                    <td align="left" valign="top">
                                        <asp:TextBox ID="txtCons" class="SmallFont uppercase" Width="50px" runat="server"  TabIndex="3"></asp:TextBox>
                                        <br />
                                    </td>
                                  
                                   
                                    <td align="left" valign="top">
                                        <asp:TextBox ID="txtAdjustedIndentQTY" Width="60px" class="SmallFont" runat="server" 
                                          MaxLength="10"  TabIndex="7"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtAdjustedIndentQTY"
                                            Display="Dynamic" ErrorMessage="Please Enter QTY" SetFocusOnError="True" ValidationGroup="YM"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtAdjustedIndentQTY"
                                            Display="Dynamic" ErrorMessage="Please Enter QTY in Numeric &amp; Precision Should be 9 and Scale 2   "
                                            MaximumValue="999999999.9999" MinimumValue="0" Type="Double" ValidationGroup="YM"></asp:RangeValidator>
                                            <cc1:FilteredTextBoxExtender ID="FiltertxtAdjustedIndentQTY" runat="server"  TargetControlID="txtAdjustedIndentQTY"   FilterType="Custom, Numbers" ValidChars="." />
                                                                                </td>
                                    <td align="left" valign="top">
                                        <asp:DropDownList ID="ddlUOM" class="SmallFont" runat="server" Width="50px" TabIndex="8">
                                        </asp:DropDownList>
                                    </td>
                                  
                                     <td align="left" valign="top">
                                     <cc2:ComboBox ID="txtLotNo" runat="server"  EnableLoadOnDemand="true"
                            OnLoadingItems="txtLotNo_LoadingItems" DataTextField="MST_CODE" DataValueField="MST_CODE"
                            EmptyText="Lot No" AutoPostBack="true"
                            EnableVirtualScrolling="true" Width="100px" MenuWidth="150px" Height="200px"  TabIndex="28">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Lot No</div>
                                <div class="header c1">
                                    Desc</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c5">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("MST_CODE") %>' /></div>
                                <div class="item c5">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("MST_DESC") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                                        <%--<asp:TextBox ID="txtLotNo" CssClass="SmallFont TextBoxNo" Width="60px" runat="server"
                                          ></asp:TextBox>--%>
                                       
                                    </td>
                                     <td align="left" valign="top">
                                         <asp:TextBox ID="txtPlanningDate" runat="server" TabIndex="2" ValidationGroup="M1"  Width="80px"
                                    CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                                           </td>
                                           <td align="left" valign="top"> 
                                           
                                            <cc2:ComboBox ID="txtPartyCode1" runat="server"  EnableLoadOnDemand="true"
                            DataTextField="PRTY_CODE" DataValueField="PRTY_NAME" EmptyText="" EnableVirtualScrolling="true"
                            Width="100px" MenuWidth="400px" Height="200px" OnLoadingItems="txtPartyCode_LoadingItems">
                            <HeaderTemplate>
                                <div class="header c2">
                                    Code</div>
                                <div class="header c5">
                                    Name</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                <div class="item c5">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                                           
                                           
                                             </td>
                                          <td align="left" valign="top">
                                          <asp:TextBox ID="txtRemarks" CssClass="SmallFont TextBoxNo" Width="90px" runat="server"
                                          ></asp:TextBox>
                                           </td>
                                          
                                  
                                    <td align="left" valign="top">
                                  
                                        <asp:Button ID="BtnBOMSave" class="SmallFont" runat="server" OnClick="BtnBOMSave_Click"
                                            Text="Add" ValidationGroup="YM" Width="50px"  TabIndex="12" />
                                        <asp:Button ID="BtnBOMCancel" runat="server" class="SmallFont" OnClick="BtnBOMCancel_Click" Width="50px" 
                                            Text="Cancel"   TabIndex="13"/>
                                           
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="9" valign="top">
                                       <%-- <asp:GridView ID="grdsub_trn" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                            Font-Bold="False" OnRowCommand="grdSub_trnArticleDetail_RowCommand" ShowFooter="True"
                                            Width="98%">--%>
                                            
                                            
                                            <asp:GridView ID="grdsub_trn" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                            Font-Bold="False" OnRowCommand="grdSub_trnArticleDetail_RowCommand" ShowFooter="True"
                                            Width="98%" >
                                            
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl&nbsp;No." ItemStyle-VerticalAlign="top" ItemStyle-Width="25px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtSubTrnUNIQUE_ID" runat="server" Text='<%# Bind("UNIQUE_ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Top" />
                                                </asp:TemplateField>                                                
                                                  <asp:TemplateField HeaderText="Comp&nbsp;Code" Visible="false">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                       <asp:Label ID="lblCompCode" runat="server" Visible="false" CssClass="LabelNo SmallFont" Text='<%# Bind("CR_COMP_CODE") %>'></asp:Label>
                                                    </ItemTemplate>                                                   
                                                </asp:TemplateField>                                                
                                                  <asp:TemplateField HeaderText="Branch&nbsp;Code" Visible="false">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                       <asp:Label ID="lblBranchCode" runat="server" Visible="false" CssClass="LabelNo SmallFont" Text='<%# Bind("CR_BRANCH_CODE") %>'></asp:Label>
                                                    </ItemTemplate>                                                   
                                                </asp:TemplateField>  
                                                   <asp:TemplateField HeaderText="MAC&nbsp;TrnNo">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                       <asp:Label ID="lblMcTrn" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("MAC_TRN_NUMB") %>'></asp:Label>
                                                    </ItemTemplate>                                                   
                                                </asp:TemplateField>                                            
                                                 <asp:TemplateField HeaderText="Trn&nbsp;No">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                       <asp:Label ID="lblTrnNo" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TRN_NUMB") %>'></asp:Label>
                                                    </ItemTemplate>                                                   
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Order No" >
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbtpino" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("CR_ST_ORDER_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="YEAR" Visible="false">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblYEAR" Visible="false" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("CR_YEAR") %>'></asp:Label>
                                                    </ItemTemplate>                                                                                           
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Artical&nbsp;No">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                       <asp:Label ID="lblArticleCode" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("ARTICAL_CODE") %>'></asp:Label>
                                                    </ItemTemplate>                                                   
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="PI&nbsp;Type" Visible="false">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                       <asp:Label ID="lblPiType" runat="server" Visible="false" CssClass="LabelNo SmallFont" Text='<%# Bind("PI_TYPE") %>'></asp:Label>
                                                    </ItemTemplate>                                                   
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SHADE CODE">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSHADE" CssClass="SmallFont Label" runat="server" ToolTip='<%# Bind("SHADE_CODE") %>'
                                                    Text='<%# Bind("SHADE_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ORDER TYPE" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblORDER_TYPE" CssClass="SmallFont Label" Visible="false" runat="server" ToolTip='<%# Bind("CR_ORDER_TYPE") %>'
                                                    Text='<%# Bind("CR_ORDER_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                              <asp:TemplateField HeaderText="ORDER CAT" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblORDER_CAT" CssClass="SmallFont Label" Visible="false" runat="server" ToolTip='<%# Bind("CR_ORDER_CAT") %>'
                                                    Text='<%# Bind("CR_ORDER_CAT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PRODUCT TYPE" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPRODUCT_TYPE" CssClass="SmallFont Label"  Visible="false" runat="server" ToolTip='<%# Bind("CR_PRODUCT_TYPE") %>'
                                                    Text='<%# Bind("CR_PRODUCT_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                          
                                        </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Business Type">
                                          <ItemTemplate>
                                             <asp:Label ID="lblBusinessType" CssClass="SmallFont Label" runat="server" ToolTip='<%# Bind("CR_BUSINESS_TYPE") %>'
                                               Text='<%# Bind("CR_BUSINESS_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                              <FooterTemplate>
                                                    <asp:Label ID="flblBOMUOM" runat="server" CssClass="LabelNo SmallFont"  >Total:</asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>                                              
                                                  <asp:TemplateField HeaderText="Machine">
                                          <ItemTemplate>
                                             <asp:Label ID="lblMachineCode" CssClass="SmallFont Label" runat="server" ToolTip='<%# Bind("MACHINE_CODE") %>'
                                               Text='<%# Bind("MACHINE_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>   
                                                <asp:TemplateField HeaderText="Plan&nbsp;Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQTY" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("ADJ_QTY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                      <FooterTemplate>
                                                    <asp:Label ID="flblQTY" runat="server" CssClass="LabelNo SmallFont" ></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                
                                                    <asp:TemplateField HeaderText="Lot No" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtLotNo" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("GREY_LOT_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                   <asp:TemplateField HeaderText="Cons" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtCons" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("CONS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                 <asp:TemplateField HeaderText="UOM">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUom" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("UOM") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                                                            
                                                <asp:TemplateField HeaderText="Planning For Date" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtPlanningDate" runat="server"  CssClass="LabelNo SmallFont" Text='<%# Bind("PLANNING_DATE","{0:dd/MM/yyyy}") %>'  ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Supplier" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtJober" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("JOBER") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField> 
                                                
                                                
                                                 <asp:TemplateField HeaderText="Remarks" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtRemarks" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("REMARKS") %>'></asp:Label>
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
                              <cc1:CalendarExtender ID="cePlanninfDate" runat="server" TargetControlID="txtPlanningDate"
            Format="dd/MM/yyyy">
        </cc1:CalendarExtender>
                        </td>
                    </tr>
                    
                    <tr>
                        <td align=center class="td"  valign="top">
                            <asp:Button ID="btnAdjustIndentItem" runat="server" Text="Submit" 
                                OnClick="btnAdjustIndentItem_Click" style="height: 26px" />
                        </td>
                    </tr>
                </table>
          </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
