<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShiftMaster.ascx.cs" Inherits="Module_HRMS_Controls_WebUserControl" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc1" %>
<table class="tContentArial">
    <tr>
        <td class="td">
            <table>
                <tr>
                    <td id="tdSave" runat="server">
                        <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="Save" ImageUrl="~/CommonImages/save.jpg"
                            Width="55px" Height="40px" ValidationGroup="M1" OnClick="imgbtnSave_Click"></asp:ImageButton>
                    </td>
                    <td id="tdUpdate" runat="server">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                            Width="48" Height="41" ValidationGroup="M1" onclick="imgbtnUpdate_Click1"></asp:ImageButton>
                    </td>
                     <td id="tdFind" runat="server" valign="top">
                        <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/link_find.png"
                            Width="48" Height="41" TabIndex="7" OnClick="imgbtnFind_Click"></asp:ImageButton>
                    </td>
                    <td id="tdClear" runat="server">
                        <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                            Width="48" Height="41" 
                            OnClientClick="javascript:return window.confirm('Are you sure you want to clear this record')" 
                            onclick="imgbtnClear_Click1">
                        </asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                            Width="48" Height="41" onclick="imgbtnPrint_Click1" ></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                            Width="48" Height="41" OnClick="imgbtnExit_Click"></asp:ImageButton>
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
        <td align="center" valign="top" class="tRowColorAdmin td">
            <span class="titleheading">Shift Master</span>
        </td>
    </tr>
    <tr>
        <td class="style1">
            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                ShowSummary="False" ValidationGroup="M1" />
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>
    <tr>
        <td class="td">
            <table width="100%">
                   <tr>
                        <td align="right" width="20%">
                            Shift Name :
                        </td>                     
                        <td align="left" valign="top" width="30%">
                               <cc1:ComboBox ID="cmbShiftName" runat="server" Width="190px" Height="150px" DataTextField="SFT_NAME"
                                DataValueField="SFT_ID" EnableLoadOnDemand="True"  EmptyText="Find Shift"
                                   OnLoadingItems="cmbShiftName_LoadingItems" 
                                   onselectedindexchanged="cmbShiftName_SelectedIndexChanged" 
                                   AutoPostBack="True">
                                <FooterTemplate>
                                    Displaying items
                                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                    out of
                                    <%# Container.ItemsCount %>.
                                </FooterTemplate>
                                <ItemTemplate>
                                    <div class="item c2">
                                        <%# Eval("SFT_NAME")%></div>
                                </ItemTemplate>
                                <HeaderTemplate>                                   
                                    <div class="header c2">
                                        Shift Name</div>
                                </HeaderTemplate>
                            </cc1:ComboBox>
                            <asp:Button ID="btnAddShiftName" runat="server" CssClass="gCtrTxt" Text="AddNew" Font-Size="10pt" OnClick="btnAddShiftName_Click"
                                Width="70px" />
                            <asp:TextBox ID="txtShiftName" runat="server" Width="70px" 
                                   CssClass="UpperCase  gCtrTxt" TabIndex="1"></asp:TextBox>
                            <asp:LinkButton ID="lnkSave" runat="server" Text="Save" OnClick="lnkSave_Click"></asp:LinkButton>&nbsp;
                            <asp:LinkButton ID="lnkBack" runat="server" Text="Back" OnClick="lnkBack_Click"></asp:LinkButton>&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="EnterShiftName"
                                ControlToValidate="cmbShiftName" ValidationGroup="M1"></asp:RequiredFieldValidator>
                        </td>
                        <td align="right" width="20%">
                            Shift Code :
                        </td>
                       
                        <td align="left" width="30%" >
                            <asp:TextBox ID="txtShiftCode" CssClass="gCtrTxt" runat="server" Width="70px" TabIndex="10"></asp:TextBox>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right"  valign="top"  width="20%">
                            In Time :
                        </td>
                       
                        <td align="left"  valign="top" width="30%" >
                            <asp:TextBox ID="txtInTime" runat="server" CssClass="gCtrTxt" Width="70px" onKeyPress="return checkNumeric(event)" onblur="return MaskTimeFormat(this)" MaxLength="4" ToolTip="Time Format like (09:00)" TabIndex="2"></asp:TextBox>
                        </td>
                        <td align="right"  valign="top" width="20%">
                            Out Time :
                        </td>
                       
                        <td align="left"  valign="top" width="30%" >
                            <asp:TextBox ID="txtOutTime" runat="server" CssClass="gCtrTxt" Width="70px" 
                                onKeyPress="return checkNumeric(event)" onblur="return MaskTimeFormat(this)" 
                                MaxLength="4"  ToolTip="Time Format (18:00 for 06:00)" TabIndex="3"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top" width="20%">
                            Relaxation Time :
                        </td>                    
                        <td align="left"  valign="top" width="30%">
                            <asp:TextBox ID="txtRelaxation" runat="server" CssClass="gCtrTxt textboxno" 
                                onKeyPress="return checkNumeric(event)" MaxLength="4" Width="70px" 
                                ValidationGroup="M1"  ToolTip="Time Format(30)" TabIndex="4"></asp:TextBox>
                            Min                           
                        </td>
                        <td align="right"  valign="top"  width="20%">
                            Lunch Time :
                        </td>
                      
                        <td align="left"  valign="top" width="30%">
                            <asp:TextBox ID="txtLunchTime" runat="server" CssClass="gCtrTxt textboxno" 
                                onKeyPress="return checkNumeric(event)" MaxLength="4" Width="70px" ValidationGroup="M1"
                                ToolTip="Time Format(30)" TabIndex="5"></asp:TextBox>Min   
                        </td>
                    </tr>
                    <tr>
                        <td align="right"  valign="top" width="20%">
                            Over Time After :
                        </td>
                       
                        <td align="left"  valign="top" width="30%">
                            <asp:TextBox ID="txtOverTime" runat="server" CssClass="gCtrTxt" 
                                onKeyPress="return checkNumeric(event)" onblur="return MaskTimeFormat(this)" 
                                MaxLength="4" Width="70px" ValidationGroup="M1"
                                ToolTip="Time Format(18.00)" TabIndex="6"></asp:TextBox>Hour                             
                        </td>
                        <td align="right"  valign="top" width="20%">
                            Min Working Hour :
                        </td>
                       
                        <td align="left"  valign="top" width="30%">
                            <asp:TextBox ID="txtMinWorkingHour" runat="server" 
                                onKeyPress="return checkNumeric(event)" onblur="return MaskTimeFormat(this)" 
                                MaxLength="4" CssClass="gCtrTxt" Width="70px" ToolTip="Time Format(08.00)" 
                                TabIndex="7"></asp:TextBox>Hour                           
                        </td>
                    </tr>
                    <tr>
                        <td align="right"  valign="top" width="20%">
                            Min working hour to consider to be present :
                        </td>
                      
                        <td align="left" valign="top" width="30%x">
                            <asp:TextBox ID="txtHourstobe_Hoilday" TabIndex="8" runat="server" onKeyPress="return checkNumeric(event)" onblur="return MaskTimeFormat(this)" MaxLength="4" CssClass="gCtrTxt" Width="70px" ToolTip="Enter Field(Time Format 8.00)"></asp:TextBox>
                            Hour                                                     
                        </td>
                        <td align="right" valign="top" width="20%">
                            Min working hour to be consider as full day :
                        </td>
                      
                        <td align="left" valign="top" width="30%">
                            <asp:TextBox ID="txthourstobe_Shortleave" runat="server" onKeyPress="return checkNumeric(event)" onblur="return MaskTimeFormat(this)" MaxLength="4" CssClass="gCtrTxt" Width="70px" TabIndex="9" ValidationGroup="M1" ToolTip="Time Format(08:00)"></asp:TextBox>
                            Hour                             
                        </td>
                    </tr>
                    <tr>
                       
                        <td align="right" valign="top" width="20%">
                            Status 
                        </td>
                        
                        <td align="left"  valign="top" valign="top" width="80%" colspan="3">
                          <asp:CheckBox ID="chkActive"
                                runat="server" TabIndex="10" /> 
                        </td>
                    </tr>  
                    <tr>
                        <td style="text-align:center; color: #008000;" colspan="4">***Please Enter Time in 24 Hours Format***</td>
                    </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="td">
            <table>
                <td align="left">
                    <cc2:Grid ID="Grid1" runat="server" AllowAddingRecords="False" 
                        PageSize="5" AutoGenerateColumns="False" OnSelect="Grid1_Select" AutoPostBackOnSelect="True">
                        <Columns>
                            <cc2:Column DataField="SFT_ID" HeaderText="SFT_ID" Visible="false" Width="1px">
                            </cc2:Column>
                            <cc2:Column DataField="SFT_NAME" HeaderText="Shift Name" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="SFT_IN_TIME" HeaderText="In Time" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="SFT_OUT_TIME" HeaderText="Out Time" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="SFT_RLX_TIME" HeaderText="Relax Time" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="SFT_LNCH_TIME" HeaderText="Lunch Time" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="SFT_OVR_TIME" HeaderText="Over Time" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="SFT_MIN_WRK_HOUR" HeaderText="Min Working Hour" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="SFT_MIN_HLD_HOUR" HeaderText="Min Holiday Hour" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="SFT_MIN_SHORT_DAY_HOUR" HeaderText="MinShortDayHour" Width="100px">
                            </cc2:Column>
                        </Columns>
                    </cc2:Grid>
                </td>
            </table>
        </td>
    </tr>
</table>