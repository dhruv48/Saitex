<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HR_APPROVAL.ascx.cs" Inherits="Module_HRMS_Controls_HR_APPROVAL" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="obout_Interface" namespace="Obout.Interface" tagprefix="cc2" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="Combo" %>
 
<style type="text/css">
    .td
    {
        text-align: left;
    }
    .style5
    {
        height: 26px;
        text-align: right;
        width: 133px;
    }
    .style6
    {
        text-align: right;
        width: 133px;
    }
    .titleheading
    {
        text-align: center;
    }
</style>
<table id="tblDesgMainTable" runat="server" cellspacing="0" cellpadding="0" align="Left" class="tContentArial">
    <tr>
        <td align="Right" class="td">
            <table align="left">
                <tr>                                         
                    <td id="tdSave" runat="server">
                        <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="Save" ImageUrl="~/CommonImages/save.jpg"
                            Width="55px" Height="40px"
                            ValidationGroup="M" onclick="imgbtnSave_Click1"  >
                        </asp:ImageButton>
                    </td>
                    <td id="tdUpdate" runat="server">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                            Width="48" Height="41" ValidationGroup="M" 
                            onclick="imgbtnUpdate_Click1"   >
                        </asp:ImageButton>
                    </td>
                    <td id="find" runat="server">
                        <asp:ImageButton ID="Imgbtnfind" runat="server" ToolTip="find" ImageUrl="~/CommonImages/link_find.png"
                            Width="48" Height="41" ValidationGroup="s" onclick="Imgbtnfind_Click"  >
                        </asp:ImageButton>
                    </td>
                    <td id="tdDelete" runat="server">
                        <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete" ImageUrl="~/CommonImages/del6.png"
                            Width="48" Height="41" onclick="imgbtnDelete_Click1" 
                           ></asp:ImageButton>
                    </td>                                     
                   
                    <td id="tdClear" runat="server">
                        <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                            Width="48" Height="41" onclick="imgbtnClear_Click" 
                           ></asp:ImageButton>
                    </td>
                    <td id="tdPrint" runat="server">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                            Width="48" Height="41" onclick="imgbtnPrint_Click"></asp:ImageButton>
                    </td>
                    <td id="tdExit" runat="server">
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                            Width="48" Height="41" onclick="imgbtnExit_Click1"  ></asp:ImageButton>
                    </td>
                    <td id="tdHelp" runat="server">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                            Width="48" Height="41" ></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
  
  <tr>
 
  
  <td  class="TableHeader td" align="center">
                                <span  class="titleheading">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Employee Advance Request(HOD 
                                Approval)</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
  
  </tr>
    <tr>
        <td align="left" valign="top" class ="td">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>     
    <tr>
        <td align="left" valign="top" class ="td">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                ShowMessageBox="True" ShowSummary="False" ValidationGroup="M" />
        </td>
    </tr>     
    <tr>
        <td align="left" class ="td">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                ControlToValidate="TxtAmountRequested" Display="None" 
                ErrorMessage="Please Enter Amount" ValidationGroup="M"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                ControlToValidate="TxtNoofInstallments" Display="None" 
                ErrorMessage="Pls Enter No of Installments" ValidationGroup="M"></asp:RequiredFieldValidator>
        </td>
    </tr>     
    <tr>
        <td align="left" class ="td">
            <asp:Label ID="Lblempdtl" runat="server" ForeColor="#333333" 
                Text="Employee Details"></asp:Label>
        </td>
    </tr>     
    <tr>
        <td class ="td">
            <table border="0" cellpadding="3" cellspacing="0" align="left" width="750">
                  <tr>
                           <td id = "xyz" style="text-align: right" runat ="server" >
                                     
                            Select Employee:</td>                      
                           <td>                          
                           <Combo:ComboBox ID="Cmbfind" runat="server" Width="200px" 
                                EmptyText="------------Find------------" Height="150px" AutoPostBack="True" 
                                                 MenuWidth="300px" DataValueField="EMP_CODE" onselectedindexchanged="Cmbfind_SelectedIndexChanged" 
                                                    >
                                                <HeaderTemplate>
                                                    <div class="header c3">
                                                        Code
                                                    </div>
                                                    <div class="header c4">
                                                        Emp Name</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div class="item c3">
                                                        <%# Eval("EMP_CODE")%></div>
                                                    <div class="item c4">
                                                        <%# Eval("EMPLOYEENAME")%></div>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Displaying items
                                                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                    out of
                                                    <%# Container.ItemsCount %>.
                                                </FooterTemplate>
                                            </Combo:ComboBox>
                           </td>    
                   </tr>
                   <tr>
                        <td style="text-align: right" >              
                            Advance Application No :</td>
                        <td >
                             <asp:TextBox ID="TxtApplicationNo" runat="server" CssClass="TextBoxNo"></asp:TextBox>
                       
                        </td>
                        <td ></td>
                        <td class="style5">Employee Name :</td>
                        <td >
                           
                            <Combo:ComboBox ID="cmbEmpCode" runat="server" Width="200px" 
                                EmptyText="------------Select------------" Height="150px" AutoPostBack="True" 
                                                 MenuWidth="300px" DataValueField="EMP_CODE" onselectedindexchanged="cmbEmpCode_SelectedIndexChanged" 
                                                    >
                                                <HeaderTemplate>
                                                    <div class="header c3">
                                                        Code
                                                    </div>
                                                    <div class="header c4">
                                                        Emp Name</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div class="item c3">
                                                        <%# Eval("EMP_CODE")%></div>
                                                    <div class="item c4">
                                                        <%# Eval("EMPLOYEENAME")%></div>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Displaying items
                                                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                    out of
                                                    <%# Container.ItemsCount %>.
                                                </FooterTemplate>
                                            </Combo:ComboBox>
                       
                        </td>
                   </tr>
                   <tr>
                        <td style="text-align: right" >Department :</td>
                        <td valign="top">
                              <asp:TextBox ID="txtdept" runat="server" CssClass="TextBox"></asp:TextBox>
                        </td>
                        <td >
                            &nbsp;</td>
                        <td class="style5">
                            
                   
                            Designation :</td>
                        <td >
                       
                            <asp:TextBox ID="txtdesig" runat="server" CssClass="TextBox"></asp:TextBox>
                        </td>
                   </tr>
                   <tr>
                        <td style="text-align: right" >
                       
                            &nbsp;Position :</td>
                        <td>
                             <asp:TextBox ID="txtposition" runat="server" CssClass="TextBox" 
                                 ></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td class="style6">
                          
                            Branch : 
                           
                            </td>
                            <td>
                            
                                 <asp:TextBox ID="txtbranch" runat="server" CssClass="TextBox"></asp:TextBox>
                        </td>
                   </tr>              
                   <tr>
                   <td style="text-align: right" >Grade :</td>
                   <td >
                     
                                             <asp:TextBox ID="txtgrade" runat="server" CssClass="TextBox"></asp:TextBox>
                           </td>
                   <td ></td>
                           <td class="style5">
                               Lavel :</td>
                   <td >
                      
                                 <asp:TextBox ID="txtlevel" runat="server" CssClass="TextBox"></asp:TextBox>
                           </td>
                   </tr>
                   <tr>
                   <td colspan="5" align="center" class="td">
                           <asp:Label ID="lbladvancedtl" runat="server" 
                               ForeColor="#333333" Text="Advance Details"></asp:Label>
                           </td>
                   </tr>
                   <tr>
                        <td style="text-align: right" >              
                            Advance Apply Date :</td>
                        <td >
                             <asp:TextBox ID="TxtApplyDate" runat="server" CssClass="TextBox"></asp:TextBox>
                       
                        </td>
                        <td ></td>
                        <td class="style5">Amount Requested :</td>
                        <td >
                            <asp:TextBox ID="TxtAmountRequested" runat="server" CssClass="TextBoxNo"></asp:TextBox>
                        </td>
                   </tr>
                   <tr>
                        <td style="text-align: right" >HOD Approval Date :</td>
                        <td >
                            <asp:TextBox ID="TxtHodApprovalDate" runat="server" CssClass="TextBox"></asp:TextBox>
                        </td>
                        <td >
                            &nbsp;</td>
                        <td class="style5">
                            
                   
                            Approved Amount by HOD :</td>
                        <td >
                       
                            <asp:TextBox ID="TxtAmtbyhod" runat="server" 
                                ontextchanged="TxtAmtbyhod_TextChanged" CssClass="TextBox"></asp:TextBox>
                        </td>
                   </tr>
                  <%# Eval("EMP_CODE")%>           
                   <tr>
                   <td style="text-align: right" >No of Installments :</td>
                   <td >
                       <asp:TextBox ID="TxtNoofInstallments" runat="server" CssClass="TextBoxNo"></asp:TextBox>
                           </td>
                   <td ></td>
                           <td class="style5">
                               &nbsp;</td>
                   <td >
                       &nbsp;</td>
                   </tr>
               <tr>  <td class ="td" colspan="5">Advance Taken Details</td></tr> 
                <tr><td colspan="5" class ="td">
                    <%# Eval("EMPLOYEENAME")%>
                        <asp:GridView ID="GridDeduction" runat="server" AutoGenerateColumns="False" 
                           AllowPaging="True"  PageSize="5" 
                        onrowcommand="GridDeduction_RowCommand" Width="100%">                                  
        <Columns>                      
        <asp:BoundField DataField="APPLICATION_NO" HeaderText="Application No" />  
        <asp:BoundField DataField="EMPLOYEENAME" HeaderText="Employee" />  
        <asp:BoundField DataField="AMOUNT_REQUESTED" HeaderText="Amount Requested" />  
        <asp:BoundField DataField="NO_OF_INSTALLMENTS" HeaderText="No_of_Installments" />  
        <asp:BoundField DataField="APPLY_DATE" HeaderText="Apply Date" />                                                                                          
        <asp:TemplateField HeaderText="Edit" ItemStyle-VerticalAlign="top">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgEdit" CommandArgument='<%# Eval("APPLICATION_NO") %>' CommandName="RecordEdit"
                                                    ImageUrl="~/CommonImages/edit1.jpg" runat="server" CssClass="ContolStyle"></asp:ImageButton>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top"></ItemStyle>
                                        </asp:TemplateField>                 
                          </Columns>                    
                         
                       </asp:GridView>
                       
                       </td>
                       
                        </tr>
                       </table>
       </td> 
     
  </tr> 
    <tr><td class ="td">
      <asp:GridView ID="GridView2" runat="server" AllowPaging="True" 
          onpageindexchanging="GridView2_PageIndexChanging" PageSize="5">
      </asp:GridView>
        
      </td></tr>
      </table>
  
