<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MaterailStockQueryForm.ascx.cs" Inherits="Module_Inventory_Controls_MaterailStockQueryForm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
    <script type="text/javascript">
function NewWindow() {
document.forms[0].target = "_blank";
}
</script>
<cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TxtFromDate"   Mask="99/99/9999" MessageValidatorTip="true"    OnFocusCssClass="MaskedEditFocus" 
    OnInvalidCssClass="MaskedEditError"  MaskType="Date"   InputDirection="LeftToRight" 
    ErrorTooltipEnabled="True"></cc1:MaskedEditExtender>
<cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="TxtToDate"   Mask="99/99/9999" MessageValidatorTip="true"    OnFocusCssClass="MaskedEditFocus" 
    OnInvalidCssClass="MaskedEditError"  MaskType="Date"   InputDirection="LeftToRight" 
     ErrorTooltipEnabled="True"></cc1:MaskedEditExtender>
<table class="tContentArial td " width="100%">
     <tr>
                        <td valign="top" colspan="7" align="left" class="tContentArial td ">
                            <table cellspacing="0" cellpadding="0" align="left" >
                                <tbody>
                                    <tr>                                                               
                                        <td valign="top" align="center">
                                            <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                                                ToolTip="Print" Height="41" Width="48" OnClientClick="NewWindow();"  onclick="imgbtnPrint_Click"></asp:ImageButton>
                                        </td>                                       
                                        <td valign="top" align="center">
                                            <asp:ImageButton ID="imgbtnExit"  runat="server" ImageUrl="~/CommonImages/link_exit.png"
                                                ToolTip="Exit" Height="41" Width="48" onclick="imgbtnExit_Click"></asp:ImageButton>
                                        </td>                                       
                                    </tr>
                                </tbody>
                            </table>
                        </td>
             </tr>
                    <tr>
                        <td colspan="7" align="center" valign="top" class="tRowColorAdmin td">
                            <span class="titleheading">Material Stock Statement Reports</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdRight">Financial Year:</td><td  class="tdLeft">
                        
                        <asp:DropDownList ID="DDLFinancialYear" Width="151px" runat="server" AutoPostBack="True" CssClass="SmallFont TextBox UpperCase" onselectedindexchanged="DDLFinancialYear_SelectedIndexChanged"></asp:DropDownList></td>
                       <td class="tdRight">From date:</td><td class="tdLeft">
                                <asp:TextBox ID="TxtFromDate" Width="150px" runat="server" CssClass="SmallFont TextBox UpperCase" AutoPostBack="True"  ontextchanged="TxtFromDate_TextChanged"></asp:TextBox></td>
                            <td class="tdLeft">
                                &nbsp;</td>
                            <td class="tdRight">To Date:</td><td class="tdLeft"> <asp:TextBox ID="TxtToDate" CssClass="SmallFont TextBox UpperCase" Width="150px" runat="server" AutoPostBack="True" ontextchanged="TxtToDate_TextChanged"></asp:TextBox></td>
                    </tr>
                    <tr>
                        
                        <td class="tdRight">Item:</td><td class="tdLeft">
                         <cc2:ComboBox ID="txtICODE" runat="server" CssClass="SmallFont" EmptyText="select..."
                            AutoPostBack="True" EnableLoadOnDemand="true" MenuWidth="580px" Width="151px"
                            OnLoadingItems="cmbPOITEM_LoadingItems" 
                            Height="200px">
                            <HeaderTemplate>
                                <div class="header c2">
                                    Item Code</div>
                                <div class="header c4">
                                    Description</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("ITEM_CODE") %>' />
                                </div>
                                <div class="item c4">
                                    <asp:Literal runat="server" ID="Container3" Text='<%# Eval("ITEM_DESC") %>' />
                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                        </td>
                        <td class="tdRight">Item Categories:</td><td class="tdLeft">
                        <cc2:ComboBox runat="server" ID="ddlItemCategory" CssClass="SmallFont" 
                                                    MenuWidth="250px" Width="150px"
                                                    Height="180px" EmptyText="Select Item Category..." EnableLoadOnDemand="true"
                                                    OnLoadingItems="ddlItemCategory_LoadingItems" AutoPostBack="True"  TabIndex="2" /></td>
                                                    
                        <td class="tdLeft">
                            &nbsp;</td>
                                                    
                        <td class="tdRight">Item Make:</td>
                            
                        <td class="tdLeft">
                            <asp:DropDownList ID="DDLItemMake" Width="151px" runat="server"  CssClass="SmallFont TextBox UpperCase" ></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                         <td class="tdRight">Item Rac:</td><td class="tdLeft">
                            <asp:DropDownList ID="DDLItemRac" Width="151px" runat="server"  CssClass="SmallFont TextBox UpperCase" ></asp:DropDownList>
                         </td>
                            <td class="tdRight">Associate Item:</td><td class="tdLeft">
                                <asp:DropDownList ID="DDLAssociate" Width="151px" runat="server"  CssClass="SmallFont TextBox UpperCase" ></asp:DropDownList>
                            </td>
                         <td>
                         </td>
                         <td>
                         </td>
                         <td class="tdLeft">
                             <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Find" 
                                 Width="149px" />
                         </td>
                    </tr>   
                    <tr>
             <td colspan="7" width="100%" class="TdBackVir">
                            <b>Total Records : &nbsp;&nbsp;</b><asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
                        </td></tr>
                    <tr>
              <td colspan="7">        
  <asp:Panel ID="pnlShowHover" runat="server" Width="950px" 
                                 ScrollBars="Auto">
   
     
                                   
      <asp:GridView ID="GridItemStocks" runat="server">
      
      </asp:GridView>
   
     
                                   
      <br />
      <br />
   
     
                                   
   </asp:Panel>
   </td>
                    </tr> 
</table>
</ContentTemplate>
</asp:UpdatePanel>
<p>
    &nbsp;</p>
<p>
    &nbsp;</p>
<p>
    &nbsp;</p>
<p>
    &nbsp;</p>
