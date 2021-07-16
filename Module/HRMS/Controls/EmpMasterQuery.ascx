<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmpMasterQuery.ascx.cs" Inherits="Module_HRMS_Controls_EmpMasterQuery" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<link type="text/css" rel="Stylesheet" href="../../../StyleSheet/css/dialog.css" />
    <link type="text/css" rel="Stylesheet" href="../../../StyleSheet/css/pager.css" />
    <link type="text/css" rel="Stylesheet" href="../../../StyleSheet/css/grid.css" />
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
    .tdText
     {
		font:11px Verdana;
		color:#333333;
		
	}
	.option2
	{
		font:11px Verdana;
		color:#0033cc;				
		padding-left:4px;
		padding-right:4px;
	 }
	a
	 {
		font:11px Verdana;
		color:#315686;
		text-decoration:underline;
	  }
	a:hover 
	 {
		color:crimson;
	 }
</style>
<table align="left" width="100%" class="td tContent">
    <tr>
        <td colspan="2" class="td">
            <table class="tContent">
                <tr>                      
                    <td ID="tdClear" runat="server" valign="top">
                        <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" 
                            ImageUrl="~/CommonImages/clear.jpg" OnClick="imgbtnClear_Click" 
                            OnClientClick="javascript:return window.confirm('Are you sure you want to clear this record')" 
                            TabIndex="8" ToolTip="Clear" Width="48" />
                    </td>
                    <td ID="tdPrint" runat="server" valign="top">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" 
                            ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click" 
                            TabIndex="9" ToolTip="Print" Width="48" />
                    </td>
                    <td ID="tdExit" runat="server" valign="top">
                        <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click" TabIndex="10" ToolTip="Exit" Width="48" />
                    </td>
                    <td ID="tdHelp" runat="server" valign="top">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41"  ImageUrl="~/CommonImages/link_help.png" TabIndex="11" ToolTip="Help" Width="48" />
                    </td>
                </tr>
    </table>
      </td> 
    </tr>
    <tr>
        <td colspan="2" align="center" valign="top" class="tRowColorAdmin td">
            <span class="titleheading">EMPLOYEE MASTER QUERY</span>
        </td>
    </tr>
    <tr>
       <td>
           <asp:Label ID="lblMode" runat="server"></asp:Label> 
        </td>
     </tr> 
    <tr>
        <td colspan="2">
            <table width="100%">
              <tr>             
                    <td>Branch:</td>
                    <td><asp:DropDownList ID="DDLBranch" Width="130px" CssClass="TextBox SmallFont"  runat="server" >  </asp:DropDownList>  </td>
                  <td>Department:</td>
                        <td>
                            <asp:DropDownList ID="DDLDepartment" Width="130px" CssClass="TextBox SmallFont"   runat="server" ></asp:DropDownList>
                        </td> 
                   <td>Designation:</td>
                    <td><asp:DropDownList ID="DDLDesigination" Width="130px" CssClass="TextBox SmallFont"  runat="server" ></asp:DropDownList>  </td>
                     
                    <td>Shift:</td>
                    <td><asp:DropDownList ID="DDLShift" DataValueField="SFT_ID" Width="130px" CssClass="TextBox SmallFont"  DataTextField="SFT_NAME"  runat="server" >
                            </asp:DropDownList>  </td> 
                </tr>
                 <tr>   
                 <td>Cader Code:</td>
                        <td><asp:DropDownList ID="DDLCader" Width="130px" CssClass="TextBox SmallFont" A runat="server" ></asp:DropDownList></td>                      
                 <td>Employee:</td>
                        <td><asp:DropDownList ID="DDLEmployee" Width="130px" CssClass="TextBox SmallFont"  runat="server" ></asp:DropDownList></td>                      
                        <td><asp:Button ID="CmdViewRecord" runat="server" Text="View Record" 
                                onclick="CmdViewRecord_Click" /></td>
                </tr> 
            </table>
        </td>      
    </tr>
  </table>  
