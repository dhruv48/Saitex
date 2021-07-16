<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Machine_Master.ascx.cs"
    Inherits="Module_Machine_Controls_Machine_Master" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc1" %>
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
        width: 130px;
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
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
        <table align="left" class="tContentArial">
            <tr>
                <td valign="top" align="left" class="td">
                    <table>
                        <tr>
                            <td id="tdSave" runat="server">
                                <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="Save" ImageUrl="~/CommonImages/save.jpg"
                                    OnClick="imgbtnSave_Click" ValidationGroup="M1" />
                            </td>
                            <td id="tdUpdate" runat="server">
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                    OnClick="imgbtnUpdate_Click" ValidationGroup="M1"></asp:ImageButton>
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
                            <td>
                                <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                                    OnClick="imgbtnHelp_Click"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="TableHeader td">
                    <span class="titleheading"><b>Machine Master</b></span>
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
                <td>
                    <table width="100%" class="td">
                        <tr>
                            <td width="30%" align="right">
                                Machine Group :
                            </td>
                            <td width="20%" align="left">
                                <cc2:ComboBox ID="cmbMachineGroup" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    DataTextField="MACHINE_GROUP" DataValueField="MACHINE_GROUP" Width="150px" MenuWidth="500px"
                                    Height="200px" CssClass="SmallFont" TabIndex="1" EmptyText="Select Machine Group"
                                    OnLoadingItems="cmbMachineGroup_LoadingItems" OnSelectedIndexChanged="cmbMachineGroup_SelectedIndexChanged">
                                    <HeaderTemplate>
                                        <div class="header c2">
                                            Machine Group
                                        </div>
                                        <div class="header c2">
                                            Machine Type
                                        </div>
                                        <div class="header c3">
                                            Machine Section
                                        </div>
                                        <div class="header c4">
                                            Machine Segment
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("MACHINE_GROUP") %>' /></div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("MACHINE_TYPE") %>' /></div>
                                        <div class="item c3">
                                            <asp:Literal runat="server" ID="Container3" Text='<%# Eval("MACHINE_SEC") %>' /></div>
                                        <div class="item c4">
                                            <asp:Literal runat="server" ID="Container4" Text='<%# Eval("MACHINE_SEGMENT") %>' /></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="cmbMachineGroup"
                                    ErrorMessage="Select Machine Group" ValidationGroup="M1" Display="None"></asp:RequiredFieldValidator>
                            </td>
                            <td width="30%" align="right">
                                <asp:Label ID="lblNoOfMachine" runat="server" Text="No Of Machines :"></asp:Label>
                                &nbsp;
                            </td>
                            <td width="20%" align="left">
                                <asp:TextBox ID="txtNos" runat="server" ValidationGroup="M1" OnTextChanged="txtNos_TextChanged"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator13" runat="server" ControlToValidate="txtNos"
                                    Display="None" ErrorMessage="Enter Numeric Value" MaximumValue="100000" MinimumValue="1"
                                    Type="Integer"></asp:RangeValidator>
                                <cc2:ComboBox ID="cmbMachineCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    DataTextField="MACHINE_CODE" DataValueField="MACHINE_CODE" Width="150px" MenuWidth="200px"
                                    Height="200px" CssClass="SmallFont" TabIndex="1" EmptyText="Find Machine Code"
                                    OnLoadingItems="cmbMachineCode_LoadingItems" OnSelectedIndexChanged="cmbMachineCode_SelectedIndexChanged">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Machine Code</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("MACHINE_CODE") %>' /></div>
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
                    </table>
                </td>
            </tr>
            <tr id="dvMachineDetail" runat="server">
                <td>
                    <table class="td" width="100%">
                        <tr>
                            <td width="10%" align="left">
                                <asp:Label ID="Label1" runat="server" Text="Segment :"></asp:Label>
                            </td>
                            <td width="10%" align="left">
                                <asp:Label ID="lblSegment" runat="server"></asp:Label>
                            </td>
                            <td width="10%" align="left">
                                <asp:Label ID="Label2" runat="server" Text="Section :"></asp:Label>
                            </td>
                            <td width="10%" align="left">
                                <asp:Label ID="lblSection" runat="server"></asp:Label>
                            </td>
                            <td width="10%" align="left">
                                <asp:Label ID="Label3" runat="server" Text="Type :"></asp:Label>
                            </td>
                            <td width="50%" align="left">
                                <asp:Label ID="lblType" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="td" width="100%">
                        <tr>
                            <td align="center" width="100%">
                                <span class="titleheading"><b>Machine Specification</b></span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="td" width="100%">
                        <tr>
                            <td width="30%" align="right">
                                Machine Make :
                            </td>
                            <td width="20%" align="left">
                                <asp:TextBox ID="txtMachineMake" runat="server" ValidationGroup="M1"></asp:TextBox>
                            </td>
                            <td width="30%" align="right">
                                YOM :
                            </td>
                            <td width="20%" align="left">
                                <asp:TextBox ID="txtYOM" runat="server" ValidationGroup="M1"></asp:TextBox>
                                <span class="Mode">
                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtYOM"
                                        Display="None" ErrorMessage="Enter Numeric Value" MaximumValue="3000" MinimumValue="1000"
                                        Type="Integer"></asp:RangeValidator>
                                </span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" class="td">
                        <tr>
                            <td width="25%" align="right">
                                No of Sides:
                            </td>
                            <td width="10%" align="left">
                                <asp:DropDownList ID="ddlUOMNoOfHeads" runat="server" Width="75px">
                                </asp:DropDownList>
                            </td>
                            <td width="15%" align="left">
                                <asp:TextBox ID="txtNoOfHeads" runat="server" ValidationGroup="M1"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtNoOfHeads"
                                    Display="None" ErrorMessage="Enter Numeric Value" MaximumValue="10000" MinimumValue="1"
                                    Type="Integer"></asp:RangeValidator>
                            </td>
                            <td width="15%" align="right">
                                No of Spindles :
                            </td>
                            <td width="10%" align="left">
                                <asp:DropDownList ID="ddlUOMNoOfSpindles" runat="server" Width="75px">
                                </asp:DropDownList>
                            </td>
                            <td width="15%" align="left">
                                <asp:TextBox ID="txtNoOfSpindles" runat="server" ValidationGroup="M1"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator10" runat="server" ControlToValidate="txtNoOfHeads"
                                    Display="None" ErrorMessage="Enter Numeric Value" MaximumValue="100000" MinimumValue="1"
                                    Type="Integer"></asp:RangeValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="25%" align="right">
                                No of Packages :
                            </td>
                            <td width="10%" align="left">
                                <asp:DropDownList ID="ddlUOMNoOfPackages" runat="server" Width="75px">
                                </asp:DropDownList>
                            </td>
                            <td width="15%" align="left">
                                <asp:TextBox ID="txtNoOfPackage" runat="server" ValidationGroup="M1"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="txtNoOfPackage"
                                    Display="None" ErrorMessage="Enter Numeric Value" MaximumValue="100000" MinimumValue="1"
                                    Type="Integer"></asp:RangeValidator>
                            </td>
                            <td width="25%" align="right">
                                Maximum Package :
                            </td>
                            <td width="10%" align="left">
                                <asp:DropDownList ID="ddlUOMMaxPackage" runat="server" Width="75px">
                                </asp:DropDownList>
                            </td>
                            <td width="15%" align="left">
                                <asp:TextBox ID="txtMaximumPackage" runat="server" ValidationGroup="M1"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="txtMaximumPackage"
                                    Display="None" ErrorMessage="Enter Numeric Value" MaximumValue="100000" MinimumValue="1"
                                    Type="Integer"></asp:RangeValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="25%" align="right">
                                Machines Speed :
                            </td>
                            <td width="10%" align="left">
                                <asp:DropDownList ID="ddlUOMMachineSpeed" runat="server" Width="75px">
                                </asp:DropDownList>
                            </td>
                            <td width="15%" align="left">
                                <asp:TextBox ID="txtMachineSpeed" runat="server" ValidationGroup="M1"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator5" runat="server" ControlToValidate="txtMachineSpeed"
                                    Display="None" ErrorMessage="Enter Numeric Value" MaximumValue="100000" MinimumValue="1"
                                    Type="Integer"></asp:RangeValidator>
                            </td>
                            <td width="25%" align="right">
                                Machine Capacity :
                            </td>
                            <td width="5%" align="left">
                                <asp:DropDownList ID="ddlUOMMachineCapacity" runat="server" Width="75px">
                                </asp:DropDownList>
                            </td>
                            <td width="20%" align="left">
                                <asp:TextBox ID="txtMachineCapacity" runat="server" ValidationGroup="M1"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator6" runat="server" ControlToValidate="txtMachineCapacity"
                                    Display="None" ErrorMessage="Enter Numeric Value" MaximumValue="100000" MinimumValue="1"
                                    Type="Integer"></asp:RangeValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="25%" align="right">
                                Count/Prod Ratio :
                            </td>
                            <td width="10%" align="left">
                                <asp:DropDownList ID="ddlUOMCount" runat="server" Width="75px">
                                </asp:DropDownList>
                            </td>
                            <td width="15%" align="left">
                                <asp:TextBox ID="txtCount" runat="server" ValidationGroup="M1"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator7" runat="server" ControlToValidate="txtCount"
                                    Display="None" ErrorMessage="Enter Numeric Value" MaximumValue="100000" MinimumValue="1"
                                    Type="Integer"></asp:RangeValidator>
                            </td>
                            <td width="25%" align="right">
                                Man Power/Day :
                            </td>
                            <td width="10%" align="left">
                                <asp:DropDownList ID="ddlUOMManpower" runat="server" Width="75px">
                                </asp:DropDownList>
                            </td>
                            <td width="15%" align="left">
                                <asp:TextBox ID="txtManpowerDay" runat="server" ValidationGroup="M1"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator8" runat="server" ControlToValidate="txtManpowerDay"
                                    Display="None" ErrorMessage="Enter Numeric Value" MaximumValue="100000" MinimumValue="1"
                                    Type="Integer"></asp:RangeValidator>
                            </td>
                        </tr>
                    <tr>
                <td width="25%" align="right">
                 Supplier :
                </td>
                 
                 <td width="10%" align="left">
                
                  </td>
                 <td width="15%" align="left">
                   <asp:TextBox ID="txtSupplier" runat="server" ValidationGroup="M1"></asp:TextBox>
                   
                   </td>
                  
                <td width="25%" align="right"></td>
               
                 <td width="10%" align="left">
                 
                  </td>
                 <td width="15%" align="left">
                
                 </td>
                </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="td" width="100%">
                        <tr>
                            <td align="center" width="100%">
                                <span class="titleheading"><b>Utility Consumption</b></span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="td">
                        <tr>
                            <td width="25%" align="right">
                                Steam :
                            </td>
                            <td width="10%" align="left">
                                <asp:DropDownList ID="ddlUOMSteam" runat="server" Width="75px">
                                </asp:DropDownList>
                            </td>
                            <td width="15%" align="left">
                                <asp:TextBox ID="txtSteam" runat="server" ValidationGroup="M1"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator9" runat="server" ControlToValidate="txtSteam"
                                    Display="None" ErrorMessage="Enter Numeric Value" MaximumValue="100000" MinimumValue="1"
                                    Type="Integer"></asp:RangeValidator>
                            </td>
                            <td width="25%" align="right">
                                Soft Water :
                            </td>
                            <td width="10%" align="left">
                                <asp:DropDownList ID="ddlUOMSoftwater" runat="server" Width="75px">
                                </asp:DropDownList>
                            </td>
                            <td width="15%" align="left">
                                <asp:TextBox ID="txtSoftWater" runat="server" ValidationGroup="M1"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator20" runat="server" ControlToValidate="txtSoftWater"
                                    Display="None" ErrorMessage="Enter Numeric Value" MaximumValue="100000" MinimumValue="1"
                                    Type="Integer"></asp:RangeValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="25%" align="right">
                                Air :
                            </td>
                            <td width="10%" align="left">
                                <asp:DropDownList ID="ddlUOMAir" runat="server" Width="75px">
                                </asp:DropDownList>
                            </td>
                            <td width="15%" align="left">
                                <asp:TextBox ID="txtAir" runat="server" ValidationGroup="M1"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator11" runat="server" ControlToValidate="txtAir"
                                    Display="None" ErrorMessage="Enter Numeric Value" MaximumValue="1000000" MinimumValue="1"
                                    Type="Integer"></asp:RangeValidator>
                            </td>
                            <td width="25%" align="right">
                                Power :
                            </td>
                            <td width="10%" align="left">
                                <asp:DropDownList ID="ddlUOMPower" runat="server" Width="75px">
                                </asp:DropDownList>
                            </td>
                            <td width="15%" align="left">
                                <asp:TextBox ID="txtPower" runat="server" ValidationGroup="M1"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator12" runat="server" ControlToValidate="txtPower"
                                    Display="None" ErrorMessage="Enter Numeric Value" MaximumValue="100000" MinimumValue="1"
                                    Type="Integer"></asp:RangeValidator>
                            </td>
                        </tr>
                        <tr>
                <td width="25%" align="right">
                 Coal :
                </td>
               
                 <td width="10%" align="left">
                <asp:DropDownList ID="ddlUomCoal" runat="server" Width="75px">
                                </asp:DropDownList>
                      </td>
                 <td width="15%" align="left">
                   <asp:TextBox ID="txtCoal" runat="server" ValidationGroup="M1"></asp:TextBox>
                    <asp:RangeValidator ID="RangeValidator18" runat="server" 
                            ControlToValidate="txtCoal" Display="None" ErrorMessage="Enter Numeric Value" 
                            MaximumValue="1000000" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                   </td>
                   
                   <td width="25%" align="right"></td>
                    <td width="10%" align="right">
              
                </td>
                 <td width="10%" align="left">
               
                      </td>
                 <td width="15%" align="left">
                 
                            
                 </td>
                </tr>
                    </table>
                </td>
            </tr>
        </table>
   <%-- </ContentTemplate>
</asp:UpdatePanel>--%>
