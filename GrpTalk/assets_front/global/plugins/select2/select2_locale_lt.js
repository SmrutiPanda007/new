(function(n){"use strict";function t(n){return" "+n+" simbol"+(n%100>9&&n%100<21||n%10==0?"ių":n%10>1?"ius":"į")}n.extend(n.fn.select2.defaults,{formatNoMatches:function(){return"Atitikmenų nerasta"},formatInputTooShort:function(n,i){return"Įrašykite dar"+t(i-n.length)},formatInputTooLong:function(n,i){return"Pašalinkite"+t(n.length-i)},formatSelectionTooBig:function(n){return"Jūs galite pasirinkti tik "+n+" element"+(n%100>9&&n%100<21||n%10==0?"ų":n%10>1?"us":"ą")},formatLoadMore:function(){return"Kraunama daugiau rezultatų…"},formatSearching:function(){return"Ieškoma…"}})})(jQuery)