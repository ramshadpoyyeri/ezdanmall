


// PRELOADER 

//function preloader() {
//    var preloader = document.getElementById("preloader");
//    preloader.style.opacity = "0";
//    preloader.setAttribute("aria-busy", "false");
//    document.getElementById("page").style.opacity = "1";
//  }
//  window.onload = preloader;
  
// PRELOADER ENDS

//{
//
//
//
//    // 01. CURSOR EFFECT 
//
//
//    const lerp = (a, b, n) => (1 - n) * a + n * b;
//    const getMousePos = (e) => {
//        let posx = 0;
//        let posy = 0;
//        if (!e) e = window.event;
//        if (e.pageX || e.pageY) {
//            posx = e.pageX;
//            posy = e.pageY;
//        }
//        else if (e.clientX || e.clientY) 	{
//            posx = e.clientX + body.scrollLeft + document.documentElement.scrollLeft;
//            posy = e.clientY + body.scrollTop + document.documentElement.scrollTop;
//        }
//        return { x : posx, y : posy }
//    }
//
//    // Custom mouse cursor.
//    class CursorFx {
//        constructor(el) {
//            this.DOM = {el: el};
//            this.DOM.dot = this.DOM.el.querySelector('.cursor-inner--dot');
//            this.DOM.circle = this.DOM.el.querySelector('.cursor-inner--circle');
//            this.bounds = {dot: this.DOM.dot.getBoundingClientRect(), circle: this.DOM.circle.getBoundingClientRect()};
//            this.scale = 1;
//            this.opacity = 1;
//            this.mousePos = {x:0, y:0};
//            this.lastMousePos = {dot: {x:0, y:0}, circle: {x:0, y:0}};
//            this.lastScale = 1;
//            this.lastOpacity = 1;
//            
//            this.initEvents();
//            requestAnimationFrame(() => this.render());
//        }
//        initEvents() {
//            window.addEventListener('mousemove', ev => this.mousePos = getMousePos(ev));
//        }
//        render() {
//            this.lastMousePos.dot.x = lerp(this.lastMousePos.dot.x, this.mousePos.x - this.bounds.dot.width/2, 1);
//            this.lastMousePos.dot.y = lerp(this.lastMousePos.dot.y, this.mousePos.y - this.bounds.dot.height/2, 1);
//            this.lastMousePos.circle.x = lerp(this.lastMousePos.circle.x, this.mousePos.x - this.bounds.circle.width/2, 0.15);
//            this.lastMousePos.circle.y = lerp(this.lastMousePos.circle.y, this.mousePos.y - this.bounds.circle.height/2, 0.15);
//            this.lastScale = lerp(this.lastScale, this.scale, 0.15);
//            this.lastOpacity = lerp(this.lastOpacity, this.opacity, 0.1);
//            this.DOM.dot.style.transform = `translateX(${(this.lastMousePos.dot.x)}px) translateY(${this.lastMousePos.dot.y}px)`;
//            this.DOM.circle.style.transform = `translateX(${(this.lastMousePos.circle.x)}px) translateY(${this.lastMousePos.circle.y}px) scale(${this.lastScale})`;
//            this.DOM.circle.style.opacity = this.lastOpacity
//            requestAnimationFrame(() => this.render());
//        }
//        enter() {
//            cursor.scale = 2;
//        }
//        leave() {
//            cursor.scale = 1;
//        }
//        click() {
//            this.lastScale = 1;
//            this.lastOpacity = 0;
//        }
//    }
//    
//    const cursor = new CursorFx(document.querySelector('.cursor'));
//
//    // Custom cursor changes state when hovering on elements with 'data-hover'.
//    [...document.querySelectorAll('[data-hover]')].forEach((link) => {
//        link.addEventListener('mouseenter', () => cursor.enter() );
//        link.addEventListener('mouseleave', () => cursor.leave() );
//        link.addEventListener('click', () => cursor.click() );
//    });
//
//}


// 02. Accordion 

$(function() {
    var Accordion = function(el, multiple) {
            this.el = el || {};
            this.multiple = multiple || false;

            var links = this.el.find('.article-title');
            links.on('click', {
                    el: this.el,
                    multiple: this.multiple
            }, this.dropdown)
    }

    Accordion.prototype.dropdown = function(e) {
            var $el = e.data.el;
            $this = $(this),
                    $next = $this.next();

            $next.slideToggle();
            $this.parent().toggleClass('open');

            if (!e.data.multiple) {
                    $el.find('.accordion-content').not($next).slideUp().parent().removeClass('open');
            };
    }
    var accordion = new Accordion($('.accordion-container'), false);
});




// 03. NICE SCROLL 

$(".scroll").niceScroll({
                cursorcolor: "rgba(0,0,0,0.5)",
                cursoropacitymin: 0.3,
                background: "transparent",
                cursorborder: "0",
                autohidemode: false,
                cursorminheight: 30
    });
    
    //Activa el nicescroll cuando esta oculto
    
    $(".scroll").getNiceScroll().resize();
    $("html").mouseover(function() {
        $(".scroll").getNiceScroll().resize();
    });



    

        
        
        //4.  SMOOTH SCROOLTOP 


        
(function( $ ) {      "use strict";   
$(function() {
    $('a[href*="#"]:not([href="#"])').click(function() {
          if (location.pathname.replace(/^\//,'') == this.pathname.replace(/^\//,'') && location.hostname == this.hostname) {
            var target = $(this.hash);
            target = target.length ? target : $('[name=' + this.hash.slice(1) +']');
            if (target.length) {
              $('html, body').animate({
                scrollTop: target.offset().top
              }, 1000);
              return false;
            }
          }
      });
	
	jQuery(".footer-widget h4").on("click", function(){
		jQuery(this).next("ul,address").slideToggle();
		jQuery(this).toggleClass("acti");
	});
});   
}(jQuery));