<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StopRunningMachine.ascx.cs"
    Inherits="Module_OrderDevelopment_Controls_StopRunningMachine" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<table width="100%" align="left" class="tContentArial">
    <tr>
        <td align="left" valign="top" class="td" width="100%">
            <table align="left">
                <tr>
                    <td id="tdUpdate" runat="server" align="left">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                            Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click"></asp:ImageButton>
                    </td>
                    <td id="tdFind" runat="server" visible="false" align="left">
                        <asp:ImageButton ID="imgbtnFindTop" Width="48" Height="41" runat="server" ToolTip="Find"
                            ImageUrl="~/CommonImages/link_find.png" OnClick="imgbtnFindTop_Click"></asp:ImageButton>
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
        <td align="center" class="TableHeader td" width="100%">
            <b class="titleheading">
                <asp:Label ID="lblFormHeading" runat="server" CssClass="SmallFont">Machine Stop After Production</asp:Label></b>
        </td>
    </tr>
    <tr>
        <td align="left" valign="top" width="100%" class="td">
            <span class="Mode">You are in&nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode</span></td>
    </tr>
    <tr>
        <td align="left" class="td" width="100%">
            <b>Total Machines&nbsp;:&nbsp;<asp:Label ID="lblTotalRecord" runat="server"></asp:Label></b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             <b>Running Machines&nbsp;:&nbsp;<asp:Label ID="lblRunningMachine" runat="server" ForeColor=Green></asp:Label></b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <b>Idle Machines&nbsp;:&nbsp;<asp:Label ID="lblIdleMachine" runat="server" ForeColor=Red></asp:Label></b>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" width="100%">
            <asp:Panel ID="pnlLotMov" runat="server" Width="100%"  ScrollBars="Both">
                <asp:GridView ID="grdMachine" CssClass="SmallFont" runat="server" AllowSorting="True"
                    AutoGenerateColumns="False" OnRowDataBound="grdMachine_RowDataBound" Width="98%">
                    <Columns>
                       
                        
                        <asp:TemplateField HeaderText="Group" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblMachineGroup" runat="server" Text='<%# Bind("MACHINE_GROUP") %>'></asp:Label>                             
                            </ItemTemplate>                           
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Machine Name" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblMachineCode" runat="server" Text='<%# Bind("MACHINE_CODE") %>'></asp:Label>
                            </ItemTemplate>
                           
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Last Unload" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblLastUnload" runat="server" Text='<%# Bind("LAST_UNLOAD","{0:dd/MM/yyyy hh:mm:ss tt}") %>'></asp:Label>
                            </ItemTemplate>                          
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Running Lot No" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblRunningLotNo" runat="server" Text='<%# Bind("RUNNING_LOT_NO") %>'></asp:Label>
                            </ItemTemplate>                            
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Manchine Status" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblMachineStatus" runat="server" Text='<%# Bind("MACHINE_STATUS") %>'></asp:Label>
                            </ItemTemplate>                          
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Machine Stop Date" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                 <asp:TextBox ID="txtLoadingDate" runat="server"  
                            CssClass="SmallFont" Font-Size="XX-Small" Width="150px"></asp:TextBox>
                        <cc1:MaskedEditExtender TargetControlID="txtLoadingDate" ID="meeLoadingDate" runat="server"
                            Mask="99/99/9999 99:99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError" MaskType="DateTime" CultureName="en-CA" AcceptAMPM="True" />
                               <cc1:CalendarExtender ID="CE1" runat="server" Format="dd/MM/yyyy hh:mm:ss tt" TargetControlID="txtLoadingDate">
                        </cc1:CalendarExtender>
                            </ItemTemplate>
                            <ItemStyle CssClass="SmallFont tdLeft" />
                        </asp:TemplateField>
                     
                    </Columns>
                    
                    <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                </asp:GridView>
            </asp:Panel>
        </td>
    </tr>
</table>
