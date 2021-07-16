<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AttendanceApproval.ascx.cs"
    Inherits="Module_HRMS_Controls_AttendanceApproval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.ComboBox" Assembly="obout_ComboBox" %>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
    <script src="../../../javascript/jquery-1.4.1.min.js" type="text/javascript"></script>

<script src="../../../javascript/ScrollableGrid.js" type="text/javascript"></script>

<script type="text/javascript">
       $(document).ready(function() {
       $('#<%=gvAttendanceRegister.ClientID %>').Scrollable();
    }
)
</script>

<style type="text/css">
    .HideControl
    {
        visibility: hidden;
    }
    .pager span
    {
        color: #009900;
        font-weight: bold;
        font-size: 16pt;
    }
</style>
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
                width: 80px;
            }
            .c2
            {
                margin-left: 4px;
                width: 150px;
            }
            .c3
            {
                margin-left: 4px;
                width: 80px;
            }
        </style>

        <script type="text/javascript">
        function SelectAll(id)
        {
            //get reference of GridView control
            var grid = document.getElementById("<%= gvAttendanceRegister.ClientID %>");
            //variable to contain the cell of the grid
            var cell;
            
            if (grid.rows.length > 0)
            {
                //loop starts from 1. rows[0] points to the header.
                for (i=1; i<grid.rows.length; i++)
                {
                    //get the reference of first column
                    cell = grid.rows[i].cells[0];
                    
                    //loop according to the number of childNodes in the cell
                    for (j=0; j<cell.childNodes.length; j++)
                    {           
                        //if childNode type is CheckBox                 
                        if (cell.childNodes[j].type =="checkbox")
                        {
                        //assign the status of the Select All checkbox to the cell checkbox within the grid
                            cell.childNodes[j].checked = document.getElementById(id).checked;
                        }
                    }
                }
            }
        }  
    
   
