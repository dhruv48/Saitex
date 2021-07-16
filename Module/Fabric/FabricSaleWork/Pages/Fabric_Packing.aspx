<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Fabric_Packing.aspx.cs" Inherits="Module_Fabric_FabricSaleWork_Pages_Fabric_Packing" %>



<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cphBody">
    <style type="text/css">
    .item 
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; display:inline;overflow:hidden;white-space:nowrap;}
    .header
    {
        margin-left: 4px;
    }
    .c1
    {
        width: 60px;
    }
    .c2
    {
        margin-left: 8px;
        width: 80px;
    }
    .c3
    {
        margin-left: 8px;
        width: 100;
    }
    .c4
    {
        margin-left: 8px;
        width: 150;
    }
    .c5
    {
        margin-left: 15px;
        width: 150;
        margin-right: 20px;
    }
        .style1
        {
            text-align: right;
            vertical-align: top;
            width: 13%;
        }
    </style>


<table width="100%" align="left" class="tContentArial">
    <tr>
        <td align="left" valign="top" class="td" width="100%">
            <table class="tContentArial">
                <tr>
                    <td id="tdSave" runat="server">
                        <asp:ImageButton ID="imgbtnSave" OnClick="imgbtnSave_Click" runat="server" ToolTip="Save"
                            ImageUrl="~/CommonImages/save.jpg" ValidationGroup="YM"></asp:ImageButton>
                    </td>
                    <td id="tdUpdate" runat="server">
                        <asp:ImageButton ID="imgbtnUpdate" OnClick="imgbtnUpdate_Click" runat="server" ToolTip="Update"
                            ImageUrl="~/CommonImages/edit1.jpg" ValidationGroup="M1"></asp:ImageButton>
                    </td>
                   
                    <td>
                        <asp:ImageButton ID="imgbtnFind"  runat="server" ToolTip="Find"
                            ImageUrl="~/CommonImages/link_find.png" onclick="imgbtnFind_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" OnClick="imgbtnClear_Click" runat="server" ToolTip="Clear"
                            ImageUrl="~/CommonImages/clear.jpg"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print"
                            ImageUrl="~/CommonImages/link_print.png"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" OnClick="imgbtnExit_Click" runat="server" ToolTip="Exit"
                            ImageUrl="~/CommonImages/link_exit.png"></asp:ImageButton>
                    </td>
                    
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader td" width="100%">
            <b class="titleheading">
                <asp:Label ID="lblFormHeading" runat="server" CssClass="SmallFont">KNITTED FABRIC PACKING</asp:Label></b>
        </td>
    </tr>
    <tr>
        <td align="left" valign="top" width="100%" class="td">
            <span class="Mode">You are in&nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" width="100%">
            <table width="100%">
                <tr>
                    <td class="tdRight" width="17%">
                        Order No.
                    </td>
                    <td align="left" valign="top" width="17%">
                        <cc2:ComboBox ID="ddlOrderNo" runat="server" AutoPostBack="True" DataTextField="ORDER_NO"
                            DataValueField="ORDER_NO" EnableLoadOnDemand="true" Height="200px" MenuWidth="800" EnableVirtualScrolling="true" OpenOnFocus="true" EmptyTextSelect="Select OrderNo" 
                             OnLoadingItems="ddlOrderNo_LoadingItems" OnSelectedIndexChanged="ddlOrderNo_SelectedIndexChanged"
                            Width="85px">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Order No</div>
                               <div class="header c3">
                                    PI_No </div>
    
                                <div class="header c2">
                                    Order Date</div>
                                <div class="header c1">
                                    Product Type</div>
                                <div class="header c2">
                                    Party Code</div>
                                <div class="header c3">
                                    Party Name</div>
                                     
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal ID="Container7" runat="server" Text='<%# Eval("ORDER_NO") %>' />
                                </div>
                                <div class="item c4">
                                    <asp:Literal runat="server" ID="Literal2" Text='<%# Eval("PI_NO") %>' /></div>

                                <div class="item c2">
                                    <asp:Literal ID="Container8" runat="server" Text='<%# Eval("ORDER_DATE","{0:dd/MM/yyyy}") %>' />
                                </div>
                                <div class="item c1">
                                    <asp:Literal ID="Container9" runat="server" Text='<%# Eval("PRODUCT_TYPE") %>' />
                                </div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Literal7" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                <div class="item c4">
                                    <asp:Literal runat="server" ID="Literal9" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                      
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>
                            </FooterTemplate>
                        </cc2:ComboBox>
                        
                           <cc2:ComboBox ID="ddlOrderNo1" runat="server" AutoPostBack="True" DataTextField="ORDER_NO"
                            DataValueField="ORDER_NO" EnableLoadOnDemand="true" Height="200px" MenuWidth="800" EnableVirtualScrolling="true" OpenOnFocus="true" EmptyTextSelect="Select OrderNo" 
                             OnLoadingItems="ddlOrderNo1_LoadingItems" OnSelectedIndexChanged="ddlOrderNo1_SelectedIndexChanged"
                            Width="85px" >
                            <HeaderTemplate>
                            <div class="header c3">
                                    Packing ID</div>
                                <div class="header c1">
                                    Order No</div>
                               <div class="header c3">
                                    PI_No </div>
    
                                <div class="header c2">
                                    Order Date</div>
                                <div class="header c1">
                                    Product Type</div>
                                <div class="header c2">
                                    Party Code</div>
                                <div class="header c3">
                                    Party Name</div>
                                     
                            </HeaderTemplate>
                            <ItemTemplate>
                            <div class="item c5">
                                    <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("PACKING_ID") %>' /></div>
                                <div class="item c1">
                                    <asp:Literal ID="Container7" runat="server" Text='<%# Eval("ORDER_NO") %>' />
                                </div>
                                <div class="item c4">
                                    <asp:Literal runat="server" ID="Literal2" Text='<%# Eval("PI_NO") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal ID="Container8" runat="server" Text='<%# Eval("ORDER_DATE","{0:dd/MM/yyyy}") %>' />
                                </div>
                                <div class="item c1">
                                    <asp:Literal ID="Container9" runat="server" Text='<%# Eval("PRODUCT_TYPE") %>' />
                                </div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Literal7" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                <div class="item c4">
                                    <asp:Literal runat="server" ID="Literal9" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                   
                                    
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>
                            </FooterTemplate>
                        </cc2:ComboBox>
                    
                        <asp:TextBox ID="txtOrderNo1" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="90px"></asp:TextBox>
                    
                    </td>
                    <td class="style1">
                        Party Code
                    </td>
                    
                    <td class="tdLeft" width="17%">
                        <asp:TextBox ID="txtpartycode" runat="server" CssClass="TextBoxDisplay SmallFont1" Font-Size="XX-Small" Width="99%"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="15%">
                        Party Name
                    </td>
                    <td class="tdLeft" width="18%">
                        <asp:TextBox ID="txtpartyname" runat="server"  CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Font-Size="XX-Small" Width="99%" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="17%">
                        Order Date
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:TextBox ID="TxtOrderdt" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Font-Size="XX-Small" Width="99%" ></asp:TextBox>
                    </td>
                    <td class="style1">
                       Order Qty.
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:TextBox ID="TxtOrdqty" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Font-Size="XX-Small" Width="99%" ></asp:TextBox>
                    </td>
                    <td class="tdRight" width="15%">
                        Product Name
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:TextBox ID="TxtPrdQty" runat="server"  CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Font-Size="XX-Small" Width="99%" ></asp:TextBox>
                    </td>
                    
                </tr>

                <tr>
                    <td class="tdRight" width="17%">
                        Remarks
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:TextBox ID="txtRemarks" runat="server" Font-Size="XX-Small" Width="99%" 
                            CssClass="SmallFont"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="17%">
                       PI No.
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Font-Size="XX-Small" Width="99%" ></asp:TextBox>
                    </td>
                    <td class="tdRight" width="17%">
                   Delivery Date:
                    </td>
                     <td class="tdLeft" width="43%" colspan="2" >
                        <asp:TextBox ID="Txtdelivrydt" runat="server"  CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Font-Size="XX-Small" Width="99%" ></asp:TextBox>
                    </td>
                   
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" width="100%">
           
            <table width="100%">
                <tr>
                    <td class="tdRight" width="100%">
                        <table width="100%">
                            <tr bgcolor="#336699" class="SmallFont titleheading">
                                <td width="15%" class="tdLeft">
                                   Packing Type
                                </td>
                                <td width="10%" class="tdLeft">
                                  <asp:Label ID="lblItemQty" runat="server" Text="Item Qty"></asp:Label>
                                </td>
                                <td width="10%" class="tdLeft">
                                    <asp:Label ID="lblNoOfItem" runat="server" Text="No Of Rolls"></asp:Label> 
                                </td>
                               
                               <td width="10%" class="tdLeft">
                                    <asp:Label ID="lblNoOfPackingItem" runat="server" Text="Qty in Mtr."></asp:Label> 
                                </td> 
                                <td width="10%" class="tdLeft">
                                    <asp:Label ID="Label1" runat="server" Text="Weight In KG."></asp:Label> 
                                </td> 
                                <td width="10%" class="tdLeft">
                                   Remarks
                                </td>
                                <td width="30%" class="tdLeft">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td width="20%" class="tdLeft">
                                    
                       
                         <asp:DropDownList ID="ddlPackingDetails" runat="server" Width="99%" 
                                        onselectedindexchanged="ddlPackingDetails_SelectedIndexChanged" AutoPostBack="true" >
                         <asp:ListItem Selected="True" Text="Select" Value="Select" ></asp:ListItem>
                          <asp:ListItem  Text="Poly Bag" Value="Poly Bag"></asp:ListItem>
                         <%--  <asp:ListItem  Text="Inner Carton" Value="Inner Carton"></asp:ListItem>--%>
                            <asp:ListItem  Text="Outer Carton" Value="Outer Carton"></asp:ListItem>
                                    </asp:DropDownList>
                    
                    </td>
                                </td>
                                <td width="15%" class="tdLeft">
                                    <asp:TextBox ID="txtItemQty" runat="server" CssClass="TextBoxDisplay TextBox SmallFont"
                                        Width="40%" ReadOnly="True" Visible="true"></asp:TextBox>    
                                        &nbsp;
                                        <asp:Label ID="lblUOM" runat="server" Text="uom" Width="35%"> </asp:Label>                               
                                </td>
                                <td width="14%" class="tdLeft">
                                    <asp:TextBox ID="txtNoOFItem" runat="server" CssClass="TextBox SmallFont" AutoPostBack="true"
                                        Width="99%"  Wrap="true" ontextchanged="txtNoOFItem_TextChanged"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FiltertxtRate" runat="server"  TargetControlID="txtNoOFItem"   FilterType="Custom, Numbers" ValidChars="."/>

                                </td>
                                
                               <td width="14%" class="tdLeft">
                                    <asp:TextBox ID="txtNoOfPackingItem" runat="server"  Width="99%"
                                        Wrap="true" CssClass="TextBox SmallFont"></asp:TextBox>
                                </td>
                                
                                 </td>
                                
                               <td width="14%" class="tdLeft">
                                    <asp:TextBox ID="txtweightinkg" runat="server"  Width="99%"
                                        Wrap="true" CssClass=" TextBox SmallFont"></asp:TextBox>
                                </td>
                              <td width="14%" class="tdLeft">
                              <asp:TextBox ID="txtSubRemark" runat="server" CssClass="TextBox SmallFont" Width="99%"
                                        Wrap="true"></asp:TextBox>
                              </td>
                               
                                
                                <td width="20%" class="tdLeft">
                                    <asp:Button ID="btnSavePackingDetails" runat="server" CssClass="SmallFont" 
                                        Text="Save" ValidationGroup="T1" Width="45px" 
                                        onclick="btnSavePackingDetails_Click" />
                                    <asp:Button ID="btnCancelPackingDetails" runat="server" CssClass="SmallFont" 
                                        Text="Cancel" Width="45px" onclick="btnCancelPackingDetails_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="td" width="100%">
                        
                            <asp:GridView ID="grdPIDetail" runat="server" AutoGenerateColumns="False" CssClass="SmallFont"
                                OnRowCommand="grdPIDetail_RowCommand" Width="98%" 
                                onrowdatabound="grdPIDetail_RowDataBound">  
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr. No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSRNo" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("UNIQUEID") %>'></asp:Label></ItemTemplateĂ
                                            <itemstyle verticalalign="Top" horizontalalign="Right" />
                                            <headerstyle verticalalign="Top" horizontalalign="Right" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrderNo" runat="server" CssClass="Label SmallFont" Text='<%# Bind("ORDER_NO") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Packing Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrderDetail" runat="server" CssClass="Label SmallFont" Text='<%# Bind("PACKING_TYPE") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Item Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemQty" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("ITEM_QTY") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderText="No Of Rolls">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLoadQty" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("NO_OF_ITEM") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                           
                                  
                                    <asp:TemplateField HeaderText="Qty in Mtr.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPacking" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("NO_OF_PACKING") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Weight In KG">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWEIGHTINKG" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("WEIGHT_IN_KG") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                  
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubRemarks" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("REMARKS") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandArgument='<%# Bind("UNIQUEID") %>'
                                                CommandName="EditDetail" Text="Edit" Visible="true"></asp:LinkButton>
                                            <asp:LinkButton ID="lnkbtnDel" runat="server" CommandArgument='<%# Bind("UNIQUEID") %>'
                                                CommandName="DelDetail" Text="Delete" Visible="true"></asp:LinkButton></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Center" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="#336699" CssClass="SmallFont titleheadingGrid" />
                                <RowStyle CssClass="SmallFont" />
                            </asp:GridView>
                       
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="YM" />
                    </td>
                </tr>
            </table>







                                








                                
    






                                








                                
    </table>







                                








                                
    






                                








                                
</asp:Content>
