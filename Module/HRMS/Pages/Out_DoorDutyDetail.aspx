<%@ Page Language="C#" MasterPageFile="~/CommonMaster/EMPMaster.master" AutoEventWireup="true" CodeFile="Out_DoorDutyDetail.aspx.cs" Inherits="Module_HRMS_Pages_Out_DoorDutyDetail" Title="Out-Door Duty Detail" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

 <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
<table border="1" cellpadding="3" cellspacing="0" width="100%" class="tContent">
         <tr>
            <td width="100%" align="center" class="TableHeader"><b class="titleheading">Out-Door Duty Detail </b> </td>
         </tr>
         <tr>
            <td>
            <asp:GridView ID="gvReportDisplayGrid"   runat="server" AllowPaging="True"   PagerSettings-Position="Bottom"   
                    AutoGenerateColumns="False" PagerSettings-Mode="Numeric" CssClass="smallfont"
                    PagerStyle-HorizontalAlign="Left" onpageindexchanging="gvReportDisplayGrid_PageIndexChanging"                   
                    Font-Size="X-Small"  CaptionAlign="Top"  ForeColor="#333333" GridLines="None"  DataKeyNames="OD_ID" EmptyDataText="There is no record for approval" 
                    PageSize="10">  
                    <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                    <Columns>                   
                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="40px">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>                           
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Apply Date" HeaderStyle-HorizontalAlign="Left" DataField="APPLIEDDATE" ItemStyle-HorizontalAlign="Center"  >                      
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Employee Name">
                            <ItemTemplate>
                                     <asp:Label ID="lblEmployeeId" Text='<%#Eval("EMPNAME") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="120px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Dept." HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                     <asp:Label ID="LblDept" Text='<%#Eval("DEPARTMENT") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Duty From" HeaderStyle-HorizontalAlign="Center" DataField="FROM_DATE" ItemStyle-HorizontalAlign="Center"  >
                                        
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                        
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Duty To" HeaderStyle-HorizontalAlign="Center" DataField="TO_DATE" ItemStyle-HorizontalAlign="Center"  >
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                            </asp:BoundField>
                             <asp:BoundField HeaderText="Days" DataField="DAYS" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"  >            
                                 <HeaderStyle HorizontalAlign="Center" />
                                 <ItemStyle HorizontalAlign="Center" Width="35px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="From" HeaderStyle-HorizontalAlign="Center" DataField="ON_FROM" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="80" >
                                                                          
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                                                                          
                        </asp:BoundField>
                        <asp:BoundField HeaderText="To" HeaderStyle-HorizontalAlign="Center" DataField="ON_TO" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="80" >
                              <HeaderStyle HorizontalAlign="Center" />
                              <ItemStyle HorizontalAlign="Left" Width="80px" />
                              </asp:BoundField>
                         <asp:BoundField HeaderText="Place" HeaderStyle-HorizontalAlign="Center" DataField="PLACE" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="80" >
                                                
                             <HeaderStyle HorizontalAlign="Center" />
                             <ItemStyle HorizontalAlign="Left" Width="80px" />
                                                
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Status" HeaderStyle-HorizontalAlign="Center" DataField="OD_status" ItemStyle-HorizontalAlign="Center"  HtmlEncode="false" >
                                                         
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                         
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Request Print" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a target="_blank" href="Out_Door_DutyRpt.aspx?EmpCode=<%# Eval("EMP_CODE") %>&OD_ID=<%# Eval("OD_ID") %>"><b>Print</b></a>
                                        </ItemTemplate>                                       
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                        </asp:TemplateField>
                    </Columns>
 <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#EFF3FB" />
         <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#507CD1" 
            ForeColor="White" Font-Bold="True" />
                </asp:GridView>
             </td>
        </tr>
    </table>    
 </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>


