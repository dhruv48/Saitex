<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Prod_Plan_Stock_Details.aspx.cs" Inherits="Module_Prod_plan_Pages_Prod_Plan_Stock_Details" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Yarn Dyeing Customer Request Stock Details</title>
 <link href="../../../StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />

    
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
                            <span class="titleheading">Stock Details</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdCenter td SmallFont">
                            
                            
                            <asp:Label ID="Label1" runat="server" Text="Quality Code  =" Font-Size="Small" Visible="false"></asp:Label>
                            <asp:Label ID="lblQualityCode" runat="server" Font-Size="Small" Visible="false"></asp:Label> 
                            &nbsp; &nbsp;
                            <asp:Label ID="Label2" runat="server" Text=" Shade Family =" Font-Size="Small" Visible="false"></asp:Label>                      
                            <asp:Label ID="txtShadeFamily" runat="server" Font-Size="Small" Visible="false"></asp:Label> 
                            &nbsp; &nbsp;
                            <asp:Label ID="Label3" runat="server" Text=" Shade =" Font-Size="Small" Visible="false"></asp:Label> 
                            <asp:Label ID="txtShadeCode" runat="server" Font-Size="Small" Visible="false"></asp:Label>            
                             
                            </td>
                    </tr>
                    
                    <tr>
                      <td class="tdCenter td SmallFont">
                             <table>
                              <tr>
                                 <td colspan="2"> 
                                    <asp:Label ID="Label4" runat="server" Text="Stock:" Font-Size="Small"></asp:Label>
                                 </td>
                                 <td> <asp:Label ID="txtStockReal" runat="server" Font-Size="Small"></asp:Label> 
                                    &nbsp; &nbsp;
                                 </td>
                                 <td colspan="2"> 
                                    <asp:Label ID="Label5" runat="server" Text="Planned Quantity:" Font-Size="Small"></asp:Label>
                                  </td>
                                  <td>  <asp:Label ID="txtPlannedQty" runat="server" Font-Size="Small"></asp:Label> 
                                    &nbsp; &nbsp;
                                  </td>
                                  <td colspan="2"> 
                                    <asp:Label ID="Label6" runat="server" Text=" Available Stock:" Font-Size="Small"></asp:Label>
                                   </td>
                                    <td><asp:Label ID="txtAvlStock" runat="server" Font-Size="Small"></asp:Label>
                               </td>
                              </tr>
                            </table>
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
                       
                                <tr>
                                    <td align="left" colspan="9" valign="top">
                                       <%-- <asp:GridView ID="grdsub_trn" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                            Font-Bold="False" OnRowCommand="grdSub_trnArticleDetail_RowCommand" ShowFooter="True"
                                            Width="98%">--%>
                                            
                                            
                                          <%--  <asp:GridView ID="grdsub_trn" runat="server" AllowSorting="True" AutoGenerateColumns="true"
                                            Font-Bold="False"  ShowFooter="True"
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
                                                      
                                               
                                               
                                            </Columns>
                                            <RowStyle CssClass="RowStyle " />
                                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                                            <AlternatingRowStyle CssClass="AltRowStyle" />
                                            <PagerStyle CssClass="PagerStyle" />
                                            <HeaderStyle BackColor="#336699" CssClass="SmallFont" ForeColor="White" />
                                        </asp:GridView>--%>
                                        
                                        
                                        
                                        
                                        
                                        
                                        <asp:GridView ID="grdPOTRN" runat="server" AutoGenerateColumns="False" Width="700px"
                                            CssClass="SmallFont">
                                            <Columns>
                                                <asp:BoundField DataField="YARN_CODE" HeaderText="Yarn Code">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="YARN_DESC" HeaderText="Yarn DESC">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LOT_NO" HeaderText="Grey Lot No">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="STOCK" HeaderText="Stock">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LOCATION" HeaderText="Location">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="STORE" HeaderText="Store">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PO_NUMB" HeaderText="Po No">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PO_ORDER_QTY" HeaderText="Po Order Qty">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PO_RECIVE_QTY" HeaderText="Recive Qty">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="BALANCE" HeaderText="Balance Qty">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                            </Columns>
                                            <RowStyle CssClass="SmallFont" Width="98%" />
                                            <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="BlueViolet" />
                                        </asp:GridView>
                                        
                                        
                                        
                                    </td>
                                </tr>
                            </table>
                             
                        </td>
                    </tr>
                    
                </table>
          </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>