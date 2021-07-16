<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomerRequestForYarn.ascx.cs"
    Inherits="Module_OrderDevelopment_CustomerRequest_Controls_CustomerRequestForYarn" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc3" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
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
        width: 200px;
    }
    .c2
    {
        margin-left: 4px;
        width: 300px;
    }
    .c3
    {
        width: 200px;
    }
    .d1
    {
        width: 180px;
    }
    .d2
    {
        margin-left: 4px;
        width: 180px;
    }
    .d3
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
<style type="text/css">
    .AutoExtender
    {
        font-family: Verdana, Helvetica, sans-serif;
        font-size: .8em;
        font-weight: normal;
        border: solid 1px #006699;
        line-height: 20px;
        padding: 10px;
        background-color: White;
        margin-left: 10px;
    }
    .AutoExtenderList
    {
        border-bottom: dotted 1px #006699;
        cursor: pointer;
        color: Maroon;
    }
    .AutoExtenderHighlight
    {
        color: White;
        background-color: #006699;
        cursor: pointer;
    }
    #divwidth
    {
        width: 200px !important;
    }
    #divwidth div
    {
        width: 200px !important;
    }
    .SmallFont
    {}
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="left" class="tContentArial" width="100%">
            <tr>
                <td align="left" class="td" valign="top" width="100%">
                    <table>
                        <tr>
                            <td id="tdSave" runat="server">
                                <asp:ImageButton ID="imgbtnSave" runat="server" ImageUrl="~/CommonImages/save.jpg"
                                    OnClick="imgbtnSave_Click1" ToolTip="Save" ValidationGroup="CR" />
                            </td>
                            <td id="tdUpdate" runat="server">
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" ImageUrl="~/CommonImages/edit1.jpg"
                                    OnClick="imgbtnUpdate_Click1" ToolTip="Update" ValidationGroup="CR" />
                            </td>
                            <td id="tdDelete" runat="server">
                                <asp:ImageButton ID="imgbtnDelete" runat="server" Height="41" ImageUrl="~/CommonImages/del6.png"
                                    OnClick="imgbtnDelete_Click1" OnClientClick="javascript:return window.confirm('Are you sure you want to delete this record')"
                                    ToolTip="Delete" ValidationGroup="M1" Width="48" />
                            </td>
                            <td id="tdFind" runat="server">
                                <asp:ImageButton ID="imgbtnFind" runat="server" ImageUrl="~/CommonImages/link_find.png"
                                    OnClick="imgbtnFind_Click1" ToolTip="Find" />
                            </td>
                            <td id="tdPrint" runat="server">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                                    OnClick="imgbtnPrint_Click" ToolTip="Print" />
                            </td>
                            <td id="tdClear" runat="server">
                                <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                    OnClick="imgbtnClear_Click1" ToolTip="Clear" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnExit" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                                    OnClick="imgbtnExit_Click1" ToolTip="Exit" />
                            </td>
                            <td style="font-style: italic">
                                <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                                    ToolTip="Help" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="TableHeader td" width="100%">
                    <span class="titleheading"><b>Customer Request For Yarn Dyeing</b></span>
                </td>
            </tr>
            <tr>
                <td align="left" class="td" valign="top" width="100%">
                    <span class="Mode">
                        <asp:Label ID="lblMode" runat="server"></asp:Label>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="CR" />
                    </span>
                </td>
            </tr>
            <tr>
                <td width="100%">
                    <table width="100%" class="tdLeft">
                        <tr>
                            <td align="right" width="17%">
                                CR Location:
                            </td>
                            <td align="left" width="17%">
                                                    <asp:TextBox ID="txtCrLocation" runat="server" ReadOnly="true" TabIndex="1" Width="120px"
                            CssClass="SmallFont TextBox UpperCase TextBoxDisplay" ValidationGroup="M1"></asp:TextBox>
                        
                               <%-- <asp:DropDownList Width="120px" CssClass="SmallFont TextBox UpperCase" ID="ddlOrderCategory"
                                    runat="server" AutoPostBack="true" TabIndex="1" OnSelectedIndexChanged="ddlOrderCategory_SelectedIndexChanged">
                                </asp:DropDownList>--%>
                                <br />
                               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlOrderCategory"
                                    Display="None" ErrorMessage="Please Select Order Category" InitialValue="0" ValidationGroup="CR"></asp:RequiredFieldValidator>--%>
                            </td>
                            <td align="right" width="17%">
                                CR Type:
                            </td>
                            <td align="left" width="17%">
                                <asp:DropDownList Width="120px" TabIndex="2" CssClass="SmallFont TextBox UpperCase"
                                    ID="ddlOrderType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOrderType_SelectedIndexChanged"
                                    AppendDataBoundItems="True">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DDLOrderType"
                                    Display="None" ErrorMessage="Please Select Order Type " InitialValue="0" ValidationGroup="CR"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right" width="17%">
                                CR No :
                            </td>
                            <td align="left" width="15%">
                                <asp:TextBox ID="txtOrderNo" TabIndex="3" runat="server" Width="120px" CssClass="TextBox TextBoxDisplay"
                                    ValidationGroup="M1" ReadOnly="True"></asp:TextBox>
                                <%--   <asp:DropDownList Width="120px" CssClass="SmallFont TextBox UpperCase" ID="cmbOrderNo"
                            runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="cmbOrderNo_SelectedIndexChanged1">
                        </asp:DropDownList>--%>
                                <cc2:ComboBox ID="cmbOrderNo" TabIndex="4" runat="server" AutoPostBack="True" CssClass="smallfont"
                                    DataTextField="ORDER_NO" DataValueField="Combined" EnableLoadOnDemand="True"
                                    MenuWidth="800" OnLoadingItems="cmbOrderNo_LoadingItems" OnSelectedIndexChanged="cmbOrderNo_SelectedIndexChanged"
                                    EnableVirtualScrolling="true" OpenOnFocus="true" Width="120px" Height="200px">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            CR No</div>
                                        <div class="header c1">
                                            CR Location</div>
                                        <div class="header c1">
                                            CR Type</div>
                                        <div class="header c1">
                                            CR date</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <%# Eval("ORDER_NO")%></div>
                                        <div class="item c1">
                                            <%# Eval("ORDER_CAT")%></div>
                                        <div class="item c1">
                                            <%# Eval("ORDER_TYPE")%></div>
                                        <div class="item c1">
                                            <%# Eval("ORDER_DATE")%></div>
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
                            <td align="right" width="17%">
                                CR Date :
                            </td>
                            <td align="left" width="17%">
                                <asp:TextBox ID="txtDate" runat="server" TabIndex="5" Width="120px" MaxLength="10"
                                    CssClass="SmallFont TextBoxDisplay UpperCase" ValidationGroup="M1"></asp:TextBox>
                            </td>
                            <td align="right" width="17%">
                                Business Type :
                            </td>
                            <td align="left" width="17%">
                                <asp:DropDownList Width="120px" CssClass="SmallFont TextBox UpperCase" ID="ddlBusinessType"
                                    runat="server" OnSelectedIndexChanged="ddlBusinessType_SelectedIndexChanged"
                                    AppendDataBoundItems="True" TabIndex="9">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DDLBusinessType"
                                    Display="None" ErrorMessage="Please Select Business Type" InitialValue="0" ValidationGroup="CR"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right" width="17%">
                                Delivery Mode:
                            </td>
                            <td align="left" width="15%">
                                <asp:DropDownList Width="120px" TabIndex="12" CssClass="SmallFont TextBox UpperCase"
                                    ID="ddlDeliveryMode" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="17%">
                                Billing Mode: </td>
                            <td align="left" width="17%">
                        <asp:DropDownList Width="120px" CssClass="SmallFont TextBox UpperCase" ID="ddlBillingMode"
                            runat="server" AppendDataBoundItems="True" TabIndex="9" AutoPostBack="True" OnSelectedIndexChanged="ddlBillingMode_SelectedIndexChanged">
                        </asp:DropDownList>
                            </td>
                            <td align="right" width="17%">
                        Agent:
                    </td>
                            <td align="left" width="17%">
                                <asp:TextBox ID="txtAgent" runat="server" CssClass="SmallFont TextBox" 
                                    ValidationGroup="M1" Width="115px"></asp:TextBox>
                                <div id="divwidth">
                                </div>
                                <cc3:AutoCompleteExtender ID="aceAgent" runat="server" 
                                    BehaviorID="autoComplete" CompletionInterval="1000" 
                                    CompletionListCssClass="AutoExtender" CompletionListElementID="divwidth" 
                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" 
                                    CompletionListItemCssClass="AutoExtenderList" CompletionSetCount="12" 
                                    EnableCaching="true" Enabled="true" MinimumPrefixLength="1" 
                                    ServiceMethod="GetAgentForCRSWLabDip" ServicePath="~/AutoComplete.asmx" 
                                    TargetControlID="txtAgent" UseContextKey="true">
                                </cc3:AutoCompleteExtender>
                            </td>
                            <td align="right" width="17%">
                                &nbsp;</td>
                            <td align="left" width="15%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right" class="" width="17%">
                                Customer Name :
                            </td>
                            <td align="left" width="17%">
                                <cc2:ComboBox ID="cmbPartyCode" runat="server" AutoPostBack="True" DataTextField="PRTY_CODE"
                            DataValueField="Address" EmptyText="Select Party" EnableLoadOnDemand="true" Height="200px"
                            MenuWidth="500px" OnLoadingItems="cmbPartyCode_LoadingItems" OnSelectedIndexChanged="cmbPartyCode_SelectedIndexChanged"
                            TabIndex="8" Width="100px" EnableVirtualScrolling="true">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Code</div>
                                <div class="header c2">
                                    NAME</div>
                                <div class="header d2">
                                    Address</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal ID="Container1" runat="server" Text='<%# Eval("PRTY_CODE") %>' />
                                </div>
                                <div class="item c2">
                                    <asp:Literal ID="Container2" runat="server" Text='<%# Eval("PRTY_NAME") %>' />
                                </div>
                                <div class="item d2">
                                    <asp:Literal ID="Container3" runat="server" Text='<%# Eval("Address") %>' />
                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox></td>
                            <td align="left" colspan="4" class="tdLeft" width="66%">
                                <asp:TextBox ID="txtPartyCode" TabIndex="7" runat="server" CssClass="SmallFont TextBox TextBoxDisplay"
                                    ReadOnly="true" ValidationGroup="M1" Width="24%"></asp:TextBox>
                                <asp:TextBox ID="txtAddress" runat="server" CssClass="SmallFont TextBox TextBoxDisplay"
                                    ReadOnly="true" TabIndex="8" ValidationGroup="M1" Width="74%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="17%">
                                &nbsp;Direct Billing :
                            </td>
                            <td align="left" width="17%">
                                <asp:TextBox ID="txtDirectBilling" runat="server" TabIndex="13" Width="120px" CssClass="SmallFont TextBox UpperCase"
                                    ValidationGroup="M1"></asp:TextBox>
                            </td>
                            <td align="right" width="17%">
                                Document No:
                            </td>
                            <td align="left" width="17%">
                                <asp:TextBox ID="txtCustomerReffNo" TabIndex="10" runat="server" Width="120px" CssClass="SmallFont TextBox UpperCase"
                                    ValidationGroup="M1"></asp:TextBox>
                            </td>
                            <td align="right" width="17%">
                                Document Date :
                            </td>
                            <td align="left" width="15%">
                                <%--       <asp:TextBox ID="TxtDocumentDate" runat="server"  onFocus="javascript:vDateType='3'"
                            onKeyUp="DateFormat(this,this.value,event,false,'3')" onBlur="DateFormat(this,this.value,event,true,'3')"
                            Width="148px" CssClass="SmallFont TextBox UpperCase" ValidationGroup="M1"></asp:TextBox>--%>
                                <asp:TextBox ID="TxtDocumentDate" runat="server" TabIndex="11" Width="120px" CssClass="SmallFont TextBox UpperCase"
                                    ValidationGroup="M1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvod" runat="server" ControlToValidate="TxtDocumentDate"
                                    Display="Dynamic" ErrorMessage="*Document Date Required" Font-Bold="False" ValidationGroup="CR"></asp:RequiredFieldValidator>
                                <cc3:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                    MaskType="Date" TargetControlID="TxtDocumentDate" PromptCharacter="_">
                                </cc3:MaskedEditExtender>
                                <cc3:CalendarExtender ID="CE1" Format="dd/MM/yyyy" runat="server" TargetControlID="TxtDocumentDate">
                                </cc3:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="17%">
                                &nbsp;Remarks:
                            </td>
                            <td align="left" width="83%" colspan="5">
                                &nbsp;<asp:TextBox ID="txtMstRemarks" runat="server" TabIndex="14" CssClass="SmallFont TextBox UpperCase"
                                    ValidationGroup="M1" Width="99.50%"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="100%">
                    <table width="100%">
                        <tr>
                            <td class="td" width="100%">
                                <table width="100%">
                                    <tr bgcolor="#006699">
                                        <td width="20%">
                                            <span class="titleheading"><b>Gray Yarn Code</b></span>
                                        </td>
                                        <td width="20%">
                                            <span class="titleheading"><b>Shade Family/Shade Code</b></span>
                                        </td>
                                        <td width="10%">
                                            <span class="titleheading"><b>Matching Reff</b></span>
                                        </td>
                                        <td class="tdRight" width="10%">
                                            <span class="titleheading"><b>Qty</b></span>
                                        </td>
                                        <td width="10%">
                                            <span class="titleheading"><b>End Use</b></span>
                                        </td>
                                        <td width="10%">
                                            <span class="titleheading"><b>Req Date</b></span>
                                        </td>
                                        <td width="20%">
                                            <span class="titleheading"><b>Remarks</b></span>
                                        </td>
                                        <td width="20%">
                                        
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top" width="20%">
                                            <cc2:ComboBox ID="cmbArticleNo" runat="server" AutoPostBack="True" CssClass="smallfont"
                                                DataTextField="YARN_CODE" DataValueField="Combined" EnableLoadOnDemand="True"
                                                MenuWidth="800" OnLoadingItems="cmbArticleNo_LoadingItems" OnSelectedIndexChanged="cmbArticleNo_SelectedIndexChanged"
                                                EnableVirtualScrolling="true" OpenOnFocus="true" TabIndex="15" Visible="true"
                                                Width="100%" Height="200px">
                                                <HeaderTemplate>
                                                    <div class="header c1">
                                                        Gray Yarn Code</div>
                                                    <div class="header c2">
                                                        Gray Yarn Desc</div>
                                                    <div class="header c3">
                                                        TYPE</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div class="item c1">
                                                        <%# Eval("YARN_CODE") %></div>
                                                    <div class="item c2">
                                                        <%# Eval("YARN_DESC") %></div>
                                                    <div class="item c3">
                                                        <%# Eval("YARN_TYPE")%></div>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Displaying items
                                                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                    out of
                                                    <%# Container.ItemsCount %>.
                                                </FooterTemplate>
                                            </cc2:ComboBox>
                                        </td>
                                        <td align="left" valign="top" width="20%">
                                            <cc2:ComboBox ID="cmbShade" runat="server" AutoPostBack="True" CssClass="smallfont"
                                                DataTextField="SHADE_FAMILY_NAME" DataValueField="Combined" EnableLoadOnDemand="True"
                                                MenuWidth="400" EnableVirtualScrolling="true" OpenOnFocus="true" TabIndex="16"
                                                Height="200px" Visible="true" Width="100%" OnLoadingItems="cmbShade_LoadingItems"
                                                OnSelectedIndexChanged="cmbShade_SelectedIndexChanged">
                                                <HeaderTemplate>
                                                    <%--<div class="header d1">
                                                        Shade Family Code</div>--%>
                                                    <div class="header d2">
                                                        Shade Family Name</div>
                                                    <%--<div class="header d3">
                                                        Shade Code</div>--%>
                                                    <div class="header d4">
                                                        Shade Name</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%--<div class="item d1">
                                                        <%# Eval("SHADE_FAMILY_CODE")%></div>--%>
                                                    <div class="item d2">
                                                        <%# Eval("SHADE_FAMILY_NAME")%></div>
                                                    <%--<div class="item d3">
                                                        <%# Eval("SHADE_CODE")%></div>--%>
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
                                        <td align="left" valign="top" width="10%">
                                            <uc2:ApproveLRLOV ID="txtMatchingReff" Width="100%" TabIndex="18" runat="server" />
                                        </td>
                                        <td align="left" valign="top" width="10%">
                                            <asp:TextBox ID="txtNoofUnit" runat="server" CssClass="SmallFont TextBoxNo" Width="95%"
                                                AutoPostBack="True" TabIndex="19" OnTextChanged="txtNoofUnit_TextChanged"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="top" width="10%">
                                            <asp:DropDownList CssClass="SmallFont TextBox UpperCase" TabIndex="21" ID="txtEndUse"
                                                runat="server" AppendDataBoundItems="true" Width="100%">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" valign="top" width="10%">
                                    <asp:TextBox ID="txtReqDate" runat="server" TabIndex="11" Width="60px" CssClass="SmallFont TextBox UpperCase"></asp:TextBox>
                                          </td>
                                        <td align="left" valign="top" width="20%" rowspan="2">
                                            <asp:TextBox ID="txtRemarks" runat="server" TabIndex="22" CssClass="SmallFont" 
                                                Width="95%" Height="40px"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="top" width="10%">
                                            <asp:Button ID="btnSTSave" runat="server" CssClass="SmallFont" TabIndex="23" OnClick="btnSTSave_Click"
                                                Text="Save"  />
                                          
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top" colspan="6" width="90%">
                                            Gray Yarn Code :<asp:TextBox ID="txtItemCodeLabel" runat="server" Width="150px" CssClass="TextBox TextBoxDisplay SmallFont"
                                                Font-Bold="False" ReadOnly="True"></asp:TextBox>
                                            Shade Family :<asp:TextBox ID="txtShadeFamilyName" runat="server" CssClass="TextBoxDisplay SmallFont"
                                                ReadOnly="true" Width="100px"></asp:TextBox>
                                            &nbsp;Shade Code :<asp:TextBox ID="txtShadeName" runat="server" CssClass="TextBoxDisplay SmallFont"
                                                ReadOnly="true" Width="120px"></asp:TextBox>
                                            <asp:TextBox ID="ddlShadeCode" runat="server" CssClass="TextBoxDisplay SmallFont"
                                                ReadOnly="true" Visible="false" Width="1px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSTCancel" runat="server" CssClass="SmallFont" TabIndex="24" OnClick="btnSTCancel_Click"
                                                Text="Cancel"  />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td width="100%">
                                <table width="100%">
                                    <tr id="tr4" runat="server">
                                        <td id="Td4" runat="server" align="left" class="td" width="100%">
                                            <asp:GridView ID="GridSpinningThread" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                CssClass="SmallFont" Font-Bold="False" OnRowCommand="GridSpinningThread_RowCommand"
                                                Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Article No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtArticleNo" runat="server" Font-Bold="true" Text='<%# Bind("ARTICLE_NO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <%--   <asp:TemplateField HeaderText="Tkt No">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtTktNo" runat="server" CssClass="Label SmallFont" Text='<%# Bind("TKT_NO") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Make">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtMake" runat="server" CssClass="Label SmallFont" Text='<%# Bind("MAKE") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Shade Family">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtShadeFamily" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("SHADE_FAMILY_NAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Shade Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtShade" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("SHADE_NAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Matching Reff.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtMatchingReff" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("MATCHING_REFF") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <%--     <asp:TemplateField HeaderText="NO OF UNIT">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtNO_OF_UNIT" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("NO_OF_UNIT") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="WEIGHT OF UNIT">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtWEIGHT_OF_UNIT" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("WEIGHT_OF_UNIT") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtQuantity" runat="server" CssClass="LabelNo SmallFont" Font-Bold="true"
                                                                Text='<%# Bind("QUANTITY") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Case/Box">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtNoOfCones" runat="server" CssClass="LabelNo SmallFont" Font-Bold="true"
                                                                Text='<%# Bind("CR_UNIT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="End Use.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtEndUse" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("END_USE_NAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Req Date.">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtReqDate" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("REQ_DATE" , "{0:d}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtRemarks" runat="server" CssClass="LabelNo SmallFont" Font-Bold="true"
                                                                Text='<%# Bind("REMARKS") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Bind("UNIQUE_ID") %>'
                                                                CommandName="EditItem" Text="Edit" />
                                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Bind("UNIQUE_ID") %>'
                                                                CommandName="DelItem" Text="Delete" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle BackColor="#336699" CssClass="SmallFont titleheadingGrid" />
                                                <RowStyle CssClass="SmallFont" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:Label ID="lblUNIT_WT" runat="server" Visible="false" Text=""></asp:Label>
        <asp:Label ID="lblNETBOX_WT" runat="server" Visible="false" Text=""></asp:Label>
        <asp:Label ID="lblNETCART_WT" runat="server" Visible="false" Text=""></asp:Label>
 <p>
     &nbsp;</p>
<cc3:MaskedEditExtender ID="MaskedEditExtender2" runat="server" 
    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
    TargetControlID="txtReqDate">
</cc3:MaskedEditExtender>
<cc3:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
    TargetControlID="txtReqDate">
</cc3:CalendarExtender>

 </ContentTemplate>
</asp:UpdatePanel>