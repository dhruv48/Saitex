<%@ Page Language="C#" MasterPageFile="~/CommonMaster/EMPMaster.master" AutoEventWireup="true" CodeFile="EmpApplication_For_Loan.aspx.cs" Inherits="Module_HRMS_Pages_EmpApplication_For_Loan" Title="Loan Application" %>
<%@ Register assembly="obout_Interface" namespace="Obout.Interface" tagprefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
    <style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;}
    .header
    {
        margin-left: 2px;
    }
    .c1
    {
        width: 300px;
    }
    .c2
    {
        margin-left: 4px;
        width: 120px;
    }
    .c3
    {
        margin-left: 4px;
        width: 80px;
    }
    .c4
    {
        margin-left: 4px;
        width: 80px;
    }
    .TableHeader
    {
	color: #000000;
	font-weight: bold;
	background-color: #336799;
	font-family: Verdana,arial;
	text-decoration: none;
	font-size: 13px;
	text-align: center;
	height: 19px;
   }


body
  {
	padding-top: 0px;
	top: 0px;
	background-color: #FFFFFF;
  }


.td
{
	border-style: ridge;
	border-bottom-width:.5px;
	border-color: #C1D3FB;
}
.titleheading
{
	font-family: arial;
	font-size: 12px;
	font-weight: bold;
	color: #ffffff;
}
.Label
{
	font-family: Arial, Arial, Helvetica, sans-serif;
	font-size: 12px;
	vertical-align: top;
	text-align: left;
}
.LabelNo
{
	font-family: Arial, Arial, Helvetica, sans-serif;
	font-size: 12px;
	text-align: right;
	vertical-align: top;
}
.tContentArial
{
	font-family: Arial;
	font-size: 12px;
}

.tablebox
{
	border: 1px solid #336799;
}

    </style>
 <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
