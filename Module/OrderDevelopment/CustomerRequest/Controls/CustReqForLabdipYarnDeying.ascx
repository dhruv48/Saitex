<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustReqForLabdipYarnDeying.ascx.cs" Inherits="Module_OrderDevelopment_CustomerRequest_Controls_CustReqForLabdipYarnDeying" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc3" %>
<%@ Register Src="~/CommonControls/LOV/PartyCodeLOV.ascx" TagName="PartyCodeLOV"
    TagPrefix="uc1" %>
<%@ Register Src="../../../../CommonControls/LOV/ApproveLRLOV.ascx" TagName="ApproveLRLOV"
    TagPrefix="uc2" %>
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
        margin-left: 4px;
        width: 100px;
    }
    .c2
    {
        margin-left: 8px;
        width: 100px;
    }
    .c3
    {
        margin-left: 8px;
        width: 100px;
    }
    .c4
    {
        margin-left: 8px;
        width: 100px;
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
        <table align="left" class="tContentArial">
            <tr>
                <td valign="top" align="left" class="td">
                    <table>
                        <tr>
                            <td id="tdSave" runat="server">
                                <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="Save" ImageUrl="~/CommonImages/save.jpg"
                                    ValidationGroup="CR" OnClick="imgbtnSave_Click1" />
                            </td>
                            <td id="tdUpdate" runat="server">
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                    ValidationGroup="CR" OnClick="imgbtnUpdate_Click1"></asp:ImageButton>
                            </td>
                            <td id="tdDelete" runat="server">
                                <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete" ImageUrl="~/CommonImages/del6.png"
                                    Width="48" Height="41" ValidationGroup="M1" OnClientClick="javascript:return window.confirm('Are you sure you want to delete this record')"
                                    TabIndex="6" OnClick="imgbtnDelete_Click1"></asp:ImageButton>
                            </td>
                            <td id="tdFind" runat="server">
                                <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/link_find.png"
                                    OnClick="imgbtnFind_Click1"></asp:ImageButton>
                            </td>
                            <td id="tdPrint" runat="server">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                                    OnClick="imgbtnPrint_Click"></asp:ImageButton>
                            </td>
                            <td id="tdClear" runat="server">
                                <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                                    OnClick="imgbtnClear_Click1"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                                    OnClick="imgbtnExit_Click1"></asp:ImageButton>
                            </td>
                            <td style="font-style: italic">
                                <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png">
                                </asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="TableHeader td">
                    <span class="titleheading"><b>Customer Request For Yarn Deying (Lab Dip)</b></span>
                </td>
            </tr>
            <tr>
                <td class="td" align="left" valign="top">
                    <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="CR" />
                    </span>
                </td>
            </tr>
            <tr>
                <td width="100%">
                    <table class="td" width="100%">
                        <tr>
                            <td width="15%" align="right">
                                Request Cat:
                            </td>
                            <td width="15%" align="left">
                                <asp:DropDownList ID="ddlOrderCategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOrderCategory_SelectedIndexChanged"
                                    CssClass="SmallFont">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlOrderCategory"
                                    Display="None" ErrorMessage="Please Select Order Category" InitialValue="0" ValidationGroup="CR"></asp:RequiredFieldValidator>
                            </td>
                            <td width="15%" align="right">
                                Request Type:
                            </td>
                            <td width="15%" align="left">
                                <asp:DropDownList ID="ddlOrderType" runat="server" CssClass="SmallFont" OnSelectedIndexChanged="ddlOrderType_SelectedIndexChanged"
                                    AutoPostBack="True" AppendDataBoundItems="true" Width="150px">
                                    <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DDLOrderType"
                                    Display="None" ErrorMessage="Please Select Order Type " InitialValue="0" ValidationGroup="CR"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%" align="right">
                                Customer Request No :&nbsp;
                            </td>
                            <td align="left" style="width: 30%">
                                <asp:TextBox ID="txtOrderNo" runat="server" CssClass="TextBox TextBoxDisplay" ValidationGroup="M1"
                                    Width="150px"></asp:TextBox>
                                <cc2:ComboBox ID="cmbOrderNo" runat="server" AutoPostBack="True" CssClass="SmallFont"
                                    DataTextField="ORDER_NO" DataValueField="Combined" EmptyText="Find Order No"
                                    EnableVirtualScrolling="true" EnableLoadOnDemand="true" Height="200px" MenuWidth="550px"
                                    OnLoadingItems="cmbOrderNo_LoadingItems" OnSelectedIndexChanged="cmbOrderNo_SelectedIndexChanged"
                                    TabIndex="1" Width="150px">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Order No</div>
                                        <div class="header c1">
                                            Business Type</div>
                                        <div class="header c1">
                                            Order Cat</div>
                                        <div class="header c1">
                                            Order Type</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal ID="Container4" runat="server" Text='<%# Eval("ORDER_NO") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("BUSINESS_TYPE") %>' />
                                        </div>
                                        <div class="item c3">
                                            <asp:Literal ID="Literal2" runat="server" Text='<%# Eval("ORDER_CAT") %>' />
                                        </div>
                                        <div class="item c4">
                                            <asp:Literal ID="Literal3" runat="server" Text='<%# Eval("ORDER_TYPE") %>' />
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
                            <td width="15%" align="right">
                                Business Type :
                            </td>
                            <td width="15%" align="left">
                                <asp:DropDownList ID="ddlBusinessType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBusinessType_SelectedIndexChanged"
                                    CssClass="SmallFont" Width="150px">
                                    <%--<asp:ListItem Text="Select" Value="Select"></asp:ListItem>--%>
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DDLBusinessType"
                                    Display="None" ErrorMessage="Please Select Business Type" InitialValue="0" ValidationGroup="CR"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%" align="right">
                                Agent :
                            </td>
                            <td width="15%" align="left">
                                <asp:TextBox ID="txtAgent" CssClass="SmallFont TextBox" runat="server" ValidationGroup="M1"
                                    Width="150px"></asp:TextBox>
                            </td>
                            <td width="15%" align="right">
                                Order Date :
                            </td>
                            <td width="45%" align="left">
                                <asp:TextBox ID="txtDate" CssClass="SmallFont TextBoxDisplay" runat="server" ReadOnly="true"
                                    ValidationGroup="M1" Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%" align="right" class="">
                                Customer Name :
                            </td>
                            <td width="15%" align="left">
                                <%--  <cc2:ComboBox ID="cmbPartyCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    DataTextField="PRTY_CODE" DataValueField="PRTY_ADDRESS" EmptyText="Select Party Code"
                                    Width="150px" MenuWidth="500px" Height="200px" TabIndex="4" OnLoadingItems="cmbPartyCode_LoadingItems"
                                    OnSelectedIndexChanged="cmbPartyCode_SelectedIndexChanged" OnTextChanged="cmbPartyCode_TextChanged">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Code</div>
                                        <div class="header c2">
                                            NAME</div>
                                        <div class="header c3">
                                            Address</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                        <div class="item c3">
                                            <asp:Literal runat="server" ID="Container3" Text='<%# Eval("PRTY_ADD1") %>' /></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>--%>
                                <uc1:PartyCodeLOV ID="cmbPartyCode" runat="server" />
                                <asp:TextBox ID="txtPartyCode" runat="server" CssClass="SmallFont TextBox TextBoxDisplay"
                                    ReadOnly="true" ValidationGroup="M1" Width="24%"></asp:TextBox>
                            </td>
                            <td width="15%" align="right">
                                Customer Reff No :
                            </td>
                            <td width="45%" align="left">
                                <asp:TextBox ID="txtCustomerReffNo" CssClass="SmallFont" runat="server" ValidationGroup="M1"
                                    Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%" align="right">
                                Customer Address :
                            </td>
                            <td width="100%" align="left" colspan="3" cssclass="TextBox TextBoxDisplay">
                                <asp:TextBox ID="txtAddress" runat="server" CssClass="SmallFont TextBox TextBoxDisplay"
                                    ValidationGroup="M1" Width="99%" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%" align="right">
                                Delivery Mode:
                            </td>
                            <td width="15%" align="left">
                                <asp:DropDownList ID="ddlDeliveryMode" runat="server" CssClass="SmallFont" Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td width="15%" align="right">
                                Direct Billing :
                            </td>
                            <td width="15%" align="left">
                                <asp:TextBox ID="txtDirectBilling" CssClass="SmallFont TextBox" runat="server" ValidationGroup="M1"
                                    Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%" align="right">
                                Remarks:
                            </td>
                            <td align="left" colspan="3">
                                <asp:TextBox ID="txtMstRemarks" CssClass="SmallFont" runat="server" ValidationGroup="M1"
                                    Width="99%"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="100%">
                    <table width="100%">
                        <tr>
                            <td class="td">
                                <table width="100%">
                                    <tr bgcolor="#006699">
                                        <td class="tdLeft SmallFont">
                                            <span class="titleheading"><b>Substrate</b></span>
                                        </td>
                                        <td class="tdLeft SmallFont">
                                            <span class="titleheading"><b>Count</b></span>
                                        </td>
                                        <td class="tdCenter SmallFont">
                                            <span class="titleheading"><b>Shade Family / Shade Code</b></span>
                                        </td>
                                        <td class="tdRight SmallFont">
                                            <span class="titleheading"><b>Quanity</b></span>
                                        </td>
                                        <td class="tdRight SmallFont">
                                            <span class="titleheading"><b>UOM</b></span>
                                            <td class="tdLeft SmallFont">
                                                <span class="titleheading"><b>End Use</b></span>
                                            </td>
                                            <td class="tdLeft SmallFont">
                                                <span class="titleheading"><b>Light Source</b></span>
                                                <td class="tdLeft SmallFont">
                                                    <span class="titleheading"><b>Remarks</b></span>
                                                </td>
                                                <td class="tdLeft SmallFont">
                                                    &nbsp;
                                                </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtSubrate" runat="server" CssClass=" SmallFont" Width="60px" TabIndex="8"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtCount" runat="server" CssClass=" SmallFont" Width="50px" TabIndex="8"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="top">
                                            <cc2:ComboBox ID="cmbShade" runat="server" AutoPostBack="True" CssClass="smallfont"
                                                DataTextField="SHADE_FAMILY_NAME" DataValueField="Combined" EnableLoadOnDemand="True"
                                                MenuWidth="400" EnableVirtualScrolling="true" OpenOnFocus="true" TabIndex="9"
                                                Height="200px" Visible="true" Width="40px" OnLoadingItems="cmbShade_LoadingItems"
                                                OnSelectedIndexChanged="cmbShade_SelectedIndexChanged">
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
                                            <asp:TextBox ID="txtShadeFamilyName" runat="server" CssClass="TextBoxDisplay SmallFont"
                                                ReadOnly="true" Width="60px"></asp:TextBox>
                                            <asp:TextBox ID="txtShadeName" runat="server" CssClass="TextBoxDisplay SmallFont"
                                                ReadOnly="true" Width="60px"></asp:TextBox>
                                            <asp:TextBox ID="ddlShadeFamilyCode" runat="server" CssClass="TextBoxDisplay SmallFont"
                                                ReadOnly="true" Visible="false" Width="1px"></asp:TextBox>
                                            <asp:TextBox ID="ddlShadeCode" runat="server" CssClass="TextBoxDisplay SmallFont"
                                                ReadOnly="true" Visible="false" Width="1px"></asp:TextBox>
                                       
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtQuantity" class="TextBoxNo SmallFont" runat="server" Width="50px"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:DropDownList ID="ddlUom" CssClass="SmallFont" runat="server" AppendDataBoundItems="true">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:DropDownList ID="txtEndUse" CssClass="SmallFont" runat="server" AppendDataBoundItems="true"
                                                Width="70px">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" valign="top">
                                            <%--<asp:TextBox ID="txtLightSource" class="SmallFont" runat="server" Width="50px"></asp:TextBox>--%>
                                            <asp:DropDownList ID="txtLightSource" CssClass="SmallFont" runat="server" AppendDataBoundItems="true"
                                                Width="85px">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtRemarks" class="SmallFont" runat="server" Width="100px"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="top" class="style1" style="width: 150px">
                                            <asp:Button ID="btnSTSave" runat="server" Text="Save" OnClick="btnSTSave_Click" 
                                                CssClass="SmallFont" /><asp:Button
                                                ID="btnSTCancel" runat="server" Text="Cancel" OnClick="btnSTCancel_Click" 
                                                CssClass="SmallFont" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td width="100%">
                                <asp:Panel ID="Panel2" runat="server" ScrollBars="Vertical" Width="100%">
                                    <asp:GridView ID="GridSpinningThread" runat="server" CssClass="SmallFont" Font-Bold="False"
                                        ShowFooter="True" AutoGenerateColumns="False" AllowSorting="True" Width="98%"
                                        OnRowCommand="GridSpinningThread_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Serial No">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtSerialNo" runat="server" Text='<%# Bind("UNIQUE_ID") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="8%"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Subtrate">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtSubtrate" runat="server" Text='<%# Bind("SUBTRATE") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="8%"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="COUNT">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtCOUNT" runat="server" Text='<%# Bind("COUNT") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="8%"></ItemStyle>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="Tkt No">
                                    <ItemTemplate>
                                        <asp:Label ID="txtTktNo" runat="server" Text='<%# Bind("TKT_NO") %>' CssClass="Label SmallFont"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="21%"></ItemStyle>
                                </asp:TemplateField>--%>
                                            <%-- <asp:TemplateField HeaderText="Make">
                                    <ItemTemplate>
                                        <asp:Label ID="txtMake" runat="server" Text='<%# Bind("MAKE") %>' CssClass="Label SmallFont"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="21%"></ItemStyle>
                                </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Shade Family Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtShade" runat="server" Text='<%# Bind("SHADE_FAMILY_NAME") %>' CssClass="LabelNo SmallFont"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Shade Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtShadeName" runat="server" Text='<%# Bind("SHADE_NAME") %>' CssClass="LabelNo SmallFont"
                                                        AutoPostBack="True"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="8%"></ItemStyle>
                                            </asp:TemplateField>
                                            <%--  <asp:TemplateField HeaderText="Matching Reff.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtMatchingReff" runat="server" Text='<%# Bind("MATCHING_REFF") %>'
                                                                CssClass="LabelNo SmallFont"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%"></ItemStyle>
                                                    </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Quantity">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtQuantity" runat="server" Text='<%# Bind("QUANTITY") %>' CssClass="LabelNo SmallFont"
                                                        AutoPostBack="True"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="8%"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UOM">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtUOM" runat="server" Text='<%# Bind("UOM") %>' CssClass="LabelNo SmallFont"
                                                        AutoPostBack="True"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="8%"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="End Use">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtEndUse" runat="server" Text='<%# Bind("END_USE_NAME") %>' CssClass="LabelNo SmallFont"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Light Source">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtLIGHT_SOURCE" runat="server" Text='<%# Bind("LIGHT_SOURCE") %>'
                                                        CssClass="LabelNo SmallFont" AutoPostBack="True"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="8%"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtRemarks" runat="server" Text='<%# Bind("REMARKS") %>' CssClass="LabelNo SmallFont"
                                                        AutoPostBack="True"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="8%"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="EditItem" CommandArgument='<%# Bind("UNIQUE_ID") %>' />
                                                    <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CommandName="DelItem"
                                                        CommandArgument='<%# Bind("UNIQUE_ID") %>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                                        <RowStyle CssClass="SmallFont" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
   <%-- </ContentTemplate>
</asp:UpdatePanel>--%>