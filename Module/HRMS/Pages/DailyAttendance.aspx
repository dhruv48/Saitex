<%@ Page Language="C#" MasterPageFile="~/CommonMaster/EMPMaster.master" AutoEventWireup="true" CodeFile="DailyAttendance.aspx.cs" Inherits="Module_HRMS_Pages_DailyAttendance" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
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
        
<table border="0" cellpadding="3" cellspacing="0" width="100%" class="tContentArial tablebox">
<tr>
                            <td align="left" colspan="8" width="100%">
                                <table class="tContentArial tablebox" cellspacing="0" width="10%" cellpadding="0" border="0"
                                    align="left">
                                    <tbody>
                                        <tr>
                                                                                    
                                            <td width="41">
                                                <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_find.png"
                                                    Width="41" Height="41"  OnClick="imgbtnFind_Click"></asp:ImageButton>
                                            </td>
                                              
                                            <td width="41">
                                                <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                                                    Width="41" Height="41"  OnClick="imgbtnExit_Click"></asp:ImageButton>
                                            </td>
                                            
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                       
<tr>
                            <td align="left" colspan="8" width="100%">
                                <table class="tContentArial tablebox" cellspacing="0" width="100%" cellpadding="0" border="0"
                                    align="left">
                                    <tbody>
                                    
                                        <tr>
                                            <td style="width: 10%" width="10%">
                                                Select Date :
                                            </td>
                                            <td style="width: 10%">
                                                <asp:TextBox ID="txtDate" runat="server" AutoPostBack="true" 
                                                    OnTextChanged="txtDate_TextChanged" Width="70px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="TxtAttDate" runat="server" Format="dd/MM/yyyy" 
                                                    TargetControlID="txtDate">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td style="width: 10%">
                                                Select Branch:
                                            </td>
                                            <td style="width: 10%" width="10%">
                                                <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="true" 
                                                    CssClass="gCtrTxt" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" 
                                                    Width="160px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 10%">
                                                Select Department :
                                            </td>
                                            <td style="width: 10%">
                                                <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" 
                                                    CssClass="gCtrTxt" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" 
                                                    Width="160px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 10%">
                                                Select Cadre :
                                            </td>
                                            <td style="width: 10%">
                                                <asp:DropDownList ID="ddlCadre" runat="server" AutoPostBack="true" 
                                                    CssClass="gCtrTxt" OnSelectedIndexChanged="ddlCadre_SelectedIndexChanged" 
                                                    Width="160px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
    
                                    </tbody>
                                </table>
                            </td>
                        </tr>

<tr>
                        <td colspan="2" width="100%" align="left">
                            <b>Total Records : &nbsp;&nbsp;</b><asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
                        </td>
                    </tr>
             
             <tr>
            <td width="100%" align="center" class="TableHeader" colspan="8"><b class="titleheading">Employee Attendance</b>  </td>
         </tr>
         <tr>
         
            <td colspan="8">
            <asp:GridView ID="gvReportDisplayGrid"  runat="server" AllowPaging="true" 
                    PageSize="15"   PagerSettings-Position="Bottom"   
                    AutoGenerateColumns="false" PagerSettings-Mode="Numeric"
                    PagerStyle-HorizontalAlign="Left" onpageindexchanging="gvReportDisplayGrid_PageIndexChanging" 
                 EmptyDataText="There is no record found" >
                    <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                    <Columns>                     
                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="40px">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>                           
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Branch Name" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                     <asp:Label ID="LblBranch" Text='<%#Eval("BRANCH_NAME") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Department Name" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                     <asp:Label ID="LblDept" Text='<%#Eval("DEPT_NAME") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Employee Code" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                     <asp:Label ID="lblEmployeeId" Text='<%#Eval("EMP_CODE") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="80px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        
                         <asp:TemplateField HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                     <asp:Label ID="LblEmpName" Text='<%#Eval("EMP_NAME") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="200px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        
                         
                         
                        
                          <asp:TemplateField HeaderText="IN Time" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                     <asp:Label ID="lblInTime" Text='<%#Eval("IN_TIME") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="OUT Time" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                     <asp:Label ID="lblOutTime" Text='<%#Eval("OUT_TIME") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Entry Type" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                     <asp:Label ID="lblEntryType" Text='<%#Eval("ENTRY_TYPE") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" HorizontalAlign="Left" />
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

