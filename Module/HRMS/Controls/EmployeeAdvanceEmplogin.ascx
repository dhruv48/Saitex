<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmployeeAdvanceEmplogin.ascx.cs" Inherits="Module_HRMS_Controls_EmployeeAdvanceApplicationScreen_employee_Login_" %>

<table class="td tContent" width="100%">
  <tr>
        <td class="tdRight" colspan = "5">
            <table class="tdLeft">
                <tr>                                         
                    <td id="tdSave" runat="server">
                        <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="Save" ImageUrl="~/CommonImages/save.jpg"
                            Width="55px" Height="40px" onclick="imgbtnSave_Click"
                            >
                        </asp:ImageButton>
                    </td>
                    <td id="tdUpdate" runat="server">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                            Width="48" Height="41" onclick="imgbtnUpdate_Click"  >
                        </asp:ImageButton>
                    </td>
                    
                    <td id="tdDelete" runat="server">
                        <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete" ImageUrl="~/CommonImages/del6.png"
                            Width="48" Height="41" 
                           ></asp:ImageButton>
                    </td>                                     
                    <td id="find" runat="server">
                        <asp:ImageButton ID="Imgbtnfind" runat="server" ToolTip="find" ImageUrl="~/CommonImages/link_find.png"
                            Width="48" Height="41"  >
                        </asp:ImageButton>
                    </td>
                    <td id="tdClear" runat="server">
                        <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                            Width="48" Height="41" onclick="imgbtnClear_Click" 
                           ></asp:ImageButton>
                    </td>
                    <td id="tdPrint" runat="server">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                            Width="48" Height="41"></asp:ImageButton>
                    </td>
                    <td id="tdExit" runat="server">
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                            Width="48" Height="41" onclick="imgbtnExit_Click"  ></asp:ImageButton>
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
  <td  class="TableHeader td" align="center" colspan = "5">
                                <span  class="titleheading">Employee Advance Request Application </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
  </td>  
  </tr>
  <tr>
        <td align="left" valign="top" class ="td" colspan = "5">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>   
  <tr>
        <td Class="TdBackVir" colspan = "5">
            <asp:Label ID="Lblempdtl" runat="server" ForeColor="#333333"  Text="Employee Details"></asp:Label>
                <asp:TextBox ID="TxtApplicationNo" runat="server" Visible="false"   
                CssClass="TextBox SmallFont" Width="5px">0</asp:TextBox>
        </td>
    </tr>   
  
 <tr>
 <td class="tdRight" > Employee Code :</td>
 <td class="tdLeft">
     <asp:TextBox ID="txtEmpCode" runat="server" CssClass="TextBox SmallFont"></asp:TextBox>
     </td>
 <td class="tdLeft"></td>
 <td class="tdRight" >Employee Name :</td>
 <td class="tdLeft">
     <asp:TextBox ID="txtEmpName" runat="server" CssClass="TextBox SmallFont"></asp:TextBox>
     </td>
 </tr>
 <tr>
 <td class="tdRight" >Department :</td>
 <td class="tdLeft">
     <asp:TextBox ID="txtDept" runat="server" CssClass="TextBox SmallFont"></asp:TextBox>
     </td>
 <td class="tdLeft"></td>
 <td class="tdRight" >Designation :</td>
 <td class="tdLeft">
     <asp:TextBox ID="txtDesig" runat="server" CssClass="TextBox SmallFont"></asp:TextBox>
     </td>
 </tr>
 <tr>
 <td class="tdRight">Position :</td>
 <td class="tdLeft">
     <asp:TextBox ID="txtposition" runat="server" CssClass="TextBox SmallFont"></asp:TextBox>
     </td>
 <td class="tdLeft"></td>
 <td class="tdRight" >Branch :</td>
 <td class="tdLeft">
     <asp:TextBox ID="txtBranch" runat="server" CssClass="TextBox SmallFont"></asp:TextBox>
     </td>
 </tr>
 <tr>
 <td class="tdRight" >Grade :</td>
 <td class="tdLeft">
     <asp:TextBox ID="txtGrade" runat="server" CssClass="TextBox SmallFont"></asp:TextBox>
     </td>
 <td class="tdLeft"></td>
 <td class="tdRight" >Lavel :</td>
 <td class="tdLeft">
     <asp:TextBox ID="txtlavel" runat="server" CssClass="TextBox SmallFont"></asp:TextBox>
     </td>
 </tr>
 <tr>
 <td colspan = "5" class = "TdBackVir">Advance Detail</td>
 </tr>
 <tr>
 <td  class="tdRight">Advance Date :</td><td class="tdLeft"> <asp:TextBox ID="txtAdvdate" runat="server" CssClass="TextBox SmallFont"></asp:TextBox></td>
 <td class="tdLeft"></td>
 <td  class="tdRight">Amount Applied :</td><td class="tdLeft"> <asp:TextBox ID="txtAmtAply" onKeyPress="return checkNumeric(event)" runat="server" CssClass = "TextBoxNo SmallFont"></asp:TextBox></td>
 </tr>
 <tr>
 <td  class="tdRight">Purpose :</td><td colspan="4"><asp:TextBox ID="txtpurpose" runat="server" CssClass="TextBox SmallFont" Width="88%"></asp:TextBox>
            </td>
 </tr>
 <tr>
 <td  colspan = "5" class = "TdBackVir">Advance Taken Details </td>
 </tr>
  <tr>
 <td  colspan = "5">
 <div style="width:100%">
     <asp:GridView ID="GridViewAdvanceTakenDetail" runat="server" AllowPaging="True" 
         AllowSorting="True" Font-Size="X-Small" PageSize="15" 
        CellPadding="3"   GridLines="Both" Width="100%" ForeColor="#333333" 
        CssClass = "smallfont" 
         AutoGenerateColumns="False" Visible = "true" 
         onrowcommand="GridViewAdvanceTakenDetail_RowCommand" 
         onpageindexchanging="GridViewAdvanceTakenDetail_PageIndexChanging" >
       <Columns>                      
        <asp:BoundField DataField="APPL_NO" HeaderText="Application No." ItemStyle-Width="3%" > <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" /> </asp:BoundField> 
        <asp:BoundField DataField="APPLY_DATE" HeaderText="Date" ItemStyle-Width="8%" DataFormatString ="{0:dd-MM-yyyy}" > <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Right" /> </asp:BoundField> 
        <asp:BoundField DataField="APPLY_AMOUNT" HeaderText="Amount" ItemStyle-Width="8%"> <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Right" /> </asp:BoundField> 
        <asp:BoundField DataField="PURPOSE" HeaderText="Purpose" ItemStyle-Width="15%"><HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Left" /> </asp:BoundField> 
         <asp:BoundField DataField="HR_APPROVED_AMT" HeaderText="Approved Amount" ItemStyle-Width="8%"><HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Right" /> </asp:BoundField>  
        <asp:BoundField DataField="NO_OF_INSTALMENT" HeaderText="No.of Installments" ItemStyle-Width="8%"> <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Right" /> </asp:BoundField>  
          <asp:BoundField DataField="DEDUCT_AMT" HeaderText="Deduction" ItemStyle-Width="8%"> <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Right" /> </asp:BoundField> 
        <asp:BoundField DataField="REMAIN_AMT" HeaderText="Balance Amount" ItemStyle-Width="8%" > <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Right" /> </asp:BoundField>
         <asp:BoundField DataField="APPROVED_STATUS" HeaderText="Status" HtmlEncode="false" ItemStyle-Width="12%" > <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Left" /> </asp:BoundField>  
          <asp:TemplateField HeaderText="Delete">
                <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                     <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="EmpEdit"  TabIndex="12" CommandArgument='<%# Eval("APPL_NO") %>'></asp:LinkButton>&nbsp;/&nbsp;
                            <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CommandName="EmpDelete" TabIndex="12" OnClientClick="javascript: return confirm('Are you sure you want to delete this record?')" CommandArgument='<%# Eval("APPL_NO") %>'></asp:LinkButton>
                     </ItemTemplate>
          </asp:TemplateField>                                                                                 
       </Columns>  
              <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle 
            HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#507CD1" 
            ForeColor="White" Font-Bold="True" />                 
     </asp:GridView>
    
     </div>
      </td>
 </tr> 
 </table> 