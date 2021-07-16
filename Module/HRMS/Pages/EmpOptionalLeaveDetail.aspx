<%@ Page Language="C#" MasterPageFile="~/CommonMaster/EMPMaster.master" AutoEventWireup="true" CodeFile="EmpOptionalLeaveDetail.aspx.cs" Inherits="Module_HRMS_Pages_EmpOptionalLeaveDetail" Title="Optional Leave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;}
    .header
    {
        margin-left: 4px;
    }
    .c1
    {
        width: 40px;
    }
    .c2
    {
        margin-left: 4px;
        width: 80px;
    }
     .c3
    {
        width: 50px;
    }
    .c4
    {
        margin-left: 4px;
        width: 100px;
    }
    .tdText
    {
        font: 11px Verdana;
        color: #333333;
    }
    .option2
    {
        font: 11px Verdana;
        color: #0033cc;
        padding-left: 4px;
        padding-right: 4px;
    }
    a
    {
        font: 11px Verdana;
        color: #315686;
        text-decoration: underline;
    }
    a:hover
    {
        color: Teal;
    }
    .ob_gMCont_DT
    {
        overflow: hidden;
    }
    .ob_gMCont
    {
        position: relative;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
            <table align="left"  width="100%" class="tContentArial tablebox">
        <tr>
            <td colspan="4">
                <table class="tContentArial" cellspacing="0" width="30%" cellpadding="0" border="0"
                                    align="left">
                                    <tbody>
                                        <tr>
                                            <td id="tdSave" width="48" runat="server">
                                               
                                                <asp:ImageButton ID="imgbtnInsert" runat="server" ToolTip="Save" ImageUrl="~/CommonImages/save.jpg"
                                                    Width="48" Height="41" ValidationGroup="M1" onclick="imgbtnInsert_Click" ></asp:ImageButton>
                                            </td>                                          
                                            <td width="48">
                                                <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                                                    Width="48" Height="41" 
                                                    OnClientClick="return confirm('Are you sure you want to Print?');" 
                                                    onclick="imgbtnPrint_Click" ></asp:ImageButton>
                                            </td>                                              
                                            <td width="48">
                                                <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                                                    Width="48" Height="41" 
                                                    OnClientClick="return confirm('Are you sure you want to Exit?');" 
                                                    onclick="imgbtnExit_Click" ></asp:ImageButton>
                                            </td>
                                            <td width="48">
                                                <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png" Width="48" Height="41" ></asp:ImageButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center" class="TableHeader td">
                 <span class="titleheading"><b>OPTIONAL HOLIDAY FORM</b></span>
             </td>
           </tr>  
          <tr>
            <td align="right" width="28%">
                Optional Holiday Detail
            </td>
            <td align="center" width="2%">
                <b>:</b>
            </td>
            <td align="left" width="70%">
                <asp:ListBox ID="lstOptionalLeave" runat="server" Width="300" Height="150px" SelectionMode="Multiple">
                </asp:ListBox><br />               
                <br />
                <br />
                <span style="font-size: 10pt"><strong><span style="color: #ff0033">Note: </span></strong>
                    <span style="font-size: 9pt">Please select only three Holiday.If selection 
                is more than three than first three leave will be consider<br />
                        <br />
                    </span></span>
            </td>
        </tr>
                <caption>
                    <br />
                    <br />
                    <br />
                    <tr>
                        <td colspan="4">
                            <table border="0" style="margin-left:20px;" cellpadding="0" cellspacing="0" class="tContentArial" 
                                width="550">
                                <tr>
                                    <td align="center" width="100%">
                                        <asp:GridView ID="gvOptionalLeaveMaster" runat="server" CssClass="SmallFont"
                                            AutoGenerateColumns="false" CellPadding="5" Width="100%" BorderWidth="1px"
                                            BorderStyle="Solid">
                                            
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" 
                                                    HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="25px" HeaderText="S.No." 
                                                    ItemStyle-Width="25px">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="25px" />
                                                    <ItemStyle Width="25px" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="LeaveName" HeaderStyle-HorizontalAlign="Center" 
                                                    HeaderText="Holiday Detail" ItemStyle-HorizontalAlign="left" 
                                                    ItemStyle-Width="400px" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Left" Width="400px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="YEAR" HeaderStyle-HorizontalAlign="Center" 
                                                    HeaderText="Year" ItemStyle-HorizontalAlign="Center" 
                                                    ItemStyle-Width="100px" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CreatedDate" HeaderStyle-HorizontalAlign="Center" 
                                                    HeaderText="Created Date" ItemStyle-HorizontalAlign="Center" 
                                                    ItemStyle-Width="100px" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:BoundField>
                                            </Columns>
                                            <PagerStyle CssClass="PagerStyle" />
                                            <HeaderStyle CssClass="HeaderStyle GrdHeader"  />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </caption>
            
          
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

