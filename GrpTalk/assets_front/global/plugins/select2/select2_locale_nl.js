(function(n){"use strict";n.extend(n.fn.select2.defaults,{formatNoMatches:function(){return"Geen resultaten gevonden"},formatInputTooShort:function(n,t){var i=t-n.length;return"Vul "+i+" karakter"+(i==1?"":"s")+" meer in"},formatInputTooLong:function(n,t){var i=n.length-t;return"Vul "+i+" karakter"+(i==1?"":"s")+" minder in"},formatSelectionTooBig:function(n){return"Maximaal "+n+" item"+(n==1?"":"s")+" toegestaan"},formatLoadMore:function(){return"Meer resultaten laden…"},formatSearching:function(){return"Zoeken…"}})})(jQuery)