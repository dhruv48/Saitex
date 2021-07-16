<%@ Page Language="C#" MasterPageFile="~/CommonMaster/EMPMaster.master" AutoEventWireup="true" CodeFile="LoanDetail.aspx.cs" Inherits="Module_HRMS_Pages_LoanDetail" Title="Loan Detail" %>

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
 <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
<table border="1" cellpadding="3" cellspacing="0" width="100%" class="tContent">
         <tr>
            <td width="100%" align="center" class="TableHeader"><b class="titleheading">Loan Detail </b> </td>
         </tr>
         <tr>
            <td>
            <asp:GridView ID="gvReportDisplayGrid"  runat="server" AllowPaging="true" 
                    PageSize="10"   PagerSettings-Position="Bottom" CssClass="SmallFont"
                    AutoGenerateColumns="false" PagerSettings-Mode="Numeric"
                    PagerStyle-HorizontalAlign="Left" onpageindexchanging="gvReportDisplayGrid_PageIndexChanging" 
                      DataKeyNames="LOAN_ID" EmptyDataText="There is no record for approval" >
                    <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                    <Columns>                     
                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="50px">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>                           
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Apply Date" DataField="APPLIEDDATE" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" >                  
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Employee Code">
                            <ItemTemplate>
                                     <asp:Label ID="lblEmployeeId" Text='<%#Eval("EMP_CODE") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                       
                         <asp:TemplateField HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                     <asp:Label ID="LblEmpName" Text='<%#Eval("EMPLOYEENAME") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Dept." HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                     <asp:Label ID="LblDept" Text='<%#Eval("DEPT_NAME") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Branch" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                     <asp:Label ID="LblBranch" Text='<%#Eval("BRANCH_NAME") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                         <asp:BoundField HeaderText="Loan Amount" DataField="LOAN_AMOUNT" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" >

                        </asp:BoundField>
                        <asp:BoundField HeaderText="Monthly Inst." DataField="MONTHLY_INST" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" >

                        </asp:BoundField>
                        <asp:BoundField HeaderText="Re-Payment" DataField="RE_PAYMENT" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150" >
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Duration" DataField="INST_DURATION" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150" >
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Status" DataField="LOAN_STATUS" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" HtmlEncode="false" >
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Print" ItemStyle-Width="75px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a target="_blank" href="EmpLoanRpt.aspx?EmpCode=<%# Eval("EMP_CODE") %>&LOAN_ID=<%# Eval("LOAN_ID") %>"><b>Print</b></a>
                                        </ItemTemplate>                                       
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

