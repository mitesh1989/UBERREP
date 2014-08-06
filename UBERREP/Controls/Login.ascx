<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs" Inherits="UBERREP.Controls.Login" %>
<!-- contact us form -->

<%--<div id="popupContact">

    <!-- heading of the form -->
    <div class="head">
        <h3>Login Here</h3>
        <p></p>
    </div>--%>
<!-- contact us form -->
<%--<input type="text" name="vname" value="Your Name" class="text-name" />--%>
<asp:TextBox AutoCompleteType="FirstName" TabIndex="1" ID="TXTUserName" runat="server" MaxLength="50" PlaceHolder="Your Name" CssClass="text-name"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Username Required" ValidationGroup="Login" ControlToValidate="TXTUserName" runat="server" Display="Dynamic" ForeColor="Red" Font-Size="14px" />
<asp:TextBox AutoCompleteType="Disabled" TabIndex="2" ID="txtPassword" runat="server" MaxLength="50" PlaceHolder="Your Password" CssClass="text-name"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Password Required" ValidationGroup="Login" ControlToValidate="txtPassword" runat="server" Display="Dynamic" ForeColor="Red" Font-Size="14px" />
<asp:Button ID="BTNLogin" runat="server" Text="Sign In" ValidationGroup="Login" OnClick="BTNLogin_Click"  />



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
