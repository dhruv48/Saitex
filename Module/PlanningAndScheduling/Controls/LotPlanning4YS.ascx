<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LotPlanning4YS.ascx.cs" Inherits="Module_PlanningAndScheduling_Controls_LotPlanning4YS" %>
<%@ Register Assembly="AjaxControlToolkit"  Namespace="AjaxControlToolkit" TagPrefix="cc1"  %>
<link href="../../../StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
<style type="text/css">
.item
{
    position:relative ! important;
    display: -moz-inline-stack;
}
.header
{
 margin-left:2px;    
}
.c1
{
    width:60px;
}
.c2
{
    margin-left:4px;
    width:80px;
}
.c3
{
    margin-left:4px;
    width:160px;
}
 .c4
 {
     margin-left:4px;
     width:200px;
 }
 .c5
 {
    margin-left:4px;
    widows:150px;
 }
 .c6
 {
    margin-left:4px;
    width:80px;
 }
 .c7
 {
     margin-left:4px;
     width:300px;
 }
    .style2
    {
        width: 498px;
    }
    
</style>
<%--<asp:UpdatePanel runat="server" ID="UpdatePanel1">
<ContentTemplate>--%>
<table class="td tContentArial" width="95%">
<tr>
<td class="td" width="95%">
  <table class="td tContentArial">
<tr>
<td id="tdUpdate" runat="server">
<asp:ImageButton ID="imgbtnUpdate" runat="server" 
        ImageUrl="~/CommonImages/edit1.jpg" ToolTip="Update" ValidationGroup="M1" 
        onclick="imgbtnUpdate_Click" />

</td>
<td id="tdExit" runat="server">
<asp:ImageButton ID="imgbtnExit" runat="server" 
        ImageUrl="~/CommonImages/link_exit.png" ToolTip="Exit" 
        onclick="imgbtnExit_Click" />
</td>
<td>
<asp:ImageButton ID="imgbtnClear" runat="server" 
        ImageUrl="~/CommonImages/clear.jpg" ToolTip="Clear" 
        onclick="imgbtnClear_Click" />
        </td>
        <td>
        
 <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                           ToolTip="Print" onclick="imgbtnPrint_Click" />
        
</td>

</tr>
</table>
</td>
</tr>
<tr>
<td align="center" class="TableHeader" colspan="3">
<table  width="100%">
<tr >
<td align="center" style="background-color:#336799; color:white;">
<b class="titleheading" >Lot Planning For Yarn Texturising</b>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td align="left">
<span class="Mode">You are in &nbsp;<asp:Label ID="lblModel" runat="server"></asp:Label>&nbsp;Mode</span>
</td>
</tr>
<tr>
<td align="center">
<asp:Label ID="LblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
<asp:Label ID="LblError" runat="server" CssClass="UserError"></asp:Label>

</td>
</tr>
<tr>
<td class="td">
<table width="100%" style="font-weight:bold">
<tr>
<td align="right" valign="top" class="tdRight" width="12%">
<asp:Label ID="lblProducttype" runat="server" Text="PRODUCT:" CssClass="SmallFont"></asp:Label>
</td>
<td class="tdLeft" width="12%">
<asp:DropDownList ID="ddlProductType" CssClass="SmallFont TextBox UpperCase BoldFont" 
        runat="server" Width="98%">
</asp:DropDownList>
</td>
<td class="tdRight" width="12%" valign="top">
<asp:Label ID="lblOrderCategory" runat="server" Text="Order Category:" CssClass="SmallFont"></asp:Label>
</td>
<td class="tdLeft" width="12%">
<asp:DropDownList ID="ddlOrderCategory" runat="server"  CssClass="SmallFont BoldFont"
                                    Width="98%" >
                                    <asp:ListItem>DIRECT SALE</asp:ListItem>
                                    <asp:ListItem>INHOUSE</asp:ListItem>
                                </asp:DropDownList>
</td>
</tr>
<tr>
<td class="tdRight" width="12%">
<asp:Label ID="lblOrderType"  runat="server" Text="Order Type:" CssClass="SmallFont"></asp:Label>
</td>
<td class="tdLeft" width="12%">
<asp:DropDownList ID="ddlordertype" runat="server"  CssClass="SmallFont BoldFont UPPERCASE"
                                    Width="99%" >
                                </asp:DropDownList>
