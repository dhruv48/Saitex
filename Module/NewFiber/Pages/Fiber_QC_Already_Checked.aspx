<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CommonMaster/admin.master" CodeFile="Fiber_QC_Already_Checked.aspx.cs" Inherits="Module_NewFiber_Pages_Fiber_QC_Already_Checked" %>


<%@ Register Src="~/Module/NewFiber/Controls/fiber_QC_Already_Checked.ascx"
    TagName="Yarn_QC_Already_Checked" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <cc1:fiber_QC_Already_Checked ID="fiber_QC_Already_Checked1" runat="server" />
</asp:Content>