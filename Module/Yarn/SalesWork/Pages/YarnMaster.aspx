<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="YarnMaster.aspx.cs" Inherits="Module_Yarn_SalesWork_Pages_YarnMaster"
    Title="Untitled Page" MaintainScrollPositionOnPostback="true" ValidateRequest="false" %>
    <%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%--<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc11" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
    <style type="text/css">
        .tdLeft
        {
            width: -110%;
            margin-left: 40px;
        }
    </style>
    
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;
        }
    .header
    {
        margin-left: 2px;
    }
    .c1
    {
        width: 100px;
    }
    .c2
    {
        margin-left: 4px;
        width: 200px;
    }
    .c3
    {
        margin-left: 4px;
        width: 250px;
    }
    .c4
    {
        margin-left: 4px;
        width: 150px;
    }
    .c5
    {
        margin-left: 4px;
        width: 100px;
    }
    .style1
    {
        height: 207px;
    }
    </style>

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
        width: 100px;
    }
    .c2
    {
        margin-left: 4px;
        width: 800px;
    }
    .c3
    {
        width: 200px;
    }
    .d1
    {
        width: 180px;
    }
    .d2
    {
        margin-left: 4px;
        width: 120px;
    }
    .d3
    {
        margin-left: 4px;
        width: 180px;
    }
    .d4
    {
        margin-left: 4px;
        width: 120px;
    }
</style>

    <style type="text/css">
    .style1
    {
        height: 21px;
        color:White;
    }
        </style>

    
<script type="text/javascript" src="../../../../javascript/jquery-1.4.1.min.js"></script>
<script src="../../../../javascript/jquery-ui.min.js" type = "text/javascript"></script>
<link href="../../../../javascript/jquery-ui.css" rel = "Stylesheet" type="text/css" />
<script type="text/javascript">
    $(document).ready(function() {
    $("#<%=txtYarnDescription.ClientID %>").autocomplete({
            source: function(request, response) {
                $.ajax({
                url: '<%=ResolveUrl("~/MOM.asmx/GetYarnDescription") %>',
                    data: "{ 'prefix': '" + request.term + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function(data) {
                        response(data.d);
                        //                        response($.map(data.d, function(item) {
                        //                            return {
                        //                                label: item.split('-')[0],
                        //                                val: item.split('-')[1]
                        //                            }
                        //                        }))
                    },
                    error: function(response) {
                        alert(response.responseText);
                    },
                    failure: function(response) {
                        alert(response.responseText);
                    }
                });
            },

            minLength: 1
        });

        $("#<%=txtGrade.ClientID %>").autocomplete({
            source: function(request, response) {
                $.ajax({
                    url: '<%=ResolveUrl("~/MOM.asmx/GetMOMGrade") %>',
                    data: "{ 'prefix': '" + request.term + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function(data) {
                        response(data.d);

                    },
                    error: function(response) {
                        alert(response.responseText);
                    },
                    failure: function(response) {
                        alert(response.responseText);
                    }
                });
            },

            minLength: 1
        });


        $("#<%=txtLotNo.ClientID %>").autocomplete({
            source: function(request, response) {
                $.ajax({
                    url: '<%=ResolveUrl("~/MOM.asmx/GetMOMMerge") %>',
                    data: "{ 'prefix': '" + request.term + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function(data) {
                        response(data.d);

                    },
                    error: function(response) {
                        alert(response.responseText);
                    },
                    failure: function(response) {
                        alert(response.responseText);
                    }
                });
            },
            minLength: 1
        });

        
    });
