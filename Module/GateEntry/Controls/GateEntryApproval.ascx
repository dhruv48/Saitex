<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GateEntryApproval.ascx.cs" Inherits="Module_GateEntry_Controls_GateEntryApproval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<link href="../../../StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
<link href="../../../StyleSheet/style.css" rel="stylesheet" type="text/css" />



<table align="left" border="0" cellpadding="0" cellspacing="0"  class="tContentArial">
   
    <tr>
        <td align="left" valign="top" class="td" width="100%">
                <table align="left">
                    <tr>
                        <td id="tdUpdate" runat="server" align="left">
                            <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click1"></asp:ImageButton>
                        </td>
                        
                        <td id="tdFind" runat="server" visible="false" align="left">
                            <asp:ImageButton ID="imgbtnFindTop" Width="48" Height="41" runat="server" ToolTip="Find"
                                ImageUrl="~/CommonImages/link_find.png"></asp:ImageButton>
                        </td>
                        <td>
                            <asp:ImageButton ID="imgbtnPrint" runat="server" Width="48" Height="41" ToolTip="Print"
                                ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click"></asp:ImageButton>&nbsp;
                        </td>
                        <td>
                            <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" ToolTip="Clear"
                                ImageUrl="~/CommonImages/clear.jpg" OnClick="imgbtnClear_Click"></asp:ImageButton>
                        </td>
                        <td>
                            <asp:ImageButton ID="imgbtnExit" runat="server" Width="48" Height="41" ToolTip="Exit"
                                ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click"></asp:ImageButton>
                        </td>
                        <td>
                            <asp:ImageButton ID="imgbtnHelp" runat="server" Width="48" Height="41" ToolTip="Help"
                                ImageUrl="~/CommonImages/link_help.png" OnClick="imgbtnHelp_Click"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader" colspan="3">
            <b class="titleheading">Gate Entry Approval</b>
        </td>
    </tr>
    <tr>
        <td align="left" colspan="3" valign="top">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>
            &nbsp;Mode </span>
        </td>
    </tr>
    <tr>
        <td align="center" colspan="3">
            <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
        </td>
    </tr>
    
     <tr>
    <td > 
    Item&nbsp;Type&nbsp;:&nbsp;&nbsp;&nbsp;  <asp:DropDownList ID="ddlItemType" 
            runat="server" CssClass="gCtrTxt " Font-Size="8" AutoPostBack="true"
                    Width="160px" 
            onselectedindexchanged="ddlItemType_SelectedIndexChanged">
                </asp:DropDownList>
    </td>
    
    <td>
    </td>
    <td>
    </td>
    </tr>
    
    <tr>
        <td align="left" colspan="3">
            <b>Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
            </b>
        </td>
    </tr>
   
    <tr>
        <td align="left" colspan="3">
            <asp:GridView ID="gvGateEntryApproval" runat="server" AllowSorting="True"  AllowPaging="true"
                AutoGenerateColumns="False" BorderWidth="2px" 
                OnPageIndexChanging="gvGateEntryApproval_Paging" PageSize="12"   >
                <Columns>                 
                   
                    <asp:TemplateField HeaderText="Gate No">
                        <ItemTemplate>
                             <asp:Label ID="lblGATE_NUMB" runat="server" Text='<%# Bind("GATE_NUMB") %>' ToolTip='<%# Bind("GATE_TYPE") %>'
                               ></asp:Label>
                            <asp:Label ID="lblITEM_TYPE" runat="server" Text='<%# Bind("ITEM_TYPE") %>'  ToolTip='<%# Bind("MATERIAL_DTL") %>' Visible="false"></asp:Label>
                             
                        </ItemTemplate>
                        <ItemStyle CssClass="Label SmallFont" HorizontalAlign="Right" 
                            VerticalAlign="Top" Width="40px" />
                    </asp:TemplateField>                   
                     <asp:BoundField DataField="ITEM_TYPE" HeaderText="Item Type" >
                    <ItemStyle CssClass="Label SmallFont" HorizontalAlign="Left" VerticalAlign="Top" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="GATE_DATE" DataFormatString="{0:dd/MM/yyyy}" 
                        HeaderText="Gate Date" HtmlEncode="False">
                      
                        <ItemStyle CssClass="Label SmallFont" HorizontalAlign="Left" 
                            VerticalAlign="Top" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PRTY_NAME" HeaderText="Party">
                        <ItemStyle CssClass="Label SmallFont" HorizontalAlign="Left" 
                            VerticalAlign="Top" Width="120px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TRSP_NAME" HeaderText="Transporter">
                        <ItemStyle CssClass="Label SmallFont" HorizontalAlign="Left" 
                            VerticalAlign="Top" Width="120px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MATERIAL_DTL" HeaderText="Details">
                        <ItemStyle CssClass="LabelNo SmallFont" HorizontalAlign="Right" 
                            VerticalAlign="Top" Width="40px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="QTY" HeaderText="No Of Item">
                        <ItemStyle CssClass="LabelNo SmallFont" HorizontalAlign="Right" 
                            VerticalAlign="Top" Width="40px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="UOM" HeaderText="UOM">
                        <ItemStyle CssClass="LabelNo SmallFont" HorizontalAlign="Right" 
                            VerticalAlign="Top" Width="40px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SECURITY_ENCHARGE" HeaderText="Security Encharge">
                        <ItemStyle CssClass="LabelNo SmallFont" HorizontalAlign="Right" 
                            VerticalAlign="Top" Width="50px" />
                    </asp:BoundField>
                         <asp:BoundField DataField="CHECK_BY" HeaderText="Checked By">
                            <ItemStyle HorizontalAlign="Right" CssClass="LabelNo SmallFont" VerticalAlign="Top"
                                Width="50px" />
                        </asp:BoundField>
                         <asp:BoundField DataField="REMARKS" HeaderText="Remark">
                            <ItemStyle HorizontalAlign="Right" CssClass="LabelNo SmallFont" VerticalAlign="Top"
                                Width="50px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Confirm">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkApproved" runat="server" />
                            </ItemTemplate>
                            <ItemStyle CssClass="Label SmallFont" HorizontalAlign="Center" 
                                VerticalAlign="Top" Width="50px" />
                    </asp:TemplateField>                    
                    <asp:TemplateField HeaderText="Confirm Date">
                        <ItemTemplate>
                            <asp:TextBox ID="txtConfirmDate" runat="server" CssClass="Label SmallFont TextBoxDisplay"  ReadOnly="true"
                                Text='<%# Bind("APPROVED_DATE") %>' Width="50px"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
                                targetcontrolid="txtConfirmDate">
                            </cc1:CalendarExtender>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Confirm By">
                        <ItemTemplate>
                            <asp:TextBox ID="txtConfirmBy" runat="server" CssClass="TextBox SmallFont" 
                                Text='<%# Bind("APPROVED_BY") %>' Width="70px"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks">
                        <ItemTemplate>
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="TextBox SmallFont" 
                                Width="100px"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px" />
                    </asp:TemplateField>
                   
                </Columns>
                <PagerStyle HorizontalAlign="Left" />
                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
            </asp:GridView>
        </td>
    </tr>
</table>