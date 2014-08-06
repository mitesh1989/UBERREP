<%@ Page ValidateRequest="false" Title="Manage Client" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Client.aspx.cs" Inherits="UBERREP.Admin.ClientManagement.Client" %>
<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    
    <style type="text/css">
        .align-text-right
        {
            text-align: right;
        }
        .textarea
        {
            height: 20px;   
            outline: 0 none;
            padding-left: 6px;
        }
    </style>
   <%-- <link rel="stylesheet" href="/../code.jquery.com/ui/1.10.4/themes/smoothness/jquery-ui.css" />
  <script src="//code.jquery.com/jquery-1.9.1.js"></script>
  <script src="//code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
  <link rel="stylesheet" href="/resources/demos/style.css" />--%>
    <script type="text/javascript">
    $(function() {
    $( "#dialog" ).dialog({
      autoOpen: false,
      show: {                    
          effect: "blind",
          duration: 500,
          modal: true          
      },
      hide: {
        effect: "explode",
        duration: 200
      }
    });
 
    $( "#opener" ).click(function() {
      $( "#dialog" ).dialog( "open" );
    });
  });
  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageHeader" runat="server">Manage Client
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <table>
                <tr style="padding-bottom:20px;">
                    <td class="align-text-right">Name</td>
                    <td><asp:TextBox ID="TXTClientName" runat="server" MaxLength="500" TabIndex="1"></asp:TextBox>*</td>                            
                </tr>                                
                <tr>
                    <td class="align-text-right">IATA Number</td>
                    <td><asp:TextBox ID="TXTIATA" runat="server" TabIndex="2"></asp:TextBox></td>
                    <td class="align-text-right">ARC Number</td><td><asp:TextBox ID="TXTARC" runat="server" TabIndex="3"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="align-text-right">Status</td>
                    <td><asp:DropDownList ID="DRPStatus" runat="server" TabIndex="4"></asp:DropDownList></td>                                            
                </tr>
                <tr>
                    <td class="align-text-right">Payment API</td>
                    <td><asp:DropDownList ID="DRPAPI" runat="server" TabIndex="5"></asp:DropDownList>*</td> 
                    <td class="align-text-right">Email API</td><td><asp:DropDownList ID="DRPEmailAPI" runat="server" TabIndex="6"></asp:DropDownList>*</td>                   
                </tr>
                <%--<tr>
                    <td class="align-text-right">SMS API</td>
                    <td><asp:DropDownList ID="DRPSMSAPI" runat="server" TabIndex="6"></asp:DropDownList></td>                    
                </tr>                --%>
                <tr>
                    <td class="align-text-right">ClientID</td>
                    <td><asp:TextBox ID="TXTClientID" runat="server" TabIndex="7" ></asp:TextBox>*</td>
                    <td style="padding-left:30px;">Callback URL</td><td><asp:TextBox ID="TXTCallBackURL" AutoCompleteType="None" runat="server" TabIndex="8"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="align-text-right">Password</td>
                    <td><asp:TextBox ID="TXTPassword" MaxLength="50" TextMode="Password" runat="server" AutoCompleteType="None" TabIndex="9"></asp:TextBox>*</td>
                    <td class="align-text-right">Confirm Password</td><td><asp:TextBox ID="TXTConfirmPassword" AutoCompleteType="None"   MaxLength="50" TextMode="Password" runat="server" TabIndex="10"></asp:TextBox>*</td>
                </tr>                
                <tr>
                    <td class="align-text-right">Email</td>
                    <td><asp:TextBox ID="TXTEmail" runat="server" TabIndex="11"></asp:TextBox>*</td>
                    <td class="align-text-right">Contact Number</td><td><asp:TextBox runat="server" ID="TXTCellNo" TabIndex="12"></asp:TextBox></td>
                </tr>  
                <tr>
                    <td class="align-text-right">Email Subject</td>
                    <td><asp:TextBox ID="TXTEmailSubject" runat="server" TabIndex="13"></asp:TextBox>*</td>
                    <td class="align-text-right">Payment Email Subject</td><td><asp:TextBox runat="server" ID="TXTPaymentSubject" TabIndex="14"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="align-text-right">CC Receipent(s)</td>
                    <td><asp:TextBox ID="TXTCCReceipent" runat="server" TabIndex="15"></asp:TextBox>*</td>
                    <td class="align-text-right">BCC Receipent(s)</td><td><asp:TextBox runat="server" ID="TXTBCCReceipent" TabIndex="16"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Website URL</td>
                    <td><asp:TextBox ID="TXTWebsiteURL" runat="server" TabIndex="17" ></asp:TextBox></td>            
                    <td>Privacy Policy URL</td>
                    <td><asp:TextBox ID="TXTPrivacyPolicy" runat="server" TabIndex="18" ></asp:TextBox></td>            
                </tr>
                <tr>
                    <td>Customer Support Email</td>
                    <td><asp:TextBox ID="TXTSupportEmail" runat="server" TabIndex="19" ToolTip="This is the email to be displayed for further customer support, in case customer is not able to verify his payment" ></asp:TextBox></td>
                    <td>Customer Support Phone</td>
                    <td><asp:TextBox ID="TXTSupportPhone" runat="server" TabIndex="20" ToolTip="This is the phone to be displayed for further customer support, in case customer is not able to verify his payment" ></asp:TextBox></td>
                </tr>                                        
                <tr>   
                    <td></td>       
                    <td> 
                        <% if (( UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser != null && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections != null && UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections.Count > 0 && (UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections[UBERREP.BusinessLayer.Users.Section.SectionCodes.OFC_MGNT].HasInsert || UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.UserAllowedSections[UBERREP.BusinessLayer.Users.Section.SectionCodes.OFC_MGNT].HasUpdate)))
                           { %>
                        <asp:Button ID="BTNSave" TabIndex="24" runat="server" Text="Save" onclick="BTNSave_Click"/>
                        <asp:Button ID="BTNCancel" TabIndex="25" runat="server" Text="Cancel" onclick="BTNCancel_Click" />
                        <%} %>
                    </td>
                </tr>                
            </table>        
    <div id="dialog" title="Basic dialog">
        <p>
            &lt;&lt;CustomerName&gt;&gt; - Customer Name<br />
            &lt;&lt;Client&gt;&gt; - Client Name<br />
            &lt;&lt;VerificationURL&gt;&gt; - Payment Verification URL<br />
            &lt;&lt;RawVerificationURL&gt;&gt; - Raw Verification URL<br />            
            &lt;&lt;ClientPrivacyPolicyURL&gt;&gt; - Client's Privacy Policy<br />
            &lt;&lt;CCLast4Digits&gt;&gt; - Credit Card's Last 4 Digits to be shown<br />
            &lt;&lt;MerchantName&gt;&gt; - Merchant Name to display in Payment Email<br />
        </p>
    </div>
</asp:Content>
