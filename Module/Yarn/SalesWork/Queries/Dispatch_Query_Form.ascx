<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Dispatch_Query_Form.ascx.cs" Inherits="Module_Yarn_SalesWork_Queries_Dispatch_Query_Form" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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

    .c5
    {
        margin-left: 4px;
        width: 390px;
    }

    .d1
    {
        width: 150px;
    }

    .d2
    {
        margin-left: 4px;
        width: 200px;
    }

    .d3
    {
        width: 100px;
    }
</style>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
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
                                <%--<asp:ImageButton ID="imgBtnExportExcel" runat="server" Width="48" Height="41" ToolTip="Excel Report"
                            ImageUrl="~/CommonImages/export.png" onclick="imgBtnExportExcel_Click" TabIndex="11" ></asp:ImageButton>--%>&nbsp;</td>

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
                    <span class="titleheading"><strong>Dispatch Query Form</strong></span>
                </td>
            </tr>
            <tr>

                 <td class="tdRight">From date:
                </td>
                <td class="tdLeft">
                    <asp:TextBox ID="TxtFromDate" runat="server" CssClass="SmallFont TextBox UpperCase"
                        OnTextChanged="TxtFromDate_TextChanged" Width="140px" AutoPostBack="True" TabIndex="1"></asp:TextBox>
                </td>
                <td class="tdRight">To Date:
                </td>
                <td class="tdLeft">
                    <asp:TextBox ID="TxtToDate" runat="server" CssClass="SmallFont TextBox UpperCase"
                        Width="140px" TabIndex="2"></asp:TextBox>
                </td>
                <td align="right"><%--Order Type--%>
                </td>
                <td>
                    <asp:DropDownList ID="ddlBusinessType" runat="server" AutoPostBack="true"
                        CssClass="gCtrTxt" Width="160px"  Visible="false">
                    </asp:DropDownList>
                </td>
                <td class="tdRight"><%--Year:--%>
                </td>
                <td>
                    <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" CssClass="gCtrTxt"
                        Font-Size="8" Width="160px"  Visible="false">
                    </asp:DropDownList>
                </td>
               
            </tr>
            <tr>
                <td align="right">Quality Code :
                </td>
                <td>
                    <cc2:ComboBox ID="txtYarn" runat="server" CssClass="smallfont" Width="161px" EnableLoadOnDemand="True"
                        DataTextField="YARN_CODE" DataValueField="ASS_YARN_DESC" MenuWidth="550px" EnableVirtualScrolling="true"
                        OpenOnFocus="true" Visible="true" Height="200px" OnLoadingItems="txtYCODE_LoadingItems" AutoPostBack="true"
                        EmptyText="" TabIndex="2">
                        <HeaderTemplate>
                            <div class="header c2">
                                Quality Code</div>
                            <div class="header d1">
                                Quality Description</div>
                                <%-- <div class="header c1">
                                Cust.Req.No</div>--%>
                          
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="item c2">
                                <%# Eval("YARN_CODE")%></div>
                            <div class="item d4">
                                <%# Eval("ASS_YARN_DESC")%></div>
                                <%--<div class="item d2">
                                <%# Eval("ORDER_NO")%></div>--%>
                           
                        </ItemTemplate>
                        <FooterTemplate>
                            Displaying items
                                 <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                 <%# Container.ItemsCount %>.
                        </FooterTemplate>
                    </cc2:ComboBox>
                </td>
            </tr>

            <tr>
                <td></td>
                <td style="font-size: large;">
                    <b>Total Records : 
                    <asp:Label ID="lblTotalRecord" runat="server"></asp:Label></b>
                </td>
                <%-- <td align="right" colspan="3">
                    <asp:UpdateProgress ID="UpdateProgress431" runat="server">
                        <ProgressTemplate>
                            <h3>
                                Loading...
                            </h3>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </td>--%>
                <td colspan="5" align="Left">
                    <asp:Button ID="btnGetReport" runat="server" Text="Get Record" OnClick="btnGetReport_Click" CssClass="AButton" TabIndex="10" />
                </td>
                <td colspan="6"></td>

            </tr>
            <tr>
                <td class="td tContentArial" colspan="8" >
                    <asp:Panel ID="pnlShowHover" runat="server" Width="100%" ScrollBars="Auto" Height="300px" HorizontalAlign="Center" >
                    <asp:GridView ID="grdLabDipSubmission" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                        CssClass="SmallFont" Width="70%" TabIndex="21" BackColor="White"
                        OnPageIndexChanging="Grid1_PageIndexChanging" HorizontalAlign="Center">
                        <Columns>
                            <asp:TemplateField HeaderText="Order No" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblOrderNo" runat="server" Font-Size="X-Small" Text='<%# Bind("ORDER_NO") %>'
                                        CssClass="LabelNo"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="10px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Order Date" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblOrderDate" runat="server" Font-Size="X-Small" Text='<%# Bind("ORDER_DATE" ,"{0:dd/MM/yyyy}") %>'
                                        CssClass="LabelNo"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="10px" HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Challan Date" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblChallanDate" runat="server" Font-Size="X-Small" Text='<%# Bind("TRN_DATE" ,"{0:dd/MM/yyyy}") %>'
                                        CssClass="LabelNo"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="10px" HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Challan No" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblChallanNo" runat="server" Font-Size="X-Small" Text='<%# Bind("TRN_NUMB") %>'
                                        CssClass="LabelNo"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="10px" HorizontalAlign="Center" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Party Name" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblPartyName" runat="server" Font-Size="X-Small" Text='<%# Bind("PRTY_NAME") %>'
                                        CssClass="LabelNo"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="100px" HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Yarn Description" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblYarnDesc" runat="server" Font-Size="X-Small" Text='<%# Bind("YARN_DESC") %>'
                                        CssClass="LabelNo"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="100px" HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Shade Code" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblShadeCode" runat="server" Font-Size="X-Small" Text='<%# Bind("SHADE_CODE") %>'
                                        CssClass="LabelNo"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="10px" HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Net Weight" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblNetWeight" runat="server" Font-Size="X-Small" Text='<%# Bind("TRN_QTY") %>'
                                        CssClass="LabelNo"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="10px" HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="No Of Cartons" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblCartons" runat="server" Font-Size="X-Small" Text='<%# Bind("CARTONS") %>'
                                        CssClass="LabelNo"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="10px" HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Cheese" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblCheese" runat="server" Font-Size="X-Small" Text='<%# Bind("NO_OF_UNIT") %>'
                                        CssClass="LabelNo"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="10px" HorizontalAlign="Left" />
                            </asp:TemplateField>



                        </Columns>

                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <RowStyle VerticalAlign="Top" />
                        <HeaderStyle CssClass="HeaderStyle SmallFont titleheading" BackColor="#336699" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                     </asp:Panel>
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
    </ContentTemplate>

</asp:UpdatePanel>
