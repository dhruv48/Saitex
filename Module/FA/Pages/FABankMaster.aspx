<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="FABankMaster.aspx.cs" Inherits="Module_FA_Pages_FABankMaster" Title="Untitled Page" %>

<%@ Register src="../Controls/FABankMaster.ascx" tagname="FABankMaster" tagprefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
   <table>
   <tr>
   <td align="left" valign="top" >
    <uc1:FABankMaster ID="FABankMaster1" runat="server" />
    </td>
    </tr>
    </table> 
</asp:Content>

