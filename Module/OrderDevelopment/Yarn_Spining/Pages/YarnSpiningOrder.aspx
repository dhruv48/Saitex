<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" MaintainScrollPositionOnPostback="true"
    AutoEventWireup="true" CodeFile="YarnSpiningOrder.aspx.cs" Inherits="Module_OrderDevelopment_Pages_YarnSpiningOrder"
    Title="Untitled Page" %>

<%@ Register Src="~/Module/OrderDevelopment/Yarn_Spining/Controls/OC_YARN_SPINING.ascx" TagName="OrderCap"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:OrderCap ID="OrderCap1" runat="server" />
</asp:Content>