</td>
<td class="tdRight" width="12%">
<asp:Label ID="lblStatus" runat="server" Text="STATUS:" CssClass="SmallFont"></asp:Label>
</td>
<td>
<asp:DropDownList ID="ddlStatus" runat="server" CssClass="SmallFont TextBox UpperCase"
                                    TabIndex="3" Width="120px">
                                    <asp:ListItem>ALL</asp:ListItem>
                                    <asp:ListItem>Un-Approved</asp:ListItem>
                                    <asp:ListItem>Approved</asp:ListItem>
                                </asp:DropDownList>
</td>
</tr>
<tr>
<td colspan="4">
<asp:Button ID="btnShow" Text="Show" runat="server" onclick="btnShow_Click" />
</td>

</tr>
<tr>
<td colspan="4"> 

</td>
</tr>

</table>
</td>
</tr>
<tr>
<td class="td" width="100%">
<b>Total Records &nbsp; &nbsp;: &nbsp; &nbsp; <asp:Label ID="lblTotalRecords" runat="server"></asp:Label>

</b>
</td>
</tr>
<tr>
<td align="left" width="100%" class="td">
<asp:Panel ID="Panel1" runat="server" Width="100%">
<span class="titleheading">
<b>
<asp:GridView ID="grdLotPlanning" runat="server" AllowSorting="true" 
        AllowPaging="true" AutoGenerateColumns="false" PageSize="10" CellPadding="4" 
        Font-Size="10px" ForeColor="#333333" GridLines="Both" Width="885px" 
        onpageindexchanging="grdLotPlanning_PageIndexChanging" 
        onrowcommand="grdLotPlanning_RowCommand" 
        onrowdatabound="grdLotPlanning_RowDataBound">
