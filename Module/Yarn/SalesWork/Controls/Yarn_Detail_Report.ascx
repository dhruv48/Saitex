<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Yarn_Detail_Report.ascx.cs"
    Inherits="Module_Yarn_SalesWork_Controls_Yarn_Detail_Report" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
    
    <asp:UpdatePanel ID="uppnl" runat="server">
    <ContentTemplate>
    
<table align="left" class=" td tContentArial" width="100%">
    <tr>
        <td class="td">
            <table>
                <tr>
                    <td>
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                            OnClick="imgbtnPrint_Click"></asp:ImageButton>
                    </td>  <td>  
<asp:ImageButton ID="imgBtnExportExcel" runat="server" Width="48" Height="41" ToolTip="Print"
                            ImageUrl="~/CommonImages/export.png" onclick="imgBtnExportExcel_Click" ></asp:ImageButton>&nbsp;</td> 

                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                            OnClick="imgbtnClear_Click"></asp:ImageButton>
                    </td>
                  
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                            ToolTip="Exit" Width="48" OnClick="imgbtnExit_Click" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                            ToolTip="Help" Width="48" />
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td align="center" class="TableHeader td" colspan="8">
                        <span class="titleheading"><strong>Yarn Detail Stock Report</strong> </span>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Branch:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="gCtrTxt " Font-Size="9"
                            Width="160px">
                        </asp:DropDownList>
                    </td>
                            <td align="right">
                                Year:
                            </td>
                            <td>
                                <%--<asp:DropDownList ID="ddlDepartment" runat="server" CssClass="gCtrTxt " 
                Font-Size="9" Width="160px">--%>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="gCtrTxt " Font-Size="9" 
                                    Width="160px" AutoPostBack="True" 
                                    onselectedindexchanged="ddlYear_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                From Date:
                            </td>
                            <td>
                                <asp:TextBox ID="TxtFromDate" runat="server" CssClass="SmallFont TextBox UpperCase"
                                     Width="150px"></asp:TextBox><%--OnTextChanged="TxtFromDate_TextChanged"--%>
                            </td>
                            <td align="right">
                                To Date:
                            </td>
                            <td>
                                <asp:TextBox ID="TxtToDate" runat="server" CssClass="SmallFont TextBox UpperCase"
                                    Width="150px" ></asp:TextBox>  <%--OnTextChanged="TxtToDate_TextChanged"--%>
                            </td>
                 
                  
                    
                   
                </tr>
                <tr>
                  <td class="tdRight">
                        Yarn Type :
                    </td>
                    <td class="tdLeft">
                        <asp:DropDownList ID="ddlyarntype" runat="server" CssClass="gCtrTxt"
                            Width="160px">
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight">
                        Party :
                    </td>
                    <td class="tdLeft">
                        <asp:DropDownList ID="ddlpartycode" runat="server" CssClass="gCtrTxt"
                            Width="160px">
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight">
                        Shade Code :
                    </td>
                    <td align="left" valign="top" class="style8">
                        <asp:DropDownList ID="ddlShadeCode" runat="server" CssClass="gCtrTxt" Width="160px">
                        </asp:DropDownList>
                    </td>
                <td style="text-align: right"  >
             Article Code :</td>
           <td >
               <asp:TextBox ID="TxtYarnCode" runat="server" Width="158px"></asp:TextBox>
           </td>
       
                </tr>
                <tr>
                    <td class="tdRight">
                        Yarn Catagory :
                     </td>
                    <td class="tdLeft">
                        <asp:DropDownList ID="ddlyarncat" runat="server" CssClass="gCtrTxt"
                            Width="160px">
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight">
                        Location :
                     </td>
                    <td class="tdLeft">
                        <asp:DropDownList ID="ddllocation" runat="server" CssClass="gCtrTxt"
                            Width="160px">
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight">
                        Store :
                     </td>
                    <td class="tdLeft">
                        <asp:DropDownList ID="ddlstore" runat="server" CssClass="gCtrTxt"
                            Width="160px">
                        </asp:DropDownList>
                    </td>
             
                     <td align="right">
                                   Filter&nbsp;For
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_TRN_Type" runat="server" CssClass="gCtrTxt " 
                                        Font-Size="9" Width="160px">
                                        <asp:ListItem Value="">All</asp:ListItem>
                                        <asp:ListItem Value="O">Opening</asp:ListItem>
                                        <asp:ListItem Value="R">Receiving</asp:ListItem>
                                        <asp:ListItem Value="P">Packing</asp:ListItem>
                                    </asp:DropDownList>
                                  
                                </td>
                </tr>
                <tr>
                    <td class="tdRight">
                        &nbsp;</td>
                    <td class="tdLeft" colspan="7">
                        <asp:Panel ID="Panel1" runat="server" BackColor="#99CCFF">
                            <asp:RadioButtonList ID="redForQuery" runat="server" Height="16px" 
                                RepeatDirection="Horizontal">
                                <asp:ListItem Text="Red" Value="red" Selected="True">Lot Wise Yarn Stock Summary</asp:ListItem>
                                <asp:ListItem  Text="Green" Value="green">Carton Wise Yarn Stock Report</asp:ListItem>
                                <asp:ListItem  Text="Blue" Value="blue">Carton Wise Yarn Stock Details Report</asp:ListItem>
                                <asp:ListItem Text="Yellow" Value="yellow">Party Wise Yarn Stock Report</asp:ListItem>
                                <asp:ListItem Text="Sky" Value="sky">Challan Wise Yarn Stock Report</asp:ListItem>
                                 
                                
                            </asp:RadioButtonList>
                        </asp:Panel>
                    </td>
                </tr>
                </table>
                 <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupPosition="TopLeft"
                        TargetControlID="TxtFromDate">
                    </cc1:CalendarExtender>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupPosition="TopLeft"
                        TargetControlID="TxtToDate">
                    </cc1:CalendarExtender>
        </td>
    </tr>
</table>

    </ContentTemplate>
    <Triggers>
    <asp:PostBackTrigger ControlID="imgBtnExportExcel" />
    </Triggers>
    </asp:UpdatePanel>
