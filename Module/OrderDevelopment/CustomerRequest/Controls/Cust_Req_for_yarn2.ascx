<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Cust_Req_for_yarn2.ascx.cs"
    Inherits="Module_OrderDevelopment_CustomerRequest_Controls_Cust_Req_for_yarn2" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;}
    .style1
    {
        font-size: 8pt;
        font-weight: bold;
    }
    .header
    {
        margin-left: 2px;
    }
    .c1
    {
        width: 100px;
    }
    .c2
    {
        margin-left: 4px;
        width: 200px;
    }
    .d2
    {
        margin-left: 4px;
        width: 180px;
    }
    .d4
    {
        margin-left: 4px;
        width: 180px;
    }
</style>
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
        <table width="100%">
            <tr>
                <td>
                    <table align="left">
                        <tr>
                            <td id="tdPrint" runat="server" visible="false" align="left">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Width="48" Height="41" ToolTip="Print"
                                    ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click"></asp:ImageButton>
                            </td>
                             <td>  
                              <asp:ImageButton ID="imgBtnExportExcel" runat="server" Width="48" Height="41" ToolTip="Print"
                            ImageUrl="~/CommonImages/export.png" onclick="imgBtnExportExcel_Click" ></asp:ImageButton>&nbsp;</td> 
                            <td>
                            <td>
                                <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" ToolTip="Clear"
                                    ImageUrl="~/CommonImages/clear.jpg" OnClick="imgbtnClear_Click"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnExit" runat="server" Width="48" Height="41" ToolTip="Exit"
                                    ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr width="100%">
                <td align="center" class="TableHeader td">
                    <b class="titleheading">Customer Request For Yarn</b>
                </td>
            </tr>
        </table>
        <fieldset>
            <table width="100%">
                <tr>
                    <td align="right" style="width: 12%;">
                        Branch :
                    </td>
                    <td align="left" style="width: 12%;">
                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="SmallFont" TabIndex="1"
                            Width="100px">
                        </asp:DropDownList>
                    </td>
                    <td align="right" style="width: 12%;">
                        Party :
                    </td>
                    <td align="left" style="width: 12%;">
                        <cc2:ComboBox ID="txtPartyCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            DataTextField="PRTY_CODE" DataValueField="Address" EmptyText="Select Party" EnableVirtualScrolling="true"
                            Width="100px" MenuWidth="350px" Height="200px" OnLoadingItems="txtPartyCode_LoadingItems">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Code</div>
                                <div class="header c2">
                                    Name</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                    </td>
                    <td align="right" style="width: 12%;">
                        Article :
                    </td>
                    <td align="left" style="width: 12%;">
                        <cc2:ComboBox ID="ddlArticle" runat="server" AutoPostBack="True" CssClass="smallfont"
                            EnableLoadOnDemand="True" DataTextField="YARN_CODE" DataValueField="Combined"
                            EmptyText="Select Article" MenuWidth="350px" EnableVirtualScrolling="true" OpenOnFocus="true"
                            TabIndex="11" Visible="true" Height="200px" Width="100px" OnLoadingItems="ddlArticle_LoadingItems">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Article Code</div>
                                <div class="header c2">
                                    Description</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <%# Eval("YARN_CODE") %></div>
                                <div class="item c2">
                                    <%# Eval("YARN_DESC") %></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                    </td>
                    <td align="right" style="width: 12%;">
                        Cust No :
                    </td>
                    <td align="left" style="width: 12%;">
                        <asp:TextBox ID="txtCustNo" runat="server" TabIndex="4" Width="95px" CssClass="SmallFont UpperCase"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 12%;">
                        Shade Code :
                    </td>
                    <td align="left" style="width: 12%;">
                        <asp:TextBox ID="txtshadecode" runat="server" Width="95px" CssClass="SmallFont UpperCase"></asp:TextBox>
                    </td>
                    <td align="right" style="width: 12%;">
                        From Date:
                    </td>
                    <td align="left" style="width: 12%;">
                        <asp:TextBox ID="txtCRFrom" runat="server" TabIndex="6" Width="95px" CssClass="SmallFont"></asp:TextBox>
                        <cc1:CalendarExtender ID="ce1" runat="server" TargetControlID="txtCRFrom" PopupPosition="TopLeft"
                            Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                    </td>
                    <td align="right" style="width: 12%;">
                        To Date:
                    </td>
                    <td align="left" style="width: 12%;">
                        <asp:TextBox ID="txtCRTo" runat="server" TabIndex="7" Width="95px" CssClass="SmallFont"
                            AutoPostBack="true" OnTextChanged="txtCRTo_TextChanged"></asp:TextBox>
                        <cc1:CalendarExtender ID="ce2" runat="server" TargetControlID="txtCRTo" Format="dd/MM/yyyy"
                            PopupPosition="TopLeft">
                        </cc1:CalendarExtender>
                    </td>
                    <td align="right" style="width: 12%;">
                        Status :
                    </td>
                    <td align="left" style="width: 12%;">
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="SmallFont" TabIndex="8"
                            Width="100px">
                            <asp:ListItem Text="------ALL------" Value="SELECT" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="UNCONFIRMED" Value="0"></asp:ListItem>
                            <asp:ListItem Text="CONFIRMED" Value="1"></asp:ListItem>
                            <asp:ListItem Text="CLOSED" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="left" style="width: 4%;">
                        <asp:Button ID="btnShow" runat="Server" Text="Get Records" OnClick="btnShow_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <table width="100%">
            <tr>
                <td align="left" width="50%">
                    <b>
                        <asp:Label ID="Label1" runat="server" Text="Total Record : " CssClass="Label"></asp:Label>
                        <asp:Label ID="lblTotalRecord" runat="server" CssClass="Label"></asp:Label></b>
                </td>
                <td align="left" valign="top" width="50%" cssclass="Label">
                    <b>
                        <asp:UpdateProgress ID="UpdateProgress9" runat="server">
                            <ProgressTemplate>
                                Loading...</ProgressTemplate>
                        </asp:UpdateProgress>
                    </b>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td align="left" class="td" width="100%">
                    <div id="divPrint" runat="server">
                        <asp:GridView ID="grdCustomerRequest" runat="server" AutoGenerateColumns="False"
                            AllowPaging="True" PageSize="20" BorderStyle="Ridge" AllowSorting="True" CellPadding="3"
                            CssClass="smallfont" EmptyDataText="No Record Found" Font-Size="X-Small" ForeColor="#333333"
                            PagerStyle-HorizontalAlign="Left" Width="100%" OnPageIndexChanging="grdCustomerRequest_PageIndexChanging">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                            <Columns>
                                <asp:BoundField DataField="BRANCH_NAME" HeaderText="Branch" />
                                <asp:TemplateField HeaderText="Order Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblORDER_DATE" runat="server" HtmlEncode="false" Text='<%# Bind("ORDER_DATE", "{0:dd-MM-yyyy}") %>'
                                            CssClass="Label smallfont"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Wrap="true" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="CUSTNO" HeaderText="Cust No" />
                                <asp:TemplateField HeaderText="Vendor" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="LblVendor" Text='<%#Eval("PRTY_CODE") %>' ToolTip='<%#Eval("PRTY_NAME") %>'
                                            runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="4%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="CONSIGNEE_NAME" HeaderText="Consignee Name" />
                                <asp:BoundField DataField="TRANSPORTER_NAME" HeaderText="Transporter Name" />
                                <asp:BoundField DataField="AGENT_NAME" HeaderText="Agent" />
                                <asp:BoundField DataField="ARTICLE_NO" HeaderText="Article No" />
                                <asp:BoundField DataField="SHADE_FAMILY_CODE" HeaderText="Shade Family Code" Visible="false" />
                                <asp:BoundField DataField="SHADE_CODE" HeaderText="Shade Code" />
                                <asp:BoundField DataField="MATCHING_REFF" HeaderText="Lab Dip. No." />
                                <asp:BoundField DataField="END_USE" HeaderText="Pack Process" />

                                <asp:BoundField DataField="SHADE_REFF_NO" HeaderText="Shade Reff. No." />
                                <asp:BoundField DataField="GRADE" HeaderText="Grade" />
                                <asp:BoundField DataField="CR_UNIT" HeaderText="UOM" />
                                <asp:BoundField DataField="NO_OF_UNIT" HeaderText="CR Unit" />
                                <asp:BoundField DataField="ADJUST_QTY" HeaderText="Adjusted Qty" />
                                <asp:BoundField DataField="INVOICE_NO_OF_UNIT" HeaderText="Invoiced Unit" />
                                <asp:BoundField DataField="BAL_INVOICE_NO_OF_UNIT" HeaderText="Bal Invoice Unit" />
                                <asp:BoundField DataField="QUANTITY" HeaderText="Req Qty" />
                                <asp:BoundField DataField="WEIGHT_OF_UNIT" HeaderText="Req WtOfUnit" />
                                <asp:BoundField DataField="WEIGHT_OF_UNIT" HeaderText="Approved WeighofUnit" />
                                <asp:BoundField DataField="QUANTITY" HeaderText="Approved Wt" />
                                <asp:BoundField DataField="BAL_INVOICE_QTY" HeaderText="Bal Inv Wt" />
                                <asp:BoundField DataField="BAL_INVOICE_NO_OF_UNIT" HeaderText="Bal Inv NoOfUnit" />
                                <asp:BoundField DataField="CONF_TYPE" HeaderText="Status" />
                            </Columns>
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
<%--    </ContentTemplate>
</asp:UpdatePanel>
--%>