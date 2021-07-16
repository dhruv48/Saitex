<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Apparel_Fabric_GateIn.ascx.cs" Inherits="Module_GateEntry_Controls_Apparel_Fabric_GateIn" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Src="../../../CommonControls/LOV/TransporterCodeLOV.ascx" TagName="TransporterCodeLOV"
    TagPrefix="uc1" %>
    
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
      
       display:inline;overflow:hidden;white-space:nowrap;}
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
</style>
<link href="../../../StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
<link href="../../../StyleSheet/style.css" rel="stylesheet" type="text/css" />


<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
        <table align="left" border="0"  width="100%" cellpadding="0" cellspacing="0" class="tContentArial">
            <tr>
                <td align="left" valign="top" class="td">
                    <table align="left"  cellpadding="0" cellspacing="0">
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
                          
                            <td align="center" valign="top">
                                <asp:ImageButton ID="imgbtnFind" runat="server" Height="41" ImageUrl="~/CommonImages/link_find.png"
                                    OnClick="imgbtnFind_Click" ToolTip="Find" Width="48" OnClientClick="if (!confirm('Are you sure to Find the record ?')) { return false; }" />
                            </td>
                            <td align="center" class="header" valign="top">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                                    OnClick="imgbtnPrint_Click" ToolTip="Print" Width="48" OnClientClick="if (!confirm('Are you sure to Print the record ?')) { return false; }" />
                            </td>
                            <td align="center" valign="top">
                                <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                                    OnClick="imgbtnClear_Click" ToolTip="Clear" Width="48" OnClientClick="if (!confirm('Are you sure to Clear the record ?')) { return false; }" />
                            </td>
                            <td align="center" valign="top">
                                <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                                    OnClick="imgbtnExit_Click" ToolTip="Exit" Width="48" OnClientClick="if (!confirm('Are you  sure Want to Exit?')) { return false; }" />
                            </td>
                          
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="TableHeader  td" width="100%">
                    <b class="titleheading">
                        <asp:Label ID="lblHeading" runat="server" Text=""></asp:Label></b>
                </td>
            </tr>
            <tr>
                <td align="left" class="td" valign="top" width="100%">
                    <span style="color: #ff0000">You are in &nbsp;<asp:Label ID="lblMode" runat="server">
                    </asp:Label>
                        &nbsp;Mode</span>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="GI" />
                </td>
            </tr>
            <tr>
                <td align="left" class="td" valign="top" width="100%">
                    <table width="100%">
                        <tr style="font-weight: bold" >
                            <td class="tdRight">
                                Tran Type:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlTranType" CssClass="SmallFont" runat="server" OnSelectedIndexChanged="ddlTranType_SelectedIndexChanged"
                                    AutoPostBack="True" TabIndex="1" Width="160px" MenuWidth="300" Enabled="False">
                                </asp:DropDownList>
                            </td>
                            <td class="tdRight">
                                Gate Entry No:
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtGateRunningNo" TabIndex="2" runat="server" MaxLength="15" ReadOnly="true"
                                    CssClass="SmallFont TextBoxDisplay TextBoxNo"></asp:TextBox>
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
                            <td class=" tdRight">
                                Gate Date:
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtGateDate" TabIndex="4" runat="server" CssClass="SmallFont TextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr >
                            <td  class="tdRight">
                                Party Code:
                            </td>
                            <td>
                                <cc2:ComboBox ID="txtPartyCode" TabIndex="5" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    OnLoadingItems="txtPartyCode_LoadingItems" DataTextField="PRTY_CODE" DataValueField="Address"
                                    EmptyText="Select Vendor" OnSelectedIndexChanged="txtPartyCode_SelectedIndexChanged"
                                    EnableVirtualScrolling="true" Width="68%" MenuWidth="550px" Height="200px">
                                    <HeaderTemplate>
                                        <div class="header c2">
                                            Code</div>
                                        <div class="header c3">
                                            NAME</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c2">
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
                            <td class="tdRight">
                                Party Name:
                            </td>
                            <td class="tdLeft" colspan="3">
                                <asp:TextBox ID="lblPartyCode" TabIndex="7" runat="server" Width="45px" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True"></asp:TextBox>
                                <asp:TextBox ID="txtPartyName" TabIndex="6" runat="server" CssClass="SmallFont TextBox TextBoxDisplay"
                                    Width="75%"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Please Select Party Code"
                                    ControlToValidate="lblPartyCode" Display="None" SetFocusOnError="True" ValidationGroup="GI"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td  class="tdRight">
                                Trasporter
                                <br />
                                Code:
                            </td>
                            <td>
                                <cc2:ComboBox ID="txtTransporterCode" TabIndex="8" runat="server" AutoPostBack="True"
                                    EnableLoadOnDemand="true" EnableVirtualScrolling="true" OnLoadingItems="txtTransporterCode_LoadingItems"
                                    EmptyText="Select Transporter" DataTextField="PRTY_CODE" DataValueField="Address"
                                    OnSelectedIndexChanged="txtTransporterCode_SelectedIndexChanged" Width="68%"
                                    Height="200px" MenuWidth="700px">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Code</div>
                                        <div class="header c2">
                                            NAME</div>
                                        <div class="header c3">
                                            Address</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                        <div class="item c3">
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
                            <td class="tdRight">
                                Transporter<br />
                                Name:</td>
                            <td class="tdLeft" colspan="3">
                                <asp:TextBox ID="lblTransporterCode" TabIndex="10" runat="server" Width="45px" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True"></asp:TextBox>
                                <asp:TextBox ID="txtTransporterAddress" TabIndex="9" runat="server" CssClass="SmallFont TextBox TextBoxDisplay"
                                    Width="75%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr >
                            <td  class="tdRight">
                                Doc No:
                            </td>
                            <td>
                                <asp:TextBox ID="txtDocNo" TabIndex="11" runat="server" CssClass="SmallFont TextBoxNo"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Doc No"
                                    ControlToValidate="txtDocNo" Display="None" SetFocusOnError="True" ValidationGroup="GI"></asp:RequiredFieldValidator>
                            </td>
                            <td class="tdRight">
                                Doc Date:
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtDocDate" TabIndex="12" runat="server" CssClass="SmallFont TextBox"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDocDate"
                                    PopupPosition="TopLeft">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter or Select Doc Date"
                                    ControlToValidate="txtDocDate" Display="None" SetFocusOnError="True" ValidationGroup="GI"></asp:RequiredFieldValidator>
                                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                    MaskType="Date" PromptCharacter="_" TargetControlID="txtDocDate">
                                </cc1:MaskedEditExtender>
                            </td>
                            <td class=" tdRight">
                                Doc Amount:
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtDocAmount" TabIndex="13" runat="server" CssClass="SmallFont TextBoxNo"
                                    MaxLength="12"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Select Doc Amount"
                                    ControlToValidate="txtDocAmount" Display="None" SetFocusOnError="True" ValidationGroup="GI"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Please Enter Doc Amount Numeric and Precision 9  Scale 2"
                                    MinimumValue="0" MaximumValue="999999999.99" ValidationGroup="GI" ControlToValidate="txtDocAmount"
                                    Type="Double" Display="None"></asp:RangeValidator>
                            </td>
                        </tr>
                        <tr >
                            <td  class="tdRight">
                                Lorry No:
                            </td>
                            <td>
                                <asp:TextBox ID="txtLorryno" TabIndex="14" runat="server" CssClass="SmallFont TextBox"></asp:TextBox>
                            </td>
                            <td class="tdRight">
                                Driver Name:
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtDriverName" TabIndex="15" runat="server" CssClass="SmallFont TextBox"></asp:TextBox>
                            </td>
                            <td class=" tdRight">
                                UOM:
                            </td>
                            <td class="tdLeft">
                                <asp:DropDownList ID="ddlUOM" TabIndex="16" CssClass="SmallFont " runat="server"
                                    Width="130px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr >
                            <td  class="tdRight">
                                No Of Items :
                            </td>
                            <td>
                                <asp:TextBox ID="txtQty" TabIndex="17" runat="server" CssClass="SmallFont TextBoxNo"
                                    MaxLength="13"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="Please Enter Quantity Numeric and Precision 9  Scale 3"
                                    MinimumValue="0" MaximumValue="999999999.999" ValidationGroup="GI" ControlToValidate="txtQty"
                                    Type="Double" Display="None"></asp:RangeValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Enter  Quantity"
                                    ControlToValidate="txtQty" Display="None" SetFocusOnError="True" ValidationGroup="GI"></asp:RequiredFieldValidator>
                            </td>
                            <td class="tdRight">
                                Security Incharge:
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtSecurityIncharge" TabIndex="18" runat="server" CssClass="SmallFont TextBox"> </asp:TextBox>
                            </td>
                            <td class=" tdRight">
                                Check By:
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtCheckBy" runat="server" TabIndex="19" CssClass="SmallFont TextBox "></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Enter Check By"
                                    ControlToValidate="txtCheckBy" Display="None" SetFocusOnError="True" ValidationGroup="GI"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </td>
               </tr> 
                <tr >
                    <td align="left" class="td" valign="top" width="100%">
                        <table width="100%">
                            <tr >
                                <td class="tdRight" width="10%">
                                    Fabric Detail:
                                </td>
                                <td class="tdLeft" width="90%">
                                    <asp:TextBox ID="txtMaterialDetail" TabIndex="20" runat="server" Width="92%" MaxLength="100"
                                        CssClass="SmallFont TextBox"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator7"
                                            runat="server" ErrorMessage="Please Enter Material Detail" ControlToValidate="txtMaterialDetail"
                                            Display="None" SetFocusOnError="True" ValidationGroup="GI"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr >
                                <td class="tdRight" width="10%">
                                    Remarks:
                                </td>
                                <td class="tdLeft"  width="90%">
                                    <asp:TextBox ID="txtRemarks" runat="server" TabIndex="21" Width="92%" MaxLength="200"
                                        CssClass="SmallFont TextBox"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator8"
                                            runat="server" ErrorMessage="Please Enter Remakrs" ControlToValidate="txtRemarks"
                                            Display="None" SetFocusOnError="True" ValidationGroup="GI"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                        </td>
                </tr>
           
        </table>
<%--</ContentTemplate>
</asp:UpdatePanel>
--%>