<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustForLabDipNew.ascx.cs"
    Inherits="Module_OrderDevelopment_CustomerRequest_Controls_CustForLabDipNew" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc3" %>
<%@ Register Src="~/CommonControls/LOV/PartyCodeLOV.ascx" TagName="PartyCodeLOV"
    TagPrefix="uc1" %>
<%@ Register Src="../../../../CommonControls/LOV/ApproveLRLOV.ascx" TagName="ApproveLRLOV"
    TagPrefix="uc2" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="left" class="tContentArial">
            <tr>
                <td align="left" class="td" valign="top">
                    <table>
                        <tr>
                            <td id="tdSave" runat="server">
                                <asp:ImageButton ID="imgbtnSave" runat="server" ImageUrl="~/CommonImages/save.jpg"
                                    OnClick="imgbtnSave_Click1" ToolTip="Save" ValidationGroup="CR" />
                            </td>
                            <td id="tdUpdate" runat="server">
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" ImageUrl="~/CommonImages/edit1.jpg"
                                    OnClick="imgbtnUpdate_Click1" ToolTip="Update" ValidationGroup="CR" />
                            </td>
                            <td id="tdDelete" runat="server">
                                <asp:ImageButton ID="imgbtnDelete" runat="server" Height="41" ImageUrl="~/CommonImages/del6.png"
                                    OnClick="imgbtnDelete_Click1" OnClientClick="javascript:return window.confirm('Are you sure you want to delete this record')"
                                    TabIndex="6" ToolTip="Delete" ValidationGroup="M1" Width="48" />
                            </td>
                            <td id="tdFind" runat="server">
                                <asp:ImageButton ID="imgbtnFind" runat="server" ImageUrl="~/CommonImages/link_find.png"
                                    OnClick="imgbtnFind_Click1" ToolTip="Find" />
                            </td>
                            <td id="tdPrint" runat="server">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                                    OnClick="imgbtnPrint_Click" ToolTip="Print" />
                            </td>
                            <td id="tdClear" runat="server">
                                <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                    OnClick="imgbtnClear_Click1" ToolTip="Clear" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnExit" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                                    OnClick="imgbtnExit_Click1" ToolTip="Exit" />
                            </td>
                            <td style="font-style: italic">
                                <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                                    ToolTip="Help" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="TableHeader td">
                    <span class="titleheading"><b>Customer Request For Sewing Thread(Lab Dip)</b></span>
                </td>
            </tr>
            <tr>
                <td align="left" class="td" valign="top">
                    <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>
                        &nbsp;Mode
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="CR" />
                    </span>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="td">
                        <tr>
                            <td align="right" width="25%">
                                Request Cat:
                            </td>
                            <td align="left" width="15%">
                                <asp:DropDownList ID="ddlOrderCategory" runat="server" AutoPostBack="true" CssClass="SmallFont"
                                    OnSelectedIndexChanged="ddlOrderCategory_SelectedIndexChanged">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlOrderCategory"
                                    Display="None" ErrorMessage="Please Select Order Category" InitialValue="0" ValidationGroup="CR"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right" width="15%">
                                Request Type:
                            </td>
                            <td align="left" width="15%">
                                <asp:DropDownList ID="ddlOrderType" runat="server" AutoPostBack="True" CssClass="SmallFont"
                                    OnSelectedIndexChanged="ddlOrderType_SelectedIndexChanged">
                                    <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DDLOrderType"
                                    Display="None" ErrorMessage="Please Select Order Type " InitialValue="0" ValidationGroup="CR"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right" width="15%">
                                Customer Request No :&nbsp;
                            </td>
                            <td align="left" width="15%">
                                <asp:TextBox ID="txtOrderNo" runat="server" CssClass="TextBox TextBoxDisplay" ValidationGroup="M1"></asp:TextBox>
                                <asp:DropDownList ID="cmbOrderNo" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                    OnSelectedIndexChanged="cmbOrderNo_SelectedIndexChanged1" Width="150px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="25%">
                                Business Type :
                            </td>
                            <td align="left" width="15%">
                                <asp:DropDownList ID="ddlBusinessType" runat="server" AutoPostBack="true" CssClass="SmallFont"
                                    OnSelectedIndexChanged="ddlBusinessType_SelectedIndexChanged">
                                    <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DDLBusinessType"
                                    Display="None" ErrorMessage="Please Select Business Type" InitialValue="0" ValidationGroup="CR"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right" width="15%">
                                Order Date :
                            </td>
                            <td align="left" colspan="3" width="45%">
                                <asp:TextBox ID="txtDate" runat="server" CssClass="SmallFont" ValidationGroup="M1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="" width="25%">
                                Customer Name :
                            </td>
                            <td align="left" width="15%">
                                <uc1:PartyCodeLOV ID="cmbPartyCode" runat="server" />
                                <asp:TextBox ID="txtPartyCode" runat="server" CssClass="SmallFont TextBox TextBoxDisplay"
                                    ReadOnly="true" ValidationGroup="M1" Width="24%"></asp:TextBox>
                            </td>
                            <td align="right" width="15%">
                                Customer Reff No :
                            </td>
                            <td align="left" colspan="3" width="45%">
                                <asp:TextBox ID="txtCustomerReffNo" runat="server" CssClass="SmallFont" ValidationGroup="M1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="25%">
                                Customer Address :
                            </td>
                            <td align="left" colspan="5" cssclass="TextBox TextBoxDisplay" width="100%">
                                <asp:TextBox ID="txtAddress" runat="server" CssClass="SmallFont TextBox TextBoxDisplay"
                                    ReadOnly="true" ValidationGroup="M1" Width="99%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="25%">
                                Delivery Mode:
                            </td>
                            <td align="left" width="15%">
                                <asp:DropDownList ID="ddlDeliveryMode" runat="server" CssClass="SmallFont">
                                </asp:DropDownList>
                            </td>
                            <td align="right" width="15%">
                                Agent :
                            </td>
                            <td align="left" width="15%">
                                <asp:TextBox ID="txtAgent" runat="server" CssClass="SmallFont TextBox" ValidationGroup="M1"
                                    Width="99%"></asp:TextBox>
                            </td>
                            <td align="right" width="15%">
                                Direct Billing :
                            </td>
                            <td align="left" width="15%">
                                <asp:TextBox ID="txtDirectBilling" runat="server" CssClass="SmallFont TextBox" ValidationGroup="M1"
                                    Width="99%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="25%">
                                Remarks:
                            </td>
                            <td align="left" colspan="5">
                                <asp:TextBox ID="txtMstRemarks" runat="server" CssClass="SmallFon TextBoxt" ValidationGroup="M1"
                                    Width="99%"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td class="td">
                                <table width="100%">
                                    <tr bgcolor="#006699">
                                        <td class="tdLeft SmallFont">
                                            <span class="titleheading"><b>Article No</b></span>
                                        </td>
                                        <td class="tdLeft SmallFont">
                                            <span class="titleheading"><b>Make</b></span>
                                        </td>
                                        <td class="tdLeft SmallFont">
                                            <span class="titleheading"><b>Tkt No</b></span>
                                        </td>
                                        <td class="tdRight SmallFont">
                                            <span class="titleheading"><b>Shade Family</b></span>
                                        </td>
                                        <td class="tdRight SmallFont">
                                            <span class="titleheading"><b>Quanity</b></span>
                                        </td>
                                        <td class="tdLeft SmallFont">
                                            <span class="titleheading"><b>No Of Case/Box</b></span>
                                        </td>
                                        <td class="tdLeft SmallFont">
                                            <span class="titleheading"><b>End Use</b></span>
                                        </td>
                                        <td class="tdLeft SmallFont">
                                            <span class="titleheading"><b>Remarks</b></span>
                                        </td>
                                        <td class="tdLeft SmallFont">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:DropDownList ID="cmbArticleNo" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                                class="SmallFont" OnSelectedIndexChanged="cmbArticleNo_SelectedIndexChanged1">
                                            </asp:DropDownList>
                                            &nbsp;
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtMake" runat="server" CssClass="TextBoxDisplay SmallFont" ReadOnly="true"
                                                TabIndex="8" Width="70px"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtTktNo" runat="server" CssClass="TextBoxDisplay SmallFont" ReadOnly="true"
                                                TabIndex="8" Width="70px"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:DropDownList ID="ddlShade" runat="server" AppendDataBoundItems="True" CssClass="SmallFont">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtQuantity" runat="server" class="SmallFont" Width="50px"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtNoOfCaseBox" runat="server" class="SmallFont" TabIndex="8" Width="70px"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:DropDownList ID="txtEndUse" runat="server" AppendDataBoundItems="true">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtRemarks" runat="server" class="SmallFont" Width="50px"></asp:TextBox>
                                        </td>
                                        <td align="left" class="style1" style="width: 150px" valign="top">
                                            <asp:Button ID="btnSTSave" runat="server" OnClick="btnSTSave_Click" Text="Save" />
                                            <asp:Button ID="btnSTCancel" runat="server" OnClick="btnSTCancel_Click" Text="Cancel" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr id="tr4" runat="server">
                                        <td id="Td4" runat="server" align="left" class="td" width="100%">
                                            <asp:GridView ID="GridSpinningThread" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                CssClass="SmallFont" Font-Bold="False" OnRowCommand="GridSpinningThread_RowCommand"
                                                ShowFooter="True" Width="98%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Article No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtArticleNo" runat="server" Font-Bold="true" Text='<%# Bind("ARTICLE_NO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tkt No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtTktNo" runat="server" CssClass="Label SmallFont" Text='<%# Bind("TKT_NO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Make">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtMake" runat="server" CssClass="Label SmallFont" Text='<%# Bind("MAKE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Shade">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtShade" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("SHADE_NAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtQuantity" runat="server" AutoPostBack="True" CssClass="LabelNo SmallFont"
                                                                Font-Bold="true" Text='<%# Bind("QUANTITY") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No Of Case/Box">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtNoOfCones" runat="server" AutoPostBack="True" CssClass="LabelNo SmallFont"
                                                                Font-Bold="true" Text='<%# Bind("NO_Of_CASE_BOX") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="End Use.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtEndUse" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("END_USE_NAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtRemarks" runat="server" AutoPostBack="True" CssClass="LabelNo SmallFont"
                                                                Font-Bold="true" Text='<%# Bind("REMARKS") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Bind("UNIQUE_ID") %>'
                                                                CommandName="EditItem" Text="Edit" />
                                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Bind("UNIQUE_ID") %>'
                                                                CommandName="DelItem" Text="Delete" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle BackColor="#336699" CssClass="SmallFont titleheadingGrid" />
                                                <RowStyle CssClass="SmallFont" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
