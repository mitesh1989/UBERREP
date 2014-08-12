﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UsersList.ascx.cs" Inherits="UBERREP.Controls.UsersList" %>
<div class="portlet-body">
    <script type="text/javascript">

        function print_page() {
            window.print();
        }

    </script>

    <asp:Repeater ID="UserListRPT" runat="server">
        <HeaderTemplate>
            <div class="portlet-body">
                <div class="clearfix">
                    <div class="btn-group">
                        <asp:Button ID="sample_editable_1_new" runat="server" class="btn red" CommandName="Insert" Text="Add New" />
                    </div>
                    <div class="btn-group pull-right">
                        <button class="btn dropdown-toggle" data-toggle="dropdown">
                            Tools <i class="icon-angle-down"></i>
                        </button>
                        <ul class="dropdown-menu">
                            <li><a href="#" onclick="javascript :print_page();">Print</a></li>
                            <li><a href="#">Save as PDF</a></li>
                            <li><a href="#">Export to Excel</a></li>
                        </ul>
                    </div>
                </div>
                <table class="table table-striped table-hover table-bordered" id="sample_editable_1">
                    <thead>
                        <tr>
                            <th style="width: 184px;">Username</th>
                            <th style="width: 257px;">Full Name</th>
                            <th style="width: 125px;">Points</th>
                            <th style="width: 180px;">Notes</th>
                            <th style="width: 86px;">Edit</th>
                            <th style="width: 127px;">Delete</th>
                        </tr>
                    </thead>
                    <asp:Panel ID="insertPnl" runat="server" Visible="false">
                        <tr class="">
                            <td>
                                <asp:TextBox ID="txt_ins_Username" runat="server" />
                            </td>
                            <td>
                                <asp:TextBox ID="txt_ins_Fullname" runat="server" />
                            </td>
                            <td>
                                <asp:TextBox ID="txt_ins_Points" runat="server" />
                            </td>
                            <td>
                                <asp:TextBox ID="txt_ins_Notes" runat="server" />
                            </td>
                            <td>
                                <asp:LinkButton ID="LinkButton1" runat="server" Text="Save" CommandName="Save" />
                            </td>
                            <td>
                                <asp:LinkButton ID="LinkButton2" runat="server" Text="Cancel" CommandName="InsertCancel" />
                            </td>
                        </tr>
                    </asp:Panel>
        </HeaderTemplate>
        <ItemTemplate>
            <tr class="">
                <td class=" ">
                    <asp:Literal ID="Lit_usename" runat="server" Text='<%#((UBERREP.BusinessLayer.Users.User)Container.DataItem).Username%>' />
                    <asp:TextBox ID="TXT_username" runat="server" Text='<%#((UBERREP.BusinessLayer.Users.User)Container.DataItem).Username%>' Visible="false" CssClass="m-wrap small" Style="margin: 0px;" />
                </td>
                <td class=" ">
                    <asp:Literal ID="lit_FullName" runat="server" Text='<%#((UBERREP.BusinessLayer.Users.User)Container.DataItem).Name%>' />
                    <asp:TextBox ID="txt_FullName" runat="server" Text='<%#((UBERREP.BusinessLayer.Users.User)Container.DataItem).Name%>' Visible="false" CssClass="m-wrap small" Style="margin: 0px;" />
                </td>
                <td class=" ">
                    <asp:Literal ID="lit_Point" runat="server" Text='<%#((UBERREP.BusinessLayer.Users.User)Container.DataItem).RecordNumber%>' />
                    <asp:TextBox ID="txt_Point" Columns="4" runat="server" Text='<%#((UBERREP.BusinessLayer.Users.User)Container.DataItem).RecordNumber%>' Visible="false" CssClass="m-wrap small" Style="margin: 0px;" />
                </td>
                <td class=" ">
                    <asp:Literal ID="lit_Notes" runat="server" Text='<%#((UBERREP.BusinessLayer.Users.User)Container.DataItem).Type%>' />
                    <asp:TextBox ID="txt_Notes" runat="server" Text='<%#((UBERREP.BusinessLayer.Users.User)Container.DataItem).Type%>' Visible="false" CssClass="m-wrap small" Style="margin: 0px;" />
                </td>
                <td class=" ">
                    <asp:LinkButton ID="lnk_Edit" runat="server" Text="Edit" CommandName="Edit" class="edit" />
                    <asp:LinkButton ID="lnk_Update" Visible="false" runat="server" Text="Save" CommandName="Update" CommandArgument='<%#((UBERREP.BusinessLayer.Users.User)Container.DataItem).ID%>' />
                </td>
                <td class=" ">
                    <asp:LinkButton ID="lnk_Cancel" Visible="false" runat="server" Text="Cancel" CommandName="Cancel" />
                    <asp:LinkButton ID="lnk_delete" runat="server" Text="Delete" CommandArgument='<%#((UBERREP.BusinessLayer.Users.User)Container.DataItem).ID%>' CommandName="Delete" OnClientClick='javascript:return confirm("Are you sure you want to delete?")' class="delete" />
                </td>
            </tr>
        </ItemTemplate>

        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</div>
