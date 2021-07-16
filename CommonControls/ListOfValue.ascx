<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ListOfValue.ascx.cs" Inherits="CommonControls_ListOfValue" %>

<script type="text/javascript" language="javascript">
         function CallCSharpCode()
        {
            document.getElementById('<%= btnLovFillRuntime.ClientID %>').click();                           
       }
     
        function SetCursorToTextEnd()
        {        
            var text = document.getElementById('<%= txtLov.ClientID %>');
            if (text != null && text.value.length > 0)
            {
                if (text.createTextRange)
                {
                    var FieldRange = text.createTextRange();
                    FieldRange.moveStart('character', text.value.length);
                    FieldRange.collapse();
                    FieldRange.select();
                }
            }
        }
      
</script>

<table>
    <tr>
        <td>         
            <div>
                <asp:TextBox ID="txtLov" runat="server" CssClass="LastFocus" onfocus="SetCursorToTextEnd(this.id)"
                    onkeyup="javascript:CallCSharpCode()" AutoCompleteType="disabled" OnTextChanged="txtLov_TextChanged"
                    Width="300px">
                </asp:TextBox>
                <asp:Panel ID="pnlLovGrid" runat="server" Height="300px" ScrollBars="Both" Width="320px">
                </asp:Panel>
                &nbsp;
                <asp:Button ID="btnLovFillRuntime" runat="server" Text="" OnClick="btnLovFillRuntime_Click" />
            </div>       
        </td>
    </tr>
</table>
