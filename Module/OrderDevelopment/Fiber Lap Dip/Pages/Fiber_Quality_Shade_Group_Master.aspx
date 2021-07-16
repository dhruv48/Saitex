<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Fiber_Quality_Shade_Group_Master.aspx.cs" Inherits="Module_OrderDevelopment_Fiber_Lap_Dip_Pages_Fiber_Quality_Shade_Group_Master" %>
<%@ Register src="../Controls/Fib_Quality_Shade_Group_Master.ascx" tagname="Quality_Shade_Group_Master" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Quality_Shade_Group_Master ID="Quality_Shade_Group_Master" runat="server" />
</asp:Content>
