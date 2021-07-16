<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LabDipEntry.ascx.cs" Inherits="Module_OrderDevelopment_Controls_LabDipEntry" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>
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
        margin-left: 4px;
        width: 100px;
    }
    .tdText
    {
        font: 11px Verdana;
        color: #333333;
    }
    .option2
    {
        font: 11px Verdana;
        color: #0033cc;
        padding-left: 4px;
        padding-right: 4px;
    }
    a
    {
        font: 11px Verdana;
        color: #315686;
        text-decoration: underline;
    }
    a:hover
    {
        color: Teal;
    }
    .ob_gMCont_DT
    {
        overflow: hidden;
    }
    .ob_gMCont
    {
        position: relative;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <%--  <div>
                <asp:Label ID="Label16" runat="server" Text="Comment :"></asp:Label>
                <asp:TextBox ID="TextBox13" runat="server" CssClass="TextBox"></asp:TextBox>
                <asp:Label ID="Label17" runat="server" Text="Status :"></asp:Label>
                <cc1:OboutDropDownList ID="OboutDropDownList5" runat="server">
                </cc1:OboutDropDownList>
            </div>--%>
        <table align="left" class="tContentArial" width="100%">
            <tr>
                <td valign="top" align="left" class="td" width="100%">
                    <table>
                        <tr>
                            <td id="tdSave" runat="server">
                                <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="Save" ImageUrl="~/CommonImages/save.jpg"
                                    OnClick="imgbtnSave_Click" ValidationGroup="M1" TabIndex="16" />
                            </td>
                            <td id="tdUpdate" runat="server">
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                    OnClick="imgbtnUpdate_Click" ValidationGroup="M1" TabIndex="17"></asp:ImageButton>
                            </td>
                            <td id="tdDelete" runat="server">
                                <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete" ImageUrl="~/CommonImages/del6.png"
                                    Width="48" Height="41" ValidationGroup="M1" OnClientClick="javascript:return window.confirm('Are you sure you want to delete this record')"
                                    OnClick="imgbtnDelete_Click" TabIndex="18"></asp:ImageButton>
                            </td>
                            <td id="tdFind" runat="server">
                                <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/link_find.png"
                                    OnClick="imgbtnFind_Click" TabIndex="19"></asp:ImageButton>
                            </td>
                            <td id="tdPrint" runat="server">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                                    OnClick="imgbtnPrint_Click" TabIndex="20"></asp:ImageButton>
                            </td>
                            <td id="tdClear" runat="server">
                                <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                                    OnClick="imgbtnClear_Click" TabIndex="21"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                                    OnClick="imgbtnExit_Click" TabIndex="22"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                                    OnClick="imgbtnHelp_Click" TabIndex="23"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="TableHeader td">
                    <span class="titleheading"><b>Lab Dip Entry Form</b></span>
                </td>
            </tr>
            <tr>
                <td class="td" align="left" valign="top">
                    <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="M1" />
                    </span>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <table width="100%">
                        <tr>
                            <td class="tdRight" width="20%" align="right">
                                <asp:Label ID="Label1" runat="server" Text="Lab Dip # :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="20%" align="left">
                                <%--<cc1:OboutTextBox ID="txtLabDipNo" runat="server"></cc1:OboutTextBox>--%>
                                <asp:TextBox ID="txtLabDipNo" runat="server" CssClass="TextBox TextBoxDisplay" TabIndex="1"
                                    ReadOnly="true"></asp:TextBox>
                                <cc2:ComboBox ID="cmbLabDipNo" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    DataTextField="LAB_DIP_NO" DataValueField="LAB_DIP_NO" Width="150px" MenuWidth="150px"
                                    Height="200px" CssClass="SmallFont" TabIndex="1" EmptyText="Find Lab Dip No"
                                    OnLoadingItems="cmbLabDipNo_LoadingItems" OnSelectedIndexChanged="cmbLabDipNo_SelectedIndexChanged">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Lab Dip No</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("LAB_DIP_NO") %>' /></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLabDipNo"
                                    Display="None" ErrorMessage="Enter Lab dip No" ValidationGroup="M1"></asp:RequiredFieldValidator>
                            </td>
                            <td class="tdRight" width="16%" align="right">
                                <asp:Label ID="Label2" runat="server" Text="Receive Date :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="14%" align="left">
                                <%-- <cc1:OboutTextBox ID="txtReceiveDate" runat="server"></cc1:OboutTextBox>--%>
                                <asp:TextBox ID="txtReceiveDate" runat="server" Width="73%" CssClass="TextBox" TabIndex="2"></asp:TextBox>
                                <cc4:CalendarExtender ID="ce2" runat="server" TargetControlID="txtReceiveDate" PopupPosition="TopLeft" Format="dd/MM/yyyy">
                                </cc4:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtReceiveDate"
                                    Display="None" ErrorMessage="Field can't be empty" ValidationGroup="M1"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtReceiveDate"
                                    Display="None" ErrorMessage="Date can't be Forward Date" ValidationGroup="M1"
                                     Type="Date"></asp:RangeValidator>
                            </td>
                            <td class="tdRight" width="17%" align="right">
                                <asp:Label ID="Label3" runat="server" Text="Due Date :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="13%" align="left">
                                <%-- <cc1:OboutTextBox ID="txtDueDate" runat="server" 
                                        ontextchanged="txtDueDate_TextChanged"></cc1:OboutTextBox>--%>
                                <asp:TextBox ID="txtDueDate" runat="server" Width="73%" CssClass="TextBox" TabIndex="3"
                                    OnTextChanged="txtDueDate_TextChanged"></asp:TextBox>
                                <cc4:CalendarExtender ID="ce1" runat="server" TargetControlID="txtDueDate" PopupPosition="TopRight" Format="dd/MM/yyyy">
                                </cc4:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtDueDate"
                                    Display="None" ErrorMessage="Field can't be Empty" ValidationGroup="M1"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtDueDate"
                                    Display="None" ErrorMessage="Date can't be Back Date"
                                    ValidationGroup="M1" Type="Date"></asp:RangeValidator>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <table width="100%">
                        <tr>
                            <td class="tdRight" width="20%" align="right">
                                <asp:Label ID="Label4" runat="server" Text="Party Code :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="20%" align="left">
                                <cc2:ComboBox ID="cmbPartyCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    OnLoadingItems="cmbPartyCode_LoadingItems" DataTextField="PRTY_CODE" DataValueField="PRTY_CODE"
                                    EmptyText="Select Party Code" OnSelectedIndexChanged="cmbPartyCode_SelectedIndexChanged"
                                    Width="150px" MenuWidth="500px" Height="200px" TabIndex="4" OnTextChanged="cmbPartyCode_TextChanged">
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
                                </cc2:ComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtLabDipNo"
                                    Display="None" ErrorMessage="Enter Lab dip No" ValidationGroup="M1"></asp:RequiredFieldValidator>
                            </td>
                            <td class="tdLeft" colspan="4" width="60%">
                                <%--<cc1:OboutTextBox ID="txtPartyCode" runat="server" Width="350"></cc1:OboutTextBox>--%>
                                <asp:TextBox ID="txtPartyCode" runat="server" Width="98%" CssClass="TextBox TextBoxDisplay"
                                    ReadOnly="true"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cmbPartyCode"
                                    Display="None" ErrorMessage="Select Party Code" ValidationGroup="M1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="20%" align="right">
                                <asp:Label ID="Label5" runat="server" Text="Fabric Code :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="20%" align="left">
                                <cc2:ComboBox ID="cmbFabricCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    DataTextField="FABR_CODE" DataValueField="FABR_CODE" Width="150px" MenuWidth="400px"
                                    Height="200px" CssClass="SmallFont" TabIndex="5" EmptyText="Find Fabric code"
                                    OnLoadingItems="cmbFabricCode_LoadingItems" OnSelectedIndexChanged="cmbFabricCode_SelectedIndexChanged">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Fabric code</div>
                                    </HeaderTemplate>
                                    <HeaderTemplate>
                                        <div class="header c3">
                                            Fabric Cat</div>
                                    </HeaderTemplate>
                                    <HeaderTemplate>
                                        <div class="header c4">
                                            Fabric Description</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <%# Eval("FABR_CODE")%></div>
                                        <div class="item c3">
                                            <%# Eval("FABR_CAT")%></div>
                                        <div class="item c4">
                                            <%# Eval("FABR_DESC")%></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtLabDipNo"
                                    Display="None" ErrorMessage="Enter Lab dip No" ValidationGroup="M1"></asp:RequiredFieldValidator>
                            </td>
                            <td class="tdLeft" colspan="4" width="60%">
                                <%-- <cc1:OboutTextBox ID="txtFabricCode" runat="server" Width="350"></cc1:OboutTextBox>--%>
                                <asp:TextBox ID="txtFabricCode" runat="server" Width="98%" CssClass="TextBox TextBoxDisplay"
                                    ReadOnly="true" TabIndex="6"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cmbFabricCode"
                                    Display="None" ErrorMessage="Select Fabric Code" ValidationGroup="M1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <table width="100%">
                        <tr>
                            <td class="tdRight" width="20%" align="right">
                                <asp:Label ID="Label6" runat="server" Text="Shade :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="50%">
                                <%-- <cc1:OboutTextBox ID="txtShade" runat="server" Width="350"></cc1:OboutTextBox>--%>
                                <asp:TextBox ID="txtShade" runat="server" Width="98%" CssClass="TextBox" TabIndex="7"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="17%" align="right">
                                <asp:Label ID="Label8" runat="server" Text="Shade Type :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="13%">
                                <%-- <cc1:OboutDropDownList ID="ddlShadeType" runat="server" width="100px">
                                    </cc1:OboutDropDownList>--%>
                                <cc2:ComboBox ID="ddlShadeType" runat="server" CssClass="SmallFont" DataTextField="MST_CODE"
                                    DataValueField="MST_CODE" EmptyText="Select" TabIndex="8" Height="80px" MenuWidth="200px"
                                    Width="100px" EnableLoadOnDemand="True" OnLoadingItems="ddlShadeType_LoadingItems">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Shade Type</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal ID="Container4" runat="server" Text='<%# Eval("MST_CODE") %>' />
                                        </div>
                                    </ItemTemplate>
                                </cc2:ComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="ddlShadeType"
                                    Display="None" ErrorMessage="Select ShadeType" ValidationGroup="M1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <table width="100%">
                        <tr>
                            <td class="tdRight" width="20%" align="right">
                                <asp:Label ID="Label7" runat="server" Text="Party Design No :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="20%">
                                <%--<cc1:OboutTextBox ID="txtPartyDesignNo" runat="server"></cc1:OboutTextBox>--%>
                                <asp:TextBox ID="txtPartyDesignNo" runat="server" Width="98%" CssClass="TextBox"
                                    TabIndex="9"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="16%" align="right">
                                <asp:Label ID="Label9" runat="server" Text="Party Ref. Doc. :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="14%">
                                <%--<cc1:OboutTextBox ID="txtPartyRefDoc" runat="server"></cc1:OboutTextBox>--%>
                                <asp:TextBox ID="txtPartyRefDoc" runat="server" CssClass="TextBox" TabIndex="10"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="17%" align="right">
                                <asp:Label ID="Label10" runat="server" Text="Party Doc Date :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="13%">
                                <%--<cc1:OboutTextBox ID="txtPartyDocdate" runat="server"></cc1:OboutTextBox>--%>
                                <asp:TextBox ID="txtPartyDocdate" runat="server" CssClass="TextBox" Width="100px"
                                    TabIndex="11"></asp:TextBox>
                                <cc4:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtPartyDocdate"
                                    PopupPosition="TopLeft">
                                </cc4:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="20%" align="right">
                                <asp:Label ID="Label12" runat="server" Text="Primary Light Source:"></asp:Label>
                            </td>
                            <td class="tdLeft" width="20%">
                                <%--<cc1:OboutDropDownList ID="ddlPrimaryLightSource" runat="server" width="100px">
                                    </cc1:OboutDropDownList>--%>
                                <cc2:ComboBox ID="ddlPrimaryLightSource" runat="server" CssClass="SmallFont" DataTextField="MST_CODE"
                                    DataValueField="MST_CODE" EmptyText="Select" Height="150px" MenuWidth="200px"
                                    TabIndex="12" Width="150px" EnableLoadOnDemand="True" OnLoadingItems="ddlPrimaryLightSource_LoadingItems">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Primary Light Source</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal ID="Container4" runat="server" Text='<%# Eval("MST_CODE") %>' />
                                        </div>
                                    </ItemTemplate>
                                </cc2:ComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlPrimaryLightSource"
                                    Display="None" ErrorMessage="Enter PrimaryLightSource" ValidationGroup="M1"></asp:RequiredFieldValidator>
                            </td>
                            <td class="tdRight" width="16%" align="right">
                                <asp:Label ID="Label13" runat="server" Text="Secondary Light Source:"></asp:Label>
                            </td>
                            <td class="tdLeft" width="44%" colspan="3">
                                <%-- <cc1:OboutDropDownList ID="ddlSecondaryLightSource" runat="server">
                                    </cc1:OboutDropDownList>--%>
                                <cc2:ComboBox ID="ddlSecondaryLightSource" runat="server" CssClass="SmallFont" DataTextField="MST_CODE"
                                    DataValueField="MST_CODE" EmptyText="Select" Height="150px" MenuWidth="200px"
                                    TabIndex="13" Width="150px" EnableLoadOnDemand="True" OnLoadingItems="ddlSecondaryLightSource_LoadingItems">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Secondary Light Source</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal ID="Container4" runat="server" Text='<%# Eval("MST_CODE") %>' />
                                        </div>
                                    </ItemTemplate>
                                </cc2:ComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlSecondaryLightSource"
                                    Display="None" ErrorMessage="Enter SecondaryLightSource" ValidationGroup="M1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <table width="100%">
                        <tr>
                            <td class="tdRight" width="20%" align="right">
                                <asp:Label ID="Label14" runat="server" Text="Comment :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="50%">
                                <%--<cc1:OboutTextBox ID="txtComment" runat="server" Width="620" TextMode="MultiLine"></cc1:OboutTextBox>--%>
                                <asp:TextBox ID="txtComment" runat="server" CssClass="TextBox" Width="98%" TabIndex="14"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="17%" align="right">
                                <asp:Label ID="Label11" runat="server" Text=" Lab Dip Status :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="13%">
                                <asp:DropDownList ID="ddlLabdipStatus" runat="server" CssClass="TextBox" Width="100px"
                                    TabIndex="15">
                                    <asp:ListItem Text="PENDING">PENDING</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
