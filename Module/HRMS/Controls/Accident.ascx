<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Accident.ascx.cs" Inherits="Module_HRMS_Controls_Accident" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc2" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="Combo" %>
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
        width: 80px;
    }
    .c2
    {
        margin-left: 4px;
        width: 150px;
    }
    .c3
    {
        margin-left: 4px;
        width: 80px;
    }
    .modalPopup
    {
        background-color: #696969;
        filter: alpha(opacity=40);
        opacity: 0.7;
        xindex: -1;
    }
</style>


<table class="td tContentArial">
 <tr>
        <td align="Right" class="td" colspan = "6">
            <table align="left">
                <tr>
                    <td id="tdSave" align="left" width="48" runat="server">
                        <asp:ImageButton ID="imgbtnNew" runat="server" Height="41" ImageUrl="~/CommonImages/save.jpg"
                            OnClick="imgbtnNew_Click" ToolTip="Save" ValidationGroup="s" Width="48" />
                    </td>
                    <td id="tdUpdate" align="left" width="48" runat="server">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" Height="41" ImageUrl="~/CommonImages/edit1.jpg"
                             ToolTip="Update" ValidationGroup="s" 
                            Width="48" onclick="imgbtnUpdate_Click" />
                    </td>
                    <td id="tdDelete" runat="server">
                        <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete" ImageUrl="~/CommonImages/del6.png"
                            Width="48" Height="41" onclick="imgbtnDelete_Click"></asp:ImageButton>
                    </td>
                    <td id="tdClear" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                            ToolTip="Clear" Width="48" onclick="imgbtnClear_Click" />
                    </td>                 
                    <td id="find" runat="server">
                        <asp:ImageButton ID="Imgbtnfind" runat="server" ToolTip="find" ImageUrl="~/CommonImages/link_find.png"
                            Width="48" Height="41" onclick="Imgbtnfind_Click"  >
                        </asp:ImageButton>
                    </td>
                    <td id="tdPrint" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" Width="48" onclick="imgbtnPrint_Click" />
                    </td>
                    <td id="tdExit" runat="server">
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                            Width="48" Height="41" onclick="imgbtnExit_Click" ></asp:ImageButton>
                    </td>
                    <td id="tdHelp" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                            ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                    </td>
                </tr>
            </table>
                        
        </td>
    </tr>
    <tr>
        <td class="TableHeader td" align="center" colspan = "6">
            <span class="titleheading">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ACCIDENT &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
    <tr>
        <td align="left" valign="top" class="td" colspan="6">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>
     <tr>
         <td style="text-align: right">

             Accident_id :</td>
         <td>

             <asp:TextBox ID="txtaccident_id" runat="server"></asp:TextBox>

        </td>
         <td style="text-align: right">

             Employee :</td>
         <td>
<Combo:ComboBox ID="cmbEmpCode" runat="server" Width="200px" EmptyText="------------Select------------"
                Height="150px" AutoPostBack="True" MenuWidth="300px" DataValueField="EMP_CODE"
                OnSelectedIndexChanged="cmbEmpCode_SelectedIndexChanged">
                <HeaderTemplate>
                    <div class="header c1">
                        Code
                    </div>
                    <div class="header c2">
                        Emp Name</div>
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="item c1">
                        <%# Eval("EMP_CODE")%></div>
                    <div class="item c2">
                        <%# Eval("EMPLOYEENAME")%></div>
                </ItemTemplate>
                <FooterTemplate>
                    Displaying items
                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                    out of
                    <%# Container.ItemsCount %>.
                </FooterTemplate>
            </Combo:ComboBox>

                            <asp:DropDownList ID="ddlfind" runat="server" 
                                 onselectedindexchanged="ddlfind_SelectedIndexChanged" AutoPostBack="True" 
                                DataTextField="EMPLOYEENAME" DataValueField="EMP_CODE" Width="200px">
                            </asp:DropDownList>
                          
                          
        </td>
         <td>

        </td>
         <td>

        </td>
         
    </tr>
     <tr>
         <td style="text-align: right">

             Designation :</td>
         <td>

             <asp:TextBox ID="txtdesig" runat="server"></asp:TextBox>

        </td>
         <td style="text-align: right">

             Department :</td>
         <td>


             <asp:TextBox ID="txtdept" runat="server"></asp:TextBox>


        </td>
         <td style="text-align: right">

             ESI_No :</td>
         <td>

             <asp:TextBox ID="txtesi_no" runat="server"></asp:TextBox>

        </td>
         
    </tr>
     <tr>
         <td style="text-align: right">

             Gender :</td>
         <td>

             <asp:TextBox ID="txtsex" runat="server"></asp:TextBox>

        </td>
         <td style="text-align: right">

             Age :</td>
         <td>


             <asp:TextBox ID="txtage" runat="server"></asp:TextBox>


        </td>
         <td style="text-align: right">

             Shift :</td>
         <td>

             <asp:TextBox ID="txtshift" runat="server"></asp:TextBox>

        </td>
         
    </tr> <tr>
         <td style="text-align: right">

             Date :</td>
         <td>

             <asp:TextBox ID="txtdate" runat="server"></asp:TextBox>

             <cc1:CalendarExtender ID="ce2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtdate" 
                 PopupPosition="TopLeft">
                        </cc1:CalendarExtender>

        </td>
         <td style="text-align: right">

             Time :</td>
         <td>


             <asp:TextBox ID="txttime" runat="server"></asp:TextBox>


        </td>
         <td style="text-align: right">

             Place :</td>
         <td>

             <asp:TextBox ID="txtplace" runat="server"></asp:TextBox>

        </td>
         
    </tr>
<tr>
<td style="text-align: right">Accident Reason :</td>
<td colspan="5">
    <asp:TextBox ID="txtreason" runat="server" Width="780px"></asp:TextBox>
            </td>

</tr>
 <tr>
        <td style="text-align: right" class="style3">
            &nbsp;
        </td>
        <td colspan="5">
            <asp:GridView ID="GridAccident" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                 PageSize="5" onrowcommand="GridAccident_RowCommand" Width="100%" >
                
                <Columns>
                    <asp:BoundField DataField="ACCIDENT_ID" HeaderText="ACCIDENT_ID" />
                    <asp:BoundField DataField="EMPLOYEENAME" HeaderText="EMPLOYEE" />
                    <asp:BoundField DataField="DEPT_NAME" HeaderText="DEPARTMENT" />
                    <asp:BoundField DataField="DESIG_NAME" HeaderText="DESIGNATION" />
                    <asp:BoundField DataField="ACCIDENT_DATE" HeaderText="ACCIDENT DATE"/>
                    <asp:BoundField DataField="ACCIDENT_TIME" HeaderText="TIME" />
                    <asp:BoundField DataField="ACCIDENT_PLACE" HeaderText="PLACE" />
                    <asp:BoundField DataField="ACCIDENT_REASON" HeaderText="REASON" />                 
                    <asp:TemplateField HeaderText="Edit" ItemStyle-VerticalAlign="top">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImgEdit" CommandArgument='<%# Eval(" ACCIDENT_ID") %>' CommandName="RecordEdit"
                                ImageUrl="~/CommonImages/Icons/editpen.png" Width="20px" runat="server" CssClass="ContolStyle"></asp:ImageButton>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>