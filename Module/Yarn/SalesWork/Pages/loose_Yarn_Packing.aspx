<%@ Page Language="C#"  MasterPageFile="~/CommonMaster/admin.master"  AutoEventWireup="true" CodeFile="loose_Yarn_Packing.aspx.cs" Inherits="Module_Yarn_SalesWork_Pages_loose_Yarn_Packing" %>

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
        width: 100px;
    }
    .c2
    {
        margin-left: 2px;
        width: 200px;
    }
    .c3
    {
        margin-left: 2px;
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
        .style2
        {
            text-align: left;
            vertical-align: top;
            width: 150px;
        }
        .style3
        {
            width: 150px;
        }
        .style4
        {
            text-align: left;
            vertical-align: top;
            width: 184px;
        }
        .style5
        {
            width: 184px;
        }
        .style6
        {
            text-align: left;
            vertical-align: top;
            width: 15%;
        }
        .style7
        {
            text-align: left;
            vertical-align: top;
            width: 95px;
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
                            ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click"></asp:ImageButton>
                    </td>
                     <td>
                        <asp:ImageButton ID="Imgbtnforward" runat="server" ToolTip="Print"
                            ImageUrl="~/CommonImages/forward.jpg" OnClick="imgbtnforward_Click"></asp:ImageButton>
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
                <asp:Label ID="lblFormHeading" runat="server" CssClass="SmallFont">LOOSE STOCK YARN PACKING</asp:Label></b>
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
                            DataValueField="ORDER_NO" EnableLoadOnDemand="true" Height="200px" MenuWidth="650" EnableVirtualScrolling="true" OpenOnFocus="true" EmptyTextSelect="Select OrderNo" 
                             OnLoadingItems="ddlOrderNo_LoadingItems" OnSelectedIndexChanged="ddlOrderNo_SelectedIndexChanged"
                            Width="85px">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Order No</div>
                               <%--<div class="header c3">
                                    PI_No </div>
    --%>
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
                               <%-- <div class="item c4">
                                    <asp:Literal runat="server" ID="Literal2" Text='<%# Eval("PI_NO") %>' /></div>
--%>
                                <div class="item c2">
                                    <asp:Literal ID="Container8" runat="server" Text='<%# Eval("ORDER_DATE","{0:dd/MM/yyyy}") %>' />
                                </div>
                                <div class="item c1">
                                    <asp:Literal ID="Container9" runat="server" Text='<%# Eval("PRODUCT_NAME") %>' />
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
                            DataValueField="ORDER_NO" EnableLoadOnDemand="true" Height="200px" MenuWidth="650" EnableVirtualScrolling="true" OpenOnFocus="true" EmptyTextSelect="Select OrderNo" 
                             OnLoadingItems="ddlOrderNo1_LoadingItems" OnSelectedIndexChanged="ddlOrderNo1_SelectedIndexChanged"
                            Width="85px" >
                            <HeaderTemplate>
                           <div class="header c3">
                                  ID</div>
                                <div class="header c3">
                                   Order No.</div>                            
                                <div class="header c3">
                                    Order Date</div>
                                <div class="header c3">
                                   Product Name</div>                              
                                <div class="header c3">
                                    Party Code</div>
                                <div class="header c4">
                                    Party Name </div>                              
                            </HeaderTemplate>
                            <ItemTemplate>
                            <div class="item c3">
                                    <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("PACKING_ID") %>' /></div>
                                <div class="item c3">
                                    <asp:Literal ID="Container7" runat="server" Text='<%# Eval("ORDER_NO") %>' />
                                </div>                              
                                <div class="item c3">
                                    <asp:Literal ID="Container8" runat="server" Text='<%# Eval("ORDER_DATE","{0:dd/MM/yyyy}") %>' />
                                </div>
                                <div class="item c3">
                                    <asp:Literal ID="Container9" runat="server" Text='<%# Eval("PRODUCT_NAME") %>' />
                                </div>
                                <div class="item c3">
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
                    <td class="tdRight" width="15%">
                        Product Name
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:TextBox ID="TxtPrdQty" runat="server"  CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Font-Size="XX-Small" Width="99%" ></asp:TextBox>
                    </td>
                    
                    <td class="tdRight" width="17%">
                        Remarks
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:TextBox ID="txtRemarks" runat="server" Font-Size="XX-Small" Width="99%" 
                            CssClass="SmallFont"></asp:TextBox>
                    </td>
                    
                </tr>

                <%--<tr>
                    <td class="tdRight" width="17%">
                        Remarks
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:TextBox ID="txtRemarks1" runat="server" Font-Size="XX-Small" Width="99%" 
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
                   
                </tr>--%>
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
                                <td class="style4">
                                   Item Details
                                </td>
                                <td width="10%" class="tdLeft">
                                  <asp:Label ID="lblItemQty" runat="server" Text="Count"></asp:Label>
                                </td>
                                <td width="10%" class="tdLeft">
                                    <asp:Label ID="lblNoOfItem" runat="server" Text="Blend"></asp:Label> 
                                </td>                               
                               <td width="10%" class="tdLeft">
                                    <asp:Label ID="lblNoOfPackingItem" runat="server" Text="Lot No."></asp:Label> 
                                </td> 
                                <td width="10%" class="tdLeft">
                                    <asp:Label ID="Label1" runat="server" Text="Cone Tip."></asp:Label> 
                                </td> 
                                <td class="style2">
                                    <asp:Label ID="Label6" runat="server" Text="CH/CW/KH/KW"></asp:Label> 
                                </td> 
                                 <td width="10%" class="tdLeft">
                                    <asp:Label ID="Label2" runat="server" Text="Cone WT."></asp:Label> 
                                </td> 
                                <%--  <td width="10%" class="tdLeft">
                                    <asp:Label ID="Label3" runat="server" Text=" Avl.No Of Cones."></asp:Label> 
                                </td> --%>
                                <td width="10%" class="tdLeft">
                                    <asp:Label ID="Label5" runat="server" Text=" No Of Cones."></asp:Label> 
                                </td> 
                                 <td class="style6">
                                    <asp:Label ID="Label4" runat="server" Text="KGS."></asp:Label> 
                                </td> 
                                <td class="style7">
                                   Remarks
                                </td>
                                <td width="30%" class="tdLeft">
                                    &nbsp;
                                </td>
                            </tr>
                            
                           
                            
                            <tr>
                                
                                    
                       
                          <td align="left" valign="top" class="style5">
                                    <cc2:ComboBox ID="cmbArticleNo" runat="server" AutoPostBack="True" CssClass="smallfont"
                                        DataTextField="YARN_CODE" DataValueField="Combined" EnableLoadOnDemand="True"
                                        MenuWidth="800" OnLoadingItems="cmbArticleNo_LoadingItems" OnSelectedIndexChanged="cmbArticleNo_SelectedIndexChanged"
                                        EnableVirtualScrolling="true" OpenOnFocus="true" TabIndex="15" 
                                        Visible="true">
                                        <HeaderTemplate>
                                            <div class="header c1">
                                                YARN CODE</div>
                                            <div class="header c2">
                                                YARN DESC</div>
                                            <div class="header c3">
                                                 COUNT</div>
                                            <div class="header c4">
                                                BLEND</div>
                                            <div class="header c5">
                                                BLEND PER</div>
                                          <div class="header c5">
                                                PI NO </div>
                                                <div class="header c5">
                                                BAL QTY </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="item c1">
                                                <%# Eval("YARN_CODE")%></div>
                                            <div class="item c2">
                                                <%# Eval("YARN_DESC")%></div>
                                            <div class="item c3">
                                                <%# Eval("Y_COUNT")%></div>
                                           <div class="item c4">
                                                <%# Eval("BLEND")%></div>
                                          <div class="item c5">
                                                <%# Eval("BLEND_PER")%></div>
                                          <div class="item c5">
                                                <%# Eval("PI_NO")%></div>
                                          <div class="item c5">
                                                <%# Eval("ORDER_QTY")%></div>
                                          
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Displaying items
                                            <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                            out of
                                            <%# Container.ItemsCount %>.
                                        </FooterTemplate>
                                    </cc2:ComboBox>
                                 
                                       <%-- &nbsp;
                                         <asp:TextBox ID="TextBox4" runat="server"  Width="99%"
                                        Wrap="true" CssClass="TextBox SmallFont"></asp:TextBox>
                    --%>
                    </td>
                  
                                                              
                                
                             
                                <td width="14%" class="tdLeft">
                                    <asp:TextBox ID="txtCount" runat="server" CssClass="TextBoxDisplay TextBox SmallFont"
                                        Width="74%" ReadOnly="True" Visible="true"></asp:TextBox>    
                                     
                                       
                                     
                                       
                                </td>
                                 <td width="15%" class="tdLeft">
                                    <asp:TextBox ID="txtBlend" runat="server" CssClass="TextBoxDisplay TextBox SmallFont"
                                        Width="90%" ReadOnly="True" Visible="true"></asp:TextBox>    
                                </td>
                                 <td width="14%" class="tdLeft">
                                    <asp:TextBox ID="txtlotid" runat="server" CssClass="TextBoxDisplay TextBox SmallFont"
                                        Width="90%"  Wrap="true" Visible="true"></asp:TextBox>    
                                    
                                   </td>
                                   <td width="14%" class="tdLeft">
                                    <asp:TextBox ID="txtConetip" runat="server" CssClass="TextBox SmallFont"
                                        Width="88%"  Wrap="true" Visible="true"></asp:TextBox>    
                                    
                                   </td>
                                    <td  align ="Right" class="style3" valign ="top" runat ="server" >
                               <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" Width="60px" CssClass="gCtrTxt" 
                                  ValidationGroup="M1" > <%--onselectedindexchanged="ddlStatus_SelectedIndexChanged">--%>
                                <asp:ListItem Text="CH"></asp:ListItem>
                                <asp:ListItem Text="CW"></asp:ListItem>  <%--Value="2"--%>
                                <asp:ListItem Text="KH"></asp:ListItem>
                                 <asp:ListItem Text="KW"></asp:ListItem>
                            </asp:DropDownList>
                                         </td>
                                <td width="14%" class="tdLeft">
                                    <asp:TextBox ID="txtConeWT" runat="server" CssClass="TextBox SmallFont" AutoPostBack="true"
                                        Width="92%"  Wrap="true" ontextchanged="txtNoOFItem_TextChanged"></asp:TextBox>
                                       <%-- <cc1:FilteredTextBoxExtender ID="FiltertxtRate" runat="server"  TargetControlID="txtNoOFItem"   FilterType="Custom, Numbers" ValidChars="."/>--%>

                                </td>
                                
                             <%--  <td width="14%" class="tdLeft">
                                    <asp:TextBox ID="txtAvalNoofCones" runat="server"  Width="99%"
                                        Wrap="true" CssClass="TextBox SmallFont"></asp:TextBox>
                                </td>
                                --%>
                             <td width="15%" class="tdLeft">
                                    <asp:TextBox ID="txtNoofCones" runat="server"  Width="83%"
                                        Wrap="true" CssClass="TextBox SmallFont" ontextchanged="txtNoofCones_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </td>
                                
                           
                                
                               <td class="style6">
                                    <asp:TextBox ID="txtweightinkg" runat="server"  Width="99%" CssClass="TextBoxDisplay TextBox SmallFont"
                                        Wrap="true" ontextchanged="txtweightinkg_TextChanged"></asp:TextBox>
                                </td>
                              <td class="style7">
                              <asp:TextBox ID="txtSubRemark" runat="server" CssClass="TextBox SmallFont" Width="99%"
                                        Wrap="true"></asp:TextBox>
                              </td>
                               
                               
                                <td width="20%" class="tdLeft">
                                    <asp:Button ID="btnSavePackingDetails" runat="server" CssClass="SmallFont" 
                                        Text="Save" ValidationGroup="T1" Width="45px" 
                                        onclick="btnSavePackingDetails_Click" />
                                  
                                </td>
                            </tr>
                            
                             <tr>
                            <td colspan="2"  class="tdLeft">
                             
                                <asp:TextBox ID="TxtYarnCode" runat="server"  Width="75%"
                                        Wrap="true" CssClass="TextBoxDisplay TextBox SmallFont"></asp:TextBox>
                                        
                            </td> 
                            <td colspan="5"  class="tdLeft">
                             Description.<asp:TextBox ID="TxtYarnDesc" runat="server"  Width="75%"
                                        Wrap="true" CssClass="TextBoxDisplay TextBox SmallFont"></asp:TextBox>
                                                  
                            </td> 
                            <td colspan ="3"  class="tdLeft">  Bal Qty.<asp:TextBox ID="TxtBalQty" runat="server"  Width="25%"
                                        Wrap="true" CssClass="TextBoxDisplay TextBox SmallFont"></asp:TextBox>  
                                        &nbsp;&nbsp;<asp:TextBox ID="TxtPiNO" Visible ="false"  runat="server"  Width="45%"
                                        Wrap="true" CssClass="TextBoxDisplay TextBox SmallFont"></asp:TextBox>
                                        </td>
                            
                            <td>  <asp:Button ID="btnCancelPackingDetails" runat="server" CssClass="SmallFont" 
                                        Text="Cancel" Width="45px" onclick="btnCancelPackingDetails_Click" /></td>
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
                                    <asp:TemplateField HeaderText="Yarn Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblYarnCode" runat="server" CssClass="Label SmallFont" Text='<%# Bind("YARN_CODE") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Yarn Desc">
                                        <ItemTemplate>
                                            <asp:Label ID="lblYarn_desc" runat="server" CssClass="Label SmallFont" Text='<%# Bind("YARN_DESC") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Pi NO">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpino" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("PI_NO") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Count">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCount" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("COUNT") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderText="Blend">
                                        <ItemTemplate>
                                            <asp:Label ID="lblblend" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("BLEND_PER") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                           
                                  
                                    <asp:TemplateField HeaderText="lot_no.">
                                        <ItemTemplate>
                                            <asp:Label ID="lbllotno" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("LOT_NO") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Cone_Tip">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCONETIP" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("CONE_TIP") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CH/CW/KH/KW">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCH" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("CH") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cone_wt">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCONEWT" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("CONE_WT") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                  <asp:TemplateField HeaderText="No_of_Cones">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNOOFCONE" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("NO_OF_CONES") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="KGS">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKGS" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("WEIGHT_IN_KG") %>'></asp:Label></ItemTemplate>
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
