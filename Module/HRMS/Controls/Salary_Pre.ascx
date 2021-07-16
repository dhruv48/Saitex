<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Salary_Pre.ascx.cs" Inherits="Module_HRMS_Controls_Salary_Pre" %>
<%@ Register TagPrefix="obout" Namespace="Obout.ComboBox" Assembly="obout_ComboBox" %>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
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
            var grid = document.getElementById("<%= GVSalaryRecord.ClientID %>");
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
                        <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" 
                            ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click" 
                            TabIndex="10" ToolTip="Exit" Width="48" />
                    </td>
                    <td ID="tdHelp" runat="server" valign="top">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" 
                            ImageUrl="~/CommonImages/link_help.png" TabIndex="11" ToolTip="Help" 
                            Width="48" />
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
            <legend> Filter Record By:</legend>     
            <table width="100%">
              <tr>                            
                    <td>Branch:</td>
                    <td><asp:DropDownList ID="DDLBranch" Width="130px" CssClass="TextBox SmallFont"  runat="server" ></asp:DropDownList>  </td>
                  <td>Department:</td>
                        <td>
                            <asp:DropDownList ID="DDLDepartment" Width="130px" CssClass="TextBox SmallFont"  runat="server" ></asp:DropDownList>
                        </td>                  
                    <td>Designation:</td>
                     <td><asp:DropDownList ID="DDLDesigination" Width="130px" CssClass="TextBox SmallFont"  runat="server" >  </asp:DropDownList>  </td>
                   
                       <td>Employee:</td>
                        <td>
                            <obout:ComboBox runat="server" ID="DDLEmployee" EnableVirtualScrolling="true" Width="130px"
                        Height="200px" DataTextField="EMPLOYEENAME" CssClass="SmallFont TextBox UpperCase"
                        DataValueField="EMP_CODE" EnableLoadOnDemand="true" OnLoadingItems="DDLEmployee_LoadingItems"
                        AutoPostBack="True"       MenuWidth="300px">
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
                       <td><asp:Button ID="CmdView" runat="server" Width="100px"  Text="View Records" onclick="CmdView_Click" /></td>                                  
                </tr>                
             </table>
        </fieldset> 
        </td> 
            
    </tr>   
      <tr><td colspan="4" class = "td">
        <table class="SmallFont" width="100%">
            <tr>
                <td>Attendance Record :-</td>
                <td><div style="width:20px; height:20px; background-color:#DA70D6;"></div> </td>
                <td>>= 25 LWP Days</td>
                <td> <div style="width:20px; height:20px; background-color:#FFF68F;"></div></td>
                 <td> >= 20 & < 25 LWP Days</td>
                <td><div style="width:20px; height:20px; background-color:#FFE4E1;"></div> </td></td>
                 <td>>= 10 & < 20 LWP Days</td>
            </tr>
        </table> 
      </td></tr>
<tr>
<td colspan="4">
<asp:UpdatePanel ID="UpdatePanel1"  runat="server">
    <ContentTemplate>
<fieldset>
            <legend> Attendance Record:</legend>    
  <asp:Panel ID="gridPanel" runat="server" ScrollBars="Vertical" Width="100%" Height="450px">
    <asp:GridView ID="GVSalaryRecord" runat="server" AutoGenerateColumns="False" 
        AllowSorting="True" Font-Size="X-Small" PageSize="15" 
        CellPadding="3"   GridLines="None" Width="100%" ForeColor="#333333" 
        CssClass = "smallfont"        
        onrowdatabound="GVSalaryRecord_RowDataBound">       
        <FooterStyle Width="100%" BackColor="#507CD1"  ForeColor="White" Font-Bold="True" />
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
            <asp:TemplateField  HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="40px">
                 <HeaderTemplate>
                         <asp:CheckBox ID="ChkMarkAll"  runat="server" AutoPostBack="True" />
                 </HeaderTemplate>
                 <ItemTemplate>
                        <asp:CheckBox ID="ChkMark"  runat="server"  />
                 </ItemTemplate>               

<HeaderStyle Width="40px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="EMP_CODE" HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="40px">
             <ItemTemplate>
                    <asp:Label ID="lblempcode" runat="server" Text='<%# Eval("EMP_CODE") %>'></asp:Label>
                </ItemTemplate>               

<HeaderStyle Width="40px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="EMPLOYEE" ItemStyle-Width="140px" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="140px"> <ItemTemplate>
                    <asp:Label ID="lblempname" runat="server" Text='<%# Eval("EMPLOYEENAME") %>'></asp:Label>
                </ItemTemplate>

<HeaderStyle HorizontalAlign="Left" Width="140px"></HeaderStyle>

<ItemStyle Width="140px"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="DEPARTMENT" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100px"> <ItemTemplate>
                    <asp:Label ID="lbldepartment" runat="server" Text='<%# Eval("DEPT_NAME") %>'></asp:Label>
                </ItemTemplate>

<HeaderStyle HorizontalAlign="Left" Width="100px"></HeaderStyle>

<ItemStyle Width="100px"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="DESIGNATION" ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="120px"> <ItemTemplate>
                    <asp:Label ID="lbldesignation" runat="server" Text='<%# Eval("DESIG_NAME") %>'></asp:Label>
                </ItemTemplate>

<HeaderStyle HorizontalAlign="Left" Width="120px"></HeaderStyle>

