<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Fabric_Issue_Against_PI.aspx.cs" Inherits="Module_Fabric_FabricSaleWork_Pages_Fabric_Issue_Against_PI" %>

<%@ Register src="../Controls/Fabric_Issue_Against_PI.ascx" tagname="Fabric_Issue_Against_PI" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
   
    <table >
        <tr>
            <td align="left" valign="top">
    <uc1:Fabric_Issue_Against_PI ID="Fabric_Issue_Against_PI1" runat="server" />
     </td>
        </tr>
    </table>
</asp:Content>


