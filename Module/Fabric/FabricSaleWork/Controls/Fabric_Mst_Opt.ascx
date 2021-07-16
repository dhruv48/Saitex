<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Fabric_Mst_Opt.ascx.cs" Inherits="Module_Inventory_Controls_Fabric_Mst_Opt" %>
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
                                    <asp:TextBox ID="txtItemCodeRpt" runat="server" Width="150px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    &nbsp; Fabric Type</td>
                                <td align="left">
                                    <asp:TextBox ID="txtCatCodeRpt" runat="server" Width="150px"></asp:TextBox>
                                </td>
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
<p>
    &nbsp;</p>
