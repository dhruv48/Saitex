<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Mobile_Allotment.ascx.cs" Inherits="Module_HRMS_Controls_Mobile_Allotment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<table class="tContentArial">
<tr>
        <td class="tdRight" colspan = "4">
            <table class="tdLeft">
                            <tr>
                                <td id="tdSave" valign="top" align="center" runat="server">
                                    <asp:ImageButton ID="imgbtnSave" TabIndex="9" OnClick="imgbtnSave_Click" runat="server"
                                        ImageUrl="~/CommonImages/save.jpg" ToolTip="Save" Height="41" Width="48" ValidationGroup="M1">
                                    </asp:ImageButton>
                                </td>
                                <td id="tdUpdate" valign="top" align="center" runat="server">
                                    <asp:ImageButton ID="imgbtnUpdate" TabIndex="9" OnClick="imgbtnUpdate_Click" runat="server"
                                        ImageUrl="~/CommonImages/edit1.jpg" ToolTip="Update" Height="41" Width="48" ValidationGroup="M1">
                                    </asp:ImageButton>
                                </td>                               
                                <td valign="top" align="center">
                                    <asp:ImageButton ID="imgbtnFind" TabIndex="9" OnClick="imgbtnFind_Click" runat="server"
                                        ImageUrl="~/CommonImages/link_find.png" ToolTip="Find" Height="41" Width="48">
                                    </asp:ImageButton>
                                </td>
                                <td valign="top" align="center">
                                    <asp:ImageButton ID="imgbtnPrint" OnClick="imgbtnPrint_Click" runat="server" ImageUrl="~/CommonImages/link_print.png"
                                        ToolTip="Print" Height="41" Width="48"></asp:ImageButton>
                                </td>
                                <td valign="top" align="center">
                                    <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                        ToolTip="Clear" Height="41" Width="48" onclick="imgbtnClear_Click"></asp:ImageButton>
                                </td>
                                <td valign="top" align="center">
                                    <asp:ImageButton ID="imgbtnExit" OnClick="imgbtnExit_Click" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                                        ToolTip="Exit" Height="41" Width="48"></asp:ImageButton>
                                </td>
                                <td valign="top" align="center">
                                    <asp:ImageButton ID="imgbtnHelp" OnClick="imgbtnHelp_Click" runat="server" ImageUrl="~/CommonImages/link_help.png"
                                        ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                                </td>
                            </tr>
                        </table>
        </td>
    </tr>
    <tr>
        <td class="TableHeader td" align="center" colspan = "4">
            <span class="titleheading">Employee Telephone Allotment</span>
            </td>
    </tr>
    <tr>
        <td align="left" valign="top" class="td" colspan="4">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>
   <tr id="trFindingRecord" runat="server" visible="false" >
        <td valign="top" class="td" align="center"  colspan="4">
            <table width="65%">
                     <tr><td class="tdRight">Month :</td><td class="tdLeft">  
                            <asp:DropDownList ID="DDLOpenMonth" Width="150px" CssClass="SmallFont TextBox UpperCase"  runat="server">
                         <asp:ListItem Value="0">------------Select-------------</asp:ListItem>
                                <asp:ListItem Value="1"> January </asp:ListItem>
                                <asp:ListItem Value="2"> February </asp:ListItem>
                                <asp:ListItem Value="3"> March </asp:ListItem>
                                <asp:ListItem Value="4"> April </asp:ListItem>
                                <asp:ListItem Value="5"> May </asp:ListItem>
                                <asp:ListItem Value="6"> June </asp:ListItem>
                                <asp:ListItem Value="7"> July </asp:ListItem>
                                <asp:ListItem Value="8"> August </asp:ListItem>
                                <asp:ListItem Value="9"> September </asp:ListItem>
                                <asp:ListItem Value="10"> October </asp:ListItem>
                                <asp:ListItem Value="11"> November </asp:ListItem>
                                <asp:ListItem Value="12"> December </asp:ListItem>
                        </asp:DropDownList>
                    </td><td class="tdRight">Year :</td><td class="tdLeft">    
                    <asp:DropDownList ID="DDLOpenYear" Width="150px" CssClass="SmallFont TextBox UpperCase" runat="server"> </asp:DropDownList>
                    </td></tr>
                    <tr>
                    <td class="tdRight">Department :</td><td class="tdLeft">
                    <asp:DropDownList ID="ddldepartment" runat="server" AutoPostBack="True" DataTextField="DEPT_NAME" DataValueField="DEPT_CODE" Width="150px" onselectedindexchanged="ddldepartment_SelectedIndexChanged" CssClass="SmallFont TextBox UpperCase">
                    </asp:DropDownList>
                  
                    </td><td class="tdRight">Branch :</td><td class="tdLeft">
                            <asp:DropDownList ID="DDLBranch" runat="server" Width="150px" AutoPostBack="True" onselectedindexchanged="DDLBranch_SelectedIndexChanged" CssClass="SmallFont TextBox UpperCase"></asp:DropDownList></td></tr>
                 <tr><td class="tdRight">Designation :</td>
                 <td class="tdLeft"><asp:DropDownList ID="DDLDesign" runat="server" AutoPostBack="True" DataTextField="DEPT_NAME" DataValueField="DEPT_CODE"  Width="150px" onselectedindexchanged="DDLDesign_SelectedIndexChanged" CssClass="SmallFont TextBox UpperCase" >
                    </asp:DropDownList></td>
                 <td class="tdRight">Employee :</td>
                 <td class="tdLeft">           
                    <asp:DropDownList ID="ddlemployee" runat="server" 
                                onselectedindexchanged="ddlemployee_SelectedIndexChanged" 
                                AutoPostBack="True" DataTextField="EMPLOYEENAME" DataValueField="EMP_CODE" 
                                Width="150px" CssClass="SmallFont TextBox UpperCase">
                            </asp:DropDownList>
       </td></tr>
            </table>
        </td>
   </tr>
   
