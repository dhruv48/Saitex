<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChequeBookMst.ascx.cs"
    Inherits="Module_FA_Controls_ChequeBookMst" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>--%>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc1" %>
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
        width: 170px;
    }
    .c3
    {
        margin-left: 4px;
        width: 170px;
    }
    .c4
    {
        margin-left: 4px;
        width: 80px;
    }
    .c5
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
        <table align="left" class="tContentArial">
            <tr>
                <td>
                    <table width="100%" class="tContentArial" cellspacing="0" cellpadding="0" align="left">
                        <tbody>
                            <tr>
                                <td align="left" class="td" colspan="3">
                                    <table class="tContentArial" cellspacing="0" cellpadding="0" border="1">
                                        <tbody>
                                            <tr>
                                                <td id="tdSave" align="left" width="48" runat="server">
                                                    <asp:ImageButton ID="imgbtnSave" OnClick="imgbtnSave_Click" runat="server" ValidationGroup="M1"
                                                        ImageUrl="~/CommonImages/save.jpg" ToolTip="Save"></asp:ImageButton>
                                                </td>
                                                <td id="tdFind" runat="server" align="left" width="48">
                                                    <asp:ImageButton ID="imgbtnFind" OnClick="imgbtnFind_Click1" runat="server" ImageUrl="~/CommonImages/link_find.png"
                                                        ToolTip="Find" Height="41" Width="48"></asp:ImageButton>
                                                </td>
                                                <td id="tdUpdate" align="left" width="48" runat="server">
                                                    <asp:ImageButton ID="imgbtnUpdate" OnClick="imgbtnUpdate_Click" runat="server" ValidationGroup="M1"
                                                        ImageUrl="~/CommonImages/edit1.jpg" ToolTip="Update" Height="41" Width="48">
                                                    </asp:ImageButton>
                                                </td>
                                                <td id="tdDelete" align="left" width="48" runat="server">
                                                    <asp:ImageButton ID="imgbtnDelete" OnClick="imgbtnDelete_Click" runat="server" ImageUrl="~/CommonImages/del6.png"
                                                        ToolTip="Delete" Height="41" Width="48"></asp:ImageButton>
                                                </td>
                                                <td id="tdClear" runat="server" align="left" width="48">
                                                    <asp:ImageButton ID="imgbtnClear" OnClick="imgbtnClear_Click" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                                        ToolTip="Clear" Height="41" Width="48"></asp:ImageButton>
                                                </td>
                                                <td id="tdPrint" runat="server" align="left" width="48">
                                                    <asp:ImageButton ID="imgbtnPrint" OnClick="imgbtnPrint_Click" runat="server" ImageUrl="~/CommonImages/link_print.png"
                                                        ToolTip="Print" Height="41" Width="48"></asp:ImageButton>
                                                </td>
                                                <td id="tdExit" runat="server" align="left" width="48">
                                                    <asp:ImageButton ID="imgbtnExit" OnClick="imgbtnExit_Click" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                                                        ToolTip="Exit" Height="41" Width="48"></asp:ImageButton>
                                                </td>
                                                <td id="tdHelp" runat="server" align="left" width="48">
                                                    <asp:ImageButton ID="imgbtnHelp" OnClick="imgbtnHelp_Click" runat="server" ImageUrl="~/CommonImages/link_help.png"
                                                        ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableHeader td" align="center" colspan="3">
                                    <b class="titleheading">Cheque Book Master</b>
                                </td>
                            </tr>
                            <tr>
                                <td class="td" valign="top" align="left" colspan="3">
                                    <span class="Mode">You are in
                                        <asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode </span>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="center" colspan="3">
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
                                            <td>
                                                *Select Bank
                                            </td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td colspan="4">
                                                <cc1:ComboBox ID="cmbBankCode" runat="server" EnableLoadOnDemand="true" OnLoadingItems="cmbBankCode_LoadingItems"
                                                    CssClass="SmallFont" DataTextField="LGR_BANK_NAME" DataValueField="LGR_BANK_CODE"
                                                    EmptyText="Select Bank Account" Width="207px" MenuWidth="500px" Height="200px"
                                                    TabIndex="1" OnSelectedIndexChanged="cmbBankCode_SelectedIndexChanged">
                                                    <HeaderTemplate>
                                                        <div class="header c1">
                                                            Code</div>
                                                        <div class="header c2">
                                                            Bank Name</div>
                                                        <div class="header c3">
                                                            Account Number</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="item c1">
                                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("LGR_BANK_CODE") %>' /></div>
                                                        <div class="item c2">
                                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("LGR_BANK_NAME") %>' /></div>
                                                        <div class="item c3">
                                                            <asp:Literal runat="server" ID="Container3" Text='<%# Eval("BANK_AC_NO") %>' /></div>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        Displaying items
                                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                        out of
                                                        <%# Container.ItemsCount %>.
                                                    </FooterTemplate>
                                                </cc1:ComboBox>
                                                <asp:RequiredFieldValidator ID="RFBankCode" runat="server" ValidationGroup="M1" Display="dynamic"
                                                    ErrorMessage="Select Bank Code" ControlToValidate="cmbBankCode" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                *Cheque Book Code
                                            </td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtChequeBookCode" TabIndex="2" runat="server" ValidationGroup="M1"
                                                    CssClass="gCtrTxt UpperCase TextBoxDisplay" MaxLength="25" TextMode="singleLine"
                                                    ReadOnly="true" Width="200px"></asp:TextBox>
                                                <cc1:ComboBox ID="cmbChequeBookCode" runat="server" EnableLoadOnDemand="True" OnLoadingItems="cmbChequeBookCode_LoadingItems"
                                                    CssClass="SmallFont" DataTextField="CHEQUEBOOK_CODE" DataValueField="CHEQUEBOOK_CODE"
                                                    EmptyText="Select ChequeBook Code" Width="207px" MenuWidth="780px" Height="200px"
                                                    TabIndex="3" OnSelectedIndexChanged="cmbChequeBookCode_SelectedIndexChanged"
                                                    AutoPostBack="True">
                                                    <HeaderTemplate>
                                                        <div class="header c2">
                                                            Bank Name</div>
                                                        <div class="header c2">
                                                            Account No</div>
                                                        <div class="header c1">
                                                            Code</div>
                                                        <div class="header c2">
                                                            Cheque Book No</div>
                                                        <div class="header c4">
                                                            Start Leaf</div>
                                                        <div class="header c5">
                                                            End Leaf</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="item c2">
                                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("LGR_BANK_NAME") %>' /></div>
                                                        <div class="item c2">
                                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("BANK_AC_NO") %>' /></div>
                                                        <div class="item c1">
                                                            <asp:Literal runat="server" ID="Container3" Text='<%# Eval("CHEQUEBOOK_CODE") %>' /></div>
                                                        <div class="item c2">
                                                            <asp:Literal runat="server" ID="Container4" Text='<%# Eval("CHEQUEBOOK_NO") %>' /></div>
                                                        <div class="item c4">
                                                            <asp:Literal runat="server" ID="Container5" Text='<%# Eval("START_LEAF") %>' /></div>
                                                        <div class="item c5">
                                                            <asp:Literal runat="server" ID="Container6" Text='<%# Eval("END_LEAF") %>' /></div>
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
                                                *Cheque Book No
                                            </td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtChequeBookNo" TabIndex="4" runat="server" ValidationGroup="M1"
                                                    CssClass="gCtrTxt UpperCase" MaxLength="20" TextMode="singleLine" Width="200px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RFChequeBookNo" runat="server" ValidationGroup="M1"
                                                    Display="dynamic" ErrorMessage="Enter Cheque Book No" ControlToValidate="txtChequeBookNo"
                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                *Starting Leaf
                                            </td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtStartLeaf" TabIndex="5" runat="server" ValidationGroup="M1" CssClass="gCtrTxt UpperCase"
                                                    MaxLength="6" TextMode="singleLine" Width="200px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RFStartLeaf" runat="server" ValidationGroup="M1"
                                                    Display="dynamic" ErrorMessage="Enter Starting Leaf" ControlToValidate="txtStartLeaf"
                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                <asp:RangeValidator ID="RVStartLeaf" ControlToValidate="txtStartLeaf" runat="server"
                                                    ErrorMessage="Only numeric value allowed" Display="None" MaximumValue="1000000"
                                                    MinimumValue="0" Type="Integer" ValidationGroup="M1"></asp:RangeValidator>
                                            </td>
                                            <td>
                                                *No Of Cheque
                                            </td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNoOfCheque" TabIndex="6" runat="server" ValidationGroup="M1"
                                                    CssClass="gCtrTxt UpperCase" MaxLength="3" TextMode="singleLine" Width="200px"
                                                    AutoPostBack="True" OnTextChanged="txtNoOfCheque_TextChanged"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RFNoOfCheque" runat="server" ValidationGroup="M1"
                                                    Display="dynamic" ErrorMessage="Enter No Of Cheque" ControlToValidate="txtNoOfCheque"
                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                <asp:RangeValidator ID="RVNoOfCheque" ControlToValidate="txtNoOfCheque" runat="server"
                                                    ErrorMessage="Only numeric value allowed" Display="None" MaximumValue="1000000"
                                                    MinimumValue="0" Type="Integer" ValidationGroup="M1"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                *End Leaf
                                            </td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEndLeaf" TabIndex="7" runat="server" ValidationGroup="M1" CssClass="gCtrTxt UpperCase TextBoxDisplay"
                                                    MaxLength="6" TextMode="singleLine" Width="200px" ReadOnly="true"></asp:TextBox>
                                            </td>
                                            <td>
                                                *Issued On Date
                                            </td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtIssuedOn" TabIndex="8" runat="server" ValidationGroup="M1" CssClass="gCtrTxt UpperCase"
                                                    MaxLength="12" TextMode="singleLine" Width="200px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtIssuedOn" runat="server"
                                                    PopupPosition="TopLeft" Format="dd/MM/yyyy">
                                                </cc1:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="M1"
                                                    Display="dynamic" ErrorMessage="Enter Issued Date" ControlToValidate="txtIssuedOn"
                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Status
                                            </td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td colspan="5">
                                                <asp:CheckBox ID="chk_Status" TabIndex="14" runat="server"></asp:CheckBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                             <tr>
              
            <td>
              <table width="100%">
              <tr>
               <td valign="top">
                        <asp:GridView ID="grdChequeBook" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="False" BorderStyle="Ridge" CellPadding="3" CssClass="smallfont"
                            EmptyDataText="No Record Found" Font-Size="X-Small" ForeColor="#333333" OnPageIndexChanging="grdChequeBook_PageIndexChanging"
                            PagerStyle-HorizontalAlign="Left" PageSize="20" OnSelect="grdChequeBook_Select" Width="100%">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                            <Columns>
                                <asp:BoundField DataField="CHEQUEBOOK_CODE" HeaderText="Code" />
                                <asp:BoundField DataField="LGR_BANK_NAME" HeaderText="Bank Name" />
                                <asp:BoundField DataField="CHEQUEBOOK_NO" HeaderText="Cheque Book No" />
                                <asp:BoundField DataField="START_LEAF" HeaderText="Start Leaf" />
                                <asp:BoundField DataField="BANK_AC_NO" HeaderText="Account No" />
                                <asp:BoundField DataField="END_LEAF" HeaderText="End Leaf" />
                                 <asp:BoundField DataField="ISSUED_ON" HeaderText="Issued Date" />
                                 </Columns>
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </table> 
                   
                    </td>
              
            </tr>
                            <%--<tr>
                                <td class="td">
                                    <cc2:Grid ID="grdChequeBook" runat="server" AllowAddingRecords="False" AllowFiltering="True"
                                        PageSize="5" AutoGenerateColumns="False" TabIndex="9" OnSelect="grdChequeBook_Select">
                                        <Columns>
                                            <cc2:Column DataField="CHEQUEBOOK_CODE" Align="Left" HeaderText="Code" Width="40px"
                                                Visible="false">
                                            </cc2:Column>
                                            <cc2:Column DataField="LGR_BANK_NAME" Align="Left" HeaderText="Bank Name" Width="120px">
                                            </cc2:Column>
                                            <cc2:Column DataField="BANK_AC_NO" Align="Left" HeaderText="Account No" Width="128px">
                                            </cc2:Column>
                                            <cc2:Column DataField="CHEQUEBOOK_NO" Align="Left" HeaderText="Cheque Book No" Width="110px">
                                            </cc2:Column>
                                            <cc2:Column DataField="START_LEAF" Align="Left" HeaderText="Start Leaf" Width="105px">
                                            </cc2:Column>
                                            <cc2:Column DataField="END_LEAF" Align="Left" HeaderText="End Leaf" Width="105px">
                                            </cc2:Column>
                                            <cc2:Column DataField="ISSUED_ON" Align="Left" HeaderText="Issued Date" Width="115px">
                                            </cc2:Column>
                                        </Columns>
                                    </cc2:Grid>
                                </td>
                            </tr>--%>
                        </tbody>
                    </table>
                </td>
            </tr>
        </table>
   <%-- </ContentTemplate>
</asp:UpdatePanel>
--%>