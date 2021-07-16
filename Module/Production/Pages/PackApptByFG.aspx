<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="PackApptByFG.aspx.cs" Inherits="Module_Production_Pages_PackApptByFG" %>
<%@ Register Src="~/Module/Production/Controls/PackApprByFG_YS.ascx"  TagName="PackApprByFG_YS" TagPrefix="uc1"%>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc1:PackApprByFG_YS ID="PackApprByFG_YS1" runat="server" />

</asp:Content>

