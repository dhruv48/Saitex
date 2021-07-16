<%@ Control Language="C#" AutoEventWireup="true" CodeFile="POCredit.ascx.cs" Inherits="Module_Yarn_SalesWork_Controls_POCredit" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>

<script language="javascript" type="text/javascript">

    function Calculation(val) {
        var name = val;

        document.getElementById('ctl00_cphBody_POCredit1_txtAdvanceAmount').value = (parseFloat(document.getElementById('ctl00_cphBody_POCredit1_txtAdvance').value) * (parseFloat(document.getElementById('ctl00_cphBody_POCredit1_txtFinalTotal').value) / 100)).toFixed(3);
    }
    function SetFocus(ControlId) {
        document.getElementById(ControlId).focus();
    }
    function GetClick(ButtonId) {
        document.getElementById(ButtonId).click();
    }
   
</script>

<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;}
    .header
    {
        margin-left: 2px;
    }
    .c1
    {
        width: 60px;
    }
    .c2
    {
        margin-left: 2px;
        width: 150px;
    }
    .c3
    {
        margin-left: 2px;
        width: 400px;
    }
     .c4
    {
        width: 70px;
    }
    .ralign
    {
        text-align: right;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
<table class="tContentArial" width="95%">
    <tr>
        <td class="td tdLeft" width="100%">
            <table align="left">
                <tr>
                    <td id="tdSave" valign="top" align="center" runat="server">
                        <asp:ImageButton ID="imgbtnSave" OnClick="imgbtnSave_Click" runat="server" ImageUrl="~/CommonImages/save.jpg"
                            ToolTip="Save" Height="41" Width="48" ValidationGroup="M1"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnUpdate" OnClick="imgbtnUpdate_Click" runat="server" ToolTip="Update"
                            ImageUrl="~/CommonImages/edit1.jpg" Width="48" Height="41" ValidationGroup="M1">
                        </asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnFind" OnClick="imgbtnFind_Click" runat="server" ToolTip="Find"
                            ImageUrl="~/CommonImages/link_find.png" Width="48" Height="41"></asp:ImageButton>
                    </td>
                    <td id="tdPrint" runat="server">
                        <asp:ImageButton ID="imgbtnPrint" OnClick="imgbtnPrint_Click" runat="server" ToolTip="Print"
                            ImageUrl="~/CommonImages/link_print.png" Width="48" Height="41"></asp:ImageButton>&nbsp;
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" OnClick="imgbtnClear_Click" runat="server" ToolTip="Clear"
                            ImageUrl="~/CommonImages/clear.jpg" Width="48" Height="41"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" OnClick="imgbtnExit_Click" runat="server" ToolTip="Exit"
                            ImageUrl="~/CommonImages/link_exit.png" Width="48" Height="41"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" OnClick="imgbtnHelp_Click" runat="server" ToolTip="Help"
                            ImageUrl="~/CommonImages/link_help.png" Width="48" Height="41"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="TableHeader td" align="center" width="100%">
            <span class="titleheading"><b>Yarn Purchaser Order </b></span>
        </td>
    </tr>
    <tr>
        <td class="td tdLeft">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                ShowSummary="False" />
        </td>
    </tr>
    <tr>
        <td width="100%" class="td SmallFont">
            <table width="100%">
                <tr style="font-weight: bold">
                    <td valign="top" align="right" width="15%">
                        <asp:Label ID="Label2" runat="server" CssClass="LabelNo" Text="Order Type :"></asp:Label>
                    </td>
                    <td valign="top" align="left" width="15%">
                        <asp:DropDownList ID="ddlOrderType" TabIndex="1" runat="server" Width="150px" CssClass="TextBox SmallFont"
                            AutoPostBack="True" Font-Bold="true" OnSelectedIndexChanged="ddlOrderType_SelectedIndexChanged">
                            <asp:ListItem Value="PUM">Main Order</asp:ListItem>
                            <asp:ListItem Value="PUS">Supplimentry Order</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td valign="top" align="right" width="15%">
                        <asp:Label ID="Label3" runat="server" CssClass="LabelNo" Text="Order Number :"></asp:Label>
                    </td>
                    <td valign="top" align="left" width="15%">
                        <asp:TextBox ID="txtOrderNumber" TabIndex="2" Font-Bold="true" runat="server" Width="150px"
                            CssClass="TextBoxNo SmallFont" MaxLength="10" OnTextChanged="txtOrderNumber_TextChanged1"
                            ReadOnly="True"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvon" runat="server" ValidationGroup="M1" Font-Bold="False"
                            Display="Dynamic" ErrorMessage="*Order number required" ControlToValidate="txtOrderNumber"></asp:RequiredFieldValidator>
                        <cc2:ComboBox ID="ddlOrderNumber" runat="server" AutoPostBack="True" CssClass="SmallFont"
                            DataTextField="PO_NUMB" DataValueField="PO_NUMB" EmptyText="Find Item" EnableLoadOnDemand="true"
                            Height="200px" MenuWidth="450px" OnLoadingItems="ddlOrderNumber_LoadingItems"
                            OnSelectedIndexChanged="ddlOrderNumber_SelectedIndexChanged" TabIndex="1" Width="150px">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Order Number</div>
                                <div class="header c1">
                                    Order Date</div>
                                <div class="header c3">
                                    Party Name</div>
                                <div class="header c1">
                                    Order Nature</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal ID="Container7" runat="server" Text='<%# Eval("PO_NUMB") %>' />
                                </div>
                                <div class="item c1">
                                    <asp:Literal ID="Container8" runat="server" Text='<%# Eval("PO_DATE") %>' />
                                </div>
                                <div class="item c3">
                                    <asp:Literal ID="Container9" runat="server" Text='<%# Eval("PRTY_NAME") %>' />
                                </div>
                                <div class="item c1">
                                    <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("PO_NATURE") %>' />
                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items - out of .
                            </FooterTemplate>
                        </cc2:ComboBox>
                    </td>
                    <td valign="top" align="right" width="15%">
                        <asp:Label ID="Label4" runat="server" CssClass="LabelNo" Text="Order Date :"></asp:Label>
                    </td>
                    <td valign="top" align="left" width="25%">
                        <asp:TextBox ID="txtOrderDate" TabIndex="3" Font-Bold="true" runat="server" Width="150px"
                            CssClass="TextBox SmallFont" MaxLength="25"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvod" runat="server" ValidationGroup="M1" Font-Bold="False"
                            Display="Dynamic" ErrorMessage="*Order Date Required" ControlToValidate="txtOrderDate"></asp:RequiredFieldValidator>
                        <cc1:CalendarExtender ID="CE1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtOrderDate">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td SmallFont">
            <table width="100%">
                <tr style="font-weight: bold">
                    <td valign="top" align="right" width="16%">
                        <asp:Label ID="Label1" runat="server" CssClass="LabelNo" Text="Party Code :"></asp:Label>
                    </td>
                    <td valign="top" align="left" width="15%">
                        <cc2:ComboBox ID="txtPartyCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="txtPartyCode_LoadingItems" DataTextField="PRTY_CODE" DataValueField="Address"
                            EmptyText="Select Vendor" OnSelectedIndexChanged="txtPartyCode_SelectedIndexChanged"
                            EnableVirtualScrolling="true" Width="150px" MenuWidth="650px" Height="200px">
                            <HeaderTemplate>
                                <div class="header c1">
                                   Party Code</div>
                                <div class="header c3">
                                   PARTY NAME</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                <div class="item c3">
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
                    <td valign="top" align="left" width="69%">
                        <asp:TextBox ID="txtPartyAddress" TabIndex="4" runat="server" Width="95%" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="True" TextMode="SingleLine"></asp:TextBox>
                    </td>
                </tr>
                <tr style="font-weight: bold">
                            <td valign="top" align="right" width="16%">
                                Party State :
                            </td>
                            <td width="15%" class="tdLeft">
                                <asp:DropDownList ID="ddlPrtyState" runat="server" CssClass="SmallFont TextBox" TabIndex="5"
                                    Width="150px" OnSelectedIndexChanged="ddlPrtyState_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td valign="top" class="tdLeft" width="15%">
                                <asp:TextBox ID="txtState" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True" Width="100px"></asp:TextBox>
                                <asp:Label ID="lblgst" runat="server" CssClass="LabelNo" Text="GST No :" Font-Bold="true">
                                </asp:Label>
                                <asp:TextBox ID="txtGstNo" Width="150px" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True"></asp:TextBox>
                                    
                                    <asp:Label ID="Label14" runat="server" CssClass="LabelNo" Text="Transportation:">
                                  </asp:Label>
                                 
                           <asp:DropDownList ID="ddlRemarks" TabIndex="16" runat="server" 
                                CssClass="SmallFont TextBox" Width="150px" AutoPostBack="True" 
                                onselectedindexchanged="ddlRemarks_SelectedIndexChanged" >
                               
                            <asp:ListItem Selected="True">To Pay</asp:ListItem>
                            <asp:ListItem>Free delivery</asp:ListItem>
                            <asp:ListItem>Included in Bill</asp:ListItem>
                        </asp:DropDownList>
                       <asp:TextBox ID="txtRemarks" TabIndex="11" runat="server" Width="20%" CssClass="TextBox TextBoxDisplay SmallFont"
                            MaxLength="500" TextMode="SingleLine" ReadOnly="true">
                            </asp:TextBox>
                            </td>
                        </tr>
                
                <tr style="font-weight: bold">
                    <td valign="top" align="right" width="16%">
                        <asp:Label ID="Label5" runat="server" CssClass="LabelNo" Text="Transporter Code :"></asp:Label>
                    </td>
                    <td valign="top" align="left" width="15%">
                        <cc2:ComboBox ID="txtTransporterCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="txtTransporterCode_LoadingItems" DataTextField="PRTY_CODE" DataValueField="Address"
                            EnableVirtualScrolling="true" OnSelectedIndexChanged="txtTransporterCode_SelectedIndexChanged"
                            Width="150px" EmptyText="Select transaporter" MenuWidth="650px" Height="200px">
                            <HeaderTemplate>
                                <div class="header c1">
                                  Tran Code</div>
                                <div class="header c3">
                                    Tran NAME</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                <div class="item c3">
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
                    <td valign="top" align="left" width="69%">
                        <asp:TextBox ID="txtTransporterName" TabIndex="5" runat="server" Width="95%" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="True" TextMode="SingleLine"></asp:TextBox>
                    </td>
                    
                </tr>
                <tr style="font-weight: bold">
                <td align="right" valign="top" width="17%">
                       Spinner/ Mill:
                    </td>
                    <td align="left" valign="top" width="15%">
                       <cc2:ComboBox ID="txtSpinnerCode" TabIndex="12" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="txtSpinner_LoadingItems" DataTextField="PRTY_CODE" DataValueField="PRTY_NAME"
                            EmptyText="Select Spinner" OnSelectedIndexChanged="txtSpinnerCode_SelectedIndexChanged"
                            EnableVirtualScrolling="true" Width="150px" MenuWidth="650px" Height="200px">
                            <HeaderTemplate>
                                <div class="header c2">
                                   Spinner Code</div>
                                <div class="header c3">
                                   Spinner NAME</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                <div class="item c3">
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
                     <td valign="top" align="left" width="69%">
                        <asp:TextBox ID="txtSpinnerName" TabIndex="5" runat="server" Width="95%" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="True" TextMode="SingleLine"></asp:TextBox>
                    </td>
                    </tr>
            </table>
            
        </td>
    </tr>
    
    <tr style="font-weight: bold">
        <td width="100%" class="td SmallFont">
            <table width="100%">
                <tr>
                    <td width="15%" class="tdRight">
                        PO Type :
                    </td>
                    <td width="15%" class="tdLeft">
                        <asp:DropDownList ID="ddlPONature" runat="server" CssClass="SmallFont TextBox" Width="150px" TabIndex="6">
                        </asp:DropDownList>
                    </td>
                    <td width="15%" class="tdRight">
                        <asp:Label ID="Label15" runat="server" CssClass="LabelNo" Text="Currency Code :"></asp:Label>
                    </td>
                    <td width="15%" class="tdLeft">
                        <asp:DropDownList ID="txtCurrencyCode" Width="150px" runat="server" CssClass="SmallFont TextBox"
                            AppendDataBoundItems="True" AutoPostBack="True" TabIndex="7">
                        </asp:DropDownList>
                    </td>
                    <td width="15%" class="tdRight">
                        <asp:Label ID="Label17" runat="server" CssClass="LabelNo" Text="Conversion Rate :"></asp:Label>
                    </td>
                    <td width="25%" class="tdLeft">
                        <asp:TextBox ID="txtconversionRate" CssClass="SmallFont TextBoxNo" runat="server"
                            TabIndex="8" Width="150px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr style="font-weight: bold">
        <td width="100%" class="td SmallFont">
            <table width="100%">
                <tr >
                    <td valign="top" align="right" width="15%">
                        <asp:Label ID="Label8" runat="server" CssClass="LabelNo" Text="Delivery Branch :"></asp:Label>
                    </td>
                    <td valign="top" align="left" width="35%">
                        <asp:DropDownList ID="ddlDelAdd" runat="server" CssClass="SmallFont TextBox" AutoPostBack="true"
                            Width="150px" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlDelAdd_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtDelAddress" TabIndex="8" runat="server" Width="250px" CssClass="SmallFont gCtrTxt"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="20%">
                        <asp:Label ID="Label7" runat="server" CssClass="LabelNo" Text="Delivery Date :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="30%">
                        <asp:TextBox ID="txtDeliveryDate" TabIndex="7" runat="server" Width="150px" CssClass="SmallFont gCtrTxt"
                            MaxLength="25"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvd" runat="server" ValidationGroup="M1" Font-Bold="False"
                            Display="Dynamic" ErrorMessage="*Del Date required" ControlToValidate="txtDeliveryDate"></asp:RequiredFieldValidator>
                        <cc1:CalendarExtender ID="CE2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDeliveryDate">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label6" runat="server" CssClass="LabelNo" Text="Pay Term :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="35%">
                        <asp:TextBox ID="txtPayTerm" TabIndex="9" runat="server" Width="99%" CssClass="SmallFont gCtrTxt"
                            MaxLength="50"></asp:TextBox>
                    </td>
                    <td valign="top" align="right" width="20%">
                        <asp:Label ID="Label9" runat="server" CssClass="LabelNo" Text="Dispatch Mode :"></asp:Label>
                    </td>
                    <td valign="top" align="left" width="30%">
                        <asp:TextBox ID="txtDespatchMode" TabIndex="10" runat="server" Width="90%" CssClass="SmallFont gCtrTxt"
                            MaxLength="200"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr style="font-weight: bold">
        <td width="100%" class="td SmallFont">
            <table width="100%">
             <tr>
                    <td align="right" valign="top" width="15%">
                        <asp:Label ID="Label16" runat="server" CssClass="LabelNo" Text="Agent Name :"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="85%">
                        <asp:TextBox ID="txtInstructions" runat="server" CssClass="SmallFont gCtrTxt" MaxLength="500"
                            TabIndex="12" TextMode="SingleLine" Width="96%" 
                            ontextchanged="txtInstructions_TextChanged"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="right" width="15%">
                        <%--<asp:Label ID="Label14" runat="server" CssClass="LabelNo" Text="Remarks :"></asp:Label>--%>
                    </td>
                    <td valign="top" align="left" width="85%">
                        
                    </td>
                </tr>
                
                <tr>
                    <td align="right" valign="top" width="15%" >
                        <%--Fetch From Indent :--%>
                    </td>
                    <td align="left" valign="top" width="85%" >
                        <asp:CheckBox ID="chkFetchIndent" runat="server" AutoPostBack="True" OnCheckedChanged="chkFetchIndent_CheckedChanged"
                            Width="10px" TabIndex="13" Visible="false" />
                        <asp:DropDownList ID="ddlIndent" runat="server" AutoPostBack="True" CssClass="SmallFont TextBox"
                            OnSelectedIndexChanged="ddlIndent_SelectedIndexChanged" Width="150px" Visible="False">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr style="font-weight: bold">
        <td align="left" class="td SmallFont" valign="top" width="100%">
            <table width="100%">
                <tr bgcolor="#336699">
                    <td class="tdLeft SmallFont">
                        <asp:Label ID="Label18" runat="server" CssClass="LabelNo titleheading" Text="Yarn"></asp:Label>
                    </td>
                    <td class="tdLeft SmallFont">
                    
                        <asp:Label ID="Label11" runat="server" CssClass="LabelNo titleheading" Text="Yarn Code"></asp:Label>
                    </td>
                    <td class="tdLeft SmallFont">
                        <asp:Label ID="Label20" runat="server" CssClass="LabelNo titleheading" Text="Adjust"></asp:Label>
                    </td>
                    <td class="tdRight SmallFont">
                        <asp:Label ID="Label21" runat="server" CssClass="LabelNo titleheading" Text="Quantity"></asp:Label>
                    </td>
                    <td class="tdLeft SmallFont">
                        <asp:Label ID="Label22" runat="server" CssClass="LabelNo titleheading" Text="UOM"></asp:Label>
                    </td>
                    <td class="tdRight SmallFont">
                        <asp:Label ID="Label23" runat="server" CssClass="LabelNo titleheading" Text="Basic Rate"></asp:Label>
                    </td>
                    <td class="tdLeft SmallFont">
                        <asp:Label ID="Label24" runat="server" CssClass="LabelNo titleheading" Text="Dis./Tax"></asp:Label>
                    </td>
                    <td class="tdRight SmallFont">
                        <asp:Label ID="Label25" runat="server" CssClass="LabelNo titleheading" Text="Final Rate"></asp:Label>
                    </td>
                    <td class="tdRight SmallFont">
                        <asp:Label ID="Label26" runat="server" CssClass="LabelNo titleheading" Text="Amount"></asp:Label>
                    </td>
                    <td class="tdLeft SmallFont">
                        <asp:Label ID="Label27" runat="server" CssClass="LabelNo titleheading" Text="Quotation No"></asp:Label>
                    </td>
                    <td class="tdLeft SmallFont">
                        <asp:Label ID="Label28" runat="server" CssClass="LabelNo titleheading" Text="Delivery Schedule"></asp:Label>
                    </td>
                    <td class="tdLeft SmallFont">
                    </td>
                </tr>
                <tr>
                    <td class="tdLeft">
                        <cc2:ComboBox ID="txtItemCode" runat="server" AutoPostBack="True" CssClass="SmallFont"
                            DataTextField="YARN_CODE" DataValueField="COMBINED" EmptyText="Find Item" EnableLoadOnDemand="true"
                            Height="200px" MenuWidth="980px" OnLoadingItems="txtItemCode_LoadingItems" OnSelectedIndexChanged="txtItemCode_SelectedIndexChanged" 
                            TabIndex="14" EnableVirtualScrolling="true" Width="100px">
                            <HeaderTemplate>
                                <div class="header c4">
                                    Code</div>
                                <div class="header c3">
                                    DESCRIPTION</div>
                                    <div class="header c1">
                                    LOT No</div>
                                    
                                 <div class="header c1">
                                    HSN #</div>
                                     <div class="header c1">
                                    Shade F#</div>
                                <div class="header c1">
                                    Shade #</div>
                                <div class="header c1 ralign">
                                    Appr Qty.</div>
                                <div class="header c1 ralign">
                                    Adj Qty</div>
                                <div class="header c1 ralign">
                                    Bal Qty
                                    </div>
                                    <div class="header c1 ralign">
                                    Grade
                                    </div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c4">
                                    <asp:Literal ID="Container4" runat="server" Text='<%# Eval("YARN_CODE") %>' />
                                </div>
                                <div class="item c3">
                                    <asp:Literal ID="Container5" runat="server" Text='<%# Eval("YARN_DESC") %>' />
                                </div>
                                 <div class="item c1">
                                    <asp:Literal ID="Literal6" runat="server" Text='<%# Eval("HSN_CODE") %>' />
                                </div>
                                 <div class="item c1">
                                    <asp:Literal ID="Literal2" runat="server" Text='<%# Eval("LOT_NO") %>' />
                                </div>
                                  <div class="item c1">
                                    <asp:Literal ID="Literal5" runat="server" Text='<%# Eval("SHADE_FAMILY") %>' />
                                </div>
                                <div class="item c1">
                                    <asp:Literal ID="Literal4" runat="server" Text='<%# Eval("SHADE_CODE") %>' />
                                </div>
                               <div class="item c1 ralign">
                                    <asp:Literal ID="Container6" runat="server" Text='<%# Eval("APPR_QTY") %>' />
                                </div>
                                <div class="item c1 ralign">
                                    <asp:Literal ID="Literal8" runat="server" Text='<%# Eval("PUR_ADJ_QTY") %>' />
                                </div>
                                <div class="item c1 ralign">
                                    <asp:Literal ID="Literal3" runat="server" Text='<%# Eval("BAL_QTY") %>' />
                                </div>
                                <%-- <div class="item c1 ralign">
                                    <asp:Literal ID="Literal7" runat="server" Text='<%# Eval("GRADE") %>' />
                                </div>--%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                        
                    </td>
                    <td class="tdLeft">
                    <asp:TextBox ID="lblItemCode" TabIndex="15" ReadOnly="true" CssClass="TextBox SmallFont TextBoxDisplay"
                            runat="server" Text='<%# Bind("YARN_CODE") %>' Width="100px"></asp:TextBox>
                    </td>
                    <td class="tdLeft">
                        <asp:Button ID="btnAdjustIndent" TabIndex="16" runat="server" Font-Size="8pt" Text="Ind.Ajust"
                            OnClick="btnAdjustIndent_Click1" />
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="txtOrderQty" TabIndex="17" CssClass="TextBoxNo SmallFont"
                            runat="server" Width="70px" Text='<%# Bind("ORD_QTY") %>' ReadOnly="false" AutoPostBack="True"
                            OnTextChanged="txtOrderQty_TextChanged"></asp:TextBox>
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="txtUnit" TabIndex="18" CssClass="TextBox SmallFont TextBoxDisplay"
                            runat="server" ReadOnly="true" Width="25px" Text='<%# Bind("UOM") %>'></asp:TextBox>
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="txtBaseRate" TabIndex="19" runat="server" Width="60px" Text='<%# Bind("BASIC_RATE") %>'
                            CssClass="TextBoxNo SmallFont" AutoPostBack="True" OnTextChanged="txtBaseRate_TextChanged1" MaxLength="8"></asp:TextBox>
                      
                      
                    </td>
                    <td class="tdLeft">
                        <asp:Button ID="btnDiscountTaxes" TabIndex="20" runat="server" Font-Size="8pt" Text="Disc/Taxes"
                            Width="65px" OnClick="btnDiscountTaxes_Click1" />
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="txtFinalRate" TabIndex="21" ReadOnly="true" runat="server" Text='<%# Bind("FINAL_RATE") %>'
                            Width="60px" CssClass="TextBoxNo SmallFont TextBoxDisplay" AutoPostBack="True"
                            OnTextChanged="txtFinalRate_TextChanged"></asp:TextBox>
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="txtAmount" TabIndex="22" Width="70px" ReadOnly="true" runat="server"
                            Text='<%# Bind("Amount") %>' CssClass="TextBoxNo SmallFont TextBoxDisplay "></asp:TextBox>
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="txtQuotation" TabIndex="23" Width="70px" runat="server" Text='<%# Bind("QUOTATION_NO") %>'
                            CssClass="TextBoxNo SmallFont" MaxLength="15"></asp:TextBox>
                    </td>
                    <td class="tdLeft">
                    
                    <asp:Button ID="btnDeliveryDate" runat="server" Text="Delivery Date" CssClass="SmallFont "
                                                Width="100%" OnClick="btnDeliveryDate_Click" />
                    
                    
                       <%-- <asp:TextBox ID="txtTrnDeliveryDate" TabIndex="24" runat="server" Width="60px" Text='<%# Bind("DEL_DATE") %>'
                            CssClass="TextBox SmallFont"></asp:TextBox>--%>
                        
                    </td>
                    <td class="tdLeft">
                        <asp:Button ID="btnSaveDetail" Text="Add" CssClass=" SmallFont" runat="server" OnClick="btnSaveDetail_Click" Width="50px" TabIndex="27">
                        
                        </asp:Button>
                        
                    </td>
                </tr>
                <tr>
                    <td class="tdLeft" colspan="11">
                        <asp:TextBox ID="txtItemDescription" TabIndex="25" ReadOnly="true" CssClass="TextBox SmallFont TextBoxDisplay"
                            runat="server" Text='<%# Bind("YARN_DESC") %>' Width="30%"></asp:TextBox>&nbsp;
                            <asp:Label ID="Label12" runat="server" CssClass="LabelNo titleheading" Text="HSN Code"></asp:Label><asp:TextBox ID="txtHSNCODE" TabIndex="27" ReadOnly="true" CssClass="TextBox SmallFont TextBoxDisplay"
                            runat="server"  Width="50px"></asp:TextBox>
                            <asp:Label ID="Label10" runat="server" CssClass="LabelNo titleheading" Text="Shade Family/Shade"></asp:Label>
                            <asp:TextBox ID="txtShadeFamily" TabIndex="19" ReadOnly="true" CssClass="TextBox SmallFont TextBoxDisplay"
                            runat="server"  Width="60px"></asp:TextBox>/
                            <asp:TextBox ID="txtShadeCode" TabIndex="26" ReadOnly="true" CssClass="TextBox SmallFont TextBoxDisplay"
                            runat="server"  Width="60px"></asp:TextBox>
                            <asp:Label ID="Label13" runat="server" CssClass="LabelNo titleheading" Text="Lot No/GRADE"></asp:Label>
                             <asp:TextBox ID="txtLotNo" TabIndex="19" ReadOnly="true" CssClass="TextBox SmallFont TextBoxDisplay"
                            runat="server"  Width="60px"></asp:TextBox>/
                            <asp:TextBox ID="txtGrade" TabIndex="26" ReadOnly="true" CssClass="TextBox SmallFont TextBoxDisplay"
                            runat="server"  Width="30px"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FiltertxtBaseRate1" runat="server"  TargetControlID="txtBaseRate"   FilterType="Custom, Numbers" ValidChars="."/>
                          
                    </td>
                    <td>
                    <asp:Button ID="btnCancelDetail" Text="Cancel" CssClass=" SmallFont" runat="server"
                            OnClick="btnCancelDetail_Click" Width="50px"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr width="100%">
        <td width="100%">
            <table width="100%">
                <tr id="trMaterialPOCreditTRN" runat="server">
                    <td width="100%" class="td SmallFont">
                        <asp:GridView ID="gvMaterialPOTRN" TabIndex="16" runat="server" OnRowCommand="gvMaterialPOTRN_RowCommand" OnRowDataBound="GridSpinningThread_RowDataBound"
                            CssClass="SmallFont" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="false" Width="100%" >
                            
                            <RowStyle CssClass="SmallFont" />
                           
                            <Columns>
                                <asp:TemplateField HeaderText="S.No." ItemStyle-VerticalAlign="top">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" Width="5%" />
                                    <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Yarn Code">
                                    <ItemTemplate>
                                        <asp:Label ID="txtItemCode" TabIndex="17"  Font-Bold="true" CssClass="Label SmallFont"
                                            runat="server" Text='<%# Bind("YARN_CODE") %>' AutoCompleteType="Disabled" ReadOnly="true"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="10%" />
                                    <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Yarn Description">
                                    <ItemTemplate>
                                        <asp:Label ID="txtItemDescription" TabIndex="19" ReadOnly="true" runat="server" 
                                            CssClass="Label SmallFont" Text='<%# Bind("YARN_DESC") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="20%" />
                                    <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LOT No.">
                                    <ItemTemplate>
                                        <asp:Label ID="txtLotNo" TabIndex="19" ReadOnly="true" runat="server" 
                                            CssClass="Label SmallFont" Text='<%# Bind("LOT_NO") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="8%" />
                                    <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="GRADE">
                                    <ItemTemplate>
                                        <asp:Label ID="txtGrade" TabIndex="19" ReadOnly="true" runat="server" 
                                            CssClass="Label SmallFont" Text='<%# Bind("GRADE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="5%" />
                                    <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="HSN Code">
                                    <ItemTemplate>
                                        <asp:Label ID="txtHSNCODE" TabIndex="19" ReadOnly="true" runat="server" 
                                            CssClass="Label SmallFont" Text='<%# Bind("HSN_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="8%" />
                                    <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="txtOrderQty" TabIndex="21" CssClass="LabelNo SmallFont" runat="server"
                                             Text='<%# Bind("ORD_QTY") %>' Font-Bold="true" ReadOnly="true"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" Width="8%" />
                                    <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UOM">
                                    <ItemTemplate>
                                        <asp:Label ID="txtUnit" TabIndex="22" CssClass="Label SmallFont" runat="server" ReadOnly="true"
                                           Text='<%# Bind("UOM") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="5%" />
                                    <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Shade Family">
                                    <ItemTemplate>
                                        <asp:Label ID="txtShadeFamily" TabIndex="22" CssClass="Label SmallFont" runat="server"
                                            ReadOnly="true"  Text='<%# Bind("SHADE_FAMILY") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="7%" />
                                    <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Shade">
                                    <ItemTemplate>
                                        <asp:Label ID="txtShadeCode" TabIndex="22" CssClass="Label SmallFont" runat="server"
                                            ReadOnly="true" Text='<%# Bind("SHADE_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="5%" />
                                    <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Base Rate">
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" Width="8%" />
                                    <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="txtBaseRate" TabIndex="23" runat="server"  Text='<%# Bind("BASIC_RATE") %>'
                                            CssClass="LabelNo SmallFont" ReadOnly="true"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Final Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="txtFinalRate" TabIndex="25" ReadOnly="true" runat="server" Text='<%# Bind("FINAL_RATE") %>'
                                            Font-Bold="true" CssClass="LabelNo SmallFont"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" Width="8%" />
                                    <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField  HeaderText="Amount" >
                                    <ItemTemplate >
                                        <asp:Label ID="txtAmount" TabIndex="26" ReadOnly="true"  runat="server"
                                            Text='<%# Bind("Amount") %>' Font-Bold="true" CssClass="LabelNo SmallFont"></asp:Label>
                                            
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" Width="8%" />
                                    <HeaderStyle  VerticalAlign="Top" HorizontalAlign="Right" />
                                   
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quotation No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCurr" runat="server" Text='<%# Bind("QUOTATION_nO") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="8%" />
                                    <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                
                                 <asp:TemplateField HeaderText="Indent Adjustment">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="Idn_Adj1" runat="server" CommandArgument='<%#Bind("SHADE_CODE") %>'
                                                                    Text="Adjustment" CommandName="ADJUST"></asp:LinkButton>
                                                                <asp:Panel ID="IdnPanel1" runat="server" BackColor="White">
                                                                    <asp:GridView runat="server" ID="Idn_grid1" AutoGenerateColumns="false" CssClass="SmallFont">
                                                                        <RowStyle CssClass="SmallFont" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="S&nbsp;No." ItemStyle-VerticalAlign="top" ItemStyle-Width="25px"
                                                                                HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                                                <ItemTemplate>
                                                                                    <%#Container.DataItemIndex+1 %>
                                                                                </ItemTemplate>
                                                                                <ItemStyle VerticalAlign="Top" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Indent No" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="txtIndentNo" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("IND_NUMB") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Indent Type" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblIndentType" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("IND_TYPE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            
                                                                            
                                                                            
                                                                             <asp:TemplateField HeaderText="Branch Name" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblBranchName" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("IND_BRANCH_NAME") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            
                                                                            
                                                                            <asp:TemplateField HeaderText="Yarn Code" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblYarnCode" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("YARN_CODE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Shade Code" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblShadeCode" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("SHADE_CODE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Adjustment Qty" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAdjustQty" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("ADJUST_QTY") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Approve Qty" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblApproveQty" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("APPR_QTY") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            
                                                                        </Columns>
                                                                        <RowStyle CssClass="SmallFont" />
                                                                        <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                                <cc4:HoverMenuExtender ID="idnHover1" runat="server" PopupControlID="IdnPanel1" PopupPosition="Left"
                                                                    TargetControlID="Idn_Adj1">
                                                                </cc4:HoverMenuExtender>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                
                                            <asp:TemplateField HeaderText="Indet Discount">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="Idn_Dis" runat="server" CommandName="DISCO" CommandArgument='<%#Bind("YARN_CODE") %>'
                                                                    Text="Discount"></asp:LinkButton>
                                                                <asp:Panel ID="IdnPanel2" runat="server" BackColor="White">
                                                                    <asp:GridView runat="server" ID="Idn_grid2" AutoGenerateColumns="false" CssClass="SmallFont">
                                                                        <RowStyle CssClass="SmallFont" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="S&nbsp;No." ItemStyle-VerticalAlign="top" ItemStyle-Width="25px"
                                                                                HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                                                <ItemTemplate>
                                                                                    <%#Container.DataItemIndex+1 %>
                                                                                </ItemTemplate>
                                                                                <ItemStyle VerticalAlign="Top" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Yarn Code" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="txtYarnCode" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("YARN_CODE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            
                                                                              <asp:TemplateField HeaderText="Shade Code" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblShadeCode" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("SHADE_CODE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            
                                                                            <asp:TemplateField HeaderText="Component" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblComponent" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("COMPO_CODE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            
                                                                            
                                                                             <asp:TemplateField HeaderText="Base Code" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblBaseCode" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("BASE_COMPO_CODE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            
                                                                            
                                                                             <asp:TemplateField HeaderText="Rate" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRate" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("RATE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAmount" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("AMOUNT") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            
                                                                        </Columns>
                                                                        <RowStyle CssClass="SmallFont" />
                                                                        <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                                <cc4:HoverMenuExtender ID="idnHover2" runat="server" PopupControlID="IdnPanel2" PopupPosition="Left"
                                                                    TargetControlID="Idn_Dis">
                                                                </cc4:HoverMenuExtender>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                
                                 <asp:TemplateField HeaderText="Delivery&nbsp;Date">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="Idn_Adj" runat="server" CommandArgument='<%#Bind("YARN_CODE") %>'
                                                                    Text="Delivery Dates" CommandName="DEL"></asp:LinkButton>
                                                                <asp:Panel ID="IdnPanel" runat="server" BackColor="White">
                                                                    <asp:GridView runat="server" ID="Idn_grid" AutoGenerateColumns="false" CssClass="SmallFont">
                                                                        <RowStyle CssClass="SmallFont" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="S&nbsp;No." ItemStyle-VerticalAlign="top" ItemStyle-Width="25px"
                                                                                HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                                                <ItemTemplate>
                                                                                    <%#Container.DataItemIndex+1 %>
                                                                                </ItemTemplate>
                                                                                <ItemStyle VerticalAlign="Top" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="QTY" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="txtQTY" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("QUANTITY") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Delivery&nbsp;Date" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDeliveryDate" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("DELIVERY_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <RowStyle CssClass="SmallFont" />
                                                                        <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                                <cc4:HoverMenuExtender ID="idnHover" runat="server" PopupControlID="IdnPanel" PopupPosition="Left"
                                                                    TargetControlID="Idn_Adj">
                                                                </cc4:HoverMenuExtender>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" TabIndex="29" runat="server" Text="Edit" CommandName="POMateialCreditEdit"
                                            CommandArgument='<%# Eval("UniqueId") %>' OnClientClick="return confirm('Are you Sure want to Edit this  Detail?');"></asp:LinkButton>
                                        /
                                        <asp:LinkButton ID="lnkDelete" TabIndex="29" runat="server" Text="Del" CommandName="POMateialCreditDelete"
                                            CommandArgument='<%# Eval("UniqueId") %>'  OnClientClick="return confirm('Are you Sure want to delete this  Detail?');"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="10%" />
                                    <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                </asp:TemplateField>
                               
                            </Columns>
                            <RowStyle CssClass="SmallFont" />
                            <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                            
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>


 <%-- <Triggers>
        <asp:AsyncPostBackTrigger ControlID="imgbtnClear" EventName="Click"></asp:AsyncPostBackTrigger>
        <asp:AsyncPostBackTrigger ControlID="imgbtnFind" EventName="Click"></asp:AsyncPostBackTrigger>
        <asp:AsyncPostBackTrigger ControlID="imgbtnUpdate" EventName="Click"></asp:AsyncPostBackTrigger>
        <asp:AsyncPostBackTrigger ControlID="txtDeliveryDate" EventName="TextChanged"></asp:AsyncPostBackTrigger>
        <asp:AsyncPostBackTrigger ControlID="txtOrderNumber" EventName="TextChanged"></asp:AsyncPostBackTrigger>
        <asp:AsyncPostBackTrigger ControlID="txtPartycode" EventName="TextChanged"></asp:AsyncPostBackTrigger>
        <asp:AsyncPostBackTrigger ControlID="txtTransporterCode" EventName="TextChanged">
        </asp:AsyncPostBackTrigger>
    </Triggers>--%>