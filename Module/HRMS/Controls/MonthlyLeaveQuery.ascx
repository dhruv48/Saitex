<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MonthlyLeaveQuery.ascx.cs" Inherits="Module_HRMS_Controls_MonthlyLeaveQuery" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc1" %>
<%@ Register TagPrefix="cc3" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>

<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;}
    .header
    {
        margin-left: 4px;
    }
    .c1
    {
        width: 50px;
    }
    .c2
    {
        margin-left: 4px;
        width: 90px;
    }
    .c3
    {
        margin-left: 4px;
        width: 80px;
    }
    .tdText
    {
        font: 11px Verdana;
        color: #333333;
    }
    .option2
    {
        font: 11px Verdana;
        color: #0033cc;
        padding-left: 4px;
        padding-right: 4px;
    }
    a
    {
        font: 11px Verdana;
        color: #315686;
        text-decoration: underline;
    }
    a:hover
    {
        color: Teal;
    }
    .ob_gMCont_DT
    {
        overflow: hidden;
    }
    .ob_gMCont
    {
        position: relative;
    }
</style>
<table class="tContentArial">
    <tr>
        <td class="td">
            <table>
          <%--      <td>
                    <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                        Width="48" Height="41" ></asp:ImageButton>
                </td>--%>
                <td>
                    <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                        Width="48" Height="41" onclick="imgbtnExit_Click" ></asp:ImageButton>
                </td>
                <td>
                    <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                        Width="48" Height="41"></asp:ImageButton>
                </td>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" valign="top" class="tRowColorAdmin td">
            <span class="titleheading">Monthly Leave Query</span>
        </td>
    </tr>
    <tr>
        <td class="td" align="center">
          <table cellpadding="0" cellspacing="0" border="0" align="left" class="tContentArial">
    <tr><td>From Date
         <asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox> <cc4:CalendarExtender ID="ce1" runat="server" TargetControlID="txtFromDate" PopupPosition="TopLeft">
                        </cc4:CalendarExtender></td>
       <td>To Date
     
     <asp:TextBox ID="txtToDate" runat="server"></asp:TextBox> 
          <cc4:calendarextender ID="ce2" runat="server" TargetControlID="txtToDate" 
              PopupPosition="TopLeft">
                        </cc4:calendarextender>
        <asp:Button ID="btndisplay" runat="server" Text="Submit" 
            onclick="btndisplay_Click" /></td>
       
    </tr>
    </table>
    </td>
    </tr>
       <tr>
        <td id="tdShowGrid" class="td" runat="server">
            <table>
                <td align="left">
                    <cc2:Grid ID="Grid1" runat="server" AllowAddingRecords="False" AllowFiltering="True" AllowPaging="true"
                        PageSize="5" AutoGenerateColumns="False" >
                        <Columns>
                            <cc2:Column DataField="EMP_CODE" HeaderText="Employee Code" Visible="false" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="EmployeeName" HeaderText="Employee Name" Width="150px">
                            </cc2:Column>
                            <cc2:Column DataField="DESIG_NAME" HeaderText="Designation" Width="120px">
                            </cc2:Column>
                            <cc2:Column DataField="EMER_M_NO" HeaderText="Contact No" Width="100px">
                            </cc2:Column>
                             <cc2:Column DataField="EMER_ADD" HeaderText="Address" Width="220px">
                            </cc2:Column>
                            <cc2:Column DataField="BRANCH_NAME" HeaderText="Branch" Width="100px">
                            </cc2:Column>
                          <%--  <cc2:Column DataField="ON_FROM" HeaderText="From Place" >
                            </cc2:Column>
                            <cc2:Column DataField="PLACE" HeaderText="To Place" >
                            </cc2:Column>--%>
                            
                            <cc2:Column DataField="SFT_NAME" HeaderText="Shift" Width="100px">
                            </cc2:Column>
                             <cc2:Column DataField="TDATE" HeaderText="Date" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="LV_TOTAL_DAYS" HeaderText="Days" Width="65px">
                            </cc2:Column>
                             <cc2:Column DataField="LV_FROM_DATE" HeaderText="From Date" Width="100px">
                            </cc2:Column>
                             <cc2:Column DataField="LV_TO_DATE" HeaderText="To Date" Width="100px">
                            </cc2:Column>
                          
                           </Columns>
		 <PagingSettings Position="Bottom"/>
          <FilteringSettings InitialState="Visible" FilterPosition="Top" FilterLinksPosition="Top" />
                    </cc2:Grid>
           <%--<cc2:Grid id="Grid1" runat="server" CallbackMode="true" Serialize="false" AutoGenerateColumns="false" FolderStyle="styles/black_glass" AllowAddingRecords="false"    AllowPageSizeSelection="false"  AllowFiltering="true"  AllowColumnReordering="true">
			<Columns>
			    <cc2:Column ID="EMP_CODE" DataField="EMP_CODE" Visible="false" HeaderText="Employee code" Width="125" runat="server"/>
			    <cc2:Column ID="EMPLOYEENAME" DataField="EMPLOYEENAME" HeaderText="Employee Name" runat="server"/>
				 <cc2:Column ID="DESIGNATION" DataField="DESIGNATION" HeaderText="Designation" Width="125" runat="server"/>
			    <cc2:Column ID="EMER_M_NO" DataField="EMER_M_NO" HeaderText="Employee Name" runat="server"/>
			     <cc2:Column ID="EMER_ADD" DataField="EMER_ADD" HeaderText="Employee code" Width="125" runat="server"/>
			    <cc2:Column ID="BRANCH_NAME" DataField="BRANCH_NAME"  runat="server"/>
			     <cc2:Column ID="ON_FROM" DataField="ON_FROM"  HeaderText="From Place" Width="125" runat="server"/>
			    <cc2:Column ID="PLACE" DataField="PLACE" HeaderText="To Place" runat="server"/>
			 <%--  <cc2:Column ID="TDATE" DataField="TDATE"  HeaderText="Employee code" Width="125" runat="server"/>
			    <cc2:Column ID="FROM_DATE" DataField="FROM_DATE" HeaderText="Employee Name" runat="server"/>
			     <cc2:Column ID="TO_DATE" DataField="TO_DATE"  HeaderText="Employee code" Width="125" runat="server"/>--%>
			 <%--<cc2:Column ID="SHIFTNAME" DataField="SHIFTNAME" HeaderText="Shift Name" runat="server"/>
			     <cc2:Column ID="DEPARTMENT" DataField="DEPARTMENT" HeaderText="Department" Width="125" runat="server"/>
			    <cc2:Column ID="LV_DAYS" DataField="LV_DAYS" HeaderText="No Of Days" runat="server"/>
				<cc2:Column ID="TDATE" DataFormatString="{0:M/d/yyyy}" DataField="TDATE" HeaderText="Date" Width="200" runat="server">
				    <FilterOptions>
				        <cc2:CustomFilterOption IsDefault="true" ID="Between_Date" Text="Between">
				            <TemplateSettings FilterTemplateId="DateBetweenFilter" 
				                FilterControlsIds="StartDate_Date,EndDate_Date"
				                FilterControlsPropertyNames="value,value" />
				        </cc2:CustomFilterOption>
				    </FilterOptions>
				</cc2:Column>
				
				<cc2:Column ID="FROM_DATE" DataFormatString="{0:M/d/yyyy}" DataField="FROM_DATE" HeaderText="FROM DATE" Width="200" runat="server">
				    <FilterOptions>
				        <cc2:CustomFilterOption IsDefault="true" ID="Between_FromDate" Text="Between">
				            <TemplateSettings FilterTemplateId="FromDateBetweenFilter" 
				                FilterControlsIds="StartDate_FromDate,EndDate_FromDate"
				                FilterControlsPropertyNames="value,value" />
				        </cc2:CustomFilterOption>
				    </FilterOptions>
				</cc2:Column>
				
				<cc2:Column ID="TO_DATE" DataFormatString="{0:M/d/yyyy}" DataField="TO_DATE" HeaderText="TO DATE" Width="200" runat="server">
				    <FilterOptions>
				        <cc2:CustomFilterOption IsDefault="true" ID="Between_RequiredDate" Text="Between">
				            <TemplateSettings FilterTemplateId="ToDateBetweenFilter" 
				                FilterControlsIds="StartDate_ToDate,EndDate_ToDate"
				                FilterControlsPropertyNames="value,value" />
				        </cc2:CustomFilterOption>
				      </FilterOptions>
				</cc2:Column>
			</Columns>
			<FilteringSettings InitialState="Visible" />
			<Templates>
			    <cc2:GridTemplate runat="server" ID="DateBetweenFilter">
			        <Template>
			            <div style="width: 99%;padding: 0px;margin: 0px; font-size: 5px;">
			                <cc3:OboutTextBox runat="server" ID="StartDate_Date" Width="45%" />
			                
			             <cc3:OboutTextBox runat="server" ID="EndDate_Date" Width="45%" />
			            </div>
			        </Template>
			    </cc2:GridTemplate>
			  
			    <cc2:GridTemplate runat="server" ID="ShippedDateBetweenFilter">
			        <Template>
			            <div style="width: 99%;padding: 0px;margin: 0px; font-size: 5px;">
			                <cc3:OboutTextBox runat="server" ID="StartDate_FromDate" Width="45%" />
			                
			                <cc3:OboutTextBox runat="server" ID="EndDate_ShippedDate" Width="45%" />
			            </div>
			        </Template>
			    </cc2:GridTemplate>
			    
			    <cc2:GridTemplate runat="server" ID="RequiredDateBetweenFilter">
			        <Template>
			            <div style="width: 99%;padding: 0px;margin: 0px; font-size: 5px;">
			                <cc3:OboutTextBox runat="server" ID="StartDate_RequiredDate" Width="45%" />
			               
			                <cc3:OboutTextBox runat="server" ID="EndDate_RequiredDate" Width="45%" />
			            </div>
			        </Template>
			    </cc2:GridTemplate>
			</Templates>
		</cc2:Grid>--%>
		 
                </td>
            </table>
        </td>
    </tr>
</table>
