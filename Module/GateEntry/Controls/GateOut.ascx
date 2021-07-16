<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GateOut.ascx.cs" Inherits="Module_GateEntry_Controls_GateOut" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Src="../../../CommonControls/LOV/TransporterCodeLOV.ascx" TagName="TransporterCodeLOV"
    TagPrefix="uc1" %>
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
        width: 80px;
    }
    .c2
    {
        margin-left: 4px;
        width: 120px;
    }
    .c3
    {
        margin-left: 4px;
        width: 400px;
    }
    .style1
    {
        height: 27px;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="left" class="tContentArial" width="800px">
            <tr>
                <td align="left" valign="top" class="td" width="100%">
                    <table align="left" cellpadding="0" cellspacing="0">
                        <tr>
                            <td id="tdSave" runat="server" align="center" valign="top">
                                <asp:ImageButton ID="imgbtnSave" runat="server" Height="41" ImageUrl="~/CommonImages/save.jpg"
                                    OnClick="imgbtnSave_Click" ToolTip="Save" ValidationGroup="GI" Width="48" OnClientClick="if (!confirm('Are you sure to Save the record ?')) { return false; }" />
                            </td>
                            <td id="tdUpdate" runat="server" align="center" valign="top">
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" Height="41" ImageUrl="~/CommonImages/edit1.jpg"
                                    OnClick="imgbtnUpdate_Click" ToolTip="Update" ValidationGroup="GI" Width="48"
                                    OnClientClick="if (!confirm('Are you sure to Update the record ?')) { return false; }" />
                            </td>
                            <td id="tdDelete" runat="server" align="center" valign="top">
                                <asp:ImageButton ID="imgbtnDelete" runat="server" Height="41" ImageUrl="~/CommonImages/del6.png"
                                    OnClick="imgbtnDelete_Click1" ToolTip="Delete" ValidationGroup="M1" Width="48"
                                    OnClientClick="if (!confirm('Are you sure to Delete the record ?')) { return false; }" />
                            </td>
                            <td align="center" valign="top">
                                <asp:ImageButton ID="imgbtnFind" runat="server" Height="41" ImageUrl="~/CommonImages/link_find.png"
                                    OnClick="imgbtnFind_Click" ToolTip="Find" Width="48" OnClientClick="if (!confirm('Are you sure to Find the record ?')) { return false; }" />
                            </td>
                            <td align="center" class="header" valign="top">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                                    OnClick="imgbtnPrint_Click" ToolTip="Print" Width="48" CausesValidation="false" OnClientClick="if (!confirm('Are you sure to Print the record ?')) { return false; }" />
                            </td>
                            <td align="center" valign="top">
                                <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                                    OnClick="imgbtnClear_Click" ToolTip="Clear" Width="48" CausesValidation="false"  OnClientClick="if (!confirm('Are you sure to Clear the record ?')) { return false; }" />
                            </td>
                            <td align="center" valign="top">
                                <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                                    OnClick="imgbtnExit_Click" ToolTip="Exit" Width="48"  CausesValidation="false"   OnClientClick="if (!confirm('Are you  sure Want to Exit?')) { return false; }" />
                            </td>
                            <td align="center" valign="top">
                                <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                                    OnClick="imgbtnHelp_Click" ToolTip="Help" Width="48" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="TableHeader  td" width="100%">
                    <b class="titleheading">
                        <asp:Label ID="lblHeading" runat="server" Text="Gate Out Entry Form"></asp:Label></b>
                </td>
            </tr>
            <tr>
                <td align="left" class="td" valign="top" width="100%">
                    <span style="color: #ff0000">
                        <asp:Label ID="lblMode" runat="server">
                        </asp:Label>
                    </span>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="GI" />
                </td>
            </tr>
            <tr>
                <td align="left" class="td" valign="top">
                    <table>
                        <tr style="font-weight: bold">
                            <td class="tdRight" width="17%">
                                Tran Type:
                            </td>
                            <td class="tdLeft" width="17%">
                                <asp:DropDownList ID="ddlTranType" runat="server" OnSelectedIndexChanged="ddlTranType_SelectedIndexChanged"
                                    AutoPostBack="True" TabIndex="1" Width="150px" MenuWidth="300" CssClass="SmallFont">
                                </asp:DropDownList>
                            </td>
                            <td class="tdRight" width="17%">
                                Gate Entry No:
                            </td>
                            <td class="tdLeft" width="17%">
                                <asp:TextBox ID="txtGateRunningNo" TabIndex="2" runat="server" MaxLength="15" ReadOnly="true"
                                    CssClass="SmallFont TextBoxDisplay TextBoxNo" Width="150px"></asp:TextBox>
                                <cc2:ComboBox ID="cmbGatedetails" TabIndex="3" runat="server" AutoPostBack="True"
                                    DataTextField="GATE_NUMB" DataValueField="GATE_NUMB" EmptyText="Select Gatetype"
                                    EnableLoadOnDemand="true" Height="200px" MenuWidth="800px" OnLoadingItems="cmbGatedetails_LoadingItems"
                                    OnSelectedIndexChanged="cmbGatedetails_SelectedIndexChanged" Visible="false"
                                    Width="130px">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            GATE NUMB</div>
                                        <div class="header c2">
                                            GATE DATE</div>
                                        <div class="header c2">
                                            PRTY CODE</div>
                                        <div class="header c3">
                                            PRTY NAME</div>
                                        <%--  <div class="header c3">
                                            TRAN TYPE</div>--%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal ID="Container4" runat="server" Text='<%# Eval("GATE_NUMB") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal ID="Container5" runat="server" Text='<%# Eval("GATE_DATE") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("PRTY_CODE") %>' />
                                        </div>
                                        <div class="item c3">
                                            <asp:Literal ID="Literal2" runat="server" Text='<%# Eval("PRTY_NAME") %>' />
                                        </div>
                                        <%-- <div class="item c3">
                                            <asp:Literal ID="Container6" runat="server" Text='<%# Eval("ITEM_TYPE") %>' />
                                        </div>--%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td class="tdRight" width="17%">
                                Gate Date:
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtGateDate" TabIndex="4" runat="server" CssClass="SmallFont TextBox" Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="17%">
                                Party Code:
                            </td>
                            <td class="tdLeft" width="17%">
                                <cc2:ComboBox ID="txtPartyCode" TabIndex="5" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    OnLoadingItems="txtPartyCode_LoadingItems" DataTextField="PRTY_CODE" DataValueField="Address"
                                    EmptyText="Select Vendor" OnSelectedIndexChanged="txtPartyCode_SelectedIndexChanged"
                                    EnableVirtualScrolling="true" Width="150px" MenuWidth="550px" Height="200px">
                                    <HeaderTemplate>
                                        <div class="header c2">
                                            Code</div>
                                        <div class="header c3">
                                            NAME</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Container6" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                        <div class="item c3">
                                            <asp:Literal runat="server" ID="Container7" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td class="tdRight" width="17%">
                                <asp:TextBox ID="lblPartyCode" TabIndex="7" runat="server" Width="98%" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True"></asp:TextBox>
                                &nbsp;
                            </td>
                            <td class="tdLeft" colspan="3" style="width: 49%">
                                <asp:TextBox ID="txtPartyName" TabIndex="6" runat="server" CssClass="SmallFont TextBox TextBoxDisplay"
                                    Width="98%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="17%">
                                Trasporter Code:
                            </td>
                            <td class="tdLeft" width="17%">
                                <cc2:ComboBox ID="txtTransporterCode" TabIndex="8" runat="server" AutoPostBack="True"
                                    EnableLoadOnDemand="true" EnableVirtualScrolling="true" OnLoadingItems="txtTransporterCode_LoadingItems"
                                    EmptyText="Select Transporter" DataTextField="PRTY_CODE" DataValueField="Address"
                                    OnSelectedIndexChanged="txtTransporterCode_SelectedIndexChanged" Width="150px"
                                    Height="200px" MenuWidth="450px">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Code</div>
                                        <div class="header c5">
                                            NAME</div>
                                        <div class="header c4">
                                            Address</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                        <div class="item c5">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                        <div class="item c4">
                                            <asp:Literal runat="server" ID="Container3" Text='<%# Eval("Address") %>' /></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td class="tdRight" width="17%">
                                <asp:TextBox ID="lblTransporterCode" TabIndex="10" runat="server" Width="98%" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                            <td class="tdLeft" colspan="3" style="width: 49%">
                                <asp:TextBox ID="txtTransporterAddress" TabIndex="9" runat="server" CssClass="SmallFont TextBox TextBoxDisplay"
                                    Width="98%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="17%">
                                <asp:Label ID="lblChallanNo" runat="server" Text="Challan No"  Font-Bold="true" Font-Size="Small" ></asp:Label>
                            </td>
                            <td class="tdLeft" width="17%">
                                <cc2:ComboBox ID="ddlRetunId" TabIndex="5" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    OnLoadingItems="ddlRetunId_LoadingItems" DataTextField="TRN_NUMB" DataValueField="TRN_DATA"
                                    EmptyText="Select return Id" EnableVirtualScrolling="true" Height="200px" MenuWidth="500px"
                                    OnSelectedIndexChanged="ddlRetunId_SelectedIndexChanged" Width="150px">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Return ID</div>
                                        <div class="header c2">
                                            Year</div>
                                        <div class="header c2">
                                            Type</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container8" Text='<%# Eval("TRN_NUMB") %>' /></div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Container9" Text='<%# Eval("YEAR") %>' /></div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Literal3" Text='<%# Eval("TRN_TYPE") %>' /></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td class="tdRight" width="17%">
                                Return Year/Type/No
                            </td>
                            <td class="tdLeft" width="17%">
                                <asp:TextBox ID="lblreturnYear" TabIndex="7" runat="server" Width="98%" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="17%">
                                <asp:TextBox ID="lblreturnType" TabIndex="7" runat="server" Width="98%" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="lblreturnNumb" TabIndex="7" runat="server" Width="98%" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="17%">
                                Doc No:
                            </td>
                            <td class="tdLeft" width="17%">
                                <asp:TextBox ID="txtDocNo" TabIndex="11" runat="server" CssClass="SmallFont TextBoxNo" Width="150px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="Please Enter Doc No"
                                    ControlToValidate="txtDocNo" Display="None" SetFocusOnError="True" ValidationGroup="GI"></asp:RequiredFieldValidator>
                            </td>
                            <td class="tdRight" width="17%">
                                Doc Date:
                            </td>
                            <td class="tdLeft" width="17%">
                                <asp:TextBox ID="txtDocDate" TabIndex="12" runat="server" CssClass="SmallFont TextBox" Width="150px"></asp:TextBox>
                                <cc1:CalendarExtender ID="ce2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDocDate"
                                    PopupPosition="TopLeft">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator ID="rfv2" runat="server" ErrorMessage="Please Enter or Select Doc Date"
                                    ControlToValidate="txtDocDate" Display="None" SetFocusOnError="True" ValidationGroup="GI"></asp:RequiredFieldValidator>
                                <cc1:MaskedEditExtender ID="mee1" runat="server" Mask="99/99/9999" MaskType="Date"
                                    PromptCharacter="_" TargetControlID="txtDocDate">
                                </cc1:MaskedEditExtender>
                            </td>
                            <td class="tdRight" width="17%">
                                Doc Amount:
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtDocAmount" TabIndex="13" runat="server" CssClass="SmallFont TextBoxNo"
                                    MaxLength="12" Width="150px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv3" runat="server" ErrorMessage="Please Select Doc Amount"
                                    ControlToValidate="txtDocAmount" Display="None" SetFocusOnError="True" ValidationGroup="GI"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="rv1" runat="server" ErrorMessage="Please Enter Doc Amount Numeric and Precision 9  Scale 2"
                                    MinimumValue="0" MaximumValue="999999999.99" ValidationGroup="GI" ControlToValidate="txtDocAmount"
                                    Type="Double" Display="None"></asp:RangeValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="17%">
                                Vehical No:
                            </td>
                            <td class="style1" width="17%">
                                <asp:TextBox ID="txtLorryno" TabIndex="14" runat="server" CssClass="SmallFont TextBox" Width="150px"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="17%">
                                Driver Name:
                            </td>
                            <td class="style1" width="17%">
                                <asp:TextBox ID="txtDriverName" TabIndex="15" runat="server" CssClass="SmallFont TextBox" Width="150px"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="17%">
                                UOM:
                            </td>
                            <td class="style1" width="15%">
                                <asp:DropDownList ID="ddlUOM" TabIndex="16" CssClass="SmallFont " runat="server"
                                    Width="150px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="17%">
                                No Of Items :
                            </td>
                            <td class="tdLeft" width="17%">
                                <asp:TextBox ID="txtQty" TabIndex="17" runat="server" CssClass="SmallFont TextBoxNo"
                                    MaxLength="13" Width="150px"></asp:TextBox>
                                <asp:RangeValidator ID="rv2" runat="server" ErrorMessage="Please Enter Quantity Numeric and Precision 9  Scale 3"
                                    MinimumValue="0" MaximumValue="999999999.999" ValidationGroup="GI" ControlToValidate="txtQty"
                                    Type="Double" Display="None"></asp:RangeValidator>
                                <asp:RequiredFieldValidator ID="rfv4" runat="server" ErrorMessage="Please Enter  Quantity"
                                    ControlToValidate="txtQty" Display="None" SetFocusOnError="True" ValidationGroup="GI"></asp:RequiredFieldValidator>
                            </td>
                            <td class="tdRight" width="17%">
                                Security Incharge:
                            </td>
                            <td class="tdLeft" width="17%">
                                <asp:TextBox ID="txtSecurityIncharge" TabIndex="18" runat="server" CssClass="SmallFont TextBox" Width="150px"> </asp:TextBox>
                            </td>
                            <td class="tdRight" width="17%">
                                Check By:
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtCheckBy" runat="server" TabIndex="19" CssClass="SmallFont TextBox " Width="150px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv5" runat="server" ErrorMessage="Please Enter Check By"
                                    ControlToValidate="txtCheckBy" Display="None" SetFocusOnError="True" ValidationGroup="GI"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="17%">
                                &nbsp;Material Detail:
                            </td>
                            <td class="tdLeft" colspan="5" width="83%">
                                <asp:TextBox ID="txtMaterialDetail" TabIndex="20" runat="server" Width="98%" MaxLength="100"
                                    CssClass="SmallFont TextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="17%">
                                &nbsp;Remarks:
                            </td>
                            <td class="tdLeft" colspan="5" width="83%">
                                <asp:TextBox ID="txtRemarks" runat="server" TabIndex="21" Width="98%" MaxLength="200"
                                    CssClass="SmallFont TextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr><td colspan="6"> <asp:Label ID="lblWorkOrder" Text="Work Order Out Side" runat="server" Font-Bold="true" Font-Size="Small" ></asp:Label></td></tr>
                        <tr>
                           
                           <td  colspan="6">
                          <%-- OnRowCommand="grdMaterialItemReceipt_RowCommand"
                    OnRowDataBound="grdMaterialItemReceipt_RowDataBound"--%>
                           <asp:Panel ID="pnlGrid" runat="server" Width="100%">
                <asp:GridView ID="grdMaterialItemReceipt" Width="99%" runat="server" AutoGenerateColumns="False"
                    CssClass="SmallFont" ShowFooter="false" >
                    <Columns>
                        <asp:TemplateField HeaderText="WO #">
                            <ItemTemplate>
                                <asp:Label ID="txtPONum" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("PO_NUMB") %>'
                                    Width="40px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Article Code">
                            <ItemTemplate>
                                <asp:Label ID="txtICODE" runat="server" CssClass="Label SmallFont" Text='<%# Bind("YARN_CODE") %>'
                                    Width="70px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                                <asp:Label ID="txtDESC" runat="server" CssClass="Label SmallFont" Text='<%# Bind("YARN_DESC") %>'
                                    Width="120px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Shade">
                            <ItemTemplate>
                                <asp:Label ID="txtSHADE_CODE" runat="server" CssClass="Label SmallFont" Text='<%# Bind("SHADE_CODE") %>'
                                    Width="120px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit">
                            <ItemTemplate>
                                <asp:Label ID="txtUNIT" runat="server" CssClass="Label SmallFont" Text='<%# Bind("UOM") %>'
                                    Width="50px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date of Mfg." Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="txtDOM" runat="server" CssClass="Label SmallFont" Text='<%# Bind("DATE_OF_MFG", "{0:dd-MMM-yyyy}") %>'
                                    Width="70px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Cartons">
                            <ItemTemplate>
                                <asp:Label ID="txtCartons" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("CARTONS") %>'
                                    Width="50px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:Label ID="txtQTY" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TRN_QTY") %>'
                                    Width="50px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="No Of Unit">
                            <ItemTemplate>
                                <asp:Label ID="lblnoofunit" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("NO_OF_UNIT") %>'
                                    Width="50px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="F. Rate">
                            <ItemTemplate>
                                <asp:Label ID="txtFinalRate" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("FINAL_RATE") %>'
                                    Width="50px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate>
                                <asp:Label ID="txtAmount" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("AMOUNT") %>'
                                    Width="70px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:Label ID="txtDetRemarks" runat="server" CssClass="Label SmallFont" Text='<%# Bind("REMARKS") %>'
                                    Width="120px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    <%--    <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="EditItem" Text="Edit"
                                    CommandArgument='<%# Bind("UNIQUEID") %>'></asp:LinkButton>/
                                <asp:LinkButton ID="lnkbtnDel" runat="server" CommandName="DelItem" Text="Delete"
                                    CommandArgument='<%# Bind("UNIQUEID") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    </Columns>
                    <RowStyle CssClass="SmallFont" />
                    <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                </asp:GridView>
                <asp:Label ID="lblSO_BRANCH" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblSO_TYPE" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblSO_COMP" runat="server" Visible="false"></asp:Label>
            </asp:Panel>
                             
                           </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>