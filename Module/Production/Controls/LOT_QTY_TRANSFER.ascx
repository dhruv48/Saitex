<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LOT_QTY_TRANSFER.ascx.cs" Inherits="Module_Production_Controls_LOT_QTY_TRANSFER" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
        margin-left: 4px;
        width: 80px;
    }
    .c3
    {
        margin-left: 4px;
        width: 300px;
    }
    .c4
    {
        margin-left: 4px;
        width: 200px;
    }
    .c5
    {
        width: 120px;
    }
    </style>
<%-- <asp:UpdatePanel ID="updPannel" runat="server">
 <ContentTemplate>--%>
 

<table class="tdMain" width="100%">
    <tr>
        <td width="100%" class="td">
            <table class="tContentArial">
                <tr>
                    <td id="tdSave" runat="server">
                        <asp:ImageButton ID="imgbtnSave" OnClick="imgbtnSave_Click" runat="server" ToolTip="Save"
                            ImageUrl="~/CommonImages/save.jpg" ValidationGroup="gg" Style="height: 41px">
                        </asp:ImageButton>
                    </td>
                    <td id="tdUpdate" runat="server" visible="false">
                        <asp:ImageButton ID="imgbtnUpdate" OnClick="imgbtnUpdate_Click" runat="server" ToolTip="Update"
                            ImageUrl="~/CommonImages/edit1.jpg" ValidationGroup="M1" ></asp:ImageButton>
                    </td>
                    <td id="tdDelete" runat="server" visible="false">
                        <asp:ImageButton ID="imgbtnDelete" OnClick="imgbtnDelete_Click" runat="server" ToolTip="Delete"
                            ImageUrl="~/CommonImages/del6.png" Enabled="false"></asp:ImageButton>
                    </td>
                    <td id="tdFind" runat="server" >
                        <asp:ImageButton ID="imgbtnFind" OnClick="imgbtnFind_Click" runat="server" ToolTip="Find"
                            ImageUrl="~/CommonImages/link_find.png"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" OnClick="imgbtnClear_Click" runat="server" ToolTip="Clear"
                            ImageUrl="~/CommonImages/clear.jpg"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnPrint" OnClick="imgbtnPrint_Click" runat="server" ToolTip="Print"
                            ImageUrl="~/CommonImages/link_print.png"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" OnClick="imgbtnExit_Click" runat="server" ToolTip="Exit"
                            ImageUrl="~/CommonImages/link_exit.png"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" OnClick="imgbtnHelp_Click" runat="server" ToolTip="Help"
                            ImageUrl="~/CommonImages/link_help.png"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader td" width="100%">
            <b class="titleheading">Yarn Production Lot Transfer</b>
        </td>
    </tr>
    <tr>
        <td valign="top" align="left" class="td" width="100%">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td SmallFont">
            <table width="100%">
                <tr>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label15" runat="server" Text="Trn Number : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtTRNNUMBer" runat="server" ValidationGroup="M1" Width="150px" TabIndex="1"
                            CssClass="TextBoxNo TextBoxDisplay SmallFont" ReadOnly="true"></asp:TextBox>
                        <cc2:ComboBox ID="ddlTRNNumber" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="ddlTRNNumber_LoadingItems" DataTextField="TRN_NUMB" DataValueField="TRN_NUMB"
                            OnSelectedIndexChanged="ddlTRNNumber_SelectedIndexChanged" Width="150px" Height="200px" EmptyText="Select Tran Number"
                            MenuWidth="300px">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Trn ID #</div>
                                <div class="header c2">
                                    Trn Date</div>
                                
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container4" Text='<%# Eval("TRN_NUMB") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Container6" Text='<%# Eval("TRN_DATE","{0:dd/MM/yyyy}") %>' /></div>                               
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                    </td>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label16" runat="server" Text="Shift : " CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                      <asp:DropDownList ID="ddlReceiptShift" CssClass="SmallFont" runat="server" TabIndex="2" Width="150px">
                        </asp:DropDownList>
                         </td>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label17" runat="server" Text="Trn Date :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                      
                           <asp:TextBox ID="txtMRNDate" runat="server" TabIndex="3" ValidationGroup="M1" Width="150px"
                            CssClass="TextBox TextBoxDisplay SmallFont" ReadOnly="false"></asp:TextBox>
                             <cc1:CalendarExtender ID="CE1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtMRNDate"></cc1:CalendarExtender>
                
                    </td>
                </tr>
            <tr>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label1" runat="server" Text="From Lot No : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                         <cc2:ComboBox ID="cmbPOITEM" runat="server" CssClass="SmallFont" EmptyText="From Lot Number"
                            AutoPostBack="True" EnableLoadOnDemand="true" MenuWidth="650px"  Width="150px" EnableVirtualScrolling="true"
                            OnLoadingItems="cmbPOITEM_LoadingItems" OnSelectedIndexChanged="cmbPOITEM_SelectedIndexChanged"
                            Height="200px"  TabIndex="4">
                            <HeaderTemplate>                          
                              
                                    <div class="header c2">
                                    Lot No</div>
                                     <div class="header c2">
                                    Merge No.</div>
                                <div class="header c2">
                                     Code</div>
                                <div class="header c4">
                                    Description</div>                              
                                  
                                <div class="header c1">
                                    Qty</div>
                             
                            </HeaderTemplate>
                            <ItemTemplate>                           
                             
                                    <div class="item c2">
                                    <asp:Literal runat="server" ID="Literal10" Text='<%# Eval("LOT_NUMBER") %>' /></div>
                                       <div class="item c2">
                                    <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("MERGE_NO") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("ARTICLE_CODE") %>' /></div>
                                <div class="item c4">
                                    <asp:Literal runat="server" ID="Container3" Text='<%# Eval("ARTICLE_DESC") %>' /></div>  
                            <div class="item c4">
                                    <asp:Literal runat="server" ID="Literal2" Text='<%# Eval("QTY_REM") %>' /></div>  
                                                                      
                                    
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox></td>
                    <td align="right" valign="top" width="17%">
                       <asp:Label ID="Label2" runat="server" Text="Merge No : " CssClass="LabelNo SmallFont"></asp:Label></td>
                    <td align="left" valign="top" width="17%">                    <asp:TextBox ID="txtMergeNO" runat="server" TabIndex="5" ValidationGroup="M1" Width="150px"
                            CssClass="TextBox TextBoxDisplay SmallFont" ></asp:TextBox>
                        
                        &nbsp;</td>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label3" runat="server" Text="Prod Qty :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                      
                           <asp:TextBox ID="txtProdQty" runat="server" TabIndex="6" ValidationGroup="M1" Width="150px"
                            CssClass="TextBox TextBoxDisplay SmallFont" ReadOnly="false"></asp:TextBox>
                            
                
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label4" runat="server" Text="To Lot No : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%"> 
                    
                    
                     <cc2:ComboBox ID="cmbTOPOITEM" runat="server" CssClass="SmallFont" EmptyText="To Lot Number"
                            EnableLoadOnDemand="true" MenuWidth="650px"  Width="150px" EnableVirtualScrolling="true" OnLoadingItems="cmbTOPOITEM_LoadingItems"
                          
                            Height="200px"  TabIndex="7">
                            <HeaderTemplate>                          
                              
                                    <div class="header c2">
                                    Lot No</div>
                                     <div class="header c2">
                                    Merge No.</div>
                                <div class="header c2">
                                     Code</div>
                                <div class="header c4">
                                    Description</div>                              
                                 
                             
                             
                            </HeaderTemplate>
                            <ItemTemplate>                           
                             
                                    <div class="item c2">
                                    <asp:Literal runat="server" ID="Literal10" Text='<%# Eval("LOT_NUMBER") %>' /></div>
                                       <div class="item c2">
                                    <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("MERGE_NO") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("ARTICLE_CODE") %>' /></div>
                                <div class="item c4">
                                    <asp:Literal runat="server" ID="Container3" Text='<%# Eval("ARTICLE_DESC") %>' /></div>  
                             
                                                                      
                                    
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                        &nbsp;</td>
                    <td align="right" valign="top" width="17%">
                       <asp:Label ID="Label5" runat="server" Text="Trn Qty : " CssClass="LabelNo SmallFont"></asp:Label></td>
                    <td align="left" valign="top" width="17%">
                    <asp:TextBox ID="txtTrnQty" runat="server" TabIndex="8" ValidationGroup="M1" Width="150px"
                            CssClass="TextBox  SmallFont" AutoPostBack="True" 
                            ontextchanged="txtTrnQty_TextChanged" ></asp:TextBox>
                        &nbsp;</td>
                    <td align="right" valign="top" width="17%">
                       
                    </td>
                    <td align="left" valign="top" width="15%">
                      
                
                    </td>
                </tr>
               </table>
        </td>
    </tr>
    </table>
 
<%--  </ContentTemplate>
<Triggers> 
<asp:PostBackTrigger ControlID="imgbtnSave" />
<asp:PostBackTrigger ControlID="imgbtnClear" />
<asp:PostBackTrigger ControlID="imgbtnExit" />
</Triggers>
 </asp:UpdatePanel>--%>