</script>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td valign="top">
                        <table align="left" class="tContentArial">
                            <tr>
                                <td align="left" class="td" valign="top">
                                    <table>
                                        <tr>
                                            <td id="tdSave" runat="server">
                                                <%--<asp:ImageButton ID="imgbtnSave" runat="server" ImageUrl="~/CommonImages/save.jpg"
                                                    OnClick="imgbtnSave_Click" ToolTip="Save" ValidationGroup="YM" TabIndex="51" />--%>
                                                <asp:ImageButton ID="imgbtnSave" TabIndex="65" OnClick="imgbtnSave_Click" runat="server"
                                                    ImageUrl="~/CommonImages/save.jpg" ToolTip="Save" Height="41px" Width="48px" ValidationGroup="M1">
                                                </asp:ImageButton>                                                    
                                            </td>
                                            <td id="tdUpdate" runat="server">
                                                <asp:ImageButton ID="imgbtnUpdate" runat="server" ImageUrl="~/CommonImages/edit1.jpg" TabIndex="65"
                                                    OnClick="imgbtnUpdate_Click" OnClientClick="if (!confirm('Are you sure to Update the record ?')) { return false; }"
                                                    ToolTip="Update" ValidationGroup="M1" />
                                            </td>
                                            <td id="tdDelete" runat="server">
                                                <asp:ImageButton ID="imgbtnDelete" runat="server" Visible="false" Enabled="false" ImageUrl="~/CommonImages/del6.png" TabIndex="66"
                                                    OnClick="imgbtnDelete_Click" OnClientClick="if (!confirm('Are you sure to Delete the record ?')) { return false; }"
                                                    ToolTip="Delete" CausesValidation="false" />
                                            </td>
                                            <td id="tdFind" runat="server">
                                                <asp:ImageButton ID="imgbtnFind" runat="server" ImageUrl="~/CommonImages/link_find.png" TabIndex="66"
                                                    OnClick="imgbtnFind_Click1" OnClientClick="if (!confirm('Are you sure to Find the record ?')) { return false; }"
                                                    ToolTip="Find"  CausesValidation="false" />
                                            </td>
                                             <td id="tdList" runat="server">
                                                <asp:ImageButton ID="imgbtnList" runat="server" ImageUrl="~/CommonImages/list.jpg" TabIndex="67"
                                                    ToolTip="Poy Master List" onclick="imgbtnList_Click" />
                                            </td>
                                            <td id="tdPrint" runat="server">
                                                <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png" 
                                                    OnClick="imgbtnPrint_Click" OnClientClick="if (!confirm('Are you sure to Print the record ?')) { return false; }"
                                                    ToolTip="Print" TabIndex="68" CausesValidation="false" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                                    OnClick="imgbtnClear_Click" OnClientClick="if (!confirm('Are you sure to Clear the record ?')) { return false; }"
                                                    ToolTip="Clear" TabIndex="69" CausesValidation="false"/>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgbtnExit" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                                                    OnClick="imgbtnExit_Click" OnClientClick="if (!confirm('Are you sure to Exit From This Form ?')) { return false; }"
                                                    ToolTip="Exit" TabIndex="70" CausesValidation="false" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                                                    ToolTip="Help" TabIndex="71"  CausesValidation="false"/>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="TableHeader td">
                                    <span class="titleheading"><b>Yarn Master</b></span>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="td" valign="top">
                                    <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>
                                        &nbsp;Mode </span>
                                </td>
                            </tr>
                            <tr>
                                <td class="td" width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td class="tdRight" width="15%">
                                                Yarn Code*
                                            </td>
                                            <td width="15%" valign="top">
                                                <asp:TextBox ID="txtYarnCode" runat="server" CssClass="SmallFont TextBoxNo TextBoxDisplay"
                                                    ReadOnly="True" Width="125" TabIndex="1"></asp:TextBox>
                                               <%-- <asp:DropDownList ID="ddlyarncode" runat="server" AutoPostBack="true" AppendDataBoundItems="True"
                                                 CssClass="SmallFont" TabIndex="4" Width="125"  
                                                 onselectedindexchanged="ddlyarncode_SelectedIndexChanged1"
                                            
                                                    >
                                                    
                                                </asp:DropDownList>--%>
                                        <cc2:ComboBox ID="ddlyarncode" runat="server" AutoPostBack="True" CssClass="SmallFont"
                                                    DataTextField="YARN_DESC" DataValueField="YARN_CODE" EmptyText="Find Yarn Code" EnableLoadOnDemand="true" EnableVirtualScrolling="true"
                                                    Height="200px" MenuWidth="700" OnLoadingItems="ddlyarncode_LoadingItems" OnSelectedIndexChanged="ddlyarncode_SelectedIndexChanged1"
                                                    TabIndex="1"  Width="125px" Visible=false >
                                                    <HeaderTemplate>
                                                        <div class="header c1">
                                                            YARN CODE</div>
                                                       
                                                        <div class="header c1">
                                                            YARN Description</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="item c1">
                                                            <asp:Literal ID="Container1" runat="server" Text='<%# Eval("YARN_CODE") %>' /></div>
                                                        <div class="item c2">
                                                            <asp:Literal ID="Container2" runat="server" Text='<%# Eval("YARN_DESC") %>' /></div>
                                                        
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        Displaying items
                                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                                        <%# Container.ItemsCount %>.
                                                    </FooterTemplate>
                                                </cc2:ComboBox>
                                                
                                                
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtYarnCode"
                                                    Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="YM12"></asp:RequiredFieldValidator>
                                            </td>
                                             <td class="tdRight" width="15%">
                                                &nbsp;Category
                                            </td>
                                            <td width="15%">
                                                <asp:DropDownList ID="ddlYarnCat" runat="server" AppendDataBoundItems="True" CssClass="SmallFont"
                                                    TabIndex="2" Width="127" 
                                                    onselectedindexchanged="ddlYarnCat_SelectedIndexChanged"  AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                           <td class="tdRight" width="15%">
                                                Luster:
                                            </td>
                                            <td width="15%" >
                                                <asp:DropDownList ID="ddlClassification" runat="server" AppendDataBoundItems="True" CssClass="SmallFont"
                                                    TabIndex="3" Width="127">
                                                </asp:DropDownList>
                                            </td>
                                           
                                        <tr>
                                        
                                        <td class="tdRight" width="20%">
                                             <%--   *Quality--%>  &nbsp;Process*
                                            </td>
                                            <td class="tdLeft" width="20%">
                                                <asp:DropDownList ID="ddlQuality" runat="server" AppendDataBoundItems="True" CssClass="SmallFont"
                                                    TabIndex="8" Width="125">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator53" runat="server" ControlToValidate="ddlQuality"
                                                    Display="Dynamic" ErrorMessage="*" InitialValue="0" SetFocusOnError="True"
                                                    ValidationGroup="AC"></asp:RequiredFieldValidator>
                                            </td>
                                            
                                            
                                            </td>
                                            <td class="tdRight" width="15%">
                                             Dyed/Non Dyed*
                                            </td>
                                            <td class="tdLeft" width="25%" valign="top">
                                                <asp:DropDownList ID="ddlYarnType" runat="server" AppendDataBoundItems="True" CssClass="SmallFont"
                                                    TabIndex="5" Width="125"  AutoPostBack="true"
                                                 onselectedindexchanged="ddlYarnType_SelectedIndexChanged"  >  
                                                </asp:DropDownList>
                                                
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="ddlYarnType"
                                                   Display="Dynamic" ErrorMessage="*" InitialValue="0" SetFocusOnError="True"
                                                    ValidationGroup="YM"></asp:RequiredFieldValidator>
                                            </td>
                                             <td class="tdRight" width="15%" valign="top">
                                                Twist/Non Twist
                                            </td>
                                            <td width="15%" valign="top">
                                                <asp:DropDownList ID="ddlCatType" runat="server" AppendDataBoundItems="True"  AutoPostBack="true"
                                                    CssClass="SmallFont"  OnSelectedIndexChanged="ddlCatType_SelectedIndexChanged"
                                                    TabIndex="4" Width="125">
                                                </asp:DropDownList>
                                                
                                           
                                        </tr>
                                       
                                        
                                        <tr>
                                            
                                            <td width="15%" class="tdRight">
                                                <asp:Label ID="lblCount" runat="server" Text="Denier*"></asp:Label>
                                            </td>
                                            <td class="tdLeft" width="15%">
                                                <asp:DropDownList ID="ddlCount" runat="server" AppendDataBoundItems="True" CssClass="SmallFont"
                                                    TabIndex="10" Width="127">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator51" runat="server" ControlToValidate="ddlCount"
                                                    Display="Dynamic" ErrorMessage="*" InitialValue="0" SetFocusOnError="True"
                                                    ValidationGroup="AC"></asp:RequiredFieldValidator>
                                            </td>
                                             <td class="tdRight" width="20%">
                                              Filament*
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCoating"
                                                   Display="Dynamic" ErrorMessage="*" InitialValue="0" SetFocusOnError="True"
                                                    ValidationGroup="YM"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="tdLeft" width="20%">
                                                <asp:DropDownList ID="ddlCoating" runat="server" AppendDataBoundItems="True" CssClass="SmallFont"
                                                    TabIndex="14" Width="125">
                                                </asp:DropDownList>
                                               
                                            </td>
                                           <%-- <td id="Classimate" runat="server" class="tdRight" width="15%">
                                                Classimate
                                            </td>
                                            <td id="Classimate1" runat="server" class="tdLeft" width="15%">
                                                <asp:TextBox ID="txtClassimate" runat="server" CssClass="SmallFont TextBoxNo" MaxLength="50"
                                                    TabIndex="12" Width="125"></asp:TextBox>
                                                <asp:RangeValidator ID="RangeValidator13" runat="server" ControlToValidate="txtClassimate"
                                                    Display="None" ErrorMessage="Please Enter Classimate in Numeric &amp; Precision Should be 7 and Scale 2   "
                                                    MaximumValue="9999999.99" MinimumValue="0" Type="Double" ValidationGroup="YM"></asp:RangeValidator>
                                                <br />
                                            </td>--%>
                                            <td class="tdRight" width="15%">
                                               Base&nbsp;PLY
                                            </td>
                                            <td width="15%" >
                                                <asp:DropDownList ID="ddlFancyEffect" runat="server" AppendDataBoundItems="True" CssClass="SmallFont"
                                                    TabIndex="3" Width="127">
                                                </asp:DropDownList>
                                            </td>
                                                
                                        </tr>  
                                        <tr>
                                         <td id="Td26" runat="server" class="tdRight"  width="15%">
                                             <font color="#ff0000">*</font>Base&nbsp;Direction&nbsp;:
                                            </td>
                                            <td id="Td27" runat="server" class="tdLeft" width="25%">
                               <asp:DropDownList ID="ddlCustom_ITCHS" Width="125px"  CssClass="SmallFont"
                            TabIndex="23" runat="server">
                        </asp:DropDownList>
                                                  <%--    <asp:RangeValidator ID="txtCustom_ITCHS_RangeValidator" ValidationGroup="M1" runat="server" ErrorMessage="Pls enter custom itchs code."
                                                    Display="None" Type="Double" ControlToValidate="txtCustom_ITCHS" MinimumValue="00000000"
                                                    MaximumValue="99999999"></asp:RangeValidator>--%>
                                                     
                                            </td>
                                            <td id="Td22" runat="server" class="tdRight" width="15%">
                                              <font color="#ff0000">*</font>Milange&nbsp;Code&nbsp;:
                                            </td>
                                            <td id="Td23" runat="server" class="tdLeft"  width="25%">
                                               <asp:DropDownList ID="ddlSales_ITCHS" Width="125px" AppendDataBoundItems="True"  CssClass="SmallFont "
                                              TabIndex="22" runat="server">
                                        </asp:DropDownList>
                                        
                                       
                                        
                                                 <%--   <asp:RangeValidator ID="txtSales_ITCHS_RangeValidator" ValidationGroup="M1" runat="server" ErrorMessage="Pls enter sales itchs code."
                                                    Display="None" Type="Double" ControlToValidate="txtSales_ITCHS" MinimumValue="00000000"
                                                    MaximumValue="99999999"></asp:RangeValidator>--%>
                                                    
                                            </td>
                                        </tr>
                                         <tr id="NormalDetails" runat="server">
                                            <td class="tdleft" colspan="6">
                                                <b>Normal&nbsp;Details................ </b>
                                            </td>
                                        </tr>                                      
                                        <tr id="NormalTPM" runat="server">
                          
                                            <td id="Td7" runat="server" class="tdRight" width="15%">
                                                            Primary(TPM)
                                                        </td>
                                                        <td id="Td8" runat="server" class="tdLeft" width="15%">
                                                            <asp:TextBox ID="txtTpm1" TabIndex="22" Width="125" CssClass="SmallFont TextBoxNo"
                                                                runat="server" MaxLength="8"></asp:TextBox>
                                                                
                                                         <cc11:FilteredTextBoxExtender ID="FiltertxtTpm1" runat="server" TargetControlID="txtTpm1" FilterType="Custom, Numbers"     />
                                                        </td>
                                            
                                           
                                          
                                                <td id="Td19" runat="server" class="tdRight" width="15%"  runat=server >
                                                    Primary TD*
                                            </td>
                                            <td id="Td20" runat="server" class="tdLeft" width="25%"  runat=server >
                                                <asp:DropDownList ID="ddlTwistDirection" runat="server" AppendDataBoundItems="True" CssClass="SmallFont"
                                                     TabIndex="15" Width="125"
                                                    >
                                                </asp:DropDownList>
                                               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlTwistDirection"
                                                    Display="Dynamic" ErrorMessage="*" InitialValue="Select" SetFocusOnError="True"
                                                    ValidationGroup="AC"></asp:RequiredFieldValidator>--%>
                                            </td>
                                             
                                            <td width="15%" align="right" valign="top">
                                                <asp:Label ID="lblNamrmalPly" runat="server" Text="Twist PLY"></asp:Label> 
                                            </td>
                                            
                                            <td id="PLY1" runat="server" class="tdLeft" width="25%">
                                                <asp:DropDownList ID="ddlPly" runat="server" AppendDataBoundItems="True" CssClass="SmallFont"
                                                    OnSelectedIndexChanged="ddlPly_SelectedIndexChanged" TabIndex="12" Width="125" 
                                                   >
                                                </asp:DropDownList>
                                              <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator52" runat="server" ControlToValidate="ddlPly"
                                                    Display="Dynamic" ErrorMessage="*" InitialValue="Select" SetFocusOnError="True"
                                                    ValidationGroup="AC"></asp:RequiredFieldValidator>--%>
                                            </td>
                                        </tr>                                        
                                        <tr id="DivValues" runat="server" >
                                            
                                            <td width="15%" class="tdRight">
                                                <asp:Label ID="lblCountValue" runat="server" Text="Denier Value"></asp:Label>
                                            </td>
                                            <td class="tdLeft" width="15%">
                                            
                                                 <asp:TextBox ID="txtCountValue" runat="server" 
                                                    CssClass="SmallFont TextBoxNo" MaxLength="20"
                                                        TabIndex="16" Width="125"></asp:TextBox>
                                            
                                            </td>
                                            
                                           <td width="15%" align="right" valign="top">
                                                <%--*D/IO/E/J--%>Yarn&nbsp;Business&nbsp;Type
                                            </td>
                                            <td class="tdLeft" width="15%">
                                                <asp:DropDownList ID="ddlDej" runat="server" AppendDataBoundItems="True" CssClass="SmallFont"
                                                    TabIndex="7" Width="127">
                                                </asp:DropDownList>
                                               
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator54" runat="server" ControlToValidate="ddlDej"
                                                    Display="Dynamic" ErrorMessage="*" InitialValue="0" SetFocusOnError="True"
                                                    ValidationGroup="AC"></asp:RequiredFieldValidator>
                                            </td>
                                          
                                                
                                        </tr>
                                          <tr>
                                            
                                            <td width="15%" class="tdRight" visible="false">
                                               <font color="#ff0000"></font> <asp:Label ID="Label3" Visible="false" runat="server" Text="Is&nbsp;Excisable&nbsp;"></asp:Label>
                                            </td>
                                            <td class="tdLeft" width="15%">
                                            
                                               <asp:RadioButtonList ID="rdIsExciable" runat="server" Visible="false" CssClass="SmallFont" RepeatColumns="4"
                                                    RepeatDirection="Horizontal" TabIndex="19" Height="11px"  
                                                    RepeatLayout="Table"  Width="100px"
                                                    onselectedindexchanged="rdIsExciable_SelectedIndexChanged" 
                                                    AutoPostBack="True" >
                                                    <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                                                    <asp:ListItem Value="0" >No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            
                                            </td>
                                            
                                          
                                               
                                            
                                        </tr>
                                          <tr id="ConRate" runat="server">
                                            <td width="15%" class="tdRight">
                                                <asp:Label ID="Label4" runat="server" Text="Conversion Rate"></asp:Label>
                                            </td>
                                            <td class="tdLeft" width="15%">
                                            
                                                 <asp:TextBox ID="txtConversionRate" runat="server" 
                                                    CssClass="SmallFont TextBoxNo" MaxLength="20"
                                                        TabIndex="22" Width="125"></asp:TextBox>
                                            
                                            </td>
                                            <td class="tdRight" width="20%">
                                             Value1
                                            </td>
                                            <td class="tdLeft" width="20%">
                                                <asp:TextBox ID="txtValue1" runat="server" 
                                                    CssClass="SmallFont TextBoxNo" MaxLength="20"
                                                        TabIndex="23" Width="125"></asp:TextBox>                                              
                                            </td>
                                          
                                                <td id="Td28" runat="server" class="tdRight" width="15%">
                                              Value2:
                                            </td>
                                            <td id="Td29" runat="server" class="tdLeft" width="25%">
                                              <asp:TextBox ID="txtValue2" runat="server" 
                                                    CssClass="SmallFont TextBoxNo" MaxLength="20"
                                                        TabIndex="24" Width="125"></asp:TextBox>
                                                    
                                            </td>
                                            
                                        </tr>
                                         <tr class="td" id="Singleplymsgtr" runat="server">
                                                        <td id="Td1" runat="server" class="tdRight " width="15%">
                                                            &nbsp;
                                                        </td>
                                                        <td id="Td2" runat="server" class="style1" width="15%" align="center">
                                                            TPM
                                                        </td>
                                                        <td id="Td3" runat="server" class="tdRight" width="15%">
                                                            &nbsp;
                                                        </td>
                                                        <td id="Td4" runat="server" class="style1" width="15%" align="center">
                                                            Direction
                                                        </td>
                                                        <td id="Td5" runat="server" class="tdRight" width="15%">
                                                            &nbsp;
                                                        </td>
                                                        <td id="Td6" runat="server" class="style1" width="25%" align="center">
                                                            Remarks
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr id="SingleTR" runat="server">
                                                        
                                                        <td id="Td9" runat="server" class="tdRight" width="15%">
                                                            Secondary(TPM)&nbsp;
                                                        </td>
                                                        <td id="Td10" runat="server" class="tdLeft" width="15%">
                                                            <asp:TextBox ID="txtDirection1" Width="125" TabIndex="23" CssClass="SmallFont TextBoxNo"
                                                                runat="server" MaxLength="50"></asp:TextBox>
                                                        </td>
                                                        <td class="tdRight" width="20%">
                                              Secondary TD*
                                            </td>
                                            <td class="tdLeft" width="20%">
                                                <asp:DropDownList ID="ddlbBlendingProcess" runat="server" AppendDataBoundItems="True" 
                                                CssClass="SmallFont" TabIndex="17" Width="125"></asp:DropDownList>                                               
                                            </td>
                                            
                                            
                                                        <td id="Td11" runat="server" class="tdRight" width="15%" visible="false">
                                                            Remark&nbsp;
                                                        </td>
                                                        <td id="Td12" runat="server" class="tdLeft" width="25%">
                                                            <asp:TextBox ID="txtRemarks1" Width="125" TabIndex="24" CssClass="SmallFont TextBoxNo" Visible="false" MaxLength="50"
                                                                runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="MultiDetails" runat="server">
                                            <td class="tdleft" colspan="6">
                                                <b>Multi&nbsp;Details................ </b>
                                            </td>
                                        </tr>  
                                                    
                                                    <tr id="PlyTr" runat="server">
                                                        <td id="Td13" runat="server" class="tdRight" width="15%">
                                                            Multi TPM
                                                        </td>
                                                        <td id="Td14" runat="server" class="tdLeft" width="15%">
                                                            <asp:TextBox ID="txtTpm2" Width="125" TabIndex="25" CssClass="SmallFont TextBoxNo"
                                                                runat="server" MaxLength="8"></asp:TextBox>
                                                                <cc11:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtTpm2" FilterType="Custom, Numbers"     />
                                                       
                                                        </td>
                                                        <td id="Td15" runat="server" class="tdRight" width="15%">
                                                           Multi TD &nbsp;
                                                        </td>
                                                        <td id="Td16" runat="server" class="tdLeft" width="15%">
                                                            <asp:DropDownList ID="txtDirection2" runat="server" AppendDataBoundItems="True" CssClass="SmallFont"
                                                              Width="125"   MaxLength="50"></asp:DropDownList>
                                                        </td>
                                                       
                                            <td width="15%" class="tdRight">
                                                <asp:Label ID="lblmultiply" runat="server" Text="Multi PLY&nbsp;*"></asp:Label>
                                            </td>
                                            <td class="tdLeft" width="15%">
                                                <asp:DropDownList ID="ddlEndUse" runat="server" AppendDataBoundItems="True" CssClass="SmallFont"
                                                    TabIndex="13" Width="127">
                                                </asp:DropDownList>
                                               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlEndUse"
                                                    Display="Dynamic" ErrorMessage="*" InitialValue="0" SetFocusOnError="True"
                                                    ValidationGroup="AC"></asp:RequiredFieldValidator>--%>
                                            </td>
                                                        <td id="Td17" runat="server" class="tdRight" width="15%">
                                                            &nbsp;
                                                        </td>
                                                        <td id="Td18" runat="server" class="tdLeft" width="25%">
                                                            <asp:TextBox ID="txtRemarks2" Width="125" TabIndex="27" Visible="false" CssClass="SmallFont TextBoxNo"
                                                                runat="server" MaxLength="50"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                                                
                                       
                                        <tr>
                                           <%-- <td id="Uster" runat="server" class="tdRight" width="15%">
                                                Uster % (U)%
                                            </td>
                                            <td id="Uster1" runat="server" class="tdLeft">
                                                <asp:TextBox ID="txtUster" runat="server" CssClass="SmallFont TextBoxNo" MaxLength="10"
                                                    TabIndex="14" Width="125"></asp:TextBox>
                                                <br />
                                                <asp:RangeValidator ID="RangeValidator9" runat="server" ControlToValidate="txtUster"
                                                    Display="None" ErrorMessage="Please Enter Uster in Numeric &amp; Precision Should be 7 and Scale 2   "
                                                    MaximumValue="9999999.99" MinimumValue="0" Type="Double" ValidationGroup="YM"></asp:RangeValidator>
                                            </td>
                                            <td id="Td19" runat="server" class="tdRight" width="15%">
                                                *Single Yarn Strength
                                            </td>
                                            <td id="Td20" runat="server" class="tdLeft" style="width: 0%">
                                                <asp:TextBox ID="txtCsP" runat="server" CssClass="SmallFont TextBoxNo" MaxLength="10"
                                                    TabIndex="15" Width="125"></asp:TextBox>
                                                <cc11:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Numbers"
                                                    TargetControlID="txtCsP">
                                                </cc11:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtCsP"
                                                    Display="None" ErrorMessage="Please Enter Single Yarn Strength" SetFocusOnError="True"
                                                    ValidationGroup="YM"></asp:RequiredFieldValidator>
                                            </td>
                                            <td id="Variencelbl" runat="server" class="tdRight" width="15%">
                                                Varience
                                            </td>
                                            <td id="Variencetxt" runat="server">
                                                <asp:TextBox ID="txtVarience" TabIndex="16" CssClass="SmallFont TextBox" 
                                                    runat="server"></asp:TextBox>
                                                <cc11:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                    TargetControlID="txtVarience">
                                                </cc11:FilteredTextBoxExtender>
                                            </td>
                                            --%>
                                        </tr>
                                        
                                        <tr id="PackageTR" runat="server">
                                            <td class="tdRight" width="15">
                                                Package Size
                                            </td>
                                            <td class="tdLeft">
                                                <asp:TextBox ID="txtpackageSize" runat="server" CssClass="SmallFont TextBoxNo" MaxLength="10"
                                                    TabIndex="28" Width="125"></asp:TextBox>
                                                <%--    <asp:DropDownList ID="ddlColor" TabIndex="25" runat="server" AppendDataBoundItems="True" 
                                                    CssClass="SmallFont"   Width="127">                                                </asp:DropDownList>
                                               
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlColor"
                                                    Display="None" ErrorMessage="Please Enter Colour" SetFocusOnError="True"  InitialValue="0" ValidationGroup="YM"></asp:RequiredFieldValidator>--%>
                                            </td>
                                            <td class="tdRight" width="15">
                                                Total Imp
                                            </td>
                                            <td class="tdLeft" width="15">
                                                <asp:TextBox ID="txtTotalImp" runat="server" CssClass="SmallFont TextBoxNo" MaxLength="10"
                                                    TabIndex="29" Width="125"></asp:TextBox>
                                                <asp:RangeValidator ID="RangeValidator10" runat="server" ControlToValidate="txtTotalImp"
                                                   Display="None" ErrorMessage="Please Enter TotalImp in Numeric " MaximumValue="9999999"
                                                    MinimumValue="0" Type="Integer" ValidationGroup="YM"></asp:RangeValidator>
                                            </td>
                                            <td class="tdRight" width="15">
                                                Count Cv %*
                                            </td>
                                            <td class="tdLeft" width="25">
                                                <asp:TextBox ID="txtCountCV" runat="server" CssClass="SmallFont TextBoxNo" MaxLength="10"
                                                    TabIndex="30" Width="125" ValidationGroup="m5"></asp:TextBox>
                                                <br />
                                                <asp:RangeValidator ID="RangeValidator7" runat="server" ControlToValidate="txtCountCV"
                                                    Display="None" ErrorMessage="Please Enter CountCv in Numeric & Precision Should be 3 and Scale 3   "
                                                    MaximumValue="100.000" MinimumValue="0" Type="Double" ValidationGroup="YM"></asp:RangeValidator>
                                                <br />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtCountCV"
                                                   Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="YM"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr id="Package1TR" runat="server">
                                            <td align="right" class="tdRight" width="15">
                                                Trarif Sub Heading
                                            </td>
                                            <td class="tdLeft" width="15">
                                                <asp:TextBox ID="txttrarifSubheading" runat="server" CssClass="SmallFont TextBoxNo"
                                                    MaxLength="10" Width="125" TabIndex="31"></asp:TextBox>
                                            </td>
                                            <td class="tdRight" width="15">
                                                Article Code
                                            </td>
                                            <td id="Td21" runat="server" class="tdLeft" width="15">
                                                <asp:Button ID="btnGenerateArticalCode" runat="server" OnClick="btnGenerateArticalCode_Click"
                                                    Text="Generate" ValidationGroup="AC" TabIndex="32" />
                                                <asp:Label ID="lblArticalCode" runat="server"></asp:Label>
                                            </td>
                                            <td class="tdRight" width="15">
                                                &nbsp;
                                            </td>
                                            <td class="tdLeft" width="25">
                                                  <asp:TextBox ID="txtTPM" runat="server" AutoPostBack="true" CssClass="SmallFont TextBoxNo"
                                                    MaxLength="10" OnTextChanged="txtTPM_TextChanged" TabIndex="33" Width="125"></asp:TextBox>
                                                <asp:RangeValidator ID="RangeValidator16" runat="server" ControlToValidate="txtTPM"
                                                    Display="None" ErrorMessage="Please Enter TPM in Numeric &amp; Precision Should be 7 and Scale 2   "
                                                    MaximumValue="9999999.99" MinimumValue="0" Type="Double" ValidationGroup="YM"></asp:RangeValidator>
                                            </td>
                                        </tr>   
                                          <tr id="tropst" runat=server visible="false">
                                            <td class="tdRight" width="15%" valign="top">
                                                Opening&nbsp;Stock*
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="txtOpeningBalanceStock"
                                                    Display="Dynamic" ErrorMessage="*" SetFocusOnError="True"
                                                    ValidationGroup="YM"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="tdLeft" width="15%">
                                                <asp:TextBox ID="txtOpeningBalanceStock" runat="server" CssClass="SmallFont TextBoxNo"
                                                    MaxLength="6" TabIndex="34" Width="125" Text="0"></asp:TextBox>
                                                
                                                
                                                <asp:RangeValidator ID="RangeValidator6" runat="server" ControlToValidate="txtOpeningBalanceStock"
                                                    Display="None" ErrorMessage="Please Enter OpeningBalanceStock in Numeric &amp; Precision Should be 6 and Scale 3  "
                                                    MaximumValue="9999999999" MinimumValue="0" Type="Double" ValidationGroup="YM"></asp:RangeValidator>
                                                   
                                            </td>
                                            <td class="tdRight" width="15%" valign="top">
                                                Minimum&nbsp;Stock*
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtMimimumStock"
                                                   Display="Dynamic" ErrorMessage="*" SetFocusOnError="True"
                                                    ValidationGroup="YM"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="tdleft" width="15%">
                                                <asp:TextBox ID="txtMimimumStock" runat="server" CssClass="SmallFont TextBoxNo" MaxLength="6"
                                                    TabIndex="35" Width="125" Text="0"></asp:TextBox>
                                                <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="txtMimimumStock"
                                                    Display="None" ErrorMessage="Please Enter Minimum Stock in Numeric &amp; Precision Should be 7 and Scale 2   "
                                                    MaximumValue="9999999999" MinimumValue="0" Type="Double" ValidationGroup="YM"></asp:RangeValidator>
                                               
                                                    
                                            </td>
                                            <td class="tdRight" width="15%" valign="top">
                                                Minimum&nbsp;Procure&nbsp;Days*
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtMinimumProcureDays"
                                                    Display="Dynamic" ErrorMessage="*" SetFocusOnError="True"
                                                    ValidationGroup="YM"></asp:RequiredFieldValidator>
                                                   
                                            </td>
                                            <td class="tdleft" width="25%">
                                                <asp:TextBox ID="txtMinimumProcureDays" runat="server" CssClass="SmallFont TextBoxNo"
                                                    MaxLength="3" TabIndex="36" Width="125" Text="0"></asp:TextBox>
                                               
                                                                                            </td>
                                        </tr>
                                        <tr id="troprt" runat=server visible="false"> 
                                            <td class="tdRight" width="15" valign="top">
                                                Opening&nbsp;Rate*
                                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="txtOpeningRate"
                                                    Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" 
                                                    ValidationGroup="YM"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="tdLeft" width="15">
                                                <asp:TextBox ID="txtOpeningRate" runat="server" CssClass="SmallFont TextBoxNo" MaxLength="6"
                                                    TabIndex="37" Width="125" Text="0"></asp:TextBox>
                                              
                                                <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="txtOpeningRate"
                                                    Display="None" ErrorMessage="Please Enter OpeningRate in Numeric &amp; Precision Should be 11 and Scale 4  "
                                                    MaximumValue="9999999999999999" MinimumValue="0" Type="Double" ValidationGroup="YM"></asp:RangeValidator>
                                                    

                                            </td>
                                            <td class="tdRight" width="15">
                                                Reorder&nbsp;Level
                                            </td>
                                            <td class="tdLeft" width="15">
                                                <asp:TextBox ID="txtRecorderLevel" runat="server" CssClass="SmallFont TextBoxNo"
                                                    MaxLength="6" TabIndex="38" Width="125" Text="0"></asp:TextBox>
                                                <asp:RangeValidator ID="RangeValidator14" runat="server" ControlToValidate="txtRecorderLevel"
                                                    Display="None" ErrorMessage="Please Enter Recorder Level in Numeric &amp; Precision Should be 7 and Scale 2   "
                                                    MaximumValue="9999999999" MinimumValue="0" Type="Double" ValidationGroup="YM"></asp:RangeValidator>
                                            </td>
                                            <td class="tdRight" width="15">
                                                Reorder&nbsp;Quantity
                                            </td>
                                            <td class="tdLeft" width="25">
                                                <asp:TextBox ID="txtRecorderQuantity" runat="server" CssClass="SmallFont TextBoxNo"
                                                    MaxLength="6" TabIndex="39" Width="125" Text="0"></asp:TextBox>
                                                <asp:RangeValidator ID="RangeValidator15" runat="server" ControlToValidate="txtRecorderQuantity"
                                                    Display="None" ErrorMessage="Please Enter Recorder Quantity in Numeric &amp; Precision Should be 7 and Scale 2   "
                                                    MaximumValue="9999999999" MinimumValue="0" Type="Double" ValidationGroup="YM"></asp:RangeValidator>
                                                                                            </td>
                                        </tr>
                                        <tr id="tropfn" runat=server visible="false"> 
                                            <td id="Td24" class="tdRight" width="15"  runat=server visible="false">
                                                *Fin Code
                                            </td>
                                            <td id="Td25" class="tdLeft" width="15" runat=server visible="false">
                                                <asp:TextBox ID="txtFindDepCode" runat="server" CssClass="SmallFont TextBoxNo" MaxLength="10"
                                                    TabIndex="40" Width="125" Text="0"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtFindDepCode"
                                                    Display="None" ErrorMessage="Please Enter Fin Deb Code" SetFocusOnError="True"
                                                    ValidationGroup="YM"></asp:RequiredFieldValidator>
                                                                                            </td>
                                            <td class="tdRight" width="15">
                                                Maximum Stock
                                            </td>
                                            <td class="tdLeft" width="15" colspan="2">
                                                <asp:TextBox ID="txtMaximumStock" runat="server" CssClass="SmallFont TextBoxNo" MaxLength="8"
                                                    TabIndex="41" Width="125" Text="0"></asp:TextBox>
                                                    
                                            </td>
                                        </tr> 
                                        <tr>
                                            <td class="tdleft" colspan="6">
                                                <b>Yarn&nbsp;Description&nbsp;Details................ </b>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td class="tdRight" width="15%">
                                                Technical&nbsp;Description*
                                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator47" runat="server"
                                                    ControlToValidate="txtYarnDescription" Display="Dynamic" ErrorMessage="*"
                                                    SetFocusOnError="True" ValidationGroup="YM" ></asp:RequiredFieldValidator>
                                            </td>
                                            <td colspan="5" width="94%">
                                                <asp:TextBox ID="txtYarnDescription" runat="server" CssClass="SmallFont TextBox" 
                                                    MaxLength="50" TabIndex="6" Width="84%" ReadOnly="false" 
                                                    ontextchanged="txtYarnDescription_TextChanged" AutoPostBack="true" onkeyup="javascript:this.value = this.value.toUpperCase();"></asp:TextBox> <%--TextBoxDisplay--%>
                                                <asp:Button ID="BtnYanDescGenerate" runat="server" OnClick="BtnYanDescGenerate_Click" Text="Generate"
                                                     CssClass="SmallFont" Width="60px"/>
                                            </td>                                                                                     
                                            
                                            
                                        </tr>
                                         <tr>
                                         <td class="tdRight"  width="20%">
                                            HSN Code
                                              <%--Tariff&nbsp;Heading&nbsp;(Chapter&nbsp;No)&nbsp;:--%>
                                            </td>
                                            <td class="tdLeft"  width="20%">
                                                 <asp:TextBox ID="txtHSNCODE" runat="server" 
                                                    CssClass="SmallFont TextBoxNo" MaxLength="20"
                                                        TabIndex="24" Width="125"></asp:TextBox>
                                               <asp:DropDownList ID="ddlTariffHeading"  Visible=false Width="125px" TabIndex="24" CssClass="SmallFont TextBox UpperCase"
                            runat="server">
                        </asp:DropDownList>
                                                   <%-- <asp:RangeValidator ID="txtTariffHeadingValidator" ValidationGroup="M1" runat="server" ErrorMessage="Pls enter tariff heading."
                                                    Display="None" Type="Double" ControlToValidate="txtTariffHeading" MinimumValue="00000000"
                                                    MaximumValue="99999999"></asp:RangeValidator>--%>
                                                                                         
                                            </td>
                                            
                                             <td class="tdRight" width="20%">
                                                UOM*
                                            </td>
                                            <td class="tdLeft" width="20%">
                                                <asp:DropDownList ID="ddlUOM" runat="server" AppendDataBoundItems="True" CssClass="SmallFont"
                                                    TabIndex="11" Width="125">
                                                </asp:DropDownList>
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator50" runat="server" ControlToValidate="ddlUOM"
                                                    Display="Dynamic" ErrorMessage="*" InitialValue="0" SetFocusOnError="True"
                                                    ValidationGroup="YM"></asp:RequiredFieldValidator>
                                            </td>
                                               
                                            
                                            
                                            <td width="15%" class="tdRight"  >
                                                &nbsp;<asp:Label ID="Label2" runat="server" visible="false" Text="&nbsp;Shade*"></asp:Label>
                                            </td>
                                            <td class="tdLeft" width="15%" >
                                                <b>
                                                  <asp:DropDownList ID="ddlYarnShade" runat="server" visible="false" AppendDataBoundItems="True" CssClass="SmallFont"
                                                    TabIndex="9" Width="127" 
                                                   >
                                                </asp:DropDownList>  
                                                
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlYarnShade"
                                                   Display="Dynamic" ErrorMessage="*" InitialValue="0" SetFocusOnError="True"
                                                    ValidationGroup="AC"></asp:RequiredFieldValidator>
                                                   
                                                </b>
                                                <br />
                                            </td>
                                        </tr>
                                          
                                        <tr>
                                             <td class="tdRight" width="15%">
                                                    QC&nbsp;Required
                                                </td>
                                                <td class="tdLeft" width="15%">
                                                    <asp:RadioButtonList ID="rad_qc_req" runat="server" CssClass="SmallFont" RepeatColumns="4"
                                                        Width="100px" RepeatDirection="Horizontal" TabIndex="18" Height="11px">
                                                        <asp:ListItem Value="yes">Yes</asp:ListItem>
                                                        <asp:ListItem Value="No">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr> 
                                    </table>
                                </td>
                            </tr>
                           
                            
          <%--                  <tr>
                                <td class="td" width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td width="13%" align="right" valign="top">
                                                Yarn Supplier
                                            </td>
                                            <td class="tdLeft" width="55%" colspan="3">
                                                <asp:DropDownList ID="ddlYarnSupplier" runat="server" AppendDataBoundItems="True"
                                                    CssClass="SmallFont" TabIndex="30" Width="94%">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="13%" class="tdRight">
                                                Manufacturer
                                            </td>
                                            <td width="86%" colspan="3">
                                                <asp:TextBox ID="txtManufaturer" runat="server" CssClass="SmallFont TextBox" MaxLength="100"
                                                    TabIndex="31" Width="94%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="tdRight" width="13%">
                                                Remarks
                                            </td>
                                            <td width="86%" colspan="3">
                                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="SmallFont TextBox" MaxLength="250"
                                                    TabIndex="32" Width="94%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdRight" width="15">
                                                Sort Name:
                                            </td>
                                            <td class="tdLeft" width="15">
                                                <asp:TextBox ID="txtSortName" runat="server" CssClass="SmallFont TextBoxNo" MaxLength="16"
                                                    TabIndex="33"></asp:TextBox>
                                            </td>
                                            <td class="tdRight" width="15">
                                                Brand Name:
                                            </td>
                                            <td class="tdLeft" width="15">
                                                <asp:TextBox ID="txtBrandName" runat="server" CssClass="SmallFont TextBox" MaxLength="16"
                                                    TabIndex="34"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdRight" width="15">
                                                Status
                                            </td>
                                            <td class="tdLeft" width="15">
                                                <asp:DropDownList ID="ddlStatus" runat="server" AppendDataBoundItems="True" CssClass="SmallFont"
                                                    TabIndex="35" Width="125">
                                                 
                                                    <asp:ListItem>Running</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="ddlStatus"
                                                    Display="None" ErrorMessage="Please Select Status" InitialValue="0" SetFocusOnError="True"
                                                    ValidationGroup="YM"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="tdRight" width="15">
                                                Is Exciseable:
                                            </td>
                                            <td class="tdLeft" width="15">
                                                <asp:CheckBox ID="IsExciseable" runat="server" TabIndex="36" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>--%>
                            <tr id="YarnComp" runat="server">
                                <td class="td">
                                    <b>Yarn Compostition....</b>
                                    <table width="100%">
                                        <tr bgcolor="#006699">
                                            <td align="left" class="tdLeft SmallFont" valign="top">
                                                <span class="titleheading"><b>Yarn Composition(Blend%)</b></span>
                                            </td>
                                            <td align="left" class="tdLeft SmallFont" valign="top">
                                                <span class="titleheading"><b>Percentage</b></span>
                                            </td>
                                            <td align="left" class="tdLeft SmallFont" valign="top">
                                                <span class="titleheading"><b>Remarks</b></span>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" class="tdLeft">
                                                <asp:DropDownList ID="ddlBland2" runat="server" AppendDataBoundItems="True" CssClass="SmallFont"
                                                    TabIndex="42" Width="150px">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left" valign="top">
                                                <asp:TextBox ID="txtbland1percentage0" runat="server" CssClass="SmallFont TextBoxNo"
                                                    MaxLength="3" TabIndex="43" Width="150"></asp:TextBox>
                                                <asp:RangeValidator ID="RangeValidator5" runat="server" ControlToValidate="txtbland1percentage0"
                                                    Display="None" ErrorMessage="Bland1percentage Value Should not exceeds 100% Or Please Enter Numeric"
                                                    MaximumValue="100.000" MinimumValue="0" Type="Double" ValidationGroup="BB"></asp:RangeValidator>
                                                    
                                                    
                                            </td>
                                            <td align="left" valign="top">
                                                <asp:TextBox ID="txtBlandRemarks" runat="server" CssClass="TextBox SmallFont " TabIndex="44"
                                                    Width="260px" MaxLength="25"></asp:TextBox>
                                            </td>
                                            <td align="left" valign="top">
                                                <asp:Button ID="btmSave" runat="server" OnClick="btmSave_Click" Text="Add" ValidationGroup="BB"
                                                    TabIndex="45"  width="60px" CssClass="SmallFont" />
                                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                                                    TabIndex="46"  width="60px"  CssClass="SmallFont"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="4" valign="top">
                                                <asp:GridView ID="grdBlandTran" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                    BorderWidth="1px" CssClass="SmallFont" Font-Bold="False" OnRowCommand="grdBlandTran_RowCommand"
                                                    Width="98%">
                                                    <Columns>
                                                   
                                                        <asp:TemplateField HeaderText="Sl No." ItemStyle-VerticalAlign="top" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                            <ItemStyle VerticalAlign="Top" Width="25px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Substrate">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtblendArtilce" runat="server" Text='<%# Bind("BlendArticle") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bland Percentage">
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblBlandTotal" runat="server" Text=""></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtItemDesc" runat="server" CssClass="Label SmallFont" Text='<%# Bind("Percentage") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remarks" ItemStyle-Width="40%" HeaderStyle-Width="40%" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"  />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRemakrs" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("Remarks") %>'
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                            <ItemTemplate>
                                                                <asp:Button ID="lnkEdit" runat="server" CommandArgument='<%# Eval("UniqueId") %>'
                                                                    CommandName="indentEdit"  Text="Edit" CssClass="SmallFont" Width="60px" CausesValidation="false" />
                                                                <asp:Button ID="lnkDelete" runat="server" CommandArgument='<%# Eval("UniqueId") %>'
                                                                    CommandName="indentDelete" OnClientClick="return confirm('Are you Sure want to delete this Bland?');"
                                                                    Text="Delete" CssClass="SmallFont" Width="60px" CausesValidation="false" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <RowStyle CssClass="RowStyle SmallFont" />
                                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                                    <PagerStyle CssClass="PagerStyle" />
                                                    <HeaderStyle BackColor="#336699" CssClass="HeaderStyle SmallFont" ForeColor="White" />
                                                </asp:GridView>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trbasearticledetails"  runat=server  >
                                <td class="td" >
                                    <b>Base Quality Detail....</b>
                                    <table width="100%">
                                        <tr bgcolor="#006699">
                                            <td align="left" class="tdLeft SmallFont" valign="top" width="20%">
                                                <span class="titleheading"><b>Product Type</b></span>
                                            </td>
                                            <td align="left" class="tdLeft SmallFont" valign="top" width="20%">
                                                <span class="titleheading"><b>Quality Description&nbsp;</b></span></td>
                                            <td align="left" class="tdLeft SmallFont" valign="top" width="15%">
                                                <span class="titleheading"><b>UOM</b></span>
                                            </td>
                                            <td align="left" class="tdLeft SmallFont" valign="top" width="15%"> 
                                               <span class="titleheading"><b>Shade Code</b></span>
                                            </td>
                                            <td align="left" class="tdLeft SmallFont" valign="top" width="0%"> 
                                               <%--<span class="titleheading"><b>Basis</b></span>--%>
                                            </td>
                                            <td align="left" class="tdLeft SmallFont" valign="top" width="15%">
                                                <span class="titleheading"><b>Percentage</b></span>
                                            </td>
                                            <td align="left" class="tdLeft SmallFont" valign="top" width="20%">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="top">
                                                <asp:DropDownList ID="ddlProductType" runat="server" AppendDataBoundItems="True"
                                                    AutoPostBack="True" CssClass="SmallFont" OnSelectedIndexChanged="ddlProductType_SelectedIndexChanged"
                                                    TabIndex="47" Width="125px">
                                                </asp:DropDownList>
                                             
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator55" runat="server" ControlToValidate="ddlProductType"
                                                    Display="Dynamic" ErrorMessage="*" InitialValue="0" SetFocusOnError="True"  
                                                    ValidationGroup="BA"></asp:RequiredFieldValidator>
                                            </td>
                                            <td align="left" valign="top">
                                                <asp:DropDownList ID="txtBaseArticleCode" runat="server" AppendDataBoundItems="True"
                                                    CssClass="SmallFont" TabIndex="48" Width="250px">
                                                </asp:DropDownList>
                                               
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator58" runat="server" ControlToValidate="txtBaseArticleCode"
                                                    Display="Dynamic" ErrorMessage="*" InitialValue="0" SetFocusOnError="True"
                                                    ValidationGroup="BA"></asp:RequiredFieldValidator>
                                            </td>
                                            <td align="left" valign="top">
                                                <asp:DropDownList ID="ddlBaseUOM" runat="server" AppendDataBoundItems="True" CssClass="SmallFont"
                                                    TabIndex="49" Width="75px">
                                                </asp:DropDownList>
                                                
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator56" runat="server" ControlToValidate="ddlBaseUOM"
                                                    Display="Dynamic" ErrorMessage="*" InitialValue="0" SetFocusOnError="True"
                                                    ValidationGroup="BA"></asp:RequiredFieldValidator>
                                            </td>
                                             <td class="tdLeft" width="15%" >
                                                <b>
                                                  <asp:DropDownList ID="ddlBaseShadeCode" runat="server"  AppendDataBoundItems="True" CssClass="SmallFont"
                                                    TabIndex="9" Width="127" 
                                                   >
                                                </asp:DropDownList>  
                                                
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlYarnShade"
                                                   Display="Dynamic" ErrorMessage="*" InitialValue="0" SetFocusOnError="True"
                                                    ValidationGroup="AC"></asp:RequiredFieldValidator>
                                                   
                                                </b>
                                                <br />
                                            </td>
                                            
                                            <td align="left" valign="top">
                                                <asp:DropDownList ID="ddlBaseBasis" Visible="false" runat="server"  CssClass="SmallFont"
                                                    TabIndex="50" Width="0px">
                                                </asp:DropDownList>
                                       
                                             
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator57" runat="server" ControlToValidate="ddlBaseBasis"
                                                    Display="Dynamic" ErrorMessage="*" InitialValue="0" SetFocusOnError="True"
                                                    ValidationGroup="BA"></asp:RequiredFieldValidator>
                                            </td>
                                            <td align="left" valign="top">
                                            
                                                <asp:TextBox ID="txtValueQty" runat="server" CssClass="SmallFont TextBoxNo" MaxLength="6"
                                                    TabIndex="51" Width="100px" ></asp:TextBox >   
                                                    
                                                <asp:RangeValidator ID="RangeValidator17" runat="server" ControlToValidate="txtValueQty"
                                                    Display="None" ErrorMessage="Please Enter  Value Quantity in Numeric &amp; Precision Should be 7 and Scale 2   "
                                                    MaximumValue="9999999.99" MinimumValue="0" Type="Double" ValidationGroup="BA"></asp:RangeValidator>
                                            </td>
                                            <td align="left" valign="top">
                                                <asp:Button ID="BtnBaseSave" runat="server" OnClick="BtnBaseSave_Click" Text="Add"
                                                    ValidationGroup="BA" TabIndex="52" CssClass="SmallFont" Width="50px"/>
                                                <asp:Button ID="BtnBaseCancel" runat="server" OnClick="BtnBaseCancel_Click" Text="Cancel" CssClass="SmallFont" Width="50px"
                                                    TabIndex="53" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="6" valign="top" >
                                                <asp:GridView ID="grdBaseArticleDetail" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                    BorderWidth="1px" CssClass="SmallFont" Font-Bold="False" OnRowCommand="grdBaseArticleDetail_RowCommand"
                                                     Width="98%">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl No." ItemStyle-VerticalAlign="top" ItemStyle-Width="25px">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                            <ItemStyle VerticalAlign="Top" Width="25px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ProductType">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtProductType" runat="server" Text='<%# Bind("ProductType") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Article Code">
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtArticleCode" runat="server" CssClass="Label SmallFont" Text='<%# Bind("ArticleCode") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="quality Desc">
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtArticleDesc" runat="server" CssClass="Label SmallFont" Text='<%# Bind("ArticleDesc") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="UOM">
                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBaseUOM" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("UOM") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Shade Code" >
                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBaseYarnShade" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("YARN_SHADE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Basis" Visible="false">
                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBasis" runat="server" Visible="false" CssClass="LabelNo SmallFont" Text='<%# Bind("Basis") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Percentage">
                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblValueQty" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("ValueQty") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemStyle HorizontalAlign="Center" Width="130px" />
                                                            <ItemTemplate>
                                                                <asp:Button ID="lnkEdit0" runat="server" CommandArgument='<%# Eval("UniqueId") %>'
                                                                    CommandName="BaseEdit"  Text="Edit" CssClass="SmallFont" Width="60px" CausesValidation="false" />
                                                                <asp:Button ID="lnkDelete0" runat="server" CommandArgument='<%# Eval("UniqueId") %>'
                                                                    CommandName="BaseDelete" OnClientClick="return confirm('Are you Sure want to delete this Bland?');"
                                                                     Text="Delete" CssClass="SmallFont" Width="60px" CausesValidation="false" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <RowStyle CssClass="RowStyle SmallFont" />
                                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                                    <PagerStyle CssClass="PagerStyle" />
                                                    <HeaderStyle BackColor="#336699" CssClass="HeaderStyle SmallFont" ForeColor="White" />
                                                </asp:GridView>
                                                &nbsp;
                                            </td>
                                        </tr>                                        
                                    </table>
                                </td>
                            </tr>
                             <tr>
                                <td class="td">
                                    <b>Display Yarn Quality  ....</b>
                                    <table width="100%">
                                        <tr bgcolor="#006699">
                                            <td align="left" class="tdLeft SmallFont" valign="top">
                                                <span class="titleheading"><b>Display Yarn Quality</b></span>
                                            </td>
                                            <td align="left" class="tdLeft SmallFont" valign="top">
                                                <span class="titleheading"><b>Display Quality Desc</b></span>
                                            </td>
                                            <td align="left" class="tdLeft SmallFont" valign="top">
                                                <span class="titleheading"><b>Remarks</b></span>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" class="tdLeft">
                                                <asp:TextBox ID="txtAssocatedItemCode" runat="server" CssClass="SmallFont"
                                                    MaxLength="100"  Width="125px"></asp:TextBox>
                                            </td>
                                            <td align="left" valign="top">
                                                <asp:TextBox ID="txtAssocatedItemDesc" runat="server" CssClass="SmallFont"
                                                    MaxLength="100"  Width="250"></asp:TextBox>
                                              
                                                    
                                                    
                                            </td>
                                            <td align="left" valign="top">
                                                <asp:TextBox ID="txtAssocatedItemRemarks" runat="server" CssClass="TextBox SmallFont " TabIndex="44"
                                                    Width="150px" MaxLength="25"></asp:TextBox>
                                            </td>
                                            <td align="left" valign="top">
                                                <asp:Button ID="btnAssSave" runat="server"  Text="Add" ValidationGroup="BB"
                                                    TabIndex="45"  width="60px" CssClass="SmallFont" 
                                                    onclick="btnAssSave_Click" />
                                                <asp:Button ID="btnAssCancel" runat="server"  Text="Cancel"
                                                    TabIndex="46"  width="60px"  CssClass="SmallFont" 
                                                    onclick="btnAssCancel_Click"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="4" valign="top">
                                                <asp:GridView ID="grdAssociatedYarn" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                    BorderWidth="1px" CssClass="SmallFont" Font-Bold="False" 
                                                     Width="98%" onrowcommand="grdAssociatedYarn_RowCommand">
                                                    <Columns>
                                                   
                                                        <asp:TemplateField HeaderText="Sl No." ItemStyle-VerticalAlign="top" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                            <ItemStyle VerticalAlign="Top" Width="25px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Yarn Code" ItemStyle-Width="10%" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"  Width="100px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblYarnMasterCode" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("YARN_CODE") %>'
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Display&nbsp;Yarn&nbsp;  Quality">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAssocatedItemCode" runat="server" Text='<%# Bind("ASS_YARN_CODE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"  Width="100px"/>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Display Quality&nbsp;Yarn&nbsp;Desc">
                                                           
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAssociatedItemDesc" runat="server" CssClass="Label SmallFont" Text='<%# Bind("ASS_YARN_DESC") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                            <ItemTemplate>
                                                                <asp:Button ID="lnkAssEdit" runat="server" CommandArgument='<%# Eval("AssUniqueId") %>'
                                                                    CommandName="indentEdit"  Text="Edit" CssClass="SmallFont" Width="60px" CausesValidation="false" />
                                                                <asp:Button ID="lnkAssDelete" runat="server" CommandArgument='<%# Eval("AssUniqueId") %>'
                                                                    CommandName="indentDelete" OnClientClick="return confirm('Are you Sure want to delete this Associated Yarn?');"
                                                                    Text="Delete" CssClass="SmallFont" Width="60px" CausesValidation="false" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <RowStyle CssClass="RowStyle SmallFont" />
                                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                                    <PagerStyle CssClass="PagerStyle" />
                                                    <HeaderStyle BackColor="#336699" CssClass="HeaderStyle SmallFont" ForeColor="White" />
                                                </asp:GridView>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            
                               <tr id="tropeningbalstock"  runat="server" visible="false"  >
                                <td class="td" >
                                    <b>Opening Balance Detail....</b>
                  <table width="100%" >
                 <tr class="TableHeader td" width="100%">
                 <td  width="5%" class="tdLeft SmallFont"> 
                  <span class="titleheading"><b>Party</b></span>
                 </td>
                 <td  width="5%" class="tdLeft SmallFont"> 
                  <span class="titleheading"><b>Color</b></span>
                 </td>
                 <td  width="10%" class="tdLeft SmallFont"> <span class="titleheading"><b>RGB</b></span> &nbsp;
                 <asp:TextBox ID="txtRGBColor" runat="server" CssClass="TextBox SmallFont" Width="15px"
                             TabIndex="17" MaxLength="9" Enabled="False"></asp:TextBox>
                 </td>
                 <td width="5%" class="tdLeft SmallFont">
                         <span class="titleheading"><b>Location</b></span></td>
                         <td width="5%" class="tdLeft SmallFont">
                         <span class="titleheading"><b>Store</b></span></td>
                         <td width="5%" class="tdLeft SmallFont">
                         <span class="titleheading"><b>Lot No</b></span></td>
                         <td width="5%" class="tdLeft SmallFont">
                         <span class="titleheading"><b>Grade</b></span></td>
                  
                 <td width="5%" class="tdLeft SmallFont"> <span class="titleheading"><b>Cartoons</b></span>
                 </td>
                 <td width="5%" class="tdLeft SmallFont"> <span class="titleheading"><b>Op.Stock</b></span>
                 </td>
                 <td width="5%" class="tdLeft SmallFont"> <span class="titleheading"><b>Op&nbsp;Rate</b></span>
                 </td>
                    <td  class="tdLeft SmallFont"> <span class="titleheading"></span>
                 </td>
                 </tr>
                 
                 <tr>
                     <td width="5%">
                   <cc2:ComboBox ID="txtPartyCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="txtPartyCode_LoadingItems" DataTextField="PRTY_CODE" DataValueField="Address"
                            EmptyText="Select Vendor" OnSelectedIndexChanged="txtPartyCode_SelectedIndexChanged"
                            EnableVirtualScrolling="true" Width="100px" MenuWidth="400px" Height="200px"  >
                            <HeaderTemplate>
                                <div class="header c5">
                                    Code</div>
                                <div class="header c3">
                                    NAME</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c5">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                <div class="item c3">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>

                                  <asp:HiddenField ID="txtPartyName" runat="server"  />                                      
                 
                                            
                 </td>  
                 <td width="5%">
                 <cc2:ComboBox ID="cmbShade" runat="server" AutoPostBack="True" CssClass="smallfont"
                                                DataTextField="SHADE_FAMILY_NAME" DataValueField="SHADE_NAME" EnableLoadOnDemand="True"
                                                MenuWidth="300" EnableVirtualScrolling="true" OpenOnFocus="true" TabIndex="54"
                                                Height="200px" Visible="true" Width="100px" OnLoadingItems="cmbShade_LoadingItems"
                                                OnSelectedIndexChanged="cmbShade_SelectedIndexChanged">
                                                <HeaderTemplate>                                                  
                                                    <div class="header d2">
                                                        Shade Family Name</div>                                                  
                                                    <div class="header d4">
                                                        Shade Name</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>                                                   
                                                    <div class="item d2">
                                                        <%# Eval("SHADE_FAMILY_NAME")%></div>                                                    
                                                    <div class="item d4">
                                                        <%# Eval("SHADE_NAME")%></div>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Displaying items
                                                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                                    <%# Container.ItemsCount %>.
                                                </FooterTemplate>
                                            </cc2:ComboBox>   
                                            <asp:TextBox ID="txtShadeFamily" runat="server" Width="30px" Visible="false" ></asp:TextBox>      
                                            <asp:TextBox ID="txtShade" runat="server" Width="30px" Visible="false"></asp:TextBox>                                    
                 
                                            
                 </td>  
                 <td width="10%">
                  <asp:TextBox ID="txtRGB" runat="server" CssClass="TextBox SmallFont" Width="60px"
                             TabIndex="55" MaxLength="11" AutoPostBack="true" 
                         ontextchanged="txtRGB_TextChanged"></asp:TextBox>
                                               
                 
                 </td>     
                    <td>
                     <asp:DropDownList ID="ddlLocation" runat="server" TabIndex="56" CssClass="SmallFont" 
                Width="80px">
            </asp:DropDownList>
                     </td>
                     <td>
                     
                   <asp:DropDownList ID="ddlStore" runat="server" TabIndex="57" CssClass="SmallFont" 
                Width="80px">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlStore"
                                                    Display="Dynamic" ErrorMessage="*" InitialValue="" SetFocusOnError="True"
                                                    ></asp:RequiredFieldValidator>
            
                     </td>      
                     <td >
                  <asp:TextBox ID="txtLotNo" runat="server" CssClass="TextBox SmallFont" Width="60px"
                              MaxLength="15"></asp:TextBox>
                 </td>
                 <td >
                  <asp:TextBox ID="txtGrade" runat="server" CssClass="TextBox SmallFont" Width="50px"
                            MaxLength="15"></asp:TextBox>
                 </td> 
                 <td align="center" width="5%">
                  <asp:Button ID="btnLotDetails" runat="server" Text="Cartoons"    TabIndex="59" 
                         Width="60px" CssClass="SmallFont" onclick="btnLotDetails_Click"  />                                           
                  
                 </td>  
                  <td width="8%">
                  <asp:TextBox ID="txtOpeningBal" runat="server" CssClass="TextBox SmallFont" Width="60px"
                             TabIndex="58" MaxLength="7"></asp:TextBox>
                 </td>
                 
                 <td width="5%">
                  <asp:TextBox ID="txtOpenRate" runat="server" CssClass="TextBox SmallFont" Width="50px"
                             TabIndex="60" MaxLength="5"></asp:TextBox>
                             <asp:TextBox ID="txtMinStock" runat="server" CssClass="TextBox SmallFont" Width="50px"
                             TabIndex="61" MaxLength="5" Visible="false"></asp:TextBox>
                             <asp:TextBox ID="txtMaxStock" runat="server" CssClass="TextBox SmallFont" Width="50px"
                             TabIndex="62" MaxLength="5" Visible="false"></asp:TextBox>
                   <asp:HiddenField ID="txtCartoons" runat="server" />
                             <asp:HiddenField ID="txtGrossWt" runat="server" />         
                             <asp:HiddenField ID="txtTareWt" runat="server" />
                            <asp:HiddenField ID="txtNoOfUnit" runat="server"      />
                            <asp:HiddenField ID="txtWeightOfUnit" runat="server"   />
                 </td>
                   
                 
                  <td  width="14%"> 
                  
                  <asp:Button ID="lbtnsavedetail" runat="server" Text="Add"   
                          onclick="lbtnsavedetailColor_Click" TabIndex="63" Width="50px" 
                          CssClass="SmallFont"  />                                           
                        <asp:Button ID="lbtnCancel" runat="server" Text="Cancel" TabIndex="64"
                            OnClick="lbtnCancel_Click1" Width="50px"  CssClass="SmallFont" />
                 </td>
                 </tr>
                 <tr >
                 <td colspan="13">
                    <asp:GridView ID="grdColorDetail" runat="server" CssClass="SmallFont" Font-Bold="False"
                    BorderWidth="1px"  
                    AutoGenerateColumns="False" AllowSorting="True" Width="98%" 
                         onrowcommand="grdColorDetail_RowCommand" 
                         onrowdatabound="grdColorDetail_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No." ItemStyle-VerticalAlign="top">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="2%" />
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </asp:TemplateField>  
                        <asp:TemplateField HeaderText="Party">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtParty" runat="server" Text='<%# Bind("PRTY_NAME") %>' ToolTip='<%# Bind("PRTY_CODE") %>' CssClass="Label SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>                   
                        <asp:TemplateField HeaderText="Shade Family">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtShadeFamily" runat="server" Text='<%# Bind("SHADE_FAMILY") %>' CssClass="Label SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Shade">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtShade" runat="server" Text='<%# Bind("SHADE") %>' CssClass="Label SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="RGB">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtRGB" runat="server" Text='<%# Bind("RGB") %>' CssClass="Label SmallFont"></asp:Label>
                                <asp:TextBox ID="txtRGBColor" runat="server" CssClass="TextBox SmallFont" Width="15px" Enabled="False"></asp:TextBox>
                 
                       </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Location">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtlocation" runat="server" Text='<%# Bind("LOCATION") %>' CssClass="LabelNo SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Store">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtstore" runat="server" Text='<%# Bind("STORE") %>' CssClass="LabelNo SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>  
                         <asp:TemplateField HeaderText="Lot No">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtLotNo" runat="server" Text='<%# Bind("LOT_NO") %>' CssClass="LabelNo SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Grade">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="7%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtGrade" runat="server" Text='<%# Bind("GRADE") %>' CssClass="LabelNo SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Op.Stock">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%"></ItemStyle>
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="txtOpeningStock" runat="server" Text='<%# Bind("OP_BAL_STOCK") %>'
                                    CssClass="LabelNo SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Op.Rate">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtOpeningRate" runat="server" Text='<%# Bind("OP_RATE") %>' CssClass="LabelNo SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>   
                                                <asp:TemplateField HeaderText="Min Stock" Visible="false">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txt_MinStock" runat="server" Text='<%# Bind("MIN_STOCK") %>' CssClass="LabelNo SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                           <asp:TemplateField HeaderText="Max Stock" Visible="false">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txt_MaxStock" runat="server" Text='<%# Bind("MAX_STOCK") %>' CssClass="LabelNo SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cops" Visible="false">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txt_NO_OF_UNIT" runat="server" Text='<%# Bind("NO_OF_UNIT") %>' CssClass="LabelNo SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Avg Weight" Visible="false">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txt_WEIGHT_OF_UNIT" runat="server" Text='<%# Bind("WEIGHT_OF_UNIT") %>' CssClass="LabelNo SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cortoons">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtCortoons" runat="server" Text='<%# Bind("CARTONS") %>' CssClass="LabelNo SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="G.Wt.">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtGrossWt" runat="server" Text='<%# Bind("GROSS_WT") %>' CssClass="LabelNo SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="T.Wt." >
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtTareWt" runat="server" Text='<%# Bind("TARE_WT") %>' CssClass="LabelNo SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                              
                         <asp:TemplateField HeaderText="Lot Details">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                 <asp:LinkButton ID="lnkunige" runat="server" CssClass="Label SmallFont" Text="View"
                                    CommandArgument='<%# Bind("UNIQUEID") %>'>
                                </asp:LinkButton>
                                <asp:Panel ID="pnlBOM" runat="server" BackColor="#C5E7F1" BorderColor="Desktop" BorderStyle="Solid"
                                    BorderWidth="5px" HorizontalAlign="Left">
                                    <asp:GridView ID="grdBOM" runat="server" AutoGenerateColumns="False">
                                        <Columns>
                                    <asp:TemplateField HeaderText="Sl&nbsp;No." ItemStyle-VerticalAlign="top" ItemStyle-Width="25px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtSubTrnUNIQUE_ID" runat="server" Text='<%# Bind("UNIQUE_ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="PI No" Visible="false">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbtpino" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("PI_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Lot&nbsp;No">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                     
                                                    <ItemTemplate>
                                                       <asp:Label ID="lblLotNO" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("LOT_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Grade">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBOMUOM" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("GRADE") %>'></asp:Label>
                                                    </ItemTemplate>
                                              <FooterTemplate>
                                                    <asp:Label ID="flblBOMUOM" runat="server" CssClass="LabelNo SmallFont"  >Total:</asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Carton&nbsp;No">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                       <asp:Label ID="lbtcartonno" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("CARTON_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                    <asp:Label ID="flbtcartonno" runat="server" CssClass="LabelNo SmallFont" ></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cops">
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNoUnit" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("NO_OF_UNIT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                    <asp:Label ID="flblNoUnit" runat="server" CssClass="LabelNo SmallFont" ></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Gross&nbsp;Wt">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGrossWt" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("GROSS_WT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                    <FooterTemplate>
                                                    <asp:Label ID="flblGrossWt" runat="server" CssClass="LabelNo SmallFont" ></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tare&nbsp;Wt">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTareWt" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TARE_WT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                    <FooterTemplate >
                                                    <asp:Label ID="flblTareWt" runat="server" CssClass="LabelNo SmallFont" ></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Net&nbsp;Wt">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQTY" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TRN_QTY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                      <FooterTemplate>
                                                    <asp:Label ID="flblQTY" runat="server" CssClass="LabelNo SmallFont" ></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="UOM">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUom" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("UOM_OF_UNIT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bar&nbsp;Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBarcodeNo" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("BARCODE_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date of Mfd" Visible="false">
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBOMValueQty" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("DATE_OF_MFG") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                               
                                                <asp:TemplateField HeaderText="WeightofUnit">
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWeightofUnit" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("WEIGHT_OF_UNIT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="SmallFont" />
                                        <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                                    </asp:GridView>
                                </asp:Panel>
                                <cc11:HoverMenuExtender ID="hmeBOM" runat="server" PopupControlID="pnlBOM" TargetControlID="lnkunige"
                                    PopupPosition="Left">
                                </cc11:HoverMenuExtender>
                            </ItemTemplate>
                        </asp:TemplateField>              
                        <asp:TemplateField >
                                    <ItemStyle HorizontalAlign="Center" ></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Button ID="lnkEdit" Text="Edit" runat="server" CommandName="colorEdit" 
                                            CommandArgument='<%# Eval("UniqueId") %>' Width="45px" CssClass="SmallFont" CausesValidation="false" >
                                            </asp:Button>
                                            
                                            <asp:Button ID="lnkDelete"
                                                runat="server" Text="Delete" CommandName="colorDelete"  CommandArgument='<%# Eval("UniqueId") %>' Width="45px" CssClass="SmallFont" CausesValidation="false" >
                                            </asp:Button>
                                    </ItemTemplate>
                                </asp:TemplateField>
                    </Columns>
                    <RowStyle CssClass="SmallFont" />
                    <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                </asp:GridView>
                   </td>
                
                 </tr>
                 </table>   
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
         <cc11:FilteredTextBoxExtender ID="FiltertxtMinimumProcureDays" runat="server"  TargetControlID="txtMinimumProcureDays"   FilterType="Custom, Numbers"  />
                                                    <cc11:FilteredTextBoxExtender ID="Filtertxtbland1percentage0" runat="server"  TargetControlID="txtbland1percentage0"   FilterType="Custom, Numbers" ValidChars="."/>                                                 
                                            <cc11:FilteredTextBoxExtender ID="FiltertxtValueQty" runat="server"      TargetControlID="txtValueQty"    FilterType="Custom, Numbers"   ValidChars="."    />
                                            <cc11:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
            TargetControlID="txtRGB"
             Mask="999,999,999"
             MessageValidatorTip="true"             
             MaskType="Number"
             InputDirection="LeftToRight"            
             ErrorTooltipEnabled="True" />
                                           
               
           <%-- <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                ShowSummary="False" ValidationGroup="YM" />
            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                ShowSummary="False" ValidationGroup="AC" />
            <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="True"
                ShowSummary="False" ValidationGroup="BB" />
            <asp:ValidationSummary ID="ValidationSummary4" runat="server" ShowMessageBox="True"
                ShowSummary="False" ValidationGroup="BA" />--%>
        </ContentTemplate>
        <Triggers>
        <asp:PostBackTrigger ControlID="txtLotNo" />
         <asp:PostBackTrigger ControlID="txtGrade" />
         <asp:PostBackTrigger ControlID="txtYarnDescription" />
        </Triggers>
    </asp:UpdatePanel>
    
</asp:Content>
