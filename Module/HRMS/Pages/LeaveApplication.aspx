<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LeaveApplication.aspx.cs" MasterPageFile="~/CommonMaster/EMPMaster.master"  Inherits="Module_HRMS_Pages_LeaveApplication" Title="Leave Application" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">   
       
   <asp:UpdatePanel ID="UpdatePanel" runat="server">
    <ContentTemplate>                    
    <table align="left" width="100%" class="td tContent">
        <tr>
            <td class="td" valign="top">
                <table class="tContentArial tablebox" cellspacing="0" cellpadding="0" align="left" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td align="left" colspan="5">
                                <table class="tContentArial" cellspacing="0" width="30%" cellpadding="0" border="0"
                                    align="left">
                                    <tbody>
                                        <tr>
                                            <td id="tdSave" width="48" runat="server">
                                               
                                                <asp:ImageButton ID="imgbtnInsert" runat="server" ToolTip="Save" ImageUrl="~/CommonImages/save.jpg" Width="48" Height="41" ValidationGroup="M1"  OnClick="imgbtnInsert_Click" ></asp:ImageButton>
                                            </td>                                          
                                            <td width="48">
                                                <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                                                    Width="48" Height="41" OnClientClick="return confirm('Are you sure you want to Print?');" OnClick="imgbtnPrint_Click"></asp:ImageButton>
                                            </td>
                                              <td width="48">
                                                <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                                                    Width="48" Height="41" OnClientClick="return confirm('Are you sure you want to clear?');" OnClick="imgbtnClear_Click"></asp:ImageButton>
                                            </td>
                                            <td width="48">
                                                <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                                                    Width="48" Height="41" OnClientClick="return confirm('Are you sure you want to Exit?');" OnClick="imgbtnExit_Click"></asp:ImageButton>
                                            </td>
                                            <td width="48">
                                                <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"  Width="48" Height="41" ></asp:ImageButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <%--  <table border="0" cellpadding="3" cellspacing="0" class="tContentArial" width="100%" align="left">--%>
                        <tr>
                            <td colspan="2" width="100%" align="center" class="TableHeader td">
                                <span class="titleheading"><b>Leave Application</b></span>
                            </td>
                        </tr>                       
                        <tr>
                            <td colspan="3" style="width:50px;" align="right">
                                 &nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="width:100%">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%" align="left" class="tContentArial">
                                    <tr>
                                        <td  >
                                            Name :
                                        </td>
                                        <td  >                                            
                                            <asp:TextBox ID="txtEmpName" runat="server" ReadOnly ="true" Width="180px" CssClass="textbox SmallFont"></asp:TextBox>
                                        </td>
                                        <td  >
                                            Emp Code :
                                        </td>
                                        <td  >                                         
                                            <asp:TextBox ID="txtEmpCode" ReadOnly ="true" runat="server" Width="100px" CssClass="textbox SmallFont"></asp:TextBox>
                                        </td>
                                         <td >Date :</td><td><asp:TextBox ID="txtCurrentDate"  runat="server" Width="120px" ReadOnly="true" CssClass="textbox SmallFont"></asp:TextBox>
                            </td>
                                    </tr>                                   
                                </table>
                            </td>
                        </tr>                   
                        <tr>
                        <td colspan="2" class="tdRight">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%" align="left" class="tContentArial">
                               <tr><td align="center" > <span class="titleheading"><b>Assigned Leave </b></span></td></tr>
                            <tr> 
                                <td class="tdLeft" width="98%">
                                <asp:GridView ID="gvLeaveAssign" runat="server" Width="100%" AutoGenerateColumns="False" CssClass="smallfont" Font-Size="Small" EmptyDataText="There is no record found">
                                    <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Leave Name">
                                            <ItemTemplate>                                            
                                                <asp:CheckBox ID="chk_LeaveType" CssClass="SmallFont" runat="server"  Text='<%# Bind("LEAVENAME") %>' TabIndex="1"   oncheckedchanged="chk_LeaveType_CheckedChanged" AutoPostBack="True" />
                                                <asp:Label ID="lblLeaveMasterId"  Visible="false" CssClass="Label" runat="server"  Text='<%# Bind("LEAVEID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="145px" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Days">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtLeaveDaysGD" CssClass="textboxno SmallFont"  BackColor="White" ReadOnly ="true" runat ="server" Text='<%# Bind("LEAVEDAYS") %>' Width="45px" ></asp:TextBox>                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Rem. Days">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TxtRemaining" CssClass="textboxno SmallFont"  BackColor="White" ReadOnly ="true" runat ="server" Text='<%# Bind("RLEAVEDAYS") %>' Width="45px" ></asp:TextBox>                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField  HeaderText="Pending Days">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtLeavePending" CssClass="textboxno SmallFont"  BackColor="White" ReadOnly ="true" runat ="server" Text='<%# Bind("PENDING")%>' Width="45px" ></asp:TextBox>                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Leave Flag">
                                            <ItemTemplate> 
                                                <asp:DropDownList ID="DDLLeaveReq" CssClass="SmallFont" Width="80px" runat="server">
                                                <asp:ListItem Value="FU">FULL</asp:ListItem>
                                                 <asp:ListItem Value="FH">FIRST HALF</asp:ListItem>
                                                  <asp:ListItem Value="SH">SECOND HALF</asp:ListItem>
                                                </asp:DropDownList>  
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="FromDate">
                                            <ItemTemplate>
                                                    <asp:TextBox ID="txtDuration_From"  TabIndex="3"  runat="server" CssClass="textbox SmallFont" TextMode="singleLine" Width="100px"></asp:TextBox>
                                                     <ajaxToolkit:CalendarExtender ID="fromdt" Format="dd/MM/yyyy" TargetControlID="txtDuration_From" runat="server">
                                </ajaxToolkit:CalendarExtender> 
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="ToDate">
                                            <ItemTemplate>
                                                   <asp:TextBox ID="txtDuration_To"  runat="server" CssClass="textbox SmallFont"   
                                                       TextMode="singleLine"  TabIndex="4" Width="100px" AutoPostBack="True" 
                                                       ontextchanged="txtDuration_To_TextChanged"></asp:TextBox>
                     <ajaxToolkit:CalendarExtender ID="Todt" TargetControlID="txtDuration_To" Format="dd/MM/yyyy" runat="server">
                                </ajaxToolkit:CalendarExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Req. Days">
                                            <ItemTemplate>                                               
                                                <asp:TextBox ID="txtReqDay" runat ="server" CssClass="textboxno SmallFont" MaxLength="3" TabIndex="2" ReadOnly ="true"  onKeyPress="return IsFlot(this);" Width="45px"  AutoPostBack="True"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Remain">
                                            <ItemTemplate>                                               
                                                <asp:TextBox ID="txtRemain" BackColor="White" CssClass="textboxno SmallFont" ReadOnly ="true" runat ="server" Width="45px"  ></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       
                                         
                                    </Columns>
                                    <RowStyle CssClass="RowStyle SmallFont" />
                                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                                            <AlternatingRowStyle CssClass="AltRowStyle" />
                                            <PagerStyle CssClass="PagerStyle" />
                                            <HeaderStyle CssClass="HeaderStyle GrdHeader"  />
                                </asp:GridView>
                            </td>                            
                            </tr>
                            </table> 
                            </td>
                        </tr> 
                        <tr>
                            <td width="18%" style="text-align:right">
                                Purpose</td>
                            <td class="tdLeft" width="82%" >
                                <asp:TextBox ID="txtPurpose" runat="server" Width="300px" Height="50px" TabIndex="5"
                                    CssClass="textbox SmallFont" TextMode="MultiLine"></asp:TextBox>
                                <br />
                                </td>
                        </tr>                        
                    </tbody>
                </table>
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td>
            <table border="1" cellpadding="3" cellspacing="0" width="100%" class="td tContent">
           
         <tr>
            <td width="100%" align="center" class="TableHeader td"><span class="titleheading"><b> Leaves Detail</b></span>  </td>
         </tr>
         <tr>
            <td class="td">
            <asp:GridView ID="gvReportDisplayGrid"  runat="server" AllowPaging="true" 
                    PageSize="10" AutoGenerateColumns="False" Font-Size="X-Small" CellPadding="4"   Width="100%" ForeColor="#333333" CssClass = "smallfont" onpageindexchanging="gvReportDisplayGrid_PageIndexChanging" 
                    DataKeyNames="LV_APP_ID" EmptyDataText="There is no record found" onrowcommand="gvReportDisplayGrid_RowCommand">
                    <FooterStyle Width="100%" BackColor="#507CD1"  ForeColor="White" Font-Bold="True" />
                    <RowStyle BackColor="#EFF3FB" />
                    <EmptyDataRowStyle Font-Bold="True" Font-Names="Annabel Script" Font-Size="Medium" />
                    <Columns>                     
                        <asp:TemplateField HeaderText="SNo." HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="45px">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>                           
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Apply Date" HeaderStyle-HorizontalAlign="Center" DataField="APPLIEDDATE" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px" >                  
                        </asp:BoundField>                      
                         <asp:TemplateField HeaderText="Leave Name" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                     <asp:Label ID="LblLeaveName" Text='<%#Eval("LV_NAME") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="90px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Leave From" HeaderStyle-HorizontalAlign="Center" DataField="From_Date" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px" >

                        </asp:BoundField>
                        <asp:BoundField HeaderText="Leave To" HeaderStyle-HorizontalAlign="Center" DataField="To_Date" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px" >

                        </asp:BoundField>
                        <asp:BoundField HeaderText="Leave Days" DataField="DAYS_LV" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="50px" >            
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Purpose" HeaderStyle-HorizontalAlign="Center" DataField="LV_PURPOSE" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="220px" >

                        </asp:BoundField>
                        <asp:BoundField HeaderText="Status" HeaderStyle-HorizontalAlign="Center" DataField="leave_status"    ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="70" HtmlEncode="false" >
                        </asp:BoundField>
                        <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>  
                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Delete" CommandName="EmpDelete" Enabled='<%# Eval("leave_status").ToString()=="<b style=\"color:Green;\">Approved</b>" ? false:true %>'
                                                             CommandArgument='<%# Eval("LV_APP_ID") %>'></asp:LinkButton>                                     
                                             </ItemTemplate>
                                    </asp:TemplateField>
                        <asp:TemplateField HeaderText="Request Print" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a target="_blank"  href="../Reports/LeaveApplication.aspx?EmpCode=<%# Eval("EMP_CODE") %>&LV_APP_ID=<%# Eval("LV_APP_ID") %>"><b>Print</b></a>
                                        </ItemTemplate>                                       
                                    </asp:TemplateField>
                    </Columns>
                         <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <EditRowStyle BackColor="#2461BF" />
                         <AlternatingRowStyle HorizontalAlign="Justify"  VerticalAlign="Top" />
                </asp:GridView>
             </td>
        </tr>       
        </table>
            
            </td>
        </tr>
    </table> 
    
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
