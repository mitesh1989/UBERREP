<%@ Page Title="" Language="C#" MasterPageFile="~/masterPages/Dashboard.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="UBERREP.Dashboard" %>


<%@ Register Src="~/Controls/MyAccount.ascx" TagPrefix="uc1" TagName="MyAccount" %>
<%@ Register Src="~/Controls/SalesList.ascx" TagPrefix="uc1" TagName="SalesList" %>
<%@ Register Src="~/Controls/RetailerList.ascx" TagPrefix="uc1" TagName="RetailerList" %>





<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .m-wrap.fixBig
        {
            width: 135px !important;
        }

        .m-wrap.fixSmall
        {
            width: 40px !important;
        }

        input[type="text"].m-wrap
        {
            margin-bottom: 0px !important;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- BEGIN SAMPLE PORTLET CONFIGURATION MODAL FORM-->
    <div id="portlet-config" class="modal hide">
        <div class="modal-header">
            <button data-dismiss="modal" class="close" type="button"></button>
            <h3>Widget Settings</h3>
        </div>
        <div class="modal-body">
            <p>Here will be a configuration form</p>
        </div>
    </div>
    <!-- END SAMPLE PORTLET CONFIGURATION MODAL FORM-->
    <!-- BEGIN PAGE CONTAINER-->
    <div class="container-fluid">
        <!-- BEGIN PAGE HEADER-->
        <div class="row-fluid">
            <div class="span12">
                <!-- BEGIN STYLE CUSTOMIZER -->
                <div class="color-panel hidden-phone">

                    <div class="color-mode-icons icon-color-close"></div>
                    <div class="color-mode">
                        <p>THEME COLOR</p>
                        <ul class="inline">
                            <li class="color-black current color-default" data-style="default"></li>
                            <li class="color-blue" data-style="blue"></li>
                            <li class="color-brown" data-style="brown"></li>
                            <li class="color-purple" data-style="purple"></li>
                            <li class="color-white color-light" data-style="light"></li>
                        </ul>
                        <label class="hidden-phone">
                            <input type="checkbox" class="header" checked value="" />
                            <span class="color-mode-label">Fixed Header</span>
                        </label>
                    </div>
                </div>
                <!-- END BEGIN STYLE CUSTOMIZER -->
                <!-- BEGIN PAGE TITLE & BREADCRUMB-->
                <h3 class="page-title">
                    <%if (UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser != null)
                      {%>Hi, <%=UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.Name %><%} %> 
                            Dashboard				
                </h3>
                <ul class="breadcrumb">
                    <li>
                        <i class="icon-home"></i>
                        <a href="index.html">Home</a>
                        <i class="icon-angle-right"></i>
                    </li>
                    <li><a href="#">Dashboard</a></li>
                    <li class="pull-right no-text-shadow">
                        <div id="dashboard-report-range" class="dashboard-date-range tooltips no-tooltip-on-touch-device responsive" data-tablet="" data-desktop="tooltips" data-placement="top" data-original-title="Change dashboard date range">
                            <i class="icon-calendar"></i>
                            <span></span>
                            <i class="icon-angle-down"></i>
                        </div>
                    </li>
                </ul>
                <!-- END PAGE TITLE & BREADCRUMB-->
            </div>
        </div>
        <!-- END PAGE HEADER-->
        <div id="dashboard">
            <!-- BEGIN DASHBOARD STATS -->
            <div class="row-fluid">
                <div class="span6">
                    <div class="portlet box blue" id="form_wizard_1">
                        <uc1:MyAccount runat="server" ID="MyAccount" />
                    </div>
                </div>


                <div class="span6">
                    <!-- BEGIN EXAMPLE TABLE PORTLET-->
                    <div class="portlet box blue">
                        <div class="portlet-title">
                            <h4><i class="icon-edit"></i>Sales</h4>
                            <div class="tools">
                                <a href="javascript:;" class="collapse"></a>
                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                <a href="javascript:;" class="reload"></a>
                            </div>
                        </div>

                        <div class="portlet-body">

                            <uc1:SalesList runat="server" id="SalesList" />
                        </div>

                        <!-- END EXAMPLE TABLE PORTLET-->

                        <div class="portlet box blue">
                            <div class="portlet-title">
                                <h4><i class="icon-edit"></i>Retailer</h4>
                                <div class="tools">
                                    <a href="javascript:;" class="collapse"></a>
                                    <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                    <a href="javascript:;" class="reload"></a>
                                </div>
                            </div>
                            <div class="portlet-body">
                                <uc1:RetailerList runat="server" id="RetailerList" />
                                
                            </div>

                        </div>
                    </div>
                </div>
                <!-- END DASHBOARD STATS -->

                <div class="clearfix"></div>
                <div class="row-fluid">
                    <div class="span12">
                        <div class="portlet box blue" id="Div1">
                            <div class="portlet-title">
                                <h4>
                                    <i class="icon-reorder"></i>Upload Inventory</span>
                                </h4>
                                <div class="tools hidden-phone">
                                    <a href="javascript:;" class="collapse"></a>
                                    <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                    <a href="javascript:;" class="reload"></a>
                                    <a href="javascript:;" class="remove"></a>
                                </div>
                            </div>
                            <div class="portlet-body">
                                <blockquote>
                                    <p style="font-size: 16px">
                                        File Upload widget with multiple file selection, drag&amp;drop support, progress bars and preview images for jQuery.<br>
                                        Supports cross-domain, chunked and resumable file uploads and client-side image resizing.<br>
                                        Works with any server-side platform (PHP, Python, Ruby on Rails, Java, Node.js, Go etc.) that supports standard HTML form file uploads.
                                    </p>
                                </blockquote>
                                <br>
                                <!-- The file upload form used as target for the file upload widget -->
                                <form id="fileupload" action="//jquery-file-upload.appspot.com/" method="POST" enctype="multipart/form-data">
                                    <!-- Redirect browsers with JavaScript disabled to the origin page -->
                                    <noscript>
                                        <input type="hidden" name="redirect" value="http://blueimp.github.com/jQuery-File-Upload/">
                                    </noscript>
                                    <!-- The fileupload-buttonbar contains buttons to add/delete files and start/cancel the upload -->
                                    <div class="row-fluid fileupload-buttonbar">
                                        <div class="span9">
                                            <!-- The fileinput-button span is used to style the file input field as button -->
                                            <span class="btn green fileinput-button">
                                                <i class="icon-plus icon-white"></i>
                                                <span>Add files...</span>
                                                <input type="file" name="files[]" multiple>
                                            </span>
                                            <button type="submit" class="btn blue start">
                                                <i class="icon-upload icon-white"></i>
                                                <span>Start upload</span>
                                            </button>
                                            <button type="reset" class="btn yellow cancel">
                                                <i class="icon-ban-circle icon-white"></i>
                                                <span>Cancel upload</span>
                                            </button>
                                            <button type="button" class="btn red delete">
                                                <i class="icon-trash icon-white"></i>
                                                <span>Delete</span>
                                            </button>
                                            <input type="checkbox" class="toggle fileupload-toggle-checkbox">
                                        </div>
                                        <!-- The global progress information -->
                                        <div class="span5 fileupload-progress fade">
                                            <!-- The global progress bar -->
                                            <div class="progress progress-success progress-striped active" role="progressbar" aria-valuemin="0" aria-valuemax="100">
                                                <div class="bar" style="width: 0%;"></div>
                                            </div>
                                            <!-- The extended global progress information -->
                                            <div class="progress-extended">&nbsp;</div>
                                        </div>
                                    </div>
                                    <!-- The loading indicator is shown during file processing -->
                                    <div class="fileupload-loading"></div>
                                    <br>
                                    <!-- The table listing the files available for upload/download -->
                                    <table role="presentation" class="table table-striped">
                                        <tbody class="files" data-toggle="modal-gallery" data-target="#modal-gallery"></tbody>
                                    </table>
                                </form>
                                <br>
                                <div class="well">
                                    <h3>Demo Notes</h3>
                                    <ul>
                                        <li>The maximum file size for uploads in this demo is <strong>5 MB</strong> (default file size is unlimited).</li>
                                        <li>Only image files (<strong>JPG, GIF, PNG</strong>) are allowed in this demo (by default there is no file type restriction).</li>
                                        <li>Uploaded files will be deleted automatically after <strong>5 minutes</strong> (demo setting).</li>
                                        <li>You can <strong>drag &amp; drop</strong> files from your desktop on this webpage with Google Chrome, Mozilla Firefox and Apple Safari.</li>

                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row-fluid">
                    <div class="span12">
                        <script id="template-upload" type="text/x-tmpl">
							{% for (var i=0, file; file=o.files[i]; i++) { %}
							    <tr class="template-upload fade">
							        <td class="preview"><span class="fade"></span></td>
							        <td class="name"><span>{%=file.name%}</span></td>
							        <td class="size"><span>{%=o.formatFileSize(file.size)%}</span></td>
							        {% if (file.error) { %}
							            <td class="error" colspan="2"><span class="label label-important">Error</span> {%=file.error%}</td>
							        {% } else if (o.files.valid && !i) { %}
							            <td>
							                <div class="progress progress-success progress-striped active" role="progressbar" aria-valuemin="0" aria-valuemax="100" aria-valuenow="0"><div class="bar" style="width:0%;"></div></div>
							            </td>
							            <td class="start">{% if (!o.options.autoUpload) { %}
							                <button class="btn">
							                    <i class="icon-upload icon-white"></i>
							                    <span>Start</span>
							                </button>
							            {% } %}</td>
							        {% } else { %}
							            <td colspan="2"></td>
							        {% } %}
							        <td class="cancel">{% if (!i) { %}
							            <button class="btn red">
							                <i class="icon-ban-circle icon-white"></i>
							                <span>Cancel</span>
							            </button>
							        {% } %}</td>
							    </tr>
							{% } %}
                        </script>
                        <!-- The template to display files available for download -->
                        <script id="template-download" type="text/x-tmpl">
							{% for (var i=0, file; file=o.files[i]; i++) { %}
							    <tr class="template-download fade">
							        {% if (file.error) { %}
							            <td></td>
							            <td class="name"><span>{%=file.name%}</span></td>
							            <td class="size"><span>{%=o.formatFileSize(file.size)%}</span></td>
							            <td class="error" colspan="2"><span class="label label-important">Error</span> {%=file.error%}</td>
							        {% } else { %}
							            <td class="preview">
							            {% if (file.thumbnail_url) { %}
							                <a class="fancybox-button" data-rel="fancybox-button" href="{%=file.url%}" title="{%=file.name%}">
							                	<img src="{%=file.thumbnail_url%}">
							                </a>
							            {% } %}</td>
							            <td class="name">
							                <a href="{%=file.url%}" title="{%=file.name%}" data-gallery="{%=file.thumbnail_url&&'gallery'%}" download="{%=file.name%}">{%=file.name%}</a>
							            </td>
							            <td class="size"><span>{%=o.formatFileSize(file.size)%}</span></td>
							            <td colspan="2"></td>
							        {% } %}
							        <td class="delete">
							            <button class="btn red" data-type="{%=file.delete_type%}" data-url="{%=file.delete_url%}"{% if (file.delete_with_credentials) { %} data-xhr-fields='{"withCredentials":true}'{% } %}>
							                <i class="icon-trash icon-white"></i>
							                <span>Delete</span>
							            </button>
							            <input type="checkbox" class="fileupload-checkbox hide" name="delete" value="1">
							        </td>
							    </tr>
							{% } %}
                        </script>
                    </div>
                </div>



                <div class="row-fluid">
                    <div class="span12">
                        <!-- BEGIN PORTLET-->
                        <div class="portlet paddingless">
                            <div class="portlet-title line">
                                <h4><i class="icon-bell"></i>Feeds</h4>

                            </div>
                            <div class="portlet-body">
                                <!--BEGIN TABS-->
                                <div class="tabbable tabbable-custom">
                                    <ul class="nav nav-tabs">
                                        <li class="active"><a href="#tab_1_1" data-toggle="tab">System</a></li>
                                        <li><a href="#tab_1_2" data-toggle="tab">Activities</a></li>
                                        <li><a href="#tab_1_3" data-toggle="tab">Recent Users</a></li>
                                    </ul>
                                    <div class="tab-content">
                                        <div class="tab-pane active" id="tab_1_1">
                                            <div class="scroller" data-height="290px" data-always-visible="1" data-rail-visible1="1">
                                                <ul class="feeds">
                                                    <li>
                                                        <div class="col1">
                                                            <div class="cont">
                                                                <div class="cont-col1">
                                                                    <div class="label label-success">
                                                                        <i class="icon-bell"></i>
                                                                    </div>
                                                                </div>
                                                                <div class="cont-col2">
                                                                    <div class="desc">
                                                                        You have 4 pending tasks.
																			<span class="label label-important label-mini">Take action 
																			<i class="icon-share-alt"></i>
                                                                            </span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col2">
                                                            <div class="date">
                                                                Just now
                                                            </div>
                                                        </div>
                                                    </li>
                                                    <li>
                                                        <a href="#">
                                                            <div class="col1">
                                                                <div class="cont">
                                                                    <div class="cont-col1">
                                                                        <div class="label label-success">
                                                                            <i class="icon-bell"></i>
                                                                        </div>
                                                                    </div>
                                                                    <div class="cont-col2">
                                                                        <div class="desc">
                                                                            New version v1.4 just lunched!	
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col2">
                                                                <div class="date">
                                                                    20 mins
                                                                </div>
                                                            </div>
                                                        </a>
                                                    </li>

                                                </ul>
                                            </div>
                                        </div>
                                        <div class="tab-pane" id="tab_1_2">
                                            <div class="scroller" data-height="290px" data-always-visible="1" data-rail-visible1="1">
                                                <ul class="feeds">
                                                    <li>
                                                        <a href="#">
                                                            <div class="col1">
                                                                <div class="cont">
                                                                    <div class="cont-col1">
                                                                        <div class="label label-success">
                                                                            <i class="icon-bell"></i>
                                                                        </div>
                                                                    </div>
                                                                    <div class="cont-col2">
                                                                        <div class="desc">
                                                                            New user registered
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col2">
                                                                <div class="date">
                                                                    Just now
                                                                </div>
                                                            </div>
                                                        </a>
                                                    </li>
                                                    <li>
                                                        <a href="#">
                                                            <div class="col1">
                                                                <div class="cont">
                                                                    <div class="cont-col1">
                                                                        <div class="label label-success">
                                                                            <i class="icon-bell"></i>
                                                                        </div>
                                                                    </div>
                                                                    <div class="cont-col2">
                                                                        <div class="desc">
                                                                            New order received 
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col2">
                                                                <div class="date">
                                                                    10 mins
                                                                </div>
                                                            </div>
                                                        </a>
                                                    </li>


                                                </ul>
                                            </div>
                                        </div>
                                        <div class="tab-pane" id="tab_1_3">
                                            <div class="scroller" data-height="290px" data-always-visible="1" data-rail-visible1="1">
                                                <div class="row-fluid">
                                                    <div class="span6 user-info">
                                                        <img alt="" src="assets/img/avatar.png" />
                                                        <div class="details">
                                                            <div>
                                                                <a href="#">Robert Nilson</a>
                                                                <span class="label label-success">Approved</span>
                                                            </div>
                                                            <div>29 Jan 2013 10:45AM</div>
                                                        </div>
                                                    </div>
                                                    <div class="span6 user-info">
                                                        <img alt="" src="assets/img/avatar.png" />
                                                        <div class="details">
                                                            <div>
                                                                <a href="#">Lisa Miller</a>
                                                                <span class="label label-info">Pending</span>
                                                            </div>
                                                            <div>19 Jan 2013 10:45AM</div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row-fluid">
                                                    <div class="span6 user-info">
                                                        <img alt="" src="assets/img/avatar.png" />
                                                        <div class="details">
                                                            <div>
                                                                <a href="#">Eric Kim</a>
                                                                <span class="label label-info">Pending</span>
                                                            </div>
                                                            <div>19 Jan 2013 12:45PM</div>
                                                        </div>
                                                    </div>
                                                    <div class="span6 user-info">
                                                        <img alt="" src="assets/img/avatar.png" />
                                                        <div class="details">
                                                            <div>
                                                                <a href="#">Lisa Miller</a>
                                                                <span class="label label-important">In progress</span>
                                                            </div>
                                                            <div>19 Jan 2013 11:55PM</div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row-fluid">
                                                    <div class="span6 user-info">
                                                        <img alt="" src="assets/img/avatar.png" />
                                                        <div class="details">
                                                            <div>
                                                                <a href="#">Eric Kim</a> <span class="label label-info">Pending</span>
                                                            </div>
                                                            <div>19 Jan 2013 12:45PM</div>
                                                        </div>
                                                    </div>
                                                    <div class="span6 user-info">
                                                        <img alt="" src="assets/img/avatar.png" />
                                                        <div class="details">
                                                            <div>
                                                                <a href="#">Lisa Miller</a>
                                                                <span class="label label-important">In progress</span>
                                                            </div>
                                                            <div>19 Jan 2013 11:55PM</div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!--END TABS-->
                            </div>
                        </div>
                        <!-- END PORTLET-->
                    </div>
                </div>
                <div class="clearfix"></div>
                <div class="row-fluid">
                    <div class="span6">
                        <!-- BEGIN PORTLET-->

                        <!-- END PORTLET-->
                    </div>
                    <div class="span6">
                    </div>
                </div>
            </div>
        </div>
        <!-- END PAGE CONTAINER-->


        <h3><%if (UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser != null)
              {%>Hi, <%=UBERREP.BusinessLayer.Common.CurrentContext.CurrentUser.Name %><%} %></h3>
</asp:Content>
