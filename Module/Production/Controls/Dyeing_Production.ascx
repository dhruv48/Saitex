<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Dyeing_Production.ascx.cs" Inherits="Module_Production_Controls_Dyeing_Production" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>
<style type="text/css">
    .item {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1;
        *display: inline;
        overflow: hidden;
        white-space: nowrap;
    }

    .header {
        margin-left: 2px;
    }

    .c1 {
        width: 80px;
    }

    .c2 {
        margin-left: 2px;
        width: 150px;
    }

    .c3 {
        margin-left: 2px;
        width: 300px;
    }

    .c4 {
        margin-left: 2px;
        width: 40px;
    }

    .c5 {
        margin-left: 2px;
        width: 60px;
    }

    .c6 {
        margin-left: 2px;
        width: 60px;
    }

    .c7 {
        margin-left: 2px;
        width: 100px;
    }
</style>

<script type="text/javascript">

    function openPopup() {

        window.open("../../../Module/Production/Pages/Raw_Finish_Availablilty.aspx", "_blank", "WIDTH=1200,HEIGHT=625,scrollbars=yes, menubar=no,resizable=yes,directories=no,location=no");
    }

</script>



<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <table class="tContentArial">
        <tr>
            <td class="td">
                <table align="left">
                    <tbody>
                        <tr>
                              <td id="tdEXEL" runat="server" align="left" width="48">

                               <asp:ImageButton ID="imgBtnExportExcel" runat="server"  Height="41" ToolTip="Excel Report"
                                    ImageUrl="~/CommonImages/export.png" OnClick="imgBtnExportExcel_Click" TabIndex="7"></asp:ImageButton>&nbsp;</td>
                            
                            <td valign="top" align="center">
                                <asp:ImageButton ID="ImageButton1" OnClick="imgbtnPrint_Click" runat="server" ImageUrl="~/CommonImages/link_print.png"
                                ToolTip="Print" Height="41" Width="48"></asp:ImageButton>
                            </td>
                            <td valign="top" align="center">
                                <asp:ImageButton ID="ImageButton2" OnClick="imgbtnClear_Click" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                ToolTip="Clear" Height="41" Width="48"></asp:ImageButton>
                            </td>
                            <td valign="top" align="center">
                                <asp:ImageButton ID="ImageButton3" OnClick="imgbtnExit_Click" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                                ToolTip="Exit" Height="41" Width="48"></asp:ImageButton>
                            </td>
                           
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
        <tr>
            <td class="TableHeader td" align="center" width="100%">
                <span class="titleheading"><b>Print
                <asp:Label ID="Label1" runat="server"></asp:Label>
                </b></span>
            </td>
        </tr>
        <tr>
            <td class="td">
                <table>
                    <tr>
                        <td class="tdRight">
                            <asp:Label ID="Label2" runat="server" Text="From" CssClass="Label SmallFont"></asp:Label>
                        </td>
                        <td class="tdLeft">
                            <asp:TextBox ID="txtFrom" CssClass="TextBoxNo SmallFont" runat="server" Width="100px"></asp:TextBox>
                           <asp:RequiredFieldValidator ControlToValidate="txtFrom" runat="server" Display="Dynamic" ErrorMessage="Enter the TRN No" ValidationGroup="M1"></asp:RequiredFieldValidator>
                             <cc1:CalendarExtender ID="cetxtDeliverySchedule" runat="server" TargetControlID="txtFrom"
            PopupPosition="TopLeft" Format="dd/MM/yyyy">
          
        </cc1:CalendarExtender>
                            <%--<asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtFrom" Display="Dynamic"
                            ErrorMessage="Invalid Number" MaximumValue="9999999" MinimumValue="1" Type="Integer"
                            ValidationGroup="M1" SetFocusOnError="True"></asp:RangeValidator>--%>
                        </td>
                        <td class="tdRight">
                            <asp:Label ID="Label3" runat="server" Text="To" CssClass="Label SmallFont"></asp:Label>
                        </td>
                        <td class="tdLeft">
                            <asp:TextBox ID="txtTo" CssClass="TextBoxNo SmallFont" runat="server" Width="100px"></asp:TextBox>
                            
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtTo" runat="server" Display="Dynamic" ErrorMessage="Enter the TRN No" ValidationGroup="M1"></asp:RequiredFieldValidator>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtTo"
            PopupPosition="TopLeft" Format="dd/MM/yyyy">
          
        </cc1:CalendarExtender>
                          <%--  <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtTo" Display="Dynamic"
                            ErrorMessage="Invalid Number" MaximumValue="9999999" MinimumValue="1" Type="Integer"
                            ValidationGroup="M1" SetFocusOnError="True"></asp:RangeValidator>--%>
                        </td>
                    </tr>
                    
                   
                    
                    <tr>
                        <td colspan="4" class="tdCenter">
                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="M1" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
       
        

   </ContentTemplate>
    </asp:UpdatePanel>
