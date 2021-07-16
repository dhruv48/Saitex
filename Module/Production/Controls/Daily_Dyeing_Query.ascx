<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Daily_Dyeing_Query.ascx.cs" Inherits="Module_Production_Controls_Daily_Dyeing_Query" %>
<link href="../../../StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
<link href="../../../StyleSheet/style.css" rel="stylesheet" type="text/css" />
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>

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
        width: 200px;
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
    .d1
    {
        width: 180px;
    }
    .d2
    {
        margin-left: 4px;
        width: 120px;
    }
    .d3
    {
        margin-left: 4px;
        width: 180px;
    }
    .d4
    {
        margin-left: 4px;
        width: 120px;
    }
</style>
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
        width: 150px;
    }
    .c2
    {
        margin-left: 4px;
        width: 150px;
    }
    .c3
    {
        margin-left: 4px;
        width: 150px;
    }
    .c4
    {
        width: 150px;
    }
    .c5
    {
        margin-left: 4px;
        width: 340px;
    }
    .c6
    {
        margin-left: 4px;
        width: 150px;
    }
    .style1
    {
        width: 19%;
    }
    .style2
    {
        width: 4%;
    }
    .style3
    {
        width: 6%;
    }
    .style4
    {
        width: 5%;
    }
    .style6
    {
        width: 12%;
    }
    .style7
    {
        width: 26%;
    }
    .style8
    {
        width: 9%;
    }
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
<table width="100%" class ="td tContentArial">
     <tr>
        <td width="100%">
           <table >
                <tr>
                    <td id="tdPrint" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" Width="48" OnClick="imgbtnPrint_Click" />
                    </td>
                    <td>  
