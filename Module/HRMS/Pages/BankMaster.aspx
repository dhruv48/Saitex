<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="BankMaster.aspx.cs" Inherits="Module_HRMS_Pages_BankMaster" Title="Bank Master" %>
<%@ Register Src="../Controls/BankMaster.ascx" TagName="BankMaster" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphBody">
    <uc1:BankMaster ID="BankMaster1" runat="server" />
</asp:Content>
