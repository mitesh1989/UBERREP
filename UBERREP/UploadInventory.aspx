<%@ Page Title="" Language="C#" MasterPageFile="~/masterPages/Dashboard.Master" AutoEventWireup="true" CodeBehind="UploadInventory.aspx.cs" Inherits="UBERREP.UploadInventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- BEGIN SAMPLE PORTLET CONFIGURATION MODAL FORM-->
    <div id="portlet-config" class="modal hide">
        <div class="modal-header">
            <button data-dismiss="modal" class="close" type="button"></button>
            <h3>portlet Settings</h3>
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
                <h3 class="page-title">Upload Inventory
                </h3>
                <ul class="breadcrumb">
                    <li>
                        <i class="icon-home"></i>
                        <a href="index.html">Home</a>
                        <i class="icon-angle-right"></i>
                    </li>

                    <li><a href="#">Multiple File Upload</a></li>
                </ul>
                <!-- END PAGE TITLE & BREADCRUMB-->
            </div>
        </div>
        <!-- END PAGE HEADER-->
        <!-- BEGIN PAGE CONTENT-->
        <div class="row-fluid">
            <div class="span12">
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
        <!-- END PAGE CONTENT-->
    </div>
    <!-- END PAGE CONTAINER-->
    </div>
</asp:Content>
