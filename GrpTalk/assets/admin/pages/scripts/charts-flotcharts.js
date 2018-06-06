var ChartsFlotcharts=function(){return{init:function(){Metronic.addResizeHandler(function(){Charts.initPieCharts()})},initCharts:function(){function t(){var f,t,u,r;for(n.length>0&&(n=n.slice(1));n.length<i;)f=n.length>0?n[n.length-1]:50,t=f+Math.random()*10-5,t<0&&(t=0),t>100&&(t=100),n.push(t);for(u=[],r=0;r<n.length;++r)u.push([r,n[r]]);return u}function r(){var t,i,r,n;if($("#chart_1").size()==1){for(t=[],n=0;n<Math.PI*2;n+=.25)t.push([n,Math.sin(n)]);for(i=[],n=0;n<Math.PI*2;n+=.25)i.push([n,Math.cos(n)]);for(r=[],n=0;n<Math.PI*2;n+=.1)r.push([n,Math.tan(n)]);$.plot($("#chart_1"),[{label:"sin(x)",data:t,lines:{lineWidth:1},shadowSize:0},{label:"cos(x)",data:i,lines:{lineWidth:1},shadowSize:0},{label:"tan(x)",data:r,lines:{lineWidth:1},shadowSize:0}],{series:{lines:{show:!0},points:{show:!0,fill:!0,radius:3,lineWidth:1}},xaxis:{tickColor:"#eee",ticks:[0,[Math.PI/2,"π/2"],[Math.PI,"π"],[Math.PI*3/2,"3π/2"],[Math.PI*2,"2π"]]},yaxis:{tickColor:"#eee",ticks:10,min:-2,max:2},grid:{borderColor:"#eee",borderWidth:1}})}}function u(){function n(){return Math.floor(Math.random()*21)+20}function u(n,t,i){$('<div id="tooltip">'+i+"<\/div>").css({position:"absolute",display:"none",top:t+5,left:n+15,border:"1px solid #333",padding:"4px",color:"#fff","border-radius":"3px","background-color":"#333",opacity:.8}).appendTo("body").fadeIn(200)}var t;if($("#chart_2").size()==1){var i=[[1,n()],[2,n()],[3,2+n()],[4,3+n()],[5,5+n()],[6,10+n()],[7,15+n()],[8,20+n()],[9,25+n()],[10,30+n()],[11,35+n()],[12,25+n()],[13,15+n()],[14,20+n()],[15,45+n()],[16,50+n()],[17,65+n()],[18,70+n()],[19,85+n()],[20,80+n()],[21,75+n()],[22,80+n()],[23,75+n()],[24,70+n()],[25,65+n()],[26,75+n()],[27,80+n()],[28,85+n()],[29,90+n()],[30,95+n()]],r=[[1,n()-5],[2,n()-5],[3,n()-5],[4,6+n()],[5,5+n()],[6,20+n()],[7,25+n()],[8,36+n()],[9,26+n()],[10,38+n()],[11,39+n()],[12,50+n()],[13,51+n()],[14,12+n()],[15,13+n()],[16,14+n()],[17,15+n()],[18,15+n()],[19,16+n()],[20,17+n()],[21,18+n()],[22,19+n()],[23,20+n()],[24,21+n()],[25,14+n()],[26,24+n()],[27,25+n()],[28,26+n()],[29,27+n()],[30,31+n()]],f=$.plot($("#chart_2"),[{data:i,label:"Unique Visits",lines:{lineWidth:1},shadowSize:0},{data:r,label:"Page Views",lines:{lineWidth:1},shadowSize:0}],{series:{lines:{show:!0,lineWidth:2,fill:!0,fillColor:{colors:[{opacity:.05},{opacity:.01}]}},points:{show:!0,radius:3,lineWidth:1},shadowSize:2},grid:{hoverable:!0,clickable:!0,tickColor:"#eee",borderColor:"#eee",borderWidth:1},colors:["#d12610","#37b7f3","#52e136"],xaxis:{ticks:11,tickDecimals:0,tickColor:"#eee"},yaxis:{ticks:11,tickDecimals:0,tickColor:"#eee"}});t=null;$("#chart_2").bind("plothover",function(n,i,r){if($("#x").text(i.x.toFixed(2)),$("#y").text(i.y.toFixed(2)),r){if(t!=r.dataIndex){t=r.dataIndex;$("#tooltip").remove();var f=r.datapoint[0].toFixed(2),e=r.datapoint[1].toFixed(2);u(r.pageX,r.pageY,r.series.label+" of "+f+" = "+e)}}else $("#tooltip").remove(),t=null})}}function f(){function e(){var n,o,s,r,c,e,l,i,h;if(t=null,n=f,o=plot.getAxes(),!(n.x<o.xaxis.min)&&!(n.x>o.xaxis.max)&&!(n.y<o.yaxis.min)&&!(n.y>o.yaxis.max))for(c=plot.getData(),s=0;s<c.length;++s){for(e=c[s],r=0;r<e.data.length;++r)if(e.data[r][0]>n.x)break;i=e.data[r-1];h=e.data[r];l=i==null?h[1]:h==null?i[1]:i[1]+(h[1]-i[1])*(n.x-i[0])/(h[0]-i[0]);u.eq(s).text(e.label.replace(/=.*/,"= "+l.toFixed(2)))}}var i,r,n,u,t,f;if($("#chart_3").size()==1){for(i=[],r=[],n=0;n<14;n+=.1)i.push([n,Math.sin(n)]),r.push([n,Math.cos(n)]);plot=$.plot($("#chart_3"),[{data:i,label:"sin(x) = -0.00",lines:{lineWidth:1},shadowSize:0},{data:r,label:"cos(x) = -0.00",lines:{lineWidth:1},shadowSize:0}],{series:{lines:{show:!0}},crosshair:{mode:"x"},grid:{hoverable:!0,autoHighlight:!1,tickColor:"#eee",borderColor:"#eee",borderWidth:1},yaxis:{min:-1.2,max:1.2}});u=$("#chart_3 .legendLabel");u.each(function(){$(this).css("width",$(this).width())});t=null;f=null;$("#chart_3").bind("plothover",function(n,i){f=i;t||(t=setTimeout(e,50))})}}function e(){function i(){n.setData([t()]);n.draw();setTimeout(i,u)}if($("#chart_4").size()==1){var r={series:{shadowSize:1},lines:{show:!0,lineWidth:.5,fill:!0,fillColor:{colors:[{opacity:.1},{opacity:1}]}},yaxis:{min:0,max:100,tickColor:"#eee",tickFormatter:function(n){return n+"%"}},xaxis:{show:!1},colors:["#6ef146"],grid:{tickColor:"#eee",borderWidth:0}},u=30,n=$.plot($("#chart_4"),[t()],r);i()}}function o(){function u(){$.plot($("#chart_5"),[{label:"sales",data:t,lines:{lineWidth:1},shadowSize:0},{label:"tax",data:i,lines:{lineWidth:1},shadowSize:0},{label:"profit",data:r,lines:{lineWidth:1},shadowSize:0}],{series:{stack:f,lines:{show:o,fill:!0,steps:s,lineWidth:0},bars:{show:e,barWidth:.5,lineWidth:0,shadowSize:0,align:"center"}},grid:{tickColor:"#eee",borderColor:"#eee",borderWidth:1}})}var t,i,r,n;if($("#chart_5").size()==1){for(t=[],n=0;n<=10;n+=1)t.push([n,parseInt(Math.random()*30)]);for(i=[],n=0;n<=10;n+=1)i.push([n,parseInt(Math.random()*30)]);for(r=[],n=0;n<=10;n+=1)r.push([n,parseInt(Math.random()*30)]);var f=0,e=!0,o=!1,s=!1;$(".stackControls input").click(function(n){n.preventDefault();f=$(this).val()=="With stacking"?!0:null;u()});$(".graphControls input").click(function(n){n.preventDefault();e=$(this).val().indexOf("Bars")!=-1;o=$(this).val().indexOf("Lines")!=-1;s=$(this).val().indexOf("steps")!=-1;u()});u()}}if(jQuery.plot){var n=[],i=250;r();u();f();e();o()}},initBarCharts:function(){function u(n){var r=[],t=100+n,u=200+n,f;for(i=1;i<=20;i++)f=Math.floor(Math.random()*(u-t+1)+t),r.push([i,f]),t++,u++;return r}var r=u(0),n={series:{bars:{show:!0}},bars:{barWidth:.8,lineWidth:0,shadowSize:0,align:"left"},grid:{tickColor:"#eee",borderColor:"#eee",borderWidth:1}},t;$("#chart_1_1").size()!==0&&$.plot($("#chart_1_1"),[{data:r,lines:{lineWidth:1},shadowSize:0}],n);t=[[10,10],[20,20],[30,30],[40,40],[50,50]];n={series:{bars:{show:!0}},bars:{horizontal:!0,barWidth:6,lineWidth:0,shadowSize:0,align:"left"},grid:{tickColor:"#eee",borderColor:"#eee",borderWidth:1}};$("#chart_1_2").size()!==0&&$.plot($("#chart_1_2"),[t],n)},initPieCharts:function(){function r(n,t,i){i&&(percent=parseFloat(i.series.percent).toFixed(2),$("#hover").html('<span style="font-weight: bold; color: '+i.series.color+'">'+i.series.label+" ("+percent+"%)<\/span>"))}function u(n,t,i){i&&(percent=parseFloat(i.series.percent).toFixed(2),alert(""+i.series.label+": "+percent+"%"))}for(var n=[],i=Math.floor(Math.random()*10)+1,i=i<5?5:i,t=0;t<i;t++)n[t]={label:"Series"+(t+1),data:Math.floor(Math.random()*100)+1};$("#pie_chart").size()!==0&&$.plot($("#pie_chart"),n,{series:{pie:{show:!0}}});$("#pie_chart_1").size()!==0&&$.plot($("#pie_chart_1"),n,{series:{pie:{show:!0}},legend:{show:!1}});$("#pie_chart_2").size()!==0&&$.plot($("#pie_chart_2"),n,{series:{pie:{show:!0,radius:1,label:{show:!0,radius:1,formatter:function(n,t){return'<div style="font-size:8pt;text-align:center;padding:2px;color:white;">'+n+"<br/>"+Math.round(t.percent)+"%<\/div>"},background:{opacity:.8}}}},legend:{show:!1}});$("#pie_chart_3").size()!==0&&$.plot($("#pie_chart_3"),n,{series:{pie:{show:!0,radius:1,label:{show:!0,radius:3/4,formatter:function(n,t){return'<div style="font-size:8pt;text-align:center;padding:2px;color:white;">'+n+"<br/>"+Math.round(t.percent)+"%<\/div>"},background:{opacity:.5}}}},legend:{show:!1}});$("#pie_chart_4").size()!==0&&$.plot($("#pie_chart_4"),n,{series:{pie:{show:!0,radius:1,label:{show:!0,radius:3/4,formatter:function(n,t){return'<div style="font-size:8pt;text-align:center;padding:2px;color:white;">'+n+"<br/>"+Math.round(t.percent)+"%<\/div>"},background:{opacity:.5,color:"#000"}}}},legend:{show:!1}});$("#pie_chart_5").size()!==0&&$.plot($("#pie_chart_5"),n,{series:{pie:{show:!0,radius:3/4,label:{show:!0,radius:3/4,formatter:function(n,t){return'<div style="font-size:8pt;text-align:center;padding:2px;color:white;">'+n+"<br/>"+Math.round(t.percent)+"%<\/div>"},background:{opacity:.5,color:"#000"}}}},legend:{show:!1}});$("#pie_chart_6").size()!==0&&$.plot($("#pie_chart_6"),n,{series:{pie:{show:!0,radius:1,label:{show:!0,radius:2/3,formatter:function(n,t){return'<div style="font-size:8pt;text-align:center;padding:2px;color:white;">'+n+"<br/>"+Math.round(t.percent)+"%<\/div>"},threshold:.1}}},legend:{show:!1}});$("#pie_chart_7").size()!==0&&$.plot($("#pie_chart_7"),n,{series:{pie:{show:!0,combine:{color:"#999",threshold:.1}}},legend:{show:!1}});$("#pie_chart_8").size()!==0&&$.plot($("#pie_chart_8"),n,{series:{pie:{show:!0,radius:300,label:{show:!0,formatter:function(n,t){return'<div style="font-size:8pt;text-align:center;padding:2px;color:white;">'+n+"<br/>"+Math.round(t.percent)+"%<\/div>"},threshold:.1}}},legend:{show:!1}});$("#pie_chart_9").size()!==0&&$.plot($("#pie_chart_9"),n,{series:{pie:{show:!0,radius:1,tilt:.5,label:{show:!0,radius:1,formatter:function(n,t){return'<div style="font-size:8pt;text-align:center;padding:2px;color:white;">'+n+"<br/>"+Math.round(t.percent)+"%<\/div>"},background:{opacity:.8}},combine:{color:"#999",threshold:.1}}},legend:{show:!1}});$("#donut").size()!==0&&$.plot($("#donut"),n,{series:{pie:{innerRadius:.5,show:!0}}});$("#interactive").size()!==0&&($.plot($("#interactive"),n,{series:{pie:{show:!0}},grid:{hoverable:!0,clickable:!0}}),$("#interactive").bind("plothover",r),$("#interactive").bind("plotclick",u))}}}()