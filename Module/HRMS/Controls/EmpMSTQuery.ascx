<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmpMSTQuery.ascx.cs" Inherits="Module_HRMS_Controls_EmpMSTQuery" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="obout" %>
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
<table class="tContentArial">
    <tr>
        <td class="td">
            <table>
                <tr>
                    <td id="tdPrint" runat="server">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                            Width="48" Height="41" TabIndex="9"></asp:ImageButton>
                    </td>
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
            <span class="titleheading">Employee Master Query</span>
        </td>
    </tr>
    <tr>
        <td align="left" valign="top" class="td">
                <obout:Grid ID="Grid1" runat="server" AllowAddingRecords="False" AllowFiltering="True"
                AllowPaging="true" PageSize="5" AutoGenerateColumns="False" 
                onselect="Grid1_Select" AutoPostBackOnSelect="True">
                <Columns>
                    <obout:Column DataField="CADDER_CODE" HeaderText="Cadder Code" Width="100px">
                    </obout:Column>
                    <obout:Column DataField="EMP_CODE" HeaderText="Emp Code" Width="100px">
                    </obout:Column>
                    <obout:Column DataField="USER_NAME" HeaderText="User Name" Width="100px">
                    </obout:Column>
                    <obout:Column DataField="F_NAME" HeaderText="First Name" Width="100px">
                    </obout:Column>
                    <obout:Column DataField="M_NAME" HeaderText="Middle Name" Width="100px">
                    </obout:Column>
                    <obout:Column DataField="L_NAME" HeaderText="Last Name" Width="100px">
                    </obout:Column>
                    <obout:Column DataField="GENDER" HeaderText="Gender" Width="100px">
                    </obout:Column>
                    <obout:Column DataField="NATION" HeaderText="Nation" Width="100px">
                    </obout:Column>
                    <obout:Column DataField="SALUTATION" HeaderText="Salutation" Width="100px">
                    </obout:Column>
                    <obout:Column DataField="DEAR" HeaderText="Dear" Width="100px">
                    </obout:Column>
                    <obout:Column DataField="DOB" HeaderText="Date of Birth" Width="100px">
                    </obout:Column>
                    <obout:Column DataField="F_H_NAME" HeaderText="F/H Name" Width="100px">
                    </obout:Column>
                    <obout:Column DataField="RELATIONSHIP" HeaderText="RelationShip" Width="100px">
                    </obout:Column>
                    <obout:Column DataField="MRTL_STATUS" HeaderText="Marital Status" Width="100px">
                    </obout:Column>
                    <obout:Column DataField="RELIGION" HeaderText="Religion" Width="100px">
                    </obout:Column>
                    <obout:Column DataField="JOIN_DT" HeaderText="Joining Date" Width="100px">
                    </obout:Column>
                    <obout:Column DataField="SFT_NAME" HeaderText="Shift Name" Width="100px">
                    </obout:Column>
                    <obout:Column DataField="DESIG_NAME" HeaderText="Designation" Width="100px">
                    </obout:Column>
                    <obout:Column DataField="PAY_MODE" HeaderText="Payment Mode" Width="100px">
                    </obout:Column>
                    <obout:Column DataField="SKILL" HeaderText="Skill" Width="100px">
                    </obout:Column>
                    <obout:Column DataField="CONF" HeaderText="Confirmation" Width="100px">
                    </obout:Column>
                    <obout:Column DataField="LAST_INC" HeaderText="Last Increment" Width="100px">
                    </obout:Column>
                    <obout:Column DataField="LAST_PROMO" HeaderText="Last Promotion" Width="100px">
                    </obout:Column>
                    <obout:Column DataField="MST_DESC" HeaderText="Grade" Width="100px">
                    </obout:Column>
                    <obout:Column DataField="EMAIL_ID" HeaderText="Email ID" Width="100px">
                    </obout:Column>
                    <obout:Column DataField="EMP_TYPE" HeaderText="Employee Type" Width="100px">
                    </obout:Column>
                    <obout:Column DataField="DEPT_NAME" HeaderText="Department" Width="100px">
                    </obout:Column>
                    <obout:Column DataField="BRANCH_NAME" HeaderText="Branch" Width="100px">
                    </obout:Column>
                    </Columns>
                <%--<MasterDetailSettings LoadingMode="OnLoad" />    --%>      
                <FilteringSettings InitialState="Visible" />
                <PagingSettings Position="Bottom" />
               <FilteringSettings InitialState="Visible" FilterPosition="Top" FilterLinksPosition="Top" />
               </obout:Grid>
		     <asp:Label ID="lblEmpMed"  runat="server" Text="EMPLOYEE MEDICAL  DETAILS :"></asp:Label>
                <obout:Grid ID="Grid2" runat="server" AllowAddingRecords="False" AllowFiltering="True"
                AllowPaging="true" PageSize="5" AutoGenerateColumns="False" >
                     <Columns>
                        <obout:Column DataField="EMP_MED_ID" Visible="false"></obout:Column>
                        <obout:Column DataField="EMP_BLD_GRP" HeaderText="Blood Group"></obout:Column>
                        <obout:Column DataField="EMP_HIGHT" HeaderText="Height"></obout:Column> 
                        <obout:Column DataField="EMP_WEIGHT" HeaderText="Weight"></obout:Column>
                        <obout:Column DataField="EMP_B_MARKS" HeaderText="Birth Mark"></obout:Column>
                        <obout:Column DataField="ESI" HeaderText="ESI"></obout:Column>
                        <obout:Column DataField="CHK_DIS" HeaderText="COUNTRY"></obout:Column> 
                        <obout:Column DataField="DIS" HeaderText="Disease"></obout:Column>
                        <obout:Column DataField="DIS_REMARKS" HeaderText="Disability Remarks"></obout:Column>
                        <obout:Column DataField="H_REMARKS" HeaderText="Health Remarks"></obout:Column>
                        <obout:Column DataField="EMER_NAME" HeaderText="Emer Name"></obout:Column> 
                        <obout:Column DataField="EMER_ADD" HeaderText="Emer Address"></obout:Column>
                        <obout:Column DataField="EMER_CITY" HeaderText="Emer City"></obout:Column>
                        <obout:Column DataField="EMER_STATE" HeaderText="Emer State"></obout:Column>
                        <obout:Column DataField="EMER_PIN" HeaderText="Emer Pin"></obout:Column> 
                        <obout:Column DataField="EMER_COUNTRY" HeaderText="Emer Country"></obout:Column>
                        <obout:Column DataField="EMER_M_NO" HeaderText="Emer Mobile"></obout:Column>
                        <obout:Column DataField="EMER_TEL_NO" HeaderText="Emer Phone"></obout:Column>
                        <obout:Column DataField="EMER_FAX" HeaderText="Emer FAX"></obout:Column> 
                        <obout:Column DataField="EMER_EMAIL" HeaderText="Emer Email"></obout:Column>                
                    </Columns>
                </obout:Grid>
                <asp:Label ID="lblEmpCompInfo"  runat="server" Text="EMPLOYEE COMPANY INFO :"></asp:Label>
                <obout:Grid ID="Grid3" runat="server" AllowAddingRecords="False" AllowFiltering="True"
                AllowPaging="true" PageSize="5" AutoGenerateColumns="False" >
                    <Columns>
                        <obout:Column DataField="EMP_COMP_INFO" Visible="false"></obout:Column>
                        <obout:Column DataField="AC_NO" HeaderText="Account No"></obout:Column>
                        <obout:Column DataField="DL_NO" HeaderText="Driving Licence No"></obout:Column>
                        <obout:Column DataField="DL_ISS_DT" HeaderText="Driving Licence Issue Date"></obout:Column> 
                        <obout:Column DataField="PASSPORT_NO" HeaderText="Passport No"></obout:Column>  
                        <obout:Column DataField="PASSPORT_ISS_DT" HeaderText="Passport Issue Date"></obout:Column>
                        <obout:Column DataField="PAN_NO" HeaderText="PAN No"></obout:Column>
                        <obout:Column DataField="PF_AC_NO" HeaderText="PF Account No"></obout:Column>       
                        <obout:Column DataField="PR_ADD" HeaderText="Present Address"></obout:Column>
                        <obout:Column DataField="PR_CITY" HeaderText="Present City"></obout:Column>
                        <obout:Column DataField="PR_STATE" HeaderText="Present COUNTRY"></obout:Column>       
                        <obout:Column DataField="PR_COUNTRY" HeaderText="Present COUNTRY"></obout:Column>
                        <obout:Column DataField="PR_PIN_NO" HeaderText="Present Pin No"></obout:Column>
                        <obout:Column DataField="PR_TEL_NO" HeaderText="Present Phone No"></obout:Column> 
                        <obout:Column DataField="PR_FAX_NO" HeaderText="Present FAX"></obout:Column>
                        <obout:Column DataField="PR_EMAIL_ID" HeaderText="Present Email"></obout:Column> 
                        <obout:Column DataField="PERM_ADD" HeaderText="Present Address"></obout:Column>
                        <obout:Column DataField="PERM_CITY" HeaderText="Permanent City"></obout:Column>
                        <obout:Column DataField="PERM_STATE" HeaderText="Permanent State"></obout:Column>       
                        <obout:Column DataField="PERM_COUNTRY" HeaderText="Permanent COUNTRY"></obout:Column>
                        <obout:Column DataField="PERM_PIN_NO" HeaderText="Permanent Pin No"></obout:Column>
                        <obout:Column DataField="PERM_TEL_NO" HeaderText="Permanent Phone No"></obout:Column> 
                        <obout:Column DataField="PERM_FAX_NO" HeaderText="Permanent FAX"></obout:Column>
                        <obout:Column DataField="PERM_EMAIL_ID" HeaderText="Permanent Email"></obout:Column> 
                        <obout:Column DataField="INSURANCE" HeaderText="Insurance"></obout:Column>
                        <obout:Column DataField="DISPENSARY" HeaderText="Dispensary"></obout:Column>
                        <obout:Column DataField="BANK_NAME" HeaderText="Bank Name"></obout:Column>       
                                            
                    </Columns>
                </obout:Grid>
                <asp:Label ID="lblEmpQual"  runat="server" Text="EMPLOYEE QUALIFICATION :"></asp:Label>
                   <obout:Grid ID="Grid4" runat="server" AllowAddingRecords="False" AllowFiltering="True"
                AllowPaging="true" PageSize="5" AutoGenerateColumns="False" >
                    <Columns>
                        <obout:Column DataField="EMP_QUAL_ID" Visible="false"></obout:Column>
                        <obout:Column DataField="EXAM" HeaderText="Exam"></obout:Column>
                        <obout:Column DataField="SCH_COL" HeaderText="School/College"></obout:Column>
                        <obout:Column DataField="PASS_YEAR" HeaderText="COUNTRY"></obout:Column>
                        <obout:Column DataField="BOARD_UNIV" HeaderText="Board/University"></obout:Column>
                        <obout:Column DataField="GRADE" HeaderText="Grade"></obout:Column>
                        <obout:Column DataField="PERCENT" HeaderText="Percent"></obout:Column>
                                                     
                    </Columns>
                </obout:Grid> 
                 <asp:Label ID="lblEmpLang"  runat="server" Text="EMPLOYEE LANGUAGE KNOWN :"></asp:Label>
                   <obout:Grid ID="Grid6" runat="server" AllowAddingRecords="False" AllowFiltering="True"
                AllowPaging="true" PageSize="5" AutoGenerateColumns="False" >
                    <Columns>
                        <obout:Column DataField="EMP_LANG_ID" Visible="false"></obout:Column>
                        <obout:Column DataField="EMP_LANG" HeaderText="Language"></obout:Column>
                        <obout:Column DataField="Read" HeaderText="Read"></obout:Column>   
                        <obout:Column DataField="Speak" HeaderText="Speak"></obout:Column>
                        <obout:Column DataField="Write" HeaderText="Write"></obout:Column>
                                
                    </Columns>
                </obout:Grid> 
                  <asp:Label ID="lblEmpFamInd"  runat="server" Text="EMPLOYEE FAMILY INDEPENDENT :"></asp:Label>       
                  <obout:Grid ID="Grid5" runat="server" AllowAddingRecords="False" AllowFiltering="True"
                AllowPaging="true" PageSize="5" AutoGenerateColumns="False" >
                    <Columns>
                        <obout:Column DataField="EMP_DEPEND_ID" Visible="false"></obout:Column>
                        <obout:Column DataField="I_F_NAME" HeaderText="First Name"></obout:Column>
                        <obout:Column DataField="I_L_NAME" HeaderText="Last Name"></obout:Column>
                        <obout:Column DataField="I_DOB" HeaderText="Date Of Birth"></obout:Column>
                        <obout:Column DataField="I_SEX" HeaderText="Sex"></obout:Column>
                        <obout:Column DataField="I_RELATION" HeaderText="Relation"></obout:Column>
                    </Columns>
                
          </obout:Grid>
        </td>
    </tr>
</table>
