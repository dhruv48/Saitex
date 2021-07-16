<%@ Page Language="C#" MasterPageFile="~/CommonMaster/UserMaster.master"  AutoEventWireup="true" CodeFile="CreateOpenMonthAndYear.aspx.cs" Inherits="Module_StartUp_CreateOpenMonthAndYear" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cphBody">
<script language="javascript" type="text/javascript">
function validate()
{
if (document.getElementById('<%=DDLOpenYear.ClientID%>').value=='0')
     {
               alert("Please select Open year");
               document.getElementById("<%=DDLOpenYear.ClientID%>").focus();
               return false;
     }  
     if (document.getElementById('<%=DDLOpenMonth.ClientID%>').value=='0')
     {
               alert("Please select Open month");
               document.getElementById("<%=DDLOpenMonth.ClientID%>").focus();
               return false;
     }  
      if (document.getElementById("<%=TxtFromDate.ClientID%>").value=="")
      {
                 alert("From Date Feild can not be blank");
                 document.getElementById("<%=TxtFromDate.ClientID%>").focus();
                 return false;
      }
      if(document.getElementById("<%=TxtToDate.ClientID %>").value=="")
      {
                 alert("To Date Feild can not be blank");
                document.getElementById("<%=TxtToDate.ClientID %>").focus();
                return false;
      }   
      
     return true;
}
</script>

<asp:ScriptManager ID="scr" runat="server"></asp:ScriptManager>

<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
<table id="tblDesgMainTable" runat="server" cellspacing="0" cellpadding="0" align="Left" width="80%"    class="tContentArial">
    <tr>
        <td align="Right" class="td">
            <table align="left">
                <tr>
                    <td id="tdSave" runat="server">
                        <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="Save" ImageUrl="~/CommonImages/save.jpg"
                            Width="48" Height="41" ValidationGroup="M1" OnClientClick="return validate()" onclick="imgbtnSave_Click" />
                    </td>
                    <td id="tdUpdate" runat="server">
                         </td>
                    <td id="tdDelete" runat="server">
                       </td>
                    <td id="tdFind" runat="server">
                        </td>
                    <td>
                     
                    </td>
                    <td>
                     
                    </td>
                    <td>
                    
                    </td>
                    <td>
                     
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader td">
            <span class="titleheading">Payroll Parameters Master</span>
        </td>
    </tr>
    <tr>
        <td align="left" valign="top" class="td">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>
    <tr>
        <td class="td">
            <table>
                <tr>
                    <td align="right" valign="top">
                        Master Code:
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="TxtMasterCode" Width="150px" CssClass="SmallFont TextBox UpperCase"  Text="New" ReadOnly="true" runat="server"></asp:TextBox>
                       
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top">
                        Open Year:
                    </td>
                    <td align="left" valign="top">
                        <asp:DropDownList ID="DDLOpenYear" Width="154px" CssClass="SmallFont TextBox UpperCase" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top">
                        Open Month:
                    </td>
                    <td align="left" valign="top">
                       <asp:DropDownList ID="DDLOpenMonth" Width="154px" CssClass="SmallFont TextBox UpperCase"  runat="server">
                         <asp:ListItem Value="0">------------Select-------------</asp:ListItem>
                                <asp:ListItem Value="1"> January </asp:ListItem>
                                <asp:ListItem Value="2"> February </asp:ListItem>
                                <asp:ListItem Value="3"> March </asp:ListItem>
                                <asp:ListItem Value="4"> April </asp:ListItem>
                                <asp:ListItem Value="5"> May </asp:ListItem>
                                <asp:ListItem Value="6"> June </asp:ListItem>
                                <asp:ListItem Value="7"> July </asp:ListItem>
                                <asp:ListItem Value="8"> August </asp:ListItem>
                                <asp:ListItem Value="9"> September </asp:ListItem>
                                <asp:ListItem Value="10"> October </asp:ListItem>
                                <asp:ListItem Value="11"> November </asp:ListItem>
                                <asp:ListItem Value="12"> December </asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top">
                        Salary FromDate:
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="TxtFromDate" CssClass="SmallFont TextBox UpperCase" Width="150px" runat="server"></asp:TextBox>
                         <cc1:CalendarExtender ID="FromDate" runat="server" Format="dd/MM/yyyy" TargetControlID="TxtFromDate">
                            </cc1:CalendarExtender>  
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top">
                        Salary ToDate:
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="TxtToDate" CssClass="SmallFont TextBox UpperCase"  Width="150px" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="ToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="TxtToDate">
                            </cc1:CalendarExtender> 
                    </td>
                </tr>
                 <tr>
                    <td align="right" valign="top">
                        Month Status:
                        </td>
                    <td align="left" valign="top">
                        <asp:CheckBox ID="ChkActive" runat="server" />
                    </td>
                </tr>
                
            </table>
        </td>
    </tr>
    <tr><td align="center" class="TableHeader td" ><span class="titleheading">Payroll Parameters Master Detail</span></td></tr>
     <tr><td colspan="2">
         <asp:GridView ID="GridViewMasterRecord" CssClass="SmallFont" Width="100%"  AutoGenerateColumns="False" AllowPaging="true" PageSize="12" GridLines="Horizontal"   runat="server">
             <Columns>
                                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="top">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Top" Width="40px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Open Year">
                                                    <ItemStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblOpenYear" runat="server" Text='<%# Bind("OPEN_YEAR") %>'></asp:Label>
                                                    </ItemTemplate>                                                     
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Open Month">
                                                    <ItemStyle HorizontalAlign="Left" Width="80px" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblOpenMonth" runat="server" Text='<%# Bind("OPEN_MONTH") %>' CssClass="Label SmallFont"></asp:Label>
                                                       
                                                    </ItemTemplate>                                                
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Salary FromDate">
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblFromDate" runat="server" Text='<%# Eval("SALARY_FROMDATE","{0:d}") %>' CssClass="Label SmallFont" Width="50px"></asp:Label>
                                                    </ItemTemplate>                                                    
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Salary ToDate">
                                                    <ItemStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblTodate" runat="server" Text='<%# Eval("SALARY_TODATE","{0:d}") %>' CssClass="Label SmallFont" Width="50px"></asp:Label>
                                                    </ItemTemplate>                                                  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblStatus" runat="server" Text='<%# Bind("ISACTIVE") %>'
                                                            CssClass="Label SmallFont" Width="100px"></asp:Label>
                                                    </ItemTemplate>                                                  
                                                </asp:TemplateField>                                                
                                                <asp:TemplateField Visible="false">                                                  
                                                    <ItemTemplate>
                                                    <asp:HiddenField ID="LblCode" runat="server" Value='<%# Eval("PMASTER_CODE") %>' />
                                                    </ItemTemplate>                                                    
                                                </asp:TemplateField>                                              
                                                
                                            </Columns>
                 <FooterStyle Width="100%" BackColor="#507CD1"  ForeColor="White" Font-Bold="True" />
                <RowStyle BackColor="#EFF3FB" />
                <EmptyDataRowStyle Font-Bold="True" Font-Names="Annabel Script" Font-Size="Medium" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle HorizontalAlign="Justify" BackColor="White" />
         </asp:GridView>
     </td></tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
                                
</asp:Content>
