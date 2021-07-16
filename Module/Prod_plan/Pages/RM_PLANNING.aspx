<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="RM_PLANNING.aspx.cs" Inherits="Module_Prod_plan_Pages_RM_PLANNING" Title="Untitled Page" %>
<%@ Register Src="~/Module/Prod_plan/Controls/RM_PLANNING.ascx" TagName="RM_PLANNING"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc1:RM_PLANNING ID="RM_PLANNING" runat="server" />
</asp:Content>

