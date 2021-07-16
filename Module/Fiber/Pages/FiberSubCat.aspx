<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="FiberSubCat.aspx.cs" Inherits="Module_Fiber_Pages_FiberSubCat" %>
<%@ Register Src="~/Module/Fiber/Controls/FiberSubCatControl.ascx" TagName="FiberSubCatControl1" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc1:FiberSubCatControl1 ID="FiberSubCat1" runat="server" />
</asp:Content>

