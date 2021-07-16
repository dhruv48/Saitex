<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" ValidateRequest="false" EnableEventValidation="false" AutoEventWireup="true"
    CodeFile="OD_CAP_QRY.aspx.cs" Inherits="Module_OrderDevelopment_Queries_OD_CAP_QRY" %>


<%@ Register src="../Controls/CR_REQ_QRY.ascx" tagname="CR_REQ_QRY" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">

    <uc1:CR_REQ_QRY ID="CR_REQ_QRY1" runat="server" />

</asp:Content>
