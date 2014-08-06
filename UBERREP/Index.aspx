<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true"
    CodeBehind="Index.aspx.cs" Inherits="UBERREP.Admin.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DataContentPlaceHolder" runat="server">
    <!-- #header-wrapper -->
    <div id="slider-bg">
        <div class="container">
            <!-- START REVOLUTION SLIDER 4.0.5 fullwidth mode -->
            <link href='http://fonts.googleapis.com/css?family=Raleway:400,700' rel='stylesheet'
                type='text/css'>
            <script type="text/javascript" src="js/jquery.themepunch.plugins.min.js"></script>
            <script type="text/javascript" src="js/jquery.themepunch.revolution.min.js"></script>
            <div id="rev_slider_2_1_wrapper" class="rev_slider_wrapper fullwidthbanner-container"
                style="margin: 0px auto; padding: 0px; margin-top: 30px; margin-bottom: 20px;
                max-height: 500px;">
                <div id="rev_slider_2_1" class="rev_slider fullwidthabanner" style="display: none;
                    max-height: 500px; height: 500;">
                    <ul>
                        <!-- SLIDE  -->
                        <li data-transition="slidehorizontal" data-slotamount="3" data-masterspeed="300"
                            data-link="slide" data-linktoslide="next" data-thumb="images/slide1-thumb.png">
                            <!-- MAIN IMAGE -->
                            <img src="images/dummy.png" alt="tab-slide1" data-lazyload="images/transparent.png"
                                data-bgfit="100% 100%" data-bgposition="center center" data-bgrepeat="no-repeat">
                            <!-- LAYERS -->
                            <!-- LAYER NR. 1 -->
                            <div class="tp-caption sfr str" data-x="right" data-hoffset="0" data-y="0" data-speed="300"
                                data-start="500" data-easing="Power3.easeInOut" data-endspeed="300" data-endeasing="Power3.easeInOut"
                                style="z-index: 2">
                                <img src="images/tab-slide1.png" alt="">
                            </div>
                            <!-- LAYER NR. 2 -->
                            <div class="tp-caption custom_heading_bold_orange sfl stl tp-resizeme" data-x="0"
                                data-y="10" data-speed="300" data-start="800" data-easing="Power3.easeInOut"
                                data-endspeed="300" data-endeasing="Power3.easeInOut" style="z-index: 3">
                                No more rough seas with</br>sales reps in the dark.</br>Effortless transactions</br>and
                                simplified sales.
                            </div>
                            <!-- LAYER NR. 3 -->
                            <div class="tp-caption custom_small_text sfl stl tp-resizeme" data-x="0" data-y="200"
                                data-speed="300" data-start="1100" data-easing="Power3.easeInOut" data-endspeed="300"
                                data-endeasing="Power3.easeInOut" style="z-index: 4">
                                <!--Autem vel eum iriure dolor in hendrerit in vulputate velit esse molestie</br> consequat, vel illum qui blandit praesent feugiat nulla facilisis at vero eros et</br> accumsan et iusto odio dignissim qui blandit praesent luptatum</br> zzril delenit augue duis dolore te feugait nulla facilisi. -->
                            </div>
                        </li>
                        <!-- SLIDE  -->
                        <li data-transition="slidehorizontal" data-slotamount="3" data-masterspeed="300"
                            data-link="slide" data-linktoslide="next" data-thumb="images/slide2-thumb.png">
                            <!-- MAIN IMAGE -->
                            <img src="images/dummy.png" alt="wp-content" data-lazyload="images/transparent.png"
                                data-bgfit="cover" data-bgposition="center center" data-bgrepeat="no-repeat">
                            <!-- LAYERS -->
                            <!-- LAYER NR. 1 -->
                            <div class="tp-caption custom_heading_bold_orange sfl stl tp-resizeme" data-x="0"
                                data-y="10" data-speed="300" data-start="800" data-easing="Power3.easeInOut"
                                data-endspeed="300" data-endeasing="Power3.easeInOut" style="z-index: 2">
                                Empower clients</br> with one small step.</br>On the spot prices and</br>accurate
                                order histories.
                            </div>
                            <!-- LAYER NR. 2 -->
                            <div class="tp-caption sfr str" data-x="right" data-hoffset="0" data-y="0" data-speed="300"
                                data-start="500" data-easing="Power3.easeInOut" data-endspeed="300" data-endeasing="Power3.easeInOut"
                                style="z-index: 3">
                                <img src="images/tab-slide2.png" alt="">
                            </div>
                            <!-- LAYER NR. 3 -->
                            <div class="tp-caption custom_small_text sfl stl tp-resizeme" data-x="0" data-y="200"
                                data-speed="300" data-start="1100" data-easing="Power3.easeInOut" data-endspeed="300"
                                data-endeasing="Power3.easeInOut" style="z-index: 4">
                                <!--Duis autem vel eum iriure dolor in hendrerit in vulputate velit esse</br> molestie consequat, vel illum dolore eu feugiat nulla facilisis at vero</br>eros et accumsan et iusto odio dignissim qui zzril delenit quam littera</br>qui blandit praesent augue duis dolore te feugait nulla facilisi. -->
                            </div>
                        </li>
                        <!-- SLIDE  -->
                        <li data-transition="slidehorizontal" data-slotamount="3" data-masterspeed="300"
                            data-link="slide" data-linktoslide="next" data-thumb="images/slide3-thumb.png">
                            <!-- MAIN IMAGE -->
                            <img src="images/dummy.png" alt="wp-content" data-lazyload="images/transparent.png"
                                data-bgfit="cover" data-bgposition="center center" data-bgrepeat="no-repeat">
                            <!-- LAYERS -->
                            <!-- LAYER NR. 1 -->
                            <div class="tp-caption sfr str" data-x="right" data-hoffset="0" data-y="0" data-speed="300"
                                data-start="500" data-easing="Power3.easeInOut" data-endspeed="300" data-endeasing="Power3.easeInOut"
                                style="z-index: 2">
                                <img src="images/tab-slide3.png" alt="">
                            </div>
                            <!-- LAYER NR. 2 -->
                            <div class="tp-caption custom_heading_bold_orange sfl stl tp-resizeme" data-x="0"
                                data-y="10" data-speed="300" data-start="800" data-easing="Power3.easeInOut"
                                data-endspeed="300" data-endeasing="Power3.easeInOut" style="z-index: 3">
                                Don't let the office</br>hold you back.</br>Instant mobile access to</br>inventory
                                and sales records.
                            </div>
                            <!-- LAYER NR. 3 -->
                            <div class="tp-caption custom_small_text sfl stl tp-resizeme" data-x="0" data-y="200"
                                data-speed="300" data-start="1100" data-easing="Power3.easeInOut" data-endspeed="300"
                                data-endeasing="Power3.easeInOut" style="z-index: 4">
                                <!--Autem vel eum iriure dolor in hendrerit in vulputate velit esse molestie</br>consequat, vel illum feugiat nulla facilisis at vero eros et accumsan</br>iusto odio dignissim qui blandit praesent luptatum zzril in iis qui facit</br> eorum delenit augue duis dolore te feugait nulla facilisi. -->
                            </div>
                        </li>
                        <!-- SLIDE  -->
                        <li data-transition="slidehorizontal" data-slotamount="3" data-masterspeed="300"
                            data-link="slide" data-linktoslide="next" data-thumb="images/slide4-thumb.png">
                            <!-- MAIN IMAGE -->
                            <img src="images/dummy.png" alt="wp-content" data-lazyload="images/transparent.png"
                                data-bgfit="cover" data-bgposition="center center" data-bgrepeat="no-repeat">
                            <!-- LAYERS -->
                            <!-- LAYER NR. 1 -->
                            <div class="tp-caption sfr str" data-x="right" data-hoffset="0" data-y="30" data-speed="300"
                                data-start="500" data-easing="Power3.easeInOut" data-endspeed="300" data-endeasing="Power3.easeInOut"
                                style="z-index: 2">
                                <img src="images/tab-slide4.png" alt="">
                            </div>
                            <!-- LAYER NR. 2 -->
                            <div class="tp-caption custom_heading_bold_orange sfl stl tp-resizeme" data-x="0"
                                data-y="10" data-speed="300" data-start="800" data-easing="Power3.easeInOut"
                                data-endspeed="300" data-endeasing="Power3.easeInOut" style="z-index: 3">
                                Give bills a break.</br>Streamlined orders with</br> flawless integration and </br>secure
                                invoice payments.
                            </div>
                            <!-- LAYER NR. 3 -->
                            <div class="tp-caption custom_small_text sfl stl tp-resizeme" data-x="0" data-y="200"
                                data-speed="300" data-start="1100" data-easing="Power3.easeInOut" data-endspeed="300"
                                data-endeasing="Power3.easeInOut" style="z-index: 4">
                                <!--Duis autem vel eum iriure dolor in hendrerit in vulputate velit esse</br>molestie consequat, vel illum dolore eu feugiat nulla facilisis at vero</br>eros accumsan et iusto odio dignissim qui qui blandit praesent zzril</br>gothica delenit augue duis dolore te feugait nulla facilisi. -->
                            </div>
                        </li>
                        <!-- SLIDE  -->
                        <li data-transition="slidehorizontal" data-slotamount="3" data-masterspeed="300"
                            data-link="slide" data-linktoslide="next" data-thumb="images/slide5-thumb.png">
                            <!-- MAIN IMAGE -->
                            <img src="images/dummy.png" alt="wp-content" data-lazyload="images/transparent.png"
                                data-bgfit="cover" data-bgposition="center center" data-bgrepeat="no-repeat">
                            <!-- LAYERS -->
                            <!-- LAYER NR. 1 -->
                            <div class="tp-caption sfr str" data-x="right" data-hoffset="0" data-y="0" data-speed="300"
                                data-start="500" data-easing="Power3.easeInOut" data-endspeed="300" data-endeasing="Power3.easeInOut"
                                style="z-index: 2">
                                <img src="images/tab-slide5.png" alt="">
                            </div>
                            <!-- LAYER NR. 2 -->
                            <div class="tp-caption custom_heading_bold_orange sfl stl tp-resizeme" data-x="0"
                                data-y="10" data-speed="300" data-start="800" data-easing="Power3.easeInOut"
                                data-endspeed="300" data-endeasing="Power3.easeInOut" style="z-index: 3">
                                Handwritten forms are so 1900s.</br>Paperless submissions and</br>tracking in record
                                time.
                            </div>
                            <!-- LAYER NR. 3 -->
                            <div class="tp-caption custom_small_text sfl stl tp-resizeme" data-x="0" data-y="160"
                                data-speed="300" data-start="1100" data-easing="Power3.easeInOut" data-endspeed="300"
                                data-endeasing="Power3.easeInOut" style="z-index: 4">
                                <!--Autem vel eum iriure dolor in hendrerit in vulputate velit esse molestie</br>consequat, vel illum feugiat nulla facilisis at vero eros accumsan </br>iusto odio dignissim qui blandit praesent luptatum zzril delenit augue</br>duis dolore te qui blandit praesent feugait nulla facilisi. -->
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
            <script type="text/javascript">

                var tpj = jQuery;
                tpj.noConflict();
                var revapi2;

                tpj(document).ready(function () {

                    if (tpj('#rev_slider_2_1').revolution == undefined)
                        revslider_showDoubleJqueryError('#rev_slider_2_1');
                    else
                        revapi2 = tpj('#rev_slider_2_1').show().revolution(
					{
					    delay: 4000,
					    startwidth: 940,
					    startheight: 500,
					    hideThumbs: 0,

					    thumbWidth: 180,
					    thumbHeight: 74,
					    thumbAmount: 5,

					    navigationType: "thumb",
					    navigationArrows: "none",
					    navigationStyle: "navbar-old",

					    touchenabled: "on",
					    onHoverStop: "on",

					    navigationHAlign: "center",
					    navigationVAlign: "bottom",
					    navigationHOffset: 0,
					    navigationVOffset: 20,

					    soloArrowLeftHalign: "left",
					    soloArrowLeftValign: "center",
					    soloArrowLeftHOffset: -70,
					    soloArrowLeftVOffset: 0,

					    soloArrowRightHalign: "right",
					    soloArrowRightValign: "center",
					    soloArrowRightHOffset: -70,
					    soloArrowRightVOffset: 0,

					    shadow: 0,
					    fullWidth: "on",
					    fullScreen: "off",

					    stopLoop: "off",
					    stopAfterLoops: -1,
					    stopAtSlide: -1,


					    shuffle: "off",

					    autoHeight: "off",
					    forceFullWidth: "off",

					    hideThumbsOnMobile: "on",
					    hideBulletsOnMobile: "off",
					    hideArrowsOnMobile: "off",
					    hideThumbsUnderResolution: 0,

					    hideSliderAtLimit: 0,
					    hideCaptionAtLimit: 0,
					    hideAllCaptionAtLilmit: 0,
					    startWithSlide: 0,
					    videoJsPath: "#",
					    fullScreenOffsetContainer: ""
					});

                }); //ready
				
            </script>
            <!-- END REVOLUTION SLIDER -->
        </div>
    </div>
    <!-- #slider-bg -->
    <section id="content" class="clearfix">
    <div class="container">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12">
                                <article id="post-45" class="post-45 page type-page status-publish hentry">
                    <div class="entry-content">
                        <div class="wpb_row vc_row-fluid">
	<div class="vc_span12 wpb_column column_container">
		<div class="wpb_wrapper">
			
	<div class="wpb_text_column wpb_content_element ">
		<div class="wpb_wrapper">
			<h1 class="entry-title" style="text-align: center;">Wholesalers,<span class="color"> Retailers and Sales</span> Reps Unite</h1>
			<h3 class="entry-title" style="text-align: center;">Because There's No Third Wheel with<span class="color"> UberReps</span></h3>
			

		</div>   
	</div>   
		</div> 
	</div> 
