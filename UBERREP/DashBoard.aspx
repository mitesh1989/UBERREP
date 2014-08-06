<%@ Page Title="UBERREP Dashboard" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="UBERREP.Admin.Admin.DashBoard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageHeader" runat="server">Admin DashBoard
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3><%if (UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser!=null) {%>Hi, <%=UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.Name %><%} %></h3>
</asp:Content>
