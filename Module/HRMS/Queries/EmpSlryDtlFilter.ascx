<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmpSlryDtlFilter.ascx.cs" Inherits="Module_HRMS_Queries_EmpSlryDtlFilter" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="obout_Interface" namespace="Obout.Interface" tagprefix="cc2" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="Combo" %>



<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
<table id="tblDesgMainTable" runat="server" cellspacing="0" cellpadding="0" align="Left" class="tContentArial">
    <tr>
        <td align="Right" class="td">
            <table align="left">
                <tr>                                         
                   
                                 
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                            Width="48" Height="41" onclick="imgbtnClear_Click" ></asp:ImageButton>
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
       
   
   <td class="TableHeader td" align="center" colspan = "10">
            <span class="titleheading">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Employee Salarly Detail &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
        </td>
   
    </tr>
      
    <tr>
        <td align="left" valign="top" class ="td">
            &nbsp;</td>
    </tr>     
    <tr>
        <td class ="td">
            <table border="0" cellpadding="3" cellspacing="0" align="left" width="750" >
                   <tr>
                        <td>Employee:</td>
                        <td>
                            
                            <asp:DropDownList ID="ddlemp" runat="server" Width="160px" CssClass="tContentArial" >
                            </asp:DropDownList>
                        </td>
                        <td></td>
                        <td>Department:</td>
                        <td>
                           
                            <asp:DropDownList ID="DDLDept" runat="server" Width="160px" CssClass="tContentArial" >
                            </asp:DropDownList>
                        </td>
                   </tr>
                   <tr>
                        <td>Designation:</td>
                        <td>
                            
                            <asp:DropDownList ID="DDLDesign" runat="server" Width="160px" CssClass="tContentArial" >
                            </asp:DropDownList>
                        </td>
                        <td></td>
                        <td>Branch:</td>
                        <td>
                          
                            <asp:DropDownList ID="DDLBranch" runat="server" Width="160px" CssClass="tContentArial" >
                            </asp:DropDownList>
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