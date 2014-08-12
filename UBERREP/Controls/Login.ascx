<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs" Inherits="UBERREP.Controls.Login" %>
<!-- contact us form -->


<script type="text/javascript">
    function ValidateLogin() {
        
        $.ajax({
            type: "POST",
            url: "AjaxCall.aspx/ValidateLogin",
            data: '{userName: "' + $("#<%=txtUserName.ClientID%>")[0].value + '" ,passWord: "' + $("#<%=txtPassword.ClientID%>")[0].value + '" }',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccess,
            failure: OnFail
        });
    }
    function OnFail(response) {
        alert(response.d);
    }
    function OnSuccess(response) {
      

        if (response.d == 'Success')
            window.location = "Dashboard.aspx";
        else
            $("#lblmsg").text(response.d);
    }
</script>
<asp:TextBox AutoCompleteType="FirstName" TabIndex="1" ID="txtUserName" runat="server" MaxLength="50" PlaceHolder="Your Name" CssClass="text-name"></asp:TextBox>

<asp:TextBox AutoCompleteType="Disabled" TabIndex="2" TextMode="Password" ID="txtPassword" runat="server" MaxLength="50" PlaceHolder="Your Password" CssClass="text-name"></asp:TextBox>


<%--<asp:Button ID="BTNLogin" runat="server" Text="Sign In" ValidationGroup="Login" OnClick="BTNLogin_Click" />--%>
<label id="lblmsg" style="color:red;font-size:12px"></label>

<input id="BTNLogin" type="button" value="Sign In"
    onclick="ValidateLogin()" />


<%--</div>--%>

<%--
<div id="login">
    <!-- heading of the form -->
    <div class="head">
        <h3>Login Hare</h3>
        <p></p>
    </div>
 



    <table>
        <tr>
            <td colspan="2">
                <asp:Label ID="LBLErrorMessage" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td width="70"><strong>Username</strong>
            </td>
            <td width="16"><strong>:</strong>
            </td>
            <td width="234">
                
        </tr>
        <tr>
            <td><strong>Password</strong></td>
            <td><strong>:</strong></td>
            <td>
                <asp:TextBox AutoCompleteType="Disabled" ID="TXTPassword" TabIndex="2" runat="server" MaxLength="50" TextMode="Password"></asp:TextBox></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>
                <asp:Button ID="BTNLogin" runat="server" Text="Login" OnClick="BTNLogin_Click" CssClass="button" /></td>
        </tr>
        <tr>
            <td colspan="3"><a href="#" id="lnkForgotPassword">Forgot Password?</a></td>
        </tr>
    </table>
</div>--%>
