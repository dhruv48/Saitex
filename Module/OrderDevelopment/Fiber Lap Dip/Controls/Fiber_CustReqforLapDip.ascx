<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Fiber_CustReqforLapDip.ascx.cs" Inherits="Module_OrderDevelopment_Fiber_Lap_Dip_Controls_Fiber_CustReqforLapDip" %>
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
        margin-left: 0px;
        width: 80px;
    }
    .c2
    {
        margin-left: 1px;
        width: 550px;
    }
    .c3
    {
        margin-left: 1px;
        width: 500px;
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
    .e1
    {
        margin-left: 4px;
        width: 70px;
    }
    .e2
    {
        margin-left: 8px;
        width: 230px;
    }
    .e3
    {
        margin-left: 8px;
        width: 300px;
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
</style><asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="left" class="tContentArial">
            <tr>
                <td valign="top" align="left" class="td">
                    <table>
                        <tr>
                            <td id="tdSave" runat="server">
                                <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="Save" ImageUrl="~/CommonImages/save.jpg"
                                    ValidationGroup="CR" OnClick="imgbtnSave_Click" />
                            </td>
                            <td id="tdUpdate" runat="server">
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                    ValidationGroup="CR" OnClick="imgbtnUpdate_Click"></asp:ImageButton>
                            </td>
                            <td id="tdDelete" runat="server">
                                <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete" ImageUrl="~/CommonImages/del6.png"
                                    Width="48" Height="41" ValidationGroup="M1" OnClientClick="javascript:return window.confirm('Are you sure you want to delete this record')"
                                    TabIndex="6" OnClick="imgbtnDelete_Click"></asp:ImageButton>
                            </td>
                            <td id="tdFind" runat="server">
                                <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/link_find.png"
                                    OnClick="imgbtnFind_Click"></asp:ImageButton>
                            </td>
                            <td id="tdPrint" runat="server">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                                    OnClick="imgbtnPrint_Click"></asp:ImageButton>
                            </td>
                            <td id="tdClear" runat="server">
                                <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                                    OnClick="imgbtnClear_Click"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                                    OnClick="imgbtnExit_Click"></asp:ImageButton>
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
                    <span class="titleheading"><b>Customer Request For Fiber (Lab Dip)</b></span>
                </td>
            </tr>
            <tr>
                <td class="td" align="left" valign="top">
                    <span class="Mode">
                        <asp:Label ID="lblMode" runat="server"></asp:Label>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="CR" />
                    </span>
                </td>
            </tr>
            <tr>
                <td width="100%">
                    <table class="td" width="100%">
                        <tr>
                            <td width="25%" align="right">
                                Request Cat :
                            </td>
                            <td width="25%" align="left">
                                <asp:DropDownList ID="ddlOrderCategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOrderCategory_SelectedIndexChanged"
                                    CssClass="SmallFont" Width="150px" TabIndex="1">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlOrderCategory"
                                    Display="None" ErrorMessage="Please Select Order Category" InitialValue="0" ValidationGroup="CR"></asp:RequiredFieldValidator>
                            </td>
                            <td width="25%" align="right">
                                Request Type :
                            </td>
                            <td width="25%" align="left">
                                <asp:DropDownList ID="ddlOrderType" runat="server" CssClass="SmallFont" 
                               OnSelectedIndexChanged="ddlOrderType_SelectedIndexChanged"
                                    AutoPostBack="True" AppendDataBoundItems="true"
                                    Width="150px" TabIndex="2">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DDLOrderType"
                                    Display="None" ErrorMessage="Please Select Order Type " InitialValue="0" ValidationGroup="CR"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="25%" align="right">
                                Customer Request No :
                            </td>
                            <td align="left" width="25%">
                                <asp:TextBox ID="txtOrderNo" runat="server" ReadOnly="true" CssClass="TextBox TextBoxDisplay"
                                    ValidationGroup="M1" Width="147px" TabIndex="3"></asp:TextBox>
                                <cc2:ComboBox ID="cmbOrderNo" runat="server" AutoPostBack="True" CssClass="SmallFont"
                                    DataTextField="ORDER_NO" DataValueField="Combined" EmptyText="Find Order No"
                                    EnableVirtualScrolling="true" EnableLoadOnDemand="true" Height="200px" MenuWidth="450px"
                                    OnLoadingItems="cmbOrderNo_LoadingItems" OnSelectedIndexChanged="cmbOrderNo_SelectedIndexChanged"
                                    TabIndex="4" Width="150px">
                                    <HeaderTemplate>
                                        <div class="header c3">
                                            Order No</div>
                                        <div class="header c1">
                                            Business Type</div>
                                        <div class="header c3">
                                            Order Cat</div>
                                        <div class="header c1">
                                            Order Type</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c3">
                                            <asp:Literal ID="Container4" runat="server" Text='<%# Eval("ORDER_NO") %>' />
                                        </div>
                                        <div class="item c1">
                                            <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("BUSINESS_TYPE") %>' />
                                        </div>
                                        <div class="item c3">
                                            <asp:Literal ID="Literal2" runat="server" Text='<%# Eval("ORDER_CAT") %>' />
                                        </div>
                                        <div class="item c1">
                                            <asp:Literal ID="Literal3" runat="server" Text='<%# Eval("ORDER_TYPE") %>' />
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td width="25%" align="right">
                                Business Type :
                            </td>
                            <td width="25%" align="left">
                                <asp:DropDownList ID="ddlBusinessType"  runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBusinessType_SelectedIndexChanged"
                                    CssClass="SmallFont" Width="150px" TabIndex="5">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DDLBusinessType"
                                    Display="None" ErrorMessage="Please Select Business Type" InitialValue="0" ValidationGroup="CR"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="25%" align="right">
                                Billing Mode :
                            </td>
                            <td align="left" width="25%">
                                <asp:DropDownList ID="ddlBillingMode"  runat="server" CssClass="SmallFont" Width="150px"
                                    OnSelectedIndexChanged="ddlBillingMode_SelectedIndexChanged" AutoPostBack="true"
                                    TabIndex="6">
                                </asp:DropDownList>
                            </td>
                            <td width="25%" align="right">
                                Cr. Rq. Date :
                            </td>
                            <td width="25%" align="left">
                                <asp:TextBox ID="txtDate" CssClass="SmallFont TextBoxDisplay" runat="server" TabIndex="7"
                                    ReadOnly="true" ValidationGroup="M1" Width="147px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="25%" align="right">
                                Customer Name :
                            </td>
                            <td width="75%" align="left" colspan="3">
                                <cc2:ComboBox ID="cmbPartyCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    DataTextField="PRTY_CODE" DataValueField="Address" EmptyText="Select Party" Width="150px"
                                    MenuWidth="650px" Height="200px" TabIndex="8" OnLoadingItems="cmbPartyCode_LoadingItems"
                                    OnSelectedIndexChanged="cmbPartyCode_SelectedIndexChanged" EnableVirtualScrolling="true" style="top: -12px; left: 2px">
                                    <HeaderTemplate>
                                        <div class="header e1">
                                            Code</div>
                                        <div class="header e2">
                                            NAME</div>
                                        <div class="header e3">
                                            Address</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item e1">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                        <div class="item e2">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                        <div class="item e2">
                                            <asp:Literal runat="server" ID="Container3" Text='<%# Eval("Address") %>' /></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                                <asp:TextBox ID="txtPartyCode" runat="server" TabIndex="9" CssClass="SmallFont TextBox TextBoxDisplay"
                                    ReadOnly="true" ValidationGroup="M1" Width="70px"></asp:TextBox>
                                <asp:TextBox ID="txtAddress" runat="server" CssClass="SmallFont TextBox TextBoxDisplay"
                                    ValidationGroup="M1" Width="345px" ReadOnly="true" TabIndex="10"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="25%" align="right">
                                Artical No :
                            </td>
                            <td width="25%" align="left">
                                <asp:TextBox ID="txtCustomerReffNo" CssClass="SmallFont" runat="server" ValidationGroup="M1"
                                    AutoPostBack="True" ontextchanged="txtCustomerReffNo_TextChanged" Width="150px" TabIndex="11"></asp:TextBox>
                                <asp:DropDownList Visible="false" ID="ddlCustomerRefNo" runat="server" CssClass="SmallFont" Width="150px"
                                    OnSelectedIndexChanged="ddlCustomerRefNo_SelectedIndexChanged" AutoPostBack="true"
                                    TabIndex="6">
                                </asp:DropDownList>   
                                  <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>                                 
                            </td>
                            <td width="25%" align="right">
                                Agent :
                            </td>
                            <td width="25%" align="left">
                                <asp:TextBox ID="txtAgent" CssClass="SmallFont TextBox" runat="server" ValidationGroup="M1"
                                    Width="150px" TabIndex="12"></asp:TextBox>
                                <div id="divwidth">
                                </div>
                                <cc3:AutoCompleteExtender ID="aceAgent" runat="server" TargetControlID="txtAgent"
                                    BehaviorID="autoComplete" EnableCaching="true" CompletionSetCount="12" CompletionListCssClass="AutoExtender"
                                    CompletionListItemCssClass="AutoExtenderList" CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                    CompletionListElementID="divwidth" Enabled="true" CompletionInterval="1000" UseContextKey="true"
                                    MinimumPrefixLength="1" ServiceMethod="GetAgentForCRSWLabDip" ServicePath="~/AutoComplete.asmx">
                                </cc3:AutoCompleteExtender>
                            </td>
                        </tr>
                        <tr>
                            <td width="25%" align="right">
                                Delivery Mode :
                            </td>
                            <td width="25%" align="left">
                                <asp:DropDownList ID="ddlDeliveryMode" runat="server" CssClass="SmallFont" TabIndex="13"
                                    Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td width="25%" align="right">
                                Direct Billing :
                            </td>
                            <td width="25%" align="left">
                                <asp:TextBox ID="txtDirectBilling" CssClass="SmallFont TextBox" runat="server" ValidationGroup="M1"
                                    Width="150px" TabIndex="14"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="25%" align="right">
                                Remarks :
                            </td>
                            <td align="left" colspan="3" width="75%">
                                <asp:TextBox ID="txtMstRemarks" class="SmallFont" runat="server" ValidationGroup="M1"
                                    Width="570px" TabIndex="15" MaxLength="500"></asp:TextBox>
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
                                            <span class="titleheading"><b>Fiber</b></span>
                                        </td>
                                       <%-- <td class="tdLeft SmallFont">
                                            <span class="titleheading"><b>Width</b></span>
                                        </td>--%>
                                        <td class="tdCenter SmallFont">
                                            <span class="titleheading"><b>Party Desc</b></span>
                                        </td>
                                        <td class="tdRight SmallFont">
                                            <span class="titleheading"><b>No Of Option</b></span>
                                        </td>
                                        <%--<td class="tdCenter SmallFont">
                                            <span class="titleheading"><b>End Use</b></span>
                                        </td>--%>
                                        <td class="tdCenter SmallFont">
                                            <span class="titleheading"><b>1st Light Source</b></span>
                                        </td>
                                        <td class="tdCenter SmallFont">
                                            <span class="titleheading"><b>2nd Light Source</b></span>
                                        </td>
                                        <td class="tdCenter SmallFont">
                                            <span class="titleheading"><b>Remarks</b></span>
                                        </td>
                                        <td class="tdLeft SmallFont">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top">
                             <cc2:ComboBox ID="cmbFabricCode" runat="server" AutoPostBack="True" CssClass="smallfont" DataTextField="FIBER_CODE" 
                             DataValueField="FIBER_CODE" EnableLoadOnDemand="True"   MenuWidth="750px" OnLoadingItems="Item_LOV_LoadingItems" 
                            OnSelectedIndexChanged="Item_LOV_SelectedIndexChanged" TabIndex="7"  Width="100px" Height="200px" EnableVirtualScrolling="true" >
                            <HeaderTemplate>
                                <div class="header c1">
                                    FIBER CODE</div>
                                <div class="header c2">
                                    FIBER DESCRIPTION</div>
                                <div class="header c3">
                                    FIBER TYPE</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <%# Eval("FIBER_CODE") %></div>
                                <div class="item c2">
                                    <%# Eval("FIBER_DESC") %></div>
                                <div class="item c3">
                                    <%# Eval("FIBER_CAT")%></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                                        </td>
                                       <%-- <td align="left" valign="top">
                                            <asp:TextBox ID="txtCount" runat="server" CssClass=" SmallFont" Width="50px" TabIndex="17" ReadOnly="true"></asp:TextBox>
                                        </td>--%>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtShadeName" runat="server" CssClass="TextBox SmallFont" Width="100px"
                                                TabIndex="18"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtQuantity" class="TextBoxNo SmallFont" runat="server" Width="80px"
                                                TabIndex="19"></asp:TextBox>
                                        </td>
                                        <%--<td align="left" valign="top">
                                            <asp:DropDownList ID="txtEndUse" TabIndex="20" CssClass="SmallFont" runat="server"
                                                AppendDataBoundItems="true" Width="120px">
                                            </asp:DropDownList>
                                        </td>--%>
                                        <td align="left" valign="top">
                                            <asp:DropDownList ID="txtLightSource" TabIndex="21" CssClass="SmallFont" runat="server"
                                                AppendDataBoundItems="true" Width="85px">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:DropDownList ID="txtLightSource1" TabIndex="22" CssClass="SmallFont" runat="server"
                                                AppendDataBoundItems="true" Width="85px">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtRemarks" TabIndex="22" class="SmallFont" runat="server" Width="120px"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="top"  >
                                            <asp:Button ID="btnSTSave" runat="server" Text="Save" OnClick="btnSTSave_Click" TabIndex="23" class="SmallFont" Width="60px"/>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                    <td align="left" valign="top">
                                    Fiber Code/Desc
                                    </td>
                                    <td align="right" valign="top">
                                    
                                    <asp:TextBox ID="txtFabCode" ReadOnly="true" class="SmallFont" runat="server" Width="120px"></asp:TextBox>
                                    </td>
                                    <td colspan="4" align="left" valign="top">
                                    <asp:TextBox ID="txtFabDesc" ReadOnly="true" runat="server" Width="450px" class="SmallFont"></asp:TextBox>
                                    </td>
                                    <td>
                                    <asp:Button ID="btnSTCancel" runat="server" Text="Cancel" OnClick="btnSTCancel_Click" TabIndex="24" class="SmallFont" Width="60px"/>
                                    </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td width="100%">
                                <asp:Panel ID="Panel2" runat="server" ScrollBars="Vertical" Width="100%">
                                    <asp:GridView ID="GridSpinningThread" runat="server" CssClass="SmallFont" Font-Bold="False"
                                         AutoGenerateColumns="False" AllowSorting="True" Width="98%"
                                        OnRowCommand="GridSpinningThread_RowCommand" TabIndex="25">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Serial No">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtSerialNo" runat="server" Text='<%# Bind("UNIQUE_ID") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="8%"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fiber Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtSubtrate" runat="server" Text='<%# Bind("SUBTRATE") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="8%"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quality Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtDescription" runat="server" Text='<%# Bind("DESCRIPTION") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="8%"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Shade Family Name" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtShade" runat="server" Text='<%# Bind("SHADE_FAMILY_NAME") %>' CssClass="LabelNo SmallFont"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Party Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtShadeName" runat="server" Text='<%# Bind("SHADE_NAME") %>' CssClass="LabelNo SmallFont"
                                                        AutoPostBack="True"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="8%"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No Of Option">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtQuantity" runat="server" Text='<%# Bind("QUANTITY") %>' CssClass="LabelNo SmallFont"
                                                        AutoPostBack="True"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="8%"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UOM" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtUOM" runat="server" Text='<%# Bind("UOM") %>' CssClass="LabelNo SmallFont"
                                                        AutoPostBack="True"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="8%"></ItemStyle>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="End Use">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtEndUse" runat="server" Text='<%# Bind("END_USE_NAME") %>' CssClass="LabelNo SmallFont"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%"></ItemStyle>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="1st Light Source">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtLIGHT_SOURCE" runat="server" Text='<%# Bind("LIGHT_SOURCE") %>'
                                                        CssClass="LabelNo SmallFont" AutoPostBack="True"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="8%"></ItemStyle>
                                            </asp:TemplateField>
                                            
                                             <asp:TemplateField HeaderText="2nd Light Source">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtLIGHT_SOURCE1" runat="server" Text='<%# Bind("LIGHT_SOURCE1") %>'
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
                                                    <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="EditItem" CommandArgument='<%# Bind("UNIQUE_ID") %>'
                                                        TabIndex="26" />
                                                    <asp:LinkButton ID="lnkDelete" runat="server" TabIndex="27" Text="Delete" CommandName="DelItem"
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
    </ContentTemplate>
</asp:UpdatePanel>