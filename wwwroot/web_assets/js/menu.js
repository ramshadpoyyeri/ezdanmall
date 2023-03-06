jQuery(function () {
	
	/*============================================
   offcanvas menu
============================================ */
    jQuery('.menu-btn').click(function(e) {
        e.preventDefault();
        jQuery(this).toggleClass('active');
        jQuery('.header .menu').toggleClass('pushed');
    });
	
	jQuery('.advancedfilter').click(function(e) {
        e.preventDefault();
        jQuery(this).toggleClass('activ');
        jQuery('.category-filter').toggleClass('slid');
    });
	
	jQuery('.category-filter li').click(function(e) {
        e.preventDefault();
        jQuery('.advancedfilter').removeClass('activ');
        jQuery('.category-filter').removeClass('slid');
    });
	
});