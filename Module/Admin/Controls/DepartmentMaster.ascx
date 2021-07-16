<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DepartmentMaster.ascx.cs"
    Inherits="Module_Admin_Controls_DepartmentMaster" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
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
        margin-left: 4px;
    }
    .c1
    {
        width: 60px;
    }
    .c2
    {
        margin-left: 4px;
        width: 130px;
    }
    .c3
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
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
<table cellpadding="0" cellspacing="0" border="0" align="left" class="tContentArial">
    <tr>
        <td>
            <table cellpadding="0" cellspacing="0" border="0" align="left" class="tContentArial">
                <tr>
                    <td class="td" align="left">
                        <table cellpadding="0" cellspacing="0" class="tContentArial" border="0" align="left">
                            <tr>
                                <td id="tdSave" runat="server" width="48" align="left">
                                    <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="Save" Width="48" Height="41"
                                        ImageUrl="~/CommonImages/save.jpg" ValidationGroup="M1" OnClick="imgbtnSave_Click"
                                        OnClientClick="if (!confirm('Are you want to Save ?')) { return false; }" />
                                </td>
                                <td id="tdFind" runat="server" width="48" align="left">
                                    <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" Width="48" Height="41"
                                        ImageUrl="~/CommonImages/link_find.png" OnClick="imgbtnFind_Click"></asp:ImageButton>
                                </td>
                                <td id="tdUpdate" runat="server" width="48" align="left">
                                    <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" Width="48" Height="41"
                                        ImageUrl="~/CommonImages/edit1.jpg" OnClick="imgbtnUpdate_Click" ValidationGroup="M1"
                                        OnClientClick="if (!confirm('Are you sure to Update Record ?')) { return false; }">
                                    </asp:ImageButton>
                                </td>
                                <td id="tdDelete" runat="server" width="48" align="left">
                                    <asp:ImageButton ID="imgbtnDelete" runat="server" Width="48" Height="41" ToolTip="Delete"
                                        ImageUrl="~/CommonImages/del6.png" OnClick="imgbtnDelete_Click" OnClientClick="if (!confirm('Are you sure to Delete Record ?')) { return false; }">
                                    </asp:ImageButton>
                                </td>
                                <td width="48" align="left">
                                    <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" ToolTip="Clear"
                                        ImageUrl="~/CommonImages/clear.jpg" OnClick="imgbtnClear_Click" OnClientClick="if (!confirm('Are you want to Clear ?')) { return false; }">
                                    </asp:ImageButton>
                                </td>
                                <td width="48" align="left">
                                    <asp:ImageButton ID="imgbtnPrint" runat="server" Width="48" Height="41" ToolTip="Print"
                                        ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click" OnClientClick="if (!confirm('Are you sure to Print Record ?')) { return false; }">
                                    </asp:ImageButton>
                                </td>
                                <td width="48" align="left">
                                    <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                                        OnClick="imgbtnExit_Click" Width="48" Height="41" OnClientClick="if (!confirm('Are you want to Exit ?')) { return false; }">
                                    </asp:ImageButton>
                                </td>
                                <td width="48" align="left">
                                    <asp:ImageButton ID="imgbtnHelp" runat="server" Width="48" Height="41" ToolTip="Help"
                                        ImageUrl="~/CommonImages/link_help.png" OnClick="imgbtnHelp_Click"></asp:ImageButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="TableHeader td" align="center">
                        <span class="titleheading">Department Master</span>
                    </td>
                </tr>
                <tr>
                    <td class="td" align="left" valign="top">
                        <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
                        </span>
                    </td>
                </tr>
                <tr>
                    <td align="center" valign="top">
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
                                    *Department Code
                                </td>
                                <td>
                                    <b>:</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDepartmentCode" runat="server" CssClass="gCtrTxt UpperCase" Width="250px"
                                        TabIndex="1" MaxLength="10" ValidationGroup="M1"></asp:TextBox>
                                    <cc1:ComboBox ID="cmbDepartmentCode" runat="server" TabIndex="2" Width="250px" Height="150px"
                                        AutoPostBack="true" EnableLoadOnDemand="True" DataTextField="DEPT_NAME" DataValueField="DEPT_CODE"
                                        OnLoadingItems="cmbDepartmentCode_LoadingItems" OnSelectedIndexChanged="cmbDepartmentCode_SelectedIndexChanged"
                                        EmptyText="Find..">
                                        <HeaderTemplate>
                                            <div class="header c1">
                                                Code
                                            </div>
                                            <div class="header c2">
                                                Comp Name</div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="item c1">
                                                <%# Eval("DEPT_CODE")%></div>
                                            <div class="item c2">
                                                <%# Eval("DEPT_NAME")%></div>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Displaying items
                                            <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                            out of
                                            <%# Container.ItemsCount %>.
                                        </FooterTemplate>
                                    </cc1:ComboBox>
                                    <asp:RequiredFieldValidator ID="RFDepartmentCode" Display="None" runat="server" SetFocusOnError="True"
                                        ErrorMessage="Enter Department Code" ControlToValidate="txtDepartmentCode" ValidationGroup="M1"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    *Department Name
                                </td>
                                <td align="center" valign="top">
                                    <b>:</b>
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtDeparmentName" runat="server" CssClass="gCtrTxt UpperCase" Width="250px"
                                        TabIndex="2" MaxLength="60" ValidationGroup="M1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RFDeptName" Display="None" runat="server" SetFocusOnError="True"
                                        ErrorMessage="Please Enter Department Name" ControlToValidate="txtDeparmentName"
                                        ValidationGroup="M1"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    Remarks
                                </td>
                                <td align="center" valign="top">
                                    <b>:</b>
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="gCtrTxt" Width="250px" Height="35px"
                                        TabIndex="3" TextMode="multiLine" Rows="2" MaxLength="200" ValidationGroup="M1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    Status
                                </td>
                                <td align="center" valign="top">
                                    <b>:</b>
                                </td>
                                <td align="left" valign="top">
                                    <asp:CheckBox ID="chk_Status" runat="server" TabIndex="4" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="td" align="center" valign="top">
                        <table class="tContentArial" cellspacing="0" cellpadding="0" width="100%" align="center"
                            border="0">
                            <tbody>
                                <tr>
                                    <td align="left" valign="top">
                                        <cc2:Grid ID="grdDepartment" runat="server" AllowAddingRecords="False" AllowFiltering="True"
                                            PageSize="5" AutoGenerateColumns="False" OnSelect="grdDepartment_Select" TabIndex="15">
                                            <Columns>
                                                <cc2:Column DataField="DEPT_CODE" Align="Left" HeaderText="Dept Code" Width="100px">
                                                </cc2:Column>
                                                <cc2:Column DataField="DEPT_NAME" Align="Left" HeaderText="Department Name" Width="200px">
                                                </cc2:Column>
                                                <cc2:Column DataField="DEPT_REMARKS" Align="Left" HeaderText="Department Remarks"
                                                    Width="300px">
                                                </cc2:Column>
                                                <cc2:Column DataField="STATUS" Align="Left" HeaderText="Status" Width="300px" Visible="false">
                                                </cc2:Column>
                                            </Columns>
                                        </cc2:Grid>
                                        <br />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
</ContentTemplate> </asp:UpdatePanel>