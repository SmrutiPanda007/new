(function(n){"use strict";n.extend(n.fn.select2.defaults,{formatNoMatches:function(){return"Nu a fost găsit nimic"},formatInputTooShort:function(n,t){var i=t-n.length;return"Vă rugăm să introduceți incă "+i+" caracter"+(i==1?"":"e")},formatInputTooLong:function(n,t){var i=n.length-t;return"Vă rugăm să introduceți mai puțin de "+i+" caracter"+(i==1?"":"e")},formatSelectionTooBig:function(n){return"Aveți voie să selectați cel mult "+n+" element"+(n==1?"":"e")},formatLoadMore:function(){return"Se încarcă…"},formatSearching:function(){return"Căutare…"}})})(jQuery)