<%@ Page Language="C#" MasterPageFile="~/CommonMaster/EMPMaster.master" AutoEventWireup="true"
    CodeFile="Pending_PO.aspx.cs" Inherits="Module_Inventory_Queries_Pending_PO"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .item
        {
            position: relative !important;
            display: -moz-inline-stack;
        }
        .header
        {
            margin-left: 4px;
        }
        .c1
        {
            width: 80px;
        }
        .c4
        {
            margin-left: 4px;
            width: 300px;
        }
        .d1
        {
            width: 150px;
        }
        .d2
        {
            margin-left: 4px;
            width: 350px;
        }
        .d3
        {
            width: 80px;
        }
    </style>
    <style type="text/css">
        .TdBackVir
        {
        }
        .style2
        {
            text-align: right;
            width: 71px;
        }
        .style3
        {
            text-align: right;
            vertical-align: top;
            width: 111px;
        }
        .style5
        {
            width: 111px;
        }
        .style6
        {
            text-align: left;
            vertical-align: top;
            width: 128px;
        }
        .style7
        {
            width: 128px;
        }
        .style8
        {
            text-align: right;
            vertical-align: top;
            width: 71px;
        }
        .style9
        {
            text-align: right;
            vertical-align: top;
            width: 77px;
        }
        .style10
        {
            width: 77px;
        }
        .style11
        {
            width: 59px;
        }
        .style12
        {
            width: 119px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <%--<asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>--%>
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TxtFromDate"
                PopupPosition="TopLeft" Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TxtToDate"
                PopupPosition="TopLeft" Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <table width="100%"  class ="tContentArial">
                <tr>
                    <td align="left" colspan="10" width="100%">
                        <table align="left">
                            <tr>
                                <td width="41">
                                    <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                                        Width="41" Height="41" OnClick="imgbtnPrint_Click"></asp:ImageButton>
                                </td>
                                 <td>  
                              <asp:ImageButton ID="imgBtnExportExcel" runat="server" Width="48" Height="41" ToolTip="Print"
                            ImageUrl="~/CommonImages/export.png" onclick="imgBtnExportExcel_Click" ></asp:ImageButton>&nbsp;</td> 
                                <td width="41">
                                
                                
                                    <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                                        Width="41" Height="41" OnClick="imgbtnExit_Click"></asp:ImageButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td width="100%" align="center" class="TableHeader" colspan="10">
                        <b class="titleheading">Material Purchase Order Query</b>
                    </td>
                </tr>
                <tr>
                    <td  class="tdRight" >
                        Branch:
                    </td>
                    <td class="tdLeft" >
                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="gCtrTxt" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td  class="tdRight" >
                        Year:
                    </td>
                    <td class="tdLeft" >
                        <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" CssClass="gCtrTxt"
                            ValidationGroup="M1" Font-Size="8" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td  class="tdRight" >
                        From Date:
                    </td>
                    <td class="tdLeft" >
                        <asp:TextBox ID="TxtFromDate" runat="server" Width="100px" CssClass="SmallFont TextBox UpperCase"
                            OnTextChanged="TxtFromDate_TextChanged" AutoPostBack="True"></asp:TextBox>
                           
                           <cc1:MaskedEditExtender ID="MaskedEditTxtFromDate" runat="server" Mask="99/99/9999"
                            MaskType="Date" PromptCharacter="_" TargetControlID="TxtFromDate">
                        </cc1:MaskedEditExtender> 
                            
                    </td>
                    <td  class="tdRight" >
                        To Date:
                    </td>
                    <td class="tdLeft" >
                        <asp:TextBox ID="TxtToDate" CssClass="SmallFont TextBox UpperCase" Width="100px"
                            runat="server" OnTextChanged="TxtToDate_TextChanged1" AutoPostBack="True"></asp:TextBox>
                            
                            <cc1:MaskedEditExtender ID="MaskedEditTxtToDate" runat="server" Mask="99/99/9999"
                            MaskType="Date" PromptCharacter="_" TargetControlID="TxtToDate">
                        </cc1:MaskedEditExtender>
                    </td>
                  
                   <td  class="tdRight" >
                        Status :
                    </td>
                     <td class="tdLeft" >
                        <asp:DropDownList ID="ddlstatus" runat="server" Width="130px">
                            <asp:ListItem>All</asp:ListItem>
                            <asp:ListItem Value="0">Unconfirm</asp:ListItem>
                            <asp:ListItem Value="1">Confirm</asp:ListItem>
                            <asp:ListItem Value="3">Close</asp:ListItem>
                        </asp:DropDownList>
                    </td>  
                </tr>
                <tr>
                   <td  class="tdRight" >
                        Item :
                    </td>
                    <td class="tdLeft" >
                        <cc2:ComboBox ID="txtICODE" runat="server" CssClass="smallfont" EnableLoadOnDemand="True"
                            DataTextField="ITEM_CODE" DataValueField="ITEM_CODE" MenuWidth="650px" EnableVirtualScrolling="true"
                            OpenOnFocus="true" Visible="true" Height="200px" OnLoadingItems="txtICODE_LoadingItems"
                            EmptyText="------------All----------" Width="130px">
                            <HeaderTemplate>
                                <div class="header d1">
                                    ITEM CODE</div>
                                <div class="header d2">
                                    ITEM DESCRIPTION</div>
                                <div class="header d3">
                                    TYPE</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item d1">
                                    <%# Eval("ITEM_CODE")%></div>
                                <div class="item d2">
                                    <%# Eval("ITEM_DESC") %></div>
                                <div class="item d3">
                                    <%# Eval("ITEM_TYPE")%></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                    </td>
                    
                    <td  class="tdRight" >
                        Vendor :
                    </td>
                    <td class="tdLeft" >
                        <asp:DropDownList ID="ddlVendor" runat="server" Width="130px" CssClass="gCtrTxt"
                            DataTextField="PRTY_CODE" DataValueField="PRTY_NAME" OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged"
                            EmptyText="------------All----------">
                        </asp:DropDownList>
                    </td>
                    <td  class="tdRight" >
                        Po.NO From :
                    </td>
                     <td class="tdLeft" >
                        <asp:TextBox ID="txtPoFrom" runat="server" Width="100px" CssClass="SmallFont TextBoxNo" MaxLength="5"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FiltertxtPoFrom" runat="server"  TargetControlID="txtPoFrom"   FilterType="Custom, Numbers" />
                    </td>
                   <td  class="tdRight" >
                        To :
                    </td>
                     <td class="tdLeft" >
                        <asp:TextBox ID="txtPoTo" runat="server" Width="100px" CssClass="SmallFont TextBoxNo" MaxLength="5"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredtxtPoTo" runat="server"  TargetControlID="txtPoTo"   FilterType="Custom, Numbers" />
                    </td>
                  
                    <td>
                    </td>
                    <td class="tdLeft" >
                        <asp:Button ID="btnGetRecord" runat="server" OnClick="btnGetRecord_Click" Text="GetRecords" CssClass="AButton" />
                    </td>
                </tr>
                <tr>
                    <td colspan="10" width="100%" class="td">
                        <b>Total Records : &nbsp;&nbsp;</b><asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="10">
                        <asp:GridView ID="gvReportDisplayGrid" runat="server" AutoGenerateColumns="False"  
                            AllowPaging="True" AllowSorting="True" Font-Size="7pt" CellPadding="3" GridLines="Vertical"
                            Width="100%"  PagerStyle-HorizontalAlign="Left" OnPageIndexChanging="gvReportDisplayGrid_PageIndexChanging"
                            EmptyDataText="No Record Found" PageSize="15" 
                            OnRowDataBound="gvReportDisplayGrid_RowDataBound" BackColor="White" 
                            BorderColor="#999999" BorderStyle="None" BorderWidth="1px">
                            <FooterStyle Width="100%" BackColor="#CCCCCC" 
                                ForeColor="Black" />
                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                            <Columns>
                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="3%">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Branch " HeaderStyle-HorizontalAlign="Left" DataField="BRANCH_NAME"
                                    ItemStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="5%" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Company" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCompCode1" Text='<%#Eval("COMP_CODE") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="5%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branch " HeaderStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBranchCode" Text='<%#Eval("BRANCH_CODE") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="5%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branch Name" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBranch" Text='<%#Eval("BRANCH_NAME") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="7%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Year" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPOYear" Text='<%#Eval("YEAR") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="4%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PO Type" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="LblPOType" Text='<%#Eval("PO_TYPE") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="3%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PO No." HeaderStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="LblPOno" Text='<%#Eval("PO_NUMB") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle Width="3%" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PO Date" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="LblPODate" Text='<%#Eval("PO_DATE") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="5%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vendor" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="LblVendor" Text='<%#Eval("PRTY_CODE") %>' ToolTip='<%#Eval("PRTY_NAME") %>'
                                            runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="4%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approval Date" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="LblPOconfdt" Text='<%#Eval("CONF_DATE") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="3%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Description" ItemStyle-Width="250px" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="LblItemDesc" Text='<%#Eval("ITEM_DESC") %>' ToolTip='<%#Eval("ITEM_CODE") %>'
                                            runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="25%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Code" ItemStyle-Width="250px" HeaderStyle-HorizontalAlign="Left"
                                    Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="LblItemCode" Text='<%#Eval("ITEM_CODE") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="25%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UOM" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="LblUOM" Text='<%#Eval("UOM") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="3%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Basic Rate" HeaderStyle-HorizontalAlign="Right" >
                                    <ItemTemplate>
                                        <asp:Label ID="LblBasicRate" Text='<%# Eval("Basic_RATE","{0:N2}").ToString()%>'
                                            runat="server" ForeColor="#009999"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle Width="3%" HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Final Rate" HeaderStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="LblFinalRate" Text='<%# Eval("FINAL_RATE","{0:N2}").ToString()%>'
                                            runat="server" ForeColor="#3333FF"></asp:Label>
                                 <asp:Panel ID="pnlShowHover" runat="server" Width="400px" BackColor="Beige" BorderWidth="2px"
                                            Height="60px" ScrollBars="Auto">
                                            <asp:GridView ID="grdTaxDetail" runat="server" Width="400px" CssClass="SmallFont"
                                                AutoGenerateColumns="False" Height="60px">
                                                <Columns>
                                                    <asp:BoundField DataField="COMPO_SL" HeaderText="SR NO." HeaderStyle-Font-Size="8">
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="COMPO_TYPE" HeaderText="TAX TYPE" HeaderStyle-Font-Size="8">
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="COMPO_CODE" HeaderText="TAX CODE" HeaderStyle-Font-Size="8">
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="BASE_COMPO_CODE" HeaderText="CALC ON" HeaderStyle-Font-Size="8">
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="RATE" HeaderText="RATE(%)" HeaderStyle-Font-Size="8">
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <RowStyle CssClass="SmallFont" Width="98%" />
                                                <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="BlueViolet" />
                                            </asp:GridView>
                                        </asp:Panel>
                                        <cc1:HoverMenuExtender ID="HoverMenuExtender1" runat="server" TargetControlID="LblFinalRate"
                                            PopupControlID="pnlShowHover" PopupPosition="Left" PopDelay="10">
                                        </cc1:HoverMenuExtender>
                               
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle Width="3%" HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order Qty." HeaderStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="LblOrderQty" Text='<%#Eval("ORD_QTY") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle Width="3%" HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Recpt. Qty." HeaderStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="LblRecptQty" Text='<%#Eval("QTY_RCPT") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle Width="3%" HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Balance Qty." HeaderStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="LblBalanceQty" Text='<%#Eval("bal_qty") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle Width="3%" HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="LblStatus" Text='<%#Eval("STATUS") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="3%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                     
                     
                     
                       <asp:TemplateField HeaderText="Delivery&nbsp;Date">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="Idn_Adjd" runat="server" CommandArgument='<%#Bind("ITEM_CODE") %>'
                                                                    Text="Delivery Dates" CommandName="DEL"></asp:LinkButton>
                                                                <asp:Panel ID="IdnPaneld" runat="server" BackColor="White">
                                                                    <asp:GridView runat="server" ID="Idn_gridd" AutoGenerateColumns="false" CssClass="SmallFont">
                                                                        <RowStyle CssClass="SmallFont" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="S&nbsp;No." ItemStyle-VerticalAlign="top" ItemStyle-Width="25px"
                                                                                HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                                                <ItemTemplate>
                                                                                    <%#Container.DataItemIndex+1 %>
                                                                                </ItemTemplate>
                                                                                <ItemStyle VerticalAlign="Top" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="QTY" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="txtQTY" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("QUANTITY") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Delivery&nbsp;Date" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDeliveryDate" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("DELIVERY_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <RowStyle CssClass="SmallFont" />
                                                                        <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                                <cc1:HoverMenuExtender ID="idnHoverd" runat="server" PopupControlID="IdnPaneld" PopupPosition="Left"
                                                                    TargetControlID="Idn_Adjd">
                                                                </cc1:HoverMenuExtender>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                     
                     
                            </Columns>
                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#000084"
                                ForeColor="White" Font-Bold="True" />
                            <AlternatingRowStyle BackColor="#DCDCDC" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
