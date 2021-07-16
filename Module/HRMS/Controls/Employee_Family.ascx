<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Employee_Family.ascx.cs" Inherits="Module_HRMS_Controls_Employee_Family" %>
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
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
       <table class="tContentArial" width="100%"  align="left" >
     <tr>
                        <td valign="top" class="td" align="left">
                            <table cellspacing="0" cellpadding="0"  align="left" >
                                <tbody>
                                    <tr>
                                        <td id="tdSave" valign="top" align="center" runat="server">
                                            <asp:ImageButton ID="imgbtnSave" TabIndex="9" runat="server"
                                                ImageUrl="~/CommonImages/save.jpg" ToolTip="Save" Height="41" Width="48" 
                                                ValidationGroup="M1" onclick="imgbtnSave_Click" >
                                            </asp:ImageButton>
                                        </td>
                                        <td id="tdUpdate" valign="top" align="center" runat="server">
                                            <asp:ImageButton ID="imgbtnUpdate" TabIndex="9"  runat="server"
                                                ImageUrl="~/CommonImages/edit1.jpg" ToolTip="Update" Height="41" 
                                                Width="48" ValidationGroup="M1" onclick="imgbtnUpdate_Click">
                                            </asp:ImageButton>
                                        </td>
                                        <td id="tdDelete" valign="top" align="center" runat="server">
                                            <asp:ImageButton ID="imgbtnDelete" TabIndex="9"  runat="server"
                                                ImageUrl="~/CommonImages/del6.png" ToolTip="Delete" Height="41" Width="48" ValidationGroup="M1">
                                            </asp:ImageButton>
                                        </td>                                       
                                        <td valign="top" align="center">
                                            <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                                                ToolTip="Print" Height="41" Width="48"></asp:ImageButton>
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
                            <b class="titleheading">Employee Family Dependent</b>
                        </td>
                    </tr>
                    <tr>
                        <td class="td" valign="top" align="left" width="100%">
                            <span style="color: #ff0000">You are in &nbsp;<asp:Label ID="lblMode" runat="server">
                            </asp:Label>&nbsp;Mode</span>                            
                        </td>
                    </tr>                    
                    <tr>
                        <td>Name:
                            <asp:Label ID="LblEmpName" Font-Bold="true"  runat="server" Text=""></asp:Label>
                            <asp:Label ID="LblEmpCode" runat="server" Visible="false"  Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                       <td  align="left" class="td" valign="top" >
                            <table width="100%">                            
                                <tr bgcolor="#006699">
                                    <td align="left" valign="top" class="GrdHeader">
                                        <b>First Name</b>
                                    </td>
                                    <td align="left" valign="top" class="GrdHeader">
                                        <b>Last Name </b>
                                    </td>
                                    <td align="left" valign="top" class="GrdHeader">
                                        <b>DOB</b>
                                    </td>
                                    <td align="left" valign="top" class="GrdHeader">
                                        <b>Age</b>
                                    </td>
                                    <td align="left" valign="top" class="GrdHeader">
                                        <b>Sex</b>
                                    </td>
                                    <td align="left" valign="top" class="GrdHeader">
                                        <b>Relation</b>
                                    </td>
                                    
                                    <td align="left" valign="top" class="GrdHeader">
           &nbsp;
                                    </td>
                                </tr>
                                <tr>                                  
                                    <td align="left" valign="top">
                                        <cc2:OboutTextBox ID="txtfname" runat="server" TabIndex="1" CssClass="Label SmallFont"
                                            Width="150px"></cc2:OboutTextBox>
                                    </td>
                                    <td align="left" valign="top">
                                        <cc2:OboutTextBox ID="txtlname" runat="server" CssClass="Label SmallFont"
                                            Width="150px" TabIndex="2"></cc2:OboutTextBox>
                                    </td>
                                    <td align="left" valign="top">
                                        <cc2:OboutTextBox ID="txtdob" runat="server" CssClass="Label SmallFont"    MaxLength="10"     Width="100px" TabIndex="3"></cc2:OboutTextBox>                                            
                                             <cc1:CalendarExtender ID="CE4" runat="server" PopupPosition="BottomLeft"  Format="dd/MM/yyyy" TargetControlID="txtdob">
                                                </cc1:CalendarExtender>                                         
                                    </td>
                                     <td align="left" valign="top">
                                        <cc2:OboutTextBox ID="txtAge" runat="server" CssClass="Label SmallFont" MaxLength="3" onKeyPress="return checkNumeric(event)"
                                            Width="50px" TabIndex="4"></cc2:OboutTextBox>                                          
                                    </td>
                                    <td align="left" valign="top">
                                        <cc2:OboutDropDownList ID="DDLSEX" runat="server" Width="80px" TabIndex="5">
                                            <asp:ListItem>MALE</asp:ListItem>
                                            <asp:ListItem>FEMALE</asp:ListItem>                                           
                                      </cc2:OboutDropDownList>
                                    </td>
                                    <td align="left" valign="top">
                                        <cc2:OboutDropDownList ID="DDLRelation" runat="server"  Width="150px" TabIndex="6"> </cc2:OboutDropDownList>                                       
                                    </td>                                   
                                    <td align="left" valign="top">
                                        <asp:LinkButton ID="lbtnsavedetail" runat="server" TabIndex="7" 
                                            onclick="lbtnsavedetail_Click">Save</asp:LinkButton>&nbsp;/&nbsp;
                                        <asp:LinkButton ID="lbtnCancel" runat="server" TabIndex="8" 
                                            onclick="lbtnCancel_Click">Cancel</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="trGridView" runat="server">
                        <td class="td" style ="width:100%;" align="left">
                            <asp:Panel ID="pnlGrid" runat="server" Height="300px" Width="100%" ScrollBars="Vertical">
                              <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                       <asp:GridView ID="grdEmpFamilyDetail" runat="server" CssClass="SmallFont" Font-Bold="False"
                                             BorderWidth="1px" Width="100%"
                                            AutoGenerateColumns="False" AllowSorting="True" 
                                            onrowcommand="grdEmpFamilyDetail_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl No." ItemStyle-Width="25px" ItemStyle-VerticalAlign="top">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Top" Width="40px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="First Name">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblFirstname" runat="server" Text='<%# Bind("I_F_NAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                     <ItemStyle VerticalAlign="Top" Width="100px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Last Name">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblLastName" runat="server" Text='<%# Bind("I_L_NAME") %>' CssClass="Label SmallFont"></asp:Label>
                                                       
                                                    </ItemTemplate>
                                                     <ItemStyle VerticalAlign="Top" Width="100px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DOB">
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblDateOfBirth" runat="server" Text='<%# Bind("I_DOB","{0:d}") %>'  CssClass="Label SmallFont" Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                     <ItemStyle VerticalAlign="Top" Width="80px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Age">
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblAge" runat="server" Text='<%# Bind("Age")  %>' CssClass="Label SmallFont" Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                     <ItemStyle VerticalAlign="Top" Width="80px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SEX">
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblSex" runat="server" Text='<%# Bind("I_SEX") %>'
                                                            CssClass="Label SmallFont" Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                     <ItemStyle VerticalAlign="Top" Width="60px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Relation">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblRelationGrid" runat="server" Text='<%# Bind("I_RELATION") %>' CssClass="LabelNo SmallFont"></asp:Label>
                                                    </ItemTemplate>
                                                     <ItemStyle VerticalAlign="Top" Width="80px" />
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