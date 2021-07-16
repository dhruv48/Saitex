<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FABankMaster.ascx.cs"
    Inherits="Module_FA_Controls_FABankMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>--%>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1;
        display: inline;
        overflow: hidden;
        white-space: nowrap;
    }
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
        width: 200px;
    }
    .c3
    {
        margin-left: 4px;
        width: 80px;
    }
    
</style>
<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>--%>
<div>
    <table width="100%">
        <tr>
            <td align= "left">
                <table class="tContentArial td" width="100%">
                    <tr>
                        <td align = "left">
                            <table class="tContentArial ">
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
                                    <td id="tdHelp" runat="server">
                                        <asp:ImageButton ID="imgbtnHelp" Width="48" Height="41" runat="server" ToolTip="Help"
                                            ImageUrl="~/CommonImages/link_help.png" OnClick="imgbtnHelp_Click"></asp:ImageButton>
                                    </td>
                                    <td id="tdExit" runat="server">
                                        <asp:ImageButton ID="imgbtnExit" Width="48" Height="41" runat="server" ToolTip="Exit"
                                            ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click"></asp:ImageButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="TableHeader td" align="center">
                            <b class="titleheading">Account Bank Master</b>
                        </td>
                    </tr>
                    <tr>
                        <td class="td" valign="top" align="left">
                            <span class="Mode">You are in
                                <asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode </span>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="center">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                ValidationGroup="M1" ShowSummary="False" />
                            <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label><strong>
                            </strong>
                            <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label><strong>
                            </strong>
                        </td>
                    </tr>
                    <tr>
                        <td class="td">
                            <table align="left" class="tContentArial">
                                <tr>
                                    <td>
                                        *Bank Code
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBankCode" runat="server" CssClass="gCtrTxt UpperCase TextBoxDisplay"
                                            ValidationGroup="M1" Width="182px" MaxLength="30" TabIndex="1"></asp:TextBox>
                                        <cc2:ComboBox ID="cmbBankCode" runat="server" Width="190px" Height="150px" AutoPostBack="True"
                                            DataTextField="LGR_BANK_NAME" DataValueField="LGR_BANK_CODE" TabIndex="2" OnLoadingItems="cmbBankCode_LoadingItems"
                                            OnSelectedIndexChanged="cmbBankCode_SelectedIndexChanged" EnableLoadOnDemand="True"
                                            MenuWidth="300px">
                                            <HeaderTemplate>
                                                <div class="header c1">
                                                    Code
                                                </div>
                                                <div class="header c2">
                                                    Bank Name</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div class="item c1">
                                                    <%# Eval("LGR_BANK_CODE")%></div>
                                                <div class="item c2">
                                                    <%# Eval("LGR_BANK_NAME")%></div>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                Displaying items
                                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                out of
                                                <%# Container.ItemsCount %>.
                                            </FooterTemplate>
                                        </cc2:ComboBox>
                                    </td>
                                    <td>
                                        *Bank Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBankName" runat="server" CssClass="gCtrTxt UpperCase" ValidationGroup="M1"
                                            Width="182px" MaxLength="30" TabIndex="3"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RFBankName" runat="server" ValidationGroup="M1" Display="None"
                                            ErrorMessage="Enter Bank Name" ControlToValidate="txtBankName" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        *Bank Branch Code
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBranchCode" runat="server" CssClass="gCtrTxt UpperCase" ValidationGroup="M1"
                                            Width="182px" TabIndex="4" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RFBankBranchCode" runat="server" ValidationGroup="M1"
                                            Display="None" ErrorMessage="Enter Bank Branch Code" ControlToValidate="txtBranchCode"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        Bank Address
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="gCtrTxt UpperCase" ValidationGroup="M1"
                                            Width="182px" TabIndex="5" MaxLength="75"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        *Account No
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtACNo" runat="server" CssClass="gCtrTxt UpperCase" ValidationGroup="M1"
                                            Width="182px" TabIndex="6" MaxLength="25"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RFAccountNo" runat="server" ValidationGroup="M1"
                                            Display="None" ErrorMessage="Enter Account No" ControlToValidate="txtACNo" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        *Account Type
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtACType" runat="server" CssClass="gCtrTxt UpperCase" ValidationGroup="M1"
                                            Width="182px" TabIndex="7" MaxLength="30"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RFAccountType" runat="server" ValidationGroup="M1"
                                            Display="None" ErrorMessage="Enter Account Type" ControlToValidate="txtACType"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        *RTGS Code
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRTGSCode" runat="server" CssClass="gCtrTxt UpperCase" ValidationGroup="M1"
                                            Width="182px" TabIndex="8" MaxLength="25"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RFRTGSCode" runat="server" ValidationGroup="M1" Display="None"
                                            ErrorMessage="Enter RTGS Code" ControlToValidate="txtRTGSCode" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="Tableheader" align="left">
                            <table>
                        </td>
                    </tr>
                    <tr>
                  
                        <td class="TableHeader td" align="center" colspan="0">
                            <b class="titleheading">Ledger Details</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            *Ledger Code
                        </td>
                        <td>
                            <asp:TextBox ID="txtLedgerCode" runat="server" CssClass="gCtrTxt UpperCase TextBoxDisplay"
                                ValidationGroup="M1" Width="182px" TabIndex="9" MaxLength="30"></asp:TextBox>
                        </td>
                        <td>
                            *Ledger Name
                        </td>
                        <td>
                            <asp:TextBox ID="txtLedgerName" runat="server" CssClass="gCtrTxt UpperCase" ValidationGroup="M1"
                                Width="182px" TabIndex="10" MaxLength="30"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFLedgerName" runat="server" ValidationGroup="M1"
                                Display="None" ErrorMessage="Enter Ledger Name" ControlToValidate="txtLedgerName"
                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            *Ledger Type
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbLedgerType" runat="server" AppendDataBoundItems="true" Width="190px"
                                DataTextField="MST_CODE" DataValueField="MST_CODE" TabIndex="11" CssClass="SmallFont">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RFLedgerType" runat="server" ControlToValidate="cmbLedgerType"
                                Display="None" ErrorMessage="Please Select Ledger Type" InitialValue="0" SetFocusOnError="True"
                                ValidationGroup="M1"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            *Print Name
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrintName" runat="server" CssClass="gCtrTxt UpperCase" ValidationGroup="M1"
                                Width="182px" TabIndex="12" MaxLength="30"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            *Group Code
                        </td>
                        <td>
                            <asp:TextBox ID="txtGroupCode" runat="server" TabIndex="13" CssClass="TextBox TextBoxDisplay"
                                ValidationGroup="M1" Width="37px" ReadOnly="true" AutoPostBack="True" OnTextChanged="txtGroupCode_TextChanged"></asp:TextBox>
                            <asp:TextBox ID="txtGroupName" runat="server" TabIndex="14" CssClass="TextBox TextBoxDisplay"
                                ValidationGroup="M1" Width="140px" ReadOnly="true"></asp:TextBox>
                            <asp:LinkButton ID="lnkbtnGroupCode" runat="server" ForeColor="BlueViolet" Font-Bold="true"
                                Font-Italic="true" OnClick="lnkbtnGroupCode_Click" TabIndex="15">Click</asp:LinkButton>
                        </td>
                        <td>
                            *Ledger Group
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbLedgerGroup" runat="server" AppendDataBoundItems="true"
                                DataTextField="MST_CODE" DataValueField="MST_CODE" CssClass="SmallFont" TabIndex="16"
                                Width="190px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RFLedgerGroup" runat="server" ControlToValidate="cmbLedgerGroup"
                                Display="None" ErrorMessage="Please Select Ledger Group" InitialValue="0" SetFocusOnError="True"
                                ValidationGroup="M1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Opening Balance
                        </td>
                        <td>
                            <asp:TextBox ID="txtOpeningBalance" runat="server" CssClass="TextBoxNo" MaxLength="17"
                                TabIndex="13" ValidationGroup="M1" Width="183px"></asp:TextBox>
                            <asp:RangeValidator ID="RVOpeningBalance" ControlToValidate="txtOpeningBalance" runat="server"
                                ErrorMessage="Only numeric value allowed" Display="None" MaximumValue="1000000"
                                MinimumValue="0" Type="Integer" ValidationGroup="M1"></asp:RangeValidator>
                        </td>
                        <td>
                            Opening Date
                        </td>
                        <td>
                            <asp:TextBox ID="txtOpeningDate" runat="server" ValidationGroup="M1" Width="183px"
                                TabIndex="18"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalOpeningDate" runat="server" TargetControlID="txtOpeningDate"
                                PopupPosition="TopLeft" Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="TableHeader td" align="center" colspan="0">
                            <b class="titleheading">Other Details</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Cheque Book
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rdoListChequeBook" runat="server" RepeatDirection="Horizontal"
                                TabIndex="19">
                                <asp:ListItem Selected="True">Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            Debit Card
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rdoListDebitCard" runat="server" RepeatDirection="Horizontal"
                                TabIndex="20">
                                <asp:ListItem Selected="True">Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Credit Card
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rdoListCreditCard" runat="server" RepeatDirection="Horizontal"
                                TabIndex="21">
                                <asp:ListItem Selected="True">Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            Internet Banking
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rdoListInternetBanking" runat="server" RepeatDirection="Horizontal"
                                TabIndex="22">
                                <asp:ListItem Selected="True">Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Phone Banking
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rdoListPhoneBanking" runat="server" RepeatDirection="Horizontal"
                                TabIndex="23">
                                <asp:ListItem Selected="True">Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            Passbook
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rdoListPassbook" runat="server" RepeatDirection="Horizontal"
                                TabIndex="24">
                                <asp:ListItem Selected="True">Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Online Shopping
                        </td>
                        <td colspan="3">
                            <asp:RadioButtonList ID="rdoListOnlineShopping" runat="server" RepeatDirection="Horizontal"
                                TabIndex="25">
                                <asp:ListItem Selected="True">Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Description
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="txtDescription" runat="server" CssClass="gCtrTxt" Width="480px"
                                TabIndex="26" TextMode="multiLine" Rows="2" MaxLength="200" Height="61px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Status
                        </td>
                        <td colspan="3">
                            <asp:CheckBox ID="chk_Status" runat="server" TabIndex="27" AutoPostBack="True" />
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td valign="top" class="td">
                            <asp:GridView ID="grdBank" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="False" BorderStyle="Ridge" CellPadding="3" CssClass="smallfont"
                                EmptyDataText="No Record Found" Font-Size="X-Small" ForeColor="#333333" OnPageIndexChanging="grdBank_PageIndexChanging"
                                PagerStyle-HorizontalAlign="Left" PageSize="7" OnSelect="grdBank_Select" Width="100%">
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                                <RowStyle BackColor="#EFF3FB" />
                                <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                                <Columns>
                                    <asp:BoundField DataField="LGR_BANK_CODE" HeaderText="Code" />
                                    <asp:BoundField DataField="BANK_BRANCH_CODE" HeaderText="Branch Code" />
                                    <asp:BoundField DataField="LGR_BANK_NAME" HeaderText="Bank Name" />
                                    <asp:BoundField DataField="BANK_AC_NO" HeaderText="Account No" />
                                    <asp:BoundField DataField="BANK_AC_TYPE" HeaderText="Account Type" />
                                    <asp:BoundField DataField="LDGR_NAME" HeaderText="Ledger Name" />
                                </Columns>
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
   </table>
</div>
<%-- </ContentTemplate>
</asp:UpdatePanel>
--%>