<asp:ImageButton ID="imgBtnExportExcel" runat="server" Width="48" Height="41" ToolTip="Print"
                            ImageUrl="~/CommonImages/export.png" onclick="imgBtnExportExcel_Click" ></asp:ImageButton>&nbsp;</td> 



                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" ToolTip="Clear"
                            ImageUrl="~/CommonImages/clear.jpg" OnClick="imgbtnClear_Click"></asp:ImageButton>
                    </td>
                    <td id="tdExit" runat="server">
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                            Width="48" Height="41" OnClick="imgbtnExit_Click"></asp:ImageButton>
                    </td>
                    <td id="tdHelp" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                            ToolTip="Help" Height="41" Width="48" onclick="imgbtnHelp_Click"></asp:ImageButton>
                    </td>
                    </tr>
                    </table>
               <table width="100%" class ="td">
                 <tr>
                 <td align="center" valign="top" class="tRowColorAdmin ">
                  <span class="titleheading">Daily Dyeing Production Query</span>
                            </td>
                        </tr>
                    </table>
     <table width="95%" >
       <tr>
        <td align="right">
           Production_Slip_No :
        </td>
      <td>
       <asp:TextBox ID="txtTRNNUMBer" runat="server"  Width="125px" 
                          CssClass="SmallFont TextBoxNo"></asp:TextBox>
      </td>
     <td align="right" > 
     Yarn Code :
      </td>
     <td align="left" valign="top" >
                   <cc2:ComboBox ID="txtItemCode" runat="server"  CssClass="smallfont"
                        DataTextField="YARN_DESC" DataValueField="YARN_CODE" EnableLoadOnDemand="true"
                        MenuWidth="500" OnLoadingItems="Item_LOV_LoadingItems" 
                        EnableVirtualScrolling="true" OpenOnFocus="true" TabIndex="9" Visible="true"
                        Height="200px" Width="150px" EmptyText="All"> 
                        <HeaderTemplate>  
                            <div class="header c4">
                                YARN CODE</div>
                            <div class="header c5">
                                YARN DESCRIPTION</div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="item c4">
                                <%# Eval("YARN_CODE") %></div>
                            <div class="item c5">
                                <%# Eval("YARN_DESC") %></div>
                        </ItemTemplate>
                        <FooterTemplate>
                            Displaying items
                            <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                            <%# Container.ItemsCount %>.
                        </FooterTemplate>
                    </cc2:ComboBox>
                    </td>
        <td align="right" >
            Department :
        </td>
        <td>
            <asp:DropDownList ID="ddldept" runat="server" DataTextField="PROS_DESC" DataValueField="PROS_CODE"
                Width="125px"  CssClass="tContentArial" >
            </asp:DropDownList>
        </td>
      </tr>
      <tr>
      <td align="right" >
      FG Lot No :
      </td>
      <td>
      <cc2:ComboBox ID="ddllotno" runat="server" CssClass="smallfont" Width="130px" MenuWidth="450px" EnableLoadOnDemand="true" EnableVirtualScrolling="true"
                         Visible="true" Height="200px" OnLoadingItems="ddllotno_LoadingItems"  EmptyText="All">
                       <HeaderTemplate>
                            <div class="header c1">
                                LOT</div>
                           <div class="header c2">
                                YARN DESCRIPTION</div> 
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="item c1">
                                <%# Eval("LOT_NO")%></div>
                            <div class="item c2">
                                <%# Eval("ASS_YARN_DESC") %></div>
                            
                        </ItemTemplate>
                      
                         <FooterTemplate>
                            Displaying items
                            <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                            <%# Container.ItemsCount %>.
                        </FooterTemplate>
                    </cc2:ComboBox>
         <%--<asp:DropDownList ID="ddllotno" runat="server" DataTextField="LOT_NUMBER" DataValueField="LOT_NUMBER"
                Width="128px"  CssClass="tContentArial" >
            </asp:DropDownList>--%>
        </td>
        <td align="right">
        Location :
        </td>
        <td>
         <asp:DropDownList ID="ddlLocation" runat="server" CssClass="SmallFont" 
                Font-Size="8" Width="150px" TabIndex="3" >
                <asp:ListItem>GODOWN</asp:ListItem>
                <asp:ListItem>MACHINE ROOM</asp:ListItem>
                </asp:DropDownList>
        </td>
        <td align="right">
           Production Date :
         </td>
         <td>
            <asp:TextBox ID="txtMRNDate" runat="server" TabIndex="2" ValidationGroup="M1" Width="125px"
                            CssClass="TextBox SmallFont"></asp:TextBox>
          <cc4:CalendarExtender ID="CE1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtMRNDate"> </cc4:CalendarExtender>                
    
         </td>
         <td>
           <asp:TextBox ID="txtMRNToDate" runat="server" TabIndex="2" ValidationGroup="M1" Width="125px"
                            CssClass="TextBox SmallFont"></asp:TextBox>
          <cc4:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtMRNToDate"> </cc4:CalendarExtender>                
    
         </td>
      </tr>  
      <tr>
     <td align="right" >
                Branch:
              </td>
             <td >
           <asp:DropDownList ID="ddlBranch" runat="server" CssClass="gCtrTxt " Font-Size="9" Width="130px" >
                </asp:DropDownList>
                   </td>
             <td align="right" >
                      Raw Lot No.
              </td>
             <td >
             <cc2:ComboBox ID="ddlMergeNo" runat="server" CssClass="smallfont" Width="150px" MenuWidth="350px" EnableLoadOnDemand="true" EnableVirtualScrolling="true"
                         Visible="true" Height="200px" OnLoadingItems="ddlMergeNo_LoadingItems"  EmptyText="All">
                       <HeaderTemplate>
                            <div class="header c1">
                                LOT</div>
                           <div class="header c2">
                                DESCRIPTION</div> 
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="item c1">
                                <%# Eval("LOT_NO")%></div>
                            <div class="item c2">
                                <%# Eval("YARN_DESC") %></div>
                            
                        </ItemTemplate>
                      
                         <FooterTemplate>
                            Displaying items
                            <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                            <%# Container.ItemsCount %>.
                        </FooterTemplate>
                    </cc2:ComboBox>
  <%--<asp:DropDownList ID="ddlPINo" runat="server" CssClass="SmallFont" Font-Size="8" Width="150px"  
               TabIndex="3"></asp:DropDownList>--%>
                            </td>      
       <td align="right" >
   Packing Process:
      </td>
      <td><asp:DropDownList ID="ddlStore" runat="server" CssClass="SmallFont" Font-Size="8" 
                 Width="125px" TabIndex="3" 
                ></asp:DropDownList>
       </td>
          <td align="right">
          <asp:Button ID="btnsave" runat="server" CssClass="AButton" 
                                        OnClick="btnsave_Click" Text="Get Record" Width="85px" />
        </td>              
      </tr>           
         <tr>
            <td colspan="3" width="50%">
                <b>Total&nbsp;Records: </b>
                <asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
            </td>
            <td colspan="3" width="50%">
                <asp:UpdateProgress ID="UpdateProgress9587" runat="server">
                    <ProgressTemplate>
                        <h3>
                            Loading...</h3>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </td>
        </tr>
     </table>
       <table  width="100%" >
                <tr>
               <td align="left">
    <asp:Panel ID="pnlShowHover" runat="server" ScrollBars="Auto" Width="100%">
        <asp:GridView ID="GridYarnLoosePacking" runat="server" AutoGenerateColumns="False" Width="100%"
          HeaderStyle-Font-Bold="true"  CellPadding="3" ForeColor="#333333"
           GridLines="Both"  BorderStyle="Ridge" Font-Names="Arial" Font-Size="X-Small" ShowFooter="true"
          OnPageIndexChanging="GridYarnLoosePacking_PageIndexChanging"
            onselectedindexchanged="GridYarnLoosePacking_SelectedIndexChanged" >
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
            <asp:TemplateField HeaderText="Date">
                                             <ItemTemplate>
                                            <asp:Label ID="lblDate" runat="server" CssClass="Label SmallFont" Text='<%# Bind("RODUCTION_DATE","{0:dd/MM/yyyy}") %>' ></asp:Label>
                                            </ItemTemplate>                                                                         
                                            </asp:TemplateField>     
            
            <asp:BoundField DataField="TRN_NUMB" HeaderText="Prod&nbsp;No" />     
             <asp:BoundField DataField="JOB_CARD_NO" HeaderText="JC&nbsp;No" />      
             <asp:BoundField DataField="MACHINE_CODE" HeaderText="Machine&nbsp;No" />      
             <asp:BoundField DataField="PARTY_NAME" HeaderText="PArty&nbsp;Name" />      
             <asp:TemplateField HeaderText="Yarn&nbsp;Desc">
                                             <ItemTemplate>
                                            <asp:Label ID="lblYarnCode" runat="server" CssClass="Label SmallFont" Text='<%# Bind("QUALITY_DESC") %>' ToolTip='<%# Bind("QUALITY_CODE") %>'></asp:Label>
                                            </ItemTemplate>                                                                         
                                            </asp:TemplateField>     
             <asp:BoundField DataField="SHADE_CODE" HeaderText="Shade" />
            <asp:BoundField DataField="GREY_LOT_NO" HeaderText="Lot&nbsp;No"  />
                      
                                          <asp:TemplateField HeaderText="Carton&nbsp;No">
                                             <ItemTemplate>
                                            <asp:Label ID="lblCortonNo" runat="server" CssClass="Label SmallFont" Text='<%# Bind("CORTOON_NO") %>' ForeColor="Green" Font-Bold="true"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                             <asp:Label ID="lblTotalCortonNo" runat="server" CssClass="Label SmallFont" ForeColor="White" Font-Bold="true"></asp:Label>                                           
                                            </FooterTemplate>                                     
                                            </asp:TemplateField>  
                                           <asp:TemplateField HeaderText="Net&nbsp;Wt">
                                             <ItemTemplate>
                                            <asp:Label ID="lblGrossWt" runat="server" CssClass="Label SmallFont" Text='<%# Bind("NET_WT") %>' ForeColor="DarkBlue" Font-Bold="true"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                             <asp:Label ID="lblTotalGrossWt" runat="server" CssClass="Label SmallFont" ForeColor="White" Font-Bold="true"></asp:Label>                                           
                                            </FooterTemplate>                                     
                                            </asp:TemplateField>    
                                           <asp:TemplateField HeaderText="Cops">
                                             <ItemTemplate>
                                            <asp:Label ID="lblCops" runat="server" CssClass="Label SmallFont" Text='<%# Bind("COPS") %>'  Font-Bold="true"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                             <asp:Label ID="lblTotalCops" runat="server" CssClass="Label SmallFont" ForeColor="White" Font-Bold="true"></asp:Label>                                           
                                            </FooterTemplate>                                     
                                            </asp:TemplateField>  
                                           <asp:TemplateField HeaderText="Rej&nbsp;Wt">
                                             <ItemTemplate>
                                            <asp:Label ID="lblTareWt" runat="server" CssClass="Label SmallFont" Text='<%# Bind("REJ_TRN_QTY") %>' ForeColor="DarkOrchid" Font-Bold="true"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                             <asp:Label ID="lblTotalTareWt" runat="server" CssClass="Label SmallFont" ForeColor="White" Font-Bold="true"></asp:Label>                                           
                                            </FooterTemplate>                                     
                                            </asp:TemplateField>  
                                             <asp:TemplateField HeaderText="Rej&nbsp;Cops">
                                             <ItemTemplate>
                                            <asp:Label ID="lblNetWt" runat="server" CssClass="Label SmallFont" Text='<%# Bind("REJ_COPS") %>' ForeColor="Green" Font-Bold="true"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                             <asp:Label ID="lblTotalNetWt" runat="server" CssClass="Label SmallFont" ForeColor="White" Font-Bold="true"></asp:Label>                                           
                                            </FooterTemplate>                                     
                                            </asp:TemplateField>     
                      
                                         
            <%--<asp:BoundField DataField="AVG_WT" HeaderText="Avg&nbsp;Wt" />--%>
             <asp:BoundField DataField="UOM_OF_UNIT" HeaderText="UOM" />           
                
           <asp:BoundField DataField="ORDER_NO" HeaderText="Order&nbsp;No" />
           <asp:BoundField DataField="TO_LOCATION" HeaderText="Location" />
            <asp:BoundField DataField="PROCESS" HeaderText="PackIng Process" />
       <asp:BoundField DataField="TROLLY_NO" HeaderText="Trolly No" />  
          <%--  <asp:BoundField DataField="PRODUCTION_ID" HeaderText="Prod&nbsp;ID" />--%>
           <%-- <asp:BoundField DataField="PRODUCTION_TYPE" HeaderText="Prod&nbsp;Type" /> --%>              
                       
                                
        </Columns>
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
      </asp:GridView>
    </asp:Panel>
    </td>
        </tr>
    </table>
  </td>
    </tr>
</table>
</ContentTemplate>
</asp:UpdatePanel> 