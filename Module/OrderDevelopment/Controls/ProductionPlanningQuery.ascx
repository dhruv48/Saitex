<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductionPlanningQuery.ascx.cs" Inherits="Module_OrderDevelopment_Controls_ProductionPlanningQuery" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc5" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc6" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc7" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style type="text/css">
    .item {
        position: relative !important;
        display: -moz-inline-stack;
    }


    .header {
        margin-left: 4px;
    }

    .c1 {
        width: 80px;
    }

    .c4 {
        margin-left: 4px;
        width: 300px;
    }

    .c5 {
        margin-left: 4px;
        width: 390px;
    }

    .d1 {
        width: 150px;
    }

    .d2 {
        margin-left: 4px;
        width: 200px;
    }

    .d3 {
        width: 100px;
    }
</style>

<table align="left" class=" td tContentArial" width="100%">
    <tr>
        <td class="td" colspan="8">
            <table>
                <tr>
                    <td>
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                                    OnClick="imgbtnPrint_Click" ></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgBtnExportExcel" runat="server" Width="48" Height="41" ToolTip="Excel Report"
                            ImageUrl="~/CommonImages/export.png" OnClick="imgBtnExportExcel_Click" TabIndex="11"></asp:ImageButton>&nbsp;</td>

                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                            OnClick="imgbtnClear_Click" TabIndex="12"></asp:ImageButton>
                    </td>
                    <td align="center" valign="top">
                        <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                            ToolTip="Exit" Width="48" OnClick="imgbtnExit_Click" TabIndex="13" />
                    </td>
                    <td>
                        <%--<asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                                    ToolTip="Help" Width="48" />--%>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader td" colspan="8">
            <span class="titleheading"><strong>Production Planning Query & Report</strong></span>
        </td>
    </tr>
    <tr>

        <td class="tdRight">Year:
        </td>
        <td>
            <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" CssClass="gCtrTxt"
                Font-Size="8" Width="160px" TabIndex="2" Enabled="false">
            </asp:DropDownList>
        </td>
        <td class="tdRight">From date:
        </td>
        <td class="tdLeft">
            <asp:TextBox ID="TxtFromDate" runat="server" CssClass="SmallFont TextBox UpperCase"
                OnTextChanged="TxtFromDate_TextChanged" Width="150px" AutoPostBack="True" TabIndex="3"></asp:TextBox>
        </td>
        <td class="tdRight">To Date:
        </td>
        <td class="tdLeft">
            <asp:TextBox ID="TxtToDate" runat="server" CssClass="SmallFont TextBox UpperCase"
                Width="140px" TabIndex="4"></asp:TextBox><%--OnTextChanged="TxtToDate_TextChanged" AutoPostBack="True"--%>
        </td>

    </tr>
    <tr>
        <td align="right">Customer Request No:
        </td>
        <td>

            <cc2:ComboBox ID="cmbCustomer" runat="server" CssClass="smallfont" Width="161px" EnableLoadOnDemand="True"
                DataTextField="ORDER_No" DataValueField="ORDER_No" MenuWidth="300px" EnableVirtualScrolling="true"
                OpenOnFocus="true" Visible="true" Height="200px" OnLoadingItems="cmbCustomer_LoadingItems"
                EmptyText="" TabIndex="5">
                <HeaderTemplate>
                    <div class="header d1">
                        Customer Request No
                    </div>
                    <div class="header d1">
                        Order No
                    </div>

                </HeaderTemplate>
                <ItemTemplate>
                    <div class="item d1">
                        <%# Eval("CUST_REQ_NO ")%>
                    </div>
                    <div class="item d1">
                        <%# Eval("ORDER_NO ")%>
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
        <td align="right">Quality Code :
        </td>
        <td>
            <cc2:ComboBox ID="txtYarn" runat="server" CssClass="smallfont" Width="161px" EnableLoadOnDemand="True"
                DataTextField="ARTICAL_CODE" DataValueField="ASS_YARN_DESC" MenuWidth="550px" EnableVirtualScrolling="true"
                OpenOnFocus="true" Visible="true" Height="200px" OnLoadingItems="txtYCODE_LoadingItems"
                EmptyText="" TabIndex="6">
                <HeaderTemplate>
                    <div class="header c1">
                        Quality Code
                    </div>
                    <div class="header c5">
                        Quality Description
                    </div>
                    <%-- <div class="header c1">
                                Cust. Req. No</div>--%>
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="item c1">
                        <%# Eval("ARTICAL_CODE")%>
                    </div>
                    <div class="item c5">
                        <%# Eval("ASS_YARN_DESC")%>
                    </div>
                    <%--<div class="item c1">
                                <%# Eval("ORDER_NO")%></div>--%>
                </ItemTemplate>
                <FooterTemplate>
                    Displaying items
                            <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                            out of
                            <%# Container.ItemsCount %>.
                </FooterTemplate>
            </cc2:ComboBox>
        </td>



    </tr>



    <tr>
        <td align="right">Customer Name:
                    
                    
        </td>
        <td>




            <cc2:ComboBox ID="cmbPartyCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                DataTextField="PRTY_CODE" DataValueField="PRTY_NAME" EmptyText="" Width="160px"
                MenuWidth="350px" Height="200px" TabIndex="9" OnLoadingItems="cmbPartyCode_LoadingItems"
                EnableVirtualScrolling="true">
                <HeaderTemplate>
                    <div class="header c1">
                        Code
                    </div>
                    <div class="header d2">
                        NAME
                    </div>
                    <%-- <div class="header d2">
                                            Address</div>--%>
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="item c1">
                        <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' />
                    </div>
                    <div class="item d2">
                        <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' />
                    </div>
                    <%-- <div class="item d2">
                                            <asp:Literal runat="server" ID="Container3" Text='<%# Eval("PRTY_ADD1") %>' /></div>--%>
                </ItemTemplate>
                <FooterTemplate>
                    Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                </FooterTemplate>
            </cc2:ComboBox>



        </td>
    </tr>
    <tr>
        <td></td>
        <td style="font-size: large;" colspan="3">
            <b>Total Records : 
                    <asp:Label ID="lblTotalRecord" runat="server"></asp:Label></b>
        </td>

        <td colspan="4" align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnGetReport" runat="server" Text="Get Report" OnClick="btnGetReport_Click" CssClass="AButton" TabIndex="10" />
        </td>
        <td colspan="6"></td>

    </tr>
    <tr>
        <td class="td tContentArial" colspan="8">
            <div style="overflow-x:scroll">
            <asp:GridView ID="grdLabDipSubmission" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                CssClass="SmallFont" Width="100%" TabIndex="21" BackColor="White"
                OnPageIndexChanging="Grid1_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="Year">
                        <ItemTemplate>
                            <asp:Label ID="lblYear" CssClass="LabelNo" Font-Size="Smaller" runat="server"
                                Text='<%# Bind("YEAR") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Order No">
                        <ItemTemplate>
                            <asp:Label ID="lblOrderNo" CssClass="LabelNo" Font-Size="Smaller" runat="server"
                                Text='<%# Bind("ORDER_NO") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="40px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Order Date" Visible="true">
                        <ItemTemplate>
                            <asp:Label ID="lblOrderDate" CssClass="LabelNo" Font-Size="Smaller" runat="server"
                                Text='<%# Bind("ORDER_DATE") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delivery Date" Visible="true">
                        <ItemTemplate>
                            <asp:Label ID="lblDelDate" CssClass="LabelNo" Font-Size="Smaller" runat="server"
                                Text='<%# Bind("DEL_DATE") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PI No">
                        <ItemTemplate>
                            <asp:Label ID="lblPI_NO" CssClass="LabelNo" Font-Size="Smaller" runat="server"
                                Text='<%# Bind("PI_NO") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Party Code">
                        <ItemTemplate>
                            <asp:Label ID="lblPartyCode" CssClass="LabelNo" Font-Size="Smaller" runat="server"
                                Text='<%# Bind("PRTY_CODE") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Party Name">
                        <ItemTemplate>
                            <asp:Label ID="lblPartyName" CssClass="LabelNo" Font-Size="Smaller" runat="server"
                                Text='<%# Bind("PRTY_NAME") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Shade Code">
                        <ItemTemplate>
                            <asp:Label ID="lblShade_Code" CssClass="LabelNo" Font-Size="Smaller" runat="server"
                                Text='<%# Bind("SHADE_CODE") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="40px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quality Code">
                        <ItemTemplate>
                            <asp:Label ID="lblQualityCode" runat="server" Font-Size="X-Small" Text='<%# Bind("QUALITY_CODE") %>'
                                CssClass="LabelNo"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="8px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quality Desc">
                        <ItemTemplate>
                            <asp:Label ID="lblQualityDesc" runat="server" Font-Size="X-Small" Text='<%# Bind("QUALITY_DESC") %>'
                                CssClass="LabelNo"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ass Yarn Code">
                        <ItemTemplate>
                            <asp:Label ID="lblAssYarnCode" runat="server" Font-Size="X-Small" Text='<%# Bind("ASS_YARN_CODE") %>'
                                CssClass="LabelNo"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="50px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="ASS_YARN_DESC" HeaderText="ass Yarn Desc" ItemStyle-Width="150px" />
                    <asp:TemplateField HeaderText="UOM" Visible="true">
                        <ItemTemplate>
                            <asp:Label ID="lblUOM" runat="server" Font-Size="X-Small" Text='<%# Bind("UOM") %>'
                                CssClass="LabelNo"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="20px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Order Quantity" Visible="true">
                        <ItemTemplate>
                            <asp:Label ID="lblORDQTY" runat="server" Font-Size="X-Small" Text='<%# Bind("ORD_QTY") %>'
                                CssClass="LabelNo"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Planned Quantity" Visible="true">
                        <ItemTemplate>
                            <asp:Label ID="lblPalnnedQty" runat="server" Font-Size="X-Small" Text='<%# Bind("PLAN_QTY") %>'
                                CssClass="LabelNo"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="ORDER_NO" HeaderText="Order No">
                        <ItemStyle HorizontalAlign="Center" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TRN_NUMB" HeaderText="TRN No">
                        <ItemStyle HorizontalAlign="Center" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MACHINE" HeaderText="Machine">
                        <ItemStyle HorizontalAlign="Center" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="QUANTITY" HeaderText="Quantity">
                        <ItemStyle HorizontalAlign="Center" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PROCESS" HeaderText="Process">
                        <ItemStyle HorizontalAlign="Center" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    
                    <asp:BoundField DataField="MACHINE_CAPACITY" HeaderText="Capacity">
                        <ItemStyle HorizontalAlign="Center" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                  
                </Columns>
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <RowStyle VerticalAlign="Top" />
                <HeaderStyle CssClass="HeaderStyle SmallFont titleheading" BackColor="#336699" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
                </div>
        </td>
    </tr>
    <tr>
        <td colspan="8"></td>
    </tr>
    <tr>
        <td colspan="8">
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupPosition="TopLeft"
                TargetControlID="TxtFromDate">
            </cc1:CalendarExtender>
            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupPosition="TopLeft"
                TargetControlID="TxtToDate">
            </cc1:CalendarExtender>
        </td>
    </tr>
</table>
