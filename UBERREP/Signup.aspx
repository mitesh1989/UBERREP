<%@ Page Title="" Language="C#" MasterPageFile="~/masterPages/Home.Master" AutoEventWireup="true"
    CodeBehind="Signup.aspx.cs" %>

<%@ Register Src="~/Controls/SignUp.ascx" TagPrefix="uc1" TagName="SignUpUC" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <head>
        <title>Sign Up</title>




        <!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
<![endif]-->

        <link rel="stylesheet" id="contact-form-7-css" href="css/styles.css" type="text/css" media="all">
        <link rel="stylesheet" id="Link1" href="css/elements.css" type="text/css" media="all">

        <link rel="stylesheet" id="rs-settings-css" href="css/settings.css" type="text/css" media="all">
        <link rel="stylesheet" id="rs-captions-css" href="css/dynamic-captions.css" type="text/css" media="all">
        <link rel="stylesheet" id="rs-plugin-static-css" href="css/static-captions.css" type="text/css" media="all">
        <link rel="stylesheet" id="tcsn_typography_Raleway:400,600-css" href="http://fonts.googleapis.com/css?family=Raleway:400,600" type="text/css" media="all">
        <link rel="stylesheet" id="bootstrap-style-css" href="css/bootstrap.css" type="text/css" media="all">
        <link rel="stylesheet" id="style-main-css" href="css/style.css" type="text/css" media="all">
        <link rel="stylesheet" id="elastislide-style-css" href="css/elastislide.css" type="text/css" media="all">
        <link rel="stylesheet" id="bootstrap-override-css" href="css/bootstrap-override.css" type="text/css" media="all">
        <link rel="stylesheet" id="rev-custom-style-css" href="css/rev-custom.css" type="text/css" media="all">
        <link rel="stylesheet" id="responsive-css" href="css/responsive.css" type="text/css" media="all">

        <style type="text/css">
            body {
                font-family: Raleway, Arial, Helvetica, sans-serif;
                font-size: 14px;
                font-weight: normal;
                color: #767676;
            }

            h1 {
                font-family: Raleway, Arial, Helvetica, sans-serif;
                font-size: 36px;
                font-weight: normal;
                color: #646464;
            }

            h2 {
                font-family: Raleway, Arial, Helvetica, sans-serif;
                font-size: 24px;
                font-weight: bold;
                color: #646464;
            }

            h3 {
                font-family: Raleway, Arial, Helvetica, sans-serif;
                font-size: 20px;
                font-weight: bold;
                color: #646464;
            }

            h4, .heading-icon {
                font-family: Raleway, Arial, Helvetica, sans-serif;
                font-size: 18px;
                font-weight: bold;
                color: #646464;
            }

            h5 {
                font-family: Raleway, Arial, Helvetica, sans-serif;
                font-size: 14px;
                font-weight: bold;
                color: #646464;
            }

            h6 {
                font-family: Raleway, Arial, Helvetica, sans-serif;
                font-size: 12px;
                font-weight: bold;
                color: #646464;
            }

            a, a:link {
                color: #d95b44;
            }

                a:hover {
                    color: #767676;
                }

            .inactive-folio-page, .page-links a {
                background-color: #d95b44;
            }

            .logo a {
                font-family: Arial, Arial, Helvetica, sans-serif;
                font-size: 32px;
                font-weight: normal;
                color: #d95b44;
                margin-top: 0px;
            }

                .logo a:hover {
                    color: #d95b44;
                }

            .call-number {
                font-family: Raleway, Arial, Helvetica, sans-serif;
                font-size: 14px;
                font-weight: bold;
                color: #727272;
            }

            .tagline {
                font-family: Raleway, Arial, Helvetica, sans-serif;
                font-size: 14px;
                font-weight: bold;
                color: #727272;
            }

            .ddsmoothmenu ul li a {
                font-family: Raleway, Arial, Helvetica, sans-serif;
                font-size: 12px;
                font-weight: bold;
                color: #a7a7a7;
            }

                .ddsmoothmenu ul li.current-menu-item a, .ddsmoothmenu ul li a:hover {
                    background: #484848;
                    color: #a7a7a7 !important;
                }

            .ddsmoothmenu ul li ul {
                background: #484848;
            }

                .ddsmoothmenu ul li ul li.current-menu-item a, .ddsmoothmenu ul li ul li a:hover {
                    color: #fff !important;
                }

            .menu-link-color a {
                color: #d95b44 !important;
            }

            #top-bar {
                background-color: #0e0e0e;
            }

            #page-header {
                font-family: Raleway, Arial, Helvetica, sans-serif;
                font-size: 12px;
                font-weight: normal;
                color: #727272;
                border-bottom: 1px solid #fff;
            }

                #page-header a {
                    color: #727272;
                }

                    #page-header a:hover {
                        color: #d95b44;
                    }

                #page-header .heading-icon {
                    font-family: Raleway, Arial, Helvetica, sans-serif;
                    font-size: 18px;
                    font-weight: bold;
                    color: #727272;
                }

            .breadcrumbs {
                margin-top: 0px;
            }

                .breadcrumbs > .crumb-active {
                    color: #999;
                }

            #footer {
                font-family: Raleway, Arial, Helvetica, sans-serif;
                font-size: 14px;
                font-weight: normal;
                color: #767676;
            }

                #footer h1, #footer h2, #footer h3, #footer h4, #footer h5, #footer h6 {
                    color: #646464;
                }

            .twitter-box {
                background: #282828;
                border: 1px solid #161616;
            }

            #copyright {
                background: #484848;
                font-family: Raleway, Arial, Helvetica, sans-serif;
                font-size: 11px;
                font-weight: normal;
                color: #242424;
                border-top: 1px solid #5b5c5d;
            }

                #copyright a {
                    color: #242424;
                }

                    #copyright a:hover {
                        color: #fff;
                    }

            .icon-bg, .feature-big-icon .icon-big-bg {
                background: #d95b44;
            }

            .post-title a {
                color: #646464;
            }

            blockquote {
                border-left: 5px solid #d95b44;
            }

                blockquote p, blockquote {
                    color: #d95b44;
                }

                    blockquote.pull-right {
                        border-right: 5px solid #d95b44;
                    }

            .mybtn, .mybtn-big, .mybtn-small, .mybtn-exsmall {
                font-family: Raleway, Arial, Helvetica, sans-serif;
                font-weight: normal;
            }

            .custom-tagcloud a, .custom-tagcloud a:link {
                background: #d95b44;
            }

            .table-th, .table-slug {
                color: #d95b44;
            }

            .color {
                color: #d95b44;
            }
        </style>
        <script async="" src="//www.google-analytics.com/analytics.js"></script>
        <script async="" src="//www.google-analytics.com/analytics.js"></script>
        <script type="text/javascript" src="js/jquery.js"></script>
        <script type="text/javascript" src="js/jquery-migrate.min.js"></script>
        <script type="text/javascript" src="js/anti-spam.js"></script>
        <script type="text/javascript" src="js/jquery.themepunch.plugins.min.js"></script>
        <script type="text/javascript" src="js/jquery.themepunch.revolution.min.js"></script>

        <style>
            body {
                background-color: #f2f2f2;
            }

            #header {
                background-color: #000;
                background-image: url( http://wordpress.tanshcreative.com/grepfrut/wp-content/themes/grepfrut/img/patterns/pat1.jpg );
                background-repeat: repeat;
            }

            #page-header {
                background-color: #e3e3e3;
            }

            #slider-bg {
                background-image: url( http://wordpress.tanshcreative.com/grepfrut/wp-content/themes/grepfrut/img/slider-bg/slider-background1.jpg );
                background-color:;
            }

            #footer {
                background-color: #000;
                background-image: url( http://wordpress.tanshcreative.com/grepfrut/wp-content/themes/grepfrut/img/patterns/pat1.jpg );
                background-repeat: repeat;
            }
        </style>

    </head>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- #header-wrapper -->

    <section id="page-header" class="clearfix">
        <div class="container">
            <div class="row">
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <h4 class="heading-icon entry-title clearfix">
                        <img src="images/heading-icon-user.png" width="40" height="40" alt="icon" class="icon-bg">
                        Sign Up</h4>
                </div>


            </div>
        </div>
    </section>
    <!-- #page-header -->

    <section id="content" class="clearfix">
        <div class="container">
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <article id="post-546" class="post-546 page type-page status-publish hentry">
                        <div class="entry-content">

                            <div class="vc_span12 wpb_column column_container">
                                <div class="wpb_wrapper">
                                </div>
                            </div>
                        </div>
                        <div class="wpb_row vc_row-fluid">
                            <div class="vc_span12 wpb_column column_container">
                                <div class="wpb_wrapper">
                                </div>
                            </div>
                        </div>
                        <div class="wpb_row vc_row-fluid">
                            <div class="sign_up"  >
                                <div class="head">
                                    <h3>Create An Account</h3>
                                    <p></p>
                                </div>

                                <!-- contact us form -->
                                <%--<form method="post" action="#" style="width: 30%;padding:20px; margin: 0 auto; background: none repeat scroll 0 0 #fff; ">
                                    <input type="text" name="vname" value="Name" class="text-name" class="Name" />
                                    <input type="text" name="vemail" value="Email" class="text-name" class="Email" />
                                    <input type="text" name="vemail" value="Password" class="text-name" class="Password" />
                                    <input type="text" name="vemail" value="Conform Password" class="text-name" class="conform_password" />
                                    <button id="reg">Register Now!</button>
                                </form>--%>
                                <uc1:SignUpUC runat="server" id="SignUp" />

                            </div>
                        </div>
                </div>
                </article>
                                          
            </div>
        </div>

    </section>


    <!-- #content -->

</asp:Content>
