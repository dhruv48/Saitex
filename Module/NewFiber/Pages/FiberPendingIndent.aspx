<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="FiberPendingIndent.aspx.cs" Inherits="Module_Fiber_Pages_FiberPendingIndent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 40%;
            height: 29px;
        }
        .style2
        {
            width: 60%;
            height: 29px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
 <script language="javascript" type="text/javascript">
        function test()
    {
         var YEAR=$('#<%= ddlYear.ClientID %>').val();
         var BCODE="";
         var DCODE="";
        
         $(".viresh").colorbox({width:"90%", height:"90%", iframe:true,href:"FiberPendingIndentsDetail.aspx?BCODE="+BCODE+"&DCODE="+DCODE+"&YEAR="+YEAR});
        return false;
    }
     function test1(arg1)
     {
         var YEAR=$('#<%= ddlYear.ClientID %>').val();
        alert(YEAR);
     }
    </script>

    <link media="screen" rel="stylesheet" type="text/css" href="../../../StyleSheet/colorbox.css" />

    <script type="text/javascript" language="javascript" src="../../../javascript/jquery.min.js"></script>

    <script type="text/javascript" language="javascript" src="../../../javascript/jquery.colorbox.js"></script>

    <script type="text/javascript" language="javascript">
		$(document).ready(function(){					
			$("#colorbox").appendTo('form'); 
		});		
    </script>

    <script language="javascript" type="text/javascript">
       function openNewWindowFromGrid(BCODE,DCODE) 
        { 
//         var YEAR=$('#<%= ddlYear.ClientID %>').val();  
//       
          window.showModalDialog("FiberPendingIndentsDetail.aspx?BCODE="+BCODE+"&DCODE="+DCODE,null,"dialogheight:600px; dialogWidth:1500px; center:yes; resizable:yes");
        }
    </script>

    <table border="1" cellpadding="3" cellspacing="0" width="50%" class="tContent">
        <tr>
            <td class="td" colspan="4">
                <table align="left">
                    <tr>
                        <%--<td valign="top" align="center">
                            <asp:ImageButton ID="imgbtnPrint" OnClick="imgbtnPrint_Click" runat="server" ImageUrl="~/CommonImages/link_print.png"
                                ToolTip="Print" Height="41" Width="48"></asp:ImageButton>
                        </td>--%>
                        <td valign="top" align="center">
                            <asp:ImageButton ID="imgbtnExit" OnClick="imgbtnExit_Click" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                                ToolTip="Exit" Height="41" Width="48"></asp:ImageButton>
                        </td>
                        <td valign="top" align="center">
                            <asp:ImageButton ID="imgbtnHelp" OnClick="imgbtnHelp_Click" runat="server" ImageUrl="~/CommonImages/link_help.png"
                                ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td width="100%" align="center" class="TableHeader" colspan="4">
                <b class="titleheading">Pending Indent Report </b>
            </td>
        </tr>
        <td style="width: 40%">
            Select Year :
        </td>
        <td style="width: 60%">
            <asp:DropDownList ID="ddlYear" runat="server" Width="160px" CssClass="gCtrTxt" AutoPostBack="true"
                OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        </tr>
        <tr>
            <td width="10%" style="width: 40%">
                Select Branch:
            </td>
            <td style="width: 60%">
                <asp:DropDownList ID="ddlBranch" runat="server" Width="160px" CssClass="gCtrTxt"
                    AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="10%" class="style1">
                Select Department :
            </td>
            <td class="style2">
                <asp:DropDownList ID="ddlDepartment" runat="server" Width="160px" CssClass="gCtrTxt"
                    AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <%--<tr>
                 <td colspan="2" align="center" valign="top" style="height: 25px;">
                       
                <asp:Button ID="btnPrint" Text="Print" runat="server" Width="75" OnClick="btnPrint_Click"/>
    </tr>--%>
        <tr>
            <td width="100%" align="center" class="TableHeader" colspan="4">
                <b class="titleheading">Department Wise List of Pending Indents </b>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvDeptPendIndents" runat="server" AllowPaging="True" PagerSettings-Position="Bottom"
                    AutoGenerateColumns="False" PagerSettings-Mode="Numeric" PagerStyle-HorizontalAlign="Left"
                    OnPageIndexChanging="gvDeptPendIndents_PageIndexChanging" EmptyDataText="No Data Found"
                    PageSize="15" 
                    onselectedindexchanged="gvDeptPendIndents_SelectedIndexChanged">
                    <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                    <Columns>
                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="40px">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Branch">
                            <ItemTemplate>
                                <asp:Label ID="lblBranchName" Text='<%#Eval("BRANCH_NAME")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Department">
                            <ItemTemplate>
                                <asp:Label ID="lblDeptCode" Text='<%#Eval("DEPT_NAME") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="120px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pending Indents">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblDOJ" Text='<%#Eval("PEND_IND") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Details" ItemStyle-Width="75px" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:HyperLink ID="hlOpenContract" runat="server" Text='View' NavigateUrl='<%# "javascript:openNewWindowFromGrid("+"&#39;"+ Eval("BRANCH_CODE") + "&#39;,&#39;" + Eval("DEPT_CODE") + "&#39;);" %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle CssClass="RowStyle SmallFont" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <PagerStyle CssClass="PagerStyle" />
                    <HeaderStyle CssClass="HeaderStyle GrdHeader" />
                </asp:GridView>
        </tr>
    </table>

</asp:Content>

