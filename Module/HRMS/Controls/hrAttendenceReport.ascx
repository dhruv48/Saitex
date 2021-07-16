<%@ Control Language="C#" AutoEventWireup="true" CodeFile="hrAttendenceReport.ascx.cs" Inherits="Module_HRMS_Controls_hrAttendenceReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>
<table class="td tContentArial" width = "950px">
<tr>
        <td align="left"  class="td" colspan = "6">
            <table align="left">
                <tr>               
                    <td id="tdPrint" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" Width="48"  />
                    </td>
                    <td id="tdExit" runat="server">
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                            Width="48" Height="41" onclick="imgbtnExit_Click"  ></asp:ImageButton>
                    </td>
                    <td id="tdHelp" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                            ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                    </td>
                </tr>
            </table>
                        
        </td>
    </tr>
    <tr>
        <td class="TableHeader td" align="center" colspan = "6">
            <span class="titleheading">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ATTENDENCE SUMMARY &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
    <tr>
        <td align="left" valign="top" class="td" colspan="6">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>
 
 <tr>
 <td align = "right" >Employee :</td><td>
            <asp:DropDownList ID="ddlemployee" runat="server" DataTextField="EMPLOYEE" 
                DataValueField="EMP_CODE" 
                Width="128px" AutoPostBack="True" CssClass="tContentArial" 
                onselectedindexchanged="ddlemployee_SelectedIndexChanged" >   
            </asp:DropDownList>
            </td><td align = "right">Branch:</td><td>
        <asp:DropDownList ID="ddlbranchcode" runat="server" DataTextField="BRANCH_NAME" 
            DataValueField="BRANCH_CODE" 
             Width="128px" 
                AutoPostBack="True" CssClass="tContentArial" onselectedindexchanged="ddlbranchcode_SelectedIndexChanged"  
                >
        </asp:DropDownList>
        </td><td align = "right">Company:</td><td>
          <asp:DropDownList ID="ddlcompany" runat="server" DataTextField="COMP_NAME" 
                DataValueField="COMP_CODE" 
                Width="128px" 
                AutoPostBack="True" CssClass="tContentArial" onselectedindexchanged="ddlcompany_SelectedIndexChanged" 
              >
            </asp:DropDownList>
            </td>
  </tr>
  <tr>
 <td align = "right">Department :</td><td> 
            <asp:DropDownList ID="ddldept" runat="server" DataTextField="DEPT_NAME" 
                DataValueField="DEPT_CODE" 
                Width="128px" 
                AutoPostBack="True" CssClass="tContentArial" onselectedindexchanged="ddldept_SelectedIndexChanged" 
               ></asp:DropDownList>
           
            </td><td align = "right">Designation:</td>
 <td>
     <asp:DropDownList ID="ddldesig" runat="server" DataTextField="DESIG_NAME" DataValueField="DESIG_CODE" 
                Width="128px" 
                AutoPostBack="True" CssClass="tContentArial" onselectedindexchanged="ddldesig_SelectedIndexChanged" 
          >
     </asp:DropDownList>
 </td><td align = "right">Month :</td><td>
        <asp:DropDownList ID="ddlmonth" runat="server" DataTextField="MONTH" 
                DataValueField="MONTH" 
                Width="128px" 
                AutoPostBack="True" CssClass="tContentArial" 
                onselectedindexchanged="ddlmonth_SelectedIndexChanged" Font-Size="10px" 
            >
            <asp:ListItem Value="SELECT">---------Select----------</asp:ListItem>
            <asp:ListItem Value="01">JANUARY</asp:ListItem>
            <asp:ListItem Value="02">FEBRUARY</asp:ListItem>
            <asp:ListItem Value="03">MARCH</asp:ListItem>
            <asp:ListItem Value="04">APRIL</asp:ListItem>
            <asp:ListItem Value="05">MAY</asp:ListItem>
            <asp:ListItem Value="06">JUNE</asp:ListItem>
            <asp:ListItem Value="06">JULY</asp:ListItem>
            <asp:ListItem Value="07">AUGUST</asp:ListItem>
            <asp:ListItem Value="09">SEPTEMBER</asp:ListItem>
            <asp:ListItem Value="10">OCTOBER</asp:ListItem>
            <asp:ListItem Value="11">NOVEMBER</asp:ListItem>
            <asp:ListItem Value="12">DECEMBER</asp:ListItem>
            </asp:DropDownList></td>
 </tr>

 
 <tr>
 <td align = "right">Attendence :</td>
 <td> 
  <asp:DropDownList ID="DropDownList1" runat="server" 
                Width="128px" 
                AutoPostBack="True" CssClass="tContentArial" 
         onselectedindexchanged="DropDownList1_SelectedIndexChanged" 
         style="height: 22px" Font-Size="10px" > 
      <asp:ListItem Value="SELECT">---------Select----------</asp:ListItem>
      <asp:ListItem Value="P">PRESENT</asp:ListItem>
      <asp:ListItem Value="A">ABSENT</asp:ListItem>
      <asp:ListItem Value="LV">LEAVE</asp:ListItem>
      <asp:ListItem Value="0D">OUT DUTY</asp:ListItem>
      <asp:ListItem Value="WO">OFF WORK</asp:ListItem>
      <asp:ListItem Value="SL">SEEK LEAVE</asp:ListItem>
     </asp:DropDownList>
     </td>
 
 <td  align = "right">Form Date :</td>
 <td>
     <asp:TextBox ID="txtformdate" runat="server" AutoPostBack="True" Width="127px" ontextchanged="txtformdate_TextChanged" 
         ></asp:TextBox>
     <cc4:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
         TargetControlID="txtformdate" PopupPosition="TopLeft">
                        </cc4:CalendarExtender>
            </td>
 <td align = "right">To Date :</td>
 <td><asp:TextBox ID="txtTodate" runat="server" AutoPostBack="True" Width="127px" ontextchanged="txtTodate_TextChanged" 
         ></asp:TextBox>
     <cc4:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
         TargetControlID="txtTodate" PopupPosition="TopLeft">
                        </cc4:CalendarExtender></td>
 
 </tr>

  <table>
   <td colspan="5"  class = "td tContentArial">
   
  <asp:Panel ID="pnlShowHover" runat="server" Width="950px" BackColor="Beige" BorderWidth="2px"
                                Height="240px" ScrollBars="Auto">
      <asp:GridView ID="GridAttendence" runat="server" 
    AutoGenerateColumns="False" 
                 Width="100%" CellPadding="4" ForeColor="#333333" 
                GridLines="None"  HeaderStyle-Wrap = "true" Font-Size="X-Small" 
          Height="216px"  >
          <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
          <RowStyle BackColor="#EFF3FB" />
          <Columns>
              <asp:BoundField DataField="EMP_CODE" HeaderText="EMP CODE" />
              <asp:BoundField DataField="Employee" HeaderText="EMPLOYEE" />
              <asp:BoundField DataField="COMP_NAME" HeaderText="COMPANY "   />
              <asp:BoundField DataField="BRANCH_NAME" HeaderText="BRANCH"  />
              <asp:BoundField DataField="DEPT_NAME" HeaderText="DEPARTMENT" />
              <asp:BoundField DataField="DESIG_NAME" HeaderText="DESIGNATION" />
              <asp:BoundField DataField="MONTH" HeaderText="MONTH" />  
              <asp:BoundField DataField="ATTN_DATE" HeaderText="ATTN DATE" DataFormatString="{0:dd/MM/yyyy}"  />
             <asp:BoundField DataField="ATTNTYPE" HeaderText="STATUS" />
          </Columns>
          <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
          <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
          <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
          <EditRowStyle BackColor="#2461BF" />
          <AlternatingRowStyle BackColor="White" />
      </asp:GridView>
                                   
   </asp:Panel>
        </td>
 </table>
 </tr> 

 </table>