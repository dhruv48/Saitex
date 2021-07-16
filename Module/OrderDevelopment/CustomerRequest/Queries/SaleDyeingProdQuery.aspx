<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="SaleDyeingProdQuery.aspx.cs" Inherits="Module_OrderDevelopment_CustomerRequest_Queries_SaleDyeingProdQuery" %>
<%@ Register src="../Controls/SaleDyeingProdQuery.ascx" tagname="SaleDyeingProdQuery" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:SaleDyeingProdQuery ID="SaleDyeingProdQuery" runat="server" />
</asp:Content>
