<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Attn_Mark_All.ascx.cs" Inherits="Module_HRMS_Controls_Attn_Mark_All" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script language="javascript" type="text/javascript">
        function ShowModalPopup(ModalBehaviour) {
            $find(ModalBehaviour).show();
        }
        
        function HideModalPopup(ModalBehaviour) {
            $find(ModalBehaviour).hide();
        }        
    </script>
<cc1:ModalPopupExtender runat="server" PopupControlID="PanLoad" ID="ModalProgress" 
                        TargetControlID="PanLoad" BackgroundCssClass="modalBackground" BehaviorID="pload">
</cc1:ModalPopupExtender>
 <asp:Panel ID="PanLoad" runat="server" CssClass="modalPopup">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
        AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>        
            <img src="../../../CommonImages/pleasewait.gif" alt="" /><br />Please Wait....
            </ProgressTemplate>
            </asp:UpdateProgress>
  </asp:Panel> 
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<table class="td tContent" width="90%">
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
        <td class="TableHeader td" colspan="4" align="center"><span class="titleheading">Attendance Data For Salary Calculation</span></td>
    </tr>
    <tr>
        <td align="right" class="TdBackVir">    
            Attendance Year
        </td>
        <td align="left" class="TdBackVir">
            <asp:TextBox ID="TxtYear" CssClass="gCtrTxt" Width="150px" Enabled="false" runat="server"></asp:TextBox>
        </td>
        
        <td align="right" class="TdBackVir">    
            Attendance Month
        </td>
        <td align="left" class="TdBackVir">
          <asp:TextBox ID="TxtMonth" CssClass="gCtrTxt" Width="150px" Enabled="false" runat="server"></asp:TextBox>
        </td>
  </tr>  
    <tr>
        <td align="right" class="TdBackVir">    
            Branch
        </td>
        <td align="left" class="TdBackVir">
             <asp:DropDownList ID="ddlBranch" runat="server" Width="150px" 
                 CssClass="gCtrTxt" AutoPostBack="True" 
                 onselectedindexchanged="ddlBranch_SelectedIndexChanged">    </asp:DropDownList>                       
                            
        </td>
        
        <td align="right" class="TdBackVir">    
            Department
        </td>
        <td align="left" class="TdBackVir">
              <asp:DropDownList ID="ddlDepartment" runat="server" Width="150px" 
                  CssClass="gCtrTxt" AutoPostBack="True" 
                  onselectedindexchanged="ddlDepartment_SelectedIndexChanged">   </asp:DropDownList>                           
        </td>
  </tr>
  <tr>
  <td class="TdBackVir" colspan="4" align="center" >
     <asp:Button ID="btnSalaryGenerate" Text="Calculate" OnClientClick="ShowModalPopup('pload');" CssClass="AButton" runat="server"
                                Width="125" ValidationGroup="M1" OnClick="btnSalaryGenerate_Click" /></td>
  </tr>
<tr><td colspan="4" class = "td">Attendance Record :-</td></tr>
<tr>
<td colspan="4">
    <asp:GridView ID="GVAttendance" runat="server" AutoGenerateColumns="False" 
        AllowPaging="True" AllowSorting="True" Font-Size="X-Small" PageSize="25" 
        CellPadding="3"   GridLines="None" Width="100%" ForeColor="#333333" 
        CssClass = "smallfont" 
        onpageindexchanging="GVAttendance_PageIndexChanging" 
        EmptyDataText="No Record Found" >       
        <FooterStyle Width="100%" BackColor="#507CD1"  ForeColor="White" Font-Bold="True" />
        <RowStyle BackColor="#EFF3FB" />
        <EmptyDataRowStyle Font-Bold="True" />
        <Columns>
            <asp:TemplateField HeaderText="S.No." >
                   <ItemTemplate>
                       <%#Container.DataItemIndex+1 %>
                   </ItemTemplate>
                   <ItemStyle  HorizontalAlign="Center" Width="3%" />
                   <HeaderStyle  HorizontalAlign="Center" />
        </asp:TemplateField>
            <asp:BoundField DataField="EMP_CODE" ItemStyle-Width="5%" 
                HeaderStyle-Width="5%" HeaderText="EMP.CODE" >
                <HeaderStyle Width="5%" />
                <ItemStyle Width="5%" />
            </asp:BoundField>
            <asp:BoundField DataField="EmployeeName" ItemStyle-Width="15%" 
                HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Left" 
                HeaderStyle-HorizontalAlign="Left"   HeaderText="EMPLOYEE NAME" >
                <HeaderStyle HorizontalAlign="Left" Width="15%" />
                <ItemStyle HorizontalAlign="Left" Width="15%" />
            </asp:BoundField>
            <asp:BoundField DataField="DEPT_NAME" ItemStyle-Width="10%" 
                HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" 
                HeaderText="DEPARTMENT" >
                <HeaderStyle Width="10%" />
                <ItemStyle HorizontalAlign="Left" Width="10%" />
            </asp:BoundField>
            <asp:BoundField DataField="DESIG_NAME" ItemStyle-Width="10%" 
                HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" 
                HeaderText="DESIGNATION" >
                <HeaderStyle Width="10%" />
                <ItemStyle HorizontalAlign="Left" Width="10%" />
            </asp:BoundField>
            <asp:BoundField DataField="SAL_MONTH" ItemStyle-Width="5%" 
                HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center"  HeaderText="MONTH" >
                <HeaderStyle Width="5%" />
                <ItemStyle HorizontalAlign="Center" Width="5%" />
            </asp:BoundField>
            <asp:BoundField DataField="SAL_YEAR" ItemStyle-Width="5%" 
                HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderText="YEAR" >
                <HeaderStyle Width="5%" />
                <ItemStyle HorizontalAlign="Center" Width="5%" />
            </asp:BoundField>
            <asp:BoundField DataField="UPDATE_WORKING_DAYS" ItemStyle-Width="5%" 
                HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center" 
                HeaderText="WORK DAYS" >
                <HeaderStyle Width="5%" />
                <ItemStyle HorizontalAlign="Center" Width="5%" />
            </asp:BoundField>
            <asp:BoundField DataField="UPDATE_LWP_DAYS" ItemStyle-Width="5%" 
                HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center" 
                HeaderText="LWP DAYS" >
                <HeaderStyle Width="5%" />
                <ItemStyle HorizontalAlign="Center" Width="5%" />
            </asp:BoundField>
            <asp:BoundField DataField="PAID_DAYS" ItemStyle-Width="5%" 
                HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center" 
                HeaderText="PAID DAYS" >
                <HeaderStyle Width="5%" />
                <ItemStyle HorizontalAlign="Center" Width="5%" />
            </asp:BoundField>
           <asp:BoundField DataField="UPDATE_PAID_DAYS" ItemStyle-Width="5%" 
                HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center" 
                HeaderText="TOTAL PAID DAYS" >
               <HeaderStyle Width="5%" />
               <ItemStyle HorizontalAlign="Center" Width="5%" />
            </asp:BoundField>
            <asp:BoundField DataField="A_STATUS" ItemStyle-Width="5%" 
                HeaderStyle-Width="5%" HtmlEncode="false" ItemStyle-HorizontalAlign="Left" 
                HeaderText="STATUS" >
            
                <HeaderStyle Width="5%" />
                <ItemStyle HorizontalAlign="Left" Width="5%" />
            </asp:BoundField>
            
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
</ContentTemplate>
</asp:UpdatePanel>