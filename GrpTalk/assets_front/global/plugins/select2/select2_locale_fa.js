(function(n){"use strict";n.extend(n.fn.select2.defaults,{formatMatches:function(n){return n+" نتیجه موجود است، کلیدهای جهت بالا و پایین را برای گشتن استفاده کنید."},formatNoMatches:function(){return"نتیجه‌ای یافت نشد."},formatInputTooShort:function(n,t){var i=t-n.length;return"لطفاً "+i+" نویسه بیشتر وارد نمایید"},formatInputTooLong:function(n,t){var i=n.length-t;return"لطفاً "+i+" نویسه را حذف کنید."},formatSelectionTooBig:function(n){return"شما فقط می‌توانید "+n+" مورد را انتخاب کنید"},formatLoadMore:function(){return"در حال بارگیری موارد بیشتر…"},formatSearching:function(){return"در حال جستجو…"}})})(jQuery)