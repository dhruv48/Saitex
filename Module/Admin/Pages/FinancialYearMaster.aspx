<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="FinancialYearMaster.aspx.cs" Inherits="Admin_FinancialYearMaster" Title="Untitled Page" %>

<%@ Register Src="~/Module/Admin/Controls/FinancialYear.ascx" TagName="FinancialYear" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:FinancialYear id="FinancialYear1" runat="server" OnLoad="FinancialYear1_Load">
    </uc1:FinancialYear>
</asp:Content>

