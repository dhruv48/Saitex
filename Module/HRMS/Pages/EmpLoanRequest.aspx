<%@ Page Language="C#" MasterPageFile="~/CommonMaster/EMPMaster.master" AutoEventWireup="true" CodeFile="EmpLoanRequest.aspx.cs" Inherits="Module_HRMS_Pages_EmpLoanRequest" Title="LOAN FOR APPROVED" %>
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
            <td width="100%" align="center" class="TableHeader"><b class="titleheading">Loan For Approval</b>
            </td>
          </tr>
          <tr>
          <td>
            <asp:GridView ID="gvReportDisplayGrid"  runat="server" AllowPaging="true" 
                    PageSize="10"   PagerSettings-Position="Bottom"   CssClass="SmallFont"
                    AutoGenerateColumns="false" PagerSettings-Mode="Numeric"
                    PagerStyle-HorizontalAlign="Left" onpageindexchanging="gvReportDisplayGrid_PageIndexChanging" 
                    onselectedindexchanged="gvReportDisplayGrid_SelectedIndexChanged" 
                    DataKeyNames="LOAN_ID" EmptyDataText="There is no record for approval" >
                    <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                    <Columns>
                     <asp:TemplateField ItemStyle-Width="50px">
                            <ItemTemplate>
                               <asp:CheckBox ID="ChkSelect" runat="server" />
                            </ItemTemplate>                           
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderTemplate>
                                <asp:CheckBox ID="ChkAll" AutoPostBack="true" runat="server" oncheckedchanged="ChkAll_CheckedChanged" />
                            </HeaderTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="50px">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>                           
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Apply Date" DataField="APPLIEDDATE" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" >                  

                            <ItemStyle HorizontalAlign="Left" Width="120px" />

                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Emp.Code">
                            <ItemTemplate>
                                     <asp:Label ID="lblEmployeeId" Text='<%#Eval("EMP_CODE") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                     <asp:Label ID="LblEmpName" Text='<%#Eval("EMPLOYEENAME") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="150px" HorizontalAlign="Left" />
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
                        <asp:BoundField HeaderText="Loan Amount" DataField="LOAN_AMOUNT" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" >

                        </asp:BoundField>
                        <asp:BoundField HeaderText="Monthly Inst." DataField="MONTHLY_INST" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" >

                        </asp:BoundField>
                        <asp:BoundField HeaderText="Re-Payment" DataField="RE_PAYMENT" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="120" >
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Duration" DataField="INST_DURATION" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="120" >
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Status" DataField="LOAN_STATUS"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" HtmlEncode="false" >
                        </asp:BoundField>
                    </Columns>
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                            <AlternatingRowStyle CssClass="AltRowStyle" />
                                            <PagerStyle CssClass="PagerStyle" />
                                            <HeaderStyle CssClass="HeaderStyle GrdHeader"  />
                </asp:GridView>
             </td>
        </tr>
    </table>
    <table>
        <tr>
        <td>Change Leave Status</td>
        <td>
            <cc2:OboutDropDownList ID="DDLStatus" runat="server">
                <asp:ListItem Value="0">---Select Status--</asp:ListItem>
                <asp:ListItem Value="A">Approve</asp:ListItem>              
                <asp:ListItem Value="R">Reject</asp:ListItem>
            </cc2:OboutDropDownList>
        </td>            
        <td> <cc2:OboutButton ID="CmdSave" runat="server" Text="Update" onclick="CmdSave_Click" /></td>
        </tr>        
    </table>
 </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

