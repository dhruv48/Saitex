
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DesigNationHierarchy.ascx.cs"
    Inherits="Module_Admin_Controls_DesigNationHierarchy" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>--%>

        <script type="text/javascript">
function CallPrint(strid)
{  
   
  
   var partContent = document.getElementById(strid);
   var winPrint = window.open("", "mywindow","location=0,status=0,scrollbars=1,width=800,height=600");
   winPrint.document.write(partContent.innerHTML);
   winPrint .document.close();
   winPrint.window.Print();
   winPrint .close();
   partContent .innerHTML=strOldOne;
}
        </script>

        <table>
            <tr>
                <td class="td">
                    <table>
                        <tr>
                            <td id="tdPrint" runat="server" onclick="CallPrint('divPrint')">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                                    ToolTip="Print" Width="48" />
                            </td>
                            <td id="tdHelp" runat="server">
                                <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                                    ToolTip="Help" Width="48" OnClick="imgbtnHelp_Click" />
                            </td>
                            <td id="tdExit" runat="server">
                                <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                                    ToolTip="Exit" Width="48" OnClick="imgbtnExit_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr class="TableHeader">
                <td align="center" valign="top" class="td">
                    <span class="titleheading">Designation Hierachy</span>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <div id="divPrint">
                        <asp:TreeView ID="TreeView1" runat="server" 
                            OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" NodeWrap="True" 
                            ShowLines="True" ToolTip="Click on " Font-Names="Times New Roman">
                            <ParentNodeStyle Font-Overline="False" />
                            <HoverNodeStyle Font-Names="Times New Roman" />
                            <SelectedNodeStyle Font-Names="Times New Roman" />
                        </asp:TreeView>
                    </div>
                </td>
            </tr>
            <%-- <tr>   
        <td class ="td">
        
       <%--     <cc2:Grid ID="Grid1" runat="server" AllowAddingRecords="False" AllowFiltering="False"
                PageSize="5" AutoGenerateColumns="False" 
                <Columns>
             
                
                   <%-- <cc2:Column DataField="LEVEL" Align="Left" HeaderText="LEVEL" Width="180px">
                    </cc2:Column>--%>
            <%--  
                    <cc2:Column DataField="EMP_CODE" Align="Left" HeaderText="Employee Code" Width="180px">
                    </cc2:Column>
                     <cc2:Column DataField="EMPLOYEENAME" Align="Left" HeaderText="Employee Name" Width="180px">
                    </cc2:Column>
                    <cc2:Column DataField="DESIG_NAME" Align="Left" HeaderText="DesigNation Name" Width="150px">
                    </cc2:Column>
                     <cc2:Column DataField="DESIG_CODE" Align="Left" HeaderText="DesigNation Code" Width="150px">
                    </cc2:Column>
                    <cc2:Column DataField="SR_DESIG" Align="Left" HeaderText="Sr.DesigNation Name" Width="180px">
                    </cc2:Column>
                    
                    <cc2:Column DataField="SR_DESIG_CODE" Align="Left" HeaderText="Sr.DesigNation Code" Width="180px">
                    </cc2:Column>
                   <cc2:Column DataField="DESIG_REMARKS" Align="Left" HeaderText="DesigNation Remarks" Width="180px">
                    </cc2:Column>
                </Columns>
            </cc2:Grid>--%>
            </td> </tr>
        </table>
   <%-- </ContentTemplate>
</asp:UpdatePanel>--%>
