<%@ Page Title="UBERREP | Admin Login" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UBERREP.Admin.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content style="float:right;" ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .forgotpassword
        {
            height:auto !important;width:auto !important;
        }
    </style>
        
    <script lang="javascript" type="text/javascript">
        $(function () { $("#forgotpassword").dialog({ autoOpen: false }); $("#lnkForgotPassword").click(function () { $("#forgotpassword").dialog("open"); }); });
        function ResetPassword(email) {
            var UserName = document.getElementById("TXTEmail").value.length;
            if (UserName < 5) {
                alert("UserName must be more than 4 characters long.");
                return;
            }

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                datatype:"json",
                url: "Login.aspx/ForgotPassword",
                //data: { email: email },
                data: "{'email':'" + email + "'}",                
                success: function (data) {
                    alert(data.d);
                    $("#forgotpassword").dialog("close");
                }
            });
        }
        <%-- Script to remove border and background-color of Content Placeholder on Login Page --%>
        var obj = $("#Article");
        $(".module").css('border', '0px');
        $(".module").css('background', 'none');
</script>
    <%-- Script to remove border and background-color of Content Placeholder on Login Page --%>
    <script  type="text/javascript">
        $(document).ready(function () {
            var obj = $("#Article");
            console.log("in");
            $(".module").css('border', '0px');
            $(".module").css('background', 'none');
        });
    </script>
<div id="login">
    <%--<div style="width: 340px; float: right; padding: 20px 5px 0px 0px;">--%>
     <div  style="width: 340px;margin-left:50%;padding: 20px  5px  0px  0px;">
        <table width="340" border="0" cellpadding="5" cellspacing="0">
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
</div>
<div id="forgotpassword" title="Forgot Password">
 Username : <input id="TXTEmail" type="text"/><input type="button" id="btnreset" value="Send" onclick="ResetPassword(document.getElementById('TXTEmail').value)" />
 <%--Username : <input id="TXTEmail" type="text"/><asp:Button id="btnreset" Text="Send" runat="server" OnClientClick="ResetPassword(document.getElementById('TXTEmail').value)" />--%>
</div>
</asp:Content>
