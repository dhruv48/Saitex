<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmpSalaryMaster.ascx.cs" Inherits="Module_HRMS_Controls_EmpSalaryMaster" %>
<style type="text/css">
    
    .tdText    {
        font: 11px Verdana;
        color: #333333;
    }
    .textboxno
    {
	    text-align:right;
    }         
</style>
<script language="javascript" type="text/javascript">
   function pricevalidate(test1)
    {
        var dec="";
        var fra="";
        var i;
        var val=test1.value;
        var l = test1.value.length;
        var res="";
        var dl=0;
        var fl=0;
        var index_of_dot;
        var index_of_dot=val.indexOf('.');
        var check=0;
        if (index_of_dot ==-1)
            dl=l;
        else
        { dl=index_of_dot;
            fl =(l-(index_of_dot))-1; 
            for (i=index_of_dot+1 ;i<l;i++)            {
            check++;
            if (check <4)
            {   var schar=val.charAt(i);
                fra+=schar ;            }
            else
            { alert ("Fraction point value should be upto 3 digit");
                break; 
             }                

            }
        }   
         for (i=0;i<dl;i++)

        {
            if (i==6)
            {
               alert ("Decimal Place length should be upto 6 digit");
               break;
            }
            var schar=val.charAt(i);
            dec+=schar ;          
        }     

       if (index_of_dot !=-1)
       { 
       if (isNaN (dec)||isNaN (fra))
       {
       test1.value='';
        
       }
       else
         test1.value=dec+"."+fra;
         }
         else
         {
         if (isNaN (dec))
       {
       test1.value='';       
       }else 

            test1.value=dec;

         }

   }

</script>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
    
<table align="left" class="tContentArial" width="100%">
        <tr>
            <td class="td">
                <table class="tContentArial" cellspacing="0" cellpadding="0" border="0" style="width: 15%; height: 39px">
                    <tr>
                         <td id="tdUpdate" width="48" runat="server">
                            <asp:ImageButton ID="imgbtnUpdate" OnClick="imgbtnUpdate_Click" runat="server" ToolTip="Update"
                                ImageUrl="~/CommonImages/edit1.jpg" Width="48" Height="41" ValidationGroup="M1">
                            </asp:ImageButton>
                        </td>
                        <td id="tdPrint" runat="server" width="48">
                            <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"   Width="48" Height="41" ></asp:ImageButton>
                        </td>                        
                        <td id="tdHelp" width="48" runat="server">
                            <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                                Width="48" Height="41" ></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left" width="100%" valign="top">
                <table class="td tContent" cellspacing="0" cellpadding="0" border="0" width="100%">
                    <tr>
                        <td  class="TableHeader td" align="center" width=100%; valign="top" >
                            <span class="titleheading">Employee Salary Master Detail</span>
                        </td>
                    </tr>
                    <tr>
                        <td  class="td" valign="top" align="left">
                            <br />
                            <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
                            <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError">
                            </asp:Label>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td  align="left" valign="top">
                            <table cellpadding="0" cellspacing="0" border="1" width="100%" align="left" class="tContentArial">
                                <tr>
                                    <td  width="15%" class="tdLeft">
                                        <b>Employee Code:</b>
                                    </td>
                                    <td  width="15%" align="left">
                                        <asp:Label ID="lblEmployeeCode" CssClass="SmallFont" runat="server" ></asp:Label>
                                    </td>
                                    <td  width="15%" class="tdLeft">
                                        <b>Employee Name:</b>
                                    </td>
                                    <td width="20%" align="left" >
                                        <asp:Label ID="lblEmployeeName" CssClass="SmallFont" runat="server" ></asp:Label>
                                    </td>
                                    <td width="10%" class="tdLeft">
                                        <b>Grade:</b>
                                    </td>
                                    <td width="15%" align="left" >
                                        <asp:Label ID="lblGrade" CssClass="SmallFont" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <td  width="15%"  class="tdLeft">
                                        <b>Salary Type:</b></td>
                                    <td  width="15%" colspan="5" align="left">
                                        <asp:RadioButtonList ID="RBSalaryType" CssClass="SmallFont"   runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True"  Value="M">Monthly Paid</asp:ListItem>
                                            <asp:ListItem Value="D">Daily Paid</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>                   
                   
                    <tr>
                        <td  align="center" style=" width:100%;">
                            <asp:GridView ID="gvSalaryGrade" runat="server" AutoGenerateColumns="false"  HorizontalAlign="Left" OnRowDataBound="gvSalaryGrade_RowDataBound" ShowFooter="false"
                                ShowHeader="false" Width="100%">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px" />
                                        <ItemTemplate>
                                            <table align="center" border="0" cellpadding="0" cellspacing="0" class="td tContent" width="100%">
                                                <tr>
                                                    <td align="left" valign="top" width="50%">
                                                        <asp:Label ID="lblHeadName" runat="server" Text='<%# Eval("HEAD_NAME")%>'></asp:Label>
                                                        <asp:Label ID="lblHeadId" runat="server" Text='<%# Eval("HEAD_ID")%>' Visible="false"
                                                            Width="25px"></asp:Label>
                                                    </td>
                                                    <td align="left" valign="top" width="25%">
                                                    </td>
                                                    <td align="left" valign="top" width="25%">
                                                    </td>                                                    
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="600px" />
                                        <ItemTemplate>
                                            <asp:GridView ID="gvSubHead" runat="server" AutoGenerateColumns="false" CssClass="tContentArial"
                                                HorizontalAlign="center" ShowFooter="false" ShowHeader="false" 
                                                Width="100%" onrowdatabound="gvSubHead_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <table align="center" border="0" cellpadding="0" cellspacing="0" class="tContentArial"
                                                                width="100%">
                                                                <tr>
                                                                    <td align="left" valign="top" width="30%">
                                                                        <asp:Label ID="lblSubHeadId" runat="server" Text='<%# Eval("SUBH_ID")%>'
                                                                            Visible="false"></asp:Label>
                                                                        <asp:Label ID="lblSalarySubHeadName" runat="server" Text='<%# Eval("SUBH_NAME")%>'></asp:Label>
                                                                    </td>
                                                                    <td align="center" valign="top" width="5%">
                                                                    </td>
                                                                    <td align="left" valign="top" width="10%">
                                                                        <asp:TextBox ID="txtDefaultValue" runat="server"  CssClass="gCtrTxt textboxno" onkeyup="pricevalidate(this);"  Text="0"  Width="100px"></asp:TextBox>
                                                                        <br />
                                                                        <asp:RequiredFieldValidator ID="RFDefaultValue" runat="server" ControlToValidate="txtDefaultValue"
                                                                            Display="Dynamic" ErrorMessage="Pls Enter Value!" ValidationGroup="M1"></asp:RequiredFieldValidator>
                                                                        <asp:RangeValidator ID="RvDefaultValue" runat="server" ControlToValidate="txtDefaultValue"
                                                                            Display="Dynamic" ErrorMessage="Value From 0-999999" MaximumValue="999999" MinimumValue="0"
                                                                            Type="Double" ValidationGroup="M1"></asp:RangeValidator>
                                                                    </td>
                                                                     <td align="left" width="45%" valign="top"><asp:Label ID="LblType" runat="server" Text='<%# Eval("SUBH_TYPE")%>'></asp:Label></td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:GridView>                           
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left" width="100%" valign="top">
            </td>
        </tr>
    </table>
</ContentTemplate>
</asp:UpdatePanel>