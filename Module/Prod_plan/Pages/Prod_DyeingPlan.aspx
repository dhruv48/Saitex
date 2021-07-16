<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Prod_DyeingPlan.aspx.cs" Inherits="Module_Prod_plan_Pages_Prod_DyeingPlan" %>

<%@ Register Src="~/Module/Prod_plan/Controls/Prod_DyeingPlan.ascx" TagName="Prod_DyeingPlan"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:Prod_DyeingPlan ID="Prod_DyeingPlan" runat="server" />
</asp:Content>
