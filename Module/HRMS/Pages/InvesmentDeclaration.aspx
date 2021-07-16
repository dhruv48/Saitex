<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvesmentDeclaration.aspx.cs" Inherits="Module_HRMS_Pages_InvesmentDeclaration" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="obout" Namespace="Obout.ComboBox" Assembly="obout_ComboBox" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>::Investment Declaration::</title>
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
        width: 150px;
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
    <link rel="stylesheet" type="text/css" href="../../../StyleSheet/CommonStyle.css" />
    <link rel="stylesheet" type="text/css" href="../../../StyleSheet/cssdesign.css" />
    <link rel="stylesheet" type="text/css" href="../../../StyleSheet/ModalPopup.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
            <table style="margin-left:15px;margin-right:15px" width="98%">
             <tr><td align="center" >                 
                            
                                 <obout:ComboBox runat="server" ID="DDLEmployee" Width="300px" Height="150px" 
                                     DataTextField="EMPLOYEENAME" DataValueField="EMP_CODE" 
                                     EnableLoadOnDemand="true" AutoPostBack="True"   MenuWidth="300px" 
                                     onloadingitems="DDLEmployee_LoadingItems" 
                                     onselectedindexchanged="DDLEmployee_SelectedIndexChanged"  >
	                                  <HeaderTemplate>	        
	                                        <div class="header c2">Employee Name</div>	     
	                                  </HeaderTemplate>
	                                    <ItemTemplate>
	                                     <div class="item c1"><%# Eval("EMPLOYEENAME")%></div>	      
	                                        </ItemTemplate>
	                                             <FooterTemplate>
                                      Displaying items <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %> out of <%# Container.ItemsCount %>.
                                        </FooterTemplate>
	                                    </obout:ComboBox>
                        
             </td></tr>             
                <tr><td align="center" >SAINATH TEXPORT  LIMITED</td></tr>
                 <tr><td align="center" >INVESTMENT DECLARATION FORM FOR THE FINANCIAL YEAR 2010-11</td></tr>
                 <tr><td style="width:20px;"></td></tr>
                 <tr><td>[PLEASE READ INSTRUCTIONS CAREFULLY BEFORE FILLING UP THE FORM]</td></tr>
                 <tr><td>
                        <table>
                            <tr><td class="td" style="width:30%">NAME</td>
                            <td class="td" style="width:60%"><asp:Label ID="LblEmpName" runat="server" Text=""></asp:Label></td></tr>
                             <tr><td class="td">ADDRESS</td>
                            <td class="td"><asp:Label ID="LblAddress" runat="server" Text=""></asp:Label></td></tr>
                             <tr><td class="td">EMPLOYEE CODE & DESIGNATION</td>
                            <td class="td"><asp:Label ID="LblCodeDesi" runat="server" Text=""></asp:Label></td></tr>
                             <tr><td class="td">E-MAIL & CONTACT NO.</td>
                            <td class="td"><asp:Label ID="LblEmail" runat="server" Text=""></asp:Label></td></tr>
                             <tr><td class="td">PAN (MANDATORY) [PLEASE REFER TO NOTE-4 BELOW].</td>
                            <td class="td"><asp:Label ID="LblPanCard" runat="server" Text=""></asp:Label></td></tr>
                        </table> 
                 </td></tr>
                 <tr><td style="width:50px;"></td></tr>
                 <tr><td align="center" >SECTION-1</td></tr>
                  <tr><td align="center" >DECLARATION OF HOUSE RENT PAID IN RESPECT OF RENTED ACCOMODATION FOR CLAIMING EXEMPTION U/S 10(13A) OF THE INCOME TAX ACT, 1961 [PLEASE REFER TO NOTE-5  BELOW]</td></tr>
                  <tr><td style="width:20px;"></td></tr>
                  <tr><td colspan="2">
                      <asp:GridView ID="GvHouserent" Width="600px" runat="server" AutoGenerateColumns="False">
                        <Columns>
                                                <asp:TemplateField HeaderText="Sl No." ItemStyle-Width="25px" ItemStyle-VerticalAlign="top">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="40px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Period for which the rented accomodation is   / will be occupied">
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblLanguage" runat="server" Text='<%# Bind("EMP_LANG") %>'></asp:Label>
                                                    </ItemTemplate>
                                                     <ItemStyle VerticalAlign="Top" Width="80px" />
                                                </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Full Address of the Rented Accomodation">
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblLanguage" runat="server" Text='<%# Bind("EMP_LANG") %>'></asp:Label>
                                                    </ItemTemplate>
                                                     <ItemStyle VerticalAlign="Top" Width="80px" />
                                                </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Rent paid per month (Rs.)">
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblLanguage" runat="server" Text='<%# Bind("EMP_LANG") %>'></asp:Label>
                                                    </ItemTemplate>
                                                     <ItemStyle VerticalAlign="Top" Width="80px" />
                                                </asp:TemplateField>
                           </Columns>
                      </asp:GridView>   
                  </td></tr>
                   <tr><td style="width:50px;"></td></tr>
                 <tr><td align="center" >SECTION-2</td></tr>
                  <tr><td align="center" >DEDUCTION IN RESPECT OF INTEREST PAID FOR PURCHASE/CONSTRUCTION OF HOUSE PROPERTY U/S 24 OF THE INCOME TAX ACT, 1961			</td></tr>
                  <tr><td style="width:20px;"></td></tr>
                  <tr><td colspan="2">
                     <table>
                            <tr><td class="td" style="width:30%">Address of the Property against which Loan taken</td>
                            <td class="td" style="width:60%"><asp:TextBox ID="TxtPropertyName" runat="server"></asp:TextBox></td></tr>
                             <tr><td class="td">Self -Occupied/Rented </td>
                            <td class="td"><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td></tr>
                             <tr><td class="td">Date of loan availed</td>
                            <td class="td"><asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td></tr>
                             <tr><td class="td">Purpose of the Loan (Purchase/Construction)	</td>
                            <td class="td"><asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td></tr>
                             <tr><td class="td">Construction of Property will be completed on or before (please specify date)	</td>
                            <td class="td"><asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td></tr>
                             <tr><td class="td">Date of possession of the House Property	</td>
                            <td class="td"><asp:TextBox ID="TextBox5" runat="server"></asp:TextBox></td></tr>
                             <tr><td class="td">Total amount of interest paid (post construction/possession) during the financial year April, 2008 to March, 2009 (enclose a certificate from the bank/financial institution)	</td>
                            <td class="td"><asp:TextBox ID="TextBox6" runat="server"></asp:TextBox></td></tr>
                              <tr><td class="td">Total amount of interest paid for pre-construction/possession period	</td>
                            <td class="td"><asp:TextBox ID="TextBox7" runat="server"></asp:TextBox></td></tr>
                            
                        </table> 
                  </td></tr>
                  <tr><td class="td"><b>DECLARATION - I hereby declare that I am the Owner/Co-owner of the above referred House Property and the EMI's are being paid from my funds and the other Co-owner(s) have not claimed any deduction with respect to EMI's paid by me.</b></td></tr>
                  <tr><td style="width:50px;"></td></tr>
                 <tr><td align="center" >SECTION-3</td></tr>
                  <tr><td align="center" >DECLARATION REGARDING OTHER SOURCES OF INCOME CHARGEABLE TO TAX UNDER THE INCOME TAX ACT, 1961	</td></tr>
                  <tr><td style="width:20px;"></td></tr>
                    <tr><td colspan="2">
                     <table cellpadding="0" width="80%"  cellspacing="1" border="1">
                            <tr><th>S.NO</th><th align="center" style="width:50%">Description</th><th>Amount(Rs.)</th></tr>
                            <tr><td align="center" >1</td>
                                <td>Salary from Previous Employer/Pension</td>
                                <td><asp:TextBox ID="TxtPreSalary3" runat="server"></asp:TextBox></td>
                            </tr>
                             <tr><td align="center" >2</td>
                                <td>Income from House Property	</td>
                                <td><asp:TextBox ID="TxtHouseIncome3" runat="server"></asp:TextBox></td>
                            </tr>
                             <tr><td align="center" >3</td>
                                <td>Bank Interest	</td>
                                <td><asp:TextBox ID="TxtBankInterest3" runat="server"></asp:TextBox></td>
                            </tr>
                             <tr><td align="center" >4</td>
                                <td>Other Interest</td>
                                <td><asp:TextBox ID="TxtOtherInterest3" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr><td align="center" >5</td>
                                <td>Any Other Income (please specify)</td>
                                <td><asp:TextBox ID="TxtOtherIncome3" runat="server"></asp:TextBox></td>
                            </tr>
                            
                        </table> 
                  </td></tr>
                  <tr><td class="td" >In case any Tax has been deducted from any of the above mentioned other incomes, please state the amount thereof and submit proof of the same alongwith the investment papers.			</td></tr>
                   <tr><td style="width:50px;"></td></tr>
                 <tr><td align="center" >SECTION-4</td></tr>
                  <tr><td align="center" >DECLARATION OF INVESTMENTS MADE FOR CLAIMING DEDUCTION UNDER SECTION 80C & 80CCC OF THE INCOME TAX ACT, 1961</td></tr>
                  <tr><td style="width:20px;"></td></tr>
                  <tr><td colspan="2">
                     <table cellpadding="0" width="90%"  cellspacing="1" border="1">
                            <tr><th>S.NO</th><th align="center" style="width:50%">Description</th><th>Amount(Rs.)</th></tr>
                            <tr><td align="center" >1</td>
                                <td>Life Insurance Premium (LIP) - for self, spouse and children only</td>
                                <td><asp:TextBox ID="TextBox8" runat="server"></asp:TextBox></td>
                            </tr>
                             <tr><td align="center" >2</td>
                                <td>Public Provident Fund  (PPF) - for self, spouse and children only</td>
                                <td><asp:TextBox ID="TextBox9" runat="server"></asp:TextBox></td>
                            </tr>
                             <tr><td align="center" >3</td>
                                <td>Unit Linked Insurance Plan  (ULIP) - for self, spouse and children only	</td>
                                <td><asp:TextBox ID="TextBox10" runat="server"></asp:TextBox></td>
                            </tr>
                             <tr><td align="center" >4</td>
                                <td>National Saving Certificates (NSC) - in self name only	</td>
                                <td><asp:TextBox ID="TextBox11" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr><td align="center" >5</td>
                                <td>Housing Loan repayment of Principal - in self name only	</td>
                                <td><asp:TextBox ID="TextBox12" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr><td align="center" >6</td>
                                <td>Pension Policy Premium u/s 80CCC - in self name only	</td>
                                <td><asp:TextBox ID="TextBox13" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr><td align="center" >7</td>
                                <td>Mutual Funds - in self name only	</td>
                                <td><asp:TextBox ID="TextBox14" runat="server"></asp:TextBox></td>
                            </tr>
                             <tr><td align="center" >8</td>
                                <td>
                                    <table width="100%">
                                            <tr><td colspan="3">Children Tuition Fee (Only) - allowed for maximum of  2 children </td></tr>
                                            <tr><th>Particulars</th><th>Ist Child</th><th>IInd child</th></tr>
                                            <tr><td>Name of Child</td>
                                               <td><asp:TextBox ID="TextBox16" runat="server"></asp:TextBox></td>
                                                <td><asp:TextBox ID="TextBox17" runat="server"></asp:TextBox></td>
                                            </tr>
                                             <tr><td>School/Institution </td>
                                               <td><asp:TextBox ID="TextBox18" runat="server"></asp:TextBox></td>
                                                <td><asp:TextBox ID="TextBox19" runat="server"></asp:TextBox></td>
                                            </tr>
                                             <tr><td>Class</td>
                                               <td><asp:TextBox ID="TextBox20" runat="server"></asp:TextBox></td>
                                                <td><asp:TextBox ID="TextBox21" runat="server"></asp:TextBox></td>
                                            </tr>
                                            <tr><td>Tuition Fee (Only) for the year</td>
                                               <td><asp:TextBox ID="TextBox22" runat="server"></asp:TextBox></td>
                                                <td><asp:TextBox ID="TextBox23" runat="server"></asp:TextBox></td>
                                            </tr>
                                    </table>
                                </td>
                                <td></td>
                            </tr>
                              <tr><td align="center" >9</td>
                                <td>Investment in Tax Saving Infrastructure Bonds - in self name only</td>
                                <td><asp:TextBox ID="TextBox15" runat="server"></asp:TextBox></td>
                            </tr>
                             <tr><td align="center" >10</td>
                                <td>Notified Fixed Deposits-in self name only</td>
                                <td><asp:TextBox ID="TextBox24" runat="server"></asp:TextBox></td>
                            </tr>
                             <tr><td align="center" >11</td>
                                <td>Others (please specify) - in self name only	</td>
                                <td><asp:TextBox ID="TextBox25" runat="server"></asp:TextBox></td>
                            </tr>
                               <tr><td align="center" >12</td>
                                <td>
                                    <table width="100%">
                                            <tr><td colspan="2">Accrued Interest on NSC's purchased between 01/03/2002 to 31/03/2008 (enclose photocopy of NSC's)</td></tr>
                                            <tr><th>Period of Purchase</th><th>Purchase Amount (Rs.)</th></tr>
                                            <tr><td>01/04/2001 TO 28/02/2002</td>
                                               <td><asp:TextBox ID="TextBox26" runat="server"></asp:TextBox></td>                                                
                                            </tr>
                                             <tr><td>01/03/2002 TO 31/03/2002</td>
                                               <td><asp:TextBox ID="TextBox28" runat="server"></asp:TextBox></td>                                                
                                            </tr>
                                             <tr><td>01/04/2002 TO 28/02/2003</td>
                                               <td><asp:TextBox ID="TextBox30" runat="server"></asp:TextBox></td>                                               
                                            </tr>
                                            <tr><td>01/03/2003 TO 31/03/2003</td>
                                               <td><asp:TextBox ID="TextBox32" runat="server"></asp:TextBox></td>
                                            </tr>
                                            
                                                <tr><td>01/04/2003 TO 31/03/2004</td>
                                               <td><asp:TextBox ID="TextBox27" runat="server"></asp:TextBox></td>                                                
                                            </tr>
                                             <tr><td>01/04/2004 TO 31/03/2005</td>
                                               <td><asp:TextBox ID="TextBox29" runat="server"></asp:TextBox></td>                                                
                                            </tr>
                                             <tr><td>01/04/2005 TO 31/03/2006</td>
                                               <td><asp:TextBox ID="TextBox31" runat="server"></asp:TextBox></td>                                               
                                            </tr>
                                            <tr><td>01/04/2006 TO 31/03/2007</td>
                                               <td><asp:TextBox ID="TextBox33" runat="server"></asp:TextBox></td>
                                            </tr>
                                             <tr><td>01/04/2007 TO 31/03/2008</td>
                                               <td><asp:TextBox ID="TextBox34" runat="server"></asp:TextBox></td>
                                            </tr>
                                    </table>
                                </td>
                                <td></td>
                            </tr>
                        </table> 
                  </td></tr>
                  <tr><td class="td" ><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Note&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;	Investments in all of the above saving plans can be made upto a limit of Rs. 1 .00 Lakh.</b>	
			</td></tr>
                     <tr><td style="width:50px;"></td></tr>
                 <tr><td align="center" >SECTION-5</td></tr>
                  <tr><td align="center" >DECLARATION OF OTHER DEDUCTIONS ALLOWED UNDER CHAPTER VI-A OF THE INCOME TAX ACT, 1961</td></tr>
                  <tr><td style="width:20px;"></td></tr>
                    <tr><td colspan="2">
                     <table cellpadding="0" width="80%"  cellspacing="1" border="1">
                            <tr><th>S.NO</th><th align="center" style="width:50%">Description</th><th>Amount(Rs.)</th></tr>
                            <tr><td align="center" >1</td>
                                <td>Section  80D                        -     Medical Insurance Premium</td>
                                <td><asp:TextBox ID="TextBox35" runat="server"></asp:TextBox></td>
                            </tr>
                             <tr><td align="center" >2</td>
                                <td>Section 80DD/80DDB          -     Medical treatement for handicapped	</td>
                                <td><asp:TextBox ID="TextBox36" runat="server"></asp:TextBox></td>
                            </tr>
                             <tr><td align="center" >3</td>
                                <td>Section 80E                         -     Payment of interest on loan for higher education 	</td>
                                <td><asp:TextBox ID="TextBox37" runat="server"></asp:TextBox></td>
                            </tr>
                             <tr><td align="center" >4</td>
                                <td>Section 80U                         -    Deduction in case of person with physical disability	</td>
                                <td><asp:TextBox ID="TextBox38" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr><td align="center" >5</td>
                                <td>Others (please specify)       -    </td>
                                <td><asp:TextBox ID="TextBox39" runat="server"></asp:TextBox></td>
                            </tr>
                            
                        </table> 
                  </td></tr>
                  <tr><td align="center" ><b>NOTES AND DECLARATION	</b></td></tr>
                  <tr><td>
                        <table width="100%">
                            <tr><td valign="top" style="width:5%">1</td>
                            <td class="smallfont">
                                I understand that the eligibility of deduction under Chapter VI-A is based on my above declaration for which I will furnish documentary evidence on or before 31st January, 2009 (including all LIC premiums whether or not due by 31st January, 09).  I also understand that no furthur notice or reminder shall be sent to me to submit documentary evidence for my investments/rent payments. I understand that in case I do not submit the necessary proofs on or before 31st Jan, 2009, the company shall deduct the balance tax from my salary for the months of Feb,09 and Mar, 09		
                            </td>                         
                            </tr>
                              <tr><td valign="top" style="width:5%">2</td>
                            <td class="smallfont">
                                I shall intimate the company of any change in my contributions/investments/payments affecting my eligibile deductions under Chapter VI-A on or before 31st January, 2009.	

                            </td>                         
                            </tr>
                              <tr><td valign="top" style="width:5%;">3</td>
                            <td class="smallfont">
                               I understand that no refund of excess tax, if any, deducted from my salary will be made to me by the company under any circumstances and that any such refund will have to be claimed in the income tax return.	
                            </td>                         
                            </tr>
                              <tr><td valign="top" style="width:5%">4</td>
                            <td class="smallfont">
                               The company is under a liability to quote the PAN No. of the employee from whose salary tax has been deducted. Failure on this part makes the company liable to a penalty of Rs.10,000/- under the Income Tax Act. As such, you are requested to quote PAN No. mandatory. In case, you have applied for PAN No. you are required to submit copy of PAN Application form alongwith this declaration and submit PAN No. within 3 months of the date of declaration. In case of failure on your part, the company shall deduct a penalty of Rs.10,000/- from your salary.		
                            </td>                         
                            </tr>
                            <tr><td valign="top" style="width:5%">5</td>
                            <td class="smallfont"><b>The employee claiming HRA Exemption must satisfy the following conditions:-</b>
                               (a) A copy of valid Rent Agreement must accompany the declaration form in case the monthly rent payment exceeds Rs.5,000/- . In the absence of Rent Agreement alongwith the Declaration Form, no exemption on account of HRA shall be allowed. The employee must also ensure that the rent agreement must be duly executed on a Rs.50/- stamp paper.		<br />
                               (b) In the case of employees whose monthly rent payment exceeds Rs.10,000/-, exemption on account of HRA shall be allowed only when the payment has been made by account payee cheque. Such cheque No. must be mentioned on the rent receipt produced by the employee. This condition is in addition to the condition mentioned in clause (a) above.		<br />
                               (c) In the case of employees whose monthly rent payment does not exceed Rs.10,000/- and the rent has been paid in cash, exemption of HRA shall be allowed only when the rent receipt has been signed by the landlord and a revenue stamp of Re1/- is duly affixed on the rent receipt. This condition is in addition to the condition mentioned in clause (a) above.
                            </td>                         
                            </tr>
                                 <tr><td valign="top" style="width:5%">6</td>
                            <td class="smallfont">
                              <b>Unsigned and Forms without date shall not be accepted and shall be returned to the concerned employee.		</b>
                            </td>                         
                            </tr>
                                 <tr><td valign="top" style="width:5%">7</td>
                            <td class="smallfont">
                              If you have any query you can contact the taxation department through E-mail at<b>  tds_sal@sainthtexport.com </b>  mentioning your name and employee code in all correspondence or you may contact the taxation department at the corporate office.
                            </td>                         
                            </tr>
                                 <tr><td valign="top" style="width:5%">8</td>
                            <td class="smallfont">
                               I declare that I have fully read and understood the Notes-1 to 7 that form part of this Declaration Form		
                            </td>                         
                            </tr>
                                <tr><td style="width:40px;"></td></tr>
                             <tr><td valign="middle"></td>
                            <td class="smallfont">
                                <asp:CheckBox ID="ChkAgree"  Text="I Agree" runat="server" />                               
                            </td>                         
                            </tr>
                            <tr><td style="width:60px;"></td></tr>
                            <tr><td colspan="2">
                                <table width="100%"> 
                                    <tr><td class="tdLeft"><b>Place:</b></td><td class="tdRight"><b>(Signature of Employee)</b></td></tr>
                                    <tr><td class="tdLeft "><b>Dated:</b></td><td class="tdRight"></td></tr>
                                </table>
                            </td></tr>
                             <tr><td align="center" colspan="2">
                                 <asp:Button ID="CmdSave" runat="server" Text="Save" Width="100px" />
                            </td></tr>
                        </table>
                  </td></tr>
            </table>
    </div>
    </form>
</body>
</html>
