var ComponentsPickers=function(){var n=function(){jQuery().datepicker&&$(".date-picker").datepicker({rtl:Metronic.isRTL(),orientation:"left",autoclose:!0})},t=function(){if(jQuery().timepicker){$(".timepicker-default").timepicker({autoclose:!0,showSeconds:!0,minuteStep:1});$(".timepicker-no-seconds").timepicker({autoclose:!0,minuteStep:5});$(".timepicker-24").timepicker({autoclose:!0,minuteStep:5,showSeconds:!1,showMeridian:!1});$(".timepicker").parent(".input-group").on("click",".input-group-btn",function(n){n.preventDefault();$(this).parent(".input-group").find(".timepicker").timepicker("showWidget")})}},i=function(){if(alert("a"),jQuery().daterangepicker){$("#defaultrange").daterangepicker({opens:Metronic.isRTL()?"left":"right",format:"MM/DD/YYYY",separator:" to ",startDate:moment().subtract("days",29),endDate:moment(),minDate:"01/01/2012",maxDate:"12/31/2018"},function(n,t){$("#defaultrange input").val(n.format("MMMM D, YYYY")+" - "+t.format("MMMM D, YYYY"))});$("#defaultrange_modal").daterangepicker({opens:Metronic.isRTL()?"left":"right",format:"MM/DD/YYYY",separator:" to ",startDate:moment().subtract("days",29),endDate:moment(),minDate:"01/01/2012",maxDate:"12/31/2018"},function(n,t){$("#defaultrange_modal input").val(n.format("MMMM D, YYYY")+" - "+t.format("MMMM D, YYYY"))});$("#defaultrange_modal").on("click",function(){$("#daterangepicker_modal").is(":visible")&&$("body").hasClass("modal-open")==!1&&$("body").addClass("modal-open")});$("#reportrange").daterangepicker({opens:Metronic.isRTL()?"left":"right",startDate:moment().subtract("days",29),endDate:moment(),minDate:"01/01/2012",maxDate:"12/31/2014",dateLimit:{days:60},showDropdowns:!0,showWeekNumbers:!0,timePicker:!1,timePickerIncrement:1,timePicker12Hour:!0,ranges:{Today:[moment(),moment()],Yesterday:[moment().subtract("days",1),moment().subtract("days",1)],"Last 7 Days":[moment().subtract("days",6),moment()],"Last 30 Days":[moment().subtract("days",29),moment()],"This Month":[moment().startOf("month"),moment().endOf("month")],"Last Month":[moment().subtract("month",1).startOf("month"),moment().subtract("month",1).endOf("month")]},buttonClasses:["btn"],applyClass:"green",cancelClass:"default",format:"MM/DD/YYYY",separator:" to ",locale:{applyLabel:"Apply",fromLabel:"From",toLabel:"To",customRangeLabel:"Custom Range",daysOfWeek:["Su","Mo","Tu","We","Th","Fr","Sa"],monthNames:["January","February","March","April","May","June","July","August","September","October","November","December"],firstDay:1}},function(n,t){$("#reportrange span").html(n.format("MMMM D, YYYY")+" - "+t.format("MMMM D, YYYY"))});$("#reportrange span").html(moment().subtract("days",29).format("MMMM D, YYYY")+" - "+moment().format("MMMM D, YYYY"))}},r=function(){jQuery().datetimepicker&&($(".form_datetime").datetimepicker({autoclose:!0,isRTL:Metronic.isRTL(),format:"dd MM yyyy - hh:ii",pickerPosition:Metronic.isRTL()?"bottom-right":"bottom-left"}),$(".form_advance_datetime").datetimepicker({isRTL:Metronic.isRTL(),format:"dd MM yyyy - hh:ii",autoclose:!0,todayBtn:!0,startDate:"2013-02-14 10:00",pickerPosition:Metronic.isRTL()?"bottom-right":"bottom-left",minuteStep:10}),$(".form_meridian_datetime").datetimepicker({isRTL:Metronic.isRTL(),format:"dd MM yyyy - HH:ii P",showMeridian:!0,autoclose:!0,pickerPosition:Metronic.isRTL()?"bottom-right":"bottom-left",todayBtn:!0}),$("body").removeClass("modal-open"))},u=function(){jQuery().clockface&&($(".clockface_1").clockface(),$("#clockface_2").clockface({format:"HH:mm",trigger:"manual"}),$("#clockface_2_toggle").click(function(n){n.stopPropagation();$("#clockface_2").clockface("toggle")}),$("#clockface_2_modal").clockface({format:"HH:mm",trigger:"manual"}),$("#clockface_2_modal_toggle").click(function(n){n.stopPropagation();$("#clockface_2_modal").clockface("toggle")}),$(".clockface_3").clockface({format:"H:mm"}).clockface("show","14:30"))},f=function(){jQuery().colorpicker&&($(".colorpicker-default").colorpicker({format:"hex"}),$(".colorpicker-rgba").colorpicker())};return{init:function(){n();t();r();i();u();f()}}}()