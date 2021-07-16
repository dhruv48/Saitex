<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AttendanceQuery.ascx.cs" Inherits="Module_HRMS_Controls_AttendanceQuery" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="obout" %>
<style type="text/css">
			.tdText {
				font:11px Verdana;
				color:#333333;
			}
			.option2{
				font:11px Verdana;
				color:#0033cc;				
				padding-left:4px;
				padding-right:4px;
			}
			a {
				font:11px Verdana;
				color:#315686;
				text-decoration:underline;
			}

			a:hover {
				color:crimson;
			}
		</style>		
<table class="tContentArial">
    <tr>
        <td class="td">
            <table>
                <tr>
                   <%-- <td id="tdPrint" runat="server">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                            Width="48" Height="41" TabIndex="9"></asp:ImageButton>
                    </td>--%>
                    <td id="tdExit" runat="server">
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                            Width="48" Height="41" TabIndex="10" OnClick="imgbtnExit_Click"></asp:ImageButton>
                    </td>
                    <td id="tdHelp" runat="server">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                            Width="48" Height="41" TabIndex="11"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" valign="top" class="tRowColorAdmin td">
            <span class="titleheading">Attendance Query</span>
        </td>
    </tr>
     <tr>
        <td class="td" align="center">
          <table cellpadding="0" cellspacing="0" border="0" align="left" class="tContentArial">
                <tr >
                <td>Select Year</td>
                <td><obout:OboutDropDownList ID="DDLYear" runat="server"></obout:OboutDropDownList> </td><td></td>
                <td >Select Month</td>
                <td><obout:OboutDropDownList ID="DDLMonth" AutoPostBack="true" runat="server" 
                        onselectedindexchanged="DDLMonth_SelectedIndexChanged" ></obout:OboutDropDownList> 
                 </td>                
    </tr>
