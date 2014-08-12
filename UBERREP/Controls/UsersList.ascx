<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UsersList.ascx.cs" Inherits="UBERREP.Controls.UsersList" %>
<div class="portlet-body">
    <div class="clearfix">
        <div class="btn-group">
            <button id="sample_editable_1_new" class="btn red">
                Add New <i class="icon-plus"></i>
            </button>
        </div>
        <div class="btn-group pull-right">
            <button class="btn dropdown-toggle" data-toggle="dropdown">
                Tools <i class="icon-angle-down"></i>
            </button>
            <ul class="dropdown-menu">
                <li><a href="#">Print</a></li>
                <li><a href="#">Save as PDF</a></li>
                <li><a href="#">Export to Excel</a></li>
            </ul>
        </div>
    </div>
    <asp:Repeater ID="UserListRPT" runat="server">
        <HeaderTemplate>
            <table class="table table-striped table-hover table-bordered" id="sample_editable_1">
                <thead>
                    <tr>
                        <th>Username</th>
                        <th>Full Name</th>
                        <th>Points</th>
                        <th>Notes</th>
                        <th>Edit</th>
                        <th>Delete</th>
                    </tr>
                </thead>
                <tbody>
                    </div>
        </HeaderTemplate>
        <ItemTemplate>
            <tr class="">
                <td>
                    <asp:Literal ID="Lit_usename" runat="server" Text='<%#((UBERREP.BusinessLayer.Users.User)Container.DataItem).Username%>' />
                    <asp:TextBox ID="TXT_username" runat="server" Text='<%#((UBERREP.BusinessLayer.Users.User)Container.DataItem).Username%>' Visible="false" />
                </td>
                <td>
                    <asp:Literal ID="lit_Age" runat="server" Text='<%#((UBERREP.BusinessLayer.Users.User)Container.DataItem).Name%>' />
                    <asp:TextBox ID="fld_Age" runat="server" Text='<%#((UBERREP.BusinessLayer.Users.User)Container.DataItem).Name%>' Columns="4" Visible="false" />
                </td>
                <td><%#((UBERREP.BusinessLayer.Users.User)Container.DataItem).RecordNumber%></td>
                <td><%#((UBERREP.BusinessLayer.Users.User)Container.DataItem).Type%></td>
                <td>
                    <asp:LinkButton ID="lnk_Edit" runat="server" Text="Edit" CommandName="EditThis" /></td>
                <td>
                    <asp:LinkButton ID="lnk_Cancel" runat="server" Text="Cancel" CommandName="CancelEdit" Visible="false" />

                </td>
            </tr>

        </ItemTemplate>
        <FooterTemplate>
            </tbody>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</div>
