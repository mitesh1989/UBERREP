<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs" Inherits="UBERREP.Controls.Login" %>

<div id="login">            
        <table>
            <tr>                
                <td colspan="2"><asp:Label ID="LBLErrorMessage" runat="server" ></asp:Label></td>
            </tr>
            <tr>
                <td width="70"><strong>Username</strong>
                </td>
                <td width="16"><strong>:</strong>
                </td>
                <td width="234"><asp:TextBox AutoCompleteType="FirstName" TabIndex="1" ID="TXTUserName" runat="server" MaxLength="50"></asp:TextBox></td>
            </tr>
            <tr>
                <td><strong>Password</strong></td>
                <td><strong>:</strong></td>
                <td><asp:TextBox  AutoCompleteType="Disabled" ID="TXTPassword" TabIndex="2" runat="server" MaxLength="50" TextMode="Password"></asp:TextBox></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td><asp:Button ID="BTNLogin" runat="server" Text="Login"  OnClick="BTNLogin_Click" CssClass="button"/></td>
            </tr>
            <tr>
                <td colspan="3"><a href="#" id="lnkForgotPassword">Forgot Password?</a></td>
            </tr>
         </table>
</div>
