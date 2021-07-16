<%@ Page Language="C#" AutoEventWireup="true" CodeFile="COST.aspx.cs" Inherits="Module_OrderDevelopment_Pages_COST" %>

<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Get Cost</title>
    <link rel="stylesheet" type="text/css" href="../../../StyleSheet/CommonStyle.css" />

    <script language="javascript" type="text/javascript">
    
    function BindYRNSPIN_COST()
    {          
        window.opener.document.forms[0].submit();      
        window.close();
    } 
     
    function Calculation(val)
    {                                                                
     document.getElementById('txtTotalCost').value=(parseFloat(document.getElementById('txtFreight').value)+parseFloat(document.getElementById('txtCommission').value)+parseFloat(document.getElementById('txtIncentives').value)+parseFloat(document.getElementById('txtExMill').value)+parseFloat(document.getElementById('txtBrokerage').value)).toFixed(2);
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
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
        </cc1:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                   <tr>
                        <td align="center" class="td TableHeader" valign="top">
                            <strong class="titleheading">Production Machine Planning:
                                <asp:Label ID="lblARTICAL_CODE" runat="server"></asp:Label>
                                &nbsp;&nbsp;
                                 Shade :
                                <asp:Label ID="lblSHADE_CODE" runat="server"></asp:Label>
                                &nbsp;&nbsp;
                                PI NO :
                                <asp:Label ID="lblpinov" runat="server"></asp:Label>
                                &nbsp;&nbsp;
                                Order NO :
                                <asp:Label ID="lblorderno" runat="server"></asp:Label>
                                &nbsp;&nbsp;
                                Order Type:
                                <asp:Label ID="lblorderTYPE" runat="server"></asp:Label>
                                
                            
                            </strong>
                        </td>
                    </tr>
                   <tr>
                        <td align="center" class="td TableHeader" valign="top">
                            <strong class="titleheading">Remaining Quantity =
                                <asp:Label ID="lblRemaining" runat="server"></asp:Label></strong>
                        </td>
                    </tr>
                    <tr class="SmallFont">
                        <td class="td tdLeft">
                            <table width="98%">
                                <tr bgcolor="#006699">
                                  <td align="center" valign="top" width="10%">
                                        <span class="titleheading"><b>Trn No.</b></span>
                                    </td>
                                    <td align="center" valign="top" width="10%">
                                        <span class="titleheading"><b>Machine</b></span>
                                    </td>
                                    <td align="center" valign="top" width="10%">
                                        <span class="titleheading"><b>Qty</b></span>
                                    </td>
                                    <td align="center" valign="top" width="5%">
                                        <span class="titleheading"><b>Cons</b></span>
                                    </td>
                                     <td align="center" valign="top" width="5%">
                                        <span class="titleheading"><b>UOM</b></span>
                                    </td>
                                    <td align="center" valign="top" width="5%">
                                        <span class="titleheading"><b>Lot No</b></span>
                                    </td> 
                                     <td align="center" valign="top" width="5%">
                                        <span class="titleheading"><b>Planning For Date</b></span>
                                    </td>
                                     
                                     <td align="center" valign="top" width="5%">
                                        <span class="titleheading"><b>Remarks</b></span>
                                    </td>
                                    <td align="left" valign="top" width="15%">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                  <td align="center" valign="top">
                                 <asp:TextBox ID="txtTRNNo" CssClass="TextBox TextBoxDisplay SmallFont" Width="700%"
                            ReadOnly="true" onkeyup="javascript:this.value = this.value.toUpperCase();" 
                            runat="server"></asp:TextBox>
                             </td>
                                 <td align="center" valign="top">
                                 <%--<asp:DropDownList ID="ddlMachine" runat="server" AppendDataBoundItems="True" CssClass="SmallFont "
                                            TabIndex="16" Width="100px">
                                        </asp:DropDownList>--%>
                                        
                                        <cc2:ComboBox ID="ddlMachine" runat="server"  CssClass="SmallFont"
                                    EmptyText="select..." EnableLoadOnDemand="true" EnableVirtualScrolling="true" TabIndex="11"
                                    Height="200px" MenuWidth="200px" OnLoadingItems="ddlMacCode_LoadingItems" 
                                    Width="99%">
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
                                   <%-- <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>--%>
                                </cc2:ComboBox>
                                         </td>
                                    <td align="left" valign="top">
                                        <asp:TextBox ID="txtQty" CssClass="SmallFont TextBoxNo" Width="95%" runat="server"
                                          ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtQty"
                                            Display="Dynamic" ErrorMessage="Please Enter QTY" SetFocusOnError="True" ValidationGroup="YM"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtQty"
                                            Display="None" ErrorMessage="Please Enter QTY in Numeric &amp; Precision Should be 9 and Scale 2   "
                                            MaximumValue="999999999.9999" MinimumValue="0" Type="Double" ValidationGroup="YM"></asp:RangeValidator>
                                    </td>
                                    
                                     <td align="left" valign="top">
                                        <asp:TextBox ID="txtCons" CssClass="SmallFont TextBoxNo" Width="95%" runat="server"
                                          ></asp:TextBox>
                                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtQty"
                                            Display="Dynamic" ErrorMessage="Please Enter QTY" SetFocusOnError="True" ValidationGroup="YM"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtQty"
                                            Display="None" ErrorMessage="Please Enter QTY in Numeric &amp; Precision Should be 9 and Scale 2   "
                                            MaximumValue="999999999.9999" MinimumValue="0" Type="Double" ValidationGroup="YM"></asp:RangeValidator>--%>
                                    </td>
                                  <td align="left" valign="top">
                                 <asp:DropDownList ID="ddUOM" runat="server" AppendDataBoundItems="True" CssClass="SmallFont "
                                            TabIndex="16" Width="50px">
                                        </asp:DropDownList>
                                         </td>
                                         
                                            <td align="left" valign="top">
                                        <asp:TextBox ID="txtLotNo" CssClass="SmallFont TextBoxNo" Width="95%" runat="server"
                                          ></asp:TextBox>
                                       
                                    </td>
                                    
                                     <td align="left" valign="top">
                                         <asp:TextBox ID="txtPlanningDate" runat="server" TabIndex="2" ValidationGroup="M1" Width="98%"
                                    CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                                           </td>
                                          <td align="left" valign="top">
                                          <asp:TextBox ID="txtRemarks" CssClass="SmallFont TextBoxNo" Width="80%" runat="server"
                                          ></asp:TextBox>
                                           </td>
                                          
                                          
                                         
                                    <td align="left" valign="top">
                                        <asp:Button ID="BtnBOMSave" runat="server" OnClick="BtnBOMSave_Click" Text="Add"
                                            ValidationGroup="YM" />
                                        <asp:Button ID="BtnBOMCancel" runat="server" OnClick="BtnBOMCancel_Click" Text="Cancel" />
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td align="left" colspan="8" valign="top">
                                        <asp:GridView ID="grdsub_trn" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                            Font-Bold="False" OnRowCommand="grdSub_trnArticleDetail_RowCommand" ShowFooter="false"
                                            Width="100%">
                                            <Columns>
                                            
                                             
                                            
                                                <asp:TemplateField HeaderText="Sr&nbsp;No." ItemStyle-VerticalAlign="top" ItemStyle-Width="25px"
                                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtSubTrnUNIQUE_ID" CssClass="SmallFont LabelNo" runat="server" Text='<%# Bind("SR_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Trn No" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTRNNo" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TRN_NUMB") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Order No" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrder_NUMB" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("ORDER_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Mahine No" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMachineNo" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("MACHINE_CODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               
                                               
                                                <asp:TemplateField HeaderText="QTY" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtQTY" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("QTY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                 
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Cons" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtCons" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("CONS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                 
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="UOM" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtUOM" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("UOM") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                
                                                 <asp:TemplateField HeaderText="Lot No" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtLotNo" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("GREY_LOT_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Planning For Date" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtPlanningDate" runat="server"  CssClass="LabelNo SmallFont" Text='<%# Bind("PLANNING_DATE","{0:dd/MM/yyyy}") %>'  ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                 <asp:TemplateField HeaderText="Remarks" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtRemarks" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("REMARKS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                    <asp:Button ID="lnkBOMEdit" class="SmallFont" runat="server" CommandArgument='<%# Eval("SR_NO") %>'
                                                            CommandName="BOMEdit" TabIndex="12" Text="Edit" />
                                                        <asp:Button ID="lnkBOMDelete" class="SmallFont" runat="server" CommandArgument='<%# Eval("SR_NO") %>'
                                                            CommandName="BOMDelete" OnClientClick="return confirm('Are you Sure want to delete this BOM Detail?');"
                                                            TabIndex="12" Text="Delete" />
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
                            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Save"
                                ValidationGroup="M1" CssClass="SmallFont" Width="60px"/>
                            <asp:Button ID="btnClose" runat="server" onclick="btnClose_Click" 
                                Text="Close" CssClass="SmallFont" Width="60px" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="td" valign="top">
                            <asp:CheckBox ID="CheckBox1" runat="server" CssClass="SmallFont" />
                            <asp:Button ID="btnupdate" runat="server" Text="Update Flag" 
                                CssClass="SmallFont" onclick="btnupdate_Click" Width="70px" />
                        </td>
                    </tr>
                </table>
                
                 <cc1:CalendarExtender ID="cePlanninfDate" runat="server" TargetControlID="txtPlanningDate"
            Format="dd/MM/yyyy">
        </cc1:CalendarExtender>
         <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
            ShowSummary="False" ValidationGroup="M1" />
                
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
