<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FabricDesignNew.ascx.cs"
    Inherits="Module_Fabric_FabricSaleWork_Controls_FabricDesignNew" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript" language="javascript">

    function Calculation(val) {
        document.getElementById('ctl00_cphBody_FabricDesignMaster1_lblFinalCost').value = document.getElementById('ctl00_cphBody_FabricDesignMaster1_txtFinalCosting').value.toFixed(3);
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
        width: 100px;
    }
    .c2
    {
        margin-left: 4px;
        width: 180px;
    }
    .style2
    {
        height: 22px;
    }
    .style3
    {
        width: 100%;
    }
    .hide
    {
        visibility: hidden;
    }
    .pnlTrn
    {
        background-color: #afcae4;
    }
    </style>
<table class="tdMain" width="100%">
    <tr>
        <td width="100%" class="td">
            <table class="tContentArial">
                <tr>
                    <td id="tdSave" runat="server">
                        <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="Save" ImageUrl="~/CommonImages/save.jpg"
                            ValidationGroup="M" OnClick="imgbtnSave_Click" TabIndex="50"></asp:ImageButton>
                    </td>
                    <td id="tdUpdate" runat="server">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                            ValidationGroup="M1" OnClick="imgbtnUpdate_Click" TabIndex="50"></asp:ImageButton>
                    </td>
                    <td id="tdFind" runat="server">
                        <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/link_find.png"
                            OnClick="imgbtnFind_Click" TabIndex="51"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                            OnClick="imgbtnClear_Click" TabIndex="52"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                            OnClick="imgbtnPrint_Click" TabIndex="53"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                            OnClick="imgbtnExit_Click" TabIndex="54"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                            OnClick="imgbtnHelp_Click" TabIndex="55"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader td" width="100%">
            <b class="titleheading">Fabric Design Master</b>
        </td>
    </tr>
     <tr>
  <td >
      <asp:ValidationSummary ID="ValidationSummary1" runat="server" Height="54px" 
          ShowMessageBox="True" ShowSummary="False" ValidationGroup="M" Width="205px" />
         </td>
    </tr>
   
    <tr>
        <td valign="top" align="left" class="td" width="100%">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>
   
    <tr>
        <td width="100%" class="td">
            <table width="100%">
                <tr>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="Label15" runat="server" Text="Design Code : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:TextBox ID="txtDesignCode" runat="server" ValidationGroup="M" Width="100px"
                            CssClass="TextBox SmallFont" TabIndex="1"></asp:TextBox>
                        <cc2:ComboBox ID="ddlFabricDesignMST" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            DataTextField="DESIGN_CODE" DataValueField="FABR_CODE" Width="100px" Height="200px"
                            MenuWidth="500px" OnLoadingItems="ddlFabricDesignMST_LoadingItems" OnSelectedIndexChanged="ddlFabricDesignMST_SelectedIndexChanged">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Design Code</div>
                                <div class="header c1">
                                    Fabric Code</div>
                                <div class="header c1">
                                    Fabric Type</div>
                                <div class="header c2">
                                    Fabric Description</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Literal9" Text='<%# Eval("DESIGN_CODE") %>' /></div>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container9" Text='<%# Eval("FABR_CODE") %>' /></div>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container10" Text='<%# Eval("FABR_TYPE") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Literal8" Text='<%# Eval("FABR_DESC") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtDesignCode" Display="None" 
                            ErrorMessage="Please Enter Design Code " ValidationGroup="M"></asp:RequiredFieldValidator>
                    </td>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="Label19" runat="server" Text="Fabric Code : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:TextBox ID="txtFabricCode" runat="server" ValidationGroup="M" Width="100px"
                            TabIndex="2" CssClass="TextBox SmallFont"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RvFabricCode" runat="server" 
                            ControlToValidate="txtFabricCode" Display="None" 
                            ErrorMessage="Please Insert Fabric Code ." ValidationGroup="M"></asp:RequiredFieldValidator>
                    </td>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="Label47" runat="server" Text="Type : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:DropDownList ID="DDLType" runat="server" Width="100px" TabIndex="3" 
                            CssClass="SmallFont" CausesValidation="True" 
                            ValidationGroup="M">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="DDLType" Display="None" ErrorMessage="Please Select Fabric Type" 
                            InitialValue="0" ValidationGroup="M"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="Label1" runat="server" Text="Fabric Description :" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="17%" colspan="5">
                        <asp:TextBox ID="txtFabricDescription" runat="server" ValidationGroup="M1" Width="99%"
                            TabIndex="4" CssClass="TextBox SmallFont"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label30" runat="server" Text="Fabric Quality : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtFabricQuality" runat="server" ValidationGroup="M1" Width="100px"
                            TabIndex="5" CssClass="TextBox SmallFont"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label48" runat="server" Text="Group : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:DropDownList ID="DDLGroup" runat="server" Width="100px" TabIndex="6" CssClass="SmallFont">
                        </asp:DropDownList>
                    </td>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label49" runat="server" Text="Fabric Width : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="TxtGreyWidth" runat="server" TabIndex="7" Width="70px" ValidationGroup="M1"
                            MaxLength="4" CssClass="TextBoxNo SmallFont"></asp:TextBox>
                        <i style="font-size: small">Inch</i>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label40" runat="server" Text="Fabric Base : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtFabricBase" runat="server" ValidationGroup="M1" Width="100px"
                            TabIndex="8" CssClass="TextBox SmallFont"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label36" runat="server" Text="EPI : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtEPI" runat="server" ValidationGroup="M1" Width="100px" TabIndex="9"
                            CssClass="TextBox SmallFont"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label37" runat="server" Text="PPI : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="txtPPI" runat="server" ValidationGroup="M1" Width="100px" TabIndex="10"
                            CssClass="TextBox SmallFont"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label39" runat="server" Text="UOM : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:DropDownList ID="DDLUOM" runat="server" CssClass="SmallFont" Width="100px" TabIndex="11">
                        </asp:DropDownList>
                    </td>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label32" runat="server" Text="Finish Process : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:DropDownList ID="ddlFinishProcess" runat="server" Width="100px" TabIndex="12"
                            CssClass="SmallFont">
                            <asp:ListItem>PROCESS 1</asp:ListItem>
                            <asp:ListItem>PROCESS 2</asp:ListItem>
                            <asp:ListItem>PROCESS 3</asp:ListItem>
                            <asp:ListItem>PROCESS 4</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label45" runat="server" Text="RailRoad : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:DropDownList ID="ddlRailRoad" runat="server" Width="100px" TabIndex="13" CssClass="SmallFont">
                            <asp:ListItem>Yes</asp:ListItem>
                            <asp:ListItem>No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label18" runat="server" Text="Collection Name : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtCollection" runat="server" ValidationGroup="M1" Width="100px"
                            TabIndex="14" CssClass="TextBox SmallFont"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label24" runat="server" Text="Compositions : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtComposition" runat="server" ValidationGroup="M1" Width="100px"
                            TabIndex="15" CssClass="TextBox SmallFont"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label35" runat="server" Text="No Of Shades : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="txtNoOfShade" runat="server" ValidationGroup="M1" Width="100px"
                            TabIndex="16" CssClass="TextBox SmallFont" AutoPostBack="True" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label38" runat="server" Text="Pickup(%) :" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtPickUp" runat="server" ValidationGroup="M1" Width="100px" TabIndex="17"
                            CssClass="TextBoxNo SmallFont"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label2" runat="server" Text="Shrinkage(%) :" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="TxtShrink" runat="server" TabIndex="18" ValidationGroup="M1" Width="100px"
                            MaxLength="6" CssClass="TextBoxNo"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label3" runat="server" Text="Contraction(%) :" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="TxtContraction" runat="server" TabIndex="19" ValidationGroup="M1"
                            MaxLength="6" Width="100px" CssClass="TextBoxNo"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label25" runat="server" Text="GSM : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtGSM" runat="server" ValidationGroup="M1" Width="100px" TabIndex="20"
                            CssClass="TextBox SmallFont"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label46" runat="server" Text="GLM : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="TxtGLM" runat="server" TabIndex="21" ValidationGroup="M1" Width="100px"
                            MaxLength="9" CssClass="TextBoxNo"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label29" runat="server" Text="Picks : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="txtPicks" runat="server" ValidationGroup="M1" Width="100px" TabIndex="22"
                            CssClass="TextBoxNo SmallFont"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label43" runat="server" Text="Modification : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtModification" runat="server" ValidationGroup="M1" Width="100px"
                            TabIndex="23" CssClass="TextBox SmallFont"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label26" runat="server" Text="Design Repeat (Hor) : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtDesignRptHor" runat="server" ValidationGroup="M1" Width="100px"
                            TabIndex="24" CssClass="TextBox SmallFont"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label27" runat="server" Text="Design Repeat (Ver) : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="txtDesignRptVer" runat="server" ValidationGroup="M1" Width="100px"
                            TabIndex="25" CssClass="TextBox SmallFont"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label28" runat="server" Text="Ends : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtEnds" runat="server" ValidationGroup="M1" Width="100px" TabIndex="26"
                            CssClass="TextBoxNo SmallFont"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label33" runat="server" Text="End Use : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:DropDownList ID="ddlEndUse" runat="server" Width="100px" TabIndex="27" CssClass="SmallFont">
                        </asp:DropDownList>
                    </td>
                    <td align="right" valign="top" width="17%">
                        &nbsp;
                        <asp:Label ID="Label42" runat="server" Text="Sale Price : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="txtSalePrice" runat="server" ValidationGroup="M1" Width="100px"
                            TabIndex="28" CssClass="TextBoxNo SmallFont"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label44" runat="server" Text="Transfer Price : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtTransferPrice" runat="server" ValidationGroup="M1" Width="100px"
                            TabIndex="29" CssClass="TextBoxNo SmallFont"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label14" runat="server" Text="Remarks :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" colspan="3" style="width: 49%">
                        <asp:TextBox ID="txtRemarks" runat="server" ValidationGroup="M1" Width="99%" TabIndex="30"
                            CssClass="TextBox SmallFont"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" align="left" valign="top" width="100%" class="td">
                        <fieldset>
                            <table width="100%" style="vertical-align: top;">
                                <tr>
                                    <td align="left" valign="top" width="46%" class="style2">
                                        <fieldset>
                                            <legend style="font-size: small"><i>Main Shade Details</i></legend>
                                            <table width="100%">
                                                <tr>
                                                    <td class="tdRight SmallFont">
                                                        Shade Name :
                                                    </td>
                                                    <td class="tdLeft SmallFont">
                                                        <asp:DropDownList ID="ddlShadeCode" runat="server" class="SmallFont" AppendDataBoundItems="True"
                                                            Width="90px" OnSelectedIndexChanged="ddlShadeCode_SelectedIndexChanged" 
                                                            TabIndex="31" ValidationGroup="M" CausesValidation="True">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                            Display="None" ErrorMessage="Please First Save Shade Details ." 
                            InitialValue="0"  ControlToValidate="ddlShadeCode"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td rowspan="4">
                                                        <asp:Image ID="Image1" runat="server" Width="124px" Height="85px" />
                                                        <br />
                                                        <asp:LinkButton ID="lbtnDesignImage" runat="server" OnClick="lbtnDesignImage_Click"
                                                            TabIndex="34">Get Design</asp:LinkButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdRight SmallFont">
                                                        Shade RGB :
                                                    </td>
                                                    <td class="tdLeft SmallFont">
                                                        <asp:TextBox ID="txtShadeRGB" CssClass="SmallFont" runat="server" Width="60px" AutoPostBack="True"
                                                            OnTextChanged="txtShadeRGB_TextChanged" TabIndex="32"></asp:TextBox>
                                                        <asp:TextBox ID="txtRGBColor" CssClass="SmallFont" runat="server" Width="20px" ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdRight SmallFont">
                                                        Doc No :
                                                    </td>
                                                    <td class="tdLeft SmallFont">
                                                        <asp:TextBox ID="txtdocNo" CssClass="SmallFont" runat="server" Width="88px" TabIndex="33"></asp:TextBox>
                                                        
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="2">
                                                        <asp:Button ID="btnsave" Text="Save" runat="server" CssClass="SmallFont" OnClick="btnsave_Click" />
                                                        <asp:Button ID="btncancel" Text="Cancel" runat="server" CssClass="SmallFont" OnClick="btncancel_Click" />
                                                        <asp:Button ID="lbtnSetImage" runat="server" CssClass="hide" OnClick="lbtnSetImage_Click"
                                                            Width="0px"></asp:Button>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                    <td align="left" valign="top" width="27%" class="style2">
                                        <fieldset>
                                            <legend style="font-size: small"><i>Shade Warp Details</i></legend>
                                            <table cellpadding="0" cellspacing="0" class="style3">
                                                <tr>
                                                    <td class="tdRight SmallFont">
                                                        Warp Count :
                                                    </td>
                                                    <td class="tdLeft SmallFont">
                                                        <asp:TextBox ID="txtWarpCount" CssClass="SmallFont" runat="server" Width="50px" TabIndex="35"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdRight SmallFont">
                                                        Warp Quality :
                                                    </td>
                                                    <td class="tdLeft SmallFont">
                                                        <asp:TextBox ID="txtWarpQuality" CssClass="SmallFont" runat="server" Width="50px"
                                                            TabIndex="36"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdRight SmallFont">
                                                        No Of Warp :
                                                    </td>
                                                    <td class="tdLeft SmallFont">
                                                        <asp:TextBox ID="txtNoOfWarp" CssClass="SmallFont" runat="server" Width="50px" TabIndex="400"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="center" class="SmallFont">
                                                        <asp:LinkButton ID="lbtnWarpDetail" runat="server" OnClientClick="document.getElementById('btngetpopupdtl').click();"
                                                            OnClick="lbtnWarpDetail_Click" TabIndex="37">Get Warp Detail</asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                    <td align="left" valign="top" width="27%" class="style2">
                                        <fieldset>
                                            <legend style="font-size: small"><i>Shade Weft Details</i></legend>
                                            <table cellpadding="0" cellspacing="0" class="style3">
                                                <tr>
                                                    <td class="tdRight SmallFont">
                                                        Weft Count :
                                                    </td>
                                                    <td class="tdLeft SmallFont">
                                                        <asp:TextBox ID="txtWeftCount" CssClass="SmallFont" runat="server" Width="50px" TabIndex="38"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdRight SmallFont">
                                                        weft Quality :
                                                    </td>
                                                    <td class="tdLeft SmallFont">
                                                        <asp:TextBox ID="txtWeftQuality" CssClass="SmallFont" runat="server" Width="50px"
                                                            TabIndex="39"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdRight SmallFont">
                                                        No Of Weft :
                                                    </td>
                                                    <td class="tdLeft SmallFont">
                                                        <asp:TextBox ID="txtNoOfWeft" CssClass="SmallFont" runat="server" Width="50px" TabIndex="400"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="center" class="SmallFont">
                                                        <asp:LinkButton ID="lbtnWeftDetail" runat="server" OnClick="lbtnWeftDetail_Click"
                                                            TabIndex="40">Get Weft Detail</asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" align="left" valign="top" width="100%">
                        <asp:GridView ID="grdfabricShadeDetail" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            BorderWidth="1px" CssClass="SmallFont" Font-Bold="False" ShowFooter="True" Width="98%"
                            OnRowCommand="grdfabricShadeDetail_RowCommand" OnRowDataBound="grdfabricShadeDetail_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="SHADE_CODE" HeaderText="Shade Code" />
                                <asp:TemplateField HeaderText="Shade RGB">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtShadeRGBColorTrn" runat="server" Width="20px" Text="" ReadOnly="true"></asp:TextBox>
                                        <asp:LinkButton ID="lbtnShadeRGBTrn" ToolTip='<%# Bind("SHADE_CODE") %>' runat="server"
                                            Text='<%# Bind("SHADE_RGB") %>'></asp:LinkButton>
                                      <asp:Panel ID="Imagepnl" runat="server" BorderStyle="Ridge" BorderWidth="4px" BorderColor="Gray">
                                            <asp:Image ID="imgDesignImage" runat="server" Width="124px" Height="85px"  ImageUrl ="content"  />
                                        </asp:Panel>
                                        <cc1:HoverMenuExtender ID="hmeShow" runat="server" TargetControlID="lbtnShadeRGBTrn"
                                            PopupControlID="Imagepnl" PopupPosition="Right" PopDelay="500" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DESIGN_DOC_NO" HeaderText="Doc No" />
                                <asp:BoundField DataField="COUNT_WARP" HeaderText="Warp Count" />
                                <asp:BoundField DataField="QUALITY_WARP" HeaderText="Warp Quality" />
                                <asp:TemplateField HeaderText="No Of Warp">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnWarpDtlTrn" CommandName="WarpDtl" CommandArgument='<%# Bind("SHADE_CODE") %>'
                                            runat="server" Text='<%# Bind("NO_WARP") %>'></asp:LinkButton>
                                        <cc1:HoverMenuExtender ID="hmeWarpTrn" runat="server" TargetControlID="lbtnWarpDtlTrn"
                                            PopupControlID="pnlWarptrn" PopupPosition="Top">
                                        </cc1:HoverMenuExtender>
                                        <asp:Panel ID="pnlWarptrn" runat="server" BackColor="White">
                                            <asp:GridView ID="grdWarptrn" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                BorderWidth="1px" CssClass="SmallFont" Font-Bold="False" ShowFooter="false" Width="98%">
                                                <Columns>
                                                    <asp:BoundField DataField="SEQUENCE_NO" HeaderText="Sequence" />
                                                    <asp:BoundField DataField="YARN_CODE" HeaderText="Yarn Code" />
                                                    <asp:BoundField DataField="YARN_SHADE_CODE" HeaderText="Shade Code" />
                                                    <asp:TemplateField HeaderText="Shade RGB">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtShadeRGBColorTrn" runat="server" Width="20px" Text="" ReadOnly="true"></asp:TextBox>
                                                            <asp:LinkButton ID="lbtnShadeRGBTrn" ToolTip='<%# Bind("YARN_SHADE_RGB") %>' runat="server"
                                                                Text='<%# Bind("YARN_SHADE_RGB") %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="COUNT" HeaderText="Count" />
                                                    <asp:BoundField DataField="YARN_STD" HeaderText="Yanr STD." />
                                                    <asp:BoundField DataField="REQ_QTY" HeaderText="Req Qty" />
                                                    <asp:BoundField DataField="SHRINKAGE" HeaderText="Shrinkage" />
                                                    <asp:BoundField DataField="WASTAGE" HeaderText="Wastage" />
                                                    <asp:BoundField DataField="REJECTION" HeaderText="Rejection" />
                                                    <asp:BoundField DataField="QTY" HeaderText="Qty." />
                                                </Columns>
                                                <RowStyle CssClass="RowStyle SmallFont" />
                                                <SelectedRowStyle CssClass="SelectedRowStyle" />
                                                <AlternatingRowStyle CssClass="AltRowStyle" />
                                                <PagerStyle CssClass="PagerStyle" />
                                                <HeaderStyle BackColor="#336699" CssClass="HeaderStyle SmallFont" ForeColor="White" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="COUNT_WEFT" HeaderText="Weft Count" />
                                <asp:BoundField DataField="QUALITY_WEFT" HeaderText="Weft Quality" />
                                <asp:TemplateField HeaderText="No Of WEFT">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnWeftDtlTrn" CommandName="WarpDtl" CommandArgument='<%# Bind("SHADE_CODE") %>'
                                            runat="server" Text='<%# Bind("NO_WEFT") %>'></asp:LinkButton>
                                        <cc1:HoverMenuExtender ID="hmeWeftTrn" runat="server" TargetControlID="lbtnWeftDtlTrn"
                                            PopupControlID="pnlWefttrn" PopupPosition="Top">
                                        </cc1:HoverMenuExtender>
                                        <asp:Panel ID="pnlWefttrn" runat="server" BackColor="White">
                                            <asp:GridView ID="grdWefttrn" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                BorderWidth="1px" CssClass="SmallFont" Font-Bold="False" ShowFooter="false" Width="98%">
                                                <Columns>
                                                    <asp:BoundField DataField="SEQUENCE_NO" HeaderText="Sequence" />
                                                    <asp:BoundField DataField="YARN_CODE" HeaderText="Yarn Code" />
                                                    <asp:BoundField DataField="YARN_SHADE_CODE" HeaderText="Shade Code" />
                                                    <asp:TemplateField HeaderText="Shade RGB">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtShadeRGBColorTrn" runat="server" Width="20px" Text="" ReadOnly="true"></asp:TextBox>
                                                            <asp:LinkButton ID="lbtnShadeRGBTrn" ToolTip='<%# Bind("YARN_SHADE_RGB") %>' runat="server"
                                                                Text='<%# Bind("YARN_SHADE_RGB") %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="COUNT" HeaderText="Count" />
                                                    <asp:BoundField DataField="YARN_STD" HeaderText="Yanr STD." />
                                                    <asp:BoundField DataField="REQ_QTY" HeaderText="Req Qty" />
                                                    <asp:BoundField DataField="SHRINKAGE" HeaderText="Shrinkage" />
                                                    <asp:BoundField DataField="WASTAGE" HeaderText="Wastage" />
                                                    <asp:BoundField DataField="REJECTION" HeaderText="Rejection" />
                                                    <asp:BoundField DataField="QTY" HeaderText="Qty." />
                                                </Columns>
                                                <RowStyle CssClass="RowStyle SmallFont" />
                                                <SelectedRowStyle CssClass="SelectedRowStyle" />
                                                <AlternatingRowStyle CssClass="AltRowStyle" />
                                                <PagerStyle CssClass="PagerStyle" />
                                                <HeaderStyle BackColor="#336699" CssClass="HeaderStyle SmallFont" ForeColor="White" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:Button ID="lnkEdit" Text="Edit" runat="server" CommandName="trnEdit" CommandArgument='<%# Eval("SHADE_CODE") %>'>
                                        </asp:Button><asp:Button ID="lnkDelete" runat="server" Text="Delete" CommandName="trnDelete"
                                            CommandArgument='<%# Eval("SHADE_CODE") %>'></asp:Button>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle CssClass="RowStyle SmallFont" />
                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                            <AlternatingRowStyle CssClass="AltRowStyle" />
                            <PagerStyle CssClass="PagerStyle" />
                            <HeaderStyle BackColor="#336699" CssClass="HeaderStyle SmallFont" ForeColor="White" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
