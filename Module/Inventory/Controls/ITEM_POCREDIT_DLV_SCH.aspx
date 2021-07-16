<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ITEM_POCREDIT_DLV_SCH.aspx.cs" Inherits="Module_Inventory_Controls_ITEM_POCREDIT_DLV_SCH" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Delivery Schedule</title>
    <link rel="stylesheet" type="text/css" href="../../../../StyleSheet/CommonStyle.css" />

    <script language="javascript" type="text/javascript">

        function BindYRNQTY() {
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
            margin-left: 4px;
        }
        .c1
        {
            width: 80px;
        }
        .c2
        {
            margin-left: 4px;
            width: 300px;
        }
        .c3
        {
            width: 200px;
        }
        .c4
        {
            margin-left: 4px;
            width: 300px;
        }
        .c5
        {
            width: 200px;
        }
        .d1
        {
            width: 80px;
        }
        .d2
        {
            margin-left: 4px;
            width: 180px;
        }
        .d3
        {
            margin-left: 4px;
            width: 180px;
        }
        .d4
        {
            margin-left: 4px;
            width: 180px;
        }
    </style>
</head>
<body bgcolor="#afcae4" class="tContentArial">
    <form id="form1" style="background-color: #afcae4" runat="server">
    <div>
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
        </cc1:ToolkitScriptManager>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
            ShowSummary="False" ValidationGroup="YM" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="100%" class="tContentArial td" >
                    <tr>
                        <td align="center" class="td TableHeader" valign="top" >
                            <strong class="titleheading">Delivery Schedule for Item :
                                <asp:Label ID="lblYarnCode" runat="server"></asp:Label>
                                &nbsp;&nbsp;
                                Order NO :
                                <asp:Label ID="lblOrderNo" runat="server"></asp:Label>
                                &nbsp;&nbsp;
                                Order Type:
                                <asp:Label ID="lblOrderType" runat="server"></asp:Label>
                                
                            
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
                                   
                                    <td align="right" valign="top" width="10%">
                                        <span class="titleheading"><b>Qty</b></span>
                                    </td>
                                    <td align="left" valign="top" width="5%">
                                        <span class="titleheading"><b>Delivery&nbsp;Date</b></span>
                                    </td>
                                    <td align="left" valign="top" width="15%">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                   
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
                                        <asp:TextBox ID="txtDeliveryDate" CssClass="SmallFont TextBox" Width="95%" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDeliveryDate"
                                            Display="Dynamic" ErrorMessage="Please Enter Delivery Date" SetFocusOnError="True"
                                            ValidationGroup="YM"></asp:RequiredFieldValidator>
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupPosition="TopLeft"
                                            Format="dd/MM/yyyy" TargetControlID="txtDeliveryDate">
                                        </cc1:CalendarExtender>
                                       
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:Button ID="BtnBOMSave" runat="server" OnClick="BtnBOMSave_Click" Text="Add"
                                            ValidationGroup="YM" />
                                        <asp:Button ID="BtnBOMCancel" runat="server" OnClick="BtnBOMCancel_Click" Text="Cancel" />
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td align="left" colspan="3" valign="top">
                                        <asp:GridView ID="grdsub_trn" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                            Font-Bold="False" OnRowCommand="grdSub_trnArticleDetail_RowCommand" ShowFooter="false"
                                            Width="98%">
                                            <Columns>
                                            
                                             
                                            
                                                <asp:TemplateField HeaderText="Sr&nbsp;No." ItemStyle-VerticalAlign="top" ItemStyle-Width="25px"
                                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtSubTrnUNIQUE_ID" CssClass="SmallFont LabelNo" runat="server" Text='<%# Bind("SR_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Order No" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrder_NUMB" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("PO_NUMB") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                
                                                
                                                <asp:TemplateField HeaderText="Item Code" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblYarncode" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("ITEM_CODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               
                                               
                                                <asp:TemplateField HeaderText="QTY" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtQTY" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("QUANTITY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                 
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delivery&nbsp;Date" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDeliveryDate" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("DELIVERY_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkBOMDelete" runat="server" CommandArgument='<%# Eval("SR_NO") %>'
                                                            CommandName="BOMDelete" OnClientClick="return confirm('Are you Sure want to delete this  Detail?');"
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
