<%@ Control Language="C#" AutoEventWireup="true" CodeFile="IssueRequisitionApproval.ascx.cs"
    Inherits="Module_OrderDevelopment_Controls_IssueRequisitionApproval" %>
<table class="td  tContentArial" >
    <tr>
        <td class="td  tContentArial">
            <table>
                <tr>
                    <td id="tdPrint" runat="server">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" Width="48" />
                    </td>
                    <td id="tdHelp" runat="server">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                            ToolTip="Help" Width="48" />
                    </td>
                    <td id="tdExit" runat="server">
                        <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                            ToolTip="Exit" Width="48" onclick="imgbtnExit_Click" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr class="TableHeader">
        <td align="center" valign="top" class="td">
            <span class="titleheading">Issue Requisition Approval</span>
        </td>
    </tr>
    <tr>
        <td colspan="8">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>
    <tr>
        <td>
           <asp:GridView ID="grdIssueReqApproval" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CssClass="SmallFont" Font-Bold="False" Width="100%" 
                CellPadding="3" BackColor="White" BorderColor="#999999" BorderStyle="None"
                BorderWidth="1px">
                <Columns>
                    <asp:TemplateField HeaderText="Branch" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblBranch" runat="server" Text='<%# Bind("BRANCH_CODE") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Issue Req No." HeaderStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblIssueReqNo" runat="server" Text='<%# Bind("ISSUE_REQ_NO") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Issue Req Date" HeaderStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:Label ID="lblIssueReqDate" runat="server" CssClass="Label SmallFont" Text='<%# Bind("ISSUE_REQ_DATE","{0:dd-MM-yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Product Type" HeaderStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:Label ID="lblProductType" runat="server" CssClass="Label SmallFont" Text='<%# Bind("PRODUCT_TYPE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PA No." HeaderStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:Label ID="lblPaNo" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("PA_NO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Lot No." HeaderStyle-HorizontalAlign="Right">
                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:Label ID="lblLOT_NUMBER" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("LOT_NUMBER") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Item Type" HeaderStyle-HorizontalAlign="Right">
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:Label ID="lblITEMTYPE" runat="server" CssClass="LabelNo SmallFont"  Text='<%# Bind("ITEM_TYPE") %>'></asp:Label>
                             
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Article Code" HeaderStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:Label ID="lblArticle" runat="server" CssClass="LabelNo SmallFont"
                                Text='<%# Bind("ARTICLE_CODE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Shad Code">
                        <ItemTemplate>
                            <asp:Label ID="lblSHADECODE" runat="server" CssClass="LabelNo SmallFont"
                                Text='<%# Bind("SHADE_CODE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UOM">                     
                        <ItemTemplate>
                            <asp:Label ID="lblUOM" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("UOM") %>'></asp:Label>
                        </ItemTemplate>                       
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty" HeaderStyle-HorizontalAlign="Right">                     
                        <ItemTemplate>
                            <asp:Label ID="lblQTY" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("QTY") %>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    </asp:TemplateField>                   
                    <asp:TemplateField HeaderText="Approval" HeaderStyle-HorizontalAlign="Left">                     
                           <ItemTemplate>
                               <asp:CheckBox ID="chkApproved" runat="server" AutoPostBack="True" 
                                   oncheckedchanged="chkApproved_CheckedChanged" Text="Approved" />
                               <asp:CheckBox ID="ChkUnApproved" runat="server" AutoPostBack="True" 
                                   oncheckedchanged="ChkUnApproved_CheckedChanged" Text="Reject" />
                           </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateField>                       
                    <asp:TemplateField HeaderText="Stock Detail" HeaderStyle-HorizontalAlign="Right">                     
                        <ItemTemplate>
                            <asp:Button ID="btnView" runat="server" Text="View" CommandName ="click" 
                                onclick="btnView_Click" />
                        </ItemTemplate>                       
                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>

                    </asp:TemplateField>
                </Columns>
                  <RowStyle CssClass="RowStyle SmallFont" BackColor="#EEEEEE" ForeColor="Black" />
              <SelectedRowStyle CssClass="SelectedRowStyle" BackColor="#008A8C" Font-Bold="True"
                    ForeColor="White" />
              <AlternatingRowStyle CssClass="AltRowStyle" BackColor="#DCDCDC" />
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <PagerStyle CssClass="PagerStyle" BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#000084" CssClass="HeaderStyle " ForeColor="White" Font-Bold="True" />
            </asp:GridView>    
        </td>
    </tr>
    <tr><td>
     <asp:Panel ID="pnlShowHover" runat="server"  ScrollBars="Horizontal"
                                    Width="945px">                                
 <asp:GridView ID="GridStockDetail" runat="server" AllowPaging="True" AllowSorting="True"
                                    AutoGenerateColumns="False" CellPadding="3" 
             BorderStyle="Ridge" CssClass="smallfont"
                                    EmptyDataText="No Record Found" Font-Size="X-Small" 
             ForeColor="#333333" PagerStyle-HorizontalAlign="Left"
                                    PageSize="10" Width="250%" 
             onpageindexchanging="GridStockDetail_PageIndexChanging" onsorting="GridStockDetail_Sorting" 
         >
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                                    <Columns>
                                        <asp:BoundField DataField="YEAR" HeaderText="Year" />                                                                               
                                        <asp:BoundField DataField="BRANCH_NAME" HeaderText="BRANCH NAME" />                                                                                 
                                        <asp:BoundField DataField="TRN_TYPE" HeaderText="Trn Type" />
                                        <asp:BoundField DataField="TRN_NUMB" HeaderText="Trn No"   SortExpression ="TRN_NUMB"/>
                                        <asp:BoundField DataField="TRN_DATE" HeaderText="Trn Date"  DataFormatString="{0:dd-MM-yyyy}" />
                                        <asp:BoundField DataField="YARN_CODE" HeaderText="Yarn Code"    />
                                        <asp:BoundField DataField="YARN_CAT" HeaderText="Yarn Catagory" />
                                        <asp:BoundField DataField="YARN_TYPE" HeaderText="Yarn Type" />
                                        <asp:BoundField DataField="YARN_DESC" HeaderText="Yarn Desc" />
                                        <asp:BoundField DataField="SHADE_CODE" HeaderText="Shade Code" />
                                        <asp:BoundField DataField="LOT_NO" HeaderText="Lot Number" />
                                        <asp:BoundField DataField="GRADE" HeaderText="Grade" />
                                        <asp:BoundField DataField="BALQTY" HeaderText="Balance Qty" />
                                        <asp:BoundField DataField="FINAL_RATE" HeaderText="Final Rate" />
                                        <asp:BoundField DataField="BALVAL" HeaderText="Balance Value" />
                                        <asp:BoundField DataField="PRTY_NAME" HeaderText="Party Name" />
                                        <asp:BoundField DataField="PO_NUMB" HeaderText="PO No" />
                                        <asp:BoundField DataField="PO_TYPE" HeaderText="PO Type" />
                                         <asp:BoundField DataField="UOM" HeaderText="UOM" />
                                         <asp:BoundField DataField="OPBAL_STOK" HeaderText="OPBAL STOK" />
                                         <asp:BoundField DataField="OPBAL_VALUE" HeaderText="OPBAL VALUE" />
                                         <asp:BoundField DataField="RECPT_QTY" HeaderText="RECEPT QTY" />
                                         <asp:BoundField DataField="LRDATE" HeaderText="LRDATE"  DataFormatString="{0:dd-MM-yyyy}" />
                                         <asp:BoundField DataField="RECPT_VALUE" HeaderText="RECPT VALUE" />
                                         <asp:BoundField DataField="ISSUE_QTY" HeaderText="ISSUE QTY" />
                                         <asp:BoundField DataField="ISSUE_VALUE" HeaderText="ISSUE VALUE" />
                                         <asp:BoundField DataField="BALVAL" HeaderText="BAL VALUE" />
                                         <asp:BoundField DataField="DATE_OF_MFG" HeaderText="DATE OF MFG"  DataFormatString="{0:dd-MM-yyyy}" />
                                          <asp:BoundField DataField="MATERIAL_STATUS" HeaderText="MATERIAL STATUS" />
                                    </Columns>
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                        VerticalAlign="Middle" />
                                </asp:GridView>
  </asp:Panel>
    </td></tr>
</table>

