<%@ Page Title="Manage Users" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UBERREP.Admin.Users.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageHeader" runat="server">Manage Users</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <% if (UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser != null && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections != null && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections.Count > 0 && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections[UBERREP.BusinessLayer.Users.Section.SectionCodes.USR_MGNT].HasInsert)
       { %>           
     <table>
        <tr>
            <td class="btn_view_site" style="margin-top:-10px;line-height:28px;"><a href="User.aspx?action=new">Add New</a></td>
        </tr>
    </table>
    <%} %>
    <fieldset><legend>Search Users</legend>
    <table>
        <tr>            
            <td>Name</td>            
            <td>Email</td>            
            <td>Status</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td><asp:TextBox id="TXTName" runat="server"></asp:TextBox> </td>            
            <td> <asp:TextBox id="TXTEmail" runat="server"></asp:TextBox> </td>            
            <td><asp:DropDownList ID="DRPStatus"  runat="server"></asp:DropDownList></td>
            <td><asp:Button TabIndex="1" ID="BTNSearch" runat="server" Text="Search" onclick="BTNSearch_Click"/></td>
        </tr>
    </table>
    </fieldset>
    <br />
<table class="tablesorter" cellspacing="0">        
    <% if (UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser != null && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections != null && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections.Count > 0 && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections[UBERREP.BusinessLayer.Users.Section.SectionCodes.USR_MGNT].HasView)
       { %>           
    <asp:Repeater ID="UserList" runat="server" OnItemCommand="UserList_ItemCommand" OnItemDataBound="UserList_OnItemDataBound">
    <HeaderTemplate>
    <thead>
        <tr>
            <th align="left">No.</th>
            <th align="left">Name</th>
            <th align="left">Email</th>
            <th align="left">UserName</th>
            <th align="left">Status</th>            
            <th align="left">Last Login</th>
            <th align="left">Actions</th>
        </tr>
    </thead>
    </HeaderTemplate>
    <ItemTemplate>
    <tr onmouseover="javascript:setMouseOverColor(this);" onmouseout="javascript:setMouseOutColor(this);">
        <td><%#(((RepeaterItem)Container).ItemIndex+1).ToString() %></td>
        <td><%# ((UBERREP.BusinessLayer.Users.User)Container.DataItem).Name %></td>
        <td><%# ((UBERREP.BusinessLayer.Users.User)Container.DataItem).Email %></td>
        <td><%# ((UBERREP.BusinessLayer.Users.User)Container.DataItem).Username %></td>
        <td><%# ((UBERREP.BusinessLayer.Users.User)Container.DataItem).Status %></td>        
        <td><%# ((UBERREP.BusinessLayer.Users.User)Container.DataItem).LastLogin == DateTime.MinValue ? "No Activity" : ((UBERREP.BusinessLayer.Users.User)Container.DataItem).LastLogin.ToString("dd MMM yyyy")%></td>
        <%--<td><a href="User.aspx?id=<%# ((BusinessLayer.Users.User)Container.DataItem).ID %>" alt="Edit User" title="Edit User"><img src="~/app_themes/default/images/buttons/editicon.png" runat="server" /></a> | --%>
        <td><% if (UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser != null && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections != null && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections.Count > 0 && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections[UBERREP.BusinessLayer.Users.Section.SectionCodes.USR_MGNT].HasUpdate)
               { %><asp:LinkButton ID="BTNEdit"  CommandName="edit" ToolTip="Edit User" runat="server"><img id="Img1" alt="Edit User" src="~/Theme/images/editicon.png" runat="server" /></asp:LinkButton>  &nbsp;&nbsp;<%} %>
        <% if (UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser != null && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections != null && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections.Count > 0 && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections[UBERREP.BusinessLayer.Users.Section.SectionCodes.USR_MGNT].HasDelete)
           { %>           
            <asp:LinkButton  ToolTip="Delete User" ID="BTNDelete" runat="server" CommandName="delete" OnClientClick="return confirm('Are you sure want to delete this?')"><img id="Img2" alt="Delete User" src="~/Theme/images/deleteicon.png" runat="server" /></asp:LinkButton><%} %></td>
    </tr>
    </ItemTemplate>
    </asp:Repeater>
    <%} %>
</table>
</asp:Content>
