<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="GridDispayofSalary.aspx.cs" Inherits="Module_HRMS_Pages_GridDispayofSalary"  Title="View Salary Slip" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.ComboBox" Assembly="obout_ComboBox" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
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
<script runat="server">
    decimal TotalUnitPrice;
    decimal GetUnitPrice(decimal Price)
    {
        TotalUnitPrice += Price;
        return Price;
    }
    decimal GetTotal()
    {
        return TotalUnitPrice;
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
    <script type="text/javascript">
        function NewWindow() 
        {
        document.forms[0].target = "_blank";
        }
        </script>
    <table class="td tContent" width="100%">
    <tr>
        <td colspan="4" class="td">
            <table class="tContent">
                <tr>                    
                    <td ID="tdClear" runat="server" valign="top">
                        <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" 
                            ImageUrl="~/CommonImages/clear.jpg"  
                            OnClientClick="javascript:return window.confirm('Are you sure you want to clear this record')" 
                            TabIndex="8" ToolTip="Clear" Width="48" onclick="imgbtnClear_Click" />
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
             <td align="left" class="TableHeader td">
                <table border="0" width="100%">
                    <tr>
                        <td align="center"  width="100%" >
                            <span class="titleheading"><b>View Salary</b></span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        
        <tr>
        
            <td width="100%" align="left" class="td">
                <table border="0" cellpadding="3" cellspacing="0" width="100%" class="tContentArial">
                    <tr>
                        <td colspan="8" align="center" valign="top" >
                            <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
                            <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                     <td width="10%" align="right" valign="top">
                            Branch
                        </td>                       
                        <td width="30%" align="left" valign="top">
                            <asp:DropDownList ID="ddlBranch" runat="server" Width="160px" 
                                CssClass="SmallFont" AutoPostBack="True" 
                                onselectedindexchanged="ddlBranch_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td width="10%" align="right" valign="top">
                            Year
                        </td>                        
                        <td width="30%" align="left" valign="top">
                            <asp:DropDownList ID="ddlYear" runat="server" Width="160px" CssClass="SmallFont">
                            </asp:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlYear"
                                ErrorMessage="Pls select year" ValidationGroup="M1" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <td width="10%" align="right" valign="top">
                            Month
                        </td>                        
                        <td width="30%" align="left" valign="top">
                            <asp:DropDownList ID="ddlMonth" runat="server" Width="160px" 
                                CssClass="SmallFont" AutoPostBack="True" 
                                onselectedindexchanged="ddlMonth_SelectedIndexChanged">
                                <asp:ListItem Value="" Text="-------------Select-----------"></asp:ListItem>
                                <asp:ListItem Value="01" Text="January"></asp:ListItem>
                                <asp:ListItem Value="02" Text="February"></asp:ListItem>
                                <asp:ListItem Value="03" Text="March"></asp:ListItem>
                                <asp:ListItem Value="04" Text="April"></asp:ListItem>
                                <asp:ListItem Value="05" Text="May"></asp:ListItem>
                                <asp:ListItem Value="06" Text="June"></asp:ListItem>
                                <asp:ListItem Value="07" Text="July"></asp:ListItem>
                                <asp:ListItem Value="08" Text="August"></asp:ListItem>
                                <asp:ListItem Value="09" Text="September"></asp:ListItem>
                                <asp:ListItem Value="10" Text="October"></asp:ListItem>
                                <asp:ListItem Value="11" Text="November"></asp:ListItem>
                                <asp:ListItem Value="12" Text="December"></asp:ListItem>
                            </asp:DropDownList>                            
                        </td>
                         <td width="10%" align="right" valign="top">
                            Department
                        </td>                       
                        <td width="30%" align="left" valign="top">
                            <asp:DropDownList ID="ddlDepartment" runat="server" Width="160px" 
                                CssClass="SmallFont" AutoPostBack="True" 
                                onselectedindexchanged="ddlDepartment_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>                       
                    </tr>
                    <tr>
                    
                        <td align="right">Designation:</td>
                        <td><asp:DropDownList ID="DDLDesigination" Width="160px" 
                            CssClass="TextBox SmallFont" AutoPostBack="True" runat="server" 
                            onselectedindexchanged="DDLDesigination_SelectedIndexChanged">  </asp:DropDownList>  </td>
                       
                        <td  class="tdRight">
                                Cadder
                            </td>
                            <td  class="tdLeft">
                                <asp:DropDownList ID="DDLCader" Width="160px" CssClass="gCtrTxt" runat="server">
                                    <asp:ListItem Value="0">----------SELECT---------</asp:ListItem>
                                    <asp:ListItem Value="STAFF">STAFF</asp:ListItem>
                                    <asp:ListItem Value="WORKMEN">WORKMEN</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="right">
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
                    </tr>
                   
                    <tr>
                    <td colspan="6" align="center" valign="top" style="height: 25px;">                       
                        <asp:Button ID="btnView" Text="View" runat="server" Width="75" OnClick="btnView_Click"        CssClass="AButton" />
                       <asp:Button ID="btnPrint" Text="Print All" runat="server" Width="75" OnClientClick="NewWindow();"  CssClass="AButton"  onclick="btnPrint_Click" />
                    <br />
                    </td>
                    </tr>
                </table>
            </td>
        </tr>
        
        <tr>
            <td width="100%" align="left" >
                <table border="0" cellpadding="0" cellspacing="0" width="100%" align="left" class="tContentArial">
                    <tr>
                        <td width="100%" align="left">
                            <b>Total Record : &nbsp;&nbsp;</b><asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td  class="tdleft" >
                <table border="0" cellpadding="0" cellspacing="0" width="100%"  align="left" class="tContentArial">
                    
                    <tr>
                        <td width="100%px" align="left">
                          <asp:Panel ID="gridPanel" runat="server" ScrollBars="Vertical" Width="100%" Height="450px">
                            <asp:GridView ID="gvSalaryDisplay" AutoGenerateColumns="false" AllowSorting="True"  Font-Size="X-Small" CellPadding="3"   
                                    GridLines="Both" Width="100%" ForeColor="#333333" PageSize="25" CssClass = "smallfont" runat="server" ShowFooter="True"
                                ShowHeader="true"  onrowdatabound="gvSalaryDisplay_RowDataBound">
                               <FooterStyle Width="100%" BackColor="#507CD1"  ForeColor="White" Font-Bold="True" />
                                <RowStyle BackColor="#EFF3FB" />
                                <EmptyDataRowStyle Font-Bold="True" Font-Names="Annabel Script" Font-Size="X-Small" />
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No." ItemStyle-VerticalAlign="top">
                                          <ItemTemplate><%#Container.DataItemIndex+1 %>
                                          </ItemTemplate>
                                          <ItemStyle VerticalAlign="Top" HorizontalAlign="Center" Width="3%" />
                                          <HeaderStyle VerticalAlign="Top" HorizontalAlign="Center" />
                                     </asp:TemplateField>  
                                    <asp:BoundField HeaderText="Month" DataField="SAL_MONTH"  ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Year" DataField="SAL_YEAR"  ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Employee Code" DataField="EMP_CODE" 
                                        ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Employee Name" DataField="empName" 
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Paid Day" DataField="PAID_DAY"  ItemStyle-HorizontalAlign="right" />
                                    <asp:BoundField HeaderText="Total Amount" DataField="ERN_AMT" 
                                        ItemStyle-HorizontalAlign="right" />
                                    <asp:BoundField HeaderText="Total Loan" DataField="LOAN_AMT" 
                                        ItemStyle-HorizontalAlign="right" />
                                    <asp:BoundField HeaderText="Total Deduction"  HtmlEncode="False" FooterStyle-HorizontalAlign="Center"  FooterText="TOTAL" FooterStyle-Font-Bold="true"  DataFormatString="{0:N}"  DataField="DEDCT_AMT" 
                                        ItemStyle-HorizontalAlign="right" />
                                        <asp:TemplateField FooterStyle-HorizontalAlign="Right"  ItemStyle-HorizontalAlign="Right"  HeaderText="Net Salary">
                                             <ItemTemplate>
                                             <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("NET_SAL","{0:N2}").ToString()%>'> </asp:Label>
                                             </ItemTemplate>
                                             <FooterTemplate>
                                             <asp:Label ID="lblTotal"  Font-Bold="true"  runat="server"></asp:Label>
                                             </FooterTemplate>
                                         </asp:TemplateField>                                    
                                    <asp:TemplateField HeaderText="Salary Print" ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a target="_blank" href="../../Admin/Pages/PrintSSlip.aspx?SalaryId=<%# Eval("SAL_SLIP_MST_ID") %>&Year=<%# Eval("SAL_YEAR") %>&EmpCode=<%# Eval("EMP_CODE") %>&Month=<%# Eval("SAL_MONTH") %>"><b>Print</b></a>
                                        </ItemTemplate>                                       
                                    </asp:TemplateField>
                                </Columns>
                                  <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle HorizontalAlign="Justify"  VerticalAlign="Top" />
                            </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </ContentTemplate>
</asp:UpdatePanel>
 </asp:Content>
