<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmpMaster_OPT.aspx.cs" Inherits="HRMS_EmpMaster_OPT" %>

<%@ Register TagPrefix="obout" Namespace="Obout.ComboBox" Assembly="obout_ComboBox" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Employee Master Option</title>
    <link rel="stylesheet" type="text/css" href="../../../StyleSheet/CommonStyle.css" />
    <link rel="stylesheet" type="text/css" href="../../../StyleSheet/cssdesign.css" />
    <link rel="stylesheet" type="text/css" href="../../../StyleSheet/ModalPopup.css" />
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
            width: 150px;
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
</head>
<body bgcolor="#FFFFFF" topmargin="0" leftmargin="0" width="100%">
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div>
        <asp:Panel ID="Panel1" runat="server" BackColor="#336799" Height="600px" Width="600px">
            <table width="600" align="left" class="tContentArial">
                <tr>
                    <td align="center" width="600">
                        <table width="600" align="left" class="tContentArial">
                            <tr>
                                <td align="center" colspan="6" class="TableHeader">
                                    <span class="titleheading">Employee Master Report</span>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="6">
                                    <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
                                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
                                </td>
                            </tr>
                            <%--<tr>
                                    <td align="right">
                                        Company Code</td>
                                    <td align="left">
                                        <asp:TextBox ID="txtCompanyCode" runat="server" Width="100px"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="ACE2" runat="server" CompletionInterval="1000" MinimumPrefixLength="1"
                                            ServiceMethod="GetCompanyName" ServicePath="../AutoComplete.asmx" TargetControlID="txtCompanyCode"
                                            UseContextKey="true">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    
                                    <td align="right">
                                        Company Name</td>
                                    <td align="left">
                                        <asp:TextBox ID="txtCompName" runat="server" Width="100px"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="ACE1" runat="server" CompletionInterval="1000" MinimumPrefixLength="1"
                                            ServiceMethod="GetCompanyName1" ServicePath="../AutoComplete.asmx" TargetControlID="txtCompName"
                                            UseContextKey="true">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    
                                </tr>--%>
                            <%-- <tr>
                                    <td align="right">
                                        Branch Code</td>
                                    <td align="left">
                                        <asp:TextBox ID="txtBranchCode" runat="server" Width="100px"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="ACE3" runat="server" CompletionInterval="1000"
                                            MinimumPrefixLength="1" ServiceMethod="GetBranchMasterList" ServicePath="../AutoComplete.asmx"
                                            TargetControlID="txtBranchCode" UseContextKey="true">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    <td align="right">
                                        Branch Name</td>
                                    <td align="left">
                                        <asp:TextBox ID="txtBranchName" runat="server" Width="100px"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="ACE4" runat="server" CompletionInterval="1000"
                                            MinimumPrefixLength="1" ServiceMethod="GetBranchMasterName" ServicePath="../AutoComplete.asmx"
                                            TargetControlID="txtBranchName" UseContextKey="true">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Department Code</td>
                                    <td align="left">
                                        <asp:TextBox ID="txtDepartmentCode" runat="server" Width="100px"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="ACE6" runat="server" CompletionInterval="1000" MinimumPrefixLength="1"
                                            ServiceMethod="GetDepartmentMasterList" ServicePath="../AutoComplete.asmx" TargetControlID="txtDepartmentCode"
                                            UseContextKey="true">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    
                                    <td align="right">
                                        Department Name</td>
                                    <td align="left">
                                        <asp:TextBox ID="txtDepartName" runat="server" Width="100px"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="ACE5" runat="server" CompletionInterval="1000" MinimumPrefixLength="1"
                                            ServiceMethod="GetDepartmentName" ServicePath="../AutoComplete.asmx" TargetControlID="txtDepartName"
                                            UseContextKey="true">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    
                                </tr>--%>
                            <tr>
                                <td align="left" width="20%" valign="top">
                                    Branch Name
                                </td>
                                <td align="left" width="25%" valign="top">
                                    <obout:ComboBox runat="server" ID="ddlBranchName" Width="150" Height="200px" DataTextField="BRANCH_NAME"
                                        DataValueField="BRANCH_CODE" EnableLoadOnDemand="true" OnLoadingItems="ddlBranchName_LoadingItems"
                                        MenuWidth="150px">
                                        <HeaderTemplate>
                                            <div class="header c2">
                                                Branch Name</div>
                                            <%--<div class="header c3">Holiday Type</div>
	                                                            <div class="header c3">Holiday Date</div>
	                                                            <div class="header c3">Holiday Year</div>--%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="item c1">
                                                <%# Eval("BRANCH_NAME")%></div>
                                            <%--<div class="item c2"><%# Eval("HLD_Type")%></div>
                                                                    <div class="item c2"><%# Eval("HLD_DATE")%></div>
                                                                    <div class="item c3"><%# Eval("YEAR")%></div>--%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Displaying items
                                            <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                            out of
                                            <%# Container.ItemsCount %>.
                                        </FooterTemplate>
                                    </obout:ComboBox>
                                </td>
                                <td align="left" width="10%" valign="top">
                                </td>
                                <td align="left" width="20%" valign="top">
                                    Department Name
                                </td>
                                <td align="left" width="25%" valign="top">
                                    <obout:ComboBox runat="server" ID="ddlDepartment" Width="150" Height="150px" DataTextField="DEPT_NAME"
                                        DataValueField="DEPT_CODE" EnableLoadOnDemand="true" OnLoadingItems="ddlDepartment_LoadingItems"
                                        MenuWidth="250px">
                                        <HeaderTemplate>
                                            <div class="header c2">
                                                Department Name</div>
                                            <%--<div class="header c3">Holiday Type</div>
	                                                            <div class="header c3">Holiday Date</div>
	                                                            <div class="header c3">Holiday Year</div>--%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="item c1">
                                                <%# Eval("DEPT_NAME")%></div>
                                            <%--<div class="item c2"><%# Eval("HLD_Type")%></div>
                                                                    <div class="item c2"><%# Eval("HLD_DATE")%></div>
                                                                    <div class="item c3"><%# Eval("YEAR")%></div>--%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Displaying items
                                            <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                            out of
                                            <%# Container.ItemsCount %>.
                                        </FooterTemplate>
                                    </obout:ComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="6">
                                    <br />
                                    <br />
                                    <asp:Button ID="btnGetReport" runat="server" Text="Get Report" OnClick="btnGetReport_Click1" />
                                    <br />
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
