var ComponentsContextMenu=function(){var n=function(){$("#main").contextmenu({target:"#context-menu2",before:function(n){return(n.preventDefault(),n.target.tagName=="SPAN")?(n.preventDefault(),this.closemenu(),!1):!0}})},t=function(){$("#context2").contextmenu({target:"#context-menu2",onItem:function(n,t){alert($(t.target).text())}});$("#context-menu2").on("show.bs.context",function(){console.log("before show event")});$("#context-menu2").on("shown.bs.context",function(){console.log("after show event")});$("#context-menu2").on("hide.bs.context",function(){console.log("before hide event")});$("#context-menu2").on("hidden.bs.context",function(){console.log("after hide event")})};return{init:function(){n();t()}}}()