<ItemStyle Width="120px"></ItemStyle>
            </asp:TemplateField> 
            <asp:TemplateField HeaderText="OLD WORK DAYS" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="40px">
             <ItemTemplate>
             <asp:TextBox ID="TxtPaidDays" Enabled="false"  runat="server" Text='<%# Eval("PRE") %>'  CssClass="TextBoxNo SmallFont" Width ="40px"></asp:TextBox>
                </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" Width="40px"></HeaderStyle>

            <ItemStyle HorizontalAlign="Right" Width="40px"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="WORK DAYS" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Left">
             <ItemTemplate>
                 <asp:TextBox ID="TxtUpdateDays" runat="server" Enabled="false" 
                     onKeyPress="filterNonNumeric(this);" Text='<%# Eval("UPDATE_WORKING_DAYS") %>' 
                     MaxLength="2" CssClass="TextBoxNo SmallFont" Width ="40px" AutoPostBack="True" 
                     ontextchanged="TxtUpdateDays_TextChanged"></asp:TextBox>
                </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:TemplateField> 
            
             <asp:TemplateField HeaderText="SL" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="40px">
             <ItemTemplate>
             <asp:TextBox ID="TxtSL" runat="server" Enabled="false"   Text='<%# Eval("SL") %>'  CssClass="TextBoxNo SmallFont" Width ="40px"></asp:TextBox>
                </ItemTemplate>

<HeaderStyle HorizontalAlign="Left" Width="40px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Width="40px"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CL" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="40px">
             <ItemTemplate>
             <asp:TextBox ID="TxtCL" runat="server" Enabled="false"   Text='<%# Eval("CL") %>'  CssClass="TextBoxNo SmallFont" Width ="40px"></asp:TextBox>
                </ItemTemplate>

<HeaderStyle HorizontalAlign="Left" Width="40px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Width="40px"></ItemStyle>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="EL" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="40px">
             <ItemTemplate>
             <asp:TextBox ID="TxtEL" runat="server" Enabled="false"  Text='<%# Eval("EL") %>'  CssClass="TextBoxNo SmallFont" Width ="40px"></asp:TextBox>
                </ItemTemplate>

<HeaderStyle HorizontalAlign="Left" Width="40px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Width="40px"></ItemStyle>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="ML" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="40px">
             <ItemTemplate>
             <asp:TextBox ID="TxtML" runat="server" Enabled="false"  Text='<%# Eval("ML") %>'  CssClass="TextBoxNo SmallFont" Width ="40px"></asp:TextBox>
                </ItemTemplate>

<HeaderStyle HorizontalAlign="Left" Width="40px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Width="40px"></ItemStyle>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="CO" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="40px">
             <ItemTemplate>
             <asp:TextBox ID="TxtCO" runat="server" Enabled="false"  Text='<%# Eval("CO") %>'  CssClass="TextBoxNo SmallFont" Width ="40px"></asp:TextBox>
                </ItemTemplate>

<HeaderStyle HorizontalAlign="Left" Width="40px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Width="40px"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="NH" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="40px">
             <ItemTemplate>
             <asp:TextBox ID="TxtNH" runat="server" Enabled="false"  Text='<%# Eval("NH") %>'  CssClass="TextBoxNo SmallFont" Width ="40px"></asp:TextBox>
                </ItemTemplate>

<HeaderStyle HorizontalAlign="Left" Width="40px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Width="40px"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="WORK OFF" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="40px">
             <ItemTemplate>
             <asp:TextBox ID="TxtWO" runat="server" Enabled="false"  Text='<%# Eval("WO") %>'  CssClass="TextBoxNo SmallFont" Width ="40px"></asp:TextBox>
                </ItemTemplate>

<HeaderStyle HorizontalAlign="Left" Width="40px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Width="40px"></ItemStyle>
            </asp:TemplateField>            
            <asp:TemplateField HeaderText="WP DAYS" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="40px">
             <ItemTemplate>
             <asp:TextBox ID="TxtWithoutPay" runat="server" Enabled="false"  Text='<%# Eval("UPDATE_LWP_DAYS") %>' CssClass="TextBoxNo SmallFont" Width ="40px"></asp:TextBox>
                </ItemTemplate>

<HeaderStyle HorizontalAlign="Left" Width="40px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Width="40px"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="APPROVE WP DAYS" ItemStyle-HorizontalAlign="Right" >
             <ItemTemplate>
                 <asp:TextBox ID="TxtUpdateWP" Enabled="false" runat="server" onKeyPress="filterNonNumeric(this);" Text='<%# Eval("UPDATE_LWP_DAYS") %>' MaxLength="2" CssClass="TextBoxNo SmallFont" Width ="40px"></asp:TextBox>
                </ItemTemplate>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="TOTAL PAID DAYS" ItemStyle-HorizontalAlign="Right" >
             <ItemTemplate>
                 <asp:TextBox ID="TxtTotalPaidDays" onKeyPress="Daysvalidate(this);" 
                     runat="server" Text='<%# Eval("UPDATE_PAID_DAYS") %>'  
                     CssClass="TextBoxNo SmallFont" Width ="40px" AutoPostBack="True" ontextchanged="TxtTotalPaidDays_TextChanged"></asp:TextBox>
                </ItemTemplate>

            <ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="REMARKS">
                <ItemTemplate>
                    <asp:TextBox ID="txtremarks" runat="server" Text='<%# Eval("REMARKS") %>' CssClass="TextBox SmallFont" Width ="100px" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField> 
            <asp:TemplateField Visible="false" >
                <ItemTemplate>                
                    <asp:Label ID="LblSMonth" runat="server" Text='<%# Eval("SAL_MONTH") %>'></asp:Label>  
                    <asp:Label ID="LblSYear" runat="server" Text='<%# Eval("SAL_YEAR") %>'></asp:Label>
                    <asp:Label ID="LblTotalMonthDays" runat="server" Text='<%# Eval("MONTH_DAYS") %>'></asp:Label>                        
                </ItemTemplate>
            </asp:TemplateField> 
            
        </Columns>
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle 
            HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#507CD1" 
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