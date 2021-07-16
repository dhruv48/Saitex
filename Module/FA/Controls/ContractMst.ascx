<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContractMst.ascx.cs" Inherits="Module_FA_Controls_ContractMst" %>
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
        width: 100px;
    }
    .tContentArial
    {
        width: 491px;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <table class="tContentArial">
            <tr>
                <td class="td">
                    <table>
                        <tr>
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
                    <span class="titleheading">Contract Master</span>
                </td>
            </tr>
            <tr>
                <td class="tdLeft td">
                    <span class="Mode">You are in&nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
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
                                Contract Code :
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtContractCode" runat="server" CssClass="TextBox TextBoxDisplay"
                                    Width="100px" TabIndex="1" ReadOnly="True"></asp:TextBox>
                                <cc2:ComboBox ID="ddlContractCode" EmptyText="contract code" runat="server" Height="250px"
                                    TabIndex="2" AutoPostBack="True" DataTextField="CONTRACT_CODE" Width="100px"
                                    DataValueField="CONTRACT_CODE" EnableLoadOnDemand="True" MenuWidth="400px" OnLoadingItems="ddlContractCode_LoadingItems"
                                    OnSelectedIndexChanged="ddlContractCode_SelectedIndexChanged">
                                    <HeaderTemplate>
                                        <div class="header c2">
                                            Contract Code</div>
                                        <div class="header c2">
                                            Section</div>
                                        <div class="header c3">
                                            Description</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c2">
                                            <%# Eval("CONTRACT_CODE")%></div>
                                        <div class="item c3">
                                            <%# Eval("SECTION")%></div>
                                        <div class="item c3">
                                            <%# Eval("CONTRACT_DESC")%></div>
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
                                Section :
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtSection" runat="server" CssClass="TextBox gCtrTxt UpperCase"
                                    Width="100px" TabIndex="3"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight">
                                Start Date :
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtStartDate" runat="server" CssClass="TextBox" Width="100px" TabIndex="4"></asp:TextBox>
                            </td>
                            <td class="tdRight">
                                End Date :
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtEndDate" runat="server" CssClass="TextBox" Width="100px" TabIndex="5"></asp:TextBox>
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
                                            <asp:TextBox ID="txtDescription" runat="server" CssClass="TextBox" Width="400px"
                                                MaxLength="200" TabIndex="6" TextMode="MultiLine"></asp:TextBox>
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
                    <table style="width: 505px">
                        <tr>
                            <td>
                                <table>
                                    <tr bgcolor="#336699" class="titleheading">
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Tax Code&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Rate&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <cc2:ComboBox ID="ddlTaxCode" EmptyText="select tax.." runat="server" Height="250px"
                                                AutoPostBack="True" DataTextField="TAX_CODE" DataValueField="TAX_CODE" EnableLoadOnDemand="True"
                                                MenuWidth="200px" TabIndex="7" OnLoadingItems="ddlTaxCode_LoadingItems" OnSelectedIndexChanged="ddlTaxCode_SelectedIndexChanged">
                                                <HeaderTemplate>
                                                    <div class="header c1">
                                                        Tax Code
                                                    </div>
                                                    <div class="header c3">
                                                        Description
                                                    </div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div class="item c1">
                                                        <%# Eval("TAX_CODE")%></div>
                                                    <div class="item c3">
                                                        <%# Eval("TAX_DESC")%></div>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Displaying items
                                                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                    out of
                                                    <%# Container.ItemsCount %>.
                                                </FooterTemplate>
                                            </cc2:ComboBox>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="txtRate" runat="server" Width="80px" TabIndex="9" MaxLength="10"
                                                CssClass="TextBoxNo"></asp:TextBox>%
                                            <asp:RangeValidator ID="RVRate" runat="server" ValidationGroup="M1" Display="Dynamic"
                                                ControlToValidate="txtRate" MinimumValue="0" MaximumValue="99.99" ErrorMessage="Rate Value Should not exceeds 100% Or Please Enter Numeric"
                                                Type="Double" SetFocusOnError="true"></asp:RangeValidator>
                                        </td>
                                        <td align="center">
                                            <asp:Button ID="btnSaveDetail" runat="server" Style="top: 0px; left: -1px" Text="Save"
                                                Width="50px" TabIndex="10" ValidationGroup="M1" OnClick="btnSaveDetail_Click">
                                            </asp:Button>
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="60px" TabIndex="11"
                                                OnClick="btnCancel_Click"></asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="grdTaxdetails" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                    CellSpacing="0" OnRowCommand="grdTaxdetails_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Tax Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTaxCode" CssClass="LabelNo" runat="server" Text='<%# Bind("TAX_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTaxRate" runat="server" Text='<%# Bind("TAX_RATE") %>' CssClass="LabelNo"></asp:Label>
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
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <cc4:CalendarExtender ID="ce1" runat="server" TargetControlID="txtStartDate" PopupPosition="TopRight"
            Format="dd/MM/yyyy">
        </cc4:CalendarExtender>
        <cc4:CalendarExtender ID="ce2" runat="server" TargetControlID="txtEndDate" PopupPosition="TopRight"
            Format="dd/MM/yyyy">
        </cc4:CalendarExtender>
        <cc4:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtStartDate" PromptCharacter="_">
        </cc4:MaskedEditExtender>
        <cc4:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtEndDate" PromptCharacter="_">
        </cc4:MaskedEditExtender>
    </ContentTemplate>
</asp:UpdatePanel>