<tr><td colspan="4" class = "td">Telephone Record :-</td></tr>
<tr>
<td colspan="4">
    <asp:GridView ID="GVTelephoneRecord" runat="server" AutoGenerateColumns="False" 
        AllowPaging="True" AllowSorting="True" Font-Size="X-Small" PageSize="15" 
        CellPadding="3"   GridLines="None" Width="100%" ForeColor="#333333" 
        CssClass = "smallfont" 
        onpageindexchanging="GVTelephoneRecord_PageIndexChanging" 
        onrowdatabound="GVTelephoneRecord_RowDataBound" >       
        <FooterStyle Width="100%" BackColor="#507CD1"  ForeColor="White" Font-Bold="True" />
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
            <asp:TemplateField HeaderText="EMP_CODE" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="50px">
             <ItemTemplate>
                    <asp:Label ID="lblempcode" runat="server" Text='<%# Eval("EMP_CODE") %>'></asp:Label>
                </ItemTemplate>               
            </asp:TemplateField>
            <asp:TemplateField HeaderText="EMPLOYEE" ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="120px"> <ItemTemplate>
                    <asp:Label ID="lblempname" runat="server" Text='<%# Eval("EMPLOYEENAME") %>'></asp:Label>
                </ItemTemplate></asp:TemplateField>
            <asp:TemplateField HeaderText="DEPARTMENT" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100px"> <ItemTemplate>
                    <asp:Label ID="lbldepartment" runat="server" Text='<%# Eval("DEPT_NAME") %>'></asp:Label>
                </ItemTemplate></asp:TemplateField>
            <asp:TemplateField HeaderText="DESIGNATION" ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="120px"> <ItemTemplate>
                    <asp:Label ID="lbldesignation" runat="server" Text='<%# Eval("DESIG_NAME") %>'></asp:Label>
                </ItemTemplate></asp:TemplateField> 
            <asp:TemplateField HeaderText="MOBILE_NO" ItemStyle-Width="100" HeaderStyle-Width="100px">
             <ItemTemplate>
                 <asp:DropDownList ID="DDLMobile" CssClass="TextBox SmallFont" DataTextField="TELEPHONE_NO" DataValueField="TELEPHONE_ID" Width="100px" runat="server"></asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="TERIFF_PLAN">
             <ItemTemplate>
                 <asp:TextBox ID="txtterif" runat="server" Text='<%# Eval("TERIFF_PLAN") %>'  CssClass="TextBoxNo SmallFont" Width ="60px"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="MOBILE LIMIT">
             <ItemTemplate>
                   <asp:TextBox ID="txtmoblimit" runat="server" Text='<%# Eval("TELEPHONE_LIMIT") %>' CssClass="SmallFont TextBoxNo" onKeyPress="pricevalidate(this);" Width ="60px"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="ALLOTEMENT DATE">
             <ItemTemplate>
                 <asp:TextBox ID="TxtAllotedDate" runat="server" Text='<%# Eval("ALLOT_DATE") %>'  CssClass="TextBoxNo SmallFont" Width ="60px"></asp:TextBox>
                 <cc1:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy"  TargetControlID="TxtAllotedDate" runat="server"></cc1:CalendarExtender>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="REMARKS">
                <ItemTemplate>
                    <asp:TextBox ID="txtremarks" runat="server" Text='<%# Eval("REMARKS") %>' CssClass="TextBox SmallFont" Width ="200px" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField> 
            <asp:TemplateField Visible="false" >
                <ItemTemplate>
                    <asp:Label ID="LblMobileID" runat="server" Text='<%# Eval("TELEPHONE_NO") %>'></asp:Label>                      
                </ItemTemplate>
            </asp:TemplateField> 
            
        </Columns>
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle 
            HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#507CD1" 
            ForeColor="White" Font-Bold="True" /> 
    </asp:GridView>
    
    </td>
  </tr>

</table>