<FooterStyle BackColor="#CCCCCC" />
<RowStyle BackColor="#EFF3FB" />
<Columns>
<asp:TemplateField HeaderText="Comp Code" ItemStyle-HorizontalAlign="Left" Visible="false">
 <ItemTemplate>
 <asp:Label ID="lblComp_Code" runat="server" Text='<%# Eval("COMP_CODE") %>'></asp:Label>
 </ItemTemplate>
     <ItemStyle HorizontalAlign="Left" />
 </asp:TemplateField>
 <asp:TemplateField HeaderText="Branch Code" ItemStyle-HorizontalAlign="Left" Visible="false">
 <ItemTemplate>
 <asp:Label ID="lbl_Branch_Code" runat="server" Text='<%# Eval("BRANCH_CODE") %>'></asp:Label>
 </ItemTemplate>
     <ItemStyle HorizontalAlign="Left" />
 </asp:TemplateField>
 <asp:TemplateField HeaderText="Business Type" ItemStyle-HorizontalAlign="Left" Visible="false">
 <ItemTemplate>
 <asp:Label ID="lblBusiness_Type" runat="server" Text='<%# Eval("BUSINESS_TYPE") %>'></asp:Label>
 </ItemTemplate>
     <ItemStyle HorizontalAlign="Left" />
 </asp:TemplateField> 
 <asp:TemplateField HeaderText="Product Type" ItemStyle-HorizontalAlign="Left" Visible="false">
 <ItemTemplate>
 <asp:Label ID="lblProduct_type" runat="server" Text='<%# Eval("PRODUCT_TYPE") %>'></asp:Label>
 </ItemTemplate>
     <ItemStyle HorizontalAlign="Left" />
 </asp:TemplateField>
 <asp:TemplateField HeaderText="Order Cat" ItemStyle-HorizontalAlign="Left" Visible="false">
 <ItemTemplate>
 <asp:Label ID="lblOrder_Cat" runat="server" Text='<%# Eval("ORDER_CAT") %>'></asp:Label>
 </ItemTemplate>
     <ItemStyle HorizontalAlign="Left" />
 </asp:TemplateField>
 <asp:TemplateField HeaderText="Order No" ItemStyle-HorizontalAlign="Left">
 <ItemTemplate>
 <asp:Label ID="lblOrder_No" runat="server" Text='<%# Eval("ORDER_NO") %>'></asp:Label>
 </ItemTemplate>
     <ItemStyle HorizontalAlign="Left" />
 </asp:TemplateField>
 <asp:TemplateField HeaderText="Order Type" ItemStyle-HorizontalAlign="Left" Visible="false">
 <ItemTemplate>
 <asp:Label ID="lblOrder_Type" runat="server" Text='<%# Eval("ORDER_TYPE") %>'></asp:Label>
 </ItemTemplate>
     <ItemStyle HorizontalAlign="Left" />
 </asp:TemplateField>
 <asp:TemplateField HeaderText="Party Code" ItemStyle-HorizontalAlign="Left">
 <ItemTemplate>
 <asp:Label ID="lblParty_Code" runat="server" Text='<%# Eval("PRTY_CODE") %>'></asp:Label>
 </ItemTemplate>
     <ItemStyle HorizontalAlign="Left" />
 </asp:TemplateField>
 <asp:TemplateField HeaderText="Party Name" ItemStyle-HorizontalAlign="Left">
 <ItemTemplate>
 <asp:Label ID="lblParty_Name" runat="server" Text='<%# Eval("PRTY_NAME") %>'></asp:Label>
 </ItemTemplate>
     <ItemStyle HorizontalAlign="Left" />
 </asp:TemplateField>
 <asp:TemplateField HeaderText="PI Type" ItemStyle-HorizontalAlign="Left" Visible="false">
 <ItemTemplate>
 <asp:Label ID="lblPi_Type" runat="server" Text='<%# Eval("PI_TYPE") %>'></asp:Label>
 </ItemTemplate>
     <ItemStyle HorizontalAlign="Left" />
 </asp:TemplateField>
 <asp:TemplateField HeaderText="PA No" ItemStyle-HorizontalAlign="Left">
 <ItemTemplate>
 <asp:Label ID="lblPA_NO" runat="server" Text='<%# Eval("PI_NO") %>'></asp:Label>
 </ItemTemplate>
     <ItemStyle HorizontalAlign="Left" />
 </asp:TemplateField>
 <asp:TemplateField HeaderText="Artical Code" ItemStyle-HorizontalAlign="Left">
 <ItemTemplate>
 <asp:Label ID="lblArtical_Code" runat="server" Text='<%# Eval("ARTICAL_CODE") %>'></asp:Label>
 </ItemTemplate>
     <ItemStyle HorizontalAlign="Left" />
 </asp:TemplateField>
 <asp:TemplateField HeaderText="Artical Desc" ItemStyle-HorizontalAlign="Left">
 <ItemTemplate>
 <asp:Label ID="lblArtical_Desc" runat="server" Text='<%# Eval("ARTICAL_DESC") %>'></asp:Label>
 </ItemTemplate>
     <ItemStyle HorizontalAlign="Left" />
 </asp:TemplateField>
 <asp:TemplateField HeaderText="UOM" ItemStyle-HorizontalAlign="Left" Visible="false">
 <ItemTemplate>
 <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UOM") %>'></asp:Label>
 </ItemTemplate>
     <ItemStyle HorizontalAlign="Left" />
 </asp:TemplateField>
 <asp:TemplateField HeaderText="Shade Code" ItemStyle-HorizontalAlign="Left">
 <ItemTemplate>
 <asp:Label ID="lblShade_Code" runat="server" Text='<%# Eval("SHADE_CODE") %>'></asp:Label>
 </ItemTemplate>
     <ItemStyle HorizontalAlign="Left" />
 </asp:TemplateField>
 <asp:TemplateField HeaderText="ORD QTY" ItemStyle-HorizontalAlign="Left">
 <ItemTemplate>
 <asp:Label ID="lblOrder_QTY" runat="server" Text='<%# Eval("ORD_QTY") %>'></asp:Label>
 </ItemTemplate>
     <ItemStyle HorizontalAlign="Left" />
 </asp:TemplateField>
 <asp:TemplateField HeaderText="Bom Flag" ItemStyle-HorizontalAlign="Left" Visible="false">
 <ItemTemplate>
 <asp:Label ID="lblBom_Flag" runat="server" Text='<%# Eval("BOM_FLAG") %>'></asp:Label>
 </ItemTemplate>
     <ItemStyle HorizontalAlign="Left" />
 </asp:TemplateField>
 <asp:TemplateField HeaderText="Cost Price Flag" ItemStyle-HorizontalAlign="Left" Visible="false">
 <ItemTemplate>
 <asp:Label ID="lblCost_Price_Flag" runat="server" Text='<%# Eval("COST_PRICE_FLAG") %>'></asp:Label>
 </ItemTemplate>
     <ItemStyle HorizontalAlign="Left" />
 </asp:TemplateField>
 <asp:TemplateField HeaderText="Process Route Flag" ItemStyle-HorizontalAlign="Left" Visible="false">
 <ItemTemplate>
 <asp:Label ID="lblProcess_Route_Flag" runat="server" Text='<%# Eval("PROCESS_ROUTE_FLAG") %>'></asp:Label>
 </ItemTemplate>
     <ItemStyle HorizontalAlign="Left" />
 </asp:TemplateField>
 <asp:TemplateField HeaderText="Lot Flag" ItemStyle-HorizontalAlign="Left" Visible="false">
 <ItemTemplate>
 <asp:Label ID="lblLot_Flag" runat="server" Text='<%# Eval("LOT_FLAG") %>'></asp:Label>
 </ItemTemplate>
     <ItemStyle HorizontalAlign="Left" />
 </asp:TemplateField>
 <asp:TemplateField HeaderText = "Final Lot Flag" Visible="false">
 <ItemTemplate>
 <asp:Label ID="lbl_Final_Lot_Flag" runat="server" Text='<%# Eval("FINAL_LOT_CNF_FLAG") %>'></asp:Label>
 </ItemTemplate>
 </asp:TemplateField>
 <asp:TemplateField HeaderText="ViewBOM" ItemStyle-HorizontalAlign="Left">
 <ItemTemplate>
 <asp:LinkButton ID="linkViewBOM" runat="server" CommandName="ViewBOM"
   CssClass="Label SmallFont" Text="ViewBOM"></asp:LinkButton>
 </ItemTemplate>
     <ItemStyle HorizontalAlign="Left" />
 </asp:TemplateField>
 <asp:TemplateField HeaderText="LotDetail" ItemStyle-HorizontalAlign="Left">
 <ItemTemplate>
 <asp:LinkButton ID="linkLotDetail" runat="server" CommandName="LotDetail"
        CssClass="Label SmallFont" Text="LotDetail">
 </asp:LinkButton>
 </ItemTemplate>
     <ItemStyle HorizontalAlign="Left" />
 </asp:TemplateField>
 <asp:TemplateField HeaderText="LotID" ItemStyle-HorizontalAlign="Left">
 <ItemTemplate>
 <%--<asp:TextBox ID="txtLotID" runat="server" CssClass="Label SmallFont" Width="80px" ReadOnly="true"></asp:TextBox>
