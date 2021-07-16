<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmployeeAdvanceHODApproval.ascx.cs" Inherits="Module_HRMS_Controls_EmployeeAdvanceRequestScreen_HOD_Approval_" %>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
<table id="tblDesgMainTable" runat="server" cellspacing="0" cellpadding="0" width = "796px"
  class="tContentArial td" >
  <tr>
        <td align="Right" class="td" colspan = "5" >
            <table align="left">
                <tr>                                         
                    <td id="tdSave" runat="server">
                        <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="Save" ImageUrl="~/CommonImages/save.jpg"
                            Width="55px" Height="40px"
                             >
                        </asp:ImageButton>
                    </td>
                    <td id="tdUpdate" runat="server">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                            Width="48" Height="41"   >
                        </asp:ImageButton>
                    </td>
                    
                    <td id="tdDelete" runat="server">
                        <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete" ImageUrl="~/CommonImages/del6.png"
                            Width="48" Height="41" 
                           ></asp:ImageButton>
                    </td>                                     
                    <td id="find" runat="server">
                        <asp:ImageButton ID="Imgbtnfind" runat="server" ToolTip="find" ImageUrl="~/CommonImages/link_find.png"
                            Width="48" Height="41"   >
                        </asp:ImageButton>
                    </td>
                    <td id="tdClear" runat="server">
                        <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                            Width="48" Height="41" onclick="imgbtnClear_Click"
                           ></asp:ImageButton>
                    </td>
                    <td id="tdPrint" runat="server">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                            Width="48" Height="41" ></asp:ImageButton>
                    </td>
                    <td id="tdExit" runat="server">
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                            Width="48" Height="41" onclick="imgbtnExit_Click"  ></asp:ImageButton>
                    </td>
                    <td id="tdHelp" runat="server">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                            Width="48" Height="41"  ></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
  <tr>
 
  
  <td  class="TableHeader td" align="center" colspan = "5">
                                <span  class="titleheading">Employee Advance Application Screen (HOD Approval)</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
  
  </tr>
  <tr>
        <td align="left" valign="top" class ="td" colspan = "5">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>   
 
  <tr>
 <td colspan = "5">
     <asp:GridView ID="GridView3" runat="server" Width = "100%" 
         AutoGenerateColumns="False" style="margin-bottom: 0px">
       <Columns>  
        <asp:BoundField DataField="" HeaderText="S.No." >                      
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
           </asp:BoundField>
        <asp:BoundField DataField="" HeaderText="Advance Application No." > <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" /> </asp:BoundField> 
        <asp:BoundField DataField="" HeaderText="Apply Date" DataFormatString ="{0:MM-dd-yyyy}" > <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="right" /> </asp:BoundField> 
        <asp:BoundField DataField="" HeaderText="Employee Code" >  <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" /> </asp:BoundField>
        <asp:BoundField DataField="" HeaderText="Name" > <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Left" /> </asp:BoundField>
         <asp:BoundField DataField="" HeaderText="Amount Requird" > <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="right" /> </asp:BoundField> 
        <asp:BoundField DataField="" HeaderText="Purpose" ><HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Left" /> </asp:BoundField>   
          <asp:BoundField DataField="" HeaderText="Status">  <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Left" /> </asp:BoundField>
        <asp:BoundField DataField="" HeaderText="Select"  > <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Left" /> </asp:BoundField>                                                                  
       </Columns>                         
     </asp:GridView>
      </td>
 </tr>
  <tr>
        <td align="left" class ="td" colspan = "5">
            <asp:Label ID="Lblempdtl" runat="server" ForeColor="#333333" 
                Text="Employee Details"></asp:Label>
        </td>
    </tr>   
   <tr><td align = "right">Advance Application No. :</td><td colspan ="4"> <asp:TextBox ID="txtAdvAppNo" runat="server" CssClass = "TextBox"></asp:TextBox></td>
   </tr>
 <tr>
 <td align = "right" > Emp_code :</td>
 <td>
     <asp:TextBox ID="txtEmpCode" runat="server" CssClass = "TextBox"></asp:TextBox>
     </td>
 <td></td>
 <td align = "right" >Employee Name :</td>
 <td>
     <asp:TextBox ID="txtEmpName" runat="server" CssClass = "TextBox"></asp:TextBox>
     </td>
 </tr>
 <tr>
 <td align = "right" >Department :</td>
 <td>
     <asp:TextBox ID="txtDept" runat="server" CssClass = "TextBox"></asp:TextBox>
     </td>
 <td></td>
 <td align = "right" >Designation :</td>
 <td>
     <asp:TextBox ID="txtDesig" runat="server" CssClass = "TextBox"></asp:TextBox>
     </td>
 </tr>
 <tr>
 <td align = "right">  Position :</td>
 <td>
     <asp:TextBox ID="txtposition" runat="server" CssClass = "TextBox"></asp:TextBox>
     </td>
 <td></td>
 <td align = "right" >Branch :</td>
 <td>
     <asp:TextBox ID="txtBranch" runat="server" CssClass = "TextBox"></asp:TextBox>
     </td>
 </tr>
 <tr>
 <td align = "right" >Grade :</td>
 <td>
     <asp:TextBox ID="txtGrade" runat="server" CssClass = "TextBox"></asp:TextBox>
     </td>
 <td></td>
 <td align = "right" >Lavel :</td>
 <td>
     <asp:TextBox ID="txtlavel" runat="server" CssClass = "TextBox"></asp:TextBox>
     </td>
 </tr>
 <tr>
 <td colspan = "5" class = "td">
 Advance Detail
 </td>
 </tr>
 <tr>
 <td  align = "right">Advance Apply Date</td><td> <asp:TextBox ID="txtAdvdate" runat="server" CssClass = "TextBox"></asp:TextBox></td>
 <td></td>
 <td  align = "right">Amount Applied :</td><td> <asp:TextBox ID="txtAmtAply" runat="server" CssClass = "TextBoxNo"></asp:TextBox></td>
 </tr>
 <tr>
 <td  align = "right" >Purpose</td><td colspan="4">
            <asp:TextBox ID="txtpurpose" runat="server" CssClass = "TextBox" Width="89%"></asp:TextBox>
            </td>
 </tr>
 <tr>
 <td align = "right">HOD Approval Yes/No :</td>
 <td>
     <asp:DropDownList ID="DropDownList1" runat="server" Width="130px">
         <asp:ListItem>Select</asp:ListItem>
         <asp:ListItem Value="Yes">Yes</asp:ListItem>
         <asp:ListItem>No</asp:ListItem>
     </asp:DropDownList>
            </td>
 <td></td>
 
 <td align = "right">Approved Amount by HOD :</td>
 <td><asp:TextBox ID="txtApproveAmtbyHOD" runat="server" CssClass = "TextBoxNo"></asp:TextBox></td>


