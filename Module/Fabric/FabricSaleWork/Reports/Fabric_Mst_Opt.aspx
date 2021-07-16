<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Fabric_Mst_Opt.aspx.cs" Inherits="Module_Inventory_Reports_Fabric_Mst_Opt" Title="Untitled Page" %>


<%@ Register src="../Controls/Fabric_Mst_Opt.ascx" tagname="Fabric_Mst_Opt" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
   
   <table align="left" class="tContentArial">
                <tr>
                    <td align="center" class="td">
                        <table align="left" class="tContentArial">
                            <tr>
                                <td align="center" class="TableHeader" colspan="2">
                                    <span style="font-size: 13pt" class="titleheading"><strong>&nbsp;Fabric Master report</strong> </span>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    &nbsp;Fabric Code
                                </td>
                                <td align="left">
                                <asp:DropDownList ID="txtItemCodeRpt" runat="server"></asp:DropDownList>
                                    <%--<asp:TextBox ID="txtItemCodeRpt" runat="server" Width="150px"></asp:TextBox>--%>
                                </td>
                       
                           
                                <td align="left">
                                    &nbsp; Fabric Type</td>
                                <td align="left">
                                <asp:DropDownList ID="txtCatCodeRpt" runat="server"  ></asp:DropDownList>
                                    <%--<asp:TextBox ID="txtCatCodeRpt" runat="server" Width="150px"></asp:TextBox>--%>
                                </td>
                            </tr>
                            <tr>
                            <td>
                            Branch Code:
                            </td>
                            <td>
                            <asp:DropDownList ID="txtBranchCode" runat="server" 
                                    DataTextField="BRANCH_NAME" DataValueField="BRANCH_CODE"></asp:DropDownList>
                           <%-- <asp:TextBox ID="txtBranchCode" runat="server" Width="150px"></asp:TextBox>--%></td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    &nbsp;<asp:Button ID="btnGetReport" runat="server" OnClick="btnGetReport_Click" Text="Get Report" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
   
</asp:Content>

