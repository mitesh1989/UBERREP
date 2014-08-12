<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="UBERREP.WebForm1" %>

<%@ Register Src="~/Controls/Login.ascx" TagPrefix="uc1" TagName="Login" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-1.4.1.js"></script>
    <script src="Scripts/jquery-1.4.1.min.js"></script>
    <script src="Scripts/jquery-1.4.1-vsdoc.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:Login runat="server" ID="Login" />
    </div>
    </form>
</body>
</html>
