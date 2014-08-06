<%@ Page Title="Manage User Group" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="UserGroup.aspx.cs" Inherits="UBERREP.Admin.Users.UserGroup" %>
<%@ Register Src="~/WebControls/DatePicker.ascx" TagName="DatePicker" TagPrefix="DT" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageHeader" runat="server">Manage User Group
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
            <table>
                <tr>
                    <td>Client</td>
                    <td><asp:DropDownList ID="DRPClient" TabIndex="1" Enabled="<%# this.EntityToEdit==null %>" runat="server"></asp:DropDownList></td>
                </tr>                                
                <tr>
                    <td>Group Name</td>
                    <td><asp:TextBox ID="TXTGroupName" runat="server" MaxLength="200" TabIndex="2"></asp:TextBox>*</td>                            
                </tr>                                
                <tr>
                    <td>Status</td>
                    <td><asp:DropDownList ID="DRPStatus" runat="server" TabIndex="3"></asp:DropDownList></td>                                            
                </tr>
                <% if (UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser != null && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.Type == UBERREP.BusinessLayer.Users.UserTypes.System){ %>                
                <tr>
                    <td>Is System</td>
                    <td><asp:CheckBox ID="CHKIsSystemGroup" TabIndex="4" runat="server" /></td>
                </tr><%} %>
                <tr>   
                    <td></td>       
                    <td> 
                         <% if (UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser != null && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections != null && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections.Count > 0 && (UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections[UBERREP.BusinessLayer.Users.Section.SectionCodes.USR_GRP_MGNT].HasUpdate || UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections[UBERREP.BusinessLayer.Users.Section.SectionCodes.USR_GRP_MGNT].HasInsert))
                            { %>
                        <asp:Button ID="BTNSave" TabIndex="5" runat="server" Text="Save" onclick="BTNSave_Click"/>
                        <%} %>
                        <asp:Button ID="BTNCancel" TabIndex="6" runat="server" Text="Cancel" onclick="BTNCancel_Click" />                        
                    </td>
                </tr>                
            </table>
        <table>
            <tr style="padding-left:20px;padding-right:20px;">
                <td>
                    <asp:Repeater ID="RPTRSectionRights" runat="server" OnItemDataBound="RPTRSectionRights_ItemDataBound" >
                        <HeaderTemplate>
                            <tr>
                                <th style="padding-left:20px;padding-right:20px;" >Name</th>
                                <th>Insert</th>
                                <th>Update</th>
                                <th>Delete</th>
                                <th>View</th>
                                <%--<th>AdminData</th>--%>
                                <% if(UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser!=null && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.Type == UBERREP.BusinessLayer.Users.UserTypes.System) { %>
                                <th>Show Encrypted Data</th><% } %>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td align="justify"><%# ((UBERREP.BusinessLayer.Users.GroupSectionRole)Container.DataItem).Section.Name %></td>                                        
                                <td align="center" style="padding-left:20px;padding-right:20px;"><asp:CheckBox ID="CHKHasInsert" runat="server" Checked="<%# ((UBERREP.BusinessLayer.Users.GroupSectionRole)Container.DataItem).Role.HasInsert %>"/></td>
                                <td><asp:CheckBox style="padding-left:20px;padding-right:20px;" ID="CHKHasUpdate" runat="server" Checked="<%# ((UBERREP.BusinessLayer.Users.GroupSectionRole)Container.DataItem).Role.HasUpdate %>"/></td>                            
                                <td><asp:CheckBox style="padding-left:20px;padding-right:20px;" ID="CHKDelete" runat="server" Checked="<%# ((UBERREP.BusinessLayer.Users.GroupSectionRole)Container.DataItem).Role.HasDelete %>"/></td>                            
                                <td><asp:CheckBox style="padding-left:20px;padding-right:20px;" ID="CHKView" runat="server" Checked="<%# ((UBERREP.BusinessLayer.Users.GroupSectionRole)Container.DataItem).Role.HasView %>"/></td>
                                <%--<td align="center"><asp:CheckBox ID="CHKShowAdminData" runat="server" Checked="<%# ((UBERREP.BusinessLayer.Users.GroupSectionRole)Container.DataItem).Role.ShowClientAdminData %>"/></td>--%>
                                <% if(UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser!=null && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.Type == UBERREP.BusinessLayer.Users.UserTypes.System) { %>
                                <td align="center"><asp:CheckBox ID="CHKShowEncryptedData" runat="server" Checked="<%# ((UBERREP.BusinessLayer.Users.GroupSectionRole)Container.DataItem).Role.ShowEncryptedData %>"/></td><% } %>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
        </table>        
</asp:Content>
