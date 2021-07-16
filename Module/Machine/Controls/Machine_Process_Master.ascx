<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Machine_Process_Master.ascx.cs"
    Inherits="Module_Machine_Controls_Machine_Process_Master" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script language="javascript" type="text/javascript">

    //    function Calculation(val)
    //    { 
    //        var name=val;
    //                   
    //        document.getElementById('ctl00_cphBody_POCredit1_txtAdvanceAmount').value=(parseFloat(document.getElementById('ctl00_cphBody_POCredit1_txtAdvance').value)*(parseFloat(document.getElementById('ctl00_cphBody_POCredit1_txtFinalTotal').value)/100)).toFixed(3) ;
    //     }           
    ////    function SetFocus(ControlId)
    ////    {    
    ////        document.getElementById(ControlId).focus();       
    ////    }
    //    function GetClick(ButtonId)
    //    {
    //        document.getElementById(ButtonId).click();
    //    }
    //   
   
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
        margin-left: 4px;
    }
    .c1
    {
        width: 80px;
    }
    .c2
    {
        margin-left: 4px;
        width: 100px;
    }
    .c3
    {
        margin-left: 4px;
        width: 100px;
    }
    .SmallFont
    {
        width: 0%;
    }
    .style1
    {
        width: 10%;
    }
    .style2
    {
        height: 23px;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
<table class="tdMain" width="95%">
    <tr>
        <td align="left" class="td" valign="top" width="100%" colspan="2">
            <table align="left">
                <tr>
                    <td>
                        <asp:ImageButton ID="imgbtnSave" runat="server" Height="41" ImageUrl="~/CommonImages/save.jpg"
                            ToolTip="Save" ValidationGroup="PM" Width="48" OnClick="imgbtnSave_Click" />
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" Height="41" ImageUrl="~/CommonImages/edit1.jpg"
                            OnClick="imgbtnUpdate_Click" ToolTip="Update" ValidationGroup="PM" Width="48" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnFind" runat="server" Height="41" ImageUrl="~/CommonImages/link_find.png"
                            OnClick="imgbtnFind_Click" ToolTip="Find" Width="48" OnClientClick="if (!confirm('Are you sure to Find the record ?')) { return false; }" />
                    </td>
                    <td id="tdPrint" runat="server">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                            OnClick="imgbtnPrint_Click" ToolTip="Print" Width="48" OnClientClick="if (!confirm('Are you sure to Print the record ?')) { return false; }" />
                        &nbsp;
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                            OnClick="imgbtnClear_Click" ToolTip="Clear" Width="48" OnClientClick="if (!confirm('Are you sure to Clear the record ?')) { return false; }" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                            OnClick="imgbtnExit_Click" ToolTip="Exit" Width="48" OnClientClick="if (!confirm('Are you sure to Exit ?')) { return false; }" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                            OnClick="imgbtnHelp_Click" ToolTip="Help" Width="48" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader td  " width="100%" colspan="2">
            <span class="titleheading"><b>Machine Process Master </b></span>&nbsp;
        </td>
    </tr>
    <tr>
        <td align="left" valign="top" class="td" colspan="2">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>
                &nbsp;Mode </span>
        </td>
    </tr>
    <tr>
        <td class="td SmallFont" width="100%" colspan="2">
            <table class="tContentArial" width="100%">
                <tr>
                    <td align="right" valign="top" width="10%">
                        <asp:Label ID="lblFindprocess" runat="server" Text="Process Code:" Visible="False"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="90%">
                        <cc1:ComboBox ID="ddlProcessCode" runat="server" AutoPostBack="True" CssClass="SmallFont"
                            DataTextField="PROS_CODE" DataValueField="PRODUCT_TYPE" EmptyText="Find Item"
                            EnableLoadOnDemand="true" Height="200px" MenuWidth="350px" OnLoadingItems="ddlProcessCode_LoadingItems"
                            OnSelectedIndexChanged="ddlProcessCode_SelectedIndexChanged" TabIndex="1" Visible="False"
                            Width="150px">
                            <HeaderTemplate>
                                <div class="header c1">
                                    PROS CODE</div>
                                <div class="header c2">
                                    PROS DESC</div>
                                <div class="header c3">
                                    PRODUCT TYPE</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal ID="Container1" runat="server" Text='<%# Eval("PROS_CODE") %>' />
                                </div>
                                <div class="item c2">
                                    <asp:Literal ID="Container2" runat="server" Text='<%# Eval("PROS_DESC") %>' />
                                </div>
                                <div class="item c3">
                                    <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("PRODUCT_TYPE") %>' />
                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc1:ComboBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="10%">
                        Product Type:
                    </td>
                    <td align="left" valign="top" width="90%">
                        <asp:DropDownList ID="ddProducttype" runat="server" Width="130" AutoPostBack="True"
                            OnSelectedIndexChanged="ddProducttype_SelectedIndexChanged" CssClass="SmallFont">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="10%">
                        <asp:Label ID="lblprocessCodeSave" runat="server" Text="Process Code :" CssClass="LabelNo"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="90%">
                        <%--<cc3:OboutDropDownList ID="ddlOrderType" runat="server" AutoPostBack="True" 
                                    CssClass="TextBox SmallFont" 
                                    OnSelectedIndexChanged="ddlOrderType_SelectedIndexChanged" TabIndex="1" 
                                    Width="150px">
                                    <asp:ListItem Value="PUM">Main Order</asp:ListItem>
                                    <asp:ListItem Value="PUS">Supplimentry Order</asp:ListItem>
                                </cc3:OboutDropDownList>--%><asp:TextBox ID="txtProcessCode" 
                            runat="server" CssClass="SmallFont TextBoxNo"
                                    Width="120px"></asp:TextBox>
                    </td>
                </tr>
                <tr Width="100%">
                    <td align="right" valign="top" width="10%">
                        Description:
                    </td>
                    <td align="left" valign="top" width="90%">
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="gCtrTxt TextBox" Rows="2"
                            TabIndex="5" Width="98%"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <td class="td" width="100%" colspan="2">
        <table width="100%" class="tContentArial">
            <tr>
                <td align="right" valign="top" width="10%">
                    Machine Group:
                </td>
                <td align="left" valign="top" width="10%">
                    <%-- <asp:DropDownList ID="ddlMachineCode" runat="server" Width="102px" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlMachineCode_SelectedIndexChanged">
                    </asp:DropDownList>--%><cc1:ComboBox ID="ddlMachineCode" runat="server" AutoPostBack="True"
                        CssClass="SmallFont" DataTextField="MACHINE_GROUP" DataValueField="MACHINE_GROUP"
                        EmptyText="Find Machine Group" EnableLoadOnDemand="true" Height="200px" MenuWidth="700"
                        OnLoadingItems="ddlMachineCode_LoadingItems" OnSelectedIndexChanged="ddlMachineCode_SelectedIndexChanged"
                        TabIndex="1" Width="150px">
                        <HeaderTemplate>
                            <div class="header c1">
                                MACHINE GROUP</div>
                            <div class="header c2">
                                MACHINE SEGMENT</div>
                            <div class="header c3">
                                MACHINE TYPE</div>
                            <div class="header c3">
                                MACHINE SEC</div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="item c1">
                                <asp:Literal ID="Container1" runat="server" Text='<%# Eval("MACHINE_GROUP") %>' />
                            </div>
                            <div class="item c2">
                                <asp:Literal ID="Container2" runat="server" Text='<%# Eval("MACHINE_SEGMENT") %>' />
                            </div>
                            <div class="item c3">
                                <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("MACHINE_TYPE") %>' />
                            </div>
                            <div class="item c3">
                                <asp:Literal ID="Literal2" runat="server" Text='<%# Eval("MACHINE_SEC") %>' />
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                            Displaying items
                            <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                            out of
                            <%# Container.ItemsCount %>.
                        </FooterTemplate>
                    </cc1:ComboBox>
                    &nbsp;
                </td>
                <td align="left" valign="top" colspan="5" width="8%">
                    <asp:TextBox ID="txtMAchineName" runat="server" CssClass="SmallFont TextBoxDisplay"
                        ReadOnly="True" TabIndex="5" Width="98%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" valign="top" class="style1">
                    Process:
                </td>
                <td align="left" valign="top" width="10%">
                    <asp:DropDownList ID="ddlProcess" runat="server" CssClass="SmallFont" Width="99px">
                    </asp:DropDownList>
                </td>
                <td align="right" valign="top" width="15%">
                    Process Type:
                </td>
                <td align="left" valign="top" width="10%">
                    <asp:DropDownList ID="ddlFabType" CssClass="SmallFont" runat="server" Width="102px">
                    </asp:DropDownList>
                </td>
                <td align="right" valign="top" width="12%">
                    Department:
                </td>
                <td align="left" valign="top" width="28%">
                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="SmallFont TextBox"
                        DataValueField="DEPT_CODE" Width="150px" DataTextField="DEPT_NAME">
                    </asp:DropDownList>
                </td>
                <td align="left" valign="top">
                    &nbsp;
                </td>
            </tr>
        </table>
    </td>
    <tr>
        <td class="td SmallFont" width="100%" colspan="2">
            <table width="100%" class="tContentArial">
                <tr width="100%">
                    <td align="right" valign="top" width="9%">
                        Dyes Receipe Flag:
                    </td>
                    <td align="left" valign="top" width="11%">
                        <asp:DropDownList ID="ddlDyesRecieptFlag" CssClass="SmallFont" runat="server" Width="102px">
                            <asp:ListItem>YES</asp:ListItem>
                            <asp:ListItem>NO</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="left" valign="top" class=" tdRight" width="10%">
                        &nbsp;&nbsp;&nbsp; Quality Control Flag:
                    </td>
                    <td align="left" valign="top" width="15%">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlQualityControlFlag" CssClass="SmallFont" runat="server"
                            Width="102px">
                            <asp:ListItem>YES</asp:ListItem>
                            <asp:ListItem>NO</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right" valign="top" width="10%">
                        Status:
                    <td align="left" valign="top" width="10%">
                        <asp:DropDownList ID="ddlStatus" CssClass="SmallFont" runat="server" Width="102px">
                            <asp:ListItem>ENABLE</asp:ListItem>
                             <asp:ListItem>DISABLE</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    
                    <td align="left" valign="top" width="10%">
                        Machine Truff Quantity:
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="txtMachineTruffQuantity" runat="server" TabIndex="10" Width="50px"
                            CssClass="SmallFont TextBoxNo"></asp:TextBox>
                        <asp:RangeValidator ID="RangeValidator21" runat="server" ErrorMessage="Please Enter Machine Truff Quantity in Numeric & Precision Should be 9 and Scale 2"
                            Display="None" Type="Double" ControlToValidate="txtMachineTruffQuantity" MinimumValue="0"
                            MaximumValue="999999999.99" ValidationGroup="PM"></asp:RangeValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
                            ErrorMessage="Please Enter Machine Truff Quantity" ValidationGroup="PM" ControlToValidate="txtMachineTruffQuantity"></asp:RequiredFieldValidator>
                    </td>
                    
                    <td align="left" valign="top" class="tdRight" width="10%">
                        Yarn/Grey Pickup/Expr:
                    </td>
                    <td align="left" valign="top" width="65%">
                        <asp:TextBox ID="txtExpr" runat="server" CssClass="SmallFont TextBoxNo" Width="120"></asp:TextBox>
                        <asp:RangeValidator ID="RangeValidator22" runat="server" ErrorMessage="Please Enter Fabric/Grey Pickup/Expr in Numeric & Precision Should be 9 and Scale 2"
                            Display="None" Type="Double" ControlToValidate="txtExpr" MinimumValue="0" MaximumValue="999999999.99"
                            ValidationGroup="PM"></asp:RangeValidator>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None"
                            ErrorMessage="Dear! Please Enter Fabric/Grey Pickup/Expr" ValidationGroup="PM"
                            ControlToValidate="txtExpr"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="td SmallFont" width="100%" colspan="2">
            <table width="100%" class="tContentArial">
                <tr width="100%">
                    <td align="right" valign="top" width="10%">
                        Speed:
                    </td>
                    <td align="left" valign="top" width="11%">
                        <asp:TextBox ID="txtSpeed" runat="server" TabIndex="10" Width="50px" CssClass="SmallFont TextBoxNo"></asp:TextBox>
                        (in MPM)<asp:RangeValidator ID="RangeValidator11" runat="server" ErrorMessage="Please Enter Speed in Numeric"
                            Display="None" Type="Integer" ControlToValidate="txtSpeed" MinimumValue="0" MaximumValue="99999"
                            ValidationGroup="PM"></asp:RangeValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="None"
                            ErrorMessage="Please Enter Speed" ValidationGroup="PM" ControlToValidate="txtSpeed"></asp:RequiredFieldValidator>
                    </td>
                    <td align="right" valign="top" class="tdRight" width="10%">
                        Temp Req:
                    </td>
                    <td align="left" valign="top" width="10%">
                        <asp:TextBox ID="txtTempReq" runat="server" TabIndex="10" Width="50px" CssClass="SmallFont TextBoxNo"></asp:TextBox>
                        (in Cel.)<asp:RangeValidator ID="RangeValidator12" runat="server" ErrorMessage="Please Enter Temp in Numeric"
                            Display="None" Type="Integer" ControlToValidate="txtTempReq" MinimumValue="0"
                            MaximumValue="99999" ValidationGroup="PM"></asp:RangeValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="None"
                            ErrorMessage="Please Enter Temp Req" ValidationGroup="PM" ControlToValidate="txtTempReq"></asp:RequiredFieldValidator>
                    </td>
                    <td align="right" valign="top" width="10%">
                        Set Time:
                    <td align="left" valign="top" width="10%">
                        <asp:TextBox ID="txtSetTime" runat="server" TabIndex="10" Width="50px" CssClass="SmallFont TextBoxNo"></asp:TextBox>
                        (in Min)<asp:RangeValidator ID="RangeValidator13" runat="server" ErrorMessage="Please Enter Set Time in Numeric"
                            Display="None" Type="Integer" ControlToValidate="txtSetTime" MinimumValue="0"
                            MaximumValue="999999" ValidationGroup="PM"></asp:RangeValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="None"
                            ErrorMessage="Please Enter Set Time" ValidationGroup="PM" ControlToValidate="txtSetTime"></asp:RequiredFieldValidator>
                    </td>
                    <td align="right" valign="top" width="10%">
                        Max Shrinkage:
                    </td>
                    <td width="12%" align="left" valign="top">
                        <asp:TextBox ID="txtMaxShrikage" runat="server" TabIndex="10" Width="50px" CssClass="SmallFont TextBoxNo"></asp:TextBox>
                        (in %)<asp:RangeValidator ID="RangeValidator14" runat="server" ErrorMessage="Please Enter Max Shrinkage in Numeric & Precision Should be 2 and Scale 3"
                            Display="None" Type="Double" ControlToValidate="txtMaxShrikage" MinimumValue="0"
                            MaximumValue="99.999" ValidationGroup="PM"></asp:RangeValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="None"
                            ErrorMessage="Please Enter Max Shrinkage" ValidationGroup="PM" ControlToValidate="txtMaxShrikage"></asp:RequiredFieldValidator>
                    </td>
                    <td width="8%" align="right">
                        Max Elongation:
                    </td>
                    <td width="14%" align="left" valign="top">
                        <asp:TextBox ID="txtMaxLongation" runat="server" TabIndex="10" Width="50px" CssClass="SmallFont TextBoxNo"></asp:TextBox>
                        (in %)<asp:RangeValidator ID="RangeValidator15" runat="server" ErrorMessage="Please Enter Max Longation in Numeric & Precision Should be 2 and Scale 3"
                            Display="None" Type="Double" ControlToValidate="txtMaxLongation" MinimumValue="0"
                            MaximumValue="99.999" ValidationGroup="PM"></asp:RangeValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="None"
                            ErrorMessage="Please Enter Max Longation" ValidationGroup="PM" ControlToValidate="txtMaxLongation"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr width="100%">
        <td class="td SmallFont" width="100%" colspan="2">
            <table width="100%" class="tContentArial">
                <tr width="100%">
                    <td align="right" valign="top" width="10%">
                        Specific Instruction :&nbsp;
                    </td>
                    <td align="Left" valign="top" width="90%">
                        <asp:TextBox ID="txtSpecificInstrction" runat="server" CssClass="SmallFont" TabIndex="12"
                            Width="98%"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="left" class="td SmallFont" valign="top" width="100%" colspan="2">
            <table width="100%" class="tContentArial">
                <tr width="100%">
                    <td width="30%" colspan="4" class="style2" style="font-weight: bold;">
                        Machine Process Standard Prarameter
                    </td>
                    <td width="10%" class="style2">
                        &nbsp;
                    </td>
                    <td colspan="7" class="style2" width="50%" style="font-weight: bold;">
                        Chemical Reciepe of Yarn Dyeing
                    </td>
                    <td class="style2" width="10%">
                        &nbsp;
                    </td>
                </tr>
                <tr bgcolor="#336699">
                    <td width="5%">
                        <span class="titleheading">Parameter</span>
                    </td>
                    <td width="5%">
                        <span class="titleheading">Quantity</span>
                    </td>
                    <td width="5%">
                        <span class="titleheading">Unit</span>
                    </td>
                    <td>
                        <span class="titleheading">Basis</span>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td >
                        <span class="titleheading">Process</span>
                    </td>
                    <td width="8%">
                        <span class="titleheading">Sub Process</span>
                    </td>
                    <td width="6%">
                        <span class="titleheading">LCode</span>
                    </td>
                    <td width="5%">
                        <span class="titleheading">Truff. </span>
                    </td>
                    <td width="5%">
                        <span class="titleheading">Expr.</span>
                    </td>
                    <td width="5%">
                        <span class="titleheading">Density. </span>
                    </td>
                    <td width="8%">
                        <span class="titleheading">Quantity </span>
                    </td>
                     <td width="8%">
                        <span class="titleheading">Temp </span>
                    </td>
                     <td width="8%">
                        <span class="titleheading">Hold </span>
                    </td>
                    <td>
                        <span class="titleheading">Unit</span>
                    </td>
                  
                     <td>
                        <span class="titleheading">Remarks</span>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <asp:DropDownList ID="ddlParameter" CssClass="SmallFont" runat="server" Width="70px">
                        </asp:DropDownList>
                    </td>
                    <td valign="top">
                        <asp:TextBox ID="txtStandardQuantity" runat="server" CssClass="TextBox smallfont "
                            TabIndex="18" Width="30px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="None"
                            ErrorMessage="Please Enter Standard Quantity" ValidationGroup="standard" ControlToValidate="txtStandardQuantity"></asp:RequiredFieldValidator>
                        <br />
                        <asp:RangeValidator ID="RangeValidator16" runat="server" ErrorMessage="Please Enter Standard Quantity in Numeric & Precision Should be 9 and Scale 3"
                            Display="None" Type="Double" ControlToValidate="txtStandardQuantity" MinimumValue="0"
                            MaximumValue="999999999.999" ValidationGroup="standard"></asp:RangeValidator>
                    </td>
                    <td valign="top">
                        <asp:DropDownList ID="ddlStandardunit" CssClass="SmallFont" runat="server" Width="40px">
                        </asp:DropDownList>
                    </td>
                    <td valign="top">
                        <asp:DropDownList ID="ddlStandardBasis" CssClass="SmallFont" runat="server" Width="50px">
                        </asp:DropDownList>
                    </td>
                    <td valign="top">
                        <asp:Button ID="btnStandardSave" runat="server" Text="Add" OnClick="btnStandardSave_Click"
                            ValidationGroup="standard" />
                        <asp:Button ID="btnStandardCancel" runat="server" Text="Cancel" OnClick="btnStandardCancel_Click" />
                    </td>
                    <td valign="top" >
                        <asp:DropDownList ID="ddlChemicalBasis" CssClass="SmallFont" runat="server" Width="100px">
                        </asp:DropDownList>
                    </td>
                    <td valign="top">
                      <asp:DropDownList ID="ddlDyeProcessCode" CssClass="SmallFont" runat="server" Width="110px">  
                        </asp:DropDownList>
                    </td>
                    <td valign="top">
                 <cc1:ComboBox ID="txtItemCode" runat="server" AutoPostBack="True" CssClass="SmallFont"
                                        DataTextField="ITEM_CODE" DataValueField="ITEM_CODE" EmptyText="Find Item" EnableLoadOnDemand="true"
                                        EnableVirtualScrolling="true" Height="200px" MenuWidth="300px" OnLoadingItems="txtItemCode_LoadingItems"
                                        OnSelectedIndexChanged="txtItemCode_SelectedIndexChanged" TabIndex="7" Width="130px">
                        <HeaderTemplate>
                            <div class="header c1">
                                Code</div>
                            <div class="header c2">
                                DESCRIPTION</div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="item c1">
                                <asp:Literal ID="Container4" runat="server" Text='<%# Eval("ITEM_CODE") %>' />
                            </div>
                            <div class="item c2">
                                <asp:Literal ID="Container5" runat="server" Text='<%# Eval("ITEM_DESC") %>' />
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                                            Displaying items
                                            <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                            out of
                                            <%# Container.ItemsCount %>.
                                        </FooterTemplate>
                    </cc1:ComboBox>
                    </td>
                    <td valign="top">
                        <asp:TextBox ID="txtChemicalTruff" runat="server" CssClass="TextBoxNo smallfont" Text="0" 
                            TabIndex="22" Width="35px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Display="None" 
                            ErrorMessage="Please Enter Reciepe Truff" ValidationGroup="Chemical" ControlToValidate="txtChemicalTruff"></asp:RequiredFieldValidator>
                        <br />
                        <asp:RangeValidator ID="RangeValidator17" runat="server" ErrorMessage="Please Enter Chemical Truff in Numeric & Precision Should be 9 and Scale 2"
                            Display="None" Type="Double" ControlToValidate="txtChemicalTruff" MinimumValue="0"
                            MaximumValue="999999999.99" ValidationGroup="Chemical"></asp:RangeValidator>
                    </td>
                    <td valign="top">
                        <asp:TextBox ID="txtChemicalExpr" runat="server" CssClass="TextBoxNo smallfont" Text="0"
                            TabIndex="24" Width="30px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" Display="None" 
                            ErrorMessage="Please Enter Reciepe Expr" ValidationGroup="Chemical" ControlToValidate="txtChemicalExpr"></asp:RequiredFieldValidator>
                        <br />
                        <asp:RangeValidator ID="RangeValidator18" runat="server" ErrorMessage="Please Enter Reciepe Expr in Numeric & Precision Should be 9 and Scale 2"
                            Display="None" Type="Double" ControlToValidate="txtChemicalExpr" MinimumValue="0"
                            MaximumValue="999999999.99" ValidationGroup="Chemical"></asp:RangeValidator>
                    </td>
                    <td valign="top">
                        <asp:TextBox ID="txtChemicalDansity" runat="server" CssClass="TextBoxNo smallfont "
                            TabIndex="24" Width="30px"></asp:TextBox>
                        <br />
                        <asp:RangeValidator ID="RangeValidator19" runat="server" ErrorMessage="Please Enter Chemical Dansity in Numeric & Precision Should be 5 and Scale 2"
                            Display="None" Type="Double" ControlToValidate="txtChemicalDansity" MinimumValue="0"
                            MaximumValue="99999.99" ValidationGroup="Chemical"></asp:RangeValidator>
                    </td>
                    <td valign="top">
                        <asp:TextBox ID="txtChemicalQuantity" runat="server" CssClass="TextBoxNo smallfont "
                            TabIndex="24" Width="48px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" Display="None"
                            ErrorMessage="Please Enter Reciepe Quantity" ValidationGroup="Chemical" ControlToValidate="txtChemicalQuantity"></asp:RequiredFieldValidator>
                        <br />
                        <asp:RangeValidator ID="RangeValidator20" runat="server" ErrorMessage="Please Enter Chemical Quantity in Numeric & Precision Should be 10 and Scale 4"
                            Display="None" Type="Double" ControlToValidate="txtChemicalQuantity" MinimumValue="0"
                            MaximumValue="9999999999.9999" ValidationGroup="Chemical"></asp:RangeValidator>
                    </td>
                    
                     <td valign="top">
                    <asp:TextBox ID="txtTemp" runat="server" CssClass="SmallFont" TabIndex="12"
                            Width="50px"></asp:TextBox>
                    </td>
                    
                     <td valign="top">
                    <asp:TextBox ID="txtHoldTime" runat="server" CssClass="SmallFont" TabIndex="12"
                            Width="50px"></asp:TextBox>
                    </td>
                    <td valign="top">
                        <asp:DropDownList ID="ddlChemicalunit" CssClass="SmallFont" runat="server" Width="50px">
                        </asp:DropDownList>
                    </td>
                    
                    <td valign="top">
                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="SmallFont" TabIndex="12"
                            Width="110px"></asp:TextBox>
                    </td>
                    <td valign="top">
                        <asp:Button ID="BtnChemicalSave" runat="server" Text="Add" OnClick="BtnChemicalSave_Click1"
                            ValidationGroup="Chemical" />
                        <asp:Button ID="BtnChemicalCancel" runat="server" Text="Cancel" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Item Code / Desc
                    </td>
                     <td colspan="6" >
                        <asp:TextBox ID="txtICode" runat="server" Width="80px" CssClass="TextBox TextBoxDisplay SmallFont"
                                        Font-Bold="False" ReadOnly="True"></asp:TextBox> &nbsp;
                     <asp:TextBox ID="txtItemDesc" runat="server" CssClass="TextBox SmallFont TextBoxDisplay"
                                        ReadOnly="true" Width="250px"></asp:TextBox>
                    </td>
                     <%--<td colspan="2">
                        Item Desc:
                    </td>
                    <td colspan="4">
                        <asp:Label ID="lblItemDesc" runat="server" Text=""></asp:Label>
                    </td>--%>
                </tr>
            </table>
            &nbsp;
        </td>
    </tr>
    <tr width="100%">
        <td class="td SmallFont" width="35%">
            <asp:GridView ID="gvStandard" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CssClass="smallfont" OnRowCommand="gvStandard_RowCommand" TabIndex="30" Width="95%">
                <RowStyle CssClass="SmallFont" Width="100%" />
                <Columns>
                    <asp:TemplateField HeaderText="Parameter">
                        <ItemTemplate>
                            <asp:Label ID="txtParameter" runat="server" CssClass="Label SmallFont" Text='<%# Bind("Parameter") %>'
                                Width="80px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                            <asp:Label ID="txtQuantity" runat="server" CssClass="Label smallfont" ReadOnly="true"
                                TabIndex="19" Text='<%# Bind("Quantity") %>' Width="120px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unit">
                        <ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="txtUnit" runat="server" CssClass="LabelNo smallfont" ReadOnly="true"
                                TabIndex="21" Text='<%# Bind("Unit") %>' Width="60px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Basis">
                        <ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="txtBasis" runat="server" CssClass="LabelNo smallfont" ReadOnly="true"
                                TabIndex="21" Text='<%# Bind("Basis") %>' Width="60px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("UniqueId") %>'
                                CommandName="StandardEdit" TabIndex="29" Text="Edit"></asp:LinkButton>
                            /
                            <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("UniqueId") %>'
                                CommandName="StandardDelete" TabIndex="29" Text="Del"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle HorizontalAlign="Left" />
                <HeaderStyle BackColor="#336699" CssClass="SmallFont titleheading" Font-Bold="True"
                    ForeColor="White" HorizontalAlign="Center" />
            </asp:GridView>
        </td>
        <td class="td SmallFont" width="75%">
            <asp:GridView ID="gvChemicalReciepe" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CssClass="smallfont" OnRowCommand="gvChemicalReciepe_RowCommand" TabIndex="30"
                Width="95%">
                <RowStyle CssClass="SmallFont" Width="100%" />
                <Columns>
                 <asp:TemplateField HeaderText="Process">
                        <ItemTemplate>
                            <asp:Label ID="txtBasis" runat="server" CssClass="Label smallfont" ReadOnly="true"
                                TabIndex="22" Text='<%# Bind("PARA_BASIS") %>' Width="100px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                <asp:TemplateField HeaderText="Sub Process">
                <ItemTemplate>
                <asp:Label ID="lblDProcessCode" runat="server" Text='<%# Bind("DyeProcess") %>' CssClass="Label SmallFont" Width="80px"></asp:Label>  
                </ItemTemplate>
                </asp:TemplateField>
                    <asp:TemplateField HeaderText="LCode" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="txtLCode" runat="server" Text='<%# Bind("LCode") %>' Width="50px"
                                CssClass="Label SmallFont"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Item Desc.">
                        <ItemTemplate>
                            <asp:Label ID="txtItemDesc" runat="server" Text='<%# Bind("ItemDesc") %>' Width="160px"
                                CssClass="Label SmallFont"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Truff">
                        <ItemTemplate>
                            <asp:Label ID="txtTruff" runat="server" CssClass="LabelNo smallfont" ReadOnly="true"
                                TabIndex="19" Text='<%# Bind("Truff") %>' Width="40px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Expr">
                        <ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="txtExpr" runat="server" CssClass="LabelNo smallfont" ReadOnly="true"
                                TabIndex="21" Text='<%# Bind("Expr") %>' Width="40px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Dansity">
                        <ItemTemplate>
                            <asp:Label ID="txtDansity" runat="server" CssClass="Label smallfont" ReadOnly="true"
                                TabIndex="22" Text='<%# Bind("Dansity") %>' Width="40px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                            <asp:Label ID="txtQuantity" runat="server" CssClass="Label smallfont" ReadOnly="true"
                                TabIndex="22" Text='<%# Bind("Quantity") %>' Width="50px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Temp">
                        <ItemTemplate>
                            <asp:Label ID="txtTemp" runat="server" CssClass="Label smallfont" ReadOnly="true"
                                TabIndex="22" Text='<%# Bind("TEMP") %>' Width="50px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Hold">
                        <ItemTemplate>
                            <asp:Label ID="txtHoldTime" runat="server" CssClass="Label smallfont" ReadOnly="true"
                                TabIndex="22" Text='<%# Bind("HOLD_TIME") %>' Width="50px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unit">
                        <ItemTemplate>
                            <asp:Label ID="txtUnit" runat="server" CssClass="Label smallfont" ReadOnly="true"
                                TabIndex="22" Text='<%# Bind("Unit") %>' Width="40px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                    <asp:TemplateField HeaderText="Remarks">
                    <ItemTemplate>
                    <asp:Label  ID="lblRemarks" runat="server" CssClass="Label smallfont" TabIndex="25" Text='<%# Bind("DYE_REMARKS") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit0" runat="server" CommandArgument='<%# Eval("UniqueId") %>'
                                CommandName="ChemicalEdit" TabIndex="29" Text="Edit"></asp:LinkButton>
                            /
                            <asp:LinkButton ID="lnkDelete0" runat="server" CommandArgument='<%# Eval("UniqueId") %>'
                                CommandName="ChemicalDelete" TabIndex="29" Text="Del"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle HorizontalAlign="Left" />
                <HeaderStyle BackColor="#336699" CssClass="SmallFont titleheading" Font-Bold="True"
                    HorizontalAlign="Center" ForeColor="White" />
            </asp:GridView>
        </td>
    </tr>
</table>
<asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="standard"
    ShowMessageBox="True" ShowSummary="False" HeaderText="Mandatory Fields !! Machine Process Standard Prarameter" />
<asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="PM"
    ShowMessageBox="True" ShowSummary="False" HeaderText="Mandatory Fields!! Machine Process Master" />
<asp:ValidationSummary ID="ValidationSummary5" runat="server" ValidationGroup="Chemical"
    ShowMessageBox="True" ShowSummary="False" HeaderText="Mandatory Fields !! Chemical Reciepe of Fabric" />
 </ContentTemplate>
   </asp:UpdatePanel>