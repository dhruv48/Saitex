<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Employee_Qualification.ascx.cs" Inherits="Module_HRMS_Controls_Employee_Qualification" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="obout_Interface" namespace="Obout.Interface" tagprefix="cc2" %>
<%@ Register TagPrefix="obout" Namespace="Obout.ComboBox" Assembly="obout_ComboBox" %>
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
        width: 300px;
    }
    .c2
    {
        margin-left: 4px;
        width: 120px;
    }
     .c3
    {
        width: 50px;
    }
    .c4
    {
        margin-left: 4px;
        width: 100px;
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
<script  language="javascript" type="text/javascript" >
    function filterNonNumeric(field) {
var result = new String();
var numbers = "0123456789.";
var chars = field.value.split(""); // create array 
for (i = 0; i < chars.length; i++) {
if (numbers.indexOf(chars[i]) != -1) result += chars[i];
}
if (field.value != result) field.value = result;
}
</script>
<asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
<table class="tContentArial" width="100%"  align="left" >
     <tr>
                        <td valign="top" class="td" align="left">
                            <table cellspacing="0" cellpadding="0" width="20%" align="left" >
                                <tbody>
                                    <tr>
                                        <td id="tdSave" valign="top" align="center" runat="server">
                                            <asp:ImageButton ID="imgbtnSave" TabIndex="9" runat="server"
                                                ImageUrl="~/CommonImages/save.jpg" ToolTip="Save" Height="41" Width="48" 
                                                ValidationGroup="M1" onclick="imgbtnSave_Click">
                                            </asp:ImageButton>
                                        </td>
                                        <td id="tdUpdate" valign="top" align="center" runat="server">
                                            <asp:ImageButton ID="imgbtnUpdate" TabIndex="9"  runat="server"
                                                ImageUrl="~/CommonImages/edit1.jpg" ToolTip="Update" Height="41" Width="48" ValidationGroup="M1">
                                            </asp:ImageButton>
                                        </td>
                                        <td id="tdDelete" valign="top" align="center" runat="server">
                                            <asp:ImageButton ID="imgbtnDelete" TabIndex="9"  runat="server"
                                                ImageUrl="~/CommonImages/del6.png" ToolTip="Delete" Height="41" Width="48" ValidationGroup="M1">
                                            </asp:ImageButton>
                                        </td>
                                        <td valign="top" align="center">
                                            <asp:ImageButton ID="imgbtnFind" TabIndex="9" Visible="false" msconfig
                                             runat="server"
                                                ImageUrl="~/CommonImages/link_find.png" ToolTip="Find" Height="41" Width="48">
                                            </asp:ImageButton>
                                        </td>
                                        <td valign="top" align="center">
                                            <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                                                ToolTip="Print" Height="41" Width="48"></asp:ImageButton>
                                        </td>
                                        <td valign="top" align="center">
                                            <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                                ToolTip="Clear" Height="41" Width="48"></asp:ImageButton>
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
                    <tr>
                        <td class="TableHeader" align="center" width="100%">
                            <b class="titleheading">Employee Qualification</b>
                        </td>
                    </tr>
                    <tr>
                        <td class="td" valign="top" align="left" width="100%">
                            <span style="color: #ff0000">You are in &nbsp;<asp:Label ID="lblMode" runat="server">
                            </asp:Label>&nbsp;Mode</span>                            
                        </td>
                    </tr>
    <tr>
                        <td align="left" class="td" valign="top">
                            <table width="100%">
                            <tr><td colspan="3">
                            
                                 <obout:ComboBox runat="server" ID="DDLEmployee" Width="300px" Height="150px" 
                                     DataTextField="EMPLOYEENAME" DataValueField="EMP_CODE"  Enabled="false"
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
                                <tr bgcolor="#006699">
                                    <td align="center" valign="top" class="GrdHeader">
                                        <b>Exam</b>
                                    </td>
                                    <td align="center" valign="top" class="GrdHeader">
                                        <b>Board/Univ.</b>
                                    </td>
                                    <td align="center" valign="top" class="GrdHeader">
                                        <b>School/College</b>
                                    </td>
                                    <td align="center" valign="top" class="GrdHeader">
                                        <b>Passing Year</b>
                                    </td>
                                    <td align="center" valign="top" class="GrdHeader">
                                        <b>Grade</b>
                                    </td>
                                     <td align="center" valign="top" class="GrdHeader">
                                        <b>(%)</b>
                                    </td>
                                    <td align="center" valign="top" class="GrdHeader">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>                                  
                                    <td align="left" valign="top">
                                        <cc2:OboutTextBox ID="TxtExam" runat="server" TabIndex="1" MaxLength="100" CssClass="Label SmallFont" Width="80px"></cc2:OboutTextBox>
                                    </td>
                                    <td align="left" valign="top">
                                        <cc2:OboutTextBox ID="TxtBoardUniv" runat="server" CssClass="Label SmallFont" Width="150px" MaxLength="200" TabIndex="2"></cc2:OboutTextBox>
                                    </td>
                                    <td align="left" valign="top">
                                        <cc2:OboutTextBox ID="TxtCollege" runat="server" CssClass="Label SmallFont" MaxLength="200" Width="150px" TabIndex="3"></cc2:OboutTextBox>                                          
                                    </td>
                                    <td align="left" valign="top">
                                       <cc2:OboutTextBox ID="TxtPassingYear" runat="server" onKeyPress="return checkNumeric(event)" MaxLength="4" CssClass="LabelNo SmallFont" Width="80px" TabIndex="4"></cc2:OboutTextBox>                                       
                                    </td>
                                    <td align="left" valign="top">
                                        <cc2:OboutTextBox ID="TxtGrade" runat="server" MaxLength="10" CssClass="Label SmallFont" Width="80px" TabIndex="5"></cc2:OboutTextBox>                                       
                                    </td>  
                                      <td align="left" valign="top">
                                        <cc2:OboutTextBox ID="TxtPercentage" runat="server" MaxLength="5" onKeyPress="return checkNumeric(event)" CssClass="LabelNo SmallFont" Width="60px" TabIndex="6"></cc2:OboutTextBox>                                       
                                    </td>                                  
                                    <td align="left" valign="middle" >
                                        <asp:LinkButton ID="lbtnsavedetail" runat="server" TabIndex="7" 
                                            onclick="lbtnsavedetail_Click">Save</asp:LinkButton>&nbsp;/&nbsp;
                                        <asp:LinkButton ID="lbtnCancel" runat="server" TabIndex="8" 
                                            onclick="lbtnCancel_Click">Cancel</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr bgcolor="#006699">
                                    <td align="left" colspan="7" class="GrdHeader">
                                        <b>Language</b>
                                    </td>                                
                                </tr>
                                <tr>                                  
                                    <td class="tdLeft">
                                       <cc2:OboutDropDownList ID="DDLLanguage" runat="server" Width="80px"> 
                                            <asp:ListItem>Hindi</asp:ListItem>
                                            <asp:ListItem>English</asp:ListItem>
                                             <asp:ListItem>Punjabi</asp:ListItem>
                                            <asp:ListItem>Urdu</asp:ListItem>
                                            <asp:ListItem>Franch</asp:ListItem>
                                             <asp:ListItem>Other</asp:ListItem>
                                      </cc2:OboutDropDownList>
                                    </td>
                                    <td class="tdLeft">                                      
                                         <asp:CheckBox ID="ChkRead" class="SmallFont" Font-Bold="true" Text="Read" runat="server" />
                                    </td>
                                    <td class="tdLeft">
                                             <asp:CheckBox ID="ChkWrite" class="SmallFont" Font-Bold="true" Text="Write" runat="server" />
                                    </td>
                                    <td class="tdLeft">
                                           <asp:CheckBox ID="ChkSpeak" class="SmallFont" Font-Bold="true" Text="Speak" runat="server" />
                                    </td>
                                    <td align="left" colspan="2" valign="top">
                                    </td>                                                                     
                                    <td align="left" valign="middle" >
                                        <asp:LinkButton ID="LbLangSave" runat="server" TabIndex="7" 
                                            onclick="LbLangSave_Click">Save</asp:LinkButton>&nbsp;/&nbsp;
                                        <asp:LinkButton ID="LbLangCancel" runat="server" TabIndex="8" 
                                            onclick="LbLangCancel_Click">Cancel</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="trGridView" runat="server">
                        <td class="td" style ="width:100%" align="left">
                            <asp:Panel ID="pnlGrid" runat="server" Height="250px" Width="100%" ScrollBars="Vertical">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdEmpQualiDetail" runat="server" Width="100%" CssClass="SmallFont" Font-Bold="False"
                                             BorderWidth="1px"
                                            AutoGenerateColumns="False" AllowSorting="True" 
                                            onrowcommand="grdEmpQualiDetail_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl No." ItemStyle-Width="25px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="top">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Top" Width="40px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField  HeaderText="Exam" HeaderStyle-HorizontalAlign="Center" >
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblExam" runat="server" Text='<%# Bind("EXAM") %>'></asp:Label>
                                                    </ItemTemplate>
                                                     <ItemStyle VerticalAlign="Top" Width="60px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Board/Univ." HeaderStyle-HorizontalAlign="Center" >
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblBoard" runat="server" Text='<%# Bind("BOARD_UNIV") %>' CssClass="Label SmallFont"></asp:Label>
                                                       
                                                    </ItemTemplate>
                                                     <ItemStyle VerticalAlign="Top" Width="100px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="School/College" HeaderStyle-HorizontalAlign="Center" >
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblSchool" runat="server" Text='<%# Bind("SCH_COL") %>' CssClass="Label SmallFont" Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                     <ItemStyle VerticalAlign="Top" Width="100px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Passing Year" HeaderStyle-HorizontalAlign="Center" >
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblPassingYear" runat="server" Text='<%# Bind("PASS_YEAR") %>' CssClass="Label SmallFont" Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                     <ItemStyle VerticalAlign="Top" Width="40px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Grade" HeaderStyle-HorizontalAlign="Center" >
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblGrade" runat="server" Text='<%# Bind("GRADE") %>' CssClass="LabelNo SmallFont"></asp:Label>
                                                    </ItemTemplate>
                                                     <ItemStyle VerticalAlign="Top" Width="50px" />
                                                </asp:TemplateField>  
                                                 <asp:TemplateField HeaderText="(%)" HeaderStyle-HorizontalAlign="Center" >
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblPercentage" runat="server" Text='<%# Bind("PERCENT") %>' CssClass="LabelNo SmallFont"></asp:Label>
                                                    </ItemTemplate>
                                                     <ItemStyle VerticalAlign="Top" Width="40px" />
                                                </asp:TemplateField>                                             
                                                <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center"  >
                                                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="EmpEdit"
                                                            TabIndex="12" CommandArgument='<%# Eval("UniqueId") %>'></asp:LinkButton>&nbsp;/&nbsp;
                                                        <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CommandName="EmpDelete"
                                                            TabIndex="12" CommandArgument='<%# Eval("UniqueId") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle CssClass="RowStyle SmallFont" />
                                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                                            <AlternatingRowStyle CssClass="AltRowStyle" />
                                            <PagerStyle CssClass="PagerStyle" />
                                            <HeaderStyle CssClass="HeaderStyle GrdHeader" BackColor="#336699" />
                                        </asp:GridView>
                                        <br />
                                        <asp:GridView ID="GVLanguage" runat="server" CssClass="SmallFont" Width="80%" Font-Bold="False"
                                             BorderWidth="1px" AutoGenerateColumns="False" AllowSorting="True" 
                                            onrowcommand="GVLanguage_RowCommand" >
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl No." HeaderStyle-HorizontalAlign="Center"  ItemStyle-Width="25px" ItemStyle-VerticalAlign="top">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="40px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Language" HeaderStyle-HorizontalAlign="Center" >
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblLanguage" runat="server" Text='<%# Bind("EMP_LANG") %>'></asp:Label>
                                                    </ItemTemplate>
                                                     <ItemStyle VerticalAlign="Top" Width="80px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Read" HeaderStyle-HorizontalAlign="Center" >
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblRead" runat="server" Text='<%# Bind("EMP_READ") %>' CssClass="Label SmallFont"></asp:Label>
                                                       
                                                    </ItemTemplate>
                                                     <ItemStyle VerticalAlign="Top" Width="50px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Write" HeaderStyle-HorizontalAlign="Center" >
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblWrite" runat="server" Text='<%# Bind("EMP_WRITE") %>' CssClass="Label SmallFont" Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                     <ItemStyle VerticalAlign="Top" Width="50px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Speak" HeaderStyle-HorizontalAlign="Center" >
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblSpeak" runat="server" Text='<%# Bind("EMP_SPEAK") %>' CssClass="Label SmallFont" Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                     <ItemStyle VerticalAlign="Top" Width="50px" />
                                                </asp:TemplateField>                                                                                            
                                                <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center" >
                                                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkLangEdit" runat="server" Text="Edit" CommandName="EmplangEdit"
                                                            TabIndex="12" CommandArgument='<%# Eval("UniqueLangId") %>'></asp:LinkButton>&nbsp;/&nbsp;
                                                        <asp:LinkButton ID="lnklangDelete" runat="server" Text="Delete" CommandName="EmpLangDelete"
                                                            TabIndex="12" CommandArgument='<%# Eval("UniqueLangId") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle CssClass="RowStyle SmallFont" />
                                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                                            <AlternatingRowStyle CssClass="AltRowStyle" />
                                            <PagerStyle CssClass="PagerStyle" />
                                            <HeaderStyle CssClass="HeaderStyle GrdHeader" BackColor="#336699" />
                                        </asp:GridView>
                                    </ContentTemplate>                                  
                                </asp:UpdatePanel>
                            </asp:Panel>
                            
                        </td>
                    </tr>

</table>
</ContentTemplate>
</asp:UpdatePanel>