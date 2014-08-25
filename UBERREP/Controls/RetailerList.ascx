<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RetailerList.ascx.cs" Inherits="UBERREP.Controls.UsersList" %>
<div class="portlet-body">
    <script type="text/javascript">

        function print_page() {

            window.print();

        }

    </script>



    <asp:Repeater ID="UserListRPT" runat="server" >
        <HeaderTemplate>
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
                        <li>
                            <asp:LinkButton ID="BTNSaveToPDF" runat="server" Text="Save as PDF" OnClick="BTNSaveToPDF_Click" /></li>
                        <li>
                            <asp:LinkButton ID="BTNSaveToExcel" runat="server" Text="Export to Excel" OnClick="BTNSaveToExcel_Click" /></li>
                    </ul>
                </div>
            </div>
            <table class="table table-striped table-hover table-bordered" id="sample_editable_1">
                <thead>
                    <tr>
                        <th>Username</th>
                        <th style="width: 20% !IMPORTANT">Full Name</th>
                        <th style="width: 20% !IMPORTANT">Points</th>
                        <th style="width: 20% !IMPORTANT">Notes</th>
                        <th>Edit</th>
                        <th>Delete</th>
                    </tr>
                </thead>
                <asp:Panel ID="insertPnl" runat="server" Visible="false">
                    <tr class="">
                        <td>
                            <asp:TextBox ID="txt_ins_Username" class="m-wrap small fixBig" runat="server" />
                            
                        </td>
                        <td>
                            <asp:TextBox ID="txt_ins_Fullname" class="m-wrap small fixSmall" runat="server" />
                        </td>
                        <td>
                            <asp:TextBox ID="txt_ins_Points" class="m-wrap small fixSmall" runat="server" />
                        </td>
                        <td>
                            <asp:TextBox ID="txt_ins_Notes" class="m-wrap small fixSmall" runat="server" />
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
                <td style="width: 160px;">
                    <asp:Label ID="lbl_pass" Visible="false" runat="server" Text="<%#(((UBERREP.BusinessLayer.Users.User)Container.DataItem).Password).ToString() %>"></asp:Label>
                      
                    <asp:Label ID="lbl_Email" Visible="false" runat="server" Text="<%#(((UBERREP.BusinessLayer.Users.User)Container.DataItem).Email).ToString() %>"></asp:Label>
                    <asp:Literal ID="Lit_usename" runat="server" Text='<%#((UBERREP.BusinessLayer.Users.User)Container.DataItem).Username%>' />
                  <%--  <asp:TextBox ID="TXT_username" class="m-wrap small fixBig" runat="server" Text='<%#((UBERREP.BusinessLayer.Users.User)Container.DataItem).Username%>' Visible="false" />--%>

                </td>
                <td>
                    <asp:Literal ID="lit_FullName" runat="server" Text='<%#((UBERREP.BusinessLayer.Users.User)Container.DataItem).Name%>' />
                    <asp:TextBox ID="txt_FullName" class="m-wrap small fixSmall" runat="server" Text='<%#((UBERREP.BusinessLayer.Users.User)Container.DataItem).Name%>' Visible="false" />
                </td>
                <td>
                    <asp:Literal ID="lit_Point" runat="server" Text='<%#((UBERREP.BusinessLayer.Users.User)Container.DataItem).Points%>' />
                    <asp:TextBox ID="txt_Point" class="m-wrap small fixSmall" Columns="4" runat="server" Text='<%#((UBERREP.BusinessLayer.Users.User)Container.DataItem).Points%>' Visible="false" />
                </td>
                <td>
                    <asp:Literal ID="lit_Notes" runat="server" Text='<%#((UBERREP.BusinessLayer.Users.User)Container.DataItem).Remarks%>' />
                    <asp:TextBox ID="txt_Notes" class="m-wrap small fixSmall" runat="server" Text='<%#((UBERREP.BusinessLayer.Users.User)Container.DataItem).Remarks%>' Visible="false" />
                </td>
                <td>
                    <asp:LinkButton ID="lnk_Edit" runat="server" Text="Edit" CommandName="Edit" />
                    <asp:LinkButton ID="lnk_Update" Visible="false" runat="server" Text="Save" CommandName="Update" CommandArgument='<%#((UBERREP.BusinessLayer.Users.User)Container.DataItem).ID%>' />

                </td>
                <td>
                    <asp:LinkButton ID="lnk_Cancel" Visible="false" runat="server" Text="Cancel" CommandName="Cancel" />
                    <asp:LinkButton ID="lnk_delete" runat="server" Text="Delete" CommandArgument='<%#((UBERREP.BusinessLayer.Users.User)Container.DataItem).ID%>' CommandName="Delete" OnClientClick='javascript:return confirm("Are you sure you want to delete?")' />

                </td>
            </tr>

        </ItemTemplate>


        <FooterTemplate>
            </tbody>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</div>
