var Todo=function(){var t=function(){$(".todo-taskbody-due").datepicker({rtl:Metronic.isRTL(),orientation:"left",autoclose:!0});$(".todo-taskbody-tags").select2({tags:["Testing","Important","Info","Pending","Completed","Requested","Approved"]})},n=function(){Metronic.getViewPort().width<=992?$(".todo-project-list-content").addClass("collapse"):$(".todo-project-list-content").removeClass("collapse").css("height","auto")};return{init:function(){t();n();Metronic.addResizeHandler(function(){n()})}}}()