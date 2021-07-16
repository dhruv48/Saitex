<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DebitNote.ascx.cs" Inherits="Module_Inventory_Controls_DebitNote" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
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
        width: 80px;
    }
    .c2
    {
        margin-left: 4px;
        width: 80px;
    }
    .c3
    {
        margin-left: 4px;
        width: 300px;
    }
    .c4
    {
        margin-left: 4px;
        width: 300px;
    }
    .c5
    {
        margin-left: 4px;
        width: 150px;
    }
</style>
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
        <table class="tContentArial" width="100%">
            <tr>
                <td class="td" width="100%">
                    <table class="tContentArial">
                        <tr>
                            <td id="tdSave" runat="server">
                                <asp:ImageButton ID="imgbtnSave" OnClick="imgbtnSave_Click" runat="server" ToolTip="Save"
                                    ImageUrl="~/CommonImages/save.jpg" ValidationGroup="M1"></asp:ImageButton>
                            </td>
                            <td id="tdUpdate" runat="server">
                                <asp:ImageButton ID="imgbtnUpdate" OnClick="imgbtnUpdate_Click" runat="server" ToolTip="Update"
                                    ImageUrl="~/CommonImages/edit1.jpg" ValidationGroup="M1"></asp:ImageButton>
                            </td>
                            <td id="tdDelete" runat="server">
                                <asp:ImageButton ID="imgbtnDelete" OnClick="imgbtnDelete_Click" runat="server" ToolTip="Delete"
                                    ImageUrl="~/CommonImages/del6.png"></asp:ImageButton>
                            </td>
                            <td id="tdFind" runat="server">
                                <asp:ImageButton ID="imgbtnFind" OnClick="imgbtnFind_Click" runat="server" ToolTip="Find"
                                    ImageUrl="~/CommonImages/link_find.png"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnClear" OnClick="imgbtnClear_Click" runat="server" ToolTip="Clear"
                                    ImageUrl="~/CommonImages/clear.jpg"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnPrint" OnClick="imgbtnPrint_Click" runat="server" ToolTip="Print"
                                    ImageUrl="~/CommonImages/link_print.png"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnExit" OnClick="imgbtnExit_Click" runat="server" ToolTip="Exit"
                                    ImageUrl="~/CommonImages/link_exit.png"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnHelp" OnClick="imgbtnHelp_Click" runat="server" ToolTip="Help"
                                    ImageUrl="~/CommonImages/link_help.png"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="TableHeader td" width="100%">
                    <b class="titleheading">
                        <asp:Label ID="lblHeading" runat="server"></asp:Label>
                    </b>
                </td>
            </tr>
            <tr>
                <td class="td tdLeft" width="100%">
                    <asp:ValidationSummary ID="VsMain" runat="server" ShowMessageBox="True" ValidationGroup="M1"
                        ShowSummary="False" />
                </td>
            </tr>
            <tr>
                <td class="td tdLeft" width="100%">
                    <span class="Mode">
                        <asp:Label ID="lblMode" runat="server"></asp:Label>
                    </span>
                </td>
            </tr>
            <tr>
                <td width="100%" class="td">
                    <table width="100%">
                        <tr>
                            <td class="tdRight" width="10%">
                                <asp:Label ID="lblNoteType" runat="server" Text="*Note Type :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <cc3:OboutDropDownList ID="ddlNoteType" runat="server" MenuWidth="200px" Width="130px"
                                    TabIndex="1">
                                </cc3:OboutDropDownList>
                            </td>
                            <td class="tdRight" width="10%">
                                <asp:Label ID="lblAdviceNo" runat="server" Text="*Advice Number :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtAdviceNo" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    MaxLength="15" TabIndex="2" ReadOnly="true"></asp:TextBox>
                                <cc2:ComboBox ID="cmbFindAdvice" runat="server" EmptyText="select..." Height="200px"
                                    AutoPostBack="True" EnableLoadOnDemand="true" MenuWidth="700px" Width="125px"
                                    TabIndex="3" OnLoadingItems="cmbFindAdvice_LoadingItems" OnSelectedIndexChanged="cmbFindAdvice_SelectedIndexChanged">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Year</div>
                                        <div class="header c1">
                                            Note Type</div>
                                        <div class="header c1">
                                            Advice #</div>
                                        <div class="header c4">
                                            Party</div>
                                        <div class="header c2">
                                            Amount</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("BILL_YEAR") %>' />
                                        </div>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container3" Text='<%# Eval("NOTE_TYPE") %>' />
                                        </div>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Literal3" Text='<%# Eval("ADVICE_NO") %>' />
                                        </div>
                                        <div class="item c4">
                                            <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("PARTY") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Literal2" Text='<%# Eval("ADVICE_AMT") %>' />
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td class="tdRight" width="10%">
                                <asp:Label ID="lblAdviceDate" runat="server" Text="*Advice Date :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtAdviceDate" runat="server" CssClass="TextBox SmallFont" TabIndex="4"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="10%">
                                <asp:Label ID="Label1" runat="server" Text="*Entry By :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <cc2:ComboBox ID="cmbEntryBy" runat="server" EmptyText="select..." Height="200px"
                                    MenuWidth="350px" Width="125px" TabIndex="3">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Emp Code</div>
                                        <div class="header c5">
                                            Name</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("EMP_CODE") %>' />
                                        </div>
                                        <div class="item c5">
                                            <asp:Literal runat="server" ID="Container3" Text='<%# Eval("EMPLOYEENAME") %>' />
                                        </div>
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
            <tr>
                <td width="100%" class="td">
                    <table width="100%">
                        <tr>
                            <td class="tdRight" width="10%">
                                <asp:Label ID="lblPartyCode" runat="server" Text="*Party Code :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <cc2:ComboBox ID="ddlParty" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    OnLoadingItems="ddlParty_LoadingItems" DataTextField="PRTY_CODE" DataValueField="Address"
                                    EmptyText="Select Party" OnSelectedIndexChanged="ddlParty_SelectedIndexChanged"
                                    EnableVirtualScrolling="true" Width="150px" MenuWidth="400px" Height="200px">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Code</div>
                                        <div class="header c3">
                                            NAME</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                        <div class="item c3">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtPartyCode" TabIndex="4" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True" Width="100px"></asp:TextBox>
                            </td>
                            <td class="tdLeft" width="60%">
                                <asp:TextBox ID="txtPartyDetail" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    Width="98%" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="100%" class="td">
                    <table width="100%">
                        <tr>
                            <td class="tdRight" width="10%">
                                <asp:Label ID="lblAdviceAmount" runat="server" Text="*Advice Amount :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtAdviceAmount" runat="server" CssClass="TextBoxNo SmallFont" Enabled="true"
                                    AutoPostBack="True" MaxLength="10" TabIndex="6" OnTextChanged="txtAdviceAmount_TextChanged"></asp:TextBox>
                            </td>
                            <td class="tdLeft" width="40%">
                                <asp:TextBox ID="txtPartyBillAmtInWords" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    Width="98%" ReadOnly="True" Font-Size="XX-Small" TabIndex="7"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="10%">
                                <asp:Label ID="lblCategory" runat="server" Text="*Note Category :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="25%">
                                <cc3:OboutDropDownList ID="ddlCategory" AppendDataBoundItems="true" runat="server"
                                    MenuWidth="200px" TabIndex="8">
                                </cc3:OboutDropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="100%" class="td">
                    <table width="100%">
                        <tr>
                            <td class="tdRight" width="10%">
                                <asp:Label ID="lblRemarks" runat="server" Text="Remarks :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="90%">
                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="TextBox SmallFont" Width="98%"
                                    MaxLength="200" TabIndex="9"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="100%" class="td">
                    <table width="98%">
                        <tr bgcolor="#336699" class="SmallFont titleheading">
                            <td>
                            </td>
                            <td>
                                Year
                            </td>
                            <td>
                                TRN Type
                            </td>
                            <td>
                                TRN Number
                            </td>
                            <td>
                                TRN Amount
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cc2:ComboBox ID="ddlTRNDetail" runat="server" CssClass="SmallFont" EmptyText="select..."
                                    AutoPostBack="True" EnableLoadOnDemand="true" MenuWidth="580px" Width="80px"
                                    Height="200px" TabIndex="10" OnLoadingItems="ddlTRNDetail_LoadingItems" OnSelectedIndexChanged="ddlTRNDetail_SelectedIndexChanged">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Year</div>
                                        <div class="header c1">
                                            Trn Type</div>
                                        <div class="header c1">
                                            Trn #</div>
                                        <div class="header c2">
                                            Amount</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("YEAR") %>' />
                                        </div>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container3" Text='<%# Eval("TRN_TYPE") %>' />
                                        </div>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("TRN_NUMB") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Literal2" Text='<%# Eval("AMOUNT") %>' />
                                        </div>
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
                                <asp:TextBox ID="txtTRNYear" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="63px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTRNType" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="63px"></asp:TextBox>
                            </td>
                            <td style="font-weight: 700">
                                <asp:TextBox ID="txtTRNNo" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="41px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTRNAmount" runat="server" CssClass="TextBoxNo SmallFont" Width="84px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnsaveTRNDetail" runat="server" CssClass="SmallFont" Text="Save"
                                    ValidationGroup="T1" TabIndex="11" Width="55px" OnClick="btnsaveTRNDetail_Click" />
                                <asp:Button ID="btnTRNCancel" runat="server" CssClass="SmallFont" Text="Cancel" TabIndex="12"
                                    OnClick="btnTRNCancel_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="100%" class="td">
                    <asp:Panel ID="pnlGrid" runat="server" Width="100%">
                        <asp:GridView ID="grdBillTRNDetail" runat="server" AutoGenerateColumns="False" CssClass="SmallFont"
                            Width="99%" ShowFooter="false" OnRowCommand="grdBillTRNDetail_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Year">
                                    <ItemTemplate>
                                        <asp:Label ID="txtYear" runat="server" CssClass="Label SmallFont" Text='<%# Bind("YEAR") %>'
                                            ReadOnly="True" Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TRN Type">
                                    <ItemTemplate>
                                        <asp:Label ID="txtTRN_Type" runat="server" CssClass="Label SmallFont" Text='<%# Bind("TRN_TYPE") %>'
                                            ReadOnly="True" Width="120px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TRN Number">
                                    <ItemTemplate>
                                        <asp:Label ID="txtTRN_NUMB" runat="server" CssClass="Label SmallFont" Text='<%# Bind("TRN_NUMB") %>'
                                            ReadOnly="True" Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TRN Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="txtTRN_Amount" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TRN_AMT") %>'
                                            ReadOnly="True" Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="EditTRNDEtail" Text="Edit"
                                            CommandArgument='<%# Bind("UNIQUE_ID") %>'></asp:LinkButton>
                                        /
                                        <asp:LinkButton ID="lnkbtnDel" runat="server" CommandName="DelTRNDetail" Text="Delete"
                                            CommandArgument='<%# Bind("UNIQUE_ID") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle CssClass="SmallFont" />
                            <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <cc1:CalendarExtender ID="CEAdviceDate" runat="server" TargetControlID="txtAdviceDate"
            PopupPosition="TopLeft">
        </cc1:CalendarExtender>
        <asp:RequiredFieldValidator ID="RFAdviceNo" runat="server" ErrorMessage="Please enter Advice Number first.."
            ControlToValidate="txtAdviceNo" ValidationGroup="M1" Display="None"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RFAdviceAmount" runat="server" ErrorMessage="Please enter Advice Amount.."
            ControlToValidate="txtAdviceAmount" ValidationGroup="M1" Display="None"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RFAdviceDate" runat="server" ErrorMessage="Please enter Advice Date.."
            ControlToValidate="txtAdviceDate" ValidationGroup="M1" Display="None"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RVAdviceAmount" runat="server" ValidationGroup="M1" Display="None"
            ControlToValidate="txtAdviceAmount" MinimumValue="0" MaximumValue="999999999"
            ErrorMessage="Please Enter Numeric !" Type="Double" SetFocusOnError="true"></asp:RangeValidator>
   <%-- </ContentTemplate>
</asp:UpdatePanel>--%>
