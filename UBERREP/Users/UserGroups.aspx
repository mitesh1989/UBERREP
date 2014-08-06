<%@ Page Title="Manage User Groups" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="UserGroups.aspx.cs" Inherits="UBERREP.Admin.Users.UserGroups" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageHeader" runat="server">Manage User Groups</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .buttonMargin
        {
            margin-left:15px;
        }
    </style>
    <% if (UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser != null && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections != null && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections.Count > 0 && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections[UBERREP.BusinessLayer.Users.Section.SectionCodes.USR_GRP_MGNT].HasInsert)
       { %>           
    <table>
        <tr>
            <td class="btn_view_site" style="margin-top:-10px;line-height:28px;"><a href="UserGroup.aspx?action=new">Add New</a></td>
        </tr>
    </table>
    <%} %>
    <fieldset><legend>Search User Groups</legend>
    <table>
        <tr>                        
            <td>Status</td>
            <td>Group Name</td>
            <td>&nbsp;</td>
        </tr>
        <tr>            
            <td><asp:DropDownList ID="DRPStatus" runat="server"></asp:DropDownList></td>
            <td><asp:TextBox ID="TXTGroupName" runat="server"></asp:TextBox></td>
            <td>
                <asp:Button ID="BTNSearch" CssClass="buttonMargin" runat="server" Text="Search" onclick="BTNSearch_Click" />
            </td>
        </tr>
    </table>
    </fieldset>
    <br />
<table class="tablesorter" cellspacing="0">
    <% if (UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser != null && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections != null && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections.Count > 0 && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections[UBERREP.BusinessLayer.Users.Section.SectionCodes.USR_GRP_MGNT].HasView)
       { %>
    <asp:Repeater ID="RPTRUserGroups" runat="server" OnItemDataBound="RPTRUserGroups_ItemDataBound" OnItemCommand="RPTRUserGroups_ItemCommand">
    <HeaderTemplate>
        <thead>    
            <tr>
                <th>No.</th>   
                <%if (UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser != null && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.Type == UBERREP.BusinessLayer.Users.UserTypes.System)
                {%>
                  <th>Name</th><%} %>
                <th>Client</th>
                <th>Status</th>
                    <% if (UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser != null && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections != null && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections.Count > 0 && (UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections[UBERREP.BusinessLayer.Users.Section.SectionCodes.USR_GRP_MGNT].HasUpdate || UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections[UBERREP.BusinessLayer.Users.Section.SectionCodes.USR_GRP_MGNT].HasDelete))
                    { %>
                <th>Actions</th><%} %>
            </tr>    
        </thead>    
    </HeaderTemplate>
    <ItemTemplate>
    <tr onmouseover="javascript:setMouseOverColor(this);" onmouseout="javascript:setMouseOutColor(this);">
        <td><%#(((RepeaterItem)Container).ItemIndex+1).ToString() %></td>
        <td><%# ((UBERREP.BusinessLayer.Users.UserGroup)Container.DataItem).Name %></td>
        <%if(UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser!=null && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.Type == UBERREP.BusinessLayer.Users.UserTypes.System) {%>
        <td><%--<%# ((UBERREP.BusinessLayer.Users.UserGroup)Container.DataItem).ClientGroup.Name %>--%></td><%} %>
        <td><%# ((UBERREP.BusinessLayer.Users.UserGroup)Container.DataItem).Status %></td>        
        <td><% if (UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser != null && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections != null && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections.Count > 0 && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections[UBERREP.BusinessLayer.Users.Section.SectionCodes.USR_GRP_MGNT].HasUpdate)
               { %><asp:LinkButton ID="BTNEdit" runat="server" CommandName="edit" ToolTip="Edit Client"><img id="Img2" alt="Edit UserGroup" src="~/Theme/images/editicon.png" runat="server" /></asp:LinkButton> &nbsp;&nbsp;<%} %>
        <% if (UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser != null && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections != null && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections.Count > 0 && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections[UBERREP.BusinessLayer.Users.Section.SectionCodes.USR_GRP_MGNT].HasDelete)
           { %><asp:LinkButton ID="BTNDelete" runat="server" CommandName="delete" OnClientClick="return confirm('Are you sure want to delete this?')" ToolTip="Delete Site"><img id="Img3" alt="Delete UserGroup" src="~/Theme/images/deleteicon.png" runat="server" /></asp:LinkButton><%} %></td>
    </tr>
    </ItemTemplate>
    </asp:Repeater>
    <%} %>
</table>
</asp:Content>
