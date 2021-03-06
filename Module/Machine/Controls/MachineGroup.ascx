<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MachineGroup.ascx.cs"
    Inherits="Module_Machine_Controls_MachineGroup" %>
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
        margin-left: 4px;
        width: 150px;
    }
    .c3
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
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="left" class="tContentArial">
            <tr>
                <td valign="top" align="left" class="td">
                    <table>
                        <tr>
                            <td id="tdSave" runat="server">
                                <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="Save" ImageUrl="~/CommonImages/save.jpg"
                                    ValidationGroup="M1" OnClick="imgbtnSave_Click" />
                            </td>
                            <td id="tdUpdate" runat="server">
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                    ValidationGroup="M1" OnClick="imgbtnUpdate_Click"></asp:ImageButton>
                            </td>
                            <td id="tdFind" runat="server">
                                <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/link_find.png"
                                    OnClick="imgbtnFind_Click"></asp:ImageButton>
                            </td>
                            <td id="tdPrint" runat="server">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" 
                            ImageUrl="~/CommonImages/link_print.png" onclick="imgbtnPrint_Click"
                            ></asp:ImageButton>
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
                                <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png">
                                </asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="TableHeader td">
                    <span class="titleheading"><b>Machine Group Master</b></span>
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
                            <td width="25%" align="right">
                                Segment :
                            </td>
                            <td width="25%" align="left">
                                <asp:DropDownList ID="ddlSegment" runat="server" DataTextField="MST_CODE" DataValueField="MST_CODE"
                                    Width="150px" CssClass="SmallFont" TabIndex="1">
                                </asp:DropDownList>
                            </td>
                            <td width="25%" align="right">
                                Section :
                            </td>
                            <td width="25%" align="left">
                                <asp:DropDownList ID="ddlSection" runat="server" DataTextField="MST_CODE" DataValueField="MST_CODE"
                                    Width="150px" CssClass="SmallFont" TabIndex="1">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlSection"
                                    ErrorMessage="Enter Machine Section" ValidationGroup="M1" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="25%" align="right">
                                Machine Type :
                            </td>
                            <td width="25%" align="left">
                                <asp:DropDownList ID="ddlMachineType" runat="server" DataTextField="MST_CODE" DataValueField="MST_CODE"
                                    Width="150px" CssClass="SmallFont" TabIndex="1">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlMachineType"
                                    ErrorMessage="Enter Machine Type" ValidationGroup="M1" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                            <td width="25%" align="right">
                                Group Code :
                            </td>
                            <td width="25%" align="left">
                                <asp:TextBox ID="txtMachineGroup" runat="server" ValidationGroup="M1" onkeyup="javascript:this.value = this.value.toUpperCase();"
                                    OnTextChanged="txtMachineGroup_TextChanged" AutoPostBack="True" MaxLength="20"></asp:TextBox>
                                <cc2:ComboBox ID="cmbMachineGroup" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    DataTextField="MACHINE_GROUP" DataValueField="MACHINE_GROUP" Width="150px" MenuWidth="500px"
                                    Height="200px" CssClass="SmallFont" TabIndex="1" EmptyText="Select Machine Group"
                                    OnLoadingItems="cmbMachineGroup_LoadingItems" OnSelectedIndexChanged="cmbMachineGroup_SelectedIndexChanged1">
                                    <HeaderTemplate>
                                        <div class="header c1">
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
                                        <div class="item c1">
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
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtMachineGroup"
                                    ErrorMessage="Enter Machine Group" ValidationGroup="M1" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
