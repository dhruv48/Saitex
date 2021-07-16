<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MachineScheduling.aspx.cs" Inherits="Module_PlanningAndScheduling_Pages_MachineScheduling" UICulture="en-US" Culture="en-US" %>
<%@ Register TagPrefix="ECalendar" Namespace="ExtendedControls" Assembly="EventCalendar" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />   
   

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scr1" runat="server"></asp:ScriptManager> 
           <table width="100%">
       <tr>
       <td>&nbsp;</td>
       </tr>
       <tr>
       <td width="100%" align="center">
       <asp:GridView ID="gvSelectedDateEvents" runat="server" AllowSorting="True"  AutoGenerateColumns="False"  Width="100%"   >
          <Columns>
                                              <%--  <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                    <asp:Label ID="lbltodaydate" runat="server"   Text='<%# DateTime.Now.ToString() %>' CssClass="LabelNo smallfont" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Order No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtOrderNO" runat="server" CssClass="Label smallfont" 
                                                            ReadOnly="true"  Text='<%# Bind("ORDER_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Party Code">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtPINO" runat="server" CssClass="LabelNo smallfont" 
                                                            ReadOnly="true" TabIndex="21" Text='<%# Bind("PI_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Planned Qty">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtPlannedQty" runat="server" 
                                                            CssClass="LabelNo smallfont" ReadOnly="true" TabIndex="21" 
                                                            Text='<%# Bind("PLANNED_QTY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Process Code">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtProcessCode" runat="server" 
                                                            CssClass="LabelNo smallfont" ReadOnly="true" TabIndex="21" 
                                                            Text='<%# Bind("PROS_ROUTE_CODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Machine Group">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtMachineGroup" runat="server" 
                                                            CssClass="LabelNo smallfont" ReadOnly="true" TabIndex="21" 
                                                            Text='<%# Bind("MACHINE_GROUP") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Machine Code">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtMachineCode" runat="server" 
                                                            CssClass="LabelNo smallfont" ReadOnly="true" TabIndex="21" 
                                                            Text='<%# Bind("MACHINE_CODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="From Date">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                      <asp:TextBox ID="txtfromdate" runat="server" ReadOnly="true" Text='<%# Bind("SCHEDULED_DATE_FROM") %>'></asp:TextBox>
                                                   
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To Date">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                   
                                                    <ItemTemplate>
                                                       <asp:TextBox ID="txttodate" runat="server" ReadOnly="true" Text='<%# Bind("SCHEDULED_DATE_TO") %>'></asp:TextBox>
                                                     
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                    <%--      <asp:TemplateField ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Middle" >
                                        <ItemTemplate>                                           
                                            <asp:CheckBox ID="chk" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Left" />
                                           </asp:TemplateField>--%>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#336699" CssClass="SmallFont titleheading" 
                                                Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />                                  
        </asp:GridView>                                        
                                         
       </td>
       </tr>
       </table>
    <div>
    
    
       <table width="100%">
        
       <tr>
       <td>
       <asp:Label ID="lblStartDate" runat="server" Visible="false" ></asp:Label>
       <asp:Label ID="lblEndDate" runat="server" Visible="false" ></asp:Label>
       <b class="titleheading" >Machine&nbsp;Scheduling&nbsp;For&nbsp;<asp:Label  ID="lblorderplanning" runat="server" Text="Dyeing"></asp:Label></b>
       </td>
       </tr>

       <tr  id="trAddButton" runat="server">
       <td>
