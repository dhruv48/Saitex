<%@ Page Language="C#" MasterPageFile="~/CommonMaster/EMPMaster.master" AutoEventWireup="true" CodeFile="Employee_Request_for_Leave.aspx.cs" Inherits="Module_Hrms_Pages_Employee_Request_for_Leave" Title="Leaves For Approval" %>
<%@ Register assembly="obout_Interface" namespace="Obout.Interface" tagprefix="cc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

 <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
<table border="1" cellpadding="3" cellspacing="0" width="100%" class="tContent">
         <tr>
            <td width="100%" align="center" class="TableHeader"><b class="titleheading">  Leaves For Approval</b></td>
          </tr>
          <tr>
          <td width="100%" class="tdLeft">
            <asp:GridView ID="gvReportDisplayGrid"  runat="server" CssClass = "smallfont"  Font-Size="X-Small" AllowPaging="true" 
                    PageSize="10"   PagerSettings-Position="Bottom"   
                    AutoGenerateColumns="false" PagerSettings-Mode="Numeric"
                    PagerStyle-HorizontalAlign="Left" onpageindexchanging="gvReportDisplayGrid_PageIndexChanging" 
                    onselectedindexchanged="gvReportDisplayGrid_SelectedIndexChanged" 
                    DataKeyNames="LV_APP_ID" EmptyDataText="There is no record for approval" >
                    <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                    <Columns>
                     <asp:TemplateField ItemStyle-Width="50px">
                            <ItemTemplate>
                                <asp:CheckBox ID="ChkSelect" runat="server" />
                            </ItemTemplate>                           
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderTemplate>
                                <asp:CheckBox ID="ChkAll" AutoPostBack="true" runat="server" oncheckedchanged="ChkAll_CheckedChanged" />
                            </HeaderTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="30px">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>                           
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Apply Date" DataField="APPLIEDDATE" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100" >                  
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Emp.Code" Visible="false" >
                            <ItemTemplate>
                                     <asp:Label ID="lblEmployeeId" Text='<%#Eval("EMP_CODE") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                     <asp:Label ID="LblEmpName" Text='<%#Eval("EMPLOYEENAME") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Dept." HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                     <asp:Label ID="LblDept" Text='<%#Eval("DEPT_NAME") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Branch" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                     <asp:Label ID="LblBranch" Text='<%#Eval("BRANCH_NAME") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Leave From" DataField="From_Date" 
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" >
<ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Leave To" DataField="To_Date" 
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" >
<ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                        </asp:BoundField>
                         <asp:BoundField HeaderText="Leave Days" DataField="DAYS_LV" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="50" >            
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Purpose" DataField="LV_PURPOSE" 
                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150" >
<ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Status" DataField="leave_status" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60" HtmlEncode="false" ><ItemStyle HorizontalAlign="Center" ></ItemStyle>
                        </asp:BoundField>
                    </Columns> 
                     <FooterStyle Width="100%" BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <RowStyle BackColor="#EFF3FB" />
                        <EmptyDataRowStyle Font-Bold="True" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#507CD1"
                            ForeColor="White" Font-Bold="True" />
                </asp:GridView>
             </td>
        </tr>
    </table>
    <table>
        <tr>
        <td>Change Leave Status</td>
        <td>
             <asp:DropDownList ID="DDLStatus" runat="server">
                <asp:ListItem Value="0">---Select Status--</asp:ListItem>
                <asp:ListItem Value="A">Approve</asp:ListItem>              
                <asp:ListItem Value="R">Reject</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td><asp:Button ID="CmdSave" runat="server" Text="Update" onclick="CmdSave_Click" /></td>
        </tr>
    </table>
 </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>


