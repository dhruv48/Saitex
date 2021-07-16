<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CommonMaster/UserMaster.master" CodeFile="CreateBranch.aspx.cs" Inherits="Module_StartUp_CreateBranch" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="cphBody">
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
    }
    .header
    {
        margin-left: 4px;
    }
    .c1
    {
        width: 70px;
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
    .c4
    {
        margin-left: 4px;
        width: 200px;
    }
</style>
<cc4:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc4:ToolkitScriptManager>
<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>--%>
        <table class="tContentArial">
            <tr>
                <td class="td">
                    <table>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td id="tdSave" align="left" width="48" runat="server">
                                            <asp:ImageButton ID="imgbtnSave" OnClick="imgbtnSave_Click" runat="server" ValidationGroup="M1"
                                                ImageUrl="~/CommonImages/save.jpg" ToolTip="Save" OnClientClick="if (!confirm('Are you want to Save ?')) { return false; }">
                                            </asp:ImageButton>
                                        </td>
                                        <td id="tdUpdate" align="left" width="48" runat="server">
                                           
                                        </td>
                                        <td id="tdDelete" align="left" width="48" runat="server">
                                           
                                        </td>
                                        <td id="tdFind" runat="server" align="left" width="48">
                                           
                                        </td>
                                        <td id="tdClear" runat="server" align="left" width="48">
                                          
                                        </td>
                                        <td id="tdPrint" runat="server" align="left" width="48">
                                            
                                        </td>
                                        <td id="tdExit" runat="server" align="left" width="48">
                                           
                                        </td>
                                        <td id="tdHelp" runat="server" align="left" width="48">
                                            
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr class="TableHeader">
                            <td align="center" valign="top" class="td">
                                <span class="titleheading">Branch Master</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="td" valign="top" align="left" colspan="3">
                                <span class="Mode">
                                    <asp:Label ID="lblMode" runat="server"></asp:Label>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="center" colspan="3">
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
                                <table>
                                    <tr>
                                        <td align="right" valign="top">
                                            *Company Name:
                                        </td>
                                        <td align="left" valign="top">
                                            <cc1:ComboBox ID="cmbCompanyName" runat="server" Width="132px" Height="150px" AutoPostBack="true"
                                                EmptyText="Select Company" EnableLoadOnDemand="True" DataTextField="COMP_NAME"
                                                DataValueField="COMP_CODE" MenuWidth="320px" OnLoadingItems="cmbCompanyName_LoadingItems"
                                                OnSelectedIndexChanged="cmbCompanyName_SelectedIndexChanged" TabIndex="1">
                                                <HeaderTemplate>
                                                    <div class="header c1">
                                                        Code
                                                    </div>
                                                    <div class="header c2">
                                                        Comp Name</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div class="item c1">
                                                        <%# Eval("COMP_CODE")%></div>
                                                    <div class="item c2">
                                                        <%# Eval("COMP_NAME")%></div>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Displaying items
                                                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                    out of
                                                    <%# Container.ItemsCount %>.
                                                </FooterTemplate>
                                            </cc1:ComboBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="M1"
                                                Display="None" ErrorMessage="Select Company Name" ControlToValidate="cmbCompanyName"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </td>
                                        <td align="right" valign="top">
                                            *Branch Code:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtBranchCode" runat="server" TabIndex="2" ValidationGroup="M1"
                                                Width="130px" CssClass="gCtrTxt UpperCase TextBox" MaxLength="7" TextMode="singleLine"></asp:TextBox>
                                            <%--<cc1:ComboBox ID="cmbBranchCode" runat="server" Width="132px" Height="150px" AutoPostBack="True"
                                                EnableLoadOnDemand="True" DataTextField="BRANCH_NAME" DataValueField="BRANCH_CODE"
                                                MenuWidth="300px" TabIndex="3" Visible="false" OnLoadingItems="cmbBranchCode_LoadingItems"
                                                OnSelectedIndexChanged="cmbBranchCode_SelectedIndexChanged">
                                                <HeaderTemplate>
                                                    <div class="header c1">
                                                        Branch Code
                                                    </div>
                                                    <div class="header c2">
                                                        Branch Name</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div class="item c1">
                                                        <%# Eval("BRANCH_CODE")%></div>
                                                    <div class="item c2">
                                                        <%# Eval("BRANCH_NAME")%></div>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Displaying items
                                                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                    out of
                                                    <%# Container.ItemsCount %>.
                                                </FooterTemplate>
                                            </cc1:ComboBox>--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="M1"
                                                Display="None" ErrorMessage="Enter Branch Code" ControlToValidate="txtBranchCode"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </td>
                                        <td align="right" valign="top">
                                            *Branch Name
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtBranchName" TabIndex="4" runat="server" ValidationGroup="M1"
                                                AutoPostBack="true" Width="130px" CssClass="gCtrTxt UpperCase" MaxLength="50"
                                                TextMode="singleLine" OnTextChanged="txtBranchName_TextChanged"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="M1"
                                                Display="None" ErrorMessage="Enter Branch Name" ControlToValidate="txtBranchName"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator><strong> </strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            Branch ID:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtbranchid" runat="server" TabIndex="5" Width="130px" CssClass="gCtrTxt UpperCase"
                                                MaxLength="15" TextMode="singleLine"></asp:TextBox>
                                        </td>
                                        <td align="right" valign="top">
                                            *Contact Number:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtBranchContactNumber" TabIndex="6" runat="server" ValidationGroup="M1"
                                                Width="130px" CssClass="TextBox" MaxLength="50" TextMode="singleLine"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="M1"
                                                Display="None" ErrorMessage="Enter Branch Contact No" ControlToValidate="txtBranchContactNumber"
                                                Font-Bold="False" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </td>
                                        <td align="right" valign="top">
                                            Branch ESI Number
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtBranchESINumber" TabIndex="7" runat="server" ValidationGroup="M1"
                                                Width="130px" CssClass="TextBox" MaxLength="20" TextMode="singleLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            Branch PF Number
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtBranchPFNumber" TabIndex="8" runat="server" ValidationGroup="M1"
                                                Width="130px" CssClass="gCtrTxt" MaxLength="15" TextMode="singleLine"></asp:TextBox>
                                        </td>
                                        <td align="right" valign="top">
                                            Branch Fax Number
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtBranchFaxNumber" TabIndex="9" runat="server" ValidationGroup="M1"
                                                Width="130px" CssClass="TextBox" MaxLength="25" TextMode="singleLine"></asp:TextBox>
                                        </td>
                                        <td align="right" valign="top">
                                            Service Tax Number:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtBranchServiceTaxNo" TabIndex="10" runat="server" ValidationGroup="M1"
                                                Width="130px" CssClass="gCtrTxt" MaxLength="200" TextMode="singleLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            CST No:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtbranchcstno" runat="server" TabIndex="11" CssClass="gCtrTxt UpperCase"
                                                Width="130px"></asp:TextBox>
                                        </td>
                                        <td align="right" valign="top">
                                            CST DATE:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="branchcstdate" runat="server" TabIndex="12" CssClass="TextBox" Width="130px"></asp:TextBox>
                                            <cc4:CalendarExtender ID="ce2" runat="server" Format="dd/MM/yyyy" TargetControlID="branchcstdate"
                                                PopupPosition="TopLeft">
                                            </cc4:CalendarExtender>
                                        </td>
                                        <td align="right" valign="top">
                                            LST No:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtbranchlstno" runat="server" TabIndex="13" CssClass=" TextBox"
                                                Width="130px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            LST DATE:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtbranchlstdate" runat="server" TabIndex="14" CssClass="TextBox"
                                                Width="130px"></asp:TextBox><cc4:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy"
                                                    runat="server" TargetControlID="txtbranchlstdate" PopupPosition="TopLeft">
                                                </cc4:CalendarExtender>
                                        </td>
                                        <td align="right" valign="top">
                                            TIN No:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtbranchtinno" runat="server" TabIndex="15" CssClass="TextBox"
                                                Width="130px"></asp:TextBox>
                                        </td>
                                        <td align="right" valign="top">
                                            TIN DATE:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="BranchTinDate" runat="server" TabIndex="16" CssClass="TextBox" Width="130px"></asp:TextBox>
                                            <cc4:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" runat="server" TargetControlID="branchtindate"
                                                PopupPosition="TopLeft">
                                            </cc4:CalendarExtender>
                                        </td>
                                    </tr>
                                      <tr>
                                        <td align="right" valign="top">
                                           ECC No:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtEccNo" runat="server" TabIndex="14" CssClass="TextBox"
                                                Width="130px"></asp:TextBox>
                                        </td>
                                        <td align="right" valign="top">
                                            Factory L/C No:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtFactoryLcNo" runat="server" TabIndex="15" CssClass="TextBox"
                                                Width="130px"></asp:TextBox>
                                        </td>
                                        <td align="right" valign="top">
                                            Air Pollution No:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtAirPolution" runat="server" TabIndex="16" CssClass="TextBox" Width="130px"></asp:TextBox>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label ID="Label1" runat="server" CssClass="LabelNo" Text="*Party Code :"></asp:Label>
                                        </td>
                                        <td valign="top" align="left" colspan="3">
                                            <asp:TextBox ID="txtPartyCode" TabIndex="18" runat="server" CssClass="UpperCase TextBox TextBoxDisplay SmallFont"
                                                ReadOnly="True" Width="130px" MaxLength="15"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="M1"
                                                Display="None" ErrorMessage="Enter Party" ControlToValidate="txtPartyCode" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="txtPartyName" TabIndex="18" runat="server" CssClass="UpperCase TextBox TextBoxDisplay SmallFont"
                                                ReadOnly="True" Width="240px" MaxLength="15"></asp:TextBox>
                                        </td>
                                        <td align="right" valign="top">
                                            *Sequence Prefix
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtPrefix" runat="server" TabIndex="23" ValidationGroup="M1" Width="130px"
                                                CssClass="SmallFont UpperCase" MaxLength="2" TextMode="singleLine"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="M1"
                                                Display="None" ErrorMessage="Enter Prefix" ControlToValidate="txtPrefix" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                      <tr>
                                        <td align="right" valign="top">
                                            *Branch Email-ID:
                                        </td>
                                        <td align="left" valign="top" colspan="3">
                                            <asp:TextBox ID="txtBranchEmailId" TabIndex="20" runat="server" ValidationGroup="M1"
                                                Width="100%" CssClass="gCtrTxt" MaxLength="75" TextMode="singleLine"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="M1"
                                                Display="None" ErrorMessage="Enter E-Mail ID" ControlToValidate="txtBranchEmailId"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="M1"
                                                Display="None" ErrorMessage="Invaild Email Id !" ControlToValidate="txtBranchEmailId"
                                                Font-Bold="False" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                SetFocusOnError="True"></asp:RegularExpressionValidator>
                                        </td>
                                        
                                        <td align="right" valign="top">
                                            *State
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtState" runat="server" TabIndex="23" ValidationGroup="M1" Width="130px"
                                                CssClass="SmallFont UpperCase" MaxLength="2" TextMode="singleLine"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="M1"
                                                Display="None" ErrorMessage="Enter State" ControlToValidate="txtState" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            *Branch Address
                                        </td>
                                        <td align="left" valign="top" colspan="5">
                                            <asp:TextBox ID="txtBranchAddress" TabIndex="21" runat="server" ValidationGroup="M1"
                                                Width="100%" CssClass="TextBox" MaxLength="300" TextMode="SingleLine"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="M1"
                                                Display="None" ErrorMessage="Enter Branch Address" ControlToValidate="txtBranchAddress"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            Remarks
                                        </td>
                                        <td align="left" valign="top" colspan="5">
                                            <asp:TextBox ID="txtRemarks" TabIndex="22" runat="server" ValidationGroup="M1" Width="100%"
                                                CssClass="gCtrTxt" MaxLength="250" TextMode="SingleLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            Status
                                        </td>
                                        <td align="left" valign="top" colspan="5">
                                            <asp:CheckBox ID="chk_Status" TabIndex="24" runat="server"></asp:CheckBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                <cc2:Grid ID="grdShowBranch" runat="server" AllowAddingRecords="False" AllowFiltering="True"
                                    PageSize="5" AutoGenerateColumns="False"  TabIndex="25">
                                    <Columns>
                                        <cc2:Column DataField="BRANCH_CODE" Align="Left" HeaderText="Code" Wrap="True" Width="80px">
                                        </cc2:Column>
                                        <cc2:Column DataField="BRANCH_NAME" Align="Left" HeaderText="Branch Name" Wrap="True"
                                            Width="100px">
                                        </cc2:Column>
                                        <cc2:Column DataField="BRANCH_ID" Align="Left" HeaderText="Branch ID" Wrap="false"
                                            Visible="false">
                                        </cc2:Column>
                                        <cc2:Column DataField="COMP_CODE" Align="Left" HeaderText="Comp Code" Wrap="True"
                                            Visible="false">
                                        </cc2:Column>
                                        <cc2:Column DataField="COMP_NAME" Align="Left" HeaderText="Comp Name" Wrap="True"
                                            Width="150px">
                                        </cc2:Column>
                                        <cc2:Column DataField="BRANCH_ADD" Align="Left" HeaderText="Address" Wrap="True"
                                            Width="210px">
                                        </cc2:Column>
                                        <cc2:Column DataField="BRANCH_CONT_NO" Align="Left" HeaderText="Contact No" Visible="false"
                                            Wrap="True">
                                        </cc2:Column>
                                        <cc2:Column DataField="BRANCH_MAIL_ID" Align="Left" HeaderText="Mail ID" Visible="false"
                                            Wrap="True">
                                        </cc2:Column>
                                        <cc2:Column DataField="BRANCH_FAX_NO" Align="Left" HeaderText="FAX No" Visible="false"
                                            Wrap="True">
                                        </cc2:Column>
                                        <cc2:Column DataField="BRANCH_ESI_NO" Align="Left" HeaderText="ESI" Visible="false"
                                            Wrap="True">
                                        </cc2:Column>
                                        <cc2:Column DataField="BRANCH_PF_NO" Align="Left" HeaderText="PF No" Visible="false"
                                            Wrap="True">
                                        </cc2:Column>
                                        <cc2:Column DataField="BRANCH_SERVICE_TAX_NO" Align="Left" HeaderText="Service TAX No"
                                            Visible="false" Wrap="True">
                                        </cc2:Column>
                                        <cc2:Column DataField="BRANCH_REMARKS" Align="Left" HeaderText="Remarks" Wrap="True"
                                            Width="220px">
                                        </cc2:Column>
                                        <cc2:Column DataField="STATUS" Align="Left" HeaderText="STATUS" Visible="false" Wrap="True">
                                        </cc2:Column>
                                        <cc2:Column DataField="BRANCH_CST_NO" Align="Left" HeaderText="Service TAX No" Visible="false"
                                            Wrap="True">
                                        </cc2:Column>
                                        <cc2:Column DataField="BRANCH_LST_NO" Align="Left" HeaderText="Service TAX No" Visible="false"
                                            Wrap="True">
                                        </cc2:Column>
                                        <cc2:Column DataField="BRANCH_TIN_NO" Align="Left" HeaderText="Remarks" Visible="false"
                                            Wrap="True">
                                        </cc2:Column>
                                        <cc2:Column DataField="BRANCH_CST_DATE" Align="Left" HeaderText="STATUS" Visible="false"
                                            Wrap="True">
                                        </cc2:Column>
                                        <cc2:Column DataField="BRANCH_LST_DATE" Align="Left" HeaderText="Remarks" Visible="false"
                                            Wrap="True">
                                        </cc2:Column>
                                        <cc2:Column DataField="BRANCH_TIN_DATE" Align="Left" HeaderText="STATUS" Visible="false"
                                            Wrap="True">
                                        </cc2:Column>
                                        <cc2:Column DataField="PARTY_CODE" Align="Left" HeaderText="Party Code" Visible="false"
                                            Wrap="True">
                                        </cc2:Column>
                                        <cc2:Column DataField="SEQ_PREFIX" Align="Left" HeaderText="Prefix" Visible="false"
                                            Wrap="True">
                                        </cc2:Column>
                                    </Columns>
                                </cc2:Grid>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>

                                
</asp:Content>
