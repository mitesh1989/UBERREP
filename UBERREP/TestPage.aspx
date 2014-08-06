<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="TestPage.aspx.cs" Inherits="UBERREP.TestPage" %>

<%@ Register Src="~/Controls/Login.ascx" TagPrefix="uc1" TagName="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="DataContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:Login runat="server" ID="Login" />
</asp:Content>