--%> 
<asp:Label ID = "lblLot_id" runat="server" Text='<%# Eval("LOT_ID") %>'></asp:Label>
</ItemTemplate>
     <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" />
 </asp:TemplateField>
 <asp:TemplateField HeaderText="Lot Date" ItemStyle-HorizontalAlign="Left">
 <ItemTemplate>
 <asp:TextBox ID="txtLotDate" runat="server" CssClass="Label SmallFont"
     Width="65px" Text='<%# Eval("LOT_CNF_DATE") %>'></asp:TextBox>
      <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
         TargetControlID="txtLotDate">
         </cc1:CalendarExtender>
 </ItemTemplate>
     <ItemStyle HorizontalAlign="Left" />
 </asp:TemplateField>
 <asp:TemplateField HeaderText="Confirm" ItemStyle-HorizontalAlign="Left">
 <ItemTemplate>
 <asp:CheckBox ID="CheckBox1" runat="server" Enabled="false" />
 </ItemTemplate>
     <ItemStyle HorizontalAlign="Left" />
 </asp:TemplateField>
 <asp:TemplateField HeaderText="Confirm By" ItemStyle-HorizontalAlign="Left">
 <ItemTemplate>
 <asp:TextBox ID="txtCofBy" runat="server" CssClass="TextBox SmallFont TextBoxDisplay"
     Width="70px" Text='<%# Eval("LOT_CNF_BY") %>' ReadOnly="true"></asp:TextBox>
 </ItemTemplate>
     <ItemStyle HorizontalAlign="Left" />
 </asp:TemplateField>
 <%--<asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Left">
 <ItemTemplate>
 <asp:TextBox ID="txtRemarks" runat="server" CssClass="Label SmallFont"
   Width="100px"></asp:TextBox>
 </ItemTemplate>
 </asp:TemplateField>--%>
 </Columns>
 <PagerStyle BackColor="#336799" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#336799" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />

</asp:GridView>
</b>
</span>

</asp:Panel>

</td>
</tr>
</table>
<%--</ContentTemplate>
</asp:UpdatePanel>
--%>