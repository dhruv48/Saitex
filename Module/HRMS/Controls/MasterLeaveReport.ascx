<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MasterLeaveReport.ascx.cs" Inherits="Module_HRMS_Controls_MasterLeaveReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="obout_Interface" namespace="Obout.Interface" tagprefix="cc2" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="Combo" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
 <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
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
        width: 40px;
    }
    .c2
    {
        margin-left: 4px;
        width: 100px;
    }
     .c3
    {
        width: 60px;
    }
    .c4
    {
        margin-left: 4px;
        width: 250px;
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
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
<table id="tblDesgMainTable" runat="server" cellspacing="0" cellpadding="0" align="Left" class="tContentArial">
    <tr>
        <td align="Right" class="td">
            <table align="left">
                <tr>                                         
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                            Width="48" Height="41" ></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                            Width="48" Height="41" ></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                            Width="48" Height="41" onclick="imgbtnExit_Click" ></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                            Width="48" Height="41"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class ="TableHeader td">
            <span class="titleheading">Leave Report Master</span>
        </td>
    </tr>
      
    <tr>
        <td align="left" valign="top" class ="td">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>     
    <tr>
        <td class ="td">
            <table border="0" cellpadding="3" cellspacing="0" align="left" width="750">
                   <tr>
                        <td>Record From:</td>
                        <td>
                            <cc2:OboutDropDownList ID="DDLTable" Width="200px"   runat="server" 
                                AutoPostBack="True" onselectedindexchanged="DDLTable_SelectedIndexChanged">
                               <asp:ListItem Value="0">------------SELECT------------</asp:ListItem>
                                            <asp:ListItem Value="CL">Casual Leave</asp:ListItem>
                                            <asp:ListItem Value="SL">Sick Leave</asp:ListItem>
                                            <asp:ListItem Value="EL">Earn Leave</asp:ListItem> 
                                            <asp:ListItem Value="ML">Medical Leave</asp:ListItem>  
                                            <asp:ListItem Value="LW">Leave Without Pay</asp:ListItem> 
                                            <asp:ListItem Value="CO">Compensatory Off</asp:ListItem>                                            
                                            <asp:ListItem Value="DW">Department Wise</asp:ListItem>
                                            <asp:ListItem Value="O">Out Door Duty</asp:ListItem>  
                                               
                            </cc2:OboutDropDownList>
                        </td>
                        <td></td>
                        <td>Employee:</td>
                        <td>
                            <Combo:ComboBox ID="cmbEmpCode" runat="server" Width="200px" EmptyText="------------SELECT------------" Height="150px" AutoPostBack="true" 
                                                TabIndex="0" DataTextField="F_NAME" MenuWidth="300px" DataValueField="EMP_CODE" 
                                                    onloadingitems="cmbEmpCode_LoadingItems" >
                                                <HeaderTemplate>
                                                    <div class="header c3">
                                                        Code
                                                    </div>
                                                    <div class="header c4">
                                                        Emp Name</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div class="item c3">
                                                        <%# Eval("EMP_CODE")%></div>
                                                    <div class="item c4">
                                                        <%# Eval("EMPLOYEENAME")%></div>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Displaying items
                                                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                    out of
                                                    <%# Container.ItemsCount %>.
                                                </FooterTemplate>
                                            </Combo:ComboBox>
                        </td>
                   </tr>
                   <tr>
                        <td>Month:</td>
                        <td>
                            <cc2:OboutDropDownList ID="DDLMonth" Height="150px" runat="server" Width="200px">
                                <asp:ListItem Value="" Text="------------SELECT------------"></asp:ListItem>
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
                            </cc2:OboutDropDownList>
                        </td>
                        <td></td>
                        <td>Department:</td>
                        <td>
                            <cc2:OboutDropDownList ID="DDLDept" Height="150px" runat="server" Width="200px">
                            </cc2:OboutDropDownList>
                        </td>
                   </tr>
                   <tr>
                        <td>Shift:</td>
                        <td>
                            <cc2:OboutDropDownList ID="DDLShift" Height="100px" runat="server" Width="200px">
                            </cc2:OboutDropDownList>
                        </td>
                        <td></td>
                        <td>Designation:</td>
                        <td>
                            <cc2:OboutDropDownList ID="DDLDesign" Height="150px" runat="server" Width="200px">
                            </cc2:OboutDropDownList>
                        </td>
                   </tr>
                   <tr>
                        <td>Year:</td>
                        <td>
                            <cc2:OboutDropDownList ID="DDLYear" Height="150px" runat="server" Width="200px">
                            </cc2:OboutDropDownList>
                        </td>
                        <td></td>
                        <td>Branch:</td>
                        <td>
                            <cc2:OboutDropDownList ID="DDLBranch" Height="150px" runat="server" Width="200px">
                            </cc2:OboutDropDownList>
                        </td>
                   </tr>
                   <tr>
                        <td>From Date:</td>
                        <td>
                            <asp:TextBox ID="TxtFromDate"  CssClass="gCtrTxt"  runat="server" Width="200px"></asp:TextBox>
                             <cc1:CalendarExtender ID="CEFromDate" runat="server" PopupPosition="Right" Format="dd/MM/yyyy" TargetControlID="TxtFromDate">
                            </cc1:CalendarExtender>
                        </td>
                        <td></td>
                        <td>To Date:</td>
                        <td>
                            <asp:TextBox ID="TxtToDate"  CssClass="gCtrTxt"  runat="server" Width="200px"></asp:TextBox>
                             <cc1:CalendarExtender ID="CEToDate" runat="server" PopupPosition="Right" Format="dd/MM/yyyy" TargetControlID="TxtToDate">
                             </cc1:CalendarExtender>
                        </td>
                   </tr>                  
                    <tr>
                        <td colspan="5" align="center" >
                            <asp:Button ID="CMDPrint" Text="Print" CssClass="AButton" runat="server"    
                                Width="175" ValidationGroup="M1" onclick="CMDPrint_Click"  />
                        </td>                       
                   </tr>
                </table>
        </td>
    </tr>
  </table>
  </ContentTemplate>                   
                </asp:UpdatePanel>