<%@ Page Title="Manage Clients" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UBERREP.Admin.ClientManagement.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageHeader" runat="server">Manage Clients
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<% if (UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser != null && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections != null && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections.Count > 0 && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections[UBERREP.BusinessLayer.Users.Section.SectionCodes.OFC_MGNT].HasInsert)
   { %>
    <table>
        <tr>            
            <td class="btn_view_site" style="margin-top:-10px;line-height:28px;"><a href="Client.aspx?action=new">Add New</a></td>
        </tr>
    </table>
    <%} %>
    <fieldset><legend>Search Clients</legend>
<table>
        <tr>            
            <td>Client Name</td>                        
            <td>Status</td>
            <td>API</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td><asp:TextBox id="TXTClientName" runat="server" ></asp:TextBox></td>            
            <td><asp:DropDownList ID="DRPStatus" runat="server"></asp:DropDownList></td>
            <td>
                <asp:DropDownList ID="DRPAPIID" runat="server">
                </asp:DropDownList>
            </td>            
            <td>
                <asp:Button ID="BTNSearch" runat="server" Text="Search" onclick="BTNSearch_Click" />
            </td>
        </tr>
    </table>   
 </fieldset>
 <br /> 
<table class="tablesorter" cellspacing="0">   
    <% if (UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser != null && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections != null && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections.Count > 0 && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections != null && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections.Count > 0 && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections[UBERREP.BusinessLayer.Users.Section.SectionCodes.OFC_MGNT].HasView)
       { %>     
    <asp:Repeater ID="RPTRClientList" runat="server" OnItemCommand="RPTRClientList_ItemCommand">        
    <HeaderTemplate>
     <thead>
        <tr>            
            <th align="left">No.</th>        
            <th align="left">Client</th>
            <th align="left">Payment API</th>
            <th align="left">Status</th>
            <th align="left">Actions</th>        
        </tr>
    </thead>
    </HeaderTemplate>
    <ItemTemplate>
    <tr onmouseover="javascript:setMouseOverColor(this);" onmouseout="javascript:setMouseOutColor(this);">
        <td><%#(((RepeaterItem)Container).ItemIndex+1).ToString() %></td>
        <td><%# ((UBERREP.BusinessLayer.Client.Client)Container.DataItem).Name %></td>        
        <td><%# ((UBERREP.BusinessLayer.Client.Client)Container.DataItem).ClientAPI!=null ? ((UBERREP.BusinessLayer.Client.Client)Container.DataItem).ClientAPI.Name : string.Empty %></td>
        <td><%# ((UBERREP.BusinessLayer.Client.Client)Container.DataItem).Status %></td>                
        <td><% if (UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser != null && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections != null && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections.Count > 0 && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections != null && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections.Count > 0 && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections[UBERREP.BusinessLayer.Users.Section.SectionCodes.OFC_MGNT].HasUpdate)
               { %><asp:LinkButton ID="BTNEdit" runat="server" CommandName="edit" ToolTip="Edit Client"><img id="Img2" alt="Edit Site" src="~/Theme/images/editicon.png" runat="server" /></asp:LinkButton>  &nbsp;&nbsp;<%} %>
        <% if (UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser != null && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections != null && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections.Count > 0 && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections != null && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections.Count > 0 && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections[UBERREP.BusinessLayer.Users.Section.SectionCodes.OFC_MGNT].HasDelete)
           { %>
            <asp:LinkButton ID="BTNDelete" runat="server" CommandName="delete" OnClientClick="return confirm('Are you sure want to delete this?')" ToolTip="Delete Site"><img id="Img3" alt="Delete Client" src="~/Theme/images/deleteicon.png" runat="server" /></asp:LinkButton><%} %></td>
    </tr>
    </ItemTemplate>
    </asp:Repeater>
    <%} %>
</table>
</asp:Content>