</table>        
 </td>
                
    </tr>
       <tr>
        <td align="left" valign="top" class="td">              
                <obout:Grid ID="Grid1" runat="server" AllowAddingRecords="False" AllowFiltering="True"
                AllowPaging="true" PageSize="5" AutoGenerateColumns="False" 
                AutoPostBackOnSelect="True">
                <Columns>
                <obout:Column DataField="EMP_CODE" HeaderText="Employee Code" Width="100px">
				<FilterOptions>
                       <obout:FilterOption Type="NoFilter" />
				        <obout:FilterOption Type="EqualTo" />
				       <obout:FilterOption Type="SmallerThan" />
		   		        <obout:FilterOption Type="GreaterThan" />	
				 </FilterOptions>
			        </obout:Column>
                <obout:Column ID="Column1" DataField="EmployeeName"  HeaderText="Employee Name" Width="150" runat="server">
				    <FilterOptions>
				        <obout:FilterOption Type="NoFilter" />
				        <obout:FilterOption Type="EqualTo" />
				        <obout:FilterOption Type="StartsWith" />
				        <obout:FilterOption Type="EndsWith" />
				    </FilterOptions>
				</obout:Column>
				    <obout:Column DataField="YR" HeaderText="Year" Width="70px" AllowFilter="false">
                    </obout:Column>
                    <obout:Column DataField="1" HeaderText="1" Width="40px" AllowSorting="false" AllowFilter="false">
                    </obout:Column>
                    <obout:Column DataField="2" HeaderText="2"  Width="40px" AllowSorting="false" AllowFilter="false">
                    </obout:Column>
                    <obout:Column DataField="3" HeaderText="3"  Width="40px" AllowSorting="false" AllowFilter="false">
                    </obout:Column>
                    <obout:Column DataField="4" HeaderText="4"  Width="40px" AllowSorting="false" AllowFilter="false">
                    </obout:Column>
                    <obout:Column DataField="5" HeaderText="5"  Width="40px" AllowSorting="false" AllowFilter="false">
                    </obout:Column>
                    <obout:Column DataField="6" HeaderText="6"  Width="40px" AllowSorting="false" AllowFilter="false">
                    </obout:Column>
                    <obout:Column DataField="7" HeaderText="7"  Width="4px" AllowSorting="false" AllowFilter="false">
                    </obout:Column>
                    <obout:Column DataField="8" HeaderText="8"  Width="40px" AllowSorting="false" AllowFilter="false">
                    </obout:Column>
                    <obout:Column DataField="9" HeaderText="9"  Width="40px" AllowSorting="false" AllowFilter="false">
                    </obout:Column>
                    <obout:Column DataField="10" HeaderText="10"  Width="50px" AllowSorting="false" AllowFilter="false">
                    </obout:Column>
                    <obout:Column DataField="11" HeaderText="11"  Width="50px" AllowSorting="false" AllowFilter="false">
                    </obout:Column>
                    <obout:Column DataField="12" HeaderText="12"  Width="50px" AllowSorting="false" AllowFilter="false">
                    </obout:Column>
                    <obout:Column DataField="13" HeaderText="13"  Width="50px" AllowSorting="false" AllowFilter="false">
                    </obout:Column>
                    <obout:Column DataField="14" HeaderText="14"  Width="50px" AllowSorting="false" AllowFilter="false">
                    </obout:Column>
                    <obout:Column DataField="15" HeaderText="15"  Width="50px" AllowSorting="false" AllowFilter="false">
                    </obout:Column>
                    <obout:Column DataField="16" HeaderText="16"  Width="50px" AllowSorting="false" AllowFilter="false"> 
                    </obout:Column>
                    <obout:Column DataField="17" HeaderText="17"  Width="50px" AllowSorting="false" AllowFilter="false">
                    </obout:Column>
                    <obout:Column DataField="18" HeaderText="18"  Width="50px" AllowSorting="false" AllowFilter="false">
                    </obout:Column>
                    <obout:Column DataField="19" HeaderText="19"  Width="50px" AllowSorting="false" AllowFilter="false">
                    </obout:Column>
                    <obout:Column DataField="20" HeaderText="20"  Width="50px" AllowSorting="false" AllowFilter="false">
                    </obout:Column>
                    <obout:Column DataField="21" HeaderText="21"  Width="50px" AllowSorting="false" AllowFilter="false">
                    </obout:Column>
                    <obout:Column DataField="22" HeaderText="22"  Width="50px" AllowSorting="false" AllowFilter="false">
                    </obout:Column>
                    <obout:Column DataField="23" HeaderText="23"  Width="50px" AllowSorting="false" AllowFilter="false">
                    </obout:Column>
                    <obout:Column DataField="24" HeaderText="24"  Width="50px" AllowSorting="false" AllowFilter="false">
                    </obout:Column>
                    <obout:Column DataField="25" HeaderText="25"  Width="50px" AllowSorting="false" AllowFilter="false">
                    </obout:Column>
                    <obout:Column DataField="26" HeaderText="26"  Width="50px" AllowSorting="false" AllowFilter="false">
                    </obout:Column>
                    <obout:Column DataField="27" HeaderText="27"  Width="50px" AllowSorting="false" AllowFilter="false">
                    </obout:Column>
                     <obout:Column DataField="28" HeaderText="28"  Width="50px" AllowSorting="false" AllowFilter="false">
                    </obout:Column>
                     <obout:Column DataField="29" HeaderText="29"  Width="50px" AllowSorting="false" AllowFilter="false">
                    </obout:Column>
                     <obout:Column DataField="30" HeaderText="30"  Width="50px" AllowSorting="false" AllowFilter="false">
                    </obout:Column>
                     <obout:Column DataField="31" HeaderText="31"  Width="50px" AllowSorting="false" AllowFilter="false">
                    </obout:Column>
                     <obout:Column DataField="TotalP" HeaderText="Total"  Width="100px" AllowSorting="false" AllowFilter="false">
                    </obout:Column>
                    </Columns>
                <%--<FilteringSettings InitialState="Visible" />--%>
                <PagingSettings Position="Bottom" />
               <%--<FilteringSettings InitialState="Visible" FilterPosition="Top" FilterLinksPosition="Top" />--%>
               </obout:Grid>
		              
               
        </td>
    </tr>
</table>