<table class="tContentArial" cellspacing="0" cellpadding="0" align="center" border="0">
     <tr>
                        <td valign="top" colspan="2" align="left">
                            <table cellspacing="0" cellpadding="0" align="left" border="1">
                                <tbody>
                                    <tr>
                                        <td id="tdSave" valign="top" align="center" runat="server">
                                            <asp:ImageButton ID="imgbtnSave" TabIndex="9" runat="server"
                                                ImageUrl="~/CommonImages/save.jpg" ToolTip="Save" Height="41" Width="48" 
                                                ValidationGroup="M1" onclick="imgbtnSave_Click">
                                            </asp:ImageButton>
                                        </td>
                                        <td id="tdUpdate" valign="top" align="center" runat="server">
                                            <asp:ImageButton ID="imgbtnUpdate" TabIndex="9"  runat="server"
                                                ImageUrl="~/CommonImages/edit1.jpg" ToolTip="Update" Height="41" 
                                                Width="48" ValidationGroup="M1" onclick="imgbtnUpdate_Click">
                                            </asp:ImageButton>
                                        </td>                                       
                                        <td valign="top" align="center">
                                            <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                                                ToolTip="Print" Height="41" Width="48" onclick="imgbtnPrint_Click"></asp:ImageButton>
                                        </td>
                                        <td valign="top" align="center">
                                            <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                                ToolTip="Clear" Height="41" Width="48" onclick="imgbtnClear_Click"></asp:ImageButton>
                                        </td>
                                        <td valign="top" align="center">
                                            <asp:ImageButton ID="imgbtnExit"  runat="server" ImageUrl="~/CommonImages/link_exit.png"
                                                ToolTip="Exit" Height="41" Width="48" onclick="imgbtnExit_Click"></asp:ImageButton>
                                        </td>
                                        <td valign="top" align="center">
                                            <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                                                ToolTip="Help" Height="41" Width="48" onclick="imgbtnHelp_Click"></asp:ImageButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="TableHeader" colspan="2" align="center" width="100%">
                            <b class="titleheading">Application For Loan</b>
                        </td>
                    </tr>
                    <tr><td colspan="2"><b>1.</b>General Information</td></tr>
                    <tr><td colspan="2">
                        <table>
                            <tr><td>EMPLOYEE CODE</td><td><cc2:OboutTextBox ID="TxtEmpCode" Width="180px" CssClass="textbox" ReadOnly="true" runat="server"></cc2:OboutTextBox></td>
                            <td>NAME</td><td><cc2:OboutTextBox ID="TxtEmpName" ReadOnly="true" Width="180px" CssClass="textbox" runat="server"></cc2:OboutTextBox></td></tr>
                             <tr>
                                <td>GRADE</td><td><cc2:OboutTextBox ID="TxtGrade" Width="180px" CssClass="textbox" ReadOnly="true"  runat="server"></cc2:OboutTextBox></td>
                                <td>DOJ</td><td><cc2:OboutTextBox ID="TxtDOJ" Width="180px" CssClass="textbox" ReadOnly="true"  runat="server"></cc2:OboutTextBox></td>
                            </tr> 
                            <tr>
                                <td>DESIGNATION</td><td><cc2:OboutTextBox ID="TxtDept" Width="180px" CssClass="textbox" ReadOnly="true"  runat="server"></cc2:OboutTextBox></td>
                                <td>DEPARTMENT</td><td><cc2:OboutTextBox ID="TxtDesig" Width="180px" CssClass="textbox" ReadOnly="true"  runat="server"></cc2:OboutTextBox></td>
                            </tr>
                             <tr>                               
                                <td>CONTACT NO.</td><td><cc2:OboutTextBox ID="TxtContactNo" Width="180px" CssClass="textbox" ReadOnly="true"  runat="server"></cc2:OboutTextBox></td>
                                <td>GROSS SALARY PER MONTH</td><td><cc2:OboutTextBox ID="TxtGrossSalary" Width="180px" CssClass="textbox" ReadOnly="true"  runat="server"></cc2:OboutTextBox></td>
                            </tr>  
                             <tr>
                              <td>PRESENT ADDRESS</td><td><cc2:OboutTextBox ID="TxtPresentAddress" Width="180px" CssClass="textbox" Height="40px" ReadOnly="true" TextMode="MultiLine" runat="server"></cc2:OboutTextBox></td> 
                              <td>PARMANENT ADDRESS</td><td><cc2:OboutTextBox ID="TxtParmanentAdd" Width="180px" CssClass="textbox" Height="40px" ReadOnly="true" TextMode="MultiLine" runat="server"></cc2:OboutTextBox></td>
                               
                            </tr> 
                             <tr><td><b>2.</b>Amount Required:</td>
                     <td colspan="3"> <cc2:OboutTextBox ID="TxtReqAmt" Width="180px" onKeyPress="return checkNumeric(event)"  CssClass="LabelNo"   
                             runat="server" MaxLength="10">0</cc2:OboutTextBox></td>
                  </tr>          
                 <tr><td><b>3.</b>Purpose of Loan:</td>
                     <td colspan="3"> <cc2:OboutTextBox ID="TxtLoanPurpose" Width="180px" CssClass="TextBox" TextMode="MultiLine"  runat="server"></cc2:OboutTextBox></td>
                  </tr> 
                   <tr><td><b>4.</b>Payment:</td>
                     <td colspan="3"> <cc2:OboutTextBox ID="TxtRepayment" Width="180px" CssClass="TextBox"   
                             runat="server" MaxLength="50"></cc2:OboutTextBox></td>
                  </tr> 
                           
                        </table> 
                 </td></tr>
                   
                   <tr><td colspan="2"><b>5.</b>Monthly installments of Rs. 
                       <cc2:OboutTextBox ID="TxtMonthlyInst" Width="120px" 
                           onKeyPress="return checkNumeric(event)" CssClass="LabelNo SmallFont"   
                           runat="server" MaxLength="10">0</cc2:OboutTextBox> From the salary payable
                   for the duration of  <cc2:OboutTextBox ID="TxtDuration" Width="120px" 
                           CssClass="TextBox"   runat="server" MaxLength="50"></cc2:OboutTextBox>(Along with interest)
                   </td></tr> 
                    <tr><td colspan="2">(The Monthly deduction should not exceed 50% of the gross salary per month)</td></tr>
                     <tr><td colspan="2"><b>6.</b>Declaration:</td></tr> 
                     <tr><td colspan="2" >
                     I declare that i have read and understood the rules governing sanction of the loan for the above purpose and confirm that I shall abide by the same.In the event of my leaving the services of the company
                     for whatever reasons,I undertake to pay the entire balance amount of loan due on the date of my resignation or termination of my services from the company.
                     I also authorize the company to recover such balance amount from my dues on account of arrears of salary/perks/ex-gratia/gratuity or any other payment due
                     to me.In case the said amount is not sufficient to pay the entire balance amount,I shall be personally liable to pay the said balance
                     amount and if not paid company may initiate legal action agsinst me for the recovery of loan amount.
                     </td></tr>
                     
                     <tr><td colspan="2">
                         <asp:CheckBox ID="ChkAgree" Checked="true" runat="server" Text="I Agree" />
                         <cc2:OboutTextBox ID="TxtLoanId" Width="10px" Visible="false" Text="0" runat="server"></cc2:OboutTextBox>
                         </td></tr>
                         <tr>
            <td width="100%" align="center" class="TableHeader"><b class="titleheading">Loan Detail </b> </td>
         </tr>
                      <tr>
                        <td>
                            <asp:GridView ID="gvReportDisplayGrid"  runat="server" AllowPaging="true" 
                    PageSize="10"   PagerSettings-Position="Bottom" CssClass="SmallFont"
                    AutoGenerateColumns="false" PagerSettings-Mode="Numeric"
                    PagerStyle-HorizontalAlign="Left" onpageindexchanging="gvReportDisplayGrid_PageIndexChanging" 
                      DataKeyNames="LOAN_ID" EmptyDataText="There is no record " >
                    <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                    <Columns>                     
                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="50px">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>                           
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Apply Date" DataField="APPLIEDDATE" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" >                  
                            
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Employee Code">
                            <ItemTemplate>
                                     <asp:Label ID="lblEmployeeId" Text='<%#Eval("EMP_CODE") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                       
                         <asp:TemplateField HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                     <asp:Label ID="LblEmpName" Text='<%#Eval("EMPLOYEENAME") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Dept." HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                     <asp:Label ID="LblDept" Text='<%#Eval("DEPT_NAME") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Branch" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                     <asp:Label ID="LblBranch" Text='<%#Eval("BRANCH_NAME") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                         <asp:BoundField HeaderText="Loan Amount" DataField="LOAN_AMOUNT" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" >

                             <ItemStyle HorizontalAlign="Center" Width="100px" />

                        </asp:BoundField>
                        <asp:BoundField HeaderText="Monthly Inst." DataField="MONTHLY_INST" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" >

                            <ItemStyle HorizontalAlign="Center" Width="100px" />

                        </asp:BoundField>
                        <asp:BoundField HeaderText="Re-Payment" DataField="RE_PAYMENT" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150" >
                            <ItemStyle HorizontalAlign="Center" Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Duration" DataField="INST_DURATION" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150" >
                            <ItemStyle HorizontalAlign="Center" Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Status" DataField="LOAN_STATUS" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" HtmlEncode="false" >
                            
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Print" ItemStyle-Width="75px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a target="_blank" href="EmpLoanRpt.aspx?EmpCode=<%# Eval("EMP_CODE") %>&LOAN_ID=<%# Eval("LOAN_ID") %>"><b>Print</b></a>
                                        </ItemTemplate>                                       
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="75px" />
                                    </asp:TemplateField>
                    </Columns>
 <RowStyle CssClass="RowStyle SmallFont" />
                                            <SelectedRowStyle CssClass="SelectedRowStyle" />                                           
                                            <PagerStyle CssClass="PagerStyle" />
                                            <HeaderStyle CssClass="HeaderStyle GrdHeader"  />
                </asp:GridView>
                        
                        </td>
                     </tr>
                    </table>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

