<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" ValidateRequest="false" EnableEventValidation="false" AutoEventWireup="true"
    CodeFile="Wo_Query_form.aspx.cs" Inherits="Module_WorkOrder_Queries_Wo_Query_form" %>

<%@ Register Src="../Controls/Work_order_entry.ascx" TagName="Work_order_entry" TagPrefix="uc1" %>
<%@ Register src="../Controls/WorkOrderQueryForm.ascx" tagname="WorkOrderQueryForm" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <uc2:WorkOrderQueryForm ID="WorkOrderQueryForm1" runat="server" />
</asp:Content>
