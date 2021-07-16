<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HiringQueryDetail.ascx.cs" Inherits="Module_HRMS_Controls_HiringQueryDetail" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style type="text/css">
    .style3
    {
        width: 12%;
    }
    .style4
    {
        width: 12%;
        height: 26px;
    }
    .textbox
    {
        margin-left: 0px;
    }
    .style5
    {
        width: 68px;
    }
</style>
<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>--%>
<table align="left" width="100%" class="tContentArial">
    <tr>
        <td>
            <table width="100%" class="tContentArial" cellspacing="0" cellpadding="0" align="left">
                     <tbody>
                            <tr>
                                <td align="left" class="td" colspan="3">
                                    <table class="tContentArial" cellspacing="0" cellpadding="0" >
                                        <tbody>
                                            <tr>
                  <td id="tdClear" runat="server" align="left" width="48">
                                                    <asp:ImageButton ID="imgbtnClear"  runat="server" 
                                                        ImageUrl="~/CommonImages/clear.jpg" ToolTip="Clear" 
                                                        onclick="imgbtnClear_Click" ></asp:ImageButton>
                                                </td>
                                                <td id="tdPrint" runat="server" align="left" width="48">
                                                    <asp:ImageButton ID="imgbtnPrint"  runat="server" ImageUrl="~/CommonImages/link_print.png" ToolTip="Print" ></asp:ImageButton>
                                                </td>
                                                 <td id="tdExit" runat="server" align="left" width="48">
                                                    <asp:ImageButton ID="imgbtnExit"  runat="server" ToolTip="Exit"
                                                        ImageUrl="~/CommonImages/link_exit.png" Width="48" Height="41" 
                                                         OnClientClick="javascript:return window.confirm('Are you sure you want to Exit')" 
                                                         onclick="imgbtnExit_Click">
                                                    </asp:ImageButton>
                                                </td>
                                                <td id="tdHelp" runat="server" align="left" width="48">
                                                    <asp:ImageButton ID="imgbtnHelp"  runat="server" ImageUrl="~/CommonImages/link_help.png" ToolTip="Help" ></asp:ImageButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableHeader td" align="center" colspan="8">
                                    <b class="titleheading"> Hiring Query Detail</b>
                                </td>
                            </tr>
                            <tr>
                                <td class="td" valign="top" align="left" colspan="3">
                                    <span class="Mode">You are in
                                        <asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode </span>
                                </td>
                            </tr>
                            
                            <tr>
                                <td class="td">
                                    <table width="100%">
                                    </tr>                                        
                                        <tr>
                                            <td colspan="8">
                                                <b><i>Hiring Details</i></b>
                                            </td>
                                        </tr>
                                        <tr id="trFind" runat="server" >
                                      <td style="width:150px;"></td>                                           
                                        </tr>
                                     <%--   <tr>
                                            <td style="text-align: right">
                                                Location</td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DDLLocation" runat="server" Width="150px">
                                                </asp:DropDownList>
                                            </td>
                                            
                                           <td>
                                                Department</td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DDLDepartment" runat="server" Width="150px">
                                                </asp:DropDownList>
                                            </td>
                                           <td>
                                                Position</td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                 <asp:DropDownList ID="DDLPosition" runat="server" Width="150px">
                                                </asp:DropDownList>
                                            </td>--%>
                                             <tr>
                                            <td style="text-align: right">
                                                Offer Date from:</td>
                                         
                                            <td>
                                                <asp:TextBox ID="TxtFromdate" Width="100px" CssClass="textbox SmallFont" runat="server"></asp:TextBox></td>
                                            
                                           <td>
                                                Offer Date To:</td>
                                           
                                            <td>
                                                <asp:TextBox ID="TxtToDate" Width="100px" CssClass="textbox SmallFont" runat="server"></asp:TextBox></td>
                                          <td style="text-align: right">
                                                Joining Date From:</td>
                                            
                                            <td>
                                              <asp:TextBox ID="TxtOnFrom" Width="100px" CssClass="textbox SmallFont" runat="server"></asp:TextBox></td>
                                            
                                            <td>
                                               Joining Date To:</td>
                                          
                                            <td>
                                                <asp:TextBox ID="TxtOnTo" Width="100px" CssClass="textbox SmallFont" runat="server" ></asp:TextBox></td>
                                         </tr>
                                             <tr>
                                            <td style="text-align: right">
                                                Location</td>
                                         
                                            <td>
                                                <asp:DropDownList ID="DDLLocation"  runat="server" Width="105px" CssClass="SmallFont">
                                                </asp:DropDownList></td>
                                            
                                           <td>
                                                Department</td>
                                           
                                            <td>
                                                <asp:DropDownList ID="DDLDepartment" runat="server" Width="105px" CssClass="SmallFont">
                                                </asp:DropDownList></td>
                                          <td style="text-align: right">
                                               Position</td>
                                            
                                            <td> <asp:DropDownList ID="DDLPosition" runat="server" Width="105px" CssClass="SmallFont">
                                                </asp:DropDownList></td>
                                           <td>
                                                <asp:Button ID="Button1" runat="server" Height="22px" OnClick="btngetrecord_Click1"
                                        Text="Get Record" Width="85px" /></td>
                                         </tr>
                                           <table>
                                            <%--     <tr>
                                            <td style="text-align: right">
                                                Location</td>
                                            <td>
                                          
                                                <asp:DropDownList ID="DDLLocation" runat="server" Width="150px" CssClass="SmallFont">
                                                </asp:DropDownList></td>
                                             <td>
                                                Department
                                                </td>
                                                <td>
                                                <asp:DropDownList ID="DDLDepartment" runat="server" Width="150px" CssClass="SmallFont BoldFont UPPERCASE">
                                                </asp:DropDownList></td>
                                           <td>
                                                Position
                                                </td>
                                                <td>
                                                 <asp:DropDownList ID="DDLPosition" runat="server" Width="150px" CssClass="SmallFont BoldFont UPPERCASE">
                                                </asp:DropDownList></td>
                                            <td>
                                           <asp:Button ID="Button2" runat="server" Height="22px" OnClick="btngetrecord_Click1"
                                        Text="Get Record" Width="85px" /></td>
                                          
                                            </tr>--%>
            <%--   <tr>
                   <td align="right" class="style4">
                        Offer Date from:
                        <asp:TextBox ID="TxtFromdate" Width="100px" CssClass="textbox" runat="server"></asp:TextBox>
                    </td>
                    <td align="left" class="style4">
                        <asp:TextBox ID="TxtFromdate" Width="100px" CssClass="textbox" runat="server"></asp:TextBox>
                         </td>
                    <td style = "text-align:left;" align="right" class="style5">
                        Offer Date To:
                    </td>
                     <td align="left" class="style1">
                    <asp:TextBox ID="TxtToDate" Width="100px" CssClass="textbox" runat="server"></asp:TextBox>
                        </td></tr>
                <tr>
                    <td align="right" class="style3">
                      Joining Date From:
                    </td>
                    <td align="left" class="style3">
                        <asp:TextBox ID="TxtOnFrom" Width="100px" CssClass="textbox" runat="server"></asp:TextBox>
                        <td class="style5" />
                    <td style="width: 5%; text-align: left;" align="right">
                        Joining Date To:
                    </td>
                    <td style="width: 20%" align="left">
                        <asp:TextBox ID="TxtOnTo" Width="100px" CssClass="textbox" runat="server" ></asp:TextBox>
                        <asp:Button ID="Button1" runat="server" Height="22px" OnClick="btngetrecord_Click1"
                                        Text="Get Record" Width="85px" />
                                        </td>
                       <td align="right" valign="top" width="10%">
                                    <asp:Button ID="btngetrecord" runat="server" Height="22px" OnClick="btngetrecord_Click1"
                                        Text="Get Record" Width="85px" />
                                </td>
                                            
                                        </tr>--%>
                                          <table width="100%">
                        <tr>
                            <td align="left" width="50%">
                                <b>
                                    <asp:Label ID="Label1" runat="server" Text="Total Record : " CssClass="Label"></asp:Label>
                                    <asp:Label ID="lblTotalRecord" runat="server" CssClass="Label"></asp:Label></b>
                            </td>
                            <td align="left" valign="top" width="50%" cssclass="Label">
                                <b>
                                    <asp:UpdateProgress ID="UpdateProgress9" runat="server">
                                        <ProgressTemplate>
                                            Loading...</ProgressTemplate>
                                    </asp:UpdateProgress>
                                </b>
                            </td>
                        </tr>
                    </table>
                                           <table width="100%">
                        <asp:GridView ID="HiringQueryDetail" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="False" BorderStyle="Ridge" CellPadding="3" CssClass="smallfont"
                            EmptyDataText="No Record Found" Font-Size="X-Small" ForeColor="#333333" OnPageIndexChanging="HiringQueryDetail_PageIndexChanging"
                            PagerStyle-HorizontalAlign="Left" PageSize="20" Width="100%">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                            <Columns>
                                <asp:BoundField DataField="BRANCH_NAME" HeaderText="Location" />
                                <asp:BoundField DataField="DEPT_NAME" HeaderText="Department" />
                                <asp:BoundField DataField="POSITION_NAME" HeaderText="Position" />
                                <asp:BoundField DataField="OFFER_DATE" HeaderText="Offer Date" />
                                <asp:BoundField DataField="CANDJOIN" HeaderText="Joining Date" />
                                <asp:BoundField DataField="NOV" HeaderText="Nature of Vacancies" />
                                 <asp:BoundField DataField="NPNAME" HeaderText="Candidate Name" />
                            </Columns>
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </table>
                                        
                                  </table>
                                </td>
                            </tr>                            
                     </tbody>
                     <tr>
                        <td>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupPosition="TopLeft"
                        TargetControlID="txtFromDate">
                    </cc1:CalendarExtender>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupPosition="TopLeft"
                        TargetControlID="txtToDate">
                    </cc1:CalendarExtender>
                </td>
            </tr>
             <tr>
                        <td>
                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" PopupPosition="TopLeft"
                        TargetControlID="txtOnFrom">
                    </cc1:CalendarExtender>
                    <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" PopupPosition="TopLeft"
                        TargetControlID="txtOnTo">
                    </cc1:CalendarExtender>
                </td>
            </tr>
             </table>
        </td>
    </tr>
</table>
<%-- </ContentTemplate>
</asp:UpdatePanel>--%>