<%@ Page Language="C#" MasterPageFile="~/CommonMaster/EMPMaster.master" AutoEventWireup="true" CodeFile="ODApplovalForm.aspx.cs" Inherits="Module_HRMS_Pages_ODApplovalForm" Title="Out Door Duty Request" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">    
 <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
<table border="1" cellpadding="3" cellspacing="0" width="100%" class="tContent">
         <tr>
            <td width="100%" align="center" class="TableHeader"><b class="titleheading"> Out-Door Duty For Approval</b> </td>
        </tr>
        <tr>
            <td width="100%" class="tdLeft">
                  <asp:GridView ID="gvReportDisplayGrid"  runat="server" AllowPaging="True"   PagerSettings-Position="Bottom"   
                    AutoGenerateColumns="False" PagerSettings-Mode="Numeric" CssClass="smallfont" Width="100%"
                    PagerStyle-HorizontalAlign="Left" onpageindexchanging="gvReportDisplayGrid_PageIndexChanging" 
                    onselectedindexchanged="gvReportDisplayGrid_SelectedIndexChanged" Font-Size="X-Small" CaptionAlign="Top"  ForeColor="#333333" GridLines="Both" 
                    DataKeyNames="OD_ID" EmptyDataText="There is no record for approval">  
                    <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                    <Columns>
                     <asp:TemplateField ItemStyle-Width="2%">
                            <ItemTemplate>
                                <asp:CheckBox ID="ChkSelect" runat="server" />
                            </ItemTemplate> 
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderTemplate>
                                <asp:CheckBox ID="ChkAll" AutoPostBack="true" runat="server" oncheckedchanged="ChkAll_CheckedChanged" />
                            </HeaderTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SNo." HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>                           
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Apply Date" DataField="APPLIEDDATE" ItemStyle-HorizontalAlign="Center"  >                      
                            <ItemStyle HorizontalAlign="Center" Width="8%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Employee Name">
                            <ItemTemplate>
                                     <asp:Label ID="lblEmployeeId" Text='<%#Eval("EMPNAME") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="12%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Duty From">
                            <ItemTemplate>
                                     <asp:Label ID="lblFromDate" Text='<%#Eval("FROM_DATE") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="8%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="Duty To">
                            <ItemTemplate>
                                     <asp:Label ID="lblToDate" Text='<%#Eval("TO_DATE") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="8%" HorizontalAlign="Center" />
                        </asp:TemplateField>                     
                      
                        <asp:BoundField HeaderText="From" DataField="ON_FROM" ItemStyle-HorizontalAlign="Left"  >
                          
                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                          
                        </asp:BoundField>
                        <asp:BoundField HeaderText="To" DataField="ON_TO" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" >            
                        </asp:BoundField>
                         <asp:BoundField HeaderText="Days" DataField="DAYS" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="3%" >            
                        </asp:BoundField>
                         <asp:BoundField HeaderText="Place" DataField="PLACE" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left"  >
                                    <ItemStyle HorizontalAlign="Left" Width="20%" />                          
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Status" DataField="OD_status" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" >                                   
                            <ItemStyle HorizontalAlign="Center" Width="5%" />                                                            
                        </asp:BoundField>
                         <asp:TemplateField Visible="false" >
                             <ItemTemplate>
                                 <asp:Label id="lblEMP_CODE" runat ="server" text='<%# Eval("EMP_CODE")%>'></asp:Label>
                             </ItemTemplate>
                          </asp:TemplateField> 
                            <asp:TemplateField  Visible="false" >
                            <ItemTemplate  >  
                                <asp:Label id="LblShiftID" runat ="server" text='<%# Eval("SFT_ID")%>'></asp:Label>
                                <asp:Label id="LblSiftInTime" runat ="server" text='<%# Eval("SFT_IN_TIME")%>'></asp:Label>
                                <asp:Label id="LblSiftOutTime" runat ="server" text='<%# Eval("SFT_OUT_TIME")%>'></asp:Label>                                                      
                            </ItemTemplate>
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
    <table  class="tContent">
        <tr>
        <td>Change Leave Status</td>
        <td>
            <asp:DropDownList ID="DDLStatus" CssClass="SmallFont TextBox UpperCase" runat="server">
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
