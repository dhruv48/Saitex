<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewCROrderAdj.aspx.cs" Inherits="Module_OrderDevelopment_Pages_ViewCROrderAdj" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>View CR</title>
    <link rel="stylesheet" type="text/css" href="../../../StyleSheet/CommonStyle.css" />

    <script language="javascript" type="text/javascript">

        function BindYRNSPIN_BOM(COST, TextBoxCOST) {
            window.opener.document.forms[0].submit();
            window.close();
        } 
          
    </script>

</head>

<body  width="100%">
    <form id="form1"  runat="server" width="100%">
    <div width="100%"><asp:scriptmanager runat="server"></asp:scriptmanager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="100%" class="tContentArial">
                    <tr>
                        <td align="center" class="td TableHeader" valign="top" width="100%">
                            <strong class="titleheading">View Machine Plan Details Against Production(PA)</strong>
                        </td>
                    </tr>
                    <tr>
                        <td class="td tdLeft" width="100%">
                            <asp:GridView ID="grdViewCR" runat="server"  AutoGenerateColumns="False"
                             CssClass="SmallFont"   Width="98%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Company" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="txtCR_COMP_CODE" CssClass="Label SmallFont" runat="server" Text='<%# Bind("CR_COMP_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Branch" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="txtCR_BRANCH_CODE" CssClass="Label SmallFont" runat="server" Text='<%# Bind("CR_BRANCH_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Year" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                        <ItemTemplate>
                                            <asp:Label ID="txtCR_YEAR" runat="server" CssClass="Label SmallFont" Text='<%# Bind("CR_YEAR") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Machine" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                        <ItemTemplate>
                                            <asp:Label ID="txtMachine" runat="server" CssClass="Label SmallFont" Text='<%# Bind("MACHINE_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Plan&nbsp;Qty" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblADJ_QTY" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("ADJ_QTY") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    
                                    
                                     <asp:TemplateField HeaderText="Plan Date" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                        <ItemTemplate>
                                            <asp:Label ID="txtPlanQty" runat="server" CssClass="Label SmallFont" Text='<%# Bind("PLANNING_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Lot No" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                        <ItemTemplate>
                                            <asp:Label ID="txtgreyLot" runat="server" CssClass="Label SmallFont" Text='<%# Bind("GREY_LOT_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                   
                                    <asp:TemplateField HeaderText="Order&nbsp;Type" Visible="false">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        <ItemTemplate>
                                            <asp:Label ID="txtCR_ORDER_TYPE" runat="server" CssClass="Label SmallFont" Text='<%# Bind("CR_ORDER_TYPE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order&nbsp;Cat" Visible="false">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCR_ORDER_CAT" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("CR_ORDER_CAT") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Product&nbsp;Type" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCR_PRODUCT_TYPE" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("CR_PRODUCT_TYPE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Business&nbsp;Type" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCR_BUSINESS_TYPE" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("CR_BUSINESS_TYPE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                 
                                    <asp:TemplateField HeaderText="SO&nbsp;No" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCR_ST_ORDER_NO" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("CR_ST_ORDER_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quality Code" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCR_ST_ARTICLE_NO" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("CR_ST_ARTICLE_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Substrate" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCR_ST_SUBSTRATE" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("CR_ST_SUBSTRATE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Count" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCR_ST_COUNT" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("CR_ST_COUNT") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Shade&nbsp;Family&nbsp;">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCR_ST_SHADE_FAMILY_CODE" runat="server" CssClass="LabelNo SmallFont"
                                                Text='<%# Bind("CR_ST_SHADE_FAMILY_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Shade&nbsp;No" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCR_ST_SHADE_CODE" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("CR_ST_SHADE_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                </Columns>
                             <HeaderStyle BackColor="#336699" CssClass="SmallFont titleheadingGrid" />
                              <RowStyle CssClass="SmallFont" />
                               
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="td" valign="top" width="100%">
                            <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click" Text="Close" ValidationGroup="M1"
                                CssClass="SmallFont" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
