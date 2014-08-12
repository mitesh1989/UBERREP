<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SignUp.ascx.cs" Inherits="UBERREP.Controls.SignUp" %>

<asp:Label id="lblMsg" runat="server" Text="" />
<asp:TextBox ID="TXTName" runat="server" class="text-name" PlaceHolder="Name"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Username Required" ValidationGroup="Signup" ControlToValidate="TXTName" runat="server" Display="Dynamic" ForeColor="Red" Font-Size="14px" />


<asp:TextBox ID="TXTEmail" runat="server" class="text-name" PlaceHolder="Email"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Email Required" ValidationGroup="Signup" ControlToValidate="TXTEmail" runat="server" Display="Dynamic" ForeColor="Red" Font-Size="14px" />


<asp:TextBox ID="TXTPassword" TextMode="Password" runat="server" class="text-name" PlaceHolder="Password"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="Password Required" ValidationGroup="Signup" ControlToValidate="TXTPassword" runat="server" Display="Dynamic" ForeColor="Red" Font-Size="14px" />


<asp:TextBox ID="TXTConfirmPassword" TextMode="Password" runat="server" class="text-name" PlaceHolder="conform_password"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator4" ErrorMessage="Password Required" ValidationGroup="Signup" ControlToValidate="TXTConfirmPassword" runat="server" Display="Dynamic" ForeColor="Red" Font-Size="14px" />
<asp:CompareValidator ID="CompareValidator1" ErrorMessage="Password Mismatch" Display="Dynamic" ControlToValidate="TXTConfirmPassword" ControlToCompare="TXTPassword"  runat="server" ForeColor="Red" Font-Size="14px" ValidationGroup="Signup"/>
<asp:DropDownList runat="server" ID="drpRoles">
    <asp:ListItem Text="Select Role" Value="0" Selected="True"/>
    <asp:ListItem Text="Wholesale-Admin" Value="1"/>
    <asp:ListItem Text="Sales Rep-Admin" Value="2" />
    <asp:ListItem Text="Sales" Value="3"/>
    <asp:ListItem Text="Retailer" Value="4"/>
</asp:DropDownList>
<asp:RequiredFieldValidator ID="RequiredFieldValidator5" ErrorMessage="Select Role Required" ControlToValidate="drpRoles" ValidationGroup="Signup" InitialValue="0" runat="server"  ForeColor="Red" Font-Size="14px" />
<asp:Button ID="BTNSave" Text="Register Now!" OnClick="BTNSave_Click" runat="server" ValidationGroup="Signup" Style="background-color: peru; border: medium none; border-radius: 3px; color: white; font-family: sans-serif; font-size: 18px; height: 45px; margin-left: 20px; margin-top: 10px; width: 82%;" />


