<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MaterailGateEntry.ascx.cs" Inherits="Module_GateEntry_Controls_MaterailGateEntry" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
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
        width: 150px;
    }
    .c3
    {
        margin-left: 4px;
        width: 80px;
    }
</style>

<script type="text/javascript" language="javascript">
   
    
    function Calculation(val)
    { 
     var name=val;                                                               
     document.getElementById('ctl00_cphBody_MaterailGateEntry1_txtFinalRate').value=(parseFloat(document.getElementById('ctl00_cphBody_MaterailGateEntry1_txtRecievedQuantity').value)*(parseFloat(document.getElementById('ctl00_cphBody_MaterailGateEntry1_txtBasicRate').value))).toFixed(3) ;
    }
   
</script>

<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate >--%>
<table align="left" border="0" cellpadding="0" cellspacing="0" class="tContentArial">
    <tr>
        <td align="left" valign="top" class="td">
            <table align="left" cellpadding="0" cellspacing="0">
                <tr>
                    <td id="tdSave" runat="server" align="center" valign="top">
                        <asp:ImageButton ID="imgbtnSave" runat="server" Height="41" ImageUrl="~/CommonImages/save.jpg"
                            OnClick="imgbtnSave_Click" TabIndex="9" ToolTip="Save" ValidationGroup="GI" Width="48"
                            OnClientClick="if (!confirm('Are you sure to Save the record ?')) { return false; }" />
                    </td>
                    <td id="tdUpdate" runat="server" align="center" valign="top">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" Height="41" ImageUrl="~/CommonImages/edit1.jpg"
                            OnClick="imgbtnUpdate_Click" TabIndex="9" ToolTip="Update" ValidationGroup="GI"
                            Width="48" OnClientClick="if (!confirm('Are you sure to Update the record ?')) { return false; }" />
                    </td>
                    <td id="tdDelete" runat="server" align="center" valign="top">
                        <asp:ImageButton ID="imgbtnDelete" runat="server" Height="41" ImageUrl="~/CommonImages/del6.png"
                            OnClick="imgbtnDelete_Click1" TabIndex="9" ToolTip="Delete" ValidationGroup="M1"
                            Width="48" OnClientClick="if (!confirm('Are you sure to Delete the record ?')) { return false; }" />
                    </td>
                    <td align="center" valign="top">
                        <asp:ImageButton ID="imgbtnFind" runat="server" Height="41" ImageUrl="~/CommonImages/link_find.png"
                            OnClick="imgbtnFind_Click" TabIndex="9" ToolTip="Find" Width="48" OnClientClick="if (!confirm('Are you sure to Find the record ?')) { return false; }" />
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
                            OnClick="imgbtnExit_Click" ToolTip="Exit" Width="48" OnClientClick="if (!confirm('Are you sure to Exit?')) { return false; }" />
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
            <table>
                <tr>
                    <td class="  tdRight">
                        Tran Type:
                    </td>
                    <td class=" ">
                        <cc3:OboutDropDownList ID="ddlTranType" runat="server" OnSelectedIndexChanged="ddlTranType_SelectedIndexChanged"
                            AutoPostBack="True" Width="150px" MenuWidth="300" Enabled="False">
                        </cc3:OboutDropDownList>
                    </td>
                    <td class="tdRight">
                        Gate Entry No:
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="txtGateRunningNo" runat="server" ReadOnly="True" MaxLength="15"
                            CssClass="SmallFont TextBoxNo TextBoxDisplay"></asp:TextBox>
                        <cc2:ComboBox ID="cmbGatedetails" runat="server" AutoPostBack="True" DataTextField="GATE_NUMB"
                            DataValueField="GATE_NUMB" EmptyText="Select Gatetype" EnableLoadOnDemand="true"
                            Height="200px" MenuWidth="750px" OnLoadingItems="cmbGatedetails_LoadingItems"
                            OnSelectedIndexChanged="cmbGatedetails_SelectedIndexChanged" Visible="false"
                            Width="130px">
                            <HeaderTemplate>
                                <div class="header c1">
                                    GATE NUMB</div>
                                <div class="header c2">
                                    GATE DATE</div>
                                <div class="header c3">
                                    TRAN TYPE</div>
                                <div class="header c3">
                                    PRTY CODE</div>
                                <div class="header c3">
                                    PRTY NAME</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal ID="Container4" runat="server" Text='<%# Eval("GATE_NUMB") %>' />
                                </div>
                                <div class="item c2">
                                    <asp:Literal ID="Container5" runat="server" Text='<%# Eval("GATE_DATE") %>' />
                                </div>
                                <div class="item c3">
                                    <asp:Literal ID="Container6" runat="server" Text='<%# Eval("ITEM_TYPE") %>' />
                                </div>
                                <div class="item c3">
                                    <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("PRTY_CODE") %>' />
                                </div>
                                <div class="item c3">
                                    <asp:Literal ID="Literal2" runat="server" Text='<%# Eval("PRTY_NAME") %>' />
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
                    <td class="style4 tdRight">
                        Gate Date:
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="txtGateDate" runat="server" ReadOnly="True" CssClass="SmallFont TextBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="  tdRight">
                        Party Code:
                    </td>
                    <td class=" ">
                        <cc2:ComboBox ID="txtPartyCode" runat="server" AutoPostBack="True" DataTextField="PRTY_CODE"
                            DataValueField="Address" EmptyText="Select Party" EnableLoadOnDemand="true" Height="200px"
                            MenuWidth="600px" OnLoadingItems="txtPartyCode_LoadingItems" OnSelectedIndexChanged="txtPartyCode_SelectedIndexChanged"
                            Width="150px">
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
                                    <asp:Literal ID="Container1" runat="server" Text='<%# Eval("PRTY_CODE") %>' />
                                </div>
                                <div class="item c2">
                                    <asp:Literal ID="Container2" runat="server" Text='<%# Eval("PRTY_NAME") %>' />
                                </div>
                                <div class="item c3">
                                    <asp:Literal ID="Container3" runat="server" Text='<%# Eval("Address") %>' />
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
                    <td class="tdRight">
                        Party Name:
                    </td>
                    <td class="tdLeft" colspan="3">
                        <asp:TextBox ID="txtPartyName" runat="server" CssClass="SmallFont TextBox TextBoxDisplay"
                            Width="98%"></asp:TextBox>
                        <%-- <asp:TextBox ID="TextBox1" runat="server" Width="389px"></asp:TextBox>--%>
                    </td>
                </tr>
                <tr>
                    <td class="  tdRight">
                        Doc No:
                    </td>
                    <td>
                        <asp:TextBox ID="txtDocNo" runat="server" CssClass="SmallFont TextBoxNo"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Doc No"
                            ControlToValidate="txtDocNo" Display="None" SetFocusOnError="True" ValidationGroup="GI"></asp:RequiredFieldValidator>
                    </td>
                    <td class="tdRight">
                        Doc Date:
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="txtDocDate" runat="server" CssClass="SmallFont TextBox"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDocDate"
                            PopupPosition="TopLeft" Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter or Select Doc Date"
                            ControlToValidate="txtDocDate" Display="None" SetFocusOnError="True" ValidationGroup="GI"></asp:RequiredFieldValidator>
                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                            MaskType="Date" PromptCharacter="_" TargetControlID="txtDocDate">
                        </cc1:MaskedEditExtender>
                        <%--<cc1:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1" ControlToValidate="txtDocDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid" IsValidEmpty="False" TooltipMessage="Input a Date"></cc1:MaskedEditValidator> 