<%--       <asp:Button ID="btnAdd" runat="server" Text="Add Order" CssClass="AButton" 
               onclick="btnAdd_Click" />--%>
       </td>
       </tr>
       <tr>
       <td align="center" width="100%">
       <div>
       <table id="tblSchedule" runat="server"  width="90%" >
      
      <tr>
       <td width="20%" valign="top"><%--Order&nbsp;Number--%></td>
       <td width="20%" valign="top"><%--Planned&nbsp;Quantity--%></td>
       <td width="25%" valign="top"><%--Start&nbsp;Date--%></td>
       <td width="25%" valign="top"><%--End&nbsp;Date --%></td>
       <td width="10%" valign="top"></td>
       </tr> 
       <tr>
       <td width="20%" valign="top"><asp:TextBox ID="txtOrderNO" runat="server" ReadOnly="true" Width="90%" Visible="false"  ></asp:TextBox></td>
       <td width="20%" valign="top"><asp:TextBox ID="txtQty" runat="server"  Width="90%"  Visible="false" ></asp:TextBox></td>
       <td width="25%" valign="top" align="center">
       <asp:TextBox ID="txtFrom" runat="server" Width="50%" Enabled="false" Visible="false"   ></asp:TextBox> 
          
       <cc3:CalendarExtender ID="txtFromCalender" Format="dd/MM/yyyy" runat="server" TargetControlID="txtFrom"></cc3:CalendarExtender>
       <cc3:MaskedEditExtender ID="txtFromMask" runat="server" Mask="99/99/9999" MaskType="Date" PromptCharacter="_" TargetControlID="txtFrom"></cc3:MaskedEditExtender>
       <cc1:TimeSelector ID="startTime" runat="server" SelectedTimeFormat="TwentyFour"  AllowSecondEditing="true" Width="40%" Visible="false" ></cc1:TimeSelector>
      
        </td>
       <td width="25%" valign="top" align="center">
       <asp:TextBox ID="txtTo" runat="server" Width="50%"  Visible="false" ></asp:TextBox> 
              <cc3:CalendarExtender ID="txtToCalender" Format="dd/MM/yyyy" runat="server" TargetControlID="txtTo"  ></cc3:CalendarExtender>
       <cc3:MaskedEditExtender ID="txtToMask" runat="server" Mask="99/99/9999" MaskType="Date" PromptCharacter="_" TargetControlID="txtTo"></cc3:MaskedEditExtender>
      

        <cc1:TimeSelector ID="endTime" runat="server"  AllowSecondEditing="true" SelectedTimeFormat="TwentyFour"  Width="40%"  Visible="false" ></cc1:TimeSelector>
       
         <%--<asp:RequiredFieldValidator ID="rfvod" runat="server" ControlToValidate="txtTo" Display="Dynamic" ErrorMessage="*" Font-Bold="False" ></asp:RequiredFieldValidator>
      --%> </td>
       <td width="10%" valign="top"><asp:Button ID="btnSubmit" Text="Schedule" CssClass="AButton"  runat="server"  Width="80%" 
               onclick="btnSubmit_Click"  Visible="false" /></td>
       </tr> 
       </table>
       </div>
       </td>
       </tr>
       <tr>
       <td> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSaveSchedult" runat="server" Text="Save" onclick="btnSaveSchedult_Click" Visible="false" />
        <asp:Button ID="btnClose" runat="server" Text="Close" 
            onclick="btnClose_Click"  Visible="false" /></td>
       </tr>
       <tr>
       <td width="100%" valign="middle" align="center">
          <ECalendar:EventCalendar ID="Calendar1" runat="server" BackColor="White" BorderColor="Silver"
            BorderWidth="1px" Font-Names="Verdana"
            Font-Size="9pt" ForeColor="Black" Height="600px"
            Width="80%" FirstDayOfWeek="Monday" NextMonthText="Next &gt;" 
               PrevMonthText="&lt; Prev" SelectionMode="DayWeekMonth" ShowGridLines="True" NextPrevFormat="FullMonth" 
              ShowDescriptionAsToolTip="True" BorderStyle="Solid" EventDateColumnName="" 
               EventDescriptionColumnName="" EventHeaderColumnName="" 
               OnSelectionChanged="Calendar1_SelectionChanged" 
               onvisiblemonthchanged="Calendar1_VisibleMonthChanged" >
            <SelectedDayStyle BackColor="#333399" ForeColor="White" />
            <TodayDayStyle BackColor="#CCCCCC" />
            <SelectorStyle BorderColor="#404040" BorderStyle="Solid" />
            <DayStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="True" />
            <OtherMonthDayStyle ForeColor="#999999" />
            <NextPrevStyle Font-Size="8pt" ForeColor="#333333" Font-Bold="True" VerticalAlign="Bottom" />
            <DayHeaderStyle BorderWidth="1px" Font-Bold="True" Font-Size="8pt" />
            <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True"
                Font-Size="12pt" ForeColor="#333399" HorizontalAlign="Center" VerticalAlign="Middle" />
        </ECalendar:EventCalendar>
       </td>
       </tr>
       </table>
       


    </div>  
    
    
    
   
     
    
    
    
    
    </form>
</body>
</html>
