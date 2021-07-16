<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="VendorMaster.aspx.cs" Inherits="Admin_VendorMaster" Title="Vendor Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<asp:Content ID="Head2" runat="server" ContentPlaceHolderID="Head1">

<script type="text/javascript" src="../../../javascript/jquery-1.4.1.min.js"></script>
<script src="../../../javascript/jquery-ui.min.js" type = "text/javascript"></script>
<link href="../../../javascript/jquery-ui.css" rel = "Stylesheet" type="text/css" />
<script type="text/javascript">
    $(document).ready(function() {
        $("#<%=txtVendorName.ClientID %>").autocomplete({
            source: function(request, response) {
                $.ajax({
                    url: '<%=ResolveUrl("~/MOM.asmx/GetPartyDescription") %>',
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



        $("#<%=txtVendState.ClientID %>").autocomplete({
            source: function(request, response) {
                $.ajax({
                url: '<%=ResolveUrl("~/MOM.asmx/GetMOMState") %>',
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

        $(function() {

        $("#<%=uploadPan.ClientID%>").change(function() {
            
                var files = !!this.files ? this.files : [];
                if (!files.length || !window.FileReader) return; // no file selected, or no FileReader support

                if (/^image/.test(files[0].type)) { // only image file
                    var reader = new FileReader(); // instance of the FileReader
                    reader.readAsDataURL(files[0]); // read the local file

                    reader.onloadend = function() { // set image data as background of div                   
                    $("#<%=imgPan.ClientID%>").attr('src', this.result);
                    }
                }
            });


            $("#<%=uploadLST.ClientID%>").change(function() {

                var files = !!this.files ? this.files : [];
                if (!files.length || !window.FileReader) return; // no file selected, or no FileReader support

                if (/^image/.test(files[0].type)) { // only image file
                    var reader = new FileReader(); // instance of the FileReader
                    reader.readAsDataURL(files[0]); // read the local file

                    reader.onloadend = function() { // set image data as background of div                   
                        $("#<%=imgLST.ClientID%>").attr('src', this.result);
                    }
                }
            });


            $("#<%=uploadCST.ClientID%>").change(function() {

                var files = !!this.files ? this.files : [];
                if (!files.length || !window.FileReader) return; // no file selected, or no FileReader support

                if (/^image/.test(files[0].type)) { // only image file
                    var reader = new FileReader(); // instance of the FileReader
                    reader.readAsDataURL(files[0]); // read the local file

                    reader.onloadend = function() { // set image data as background of div                   
                        $("#<%=imgCST.ClientID%>").attr('src', this.result);
                    }
                }
            });
        });


    });

    
</script>  


<style type="text/css">
        .item
        {
            position: relative !important;
            display: -moz-inline-stack;
            display: inline-block;
            zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;}
        .header
        {
            margin-left: 2px;
        }
        .c1
        {
            width: 80px;
        }
        .c2
        {
            margin-left: 4px;
            width: 120px;
        }
        .c3
        {
            margin-left: 4px;
            width: 100px;
        }
        .c4
        {
            margin-left: 4px;
            width: 100px;
        }
    .style1
    {
        width: 30%;
    }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <table align="left" class="tContentArial" width="95%">
                <tr>
                    <td align="left" valign="top" class="td">
                        <table align="left">
                            <tr>
                                <td id="tdSave" align="left" width="48" runat="server">
                                    <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="Save" Width="48 px" Height="41"
                                        ImageUrl="~/CommonImages/save.jpg" OnClick="imgbtnSave_Click" ValidationGroup="M1"
                                        TabIndex="52"></asp:ImageButton>
                                </td>
                                <td id="tdUpdate" runat="server" width="48 px">
                                    <asp:ImageButton ID="imgbtnUpdate" runat="server" Width="48" Height="41" ToolTip="Update"
                                        ImageUrl="~/CommonImages/edit1.jpg" OnClick="imgbtnUpdate_Click" ValidationGroup="M1"
                                        TabIndex="52"></asp:ImageButton>
                                </td>
                                <td id="tdDelete" runat="server" width="48 px">
                                    <asp:ImageButton ID="imgbtnDelete" runat="server" Width="48" Height="41" ToolTip="Delete"
                                        ImageUrl="~/CommonImages/del6.png" OnClick="imgbtnDelete_Click" TabIndex="53">
                                    </asp:ImageButton>
                                </td>
                                <td id="tdFind" runat="server" align="left" width="48 px">
                                    <asp:ImageButton ID="imgbtnFind" runat="server" Width="48" Height="41" ToolTip="Find"
                                        ImageUrl="~/CommonImages/link_find.png" OnClick="imgbtnFind_Click" TabIndex="54">
                                    </asp:ImageButton>
                                </td>
                                <td id="tdList" runat="server">
                                                <asp:ImageButton ID="imgbtnList" runat="server" ImageUrl="~/CommonImages/list.jpg" TabIndex="67"
                                                    ToolTip="Poy Master List" onclick="imgbtnList_Click" />
                                            </td>
                                <td>
                                    <asp:ImageButton ID="imgbtnPrint" runat="server" Width="48" Height="41" ToolTip="Print"
                                        ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click" TabIndex="55">
                                    </asp:ImageButton>&nbsp;
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" ToolTip="Clear"
                                        ImageUrl="~/CommonImages/clear.jpg" OnClick="imgbtnClear_Click" TabIndex="56">
                                    </asp:ImageButton>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgbtnExit" runat="server" Width="48" Height="41" ToolTip="Exit"
                                        ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click" TabIndex="57">
                                    </asp:ImageButton>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgbtnHelp" runat="server" Width="48" Height="41" ToolTip="Help"
                                        ImageUrl="~/CommonImages/link_help.png" OnClick="imgbtnHelp_Click" TabIndex="58">
                                    </asp:ImageButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="TableHeader td" width="100%">
                        <span class="titleheading"><b>Vendor Master Entry</b></span>
                    </td>
                </tr>
                <tr>
                    <td class="td" width="100%">
                        <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="M1" />
                        </span>
                    </td>
                </tr>
               
                <tr>
                    <td class="td" width="100%">
                        <table width="100%">
                           <tr>
                                <td align="right" valign="top" style="font-weight: bold" width="20%" class="tdRight">
                                     <font color="#ff0000">*</font>Vendor Code&nbsp;
                                </td>
                                <td class="tdleft" width="30%">
                                     <asp:TextBox ID="txtVendCode" TabIndex="1" runat="server" CssClass="TextBoxNo SmallFont UpperCase"
                                       Width="200px" MaxLength="10" TextMode="singleLine" AutoCompleteType="Disabled" ></asp:TextBox>
                                    <cc2:ComboBox ID="ddlFindVendor" runat="server" CssClass="SmallFont" Width="200px"
                                        MenuWidth="450px" Height="180px" EmptyText="Search Vendor" EnableLoadOnDemand="true"
                                        OnLoadingItems="ddlFindVendor_LoadingItems" AutoPostBack="True" OnSelectedIndexChanged="ddlFindVendor_SelectedIndexChanged"
                                        TabIndex="1">
                                        <HeaderTemplate>
                                            <div class="header c1">
                                                Code</div>
                                            <div class="header c2">
                                                Name</div>
                                            <div class="header c3">
                                                Group Code</div>
                                            <div class="header c4">
                                                Category</div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="item c1">
                                                <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                            <div class="item c2">
                                                <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                            <div class="item c3">
                                                <asp:Literal runat="server" ID="Container3" Text='<%# Eval("PRTY_GRP_CODE") %>' /></div>
                                            <div class="item c4">
                                                <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("VENDOR_CAT_CODE") %>' /></div>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Displaying items
                                            <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                            out of
                                            <%# Container.ItemsCount %>.
                                        </FooterTemplate>
                                    </cc2:ComboBox>
                                   
                                    <asp:RequiredFieldValidator ID="rfv1" Display="Dynamic" runat="server" ErrorMessage="Pls enter vendor code."
                                        ControlToValidate="txtVendCode" ValidationGroup="M1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        
                                </td>
                                <td align="right" valign="top" style="font-weight: bold" width="15%">
                                    <font color="#ff0000">*</font>Vendor Name&nbsp;
                                </td>
                                <td class="tdleft" width="35%">
                                     <asp:TextBox ID="txtVendorName" runat="server" CssClass="TextBox SmallFont UpperCase"
                                        MaxLength="100" TabIndex="2" TextMode="singleLine" Width="200px"
                                        AutoCompleteType="Disabled"></asp:TextBox>
                                   
                                    <asp:RequiredFieldValidator ID="rfv2" Display="Dynamic" runat="server" ErrorMessage="Pls enter vendor name."
                                        ControlToValidate="txtVendorName" ValidationGroup="M1" SetFocusOnError="True"></asp:RequiredFieldValidator>  </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" style="font-weight: bold" width="20%" class="tdRight">
                                     <font color="#ff0000">*</font>Vendor Group Code&nbsp;
                                </td>
                                <td class="tdleft" width="30%">
                                   <%-- <cc2:ComboBox ID="ddlPartyGroupCode" runat="server" CssClass="SmallFont" Width="200px"
                                        Height="180px" EmptyText="Select vendor group..." EnableLoadOnDemand="true" OnLoadingItems="ddlPartyGroupCode_LoadingItems"
                                        TabIndex="3" />--%>
                                    <asp:DropDownList ID="ddlPartyGroupCode" runat="server" CssClass="SmallFont" 
                                        Width="200px" Height="20px" TabIndex="3" 
                                        onselectedindexchanged="ddlPartyGroupCode_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                        
                                </td>
                                <td align="right" valign="top" style="font-weight: bold" width="15%">
                                    <font color="#ff0000">*</font>Vendor Category&nbsp;
                                </td>
                                <td class="tdleft" width="35%">
                                    <%--<cc2:ComboBox ID="ddlVendorCategory" runat="server" CssClass="SmallFont" Width="200px"
                                        Height="180px" EmptyText="Select vendor Category..." EnableLoadOnDemand="true"
                                        OnLoadingItems="ddlVendorCategory_LoadingItems" TabIndex="4" />--%>
                                    <asp:DropDownList ID="ddlVendorCategory" runat="server" CssClass="SmallFont" 
                                        Width="200px" Height="20px" TabIndex="5" AutoPostBack="true" 
                                        onselectedindexchanged="ddlVendorCategory_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv3" Display="Dynamic" runat="server" ErrorMessage="Pls enter vendor name."
                                        ControlToValidate="ddlVendorCategory" ValidationGroup="M1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                            
                                <td align="right" class="tdRight" style="font-weight: bold" valign="top" 
                                    width="20%">
                                    <font color="#ff0000">*</font>Vendor A/C Type&nbsp;
                                </td>
                                <td class="tdleft" width="30%">                                    
                                    <asp:DropDownList ID="ddlAccountType" runat="server"  
                                        CssClass="SmallFont" Height="20px" TabIndex="4" Width="200px">
                                    </asp:DropDownList>
                                </td>
                            
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="td" width="100%">
                        <table width="100%">
                            <tr>
                                <td class="tdRight" valign="top" width="20%">
                                    Address 1&nbsp;
                                </td>
                                <td class="tdLeft" width="30%">
                                    <asp:TextBox ID="txtVendAdd1" runat="server" CssClass="TextBox SmallFont" MaxLength="60"
                                        TabIndex="5" TextMode="singleLine" Width="200px"></asp:TextBox>
                                </td>
                                <td class="tdRight" valign="top" width="15%">
                                    Address 2&nbsp;
                                </td>
                                <td class="tdLeft" width="35%">
                                    <asp:TextBox ID="txtVendAdd2" runat="server" CssClass="TextBox SmallFont" MaxLength="60"
                                        TabIndex="6" TextMode="singleLine" Width="200px"></asp:TextBox>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="tdRight" valign="top" width="20%">
                                    Address 3&nbsp;
                                </td>
                                <td class="tdLeft" width="30%">
                                    <asp:TextBox ID="txtVendAdd3" runat="server" CssClass="TextBox SmallFont" MaxLength="60"
                                        TabIndex="5" TextMode="singleLine" Width="200px"></asp:TextBox>
                                </td>
                                <td class="tdRight" valign="top" width="15%">
                                  
                                </td>
                                <td class="tdLeft" width="35%">
                                   
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" width="20%" class="tdRight">
                                    City&nbsp;
                                </td>
                                <td class="tdleft" width="30%">
                                    <asp:TextBox ID="txtVendCity" runat="server" CssClass="TextBox SmallFont" MaxLength="40"
                                        TabIndex="7" TextMode="singleLine" Width="200px"></asp:TextBox>
                                </td>
                                <td align="right" valign="top" width="15%" class="tdRight">
                                    State
                                </td>
                                <td class="tdleft" width="35%">
                                    <asp:TextBox ID="txtVendState" runat="server" CssClass="TextBox SmallFont" MaxLength="40"
                                        TabIndex="8" TextMode="singleLine" Width="200px"></asp:TextBox>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" width="20%">
                                    Country&nbsp;
                                </td>
                                <td class="tdleft" width="30%">
                                    <asp:TextBox ID="txtVendCountry" runat="server" CssClass="TextBox SmallFont" MaxLength="50"
                                        TabIndex="9" TextMode="singleLine" Width="200px"></asp:TextBox>
                                </td>
                                <td align="right" valign="top" width="15%" class="tdRight">
                                    <font color="#ff0000">*</font>Pin No.&nbsp;
                                </td>
                                <td class="tdleft" width="35%">
                                    <asp:TextBox ID="txtVendPostalCode" runat="server" CssClass="TextBoxNo SmallFont"
                                        MaxLength="6" TabIndex="10" TextMode="singleLine" Width="200px"></asp:TextBox>
                                    <asp:RangeValidator ID="rvVendorPostalCode" runat="server" ControlToValidate="txtVendPostalCode"
                                        ErrorMessage="Enter Corrent Postal Code" MaximumValue="999999" MinimumValue="000001"
                                        TabIndex="10" Type="Double" ValidationGroup="M1" Display="Dynamic" SetFocusOnError="True"></asp:RangeValidator>
                                    <asp:RequiredFieldValidator ID="rfVendorPostalCode" runat="server" ControlToValidate="txtVendPostalCode"
                                        ErrorMessage="Please Enter Postal Code." ValidationGroup="M1" Display="Dynamic"
                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="td" width="100%">
                        <table width="100%">
                            <tr>
                                <td align="right" valign="top" width="20%" class="tdRight">
                                    Phone No&nbsp;
                                </td>
                                <td class="tdleft" width="30%">
                                    <asp:TextBox ID="txtVendPhone" runat="server" CssClass="TextBoxNo SmallFont" MaxLength="60"
                                        TabIndex="11" TextMode="singleLine" Width="200px"></asp:TextBox>
                                    <%--<asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtVendPhone"
                                        Display="Dynamic" ErrorMessage="Invalid Phone No." 
                                        MaximumValue="999999999999999" MinimumValue="000000000000000"
                                        SetFocusOnError="True" Type="Double"></asp:RangeValidator>--%>
                                </td>
                                <td align="right" valign="top" width="15%">
                                    Mobile No&nbsp;
                                </td>
                                <td class="tdleft" width="35%">
                                    <asp:TextBox ID="txtVendMobile" runat="server" CssClass="TextBoxNo SmallFont" MaxLength="20"
                                        TabIndex="12" TextMode="singleLine" Width="200px"></asp:TextBox>
                                    <%--  <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtVendMobile"
                                        Display="Dynamic" ErrorMessage="Invalid Mobile No." MaximumValue="999999999999"
                                        MinimumValue="000000000000" SetFocusOnError="True" Type="Double"></asp:RangeValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" width="20%" class="tdRight">
                                    Fax No&nbsp;
                                </td>
                                <td class="tdleft" width="30%">
                                    <asp:TextBox ID="txtVendFax" runat="server" CssClass="TextBoxNo SmallFont" MaxLength="20"
                                        TabIndex="13" TextMode="singleLine" Width="200px"></asp:TextBox>
                                </td>
                                <td align="right" valign="top" width="15%" class="tdRight">
                                    Email Id&nbsp;
                                </td>
                                <td class="tdleft" width="35%">
                                    <asp:TextBox ID="txtVendEmail" runat="server" CssClass="TextBox SmallFont" MaxLength="200"
                                        TabIndex="14" TextMode="singleLine" Width="200px"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="rfv5" runat="server" Display="Dynamic" ErrorMessage="Pls Enter Email Id !"
                                        ControlToValidate="txtVendEmail" ValidationGroup="M1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                   --%> 
                                   <asp:RegularExpressionValidator ID="revEmail2" runat="server" ControlToValidate="txtVendEmail"
                                        ErrorMessage="Please enter valid Email Id" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ValidationGroup="M1" Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" width="20%" class="tdRight">
                                    Website&nbsp;
                                </td>
                                <td class="tdleft" colspan="3" width="80%">
                                    <asp:TextBox ID="txtVendWebsite" runat="server" CssClass="TextBox SmallFont" MaxLength="100"
                                        TabIndex="15" TextMode="singleLine" Width="78%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="td" width="100%">
                        <table width="100%">
                            <tr>
                                <td class="tdRight" width="20%">
                                    Pan No
                                </td>
                                <td class="tdleft">
                                
                                    <asp:TextBox ID="txtVendPan" runat="server" CssClass="TextBoxNo SmallFont UpperCase"
                                        MaxLength="20" TabIndex="16" TextMode="singleLine" Width="200px"></asp:TextBox>
                                    <br/>
                                  
                                    <asp:Image ID="imgPan" runat="server" Height="20" Width="20" />
                                    <asp:FileUpload ID="uploadPan" runat="server" CssClass="SmallFont"  TabIndex="18"
                                        Width="200px" />    
                                         <asp:RequiredFieldValidator ID="rfvuploadPan" ValidationGroup="M1" runat="server" Display="Dynamic"
                                          ErrorMessage="*PAN image required." ControlToValidate="uploadPan"></asp:RequiredFieldValidator>                               
                                    </div>
                                   
                                </td>
                                <td class="tdRight" width="15%">
                                    Tin Type&nbsp;
                                </td>
                                <td class="tdleft" width="35%">
                                    <asp:TextBox ID="txtVendTinType" runat="server" CssClass="TextBox SmallFont" MaxLength="5"
                                        TabIndex="17" TextMode="singleLine" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdRight" width="20%">
                                    LST No&nbsp;
                                </td>
                                <td class="style1">
                                    <asp:TextBox ID="txtVendLST_No" runat="server" CssClass="TextBoxNo SmallFont UpperCase"
                                        MaxLength="50" TabIndex="19" TextMode="singleLine" Width="200px"></asp:TextBox>
                                         <br/>
                                         <asp:Image ID="imgLST" runat="server"  Height="20" Width="20" />
                                    <asp:FileUpload ID="uploadLST" runat="server" CssClass="SmallFont"  TabIndex="21"
                                        Width="200px" />  
                                            <asp:RequiredFieldValidator ID="RFVuploadLST" ValidationGroup="M1" runat="server" Display="Dynamic"
                                          ErrorMessage="*LST image required." ControlToValidate="uploadLST"></asp:RequiredFieldValidator>      
                                </td>
                                <td class="tdRight" width="15%">
                                    LST Date&nbsp;
                                </td>
                                <td class="tdleft" width="35%">
                                    <asp:TextBox ID="txtVendLST_DT" runat="server" CssClass="TextBox SmallFont" MaxLength="200"
                                        TabIndex="20" TextMode="singleLine" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdRight" width="20%">
                                    CST No&nbsp;
                                </td>
                                <td class="tdleft">
                                    <asp:TextBox ID="txtVendCST_No" runat="server" CssClass="TextBoxNo SmallFont UpperCase"
                                        MaxLength="50" TabIndex="22" TextMode="singleLine" Width="200px"></asp:TextBox>
                                         <br/>
                                        <asp:Image ID="imgCST" runat="server"  Height="20" Width="20" />
                                    <asp:FileUpload ID="uploadCST" runat="server" CssClass="SmallFont" TabIndex="24"
                                        Width="200px" />  
                                        <asp:RequiredFieldValidator ID="RFVuploadCST" ValidationGroup="M1" runat="server" Display="Dynamic"
                                          ErrorMessage="*CST image required." ControlToValidate="uploadCST"></asp:RequiredFieldValidator> 
                                </td>
                                <td class="tdRight" width="15%">
                                    CST Date&nbsp;
                                </td>
                                <td class="tdleft" width="35%">
                                    <asp:TextBox ID="txtVendCST_DT" runat="server" CssClass="TextBox SmallFont" MaxLength="200"
                                        TabIndex="23" TextMode="singleLine" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdRight" width="20%">
                                    Tin No&nbsp;
                                </td>
                                <td class="tdleft">
                                    <asp:TextBox ID="txtVendTINNo" runat="server" CssClass="TextBoxNo SmallFont" MaxLength="50"
                                        TabIndex="25" TextMode="singleLine" Width="200px"></asp:TextBox>
                                </td>
                                <td class="tdRight" width="15%">
                                    Tin Date&nbsp;
                                </td>
                                <td class="tdleft" width="35%">
                                    <asp:TextBox ID="txtVendTINDate" runat="server" CssClass="TextBox SmallFont" MaxLength="200"
                                        TabIndex="26" TextMode="singleLine" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="td" width="100%">
                        <table width="100%">
                            <tr>
                                <td class="tdRight" width="20%">
                                     <font color="#ff0000">*</font>Service Tax No
                                </td>
                                <td class="tdleft" width="30%">
                                    <asp:TextBox ID="txtServiceTaxNo" runat="server" CssClass="TextBoxNo SmallFont "
                                        MaxLength="50" TabIndex="27" TextMode="singleLine" Width="200px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfService" runat="server" ControlToValidate="txtServiceTaxNo"
                                        Display="Dynamic" ErrorMessage="Pls Enter Service Tax No." SetFocusOnError="True"
                                        ValidationGroup="M1"></asp:RequiredFieldValidator>
                                </td>
                                <td class="tdRight" width="15%">
                                    ECC No:</td>
                                <td class="tdleft" width="35%">
                                    <asp:TextBox ID="txtEccNo" runat="server" CssClass="TextBox SmallFont" 
                                        MaxLength="200" TabIndex="28" TextMode="singleLine" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdRight" width="20%">
                                     <font color="#ff0000">*</font>Bank Name&nbsp;
                                </td>
                                <td class="tdleft" width="30%">
                                    <asp:TextBox ID="txtBankName" runat="server" CssClass="TextBoxNo SmallFont" MaxLength="100"
                                        TabIndex="29" TextMode="singleLine" Width="200px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfBank" runat="server" ControlToValidate="txtBankName"
                                        Display="Dynamic" ErrorMessage="Pls Enter Bank Name." SetFocusOnError="True" ValidationGroup="M1"></asp:RequiredFieldValidator>
                                </td>
                                <td class="tdRight" width="15%">
                                     <font color="#ff0000">*</font>Branch&nbsp;
                                </td>
                                <td class="tdleft" width="35%">
                                    <asp:TextBox ID="txtBranchName" runat="server" CssClass="TextBox SmallFont" MaxLength="100"
                                        TabIndex="30" TextMode="singleLine" Width="200px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfBranch" runat="server" ControlToValidate="txtBranchName"
                                        Display="Dynamic" ErrorMessage="Pls Enter Branch name." SetFocusOnError="True"
                                        ValidationGroup="M1"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdRight" width="20%">
                                     <font color="#ff0000">*</font>Account No&nbsp;
                                </td>
                                <td class="tdleft" width="30%">
                                    <asp:TextBox ID="txtAccountNo" runat="server" CssClass="TextBoxNo SmallFont " MaxLength="50"
                                        TabIndex="31" TextMode="singleLine" Width="200px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfAccount" runat="server" ControlToValidate="txtAccountNo"
                                        Display="Dynamic" ErrorMessage="Pls Enter Account No." SetFocusOnError="True"
                                        ValidationGroup="M1"></asp:RequiredFieldValidator>
                                </td>
                                <td class="tdRight" width="15%">
                                     <font color="#ff0000">*</font>NEFT/RTGC Code&nbsp;
                                </td>
                                <td class="tdleft" width="35%">
                                    <asp:TextBox ID="txtNEFTRTGCCode" runat="server" CssClass="TextBoxNo SmallFont" MaxLength="51"
                                        TabIndex="32" TextMode="singleLine" Width="200px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfRtgs" runat="server" ControlToValidate="txtNEFTRTGCCode"
                                        Display="Dynamic" ErrorMessage="Pls Enter NEFT/RTGC Code." SetFocusOnError="True"
                                        ValidationGroup="M1"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdRight" width="20%">
                                </td>
                                <td class="tdleft" width="30%">
                                </td>
                                <td class="tdRight" width="15%">
                                </td>
                                <td class="tdleft" width="35%">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="td" width="100%">
                        <table width="100%">
                            <tr>
                                <td class="tdRight" width="20%">
                                    Contact Person 1 Name&nbsp;
                                </td>
                                <td class="tdleft" width="30%">
                                    <asp:TextBox ID="txtVendCP1_Name" runat="server" CssClass="TextBox SmallFont" MaxLength="100"
                                        TabIndex="33" TextMode="singleLine" Width="200px"></asp:TextBox>
                                </td>
                                <td class="tdRight" width="15%">
                                    Designation&nbsp;
                                </td>
                                <td class="tdleft" width="35%">
                                    <asp:TextBox ID="txtVendCP1_Desig" runat="server" CssClass="TextBox SmallFont" MaxLength="100"
                                        TabIndex="34" TextMode="singleLine" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdRight" width="20%">
                                    Land Line No&nbsp;
                                </td>
                                <td class="tdleft" width="30%">
                                    <asp:TextBox ID="txtVendCP1_LL" runat="server" CssClass="TextBoxNo SmallFont" MaxLength="60"
                                        TabIndex="35" TextMode="singleLine" Width="200px"></asp:TextBox>
                                    <%--<asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="txtVendCP1_LL"
                                        Display="Dynamic" ErrorMessage="Invalid Phone No." 
                                        MaximumValue="999999999999999" MinimumValue="00000000"
                                        SetFocusOnError="True" Type="Double"></asp:RangeValidator>--%>
                                </td>
                                <td class="tdRight" width="15%">
                                    Mobile&nbsp;
                                </td>
                                <td class="tdleft" width="35%">
                                    <asp:TextBox ID="txtVendCP1_Mob" runat="server" CssClass="TextBoxNo SmallFont" MaxLength="60"
                                        TabIndex="36" TextMode="singleLine" Width="200px"></asp:TextBox>
                                    <%--   <asp:RangeValidator ID="RangeValidator5" runat="server" ControlToValidate="txtVendCP1_Mob"
                                        Display="Dynamic" ErrorMessage="Invalid Mobile No." MaximumValue="9999999999"
                                        MinimumValue="0000000000" SetFocusOnError="True" Type="Double"></asp:RangeValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdRight" width="20%">
                                     Email Id&nbsp;
                                </td>
                                <td colspan="3" class="tdleft" width="80%">
                                    <asp:TextBox ID="txtVendCP1_Mail" runat="server" CssClass="TextBox SmallFont" MaxLength="200"
                                        TabIndex="37" TextMode="singleLine" Width="78%"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="reEmail1" runat="server" ControlToValidate="txtVendCP1_Mail"
                                        ErrorMessage="Please enter valid Email Id" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ValidationGroup="M1" Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdRight" width="20%">
                                    Contact Person 2 Name&nbsp;
                                </td>
                                <td class="tdleft" width="30%">
                                    <asp:TextBox ID="txtVendCP2_Name" runat="server" CssClass="TextBox SmallFont" MaxLength="100"
                                        TabIndex="38" TextMode="singleLine" Width="200px"></asp:TextBox>
                                </td>
                                <td class="tdRight" width="15%">
                                    Designation&nbsp;
                                </td>
                                <td class="tdleft" width="35%">
                                    <asp:TextBox ID="txtVendCP2_Desig" runat="server" CssClass="TextBox SmallFont" MaxLength="100"
                                        TabIndex="39" TextMode="singleLine" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdRight" width="20%">
                                    Land Line No&nbsp;
                                </td>
                                <td class="tdleft" width="30%">
                                    <asp:TextBox ID="txtVendCP2_LL" runat="server" CssClass="TextBoxNo SmallFont" MaxLength="60"
                                        TabIndex="40" TextMode="singleLine" Width="200px"></asp:TextBox>
                                    <%-- <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="txtVendCP2_LL"
                                        Display="Dynamic" ErrorMessage="Invalid Phone No." MaximumValue="99999999" MinimumValue="00000000"
                                        SetFocusOnError="True" Type="Double"></asp:RangeValidator>--%>
                                </td>
                                <td class="tdRight" width="15%">
                                    Mobile&nbsp;
                                </td>
                                <td class="tdleft" width="35%">
                                    <asp:TextBox ID="txtVendCP2_Mob" runat="server" CssClass="TextBoxNo SmallFont" MaxLength="60"
                                        TabIndex="41" TextMode="singleLine" Width="200px"></asp:TextBox>
                                    <%-- <asp:RangeValidator ID="RangeValidator6" runat="server" ControlToValidate="txtVendCP2_Mob"
                                        Display="Dynamic" ErrorMessage="Invalid Mobile No." MaximumValue="9999999999"
                                        MinimumValue="0000000000" SetFocusOnError="True" Type="Double"></asp:RangeValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdRight" width="20%">
                                    Email Id&nbsp;
                                </td>
                                <td colspan="3" class="tdleft" width="80%">
                                    <asp:TextBox ID="txtVendCP2_Mail" runat="server" CssClass="TextBox SmallFont" MaxLength="200"
                                        TabIndex="42" TextMode="singleLine" Width="78%"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="reEmail3" runat="server" ControlToValidate="txtVendCP2_Mail"
                                        ErrorMessage="Please enter valid Email Id" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ValidationGroup="M1" Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="td" width="100%">
                        <table width="100%">
                            <tr>
                                <td class="tdRight" width="20%">
                                    Insu. Policy No.&nbsp;
                                </td>
                                <td class="tdleft" width="30%">
                                    <asp:TextBox ID="txtVendINSPolicy" runat="server" CssClass="TextBoxNo SmallFont"
                                        MaxLength="20" TabIndex="43" TextMode="singleLine" Width="200px"></asp:TextBox>
                                </td>
                                <td class="tdRight" width="15%">
                                    Sales Tax Type&nbsp;
                                </td>
                                <td class="tdleft" width="35%">
                                    <asp:TextBox ID="txtVendStaxType" runat="server" CssClass="TextBox SmallFont" MaxLength="15"
                                        TabIndex="44" TextMode="singleLine" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="td" width="100%">
                        <table width="100%">
                            <tr>
                                <td class="tdRight" width="20%">
                                    Remarks&nbsp;
                                </td>
                                <td class="tdleft" width="80%">
                                    <asp:TextBox ID="txtVendRemarks" runat="server" CssClass="TextBox SmallFont" Height="50px"
                                        MaxLength="200" TabIndex="45" TextMode="MultiLine" Width="81%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="td" width="100%">
                        <table width="100%">
                            <tr>
                                <td class="tdRight" width="20%">
                                    Product&nbsp;
                                </td>
                                <td class="tdleft" width="30%">
                                    <asp:TextBox ID="txtVendProduct" runat="server" CssClass="TextBox SmallFont" MaxLength="40"
                                        TabIndex="46" TextMode="singleLine" Width="200px"></asp:TextBox>
                                </td>
                                <td class="tdRight" width="20%">
                                   <asp:Label ID=lblCreditLimit runat="server" Text="Credit Limit">
                                  
                                   </asp:Label> 
                                </td>
                                <td class="tdleft" width="30%">
                                    <asp:TextBox ID="txtCrLimit" runat="server" CssClass="TextBox SmallFont" MaxLength="40" Text="0"
                                        TabIndex="47" TextMode="singleLine" Width="200px"></asp:TextBox>
                                </td>
                                </tr>
                              <tr>
                    <td id="tdLoginId1" runat="server" align="Right"  valign="top">
                        <font color="#ff0000">*</font>Login Id
                    </td>
                    
                    <td id="tdLoginId3" runat="server" align="left" valign="top">
                        <asp:TextBox ID="txtLoginId" runat="server" CssClass="TextBox" Width="200px"  TabIndex="48"
                            MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RVLogin" Display="None" runat="server"
                            ErrorMessage="Please enter the Login Id." ControlToValidate="txtLoginId" ValidationGroup="M1"></asp:RequiredFieldValidator>
                    </td>
                    <td align="Right"  valign="top">
                       <font color="#ff0000">*</font>Password
                    </td>                    
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtPassword" runat="server"  CssClass="TextBox" Width="200px" TabIndex="49"
                            TextMode="Password" MaxLength="10"></asp:TextBox>
                        <%--<asp:TextBox ID="txtPassword" runat="server" CssClass="TextBox" Width="150px" TabIndex="5"
                            TextMode="Password" MaxLength="10"></asp:TextBox>--%>
                        <asp:RequiredFieldValidator ID="rvPassword" Display="None" runat="server"
                            ErrorMessage="Please enter the Password." ControlToValidate="txtPassword" ValidationGroup="M1"></asp:RequiredFieldValidator>
                    </td>
               
                </tr>
                                <tr>
                                <td class="tdRight" width="15%">
                                    Status&nbsp;
                                </td>
                                <td class="tdleft" width="35%">
                                    <asp:CheckBox ID="chk_Status" runat="server" TabIndex="50" />
                                </td>
                                 <td class="tdRight" width="15%">
                                    Region:
                                </td>
                                <td class="tdleft" width="35%">
                                    <asp:DropDownList ID="ddlRegion" runat="server" Width="200px" CssClass="SmallFont" TabIndex="51">
                                    <asp:ListItem  >East</asp:ListItem>
                                    <asp:ListItem  >West</asp:ListItem>
                                    <asp:ListItem  >North</asp:ListItem>
                                    <asp:ListItem  >South</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tdleft">
                        <cc1:CalendarExtender ID="cetTindt" TargetControlID="txtVendTINDate" runat="server">
                        </cc1:CalendarExtender>
                        <cc1:CalendarExtender ID="cetCSTdt" TargetControlID="txtVendCST_DT" runat="server">
                        </cc1:CalendarExtender>
                        <br />
                        <cc1:CalendarExtender ID="cetLSTdt" TargetControlID="txtVendLST_DT" runat="server">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers >
       <asp:PostBackTrigger ControlID="imgbtnSave" />
        <asp:PostBackTrigger ControlID="imgbtnUpdate" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
