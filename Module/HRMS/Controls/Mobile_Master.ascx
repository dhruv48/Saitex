<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Mobile_Master.ascx.cs" Inherits="Module_HRMS_Controls_Mobile_Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
     <cc1:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy"  TargetControlID="TxtPurchageDate" runat="server"></cc1:CalendarExtender>
<table class="tContentArial" width="80%">
<tr>        <td  class="tdRight" colspan = "4">
            <table align="left">
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
                                    <asp:ImageButton ID="imgbtnHelp"  runat="server" ImageUrl="~/CommonImages/link_help.png"
                                        ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                                </td>
                            </tr>
                        </table>
        </td>
    </tr>
    <tr>
        <td class="TableHeader td" align="center" colspan = "4">
            <span class="titleheading">TELEPHONE MASTER</span>
            </td>
    </tr>
    <tr>
        <td align="left" valign="top" class="td" colspan="4">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>
   <tr id="trFindingRecord" runat="server" >
        <td valign="top" class="td" align="center"  colspan="4">
            <table width="100%">
                  <tr>                  
                    <td class="tdRight" style="width:30%">Telephone/Mobile No:</td>
                    <td class="tdLeft" style="width:20%"><asp:TextBox ID="TxtTelephoneNo" Width="150px" MaxLength="12" CssClass="SmallFont TextBoxNo" onKeyUp="filterNonNumeric(this)" runat="server"></asp:TextBox></td> 
                    <td class="tdRight" style="width:30%">Service Provider:</td>
                    <td class="tdLeft" style="width:20%"><asp:DropDownList ID="DDLServiceprovider" CssClass="SmallFont" Width="102px" runat="server">
                        <asp:ListItem>Airtel</asp:ListItem>
                        <asp:ListItem>Aircel</asp:ListItem>
                        <asp:ListItem>BSNL</asp:ListItem>
                        <asp:ListItem>Idea</asp:ListItem>
                        <asp:ListItem>MTS</asp:ListItem>
                        <asp:ListItem>Reliance</asp:ListItem>
                        <asp:ListItem>TATA</asp:ListItem>
                        <asp:ListItem>VodaFone</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                   
                  </tr>
                   <tr>
                     <td class="tdRight" style="width:30%">Purchage Date:</td>
                    <td class="tdLeft" style="width:20%"><asp:TextBox ID="TxtPurchageDate" Width="150px" MaxLength="10" CssClass="SmallFont TextBoxNo"  runat="server"></asp:TextBox></td>
                    <td class="tdRight" style="width:30%">Area:</td>
                    <td class="tdLeft" style="width:20%"><asp:TextBox ID="TxtArea" CssClass="TextBox SmallFont" Width="150px" runat="server"></asp:TextBox></td>
                   
                  </tr>
            </table>
        </td>
   </tr>
   
<tr><td colspan="4" class ="td">Telephone Record :-</td></tr>
<tr>
<td colspan="4">
    <asp:GridView ID="GVTelephoneRecord" runat="server" AutoGenerateColumns="False" 
        AllowPaging="True" AllowSorting="True" Font-Size="X-Small" CellPadding="3"   
        GridLines="None" Width="100%" ForeColor="#333333" 
        CssClass = "smallfont" 
        onpageindexchanging="GVTelephoneRecord_PageIndexChanging" 
        EmptyDataText="There is no record found" 
        onrowcommand="GVTelephoneRecord_RowCommand" >       
        <FooterStyle Width="100%" BackColor="#507CD1"  ForeColor="White" Font-Bold="True" />
        <RowStyle BackColor="#EFF3FB" />
        <EmptyDataRowStyle Font-Bold="True" Font-Names="Annabel Script" 
            Font-Size="Medium" />
        <Columns>
            <asp:TemplateField HeaderText="S.No." ItemStyle-VerticalAlign="top">
                  <ItemTemplate><%#Container.DataItemIndex+1 %>
                  </ItemTemplate>
                  <ItemStyle VerticalAlign="Top" HorizontalAlign="Center" Width="3%" />
                  <HeaderStyle VerticalAlign="Top" HorizontalAlign="Center" />
             </asp:TemplateField>         
            <asp:BoundField DataField="TELEPHONE_NO" HeaderText="TELEPHONE NO" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="AREA" HeaderText="AREA" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="SERVICE_PROVIDER" HeaderText="SERVICE PROVIDER" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
             <asp:BoundField DataField="PURCHAGE_DATE" HeaderText="PURCHAGE DATE" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="ALLOT_STATUS" HtmlEncode="false"  HeaderText="STATUS" > 
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
           <asp:TemplateField HeaderText="Delete">
                <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                     <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="EmpEdit"  TabIndex="12" CommandArgument='<%# Eval("TELEPHONE_NO") %>'></asp:LinkButton>&nbsp;/&nbsp;
                            <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CommandName="EmpDelete" TabIndex="12" OnClientClick="javascript: return confirm('Are you sure you want to delete this record?')" CommandArgument='<%# Eval("TELEPHONE_NO") %>'></asp:LinkButton>
                     </ItemTemplate>
          </asp:TemplateField>         
        </Columns>
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle HorizontalAlign="Justify"  VerticalAlign="Top" />
    </asp:GridView>
    
    </td>
  </tr>

</table>
</ContentTemplate>
</asp:UpdatePanel>