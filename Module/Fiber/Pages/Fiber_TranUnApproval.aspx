<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Fiber_TranUnApproval.aspx.cs" Inherits="Module_Fiber_Pages_Fiber_TranUnApproval" %>
<%@ Register Src="~/Module/Fiber/Controls/Fiber_Receipt_UnApproval.ascx" TagName="MRNApproval" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc1:MRNApproval id = "MRN1Approval" runat="server" />
</asp:Content>

