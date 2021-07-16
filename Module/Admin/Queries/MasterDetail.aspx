<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="MasterDetail.aspx.cs" Inherits="Module_Admin_Queries_MasterDetail" Title="Untitled Page" %>
<%@ Register Src="~/Module/Admin/Controls/MasterDetailQuery.ascx" TagName="MatserDetailQuery" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc1:MatserDetailQuery id="MasterDetailQuery1" runat="server" /> 
</asp:Content>



