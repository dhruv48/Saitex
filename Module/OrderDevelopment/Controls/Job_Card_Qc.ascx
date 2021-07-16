<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Job_Card_Qc.ascx.cs" Inherits="Module_OrderDevelopment_Controls_Job_Card_Qc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
        width: 100px;
    }
    .c2
    {
        margin-left: 10px;
        width: 140px;
    }
    .c3
    {
        margin-left: 4px;
        width: 120px;
    }
    .c4
    {
        width: 190px;
    }
    .c5
    {
        margin-left: 4px;
        width: 320px;
    }
    .c6
    {
        margin-left: 4px;
        width: 150px;
    }
    .style1
    {
        height: 26px;
    }
</style>
<asp:UpdatePanel id="UpdatePanel1"  runat="server">
<ContentTemplate>
<table class="tContentArial" align="center" width="95%">
    <tr>
        <td valign="top" class="td" align="left" width="100%">
            <table align="left">
                <tr>
                   <%-- <td id="tdSave" valign="top" align="center" runat="server">
                        <asp:ImageButton ID="imgbtnSave" OnClick="imgbtnSave_Click" runat="server" ImageUrl="~/CommonImages/save.jpg"
                            ToolTip="Save" Height="41" Width="48" ValidationGroup="M1"></asp:ImageButton>
                    </td>--%>
                    <td id="tdUpdate" valign="top" align="center" runat="server">
                        <asp:ImageButton ID="imgbtnUpdate" OnClick="imgbtnUpdate_Click" runat="server" ImageUrl="~/CommonImages/edit1.jpg"
                            ToolTip="Update" Height="41" Width="48" ValidationGroup="M1"></asp:ImageButton>
                    </td>
                    <td valign="top" align="center">
                        <asp:ImageButton ID="imgbtnFind" OnClick="imgbtnFind_Click" runat="server" ImageUrl="~/CommonImages/link_find.png"
                            ToolTip="Find" Height="41" Width="48" Visible="true"></asp:ImageButton>
                    </td>
                    <td valign="top" align="center">
                        <asp:ImageButton ID="imgbtnPrint" OnClick="imgbtnPrint_Click" runat="server" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" Height="41" Width="48"></asp:ImageButton>
                    </td>
                    <td valign="top" align="center">
                        <asp:ImageButton ID="imgbtnClear" OnClick="imgbtnClear_Click" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                            ToolTip="Clear" Height="41" Width="48"></asp:ImageButton>
                    </td>
                    <td valign="top" align="center">
                        <asp:ImageButton ID="imgbtnExit" OnClick="imgbtnExit_Click" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                            ToolTip="Exit" Height="41" Width="48"></asp:ImageButton>
                    </td>
                    <td valign="top" align="center">
                        <asp:ImageButton ID="imgbtnHelp" OnClick="imgbtnHelp_Click" runat="server" ImageUrl="~/CommonImages/link_help.png"
                            ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="TableHeader" class="td" align="center" width="100%">
            <b class="titleheading">Job Card QC</b>
        </td>
    </tr>
    <tr>
        <td class="td" valign="top" align="left" width="100%">
            <span style="color: #ff0000">You are in &nbsp;<asp:Label ID="lblMode" runat="server">
            </asp:Label>&nbsp;Mode</span>
        </td>
    </tr>
    
    
    <tr>
      <td> 
      
      <table width="100%">
      <tr>
      
                   <td class="tdRight" width="16%" >
                         <asp:Label ID="lblJobCardNo" runat="server" Text="Job Card No" Width="100%" Font-Bold="true" ></asp:Label>
                    </td>
                    <td class="tdLeft" width="16%">
                        <cc2:ComboBox ID="ddlPaNo" runat="server" TabIndex="1" Width="100%" MenuWidth="700"
                                    AutoPostBack="true" CssClass="SmallFont" EnableLoadOnDemand="true" OpenOnFocus="true"
                                    Height="200px" EnableVirtualScrolling="true" OnLoadingItems="ddlPaNo_LoadingItems"
                                     EmptyText="Select Job Card No" 
                            onselectedindexchanged="ddlPaNo_SelectedIndexChanged"  >
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Job Card No
                                        </div>
                                       <%-- <div class="header c1">
                                            Cust Req NO</div>
                                        <div class="header c3">
                                            Pa No</div>--%>
                                         <div class="header c1">
                                            Batch Qty
                                        </div>
                                        <div class="header c1">
                                            Shade Code
                                        </div>
                                        <div class="header c1">
                                            Prod Date
                                        </div>
                                         <div class="header c1">
                                            Party Name
                                        </div>
                                        </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <%# Eval("BATCH_CODE")%></div>
                                        <%--<div class="item c1">
                                            <%# Eval("CUST_REQ_NO")%></div>
                                        <div class="item c3">
                                            <%# Eval("PA_NO")%></div>--%>
                                           <div class="item c1">
                                            <%# Eval("TRN_QTY")%></div>
                                            <div class="item c1">
                                            <%# Eval("SHADE_CODE")%></div>
                                            <div class="item c1">
                                            <%# Eval("TRN_DATE")%></div>
                                            <div class="item c5">
                                            <%# Eval("PRTY_NAME")%></div>
                                            
                                       
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items<%# Container.ItemsCount> 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                </cc2:ComboBox> 
                
                
                <cc2:ComboBox ID="ddlJobCard" runat="server" TabIndex="1" Width="100%" MenuWidth="600"
                                    AutoPostBack="true" CssClass="SmallFont" EnableLoadOnDemand="true" OpenOnFocus="true"
                                    Height="200px" EnableVirtualScrolling="true" 
                                     EmptyText="Select Job Card No" 
                            onloadingitems="ddlJobCard_LoadingItems" onselectedindexchanged="ddlJobCard_SelectedIndexChanged"  
                              >
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Job Card No
                                        </div>
                                       <%-- <div class="header c1">
                                            Cust Req NO</div>
                                        <div class="header c2">
                                            Pa No</div>--%>
                                         
                                        <div class="header c1">
                                             Batch Qty
                                        </div>
                                         <div class="header c1">
                                           Shade Code
                                        </div>
                                        <div class="header c1">
                                           Party Code
                                        </div>
                                       
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <%# Eval("BATCH_CODE")%></div>
                                        <%--<div class="item c1">
                                            <%# Eval("CUST_REQ_NO")%></div>
                                        <div class="item c2">
                                           <%# Eval("PA_NO")%></div>--%>
                                        <div class="item c1">
                                            <%# Eval("TRN_QTY")%></div>
                                        <div class="item c1">
                                            <%# Eval("SHADE_CODE")%></div>
                                        <div class="item c5">
                                            <%# Eval("PRTY_NAME")%></div>
                                       
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items<%# Container.ItemsCount> 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                </cc2:ComboBox> 
                
                
                    </td>
                    <td class="tdRight" width="16%">
                        <asp:Label ID="lblDE" runat="server" Text="DE" Width="100%" Font-Bold="true"></asp:Label>
                    </td>
                    <td class="tdLeft" width="16%">
                       <asp:TextBox ID="txtDE" runat="server" Width="100%" CssClass="TextBoxNo"></asp:TextBox>
                    </td>
                    
                    
                    
                     <td class="tdRight" width="16%">
                     
                      <asp:Label ID="lblGrade" runat="server" Text="MATCHING GRADE" Width="100%" Font-Bold="true"></asp:Label>
                        
                    </td>
                    <td class="tdLeft" width="20%">
                    
                     <asp:DropDownList ID="dllGrade" runat="server" Width="30%" CssClass="TextBoxNo  SmallFont" >
                          <asp:ListItem>A</asp:ListItem>
                          <asp:ListItem>B</asp:ListItem>
                          <asp:ListItem>C</asp:ListItem>
                          <asp:ListItem>D</asp:ListItem>
                        </asp:DropDownList>
                        
                    </td>
                    
                    
                       
                  
    </tr>
    
    <tr>
                    <td class="tdRight" width="16%">
                        <asp:Label ID="lblDMF" runat="server" Text="FASTNESS" Width="100%" Font-Bold="true"></asp:Label>
                    </td>
                    <td class="tdLeft" width="16%">
                        <asp:TextBox ID="txtDMF" runat="server" Width="100%" CssClass="TextBoxNo"></asp:TextBox>
                    </td>
                    
                    
                    <td class="tdRight" width="16%">
                        <asp:Label ID="lblOIL_FR" runat="server" Text="OIL FR" Width="100%" Font-Bold="true"></asp:Label>
                    </td>
                    <td class="tdLeft" width="16%">
                        <asp:TextBox ID="txtOILFR" runat="server" Width="100%" CssClass="TextBoxNo"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="16%">
                       <asp:Label ID="lblRC" runat="server" Text="RC" Width="100%" Font-Bold="true"  Visible="false"></asp:Label>
                    </td>
                    <td class="tdLeft" width="20%" >
                       <asp:TextBox ID="txtRC" runat="server" Width="100%" CssClass="TextBoxNo"  Visible="false"></asp:TextBox>
                    </td>
    
    </tr>
    <tr>
                    
                    <td class="tdRight" width="16%">
                        <asp:Label ID="lblRelease" runat="server" Text="Release" Width="100%" Font-Bold="true" ></asp:Label>
                    </td>
                    <td class="tdLeft" width="16%">
                        <asp:DropDownList ID="ddlRelease" runat="server" Width="30%" CssClass="TextBoxNo  SmallFont">
                        <asp:ListItem Text="Approved" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Not Approved" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Partially Approved" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    
                    
                   <td class="tdRight" width="16%">
                        <asp:Label ID="lblREMARK" runat="server" Text="REMARK :" Width="100%" Font-Bold="true"></asp:Label>
                    </td>
                    <td class="tdLeft" width="16%">
                        <asp:TextBox ID="txtRemark" runat="server" Width="100%" CssClass="TextBoxNo"></asp:TextBox>
                    </td>
                 
                 
                 
                 </tr></table>
                 
                 </td>     
    </tr>
    
   
    <tr>
   <td align="left" class="td" width="100%">
     <asp:GridView ID="gvJobCardApproval" CssClass="SmallFont" runat="server" AllowSorting="True"
       AutoGenerateColumns="False" OnRowDataBound="gvJobCardApproval_RowDataBound" Width="100%" >
                <Columns>
                    <asp:TemplateField HeaderText="Job Code" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                       <asp:Label ID="lblBATCH_CODE" runat="server" ToolTip='<%# Bind("BATCH_CODE") %>' Text='<%# Bind("BATCH_CODE") %>'
                            CssClass="SmallFont LabelNo" ></asp:Label>
                          <asp:Label ID="lblCOMP_CODE" runat="server" Text='<%# Bind("COMP_CODE") %>' Visible="false"></asp:Label>
                           <asp:Label ID="lblBRANCH_CODE" runat="server" Text='<%# Bind("BRANCH_CODE") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lblYEAR" runat="server" Text='<%# Bind("YEAR") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="labelNo SmallFont" Wrap="true" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="BATCH_DATE" HeaderText="Job Date" DataFormatString="{0:dd/MM/yyyy}">
                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    
                  <asp:BoundField DataField="GREY_LOT_NO" HeaderText=" Grey Lot No">
                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LAB_DIP_NO" HeaderText=" LAB DIP NO" Visible="false">
                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CUST_REQ_NO" HeaderText=" Order No" Visible="false" >
                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PA_NO" HeaderText="PA No" Visible="false" >
                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MACHINE_CODE" HeaderText="Machine Code" Visible="false">
                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                     <asp:BoundField DataField="MACHINE_MAKE" HeaderText="Machine Name" Visible="false">
                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                     <asp:BoundField DataField="SPRINGS" HeaderText="Batch Cheese" Visible="true" >
                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                     <asp:BoundField DataField="LOT_SIZE" HeaderText="Batch Qty" Visible="true" >
                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MACHINE_CAPACITY" HeaderText="Machine Volumn" Visible="false"  > 
                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField> 
                     <asp:BoundField DataField="PRTY_CODE" HeaderText="Party Code" Visible="false" > 
                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                     <asp:BoundField DataField="PRTY_NAME" HeaderText="Party" > 
                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                     <asp:BoundField DataField="ARTICLE_CODE" HeaderText="Yarn Code" Visible="false" > 
                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                     <asp:BoundField DataField="YARN_DESC" HeaderText="Yarn" > 
                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                     <asp:BoundField DataField="SHADE_CODE" HeaderText="Shade" > 
                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                  
                <asp:TemplateField HeaderText="Confirm Date">
                    <ItemTemplate>
                        <asp:TextBox ID="txtConfirmDate" runat="server" ReadOnly="true" Width="60px" Text='<%# Bind("CONF_DATE") %>'
                            CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Confirm By">
                    <ItemTemplate>
                        <asp:TextBox ID="txtConfirmBy" runat="server" Width="60px" ReadOnly="true" Text='<%# Bind("CONF_BY") %>'
                            ToolTip='<%# Bind("CONF_BY") %>' CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                    </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText=" View Chemical " Visible="false">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnViewJobCardTRN" runat="server" Text="View Chemical "></asp:LinkButton>
                    <asp:Panel ID="pnlJobCardTRN" runat="server"  Width="520px" BackColor="Beige" BorderWidth="2px"
                    Height="140px" ScrollBars="Auto">
                    <asp:GridView ID="grdJobCardTRN" runat="server" AutoGenerateColumns="False" Width="520px"
                        CssClass="SmallFont" Height="140px" ShowFooter="true" >
                        <Columns>
                 <asp:TemplateField HeaderText="Sr. No.">
                 <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                 <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" ></ItemStyle>
                <ItemTemplate>
                <%# Container.DataItemIndex+1 %>
                </ItemTemplate> 
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Job Code" Visible="false">
                 <ItemTemplate>
                  <asp:Label ID="lblBATCH_CODE" runat="server" ToolTip='<%# Bind("BATCH_CODE") %>' Text='<%# Bind("BATCH_CODE") %>'
                    CssClass="SmallFont LabelNo" ></asp:Label>
                 </ItemTemplate>
                 </asp:TemplateField>
                     <asp:BoundField DataField="DYE_PROCESS" HeaderText="Dye Process">
                     <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                     </asp:BoundField>
                        <asp:BoundField DataField="ITEM_CODE" HeaderText="Item&nbsp;Code" Visible="false">
                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ITEM_DESC" HeaderText="Item&nbsp;Description">
                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                        </asp:BoundField>
                        <asp:BoundField DataField="UOM_OF_UNIT" HeaderText="UOM">
                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                        </asp:BoundField>
                        <asp:BoundField DataField="QTY" HeaderText="Qty" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left">
                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="LabelNo SmallFont" VerticalAlign="Top" />
                        </asp:BoundField>
                        <asp:BoundField DataField="LOT_SIZE" HeaderText="Lot Size" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left">
                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="LabelNo SmallFont" VerticalAlign="Top" />
                        </asp:BoundField>
                        <asp:BoundField DataField="MACHINE_CAPACITY" HeaderText="Machine Volumn" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left">
                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="LabelNo SmallFont" VerticalAlign="Top" />
                        </asp:BoundField>
                       <asp:BoundField DataField="TRN_QTY" HeaderText="Item Qty(Kg)" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left">
                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="LabelNo SmallFont" VerticalAlign="Top" />
                        </asp:BoundField>
                        <asp:BoundField DataField="YEAR" HeaderText="Year" Visible="false" HeaderStyle-HorizontalAlign="Left"  ItemStyle-HorizontalAlign="Left">
                         <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="LabelNo SmallFont" VerticalAlign="Top" />
                        </asp:BoundField>
                    </Columns>
                    <RowStyle CssClass="SmallFont" Width="98%" />
                       <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="BlueViolet" />
                      </asp:GridView>
                   </asp:Panel>
               <cc1:HoverMenuExtender ID="hmeJobCardTRN" runat="server" PopupPosition="Left" PopupControlID="pnlJobCardTRN"
                  TargetControlID="lbtnViewJobCardTRN">
                    </cc1:HoverMenuExtender>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
            </asp:TemplateField>
            
            
            <asp:TemplateField HeaderText=" View Dyes" Visible="false" >
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnViewJobCardDyes" runat="server" Text="View Dyes "></asp:LinkButton>
                    <asp:Panel ID="pnlJobCardDYES" runat="server"  Width="520px" BackColor="Beige" BorderWidth="2px"
                    Height="140px" ScrollBars="Auto">
                    <asp:GridView ID="grdJobCardDYES" runat="server" AutoGenerateColumns="true" Width="520px"
                        CssClass="SmallFont" Height="140px" ShowFooter="true" OnRowDataBound="grdJobCardDYES_RowDataBound">
                        <Columns>
                 <asp:TemplateField HeaderText="Sr. No.">
                 <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                 <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" ></ItemStyle>
                <ItemTemplate>
                <%# Container.DataItemIndex+1 %>
                </ItemTemplate> 
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Job Code" Visible="false">
                 <ItemTemplate>
                  <asp:Label ID="lblBATCH_CODE" runat="server" ToolTip='<%# Bind("BATCH_CODE") %>' Text='<%# Bind("BATCH_CODE") %>'
                    CssClass="SmallFont LabelNo" ></asp:Label>
                 </ItemTemplate>
                 </asp:TemplateField>
                     <asp:BoundField DataField="DYE_PROCESS" HeaderText="Dye Process">
                     <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                     </asp:BoundField>
                        <asp:BoundField DataField="DYE_NAME" HeaderText="Dyes&nbsp;Code" Visible="false">
                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DYE_DTL" HeaderText="Dyes&nbsp;Description">
                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                        </asp:BoundField>
                       
                        <asp:BoundField DataField="DOSE" HeaderText="DOSE%" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left">
                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="LabelNo SmallFont" VerticalAlign="Top" />
                        </asp:BoundField>
                        <asp:BoundField DataField="LOT_SIZE" HeaderText="Lot Size" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left">
                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="LabelNo SmallFont" VerticalAlign="Top" />
                        </asp:BoundField>
                       
                       <asp:BoundField DataField="TRN_QTY" HeaderText="Item Qty(Kg)" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left">
                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="LabelNo SmallFont" VerticalAlign="Top" />
                        </asp:BoundField>
                        <asp:BoundField DataField="YEAR" HeaderText="Year" Visible="false" HeaderStyle-HorizontalAlign="Left"  ItemStyle-HorizontalAlign="Left">
                         <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="LabelNo SmallFont" VerticalAlign="Top" />
                        </asp:BoundField>
                    </Columns>
                    <RowStyle CssClass="SmallFont" Width="98%" />
                       <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="BlueViolet" />
                      </asp:GridView>
                   </asp:Panel>
               <cc1:HoverMenuExtender ID="hmeJobCardDYES" runat="server" PopupPosition="Left" PopupControlID="pnlJobCardDYES"
                  TargetControlID="lbtnViewJobCardDYES">
                    </cc1:HoverMenuExtender>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
            </asp:TemplateField>
            
            
        </Columns>
                <RowStyle CssClass="SmallFont" Width="98%" />
                <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
            </asp:GridView>
        </td>
    </tr>
    
    
</table>
</ContentTemplate>
</asp:UpdatePanel>