</div><div class="wpb_row vc_row-fluid">
	<div class="vc_span4 wpb_column column_container">
		<div class="wpb_wrapper">
			
	<div class="wpb_text_column wpb_content_element ">
		<div class="wpb_wrapper">
			<div class="feature-big-icon"><img src="images/icon1-iphone.png" width="120" height="120" alt="icon" class="icon-big-bg"><h2>Customer Access</h2></p>
<p>Clients can place orders, review inventory and history online.</div>

		</div> 
	</div> 
		</div> 
	</div> 

	<div class="vc_span4 wpb_column column_container">
		<div class="wpb_wrapper">
			
	<div class="wpb_text_column wpb_content_element ">
		<div class="wpb_wrapper">
			<div class="feature-big-icon"><img src="images/icon1-leaf.png" width="120" height="120" alt="icon" class="icon-big-bg"><h2>Sales Team Management</h2></p>
<p>Instant access to account information, prices and inventory.</div>

		</div> 
	</div> 
		</div> 
	</div> 

	<div class="vc_span4 wpb_column column_container">
		<div class="wpb_wrapper">
			
	<div class="wpb_text_column wpb_content_element ">
		<div class="wpb_wrapper">
			<div class="feature-big-icon"><img src="images/icon1-create.png" width="120" height="120" alt="icon" class="icon-big-bg"><h2>Warehouse Integration</h2></p>
