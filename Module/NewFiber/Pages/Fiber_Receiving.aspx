<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master"  AutoEventWireup="true" CodeFile="Fiber_Receiving.aspx.cs" Inherits="Module_Fiber_Pages_Fiber_Receiving" %>
<%@ Register Src="~/Module/NewFiber/Controls/ReceivingPOcredit.ascx" TagName="ReceiptPOCredit" TagPrefix="uc1" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="cphBody">
<uc1:ReceiptPOCredit ID="ReceiptPOCredit1" runat="server" />
                                
</asp:Content>
