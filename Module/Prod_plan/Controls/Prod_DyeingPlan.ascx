<%@ Register Src="../../../CommonControls/LOV/PartyCodeLOV.ascx" TagName="PartyCodeLOV" TagPrefix="uc1" %>
<%@ Register Src="../../../CommonControls/LOV/OrderNoLOV.ascx" TagName="OrderNoLOV" TagPrefix="uc3" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Prod_DyeingPlan.ascx.cs" Inherits="Module_Prod_plan_Controls_Prod_DyeingPlan" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>

<style type="text/css">
    .item {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1;
        *display: inline;
        overflow: hidden;
        white-space: nowrap;
    }

    .header {
        margin-left: 2px;
    }

    .c1 {
        width: 80px;
    }

    .c2 {
        margin-left: 2px;
        width: 150px;
    }

    .c3 {
        margin-left: 2px;
        width: 300px;
    }

    .c4 {
        margin-left: 2px;
        width: 40px;
    }

    .c5 {
        margin-left: 2px;
        width: 60px;
    }

    .c6 {
        margin-left: 2px;
        width: 60px;
    }

    .c7 {
        margin-left: 2px;
        width: 100px;
    }
</style>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table class="tdMain" width="100%">
            <tr>
                <td class="td" width="100%">
                    <table class="tContentArial">
                        <tr>
                            <td id="tdSave" runat="server">
                                <asp:ImageButton ID="imgbtnSave" OnClick="imgbtnSave_Click" runat="server" ToolTip="Save"
                                    ImageUrl="~/CommonImages/save.jpg" ValidationGroup="M1"></asp:ImageButton>
                            </td>
                            <td id="tdUpdate" runat="server">
                                <asp:ImageButton ID="imgbtnUpdate" OnClick="imgbtnUpdate_Click" runat="server" ToolTip="Update"
                                    ImageUrl="~/CommonImages/edit1.jpg" ValidationGroup="M1"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgBtnExportExcel" runat="server" Width="48" Height="41" ToolTip="Excel Report"
                                    ImageUrl="~/CommonImages/export.png" OnClick="imgBtnExportExcel_Click" TabIndex="7"></asp:ImageButton>&nbsp;</td>

                            <td>
                            <td id="tdDelete" runat="server">
                                <asp:ImageButton ID="imgbtnDelete" OnClick="imgbtnDelete_Click" runat="server" ToolTip="Delete"
                                    ImageUrl="~/CommonImages/del6.png"></asp:ImageButton>
                            </td>
                            <td id="tdFind" runat="server">
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
                    <b class="titleheading">
                        <%--Order Capture Form for--%>Sale Order Production Machine For
                        <asp:Label ID="lblFormHeading" runat="server" CssClass="SmallFont"></asp:Label>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="TRN" />
                    </b>
                </td>
            </tr>
            <tr>
                <td class="td tdLeft" width="100%">
                    <span class="Mode">You are in
                        <asp:Label ID="lblMode" runat="server"></asp:Label>
                        Mode</span>
                </td>
            </tr>
            <tr>
                <td width="100%" class="td">
                    <table width="100%" style="font-weight: bold">
                        <tr id="TR2" runat="server" visible="false">
                            <td class="tdRight" width="12%">
                                <asp:Label ID="lblBusinessType" runat="server" Text="Business Type :" CssClass="SmallFont BoldFont"></asp:Label>
                            </td>
                            <%--<td class="tdLeft" width="12%">
                                <asp:DropDownList ID="ddlBusinessType" runat="server" AutoPostBack="True" CssClass="SmallFont BoldFont"
                                    Width="98%" OnSelectedIndexChanged="ddlBusinessType_SelectedIndexChanged" TabIndex="1">
                                </asp:DropDownList>
                            </td>--%>
                            <td class="tdRight" width="12%">
                                <asp:Label ID="lblProdType" runat="server" Text="Product Type :" CssClass="SmallFont BoldFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="12%">
                                <asp:DropDownList ID="ddlProductType" runat="server" AutoPostBack="True" CssClass="SmallFont BoldFont"
                                    Width="99%" OnSelectedIndexChanged="ddlProductType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="tdRight" width="12%">
                                <asp:Label ID="lblOrderCategory" runat="server" Text="Order Category :" CssClass="SmallFont BoldFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="12%">
                                <asp:DropDownList ID="ddlOrderCategory" runat="server" AutoPostBack="True" CssClass="SmallFont BoldFont"
                                    Width="98%" OnSelectedIndexChanged="ddlOrderCategory_SelectedIndexChanged" TabIndex="2">
                                    <asp:ListItem>DIRECT SALE</asp:ListItem>
                                    <asp:ListItem>INHOUSE</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="12%">
                                <asp:Label ID="lblOrderNo" runat="server" Text="Order number :" CssClass="SmallFont BoldFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="12%">
                                <asp:TextBox ID="txtOrderNo" runat="server" ReadOnly="true" CssClass="TextBoxNo TextBoxDisplay SmallFont BoldFont"
                                    Width="98%"></asp:TextBox>
                                <uc3:OrderNoLOV ID="ddlOrderNo" runat="server" Width="98%" />
                            </td>
                            <td class="tdRight" width="12%">
                                <asp:Label ID="lblOrderDate" runat="server" Text="Order Date :" CssClass="SmallFont BoldFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="12%">
                                <asp:TextBox ID="txtOrderDate" runat="server" CssClass="TextBox  SmallFont BoldFont"
                                    Width="98%"></asp:TextBox>
                                <cc1:CalendarExtender ID="cldtxtOrderDate" runat="server" TargetControlID="txtOrderDate"
                                    PopupPosition="TopLeft" Format="dd/MM/yyyy">
                                </cc1:CalendarExtender>
                            </td>
                            <td class="tdRight" width="12%">
                                <asp:Label ID="lblOrderType" runat="server" Text="Order Type :" CssClass="SmallFont BoldFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="16%">
                                <asp:DropDownList ID="ddlOrderType" runat="server" AutoPostBack="True" CssClass="SmallFont BoldFont UPPERCASE"
                                    Width="99%" OnSelectedIndexChanged="ddlOrderType_SelectedIndexChanged" TabIndex="3">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="12%">
                                <asp:Label ID="Label1" runat="server" Text="Business Type :" CssClass="SmallFont BoldFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="12%">
                                <asp:DropDownList ID="ddlBusinessType" runat="server" AutoPostBack="True" CssClass="SmallFont BoldFont"
                                    Width="98%" OnSelectedIndexChanged="ddlBusinessType_SelectedIndexChanged" TabIndex="1">
                                </asp:DropDownList>
                            </td>
                            <td class="tdRight" width="12%">
                                <asp:Label ID="Label2" runat="server" Text="Party Name :" CssClass="SmallFont BoldFont"></asp:Label>
                            </td>
                            <td align="left" style="width: 12%;">
                                <cc2:ComboBox ID="txtPartyCode1" runat="server" EnableLoadOnDemand="true" DataTextField="PRTY_CODE"
                                    DataValueField="PRTY_NAME" EmptyText="Select Party" EnableVirtualScrolling="true"
                                    Width="98%" MenuWidth="350px" Height="200px" OnLoadingItems="txtPartyCode_LoadingItems">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Code
                                        </div>
                                        <div class="header c2">
                                            Name
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' />
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td class="tdRight" width="12%">
                                <asp:Label ID="Label3" runat="server" Text="Quality Name :" CssClass="SmallFont BoldFont"></asp:Label>
                            </td>
                            <td align="left" style="width: 12%;">
                                <cc2:ComboBox ID="ddlArticle" runat="server" CssClass="smallfont" EnableLoadOnDemand="True"
                                    DataTextField="YARN_CODE" DataValueField="YARN_DESC" EmptyText="Select Article"
                                    MenuWidth="400px" EnableVirtualScrolling="true" OpenOnFocus="true" TabIndex="11"
                                    Visible="true" Height="200px" Width="98%" OnLoadingItems="ddlArticle_LoadingItems">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Article Code
                                        </div>
                                        <div class="header c2">
                                            Description
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <%# Eval("YARN_CODE") %>
                                        </div>
                                        <div class="item c2">
                                            <%# Eval("YARN_DESC") %>
                                        </div>
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
                            <td class="tdRight" width="12%">
                                <asp:Label ID="Label4" runat="server" Text="Quality Shade:" CssClass="SmallFont BoldFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="12%">
                                <cc2:ComboBox ID="cmbShade" runat="server" AutoPostBack="True" CssClass="smallfont"
                                    DataTextField="SHADE_FAMILY_CODE" DataValueField="SHADE_CODE" EnableLoadOnDemand="True"
                                    MenuWidth="300px" EnableVirtualScrolling="true" OpenOnFocus="true" TabIndex="16"
                                    Height="200px" Visible="true" Width="98%" OnLoadingItems="cmbShade_LoadingItems">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Shade Family Name
                                        </div>
                                        <div class="header c2">
                                            Shade Code
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <%# Eval("SHADE_FAMILY_CODE")%>
                                        </div>
                                        <div class="item c2">
                                            <%# Eval("SHADE_CODE")%>
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td class="tdRight" width="12%">
                                <asp:Label ID="Label5" runat="server" Text="From Date :" CssClass="SmallFont BoldFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="12%">
                                <asp:TextBox ID="txtCRFrom" runat="server" TabIndex="6" Width="98%" CssClass="SmallFont"></asp:TextBox>
                                <cc1:CalendarExtender ID="ce1" runat="server" TargetControlID="txtCRFrom" PopupPosition="TopLeft"
                                    Format="dd/MM/yyyy">
                                </cc1:CalendarExtender>
                            </td>
                            <td class="tdRight" width="12%">
                                <asp:Label ID="Label6" runat="server" Text="To Date :" CssClass="SmallFont BoldFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="16%">
                                <asp:TextBox ID="txtCRTo" runat="server" TabIndex="7" Width="98%" CssClass="SmallFont"
                                    AutoPostBack="true" OnTextChanged="txtCRTo_TextChanged"></asp:TextBox>
                                <cc1:CalendarExtender ID="ce2" runat="server" TargetControlID="txtCRTo" Format="dd/MM/yyyy"
                                    PopupPosition="TopLeft">
                                </cc1:CalendarExtender>
                            </td>
                        </tr>


                        <tr>

                            <td class="tdRight" width="12%">
                                <asp:Label ID="Label7" runat="server" Text="Machine:" CssClass="SmallFont BoldFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="12%">
                                <cc2:ComboBox ID="cmbMachine" runat="server" AutoPostBack="True" CssClass="smallfont"
                                    DataTextField="MACHINE_CODE" DataValueField="MACHINE_CODE" EnableLoadOnDemand="True"
                                    MenuWidth="300px" EnableVirtualScrolling="true" OpenOnFocus="true" TabIndex="16"
                                    Height="200px" Visible="true" Width="98%" OnLoadingItems="cmbMachine_LoadingItems">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Machine
                                        </div>

                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <%# Eval("MACHINE_CODE")%>
                                        </div>

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
                            <%--<td class="tdRight" width="12%">
                               
                            </td>--%>
                            <td class="tdRight" width="12%">
                                <asp:Button ID="btnShow" runat="Server" Text="Get Records" OnClick="btnShow_Click" />
                            </td>
                        </tr>
                        <tr id="TR0" runat="server" visible="false">
                            <td class="tdRight" width="12%">
                                <asp:Label ID="lblCurrencyCode" runat="server" Text="Currency Code :" CssClass="SmallFont BoldFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="12%">
                                <asp:DropDownList ID="ddlCurrencyCode" runat="server" AutoPostBack="True" CssClass="SmallFont BoldFont"
                                    Width="98%" OnSelectedIndexChanged="ddlCurrencyCode_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="tdRight" width="12%">
                                <asp:Label ID="lblConversionRate" runat="server" Text="Conversion Rate :" CssClass="SmallFont BoldFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="16%">
                                <asp:TextBox ID="txtConversionRate" runat="server" CssClass="TextBoxNo SmallFont BoldFont"
                                    Width="97%"></asp:TextBox>
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="100%" class="td">
                    <table width="100%">
                        <tr id="TR12" runat="server" visible="false">
                            <td class="tdRight" width="12%">
                                <asp:Label ID="lblPartyCode" runat="server" Text="Party Code :" CssClass="SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="12%">
                                <cc2:ComboBox ID="txtPartyCodecmb" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    OnLoadingItems="txtPartyCodecmb_LoadingItems" DataTextField="PRTY_CODE" DataValueField="PRTY_NAME"
                                    EmptyText="Select Party" OnSelectedIndexChanged="txtPartyCodecmb_SelectedIndexChanged"
                                    EnableVirtualScrolling="true" Width="150px" MenuWidth="500px" Height="200px"
                                    TabIndex="5">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Code
                                        </div>
                                        <div class="header c3">
                                            NAME
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' />
                                        </div>
                                        <div class="item c3">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' />
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td class="tdLeft" width="12%">
                                <asp:TextBox ID="txtPartyCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    Width="100%" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td class="tdLeft" width="64%">
                                <asp:TextBox ID="txtPartyDetail" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    Width="100%" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="100%" class="td">
                    <table width="100%">
                        <tr id="TR1" runat="server" visible="false">
                            <td class="tdRight" width="12%">
                                <asp:Label ID="lblPartyRefNumber" runat="server" Text="Party Ref # :" CssClass="SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="12%">
                                <asp:TextBox ID="txtPartyRefNumber" runat="server" CssClass="TextBoxNo SmallFont"
                                    MaxLength="50" Width="98%"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="12%">
                                <asp:Label ID="lblPartyRefDate" runat="server" Text="Party Ref Date :" CssClass="SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="12%">
                                <asp:TextBox ID="txtPartyRefDate" runat="server" CssClass="TextBox SmallFont" Width="98%"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="12%">
                                <asp:Label ID="lblOrderProcess" runat="server" Text="Order Process :" CssClass="SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="12%">
                                <asp:DropDownList ID="ddlOrderProcess" runat="server" MenuWidth="200px" Width="98%"
                                    CssClass="SmallFont">
                                </asp:DropDownList>
                            </td>
                            <td class="tdRight" width="12%">
                                <asp:Label ID="lblShipment" runat="server" Text="Shipment :" CssClass="SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="16%">
                                <asp:TextBox ID="txtShipment" runat="server" CssClass="TextBox SmallFont" MaxLength="500"
                                    Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" class="td" width="100%">
                    <%--<asp:Panel ID="grdpanel" runat="server" ScrollBars="Horizontal">--%>
                    <asp:GridView ID="grdPlaningData" runat="server" AutoGenerateColumns="False" CssClass="SmallFont"
                        Width="100%" AllowPaging="True" PagerStyle-CssClass="pager" PageSize="10" OnRowCommand="grdPlaningData_RowCommand"
                        OnPageIndexChanging="grdPlaningData_PageIndexChanging" OnRowDataBound="grdPlaningData_RowDataBound1">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:TemplateField HeaderText="Year" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblYear" runat="server" Text='<%#Eval("YEAR") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Machine No" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblMachine" runat="server" Text='<%#Eval("MACHINE") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mc Cap#" ItemStyle-HorizontalAlign="Center" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblMcCap" runat="server" Text='<%#Eval("MACHINE_CAPACITY") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" Company" ItemStyle-HorizontalAlign="Center" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblbranchcode" runat="server" Text='<%#Eval("BRANCH_CODE") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" Company" ItemStyle-HorizontalAlign="Center" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblcompcode" runat="server" Text='<%#Eval("COMP_CODE") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="S#Family">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemTemplate>
                                    <asp:Label ID="lblShadeFamily" runat="server" CssClass="Label SmallFont" Text='<%# Bind("SHADE_FAMILY_CODE") %>'
                                        Width="50px" ReadOnly="true">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="S#Nature">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemTemplate>
                                    <asp:Label ID="lblShadeNature" runat="server" CssClass="Label SmallFont" Text='<%# Bind("SHADE_NATURE") %>'
                                        Width="50px" ReadOnly="true">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Shade No">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemTemplate>
                                    <asp:Label ID="lblShadeCode" runat="server" CssClass="Label SmallFont" Text='<%# Bind("SHADE_CODE") %>'
                                        Width="50px" ReadOnly="true">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TRN NUMB" Visible="false">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTrnNumb" runat="server" Visible="false" CssClass="Label SmallFont"
                                        Text='<%# Bind("TRN_NUMB") %>' Width="50px" ReadOnly="true">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Order No" ItemStyle-HorizontalAlign="Center" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblOrderCat" runat="server" Visible="false" CssClass="Label SmallFont"
                                        Text='<%# Bind("ORDER_CAT") %>' Width="80px" ReadOnly="true" ToolTip='<%# Bind("ORDER_CAT") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Order No" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblOrderNo" runat="server" CssClass="Label SmallFont" Text='<%# Bind("ORDER_NO") %>'
                                        Width="80px" ReadOnly="true" ToolTip='<%# Bind("ORDER_NO") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Order Type" Visible="false">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblarticleCode" runat="server" Visible="false" CssClass="Label SmallFont"
                                        Text='<%# Bind("BUSINESS_TYPE") %>' Width="80px" ReadOnly="true">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Party Code" ItemStyle-HorizontalAlign="Center" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="txtPartyCode1" Visible="false" runat="server" Text='<%#Eval("PRTY_CODE") %>'
                                        ToolTip='<%#Eval("PRTY_CODE") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Party Name" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="txtPartyDetail1" runat="server" Text='<%#Eval("PRTY_NAME") %>' ToolTip='<%#Eval("PRTY_CODE") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quality Code" Visible="false">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblQualityCode" runat="server" Visible="false" CssClass="Label SmallFont"
                                        Text='<%# Bind("ASS_YARN_CODE") %>' Width="50px" ReadOnly="true">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quality Name" ItemStyle-HorizontalAlign="Center" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblQulityProduct" runat="server" Text='<%#Eval("YARN_DESC") %>' ToolTip='<%#Eval("YARN_CODE") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quality Dis Name" ItemStyle-HorizontalAlign="Center"
                                Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblQulityCode" runat="server" Text='<%#Eval("ARTICAL_DESC") %>' ToolTip='<%#Eval("ARTICLE_CODE") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Base Quality Name" ItemStyle-HorizontalAlign="Center" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblBaseQuality" runat="server" Text='<%#Eval("QUALITY_YARN_DESC") %>'
                                        ToolTip='<%#Eval("ARTICLE_CODE") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Order Qty" ItemStyle-HorizontalAlign="Center" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblreqqty" runat="server" Visible="false" Text='<%#Eval("QUANTITY") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit-1" ItemStyle-HorizontalAlign="Center" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblyarnstock" runat="server" Visible="false" Text='<%#Eval("YARN_STOCK_QTY") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit-3" ItemStyle-HorizontalAlign="Center" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblyarnstockUnit3" runat="server" Visible="false" Text='<%#Eval("YARN_STOCK_QTY1") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Stock" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotalyarnstock" runat="server" Text='<%#Eval("YARN_STOCK","{0:0}") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Stock Detail" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAdjStock" Text=" Stock Details" runat="server" CommandName="ViewAdjStock"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Batch" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblBatch" runat="server" Text='<%#Eval("BATCH") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ord Qty" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Right" BackColor="#008000" Font-Bold="true" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMaxQTY" runat="server" Text='<%#Eval("ASS_QTY") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Plan Qty" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Right" BackColor="#99FF66" Font-Bold="true" />
                                <ItemTemplate>
                                    <asp:Label ID="lblAdjQty" runat="server" Text='<%#Eval("ASS_ADJ_QTY") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Adj Qty" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtTRNYarnSpiningOrderQty" CssClass="TextBoxDisplay TextBoxNo SmallFont"
                                        runat="server" OnTextChanged="txtTRNYarnSpiningOrderQty_TextChanged" ReadOnly="True"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkTransfer" runat="server" AutoPostBack="true" OnCheckedChanged="chkTransfer_CheckedChanged" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Un Plan" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Right" BackColor="#FF0000" Font-Bold="true" />
                                <ItemTemplate>
                                    <asp:Label ID="lblUnPlan" runat="server" Text='<%#Eval("QTYASSBAL") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MC# Plan" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAdjOrder" Text=" MachinePlan " runat="server" CommandName="ViewAdjOrder"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UOM" ItemStyle-HorizontalAlign="Center" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblUom" runat="server" Visible="false" Text='<%#Eval("UOM") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remark" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemark" runat="server" Text='<%#Eval("REMARKS") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Order Date" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSaleOrderDate" runat="server" Text='<%#Eval("SALE_ORDER_DATE") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Exp.Dispatch Date" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblDeldate" runat="server" Text='<%# Bind("DEL_DATE","{0:d}") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle BackColor="#336799" ForeColor="White" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#336799" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                    </asp:GridView>
                    <%-- </asp:Panel>--%>
                </td>
            </tr>
            <tr>
                <td width="100%" class="td SmallFont">
                    <%-- <asp:Panel ID="pnlTRNDetail" runat="server" Width="100%" Style="border-color: Blue;
                        border-width: 1px;">--%>
                    <table width="100%">
                        <tr>
                            <td class="td" width="100%">
                                <table width="100%">
                                    <tr id="TR13" runat="server" visible="false" bgcolor="#336699" class="SmallFont titleheading">
                                        <td class="tdLeft">Select Artical Code
                                        </td>
                                        <td class="td" align="center">Shade Code
                                        </td>
                                        <td class="td" align="center">Lab Dip No
                                        </td>
                                        <td align="center" class="td">Adj Cust Req
                                        </td>
                                        <td class="tdRight">Total Quantity
                                        </td>
                                        <td class="tdRight">Final Del. Date
                                        </td>
                                        <td class="tdRight">Waste (%)
                                        </td>
                                        <td class="tdRight"></td>
                                    </tr>
                                    <tr>
                                        <td class="tdLeft" id="TR15" runat="server" visible="false">
                                            <cc2:ComboBox ID="txtTRNYarnSpiningArticalCode" runat="server" AutoPostBack="True"
                                                CssClass="smallfont" EnableLoadOnDemand="True" DataTextField="ARTICAL_CODE" DataValueField="Combined"
                                                MenuWidth="900" OnLoadingItems="txtTRNYarnSpiningArticalCode_LoadingItems" OnSelectedIndexChanged="txtTRNYarnSpiningArticalCode_SelectedIndexChanged"
                                                EnableVirtualScrolling="true" OpenOnFocus="true" Visible="true" TabIndex="10"
                                                Height="200px">
                                                <HeaderTemplate>
                                                    <div class="header c1">
                                                        CODE
                                                    </div>
                                                    <div class="header c3">
                                                        DESCRIPTION
                                                    </div>
                                                    <div class="header c1">
                                                        SHADE CODE
                                                    </div>
                                                    <div class="header c1">
                                                        QTY.REQ
                                                    </div>
                                                    <div class="header c1">
                                                        QTY.APPR
                                                    </div>
                                                    <div class="header c1">
                                                        QTY.BAL
                                                    </div>
                                                    <div class="header c1">
                                                        ORDER NO.
                                                    </div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div class="item c1">
                                                        <%# Eval("ARTICAL_CODE")%>
                                                    </div>
                                                    <div class="item c3">
                                                        <%# Eval("ARTICAL_DESC")%>
                                                    </div>
                                                    <div class="item c1">
                                                        <%# Eval("SHADE_CODE") %>
                                                    </div>
                                                    <div class="item c1">
                                                        <%# Eval("QUANTITY")%>
                                                    </div>
                                                    <div class="item c1">
                                                        <%# Eval("QTY_APPROVED")%>
                                                    </div>
                                                    <div class="item c1">
                                                        <%# Eval("QTYBAL")%>
                                                    </div>
                                                    <div class="item c1">
                                                        <%# Eval("ORDER_NO")%>
                                                    </div>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Displaying items
                                                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                                    <%# Container.ItemsCount %>.
                                                </FooterTemplate>
                                            </cc2:ComboBox>
                                        </td>
                                        <td id="TR16" runat="server" visible="false" class="tdLeft">
                                            <asp:TextBox ID="txtShadeCode" ReadOnly="true" CssClass="SmallFont TextBoxDisplay"
                                                runat="server" Width="95%"> </asp:TextBox>
                                            <%--<asp:DropDownList ID="ddlShadeCode" CssClass="SmallFont"  runat="server" 
                                                    AutoPostBack="True" onselectedindexchanged="ddlShadeCode_SelectedIndexChanged">
                                                </asp:DropDownList>--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtShadeCode"
                                                Display="None" ErrorMessage="Please Select Shade Code" ValidationGroup="TRN"></asp:RequiredFieldValidator>
                                        </td>
                                        <td id="TR17" runat="server" visible="false" class="tdLeft">
                                            <asp:TextBox ID="txtLabDipNo" ReadOnly="true" CssClass="SmallFont TextBoxDisplay"
                                                runat="server" Width="118px"></asp:TextBox>
                                        </td>
                                        <td id="TR18" runat="server" visible="false" class="tdLeft">
                                            <asp:Button ID="btnAdjCustReq" runat="server" CssClass="SmallFont" OnClick="btnAdjCustReq_Click"
                                                Text="Adj Cust Req" Width="100%" TabIndex="11" />
                                        </td>
                                        <td id="TR19" runat="server" visible="false" class="tdRight">
                                            <asp:TextBox ID="txtTRNYarnSpiningOrderQty" Width="100%" CssClass="TextBoxDisplay TextBoxNo SmallFont"
                                                runat="server" OnTextChanged="txtTRNYarnSpiningOrderQty_TextChanged" ReadOnly="True"></asp:TextBox>
                                        </td>
                                        <td id="TR20" runat="server" visible="false" class="tdRight">
                                            <asp:TextBox ID="txtTRNYarnSpiningDelDate" runat="server" CssClass="SmallFont TextBoxNo"
                                                Width="100%" TabIndex="12"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="TRN" runat="server"
                                                ControlToValidate="txtTRNYarnSpiningDelDate" Display="None" ErrorMessage="Please Select Enter Date"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="tdLeft" id="TR21" runat="server" visible="false">
                                            <asp:TextBox ID="txtTRNYarnSpiningSrinkage" runat="server" CssClass="TextBoxNo SmallFont"
                                                Width="100%" MaxLength="4" TabIndex="13"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="TRN" runat="server"
                                                ControlToValidate="txtTRNYarnSpiningSrinkage" Display="None" ErrorMessage="Please Enter Shrinkage"></asp:RequiredFieldValidator>--%>
                                            <%-- <asp:RangeValidator ID="RangeValidator1" ValidationGroup="TRN" runat="server" ErrorMessage="Percentage In Between 0 - 100%"
                                                ControlToValidate="txtTRNYarnSpiningSrinkage" Display="None" MaximumValue="100"
                                                MinimumValue="0" Type="Double"></asp:RangeValidator>--%>
                                            <%-- <cc1:FilteredTextBoxExtender ID="FiltertxtTRNYarnSpiningSrinkage" runat="server"  TargetControlID="txtTRNYarnSpiningSrinkage"   FilterType="Custom, Numbers" ValidChars="."/>--%>
                                        </td>
                                        <td id="TR22" runat="server" visible="false" align="center">
                                            <asp:Button ID="btnTrnSave" runat="server" Text="Add" OnClick="btnTrnSave_Click"
                                                ValidationGroup="TRN" TabIndex="14" CssClass="SmallFont" Width="60px" />
                                            <asp:Button ID="btnTrnCancel" runat="server" Text="Cancel" OnClick="btnTrnCancel_Click"
                                                TabIndex="15" CssClass="SmallFont" Width="60px" />
                                        </td>
                                    </tr>
                                    <tr id="TR23" runat="server" visible="false">
                                        <td class="tdLeft" colspan="7">
                                            <b>Article Code :<asp:TextBox ID="lblTRNYarnSpiningArticalCode" CssClass="TextBoxDisplay TextBox SmallFont "
                                                runat="server" Text=""></asp:TextBox>
                                                Art. Desc :
                                                <asp:TextBox ID="lblTRNYSpinDesc" runat="server" CssClass="TextBoxDisplay TextBox SmallFont "
                                                    ReadOnly="true" Width="400px"></asp:TextBox>
                                                Unit :<asp:TextBox ID="txtTRNYarnSpiningUOM" runat="server" CssClass="TextBoxDisplay TextBox SmallFont"
                                                    ReadOnly="True" Wrap="true" Width="100px"></asp:TextBox>
                                                Remark :<asp:TextBox ID="txtTRNYyarnRemarks" runat="server" CssClass="TextBoxDisplay TextBox SmallFont"
                                                    ReadOnly="True" Wrap="true" Width="100px"></asp:TextBox>
                                                <asp:Label ID="lblpi_no" Visible="false" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="lblCRNo" runat="server" Visible="false" Text=""></asp:Label>
                                                <asp:LinkButton ID="lnkStock" runat="server" Text="Stock" OnClientClick="return openPopup()"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr width="100%">
                            <td class="td" width="100%" align="left">
                                <asp:Panel ID="pnlTRNYarnSpiningGrid" runat="server" Width="100%">
                                    <asp:GridView ID="grdTRNYarnSpiningDetail" runat="server" AutoGenerateColumns="False"
                                        CssClass="SmallFont" OnRowCommand="grdTRNYarnSpiningDetail_RowCommand" Width="100%"
                                        OnRowDataBound="grdTRNYarnSpiningDetail_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="PI Indent #">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtTRNYarnSpiningPI_NO" runat="server" CssClass="LabelNo SmallFont"
                                                        Text='<%# Bind("PI_NO") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Artical Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtTRNYarnSpiningArticalCode" runat="server" CssClass="Label SmallFont"
                                                        Text='<%# Bind("ARTICAL_CODE") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false" HeaderText="FINAL ORDER CONF CLAG">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFINAL_ORDER_CONF_CLAG" runat="server" CssClass="Label SmallFont"
                                                        Text='<%# Bind("FINAL_ORDER_CONF_CLAG") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Shade Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtTRNYarnSpiningShade" runat="server" CssClass="LabelNo SmallFont"
                                                        Text='<%# Bind("SHADE_CODE") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="LAB DIP NO">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtLabDipNo" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("LAB_DIP_NO") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UOM">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtTRNYarnSpiningUOM" runat="server" CssClass="Label SmallFont" Text='<%# Bind("UOM") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order Quantity">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtTRNYarnSpiningOrderQuantity" runat="server" CssClass="LabelNo SmallFont"
                                                        Text='<%# Bind("ORD_QTY") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtTRNYyarnRemarks" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("REMARKS") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Del Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtTRNYarnSpiningOrderDelDate" runat="server" CssClass="LabelNo SmallFont"
                                                        Text='<%# Bind("DEL_DATE","{0:d}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Waste(%)" HeaderStyle-Width="75px">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtTRNYarnSpiningSrinkage" runat="server" CssClass="Label SmallFont"
                                                        Text='<%# Bind("SRINKAGE") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Select">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandArgument='<%# Bind("UNIQUE_ID") %>'
                                                        CommandName="EditTRNYarnSpiningDetail" Text="Edit"></asp:LinkButton>/
                                                    <asp:LinkButton ID="lnkbtnDel" runat="server" CommandArgument='<%# Bind("UNIQUE_ID") %>'
                                                        CommandName="DelTRNYarnSpiningDetail" Text="Delete"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle BackColor="#336699" CssClass="SmallFont titleheadingGrid" />
                                        <RowStyle CssClass="SmallFont" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        </cc1:CalendarExtender>
        <cc1:CalendarExtender ID="cetxtDeliverySchedule" runat="server" TargetControlID="txtTRNYarnSpiningDelDate"
            PopupPosition="TopLeft" Format="dd/MM/yyyy">
            <%--OnClientDateSelectionChanged="checkBackDate"--%>
        </cc1:CalendarExtender>
        <cc1:MaskedEditExtender ID="MEditApplicationDate" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtTRNYarnSpiningDelDate" PromptCharacter="_"
            UserDateFormat="DayMonthYear">
        </cc1:MaskedEditExtender>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="imgBtnExportExcel" />
    </Triggers>
</asp:UpdatePanel>
