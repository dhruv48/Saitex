<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PurchaseVoucher.ascx.cs"
    Inherits="Module_FA_Controls_PurchaseVoucher" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>
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
        margin-left: 4px;
        width: 100px;
    }
    .c3
    {
        margin-left: 4px;
        width: 230px;
    }
    .c4
    {
        margin-left: 4px;
        width: 150px;
    }
    .TextBoxNo
    {
    }
</style>

<script language="javascript" type="text/javascript">
function GetRowValue() {
window.opener.location.href = window.opener.location.href;
if (window.opener.progressWindow)
{
window.opener.progressWindow.close()
}
window.close();
}
</script>
<asp:ScriptManager ID=scrid1 runat="server"></asp:ScriptManager>
<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>--%>
        <table class="tContentArial">
            <tr>
                <td class="td">
                    <table>
                        <tr id="trToolBar" runat="server">
                            <td id="tdSave" runat="server">
                                <asp:ImageButton ID="imgbtnNew" runat="server" Height="41" ImageUrl="~/CommonImages/save.jpg"
                                    OnClick="imgbtnNew_Click" ToolTip="Save" ValidationGroup="M1" Width="48" />
                            </td>
                            <td id="tdUpdate" runat="server">
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" Height="41" ImageUrl="~/CommonImages/edit1.jpg"
                                    OnClick="imgbtnUpdate_Click" ToolTip="Update" ValidationGroup="M1" Width="48" />
                            </td>
                            <td id="tdDelete" runat="server">
                                <asp:ImageButton ID="imgbtnDelete" runat="server" Height="41" ImageUrl="~/CommonImages/del6.png"
                                    OnClick="imgbtnDelete_Click" ToolTip="Delete" Width="48" />
                            </td>
                            <td id="tdFind" runat="server">
                                <asp:ImageButton ID="imgbtnFind" runat="server" Height="41" ImageUrl="~/CommonImages/link_find.png"
                                    OnClick="imgbtnFind_Click" ToolTip="Find" Width="48" />
                            </td>
                            <td id="tdClear" runat="server">
                                <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                                    OnClick="imgbtnClear_Click" ToolTip="Clear" Width="48" />
                            </td>
                            <td id="tdPrint" runat="server">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                                    OnClick="imgbtnPrint_Click" ToolTip="Print" Width="48" />
                            </td>
                            <td id="tdHelp" runat="server">
                                <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                                    OnClick="imgbtnHelp_Click" ToolTip="Help" Width="48" />
                            </td>
                            <td id="tdExit" runat="server">
                                <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                                    OnClick="imgbtnExit_Click" ToolTip="Exit" Width="48" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr class="TableHeader">
                <td align="center" valign="top" class="td">
                    <span class="titleheading">Purchase Voucher</span>
                </td>
            </tr>
            <tr>
                <td class="tdLeft td">
                    <span class="Mode">
                        <asp:Label ID="lblMode" runat="server"></asp:Label>
                    </span>
                </td>
            </tr>
            <tr>
                <td valign="top" align="center">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ValidationGroup="M1" />
                    <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label><strong>
                    </strong>
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label><strong>
                    </strong>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <table>
                        <tr>
                            <td class="TableHeader SmallFont tdLeft" colspan="6">
                                <span class="titleheading"><i>main details.... </i></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight">
                                Voucher Type :
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="ddlVoucherType" runat="server" CssClass="TextBox TextBoxDisplay"
                                    Width="130px" TabIndex="1" ReadOnly="True" OnTextChanged="txtVoucherNo_TextChanged"></asp:TextBox>
                            </td>
                            <td class="tdRight">
                                Voucher No.:
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtVoucherNo" runat="server" CssClass="TextBox TextBoxDisplay" Width="100px"
                                    TabIndex="2" ReadOnly="True" OnTextChanged="txtVoucherNo_TextChanged"></asp:TextBox>
                                <cc2:ComboBox ID="ddlVoucherNo" EmptyText="select Voucher No" runat="server" Width="150px"
                                    Height="250px" TabIndex="3" AutoPostBack="True" DataTextField="VCHR_NO" DataValueField="VCHR_NO"
                                    EnableLoadOnDemand="True" MenuWidth="300px" OnLoadingItems="ddlVoucherNo_LoadingItems"
                                    OnSelectedIndexChanged="ddlVoucherNo_SelectedIndexChanged">
                                    <HeaderTemplate>
                                        <div class="header c2">
                                            Voucher Type</div>
                                        <div class="header c4">
                                            Voucher No</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c2">
                                            <%# Eval("VCHR_NAME")%></div>
                                        <div class="item c4">
                                            <%# Eval("VCHR_NO")%></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td class="tdRight">
                                Date :
                            </td>
                            <td class="tdRight">
                                <asp:TextBox ID="txtJournalDate" runat="server" CssClass="TextBox" Width="100px"
                                    TabIndex="4"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <table>
                                    <tr>
                                        <td class="tdRight">
                                            Description :
                                        </td>
                                        <td class="tdLeft" colspan="3">
                                            <asp:TextBox ID="txtDescription" runat="server" CssClass="TextBox" Width="600px"
                                                MaxLength="200" TabIndex="5"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="TableHeader SmallFont tdLeft" colspan="6">
                                <span class="titleheading"><i>transaction details.... </i></span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="td" valign="top">
                    <table>
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0">
                                    <tr bgcolor="#336699" class="titleheading">
                                        <td colspan="2">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            Particulars
                                        </td>
                                        <td style="text-align: right">
                                            Debit
                                        </td>
                                        <td style="text-align: right">
                                            Credit
                                        </td>
                                        <td style="text-align: center">
                                            Doc No
                                        </td>
                                        <td style="text-align: center">
                                            Doc Date
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlEntry_Type" runat="server" Width="50px" TabIndex="6" CssClass="SmallFont"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlEntry_Type_SelectedIndexChanged">
                                                <asp:ListItem Value="Dr">Dr</asp:ListItem>
                                                <asp:ListItem Value="Cr">Cr</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlLedgerCode" runat="server" AppendDataBoundItems="true" Width="190px"
                                                DataTextField="LDGR_NAME" DataValueField="LDGR_CODE" TabIndex="7" CssClass="SmallFont"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlLedgerCode_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDebitAmount" runat="server" Width="80px" TabIndex="8" CssClass="TextBoxNo"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCreditAmount" runat="server" Width="80px" TabIndex="9" CssClass="TextBoxNo"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDocNo" runat="server" Width="80px" TabIndex="10" CssClass="gCtrTxt UpperCase"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDocDT" runat="server" Width="90px" TabIndex="11"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSaveDetail" runat="server" Style="top: 0px; left: -1px" Text="Save"
                                                Width="50px" OnClick="btnSaveDetail_Click" TabIndex="12" ValidationGroup="M1">
                                            </asp:Button>
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="60px" OnClick="btnCancel_Click"
                                                TabIndex="13"></asp:Button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdRight">
                                            Narration :
                                        </td>
                                        <td class="tdLeft" colspan="5">
                                            <asp:TextBox ID="txtTranDescription" runat="server" CssClass="TextBox" Width="550px"
                                                MaxLength="200" TabIndex="14"></asp:TextBox>
                                        </td>
                                        <td class="tdCentre">
                                            <asp:TextBox ID="txtLedgerPopUp" runat="server" Width="1px" Visible="false" AutoPostBack="true"
                                                BackColor="#C1D3FB" BorderStyle="None" OnTextChanged="txtLedgerPopUp_TextChanged"></asp:TextBox>
                                            <asp:Button ID="btnLedger" runat="server" Text="Add Ledger" OnClick="btnLedger_Click">
                                            </asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="grdJourenaldetails" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" CellSpacing="0" Width="730px" TabIndex="15" OnRowCommand="grdJourenaldetails_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Particulars">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" CssClass="LabelNo" runat="server" Text='<%# Bind("ENTRY_TYPE") %>'></asp:Label>
                                                <asp:Label ID="Label2" CssClass="LabelNo" runat="server" Text='<%# Bind("LEDGER_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="200px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Debit Amount" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                            <FooterTemplate>
                                                <asp:Label ID="lblDr_Amount_ftr" runat="server" CssClass="LabelNo"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("DR_AMOUNT") %>' CssClass="LabelNo"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Credit Amount" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                            <FooterTemplate>
                                                <asp:Label ID="lblCr_Amount_ftr" runat="server" CssClass="LabelNo" HeaderStyle-HorizontalAlign="Right"
                                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("CR_AMOUNT") %>' CssClass="LabelNo"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Doc No" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("DOC_NO") %>' CssClass="LabelNo"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Doc Date" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("DOC_DT", "{0:dd-MM-yyyy}") %>'
                                                    CssClass="LabelNo"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Narration" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("DESC") %>' CssClass="LabelNo"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnEdit" runat="server" CommandArgument='<%# bind("UNIQUE_ID") %>'
                                                    CommandName="EditTRN" Text="Edit" />
                                                <asp:Button ID="btnDelete" runat="server" CommandArgument='<%# bind("UNIQUE_ID") %>'
                                                    CommandName="DeleteTRN" Text="Delete" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle VerticalAlign="Top" />
                                    <HeaderStyle CssClass="HeaderStyle SmallFont titleheading" BackColor="#336699" />
                                </asp:GridView>
                                <asp:GridView ID="grdJourenaldetailsTax" runat="server" AutoGenerateColumns="False"
                                     CellSpacing="0" Width="730px" TabIndex="15" >
                                    <Columns>
                                        <asp:TemplateField >
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" CssClass="LabelNo" runat="server" Text='<%# Bind("ENTRY_TYPE") %>'></asp:Label>
                                                <asp:Label ID="Label2" CssClass="LabelNo" runat="server" Text='<%# Bind("LEDGER_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="200px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField  HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                            <FooterTemplate>
                                                <asp:Label ID="lblDr_Amount_ftr" runat="server" CssClass="LabelNo"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("DR_AMOUNT") %>' CssClass="LabelNo"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                            <FooterTemplate>
                                                <asp:Label ID="lblCr_Amount_ftr" runat="server" CssClass="LabelNo" HeaderStyle-HorizontalAlign="Right"
                                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label4" runat="server" Text="0" CssClass="LabelNo"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField  HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("DOC_NO") %>' CssClass="LabelNo"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField  HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("DOC_DT", "{0:dd-MM-yyyy}") %>'
                                                    CssClass="LabelNo"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField  HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("DESC") %>' CssClass="LabelNo"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       
                                    </Columns>
                                    <RowStyle VerticalAlign="Top" />
                                    
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr id="trOption" runat="server">
                            <td>
                                <asp:RadioButtonList ID="rdoLstOption" runat="server" RepeatDirection="Horizontal"
                                    AutoPostBack="true" OnSelectedIndexChanged="rdoLstOption_SelectedIndexChanged">
                                    <asp:ListItem>Bill Wise</asp:ListItem>
                                    <asp:ListItem>Advice Wise</asp:ListItem>
                                </asp:RadioButtonList>
                                <b>
                                    <asp:Label ID="lblNewMsg" runat="server" Text="New Voucher Amount for TDS Deduction is : "></asp:Label>
                                    <asp:TextBox ID="txtNewAmount" runat="server" BackColor="#AFCAE4" OnTextChanged="txtNewAmount_TextChanged"
                                        BorderStyle="None"></asp:TextBox></b>
                            </td>
                        </tr>
                        <tr id="trTDS" runat="server">
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblTDSLedgerCode" runat="server" Visible="false" CssClass="LabelNo"></asp:Label>
                                            <asp:Label ID="lblTDSText" runat="server" Visible="false" CssClass="LabelNo" Text="Tax Applicable On : "></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTDSLedgerName" runat="server" CssClass="LabelNo"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkTDS" runat="server" Text="Deduct TDS" Font-Bold="true" AutoPostBack="true"
                                                OnCheckedChanged="chkTDS_CheckedChanged" CssClass="LabelNo" TabIndex="16" />
                                        </td>
                                        <td>
                                            <cc2:ComboBox ID="ddlContractCode" runat="server" EnableLoadOnDemand="true" DataTextField="CONTRACT_CODE"
                                                DataValueField="CONTRACT_CODE" Height="200px" CssClass="SmallFont" EmptyText="Code"
                                                MenuWidth="650px" Width="70px" OnLoadingItems="ddlContractCode_LoadingItems"
                                                TabIndex="17">
                                                <HeaderTemplate>
                                                    <div class="header c2">
                                                        Contract Code</div>
                                                    <div class="header c3">
                                                        Description</div>
                                                    <div class="header c1">
                                                        Section</div>
                                                    <div class="header c2">
                                                        Start Date</div>
                                                    <div class="header c2">
                                                        End Date</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div class="item c2">
                                                        <asp:Literal runat="server" ID="Container2" Text='<%# Eval("CONTRACT_CODE") %>' /></div>
                                                    <div class="item c3">
                                                        <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("CONTRACT_DESC") %>' /></div>
                                                    <div class="item c1">
                                                        <asp:Literal runat="server" ID="Literal2" Text='<%# Eval("SECTION") %>' /></div>
                                                    <div class="item c1">
                                                        <asp:Literal runat="server" ID="Literal3" Text='<%# Eval("START_DATE", "{0:dd-MM-yyyy}") %>' /></div>
                                                    <div class="item c1">
                                                        <asp:Literal runat="server" ID="Literal4" Text='<%# Eval("END_DATE", "{0:dd-MM-yyyy}") %>' /></div>
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
                                            <asp:Button ID="btnTDS" runat="server" Text="Save TDS" TabIndex="18" OnClick="btnTDS_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="trTDSGrid" runat="server">
                            <td>
                                <asp:GridView ID="grdTDS" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                    CellSpacing="0" Width="548px" TabIndex="19">
                                    <Columns>
                                        <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Particulars">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" CssClass="LabelNo" runat="server" Text='<%# Bind("ENTRY_TYPE") %>'></asp:Label>
                                                <asp:Label ID="Label2" CssClass="LabelNo" runat="server" Text='<%# Bind("LEDGER_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="200px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Debit Amount" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                            <FooterTemplate>
                                                <asp:Label ID="lblDr_Amount_ftr" runat="server" CssClass="LabelNo"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("DR_AMOUNT") %>' CssClass="LabelNo"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Credit Amount" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                            <FooterTemplate>
                                                <asp:Label ID="lblCr_Amount_ftr" runat="server" CssClass="LabelNo"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("CR_AMOUNT") %>' CssClass="LabelNo"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tax In (%)" HeaderStyle-HorizontalAlign="Center">
                                            <FooterTemplate>
                                                <asp:Label ID="lblTax_ftr" runat="server" CssClass="LabelNo"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("TAX_PERCENT") %>' CssClass="LabelNo"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle VerticalAlign="Top" />
                                    <HeaderStyle CssClass="HeaderStyle SmallFont titleheading" BackColor="#336699" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr id="trClose" runat="server">
                            <td align="center">
                                <asp:Button ID="btnClose" runat="server" Text="Save & Close" OnClick="btnClose_Click" />
                                <asp:Button ID="btnCancelPopUp" runat="server" Text="Cancel" OnClick="btnCancelPopUp_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <cc4:CalendarExtender ID="ce1" runat="server" TargetControlID="txtJournalDate" PopupPosition="TopRight"
            Format="dd/MM/yyyy">
        </cc4:CalendarExtender>
        <cc4:CalendarExtender ID="ce2" runat="server" TargetControlID="txtDocDT" PopupPosition="TopRight"
            Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate">
        </cc4:CalendarExtender>
        <cc4:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtJournalDate" PromptCharacter="_">
        </cc4:MaskedEditExtender>
        <cc4:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtDocDT" PromptCharacter="_">
        </cc4:MaskedEditExtender>
<%--    </ContentTemplate>
</asp:UpdatePanel>
--%>