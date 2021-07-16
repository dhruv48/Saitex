<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SewingThreadMst.ascx.cs"
    Inherits="Module_Sewing_Thread_Controls_SewingThreadMst" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc1" %>

<script language="javascript" type="text/javascript">
    function CalculationTotalNoOfUnits() {
        document.getElementById('ctl00_cphBody_SewingThreadMst1_txtTotalNoOfUnits').value = (parseFloat(document.getElementById('ctl00_cphBody_SewingThreadMst1_txtBoxPkg').value) * parseFloat(document.getElementById('ctl00_cphBody_SewingThreadMst1_txtCartonPkg').value)).toFixed(3);
    }
    function CalculationNetBoxWt() {
        document.getElementById('ctl00_cphBody_SewingThreadMst1_txtNetBoxWt').value = (parseFloat(document.getElementById('ctl00_cphBody_SewingThreadMst1_txtUnitWt').value) * parseFloat(document.getElementById('ctl00_cphBody_SewingThreadMst1_txtBoxPkg').value)).toFixed(3);
    }


    function CalculationEmptyCarton() {
        //       var EmptyCarton ;
        //       var netCarton;
        //       var unitwt;
        //       var totalnoofUnit;
        //       netCarton =  parseFloat(document.getElementById('ctl00_cphBody_SewingThreadMst1_txtNetCarton').value);
        //       unitwt= parseFloat(document.getElementById('ctl00_cphBody_SewingThreadMst1_txtUnitWt').value);
        //       totalnoofUnit=   parseFloat(document.getElementById('ctl00_cphBody_SewingThreadMst1_txtTotalNoOfUnits').innerHTML);
        //       EmptyCarton = (netCarton - (unitwt *totalnoofUnit)).toFixed(3);
        //       document.getElementById('ctl00_cphBody_SewingThreadMst1_txtEmptyCarton').innerHTML  = EmptyCarton;
        document.getElementById('ctl00_cphBody_SewingThreadMst1_txtEmptyCarton').value = parseFloat(document.getElementById('ctl00_cphBody_SewingThreadMst1_txtNetCarton').value) - (parseFloat(document.getElementById('ctl00_cphBody_SewingThreadMst1_txtUnitWt').value) * parseFloat(document.getElementById('ctl00_cphBody_SewingThreadMst1_txtTotalNoOfUnits').value)).toFixed(3);
    }
    // Added By Rajesh 01 Dec 2011 (Guided By Akhilesh Sir)
    function CalculationNetCartonWt() {
        document.getElementById('ctl00_cphBody_SewingThreadMst1_txtNetCarton').value = (parseFloat(document.getElementById('ctl00_cphBody_SewingThreadMst1_txtUnitWt').value) * parseFloat(document.getElementById('ctl00_cphBody_SewingThreadMst1_txtTotalNoOfUnits').value)).toFixed(3);
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
        width: 120px;
    }
    .c2
    {
        margin-left: 4px;
        width: 150px;
    }
    .c3
    {
        margin-left: 4px;
        width: 200px;
    }
    .ralign
    {
        text-align: right;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <table cellpadding="0" cellspacing="0" border="0" align="left" class="tContentArial ">
            <tr>
                <td class="td">
                    <table cellpadding="0" cellspacing="0" align="left" class="tContentArial ">
                        <tr>
                            <td id="tdSave" runat="server">
                                <asp:ImageButton ID="imgbtnSave" runat="server" Width="48" Height="41" ToolTip="Save"
                                    ValidationGroup="M1" ImageUrl="~/CommonImages/save.jpg" 
                                    OnClick="imgbtnSave_Click" TabIndex="1" />
                            </td>
                            <td id="tdUpdate" runat="server">
                                <asp:ImageButton ID="imgbtnUpdate" Width="48" Height="41" runat="server" ToolTip="Update"
                                    ImageUrl="~/CommonImages/edit1.jpg" OnClick="imgbtnUpdate_Click" 
                                    ValidationGroup="M1" TabIndex="2">
                                </asp:ImageButton>
                            </td>
                            <td id="tdDelete" runat="server">
                                <asp:ImageButton ID="imgbtnDelete" Width="48" Height="41" runat="server" ToolTip="Delete"
                                    ImageUrl="~/CommonImages/del6.png" OnClick="imgbtnDelete_Click" 
                                    TabIndex="3"></asp:ImageButton>
                            </td>
                            <td id="tdFind" runat="server">
                                <asp:ImageButton ID="imgbtnFind" runat="server" Width="48" Height="41" ToolTip="Find"
                                    ImageUrl="~/CommonImages/link_find.png" OnClick="imgbtnFind_Click" 
                                    TabIndex="4"></asp:ImageButton>
                            </td>
                            <td id="tdClear" runat="server">
                                <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" ToolTip="Clear"
                                    ImageUrl="~/CommonImages/clear.jpg" OnClick="imgbtnClear_Click" 
                                    TabIndex="5"></asp:ImageButton>
                            </td>
                            <td id="tdPrint" runat="server">
                                <asp:ImageButton ID="imgbtnPrint" Width="48" Height="41" runat="server" ToolTip="Print"
                                    ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click" 
                                    TabIndex="6"></asp:ImageButton>
                            </td>
                            <td id="tdExit" runat="server">
                                <asp:ImageButton ID="imgbtnExit" Width="48" Height="41" runat="server" ToolTip="Exit"
                                    ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click" 
                                    TabIndex="7"></asp:ImageButton>
                            </td>
                            <td id="tdHelp" runat="server">
                                <asp:ImageButton ID="imgbtnHelp" Width="48" Height="41" runat="server" ToolTip="Help"
                                    ImageUrl="~/CommonImages/link_help.png" OnClick="imgbtnHelp_Click" 
                                    TabIndex="8"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="TableHeader td" align="center">
                    <b class="titleheading">Sewing Thread Master</b>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <table cellpadding="0" cellspacing="0" border="0" align="left" class="tContentArial">
                        <tr>
                            <td align="left" class="td" colspan="6" valign="top">
                                <span class="Mode">You are in
                                    <asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode </span>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="td" colspan="6" valign="top">
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                                    ShowSummary="false" ValidationGroup="STT" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="false" ValidationGroup="M1" />
                                <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label><strong>
                                </strong>
                                <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label><strong>
                                </strong>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                Article No :
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtArticleNo" runat="server" CssClass="TextBox UpperCase" ValidationGroup="M1"
                                    MaxLength="25" TabIndex="9"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFArticleNo" runat="server" ControlToValidate="txtArticleNo"
                                    Display="None" ErrorMessage="Please Enter Article Number" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="M1"></asp:RequiredFieldValidator>
                                <cc1:ComboBox ID="cmbArticleNo" runat="server" Width="133px" Height="200px" AutoPostBack="True"
                                    EnableLoadOnDemand="True" EmptyText="Select Article No" DataTextField="YARN_CODE"
                                    EnableVirtualScrolling="true" DataValueField="YARN_CODE" TabIndex="10" MenuWidth="550px"
                                    OnLoadingItems="cmbArticleNo_LoadingItems" 
                                    OnSelectedIndexChanged="cmbArticleNo_SelectedIndexChanged">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Category</div>
                                        <div class="header c1">
                                            Article No</div>
                                        <div class="header c3">
                                            Article Desc</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <%# Eval("YARN_CAT")%></div>
                                        <div class="item c1">
                                            <%# Eval("YARN_CODE")%></div>
                                        <div class="item c3">
                                            <%# Eval("YARN_DESC")%></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc1:ComboBox>
                            </td>
                            <td align="right" valign="top">
                                &nbsp;Article Description:
                            </td>
                            <td align="left" valign="top" colspan="3">
                                &nbsp;<asp:TextBox ID="txtArticleDescription" runat="server" 
                                    CssClass="TextBox" Width="99%"
                                    ValidationGroup="M1" TabIndex="11" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                Make:
                            </td>
                            <td align="left" valign="top">
                                <asp:DropDownList ID="ddlMake" runat="server" AppendDataBoundItems="true" DataTextField="UOM"
                                    DataValueField="UOM" CssClass="SmallFont" TabIndex="12" Width="133px" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlMake_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td align="right" valign="top">
                                UOM :
                            </td>
                            <td align="left" valign="top">
                                <asp:DropDownList ID="ddlUOM" runat="server" AppendDataBoundItems="true" CssClass="SmallFont"
                                    TabIndex="13" Width="133px">
                                    <asp:ListItem>BASE UNIT</asp:ListItem>
                                    <asp:ListItem>BOX</asp:ListItem>
                                    <asp:ListItem>CARTON</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="right" valign="top">
                                TKT No :
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtTKTNo" runat="server" CssClass="TextBox UpperCase" ValidationGroup="M1"
                                    MaxLength="15" TabIndex="14"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFTKTNo" runat="server" ControlToValidate="txtTKTNo"
                                    Display="None" ErrorMessage="Please Enter TKT Number" SetFocusOnError="True"
                                    ValidationGroup="M1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                Ply:
                            </td>
                            <td align="left" valign="top">
                                <asp:DropDownList ID="ddlPLy" runat="server" AppendDataBoundItems="true" DataTextField="UOM"
                                    DataValueField="UOM" CssClass="SmallFont" TabIndex="15" Width="133px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFUOM3" runat="server" ControlToValidate="ddlPLy"
                                    Display="None" ErrorMessage="Please Select Ply" InitialValue="Select" SetFocusOnError="True"
                                    ValidationGroup="M1"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right" valign="top">
                                &nbsp;Count:
                            </td>
                            <td align="left" valign="top">
                                <asp:DropDownList ID="ddlCount" runat="server" AppendDataBoundItems="true" DataTextField="UOM"
                                    DataValueField="UOM" CssClass="SmallFont" TabIndex="16" Width="133px" 
                                    OnSelectedIndexChanged="ddlCount_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFUOM4" runat="server" ControlToValidate="ddlCount"
                                    Display="None" ErrorMessage="Please Select Count" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="M1"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right" valign="top">
                                Tex Size :
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtTexSize" runat="server" CssClass="TextBoxNo" ValidationGroup="M1"
                                    MaxLength="8" TabIndex="17"></asp:TextBox>
                                <asp:RangeValidator ID="RVTexSize" runat="server" ValidationGroup="M1" Display="None"
                                    ControlToValidate="txtTexSize" MinimumValue="0" MaximumValue="9999999" ErrorMessage="Enter only number in Tex Size"
                                    Type="Double" SetFocusOnError="true"></asp:RangeValidator>
                                    
                                <asp:RequiredFieldValidator ID="RFTexSize" runat="server" ControlToValidate="txtTexSize"
                                    Display="None" ErrorMessage="Please Enter Tex Size" SetFocusOnError="True" ValidationGroup="M1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                TPI :
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtTPI" runat="server" CssClass="TextBoxNo" ValidationGroup="M1"
                                    MaxLength="10" TabIndex="18"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFTPI" runat="server" ControlToValidate="txtTPI"
                                    Display="None" ErrorMessage="Please Enter TPI" SetFocusOnError="True" ValidationGroup="M1"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RVTPI" runat="server" ValidationGroup="M1" Display="None"
                                    ControlToValidate="txtTPI" MinimumValue="0" MaximumValue="9999999" ErrorMessage="Enter only number in TPI"
                                    Type="Double" SetFocusOnError="true"></asp:RangeValidator>
                                  
                            </td>
                            <td align="right" valign="top">
                                Twist :
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtTwist" runat="server" CssClass="TextBox" ValidationGroup="M1"
                                    MaxLength="50" TabIndex="19"></asp:TextBox>
                            </td>
                            <td align="right" valign="top">
                                Quality :
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtQuality" runat="server" CssClass="TextBox" ValidationGroup="M1"
                                    MaxLength="10" TabIndex="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFQuality" runat="server" ControlToValidate="txtQuality"
                                    Display="None" ErrorMessage="Please Enter Quality" SetFocusOnError="True" ValidationGroup="M1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                Length-Mtr :
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtLengthMtr" runat="server" CssClass="TextBox" ValidationGroup="M1"
                                    MaxLength="20" TabIndex="21"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFLengthMtr" runat="server" ControlToValidate="txtLengthMtr"
                                    Display="None" ErrorMessage="Please Enter Length" SetFocusOnError="True" ValidationGroup="M1"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right" valign="top">
                                Colour Of Make :
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtColourOfUnit" runat="server" CssClass="TextBox" ValidationGroup="M1"
                                    MaxLength="50" TabIndex="22"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFColourOfUnit" runat="server" ControlToValidate="txtColourOfUnit"
                                    Display="None" ErrorMessage="Please Enter Colour Unit" SetFocusOnError="True"
                                    ValidationGroup="M1"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right" valign="top">
                                Brand :
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtBrand" runat="server" CssClass="TextBox" ValidationGroup="M1"
                                    MaxLength="100" TabIndex="23"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                Unit Weight :
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtUnitWt" runat="server" CssClass="TextBoxNo" ValidationGroup="M1"
                                    onkeyup="javascript:CalculationTotalNoOfUnits();CalculationNetBoxWt();CalculationNetCartonWt();"
                                    MaxLength="15" TabIndex="24" AutoPostBack="false"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFUnitWt" runat="server" ControlToValidate="txtUnitWt"
                                    Display="None" ErrorMessage="Please Enter Unit Weight" SetFocusOnError="True"
                                    ValidationGroup="M1"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RVUnitWt" runat="server" ValidationGroup="M1" Display="None"
                                    ControlToValidate="txtUnitWt" MinimumValue="0" MaximumValue="9999999" ErrorMessage="Enter only number in Unit Weight"
                                    Type="Double" SetFocusOnError="true"></asp:RangeValidator>
                                   
                            </td>
                            <td align="right" valign="top">
                                Uom of Unit Weight :
                            </td>
                            <td align="left" valign="top">
                                <asp:DropDownList ID="ddlUOMUnitWeight" runat="server" AppendDataBoundItems="true"
                                    DataTextField="UOM" DataValueField="UOM" CssClass="SmallFont" 
                                    TabIndex="25" Width="133px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFUOM" runat="server" ControlToValidate="ddlUOMUnitWeight"
                                    Display="None" ErrorMessage="Please Select Unit" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="M1"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right" valign="top">
                                Unit-Size (Inch) :
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtUnitSize" runat="server" CssClass="TextBoxNo" ValidationGroup="M1"
                                    MaxLength="15" TabIndex="26"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFUnitSize" runat="server" ControlToValidate="txtUnitSize"
                                    Display="None" ErrorMessage="Please Enter Unit Size" SetFocusOnError="True" ValidationGroup="M1"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RVUnitSize" runat="server" ValidationGroup="M1" Display="None"
                                    ControlToValidate="txtUnitSize" MinimumValue="0" MaximumValue="9999999" ErrorMessage="Enter only number in Unit Size"
                                    Type="Double" SetFocusOnError="true"></asp:RangeValidator>
                                   
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                Box Pkg - No of Unit/Box :
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtBoxPkg" runat="server" CssClass="TextBoxNo" ValidationGroup="M1"
                                    onkeyup="javascript:CalculationTotalNoOfUnits();CalculationNetBoxWt();CalculationEmptyCarton();CalculationNetCartonWt();"
                                    MaxLength="15" TabIndex="27"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFBoxPkg" runat="server" ControlToValidate="txtBoxPkg"
                                    Display="None" ErrorMessage="Please Enter Box Pkg" SetFocusOnError="True" ValidationGroup="M1"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RVBoxPkg" runat="server" ValidationGroup="M1" Display="None"
                                    ControlToValidate="txtBoxPkg" MinimumValue="0" MaximumValue="9999999" ErrorMessage="Enter only number in Box Pkg"
                                    Type="Double" SetFocusOnError="true"></asp:RangeValidator>
                                    
                            </td>
                            <td align="right" valign="top">
                                Carton Pkg - No of Boxes/Carton :
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtCartonPkg" onkeyup="javascript:CalculationTotalNoOfUnits();CalculationNetBoxWt();CalculationEmptyCarton();CalculationNetCartonWt();"
                                    runat="server" CssClass="TextBoxNo" ValidationGroup="M1" MaxLength="19" 
                                    TabIndex="28"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFCartonPkg" runat="server" ControlToValidate="txtCartonPkg"
                                    Display="None" ErrorMessage="Please Enter Carton Pkg" SetFocusOnError="True"
                                    ValidationGroup="M1"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RVCartonPkg" runat="server" ValidationGroup="M1" Display="None"
                                    ControlToValidate="txtCartonPkg" MinimumValue="0" MaximumValue="9999999" ErrorMessage="Enter only number in Carton Pkg"
                                    Type="Double" SetFocusOnError="true"></asp:RangeValidator>
                                   
                            </td>
                            <td align="right" valign="top">
                                Total No of Units :
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtTotalNoOfUnits" runat="server" CssClass="TextBoxNo TextBoxDisplay"
                                    ValidationGroup="M1" onkeyup="javascript:CalculationTotalNoOfUnits();CalculationNetBoxWt();CalculationEmptyCarton();CalculationNetCartonWt();"
                                    MaxLength="15"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFTotalNoOfUnits" runat="server" ControlToValidate="txtTotalNoOfUnits"
                                    Display="None" ErrorMessage="Please Enter Total No Of Units" SetFocusOnError="True"
                                    ValidationGroup="M1"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RVTotalNoOfUnits" runat="server" ValidationGroup="M1" Display="None"
                                    ControlToValidate="txtTotalNoOfUnits" MinimumValue="0" MaximumValue="9999999"
                                    ErrorMessage="Enter only number in Total No Of Units" Type="Double" SetFocusOnError="true"></asp:RangeValidator>
                                   
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                &nbsp;Net Carton Wt :
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtNetCarton" onkeyup="javascript:CalculationNetCartonWt();CalculationEmptyCarton();"
                                    runat="server" CssClass="TextBoxNo TextBoxDisplay" ValidationGroup="M1" 
                                    MaxLength="15"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFNetCarton" runat="server" ControlToValidate="txtNetCarton"
                                    Display="None" ErrorMessage="Please Enter Net Carton Weight" SetFocusOnError="True"
                                    ValidationGroup="M1"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RVNetCarton" runat="server" ValidationGroup="M1" Display="None"
                                    ControlToValidate="txtNetCarton" MinimumValue="0" MaximumValue="9999999" ErrorMessage="Enter only number in Net Carton Weight"
                                    Type="Double" SetFocusOnError="true"></asp:RangeValidator>
                                 
                            </td>
                            <td align="right" valign="top">
                                Tarif Sub Heading:
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtTarifSubheading" CssClass="TextBox SmallFont" runat="server"
                                    ValidationGroup="M1" MaxLength="15" TabIndex="31"></asp:TextBox>
                            </td>
                            <td align="right" valign="top">
                                Empty Catron Weight:
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtEmptyCarton" onkeyup="javascript:CalculationTotalNoOfUnits();CalculationNetBoxWt();CalculationEmptyCarton();CalculationNetCartonWt();"
                                    CssClass="TextBoxNo TextBoxDisplay" runat="server" MaxLength="15"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFTPI2" runat="server" ControlToValidate="txtEmptyCarton"
                                    Display="None" ErrorMessage="Please Enter Empty Carton" SetFocusOnError="True"
                                    ValidationGroup="M1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                Is Exciseable:
                            </td>
                            <td align="left" valign="top">
                                <asp:CheckBox ID="IsExciseable" runat="server" TabIndex="33" />
                            </td>
                            <td align="right" valign="top">
                                Net Box Wt :
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtNetBoxWt" runat="server" 
                                    CssClass="TextBoxNo TextBoxDisplay" ValidationGroup="M1"
                                    MaxLength="15" 
                                    
                                    onkeyup="javascript:CalculationTotalNoOfUnits();CalculationNetBoxWt();CalculationEmptyCarton();CalculationNetCartonWt();"></asp:TextBox>
                                <asp:RangeValidator ID="RVNetBoxWt" runat="server" ValidationGroup="M1" Display="None"
                                    ControlToValidate="txtNetBoxWt" MinimumValue="0" MaximumValue="9999999" ErrorMessage="Enter only number in Net Box Weight"
                                    Type="Double" SetFocusOnError="true"></asp:RangeValidator>
                                <asp:RequiredFieldValidator ID="RFNetBoxWt" runat="server" ControlToValidate="txtNetBoxWt"
                                    Display="None" ErrorMessage="Please Enter Total Box Weight" SetFocusOnError="True"
                                    ValidationGroup="M1"></asp:RequiredFieldValidator>
                                   
                            </td>
                            <td align="right" valign="top">
                                &nbsp; End Use :
                            </td>
                            <td align="left" valign="top">
                                <asp:DropDownList ID="ddlenduse" runat="server" CssClass="TextBox" TabIndex="35"
                                    Width="125px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                Opening Stock :
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtOpeningStock" runat="server" CssClass="TextBoxNo" ValidationGroup="M1"
                                    MaxLength="10" TabIndex="36"></asp:TextBox>
                                <asp:RangeValidator ID="RVOpeningStock" runat="server" ValidationGroup="M1" Display="None"
                                    ControlToValidate="txtOpeningStock" MinimumValue="0" MaximumValue="9999999" ErrorMessage="Enter only number in Opening Stock"
                                    Type="Double" SetFocusOnError="true"></asp:RangeValidator>
                                <asp:RequiredFieldValidator ID="RFOpeningStock" runat="server" ControlToValidate="txtOpeningStock"
                                    Display="None" ErrorMessage="Please Enter Opening Stock" SetFocusOnError="True"
                                    ValidationGroup="M1"></asp:RequiredFieldValidator>
                                   
                            </td>
                            <td align="right" valign="top">
                                &nbsp;Opening Rate :
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtOpeningRate" runat="server" CssClass="TextBoxNo" ValidationGroup="M1"
                                    MaxLength="10" TabIndex="37"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFOpeningRate" runat="server" ControlToValidate="txtOpeningRate"
                                    Display="None" ErrorMessage="Please Enter Opening Rate" SetFocusOnError="True"
                                    ValidationGroup="M1"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RVOpeningRate" runat="server" ValidationGroup="M1" Display="None"
                                    ControlToValidate="txtOpeningRate" MinimumValue="0" MaximumValue="9999999" ErrorMessage="Enter only number in Opening Rate"
                                    Type="Double" SetFocusOnError="true"></asp:RangeValidator>
                                 
                            </td>
                            <td align="right" valign="top">
                                &nbsp;
                            </td>
                            <td align="left" valign="top">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <b>Base Article Detail....</b>
                    <table width="98%">
                        <tr bgcolor="#006699">
                            <td align="left" class="tdLeft SmallFont" valign="top">
                                <span class="titleheading"><b>Product Type</b></span>
                            </td>
                            <td align="left" class="tdLeft SmallFont" valign="top">
                                <span class="titleheading"><b>Article Code</b></span>
                            </td>
                            <td align="left" class="tdLeft SmallFont" valign="top">
                                <span class="titleheading"><b>UOM</b></span>
                            </td>
                            <td align="left" class="tdLeft SmallFont" valign="top">
                                <span class="titleheading"><b>Value Qty</b></span>
                            </td>
                            <td align="left" class="tdLeft SmallFont" valign="top">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                <asp:DropDownList ID="ddlProductType" runat="server" AppendDataBoundItems="True"
                                    AutoPostBack="True" CssClass="SmallFont" OnSelectedIndexChanged="ddlProductType_SelectedIndexChanged"
                                    TabIndex="38" Width="125">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator55" runat="server" ControlToValidate="ddlProductType"
                                    Display="None" ErrorMessage="Please Select Product Type" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="STT"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left" valign="top">
                                <asp:DropDownList ID="txtBaseArticleCode" runat="server" AppendDataBoundItems="True"
                                    CssClass="SmallFont TextBoxDisplay" TabIndex="39" Width="125">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator58" runat="server" ControlToValidate="txtBaseArticleCode"
                                    Display="None" ErrorMessage="Please Select Base UOM" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="STT"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left" valign="top">
                                <asp:DropDownList ID="ddlBaseUOM" runat="server" AppendDataBoundItems="True" CssClass="SmallFont"
                                    TabIndex="40" Width="125">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator56" runat="server" ControlToValidate="ddlBaseUOM"
                                    Display="None" ErrorMessage="Please Select Base UOM" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="STT"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtValueQty" runat="server" CssClass="SmallFont TextBoxNo" MaxLength="6"
                                    TabIndex="41" Width="39px"></asp:TextBox>
                                (Single Unit)<br />
                                <asp:RangeValidator ID="RangeValidator17" runat="server" ControlToValidate="txtValueQty"
                                    Display="None" ErrorMessage="Please Enter  Value Quantity in Numeric &amp; Precision Should be 7 and Scale 2   "
                                    MaximumValue="9999999.99" MinimumValue="0" Type="Double" ValidationGroup="STT"></asp:RangeValidator>
                                    <cc4:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" FilterType="Numbers"
                                                    TargetControlID="txtValueQty">
                                                </cc4:FilteredTextBoxExtender>
                            </td>
                            <td align="left" valign="top">
                                <asp:Button ID="BtnBaseSave" runat="server" OnClick="BtnBaseSave_Click" Text="Save"
                                    ValidationGroup="STT" TabIndex="42" />
                                <asp:Button ID="BtnBaseCancel" runat="server" OnClick="BtnBaseCancel_Click" Text="Cancel"
                                    TabIndex="43" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="6" valign="top">
                                <asp:GridView ID="grdBaseArticleDetail" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                    BorderWidth="1px" TabIndex="36" CssClass="SmallFont" Font-Bold="False" OnRowCommand="grdBaseArticleDetail_RowCommand"
                                    Width="98%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr No." ItemStyle-VerticalAlign="top" ItemStyle-Width="40px">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" Width="40px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ProductType">
                                            <ItemTemplate>
                                                <asp:Label ID="txtProductType" runat="server" Text='<%# Bind("ProductType") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Article Code">
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemTemplate>
                                                <asp:Label ID="txtArticleCode" runat="server" CssClass="Label SmallFont" Text='<%# Bind("ArticleCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UOM">
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblBaseUOM" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("UOM") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Basis" Visible="false" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblBasis" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("Basis") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Value Qty" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblValueQty" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("ValueQty") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemStyle HorizontalAlign="Center" Width="150px" />
                                            <ItemTemplate>
                                                <asp:Button ID="lnkEdit0" runat="server" CommandArgument='<%# Eval("UniqueId") %>'
                                                    CommandName="BaseEdit" Text="Edit" />
                                                <asp:Button ID="lnkDelete0" runat="server" CommandArgument='<%# Eval("UniqueId") %>'
                                                    CommandName="BaseDelete" OnClientClick="return confirm('Are you Sure want to delete this Bland?');"
                                                    Text="Delete" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="RowStyle SmallFont" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                    <PagerStyle CssClass="PagerStyle" />
                                    <HeaderStyle BackColor="#336699" CssClass="HeaderStyle SmallFont" ForeColor="White" />
                                </asp:GridView>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
