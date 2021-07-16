<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserHeader.ascx.cs" Inherits="CommonControls_UserHeader" %>
<%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
--%><table width="98%" height="79" border="0" cellpadding="0" align="center" cellspacing="0">
    <tr>
        <td width="150px" align="right"  background="/Saitex/CommonImages/bgtop.jpg" bgcolor="#336799" valign="middle">
            <asp:Image ID="Image1" ImageUrl="~/CommonImages/TexAms.jpg" Width="300px" Height="60px"
                runat="server" />
               
        </td>
        <td align="right" valign="middle" background="/Saitex/CommonImages/bgtop.jpg" bgcolor="#336799">
            <table width="435" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="246">
                        &nbsp;
                    </td>
                    <td width="300" align="left" class="saitex" colspan="2" >
                      <table width="240" align="right">
                      <tr>
                      <td colspan="2" align="left" width="240"><div id="clock"></div></td>
                      <%--<td align="left"> 
</td>--%>
                      </tr>
                      <tr>
                      <td align="left" colspan="2">
                      Support: support@erptextiles.com
                      </td>
                      </tr>
                      </table>
                      
                     
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
