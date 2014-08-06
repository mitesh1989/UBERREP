<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SignUp.ascx.cs" Inherits="UBERREP.Controls.SignUp" %>
    

            <asp:TextBox ID="TXTName" runat="server" class="text-name" value="Name"></asp:TextBox>
    
            <asp:TextBox ID="TXTEmail" runat="server" class="text-name" value="Email"></asp:TextBox>
    
            
            <asp:TextBox ID="TXTPassword" TextMode="Password" runat="server" class="text-name" value="Password"></asp:TextBox>
        
            
            <asp:TextBox ID="TXTConfirmPassword" TextMode="Password" runat="server" class="text-name" value="conform_password"></asp:TextBox>
        
            
            <asp:Button ID="BTNSave" Text="Register Now!" OnClick="BTNSave_Click" runat="server" />
        
    