(function(n){"use strict";n.extend(n.fn.select2.defaults,{formatNoMatches:function(){return"No s'ha trobat cap coincidència"},formatInputTooShort:function(n,t){var i=t-n.length;return"Introduïu "+i+" caràcter"+(i==1?"":"s")+" més"},formatInputTooLong:function(n,t){var i=n.length-t;return"Introduïu "+i+" caràcter"+(i==1?"":"s")+"menys"},formatSelectionTooBig:function(n){return"Només podeu seleccionar "+n+" element"+(n==1?"":"s")},formatLoadMore:function(){return"S'estan carregant més resultats…"},formatSearching:function(){return"S'està cercant…"}})})(jQuery)