function Daysvalidate(test1)
    {
        var dec="";
        var fra="";
        var i;
        var val=test1.value;
        var l = test1.value.length;
        var res="";
        var dl=0;
        var fl=0;
        var index_of_dot;
        var index_of_dot=val.indexOf('.');
        var check=0;
        if (index_of_dot ==-1)
            dl=l;
        else
        { dl=index_of_dot;
            fl =(l-(index_of_dot))-1; 
            for (i=index_of_dot+1 ;i<l;i++)            {
            check++;
            if (check <2)
            {   var schar=val.charAt(i);
                fra+=schar ;            }
            else
            { alert ("Fraction point value should be upto 2 digit");
                break; 
             }                

            }
        }   
         for (i=0;i<dl;i++)

        {
            if (i <=2)
            {
                var schar=val.charAt(i);
                dec+=schar ;
            }
            else
            {
               alert ("Decimal Place length should be upto 2 digit");
               break;
            }                     
        }     

       if (index_of_dot !=-1)
       { 
       if (isNaN (dec)||isNaN (fra))
       {
       test1.value='';
        
       }
       else
         test1.value=dec+"."+fra;
         }
         else
         {
         if (isNaN (dec))
       {
       test1.value='';       
       }else 
            test1.value=dec;
         }   }    
        </script>
        <cc1:CalendarExtender ID="CalExDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>
        <table align="left" width="100%" class="td tContent">
            <tr>
                <td colspan="2" class="td">
                    <table class="tContent">
                        <tr>
                            <td id="tdUpdate" align="left" width="48" runat="server">
                                <asp:ImageButton ID="imgbtnUpdate" OnClick="imgbtnUpdate_Click" runat="server" ValidationGroup="M1"
                                    ImageUrl="~/CommonImages/edit1.jpg" ToolTip="Update" Height="41" Width="48">
                                </asp:ImageButton>
                            </td>
                            <td id="tdClear" runat="server" valign="top">
                                <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                                    OnClientClick="javascript:return window.confirm('Are you sure you want to clear this record')"
                                    TabIndex="8" ToolTip="Clear" Width="48" />
                            </td>
                            <td id="tdPrint" runat="server" valign="top">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"  TabIndex="9" ToolTip="Print" Width="48" />
                            </td>
                            <td id="tdExit" runat="server" valign="top">
                                <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"  OnClick="imgbtnExit_Click" TabIndex="10" ToolTip="Exit" Width="48" />
                            </td>
                            <td id="tdHelp" runat="server" valign="top">
                                <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"  TabIndex="11" ToolTip="Help" Width="48" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center" valign="top" class="tRowColorAdmin td">
                    Attendance Data For Approving
                </td>
            </tr>
            <tr>
                <td colspan="2" class="td">
                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="M1" />
                    <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
                    </span>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <fieldset>
                        <legend>Filter Record By:</legend>
                        <table width="100%">
                            <tr>
                                <td>
                                    Branch:
                                </td>
                                <td>
                                    <asp:DropDownList ID="DDLBranch" Width="130px" CssClass="TextBox SmallFont" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    Department:
                                </td>
                                <td>
                                    <asp:DropDownList ID="DDLDepartment" Width="130px" CssClass="TextBox SmallFont" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    Designation:
                                </td>
                                <td>
                                    <asp:DropDownList ID="DDLDesigination" Width="130px" CssClass="TextBox SmallFont"
                                        runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    Cadder:
                                </td>
                                <td>
                                    <asp:DropDownList ID="DDLCader" Width="130px" CssClass="TextBox SmallFont" runat="server">
                                        <asp:ListItem Value="">--------SELECT-------</asp:ListItem>
                                        <asp:ListItem Value="STAFF">STAFF</asp:ListItem>
                                        <asp:ListItem Value="WORKMEN">WORKMEN</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                 <td>Shift:</td>
                                <td><asp:DropDownList ID="DDLShift" DataValueField="SFT_ID" Width="130px" CssClass="TextBox SmallFont"  DataTextField="SFT_NAME"   runat="server" >
                                        </asp:DropDownList>  </td>
                                <td>Attendance Date:</td>
                                <td><asp:TextBox ID="txtDate" runat="server" Width="130px" CssClass="TextBox SmallFont" ></asp:TextBox></td>
                         
                                <td>
                                    Employee:
                                </td>
                                <td>
                                    <obout:ComboBox runat="server" ID="DDLEmployee" EnableVirtualScrolling="true" Width="130px"
                                        Height="200px" DataTextField="EMPLOYEENAME" CssClass="SmallFont TextBox UpperCase"
                                        DataValueField="EMP_CODE" EnableLoadOnDemand="true" OnLoadingItems="DDLEmployee_LoadingItems"
                                        AutoPostBack="True" MenuWidth="300px">
                                        <HeaderTemplate>
                                            <div class="header c1">
                                                Emp Code</div>
                                            <div class="header c2">
                                                Employee Name</div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="item c1">
                                                <%# Eval("EMP_CODE")%></div>
                                            <div class="item c2">
                                                <%# Eval("EMPLOYEENAME")%></div>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Displaying items
                                            <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                            out of
                                            <%# Container.ItemsCount %>.
                                        </FooterTemplate>
                                    </obout:ComboBox>
                                </td>
                                <td>
                                    <asp:Button ID="CmdView" runat="server" Width="100px" Text="View Records" OnClick="CmdView_Click" />
                                </td>
                            </tr>
                            <tr  visible="false"  style="font-weight:bold">
                    <td>Shift InTime:</td>
                    <td><b><asp:Label ID="LblInTime" CssClass="TextBox" runat="server" Text=""></asp:Label></b></td>
                    <td></td>
                    <td>Shift OutTime:</td>
                    <td><b><asp:Label ID="LblOutTime" CssClass="TextBox" runat="server" Text=""></asp:Label></b></td>
                </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <fieldset>
                                <legend>Attendance Record:</legend>
                                <asp:Panel ID="gridPanel" runat="server" ScrollBars="Vertical" Width="100%" Height="450px">
                                    <asp:GridView ID="gvAttendanceRegister" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                                        Font-Size="X-Small" PageSize="15" CellPadding="3" GridLines="None" Width="100%"
                                        ForeColor="#333333" CssClass="smallfont" >
                                        <FooterStyle Width="100%" BackColor="#B30101" ForeColor="White" Font-Bold="True" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40px">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="ChkMarkAll" runat="server" AutoPostBack="True" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ChkMark" runat="server" />
                                                </ItemTemplate>
                                                <HeaderStyle Width="40px"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EMP_CODE" HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="40px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblempcode" runat="server" Text='<%# Eval("EMP_CODE") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="40px"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EMPLOYEE" ItemStyle-Width="140px" HeaderStyle-HorizontalAlign="Left"
                                                HeaderStyle-Width="140px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblempname" runat="server" Text='<%# Eval("EMPLOYEENAME") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="140px"></HeaderStyle>
                                                <ItemStyle Width="140px"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DEPARTMENT" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"
                                                HeaderStyle-Width="100px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldepartment" runat="server" Text='<%# Eval("DEPT_NAME") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="100px"></HeaderStyle>
                                                <ItemStyle Width="100px"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DESIGNATION" ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Left"
                                                HeaderStyle-Width="120px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldesignation" runat="server" Text='<%# Eval("DESIG_NAME") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="120px"></HeaderStyle>
                                                <ItemStyle Width="120px"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                <asp:Label ID="LblAttendance" runat="server" Text='<%# Eval("ATTN_DATE") %>'></asp:Label>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SHIFT" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="40px">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblShift" runat="server" Text='<%# Eval("SFT_NAME") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="40px"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Right" Width="40px"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Attn.InTime" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblShiftInTime" runat="server" Text='<%# Eval("IN_TIME") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Attn.OutTime" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="40px">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblShiftOutTime" runat="server" Text='<%# Eval("OUT_TIME") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="40px"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Right" Width="40px"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="InTime" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="40px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtIntime" runat="server" onblur="return MaskTimeFormat(this)" Text='<%# Eval("IN_TIME") %>' CssClass="TextBoxNo SmallFont" Width="40px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="40px"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Right" Width="40px"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="OutTime" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="40px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtOutTime" onblur="return MaskTimeFormat(this)" runat="server" Text='<%# Eval("OUT_TIME") %>'
                                                        CssClass="TextBoxNo SmallFont" Width="40px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="40px"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Right" Width="40px"></ItemStyle>
                                            </asp:TemplateField>                                           
                                           <%-- <asp:TemplateField HeaderText="User Comment">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtUserComment" Text='<%#DataBinder.Eval(Container.DataItem, "APPROVE_COMMENT")%>'
                                                        runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                          <%--  <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblApprovedBy" runat="server" Text='<%# Eval("REPORTTO") %>'></asp:Label>                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                        <PagerStyle BackColor="#B30101" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#B30101"
                                            ForeColor="White" Font-Bold="True" />
                                    </asp:GridView>
                                </asp:Panel>
                            </fieldset>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
