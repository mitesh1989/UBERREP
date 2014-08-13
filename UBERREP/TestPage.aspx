<%@ Page Title="" Language="C#" MasterPageFile="~/masterPages/Home.Master" AutoEventWireup="true" CodeBehind="TestPage.aspx.cs" %>

<%@ Register Src="~/Controls/Login.ascx" TagPrefix="uc1" TagName="Login" %>
<%@ Register Src="~/Controls/UsersList.ascx" TagPrefix="uc1" TagName="UsersList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">




    <script type="text/javascript">
        function ShowCurrentTime() {
            alert('hi');
            $.ajax({
                type: "POST",
                url: "TestPage.aspx/GetCurrentTime",
                data: '{name: "' + $("#<%=TXTUserName.ClientID%>")[0].value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    alert(response.d);
                }
            });
        }
        function OnSuccess(response) {
            alert(response.d);
        }
    </script>

    <asp:TextBox AutoCompleteType="FirstName" TabIndex="1" ID="TXTUserName" runat="server" MaxLength="50" PlaceHolder="Your Name" CssClass="text-name"></asp:TextBox>
    <input id="btnGetTime" type="button" value="Show Current Time"
        onclick="ShowCurrentTime()" />


    <uc1:UsersList runat="server" id="UsersListUC" />

</asp:Content>
