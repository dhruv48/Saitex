<%@ Page Language="C#" MasterPageFile="~/CommonMaster/EMPMaster.master" AutoEventWireup="true" CodeFile="InvesmentDeclaration1.aspx.cs" Inherits="Module_HRMS_Pages_InvesmentDeclaration1" Title="::Investment Declaration::" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
    <cc1:CalendarExtender ID="CalDob" runat="server" TargetControlID="TxtDOL"></cc1:CalendarExtender>
  <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TxtCopdate"></cc1:CalendarExtender>
   <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TxtDateOfProcess"></cc1:CalendarExtender>
<table style="margin-left:15px;margin-right:15px" width="98%">
             <tr>
                        <td valign="top" align="left">
                            <table cellspacing="0" cellpadding="0" align="left" border="1">
                                <tbody>
                                    <tr>
                                        <td id="tdSave" valign="top" align="center" runat="server">
                                            <asp:ImageButton ID="imgbtnSave" TabIndex="9" runat="server"
                                                ImageUrl="~/CommonImages/save.jpg" ToolTip="Save" Height="41" Width="48" 
                                                ValidationGroup="M1" onclick="imgbtnSave_Click" >
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
                                                ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>            
                <tr><td align="center" class="TableHeader td" > <span class="titleheading"><b>SAINATH TEXPORT  LIMITED</b></span></td></tr>
                 <tr><td align="center" ><b>INVESTMENT DECLARATION FORM FOR THE FINANCIAL YEAR 2010-11</b></td></tr>
                 <tr><td style="width:20px;"></td></tr>
                 <tr><td>[PLEASE READ INSTRUCTIONS CAREFULLY BEFORE FILLING UP THE FORM]</td></tr>
                 <tr><td>
                        <table>
                            <tr><td>EMPLOYEE CODE</td><td><asp:TextBox ID="TxtEmpCode" Width="180px" CssClass="TextBox" ReadOnly="true" runat="server"></asp:TextBox></td>
                            <td>NAME</td><td><asp:TextBox ID="TxtEmpName" ReadOnly="true" Width="180px" CssClass="TextBox" runat="server"></asp:TextBox></td></tr>
                            <tr>
                                <td>DESIGNATION</td><td><asp:TextBox ID="TxtDept" Width="180px" CssClass="TextBox" ReadOnly="true"  runat="server"></asp:TextBox></td>
                                <td>DEPARTMENT</td><td><asp:TextBox ID="TxtDesig" Width="180px" CssClass="TextBox" ReadOnly="true"  runat="server"></asp:TextBox></td>
                            </tr>
                             <tr>
                                <td>ADDRESS</td><td><asp:TextBox ID="TxtAddress" Width="180px" CssClass="TextBox" Height="30px" ReadOnly="true" TextMode="MultiLine" runat="server"></asp:TextBox></td>
                                <td>CONTACT NO.</td><td><asp:TextBox ID="TxtContactNo" Width="180px" CssClass="TextBox" ReadOnly="true"  runat="server"></asp:TextBox></td>
                            </tr>                         
                              <tr>
                                <td>E-MAIL</td><td><asp:TextBox ID="TxtEmail" Width="180px" CssClass="TextBox" ReadOnly="true"  runat="server"></asp:TextBox></td>
                                <td>PAN CARD.</td><td><asp:TextBox ID="TxtPanCard" Width="180px" CssClass="TextBox" ReadOnly="true"  runat="server"></asp:TextBox></td>
                            </tr> 
                        </table> 
                 </td></tr>
                 <tr><td style="width:50px;"></td></tr>
                 <tr><td align="center" class="td" ><b>SECTION-1</b></td></tr>
                  <tr><td align="center" >DECLARATION OF HOUSE RENT PAID IN RESPECT OF RENTED ACCOMODATION FOR CLAIMING EXEMPTION U/S 10(13A) OF THE INCOME TAX ACT, 1961 [PLEASE REFER TO NOTE-5  BELOW]</td></tr>
                  <tr><td style="width:20px;"></td></tr>
                  <tr>
                        <td>
                            <asp:Panel ID="PanelSection1" runat="server">
                              <table>
                                 <tr bgcolor="#006699">
                                    <td align="left" valign="top" style="width:30%" class="SmallFont">
                                        <b>Period for which the rented accomodation is   / will be occupied</b>
                                    </td>
                                    <td align="left" valign="top" style="width:30%" class="SmallFont">
                                        <b>Full Address of the Rented Accomodation </b>
                                    </td>
                                    <td align="left" valign="top" style="width:20%" class="SmallFont">
                                        <b>Rent paid per month (Rs.)</b>
                                    </td>
                                    <td align="left" valign="top" class="SmallFont">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>                                  
                                    <td align="left" valign="top">
                                        <asp:TextBox ID="TxtPeriodSec1" runat="server" TabIndex="1" CssClass="Label SmallFont" Width="200px"></asp:TextBox>
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:TextBox ID="TxtAddSec1" runat="server" CssClass="Label SmallFont" Width="200px" TabIndex="2"></asp:TextBox>
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:TextBox ID="TxtRentSec1" runat="server" onKeyPress="return checkNumeric(event)" CssClass="LabelNo SmallFont" Width="180px" TabIndex="3"></asp:TextBox>                                          
                                    </td>                                                                 
                                    <td align="left" valign="top">
                                        <asp:LinkButton ID="lbtnsavedetail" runat="server" TabIndex="6" onclick="lbtnsavedetail_Click"><b>Save</b></asp:LinkButton>&nbsp;/&nbsp;
                                        <asp:LinkButton ID="lbtnCancel" runat="server" TabIndex="7" onclick="lbtnCancel_Click"><b>Cancel</b></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                            </asp:Panel>
                          
                        </td>
                  </tr>
                  <tr><td >
                      <asp:GridView ID="GvHouserent" Width="98%" runat="server" 
                          AutoGenerateColumns="False" BorderStyle="Solid" 
                          onrowcommand="GvHouserent_RowCommand">
                        <Columns>
                                                <asp:TemplateField HeaderText="Sl No." HeaderStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="top">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Period for which the rented accomodation is   / will be occupied" HeaderStyle-HorizontalAlign="Left" >

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

                                                    <ItemStyle HorizontalAlign="Left"  VerticalAlign="Top" Width="30%"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblPeriod" runat="server" Text='<%# Bind("HR_PERIOD") %>'></asp:Label>
                                                    </ItemTemplate>                                                    
                                                </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Full Address of the Rented Accomodation" HeaderStyle-HorizontalAlign="Left">

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="30%"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblHouseAddress" runat="server" Text='<%# Bind("HRHOUSE_ADD") %>'></asp:Label>
                                                    </ItemTemplate>                                                   
                                                </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Rent paid per month (Rs.)">
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="20%"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblHouseRent" runat="server" Text='<%# Bind("HRRENTPAID") %>'></asp:Label>
                                                    </ItemTemplate>                                                   
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Delete">
                                                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="EmpEdit"
                                                            TabIndex="12" CommandArgument='<%# Eval("UniqueId") %>'></asp:LinkButton>&nbsp;/&nbsp;
                                                        <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CommandName="EmpDelete"
                                                            TabIndex="12" CommandArgument='<%# Eval("UniqueId") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                           </Columns>
                      </asp:GridView>   
                  </td></tr>
                   <tr><td style="width:50px;"></td></tr>
                 <tr><td align="center" class="td" ><b>SECTION-2</b></td></tr>
                  <tr><td align="center" style="height: 34px" >DEDUCTION IN RESPECT OF INTEREST PAID FOR PURCHASE/CONSTRUCTION OF HOUSE PROPERTY U/S 24 OF THE INCOME TAX ACT, 1961			</td></tr>
                  <tr><td style="width:20px; height: 16px;"></td></tr>
                  <tr><td colspan="2">
                     <table>
                            <tr><td style="width:50%">Address of the Property against which Loan taken</td>
                            <td style="width:50%"><asp:TextBox  CssClass="TextBox" Width="180px" ID="TxtPropertyName" runat="server"></asp:TextBox></td></tr>
                             <tr><td >Self -Occupied/Rented </td>
                            <td ><asp:TextBox  CssClass="TextBox" Width="180px" ID="TxtSOR" runat="server"></asp:TextBox></td></tr>
                             <tr><td >Date of loan availed</td>
                            <td ><asp:TextBox  CssClass="TextBox" Width="180px" ID="TxtDOL" runat="server"></asp:TextBox></td></tr>
                             <tr><td >Purpose of the Loan (Purchase/Construction)	</td>
                            <td ><asp:TextBox  CssClass="TextBox" Width="180px" ID="TxtPOL" runat="server"></asp:TextBox></td></tr>
                             <tr><td >Construction of Property will be completed on or before (please specify date)	</td>
                            <td ><asp:TextBox  CssClass="TextBox" Width="180px" ID="TxtCopdate" runat="server"></asp:TextBox></td></tr>
                             <tr><td >Date of possession of the House Property	</td>
                            <td ><asp:TextBox  CssClass="TextBox" Width="180px" ID="TxtDateOfProcess" runat="server"></asp:TextBox></td></tr>
                             <tr><td >Total amount of interest paid (post construction/possession) during the financial year April, 2008 to March, 2009 (enclose a certificate from the bank/financial institution)	</td>
                            <td ><asp:TextBox  CssClass="TextBoxNo" onKeyPress="return checkNumeric(event)" Width="180px" ID="TxtPostAmount" runat="server">0</asp:TextBox></td></tr>
                              <tr><td >Total amount of interest paid for pre-construction/possession period	</td>
                            <td ><asp:TextBox  CssClass="TextBoxNo" onKeyPress="return checkNumeric(event)" Width="180px" ID="TxtPreAmount" runat="server">0</asp:TextBox></td></tr>
                            
                        </table> 
                  </td></tr>
                  <tr><td class="td"><b>DECLARATION - I hereby declare that I am the Owner/Co-owner of the above referred House Property and the EMI's are being paid from my funds and the other Co-owner(s) have not claimed any deduction with respect to EMI's paid by me.</b></td></tr>
                  <tr><td style="width:50px;"></td></tr>
                 <tr><td align="center" class="td" ><b>SECTION-3</b></td></tr>
                  <tr><td align="center" >DECLARATION REGARDING OTHER SOURCES OF INCOME CHARGEABLE TO TAX UNDER THE INCOME TAX ACT, 1961	</td></tr>
                  <tr><td style="width:20px;"></td></tr>
                    <tr><td colspan="2">
                     <table cellpadding="0" width="80%"  cellspacing="1" border="1">
                            <tr><th>S.NO</th><th align="center" style="width:50%">Description</th><th>Amount(Rs.)</th></tr>
                            <tr><td align="center" >1</td>
                                <td>Salary from Previous Employer/Pension</td>
                                <td><asp:TextBox  CssClass="TextBoxNo" onKeyPress="return checkNumeric(event)" Width="180px" ID="TxtPreSalary3" runat="server">0</asp:TextBox></td>
                            </tr>
                             <tr><td align="center" >2</td>
                                <td>Income from House Property	</td>
                                <td><asp:TextBox  CssClass="TextBoxNo" onKeyPress="return checkNumeric(event)" Width="180px" ID="TxtHouseIncome3" runat="server">0</asp:TextBox></td>
                            </tr>
                             <tr><td align="center" >3</td>
                                <td>Bank Interest	</td>
                                <td><asp:TextBox  CssClass="TextBoxNo" onKeyPress="return checkNumeric(event)" Width="180px" ID="TxtBankInterest3" runat="server">0</asp:TextBox></td>
                            </tr>
                             <tr><td align="center" >4</td>
                                <td>Other Interest</td>
                                <td><asp:TextBox  CssClass="TextBoxNo" onKeyPress="return checkNumeric(event)" Width="180px" ID="TxtOtherInterest3" runat="server">0</asp:TextBox></td>
                            </tr>
                            <tr><td align="center" >5</td>
                                <td>Any Other Income (please specify)</td>
                                <td><asp:TextBox  CssClass="TextBox" Width="180px" ID="TxtOtherIncome3" runat="server"></asp:TextBox></td>
                            </tr>
                            
                        </table> 
                  </td></tr>
                  <tr><td class="td" >In case any Tax has been deducted from any of the above mentioned other incomes, please state the amount thereof and submit proof of the same alongwith the investment papers.			</td></tr>
                   <tr><td style="width:50px;"></td></tr>
                 <tr><td align="center" class="td" ><b>SECTION-4</b></td></tr>
                  <tr><td align="center" >DECLARATION OF INVESTMENTS MADE FOR CLAIMING DEDUCTION UNDER SECTION 80C & 80CCC OF THE INCOME TAX ACT, 1961</td></tr>
                  <tr><td style="width:20px;"></td></tr>
                  <tr><td colspan="2">
                     <table cellpadding="0"   cellspacing="1" border="1">
                            <tr><th>S.NO</th><th align="center" style="width:50%">Description</th><th>Amount(Rs.)</th></tr>
                            <tr><td align="center" >1</td>
                                <td>Life Insurance Premium (LIP) - for self, spouse and children only</td>
                                <td><asp:TextBox  CssClass="TextBoxNo" onKeyPress="return checkNumeric(event)" 
                                        Width="180px" ID="TxtLIP" runat="server">0</asp:TextBox></td>
                            </tr>
                             <tr><td align="center" >2</td>
                                <td>Public Provident Fund  (PPF) - for self, spouse and children only</td>
                                <td><asp:TextBox  CssClass="TextBoxNo" onKeyPress="return checkNumeric(event)" 
                                        Width="180px" ID="TxtPPF" runat="server">0</asp:TextBox></td>
                            </tr>
                             <tr><td align="center" >3</td>
                                <td>Unit Linked Insurance Plan  (ULIP) - for self, spouse and children only	</td>
                                <td><asp:TextBox  CssClass="TextBoxNo" onKeyPress="return checkNumeric(event)" 
                                        Width="180px" ID="TxtULIP" runat="server">0</asp:TextBox></td>
                            </tr>
                             <tr><td align="center" >4</td>
                                <td>National Saving Certificates (NSC) - in self name only	</td>
                                <td><asp:TextBox  CssClass="TextBoxNo" onKeyPress="return checkNumeric(event)" 
                                        Width="180px" ID="TxtNSC" runat="server">0</asp:TextBox></td>
                            </tr>
                            <tr><td align="center" >5</td>
                                <td>Housing Loan repayment of Principal - in self name only	</td>
                                <td><asp:TextBox  CssClass="TextBoxNo" onKeyPress="return checkNumeric(event)" 
                                        Width="180px" ID="TxtHouseLoan" runat="server">0</asp:TextBox></td>
                            </tr>
                            <tr><td align="center" >6</td>
                                <td>Pension Policy Premium u/s 80CCC - in self name only	</td>
                                <td><asp:TextBox  CssClass="TextBoxNo" onKeyPress="return checkNumeric(event)" 
                                        Width="180px" ID="TxtPPP" runat="server">0</asp:TextBox></td>
                            </tr>
                            <tr><td align="center" >7</td>
                                <td>Mutual Funds - in self name only	</td>
                                <td><asp:TextBox  CssClass="TextBoxNo" onKeyPress="return checkNumeric(event)" 
                                        Width="180px" ID="TxtMF" runat="server">0</asp:TextBox></td>
                            </tr>
                             <tr><td align="center" >8</td>
                                <td>
                                    <table width="100%">
                                            <tr><td colspan="3">Children Tuition Fee (Only) - allowed for maximum of  2 children </td></tr>
                                            <tr><th>Particulars</th><th>Ist Child</th><th>IInd child</th></tr>
                                            <tr><td>Name of Child</td>
                                               <td><asp:TextBox  CssClass="TextBox" Width="180px" ID="TxtFCName" runat="server"></asp:TextBox></td>
                                                <td><asp:TextBox  CssClass="TextBox" Width="180px" ID="TxtSCName" runat="server"></asp:TextBox></td>
                                            </tr>
                                             <tr><td>School/Institution </td>
                                               <td><asp:TextBox  CssClass="TextBox" Width="180px" ID="txtFCSchool" runat="server"></asp:TextBox></td>
                                                <td><asp:TextBox  CssClass="TextBox" Width="180px" ID="TxtSCSchool" runat="server"></asp:TextBox></td>
                                            </tr>
                                             <tr><td>Class</td>
                                               <td><asp:TextBox  CssClass="TextBox" Width="180px" ID="TxtFCClass" runat="server"></asp:TextBox></td>
                                                <td><asp:TextBox  CssClass="TextBox" Width="180px" ID="TxtSCClass" runat="server"></asp:TextBox></td>
                                            </tr>
                                            <tr><td>Tuition Fee (Only) for the year</td>
                                               <td><asp:TextBox  CssClass="TextBoxNo" onKeyPress="return checkNumeric(event)" 
                                                       Width="180px" ID="TxtFCTutionFee" runat="server">0</asp:TextBox></td>
                                                <td><asp:TextBox  CssClass="TextBoxNo" onKeyPress="return checkNumeric(event)" 
                                                        Width="180px" ID="TxtSCTutionFee" runat="server">0</asp:TextBox></td>
                                            </tr>
                                    </table>
                                </td>
                                <td></td>
                            </tr>
                              <tr><td align="center" >9</td>
                                <td>Investment in Tax Saving Infrastructure Bonds - in self name only</td>
                                <td><asp:TextBox  CssClass="TextBoxNo" onKeyPress="return checkNumeric(event)" 
                                        Width="180px" ID="TxtITSI" runat="server">0</asp:TextBox></td>
                            </tr>
                             <tr><td align="center" >10</td>
                                <td>Notified Fixed Deposits-in self name only</td>
                                <td><asp:TextBox  CssClass="TextBoxNo" onKeyPress="return checkNumeric(event)" 
                                        Width="180px" ID="TxtNFD" runat="server">0</asp:TextBox></td>
                            </tr>
                             <tr><td align="center" >11</td>
                                <td>Others (please specify) - in self name only	</td>
                                <td><asp:TextBox  CssClass="TextBox" Width="180px" ID="TxtOtherFund" runat="server"></asp:TextBox></td>
                            </tr>
                               <tr><td align="center" >12</td>
                                <td>
                                    <table width="100%">
                                            <tr><td colspan="2">Accrued Interest on NSC's purchased between 01/03/2002 to 31/03/2008 (enclose photocopy of NSC's)</td></tr>
                                            <tr><th>Period of Purchase</th><th>Purchase Amount (Rs.)</th></tr>
                                            <tr><td>01/04/2001 TO 28/02/2002</td>
                                               <td><asp:TextBox  CssClass="TextBoxNo" onKeyPress="return checkNumeric(event)" 
                                                       Width="180px" ID="Txt1to2" runat="server">0</asp:TextBox></td>                                                
                                            </tr>
                                             <tr><td>01/03/2002 TO 31/03/2002</td>
                                               <td><asp:TextBox  CssClass="TextBoxNo" onKeyPress="return checkNumeric(event)" 
                                                       Width="180px" ID="Txt2to2" runat="server">0</asp:TextBox></td>                                                
                                            </tr>
                                             <tr><td>01/04/2002 TO 28/02/2003</td>
                                               <td><asp:TextBox  CssClass="TextBoxNo" onKeyPress="return checkNumeric(event)" 
                                                       Width="180px" ID="Txt2to3" runat="server">0</asp:TextBox></td>                                               
                                            </tr>
                                            <tr><td>01/03/2003 TO 31/03/2003</td>
                                               <td><asp:TextBox  CssClass="TextBoxNo" onKeyPress="return checkNumeric(event)" 
                                                       Width="180px" ID="Txt3to3" runat="server">0</asp:TextBox></td>
                                            </tr>
                                            
                                                <tr><td>01/04/2003 TO 31/03/2004</td>
                                               <td><asp:TextBox  CssClass="TextBoxNo" onKeyPress="return checkNumeric(event)" 
                                                       Width="180px" ID="Txt3to4" runat="server">0</asp:TextBox></td>                                                
                                            </tr>
                                             <tr><td>01/04/2004 TO 31/03/2005</td>
                                               <td><asp:TextBox  CssClass="TextBoxNo" onKeyPress="return checkNumeric(event)" 
                                                       Width="180px" ID="Txt4to5" runat="server">0</asp:TextBox></td>                                                
                                            </tr>
                                             <tr><td>01/04/2005 TO 31/03/2006</td>
                                               <td><asp:TextBox  CssClass="TextBoxNo" onKeyPress="return checkNumeric(event)" 
                                                       Width="180px" ID="Txt5to6" runat="server">0</asp:TextBox></td>                                               
                                            </tr>
                                            <tr><td>01/04/2006 TO 31/03/2007</td>
                                               <td><asp:TextBox  CssClass="TextBoxNo" onKeyPress="return checkNumeric(event)" 
                                                       Width="180px" ID="Txt6to7" runat="server">0</asp:TextBox></td>
                                            </tr>
                                             <tr><td>01/04/2007 TO 31/03/2008</td>
                                               <td><asp:TextBox  CssClass="TextBoxNo" onKeyPress="return checkNumeric(event)" 
                                                       Width="180px" ID="Txt7to8" runat="server">0</asp:TextBox></td>
                                            </tr>
                                            <tr><td>01/04/2008 TO 31/03/2009</td>
                                               <td><asp:TextBox  CssClass="TextBoxNo" onKeyPress="return checkNumeric(event)" 
                                                       Width="180px" ID="Txt8To9" runat="server">0</asp:TextBox></td>
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
                 <tr><td align="center" class="td" ><b>SECTION-5</b></td></tr>
                  <tr><td align="center" >DECLARATION OF OTHER DEDUCTIONS ALLOWED UNDER CHAPTER VI-A OF THE INCOME TAX ACT, 1961</td></tr>
                  <tr><td style="width:20px;"></td></tr>
                    <tr><td colspan="2">
                     <table cellpadding="0"   cellspacing="1" border="1">
                            <tr><th>S.NO</th><th align="center" style="width:50%">Description</th><th style="width:46%">Amount(Rs.)</th></tr>
                            <tr><td align="center" >1</td>
                                <td  style="width:50%" >Section  80D                        -     Medical Insurance Premium</td>
                                <td style="width:46%"><asp:TextBox  CssClass="TextBoxNo" onKeyPress="return checkNumeric(event)" Width="180px" ID="TxtMedicalInsu" runat="server">0</asp:TextBox></td>
                            </tr>
                             <tr><td align="center" >2</td>
                                <td>Section 80DD/80DDB          -     Medical treatement for handicapped	</td>
                                <td><asp:TextBox  CssClass="TextBoxNo" onKeyPress="return checkNumeric(event)" Width="180px" ID="TxtMedicalTreat" runat="server">0</asp:TextBox></td>
                            </tr>
                             <tr><td align="center" >3</td>
                                <td>Section 80E                         -     Payment of interest on loan for higher education 	</td>
                                <td><asp:TextBox  CssClass="TextBoxNo"  onKeyPress="return checkNumeric(event)" Width="180px" ID="TxtInterestOnLoan" runat="server">0</asp:TextBox></td>
                            </tr>
                             <tr><td align="center" >4</td>
                                <td>Section 80U                         -    Deduction in case of person with physical disability	</td>
                                <td><asp:TextBox  CssClass="TextBoxNo"  onKeyPress="return checkNumeric(event)" Width="180px" ID="TxtDPP" runat="server">0</asp:TextBox></td>
                            </tr>
                            <tr><td align="center" >5</td>
                                <td>Others (please specify)       -    </td>
                                <td><asp:TextBox  CssClass="TextBox" Width="180px"  ID="txtOtherDeduct" runat="server">0</asp:TextBox></td>
                            </tr>
                             <tr><td><asp:TextBox   Width="20px" Visible="false" ID="TxtOtherDeId" Text="0" runat="server"></asp:TextBox></td>
                                <td> <asp:TextBox   Width="20px" Visible="false" ID="TxtPurDeductID" Text="0" runat="server"></asp:TextBox>
                                <asp:TextBox   Width="20px" Visible="false" ID="TxtClaimDeductId" Text="0" runat="server"></asp:TextBox></td>
                                <td><asp:TextBox   Width="20px" Visible="false" ID="txtSourceOfIncomeId" Text="0" runat="server"></asp:TextBox></td>
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
                                <asp:CheckBox ID="ChkAgree" Checked="true" Text="I Agree" runat="server" />                               
                            </td>                         
                            </tr>
                            <tr><td style="width:60px;"></td></tr>
                            <tr><td colspan="2">
                                <table width="100%"> 
                                    <tr><td class="tdLeft"><b>Place:</b></td><td class="tdRight"><b>(Signature of Employee)</b></td></tr>
                                    <tr><td class="tdLeft "><b>Dated:</b></td><td class="tdRight"></td></tr>
                                </table>
                            </td></tr>                             
                        </table>
                  </td></tr>
            </table>
</asp:Content>