</tr>
<tr>
<td align = "right">HOD Approval Date :</td>
<td colspan = "5"><asp:TextBox ID="txtHODApprovDate" runat="server" CssClass = "TextBoxNo"></asp:TextBox> </td>
 </tr>
 <tr>
 <td  colspan = "5" class = "td">Advance Teken Details </td>
 </tr>
  <tr>
 <td colspan = "5">
     <asp:GridView ID="GridView1" runat="server" Width = "100%" 
         AutoGenerateColumns="False" >
       <Columns>                      
        <asp:BoundField DataField="" HeaderText="Advance Application No."> <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" /></asp:BoundField>  
        <asp:BoundField DataField="" HeaderText="Date" DataFormatString ="{0:MM-dd-yyyy}" > <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Right" /> </asp:BoundField> 
        <asp:BoundField DataField="" HeaderText="Amount" >  
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Right" />
           </asp:BoundField>
        <asp:BoundField DataField="" HeaderText="Purpose" ><HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Left" /> </asp:BoundField>
         <asp:BoundField DataField="" HeaderText="Approved Amount" > <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Right" /> </asp:BoundField>
        <asp:BoundField DataField="" HeaderText="No.of Installments" > <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Right" /> </asp:BoundField> 
          <asp:BoundField DataField="" HeaderText="Deduction" ><HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Right" />  </asp:BoundField> 
        <asp:BoundField DataField="" HeaderText="Balance Amount" > <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Right" /></asp:BoundField>
         <asp:BoundField DataField="" HeaderText="Status" > <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Left" /> </asp:BoundField>
                                                                             
       </Columns>  
                            
     </asp:GridView>
      </td>
 </tr>
 </table>
  </ContentTemplate>
</asp:UpdatePanel>