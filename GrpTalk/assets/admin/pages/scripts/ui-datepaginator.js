var UIDatepaginator=function(){return{init:function(){$("#datepaginator_sample_1").datepaginator();$("#datepaginator_sample_2").datepaginator({size:"large"});$("#datepaginator_sample_3").datepaginator({size:"small"});$("#datepaginator_sample_4").datepaginator({onSelectedDateChanged:function(n,t){alert("Selected date: "+moment(t).format("Do, MMM YYYY"))}})}}}()