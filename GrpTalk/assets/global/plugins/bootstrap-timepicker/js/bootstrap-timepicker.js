/*!
 * Timepicker Component for Twitter Bootstrap
 *
 * Copyright 2013 Joris de Wit
 *
 * Contributors https://github.com/jdewit/bootstrap-timepicker/graphs/contributors
 *
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 */
(function(n,t,i){"use strict";var r=function(t,i){this.widget="";this.$element=n(t);this.defaultTime=i.defaultTime;this.disableFocus=i.disableFocus;this.disableMousewheel=i.disableMousewheel;this.isOpen=i.isOpen;this.minuteStep=i.minuteStep;this.modalBackdrop=i.modalBackdrop;this.orientation=i.orientation;this.secondStep=i.secondStep;this.showInputs=i.showInputs;this.showMeridian=i.showMeridian;this.showSeconds=i.showSeconds;this.template=i.template;this.appendWidgetTo=i.appendWidgetTo;this.showWidgetOnAddonClick=i.showWidgetOnAddonClick;this._init()};r.prototype={constructor:r,_init:function(){var t=this;if(this.showWidgetOnAddonClick&&(this.$element.parent().hasClass("input-append")||this.$element.parent().hasClass("input-prepend"))){this.$element.parent(".input-append, .input-prepend").find(".add-on").on({"click.timepicker":n.proxy(this.showWidget,this)});this.$element.on({"focus.timepicker":n.proxy(this.highlightUnit,this),"click.timepicker":n.proxy(this.highlightUnit,this),"keydown.timepicker":n.proxy(this.elementKeydown,this),"blur.timepicker":n.proxy(this.blurElement,this),"mousewheel.timepicker DOMMouseScroll.timepicker":n.proxy(this.mousewheel,this)})}else if(this.template)this.$element.on({"focus.timepicker":n.proxy(this.showWidget,this),"click.timepicker":n.proxy(this.showWidget,this),"blur.timepicker":n.proxy(this.blurElement,this),"mousewheel.timepicker DOMMouseScroll.timepicker":n.proxy(this.mousewheel,this)});else this.$element.on({"focus.timepicker":n.proxy(this.highlightUnit,this),"click.timepicker":n.proxy(this.highlightUnit,this),"keydown.timepicker":n.proxy(this.elementKeydown,this),"blur.timepicker":n.proxy(this.blurElement,this),"mousewheel.timepicker DOMMouseScroll.timepicker":n.proxy(this.mousewheel,this)});this.$widget=this.template!==!1?n(this.getTemplate()).on("click",n.proxy(this.widgetClick,this)):!1;this.showInputs&&this.$widget!==!1&&this.$widget.find("input").each(function(){n(this).on({"click.timepicker":function(){n(this).select()},"keydown.timepicker":n.proxy(t.widgetKeydown,t),"keyup.timepicker":n.proxy(t.widgetKeyup,t)})});this.setDefaultTime(this.defaultTime)},blurElement:function(){this.highlightedUnit=null;this.updateFromElementVal()},clear:function(){this.hour="";this.minute="";this.second="";this.meridian="";this.$element.val("")},decrementHour:function(){if(this.showMeridian)if(this.hour===1)this.hour=12;else{if(this.hour===12)return this.hour--,this.toggleMeridian();if(this.hour===0)return this.hour=11,this.toggleMeridian();this.hour--}else this.hour<=0?this.hour=23:this.hour--},decrementMinute:function(n){var t;t=n?this.minute-n:this.minute-this.minuteStep;t<0?(this.decrementHour(),this.minute=t+60):this.minute=t},decrementSecond:function(){var n=this.second-this.secondStep;n<0?(this.decrementMinute(!0),this.second=n+60):this.second=n},elementKeydown:function(n){switch(n.keyCode){case 9:case 27:this.updateFromElementVal();break;case 37:n.preventDefault();this.highlightPrevUnit();break;case 38:n.preventDefault();switch(this.highlightedUnit){case"hour":this.incrementHour();this.highlightHour();break;case"minute":this.incrementMinute();this.highlightMinute();break;case"second":this.incrementSecond();this.highlightSecond();break;case"meridian":this.toggleMeridian();this.highlightMeridian()}this.update();break;case 39:n.preventDefault();this.highlightNextUnit();break;case 40:n.preventDefault();switch(this.highlightedUnit){case"hour":this.decrementHour();this.highlightHour();break;case"minute":this.decrementMinute();this.highlightMinute();break;case"second":this.decrementSecond();this.highlightSecond();break;case"meridian":this.toggleMeridian();this.highlightMeridian()}this.update()}},getCursorPosition:function(){var n=this.$element.get(0),t,r;return"selectionStart"in n?n.selectionStart:i.selection?(n.focus(),t=i.selection.createRange(),r=i.selection.createRange().text.length,t.moveStart("character",-n.value.length),t.text.length-r):void 0},getTemplate:function(){var n,t,i,r,u,f;this.showInputs?(t='<input type="text" class="bootstrap-timepicker-hour" maxlength="2"/>',i='<input type="text" class="bootstrap-timepicker-minute" maxlength="2"/>',r='<input type="text" class="bootstrap-timepicker-second" maxlength="2"/>',u='<input type="text" class="bootstrap-timepicker-meridian" maxlength="2"/>'):(t='<span class="bootstrap-timepicker-hour"><\/span>',i='<span class="bootstrap-timepicker-minute"><\/span>',r='<span class="bootstrap-timepicker-second"><\/span>',u='<span class="bootstrap-timepicker-meridian"><\/span>');f='<table><tr><td><a href="#" data-action="incrementHour"><i class="fa fa-angle-up"><\/i><\/a><\/td><td class="separator">&nbsp;<\/td><td><a href="#" data-action="incrementMinute"><i class="fa fa-angle-up"><\/i><\/a><\/td>'+(this.showSeconds?'<td class="separator">&nbsp;<\/td><td><a href="#" data-action="incrementSecond"><i class="fa fa-angle-up"><\/i><\/a><\/td>':"")+(this.showMeridian?'<td class="separator">&nbsp;<\/td><td class="meridian-column"><a href="#" data-action="toggleMeridian"><i class="fa fa-angle-up"><\/i><\/a><\/td>':"")+"<\/tr><tr><td>"+t+'<\/td> <td class="separator">:<\/td><td>'+i+"<\/td> "+(this.showSeconds?'<td class="separator">:<\/td><td>'+r+"<\/td>":"")+(this.showMeridian?'<td class="separator">&nbsp;<\/td><td>'+u+"<\/td>":"")+'<\/tr><tr><td><a href="#" data-action="decrementHour"><i class="fa fa-angle-down"><\/i><\/a><\/td><td class="separator"><\/td><td><a href="#" data-action="decrementMinute"><i class="fa fa-angle-down"><\/i><\/a><\/td>'+(this.showSeconds?'<td class="separator">&nbsp;<\/td><td><a href="#" data-action="decrementSecond"><i class="fa fa-angle-down"><\/i><\/a><\/td>':"")+(this.showMeridian?'<td class="separator">&nbsp;<\/td><td><a href="#" data-action="toggleMeridian"><i class="fa fa-angle-down"><\/i><\/a><\/td>':"")+"<\/tr><\/table>";switch(this.template){case"modal":n='<div class="bootstrap-timepicker-widget modal hide fade in" data-backdrop="'+(this.modalBackdrop?"true":"false")+'"><div class="modal-header"><a href="#" class="close" data-dismiss="modal">×<\/a><h3>Pick a Time<\/h3><\/div><div class="modal-content">'+f+'<\/div><div class="modal-footer"><a href="#" class="btn btn-primary" data-dismiss="modal">OK<\/a><\/div><\/div>';break;case"dropdown":n='<div class="bootstrap-timepicker-widget dropdown-menu">'+f+"<\/div>"}return n},getTime:function(){return this.hour===""?"":this.hour+":"+(this.minute.toString().length===1?"0"+this.minute:this.minute)+(this.showSeconds?":"+(this.second.toString().length===1?"0"+this.second:this.second):"")+(this.showMeridian?" "+this.meridian:"")},hideWidget:function(){this.isOpen!==!1&&(this.$element.trigger({type:"hide.timepicker",time:{value:this.getTime(),hours:this.hour,minutes:this.minute,seconds:this.second,meridian:this.meridian}}),this.template==="modal"&&this.$widget.modal?this.$widget.modal("hide"):this.$widget.removeClass("open"),n(i).off("mousedown.timepicker, touchend.timepicker"),this.isOpen=!1,this.$widget.detach())},highlightUnit:function(){this.position=this.getCursorPosition();this.position>=0&&this.position<=2?this.highlightHour():this.position>=3&&this.position<=5?this.highlightMinute():this.position>=6&&this.position<=8?this.showSeconds?this.highlightSecond():this.highlightMeridian():this.position>=9&&this.position<=11&&this.highlightMeridian()},highlightNextUnit:function(){switch(this.highlightedUnit){case"hour":this.highlightMinute();break;case"minute":this.showSeconds?this.highlightSecond():this.showMeridian?this.highlightMeridian():this.highlightHour();break;case"second":this.showMeridian?this.highlightMeridian():this.highlightHour();break;case"meridian":this.highlightHour()}},highlightPrevUnit:function(){switch(this.highlightedUnit){case"hour":this.showMeridian?this.highlightMeridian():this.showSeconds?this.highlightSecond():this.highlightMinute();break;case"minute":this.highlightHour();break;case"second":this.highlightMinute();break;case"meridian":this.showSeconds?this.highlightSecond():this.highlightMinute()}},highlightHour:function(){var n=this.$element.get(0),t=this;this.highlightedUnit="hour";n.setSelectionRange&&setTimeout(function(){t.hour<10?n.setSelectionRange(0,1):n.setSelectionRange(0,2)},0)},highlightMinute:function(){var n=this.$element.get(0),t=this;this.highlightedUnit="minute";n.setSelectionRange&&setTimeout(function(){t.hour<10?n.setSelectionRange(2,4):n.setSelectionRange(3,5)},0)},highlightSecond:function(){var n=this.$element.get(0),t=this;this.highlightedUnit="second";n.setSelectionRange&&setTimeout(function(){t.hour<10?n.setSelectionRange(5,7):n.setSelectionRange(6,8)},0)},highlightMeridian:function(){var n=this.$element.get(0),t=this;this.highlightedUnit="meridian";n.setSelectionRange&&(this.showSeconds?setTimeout(function(){t.hour<10?n.setSelectionRange(8,10):n.setSelectionRange(9,11)},0):setTimeout(function(){t.hour<10?n.setSelectionRange(5,7):n.setSelectionRange(6,8)},0))},incrementHour:function(){if(this.showMeridian){if(this.hour===11)return this.hour++,this.toggleMeridian();this.hour===12&&(this.hour=0)}if(this.hour===23){this.hour=0;return}this.hour++},incrementMinute:function(n){var t;t=n?this.minute+n:this.minute+this.minuteStep-this.minute%this.minuteStep;t>59?(this.incrementHour(),this.minute=t-60):this.minute=t},incrementSecond:function(){var n=this.second+this.secondStep-this.second%this.secondStep;n>59?(this.incrementMinute(!0),this.second=n-60):this.second=n},mousewheel:function(t){if(!this.disableMousewheel){t.preventDefault();t.stopPropagation();var r=t.originalEvent.wheelDelta||-t.originalEvent.detail,i=null;t.type==="mousewheel"?i=t.originalEvent.wheelDelta*-1:t.type==="DOMMouseScroll"&&(i=40*t.originalEvent.detail);i&&(t.preventDefault(),n(this).scrollTop(i+n(this).scrollTop()));switch(this.highlightedUnit){case"minute":r>0?this.incrementMinute():this.decrementMinute();this.highlightMinute();break;case"second":r>0?this.incrementSecond():this.decrementSecond();this.highlightSecond();break;case"meridian":this.toggleMeridian();this.highlightMeridian();break;default:r>0?this.incrementHour():this.decrementHour();this.highlightHour()}return!1}},place:function(){var r,v,s;if(!this.isInline){var f=this.$widget.outerWidth(),e=this.$widget.outerHeight(),h=10,c=n(t).width(),y=n(t).height(),l=n(t).scrollTop(),p=parseInt(this.$element.parents().filter(function(){}).first().css("z-index"),10)+10,i=this.component?this.component.parent().offset():this.$element.offset(),a=this.component?this.component.outerHeight(!0):this.$element.outerHeight(!1),w=this.component?this.component.outerWidth(!0):this.$element.outerWidth(!1),u=i.left,o=i.top;this.$widget.removeClass("timepicker-orient-top timepicker-orient-bottom timepicker-orient-right timepicker-orient-left");this.orientation.x!=="auto"?(this.picker.addClass("datepicker-orient-"+this.orientation.x),this.orientation.x==="right"&&(u-=f-w)):(this.$widget.addClass("timepicker-orient-left"),i.left<0?u-=i.left-h:i.left+f>c&&(u=c-f-h));r=this.orientation.y;r==="auto"&&(v=-l+i.top-e,s=l+y-(i.top+a+e),r=Math.max(v,s)===s?"top":"bottom");this.$widget.addClass("timepicker-orient-"+r);r==="top"?o+=a:o-=e+parseInt(this.$widget.css("padding-top"),10);this.$widget.css({top:o,left:u,zIndex:p})}},remove:function(){n("document").off(".timepicker");this.$widget&&this.$widget.remove();delete this.$element.data().timepicker},setDefaultTime:function(n){if(this.$element.val())this.updateFromElementVal();else if(n==="current"){var r=new Date,t=r.getHours(),i=r.getMinutes(),u=r.getSeconds(),f="AM";u!==0&&(u=Math.ceil(r.getSeconds()/this.secondStep)*this.secondStep,u===60&&(i+=1,u=0));i!==0&&(i=Math.ceil(r.getMinutes()/this.minuteStep)*this.minuteStep,i===60&&(t+=1,i=0));this.showMeridian&&(t===0?t=12:t>=12?(t>12&&(t=t-12),f="PM"):f="AM");this.hour=t;this.minute=i;this.second=u;this.meridian=f;this.update()}else n===!1?(this.hour=0,this.minute=0,this.second=0,this.meridian="AM"):this.setTime(n)},setTime:function(n,t){if(!n){this.clear();return}var f,i,u,r,e;typeof n=="object"&&n.getMonth?(i=n.getHours(),u=n.getMinutes(),r=n.getSeconds(),this.showMeridian&&(e="AM",i>12&&(e="PM",i=i%12),i===12&&(e="PM"))):(e=n.match(/p/i)!==null?"PM":"AM",n=n.replace(/[^0-9\:]/g,""),f=n.split(":"),i=f[0]?f[0].toString():f.toString(),u=f[1]?f[1].toString():"",r=f[2]?f[2].toString():"",i.length>4&&(r=i.substr(4,2)),i.length>2&&(u=i.substr(2,2),i=i.substr(0,2)),u.length>2&&(r=u.substr(2,2),u=u.substr(0,2)),r.length>2&&(r=r.substr(2,2)),i=parseInt(i,10),u=parseInt(u,10),r=parseInt(r,10),isNaN(i)&&(i=0),isNaN(u)&&(u=0),isNaN(r)&&(r=0),this.showMeridian?i<1?i=1:i>12&&(i=12):(i>=24?i=23:i<0&&(i=0),i<13&&e==="PM"&&(i=i+12)),u<0?u=0:u>=60&&(u=59),this.showSeconds&&(isNaN(r)?r=0:r<0?r=0:r>=60&&(r=59)));this.hour=i;this.minute=u;this.second=r;this.meridian=e;this.update(t)},showWidget:function(){if(!this.isOpen&&!this.$element.is(":disabled")){this.$widget.appendTo(this.appendWidgetTo);var t=this;n(i).on("mousedown.timepicker, touchend.timepicker",function(n){t.$element.parent().find(n.target).length||t.$widget.is(n.target)||t.$widget.find(n.target).length||t.hideWidget()});if(this.$element.trigger({type:"show.timepicker",time:{value:this.getTime(),hours:this.hour,minutes:this.minute,seconds:this.second,meridian:this.meridian}}),this.place(),this.disableFocus&&this.$element.blur(),this.hour===""&&(this.defaultTime?this.setDefaultTime(this.defaultTime):this.setTime("0:0:0")),this.template==="modal"&&this.$widget.modal)this.$widget.modal("show").on("hidden",n.proxy(this.hideWidget,this));else this.isOpen===!1&&this.$widget.addClass("open");this.isOpen=!0}},toggleMeridian:function(){this.meridian=this.meridian==="AM"?"PM":"AM"},update:function(n){this.updateElement();n||this.updateWidget();this.$element.trigger({type:"changeTime.timepicker",time:{value:this.getTime(),hours:this.hour,minutes:this.minute,seconds:this.second,meridian:this.meridian}})},updateElement:function(){this.$element.val(this.getTime()).change()},updateFromElementVal:function(){this.setTime(this.$element.val())},updateWidget:function(){if(this.$widget!==!1){var n=this.hour,t=this.minute.toString().length===1?"0"+this.minute:this.minute,i=this.second.toString().length===1?"0"+this.second:this.second;this.showInputs?(this.$widget.find("input.bootstrap-timepicker-hour").val(n),this.$widget.find("input.bootstrap-timepicker-minute").val(t),this.showSeconds&&this.$widget.find("input.bootstrap-timepicker-second").val(i),this.showMeridian&&this.$widget.find("input.bootstrap-timepicker-meridian").val(this.meridian)):(this.$widget.find("span.bootstrap-timepicker-hour").text(n),this.$widget.find("span.bootstrap-timepicker-minute").text(t),this.showSeconds&&this.$widget.find("span.bootstrap-timepicker-second").text(i),this.showMeridian&&this.$widget.find("span.bootstrap-timepicker-meridian").text(this.meridian))}},updateFromWidgetInputs:function(){if(this.$widget!==!1){var n=this.$widget.find("input.bootstrap-timepicker-hour").val()+":"+this.$widget.find("input.bootstrap-timepicker-minute").val()+(this.showSeconds?":"+this.$widget.find("input.bootstrap-timepicker-second").val():"")+(this.showMeridian?this.$widget.find("input.bootstrap-timepicker-meridian").val():"");this.setTime(n,!0)}},widgetClick:function(t){t.stopPropagation();t.preventDefault();var i=n(t.target),r=i.closest("a").data("action");r&&this[r]();this.update();i.is("input")&&i.get(0).setSelectionRange(0,2)},widgetKeydown:function(t){var r=n(t.target),i=r.attr("class").replace("bootstrap-timepicker-","");switch(t.keyCode){case 9:if(this.showMeridian&&i==="meridian"||this.showSeconds&&i==="second"||!this.showMeridian&&!this.showSeconds&&i==="minute")return this.hideWidget();break;case 27:this.hideWidget();break;case 38:t.preventDefault();switch(i){case"hour":this.incrementHour();break;case"minute":this.incrementMinute();break;case"second":this.incrementSecond();break;case"meridian":this.toggleMeridian()}this.setTime(this.getTime());r.get(0).setSelectionRange(0,2);break;case 40:t.preventDefault();switch(i){case"hour":this.decrementHour();break;case"minute":this.decrementMinute();break;case"second":this.decrementSecond();break;case"meridian":this.toggleMeridian()}this.setTime(this.getTime());r.get(0).setSelectionRange(0,2)}},widgetKeyup:function(n){(n.keyCode===65||n.keyCode===77||n.keyCode===80||n.keyCode===46||n.keyCode===8||n.keyCode>=46&&n.keyCode<=57)&&this.updateFromWidgetInputs()}};n.fn.timepicker=function(t){var i=Array.apply(null,arguments);return i.shift(),this.each(function(){var f=n(this),u=f.data("timepicker"),e=typeof t=="object"&&t;u||f.data("timepicker",u=new r(this,n.extend({},n.fn.timepicker.defaults,e,n(this).data())));typeof t=="string"&&u[t].apply(u,i)})};n.fn.timepicker.defaults={defaultTime:"current",disableFocus:!1,disableMousewheel:!1,isOpen:!1,minuteStep:15,modalBackdrop:!1,orientation:{x:"auto",y:"auto"},secondStep:15,showSeconds:!1,showInputs:!0,showMeridian:!0,template:"dropdown",appendWidgetTo:"body",showWidgetOnAddonClick:!0};n.fn.timepicker.Constructor=r})(jQuery,window,document)