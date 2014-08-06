<%@ Page Title="Manage User" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="UBERREP.Admin.Users.User" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1
        {
            text-align: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageHeader" runat="server">Manage User</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table>
    <tr>
        <td width="200px" style="text-align: right">Name</td>
        <td><asp:TextBox ID="TXTName" runat="server" AutoCompleteType="FirstName" MaxLength="500" TabIndex="1"></asp:TextBox></td>
    </tr>    
    <tr>
        <td class="auto-style1">Username</td>
        <td><asp:TextBox ID="TXTUserName" autocomplete="off" runat="server" AutoCompleteType="Disabled" MaxLength="50" TabIndex="2"></asp:TextBox></td>        
    </tr>
    <tr>
        <td class="auto-style1">Password</td>
        <td><asp:TextBox ID="TXTPassword" TextMode="Password" runat="server" AutoCompleteType="Disabled" MaxLength="50" TabIndex="3"></asp:TextBox></td>               
    </tr>
    <tr>
        <td class="auto-style1">Confirm Password</td>
        <td><asp:TextBox ID="TXTConfirmPassword" TextMode="Password" runat="server" AutoCompleteType="Disabled" MaxLength="50" TabIndex="4"></asp:TextBox></td>        
    </tr>
    <tr>
        <td class="auto-style1">Email</td>
        <td><asp:TextBox ID="TXTEmail" runat="server" AutoCompleteType="Disabled" MaxLength="500" TabIndex="5"></asp:TextBox></td>            
    </tr>    
    <tr>
        <td class="auto-style1">Contact Number</td>
        <td><asp:TextBox ID="TXTCell" runat="server" AutoCompleteType="Cellular" MaxLength="50" TabIndex="6"></asp:TextBox></td>        
    </tr>    
    <tr id="TRStatus" runat="server">
        <td class="auto-style1">Status</td>
        <td><asp:DropDownList ID="DRPStatus" runat="server" TabIndex="7"></asp:DropDownList></td>        
    </tr>    
    <%if(this.IsSystemUser){ %>    
    <tr id="TRClients" runat="server">
        <td class="auto-style1">Clients</td>
        <td><asp:ListBox ID="LSTClients" OnSelectedIndexChanged="LSTClients_SelectedIndexChanged" Height="80px" AutoPostBack="true" runat="server" SelectionMode="Single" TabIndex="8"></asp:ListBox></td>
    </tr>
    <%} %>
    <tr>
        <td class="auto-style1">Groups</td>
        <td><asp:ListBox SelectionMode="Single" Height="80px" TabIndex="9" ID="LSTGroups" runat="server" ></asp:ListBox></td>
    </tr>
    <%if(this.IsSystemUser){ %>    
    <tr>
        <td class="auto-style1">User Type</td>
        <td><asp:DropDownList ID="DRPType" runat="server" TabIndex="10"></asp:DropDownList></td>
    </tr>        
    <%} %>   
    <tr>
        <td></td>
        <td><!-- do not implmenet permission management here, as user profile management will have this page-->
            <asp:Button ID="BTNSave" TabIndex="11" runat="server" Text="Save" onclick="BTNSave_Click"/>
            <asp:Button ID="BTNCancel" TabIndex="12" runat="server" Text="Cancel" onclick="BTNCancel_Click"/>
        </td>
    </tr>    
    </table>
</asp:Content>