<asp:Panel ID="pnl1" runat="server"  ScrollBars="Both" Height="400px" Width="970px">
  <asp:GridView ID="grdpnl1"  runat="server" AutoGenerateColumns="false"  ShowFooter="false" AllowPaging="true" Width="100%" OnPageIndexChanging="grdpnl1_PageChanging" OnRowDataBound="Grid_dataBouund" >
     <Columns>
     
        <asp:TemplateField HeaderText="Emp_Code"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Justify">
           <ItemTemplate>
               <asp:LinkButton ID="Emp_Code" runat="server" CssClass="LabelNo SmallFont" ReadOnly="true" Text='<%# Bind("EMP_CODE") %>' Width="40px"></asp:LinkButton>
                <asp:Panel ID="Imagepnl" runat="server" BorderStyle="Ridge" BorderWidth="4px" BorderColor="Gray">
                                            <asp:Image ID="imgDesignImage" runat="server" Width="124px" Height="85px" ImageUrl="~/CommonImages/ImageResizer/No_Image.jpg" />
                                        </asp:Panel>
                                        <cc1:HoverMenuExtender ID="hmeShow" runat="server" TargetControlID="Emp_Code"
                                            PopupControlID="Imagepnl" PopupPosition="Right"  />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="User Name"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Justify">
           <ItemTemplate>
               <asp:Label ID="user_Name" runat="server" CssClass="LabelNo SmallFont" ReadOnly="true" Text='<%# Bind("USER_NAME") %>' Width="40px"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="EmployeeName"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Justify">
           <ItemTemplate>
               <asp:Label ID="EmployeeName" runat="server" CssClass="LabelNo SmallFont" ReadOnly="true" Text='<%# Bind("EmployeeName") %>' Width="40px"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <%--<asp:TemplateField HeaderText="Last_Name"  HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
           <ItemTemplate>
               <asp:Label ID="Last_Name" runat="server" CssClass="LabelNo SmallFont" ReadOnly="true" Text='<%# Bind("L_NAME") %>' Width="40px"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>--%>
        <asp:TemplateField HeaderText="EmailId "  HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="50px" ItemStyle-Wrap="true"  ItemStyle-HorizontalAlign="Justify">
           <ItemTemplate>
               <asp:Literal ID="Email_ID" runat="server"  Text='<%# Bind("EMAIL_ID") %>' ></asp:Literal>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Department"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Justify">
           <ItemTemplate>
               <asp:Label ID="Department" runat="server" CssClass="LabelNo SmallFont" ReadOnly="true" Text='<%# Bind("DEPT_NAME") %>' Width="40px"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Designation"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Justify">
           <ItemTemplate>
               <asp:Label ID="Designation" runat="server" CssClass="LabelNo SmallFont" ReadOnly="true" Text='<%# Bind("DESIG_NAME") %>' Width="40px"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Other_Detail"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Justify">
           <ItemTemplate>
               <asp:LinkButton ID="Other_detail" runat="server" CssClass="LabelNo SmallFont" ReadOnly="true" Text='OTHER DETAIL' Width="40px"></asp:LinkButton>
              
               <asp:Panel ID="pnl2" runat="server" BackColor="#C5E7F1" BorderColor="BlueViolet" BorderStyle="Solid" BorderWidth="5px" HorizontalAlign="Left">
                
              <table>
              <tr class="grid" style="background-color:#315686">
              <td  style="color:White ; border-color:Black; ">CADDER CODE</td>
              <td  style="color:White; border-color:Black">CARD NO</td>
               <td style="color:White; border-color:Black" >GENDER</td>
                <td style="color:White; border-color:Black" >NATIONALITY</td>
                 <td style="color:White; border-color:Black">FATHER/HUSBAND NAME</td>
                  <td style="color:White; border-color:Black" >RELATION</td>
                  <td style="color:White; border-color:Black" >MARITAL STATUS</td>
                  <td  style="color:White; border-color:Black">GRADE</td>
                  <td  style="color:White; border-color:Black">EMPLOYEE TYPE</td>
                  <td  style="color:White; border-color:Black">JOINING DATE</td>
                  <td  style="color:White; border-color:Black">DATE OF BIRTH</td>
                  <td  style="color:White; border-color:Black">BRANCH NAME</td>
                  <td  style="color:White; border-color:Black">POSITION</td>
                  <td style="color:White; border-color:Black">EMPLOYEE Level</td>
                  <td style="color:White; border-color:Black" >SHIFT NAME</td>
              
              </tr>
              <tr class="grid">
              <td><asp:Label id="lblid" runat="server" ReadOnly="true" Text='<%# Bind("CADDER_CODE") %>'></asp:Label></td>
              <td><asp:Label id="Label1" runat="server" ReadOnly="true" Text='<%# Bind("CARD_NO") %>'></asp:Label></td>
              <td><asp:Label id="Label2" runat="server" ReadOnly="true" Text='<%# Bind("GENDER") %>'></asp:Label></td>
              <td><asp:Label id="Label3" runat="server" ReadOnly="true" Text='<%# Bind("NATION") %>'></asp:Label></td>
              <td><asp:Label id="Label4" runat="server" ReadOnly="true" Text='<%# Bind("F_H_NAME") %>'></asp:Label></td>
              <td><asp:Label id="Label5" runat="server" ReadOnly="true" Text='<%# Bind("RELATIONSHIP") %>'></asp:Label></td>
              <td><asp:Label id="Label6" runat="server" ReadOnly="true" Text='<%# Bind("MRTL_STATUS") %>'></asp:Label></td>
              <td><asp:Label id="Label7" runat="server" ReadOnly="true" Text='<%# Bind("GRADE_ID") %>'></asp:Label></td>
              <td><asp:Label id="Label8" runat="server" ReadOnly="true" Text='<%# Bind("EMP_TYPE") %>'></asp:Label></td>
              <td><asp:Label id="Label9" runat="server" ReadOnly="true" Text='<%# Bind("JOIN_DT") %>'></asp:Label>
              </td>
              <td><asp:Label id="Label13" runat="server" ReadOnly="true" Text='<%# Bind("DOB") %>'></asp:Label>
              </td>
                <td><asp:Label id="Label10" runat="server" ReadOnly="true" Text='<%# Bind("BRANCH_CODE") %>'></asp:Label></td>
                  <td><asp:Label id="Label11" runat="server" ReadOnly="true" Text='<%# Bind("POSITION") %>'></asp:Label></td>
                    <td><asp:Label id="Label12" runat="server" ReadOnly="true" Text='<%# Bind("EMPLEVEL") %>'></asp:Label></td>
                    <td><asp:Label id="Label14" runat="server" ReadOnly="true" Text='<%# Bind("SFT_NAME") %>'></asp:Label></td>
              </tr>
              </table>
              
               
              
               
               </asp:Panel>
               <cc1:HoverMenuExtender ID="hmeBOM" runat="server" PopupControlID="pnl2" PopupPosition="Left"
                                            TargetControlID="Other_detail">
                                        </cc1:HoverMenuExtender>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Company Info"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Justify">
           <ItemTemplate>
               <asp:LinkButton ID="Company_info" runat="server" CommandArgument='<%# Bind("EMP_CODE") %>' Text='Company Info' Width="40px"></asp:LinkButton>
                <asp:Panel ID="pnl3" runat="server" BackColor="#C5E7F1" BorderColor="BlueViolet"
                                            BorderStyle="Solid" BorderWidth="5px" HorizontalAlign="Left">
                                            <asp:GridView ID="grdpnl2" runat="server" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="EMP COMPANY INFO" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Justify">
                                                        <ItemTemplate>
                                                            <asp:Label ID="EMP_COMPANY_INFO" runat="server" CssClass="SmallFont LabelNo" Text='<%# Bind("EMP_COMP_INFO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ACCOUNT NO" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Justify">
                                                        <ItemTemplate>
                                                            <asp:Label ID="AC_NO" runat="server" CssClass="SmallFont LabelNo" Text='<%# Bind("AC_NO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PASSPORT NO">
                                                        <ItemTemplate>
                                                            <asp:Label ID="PASSPORT_NO" runat="server" CssClass="SmallFont Label" Text='<%# Bind("PASSPORT_NO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PAN CARD NO" HeaderStyle-HorizontalAlign="Right"
                                                        ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="PAN_NO" runat="server" CssClass="SmallFont LabelNo" Text='<%# Bind("PAN_NO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PF_AC_NO">
                                                        <ItemTemplate>
                                                            <asp:Label ID="PF_AC_NO" runat="server" CssClass="SmallFont Label" Text='<%# Bind("PF_AC_NO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ADDRESS">
                                                        <ItemTemplate>
                                                            <asp:Label ID="PR_ADD" runat="server" CssClass="SmallFont Label" Text='<%# Bind("PR_ADD") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CITY">
                                                        <ItemTemplate>
                                                            <asp:Label ID="PR_CITY" runat="server" CssClass="SmallFont Label" Text='<%# Bind("PR_CITY") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="STATE">
                                                        <ItemTemplate>
                                                            <asp:Label ID="PR_STATE" runat="server" CssClass="SmallFont LabelNo" Text='<%#Bind( "PR_STATE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PIN NO">
                                                        <ItemTemplate>
                                                            <asp:Label ID="PR_PIN_NO" runat="server" CssClass="SmallFont LabelNo" Text='<%# Bind("PR_PIN_NO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="BANK CODE">
                                                        <ItemTemplate>
                                                            <asp:Label ID="BANK_CODE" runat="server" CssClass="SmallFont LabelNo" Text='<%# Bind("BANK_CODE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <RowStyle CssClass="SmallFont" />
                                                <HeaderStyle BackColor="#336699" CssClass="SmallFont titleheadingGrid" />
                                            </asp:GridView>
                                        </asp:Panel>
                                        <cc1:HoverMenuExtender ID="hmeBOM1" runat="server" PopupControlID="pnl3" PopupPosition="Left"
                                            TargetControlID="Company_info">
                                        </cc1:HoverMenuExtender>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Medical Detail"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Justify">
           <ItemTemplate>
               <asp:LinkButton ID="Medical_Detail" runat="server"  CommandArgument='<%# Bind("EMP_CODE") %>'  Text='MEDICAL DETAIL' Width="40px"></asp:LinkButton>
                <asp:Panel ID="pnl4" runat="server" BackColor="#C5E7F1" BorderColor="BlueViolet"
                                            BorderStyle="Solid" BorderWidth="5px" HorizontalAlign="Left">
                                            <asp:GridView ID="grdpnl3" runat="server" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="EMP MED ID" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Justify">
                                                        <ItemTemplate>
                                                            <asp:Label ID="EMP_MED_ID" runat="server" CssClass="SmallFont LabelNo" Text='<%# Bind("EMP_MED_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="BLD GRP" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Justify">
                                                        <ItemTemplate>
                                                            <asp:Label ID="EMP_BLD_GRP" runat="server" CssClass="SmallFont LabelNo" Text='<%# Bind("EMP_BLD_GRP") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="HEIGHT">
                                                        <ItemTemplate>
                                                            <asp:Label ID="EMP_HIGHT" runat="server" CssClass="SmallFont Label" Text='<%# Bind("EMP_HIGHT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="WEIGHT" HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-HorizontalAlign="Justify">
                                                        <ItemTemplate>
                                                            <asp:Label ID="EMP_WEIGHT" runat="server" CssClass="SmallFont LabelNo" Text='<%# Bind("EMP_WEIGHT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="EMERGENCY CONTACT">
                                                        <ItemTemplate>
                                                            <asp:Label ID="EMER_NAME" runat="server" CssClass="SmallFont Label" Text='<%# Bind("EMER_NAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="EMERGENCY ADDRESS">
                                                        <ItemTemplate>
                                                            <asp:Label ID="EMER_ADD" runat="server" CssClass="SmallFont Label" Text='<%# Bind("EMER_ADD") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CITY">
                                                        <ItemTemplate>
                                                            <asp:Label ID="EMER_CITY" runat="server" CssClass="SmallFont Label" Text='<%# Bind("EMER_CITY") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="STATE">
                                                        <ItemTemplate>
                                                            <asp:Label ID="EMER_STATE" runat="server" CssClass="SmallFont LabelNo" Text='<%#Bind( "EMER_STATE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PIN">
                                                        <ItemTemplate>
                                                            <asp:Label ID="EMER_PIN" runat="server" CssClass="SmallFont LabelNo" Text='<%# Bind("EMER_PIN") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="MOBILE NO">
                                                        <ItemTemplate>
                                                            <asp:Label ID="EMER_M_NO" runat="server" CssClass="SmallFont LabelNo" Text='<%# Bind("EMER_M_NO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <RowStyle CssClass="SmallFont" />
                                                <HeaderStyle BackColor="#336699" CssClass="SmallFont titleheadingGrid" />
                                            </asp:GridView>
                                        </asp:Panel>
                                        <cc1:HoverMenuExtender ID="hmeBOM2" runat="server" PopupControlID="pnl4" PopupPosition="Left"
                                            TargetControlID="Medical_Detail">
                                        </cc1:HoverMenuExtender> 
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="LeaveAssignDetail"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Justify">
           <ItemTemplate>
               <asp:LinkButton ID="LeaveAssignDetail" runat="server" CommandArgument='<%# Bind("EMP_CODE") %>' Text='LEAVE DETAIL' Width="40px"></asp:LinkButton>
               <asp:Panel ID="pnl5" runat="server" BackColor="#C5E7F1" BorderColor="BlueViolet"
                                            BorderStyle="Solid" BorderWidth="5px" HorizontalAlign="Left">
                                            <asp:GridView ID="grdpnl4" runat="server" AutoGenerateColumns="False">
                                                <Columns>
                                                  <asp:TemplateField HeaderText="CURRENT YEAR" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Justify">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CUR_YEAR" runat="server" CssClass="SmallFont LabelNo" Text='<%# Bind("CUR_YEAR") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="LV_MST_ID" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Justify">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LV_MST_ID" runat="server" CssClass="SmallFont LabelNo" Text='<%# Bind("LV_MST_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="LEAVE NAME">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LV_ID" runat="server" CssClass="SmallFont Label" Text='<%# Bind("LV_NAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="LEAVE DAYS" HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-HorizontalAlign="Justify">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LV_DAYS" runat="server" CssClass="SmallFont LabelNo" Text='<%# Bind("LV_DAYS") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TAKEN LEAVE DAYS">
                                                        <ItemTemplate>
                                                            <asp:Label ID="TAKEN_LV_DAYS" runat="server" CssClass="SmallFont Label" Text='<%# Bind("TAKEN_LV_DAYS") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="REMAIN LEAVE DAYS">
                                                        <ItemTemplate>
                                                            <asp:Label ID="REMAIN_LV_DAYS" runat="server" CssClass="SmallFont Label" Text='<%# Bind("REMAIN_LV_DAYS") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   
                                                         
                                                </Columns>
                                                <RowStyle CssClass="SmallFont" />
                                                <HeaderStyle BackColor="#336699" CssClass="SmallFont titleheadingGrid" />
                                            </asp:GridView>
                                        </asp:Panel>
                                        <cc1:HoverMenuExtender ID="hmeBOM3" runat="server" PopupControlID="pnl5" PopupPosition="Left"
                                            TargetControlID="LeaveAssignDetail">
                                        </cc1:HoverMenuExtender> 
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Salary Detail"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Justify">
           <ItemTemplate>
               <asp:LinkButton ID="Salary_Detail" runat="server" CssClass="LabelNo SmallFont" ReadOnly="true" Text='SALARY DETAIL' Width="40px"></asp:LinkButton>
               <asp:Panel ID="pnl6" runat="server" BackColor="#C5E7F1" BorderColor="BlueViolet"
                                            BorderStyle="Solid" BorderWidth="5px" HorizontalAlign="Left">
                                            <asp:GridView ID="grdpnl5" runat="server" AutoGenerateColumns="False">
                                                <Columns>
                                                  <asp:TemplateField HeaderText="SAL GRADE ID" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Justify">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SAL_GRADE_ID" runat="server" CssClass="SmallFont LabelNo" Text='<%# Bind("SAL_GRADE_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="HEAD ID" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Justify">
                                                        <ItemTemplate>
                                                            <asp:Label ID="HEAD_ID" runat="server" CssClass="SmallFont LabelNo" Text='<%# Bind("HEAD_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="AMOUNT">
                                                        <ItemTemplate>
                                                            <asp:Label ID="AMT" runat="server" CssClass="SmallFont Label" Text='<%# Bind("AMT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SUBHEAD NAME" HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-HorizontalAlign="Justify">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SUBH_ID" runat="server" CssClass="SmallFont LabelNo" Text='<%# Bind("SUBH_NAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="GRADE ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="GRADE_ID" runat="server" CssClass="SmallFont Label" Text='<%# Bind("GRADE_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                   
                                                         
                                                </Columns>
                                                <RowStyle CssClass="SmallFont" />
                                                <HeaderStyle BackColor="#336699" CssClass="SmallFont titleheadingGrid" />
                                            </asp:GridView>
                                        </asp:Panel>
                                        <cc1:HoverMenuExtender ID="hmeBOM4" runat="server" PopupControlID="pnl6" PopupPosition="Left"
                                            TargetControlID="Salary_Detail">
                                        </cc1:HoverMenuExtender> 
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Family_Detail"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Justify">
           <ItemTemplate>
               <asp:LinkButton ID="Family_Detail" runat="server" CssClass="LabelNo SmallFont" ReadOnly="true" Text='FAMILY DETAIL' Width="40px"></asp:LinkButton>
               <asp:Panel ID="pnl7" runat="server" BackColor="#C5E7F1" BorderColor="BlueViolet"
                                            BorderStyle="Solid" BorderWidth="5px" HorizontalAlign="Left">
                                            <asp:GridView ID="grdpnl6" runat="server" AutoGenerateColumns="False">
                                                <Columns>
                                                  <asp:TemplateField HeaderText="DEPENDENT NAME" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Justify">
                                                        <ItemTemplate>
                                                            <asp:Label ID="I_F_NAME" runat="server" CssClass="SmallFont LabelNo" Text='<%# Bind("DepName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date Of Birth" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Justify">
                                                        <ItemTemplate>
                                                            <asp:Label ID="I_DOB" runat="server" CssClass="SmallFont LabelNo" Text='<%# Bind("DOB") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SEX">
                                                        <ItemTemplate>
                                                            <asp:Label ID="I_SEX" runat="server" CssClass="SmallFont Label" Text='<%# Bind("I_SEX") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="RELATION" HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-HorizontalAlign="Justify">
                                                        <ItemTemplate>
                                                            <asp:Label ID="I_RELATION" runat="server" CssClass="SmallFont LabelNo" Text='<%# Bind("I_RELATION") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="AGE">
                                                        <ItemTemplate>
                                                            <asp:Label ID="AGE" runat="server" CssClass="SmallFont Label" Text='<%# Bind("AGE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                   
                                                         
                                                </Columns>
                                                <RowStyle CssClass="SmallFont" />
                                                <HeaderStyle BackColor="#336699" CssClass="SmallFont titleheadingGrid" />
                                            </asp:GridView>
                                        </asp:Panel>
                                        <cc1:HoverMenuExtender ID="hmeBOM5" runat="server" PopupControlID="pnl7" PopupPosition="Left"
                                            TargetControlID="Family_Detail">
                                        </cc1:HoverMenuExtender> 
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Qualification"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Justify">
           <ItemTemplate>
               <asp:LinkButton ID="Qualification" runat="server" CssClass="LabelNo SmallFont" ReadOnly="true" Text='QUALIFICATION' Width="40px"></asp:LinkButton>
               <asp:Panel ID="pnl8" runat="server" BackColor="#C5E7F1" BorderColor="BlueViolet"
                                            BorderStyle="Solid" BorderWidth="5px" HorizontalAlign="Left">
                                            <asp:GridView ID="grdpnl7" runat="server" AutoGenerateColumns="False">
                                                <Columns>
                                                  <asp:TemplateField HeaderText="EXAM" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Justify">
                                                        <ItemTemplate>
                                                            <asp:Label ID="EXAM" runat="server" CssClass="SmallFont LabelNo" Text='<%# Bind("EXAM") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SCHOOL\COLLEGE" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Justify">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SCH_COL" runat="server" CssClass="SmallFont LabelNo" Text='<%# Bind("SCH_COL") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pass Year">
                                                        <ItemTemplate>
                                                            <asp:Label ID="PASS_YEAR" runat="server" CssClass="SmallFont Label" Text='<%# Bind("PASS_YEAR") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Board/University" HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-HorizontalAlign="Justify">
                                                        <ItemTemplate>
                                                            <asp:Label ID="BOARD_UNIV" runat="server" CssClass="SmallFont LabelNo" Text='<%# Bind("BOARD_UNIV") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="GRADE">
                                                        <ItemTemplate>
                                                            <asp:Label ID="GRADE" runat="server" CssClass="SmallFont Label" Text='<%# Bind("GRADE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                     <asp:TemplateField HeaderText="PERCENT">
                                                        <ItemTemplate>
                                                            <asp:Label ID="PERCENT" runat="server" CssClass="SmallFont Label" Text='<%# Bind("PERCENT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                         
                                                </Columns>
                                                <RowStyle CssClass="SmallFont" />
                                                <HeaderStyle BackColor="#336699" CssClass="SmallFont titleheadingGrid" />
                                            </asp:GridView>
                                        </asp:Panel>
                                        <cc1:HoverMenuExtender ID="hmeBOM6" runat="server" PopupControlID="pnl8" PopupPosition="Left"
                                            TargetControlID="Qualification">
                                        </cc1:HoverMenuExtender> 
            </ItemTemplate>
        </asp:TemplateField>
       </Columns>
       <HeaderStyle BackColor="#336699"  />
 </asp:GridView>
</asp:Panel>     