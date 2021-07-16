<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CommonMaster/admin.master" CodeFile="Fiber_LapDipSubmission.aspx.cs" Inherits="Module_OrderDevelopment_Fiber_Lap_Dip_Pages_Fiber_LapDipSubmission" %>
<%@ Register src="../Controls/Fib_LabDipSubmission.ascx" tagname="Fib_LabDipSubmission" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Fib_LabDipSubmission ID="Fib_LabDipSubmission" runat="server" />
</asp:Content>
