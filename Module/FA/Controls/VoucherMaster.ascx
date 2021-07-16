<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VoucherMaster.ascx.cs"
    Inherits="Module_FA_Controls_VoucherMaster" %>
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
        width: 120px;
    }
    .c3
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
<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>--%>
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
                    <b class="titleheading">Voucher Master</b>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <table cellpadding="0" cellspacing="0" border="0" align="left" class="tContentArial">
                        <tr>
                            <td align="left" colspan="5" valign="top">
                                <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="center" colspan="5">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ValidationGroup="M1" ShowSummary="False" />
                                <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label><strong>
                                </strong>
                                <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label><strong>
                                </strong>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                *Voucher Code
                            </td>
                            <td align="left" valign="top" colspan="3">
                                <asp:TextBox ID="txtVoucherCode" runat="server" CssClass="gCtrTxt UpperCase TextBoxDisplay"
                                    ValidationGroup="M1" MaxLength="50" Width="145px" ReadOnly="true"></asp:TextBox>
                                <cc1:ComboBox ID="cmbVoucherCode" runat="server" Width="150px" Height="200px" AutoPostBack="True"
                                    EmptyText="Select Voucher" EnableLoadOnDemand="True" OnLoadingItems="cmbVoucherCode_LoadingItems"
                                    OnSelectedIndexChanged="cmbVoucherCode_SelectedIndexChanged" DataTextField="VCHR_NAME"
                                    DataValueField="VCHR_CODE" TabIndex="1" MenuWidth="200px">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Code
                                        </div>
                                        <div class="header c2">
                                            Voucher Name</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <%# Eval("VCHR_CODE")%></div>
                                        <div class="item c2">
                                            <%# Eval("VCHR_NAME")%></div>
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
                                *Voucher Name
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtVoucherName" runat="server" CssClass="gCtrTxt UpperCase" ValidationGroup="M1"
                                    Width="145px" TabIndex="2" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFVoucherName" runat="server" ValidationGroup="M1"
                                    Display="None" ErrorMessage="Enter Voucher Name" ControlToValidate="txtVoucherName"
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right" valign="top">
                                *Voucher Type
                            </td>
                            <td align="left" valign="top">
                                <asp:DropDownList ID="cmbVoucherType" runat="server" Width="148px" AppendDataBoundItems="true"
                                    DataTextField="MST_CODE" DataValueField="MST_CODE" TabIndex="3" CssClass="SmallFont">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFVoucherType" runat="server" ControlToValidate="cmbVoucherType"
                                    Display="None" ErrorMessage="Please Select Voucher Type" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="M1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                *Prefix
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtPrefix" runat="server" CssClass="gCtrTxt UpperCase" MaxLength="2"
                                    TabIndex="4" ValidationGroup="M1" Width="145px"></asp:TextBox>
                            </td>
                            <td align="right" valign="top">
                                Suffix
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtSuffix" runat="server" CssClass="gCtrTxt UpperCase" MaxLength="2"
                                    Width="145px" TabIndex="5" ValidationGroup="M1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                Description
                            </td>
                            <td align="left" valign="top" colspan="5">
                                <asp:TextBox ID="txtDescription" runat="server" CssClass="gCtrTxt" Width="427px"
                                    TabIndex="6" TextMode="multiLine" Rows="2" MaxLength="200" Height="45px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                Status
                            </td>
                            <td align="left" valign="top" colspan="5">
                                <asp:CheckBox ID="chk_Status" runat="server" TabIndex="7" />
                            </td>
                            <td align="left" valign="top">
                                &nbsp;
                            </td>
                            <td align="left" valign="top">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" colspan="5">
                                <table>
                                    <tr>
                                        <td style="margin-left: 80px">
                                            <cc2:Grid ID="grdVoucher" runat="server" AllowAddingRecords="False" AllowFiltering="True"
                                                PageSize="5" AutoGenerateColumns="False" OnSelect="grdVoucher_Select">
                                                <Columns>
                                                    <cc2:Column DataField="VCHR_CODE" Align="Left" HeaderText="Code" Width="65px">
                                                    </cc2:Column>
                                                    <cc2:Column DataField="VCHR_NAME" Align="Left" HeaderText="Voucher Name" Width="125px">
                                                    </cc2:Column>
                                                    <cc2:Column DataField="VCHR_TYPE" Align="Left" HeaderText="Type" Width="90px">
                                                    </cc2:Column>
                                                    <cc2:Column DataField="VCHR_PREFIX" Align="Left" HeaderText="Pre" Width="60px">
                                                    </cc2:Column>
                                                    <cc2:Column DataField="VCHR_SUFFIX" Align="Left" HeaderText="Suff" Width="60px">
                                                    </cc2:Column>
                                                    <cc2:Column DataField="VCHR_DESC" Align="Left" HeaderText="Description" Width="150px">
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
<%--    </ContentTemplate>
</asp:UpdatePanel>--%>