<p>Faster, accurate fulfilment with online data, orders and tracking.</div>

		</div> 
	</div> 
		</div> 
	</div> 
</div><div class="wpb_row vc_row-fluid">
	<div class="vc_span12 wpb_column column_container">
		<div class="wpb_wrapper">
			
	<div class="wpb_text_column wpb_content_element ">
		<div class="wpb_wrapper">
			<p><h4 class="heading-icon clearfix"><img src="images/heading-icon-preview.png" width="40" height="40" alt="icon" class="icon-bg">See<span class="color"> UberReps</span> esteemed solutions for manufacturers, <span class="color"> retailers and sales</span> reps in action.</h4><div class="es-carousel-wrapper">
    <div class="div2">
        <div id="mcts1">
            <img src="images/picture2.jpg" alt="sliderImage" />
            <img src="images/picture3.jpg" alt="sliderImage" />
            <img src="images/picture4.jpg" alt="sliderImage" />
            <img src="images/picture5.jpg" alt="sliderImage" />
            <img src="images/picture6.jpg" alt="sliderImage" />
            <img src="images/picture2.jpg" alt="sliderImage" />
            <img src="images/picture3.jpg" alt="sliderImage" />
            <img src="images/picture4.jpg" alt="sliderImage" />
        </div>
    </div>
			</div>

		</div> 
	</div> 
		</div> 
	</div> 
