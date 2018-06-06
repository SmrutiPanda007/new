/*!
 * Bootstrap v3.2.0 (http://getbootstrap.com)
 * Copyright 2011-2014 Twitter, Inc.
 * Licensed under MIT (https://github.com/twbs/bootstrap/blob/master/LICENSE)
 */
if(typeof jQuery=="undefined")throw new Error("Bootstrap's JavaScript requires jQuery");+function(n){"use strict";function t(){var i=document.createElement("bootstrap"),n={WebkitTransition:"webkitTransitionEnd",MozTransition:"transitionend",OTransition:"oTransitionEnd otransitionend",transition:"transitionend"};for(var t in n)if(i.style[t]!==undefined)return{end:n[t]};return!1}n.fn.emulateTransitionEnd=function(t){var i=!1,u=this,r;n(this).one("bsTransitionEnd",function(){i=!0});return r=function(){i||n(u).trigger(n.support.transition.end)},setTimeout(r,t),this};n(function(){(n.support.transition=t(),n.support.transition)&&(n.event.special.bsTransitionEnd={bindType:n.support.transition.end,delegateType:n.support.transition.end,handle:function(t){if(n(t.target).is(this))return t.handleObj.handler.apply(this,arguments)}})})}(jQuery);+function(n){"use strict";function u(i){return this.each(function(){var r=n(this),u=r.data("bs.alert");u||r.data("bs.alert",u=new t(this));typeof i=="string"&&u[i].call(r)})}var i='[data-dismiss="alert"]',t=function(t){n(t).on("click",i,this.close)},r;t.VERSION="3.2.0";t.prototype.close=function(t){function f(){i.detach().trigger("closed.bs.alert").remove()}var u=n(this),r=u.attr("data-target"),i;(r||(r=u.attr("href"),r=r&&r.replace(/.*(?=#[^\s]*$)/,"")),i=n(r),t&&t.preventDefault(),i.length||(i=u.hasClass("alert")?u:u.parent()),i.trigger(t=n.Event("close.bs.alert")),t.isDefaultPrevented())||(i.removeClass("in"),n.support.transition&&i.hasClass("fade")?i.one("bsTransitionEnd",f).emulateTransitionEnd(150):f())};r=n.fn.alert;n.fn.alert=u;n.fn.alert.Constructor=t;n.fn.alert.noConflict=function(){return n.fn.alert=r,this};n(document).on("click.bs.alert.data-api",i,t.prototype.close)}(jQuery);+function(n){"use strict";function i(i){return this.each(function(){var u=n(this),r=u.data("bs.button"),f=typeof i=="object"&&i;r||u.data("bs.button",r=new t(this,f));i=="toggle"?r.toggle():i&&r.setState(i)})}var t=function(i,r){this.$element=n(i);this.options=n.extend({},t.DEFAULTS,r);this.isLoading=!1},r;t.VERSION="3.2.0";t.DEFAULTS={loadingText:"loading..."};t.prototype.setState=function(t){var r="disabled",i=this.$element,f=i.is("input")?"val":"html",u=i.data();t=t+"Text";u.resetText==null&&i.data("resetText",i[f]());i[f](u[t]==null?this.options[t]:u[t]);setTimeout(n.proxy(function(){t=="loadingText"?(this.isLoading=!0,i.addClass(r).attr(r,r)):this.isLoading&&(this.isLoading=!1,i.removeClass(r).removeAttr(r))},this),0)};t.prototype.toggle=function(){var t=!0,i=this.$element.closest('[data-toggle="buttons"]'),n;i.length&&(n=this.$element.find("input"),n.prop("type")=="radio"&&(n.prop("checked")&&this.$element.hasClass("active")?t=!1:i.find(".active").removeClass("active")),t&&n.prop("checked",!this.$element.hasClass("active")).trigger("change"));t&&this.$element.toggleClass("active")};r=n.fn.button;n.fn.button=i;n.fn.button.Constructor=t;n.fn.button.noConflict=function(){return n.fn.button=r,this};n(document).on("click.bs.button.data-api",'[data-toggle^="button"]',function(t){var r=n(t.target);r.hasClass("btn")||(r=r.closest(".btn"));i.call(r,"toggle");t.preventDefault()})}(jQuery);+function(n){"use strict";function i(i){return this.each(function(){var u=n(this),r=u.data("bs.carousel"),f=n.extend({},t.DEFAULTS,u.data(),typeof i=="object"&&i),e=typeof i=="string"?i:f.slide;r||u.data("bs.carousel",r=new t(this,f));typeof i=="number"?r.to(i):e?r[e]():f.interval&&r.pause().cycle()})}var t=function(t,i){this.$element=n(t).on("keydown.bs.carousel",n.proxy(this.keydown,this));this.$indicators=this.$element.find(".carousel-indicators");this.options=i;this.paused=this.sliding=this.interval=this.$active=this.$items=null;this.options.pause=="hover"&&this.$element.on("mouseenter.bs.carousel",n.proxy(this.pause,this)).on("mouseleave.bs.carousel",n.proxy(this.cycle,this))},r;t.VERSION="3.2.0";t.DEFAULTS={interval:5e3,pause:"hover",wrap:!0};t.prototype.keydown=function(n){switch(n.which){case 37:this.prev();break;case 39:this.next();break;default:return}n.preventDefault()};t.prototype.cycle=function(t){return t||(this.paused=!1),this.interval&&clearInterval(this.interval),this.options.interval&&!this.paused&&(this.interval=setInterval(n.proxy(this.next,this),this.options.interval)),this};t.prototype.getItemIndex=function(n){return this.$items=n.parent().children(".item"),this.$items.index(n||this.$active)};t.prototype.to=function(t){var r=this,i=this.getItemIndex(this.$active=this.$element.find(".item.active"));if(!(t>this.$items.length-1)&&!(t<0))return this.sliding?this.$element.one("slid.bs.carousel",function(){r.to(t)}):i==t?this.pause().cycle():this.slide(t>i?"next":"prev",n(this.$items[t]))};t.prototype.pause=function(t){return t||(this.paused=!0),this.$element.find(".next, .prev").length&&n.support.transition&&(this.$element.trigger(n.support.transition.end),this.cycle(!0)),this.interval=clearInterval(this.interval),this};t.prototype.next=function(){if(!this.sliding)return this.slide("next")};t.prototype.prev=function(){if(!this.sliding)return this.slide("prev")};t.prototype.slide=function(t,i){var u=this.$element.find(".item.active"),r=i||u[t](),c=this.interval,f=t=="next"?"left":"right",a=t=="next"?"first":"last",l=this,e,o,s,h;if(!r.length){if(!this.options.wrap)return;r=this.$element.find(".item")[a]()}return r.hasClass("active")?this.sliding=!1:(e=r[0],o=n.Event("slide.bs.carousel",{relatedTarget:e,direction:f}),this.$element.trigger(o),o.isDefaultPrevented())?void 0:(this.sliding=!0,c&&this.pause(),this.$indicators.length&&(this.$indicators.find(".active").removeClass("active"),s=n(this.$indicators.children()[this.getItemIndex(r)]),s&&s.addClass("active")),h=n.Event("slid.bs.carousel",{relatedTarget:e,direction:f}),n.support.transition&&this.$element.hasClass("slide")?(r.addClass(t),r[0].offsetWidth,u.addClass(f),r.addClass(f),u.one("bsTransitionEnd",function(){r.removeClass([t,f].join(" ")).addClass("active");u.removeClass(["active",f].join(" "));l.sliding=!1;setTimeout(function(){l.$element.trigger(h)},0)}).emulateTransitionEnd(u.css("transition-duration").slice(0,-1)*1e3)):(u.removeClass("active"),r.addClass("active"),this.sliding=!1,this.$element.trigger(h)),c&&this.cycle(),this)};r=n.fn.carousel;n.fn.carousel=i;n.fn.carousel.Constructor=t;n.fn.carousel.noConflict=function(){return n.fn.carousel=r,this};n(document).on("click.bs.carousel.data-api","[data-slide], [data-slide-to]",function(t){var o,r=n(this),u=n(r.attr("data-target")||(o=r.attr("href"))&&o.replace(/.*(?=#[^\s]+$)/,"")),e,f;u.hasClass("carousel")&&(e=n.extend({},u.data(),r.data()),f=r.attr("data-slide-to"),f&&(e.interval=!1),i.call(u,e),f&&u.data("bs.carousel").to(f),t.preventDefault())});n(window).on("load",function(){n('[data-ride="carousel"]').each(function(){var t=n(this);i.call(t,t.data())})})}(jQuery);+function(n){"use strict";function i(i){return this.each(function(){var u=n(this),r=u.data("bs.collapse"),f=n.extend({},t.DEFAULTS,u.data(),typeof i=="object"&&i);!r&&f.toggle&&i=="show"&&(i=!i);r||u.data("bs.collapse",r=new t(this,f));typeof i=="string"&&r[i]()})}var t=function(i,r){this.$element=n(i);this.options=n.extend({},t.DEFAULTS,r);this.transitioning=null;this.options.parent&&(this.$parent=n(this.options.parent));this.options.toggle&&this.toggle()},r;t.VERSION="3.2.0";t.DEFAULTS={toggle:!0};t.prototype.dimension=function(){var n=this.$element.hasClass("width");return n?"width":"height"};t.prototype.show=function(){var f,t,u,r,e,o;if(!this.transitioning&&!this.$element.hasClass("in")&&(f=n.Event("show.bs.collapse"),this.$element.trigger(f),!f.isDefaultPrevented())){if(t=this.$parent&&this.$parent.find("> .panel > .in"),t&&t.length){if(u=t.data("bs.collapse"),u&&u.transitioning)return;i.call(t,"hide");u||t.data("bs.collapse",null)}if(r=this.dimension(),this.$element.removeClass("collapse").addClass("collapsing")[r](0),this.transitioning=1,e=function(){this.$element.removeClass("collapsing").addClass("collapse in")[r]("");this.transitioning=0;this.$element.trigger("shown.bs.collapse")},!n.support.transition)return e.call(this);o=n.camelCase(["scroll",r].join("-"));this.$element.one("bsTransitionEnd",n.proxy(e,this)).emulateTransitionEnd(350)[r](this.$element[0][o])}};t.prototype.hide=function(){var i,t,r;if(!this.transitioning&&this.$element.hasClass("in")&&(i=n.Event("hide.bs.collapse"),this.$element.trigger(i),!i.isDefaultPrevented())){if(t=this.dimension(),this.$element[t](this.$element[t]())[0].offsetHeight,this.$element.addClass("collapsing").removeClass("collapse").removeClass("in"),this.transitioning=1,r=function(){this.transitioning=0;this.$element.trigger("hidden.bs.collapse").removeClass("collapsing").addClass("collapse")},!n.support.transition)return r.call(this);this.$element[t](0).one("bsTransitionEnd",n.proxy(r,this)).emulateTransitionEnd(350)}};t.prototype.toggle=function(){this[this.$element.hasClass("in")?"hide":"show"]()};r=n.fn.collapse;n.fn.collapse=i;n.fn.collapse.Constructor=t;n.fn.collapse.noConflict=function(){return n.fn.collapse=r,this};n(document).on("click.bs.collapse.data-api",'[data-toggle="collapse"]',function(t){var o,r=n(this),h=r.attr("data-target")||t.preventDefault()||(o=r.attr("href"))&&o.replace(/.*(?=#[^\s]+$)/,""),u=n(h),f=u.data("bs.collapse"),c=f?"toggle":r.data(),e=r.attr("data-parent"),s=e&&n(e);f&&f.transitioning||(s&&s.find('[data-toggle="collapse"][data-parent="'+e+'"]').not(r).addClass("collapsed"),r[u.hasClass("in")?"addClass":"removeClass"]("collapsed"));i.call(u,c)})}(jQuery);+function(n){"use strict";function r(t){t&&t.which===3||(n(e).remove(),n(i).each(function(){var i=u(n(this)),r={relatedTarget:this};i.hasClass("open")&&((i.trigger(t=n.Event("hide.bs.dropdown",r)),t.isDefaultPrevented())||i.removeClass("open").trigger("hidden.bs.dropdown",r))}))}function u(t){var i=t.attr("data-target"),r;return i||(i=t.attr("href"),i=i&&/#[A-Za-z]/.test(i)&&i.replace(/.*(?=#[^\s]*$)/,"")),r=i&&n(i),r&&r.length?r:t.parent()}function o(i){return this.each(function(){var r=n(this),u=r.data("bs.dropdown");u||r.data("bs.dropdown",u=new t(this));typeof i=="string"&&u[i].call(r)})}var e=".dropdown-backdrop",i='[data-toggle="dropdown"]',t=function(t){n(t).on("click.bs.dropdown",this.toggle)},f;t.VERSION="3.2.0";t.prototype.toggle=function(t){var f=n(this),i,o,e;if(!f.is(".disabled, :disabled")){if(i=u(f),o=i.hasClass("open"),r(),!o){if("ontouchstart"in document.documentElement&&!i.closest(".navbar-nav").length)n('<div class="dropdown-backdrop"/>').insertAfter(n(this)).on("click",r);if(e={relatedTarget:this},i.trigger(t=n.Event("show.bs.dropdown",e)),t.isDefaultPrevented())return;f.trigger("focus");i.toggleClass("open").trigger("shown.bs.dropdown",e)}return!1}};t.prototype.keydown=function(t){var e,o,s,h,f,r;if(/(38|40|27)/.test(t.keyCode)&&(e=n(this),t.preventDefault(),t.stopPropagation(),!e.is(".disabled, :disabled"))){if(o=u(e),s=o.hasClass("open"),!s||s&&t.keyCode==27)return t.which==27&&o.find(i).trigger("focus"),e.trigger("click");(h=" li:not(.divider):visible a",f=o.find('[role="menu"]'+h+', [role="listbox"]'+h),f.length)&&(r=f.index(f.filter(":focus")),t.keyCode==38&&r>0&&r--,t.keyCode==40&&r<f.length-1&&r++,~r||(r=0),f.eq(r).trigger("focus"))}};f=n.fn.dropdown;n.fn.dropdown=o;n.fn.dropdown.Constructor=t;n.fn.dropdown.noConflict=function(){return n.fn.dropdown=f,this};n(document).on("click.bs.dropdown.data-api",r).on("click.bs.dropdown.data-api",".dropdown form",function(n){n.stopPropagation()}).on("click.bs.dropdown.data-api",i,t.prototype.toggle).on("keydown.bs.dropdown.data-api",i+', [role="menu"], [role="listbox"]',t.prototype.keydown)}(jQuery);+function(n){"use strict";function i(i,r){return this.each(function(){var f=n(this),u=f.data("bs.modal"),e=n.extend({},t.DEFAULTS,f.data(),typeof i=="object"&&i);u||f.data("bs.modal",u=new t(this,e));typeof i=="string"?u[i](r):e.show&&u.show(r)})}var t=function(t,i){this.options=i;this.$body=n(document.body);this.$element=n(t);this.$backdrop=this.isShown=null;this.scrollbarWidth=0;this.options.remote&&this.$element.find(".modal-content").load(this.options.remote,n.proxy(function(){this.$element.trigger("loaded.bs.modal")},this))},r;t.VERSION="3.2.0";t.DEFAULTS={backdrop:!0,keyboard:!0,show:!0};t.prototype.toggle=function(n){return this.isShown?this.hide():this.show(n)};t.prototype.show=function(t){var i=this,r=n.Event("show.bs.modal",{relatedTarget:t});if(this.$element.trigger(r),!this.isShown&&!r.isDefaultPrevented()){this.isShown=!0;this.checkScrollbar();this.$body.addClass("modal-open");this.setScrollbar();this.escape();this.$element.on("click.dismiss.bs.modal",'[data-dismiss="modal"]',n.proxy(this.hide,this));this.backdrop(function(){var u=n.support.transition&&i.$element.hasClass("fade"),r;i.$element.parent().length||i.$element.appendTo(i.$body);i.$element.show().scrollTop(0);u&&i.$element[0].offsetWidth;i.$element.addClass("in").attr("aria-hidden",!1);i.enforceFocus();r=n.Event("shown.bs.modal",{relatedTarget:t});u?i.$element.find(".modal-dialog").one("bsTransitionEnd",function(){i.$element.trigger("focus").trigger(r)}).emulateTransitionEnd(300):i.$element.trigger("focus").trigger(r)})}};t.prototype.hide=function(t){(t&&t.preventDefault(),t=n.Event("hide.bs.modal"),this.$element.trigger(t),this.isShown&&!t.isDefaultPrevented())&&(this.isShown=!1,this.$body.removeClass("modal-open"),this.resetScrollbar(),this.escape(),n(document).off("focusin.bs.modal"),this.$element.removeClass("in").attr("aria-hidden",!0).off("click.dismiss.bs.modal"),n.support.transition&&this.$element.hasClass("fade")?this.$element.one("bsTransitionEnd",n.proxy(this.hideModal,this)).emulateTransitionEnd(300):this.hideModal())};t.prototype.enforceFocus=function(){n(document).off("focusin.bs.modal").on("focusin.bs.modal",n.proxy(function(n){this.$element[0]===n.target||this.$element.has(n.target).length||this.$element.trigger("focus")},this))};t.prototype.escape=function(){if(this.isShown&&this.options.keyboard)this.$element.on("keyup.dismiss.bs.modal",n.proxy(function(n){n.which==27&&this.hide()},this));else this.isShown||this.$element.off("keyup.dismiss.bs.modal")};t.prototype.hideModal=function(){var n=this;this.$element.hide();this.backdrop(function(){n.$element.trigger("hidden.bs.modal")})};t.prototype.removeBackdrop=function(){this.$backdrop&&this.$backdrop.remove();this.$backdrop=null};t.prototype.backdrop=function(t){var f=this,u=this.$element.hasClass("fade")?"fade":"",i,r;if(this.isShown&&this.options.backdrop){i=n.support.transition&&u;this.$backdrop=n('<div class="modal-backdrop '+u+'" />').appendTo(this.$body);this.$element.on("click.dismiss.bs.modal",n.proxy(function(n){n.target===n.currentTarget&&(this.options.backdrop=="static"?this.$element[0].focus.call(this.$element[0]):this.hide.call(this))},this));if(i&&this.$backdrop[0].offsetWidth,this.$backdrop.addClass("in"),!t)return;i?this.$backdrop.one("bsTransitionEnd",t).emulateTransitionEnd(150):t()}else!this.isShown&&this.$backdrop?(this.$backdrop.removeClass("in"),r=function(){f.removeBackdrop();t&&t()},n.support.transition&&this.$element.hasClass("fade")?this.$backdrop.one("bsTransitionEnd",r).emulateTransitionEnd(150):r()):t&&t()};t.prototype.checkScrollbar=function(){document.body.clientWidth>=window.innerWidth||(this.scrollbarWidth=this.scrollbarWidth||this.measureScrollbar())};t.prototype.setScrollbar=function(){var n=parseInt(this.$body.css("padding-right")||0,10);this.scrollbarWidth&&this.$body.css("padding-right",n+this.scrollbarWidth)};t.prototype.resetScrollbar=function(){this.$body.css("padding-right","")};t.prototype.measureScrollbar=function(){var n=document.createElement("div"),t;return n.className="modal-scrollbar-measure",this.$body.append(n),t=n.offsetWidth-n.clientWidth,this.$body[0].removeChild(n),t};r=n.fn.modal;n.fn.modal=i;n.fn.modal.Constructor=t;n.fn.modal.noConflict=function(){return n.fn.modal=r,this};n(document).on("click.bs.modal.data-api",'[data-toggle="modal"]',function(t){var r=n(this),f=r.attr("href"),u=n(r.attr("data-target")||f&&f.replace(/.*(?=#[^\s]+$)/,"")),e=u.data("bs.modal")?"toggle":n.extend({remote:!/#/.test(f)&&f},u.data(),r.data());r.is("a")&&t.preventDefault();u.one("show.bs.modal",function(n){if(!n.isDefaultPrevented())u.one("hidden.bs.modal",function(){r.is(":visible")&&r.trigger("focus")})});i.call(u,e,this)})}(jQuery);+function(n){"use strict";function r(i){return this.each(function(){var u=n(this),r=u.data("bs.tooltip"),f=typeof i=="object"&&i;(r||i!="destroy")&&(r||u.data("bs.tooltip",r=new t(this,f)),typeof i=="string"&&r[i]())})}var t=function(n,t){this.type=this.options=this.enabled=this.timeout=this.hoverState=this.$element=null;this.init("tooltip",n,t)},i;t.VERSION="3.2.0";t.DEFAULTS={animation:!0,placement:"top",selector:!1,template:'<div class="tooltip" role="tooltip"><div class="tooltip-arrow"><\/div><div class="tooltip-inner"><\/div><\/div>',trigger:"hover focus",title:"",delay:0,html:!1,container:!1,viewport:{selector:"body",padding:0}};t.prototype.init=function(t,i,r){var f,e,u,o,s;for(this.enabled=!0,this.type=t,this.$element=n(i),this.options=this.getOptions(r),this.$viewport=this.options.viewport&&n(this.options.viewport.selector||this.options.viewport),f=this.options.trigger.split(" "),e=f.length;e--;)if(u=f[e],u=="click")this.$element.on("click."+this.type,this.options.selector,n.proxy(this.toggle,this));else if(u!="manual"){o=u=="hover"?"mouseenter":"focusin";s=u=="hover"?"mouseleave":"focusout";this.$element.on(o+"."+this.type,this.options.selector,n.proxy(this.enter,this));this.$element.on(s+"."+this.type,this.options.selector,n.proxy(this.leave,this))}this.options.selector?this._options=n.extend({},this.options,{trigger:"manual",selector:""}):this.fixTitle()};t.prototype.getDefaults=function(){return t.DEFAULTS};t.prototype.getOptions=function(t){return t=n.extend({},this.getDefaults(),this.$element.data(),t),t.delay&&typeof t.delay=="number"&&(t.delay={show:t.delay,hide:t.delay}),t};t.prototype.getDelegateOptions=function(){var t={},i=this.getDefaults();return this._options&&n.each(this._options,function(n,r){i[n]!=r&&(t[n]=r)}),t};t.prototype.enter=function(t){var i=t instanceof this.constructor?t:n(t.currentTarget).data("bs."+this.type);if(i||(i=new this.constructor(t.currentTarget,this.getDelegateOptions()),n(t.currentTarget).data("bs."+this.type,i)),clearTimeout(i.timeout),i.hoverState="in",!i.options.delay||!i.options.delay.show)return i.show();i.timeout=setTimeout(function(){i.hoverState=="in"&&i.show()},i.options.delay.show)};t.prototype.leave=function(t){var i=t instanceof this.constructor?t:n(t.currentTarget).data("bs."+this.type);if(i||(i=new this.constructor(t.currentTarget,this.getDelegateOptions()),n(t.currentTarget).data("bs."+this.type,i)),clearTimeout(i.timeout),i.hoverState="out",!i.options.delay||!i.options.delay.hide)return i.hide();i.timeout=setTimeout(function(){i.hoverState=="out"&&i.hide()},i.options.delay.hide)};t.prototype.show=function(){var h=n.Event("show.bs."+this.type),c,y,s;if(this.hasContent()&&this.enabled){if(this.$element.trigger(h),c=n.contains(document.documentElement,this.$element[0]),h.isDefaultPrevented()||!c)return;var f=this,i=this.tip(),l=this.getUID(this.type);this.setContent();i.attr("id",l);this.$element.attr("aria-describedby",l);this.options.animation&&i.addClass("fade");var t=typeof this.options.placement=="function"?this.options.placement.call(this,i[0],this.$element[0]):this.options.placement,a=/\s?auto?\s?/i,v=a.test(t);v&&(t=t.replace(a,"")||"top");i.detach().css({top:0,left:0,display:"block"}).addClass(t).data("bs."+this.type,this);this.options.container?i.appendTo(this.options.container):i.insertAfter(this.$element);var r=this.getPosition(),e=i[0].offsetWidth,o=i[0].offsetHeight;if(v){var p=t,w=this.$element.parent(),u=this.getPosition(w);t=t=="bottom"&&r.top+r.height+o-u.scroll>u.height?"top":t=="top"&&r.top-u.scroll-o<0?"bottom":t=="right"&&r.right+e>u.width?"left":t=="left"&&r.left-e<u.left?"right":t;i.removeClass(p).addClass(t)}y=this.getCalculatedOffset(t,r,e,o);this.applyPlacement(y,t);s=function(){f.$element.trigger("shown.bs."+f.type);f.hoverState=null};n.support.transition&&this.$tip.hasClass("fade")?i.one("bsTransitionEnd",s).emulateTransitionEnd(150):s()}};t.prototype.applyPlacement=function(t,i){var r=this.tip(),c=r[0].offsetWidth,e=r[0].offsetHeight,o=parseInt(r.css("margin-top"),10),s=parseInt(r.css("margin-left"),10),h,f,u;isNaN(o)&&(o=0);isNaN(s)&&(s=0);t.top=t.top+o;t.left=t.left+s;n.offset.setOffset(r[0],n.extend({using:function(n){r.css({top:Math.round(n.top),left:Math.round(n.left)})}},t),0);r.addClass("in");h=r[0].offsetWidth;f=r[0].offsetHeight;i=="top"&&f!=e&&(t.top=t.top+e-f);u=this.getViewportAdjustedDelta(i,t,h,f);u.left?t.left+=u.left:t.top+=u.top;var l=u.left?u.left*2-c+h:u.top*2-e+f,a=u.left?"left":"top",v=u.left?"offsetWidth":"offsetHeight";r.offset(t);this.replaceArrow(l,r[0][v],a)};t.prototype.replaceArrow=function(n,t,i){this.arrow().css(i,n?50*(1-n/t)+"%":"")};t.prototype.setContent=function(){var n=this.tip(),t=this.getTitle();n.find(".tooltip-inner")[this.options.html?"html":"text"](t);n.removeClass("fade in top bottom left right")};t.prototype.hide=function(){function u(){t.hoverState!="in"&&i.detach();t.$element.trigger("hidden.bs."+t.type)}var t=this,i=this.tip(),r=n.Event("hide.bs."+this.type);if(this.$element.removeAttr("aria-describedby"),this.$element.trigger(r),!r.isDefaultPrevented())return i.removeClass("in"),n.support.transition&&this.$tip.hasClass("fade")?i.one("bsTransitionEnd",u).emulateTransitionEnd(150):u(),this.hoverState=null,this};t.prototype.fixTitle=function(){var n=this.$element;(n.attr("title")||typeof n.attr("data-original-title")!="string")&&n.attr("data-original-title",n.attr("title")||"").attr("title","")};t.prototype.hasContent=function(){return this.getTitle()};t.prototype.getPosition=function(t){t=t||this.$element;var r=t[0],i=r.tagName=="BODY";return n.extend({},typeof r.getBoundingClientRect=="function"?r.getBoundingClientRect():null,{scroll:i?document.documentElement.scrollTop||document.body.scrollTop:t.scrollTop(),width:i?n(window).width():t.outerWidth(),height:i?n(window).height():t.outerHeight()},i?{top:0,left:0}:t.offset())};t.prototype.getCalculatedOffset=function(n,t,i,r){return n=="bottom"?{top:t.top+t.height,left:t.left+t.width/2-i/2}:n=="top"?{top:t.top-r,left:t.left+t.width/2-i/2}:n=="left"?{top:t.top+t.height/2-r/2,left:t.left-i}:{top:t.top+t.height/2-r/2,left:t.left+t.width}};t.prototype.getViewportAdjustedDelta=function(n,t,i,r){var f={top:0,left:0},e,u,o,s,h,c;return this.$viewport?(e=this.options.viewport&&this.options.viewport.padding||0,u=this.getPosition(this.$viewport),/right|left/.test(n)?(o=t.top-e-u.scroll,s=t.top+e-u.scroll+r,o<u.top?f.top=u.top-o:s>u.top+u.height&&(f.top=u.top+u.height-s)):(h=t.left-e,c=t.left+e+i,h<u.left?f.left=u.left-h:c>u.width&&(f.left=u.left+u.width-c)),f):f};t.prototype.getTitle=function(){var t=this.$element,n=this.options;return t.attr("data-original-title")||(typeof n.title=="function"?n.title.call(t[0]):n.title)};t.prototype.getUID=function(n){do n+=~~(Math.random()*1e6);while(document.getElementById(n));return n};t.prototype.tip=function(){return this.$tip=this.$tip||n(this.options.template)};t.prototype.arrow=function(){return this.$arrow=this.$arrow||this.tip().find(".tooltip-arrow")};t.prototype.validate=function(){this.$element[0].parentNode||(this.hide(),this.$element=null,this.options=null)};t.prototype.enable=function(){this.enabled=!0};t.prototype.disable=function(){this.enabled=!1};t.prototype.toggleEnabled=function(){this.enabled=!this.enabled};t.prototype.toggle=function(t){var i=this;t&&(i=n(t.currentTarget).data("bs."+this.type),i||(i=new this.constructor(t.currentTarget,this.getDelegateOptions()),n(t.currentTarget).data("bs."+this.type,i)));i.tip().hasClass("in")?i.leave(i):i.enter(i)};t.prototype.destroy=function(){clearTimeout(this.timeout);this.hide().$element.off("."+this.type).removeData("bs."+this.type)};i=n.fn.tooltip;n.fn.tooltip=r;n.fn.tooltip.Constructor=t;n.fn.tooltip.noConflict=function(){return n.fn.tooltip=i,this}}(jQuery);+function(n){"use strict";function r(i){return this.each(function(){var u=n(this),r=u.data("bs.popover"),f=typeof i=="object"&&i;(r||i!="destroy")&&(r||u.data("bs.popover",r=new t(this,f)),typeof i=="string"&&r[i]())})}var t=function(n,t){this.init("popover",n,t)},i;if(!n.fn.tooltip)throw new Error("Popover requires tooltip.js");t.VERSION="3.2.0";t.DEFAULTS=n.extend({},n.fn.tooltip.Constructor.DEFAULTS,{placement:"right",trigger:"click",content:"",template:'<div class="popover" role="tooltip"><div class="arrow"><\/div><h3 class="popover-title"><\/h3><div class="popover-content"><\/div><\/div>'});t.prototype=n.extend({},n.fn.tooltip.Constructor.prototype);t.prototype.constructor=t;t.prototype.getDefaults=function(){return t.DEFAULTS};t.prototype.setContent=function(){var n=this.tip(),i=this.getTitle(),t=this.getContent();n.find(".popover-title")[this.options.html?"html":"text"](i);n.find(".popover-content").empty()[this.options.html?typeof t=="string"?"html":"append":"text"](t);n.removeClass("fade top bottom left right in");n.find(".popover-title").html()||n.find(".popover-title").hide()};t.prototype.hasContent=function(){return this.getTitle()||this.getContent()};t.prototype.getContent=function(){var t=this.$element,n=this.options;return t.attr("data-content")||(typeof n.content=="function"?n.content.call(t[0]):n.content)};t.prototype.arrow=function(){return this.$arrow=this.$arrow||this.tip().find(".arrow")};t.prototype.tip=function(){return this.$tip||(this.$tip=n(this.options.template)),this.$tip};i=n.fn.popover;n.fn.popover=r;n.fn.popover.Constructor=t;n.fn.popover.noConflict=function(){return n.fn.popover=i,this}}(jQuery);+function(n){"use strict";function t(i,r){var u=n.proxy(this.process,this);this.$body=n("body");this.$scrollElement=n(i).is("body")?n(window):n(i);this.options=n.extend({},t.DEFAULTS,r);this.selector=(this.options.target||"")+" .nav li > a";this.offsets=[];this.targets=[];this.activeTarget=null;this.scrollHeight=0;this.$scrollElement.on("scroll.bs.scrollspy",u);this.refresh();this.process()}function i(i){return this.each(function(){var u=n(this),r=u.data("bs.scrollspy"),f=typeof i=="object"&&i;r||u.data("bs.scrollspy",r=new t(this,f));typeof i=="string"&&r[i]()})}t.VERSION="3.2.0";t.DEFAULTS={offset:10};t.prototype.getScrollHeight=function(){return this.$scrollElement[0].scrollHeight||Math.max(this.$body[0].scrollHeight,document.documentElement.scrollHeight)};t.prototype.refresh=function(){var i="offset",r=0,t;n.isWindow(this.$scrollElement[0])||(i="position",r=this.$scrollElement.scrollTop());this.offsets=[];this.targets=[];this.scrollHeight=this.getScrollHeight();t=this;this.$body.find(this.selector).map(function(){var f=n(this),u=f.data("target")||f.attr("href"),t=/^#./.test(u)&&n(u);return t&&t.length&&t.is(":visible")&&[[t[i]().top+r,u]]||null}).sort(function(n,t){return n[0]-t[0]}).each(function(){t.offsets.push(this[0]);t.targets.push(this[1])})};t.prototype.process=function(){var r=this.$scrollElement.scrollTop()+this.options.offset,f=this.getScrollHeight(),e=this.options.offset+f-this.$scrollElement.height(),t=this.offsets,i=this.targets,u=this.activeTarget,n;if(this.scrollHeight!=f&&this.refresh(),r>=e)return u!=(n=i[i.length-1])&&this.activate(n);if(u&&r<=t[0])return u!=(n=i[0])&&this.activate(n);for(n=t.length;n--;)u!=i[n]&&r>=t[n]&&(!t[n+1]||r<=t[n+1])&&this.activate(i[n])};t.prototype.activate=function(t){this.activeTarget=t;n(this.selector).parentsUntil(this.options.target,".active").removeClass("active");var r=this.selector+'[data-target="'+t+'"],'+this.selector+'[href="'+t+'"]',i=n(r).parents("li").addClass("active");i.parent(".dropdown-menu").length&&(i=i.closest("li.dropdown").addClass("active"));i.trigger("activate.bs.scrollspy")};var r=n.fn.scrollspy;n.fn.scrollspy=i;n.fn.scrollspy.Constructor=t;n.fn.scrollspy.noConflict=function(){return n.fn.scrollspy=r,this};n(window).on("load.bs.scrollspy.data-api",function(){n('[data-spy="scroll"]').each(function(){var t=n(this);i.call(t,t.data())})})}(jQuery);+function(n){"use strict";function i(i){return this.each(function(){var u=n(this),r=u.data("bs.tab");r||u.data("bs.tab",r=new t(this));typeof i=="string"&&r[i]()})}var t=function(t){this.element=n(t)},r;t.VERSION="3.2.0";t.prototype.show=function(){var t=this.element,e=t.closest("ul:not(.dropdown-menu)"),i=t.data("target"),r,u,f;(i||(i=t.attr("href"),i=i&&i.replace(/.*(?=#[^\s]*$)/,"")),t.parent("li").hasClass("active"))||(r=e.find(".active:last a")[0],u=n.Event("show.bs.tab",{relatedTarget:r}),t.trigger(u),u.isDefaultPrevented())||(f=n(i),this.activate(t.closest("li"),e),this.activate(f,f.parent(),function(){t.trigger({type:"shown.bs.tab",relatedTarget:r})}))};t.prototype.activate=function(t,i,r){function e(){u.removeClass("active").find("> .dropdown-menu > .active").removeClass("active");t.addClass("active");f?(t[0].offsetWidth,t.addClass("in")):t.removeClass("fade");t.parent(".dropdown-menu")&&t.closest("li.dropdown").addClass("active");r&&r()}var u=i.find("> .active"),f=r&&n.support.transition&&u.hasClass("fade");f?u.one("bsTransitionEnd",e).emulateTransitionEnd(150):e();u.removeClass("in")};r=n.fn.tab;n.fn.tab=i;n.fn.tab.Constructor=t;n.fn.tab.noConflict=function(){return n.fn.tab=r,this};n(document).on("click.bs.tab.data-api",'[data-toggle="tab"], [data-toggle="pill"]',function(t){t.preventDefault();i.call(n(this),"show")})}(jQuery);+function(n){"use strict";function i(i){return this.each(function(){var u=n(this),r=u.data("bs.affix"),f=typeof i=="object"&&i;r||u.data("bs.affix",r=new t(this,f));typeof i=="string"&&r[i]()})}var t=function(i,r){this.options=n.extend({},t.DEFAULTS,r);this.$target=n(this.options.target).on("scroll.bs.affix.data-api",n.proxy(this.checkPosition,this)).on("click.bs.affix.data-api",n.proxy(this.checkPositionWithEventLoop,this));this.$element=n(i);this.affixed=this.unpin=this.pinnedOffset=null;this.checkPosition()},r;t.VERSION="3.2.0";t.RESET="affix affix-top affix-bottom";t.DEFAULTS={offset:0,target:window};t.prototype.getPinnedOffset=function(){if(this.pinnedOffset)return this.pinnedOffset;this.$element.removeClass(t.RESET).addClass("affix");var n=this.$target.scrollTop(),i=this.$element.offset();return this.pinnedOffset=i.top-n};t.prototype.checkPositionWithEventLoop=function(){setTimeout(n.proxy(this.checkPosition,this),1)};t.prototype.checkPosition=function(){var i,e,o;if(this.$element.is(":visible")){var s=n(document).height(),h=this.$target.scrollTop(),c=this.$element.offset(),r=this.options.offset,f=r.top,u=r.bottom;(typeof r!="object"&&(u=f=r),typeof f=="function"&&(f=r.top(this.$element)),typeof u=="function"&&(u=r.bottom(this.$element)),i=this.unpin!=null&&h+this.unpin<=c.top?!1:u!=null&&c.top+this.$element.height()>=s-u?"bottom":f!=null&&h<=f?"top":!1,this.affixed!==i)&&((this.unpin!=null&&this.$element.css("top",""),e="affix"+(i?"-"+i:""),o=n.Event(e+".bs.affix"),this.$element.trigger(o),o.isDefaultPrevented())||(this.affixed=i,this.unpin=i=="bottom"?this.getPinnedOffset():null,this.$element.removeClass(t.RESET).addClass(e).trigger(n.Event(e.replace("affix","affixed"))),i=="bottom"&&this.$element.offset({top:s-this.$element.height()-u})))}};r=n.fn.affix;n.fn.affix=i;n.fn.affix.Constructor=t;n.fn.affix.noConflict=function(){return n.fn.affix=r,this};n(window).on("load",function(){n('[data-spy="affix"]').each(function(){var r=n(this),t=r.data();t.offset=t.offset||{};t.offsetBottom&&(t.offset.bottom=t.offsetBottom);t.offsetTop&&(t.offset.top=t.offsetTop);i.call(r,t)})})}(jQuery)