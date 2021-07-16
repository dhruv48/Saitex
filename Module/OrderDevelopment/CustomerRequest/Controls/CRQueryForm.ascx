<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CRQueryForm.ascx.cs" Inherits="Module_SewingThread_Controls_CRQueryForm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%--<script src="../../../../javascript/jquery-1.4.1.min.js" type="text/javascript"></script>

<script src="../../../../javascript/ScrollableGrid.js" type="text/javascript"></script>

<script type="text/javascript" language="javascript">
    $(document).ready(function() {
        $('#<%=grdCustomerRequest.ClientID %>').Scrollable();
    }
)
</script>--%>
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; display:inline;overflow:hidden;white-space:nowrap;}
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

<script type="text/javascript" language="javascript">
    // Added By Rajesh for Printing Directly from Form (05 Jan 2012)
    function CallPrint(strid) {
        var prtContent = document.getElementById(strid);
        if (prtContent != null) {
            var WinPrint = window.open('', '', 'center=1,width=800,height=600,toolbar=0,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            //WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            //WinPrint.close();
            //prtContent.innerHTML = strOldOne;
        }
    }    
</script>

<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
        <table align="left" class="tContentArial" width="100%">
            <tr>
                <td align="left" valign="top" class="td" width="100%">
                    <table align="left">
                        <tr>
                            <td id="tdUpdate" runat="server" align="left">
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                    Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click"></asp:ImageButton>
                            </td>
                            <td id="tdDelete" runat="server" align="left">
                                <asp:ImageButton ID="imgbtnDelete" runat="server" Width="48" Height="41" ToolTip="Delete"
                                    ImageUrl="~/CommonImages/del6.png" OnClick="imgbtnDelete_Click"></asp:ImageButton>
                            </td>
                            <td id="tdFind" runat="server" visible="false" align="left">
                                <asp:ImageButton ID="imgbtnFindTop" Width="48" Height="41" runat="server" ToolTip="Find"
                                    ImageUrl="~/CommonImages/link_find.png" OnClick="imgbtnFindTop_Click"></asp:ImageButton>
                            </td>
                            <td id="tdPrint" runat="server" visible="false" align="left">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Width="48" Height="41" ToolTip="Print"
                                    ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" ToolTip="Clear"
                                    ImageUrl="~/CommonImages/clear.jpg" OnClick="imgbtnClear_Click"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnExit" runat="server" Width="48" Height="41" ToolTip="Exit"
                                    ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnHelp" runat="server" Width="48" Height="41" ToolTip="Help"
                                    ImageUrl="~/CommonImages/link_help.png" OnClick="imgbtnHelp_Click"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr width="100%">
                <td align="center" class="TableHeader td">
                    <b class="titleheading">Customer Request For Sewing Thread Query Form</b>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" class="td">
                    <span class="Mode">
                        <asp:Label ID="lblMode" runat="server"></asp:Label>
                    </span>
                </td>
            </tr>
            <tr>
                <td align="center" width="100%" class="td">
                    <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlFilter" runat="server">
                        <table>
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
                                    <%-- <asp:DropDownList ID="ddlShadeFamily" runat="server" CssClass="SmallFont" TabIndex="4"
                                Width="100px">
                            </asp:DropDownList>--%>
                                    <asp:TextBox ID="txtCustNo" runat="server" TabIndex="4" Width="95px" CssClass="SmallFont UpperCase"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 12%;">
                                    Shade Code :
                                </td>
                                <td align="left" style="width: 12%;">
                                    <cc2:ComboBox ID="ddlShadeCode" runat="server" AutoPostBack="True" CssClass="smallfont"
                                        DataTextField="SHADE_CODE" DataValueField="SHADE_CODE" EnableLoadOnDemand="True"
                                        MenuWidth="400" EnableVirtualScrolling="true" OpenOnFocus="true" TabIndex="5"
                                        Height="200px" Visible="true" Width="100px" OnLoadingItems="ddlShadeCode_LoadingItems"
                                        EmptyText="ALL">
                                        <HeaderTemplate>
                                            <div class="header d2">
                                                Shade Family Name</div>
                                            <div class="header d4">
                                                Shade Name</div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="item d2">
                                                <%# Eval("SHADE_FAMILY_NAME")%></div>
                                            <div class="item d4">
                                                <%# Eval("SHADE_NAME")%></div>
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
                                    CR Date From :
                                </td>
                                <td align="left" style="width: 12%;">
                                    <asp:TextBox ID="txtCRFrom" runat="server" TabIndex="6" Width="95px" CssClass="SmallFont"></asp:TextBox>
                                </td>
                                <td align="right" style="width: 12%;">
                                    CR Date To :
                                </td>
                                <td align="left" style="width: 12%;">
                                    <asp:TextBox ID="txtCRTo" runat="server" TabIndex="7" Width="95px" CssClass="SmallFont"
                                        AutoPostBack="true" OnTextChanged="txtCRTo_TextChanged"></asp:TextBox>
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
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="left" class="td" width="100%" valign="top">
                    <table style="width: 550px">
                        <tr>
                            <td>
                                <b>Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server"></asp:Label></b>
                                <%--<asp:Button ID="btnPrint" runat="Server" Text="Print Records" 
                                    onclick="btnPrint_Click" />--%>
                                <asp:Button ID="btnExcel" runat="Server" Text="Excel" OnClick="btnExcel_Click" Visible="false" />
                                <asp:Button ID="btnWord" runat="Server" Text="Word" OnClick="btnWord_Click" Visible="false" />
                            </td>
                            <td align="center">
                                <asp:UpdateProgress ID="UpdateProgress431" runat="server">
                                    <ProgressTemplate>
                                        <h3>
                                            Loading...
                                        </h3>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" class="td" width="100%">
                    <%--<asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Height="350px" Width="960px">--%>
                        <div id="divPrint" runat="server">
                            <asp:GridView ID="grdCustomerRequest" runat="server" AutoGenerateColumns="False"
                            AllowPaging="True" AllowSorting="True" Font-Size="7pt" CellPadding="3" GridLines="Vertical"
                            Width="100%"  PagerStyle-HorizontalAlign="Left" OnPageIndexChanging="grdCustomerRequest_PageIndexChanging"
                            EmptyDataText="No Record Found" PageSize="15" 
                             BackColor="White" 
                            BorderStyle="Ridge">
                                <%--CssClass="SmallFont" CellPadding="4" ForeColor="#333333" BorderStyle="Ridge"
                                GridLines="Both">--%>
                               <%-- <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />--%>
                               <%--<FooterStyle Width="100%" BackColor="#CCCCCC" 
                                ForeColor="Black" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />--%>
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
                                    <%--<asp:BoundField DataField="PRTY_CODE" HeaderText="Party Code" />
                                    <asp:BoundField DataField="PRTY_Name" HeaderText="Party Name" />--%>
                                    <%--<asp:BoundField DataField="ARTICLE_NO" HeaderText="Article No" />--%>
                                    <asp:TemplateField HeaderText="Party Code" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                    <asp:Label ID="LblPartyCode" Text='<%#Eval("PRTY_CODE") %>' ToolTip='<%#Eval("PRTY_NAME") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                   <%-- <asp:BoundField DataField="YARN_DESC" HeaderText="Article Desc" />--%>
                                   <asp:TemplateField HeaderText="Article No" HeaderStyle-HorizontalAlign="Left">
                                   <ItemTemplate>
                                   <asp:Label ID="LblArticleNo" Text='<%#Eval("ARTICLE_NO") %>' ToolTip='<%#Eval("YARN_DESC") %>' runat="server"></asp:Label>
                                   </ItemTemplate>
                                   </asp:TemplateField>
                                    <asp:BoundField DataField="SHADE_FAMILY_CODE" HeaderText="Shade Family Code" Visible="false" />
                                    <asp:BoundField DataField="SHADE_CODE" HeaderText="Shade Code" />
                                    <asp:BoundField DataField="CR_UNIT" HeaderText="UOM" />
                                    <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Trans Price">
                                <ItemTemplate>
                                    <asp:Label ID="lblTRANS_PRICE" runat="server" Text='<%# Eval("TRANS_PRICE") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Sale Price">
                                <ItemTemplate>
                                    <asp:Label ID="lblSALE_PRICE" runat="server" Text='<%# Eval("SALE_PRICE") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="CR Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRequestedNoOfUnit" runat="server" Text='<%# Eval("NO_OF_UNIT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                            Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Adjusted Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblADJUST_QTY" runat="server" Text='<%# Eval("ADJUST_QTY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                            Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Invoiced Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblINVOICE_NO_OF_UNIT" runat="server" Text='<%# Eval("INVOICE_NO_OF_UNIT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                            Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Bal Invoice Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblINVOICE_NO_OF_UNIT" runat="server" Text='<%# Eval("BAL_INVOICE_NO_OF_UNIT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                            Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Req Qty" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRequestedQuantity" runat="server" Text='<%# Eval("QUANTITY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                            Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Req WtOfUnit"
                                        Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReqWeighofUnit" runat="server" Text='<%# Eval("WEIGHT_OF_UNIT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                            Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Approved WeighofUnit"
                                        Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWeighofUnit" runat="server" Text='<%# Eval("WEIGHT_OF_UNIT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                            Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Approved Wt" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("QUANTITY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                            Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Bal Inv Wt" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBAL_INVOICE_QTY" runat="server" Text='<%# Eval("BAL_INVOICE_QTY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                            Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Bal Inv NoOfUnit"
                                        Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBAL_INVOICE_NO_OF_UNIT" runat="server" Text='<%# Eval("BAL_INVOICE_NO_OF_UNIT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                            Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CONF_TYPE" HeaderText="Status" />
                                </Columns>
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            </asp:GridView>
                        </div>
                    <%--</asp:Panel>--%>
                </td>
            </tr>
        </table>
        <cc1:CalendarExtender ID="ce1" runat="server" TargetControlID="txtCRFrom" PopupPosition="TopLeft"
            Format="dd/MM/yyyy">
        </cc1:CalendarExtender>
        <cc1:CalendarExtender ID="ce2" runat="server" TargetControlID="txtCRTo" Format="dd/MM/yyyy"
            PopupPosition="TopLeft">
        </cc1:CalendarExtender>
        <cc1:MaskedEditExtender ID="ME1" runat="server" Mask="99/99/9999" MaskType="Date"
            TargetControlID="txtCRFrom" PromptCharacter="_">
        </cc1:MaskedEditExtender>
        <cc1:MaskedEditExtender ID="ME2" runat="server" Mask="99/99/9999" MaskType="Date"
            TargetControlID="txtCRTo" PromptCharacter="_">
        </cc1:MaskedEditExtender>
<%--    </ContentTemplate>
</asp:UpdatePanel>--%>