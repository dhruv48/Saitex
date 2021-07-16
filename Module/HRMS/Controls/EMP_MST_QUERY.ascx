<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EMP_MST_QUERY.ascx.cs" Inherits="Module_HRMS_Controls_EMP_MST_QUERY" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<link type="text/css" rel="Stylesheet" href="../../../StyleSheet/css/dialog.css" />
    <link type="text/css" rel="Stylesheet" href="../../../StyleSheet/css/pager.css" />
    <link type="text/css" rel="Stylesheet" href="../../../StyleSheet/css/grid.css" />
<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>--%>
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
            <span class="titleheading">EMPLOYEE RECORDS</span>
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
    <tr>
        <td colspan="2" style="height:10px"></td>
    </tr>    
    <tr>
        <td colspan="2" style="width:100%">            
        <div id="dlg" class="dialog" style="width:100%">
           <div class="header" style="cursor: default">
                <div class="outer">
                    <div class="inner">
                        <div class="content">
                            <h5>EMPLOYEE RECORD</h5>
                        </div>
                    </div>
                </div>
            </div>
            <div class="body">
                <div class="outer">
                    <div class="inner">
                        <div class="content">
                            <asp:Panel CssClass = "smallfont" Width="100%" ID="pnlCust" runat="server">
                                <asp:UpdatePanel ID="pnlUpdate" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView Width="100%" CssClass="grid" AllowPaging="True" ID="GvEmployee" AutoGenerateColumns="False"
                                             runat="server" ShowHeader="False"                                            
                                            onrowdatabound="GvEmployee_RowDataBound" 
                                            onpageindexchanging="GvEmployee_PageIndexChanging" >
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Panel CssClass="row" ID="pnlCustomer" runat="server">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td style="width:5%" ><asp:Image ID="imgCollapsible" CssClass="first" ImageUrl="~/CommonImages/img/plus.png"  Style="margin-right: 5px;" runat="server" /></td>
                                                                    <td style="width:8%"><asp:Label ID="LblCader" runat="server" Text='<%# Bind("CADDER_CODE") %>'></asp:Label>  </td>
                                                                    <td style="width:5%"><asp:Label ID="lblEmployeeCode" runat="server" Text='<%# Bind("EMP_CODE") %>' ></asp:Label> </td>
                                                                    <td style="width:12%"><asp:Label ID="lblEmployeeName" runat="server" Text='<%# Bind("EmployeeName") %>'></asp:Label>  </td>
                                                                    <td style="width:8%"><asp:Label ID="LblDesign" runat="server" Text='<%# Bind("DESIG_NAME") %>'></asp:Label> </td>
                                                                    <td style="width:10%"><asp:Label ID="LblDept" runat="server" Text='<%# Bind("DEPT_NAME") %>'></asp:Label> </td> 
                                                                    <td style="width:5%"><asp:Label ID="LblGender" runat="server" Text='<%# Bind("GENDER") %>'></asp:Label> </td>   
                                                                    <td style="width:5%"><b>DOB:</b><asp:Label ID="LblDOB" runat="server" Text='<%# Bind("DOB") %>'></asp:Label> </td>                                                                
                                                                    <td style="width:5%"><b>DOJ:</b><asp:Label ID="LblDOJ" runat="server" Text='<%# Bind("JOIN_DT") %>'></asp:Label> </td>                                                                   
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel Style="margin-left: 20px; margin-right: 20px" ID="pnlOrders" runat="server">
                                                             <asp:UpdatePanel ID="pnlUpdateGrid" runat="server">
                                                                <ContentTemplate>
                                                                    <table width="100%" class="tContent">
                                                                        <tr>
                                                                            <td style="color:Green; font-weight:bold;">EMPLOYEE COMPANY INFO</td>
                                                                        </tr> 
                                                                        <tr>
                                                                            <td><asp:GridView  ID="GVCompInfo" runat="server" Width="100%" AutoGenerateColumns="False" >
                                                                                <Columns>                                                                
                                                                                    <asp:BoundField DataField="EMP_COMP_INFO" ItemStyle-Width="1%" HeaderStyle-Width="1%"  Visible="false" />
                                                                                    <asp:BoundField DataField="AC_NO" ItemStyle-Width="8%"  HeaderStyle-Width="8%"   HeaderText="A/C No" />
                                                                                    <asp:BoundField DataField="DL_NO"  ItemStyle-Width="5%"  HeaderStyle-Width="5%"   HeaderText="DL.No" />
                                                                                    <asp:BoundField DataField="DL_ISS_DT"  ItemStyle-Width="5%"  HeaderStyle-Width="5%"   HeaderText="DL Issue Date" /> 
                                                                                    <asp:BoundField DataField="PASSPORT_NO"  ItemStyle-Width="8%"  HeaderStyle-Width="8%"   HeaderText="Passport No" />  
                                                                                    <asp:BoundField DataField="PASSPORT_ISS_DT" ItemStyle-Width="5%"  HeaderStyle-Width="5%"   HeaderText="Passport Issue Date" />
                                                                                    <asp:BoundField DataField="PAN_NO" ItemStyle-Width="5%"  HeaderStyle-Width="5%"    HeaderText="PAN No" />
                                                                                    <asp:BoundField DataField="PF_AC_NO"    HeaderText="PF A/C No" />       
                                                                                    <asp:BoundField DataField="PR_ADD"  ItemStyle-Width="10%"  HeaderStyle-Width="10%"  HeaderText="Present Address" />
                                                                                    <asp:BoundField DataField="PR_CITY"  ItemStyle-Width="5%"  HeaderStyle-Width="5%"  HeaderText="Present City" />
                                                                                    <asp:BoundField DataField="PR_STATE"  ItemStyle-Width="5%" HeaderStyle-Width="5%" HeaderText="Present COUNTRY" />       
                                                                                    <asp:BoundField DataField="PR_COUNTRY"  ItemStyle-Width="5%" HeaderStyle-Width="5%" HeaderText="Present COUNTRY" />
                                                                                    <asp:BoundField DataField="PR_PIN_NO"    HeaderText="Present Pin No" />
                                                                                    <asp:BoundField DataField="PR_TEL_NO"  ItemStyle-Width="5%"  HeaderStyle-Width="5%"   HeaderText="Present Phone No" /> 
                                                                                    <asp:BoundField DataField="PR_FAX_NO"  ItemStyle-Width="5%"  HeaderStyle-Width="5%"   HeaderText="Present FAX" />
                                                                                    <asp:BoundField DataField="PR_EMAIL_ID" ItemStyle-Width="8%"   HeaderStyle-Width="8%"   HeaderText="Present Email" /> 
                                                                                    <asp:BoundField DataField="PERM_ADD"  ItemStyle-Width="10%"  HeaderStyle-Width="10%"   HeaderText="Present Address" />
                                                                                    <asp:BoundField DataField="PERM_CITY"  ItemStyle-Width="5%"  HeaderStyle-Width="5%"   HeaderText="Permanent City" />
                                                                                    <asp:BoundField DataField="PERM_STATE" ItemStyle-Width="5%"  HeaderStyle-Width="5%"   HeaderText="Permanent State" />       
                                                                                    <asp:BoundField DataField="PERM_COUNTRY" ItemStyle-Width="5%"  HeaderStyle-Width="5%"   HeaderText="Permanent COUNTRY" />
                                                                                    <asp:BoundField DataField="PERM_PIN_NO"   HeaderText="Permanent Pin No" />
                                                                                    <asp:BoundField DataField="PERM_TEL_NO"    HeaderText="Permanent Phone No" /> 
                                                                                    <asp:BoundField DataField="PERM_FAX_NO"   HeaderText="Permanent FAX" />
                                                                                    <asp:BoundField DataField="PERM_EMAIL_ID" ItemStyle-Width="8%"  HeaderStyle-Width="8%"   HeaderText="Permanent Email" /> 
                                                                                    <asp:BoundField DataField="INSURANCE"  HeaderText="Insurance" />
                                                                                    <asp:BoundField DataField="DISPENSARY"    HeaderText="Dispensary" />
                                                                                    <asp:BoundField DataField="BANK_NAME" ItemStyle-Width="5%"   HeaderStyle-Width="5%"  HeaderText="Bank Name" />
                                                                                </Columns>
                                                                            </asp:GridView></td>
                                                                        </tr>  
                                                                        <tr>
                                                                            <td style="color:Green; font-weight:bold;">EMPLOYEE MEDICAL  DETAILS</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><asp:GridView ID="GVMedical" runat="server"  AllowPaging="true" PageSize="5" AutoGenerateColumns="False" >
                                                                                     <Columns>
                                                                                        <asp:BoundField DataField="EMP_MED_ID" Visible="false" />
                                                                                        <asp:BoundField DataField="EMP_BLD_GRP" HeaderText="Blood Group" />
                                                                                        <asp:BoundField DataField="EMP_HIGHT" HeaderText="Height" /> 
                                                                                        <asp:BoundField DataField="EMP_WEIGHT" HeaderText="Weight" />
                                                                                        <asp:BoundField DataField="EMP_B_MARKS" HeaderText="Birth Mark" />
                                                                                        <asp:BoundField DataField="ESI" HeaderText="ESI" />
                                                                                        <asp:BoundField DataField="CHK_DIS" HeaderText="COUNTRY" /> 
                                                                                        <asp:BoundField DataField="DIS" HeaderText="Disease" />
                                                                                        <asp:BoundField DataField="DIS_REMARKS" HeaderText="Disability Remarks" />
                                                                                        <asp:BoundField DataField="H_REMARKS" HeaderText="Health Remarks" />
                                                                                        <asp:BoundField DataField="EMER_NAME" HeaderText="Emer Name" /> 
                                                                                        <asp:BoundField DataField="EMER_ADD" HeaderText="Emer Address" />
                                                                                        <asp:BoundField DataField="EMER_CITY" HeaderText="Emer City" />
                                                                                        <asp:BoundField DataField="EMER_STATE" HeaderText="Emer State" />
                                                                                        <asp:BoundField DataField="EMER_PIN" HeaderText="Emer Pin" /> 
                                                                                        <asp:BoundField DataField="EMER_COUNTRY" HeaderText="Emer Country" />
                                                                                        <asp:BoundField DataField="EMER_M_NO" HeaderText="Emer Mobile" />
                                                                                        <asp:BoundField DataField="EMER_TEL_NO" HeaderText="Emer Phone" />
                                                                                        <asp:BoundField DataField="EMER_FAX" HeaderText="Emer FAX" /> 
                                                                                        <asp:BoundField DataField="EMER_EMAIL" HeaderText="Emer Email" />                
                                                                                    </Columns>
                                                                                </asp:GridView></td>
                                                                        </tr> 
                                                                        <tr>
                                                                            <td style="color:Green; font-weight:bold;">EMPLOYEE FAMILY INDEPENDENT</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><asp:GridView ID="GVFamily" runat="server" AllowAddingRecords="False" AllowFiltering="True"
                                                                                        AllowPaging="true" PageSize="5" AutoGenerateColumns="False" >
                                                                                            <Columns>
                                                                                                <asp:BoundField DataField="EMP_DEPEND_ID" Visible="false" />
                                                                                                <asp:BoundField DataField="I_F_NAME" HeaderText="First Name" />
                                                                                                <asp:BoundField DataField="I_L_NAME" HeaderText="Last Name" />
                                                                                                <asp:BoundField DataField="I_DOB" HeaderText="Date Of Birth" />
                                                                                                <asp:BoundField DataField="I_SEX" HeaderText="Sex" />
                                                                                                <asp:BoundField DataField="I_RELATION" HeaderText="Relation" />
                                                                                            </Columns>
                                                                                        
                                                                                  </asp:GridView></td>
                                                                        </tr>  
                                                                        <tr>
                                                                            <td style="color:Green; font-weight:bold;">EMPLOYEE QUALIFICATION</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><asp:GridView ID="GVQualification" runat="server"   AllowPaging="true" PageSize="5" AutoGenerateColumns="False" >
                                                                            <Columns>
                                                                                <asp:BoundField DataField="EMP_QUAL_ID" Visible="false" />
                                                                                <asp:BoundField DataField="EXAM" HeaderText="Exam" />
                                                                                <asp:BoundField DataField="SCH_COL" HeaderText="School/College" />
                                                                                <asp:BoundField DataField="PASS_YEAR" HeaderText="COUNTRY" />
                                                                                <asp:BoundField DataField="BOARD_UNIV" HeaderText="Board/University" />
                                                                                <asp:BoundField DataField="GRADE" HeaderText="Grade" />
                                                                                <asp:BoundField DataField="PERCENT" HeaderText="Percent" />
                                                                                                             
                                                                            </Columns>
                                                                        </asp:GridView></td>
                                                                        </tr>  
                                                                        <tr>
                                                                            <td style="color:Green; font-weight:bold;">EMPLOYEE FAMILY INDEPENDENT :</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><asp:GridView ID="GVLanguage" runat="server" AllowAddingRecords="False" AllowFiltering="True"
                                                                                            AllowPaging="true" PageSize="5" AutoGenerateColumns="False" >
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="EMP_LANG_ID" Visible="false" />
                                                                                                    <asp:BoundField DataField="EMP_LANG" HeaderText="Language" />
                                                                                                    <asp:BoundField DataField="Read" HeaderText="Read" />   
                                                                                                    <asp:BoundField DataField="Speak" HeaderText="Speak" />
                                                                                                    <asp:BoundField DataField="Write" HeaderText="Write" />
                                                                                                            
                                                                                                </Columns>
                                                                                            </asp:GridView></td>
                                                                        </tr>                                                                 
                                                                    </table>                                                                    
                                                             </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </asp:Panel>
                                                        <cc1:CollapsiblePanelExtender ID="cpe" runat="Server" TargetControlID="pnlOrders"
                                                            CollapsedSize="0" Collapsed="True" ExpandControlID="pnlCustomer" CollapseControlID="pnlCustomer"
                                                            AutoCollapse="False" AutoExpand="False" ScrollContents="false" ImageControlID="imgCollapsible"
                                                            ExpandedImage="~/CommonImages/img/minus.png" CollapsedImage="~/CommonImages/img/plus.png"
                                                            ExpandDirection="Vertical" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                             <RowStyle CssClass="row" />
                                             <AlternatingRowStyle CssClass="altrow" />
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
            <div class="footer">
                <div class="outer">
                    <div class="inner">
                        <div class="content">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </td>
    </tr>   
</table>
<%--</ContentTemplate>
</asp:UpdatePanel>--%>