<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CommonMaster/admin.master" CodeFile="Fiber_Qc_Approval.aspx.cs" Inherits="Module_NewFiber_Pages_Fiber_Qc_Approval" %>

<%@ Register Src="~/Module/NewFiber/Controls/Fiber_Qc_Stan_Approval.ascx" TagName="Fiber_QC_Approval" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<cc1:Fiber_QC_Approval ID="Fiber_QC_Approval" runat="server" />
</asp:Content>
