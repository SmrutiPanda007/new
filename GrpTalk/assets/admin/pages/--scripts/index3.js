var Index=function(){var n=null;return{init:function(){Metronic.addResizeHandler(function(){jQuery(".vmaps").each(function(){var n=jQuery(this);n.width(n.parent().width())})});Index.initCharts();Index.initMiniCharts();Index.initJQVMAP()},initJQVMAP:function(){var n=function(n){jQuery(".vmaps").hide();jQuery("#vmap_"+n).show()},t=function(n){var i={map:"world_en",backgroundColor:null,borderColor:"#333333",borderOpacity:.5,borderWidth:1,color:"#c6c6c6",enableZoom:!0,hoverColor:"#c9dfaf",hoverOpacity:null,values:sample_data,normalizeFunction:"linear",scaleColors:["#b6da93","#909cae"],selectedColor:"#c9dfaf",selectedRegion:null,showTooltip:!0,onLabelShow:function(){},onRegionOver:function(n,t){t=="ca"&&n.preventDefault()},onRegionClick:function(n,t,i){var r='You clicked "'+i+'" which has the code: '+t.toUpperCase();alert(r)}},t;(i.map=n+"_en",t=jQuery("#vmap_"+n),t)&&(t.width(t.parent().parent().width()),t.show(),t.vectorMap(i),t.hide())};t("world");t("usa");t("europe");t("russia");t("germany");n("world");jQuery("#regional_stat_world").click(function(){n("world")});jQuery("#regional_stat_usa").click(function(){n("usa")});jQuery("#regional_stat_europe").click(function(){n("europe")});jQuery("#regional_stat_russia").click(function(){n("russia")});jQuery("#regional_stat_germany").click(function(){n("germany")});$("#region_statistics_loading").hide();$("#region_statistics_content").show()},initCharts:function(){Morris.EventEmitter&&(n=Morris.Area({element:"sales_statistics",padding:0,behaveLikeLine:!1,gridEnabled:!1,gridLineColor:!1,axes:!1,fillOpacity:1,data:[{period:"2011 Q1",sales:1400,profit:400},{period:"2011 Q2",sales:1100,profit:600},{period:"2011 Q3",sales:1600,profit:500},{period:"2011 Q4",sales:1200,profit:400},{period:"2012 Q1",sales:1550,profit:800}],lineColors:["#399a8c","#92e9dc"],xkey:"period",ykeys:["sales","profit"],labels:["Sales","Profit"],pointSize:0,lineWidth:0,hideHover:"auto",resize:!0}))},redrawCharts:function(){n.resizeHandler()},initMiniCharts:function(){Metronic.isIE8()&&!Function.prototype.bind&&(Function.prototype.bind=function(n){if(typeof this!="function")throw new TypeError("Function.prototype.bind - what is trying to be bound is not callable");var r=Array.prototype.slice.call(arguments,1),u=this,t=function(){},i=function(){return u.apply(this instanceof t&&n?this:n,r.concat(Array.prototype.slice.call(arguments)))};return t.prototype=this.prototype,i.prototype=new t,i});$("#sparkline_bar").sparkline([8,9,10,11,10,10,12,10,10,11,9,12,11],{type:"bar",width:"100",barWidth:6,height:"45",barColor:"#F36A5B",negBarColor:"#e02222"});$("#sparkline_bar2").sparkline([9,11,12,13,12,13,10,14,13,11,11,12,11],{type:"bar",width:"100",barWidth:6,height:"45",barColor:"#5C9BD1",negBarColor:"#e02222"})}}}()