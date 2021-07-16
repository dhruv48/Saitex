<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdvancedAdvice.ascx.cs"
    Inherits="Module_FA_Controls_AdvancedAdvice" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
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
        width: 50px;
    }
    .c2
    {
        margin-left: 4px;
        width: 220px;
    }
    .c3
    {
        margin-left: 4px;
        width: 80px;
    }
    .c4
    {
        margin-left: 4px;
        width: 80px;
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
        <table cellpadding="0" cellspacing="0" border="0" align="left" class="tContentArial tablebox">
            <tr>
                <td class="td" colspan="3">
                    <table cellpadding="0" cellspacing="0" border="1" align="left" class="tContentArial ">
                        <tr>
                            <td id="tdSave" runat="server">
                                <asp:ImageButton ID="imgbtnSave" runat="server" Width="48" Height="41" ToolTip="Save"
                                    ValidationGroup="M1" ImageUrl="~/CommonImages/save.jpg" OnClick="imgbtnSave_Click"
                                    TabIndex="8" />
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
                            <td id="tdExit" runat="server">
                                <asp:ImageButton ID="imgbtnExit" Width="48" Height="41" runat="server" ToolTip="Exit"
                                    ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click"></asp:ImageButton>
                            </td>
                            <td id="tdHelp" runat="server">
                                <asp:ImageButton ID="imgbtnHelp" Width="48" Height="41" runat="server" ToolTip="Help"
                                    ImageUrl="~/CommonImages/link_help.png" OnClick="imgbtnHelp_Click"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="TableHeader td" align="center" colspan="3">
                    <b class="titleheading">Advanced Payment Advice</b>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <table cellpadding="0" cellspacing="0" border="0" align="left" class="tContentArial">
                        <tr>
                            <td align="left" colspan="5" valign="top">
                                <span class="Mode">You are in
                                    <asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode </span>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="center" colspan="5">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ValidationGroup="M1" />
                                <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label><strong>
                                </strong>
                                <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label><strong>
                                </strong>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                *Advice No :
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtAdviceNo" runat="server" CssClass="TextBoxNo TextBoxDisplay UpperCase"
                                    ValidationGroup="M1" MaxLength="15" Width="175px" ReadOnly="true" TabIndex="1"></asp:TextBox>
                                <cc1:ComboBox ID="cmbAdviceNo" runat="server" Width="175px" Height="200px" AutoPostBack="True"
                                    EnableLoadOnDemand="True" EmptyText="select advice no" DataTextField="ADV_NO"
                                    DataValueField="ADV_NO" TabIndex="2" MenuWidth="550px" OnLoadingItems="cmbAdviceNo_LoadingItems"
                                    OnSelectedIndexChanged="cmbAdviceNo_SelectedIndexChanged">
                                    <HeaderTemplate>
                                        <div class="header c3">
                                            Advice No
                                        </div>
                                        <div class="header c3">
                                            Code</div>
                                        <div class="header c2">
                                            Party Name</div>
                                        <div class="header c3">
                                            Amount</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c3">
                                            <%# Eval("ADV_NO")%></div>
                                        <div class="item c3">
                                            <%# Eval("LEDGER_CODE")%></div>
                                        <div class="item c2">
                                            <%# Eval("LDGR_NAME")%></div>
                                        <div class="item c3">
                                            <%# Eval("ADV_AMT")%></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc1:ComboBox>
                            </td>
                            <td>
                                *Select Party :
                            </td>
                            <td>
                                <cc1:ComboBox ID="cmbPartyLedger" runat="server" Width="175px" Height="200px" EmptyText="select party"
                                    DataTextField="LDGR_NAME" DataValueField="LDGR_CODE" TabIndex="3" MenuWidth="320px"
                                    OnLoadingItems="cmbPartyLedger_LoadingItems" AutoPostBack="true">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Code
                                        </div>
                                        <div class="header c2">
                                            Party Name</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <%# Eval("LDGR_CODE")%></div>
                                        <div class="item c2">
                                            <%# Eval("LDGR_NAME")%></div>
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
                            <td align="right" valign="top">
                                *Amount :
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtAmount" runat="server" CssClass="TextBoxNo" ValidationGroup="M1"
                                    Width="175px" TabIndex="4" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFAmount" runat="server" ValidationGroup="M1" Display="None"
                                    ErrorMessage="Enter Advice Amount" ControlToValidate="txtAmount" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RVAmount" runat="server" ValidationGroup="M1" Display="None"
                                    ControlToValidate="txtAmount" MinimumValue="0" MaximumValue="9999999" ErrorMessage="Enter only number"
                                    Type="Integer" SetFocusOnError="true"></asp:RangeValidator>
                            </td>
                            <td align="left" valign="top">
                                *Advice Date :
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtAdviceDate" runat="server" CssClass="TextBox" Width="175px" TabIndex="5"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                Description :
                            </td>
                            <td align="left" valign="top" colspan="5">
                                <asp:TextBox ID="txtDescription" runat="server" CssClass="gCtrTxt" Width="520px"
                                    TabIndex="6" MaxLength="200"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" colspan="5">
                                <table>
                                    <tr>
                                        <td style="margin-left: 80px">
                                            <cc2:Grid ID="grdAdvice" runat="server" AllowAddingRecords="False" AllowFiltering="True"
                                                PageSize="5" AutoGenerateColumns="False" OnSelect="grdAdvice_Select">
                                                <Columns>
                                                    <cc2:Column DataField="ADV_NO" Align="Left" HeaderText="Adv No" Width="80px">
                                                    </cc2:Column>
                                                    <cc2:Column DataField="LEDGER_CODE" Align="Left" HeaderText="Code" Width="45px" Visible="false">
                                                    </cc2:Column>
                                                    <cc2:Column DataField="LDGR_NAME" Align="Left" HeaderText="Ledger Name" Width="175px">
                                                    </cc2:Column>
                                                    <cc2:Column DataField="ADV_AMT" Align="Left" HeaderText="Amount" Width="90px">
                                                    </cc2:Column>
                                                    <cc2:Column DataField="ADV_DATE" Align="Left" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}"
                                                        Width="90px">
                                                    </cc2:Column>
                                                    <cc2:Column DataField="DESCRIPTION" Align="Left" HeaderText="Description" Width="250px">
                                                    </cc2:Column>
                                                </Columns>
                                            </cc2:Grid>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <cc4:CalendarExtender ID="ce1" runat="server" TargetControlID="txtAdviceDate" OnClientDateSelectionChanged="checkDate"
            PopupPosition="TopLeft" Format="dd/MM/yyyy">
        </cc4:CalendarExtender>
        <cc4:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtAdviceDate" PromptCharacter="_">
        </cc4:MaskedEditExtender>
    </ContentTemplate>
</asp:UpdatePanel>