--%>
                    </td>
                    <td class="style4 tdRight" charoff=" ">
                        Doc Amount:
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="txtDocAmount" runat="server" CssClass="SmallFont TextBoxNo" MaxLength="12"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Select Doc Amount"
                            ControlToValidate="txtDocAmount" Display="None" SetFocusOnError="True" ValidationGroup="GI"></asp:RequiredFieldValidator>
                        <%--  <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" 
                                        InputDirection ="RightToLeft"  TargetControlID="txtDocAmount" 
                                        MaskType ="Number"  Mask="9999999999.99"  >
                                    </cc1:MaskedEditExtender>
                                   --%>
                        <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Please Enter Doc Amount Numeric and Precision 9  Scale 2"
                            MinimumValue="0" MaximumValue="999999999.99" ValidationGroup="GI" ControlToValidate="txtDocAmount"
                            Type="Double" Display="None"></asp:RangeValidator>
                    </td>
                </tr>
                <tr>
                    <td class="  tdRight">
                        Lorry No:
                    </td>
                    <td class=" ">
                        <asp:TextBox ID="txtLorryno" runat="server" CssClass="SmallFont TextBox"></asp:TextBox>
                    </td>
                    <td class="tdRight">
                        Driver Name:
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="txtDriverName" runat="server" CssClass="SmallFont TextBox"></asp:TextBox>
                    </td>
                    <td class="tdRight">
                        Security Incharge:
                    </td>
                    <td class="tdLeft">
                        <%--  <cc3:OboutDropDownList ID="OboutDropDownList1" runat="server">--%>
                        <asp:TextBox ID="txtSecurityIncharge" runat="server" CssClass="SmallFont TextBox"> </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="  tdRight">
                        Check By:
                    </td>
                    <td class=" ">
                        <%--<cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" 
                                        InputDirection ="RightToLeft" TargetControlID="txtQty" 
                                        MaskType ="Number"  Mask="9999999999.999"  >
                                    </cc1:MaskedEditExtender>--%><asp:TextBox ID="txtCheckBy" runat="server" CssClass="SmallFont TextBox "></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Enter Check By"
                            ControlToValidate="txtCheckBy" Display="None" SetFocusOnError="True" ValidationGroup="GI"></asp:RequiredFieldValidator>
                    </td>
                    <td class="tdRight">
                        Remarks:
                    </td>
                    <td class="tdLeft" colspan="3">
                        <asp:TextBox ID="txtRemarks" runat="server" Width="99%" MaxLength="200" CssClass="SmallFont TextBox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please Enter Remakrs"
                            ControlToValidate="txtRemarks" Display="None" SetFocusOnError="True" ValidationGroup="GI"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="  tdRight">
                        Is Fetch From Po:
                    </td>
                    <td class=" ">
                        <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged"
                            ToolTip="Dear!!Please Check If U Want To Fetch Data From PO" />
                    </td>
                    <td class="tdRight" id="tdSelectPO" runat="server" visible="false">
                        Select PO:
                    </td>
                    <td class="tdLeft" colspan="3" id="tdSelectPODrop" runat="server" visible="false">
                        <cc2:ComboBox ID="CMBPO" runat="server" AutoPostBack="True" DataTextField="PO_NUMB"
                            DataValueField="Combined" EmptyText="Select PO" EnableLoadOnDemand="true" Height="200px"
                            MenuWidth="600px" OnLoadingItems="CMBPO_LoadingItems" OnSelectedIndexChanged="CMBPO_SelectedIndexChanged"
                            Width="150px">
                            <HeaderTemplate>
                                <div class="header c1">
                                    PO No.</div>
                                <div class="header c2">
                                    PO Type</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PO_TYPE") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PO_NUMB") %>' /></div>
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
                <tr>
                    <td class="  tdRight">
                        &nbsp;
                    </td>
                    <td class=" " colspan="5">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </td>
        <tr>
            <td align="left" class="td" valign="top" width="100%">
                <table>
                    <tr>
                        <td align="left" class="td" valign="top" width="98%">
                            <table width="98%">
                                <tr bgcolor="#006699">
                                    <td align="left" valign="top" class="tdLeft SmallFont">
                                        <span class="titleheading"><b>Item Code </b></span>
                                    </td>
                                    <td align="left" valign="top" class="tdLeft SmallFont">
                                        <span class="titleheading"><b>Description </b></span>
                                    </td>
                                    <td align="left" valign="top" class="tdLeft SmallFont">
                                        <span class="titleheading"><b>UOM </b></span>
                                    </td>
                                    <td  id ="lblQuantity" align="left" runat ="server"  valign="top" class="tdLeft SmallFont">
                                        <span class="titleheading"><b>Order<br />
                                        Quantity</b></span>
                                    </td>
                                    <td align="left" valign="top" class="tdLeft SmallFont">
                                      <span class="titleheading"><b>  Basic Rate</b></span>  
                                    </td>
                                    <td align="left" valign="top" class="tdLeft SmallFont" width="10px">
                                        <span class="titleheading"><b>Recived Quantity </b></span>
                                    </td>
                                    <td align="left" valign="top" class="tdLeft SmallFont">
                                     <span class="titleheading"><b>Amount</b></span> 
                                    </td>
                                    
                                    <td align="left" valign="top" class="tdLeft SmallFont">
                                        <span class="titleheading"><b>Remarks </b></span>
                                    </td>
                                    <td align="left" valign="top" class="tdLeft SmallFont" width="150px">
                                        <span class="titleheading"><b>&nbsp;</b></span>
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td align="left" valign="top" class="tdLeft SmallFont">
                                        <cc2:ComboBox ID="txtPartyCode1" runat="server" AutoPostBack="True" DataTextField="ITEM_CODE"
                                            DataValueField="ITEM_CODE" EmptyText="Select Items" EnableLoadOnDemand="true"
                                            Height="200px" MenuWidth="600px" OnLoadingItems="txtPartyCode1_LoadingItems"
                                            OnSelectedIndexChanged="txtPartyCode1_SelectedIndexChanged" Width="100px">
                                            <HeaderTemplate>
                                                <div class="header c1">
                                                    ITEM CODE</div>
                                                <div class="header c2">
                                                    ITEM DESCRIPTION</div>
                                                <div class="header c3">
                                                    TYPE</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div class="item c1">
                                                    <%# Eval("ITEM_CODE") %></div>
                                                <div class="item c2">
                                                    <%# Eval("ITEM_DESC") %></div>
                                                <div class="item c3">
                                                    <%# Eval("ITEM_TYPE") %></div>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                Displaying items
                                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                out of
                                                <%# Container.ItemsCount %>.
                                            </FooterTemplate>
                                        </cc2:ComboBox>
                                    </td>
                                    <td align="left" valign="top" class="tdLeft SmallFont">
                                        <asp:TextBox ID="txtDescription" runat="server" CssClass="SmallFont TextBox TextBoxDisplay"
                                            ReadOnly="true" Width="200px"></asp:TextBox>
                                    </td>
                                    <td align="left" valign="top" class="tdLeft SmallFont">
                                        <asp:TextBox ID="txtUOm" runat="server" CssClass="SmallFont TextBox TextBoxDisplay "
                                            Width="40px" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td id ="textQuantity" align="left"  runat ="server" valign="top" class="tdLeft SmallFont ">
                                        <asp:TextBox ID="txtQuantity" runat="server" CssClass="SmallFont TextBox  TextBoxDisplay"
                                           ReadOnly="true" Width="40px" onChange="javascript:Calculation(this.id)"  onkeyup="javascript:Calculation(this.id)"></asp:TextBox>
                                    </td>
                                    <td align="left" valign="top" class="tdLeft SmallFont"><asp:TextBox ID="txtBasicRate" runat="server" CssClass="SmallFont TextBox TextBoxDisplay"
                                            Width="60" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td align="left" valign="top" class="tdLeft SmallFont">
                                        <asp:TextBox ID="txtRecievedQuantity" runat="server" CssClass="SmallFont TextBox "
                                            Width="40px" onChange="javascript:Calculation(this.id)"  onkeyup="javascript:Calculation(this.id)" ></asp:TextBox>
                                    </td>
                                    <td align="left" valign="top" class="tdLeft SmallFont">
                                         <asp:TextBox ID="txtFinalRate" runat="server" CssClass="SmallFont TextBox TextBoxDisplay"
                                            Width="60px" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td align="left" valign="top" class="tdLeft SmallFont">
                                       <asp:TextBox ID="txtDetailsRemarks" runat="server" CssClass="SmallFont TextBox "
                                            Width="150px"></asp:TextBox>
                                    </td>
                                    <td align="left" valign="top" class="tdLeft SmallFont">
                                        <asp:Button ID="BtnSave" runat="server" Text="Save" OnClick="BtnSave_Click"/>
                                        <asp:Button ID="BtnCancel" runat="server" Text="Cancel" OnClick="BtnCancel_Click" />
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td width="100%"  colspan="9">
                                        <asp:Panel ID="pnlGrid" runat="server" Width="100%">
                                            <asp:GridView ID="grdMaterialGateIn" runat="server" AutoGenerateColumns="False" CssClass="SmallFont"
                                                OnRowCommand="grdMaterialGateIn_RowCommand" Width="99%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Item Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtICODE" runat="server" CssClass="Label SmallFont" ReadOnly="true"
                                                                Text='<%# Bind("ITEM_CODE") %>' Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtDESC" runat="server" CssClass="Label SmallFont" ReadOnly="true"
                                                                Text='<%# Bind("ITEM_DESC") %>' Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtUNIT" runat="server" CssClass="Label SmallFont" ReadOnly="true"
                                                                Text='<%# Bind("UOM") %>' Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Order Quantity">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtQTY0" runat="server" CssClass="LabelNo SmallFont" ReadOnly="true"
                                                                Text='<%# Bind("ORD_QTY") %>' Width="50px"></asp:Label>
                                                            <asp:Label ID="txtTRN_QTY_1" runat="server" CssClass="LabelNo SmallFont" ReadOnly="true"
                                                                Text='<%# Bind("ORD_QTY") %>' Visible="false" Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="B. Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtBasicRate" runat="server" CssClass="LabelNo SmallFont" ReadOnly="true"
                                                                Text='<%# Bind("BASIC_RATE") %>' Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   
                                                    <%-- <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate>
                                <asp:Label ID="txtAmount" runat="server" CssClass="LabelNo SmallFont" 
                                    ReadOnly="true" Text='<%# Bind("AMOUNT") %>' Width="70px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Recieved Quantity">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtCostCode" runat="server" CssClass="Label SmallFont" ReadOnly="true"
                                                                Text='<%# Bind("RecivedQuanitity") %>' Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtFinalRate" runat="server" CssClass="LabelNo SmallFont" ReadOnly="true"
                                                                Text='<%# Bind("FINAL_RATE") %>' Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtDetRemarks" runat="server" CssClass="Label SmallFont" ReadOnly="true"
                                                                Text='<%# Bind("REMARKS") %>' Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--   <asp:TemplateField HeaderText="Q.C.">
                            <ItemTemplate>
                                <asp:Label ID="txtQC" runat="server" CssClass="Label SmallFont" ReadOnly="true" 
                                    Text='<%# Bind("QCFLAG") %>' Width="40px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandArgument='<%# Bind("UNIQUEID") %>'
                                                                CommandName="EditItem" Text="Edit"></asp:LinkButton>
                                                            /
                                                            <asp:LinkButton ID="lnkbtnDel" runat="server" CommandArgument='<%# Bind("UNIQUEID") %>'
                                                                CommandName="DelItem" Text="Delete"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <RowStyle CssClass="SmallFont" />
                                                <HeaderStyle BackColor="#336699" CssClass="SmallFont titleheadingGrid" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                   
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
        </tr>
    </tr>
</table>
<%--</ContentTemplate>
            </asp:UpdatePanel>--%>