<%@ Page Language="C#" MasterPageFile="~/CommonMaster/EMPMaster.master" AutoEventWireup="true" CodeFile="FiberPendingPO.aspx.cs" Inherits="Module_Fiber_Reports_FiberPendingPO" %>

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
        width: 100px;
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
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TxtFromDate"
                PopupPosition="TopLeft" Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TxtToDate"
                PopupPosition="TopLeft" Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <table width="100%"  class ="td tContentArial">
                <tr>
                    <td align="left" colspan="10" width="100%">
                        <table align="left">
                            <tr>
                                <td width="41">
                                    <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                                        Width="41" Height="41" OnClick="imgbtnPrint_Click" Visible="false"></asp:ImageButton>
                                </td>
                                <td>  
<asp:ImageButton ID="imgBtnExportExcel" runat="server" Width="48" Height="41" ToolTip="Print"
                            ImageUrl="~/CommonImages/export.png" onclick="imgBtnExportExcel_Click" ></asp:ImageButton>&nbsp;</td> 


                                  <td valign="top" align="center">
                                <asp:ImageButton ID="imgbtnClear" OnClick="imgbtnClear_Click" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                    ToolTip="Clear" Height="41" Width="48"></asp:ImageButton>
                            </td>
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
                        <b class="titleheading">Poy Purchase Order Query</b>
                    </td>
                </tr>
                <tr>
                    <td  class="tdRight" >
                        Branch:
                    </td>
                    <td class="tdLeft" >
                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="gCtrTxt" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" Width="150px">
                        </asp:DropDownList>
                    </td>
                    <td  class="tdRight" >
                        Year:
                    </td>
                    <td class="tdLeft" >
                        <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" CssClass="gCtrTxt"
                            ValidationGroup="M1" Font-Size="8" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"  Width="150px">
                        </asp:DropDownList>
                    </td>
                    <td  class="tdRight" >
                        From Date:
                    </td>
                    <td class="tdLeft" >
                        <asp:TextBox ID="TxtFromDate" runat="server"  Width="150px" CssClass="SmallFont TextBox UpperCase"
                            OnTextChanged="TxtFromDate_TextChanged" AutoPostBack="True"></asp:TextBox>
                    </td>
                    <td  class="tdRight" >
                        To Date:
                    </td>
                    <td class="tdLeft" >
                        <asp:TextBox ID="TxtToDate" CssClass="SmallFont TextBox UpperCase"  Width="150px"
                            runat="server" OnTextChanged="TxtToDate_TextChanged1" AutoPostBack="True"></asp:TextBox>
                    </td>
                   <%-- <td style="text-align: right">
                        ShadeCode :</td>
                   <td>
                       <asp:TextBox ID="txtShadcode" runat="server" Width ="100px" CssClass="TextBox"></asp:TextBox>
                    </td>--%>
                      <td  class="tdRight" >
                        Status :
                    </td>
                     <td class="tdLeft" >
                        <asp:DropDownList ID="ddlstatus" runat="server"  Width="150px">
                            <asp:ListItem>All</asp:ListItem>
                            <asp:ListItem Value="0">Unconfirm</asp:ListItem>
                            <asp:ListItem Value="1">Confirm</asp:ListItem>
                            <asp:ListItem Value="3">Close</asp:ListItem>
                        </asp:DropDownList>
                    </td> 
                
                    
                </tr>
                <tr>
                  <td class="tdRight">
            Poy:
        </td>
        <td class="tdLeft">
           <cc2:ComboBox ID="ddlYarn" runat="server" AutoPostBack="True" CssClass="smallfont"
                                    DataTextField="FIBER_CODE" DataValueField="FIBER_CODE" EnableLoadOnDemand="true"
                                    MenuWidth="660" OnLoadingItems="Item_LOV_LoadingItems"
                                    EnableVirtualScrolling="true" OpenOnFocus="true"   Width="150px"
                TabIndex="9" Visible="true"
                                     EmptyText="---------All--------">
                                    <HeaderTemplate>
                                        <div class="header c4">
                                            Poy CODE</div>
                                        <div class="header c5">
                                            Poy DESCRIPTION</div>
                                        <div class="header c6">
                                            TYPE</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c4">
                                            <%# Eval("FIBER_CODE") %></div>
                                        <div class="item c5">
                                            <%# Eval("FIBER_DESC") %></div>
                                        <div class="item c6">
                                            <%# Eval("FIBER_CAT")%></div>
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
                        <asp:DropDownList ID="ddlVendor" runat="server"  Width="150px" CssClass="gCtrTxt"
                            DataTextField="PRTY_CODE" DataValueField="PRTY_NAME" OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged"
                            EmptyText="------------All----------">
                        </asp:DropDownList>
                    </td>
                    <td  class="tdRight" >
                        Po.NO From :
                    </td>
                     <td class="tdLeft" >
                        <asp:TextBox ID="txtPoFrom" runat="server"  Width="150px" CssClass="SmallFont TextBoxNo"></asp:TextBox>
                    </td>
                   <td  class="tdRight" >
                        To :
                    </td>
                     <td class="tdLeft" >
                        <asp:TextBox ID="txtPoTo" runat="server"  Width="150px" CssClass="SmallFont TextBoxNo"></asp:TextBox>
                    </td>
                  <td></td> <td class="tdLeft" >
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
                              <%--  <asp:TemplateField HeaderText="Year" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPOYear" Text='<%#Eval("YEAR") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="4%" HorizontalAlign="Left" />
                                </asp:TemplateField>--%>
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
                                        <asp:Label ID="LblVendor" Text='<%#Eval("PRTY_NAME") %>' ToolTip='<%#Eval("PRTY_CODE") %>'
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
                                <asp:TemplateField HeaderText="Poy Description" ItemStyle-Width="250px" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="LblItemDesc" Text='<%#Eval("FIBER_DESC") %>' ToolTip='<%#Eval("FIBER_CODE") %>'
                                            runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="25%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                               <%-- <asp:TemplateField HeaderText="SHADE CODE" ItemStyle-Width="250px" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="LblShadeCode" Text='<%#Eval("SHADE_CODE") %>' runat="server">
                                           </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="5%" HorizontalAlign="Left" />
                                </asp:TemplateField>--%>
                                
                                <asp:TemplateField HeaderText="Poy Code" ItemStyle-Width="250px" HeaderStyle-HorizontalAlign="Left"
                                    Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="LblYarnCode" Text='<%#Eval("FIBER_CODE") %>' runat="server"></asp:Label>
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
        </ContentTemplate>
        <Triggers><asp:PostBackTrigger  ControlID="imgBtnExportExcel"/></Triggers>
    </asp:UpdatePanel>
</asp:Content>
