<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Promotion.ascx.cs" Inherits="Module_HRMS_Controls_Promotion" %>
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
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                            Width="48" Height="41" onclick="imgbtnPrint_Click" ></asp:ImageButton>
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
            <span class="titleheading">Promotion & Increment Report</span>
        </td>
    </tr>
      
    <tr>
        <td align="left" valign="top" class ="td">
            &nbsp;</td>
    </tr>     
    <tr>
        <td class ="td">
            <table border="0" cellpadding="3" cellspacing="0" align="left" width="750">
                   <tr>
                        <td>Employee:</td>
                        <td>
                            <Combo:ComboBox ID="cmbEmpCode" runat="server" Width="200px" 
                                EmptyText="------------SELECT------------" Height="150px" AutoPostBack="true" 
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
                        <td></td>
                        <td>Department:</td>
                        <td>
                            <cc2:OboutDropDownList ID="DDLDept" Height="150px" runat="server" 
                                Width="200px" >                             
                            </cc2:OboutDropDownList>
                        </td>
                   </tr>
                   <tr>
                        <td>Designation:</td>
                        <td>
                            <cc2:OboutDropDownList ID="DDLDesign" Height="150px" runat="server" 
                                Width="200px" >
                            </cc2:OboutDropDownList>
                        </td>
                        <td></td>
                        <td>Branch:</td>
                        <td>
                            <cc2:OboutDropDownList ID="DDLBranch" Height="150px" runat="server" 
                                Width="200px" >
                            </cc2:OboutDropDownList>
                        </td>
                   </tr>
                    <tr>
                        <td colspan="5" align="center" >
                       <%--      <asp:Button ID="CMDPrint" Text="Print" CssClass="AButton" runat="server"    
                                Width="175" ValidationGroup="M1" onclick="CMDPrint_Click"  /> --%>
                        </td>                       
                   </tr>
                </table>
        </td>
    </tr>
  </table>
  </ContentTemplate>                   
                </asp:UpdatePanel>