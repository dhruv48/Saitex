<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LedgerMstPopUp.ascx.cs"
    Inherits="FA_Controls_LedgerMstPopUp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>

<script language="javascript" type="text/javascript">
    function GetRowValue(val,TextBoxId)
    {           
        window.opener.document.getElementById(TextBoxId).value=val;   
        window.opener.document.forms[0].submit();      
        window.close();
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
        width: 40px;
    }
    .c2
    {
        margin-left: 4px;
        width: 100px;
    }
    .c3
    {
        margin-left: 4px;
        width: 200px;
    }
</style>
<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>--%>
<table bgcolor="#C1D3FB">
    <tr>
        <td class="td">
            <table>
                <tr>
                    <td id="tdSave" runat="server">
                        <asp:ImageButton ID="imgbtnSave" runat="server" Width="48" Height="41" ToolTip="Save"
                            ValidationGroup="M1" ImageUrl="~/CommonImages/save.jpg" OnClick="imgbtnSave_Click"
                            TabIndex="10" />
                    </td>
                    <td id="tdFind" runat="server">
                        <asp:ImageButton ID="imgbtnFind" runat="server" Width="48" Height="41" ToolTip="Find"
                            ImageUrl="~/CommonImages/link_find.png" OnClick="imgbtnFind_Click"></asp:ImageButton>
                    </td>
                    <td id="tdUpdate" runat="server">
                        <asp:ImageButton ID="imgbtnUpdate" Width="48" Height="41" runat="server" ToolTip="Update"
                            ImageUrl="~/CommonImages/edit1.jpg" OnClick="imgbtnUpdate_Click" ValidationGroup="M1">
                        </asp:ImageButton>
                    </td>
                    <td id="tdDelete" runat="server">
                        <asp:ImageButton ID="imgbtnDelete" Width="48" Height="41" runat="server" ToolTip="Delete"
                            ImageUrl="~/CommonImages/del6.png" OnClick="imgbtnDelete_Click"></asp:ImageButton>
                    </td>
                    <td id="tdClear" runat="server">
                        <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" ToolTip="Clear"
                            ImageUrl="~/CommonImages/clear.jpg" OnClick="imgbtnClear_Click"></asp:ImageButton>
                    </td>
                    <td id="tdPrint" runat="server">
                        <asp:ImageButton ID="imgbtnPrint" Width="48" Height="41" runat="server" ToolTip="Print"
                            ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click"></asp:ImageButton>
                    </td>
                    <%--<td id="tdExit" runat="server">
                        <asp:ImageButton ID="imgbtnExit" Width="48" Height="41" runat="server" ToolTip="Exit"
                            ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click"></asp:ImageButton>
                    </td>--%>
                    <td id="tdHelp" runat="server">
                        <asp:ImageButton ID="imgbtnHelp" Width="48" Height="41" runat="server" ToolTip="Help"
                            ImageUrl="~/CommonImages/link_help.png" OnClick="imgbtnHelp_Click"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr class="TableHeader">
        <td align="center" valign="top" class="td">
            <span class="titleheading">Ledger Master</span>
        </td>
    </tr>
    <tr>
        <td class="tdLeft td">
            <span class="Mode">You are in
                <asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode </span>
        </td>
    </tr>
    <tr>
        <td class="td">
            <table>
                <tr>
                    <td valign="top" align="center" colspan="3">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ValidationGroup="M1" ShowSummary="False" />
                        <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label><strong>
                        </strong>
                        <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label><strong>
                        </strong>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" style="height: 23px">
                        *Ledger Code
                    </td>
                    <td align="left" valign="top" style="height: 23px">
                        <asp:TextBox ID="txtLedgerCode" runat="server" CssClass="gCtrTxt UpperCase TextBoxDisplay"
                            ValidationGroup="M1" Width="182px" MaxLength="50" ReadOnly="true"></asp:TextBox>
                        <cc2:ComboBox ID="cmbLedgerCode" runat="server" Width="190px" Height="200px" AutoPostBack="True"
                            DataTextField="LDGR_NAME" DataValueField="LDGR_CODE" EnableLoadOnDemand="True"
                            OnLoadingItems="cmbLedgerCode_LoadingItems" OnSelectedIndexChanged="cmbLedgerCode_SelectedIndexChanged"
                            TabIndex="1" MenuWidth="325px">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Code
                                </div>
                                <div class="header c3">
                                    Ledger Name</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <%# Eval("LDGR_CODE")%></div>
                                <div class="item c3">
                                    <%# Eval("LDGR_NAME")%></div>
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
                    <td align="right" valign="top" style="height: 23px">
                        *Ledger Name
                    </td>
                    <td align="left" valign="top" style="height: 23px">
                        <asp:TextBox ID="txtLedgerName" runat="server" CssClass="gCtrTxt UpperCase" ValidationGroup="M1"
                            Width="182px" TabIndex="2" MaxLength="50"></asp:TextBox>&nbsp;
                        <asp:RequiredFieldValidator ID="RFLedgerName" runat="server" ValidationGroup="M1"
                            Display="None" ErrorMessage="Enter Ledger Name" ControlToValidate="txtLedgerName"
                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" style="height: 24px">
                        *Ledger Type
                    </td>
                    <td align="left" valign="top" style="height: 24px">
                        <asp:DropDownList ID="cmbLedgerType" runat="server" AppendDataBoundItems="true" Width="190px"
                            DataTextField="MST_CODE" DataValueField="MST_CODE" TabIndex="3" CssClass="SmallFont">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RFLedgerType" runat="server" ControlToValidate="cmbLedgerType"
                            Display="None" ErrorMessage="Please Select Ledger Type" InitialValue="0" SetFocusOnError="True"
                            ValidationGroup="M1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" style="height: 24px">
                        *Ledger Group
                    </td>
                    <td align="left" valign="top" style="height: 24px">
                        <asp:DropDownList ID="cmbLedgerGroup" runat="server" AppendDataBoundItems="true"
                            DataTextField="MST_CODE" DataValueField="MST_CODE" CssClass="SmallFont" TabIndex="4"
                            Width="190px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RFLedgerGroup" runat="server" ControlToValidate="cmbLedgerGroup"
                            Display="None" ErrorMessage="Please Select Ledger Group" InitialValue="0" SetFocusOnError="True"
                            ValidationGroup="M1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" style="height: 24px">
                        *Group Code
                    </td>
                    <td align="left" valign="top" style="height: 24px">
                        <asp:TextBox ID="txtGroupCode" runat="server" TabIndex="5" CssClass="TextBox TextBoxDisplay"
                            ValidationGroup="M1" Width="37px" ReadOnly="true" AutoPostBack="True" OnTextChanged="txtGroupCode_TextChanged"></asp:TextBox>
                        <asp:TextBox ID="txtGroupName" runat="server" TabIndex="5" CssClass="TextBox TextBoxDisplay"
                            ValidationGroup="M1" Width="137px" ReadOnly="true"></asp:TextBox>
                        <asp:Button ID="lnkbtnGroupCode" runat="server" Text="Browse" OnClick="lnkbtnGroupCode_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="right" style="height: 24px" valign="top">
                        *Print Ledger
                    </td>
                    <td align="left" style="height: 24px" valign="top">
                        <asp:TextBox ID="txtLedgerPrint" runat="server" CssClass="gCtrTxt UpperCase" MaxLength="50"
                            TabIndex="6" ValidationGroup="M1" Width="182px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFPrintLedger" runat="server" ValidationGroup="M1"
                            Display="None" ErrorMessage="Enter Print Ledger" ControlToValidate="txtLedgerPrint"
                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" style="height: 40px">
                        Description
                    </td>
                    <td align="left" valign="top" style="height: 40px">
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="gCtrTxt" Width="184px"
                            TabIndex="7" TextMode="multiLine" Rows="2" MaxLength="200" Height="44px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top">
                        Opening Balance
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtOpeningBalance" runat="server" CssClass="TextBoxNo" MaxLength="15"
                            TabIndex="8" ValidationGroup="M1" Width="183px"></asp:TextBox>
                        <asp:RangeValidator ID="RVOpeningBalance" ControlToValidate="txtOpeningBalance" runat="server"
                            ErrorMessage="Only numeric value allowed" Display="None" MaximumValue="1000000"
                            MinimumValue="0" Type="Integer" ValidationGroup="M1"></asp:RangeValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top">
                        Status
                    </td>
                    <td align="left" valign="top">
                        <asp:CheckBox ID="chk_Status" runat="server" TabIndex="9" AutoPostBack="True" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Opening Debit :
                        <asp:CheckBox ID="chk_Debit" runat="server" TabIndex="10" AutoPostBack="True" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnClose" runat="server" Text="Close Window" Width="120px" OnClick="btnClose_Click" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<%--</ContentTemplate>
</asp:UpdatePanel>--%>