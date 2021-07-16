<%@ Page Language="C#" MasterPageFile="~/CommonMaster/UserMaster.master" AutoEventWireup="true" CodeFile="Waste_MST_OPT.aspx.cs" Inherits="Module_Waste_Report_Waste_MST_OPT" Title="Untitled Page" %>


<asp:Content ID="Content1" runat="server" contentplaceholderid="cphBody">
 <asp:UpdatePanel ID="upnl" runat="server">
   <ContentTemplate>  
   <asp:ScriptManager ID=SCRID runat=server ></asp:ScriptManager>
   
            <table align="left" class="tContentArial">
                <tr>
        <td class="td" colspan="10">
            <table>
                
                <tr>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" 
                            ImageUrl="~/CommonImages/link_exit.png" onclick="imgbtnExit_Click" 
                            ToolTip="Exit" Width="48" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" 
                            ImageUrl="~/CommonImages/link_help.png" ToolTip="Help" Width="48" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
                <tr>
                    <td align="center" class="td">
                        <table align="left" class="tContentArial">
                            <tr>
                                <td align="center" class="TableHeader" colspan="6">
                                    <span style="font-size: 13pt" class="titleheading"><strong>Waste Master Master</strong> </span>
                                </td>
                            </tr>
                             <tr>
        <td  align ="right">
            Select Branch:
        </td>
        <td >
            <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="true" 
                CssClass="gCtrTxt " Font-Size="9"
               Width="160px" >
            </asp:DropDownList>
        </td>
       
        
        <td align ="right">
            Select Department:
        </td>
        <td >
            <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" 
                CssClass="gCtrTxt " Font-Size="9"
                 Width="160px" >
            </asp:DropDownList>
        </td>
   </tr>
   <tr>
  
        <td align ="right">
           Item Category:
        </td>
        <td  >
            <asp:DropDownList ID="ddlItemCate" runat="server" AutoPostBack="true" 
                CssClass="gCtrTxt " Font-Size="9"
                 Width="160px" >
            </asp:DropDownList>
        </td>
        <td  align ="right">
        <%--  Item Type:--%>
        </td>
        <td >
            <asp:DropDownList ID="ddlItemType" runat="server" AutoPostBack="true"  Visible="false"
                CssClass="gCtrTxt " Font-Size="9"
               Width="160px" >
            </asp:DropDownList>
        </td>
   
    </tr>
                            <tr>
                                <td align="center" colspan="6">
                                    <asp:Button ID="btnGetReport" runat="server" OnClick="btnGetReport_Click" Text="Get Report" CssClass="AButton" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br>
            </br>
              <br>
              </br>
                <br>
                </br>
                  <br>
                  </br>
                    <br>
                    </br>
                      <br>
                      </br>
                        <br>
                        </br>
                          <br>
                          </br>
                            <br>
                           
      </ContentTemplate>
   </asp:UpdatePanel>
                                
</asp:Content>