</div><div class="wpb_row vc_row-fluid">
	<div class="vc_span6 wpb_column column_container">
		<div class="wpb_wrapper">
			
	<div class="wpb_text_column wpb_content_element ">
		<div class="wpb_wrapper">
			<h4 class="heading-icon clearfix"><img src="images/heading-icon-listwithimg.png" width="40" height="40" alt="icon" class="icon-bg">Every account is backed by our powerful no contract guarantee.</h4>

<ul class="" style="margin-left:50px"><li>Simple installation</li><li>Time saving tools</li><li>More sales, less errors</li></ul>

		</div> 
	</div> 
		</div> 
	</div> 

	<div class="vc_span6 wpb_column column_container">
		<div class="wpb_wrapper">
			
	<div class="wpb_single_image wpb_content_element vc_align_center">
		<div class="wpb_wrapper">
			
			<img class="" src="images/img-window.png" width="460" height="280" alt="image" />
		</div> 
	</div> 
		</div> 
	</div> 
</div><div class="wpb_row vc_row-fluid">
	<div class="vc_span12 wpb_column column_container">
		<div class="wpb_wrapper">
			
	<div class="wpb_text_column wpb_content_element ">
		<div class="wpb_wrapper">
			<div class="box-dark"></p>
<h1 style="text-align: center;">Try UberReps FREE,<span class="color">Get started</span></h1>
<p><ul class="list-separator"><li>14 days free trial</li><li>No credit card required</li><li><span class="color">$10/mo</span> after end of trial period</li></ul><div class="spacer" style="height: 10px;"></div><a class="mybtn btn-orange" target="_blank" href="">Download</a></div>

		</div> 
	</div> 
		</div> 
	</div> 
</div>
                                            </div>
                </article>
                                            </div>
        </div>
    </div>
</section>
</asp:Content>
