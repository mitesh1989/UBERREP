<%@ Page Title="" Language="C#" MasterPageFile="~/masterPages/Dashboard.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="UBERREP.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3><%if (UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser != null)
          {%>Hi, <%=UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.Name %><%} %></h3>
</asp:Content>
