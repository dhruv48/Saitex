<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmpCompInfo.ascx.cs" Inherits="Module_HRMS_Controls_EmpCompInfo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
        width: 40px;
    }
    .c2
    {
        margin-left: 4px;
        width: 80px;
    }
     .c3
    {
        width: 50px;
    }
    .c4
    {
        margin-left: 4px;
        width: 100px;
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
        <table align="left" width="100%" class="tContentArial">
            <tr>
                <td>
                    <table width="100%" class="tContentArial" cellspacing="0" cellpadding="0" align="left">
                        <tbody>
                            <tr>
                                <td align="left" class="td" colspan="3">
                                    <table class="tContentArial" cellspacing="0" cellpadding="0" >
                                        <tbody>
                                            <tr>
                                                <td id="tdSave" align="left" width="48" runat="server">
                                                    <asp:ImageButton ID="imgbtnSave" OnClick="imgbtnSave_Click" runat="server" ValidationGroup="M1"
                                                        ImageUrl="~/CommonImages/save.jpg" ToolTip="Save"></asp:ImageButton>
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
                                                <td id="tdHelp" runat="server" align="left" width="48">
                                                    <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                                                        ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableHeader td" align="center" colspan="3">
                                    <b class="titleheading">Employee Company Info</b>
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
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                Employee Name
                                                <td><b>:</b></td>
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="TxtEmpname" CssClass="gCtrTxt" Width="200px"  runat="server"></asp:TextBox>
                                                    <asp:Label ID="lblEmp_id" runat="server" Visible="false"  Text=""></asp:Label>
                                            </td>                                           
                                        </tr>
                                        <tr>
                                            <td colspan="9">
                                                <b><i>Personal Details</i></b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Bank Name
                                            </td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <cc1:ComboBox ID="cmbBankName" runat="server" Width="155px" Height="150px" 
                                                TabIndex="0" onloadingitems="cmbBankName_LoadingItems" 
                                                    DataTextField="BANK_NAME" DataValueField="BANK_CODE">
                                                <HeaderTemplate>
                                                    <div class="header c1">
                                                        Code
                                                    </div>
                                                    <div class="header c2">
                                                        Bank Name</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div class="item c1">
                                                        <%# Eval("BANK_CODE")%></div>
                                                    <div class="item c2">
                                                        <%# Eval("BANK_NAME")%></div>
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
                                                A/C No
                                            </td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtACNo" runat="server" CssClass="gCtrTxt UpperCase" 
                                                    Width="170px"></asp:TextBox>
                                            </td>
                                            <td>
                                                Driving Licence No
                                            </td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDLNo" runat="server" CssClass="gCtrTxt UpperCase" 
                                                    Width="170px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Issue Date
                                            </td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDLIssueDate" CssClass="gCtrTxt"  runat="server" Width="170px"></asp:TextBox>
                                            </td>
                                            <td>
                                                Passport No
                                            </td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPassportNo" runat="server" CssClass="gCtrTxt UpperCase" 
                                                    Width="170px"></asp:TextBox>
                                            </td>
                                            <td>
                                                Issue Date
                                            </td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPassportIssueDate" runat="server" Width="170px" CssClass="gCtrTxt"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                PAN No
                                            </td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPANNo" runat="server" CssClass="gCtrTxt UpperCase" Width="170px"></asp:TextBox>
                                            </td>
                                            <td>
                                                PF A/C No
                                            </td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPFACNo" runat="server" CssClass="gCtrTxt UpperCase" 
                                                    Width="170px"></asp:TextBox>
                                            </td>
                                            <td>
                                                Insurance No
                                            </td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtInsuranceNo" runat="server" CssClass="gCtrTxt" 
                                                    Width="170px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                ESI Dispensary
                                            </td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDispensary" runat="server" Width="170px" CssClass="gCtrTxt"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="9">
                                                <b><i>Present Address</i></b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Address
                                            </td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td rowspan="2">
                                                <asp:TextBox ID="txtPreAddress" runat="server" CssClass="gCtrTxt" Width="170px" TextMode="multiLine"
                                                    Rows="2" MaxLength="200" Height="61px"></asp:TextBox>
                                            </td>
                                            <td>
                                                City
                                            </td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPreCity" CssClass="gCtrTxt" Width="170px" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                State
                                            </td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPreState" CssClass="gCtrTxt" Width="170px" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                Country
                                            </td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPreCountry" CssClass="gCtrTxt" Width="170px" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                Pin No
                                            </td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPrePinNo" CssClass="gCtrTxt" onKeyPress="return checkNumeric(event)" Width="170px" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Tel No
                                            </td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPreTelNo" runat="server" onKeyPress="return checkNumeric(event)" CssClass="gCtrTxt" Width="170px"></asp:TextBox>
                                            </td>
                                            <td>
                                                FAX No
                                            </td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPreFAXNo" CssClass="gCtrTxt" onKeyPress="return checkNumeric(event)" Width="170px" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                Email ID
                                            </td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPreEmailID" CssClass="gCtrTxt" Width="170px" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="9">
                                                <b><i>Permanent Address</i></b>&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkSameAddress"
                                                    runat="server" Text="Same as Present Address" AutoPostBack="true" OnCheckedChanged="chkSameAddress_CheckedChanged" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Address
                                            </td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td rowspan="2">
                                                <asp:TextBox ID="txtPermAddress" runat="server" CssClass="gCtrTxt" Width="170px"
                                                    TextMode="multiLine" Rows="2" MaxLength="200" Height="61px"></asp:TextBox>
                                            </td>
                                            <td>
                                                City
                                            </td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPermCity" CssClass="gCtrTxt" Width="170px" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                State
                                            </td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPermState" CssClass="gCtrTxt" Width="170px" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                Country
                                            </td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPermCountry" CssClass="gCtrTxt" Width="170px" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                Pin No
                                            </td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPermPinNo" CssClass="gCtrTxt" onKeyPress="return checkNumeric(event)" Width="170px" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Tel No
                                            </td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPermTelNo" CssClass="gCtrTxt" onKeyPress="return checkNumeric(event)" runat="server" Width="170px"></asp:TextBox>
                                            </td>
                                            <td>
                                                FAX No
                                            </td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPermFAXNo" CssClass="gCtrTxt" onKeyPress="return checkNumeric(event)" Width="170px" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                Email ID
                                            </td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPermEmailID" CssClass="gCtrTxt" Width="170px" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </table>
        <cc1:CalendarExtender ID="ceDLIssueDate" runat="server" TargetControlID="txtDLIssueDate">
        </cc1:CalendarExtender>
        <cc1:CalendarExtender ID="cePassportIssueDate" runat="server" TargetControlID="txtPassportIssueDate">
        </cc1:CalendarExtender>
 <%--</ContentTemplate>
</asp:UpdatePanel>--%>