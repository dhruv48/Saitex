<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Employe_Variable_trn.ascx.cs" Inherits="Module_HRMS_Controls_Employe_Variable_trn" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.ComboBox" Assembly="obout_ComboBox" %>
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;}
    .header
    {
        margin-left: 4px;
    }
    .c1
    {
        width: 80px;
    }
    .c2
    {
        margin-left: 4px;
        width: 150px;
    }
    .c3
    {
        margin-left: 4px;
        width: 80px;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
<table class="tContentArial" width="100%">
    <tr>
        <td class="tdRight" >
            <table class="tdLeft">
                            <tr>
                                <td id="tdSave" valign="top" align="center" runat="server">
                                    <asp:ImageButton ID="imgbtnSave" TabIndex="9" OnClick="imgbtnSave_Click" runat="server"
                                        ImageUrl="~/CommonImages/save.jpg" ToolTip="Save" Height="41" Width="48" ValidationGroup="M1">
                                    </asp:ImageButton>
                                </td>
                                <td id="tdUpdate" valign="top" align="center" runat="server">
                                    <asp:ImageButton ID="imgbtnUpdate" TabIndex="9" OnClick="imgbtnUpdate_Click" runat="server"
                                        ImageUrl="~/CommonImages/edit1.jpg" ToolTip="Update" Height="41" Width="48" ValidationGroup="M1">
                                    </asp:ImageButton>
                                </td>                               
                                <td valign="top" align="center">
                                    <asp:ImageButton ID="imgbtnFind" TabIndex="9"  runat="server"
                                        ImageUrl="~/CommonImages/link_find.png" ToolTip="Find" Height="41" Width="48">
                                    </asp:ImageButton>
                                </td>
                                <td valign="top" align="center">
                                    <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                                        ToolTip="Print" Height="41" Width="48"></asp:ImageButton>
                                </td>
                                <td valign="top" align="center">
                                    <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                        ToolTip="Clear" Height="41" Width="48" onclick="imgbtnClear_Click"></asp:ImageButton>
                                </td>
                                <td valign="top" align="center">
                                    <asp:ImageButton ID="imgbtnExit" OnClick="imgbtnExit_Click" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                                        ToolTip="Exit" Height="41" Width="48"></asp:ImageButton>
                                </td>
                                <td valign="top" align="center">
                                    <asp:ImageButton ID="imgbtnHelp"  runat="server" ImageUrl="~/CommonImages/link_help.png"
                                        ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                                </td>
                            </tr>
                        </table>
        </td>
    </tr>
 <tr> 
    <td class="tdLeft">    
        <table class="td tContent" width="100%">
                <tr>
        <td class="TableHeader td" align="center" colspan = "5">
            <span class="titleheading">Employee Variable Transaction</span>
            </td>
    </tr>
                <tr>
        <td align="left" valign="top" class="td" colspan="5">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>
                <tr><td class="tdRight">Month :</td><td class="tdLeft">  
                            <asp:TextBox ID="TxtMonthName" Enabled="false"  CssClass="SmallFont TextBox readonly" Width="178px" runat="server"></asp:TextBox>
                    </td>
                    <td></td>
                    <td class="tdRight">Year :</td><td class="tdLeft">    
                        <asp:TextBox ID="TxtYear" Enabled ="false" CssClass="SmallFont TextBox readonly" Width="178px" runat="server"></asp:TextBox>
                    </td></tr>
                <tr>
             <td class="tdRight" > Head Name :</td>
             <td>
                 <asp:DropDownList ID="DDLSalryHeadMaster" CssClass="SmallFont TextBox" 
                     Width="181px" runat="server" AutoPostBack="True" 
                     onselectedindexchanged="DDLSalryHeadMaster_SelectedIndexChanged">
                 </asp:DropDownList>
             </td>
             <td></td> <td class="tdRight">SubHead Name:</td>
             <td>
                   <asp:DropDownList ID="DDLSalrySubHeadMaster" CssClass="SmallFont TextBox" 
                       Width="181px" runat="server" AutoPostBack="True" 
                       onselectedindexchanged="DDLSalrySubHeadMaster_SelectedIndexChanged">
                 </asp:DropDownList>
             </td>
 </tr>
 <tr>
    <td colspan="6" style="text-align: center">
        <asp:Button ID="CmdViewRecord" runat="server" CssClass="SmallFont TextBox"  Text="View Record" 
            onclick="CmdViewRecord_Click" /></td>
 </tr>
    </table></td>
    </tr>
  <tr><td  class="TdBackVir">Variable Income/Deduction Detail :-</td></tr>

     <tr>
                    <td align="left" class="td" valign="top" width="100%">
                        <table width="85%">
                            <tr bgcolor="#006699">
                                <td class="tdLeft SmallFont">
                                    <span class="titleheading"><b>Employee</b></span>
                                </td>
                                <td class="tdLeft SmallFont">
                                    <span class="titleheading"><b>Employee Code</b></span>
                                </td>                             
                                <td class="tdLeft SmallFont">
                                    <span class="titleheading"><b>Department </b></span>
                                </td>
                                <td class="tdRight SmallFont">
                                    <span class="titleheading"><b>Amount</b></span>
                                </td>
                                <td class="tdLeft SmallFont">
                                    <span class="titleheading"><b>Remarks </b></span>
                                </td> 
                                <td></td>  
                            </tr>
                            <tr>
                                <td align="left" valign="top">
                  <obout:ComboBox runat="server" ID="DDLEmployee" EnableVirtualScrolling="true" Width="150px"
                        Height="200px" DataTextField="EMPLOYEENAME" CssClass="SmallFont TextBox UpperCase"
                        DataValueField="EMP_CODE" EnableLoadOnDemand="true" OnLoadingItems="DDLEmployee_LoadingItems"
                        AutoPostBack="True"       MenuWidth="300px" 
                                        onselectedindexchanged="DDLEmployee_SelectedIndexChanged1">
                        <HeaderTemplate>
                            <div class="header c1">
                                Emp Code</div>
                            <div class="header c2">
                                Employee Name</div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="item c1">
                                <%# Eval("EMP_CODE")%></div>
                            <div class="item c2">
                                <%# Eval("EMPLOYEENAME")%></div>
                        </ItemTemplate>
                        <FooterTemplate>
                            Displaying items
                            <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                            out of
                            <%# Container.ItemsCount %>.
                        </FooterTemplate>
                    </obout:ComboBox>
                                </td>                                
                                
                                 <td align="left" valign="top">
                                    <asp:TextBox ID="TxtEmpCode" runat="server" ReadOnly="true" CssClass="TextBox SmallFont TextBoxDisplay" Width="180px"></asp:TextBox>
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="TxtDepartment" runat="server" ReadOnly="true" CssClass="TextBox SmallFont TextBoxDisplay" Width="180px"></asp:TextBox>
                                </td>
                                <td align="right" valign="top">
                                    <asp:TextBox ID="TxtAmount" runat="server"  CssClass="TextBoxNo SmallFont " onKeyUp="filterNonNumeric(this)"  Width="60px"></asp:TextBox>
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="TxtRemarks" runat="server" CssClass="TextBox SmallFont" Width="200px" ></asp:TextBox>
                                </td>
                                <td align="left" valign="top">
                                       <asp:LinkButton ID="lbtnsavedetail" runat="server" TabIndex="7" 
                                            onclick="lbtnsavedetail_Click">Save</asp:LinkButton>&nbsp;/&nbsp;
                                        <asp:LinkButton ID="lbtnCancel" runat="server" TabIndex="8" 
                                            onclick="lbtnCancel_Click">Cancel</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </td>
     </tr>
  <tr id="trGridView" runat="server">
                    <td class="td" align="left" width="100%">
                        <asp:Panel ID="pnlGrid" runat="server" Height="250px" ScrollBars="Vertical" Width="100%">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GridViewVariableTRN" runat="server" AutoGenerateColumns="False" 
                                                     AllowSorting="True" ShowFooter="true"  
                                        Font-Size="X-Small" CellPadding="3"   
                                                    GridLines="Both" Width="100%" ForeColor="#333333" 
                                                   CssClass = "smallfont"  OnRowCommand="GridViewVariableTRN_RowCommand" 
                                        EmptyDataText="No Record Found"  >
                                       <FooterStyle  BackColor="#507CD1"  ForeColor="White" Font-Bold="True" />
                <RowStyle BackColor="#EFF3FB" />
                <EmptyDataRowStyle Font-Bold="True" Font-Names="Annabel Script" Font-Size="Medium" />
                                        <Columns>
                                                <asp:TemplateField HeaderText="S.No." HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="top">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" Width="3%" />                                                
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Code">
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="LblEmp_Code"  runat="server" CssClass="Label SmallFont" Text='<%# Bind("EMP_CODE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name">
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="18%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="LblEmpName" runat="server" Text='<%# Bind("EMPLOYEE") %>' CssClass="Label SmallFont"></asp:Label>                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Department">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="12%"></ItemStyle>
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                 <FooterTemplate>
                                                    <b>Total Amount:</b>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="LblDepartment" runat="server" Text='<%# Bind("DEPARTMENT") %>' CssClass="Label SmallFont"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                             <asp:TemplateField HeaderText="Head Master">
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="LblHeadMaster" runat="server" Text='<%# Bind("HEAD_NAME") %>' CssClass="Label SmallFont"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Sub Head Master">
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="12%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="LblSubHeadMaster" runat="server" Text='<%# Bind("SUBH_NAME") %>' CssClass="Label SmallFont"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Amount">
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="5%"></ItemStyle>
                                                <FooterTemplate>
                                                    <asp:Label ID="LblFooterAmount" runat="server" CssClass="LabelNo SmallFont"></asp:Label>
                                                </FooterTemplate>
                                                 <FooterStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="LblAmount" runat="server" Text='<%# Eval("TRN_AMOUNT", "{0:0.00}") %>'  CssClass="LabelNo SmallFont"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                          
                                            <asp:TemplateField HeaderText="Remarks">
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="15%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="LblRemark" runat="server" Text='<%# Bind("TRN_REMARKS") %>' CssClass="Label SmallFont"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false"  >                                               
                                                     <ItemTemplate>
                                                            <asp:Label ID="LblHead_ID" runat="server" Text='<%# Bind("HEAD_ID") %>' CssClass="TextBox SmallFont"></asp:Label>
                                                            <asp:Label ID="LblSubhHeadID" runat="server" Text='<%# Bind("SUBH_HEAD_ID") %>' CssClass="TextBox SmallFont"></asp:Label>
                                                     </ItemTemplate>
                                          </asp:TemplateField>   
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                                                     <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="EmpEdit"  TabIndex="12" CommandArgument='<%# Eval("UniqueId") %>'></asp:LinkButton>&nbsp;/&nbsp;
                                                            <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CommandName="EmpDelete" TabIndex="12" OnClientClick="javascript: return confirm('Are you sure you want to delete this record?')" CommandArgument='<%# Eval("UniqueId") %>'></asp:LinkButton>
                                                     </ItemTemplate>
                                          </asp:TemplateField>   
                                        </Columns>
                                     <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle HorizontalAlign="Justify" BackColor="White" />
                                    </asp:GridView> 
                                      <asp:Label ID="Label2" runat="server" CssClass="Label SmallFont" Text="Amounts In Words :"></asp:Label>
                                    <asp:Label ID="lblAmountInWords"  CssClass="Label SmallFont" runat="server"></asp:Label>                                 
                                </ContentTemplate>                              
                            </asp:UpdatePanel>
                        </asp:Panel>                        
                    </td>
                </tr>
 
</table>
    
     </ContentTemplate>
</asp:UpdatePanel>