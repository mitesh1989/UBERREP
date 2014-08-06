// JavaScript Document for Grepfrut
jQuery(document).ready(function($) {

// Fitvids
$(".video-wrapper").fitVids();

//Thumbnail hover effect for portfolio
$('.folio-thumb').hover(function () {
    $(this).find(".icon-zoom, .icon-link, .icon-zoom-only").fadeTo("fast", 1);
}, function () {
    $(this).find(".icon-zoom, .icon-link, .icon-zoom-only").fadeTo("fast", 0);
});
 
//Smooth menu for grepfrut
ddsmoothmenu.init({
    mainmenuid: "smoothmenu", //menu DIV id
    orientation: 'h', //Horizontal or vertical menu: Set to "h" or "v"
    classname: 'ddsmoothmenu', //class added to menu's outer DIV
    contentsource: "markup" //"markup" or ["container_id", "path_to_menu_file"]
})

// Dropdown nav for responsive
selectnav('nav', {
    label: '',
    nested: true,
    indent: '-'
});

// Isotope
var $container = $('.filter-content');
$container.imagesLoaded(function () {
    $container.isotope({
        itemSelector: '.item'
    });
});
$('.filter_nav li a').click(function () {
    $('.filter_nav li a').removeClass('active');
    $(this).addClass('active');
    var selector = $(this).attr('data-filter');
    $container.isotope({
        filter: selector
    });
    return false;
});
				
//prettyPhoto
$('a[data-rel]').each(function () {
    $(this).attr('rel', $(this).data('rel'));
});
$("a[rel^='prettyPhoto'],a[rel^='prettyPhoto[gallery]']").prettyPhoto({
    animation_speed: 'fast',
    slideshow: 5000,
    autoplay_slideshow: false,
    opacity: 0.80,
    show_title: false,
    theme: 'pp_default',
    /* light_rounded / dark_rounded / light_square / dark_square / facebook */
    overlay_gallery: false,
    social_tools: false,
    changepicturecallback: function () {
        var $pp = $('.pp_default');
        if (parseInt($pp.css('left')) < 0) {
            $pp.css('left', 0);
        }
    }
});

// Elastislide carousel
$('.es-carousel').each(function(){
	$(this).elastislide( {
		imageW: 300,
		margin: 20,
		border: 0,
		easing: ''
	});
})

//Tooltip
$('body').popover({
    selector: '[data-toggle="popover"]'
});
$('body').tooltip({
	selector: 'a[rel="tooltip"], [data-toggle="tooltip"]'
});

}); // Close document ready

/* IE Image Resizing - by Ethan Marcotte - http://unstoppablerobotninja.com/entry/fluid-images/ */
var imgSizer = {
	Config : {
		imgCache : []
		,spacer : "../images/spacer.gif"
	}

	,collate : function(aScope) {
		var isOldIE = (document.all && !window.opera && !window.XDomainRequest) ? 1 : 0;
		if (isOldIE && document.getElementsByTagName) {
			var c = imgSizer;
			var imgCache = c.Config.imgCache;

			var images = (aScope && aScope.length) ? aScope : document.getElementsByTagName("img");
			for (var i = 0; i < images.length; i++) {
				images[i].origWidth = images[i].offsetWidth;
				images[i].origHeight = images[i].offsetHeight;

				imgCache.push(images[i]);
				c.ieAlpha(images[i]);
				images[i].style.width = "100%";
			}

			if (imgCache.length) {
				c.resize(function() {
					for (var i = 0; i < imgCache.length; i++) {
						var ratio = (imgCache[i].offsetWidth / imgCache[i].origWidth);
						imgCache[i].style.height = (imgCache[i].origHeight * ratio) + "px";
					}
				});
			}
		}
	}

	,ieAlpha : function(img) {
		var c = imgSizer;
		if (img.oldSrc) {
			img.src = img.oldSrc;
		}
		var src = img.src;
		img.style.width = img.offsetWidth + "px";
		img.style.height = img.offsetHeight + "px";
		img.style.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(src='" + src + "', sizingMethod='scale')"
		img.oldSrc = src;
		img.src = c.Config.spacer;
	}

	// Ghettomodified version of Simon Willison's addLoadEvent() -- http://simonwillison.net/2004/May/26/addLoadEvent/
	,resize : function(func) {
		var oldonresize = window.onresize;
		if (typeof window.onresize != 'function') {
			window.onresize = func;
		} else {
			window.onresize = function() {
				if (oldonresize) {
					oldonresize();
				}
				func();
			}
		}
	}
}
